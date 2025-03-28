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
    /// Interaction logic for SearchConfig.xaml
    /// </summary>
    public partial class SearchConfig : UserControl
    {
        public SearchConfig()
        {
            InitializeComponent();
        }
        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute =
                txtSearch.Text != null
                && txtSearch.Text.Trim() != "";
        }
        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            lbReplaceList.Items.Add(txtSearch.Text.Trim());
            txtSearch.Text = "";
        }
        private void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute =
                lbReplaceList != null
                && lbReplaceList.SelectedIndex != -1;
        }
        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            lbReplaceList.Items.RemoveAt(lbReplaceList.SelectedIndex);
        }

        public JsonObject JsonObj
        {
            get
            {
                JsonObject json = new JsonObject();
                json[JCfgName.search] = slSearchLayers.JsonObj;

                JsonArray replaces = new JsonArray();
                json[JCfgName.replaces] = replaces;
                foreach (string item in lbReplaceList.Items)
                {
                    JsonValue replace = JsonValue.Create(item);
                    replaces.Add(replace);
                }

                json[JCfgName.AddBefore] = txtAddBefore.Text;
                json[JCfgName.AddAfter] = txtAddAfter.Text;
                json[JCfgName.SearchList] = 
                    slSearchLayers.SearchMultiResults;

                return json;
            }

            set
            {
                txtAddBefore.Text
                    = value[JCfgName.AddBefore].GetValue<String>();
                txtAddAfter.Text
                    = value[JCfgName.AddAfter].GetValue<String>();
                slSearchLayers.SearchMultiResults
                    = value[JCfgName.SearchList].GetValue<bool>();

                JsonArray searchLayers
                    = value[JCfgName.search].AsArray();
                slSearchLayers.JsonObj = searchLayers;

                JsonArray replaces
                    = value[JCfgName.replaces].AsArray();
                foreach (JsonValue item in replaces)
                {
                    lbReplaceList.Items
                        .Add(item.GetValue<String>());
                }
            }
        }
    }
}
