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
using System.Web;
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

        string teststream = "teststream";
        string savePath = @"download";
        string filePath = @"\path1\path2\";
        string fileName = @"name";
        string fileUrlStr = "fileUrl";
        string msgSucceed = "Succeed: ";
        string cancelFlag = "cancel";
        string HttpRequestExceptionFlag = "HttpRequestException";
        string TaskCanceledExceptionFlag = "TaskCanceledException";
        string ExceptionFlag = "Exception";
        string downloadUrl = "downloadUrl";
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
        private Dictionary<string, string> createDownloadDict(
            int Count, 
            int FlagLoc = -1,
            string? flag = null)
        {
            Dictionary<string, string> downloadDict = new Dictionary<string, string>();
            for (int i = 0; i < Count; i++)
            {
                if (i == FlagLoc && flag != null)
                {
                    downloadDict.Add(flag , filePath + fileName + i);
                }
                else
                {
                    downloadDict.Add(fileUrlStr + i, filePath + fileName + i);
                }
            }
            return downloadDict;
        }
        private List<string> createExpectReportList(int Count)
        {
            List<string> expectReportList = new List<string>();
            for (int i = 0; i < Count; i++)
            {
                expectReportList.Add(msgSucceed + filePath + fileName + i);
            }
            return expectReportList;
        }
        private void IHtmlDownloaderMockSetup_File(
            Mock<IHtmlDownloader> IHtmlDownloaderMock
            )
        {
            IHtmlDownloaderMock.Setup(
                x => x.GetHtmlStringAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(teststream);
            IHtmlDownloaderMock.Setup(
                x => x.DownloadFileAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((uri, path) =>
                {
                    if (uri == TaskCanceledExceptionFlag)
                    {
                        throw new TaskCanceledException();
                    }
                    else if (uri == cancelFlag)
                    {
                        throw new OperationCanceledException();
                    }
                    else if (uri == HttpRequestExceptionFlag)
                    {
                        throw new HttpRequestException();
                    }
                    else if (uri == ExceptionFlag)
                    {
                        throw new Exception();
                    }
                });
        }
        private void IHtmlDownloaderMockSetup_Html(
            Mock<IHtmlDownloader> IHtmlDownloaderMock
            )
        {
            IHtmlDownloaderMock.Setup(
                x => x.GetHtmlStringAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((uri, path) =>
                {
                    if (uri == TaskCanceledExceptionFlag)
                    {
                        throw new TaskCanceledException();
                    }
                    else if (uri == cancelFlag)
                    {
                        throw new OperationCanceledException();
                    }
                    else if (uri == HttpRequestExceptionFlag)
                    {
                        throw new HttpRequestException();
                    }
                    else if (uri == ExceptionFlag)
                    {
                        throw new Exception();
                    }
                })
                .ReturnsAsync(teststream);
        }

        string pageUrlStr = "pageUrl";
        private List<(string, string)> createTaskList(int Count)
        {
            List<(string, string)> taskList = new List<(string, string)>();
            for (int i = 0; i < Count; i++)
            {
                taskList.Add((pageUrlStr, i.ToString()));
            }
            return taskList;
        }
        private void IHtmlParserMockSetup(
            Mock<IHtmlParser> IHtmlParserMock,
            int files,
            int links,
            string MockEncoding = "utf-8"
            )
        {
            IHtmlParserMock.Setup(
                x => x.GetEncoding())
                .Returns(MockEncoding);
            IHtmlParserMock.Setup<List<(string, string)>>(
                x => x.getOtherPageDict(It.IsAny<string>()))
                .Returns((string htmlString) => {
                    List < (string, string) > taskList;
                    if (links > 0)
                    {
                        taskList = createTaskList(links);
                    }
                    else
                    {
                        taskList = new List<(string, string)>();
                    }
                    return taskList;
                });
            IHtmlParserMock.Setup<Dictionary<string, string>>(
                x => x.getDownloadDict(It.IsAny<string>()))
                .Returns((string htmlString) => {
                    if(files > 0)
                    {
                        return createDownloadDict(files);
                    }
                    else
                    {
                        return new Dictionary<string, string>();
                    }
                });
        }

        #region download_Url
        [TestMethod]
        public async Task download_Url()
        {
            await download_Url(20, 2);
        }
        [TestMethod]
        public async Task download_Url_OnlyFiles()
        {
            await download_Url(20, 0);
        }
        [TestMethod]
        public async Task download_Url_OnlyLinks()
        {
            await download_Url(0, 20);
        }
        private async Task download_Url(int files, int links)
        {
            // ARRANGE

            //parameters
            CancellationTokenSource cts = new CancellationTokenSource();
            IProgress<string> progress = new Progress<string>();
            if (Directory.Exists(savePath))
            {
                deleteFolder(savePath);
            }

            //mockPageScraper
            int downloadDictCount = files;
            int expectCount = downloadDictCount;
            int retListCount = links;

            var IHtmlParserMock = new Mock<IHtmlParser>();
            IHtmlParserMockSetup(IHtmlParserMock, downloadDictCount, retListCount);

            var IHtmlDownloaderMock = new Mock<IHtmlDownloader>();
            IHtmlDownloaderMockSetup_Html(IHtmlDownloaderMock);
            IHtmlDownloaderMockSetup_File(IHtmlDownloaderMock);
            pageScraper mockPageScraper = new pageScraper(
                IHtmlParserMock.Object,
                IHtmlDownloaderMock.Object);

            // ACT
            List<(string, string)> retList =
                await mockPageScraper.download(downloadUrl, cts.Token, progress, savePath);

            // ASSERT
            Assert.AreEqual(retList.Count, retListCount);
            if(files > 0)
            {
                Assert.IsTrue(Directory.Exists(savePath + filePath));
            }
            IHtmlParserMock.Verify(
                x => x.getOtherPageDict(It.IsAny<string>()),
                Times.Once);
            IHtmlParserMock.Verify(
                x => x.getDownloadDict(It.IsAny<string>()),
                Times.Once);
            IHtmlDownloaderMock.Verify(
                x => x.DownloadFileAsync(It.IsAny<string>(), It.IsAny<string>()),
                Times.Exactly(expectCount));
        }
        #endregion

        #region download_Dict
        [TestMethod]
        public async Task download_Dict()
        {
            // ARRANGE

            //parameters
            int downloadDictCount = 20;
            Dictionary<string, string> downloadDict 
                = createDownloadDict(downloadDictCount, -1, null);
            int expectCount = downloadDictCount;
            CancellationTokenSource cts = new CancellationTokenSource();
            IProgress<string> progress = new Progress<string>();
            if (Directory.Exists(savePath))
            {
                deleteFolder(savePath);
            }

            //mockPageScraper
            var IHtmlParserMock = new Mock<IHtmlParser>();
            var IHtmlDownloaderMock = new Mock<IHtmlDownloader>();
            IHtmlDownloaderMockSetup_File(IHtmlDownloaderMock);
            pageScraper mockPageScraper = new pageScraper(
                IHtmlParserMock.Object,
                IHtmlDownloaderMock.Object);

            // ACT
            await mockPageScraper.download(downloadDict, cts.Token, progress, savePath);

            // ASSERT
            Assert.IsTrue(Directory.Exists(savePath + filePath));
            IHtmlDownloaderMock.Verify(
                x => x.DownloadFileAsync(It.IsAny<string>(), It.IsAny<string>()),
                Times.Exactly(expectCount));
        }
        [TestMethod]
        public async Task download_Dict_Cancel()
        {
            // ARRANGE

            //parameters
            int FlagLoc = 11;
            int downloadDictCount = 20;
            Dictionary<string, string> downloadDict 
                = createDownloadDict(downloadDictCount, FlagLoc, cancelFlag);
            int expectCount = FlagLoc + 1;
            CancellationTokenSource cts = new CancellationTokenSource();
            IProgress<string> progress = new Progress<string>();
            if (Directory.Exists(savePath))
            {
                deleteFolder(savePath);
            }

            //mockPageScraper
            var IHtmlParserMock = new Mock<IHtmlParser>();
            var IHtmlDownloaderMock = new Mock<IHtmlDownloader>();
            IHtmlDownloaderMockSetup_File(IHtmlDownloaderMock);
            pageScraper mockPageScraper = new pageScraper(
                IHtmlParserMock.Object,
                IHtmlDownloaderMock.Object);

            // ACT
            // ASSERT
            OperationCanceledException ex =
                await Assert.ThrowsExceptionAsync<OperationCanceledException>(
                () => mockPageScraper.download(downloadDict, cts.Token, progress, savePath)
                );
            Assert.AreEqual(ex.Data["savePath"], savePath);
            Assert.AreEqual(ex.Data["retList"], null);
            Dictionary<string,string> saveDic = (Dictionary<string, string>)ex.Data["downloadDict"];
            Assert.AreEqual(saveDic.Count, downloadDictCount - FlagLoc);

            Assert.IsTrue(Directory.Exists(savePath + filePath));
            IHtmlDownloaderMock.Verify(
                x => x.DownloadFileAsync(It.IsAny<string>(), It.IsAny<string>()),
                Times.Exactly(expectCount));
        }
        [TestMethod]
        public async Task download_Dict_Report()
        {
            // ARRANGE

            //parameters
            int downloadDictCount = 20;
            Dictionary<string, string> downloadDict 
                = createDownloadDict(downloadDictCount, -1, null);
            int expectCount = downloadDictCount;
            CancellationTokenSource cts = new CancellationTokenSource();
            List<string> expectReportList = createExpectReportList(20);
            List<string> reportList = new List<string>();
            IProgress<string> progress = new Progress<string>((report) =>
            {
                reportList.Add(report);
            });

            
            if (Directory.Exists(savePath))
            {
                deleteFolder(savePath);
            }

            //mockPageScraper
            var IHtmlParserMock = new Mock<IHtmlParser>();
            var IHtmlDownloaderMock = new Mock<IHtmlDownloader>();
            IHtmlDownloaderMockSetup_File(IHtmlDownloaderMock);
            pageScraper mockPageScraper = new pageScraper(
                IHtmlParserMock.Object,
                IHtmlDownloaderMock.Object);

            // ACT
            await mockPageScraper.download(downloadDict, cts.Token, progress, savePath);

            // ASSERT
            Assert.IsTrue(Directory.Exists(savePath + filePath));
            Thread.Sleep(5000); //wait for the progress report(when batch execute tests, this is must)
            Assert.AreEqual(expectReportList.Count, reportList.Count);
            for (int i = 0; i < expectReportList.Count; i++)
            {
                Assert.AreEqual(expectReportList[i], reportList[i]);
            }
            IHtmlDownloaderMock.Verify(
                x => x.DownloadFileAsync(It.IsAny<string>(), It.IsAny<string>()),
                Times.Exactly(expectCount));
        }
        [TestMethod]
        public async Task download_Dict_HttpRequestException()
        {
            // ARRANGE

            //parameters
            int FlagLoc = 11;
            int downloadDictCount = 20;
            Dictionary<string, string> downloadDict
                = createDownloadDict(20, FlagLoc, HttpRequestExceptionFlag);
            int expectCount = downloadDictCount + 5;
            CancellationTokenSource cts = new CancellationTokenSource();
            IProgress<string> progress = new Progress<string>();
            if (Directory.Exists(savePath))
            {
                deleteFolder(savePath);
            }

            //mockPageScraper
            var IHtmlParserMock = new Mock<IHtmlParser>();
            var IHtmlDownloaderMock = new Mock<IHtmlDownloader>();
            IHtmlDownloaderMockSetup_File(IHtmlDownloaderMock);
            pageScraper mockPageScraper = new pageScraper(
                IHtmlParserMock.Object,
                IHtmlDownloaderMock.Object);

            // ACT
            await mockPageScraper.download(downloadDict, cts.Token, progress, savePath);

            // ASSERT
            Assert.IsTrue(Directory.Exists(savePath + filePath));
            IHtmlDownloaderMock.Verify(
                x => x.DownloadFileAsync(It.IsAny<string>(), It.IsAny<string>()),
                Times.Exactly(expectCount));
        }
        [TestMethod]
        public async Task download_Dict_TaskCanceledException()
        {
            // ARRANGE

            //parameters
            int FlagLoc = 11;
            int downloadDictCount = 20;
            Dictionary<string, string> downloadDict
                = createDownloadDict(20, FlagLoc, TaskCanceledExceptionFlag);
            int expectCount = FlagLoc + 1 + 5;
            CancellationTokenSource cts = new CancellationTokenSource();
            IProgress<string> progress = new Progress<string>();
            if (Directory.Exists(savePath))
            {
                deleteFolder(savePath);
            }

            //mockPageScraper
            var IHtmlParserMock = new Mock<IHtmlParser>();
            var IHtmlDownloaderMock = new Mock<IHtmlDownloader>();
            IHtmlDownloaderMockSetup_File(IHtmlDownloaderMock);
            pageScraper mockPageScraper = new pageScraper(
                IHtmlParserMock.Object,
                IHtmlDownloaderMock.Object);

            // ACT
            // ASSERT
            OperationCanceledException ex =
                await Assert.ThrowsExceptionAsync<OperationCanceledException>(
                () => mockPageScraper.download(downloadDict, cts.Token, progress, savePath)
                );
            Assert.AreEqual(ex.Data["savePath"], savePath);
            Assert.AreEqual(ex.Data["retList"], null);
            Dictionary<string, string> saveDic = (Dictionary<string, string>)ex.Data["downloadDict"];
            Assert.AreEqual(saveDic.Count, downloadDictCount - FlagLoc);

            Assert.IsTrue(Directory.Exists(savePath + filePath));
            IHtmlDownloaderMock.Verify(
                x => x.DownloadFileAsync(It.IsAny<string>(), It.IsAny<string>()),
                Times.Exactly(expectCount));
        }
        [TestMethod]
        public async Task download_Dict_Exception()
        {
            // ARRANGE

            //parameters
            int FlagLoc = 11;
            int downloadDictCount = 20;
            Dictionary<string, string> downloadDict
                = createDownloadDict(20, FlagLoc, ExceptionFlag);
            int expectCount = FlagLoc + 1;
            CancellationTokenSource cts = new CancellationTokenSource();
            IProgress<string> progress = new Progress<string>();
            if (Directory.Exists(savePath))
            {
                deleteFolder(savePath);
            }

            //mockPageScraper
            var IHtmlParserMock = new Mock<IHtmlParser>();
            var IHtmlDownloaderMock = new Mock<IHtmlDownloader>();
            IHtmlDownloaderMockSetup_File(IHtmlDownloaderMock);
            pageScraper mockPageScraper = new pageScraper(
                IHtmlParserMock.Object,
                IHtmlDownloaderMock.Object);

            // ACT
            // ASSERT
            OperationCanceledException ex =
                await Assert.ThrowsExceptionAsync<OperationCanceledException>(
                () => mockPageScraper.download(downloadDict, cts.Token, progress, savePath)
                );
            Assert.AreEqual(ex.Data["savePath"], savePath);
            Assert.AreEqual(ex.Data["retList"], null);
            Dictionary<string, string> saveDic = (Dictionary<string, string>)ex.Data["downloadDict"];
            Assert.AreEqual(saveDic.Count, downloadDictCount - FlagLoc);

            Assert.IsTrue(Directory.Exists(savePath + filePath));
            IHtmlDownloaderMock.Verify(
                x => x.DownloadFileAsync(It.IsAny<string>(), It.IsAny<string>()),
                Times.Exactly(expectCount));
        }
        #endregion
    }
}
