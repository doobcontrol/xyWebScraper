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
        }
        private void AddPageConfig()
        {
            PageConfig pageConfig = new PageConfig();
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
            AddPageConfig();
            ((PageConfig)((TabItem)tabControl.SelectedItem).Content).JsonObj = sPgCfg;
        }

    }
}
