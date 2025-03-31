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
    /// Interaction logic for SearchConfigs.xaml
    /// </summary>
    public partial class SearchConfigs : UserControl
    {
        public SearchConfigs()
        {
            InitializeComponent();
            
            Loaded += (s, e) =>
            {
                if (tabControl.Items.Count == 0)
                {
                    // Add the first SearchConfig.
                    // This need isNavgateSearchConfig been set, but xmal can't put parameters to constructor.
                    // So when in SearchConfigs() not get isNavgateSearchConfig value yet.
                    AddSearchConfig();
                }
            };
        }
        private void AddSearchConfig()
        {
            tabControl.Items.Add(
                new TabItem()
                {
                    Header = "Search",
                    Content = new SearchConfig()
                    {
                        IsNavgateSearchConfig = isNavgateSearchConfig
                    }
                });
            tabControl.SelectedIndex = tabControl.Items.Count - 1;
        }
        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddSearchConfig();
        }
        private void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute =
                tabControl != null
                && tabControl.Items.Count > 1;
        }
        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            object scItem = tabControl.SelectedItem;
            int index = tabControl.SelectedIndex;
            index--;
            if (index < 0)
            {
                index = 0;
            }
            tabControl.Items.Remove(scItem);
            tabControl.SelectedIndex = index;
        }

        private bool isNavgateSearchConfig = false;
        public bool IsNavgateSearchConfig
        {
            get
            {
                return isNavgateSearchConfig;
            }
            set
            {
                isNavgateSearchConfig = value;
            }
        }

        public JsonArray JsonObj
        {
            get
            {
                JsonArray searchConfigs = new JsonArray();
                foreach (TabItem tabItem in tabControl.Items)
                {

                    SearchConfig searchConfig = (SearchConfig)tabItem.Content;

                    if(IsNavgateSearchConfig)
                    {
                        JsonObject searchConfigObj = new JsonObject();
                        searchConfigObj[JCfgName.cfgid] =
                            searchConfig.PageConfigID;

                        if (searchConfig.IsAutoUrl)
                        {
                            searchConfigObj[JCfgName.AutoGrowthUrl] = searchConfig.AutoGrowthUrl;
                        }
                        else
                        {
                            searchConfigObj[JCfgName.searchs] = searchConfig.JsonObj;
                        }
                        searchConfigs.Add(searchConfigObj);
                    }
                    else
                    {
                        searchConfigs.Add(searchConfig.JsonObj);
                    }
                }
                return searchConfigs;
            }
            set
            {
                tabControl.Items.Clear();
                foreach (JsonObject jsonObj in value)
                {
                    SearchConfig searchConfig = new SearchConfig()
                    {
                        IsNavgateSearchConfig = isNavgateSearchConfig
                    };
                    tabControl.Items.Add(
                        new TabItem()
                        {
                            Header = "Search",
                            Content = searchConfig
                        });
                    tabControl.SelectedIndex = tabControl.Items.Count - 1;


                    if (IsNavgateSearchConfig)
                    {
                        searchConfig.PageConfigID =
                            jsonObj[JCfgName.cfgid].GetValue<string>();
                        if (jsonObj.ContainsKey(JCfgName.AutoGrowthUrl))
                        {
                            searchConfig.IsAutoUrl = true;
                            searchConfig.AutoGrowthUrl =
                                jsonObj[JCfgName.AutoGrowthUrl].AsObject();
                        }
                        else
                        {
                            searchConfig.IsAutoUrl = false;
                            searchConfig.JsonObj = 
                                jsonObj[JCfgName.searchs].AsObject();
                        }
                    }
                    else
                    {
                        searchConfig.JsonObj = jsonObj;
                    }
                }
            }
        }
    }
}
