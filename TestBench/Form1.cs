using System;
using System.Text;
using xy.scraper.page;
using xy.scraper.page.parserConfig;
using xySoft.log;

namespace TestBench
{
    public partial class Form1 : Form
    {
        Progress<string> progress = new Progress<string>();
        CancellationTokenSource cts = new CancellationTokenSource();

        public Form1()
        {
            InitializeComponent();
            label1.Text = "";

            progress.ProgressChanged += async (_, data) =>
            {
                showMsg(data + "\r\n");
            };

            if (File.Exists(startScraper._breakPointSavePath))
            {
                button3.Visible = true;
            }

            string jFile = @"textConfig.cfg"; //test: textConfig.cfg
            string json = File.ReadAllText(jFile);
            ParserJosnConfig.setConfigs(json);
        }

        List<string> messageBuffer = new List<string>();
        private void showMsg(string msg)
        {
            if (textBox2.InvokeRequired)
            {
                textBox2.Invoke(() =>
                {
                    showMsg(msg);
                }
                );
            }
            else
            {
                messageBuffer.Add(msg);
                if (messageBuffer.Count > 100)
                {
                    messageBuffer.RemoveAt(0);
                }
                textBox2.Text = string.Join("", messageBuffer);
                textBox2.Select(textBox2.Text.Length, 0);
                textBox2.ScrollToCaret();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            IParserConfig ips = ParserJosnConfig.getParserConfig("meitulu.me.guochan");

            this.ControlBox = false;
            button2.Enabled = false;
            button3.Visible = false;
            button1.Visible = true;
            label1.Text = "downloading ...";
            XyLog.log("start download");

            try
            {
                await new startScraper().newScrape(
                textBox1.Text,
                new ParserByConfig(ips),
                cts.Token,
                progress);
            }
            catch (OperationCanceledException error)
            {
                XyLog.log("task canceled, break point has saved");
                showMsg("\r\ntask canceled, break point has saved \r\n\r\n");
            }
            catch (Exception error)
            {
                XyLog.log(error);
            }

            XyLog.log("download end");
            this.ControlBox = true;
            button2.Enabled = true;
            button1.Visible = false;
            label1.Text = "downloaded";
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            this.ControlBox = false;
            button2.Enabled = false;
            button3.Visible = false;
            button1.Visible = true;
            label1.Text = "downloading ...";
            XyLog.log("start download");

            try
            {
                await new startScraper().resumeScrape(
                cts.Token,
                progress);
            }
            catch (OperationCanceledException error)
            {
                XyLog.log("task canceled, break point has saved");
            }
            catch (Exception error)
            {
                XyLog.log(error);
            }

            XyLog.log("download end");
            this.ControlBox = true;
            button2.Enabled = true;
            button1.Visible = false;
            label1.Text = "downloaded";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showMsg("\r\nstart cancelling download task ... \r\n\r\n");
            cts.Cancel();
            button1.Visible = false;
        }
    }
}
