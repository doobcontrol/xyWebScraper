using System;
using System.Collections.Generic;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;
using xy.scraper.page.parserConfig;
using System.ComponentModel;

namespace configControlWpf
{
    /// <summary>
    /// Interaction logic for PageConfig.xaml
    /// </summary>
    public partial class PageConfig : UserControl,INotifyPropertyChanged
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

        public event PropertyChangedEventHandler? PropertyChanged;

        private void cbSearch_CheckedChanged(object sender, RoutedEventArgs e)
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

        public string PageID
        {
            get
            {
                return txtPageID.Text;
            }

            set
            {
                txtPageID.Text = value;
                NotePageIDPropertyChanged();
            }
        }
        public string Encoding
        {
            get
            {
                return txtCoding.Text;
            }
            set
            {
                txtCoding.Text = value;
            }
        }
        public JsonObject JsonObj
        {
            get
            {
                JsonObject json = new JsonObject();

                json[JCfgName.cfgid] = PageID;
                json[JCfgName.encoding] = txtCoding.Text;

                if (cbpaths.IsChecked == true)
                {
                    json[JCfgName.paths] = scpaths.JsonObj;
                }

                if (cbfiles.IsChecked == true)
                {
                    json[JCfgName.files] = scfiles.JsonObj;
                }

                if (cbnexts.IsChecked == true)
                {
                    json[JCfgName.nexts] = scnexts.JsonObj;
                }

                return json;
            }

            set
            {
                PageID
                    = value[JCfgName.cfgid].GetValue<String>();
                txtCoding.Text
                    = value[JCfgName.encoding].GetValue<String>();

                if (value.ContainsKey(JCfgName.paths))
                {
                    scpaths.JsonObj = value[JCfgName.paths].AsArray();
                    cbpaths.IsChecked = true;
                }
                else
                {
                    cbpaths.IsChecked = false;
                }

                if (value.ContainsKey(JCfgName.files))
                {
                    scfiles.JsonObj = value[JCfgName.files].AsArray();
                    cbfiles.IsChecked = true;
                }
                else
                {
                    cbfiles.IsChecked = false;
                }

                if (value.ContainsKey(JCfgName.nexts))
                {
                    scnexts.JsonObj = value[JCfgName.nexts].AsArray();
                    cbnexts.IsChecked = true;
                }
                else
                {
                    cbnexts.IsChecked = false;
                }
            }
        }

        private void NotePageIDPropertyChanged()
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(
                    this,
                    new PropertyChangedEventArgs("PageID")
                    );
        }
        private void txtPageID_TextChanged(object sender, TextChangedEventArgs e)
        {
            NotePageIDPropertyChanged();
        }
    }
}
