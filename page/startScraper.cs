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
            IProgress<string> progress = null,
            string savePath = "download")
        {
            pageScraper Scraper = new pageScraper(htmlParser);

            List<string> handledUrls = new List<string>();
            Dictionary<string, (Type, Object?)> toBeHandledDic 
                = await Scraper.download(url, progress, savePath);

            while (toBeHandledDic.Count !=0)
            {
                string returnUrl = toBeHandledDic.Keys.ElementAt(0);

                Scraper = new pageScraper(
                    (IHtmlParser)Activator.CreateInstance(
                            toBeHandledDic[returnUrl].Item1,
                            new object[] { toBeHandledDic[returnUrl].Item2 }
                        )
                    );

                Dictionary<string, (Type, Object?)> moreDic 
                    = await Scraper.download(returnUrl, progress, savePath);
                
                foreach (string key in moreDic.Keys)
                {
                    if (!toBeHandledDic.ContainsKey(key) && !handledUrls.Contains(key))
                    {
                        toBeHandledDic.Add(key, moreDic[key]);
                    }
                }

                //remove the first element, and save it for duplication handle
                toBeHandledDic.Remove(returnUrl);
                handledUrls.Add(returnUrl);
            }
        }
    }
}
