using System;
using System.Collections;
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
    /// Interaction logic for SearchLayer.xaml
    /// </summary>
    public partial class SearchLayer : UserControl
    {
        public Brush focusBrush = Brushes.LightBlue;
        public Brush unfocusBrush;
        public SearchLayer()
        {
            InitializeComponent();
            unfocusBrush = this.Background;
        }        
        private void mouseUp(object sender, RoutedEventArgs e)
        {
            txtStart.Focus();
        }
        private void getFocus(object sender, RoutedEventArgs e)
        {
            //this.Background = focusBrush;
        }
        private void lostFocus(object sender, RoutedEventArgs e)
        {
            //this.Background = unfocusBrush;
        }

        public string Start
        {
            get
            {
                return txtStart.Text;
            }

            set
            {
                txtStart.Text = value;
            }
        }
        public string End
        {
            get
            {
                return txtEnd.Text;
            }

            set
            {
                txtEnd.Text = value;
            }
        }

        public JsonObject JsonObj
        {
            get
            {
                JsonObject json = new JsonObject();
                json[JCfgName.start] = Start;
                json[JCfgName.end] = End;
                return json;
            }

            set
            {
                Start = value[JCfgName.start].GetValue<String>();
                End = value[JCfgName.end].GetValue<String>();
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
