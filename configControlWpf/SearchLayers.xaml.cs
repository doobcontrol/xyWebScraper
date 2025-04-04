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
    /// Interaction logic for SearchLayers.xaml
    /// </summary>
    public partial class SearchLayers : UserControl
    {
        public SearchLayers()
        {
            InitializeComponent();
            addSearchLayer();
        }
        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            addSearchLayer();
        }
        private void addSearchLayer()
        {
            SearchLayer newSl = new SearchLayer();
            panelSearchLayers.Children.Add(newSl);
            newSl.GotFocus += new RoutedEventHandler((sender, e) =>
            {
                SearchLayer focusedSearchLayer = (SearchLayer)sender;
                focusedSearchLayer.Background = newSl.focusBrush;
                focusedSearchLayer.Foreground = Brushes.White;
                panelSearchLayers.Tag = newSl;
                foreach (SearchLayer sl in panelSearchLayers.Children)
                {
                    if (sl != focusedSearchLayer)
                    {
                        sl.Background = sl.unfocusBrush;
                        sl.Foreground = Brushes.Black;
                    }
                }
            });
            newSl.Focus();
        }
        private void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (panelSearchLayers != null
                && panelSearchLayers.Children.Count > 1
                && panelSearchLayers.Tag != null
                && panelSearchLayers.Tag is SearchLayer);
        }
        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SearchLayer focusedSearchLayer = (SearchLayer)panelSearchLayers.Tag;
            int index = panelSearchLayers.Children.IndexOf(focusedSearchLayer);
            index--;
            if (index < 0)
            {
                index = 0;
            }
            panelSearchLayers.Children.Remove(focusedSearchLayer);
            panelSearchLayers.Tag = null;
            panelSearchLayers.Children[index].Focus();
        }

        public bool SearchMultiResults
        {
            get
            {
                return cbSearchMultiResults.IsChecked == true;
            }
            set
            {
                cbSearchMultiResults.IsChecked = value;
            }
        }
        public JsonArray JsonObj
        {
            get
            {
                JsonArray ja = new JsonArray();
                foreach (SearchLayer sl in panelSearchLayers.Children)
                {
                    ja.Add(sl.JsonObj);
                }
                return ja;
            }
            set
            {
                panelSearchLayers.Children.Clear();
                foreach (JsonObject jo in value)
                {
                    SearchLayer sl = new SearchLayer();
                    sl.JsonObj = jo;
                    panelSearchLayers.Children.Add(sl);
                }
            }
        }
    }
}
