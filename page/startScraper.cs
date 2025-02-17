using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace xy.scraper.page
{
    public class startScraper
    {
        public async Task scrape(
            string url, 
            IHtmlParser htmlParser,
            CancellationToken token,
            IProgress<string> progress = null,
            string savePath = "download")
        {
            pageScraper Scraper = new pageScraper(htmlParser);

            List<(string, (Type, Object?))> toBeHandledList 
                = await Scraper.download(url, token, progress, savePath);

            while (toBeHandledList.Count !=0)
            {
                if (token.IsCancellationRequested)
                {
                    progress.Report("\r\ntask canceled\r\n");
                    break;
                }
                (string, (Type, Object ?)) toBeHandled = toBeHandledList[0];
                string toBeHandledUrl = toBeHandledList[0].Item1;

                Scraper = new pageScraper(
                    (IHtmlParser)Activator.CreateInstance(
                            toBeHandled.Item2.Item1,
                            new object[] { toBeHandled.Item2.Item2 }
                        )
                    );

                List<(string, (Type, Object?))> moreList
                    = await Scraper.download(toBeHandledUrl, token, progress, savePath);

                List<(string, (Type, Object?))> tempList = new List<(string, (Type, object?))>();
                foreach ((string, (Type, Object?)) moretask in moreList)
                {
                    bool hasDuplication = false;
                    //!toBeHandledDic.ContainsKey(key) && 
                    foreach ((string, (Type, Object?)) handled in toBeHandledList)
                    {
                        if (handled.Item1 == moretask.Item1)
                        {
                            //if the url is already in the toBeHandledList
                            //set the hasDuplication flag, then break it
                            hasDuplication = true;
                            break;
                        }
                    }
                    if (!hasDuplication)
                    {
                        tempList.Add(moretask);
                    }
                }
                toBeHandledList.InsertRange(0, tempList);

                //remove the first element, and save it for duplication handle
                toBeHandledList.Remove(toBeHandled);
                progress.Report("task statistics:" + "\r\n"
                        + "    to be done: " + toBeHandledList.Count + "\r\n"
                    );
            }
        }
    }
}
