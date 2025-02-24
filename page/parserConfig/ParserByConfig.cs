using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace xy.scraper.page.parserConfig
{
    public class ParserByConfig : IHtmlParser
    {
        string _configId;
        public ParserByConfig(string configId)
        {
            _configId = configId;
        }

        public Dictionary<string, string> getDownloadDict(string htmlString)
        {
            return ParserJosnConfig.searchDownloadDict(_configId, htmlString);
        }

        public List<(string, string)> getOtherPageDict(string htmlString)
        {
            return ParserJosnConfig.searchOtherPageDict(_configId, htmlString);
        }

        public string GetEncoding()
        {
            return ParserJosnConfig.getParserConfig(_configId).Encoding;
        }
    }
}
