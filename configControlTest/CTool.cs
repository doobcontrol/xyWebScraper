﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using xy.scraper.configControl;
using xy.scraper.page.parserConfig;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace configControlTest
{
    static class CTool
    {
        public static Control? GetControl(Control control, string name)
        {
            Control? retControl = null;
            foreach (Control c in control.Controls)
            {
                if (c.Name == name)
                {
                    retControl = c;
                    break;
                }
                else
                {
                    Control? subControl = CTool.GetControl(c, name);
                    if (subControl != null)
                    {
                        retControl = subControl;
                        break;
                    }
                }
            }
            return retControl;
        }
    
        public static Control? GetControl(Control control, string name, 
            List<Type> excludeInTypes)
        {
            Control? retControl = null;
            foreach (Control c in control.Controls)
            {
                if (c.Name == name)
                {
                    retControl = c;
                    break;
                }
                else
                {
                    if(!excludeInTypes.Contains(c.GetType()))
                    {
                        Control? subControl = CTool.GetControl(
                            c, name, excludeInTypes);
                        if (subControl != null)
                        {
                            retControl = subControl;
                            break;
                        }
                    }
                }
            }
            return retControl;
        }

        public static string randomString(int minLength, int maxLength)
        {
            Random random = new Random();
            int length = random.Next(minLength, maxLength);
            const string chars = 
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string randomString()
        {
            return randomString(10, 20);
        }
        public static int randomInt(int min, int max)
        {
            Random random = new Random();
            int length = random.Next(min, min);
            return random.Next(min, min);
        }
        public static (string start, string end, string searchStr, string resultStr) 
            generateSearchTestData()
        {
            string start = randomString();
            string end = randomString();
            string resultStr = randomString();
            string searchStr = 
                randomString(20, 50) 
                + start 
                + resultStr
                + end 
                + randomString(20, 50);

            return (start, end, searchStr, resultStr);
        }
        public static 
            (string start, string end, string searchStr, List<string> resultStrs)
            generateSearchListTestData()
        {
            int count = randomInt(5, 10);
            string start = randomString();
            string end = randomString();
            string searchStr = randomString(20, 50);
            List<string> resultStrs = new List<string>();
            for (int i = 0; i < count; i++)
            {
                string resultStr = randomString(20, 50);
                resultStrs.Add(resultStr);
                searchStr += randomString(20, 50) 
                    + start
                    + resultStr
                    + end
                    + randomString(20, 50);
            }
            searchStr += randomString(20, 50);

            return (start, end, searchStr, resultStrs);
        }

        public static JsonObject testSearchLayer_JsonObj()
        {
            JsonObject jObj = new JsonObject();
            jObj[JCfgName.start] = randomString();
            jObj[JCfgName.end] = randomString();
            return jObj;
        }
        public static JsonObject testSearchLayer_JsonObj(string start, string end)
        {
            JsonObject jObj = new JsonObject();
            jObj[JCfgName.start] = start;
            jObj[JCfgName.end] = end;
            return jObj;
        }

        public static JsonObject testSearchConfig_JsonObj()
        {
            JsonObject jObj = new JsonObject();

            JsonArray searchLayers = new JsonArray();
            jObj[JCfgName.search] = searchLayers;
            searchLayers.Insert(0, testSearchLayer_JsonObj());
            searchLayers.Insert(0, testSearchLayer_JsonObj());

            JsonArray replaces = new JsonArray();
            jObj[JCfgName.replaces] = replaces;
            replaces.Add(JsonValue.Create(randomString()));
            replaces.Add(JsonValue.Create(randomString()));

            jObj[JCfgName.AddBefore] = randomString();
            jObj[JCfgName.AddAfter] = randomString();
            jObj[JCfgName.SearchList] = true;

            return jObj;
        }
        public static JsonObject testSearchConfig_JsonObj(
            List<JsonObject> searchLayerList)
        {
            JsonObject jObj = new JsonObject();

            JsonArray searchLayers = new JsonArray();
            jObj[JCfgName.search] = searchLayers;
            foreach (JsonObject layer in searchLayerList)
            {
                searchLayers.Insert(0, layer);
            }

            JsonArray replaces = new JsonArray();
            jObj[JCfgName.replaces] = replaces;
            replaces.Add(JsonValue.Create(randomString()));
            replaces.Add(JsonValue.Create(randomString()));

            jObj[JCfgName.AddBefore] = "";
            jObj[JCfgName.AddAfter] = "";
            jObj[JCfgName.SearchList] = true;

            return jObj;
        }

        public static JsonObject testPageConfig_JsonObj()
        {
            JsonObject jObj = new JsonObject();
            int count = 3;

            jObj[JCfgName.cfgid] = randomString();
            jObj[JCfgName.encoding] = randomString();

            JsonArray paths = new JsonArray();
            jObj[JCfgName.paths] = paths;
            for (int i = 0; i < count; i++)
            {
                paths.Add(testSearchConfig_JsonObj());
            }

            JsonArray files = new JsonArray();
            jObj[JCfgName.files] = files;
            for (int i = 0; i < count; i++)
            {
                files.Add(testSearchConfig_JsonObj());
            }

            JsonArray nexts = new JsonArray();
            jObj[JCfgName.nexts] = nexts;
            for (int i = 0; i < count; i++)
            {
                JsonObject nextObj = new JsonObject();
                nextObj[JCfgName.cfgid] = randomString();
                nextObj[JCfgName.searchs] = testSearchConfig_JsonObj();

                nexts.Add(nextObj);
            }

            return jObj;
        }
    }
}
