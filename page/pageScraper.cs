using System;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace xy.scraper.page
{
    public class pageScraper
    {
        protected IHtmlDownloader _htmlDownloader = new HttpClientDownloader();
        protected IHtmlParser _htmlParser;

        public pageScraper(IHtmlParser htmlParser)
        {
            _htmlParser = htmlParser;
            _htmlDownloader = new HttpClientDownloader();
        }

        public async Task<List<(string, (Type, Object?))>> download(
            string pUrl,
            CancellationToken token,
            IProgress<string> progress,
            string savePath
            )
        {
            progress.Report("get task html: " + pUrl);
            string htmlString = await _htmlDownloader.GetHtmlStringAsync(
                pUrl, _htmlParser.GetEncoding(), progress);

            Dictionary<string, string> downloadDict =
                _htmlParser.getDownloadDict(htmlString);
            List<(string, (Type, Object?))> retList = _htmlParser.getOtherPageDict(htmlString);
            progress.Report("get other page links: " + retList.Count);
            progress.Report("got download items:" + downloadDict.Count);

            try
            {
                await download(downloadDict, token, progress, savePath);
            }
            catch (OperationCanceledException e)
            {
                //save the retList to a file
                e.Data["retList"] = retList;
                throw e;
                //if (token.IsCancellationRequested)
                //{
                    //save the downloadDict to a file
                    //OperationCanceledException ex = new OperationCanceledException(token);
                    //ex.Data["savePath"] = e.Data["savePath"];
                    //ex.Data["retList"] = retList;
                    //ex.Data["downloadDict"] = e.Data["downloadDict"];
                    //throw ex;
                //}
            }

            return retList;
        }

        public async Task download(
            Dictionary<string, string> downloadDict,
            CancellationToken token,
            IProgress<string> progress,
            string savePath
            )
        {
            List<string> urlList = downloadDict.Keys.ToList();
            foreach (string dUrl in urlList)
            {
                if (token.IsCancellationRequested)
                {
                    progress.Report("\r\ncancel task, start save break point ... \r\n");

                    //save the downloadDict to a file
                    OperationCanceledException e = new OperationCanceledException(token);
                    e.Data["savePath"] = savePath;
                    e.Data["retList"] = null;
                    e.Data["downloadDict"] = downloadDict;
                    throw e;
                }
                try
                {
                    string fileFullName = savePath + downloadDict[dUrl];
                    //create driectory
                    string filePath = new FileInfo(fileFullName).Directory.FullName;
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    await _htmlDownloader.DownloadFileAsync(
                        dUrl, fileFullName, progress
                        );
                    progress.Report("Succeed: " + downloadDict[dUrl]);
                }
                catch (HttpRequestException e)
                {
                    if (((int?)e.StatusCode) == 424)
                    {
                        //The HTTP 424 Failed Dependency client error response status
                        //code indicates that the method could not be performed on the
                        //resource because the requested action depended on another
                        //action, and that action failed.
                        progress.Report("Failed: " + downloadDict[dUrl]);
                    }
                    else
                    {
                        progress.Report(
                            "Failed: " + downloadDict[dUrl] + "\r\n" + e.Message);
                    }
                }
                catch (Exception e)
                {
                    progress.Report(
                        "Failed: " + downloadDict[dUrl] + "\r\n" + e.Message);
                }
                finally
                {
                    downloadDict.Remove(dUrl);
                }
            }
        }
    }
}
