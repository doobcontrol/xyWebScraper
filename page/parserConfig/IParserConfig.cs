using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xy.scraper.page.parserConfig
{
    public interface IParserConfig
    {
        //search start and end index, may need muti refind inside
        List<List<(string, string)>> getPathCfg();

        //1, search start and end index
        //2, may need muti replace
        //3, may need add something (such as: "https://")
        List<(List<(string, string)>, List<string>, string)> getFileCfg();
        List<(List<(string, string)>, List<string>, string, string)> getNextCfg();
        string GetEncoding();
        string GetConfigId();
    }
}
