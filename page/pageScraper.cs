using System;

namespace xy.scraper.page
{
    public class pageScraper
    {
        protected IHtmlDownloader _htmlDownloader;
        protected IHtmlParser _htmlParser;

        public pageScraper(IHtmlParser htmlParser)
        {
            _htmlParser = htmlParser;
            _htmlDownloader = new HttpClientDownloader();
        }

        public async Task<List<(string, (Type, Object?))>> download(
            string pUrl,
            IProgress<string> progress,
            string savePath
            )
        {
            string htmlString = await _htmlDownloader.GetHtmlStringAsync(
                pUrl, _htmlParser.GetEncoding(), progress);

            progress.Report("get task html: " + pUrl);
            Dictionary<string, string> downloadDict = 
                _htmlParser.getDownloadDict(htmlString);
            progress.Report("got download infomation:" + "\r\n"
                    + "    download items: " + downloadDict.Count
                );
            foreach (string dUrl in downloadDict.Keys)
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
                    progress.Report("Succeed: "+ downloadDict[dUrl]);
                }
                catch (HttpRequestException e)
                {
                    if(((int?)e.StatusCode) == 424)
                    {
                        //The HTTP 424 Failed Dependency client error response status
                        //code indicates that the method could not be performed on the
                        //resource because the requested action depended on another
                        //action, and that action failed.
                        progress.Report("Failed: " + downloadDict[dUrl]);
                    }
                }
            }

            List<(string, (Type, Object?))> retList = _htmlParser.getOtherPageDict(htmlString);
            progress.Report("get other page links: " + retList.Count);

            return retList;
        }
    }
}
