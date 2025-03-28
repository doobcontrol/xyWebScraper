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
    /// Interaction logic for SearchConfig.xaml
    /// </summary>
    public partial class SearchConfig : UserControl
    {
        public SearchConfig()
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
            newSl.GotFocus+= new RoutedEventHandler((sender, e) => {
                SearchLayer focusedSearchLayer = (SearchLayer)sender;
                focusedSearchLayer.Background = newSl.focusBrush;
                panelSearchLayers.Tag = newSl;
                foreach(SearchLayer sl in panelSearchLayers.Children)
                {
                    if (sl != focusedSearchLayer)
                    {
                        sl.Background = sl.unfocusBrush;
                    }
                }
            });
        }
        private void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (panelSearchLayers !=null 
                && panelSearchLayers.Tag is SearchLayer);
        }
        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SearchLayer focusedSearchLayer = (SearchLayer)panelSearchLayers.Tag;
            panelSearchLayers.Children.Remove(focusedSearchLayer);
            panelSearchLayers.Tag = null;
        }
    }
}
