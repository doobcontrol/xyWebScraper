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
            PageStart,
            PageDone,
            FileTask,
            FileStart,
            FileDone,
            Error
        }

        internal rType reportType = rType.Msg;
        internal string msg = string.Empty;
        internal Exception? e;

        internal List<(string, string)>? pageTaskList;

        internal Dictionary<string, string>? fileTaskDict;
        internal string fileUrl;
        internal (string fileUrl, bool succeed) fileRusult;



        public rType ReportType { get => reportType; }
        public string Msg { get => msg; }
        public List<(string, string)>? PageTaskList { get => pageTaskList; }
        public Dictionary<string, string>? FileTaskDict { get => fileTaskDict; }
        public Exception? E { get => e; }
        public string FileUrl { get => fileUrl; set => fileUrl = value; }
        public (string fileUrl, bool succeed) FileRusult { get => fileRusult; set => fileRusult = value; }

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

        static public void reportFileTask(IProgress<CReport> progress,
            Dictionary<string, string>? fileTaskDict)
        {
            progress.Report(new CReport()
            {
                reportType = rType.FileTask,
                fileTaskDict = fileTaskDict
            });
        }

        static public void reportFileStart(IProgress<CReport> progress,
            string fileUrl)
        {
            progress.Report(new CReport()
            {
                reportType = rType.FileStart,
                fileUrl = fileUrl
            });
        }

        static public void reportFileDone(IProgress<CReport> progress,
            (string fileUrl, bool succeed) fileRusult)
        {
            progress.Report(new CReport()
            {
                reportType = rType.FileDone,
                fileRusult = fileRusult
            });
        }
    }
}
