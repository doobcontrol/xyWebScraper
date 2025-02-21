using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using xy.scraper.page.parserConfig;

namespace xy.scraper.page
{
    public class startScraper
    {
        private string _savePath;

        private async Task doScrapeTask(
            List<(string, (Type, Object?))> toBeHandledList,
            CancellationToken token,
            IProgress<string> progress = null,
            Dictionary<string, string> preDownloadDict = null,
            string preDownloadSavePath = "")
        {
            pageScraper Scraper;
            while (toBeHandledList.Count != 0)
            {
                (string, (Type, Object?)) toBeHandled = toBeHandledList[0];
                string toBeHandledUrl = toBeHandledList[0].Item1;

                Scraper = new pageScraper(
                    (IHtmlParser)Activator.CreateInstance(
                            toBeHandled.Item2.Item1,
                            new object[] { toBeHandled.Item2.Item2 }
                        )
                    );

                List<(string, (Type, Object?))> moreList = null;

                try
                {
                    moreList
                    = await Scraper.download(toBeHandledUrl, token, progress, _savePath);
                }
                catch (OperationCanceledException e)
                {
                    //save the downloadDict to a file
                    saveBreakpoint(e, toBeHandledList);
                    //token.ThrowIfCancellationRequested();
                    throw e;
                }

                List<(string, (Type, Object?))> tempList = new List<(string, (Type, object?))>();
                foreach ((string, (Type, Object?)) moretask in moreList)
                {
                    bool hasDuplication = false;
                    foreach ((string, (Type, Object?)) handled in toBeHandledList)
                    {
                        if (handled.Item1 == moretask.Item1)
                        {
                            //if the url is already in the toBeHandledList
                            //set the hasDuplication flag, then break it
                            hasDuplication = true;
                            break;
                        }
                    }
                    if (!hasDuplication)
                    {
                        tempList.Add(moretask);
                    }
                }
                toBeHandledList.InsertRange(0, tempList);

                //remove the first element, and save it for duplication handle
                toBeHandledList.Remove(toBeHandled);
                progress.Report("to be done: " + toBeHandledList.Count + "\r\n");
            }
        }

        public startScraper(string savePath = "download")
        {
            _savePath = savePath;
        }
        public async Task newScrape(
            string url,
            ParserByConfig htmlParser,
            CancellationToken token,
            IProgress<string> progress = null)
        {
            List<(string, (Type, Object?))> toBeHandledList 
                = new List<(string, (Type, object?))>();
            toBeHandledList.Add((url, 
                    (htmlParser.GetType(), htmlParser.GetParserConfig())
                    )
                );
            await doScrapeTask(toBeHandledList, token, progress);
        }

        public async Task resumeScrape(CancellationToken token,
            IProgress<string> progress = null)
        {
            JsonObject root = JsonSerializer.Deserialize<JsonObject>
                (File.ReadAllText(_breakPointSavePath));


            // Replace the problematic line with the following code
            JsonNode downloadNode = root["downloadNode"];
            string savePath = downloadNode["savePath"].GetValue<string>();
            Dictionary<string, string> downloadDict = new Dictionary<string, string>();
            foreach (var kvp in downloadNode["downloadDict"].AsObject())
            {
                downloadDict[kvp.Key] = kvp.Value.GetValue<string>();
            }
            progress.Report("start resume break point ...\r\n");
            progress.Report("to be download break point files: " + downloadDict.Count);
            await new pageScraper(null) // download function do not need a parser
                .download(downloadDict, token, progress, _savePath);
            progress.Report("break point files done\r\n");

            List<(string, (Type, Object?))> toBeHandledList
                = new List<(string, (Type, object?))>();

            foreach (var kvp in root["toBeHandledList"].AsObject())
            {
                toBeHandledList.Add(
                    (kvp.Key,
                    (typeof(ParserByConfig),
                    ParserJosnConfig.getParserConfig(
                        kvp.Value.GetValue<string>()
                        )
                    )
                    )
                );
                downloadDict[kvp.Key] = kvp.Value.GetValue<string>();
            }

            progress.Report("to be download break point tasks: " 
                + toBeHandledList.Count);

            await doScrapeTask(
                toBeHandledList, 
                token, 
                progress, 
                downloadDict, 
                savePath);
        }

        public static string _breakPointSavePath = "breakPoint.json";
        private void saveBreakpoint(
            OperationCanceledException e,
            List<(string, (Type, Object?))>? toBeHandledList)
        {
            string savePath = (string)e.Data["savePath"];
            Dictionary<string, string> downloadDict 
                = (Dictionary<string, string>)e.Data["downloadDict"];

            // Create a JsonObject to hold the downloadDict
            JsonObject root = new JsonObject();
            JsonObject downloadNode = new JsonObject();
            root["downloadNode"] = downloadNode;

            downloadNode["savePath"] = savePath;

            JsonObject downloadDictNode = new JsonObject();
            foreach (var kvp in downloadDict)
            {
                downloadDictNode[kvp.Key] = kvp.Value;
            }
            downloadNode["downloadDict"] = downloadDictNode;

            if(e.Data["retList"] != null)
            {
                List<(string, (Type, Object?))> retList
                    = (List<(string, (Type, Object?))>)e.Data["retList"];
                if (toBeHandledList == null)
                {
                    toBeHandledList = new List<(string, (Type, Object?))>();
                }
                if (toBeHandledList.Count != 0)
                {
                    toBeHandledList.RemoveAt(0);
                }
                toBeHandledList.InsertRange(0, retList);
            }


            JsonObject toBeHandledListNode = new JsonObject();
            foreach ((string, (Type, Object?)) ret in toBeHandledList)
            {
                toBeHandledListNode[ret.Item1] 
                    = ((ParserJosnConfig)(ret.Item2.Item2)).GetConfigId();
            }
            root["toBeHandledList"] = toBeHandledListNode;

            string jsonString = JsonSerializer.Serialize(root);
            File.WriteAllText(_breakPointSavePath, jsonString);
        }
    }
}
