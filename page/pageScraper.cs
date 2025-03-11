using System;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace xy.scraper.page
{
    public class pageScraper
    {
        protected IHtmlDownloader _htmlDownloader;
        protected IHtmlParser _htmlParser;

        public pageScraper(IHtmlParser htmlParser,
            IHtmlDownloader? htmlDownloader = null)
        {
            _htmlParser = htmlParser;
            _htmlDownloader = htmlDownloader ?? new HttpClientDownloader();
        }

        public async Task<List<(string, string)>> download(
            string pUrl,
            CancellationToken token,
            IProgress<CReport> progress,
            string savePath
            )
        {
            CReport.reportMsg(progress,
                Resources.GetTaskHtml + pUrl);
            string htmlString;
            int tryCount = 0;
            while (true)
            {
                try
                {
                    if (token.IsCancellationRequested)
                    {
                        CReport.reportMsg(progress,
                            Resources.CancelTaskStartSaveBreakPoint);

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
                    CReport.reportError(progress,
                        string.Format(Resources.ExceptionInfo,
                        "HttpRequestException", e.Message), e);

                    if (tryCount < 5)
                    {
                        tryCount++;
                        CReport.reportMsg(progress,
                            Resources.Retry + tryCount);
                        await Task.Delay(1000);
                    }
                    else
                    {
                        CReport.reportMsg(progress,
                            Resources.GaveUpTry);
                        return new List<(string, string)>();
                    }
                }
                catch (TaskCanceledException e)
                {
                    CReport.reportError(progress,
                        string.Format(Resources.ExceptionInfo,
                        "TaskCanceledException", e.Message), e);

                    if (tryCount < 5)
                    {
                        tryCount++;
                        CReport.reportMsg(progress,
                            Resources.Retry + tryCount);
                        await Task.Delay(10000);
                    }
                    else
                    {
                        CReport.reportMsg(progress,
                            Resources.GaveUpTry);
                        throw;
                    }
                }
            }

            Dictionary<string, string> downloadDict =
                _htmlParser.getDownloadDict(htmlString);
            List<(string, string)> retList = _htmlParser.getOtherPageDict(htmlString);
            CReport.reportMsg(progress,
                Resources.GotOtherPageLinks + retList.Count);
            CReport.reportMsg(progress,
                Resources.GotDownloadItems + downloadDict.Count);

            try
            {
                await download(downloadDict, token, progress, savePath);
            }
            catch (OperationCanceledException e)
            {
                //save the retList to a file
                e.Data["retList"] = retList;
                throw;
            }

            return retList;
        }

        public async Task download(
            Dictionary<string, string> downloadDict,
            CancellationToken token,
            IProgress<CReport> progress,
            string savePath
            )
        {
            CReport.reportFileTask(progress, downloadDict);
            List<string> urlList = downloadDict.Keys.ToList();
            foreach (string dUrl in urlList)
            {
                if (token.IsCancellationRequested)
                {
                    CReport.reportMsg(progress,
                        Resources.CancelTaskStartSaveBreakPoint);

                    //save the downloadDict to a file
                    OperationCanceledException e = new OperationCanceledException(token);
                    e.Data["savePath"] = savePath;
                    e.Data["retList"] = null;
                    e.Data["downloadDict"] = downloadDict;
                    throw e;
                }
                try
                {
                    CReport.reportFileStart(progress, dUrl);
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
                            CReport.reportMsg(progress,
                                Resources.Succeed + downloadDict[dUrl]);
                            CReport.reportFileDone(progress, (dUrl, true));
                            break;
                        }
                        catch (HttpRequestException e)
                        {
                            CReport.reportError(progress,
                                string.Format(Resources.ExceptionInfo,
                                "HttpRequestException", e.Message), e);

                            if (tryCount < 5)
                            {
                                tryCount++;
                                CReport.reportMsg(progress,
                                    Resources.Retry + tryCount);
                                await Task.Delay(1000);
                            }
                            else
                            {
                                CReport.reportMsg(progress,
                                    Resources.GaveUpTry + downloadDict[dUrl]);
                                CReport.reportFileDone(progress, (dUrl, false));
                                break;
                            }
                        }
                        catch (TaskCanceledException e)
                        {
                            CReport.reportError(progress,
                                string.Format(Resources.ExceptionInfo,
                                "TaskCanceledException", e.Message), e);

                            if (tryCount < 5)
                            {
                                tryCount++;
                                CReport.reportMsg(progress,
                                    Resources.Retry + tryCount);
                                await Task.Delay(10000);
                            }
                            else
                            {
                                CReport.reportMsg(progress,
                                    Resources.GaveUpTry + downloadDict[dUrl]);
                                CReport.reportFileDone(progress, (dUrl, false));
                                throw;
                            }
                        }
                    }
                    downloadDict.Remove(dUrl);
                }
                catch (Exception e)
                {
                    CReport.reportError(progress,
                        Resources.Failed + downloadDict[dUrl], e);
                    CReport.reportFileDone(progress, (dUrl, false));

                    //save the downloadDict to a file
                    OperationCanceledException oe = new OperationCanceledException(token);
                    oe.Data["savePath"] = savePath;
                    oe.Data["retList"] = null;
                    oe.Data["downloadDict"] = downloadDict;
                    throw oe;
                }
            }
        }
    }
}
