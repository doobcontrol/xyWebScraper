
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
    public class ParserJosnConfig
    {
        private string encoding;
        private string configId;

        private JsonArray pathsE;
        private JsonArray filesE;
        private JsonArray nextsE;

        public JsonArray PathsE { get => pathsE;}
        public JsonArray FilesE { get => filesE;}
        public JsonArray NextsE { get => nextsE;}
        public string Encoding { get => encoding;}
        public string ConfigId { get => configId;}

        private ParserJosnConfig(JsonObject root)
        {
            encoding = root[JCfgName.encoding].GetValue<String>();
            configId = root[JCfgName.cfgid].GetValue<String>();

            if (root.ContainsKey(JCfgName.paths) && root.ContainsKey(JCfgName.files))
            {
                pathsE = root[JCfgName.paths].AsArray();

                filesE = root[JCfgName.files].AsArray();
            }

            if (root.ContainsKey(JCfgName.nexts))
            {
                nextsE = root[JCfgName.nexts].AsArray();
            }
        }

        #region static members

        private static Dictionary<string, ParserJosnConfig> parserConfigDic;

        public static void setConfigs(string jsonStr)
        {
            JsonArray root = JsonSerializer.Deserialize<JsonArray>(jsonStr);
            parserConfigDic = new Dictionary<string, ParserJosnConfig>();

            foreach (JsonObject configElement in root)
            {
                ParserJosnConfig parserJosnConfig= new ParserJosnConfig(configElement);
                parserConfigDic[parserJosnConfig.ConfigId] = parserJosnConfig;
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

        #region search tools

        public static string? search(string searchString, JsonObject searchJson)
        {
            JsonArray searchLayers = searchJson[JCfgName.search].AsArray();
            string? retStr = searchString;
            foreach (JsonObject searchLayer in searchLayers)
            {
                string startStr = searchLayer[JCfgName.start].GetValue<String>();
                string endStr = searchLayer[JCfgName.end].GetValue<String>();
                string tempStr = htmlParserTool.findBetween(retStr, startStr, endStr);
                if (tempStr != null)
                {
                    retStr = tempStr;
                }
                else
                {
                    retStr = null;
                    break;
                }
            }

            if (retStr != null)
            {
                foreach (JsonValue replaceE in searchJson[JCfgName.replaces].AsArray())
                {
                    retStr = retStr?.Replace(replaceE.GetValue<String>(), "");
                }

                string addBefore = searchJson[JCfgName.AddBefore].GetValue<String>();
                string addAfter = searchJson[JCfgName.AddAfter].GetValue<String>();
                retStr = addBefore + retStr + addAfter;
            }

            return retStr;
        }

        public static List<string> searchList(string searchString, JsonObject searchJson)
        {
            JsonArray searchLayers = searchJson[JCfgName.search].AsArray();

            string? retStr = searchString;
            List<string> retList = htmlParserTool.findAllBetween(
                retStr,
                searchLayers[0][JCfgName.start].GetValue<String>(),
                searchLayers[0][JCfgName.end].GetValue<String>()
                );

            foreach(string subSeach in retList.ToList<string>())
            {
                int index = retList.IndexOf(subSeach);
                retStr = subSeach;
                foreach (JsonObject searchLayer in searchLayers)
                {
                    if(searchLayer == searchLayers[0])
                    {
                        continue;
                    }
                    string startStr = searchLayer[JCfgName.start].GetValue<String>();
                    string endStr = searchLayer[JCfgName.end].GetValue<String>();
                    string tempStr = htmlParserTool.findBetween(retStr, startStr, endStr);
                    if (tempStr != null)
                    {
                        retStr = tempStr;
                    }
                    else
                    {
                        retStr = null;
                        break;
                    }
                }
                if(retStr != null)
                {
                    foreach (JsonValue replaceE in searchJson[JCfgName.replaces].AsArray())
                    {
                        retStr = retStr?.Replace(replaceE.GetValue<String>(), "");
                    }
                    string addBefore = searchJson[JCfgName.AddBefore].GetValue<String>();
                    string addAfter = searchJson[JCfgName.AddAfter].GetValue<String>();
                    retStr = addBefore + retStr + addAfter;
                    retList[index] = retStr;
                }
            }

            return retList;
        }

        private static List<string> fileNameSpliter = new List<string>()
        { "?", "!"};
        public static Dictionary<string, string> searchDownloadDict(
            string configId, string htmlString)
        {
            Dictionary<string, string> retDic = new Dictionary<string, string>();
            ParserJosnConfig parserJosnConfig = getParserConfig(configId);

            if(parserJosnConfig.pathsE != null && parserJosnConfig.filesE != null)
            {
                List<string> pathList = new List<string>();
                foreach (JsonObject pathE in parserJosnConfig.pathsE)
                {
                    string? subpath = search(htmlString, pathE);
                    if (subpath != null && subpath.Trim() != "")
                    {
                        pathList.Add(
                            htmlParserTool.washPathStr(subpath.Trim()));
                    }
                }
                string path = @"\" + String.Join(@"\", pathList) + @"\";

                foreach (JsonObject fileE in parserJosnConfig.filesE)
                {
                    List<string> fileList = searchList(htmlString, fileE);
                    foreach (string fileUrl in fileList)
                    {
                        //make sure the file name is in the end of url ??
                        string[] tArr = fileUrl.Split("/");
                        string fileName = tArr[tArr.Length - 1];
                        foreach (string spliter in fileNameSpliter)
                        {
                            fileName = fileName.Split(spliter)[0];
                        }
                        retDic[fileUrl] = path + fileName;
                    }
                }
            }

            return retDic;
        }

        public static List<(string, string)> searchOtherPageDict(
            string configId, string htmlString)
        {
            ParserJosnConfig parserJosnConfig = getParserConfig(configId);

            List<(string, string)> retList = new List<(string, string)>();

            if (parserJosnConfig.nextsE != null)
            {
                foreach (JsonObject nextE in parserJosnConfig.NextsE)
                {
                    String cfgid = nextE[JCfgName.cfgid].GetValue<String>();
                    JsonObject nextsSearchE = nextE[JCfgName.searchs].AsObject();
                    bool isList = nextsSearchE[JCfgName.SearchList].GetValue<Boolean>();
                    if (isList)
                    {
                        List<string> urlList = searchList(htmlString, nextsSearchE);
                        foreach (string url in urlList)
                        {
                            retList.Add((url, cfgid));
                        }
                    }
                    else
                    {
                        string? url = search(htmlString, nextsSearchE);
                        if (url != null)
                        {
                            retList.Add((url, cfgid));
                        }
                    }
                }
            }

            return retList;
        }

        #endregion

        #endregion
    }
}
