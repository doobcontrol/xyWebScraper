using System;
using System.Text;
using xy.scraper.page;
using xy.scraper.page.parserConfig;

namespace TestBench
{
    public partial class Form1 : Form
    {
        Progress<string> progress = new Progress<string>();

        public Form1()
        {
            InitializeComponent();
            label1.Text = "";

            progress.ProgressChanged += async (_, data) =>
            {
                showMsg(data + "\r\n");
            };
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            this.ControlBox = false;
            button1.Enabled = false;
            label1.Text = "downloading ...";
            await new startScraper().scrape(
                textBox1.Text,
                new TestParser(),
                progress);
            this.ControlBox = true;
            button1.Enabled = true;
            label1.Text = "downloaded";
        }

        List<string> messageBuffer = new List<string>();
        private void showMsg(string msg)
        {
            if (textBox2.InvokeRequired)
            {
                textBox2.Invoke(() =>
                {  //BeginInvoke debug:看是否能解决长时大量任务下的失去响应问题
                    showMsg(msg);
                }
                );
            }
            else
            {
                messageBuffer.Add(msg);
                if(messageBuffer.Count > 100)
                {
                    messageBuffer.RemoveAt(0);
                }
                textBox2.Text = string.Join("", messageBuffer);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string jFile = @"textConfig.cfg"; //test: textConfig.cfg
            string json = File.ReadAllText(jFile);
            ParserJosnConfig.setConfigs(json); 
            IParserConfig ips = ParserJosnConfig.getParserConfig("meitulu.me.guochan");

            this.ControlBox = false;
            button2.Enabled = false;
            label1.Text = "downloading ...";

            await new startScraper().scrape(
                textBox1.Text,
                new ParserByConfig(ips),
                progress);

            this.ControlBox = true;
            button2.Enabled = true;
            label1.Text = "downloaded";
        }
    }
}
