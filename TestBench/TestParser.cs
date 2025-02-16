using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using xy.scraper.page;

namespace TestBench
{
    class TestParser : IHtmlParser
    {
        public Dictionary<string, string> getDownloadDict(string htmlString)
        {
            string start = "<i id=\"Jcamerist\" class=\"camerist\"><a href=\"";
            string end = "</a>";
            string path = htmlParserTool.findBetween(
                htmlString, start, end
                );
            start = "  target=\"_blank\">";
            end = "";
            path = htmlParserTool.findBetween(
                path, start, end
                );
            path = @"\" + htmlParserTool.washPathStr(path);

            start = "<meta itemprop=\"name\" content=\"【";
            end = "】\" />";
            path = path + @"\" + htmlParserTool.washPathStr(
                    htmlParserTool.findBetween(
                        htmlString, start, end
                    )
                    ) + @"\";

            start = "src=\"//";
            end = "\" onload=\"bigPicLoaded(this);\"";
            List<string> urlList = htmlParserTool.findAllBetween(
                htmlString, start, end
                );
            Dictionary<string, string> retDic = new Dictionary<string, string>();
            
            foreach(string url in urlList)
            {
                string urlStr = "https://" + url.Replace("_mthumb", "");
                string[] tArr = url.Split("/");
                string nameStr = path + tArr[tArr.Length - 1].Replace("_mthumb", "");
                retDic.Add(urlStr, nameStr);
            }

            return retDic;
        }

        public string GetEncoding()
        {
            return "GB18030";
        }

        public List<(string, (Type, Object?))> getOtherPageDict(string htmlString)
        {
            return new List<(string, (Type, Object?))>();
        }
    }
}
