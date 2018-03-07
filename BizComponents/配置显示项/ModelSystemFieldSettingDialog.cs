using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizCommon;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Model;

namespace BizComponents
{
    public partial class ModelSystemFieldSettingDialog : Form
    {
        Sys_Module module;
        Guid moduleID;
       
        public ModelSystemFieldSettingDialog(Guid moduleID)
        {
            InitializeComponent();
            this.moduleID = moduleID;
        }

        private void ModelFieldSettingDialog_Load(object sender, EventArgs e)
        {
            this.fpSpread1_Sheet1.OperationMode = OperationMode.ReadOnly;
            module = ModuleHelperClient.GetModuleBaseInfoByID(moduleID);
            if (module.ModuleType == 1)
            {
                cb_moduleType.Checked = true;
            }
            else
            {
                cb_moduleType.Checked = false;
            }
            BindModuleSetting();
        }

        private void BindModuleSetting()
        {
            if (this.fpSpread1_Sheet1.Rows.Count > 0)
            {
                this.fpSpread1_Sheet1.RemoveRows(0, this.fpSpread1_Sheet1.Rows.Count);
            }
            if (module != null)
            {
                if (module.ModuleSettings != null)
                {
                    foreach (ModuleSetting ms in module.ModuleSettings)
                    {
                        this.fpSpread1.ActiveSheet.AddRows(this.fpSpread1.ActiveSheet.RowCount, 1);
                        this.fpSpread1.ActiveSheet.Rows[this.fpSpread1.ActiveSheet.RowCount - 1].Tag = ms.SheetID;
                        this.fpSpread1.ActiveSheet.Rows[this.fpSpread1.ActiveSheet.RowCount - 1].HorizontalAlignment = CellHorizontalAlignment.Center;
                        this.fpSpread1.ActiveSheet.Rows[this.fpSpread1.ActiveSheet.RowCount - 1].VerticalAlignment = CellVerticalAlignment.Center;
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 0].Value = ms.Description;
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 1].Value = ms.CellName;
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 2].Value = ms.DocColumn;
                    }
                }
            }
        }

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == AppendButton)
            {
                AppendItem();
            }
            else if (sender == RemoveButton)
            {
                RemoveItem();
            }
        }

        /// <summary>
        /// 添加显示项
        /// </summary>
        private void AppendItem()
        {
            ModuleSettingDialog dlg = new ModuleSettingDialog(null, moduleID);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                if(dlg.Item!=null)
                {
                    if (module.ModuleSettings == null)
                    {
                        module.ModuleSettings = new List<ModuleSetting>();
                    }
                    module.ModuleSettings.Add(dlg.Item);
                    BindModuleSetting();
                }
            }
        }

        /// <summary>
        /// 移除显示项
        /// </summary>
        private void RemoveItem()
        {
            if (this.fpSpread1.ActiveSheet.RowCount > 0)
            {
                int rowIndex = this.fpSpread1.ActiveSheet.ActiveRow.Index;
                String Msg = "是否删除所选显示项？";
                if (DialogResult.Yes == MessageBox.Show(Msg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                     if (module != null)
                    {
                        if (module.ModuleSettings != null)
                        {
                            foreach (ModuleSetting ms in module.ModuleSettings)
                            {
                                if (ms.DocColumn == this.fpSpread1.ActiveSheet.Cells[rowIndex, 2].Value.ToString())
                                {
                                    module.ModuleSettings.Remove(ms);
                                    break;
                                }
                            }
                            
                        }
                    }
                     BindModuleSetting();
                }
            }
        }

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fpSpread1_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            ModuleSetting item = null;
            
            if (module != null)
            {
                if (module.ModuleSettings != null)
                {
                    foreach (ModuleSetting ms in module.ModuleSettings)
                    {
                        if (ms.DocColumn == this.fpSpread1.ActiveSheet.Cells[e.Row, 2].Value.ToString()
                            && ms.CellName == (this.fpSpread1.ActiveSheet.Cells[e.Row, 1].Value ?? "").ToString())
                        {
                            item = ms;
                        }
                    }
                }
            }
            ModuleSettingDialog dlg = new ModuleSettingDialog(item, moduleID);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                BindModuleSetting();
            }
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            if (module != null)
            {
                module.ModuleType = cb_moduleType.Checked ? (short)1 : (short)0;
                ModuleHelperClient.UpdateModuleSetting(module);
            }
            this.Close();
        }

        private void bt_apply_Click(object sender, EventArgs e)
        {
            DocumentHelperClient.ApplyExtFields(moduleID);
            MessageBox.Show("完成");
        }
    }
}
