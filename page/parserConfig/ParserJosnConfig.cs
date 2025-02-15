using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace xy.scraper.page.parserConfig
{
    public class ParserJosnConfig : IParserConfig
    {
        List<List<(string, string)>> pathCfg;
        List<(List<(string, string)>, List<string>, string)> fileCfg;
        string encoding;
        public ParserJosnConfig(string jsonStr)
        {
            JsonElement root = JsonDocument.Parse(jsonStr).RootElement;
            JsonElement searchsE;

            encoding = root.GetProperty("encoding").GetString();

            //get path string search configs
            pathCfg = new List<List<(string, string)>>();
            JsonElement pathsE = root.GetProperty("paths");
            foreach (JsonElement pathE in pathsE.EnumerateArray())
            {
                List<(string, string)> search = new List<(string, string)>();

                searchsE = pathE.GetProperty("search");
                foreach (JsonElement searchE in searchsE.EnumerateArray())
                {
                    search.Add(
                            (
                            searchE.GetProperty("start").GetString(),
                            searchE.GetProperty("end").GetString()
                            )
                        );
                }
                pathCfg.Add(search);
            }

            //get file download url string search configs
            JsonElement filesE = root.GetProperty("files");
            fileCfg = new List<(List<(string, string)>, List<string>, string)>();
            foreach (JsonElement fileE in filesE.EnumerateArray())
            {

                List<(string, string)> fSearchs = new List<(string, string)>();
                List<string> fReplaces = new List<string>();
                string urlAddStr = null;

                urlAddStr = fileE.GetProperty("add").GetString();
                searchsE = fileE.GetProperty("search");
                foreach (JsonElement searchE in searchsE.EnumerateArray())
                {
                    fSearchs.Add(
                            (
                            searchE.GetProperty("start").GetString(),
                            searchE.GetProperty("end").GetString()
                            )
                        );
                }
                JsonElement replacesE = fileE.GetProperty("replaces");
                foreach (JsonElement replaceE in replacesE.EnumerateArray())
                {
                    fReplaces.Add(replaceE.GetProperty("replace").GetString());
                }

                fileCfg.Add((fSearchs, fReplaces, urlAddStr));
            }
        }

        public string GetEncoding()
        {
            return encoding;
        }

        public List<(List<(string, string)>, List<string>, string)> getFileCfg()
        {
            return fileCfg;
        }

        public List<List<(string, string)>> getPathCfg()
        {
            return pathCfg;
        }
    }
}
