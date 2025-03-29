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
    /// Interaction logic for PageConfig.xaml
    /// </summary>
    public partial class PageConfig : UserControl
    {
        public PageConfig()
        {
            InitializeComponent();
            cbList.Add(cbpaths, tipaths);
            cbList.Add(cbfiles, tifiles);
            cbList.Add(cbnexts, tinexts);
            tabControl.Items.Clear();
        }

        Dictionary<CheckBox, TabItem> cbList = 
            new Dictionary<CheckBox, TabItem>();
        private void cbpaths_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int TabItemIndex = tabControl.Items.IndexOf(cbList[checkBox]);

            tabControl.Items.Clear();
            foreach (CheckBox cb in cbList.Keys)
            {
                if (cb.IsChecked == true)
                {
                    tabControl.Items.Add(cbList[cb]);
                }
            }

            if (checkBox.IsChecked == true)
            {
                tabControl.SelectedItem = cbList[(CheckBox)sender];
            }
            else
            {
                if (tabControl.Items.Count > 0)
                {
                    TabItemIndex--;
                    if (TabItemIndex < 0)
                    {
                        TabItemIndex = 0;
                    }
                    tabControl.SelectedIndex = TabItemIndex;
                }
            }
        }
    }
}
