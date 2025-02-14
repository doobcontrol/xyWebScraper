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
        public async Task scraper(
            string url, 
            IHtmlParser htmlParser,
            IProgress<string> progress = null,
            string savePath = "download")
        {
            pageScraper Scraper = new pageScraper(htmlParser);
            Dictionary<string, Type> toBeHandledDic 
                = await Scraper.download(url, progress, savePath);
            while (toBeHandledDic.Count !=0)
            {
                string returnUrl = toBeHandledDic.Keys.ElementAt(0);
                pageScraper returnScraper = new pageScraper(
                    (IHtmlParser)Activator.CreateInstance(toBeHandledDic[returnUrl])
                    );
                Dictionary<string, Type> moreDic 
                    = await returnScraper.download(returnUrl, progress, savePath);
                
                foreach (string key in moreDic.Keys)
                {
                    if (!toBeHandledDic.ContainsKey(key))
                    {
                        toBeHandledDic.Add(key, moreDic[key]);
                    }
                }
            }
        }
    }
}
