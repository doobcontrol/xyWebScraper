using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using xy.scraper.configControl.Properties;
using System.Text.Json.Nodes;
using xy.scraper.page.parserConfig;

namespace xy.scraper.configControl
{
    public partial class AutoGrowthUrl: UserControl
    {
        public AutoGrowthUrl()
        {
            InitializeComponent();

            setUiText();
        }
        private void setUiText()
        {
            lbAutoGrowthPar.Text = Resources.lbAutoGrowthPar_text;
            lbCheckExist.Text = Resources.lbCheckExist_text;
        }

        public JsonObject JsonObj
        {
            get
            {
                JsonObject json = new JsonObject();
                json[JCfgName.AutoGrowthPar] = txtAutoGrowthPar.Text;
                json[JCfgName.CheckExist] = txtCheckExist.Text;
                return json;
            }

            set
            {
                txtAutoGrowthPar.Text = 
                    value[JCfgName.AutoGrowthPar].GetValue<String>();
                txtCheckExist.Text = 
                    value[JCfgName.CheckExist].GetValue<String>();
            }
        }
    }
}
