using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using xy.scraper.configControl.Properties;
using xy.scraper.page.parserConfig;

namespace xy.scraper.configControl
{
    public partial class FrmPageModelSelect : Form
    {
        public FrmPageModelSelect(
            JsonArray JsonObj,
            string ActionName,
            string OkName
            )
        {
            InitializeComponent();
            ControlBox = false;

            Text = ActionName;
            btnOk.Text = OkName;
            btnCancel.Text = Resources.btnCancel;
            ShowPageModelList(JsonObj);
        }

        private void ShowPageModelList(JsonArray JsonObj)
        {
            ArrayList pModelList = new ArrayList();

            foreach (JsonObject item in JsonObj)
            {
                pModelList.Add(new DictionaryEntry(item,
                    item[JCfgName.cfgid].GetValue<String>()));
            }
            lbPageModelList.DataSource = pModelList;
            lbPageModelList.DisplayMember = "value";
            lbPageModelList.ValueMember = "key";

            for (int i = 0; i < pModelList.Count; i++)
            {
                lbPageModelList.SelectedItems.Add(pModelList[i]);
            }

            tslbSelectMsg.Text = Resources.msg_selectedCount 
                + lbPageModelList.SelectedItems.Count;
        }

        private JsonArray selectedJsonObj;

        public JsonArray SelectedJsonObj { get => selectedJsonObj; }

        private void btnOk_Click(object sender, EventArgs e)
        {
            selectedJsonObj = new JsonArray();
            foreach (DictionaryEntry item in lbPageModelList.SelectedItems)
            {
                selectedJsonObj.Add(((JsonObject)item.Key).DeepClone());
            }

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void lbPageModelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            tslbSelectMsg.Text = Resources.msg_selectedCount 
                + lbPageModelList.SelectedItems.Count;
        }
    }
}
