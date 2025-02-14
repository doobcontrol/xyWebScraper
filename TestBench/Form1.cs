using System;
using System.Text;
using xy.scraper.page;

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
            await new startScraper().scraper(
                textBox1.Text, 
                new TestParser(),
                progress);
            this.ControlBox = true;
            button1.Enabled = true;
            label1.Text = "downloaded";
        }

        private void showMsg(string msg)
        {
            if (textBox2.InvokeRequired)
            {
                textBox2.Invoke(() => {  //BeginInvoke debug:看是否能解决长时大量任务下的失去响应问题
                    showMsg(msg);
                }
                );
            }
            else
            {
                textBox2.AppendText(msg);
            }
        }

    }
}
