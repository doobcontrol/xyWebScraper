using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
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

        private ParserJosnConfig(JsonObject root)
        {
            JsonElement searchsE;

            encoding = root[JCfgName.encoding].GetValue<String>();
            configId = root[JCfgName.cfgid].GetValue<String>();

            //get path string search configs
            pathCfg = new List<List<(string, string)>>();
            fileCfg = new List<(List<(string, string)>, List<string>, string)>();

            JsonArray pathsE;
            JsonArray filesE;

            if (root.ContainsKey(JCfgName.paths) && root.ContainsKey(JCfgName.files))
            {
                pathsE = root[JCfgName.paths].AsArray();
                foreach (JsonObject pathE in pathsE)
                {
                    List<(string, string)> search = new List<(string, string)>();

                    foreach (JsonObject searchE in pathE[JCfgName.search].AsArray())
                    {
                        search.Add(
                                (
                                searchE[JCfgName.start].GetValue<String>(),
                                searchE[JCfgName.end].GetValue<String>()
                                )
                            );
                    }
                    pathCfg.Add(search);
                }

                filesE = root[JCfgName.files].AsArray();
                //get file download url string search configs
                foreach (JsonObject fileE in filesE)
                {

                    List<(string, string)> fSearchs = new List<(string, string)>();
                    List<string> fReplaces = new List<string>();
                    string urlAddStr = null;

                    urlAddStr = fileE[JCfgName.AddBefore].GetValue<String>();
                    
                    foreach (JsonObject searchE in fileE[JCfgName.search].AsArray())
                    {
                        fSearchs.Add(
                                (
                                searchE[JCfgName.start].GetValue<String>(),
                                searchE[JCfgName.end].GetValue<String>()
                                )
                            );
                    }
                    JsonArray replacesE = fileE[JCfgName.replaces].AsArray();
                    foreach (JsonValue replaceE in replacesE)
                    {
                        fReplaces.Add(replaceE.GetValue<String>());
                    }

                    fileCfg.Add((fSearchs, fReplaces, urlAddStr));
                }
            }

            //get next page url string search configs
            JsonArray nextsE;
            nextCfg = new List<(List<(string, string)>, List<string>, string, string)>();
            if (root.ContainsKey(JCfgName.nexts))
            {
                nextsE = root[JCfgName.nexts].AsArray();
                foreach (JsonObject nextE in nextsE)
                {

                    List<(string, string)> fSearchs = new List<(string, string)>();
                    List<string> fReplaces = new List<string>();
                    string urlAddStr = null;

                    String cfgid = nextE[JCfgName.cfgid].GetValue<String>();

                    JsonObject nextsSearchE = nextE[JCfgName.searchs].AsObject();
                    urlAddStr = nextsSearchE[JCfgName.AddBefore].GetValue<String>();
                    foreach (JsonObject searchE in nextsSearchE[JCfgName.search].AsArray())
                    {
                        fSearchs.Add(
                                (
                                searchE[JCfgName.start].GetValue<String>(),
                                searchE[JCfgName.end].GetValue<String>()
                                )
                            );
                    }
                    JsonArray replacesE = nextsSearchE[JCfgName.replaces].AsArray();
                    foreach (JsonValue replaceE in replacesE)
                    {
                        fReplaces.Add(replaceE.GetValue<String>());
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
            JsonArray root = JsonSerializer.Deserialize<JsonArray>(jsonStr);

            foreach (JsonObject configElement in root)
            {
                ParserJosnConfig parserJosnConfig= new ParserJosnConfig(configElement);
                parserConfigDic[parserJosnConfig.GetConfigId()] = parserJosnConfig;
            }
        }
        public static ParserJosnConfig getParserConfig(string configId)
        {
            return parserConfigDic[configId];
        }
        public static List<string> getConfigIdList()
        {
            return parserConfigDic.Keys.ToList();
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
