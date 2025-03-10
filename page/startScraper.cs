using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
            List<(string, string)> toBeHandledList,
            CancellationToken token,
            IProgress<CReport> progress = null)
        {
            CReport.reportPageTask(progress, toBeHandledList);

            pageScraper Scraper;
            bool pageSucceed = true;
            while (toBeHandledList.Count != 0)
            {
                (string, string) toBeHandled = toBeHandledList[0];
                string toBeHandledUrl = toBeHandledList[0].Item1;

                Scraper = new pageScraper(
                    new ParserByConfig(toBeHandled.Item2)
                    );

                List<(string, string)> moreList = null;

                try
                {
                    CReport.reportPageStart(progress, toBeHandledUrl);
                    pageSucceed = true;
                    moreList
                    = await Scraper.download(toBeHandledUrl, token, progress, _savePath);
                }
                catch (OperationCanceledException e)
                {
                    //save the downloadDict to a file
                    saveBreakpoint(e, toBeHandledList);
                    pageSucceed = false;
                    throw e;
                }
                catch (Exception e)
                {
                    CReport.reportError(progress, Resources.Failed, e);
                    pageSucceed = false;

                    //save the downloadDict to a file
                    OperationCanceledException oe = new OperationCanceledException(token);
                    oe.Data["savePath"] = _savePath;
                    oe.Data["retList"] = null;
                    oe.Data["downloadDict"] = null;
                    saveBreakpoint(oe, toBeHandledList);
                    throw oe;
                }

                List<(string, string)> tempList = new List<(string, string)>();
                foreach ((string, string) moretask in moreList)
                {
                    bool hasDuplication = false;
                    foreach ((string, string) handled in toBeHandledList)
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

                CReport.reportMsg(progress, 
                    Resources.ToBeDone + toBeHandledList.Count);

                CReport.reportPageDone(progress, (toBeHandledUrl, pageSucceed));
            }
        }

        public startScraper(string savePath = "download")
        {
            _savePath = savePath;
        }
        public async Task newScrape(
            string url,
            string configId,
            CancellationToken token,
            IProgress<CReport> progress = null)
        {
            List<(string, string)> toBeHandledList 
                = new List<(string, string)>();
            toBeHandledList.Add((url, configId)
                );
            await doScrapeTask(toBeHandledList, token, progress);
        }

        public async Task resumeScrape(CancellationToken token,
            IProgress<CReport> progress = null)
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

            CReport.reportMsg(progress,
                Resources.StartResumeBreakPoint);
            CReport.reportMsg(progress,
                Resources.BreakPointFiles + downloadDict.Count);
            await new pageScraper(null) // download function do not need a parser
                .download(downloadDict, token, progress, _savePath);
            CReport.reportMsg(progress,
                Resources.BreakPointFilesDone);

            List<(string, string)> toBeHandledList
                = new List<(string, string)>();

            foreach (var kvp in root["toBeHandledList"].AsObject())
            {
                toBeHandledList.Add(
                    (kvp.Key, kvp.Value.GetValue<string>())
                );
            }

            CReport.reportMsg(progress,
                Resources.BreakPointTasks
                + toBeHandledList.Count);

            await doScrapeTask(
                toBeHandledList, 
                token, 
                progress);
        }

        public static string _breakPointSavePath = "breakPoint.json";
        private void saveBreakpoint(
            OperationCanceledException e,
            List<(string, string)>? toBeHandledList)
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
            if (downloadDict != null)
            {
                foreach (var kvp in downloadDict)
                {
                    downloadDictNode[kvp.Key] = kvp.Value;
                }
            }
            downloadNode["downloadDict"] = downloadDictNode;

            if(e.Data["retList"] != null)
            {
                List<(string, string)> retList
                    = (List<(string, string)>)e.Data["retList"];
                if (toBeHandledList == null)
                {
                    toBeHandledList = new List<(string, string)>();
                }
                if (toBeHandledList.Count != 0)
                {
                    toBeHandledList.RemoveAt(0);
                }
                toBeHandledList.InsertRange(0, retList);
            }


            JsonObject toBeHandledListNode = new JsonObject();
            foreach ((string, string) ret in toBeHandledList)
            {
                toBeHandledListNode[ret.Item1] 
                    = ret.Item2;
            }
            root["toBeHandledList"] = toBeHandledListNode;

            string jsonString = JsonSerializer.Serialize(root);
            File.WriteAllText(_breakPointSavePath, jsonString);
        }
    }
}
