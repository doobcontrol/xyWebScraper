using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xy.scraper.page;
using xy.scraper.page.parserConfig;
using System.Text.Json.Nodes;

namespace xy.scraper.configControl
{
    public partial class SearchTest : UserControl
    {
        public SearchTest()
        {
            InitializeComponent();
        }

        private string html;
        private async void btnGetHtml_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            HttpClientDownloader httpClientDownloader = new HttpClientDownloader();
            html =
                await new HttpClientDownloader().GetHtmlStringAsync(
                    txtUrl.Text,
                    scraperConfig.Encoding,
                    new Progress<string>(s => { })
                );
            tabControl1.SelectedIndex = 1;
            txtHtml.Text = html;
            this.Enabled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            JsonObject searchJson = scraperConfig.CurrentSearchConfig.JsonObj;
            bool searchList = searchJson[JCfgName.SearchList].GetValue<bool>();
            if (searchList)
            {
                List<string> searchResult = ParserJosnConfig.searchList(html, searchJson);
                txtShowBox.Text = string.Join("\r\n", searchResult);
                txtShowBox.Text = searchResult.Count + " items:\r\n" + txtShowBox.Text;
            }
            else
            {
                string searchResult = ParserJosnConfig.search(html, searchJson);
                txtShowBox.Text = searchResult;
            }
        }

        private ScraperConfig scraperConfig;
        public ScraperConfig ScraperConfig
        {
            set
            {
                scraperConfig = value;
            }
        }
    }
}
