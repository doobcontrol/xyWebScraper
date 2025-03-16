using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace xy.scraper.page
{
    public interface IHtmlParser
    {
        Dictionary<string, string> getDownloadDict(string htmlString);
        List<(string, string)> getOtherPageDict(
            string htmlString, string origUrl);
        string GetEncoding();
    }
}
