using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread.CellType;
using BizCommon;
using FarPoint.Win.Spread;

namespace BizComponents
{
    public partial class GGCUploadSettingDialog : Form
    {
        Guid moduleID;
        GGCUploadSetting uploadSetting;
        public GGCUploadSettingDialog(Guid moduleID, String moduleName)
        {
            InitializeComponent();
            this.moduleID = moduleID;
            tb_moduleName.Text = moduleName;
        }

        private void ItemInfoDialog_Load(object sender, EventArgs e)
        {
            uploadSetting = UploadHelperClient.GetGGCUploadSettingByModuleID(moduleID);

            if (uploadSetting != null && uploadSetting.Items != null)
            {
                SheetView2.Rows.Count = uploadSetting.Items.Count;
                tb_rootName.Text = uploadSetting.Name;
                for (int i = 0; i < uploadSetting.Items.Count; i++)
                {
                    SheetView2.Rows[i].Tag = uploadSetting.Items[i].NeedSetting;
                    SheetView2.Cells[i, 0].Value = uploadSetting.Items[i].Description;
                    SheetView2.Cells[i, 0].Tag = uploadSetting.Items[i].Name;
                    SheetView2.Cells[i, 1].Value = uploadSetting.Items[i].CellName;
                    SheetView2.Cells[i, 1].Tag = uploadSetting.Items[i].SheetID;
                }
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (uploadSetting!=null)
            {
                for (int i = 0; i < SheetView2.Rows.Count; i++)
                {
                    Cell cell = SheetView2.Cells[i, 0];
                    Cell cell2 = SheetView2.Cells[i, 1];
                    if (cell != null && cell2 != null)
                    {
                        UploadSettingItem item = GetSettingItem(cell.Tag.ToString());
                        if (item != null)
                        {
                            item.CellName = cell2.Value.ToString();
                            item.SheetID = new Guid(cell2.Tag.ToString());
                        }
                    }
                }
            }
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(uploadSetting);
            if (UploadHelperClient.UpdateGGCUploadSetting(moduleID, json))
            {
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("保存失败");
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_preview_Click(object sender, EventArgs e)
        {

        }

        private UploadSettingItem GetSettingItem(String name)
        {
            if (uploadSetting != null && uploadSetting.Items != null)
            {
                foreach (var item in uploadSetting.Items)
                {
                    if (item.Name == name)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        private void fpSpread2_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            if (e.Column == 2 && Convert.ToBoolean(SheetView2.Rows[e.Row].Tag))
            {
                CellSelector cellSelector = new CellSelector(moduleID, "", Guid.Empty);
                if (DialogResult.OK == cellSelector.ShowDialog())
                {
                    SheetView2.Cells[e.Row, 1].Value = cellSelector.CellName;
                    SheetView2.Cells[e.Row, 1].Tag = cellSelector.SheetID;
                }
            }
        }
    }
}
