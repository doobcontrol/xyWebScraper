﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xy.scraper.page
{
    public interface IHtmlDownloader
    {
        Task<string> GetHtmlStringAsync(
            string url,
            string encoding);
        Task DownloadFileAsync(
            string uri, 
            string outputPath);
    }
}
