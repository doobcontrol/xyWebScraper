using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using xy.scraper.page;
using xy.scraper.page.parserConfig;

namespace configControlWpf
{
    /// <summary>
    /// Interaction logic for SearchTest.xaml
    /// </summary>
    public partial class SearchTest : UserControl
    {

        private string? html;
        public SearchTest()
        {
            InitializeComponent();

            spTest.Visibility = Visibility.Hidden;
            lbMsg.Text = "Input url and click 'Get Html Text' button";

            btnGetHtmlText.Click += async void (o, e) =>
            {
                spTest.Visibility = Visibility.Hidden;
                btnGetHtmlText.IsEnabled = false;

                HttpClientDownloader httpClientDownloader = new HttpClientDownloader();

                try
                {
                    lbMsg.Text = "Getting...";
                    html =
                        await getHtmlString(txtUrl.Text);
                    tabControl.SelectedItem = tiHtmlText;
                    txtHtmlText.Text = html;
                    spTest.Visibility = Visibility.Visible;

                    lbMsg.Text = "Got Html text, switch to Test tab for search test.";
                }
                catch (Exception ex)
                {
                    lbMsg.Text = "Fail, check the url and retry.";
                    txtHtmlText.Text = ex.Message;
                }
                finally
                {
                    btnGetHtmlText.IsEnabled = true;
                }
            };

            btnTest.Click += (o, e) =>
            {
                try
                {
                    lbMsg.Text = "Testing...";
                    JsonObject searchJson = getSearchJsonObject();

                    if (searchJson.ContainsKey(JCfgName.search))
                    {
                        bool searchList = searchJson[JCfgName.SearchList].GetValue<bool>();
                        if (searchList)
                        {
                            List<string> searchResult = ParserJosnConfig.searchList(html, searchJson);
                            txtTest.Text =
                               string.Format("found  {0} items: ", searchResult.Count)
                               + "\r\n"
                               + string.Join("\r\n", searchResult);
                        }
                        else
                        {
                            string? searchResult = ParserJosnConfig.search(html, searchJson);
                            txtTest.Text = "found string: " +
                                "\r\n" + searchResult;
                        }
                    }
                    else if (searchJson.ContainsKey(JCfgName.AutoGrowthUrl))
                    {
                        string? searchResult = ParserJosnConfig.createUrlFromUrl(
                            html, txtUrl.Text, searchJson[JCfgName.AutoGrowthUrl].AsObject());
                        txtTest.Text = "found string: " +
                            "\r\n" + searchResult;
                    }

                    lbMsg.Text = "Test done.";
                }
                catch (Exception ex)
                {
                    lbMsg.Text = "Test fail.";
                    txtTest.Text = "search error: "
                        + "\r\n" + ex.Message;
                }
            };

            txtUrl.GotFocus += (o, e) =>
            {
                // Fixes issue when clicking cut/copy/paste in context menu
                if (txtUrl.SelectionLength == 0)
                    txtUrl.SelectAll();
            };

            //refrence:
            //https://stackoverflow.com/questions/1356045/select-all-text-on-focus-in-wpf-textbox
            txtUrl.GotKeyboardFocus += TextBox_GotKeyboardFocus;
            txtUrl.LostMouseCapture += TextBox_LostMouseCapture;
            txtUrl.LostKeyboardFocus += TextBox_LostKeyboardFocus;
        }
        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            // Fixes issue when clicking cut/copy/paste in context menu
            if (txtUrl.SelectionLength == 0)
                txtUrl.SelectAll();
        }
        private void TextBox_LostMouseCapture(object sender, MouseEventArgs e)
        {
            // If user highlights some text, don't override it
            if (txtUrl.SelectionLength == 0)
                txtUrl.SelectAll();

            // further clicks will not select all
            txtUrl.LostMouseCapture -= TextBox_LostMouseCapture;
        }
        private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            // once we've left the TextBox, return the select all behavior
            txtUrl.LostMouseCapture += TextBox_LostMouseCapture;
        }

        public delegate Task<string> GetHtmlString(string Url);
        public GetHtmlString getHtmlString;

        public delegate JsonObject SearchJsonObject();
        public SearchJsonObject getSearchJsonObject;
    }
}
