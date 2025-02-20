using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json.Nodes;

namespace xy.scraper.configControl
{
    public partial class SearchConfig : UserControl
    {
        public SearchConfig()
        {
            InitializeComponent();
        }

        #region SearchLayers

        private void tbAddSearchLayer_Click(object sender, EventArgs e)
        {
            SearchLayer sl = new SearchLayer();
            sl.Enter += searchLayer_Enter;
            panel1.Controls.Add(sl);
            sl.BringToFront();
            sl.Dock = DockStyle.Top;
        }

        private void tbDelSearchLayer_Click(object sender, EventArgs e)
        {
            if (selectedSearchLayer != null)
            {
                panel1.Controls.Remove(selectedSearchLayer);
                selectedSearchLayer.Dispose();
            }
        }

        private SearchLayer? selectedSearchLayer;
        private void searchLayer_Enter(object sender, EventArgs e)
        {
            if (defaultDearchLayer == sender)
            {
                selectedSearchLayer = null;
            }
            else
            {
                selectedSearchLayer = (SearchLayer)sender;
            }
        }

        #endregion

        private void tbAddReplace_Click(object sender, EventArgs e)
        {
            if(txtAddReplace.Text.Trim() != "")
            {
                lbReplaceList.Items.Add(
                    txtAddReplace.Text.Trim());
                txtAddReplace.Text = "";
            }
        }

        private void tbDelReplace_Click(object sender, EventArgs e)
        {
            if(lbReplaceList.SelectedIndex != -1)
            {
                lbReplaceList.Items.RemoveAt(
                    lbReplaceList.SelectedIndex);
            }
        }

        public JsonObject JsonObj
        {
            get
            {
                JsonObject json = new JsonObject();
                JsonArray searchLayers = new JsonArray();
                json["search"] = searchLayers;
                foreach (SearchLayer sl in panel1.Controls)
                {
                    searchLayers.Add(sl.JsonObj);
                }

                JsonArray replaces = new JsonArray();
                json["replaces"] = replaces;
                foreach (string item in lbReplaceList.Items)
                {
                    JsonValue replace = JsonValue.Create(item);
                    replaces.Add(replace);
                }

                json["AddBefore"] = txtAddBefore.Text;
                json["AddAfter"] = txtAddAfter.Text;
                json["SearchList"] = cbSearchList.Checked;

                return json;
            }

            set
            {
                txtAddBefore.Text 
                    = value["AddBefore"].GetValue<String>();
                txtAddAfter.Text
                    = value["AddAfter"].GetValue<String>();
                cbSearchList.Checked
                    = value["SearchList"].GetValue<bool>();

                JsonArray searchLayers 
                    = value["search"].AsArray();
                foreach (JsonObject item in searchLayers)
                {
                    SearchLayer sl;
                    if (searchLayers.IndexOf(item) == 0)
                    {
                        sl = ((SearchLayer)panel1.Controls[0]);
                    }
                    else
                    {
                        sl = new SearchLayer();
                        panel1.Controls.Add(sl);
                        sl.Enter += searchLayer_Enter;
                        sl.Dock = DockStyle.Top;
                    }
                    sl.JsonObj = item;
                }

                JsonArray replaces
                    = value["replaces"].AsArray();
                foreach (JsonValue item in replaces)
                {
                    lbReplaceList.Items
                        .Add(item.GetValue<String>());
                }
            }
        }
    }
}
