using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xy.scraper.xyWebScraper.Properties;
using xySoft.log;

namespace xy.scraper.xyWebScraper
{
    public partial class FrmLoading: Form
    {
        public FrmLoading(CancellationToken ct)
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
            lbMsg.Text = Resources.LoadingSplashMsg;
            lbMsg.BorderStyle = BorderStyle.FixedSingle;

            _ = Task.Run(() =>
            {
                while(true)
                {
                    if (ct.IsCancellationRequested)
                    {
                        break;
                    }
                }
                closeMe();
            });
        }
        private void closeMe()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(() =>
                {
                    closeMe();
                }
                );
            }
            else
            {
                Close();
            }
        }
    }
}
