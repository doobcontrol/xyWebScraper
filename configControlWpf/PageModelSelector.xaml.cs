using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for PageModelSelector.xaml
    /// </summary>
    public partial class PageModelSelector : UserControl, INotifyPropertyChanged
    {
        public PageModelSelector()
        {
            InitializeComponent();
            DataContext = this;

            lbPageModelId.SelectionChanged += (s, e) =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HasItemSelected"));
            };
        }

        List<string> pageModelIdList;
        public List<string> PageModelIdList { 
            get => pageModelIdList; 
            set => pageModelIdList = value; }

        List<string> selectedPageModelIdList;
        public List<string> SelectedPageModelIdList { 
            get => selectedPageModelIdList;}
        public bool HasItemSelected { get => lbPageModelId.SelectedItems.Count > 0; }

        public event EventHandler okHandler;
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            selectedPageModelIdList = new List<string>();
            foreach (string items in lbPageModelId.SelectedItems)
            {
                selectedPageModelIdList.Add(items);
            }
            okHandler?.Invoke(this, e);
            clearEventHandler();
        }

        public event EventHandler cancelHandler;
        public event PropertyChangedEventHandler? PropertyChanged;

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            cancelHandler?.Invoke(this, e);
            clearEventHandler();
        }
        private void clearEventHandler()
        {
            foreach (Delegate d in okHandler.GetInvocationList())
            {
                okHandler -= (EventHandler)d;
            }
            foreach (Delegate d in cancelHandler.GetInvocationList())
            {
                cancelHandler -= (EventHandler)d;
            }
        }
    }
}
