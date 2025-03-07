using System.Threading.Tasks;
using xy.scraper.page;
using xy.scraper.page.parserConfig;
using xy.scraper.xyWebScraper.Properties;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace xy.scraper.xyWebScraper
{
    public partial class FrmMain : Form
    {
        Progress<CReport> progress;
        CancellationTokenSource cts;
        string confnfigFile = @"xyWebScraper.cfg";

        public FrmMain()
        {
            InitializeComponent();

            toolTip1.SetToolTip(cbConfigIdList, "select PageModel");
            toolTip1.SetToolTip(txtUrl, "input Url");

            txtLog.Visible = false;
            splitter1.Visible = false;
            tbLog.Visible = false;
            pbScrapeFlag.Image = Resources.Button_Blank_Gray_icon;
            spbFileTask.Visible = false;

            tbStart.Enabled = false;
            tbStart.ToolTipText = 
                "please select page model, and input page url to active this button";

            tslbMsg.Text = "";
            this.Text = "xyWebScraper";

            setPageModelConfigs();

            progress = new Progress<CReport>(scrappingReport);

            formateDatagridview(dataGridView1);
        }

        private void setPageModelConfigs()
        {
            if (File.Exists(confnfigFile))
            {
                string json = File.ReadAllText(confnfigFile);
                ParserJosnConfig.setConfigs(json);
                cbConfigIdList.Items.Clear();
                cbConfigIdList.Items.AddRange(ParserJosnConfig.getConfigIdList().ToArray());
                if (cbConfigIdList.Items.Count > 0)
                {
                    cbConfigIdList.SelectedIndex = 0;
                }
            }
            else
            {
                tslbMsg.Text = "no page model configed";
            }
        }

        private void tbLog_Click(object sender, EventArgs e)
        {
            txtLog.Visible = tbLog.Checked;
            splitter1.Visible = tbLog.Checked;
        }

        private async void tbStart_Click(object sender, EventArgs e)
        {
            if (tbStart.Checked)
            {
                tbStart.ToolTipText =
                    "push to cancel scrap";
                await runScrappingAsync();
            }
            else
            {
                tbStart.Enabled = false;

                tbStart.ToolTipText =
                    "waitting for canceled...";

                cts?.Cancel();
            }
        }

        private void inputCheck()
        {
            if (cbConfigIdList.Text.Trim().Length > 0
                && txtUrl.Text.Trim().Length > 0
                )
            {
                tbStart.Enabled = true;
                tbStart.ToolTipText =
                    "push to start scrap";
            }
            else
            {
                tbStart.Enabled = false;
                tbStart.ToolTipText =
                    "please select page model, and input page url to active this button";
            }
        }

        private void txtUrl_TextChanged(object sender, EventArgs e)
        {
            inputCheck();
        }

        private void cbConfigIdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputCheck();
        }

        private async Task runScrappingAsync()
        {
            try
            {
                pbScrapeFlag.Image = Resources.Button_Blank_Yellow_icon;
                tbLog.Visible = true;
                tbSetting.Visible = false;
                cbConfigIdList.Enabled = false;
                txtUrl.Enabled = false;

                ControlBox = false;

                cts = new CancellationTokenSource();
                await new startScraper().newScrape(
                txtUrl.Text,
                cbConfigIdList.Text,
                cts.Token,
                progress);
            }
            catch (OperationCanceledException error)
            {
                tslbMsg.Text = error.Message;
            }
            catch (Exception error)
            {
                tslbMsg.Text = error.Message;
            }
            finally
            {
                pbScrapeFlag.Image = Resources.Button_Blank_Gray_icon;
                tbLog.Visible = false;
                tbSetting.Visible = true;
                cbConfigIdList.Enabled = true;
                txtUrl.Enabled = true;

                tbStart.Enabled = true;
                tbStart.ToolTipText =
                    "push to start scrap";

                ControlBox = true;
            }
        }
        Dictionary<string, string>? fileTaskDict;
        private void scrappingReport(CReport data)
        {
            switch (data.ReportType)
            {
                case CReport.rType.Msg:
                    tslbMsg.Text = data.Msg;
                    break;
                case CReport.rType.Error:
                    tslbMsg.Text = data.Msg;
                    break;

                case CReport.rType.FileTask:
                    fileTaskDict = data.FileTaskDict;
                    spbFileTask.Minimum = 0;
                    spbFileTask.Maximum = data.FileTaskDict.Count;
                    spbFileTask.Value = 0;
                    spbFileTask.Visible = true;
                    showFileTask(data.FileTaskDict);
                    break;
                case CReport.rType.FileStart:
                    changeRowColor(fileRowDic[data.FileUrl],
                        ColorTranslator.FromHtml("#EAE0D5"),
                        ColorTranslator.FromHtml("#22333B")
                        );
                    break;
                case CReport.rType.FileDone:
                    spbFileTask.Value = spbFileTask.Maximum - fileTaskDict.Count;
                    if (data.FileRusult.succeed)
                    {
                        changeRowColor(fileRowDic[data.FileRusult.fileUrl],
                        ColorTranslator.FromHtml("#81E979"),
                        ColorTranslator.FromHtml("#595A4A"));
                    }
                    else
                    {
                        changeRowColor(fileRowDic[data.FileRusult.fileUrl],
                        ColorTranslator.FromHtml("#DBC2CF"),
                        ColorTranslator.FromHtml("#DBC2CF"));
                    }
                    break;
            }
        }
        Dictionary<string, DataGridViewRow> fileRowDic;
        private void showFileTask(Dictionary<string, string>? fileTaskDict)
        {
            DataGridView dgv = dataGridView1;

            if (dgv.InvokeRequired)
            {
                dgv.Invoke(() =>
                {
                    showFileTask(fileTaskDict);
                }
                );
            }
            else
            {
                dgv.Rows.Clear();

                DataGridViewRow row;
                fileRowDic = new Dictionary<string, DataGridViewRow>();
                foreach (string key in fileTaskDict.Keys)
                {
                    row = new DataGridViewRow();
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = key });
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = fileTaskDict[key] });
                    dgv.Rows.Add(row);
                    dgv.AutoResizeColumns();
                    fileRowDic.Add(key, row);
                }
            }
        }
        private void changeRowColor(DataGridViewRow row, Color bcolor, Color fcolor)
        {
            DataGridView dgv = dataGridView1;

            if (dgv.InvokeRequired)
            {
                dgv.Invoke(() =>
                {
                    changeRowColor(row, bcolor, fcolor);
                }
                );
            }
            else
            {
                row.DefaultCellStyle.BackColor = bcolor;
                row.DefaultCellStyle.ForeColor = fcolor;

                row.DefaultCellStyle.SelectionBackColor = row.DefaultCellStyle.BackColor;
                row.DefaultCellStyle.SelectionForeColor = row.DefaultCellStyle.ForeColor;
            }
        }
        static public void formateDatagridview(DataGridView dgv)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.DefaultCellStyle.SelectionBackColor = dgv.DefaultCellStyle.BackColor;
            dgv.DefaultCellStyle.SelectionForeColor = dgv.DefaultCellStyle.ForeColor;

            dgv.Columns.Clear();
            dgv.Columns.Add("url", "url");
            dgv.Columns.Add("file", "file");
        }
    }
}
