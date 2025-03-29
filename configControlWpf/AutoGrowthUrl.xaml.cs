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
using xy.scraper.page.parserConfig;

namespace configControlWpf
{
    /// <summary>
    /// Interaction logic for AutoGrowthUrl.xaml
    /// </summary>
    public partial class AutoGrowthUrl : UserControl
    {
        public AutoGrowthUrl()
        {
            InitializeComponent();
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
