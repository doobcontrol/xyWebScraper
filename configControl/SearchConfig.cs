﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json.Nodes;
using xy.scraper.page.parserConfig;
using xy.scraper.configControl.Properties;

namespace xy.scraper.configControl
{
    public partial class SearchConfig : UserControl
    {
        public SearchConfig()
        {
            InitializeComponent();

            setUiText();
        }
        private void setUiText()
        {
            tpSearchLayers.Text = Resources.tpSearchLayers;
            tpFinalHandle.Text = Resources.tpFinalHandle;
            tpOtherSetting.Text = Resources.tpOtherSetting;
            tbAddSearchLayer.ToolTipText = Resources.tbAddSearchLayer;
            tbDelSearchLayer.ToolTipText = Resources.tpOtherSetting;
            tbAddReplace.ToolTipText = Resources.tbAddReplace;
            tbDelReplace.ToolTipText = Resources.tbDelReplace;
            lbReplace.Text = Resources.lbReplace;
            lbAddBefore.Text = Resources.lbAddBefore;
            lbAddAfter.Text = Resources.lbAddAfter;
            cbSearchList.Text = Resources.cbSearchList;
        }

        #region SearchLayers

        private void tbAddSearchLayer_Click(object sender, EventArgs e)
        {
            SearchLayer sl = new SearchLayer();
            sl.Height = defaultDearchLayer.Height;
            sl.Dock = DockStyle.Top;
            sl.Enter += searchLayer_Enter;

            RowStyle tempStyle = tableLayoutPanel1.RowStyles[0];
            tableLayoutPanel1.RowStyles.Add(
                new RowStyle(tempStyle.SizeType));

            tableLayoutPanel1.Controls.Add(sl);
        }

        private void tbDelSearchLayer_Click(object sender, EventArgs e)
        {
            if (selectedSearchLayer != null)
            {
                tableLayoutPanel1.Controls.Remove(selectedSearchLayer);
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
                json[JCfgName.search] = searchLayers;
                foreach (SearchLayer sl in tableLayoutPanel1.Controls)
                {
                    searchLayers.Add(sl.JsonObj);
                }

                JsonArray replaces = new JsonArray();
                json[JCfgName.replaces] = replaces;
                foreach (string item in lbReplaceList.Items)
                {
                    JsonValue replace = JsonValue.Create(item);
                    replaces.Add(replace);
                }

                json[JCfgName.AddBefore] = txtAddBefore.Text;
                json[JCfgName.AddAfter] = txtAddAfter.Text;
                json[JCfgName.SearchList] = cbSearchList.Checked;

                return json;
            }

            set
            {
                txtAddBefore.Text 
                    = value[JCfgName.AddBefore].GetValue<String>();
                txtAddAfter.Text
                    = value[JCfgName.AddAfter].GetValue<String>();
                cbSearchList.Checked
                    = value[JCfgName.SearchList].GetValue<bool>();

                JsonArray searchLayers 
                    = value[JCfgName.search].AsArray();
                foreach (JsonObject item in searchLayers)
                {
                    SearchLayer sl;
                    if (searchLayers.IndexOf(item) == 0)
                    {
                        sl = ((SearchLayer)tableLayoutPanel1.Controls[0]);
                    }
                    else
                    {
                        sl = new SearchLayer();
                        sl.Height = defaultDearchLayer.Height;
                        sl.Dock = DockStyle.Top;
                        sl.Enter += searchLayer_Enter;

                        RowStyle tempStyle = tableLayoutPanel1.RowStyles[0];
                        tableLayoutPanel1.RowStyles.Add(
                            new RowStyle(tempStyle.SizeType));

                        tableLayoutPanel1.Controls.Add(sl);
                    }
                    sl.JsonObj = item;
                }

                JsonArray replaces
                    = value[JCfgName.replaces].AsArray();
                foreach (JsonValue item in replaces)
                {
                    lbReplaceList.Items
                        .Add(item.GetValue<String>());
                }
            }
        }
    }
}
