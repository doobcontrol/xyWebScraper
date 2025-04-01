using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using xy.scraper.page;
using xy.scraper.page.parserConfig;

namespace configControlWpf
{
    /// <summary>
    /// Interaction logic for ScraperConfig.xaml
    /// </summary>
    public partial class ScraperConfig : UserControl
    {
        public ScraperConfig()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                if (File.Exists(ConfnfigFile))
                {
                    string jsonString = File.ReadAllText(confnfigFile);
                    JsonObj = JsonNode.Parse(jsonString).AsArray();
                }
            };
            searchTest.getHtmlString += async (url) =>
            {
                string html =
                        await new HttpClientDownloader().GetHtmlStringAsync(
                            url,
                            Encoding);
                return html;
            };
            searchTest.getSearchJsonObject += () =>
            {
                return CurrentSearchConfig;
            };
        }

        public string Encoding
        {
            get
            {
                return ((PageConfig)((TabItem)tabControl.SelectedItem).Content).Encoding;
            }
        }
        public JsonArray JsonObj
        {
            get
            {
                JsonArray json = new JsonArray();
                foreach (TabItem ti in tabControl.Items)
                {
                    PageConfig sc = (PageConfig)ti.Content;
                    json.Add(sc.JsonObj);
                }

                return json;
            }

            set
            {
                tabControl.Items.Clear();
                foreach (JsonObject item in value)
                {
                    PageConfig pc = new PageConfig();
                    AddPageConfig(pc);
                    pc.JsonObj = item;
                }
            }
        }

        private string confnfigFile = "scraperConfig.json";
        public string ConfnfigFile { 
            get => confnfigFile; 
            set => confnfigFile = value; 
        }

        private void AddPageConfig(PageConfig? pc = null)
        {
            if(pc == null)
            {
                pc = new PageConfig();
            }
            PageConfig pageConfig = pc;
            string pageID = "Page Model " 
                + (tabControl.Items.Count + 1);
            TabItem tabItem = new TabItem()
            {
                Content = pageConfig
            };
            tabControl.Items.Add(tabItem);

            Binding binding = new Binding("PageID");
            binding.Source = pageConfig;
            tabItem.SetBinding(TabItem.HeaderProperty, binding);
            pageConfig.PageID = pageID;
            
            tabControl.SelectedIndex = tabControl.Items.Count - 1;
        }

        #region Commands
        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddPageConfig();
        }
        private void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute =
                tabControl != null
                && tabControl.Items.Count > 0;
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
        private void CopyCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute =
                tabControl != null
                && tabControl.Items.Count > 0;
        }
        private void CopyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            JsonObject sPgCfg = 
                (((PageConfig)((TabItem)tabControl.SelectedItem).Content)).JsonObj;
            PageConfig pageConfig = new PageConfig();
            AddPageConfig(pageConfig);
            pageConfig.JsonObj = sPgCfg;
        }

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "page config files (*.cfg)|*.cfg";
            if (openFileDialog.ShowDialog() == true)
            {
                confnfigFile = openFileDialog.FileName;
                string jsonString = File.ReadAllText(confnfigFile);
                JsonObj = JsonNode.Parse(jsonString).AsArray();
            }
        }
        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute =
                tabControl != null
                && tabControl.Items.Count > 0;
        }
        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string jsonString = JsonSerializer.Serialize(JsonObj);
            File.WriteAllText(ConfnfigFile, jsonString);
        }
        private void SaveAsCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute =
                tabControl != null
                && tabControl.Items.Count > 0;
        }
        private void SaveAsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<string> pageModelIdList = new List<string>();
            foreach (TabItem ti in tabControl.Items)
            {
                pageModelIdList.Add(((PageConfig)ti.Content).PageID);
            }
            PageModelSelector pms = new PageModelSelector()
                {
                    PageModelIdList = pageModelIdList
                };
            EventHandler okHandler =
                (o, e) => {
                    topGrid.Children.Remove(pms);
                    dpMain.IsEnabled = true;
                    dpMain.Opacity = 1;

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "page config files (*.cfg)|*.cfg";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string saveFile = saveFileDialog.FileName;
                        JsonArray jsonArray = JsonObj;
                        List<JsonObject> selectedPageModelList = new List<JsonObject>();
                        foreach (JsonObject keyValuePairs in jsonArray)
                        {
                            if (!pms.SelectedPageModelIdList.Contains(keyValuePairs[JCfgName.cfgid].GetValue<string>()))
                            {
                                selectedPageModelList.Add(keyValuePairs);
                            }
                        }
                        foreach (JsonObject keyValuePairs in selectedPageModelList)
                        {
                            jsonArray.Remove(keyValuePairs);
                        }
                        string jsonString = JsonSerializer.Serialize(jsonArray);
                        File.WriteAllText(saveFile, jsonString);
                    }
                };
            EventHandler cancelHandler =
                (o, e) => {

                    topGrid.Children.Remove(pms);
                    dpMain.IsEnabled = true;
                    dpMain.Opacity = 1;
                };
            pms.okHandler += okHandler;
            pms.cancelHandler += cancelHandler;

            dpMain.IsEnabled = false;
            dpMain.Opacity = 0.5;
            topGrid.Children.Add(pms);
        }
        private void ImportCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute =
                tabControl != null;
        }
        private void ImportCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "page config files (*.cfg)|*.cfg";
            if (openFileDialog.ShowDialog() == true)
            {
                confnfigFile = openFileDialog.FileName;
                string jsonString = File.ReadAllText(confnfigFile);
                JsonArray ja = JsonNode.Parse(jsonString).AsArray();
                List<string> pageModelIdList = new List<string>();
                foreach (JsonObject item in ja)
                {
                    pageModelIdList.Add(item[JCfgName.cfgid].GetValue<string>());
                }
                PageModelSelector pms = new PageModelSelector()
                    {
                        PageModelIdList = pageModelIdList
                    };
                EventHandler okHandler =
                    (o, e) => {
                        topGrid.Children.Remove(pms);
                        dpMain.IsEnabled = true;
                        dpMain.Opacity = 1;

                        List<JsonObject> selectedPageModelList = new List<JsonObject>();
                        foreach (JsonObject keyValuePairs in ja)
                        {
                            if (pms.SelectedPageModelIdList.Contains(keyValuePairs[JCfgName.cfgid].GetValue<string>()))
                            {
                                PageConfig pc = new PageConfig();
                                AddPageConfig(pc);
                                pc.JsonObj = keyValuePairs;
                            }
                        }
                    };
                EventHandler cancelHandler =
                    (o, e) => {

                        topGrid.Children.Remove(pms);
                        dpMain.IsEnabled = true;
                        dpMain.Opacity = 1;
                    };
                pms.okHandler += okHandler;
                pms.cancelHandler += cancelHandler;

                dpMain.IsEnabled = false;
                dpMain.Opacity = 0.5;
                topGrid.Children.Add(pms);
            }
        }

        private void TestCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute =
                tabControl != null
                && tabControl.Items.Count > 0;
        }
        private void TestCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
        }
        #endregion

        //for search test
        public JsonObject CurrentSearchConfig
        {
            get
            {
                return ((PageConfig)((TabItem)tabControl.SelectedItem).Content).CurrentSearchConfig;
            }
        }
    }

    public static class ScCmd
    {
        public static readonly RoutedUICommand Import = new RoutedUICommand
            (
                "Import",
                "Import",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.I, ModifierKeys.Alt)
                }
            );
        public static readonly RoutedUICommand Test = new RoutedUICommand
            (
                "Test",
                "Test",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.T, ModifierKeys.Alt)
                }
            );
    }
}
