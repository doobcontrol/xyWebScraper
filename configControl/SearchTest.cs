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
using xy.scraper.configControl.Properties;

namespace xy.scraper.configControl
{
    public partial class SearchTest : UserControl
    {
        public SearchTest()
        {
            InitializeComponent();
            tabControl1.Controls.Remove(tpTest);
            lbGetting.Visible = false;

            setUiText();
        }
        private void setUiText()
        {
            btnGetHtml.Text = Resources.btnGetHtml;
            btnSearch.Text = Resources.btnSearch;
            lbGetting.Text = Resources.lbGetting;
            tpHtml.Text = Resources.tpHtml;
            tpTest.Text = Resources.tpTest;
        }

        private string html;
        private async void btnGetHtml_Click(object sender, EventArgs e)
        {
            tabControl1.Controls.Remove(tpTest);
            txtHtml.Clear();
            lbGetting.Visible = true;
            tabControl1.Visible = false;
            btnGetHtml.Enabled = false;
            HttpClientDownloader httpClientDownloader = new HttpClientDownloader();
            try
            {
                html =
                    await getHtmlString(txtUrl.Text);
                tabControl1.SelectedIndex = 1;
                txtHtml.Text = html;
                tabControl1.Controls.Add(tpTest);
            }
            catch (Exception ex)
            {
                txtHtml.Text = ex.Message;
                tabControl1.Controls.Remove(tpTest);
            }
            finally
            {
                btnGetHtml.Enabled = true;
                lbGetting.Visible = false;
                tabControl1.Visible = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                JsonObject searchJson = searchJsonObject();
                bool searchList = searchJson[JCfgName.SearchList].GetValue<bool>();
                if (searchList)
                {
                    List<string> searchResult = ParserJosnConfig.searchList(html, searchJson);
                    txtShowBox.Text = string.Join("\r\n", searchResult);
                    txtShowBox.Text = "found " + searchResult.Count + " items:\r\n" + txtShowBox.Text;
                }
                else
                {
                    string searchResult = ParserJosnConfig.search(html, searchJson);
                    txtShowBox.Text = searchResult;
                    txtShowBox.Text = "found string:\r\n" + txtShowBox.Text;
                }
            }
            catch (Exception ex)
            {
                txtShowBox.Text = "search error:\r\n" + ex.Message;
            }
        }

        public delegate JsonObject SearchJsonObject();
        private SearchJsonObject searchJsonObject;
        public SearchJsonObject SearchJsonObj
        {
            set
            {
                searchJsonObject = value;
            }
        }

        public delegate Task<string> GetHtmlString(string Url);
        GetHtmlString getHtmlString;
        public GetHtmlString GetHtmlStringHandler
        {
            set
            {
                getHtmlString = value;
            }
        }
    }
}
