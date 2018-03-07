using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizCommon;
using System.IO;

namespace BizComponents
{
    public partial class CaiJiConfigDialog : Form
    {
        public CaiJiConfigDialog()
        {
            InitializeComponent();
        }

        private void StadiumDialog_Load(object sender, EventArgs e)
        {
            BindModelList();
        }

        public void BindModelList()
        {
            ListView_StadiumInfo.Items.Clear();

            DataTable dt = ModuleHelperClient.GetModuleConfigList();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ListViewItem Item = new ListViewItem(row["Name"].ToString());
                    Item.Tag = row;
                    Item.SubItems.Add(row["IsActive"].ToString());
                    ListView_StadiumInfo.Items.Add(Item);
                }
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender == Button_AddItem)
            {
                ModelSelectDialog dlg = new ModelSelectDialog();
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    BindModelList();
                }
            }
            else if (sender == Button_DeleteItem)
            {
                if (ListView_StadiumInfo.SelectedItems.Count > 0)
                {
                    DataRow row = ListView_StadiumInfo.SelectedItems[0].Tag as DataRow;
                    Guid moduleID = new Guid(row["ModuleID"].ToString());
                    String Message = string.Format("确认删除选中的试验采集配置吗？");
                    if (DialogResult.OK == MessageBox.Show(Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                    {
                        Boolean r = ModuleHelperClient.DelectModuleConfig(moduleID);
                        if (r)
                        {
                            ListView_StadiumInfo.SelectedItems[0].Remove();
                        }
                        Message = (r ? "删除试验采集配置成功！" : "删除试验采集配置失败！");
                        MessageBoxIcon Icon = (r? MessageBoxIcon.Information:MessageBoxIcon.Error);
                        MessageBox.Show(Message, "提示", MessageBoxButtons.OK, Icon);
                    }
                }
            }
        }

        private void ListView_StadiumInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem Item = ListView_StadiumInfo.GetItemAt(e.X, e.Y);
            if (Item == null)
                return;
            DataRow row = Item.Tag as DataRow;
            CaiJiItemDialog Dialog = new CaiJiItemDialog(new Guid(row["ModuleID"].ToString()), 
                Convert.ToBoolean(row["IsActive"]), this);
            Dialog.ShowDialog();
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
