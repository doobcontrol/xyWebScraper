using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using xy.scraper.page;

namespace pageTest
{
    [TestClass]
    public class pageScraperTests
    {
        private static class LockClass
        {
            public static object LockObject = new object();
        }
        [TestInitialize]
        public void TestSetup()
        {
            Monitor.Enter(LockClass.LockObject);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            deleteFolder(@"download");
            Monitor.Exit(LockClass.LockObject);
        }

        private void deleteFolder(string FolderName)
        {
            if(!Directory.Exists(FolderName))
            {
                return;
            }

            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                deleteFolder(di.FullName);
            }
            dir.Delete();
        }

        #region download_Url
        [TestMethod]
        public void download_Url()
        {
            // ARRANGE

            // ACT

            // ASSERT

        }
        #endregion

        #region download_Dict
        [TestMethod]
        public async Task download_Dict()
        {
            // ARRANGE

            //parameters
            Dictionary<string, string> downloadDict = new Dictionary<string, string>();
            downloadDict.Add("url1", @"\path1\path2\name1");
            downloadDict.Add("url2", @"\path1\path2\name2");
            downloadDict.Add("url3", @"\path1\path2\name3");
            int count = downloadDict.Count;
            CancellationTokenSource cts = new CancellationTokenSource();
            IProgress<string> progress = new Progress<string>();
            string savePath = @"download";
            if (Directory.Exists(savePath))
            {
                deleteFolder("download");
            }

            //mockPageScraper
            var IHtmlParserMock = new Mock<IHtmlParser>();
            var IHtmlDownloaderMock = new Mock<IHtmlDownloader>();
            IHtmlDownloaderMock.Setup(
                x => x.GetHtmlStringAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("teststream");
            IHtmlDownloaderMock.Setup(
                x => x.DownloadFileAsync(It.IsAny<string>(), It.IsAny<string>()));
            pageScraper mockPageScraper = new pageScraper(
                IHtmlParserMock.Object,
                IHtmlDownloaderMock.Object);

            // ACT
            await mockPageScraper.download(downloadDict, cts.Token, progress, savePath);

            // ASSERT
            Assert.IsTrue(Directory.Exists(savePath + @"\path1\path2"));
            IHtmlDownloaderMock.Verify(
                x => x.DownloadFileAsync(It.IsAny<string>(), It.IsAny<string>()),
                Times.Exactly(count));
        }
        [TestMethod]
        public async Task download_Dict_Cancel()
        {
            // ARRANGE

            //parameters
            Dictionary<string, string> downloadDict = new Dictionary<string, string>();
            downloadDict.Add("url1", @"\path1\path2\name1");
            downloadDict.Add("url2", @"\path1\path2\name2");
            downloadDict.Add("cancel", @"\path1\path2\name3");
            downloadDict.Add("url4", @"\path1\path2\name4");
            downloadDict.Add("url5", @"\path1\path2\name5");
            int count = downloadDict.Count - 2;
            CancellationTokenSource cts = new CancellationTokenSource();
            IProgress<string> progress = new Progress<string>();
            string savePath = @"download";
            if (Directory.Exists(savePath))
            {
                deleteFolder("download");
            }

            //mockPageScraper
            var IHtmlParserMock = new Mock<IHtmlParser>();
            var IHtmlDownloaderMock = new Mock<IHtmlDownloader>();
            IHtmlDownloaderMock.Setup(
                x => x.GetHtmlStringAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("teststream");
            IHtmlDownloaderMock.Setup(
                x => x.DownloadFileAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((uri, path) => {
                    if (uri == "cancel")
                    {
                        cts.Cancel();
                    }
                });
            pageScraper mockPageScraper = new pageScraper(
                IHtmlParserMock.Object,
                IHtmlDownloaderMock.Object);

            // ACT
            // ASSERT
            OperationCanceledException ex =
                await Assert.ThrowsExceptionAsync<OperationCanceledException>(
                () => mockPageScraper.download(downloadDict, cts.Token, progress, savePath)
                );
            Assert.IsTrue(Directory.Exists(savePath + @"\path1\path2"));
            IHtmlDownloaderMock.Verify(
                x => x.DownloadFileAsync(It.IsAny<string>(), It.IsAny<string>()),
                Times.Exactly(count));
        }
        [TestMethod]
        public async Task download_Dict_Report()
        {
            // ARRANGE
            
            //parameters
            Dictionary<string, string> downloadDict = new Dictionary<string, string>();
            downloadDict.Add("url1", @"\path1\path2\name1");
            downloadDict.Add("url2", @"\path1\path2\name2");
            downloadDict.Add("cancel", @"\path1\path2\name3");
            downloadDict.Add("url4", @"\path1\path2\name4");
            downloadDict.Add("url5", @"\path1\path2\name5");
            int count = downloadDict.Count - 2;
            CancellationTokenSource cts = new CancellationTokenSource();
            List<string> expectReportList = new List<string>();
            expectReportList.Add("Succeed: " + @"\path1\path2\name1");
            expectReportList.Add("Succeed: " + @"\path1\path2\name2");
            expectReportList.Add("Succeed: " + @"\path1\path2\name3");
            expectReportList.Add("\r\ncancel task, start save break point ... \r\n");
            List<string> reportList = new List<string>();
            IProgress<string> progress = new Progress<string>((report) =>
            {
                reportList.Add(report);
            });

            string savePath = @"download";
            if (Directory.Exists(savePath))
            {
                deleteFolder("download");
            }

            //mockPageScraper
            var IHtmlParserMock = new Mock<IHtmlParser>();
            var IHtmlDownloaderMock = new Mock<IHtmlDownloader>();
            IHtmlDownloaderMock.Setup(
                x => x.GetHtmlStringAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("teststream");
            IHtmlDownloaderMock.Setup(
                x => x.DownloadFileAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((uri, path) => {
                    if (uri == "cancel")
                    {
                        cts.Cancel();
                    }
                });
            pageScraper mockPageScraper = new pageScraper(
                IHtmlParserMock.Object,
                IHtmlDownloaderMock.Object);

            // ACT
            // ASSERT
            OperationCanceledException ex =
                await Assert.ThrowsExceptionAsync<OperationCanceledException>(
                () => mockPageScraper.download(downloadDict, cts.Token, progress, savePath)
                );
            Assert.IsTrue(Directory.Exists(savePath + @"\path1\path2"));
            Thread.Sleep(1000); //wait for the progress report(when batch execute tests, this is must)
            Assert.AreEqual(expectReportList.Count, reportList.Count);
            for (int i = 0; i < expectReportList.Count; i++)
            {
                Assert.AreEqual(expectReportList[i], reportList[i]);
            }
            IHtmlDownloaderMock.Verify(
                x => x.DownloadFileAsync(It.IsAny<string>(), It.IsAny<string>()),
                Times.Exactly(count));
        }
        [TestMethod]
        public async Task download_Dict_TaskCanceledException()
        {
            // ARRANGE
            //parameters
            Dictionary<string, string> downloadDict = new Dictionary<string, string>();
            downloadDict.Add("url1", @"\path1\path2\name1");
            int count = downloadDict.Count;
            CancellationTokenSource cts = new CancellationTokenSource();
            IProgress<string> progress = new Progress<string>();
            string savePath = @"download";
            if (System.IO.Directory.Exists(savePath))
            {
                deleteFolder("download");
            }

            //mockPageScraper
            var IHtmlParserMock = new Mock<IHtmlParser>();
            var IHtmlDownloaderMock = new Mock<IHtmlDownloader>();
            IHtmlDownloaderMock.Setup(
                x => x.GetHtmlStringAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("teststream");
            IHtmlDownloaderMock.Setup(
                x => x.DownloadFileAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((uri, path) => {
                    if (uri == "HttpRequestException")
                    {
                        throw new HttpRequestException();
                    }
                    else if (uri == "TaskCanceledException")
                    {
                        throw new TaskCanceledException();
                    }
                });
            pageScraper mockPageScraper = new pageScraper(
                IHtmlParserMock.Object,
                IHtmlDownloaderMock.Object);

            // ACT
            await mockPageScraper.download(downloadDict, cts.Token, progress, savePath);

            // ASSERT
            Assert.IsTrue(System.IO.Directory.Exists(savePath + @"\path1\path2"));
        }
        #endregion
    }
}
