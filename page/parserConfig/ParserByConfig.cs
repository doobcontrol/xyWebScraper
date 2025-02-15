using System;
using System.Collections.Generic;
using System.IO;
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
            foreach (List<(string, string)> pathCfg in pathCfgs)
            {
                string foundStr = htmlString;
                //do muti times find
                foreach ((string, string) sCfg in pathCfg)
                {
                    foundStr = htmlParserTool.findBetween(
                        foundStr, sCfg.Item1, sCfg.Item2
                        );
                }
                path += @"\" + htmlParserTool.washPathStr(foundStr);
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
                foreach (string url in urlList)
                {
                    //refind inside every urlstring if need

                    string UrlStr = url;
                    foreach ((string, string) sCfg in fCfg.Item1)
                    {
                        if (fCfg.Item1.IndexOf(sCfg) == 0)
                        {
                            continue;
                        }
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
                    string[] tArr = UrlStr.Split("/");
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

        public Dictionary<string, (Type, Object?)> getOtherPageDict(string htmlString)
        {
            List<(List<(string, string)>, List<string>, string, string)> NextCfg = _parserConfig.getNextCfg();
            Dictionary<string, (Type, Object?)> retDic = new Dictionary<string, (Type, Object?)>();


            foreach ((List<(string, string)>, List<string>, string, string) nCfg in NextCfg)
            {
                //get file url list(may need refind inside)
                List<string> urlList = htmlParserTool.findAllBetween(
                    htmlString, nCfg.Item1[0].Item1, nCfg.Item1[0].Item2
                    );
                foreach (string url in urlList)
                {
                    //refind inside every urlstring if need

                    string UrlStr = url;
                    foreach ((string, string) sCfg in nCfg.Item1)
                    {
                        if(nCfg.Item1.IndexOf(sCfg) == 0)
                        {
                            continue;
                        }
                        UrlStr = htmlParserTool.findBetween(
                            UrlStr, sCfg.Item1, sCfg.Item2
                            );
                    }

                    //handle url
                    //Replace
                    foreach (string rStr in nCfg.Item2)
                    {
                        UrlStr = UrlStr.Replace(rStr, "");
                    }
                    //add "https://" and domain name
                    UrlStr = nCfg.Item3 + UrlStr;

                    retDic.Add(UrlStr, (typeof(ParserByConfig), ParserJosnConfig.getParserConfig(nCfg.Item4)));
                }
            }

            return retDic;
        }
    }
}
