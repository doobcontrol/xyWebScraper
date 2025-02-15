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
        private List<List<(string, string)>> pathCfg;
        private List<(List<(string, string)>, List<string>, string)> fileCfg;
        List<(List<(string, string)>, List<string>, string, string)> nextCfg;
        private string encoding;
        private string configId;
        private ParserJosnConfig(JsonElement root)
        {
            //JsonElement root = JsonDocument.Parse(jsonStr).RootElement;
            JsonElement searchsE;

            encoding = root.GetProperty("encoding").GetString();
            configId = root.GetProperty("cfgid").GetString();

            //get path string search configs
            pathCfg = new List<List<(string, string)>>();
            fileCfg = new List<(List<(string, string)>, List<string>, string)>();

            JsonElement pathsE;
            JsonElement filesE;

            if (root.TryGetProperty("paths", out pathsE))
                if (root.TryGetProperty("files", out filesE))
                {
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

            //get next page url string search configs
            JsonElement nextsE;
            nextCfg = new List<(List<(string, string)>, List<string>, string, string)>();
            if (root.TryGetProperty("nexts", out nextsE))
            {
                foreach (JsonElement nextE in nextsE.EnumerateArray())
                {

                    List<(string, string)> fSearchs = new List<(string, string)>();
                    List<string> fReplaces = new List<string>();
                    string urlAddStr = null;

                    urlAddStr = nextE.GetProperty("add").GetString();
                    String cfgid = nextE.GetProperty("cfgid").GetString();
                    searchsE = nextE.GetProperty("search");
                    foreach (JsonElement searchE in searchsE.EnumerateArray())
                    {
                        fSearchs.Add(
                                (
                                searchE.GetProperty("start").GetString(),
                                searchE.GetProperty("end").GetString()
                                )
                            );
                    }
                    JsonElement replacesE = nextE.GetProperty("replaces");
                    foreach (JsonElement replaceE in replacesE.EnumerateArray())
                    {
                        fReplaces.Add(replaceE.GetProperty("replace").GetString());
                    }

                    nextCfg.Add((fSearchs, fReplaces, urlAddStr, cfgid));
                }
            }
        }

        public string GetEncoding()
        {
            return encoding;
        }
        public string GetConfigId()
        {
            return configId;
        }

        public List<(List<(string, string)>, List<string>, string)> getFileCfg()
        {
            return fileCfg;
        }
        public List<(List<(string, string)>, List<string>, string, string)> getNextCfg()
        {
            return nextCfg;
        }

        public List<List<(string, string)>> getPathCfg()
        {
            return pathCfg;
        }

        #region static members

        private static Dictionary<string, ParserJosnConfig> parserConfigDic = new Dictionary<string, ParserJosnConfig>();
        public static void setConfigs(string jsonStr)
        {
            JsonElement root = JsonDocument.Parse(jsonStr).RootElement;

            foreach (JsonElement configElement in root.EnumerateArray())
            {
                ParserJosnConfig parserJosnConfig= new ParserJosnConfig(configElement);
                parserConfigDic[parserJosnConfig.GetConfigId()] = parserJosnConfig;
            }
        }
        public static ParserJosnConfig getParserConfig(string configId)
        {
            return parserConfigDic[configId];
        }




        //copilot created refrence code
        public static string getJsonStr(string encoding, List<List<(string, string)>> pathCfg, List<(List<(string, string)>, List<string>, string)> fileCfg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("\"encoding\":\"" + encoding + "\",");
            sb.Append("\"paths\":[");
            foreach (List<(string, string)> path in pathCfg)
            {
                sb.Append("{");
                sb.Append("\"search\":[");
                foreach ((string, string) search in path)
                {
                    sb.Append("{");
                    sb.Append("\"start\":\"" + search.Item1 + "\",");
                    sb.Append("\"end\":\"" + search.Item2 + "\"");
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("},");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("],");
            sb.Append("\"files\":[");
            foreach ((List<(string, string)>, List<string>, string) file in fileCfg)
            {
                sb.Append("{");
                sb.Append("\"add\":\"" + file.Item3 + "\",");
                sb.Append("\"search\":[");
                foreach ((string, string) search in file.Item1)
                {
                    sb.Append("{");
                    sb.Append("\"start\":\"" + search.Item1 + "\",");
                    sb.Append("\"end\":\"" + search.Item2 + "\"");
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("],");
                sb.Append("\"replaces\":[");
                foreach (string replace in file.Item2)
                {
                    sb.Append("{");
                    sb.Append("\"replace\":\"" + replace + "\"");
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("},");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("],");
            sb.Append("\"nexts\":[");
            sb.Append("]");
            sb.Append("}");
            return sb.ToString();
        }

        public static void saveParserConfig(string jsonStr, string filePath)
        {
            System.IO.File.WriteAllText(filePath, jsonStr);
        }

        #endregion
    }
}
