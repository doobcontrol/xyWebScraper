using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xy.scraper.page
{
    public class CReport
    {
        public enum rType
        {
            Msg,
            PageTask,
            FileTask,
            Error
        }

        internal rType reportType = rType.Msg;
        internal string msg = string.Empty;
        internal List<(string, string)>? pageTaskList;
        internal Dictionary<string, string>? fileTaskDict;
        internal Exception? e;

        public rType ReportType { get => reportType; }
        public string Msg { get => msg; }
        public List<(string, string)>? PageTaskList { get => pageTaskList; }
        public Dictionary<string, string>? FileTaskDict { get => fileTaskDict; }
        public Exception? E { get => e; }

        static public void reportMsg(IProgress<CReport> progress, string msg)
        {
            progress.Report(new CReport() { msg = msg });
        }
        static public void reportError(IProgress<CReport> progress, 
            string msg, Exception e)
        {
            progress.Report(new CReport() {
                reportType = rType.Error,
                msg = msg,
                e = e
            });
        }
    }
}
