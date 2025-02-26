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

        public async Task<List<(string, string)>> download(
            string pUrl,
            CancellationToken token,
            IProgress<string> progress,
            string savePath
            )
        {
            progress.Report(Resources.GetTaskHtml + pUrl);
            string htmlString;
            int tryCount = 0;
            while (true)
            {
                try
                {
                    if (token.IsCancellationRequested)
                    {
                        progress.Report(Resources.CancelTaskStartSaveBreakPoint);

                        OperationCanceledException e = new OperationCanceledException(token);
                        e.Data["savePath"] = savePath;
                        e.Data["retList"] = null;
                        e.Data["downloadDict"] = null;
                        throw e;
                    }
                    htmlString = await _htmlDownloader.GetHtmlStringAsync(
                    pUrl, _htmlParser.GetEncoding());
                    break;
                }
                catch (HttpRequestException e)
                {
                    progress.Report(Resources.HttpRequestException + e.Message);

                    if (tryCount < 5)
                    {
                        tryCount++;
                        progress.Report(Resources.Retry + tryCount);
                        await Task.Delay(1000);
                    }
                    else
                    {
                        progress.Report(Resources.GaveUpTry);
                        return new List<(string, string)>();
                    }
                }
            }

            Dictionary<string, string> downloadDict =
                _htmlParser.getDownloadDict(htmlString);
            List<(string, string)> retList = _htmlParser.getOtherPageDict(htmlString);
            progress.Report(Resources.GotOtherPageLinks + retList.Count);
            progress.Report(Resources.GotDownloadItems + downloadDict.Count);

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
                    progress.Report(Resources.CancelTaskStartSaveBreakPoint);

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
                                dUrl, fileFullName);
                            progress.Report(Resources.Succeed + downloadDict[dUrl]);
                            break;
                        }
                        catch (HttpRequestException e)
                        {
                            progress.Report(Resources.HttpRequestException + e.Message);

                            if (tryCount < 5)
                            {
                                tryCount++;
                                progress.Report(Resources.Retry + tryCount);
                                await Task.Delay(1000);
                            }
                            else
                            {
                                progress.Report(Resources.GaveUpTry);
                                break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    progress.Report(
                        Resources.Failed + downloadDict[dUrl] + "\r\n" + e.Message);

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
