using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace xy.scraper.page
{
    public class HttpClientDownloader : IHtmlDownloader
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task DownloadFileAsync(
            string uri, 
            string outputPath, 
            IProgress<string> progress)
        {
            Uri uriResult;

            if (!Uri.TryCreate(uri, UriKind.Absolute, out uriResult))
                throw new InvalidOperationException("URI is invalid.");

            byte[] fileBytes = await _httpClient.GetByteArrayAsync(uri);
            File.WriteAllBytes(outputPath, fileBytes);
        }

        public async Task<string> GetHtmlStringAsync(
            string url,
            IProgress<string> progress)
        {
            HttpClient _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Custom");
            var response = await _httpClient.GetByteArrayAsync(url);
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var responseString = await Task.Run(
                () =>
                Encoding.GetEncoding("GB18030").GetString(response, 0, response.Length - 1)
            ); 
            return responseString;
        }
    }
}
