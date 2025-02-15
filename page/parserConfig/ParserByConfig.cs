using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xy.scraper.page.parserConfig
{
    public class ParserByConfig : IHtmlParser
    {
        private IParserConfig _parserConfig;
        public ParserByConfig(IParserConfig parserConfig)
        {
            _parserConfig = parserConfig;
        }

        public Dictionary<string, string> getDownloadDict(string htmlString)
        {
            List<List<(string, string)>> pathCfgs = _parserConfig.getPathCfg();

            string path = null;
            //get muti level path
            foreach(List<(string, string)> pathCfg in pathCfgs)
            {
                string foundStr = htmlString;
                //do muti times find
                foreach((string, string) sCfg in pathCfg)
                {
                    foundStr = htmlParserTool.findBetween(
                        foundStr, sCfg.Item1, sCfg.Item2
                        );
                }
                path += @"\" + foundStr;
            }
            path = path + @"\";

            List<(List<(string, string)>, List<string>, string)> FileCfg 
                = _parserConfig.getFileCfg();
            Dictionary<string, string> retDic = new Dictionary<string, string>();
            foreach ((List<(string, string)>, List<string>, string) fCfg in FileCfg)
            {
                //get file url list(may need refind inside)
                List<string> urlList = htmlParserTool.findAllBetween(
                    htmlString, fCfg.Item1[0].Item1, fCfg.Item1[0].Item2
                    );
                fCfg.Item1.RemoveAt(0);
                foreach (string url in urlList)
                {
                    //refind inside every urlstring if need

                    string UrlStr = url;
                    foreach ((string, string) sCfg in fCfg.Item1)
                    {
                        UrlStr = htmlParserTool.findBetween(
                            UrlStr, sCfg.Item1, sCfg.Item2
                            );
                    }

                    //handle url
                    //Replace
                    foreach (string rStr in fCfg.Item2)
                    {
                        UrlStr = UrlStr.Replace(rStr, "");
                    }
                    //add "https://" and domain name
                    UrlStr = fCfg.Item3 + UrlStr;

                    //find file name(must in url)
                    string[] tArr = url.Split("/");
                    string nameStr = path + tArr[tArr.Length - 1];//.Replace("_mthumb", "");

                    retDic.Add(UrlStr, nameStr);
                }
            }

            return retDic;
        }

        public string GetEncoding()
        {
            return _parserConfig.GetEncoding();
        }

        public Dictionary<string, Type> getOtherPageDict(string htmlString)
        {
            return new Dictionary<string, Type>();
        }
    }
}
