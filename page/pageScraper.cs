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
            string htmlString;
            int tryCount = 0;
            while (true)
            {
                try
                {
                    htmlString = await _htmlDownloader.GetHtmlStringAsync(
                    pUrl, _htmlParser.GetEncoding(), progress);
                    break;
                }
                catch (HttpRequestException e)
                {
                    progress.Report("HttpRequestException: " + e.Message);

                    if (tryCount < 5)
                    {
                        tryCount++;
                        progress.Report("Retry: " + tryCount);
                        await Task.Delay(1000);
                    }
                    else
                    {
                        progress.Report("Tried many times and gave up");
                        return new List<(string, (Type, Object?))>();
                    }
                }
            }

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
                    int tryCount = 0;
                    while (true)
                    {
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
                            break;
                        }
                        catch (HttpRequestException e)
                        {
                            progress.Report("HttpRequestException: " + e.Message);

                            if (tryCount < 5)
                            {
                                tryCount++;
                                progress.Report("Retry: " + tryCount);
                                await Task.Delay(1000);
                            }
                            else
                            {
                                progress.Report("Tried many times and gave up");
                                break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    progress.Report(
                        "Failed: " + downloadDict[dUrl] + "\r\n" + e.Message);

                    //save the downloadDict to a file
                    OperationCanceledException oe = new OperationCanceledException(token);
                    oe.Data["savePath"] = savePath;
                    oe.Data["retList"] = null;
                    oe.Data["downloadDict"] = downloadDict;
                    throw oe;
                }
                finally
                {
                    downloadDict.Remove(dUrl);
                }
            }
        }
    }
}
