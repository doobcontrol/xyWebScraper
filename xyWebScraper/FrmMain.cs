using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using xy.scraper.configControl;
using xy.scraper.page;
using xy.scraper.page.parserConfig;
using xy.scraper.xyWebScraper.Properties;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace xy.scraper.xyWebScraper
{
    public partial class FrmMain : Form
    {
        IProgress<CReport> progress;
        CancellationTokenSource cts;
        string confnfigFile = @"xyWebScraper.cfg";

        public FrmMain()
        {
            InitializeComponent();

            txtLog.Visible = false;
            splitter1.Visible = false;
            tbLog.Visible = false;
            pbScrapeFlag.Image = Resources.Button_Blank_Gray_icon;
            spbFileTask.Visible = false;

            tbStart.Enabled = false;

            tslbMsg.Text = "";

            setPageModelConfigs();

            progress = new SimpleProgress<CReport>(scrappingReport);

            formateDatagridview(dataGridView1);

            setUiText();
        }
        private void setUiText()
        {
            toolTip1.SetToolTip(cbConfigIdList, Resources.toolTipText_cbConfigIdList);
            toolTip1.SetToolTip(txtUrl, Resources.toolTipText_txtUrl);
            tbLog.ToolTipText = Resources.toolTipText_tbLog;
            tbSetting.ToolTipText = Resources.toolTipText_tbSetting;
            tbBreakPoint.ToolTipText = Resources.toolTipText_tbBreakPoint;
            tbStart.ToolTipText = Resources.toolTipText_tbStart;

            this.Text = Resources.text_AppName;
            lbPageTask.Text = Resources.text_lbPageTask + "0";
            lbCurrentPage.Text = Resources.text_lbCurrentPage;
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
                tslbMsg.Text = Resources.msg_NoPageModel;
            }
        }

        private void tbLog_Click(object sender, EventArgs e)
        {
            splitter1.Visible = tbLog.Checked;
            txtLog.Visible = tbLog.Checked;
            if (!tbLog.Checked)
            {
                messageBuffer.Clear();
                txtLog.Clear();
            }
        }

        private async void tbStart_Click(object sender, EventArgs e)
        {
            if (tbStart.Checked)
            {
                tbStart.ToolTipText = Resources.toolTipText_tbStart_scraping;
                await runScrappingAsync();
            }
            else
            {
                tbStart.Enabled = false;

                tbStart.ToolTipText = Resources.toolTipText_tbStart_waitingCancel; ;

                cts?.Cancel();
            }
        }

        private async void tbBreakPoint_Click(object sender, EventArgs e)
        {
            if (File.Exists(startScraper._breakPointSavePath))
            {
                tbStart.Checked = true;
                tbStart.Enabled = true;
                tbStart.ToolTipText = Resources.toolTipText_tbStart_scraping;
                await runScrappingAsync(true);
            }
            else
            {
                tslbMsg.Text = Resources.msg_Nobreakpoint;
            }
        }

        private void inputCheck()
        {
            if (cbConfigIdList.Text.Trim().Length > 0
                && txtUrl.Text.Trim().Length > 0
                )
            {
                tbStart.Enabled = true;
                tbStart.ToolTipText = Resources.toolTipText_tbStart_active;
            }
            else
            {
                tbStart.Enabled = false;
                tbStart.ToolTipText = Resources.toolTipText_tbStart;
            }
        }
        private void setUIScrappingStatus(bool inScrapping = true)
        {
            tbLog.Visible = inScrapping;
            tbSetting.Visible = !inScrapping;
            tbBreakPoint.Visible = !inScrapping;
            cbConfigIdList.Enabled = !inScrapping;
            txtUrl.Enabled = !inScrapping;
            ControlBox = !inScrapping;

            if (inScrapping)
            {
                pbScrapeFlag.Image = Resources.Button_Blank_Green_icon;

            }
            else
            {
                pbScrapeFlag.Image = Resources.Button_Blank_Gray_icon;

                inputCheck();
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

        private async Task runScrappingAsync(bool breakPointResume = false)
        {
            try
            {
                setUIScrappingStatus();

                cts = new CancellationTokenSource();
                if (breakPointResume)
                {
                    await new startScraper().resumeScrape(
                    cts.Token,
                    progress);
                }
                else
                {
                    await new startScraper().newScrape(
                    txtUrl.Text,
                    cbConfigIdList.Text,
                    cts.Token,
                    progress);
                }
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
                setUIScrappingStatus(false);
            }
        }
        Dictionary<string, string>? fileTaskDict;
        List<(string, string)>? pageTaskList;
        private void scrappingReport(CReport data)
        {
            switch (data.ReportType)
            {
                case CReport.rType.Msg:
                    tslbMsg.Text = data.Msg;
                    showMsg(data.Msg);
                    break;
                case CReport.rType.Error:
                    tslbMsg.Text = data.Msg;
                    showMsg(data.Msg);
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

                case CReport.rType.PageTask:
                    pageTaskList = data.PageTaskList;
                    showPageTaskInfo("");
                    break;
                case CReport.rType.PageStart:
                    showPageTaskInfo(data.PageUrl);
                    break;
                case CReport.rType.PageDone:
                    showPageTaskInfo(data.PageRusult.pageUrl + " ("
                        + (data.PageRusult.succeed ? 
                            Resources.msg_succeed : Resources.msg_fail)
                        + ")");
                    showMsg("");
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
        private void formateDatagridview(DataGridView dgv)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
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
        private void showPageTaskInfo(string CurrentPage)
        {

            DataGridView dgv = dataGridView1;

            if (panelPageTaskInfo.InvokeRequired)
            {
                panelPageTaskInfo.Invoke(() =>
                {
                    showPageTaskInfo(CurrentPage);
                }
                );
            }
            else
            {
                lbPageTask.Text = Resources.text_lbPageTask + pageTaskList.Count;
                lbCurrentPage.Text = Resources.text_lbCurrentPage + CurrentPage;
            }
        }

        private void tbSetting_Click(object sender, EventArgs e)
        {
            Form pageSetting = new Form();
            pageSetting.Height *= 2;
            pageSetting.Width *= 3;
            pageSetting.Text = tbSetting.ToolTipText;

            ScraperConfig sageScraper = new ScraperConfig();
            sageScraper.Saved += pageConifg_saved;
            sageScraper.Dock = DockStyle.Fill;

            if (File.Exists(confnfigFile))
            {
                string json = File.ReadAllText(confnfigFile);
                sageScraper.JsonObj = JsonSerializer.Deserialize<JsonArray>(json);
            }

            pageSetting.Controls.Add(sageScraper);
            pageSetting.Show();
        }
        private void pageConifg_saved(object? sender, EventArgs e)
        {
            setPageModelConfigs();
        }

        #region scrapping log box

        List<string> messageBuffer = new List<string>();
        int messageBufferSize = 100;
        private void showMsg(string msg)
        {
            if (tbLog.Checked)
            {
                messageBuffer.Add(msg);
                if (messageBuffer.Count > messageBufferSize)
                {
                    messageBuffer.RemoveAt(0);
                }
                updateMsg(string.Join("\r\n", messageBuffer));
            }
        }
        private void updateMsg(string msg)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(() =>
                {
                    updateMsg(msg);
                }
                );
            }
            else
            {
                txtLog.Text = msg;
                txtLog.Select(txtLog.Text.Length, 0);
                txtLog.ScrollToCaret();
            }
        }

        #endregion
    }
}
