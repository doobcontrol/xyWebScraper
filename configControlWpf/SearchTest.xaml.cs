using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }

        public delegate Task<string> GetHtmlString(string Url);
        public GetHtmlString getHtmlString;
    }
}
