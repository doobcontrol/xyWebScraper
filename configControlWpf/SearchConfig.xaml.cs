﻿using System;
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
            this.DataContext = this;

            aguAutoGrowthUrl.Visibility = Visibility.Collapsed;
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            if(((CheckBox)sender).IsChecked == true)
            {
                tcSearch.Visibility = Visibility.Collapsed;
                aguAutoGrowthUrl.Visibility = Visibility.Visible;
            }
            else
            {
                aguAutoGrowthUrl.Visibility = Visibility.Collapsed;
                tcSearch.Visibility = Visibility.Visible;
            }
        }
        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute =
                txtReplace.Text != null
                && txtReplace.Text.Trim() != "";
        }
        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            lbReplaceList.Items.Add(txtReplace.Text.Trim());
            txtReplace.Text = "";
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

        private Visibility showNavgateSearchConfig = Visibility.Collapsed;
        public Visibility ShowNavgateSearchConfig
        {
            get
            {
                return showNavgateSearchConfig;
            }
        }
        public bool IsNavgateSearchConfig
        {
            get
            {
                return showNavgateSearchConfig == Visibility.Visible;
            }
            set
            {
                showNavgateSearchConfig = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public string PageConfigID
        {
            get {

                return txtPageConfigID.Text;
            }
            
            set {
                txtPageConfigID.Text = value;
            }
        }
        public bool IsAutoUrl
        {
            get
            {
                return cbIsAutoUrl.IsChecked == true;
            }
            set
            {
                cbIsAutoUrl.IsChecked = value;
            }
        }
        public JsonObject AutoGrowthUrl
        {
            get
            {
                return aguAutoGrowthUrl.JsonObj;
            }
            set
            {
                aguAutoGrowthUrl.JsonObj = value;
            }
        }
        public JsonObject JsonObj
        {
            get
            {
                JsonObject json = new JsonObject();
                if(cbIsAutoUrl.IsChecked == true)
                {
                    json[JCfgName.AutoGrowthUrl] = aguAutoGrowthUrl.JsonObj;
                }
                else
                {
                    json[JCfgName.search] = slSearchLayers.JsonObj;
                }

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
