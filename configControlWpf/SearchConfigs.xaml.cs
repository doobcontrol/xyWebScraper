using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for SearchConfigs.xaml
    /// </summary>
    public partial class SearchConfigs : UserControl
    {
        public SearchConfigs()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Add the first SearchConfig.
            // When in SearchConfigs() not get isNavgateSearchConfig value yet.
            AddSearchConfig();
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
    }
}
