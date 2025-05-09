﻿using Moq.Protected;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using xy.scraper.page;

namespace pageTest
{
    [TestClass]
    public class HttpClientDownloaderTests
    {
        private HttpClientDownloader MockHttpClientDownloader(
            HttpStatusCode statusCode, 
            string returnStr,
            Exception? ex = null)
        {
            var handlerMock = new Mock<HttpMessageHandler>();

            if(ex == null)
            {
                handlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage()
                    {
                        StatusCode = statusCode,
                        Content = new StringContent(returnStr),
                    });
            }
            else
            {
                handlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>())
                    //.Callback(() => throw ex);
                    .ThrowsAsync(ex);
            }

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object);

            return new HttpClientDownloader(httpClient);
        }

        #region GetHtmlStringAsync
        [TestMethod]
        public async Task GetHtmlStringAsync()
        {
            // ARRANGE
            string expected = "[{'id':1,'value':'1'}]";
            HttpClientDownloader mockHttpClientDownloader = 
                MockHttpClientDownloader(HttpStatusCode.OK, expected);
            string uri = "https://test";
            string endocing = "utf-8";

            // ACT
            string actual = await mockHttpClientDownloader
               .GetHtmlStringAsync(uri, endocing);

            // ASSERT
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region DownloadFileAsync
        [TestMethod]
        public async Task DownloadFileAsync()
        {
            // ARRANGE
            string expected = "[{'id':1,'value':'1'}]";
            HttpClientDownloader mockHttpClientDownloader = 
                MockHttpClientDownloader(HttpStatusCode.OK, expected);
            string uri = "https://test";
            string savePath = "test.txt";
            if(File.Exists(savePath))
            {
                File.Delete(savePath);
            }

            // ACT
            await mockHttpClientDownloader
               .DownloadFileAsync(uri, savePath);

            // ASSERT
            Assert.IsTrue(File.Exists(savePath));
        }
        [TestMethod]
        public async Task DownloadFileAsync_URIIsInvalid()
        {
            // ARRANGE
            string expected = "[{'id':1,'value':'1'}]";
            HttpClientDownloader mockHttpClientDownloader =
                MockHttpClientDownloader(HttpStatusCode.NotFound, "");
            string uri = "https://";
            string savePath = "test.txt";
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }

            // ACT
            // ASSERT
            InvalidOperationException ex = 
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(
                async () => await mockHttpClientDownloader
               .DownloadFileAsync(uri, savePath)
                );
            Assert.AreEqual("URI is invalid.", ex.Message);
        }
        [TestMethod]
        public async Task DownloadFileAsync_Timeout()
        {
            // ARRANGE
            string expected = "The request was canceled due to the configured HttpClient.Timeout of 100 seconds elapsing.";
            TaskCanceledException expectedEx = new TaskCanceledException(expected);
            HttpClientDownloader mockHttpClientDownloader =
                MockHttpClientDownloader(HttpStatusCode.NotFound, "",
                expectedEx
                );
            string uri = "https://test.txt";
            string savePath = "test.txt";
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }

            // ACT
            // ASSERT
            TaskCanceledException ex =
                await Assert.ThrowsExceptionAsync<TaskCanceledException>(
                async () => await mockHttpClientDownloader
               .DownloadFileAsync(uri, savePath)
                );
            Assert.AreEqual(expected, ex.Message);
        }
        [TestMethod]
        public async Task DownloadFileAsync_404()
        {
            // ARRANGE
            string expected = "[{'id':1,'value':'1'}]";
            HttpClientDownloader mockHttpClientDownloader = 
                MockHttpClientDownloader(HttpStatusCode.NotFound, "");
            string uri = "https://test";
            string savePath = "test.txt";
            if(File.Exists(savePath))
            {
                File.Delete(savePath);
            }

            // ACT
            // ASSERT
            HttpRequestException ex = await Assert.ThrowsExceptionAsync<HttpRequestException>(
                async () => await mockHttpClientDownloader
               .DownloadFileAsync(uri, savePath)
                );
            Assert.AreEqual(HttpStatusCode.NotFound, ex.StatusCode);
        }
        #endregion
    }
}
