using xy.scraper.xyWebScraper.Properties;

namespace xy.scraper.xyWebScraper
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            toolTip1.SetToolTip(comboBox1, "select PageModel");
            toolTip1.SetToolTip(textBox1, "input Url");

            txtLog.Visible = false;
            splitter1.Visible = false;
            tbLog.Visible = false;
            pbScrapeFlag.Image = Resources.Button_Blank_Gray_icon;
        }

        private void tbLog_Click(object sender, EventArgs e)
        {
            txtLog.Visible = tbLog.Checked;
            splitter1.Visible = tbLog.Checked;
        }

        private void tbStart_Click(object sender, EventArgs e)
        {
            if (tbStart.Checked)
            {
                pbScrapeFlag.Image = Resources.Button_Blank_Yellow_icon;
                tbLog.Visible = true;
                tbSetting.Visible = false;
            }
            else
            {
                pbScrapeFlag.Image = Resources.Button_Blank_Gray_icon;
                tbLog.Visible = false;
                tbSetting.Visible = true;
            }
            ControlBox = !tbStart.Checked;
        }
    }
}
