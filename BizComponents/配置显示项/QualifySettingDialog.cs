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
    public partial class QualifySettingDialog : Form
    {
        Sys_Module module;
        Guid moduleID;

        public QualifySettingDialog(Guid moduleID)
        {
            InitializeComponent();
            this.moduleID = moduleID;
        }

        private void ModelFieldSettingDialog_Load(object sender, EventArgs e)
        {
            this.fpSpread1_Sheet1.OperationMode = OperationMode.ReadOnly;
            module = ModuleHelperClient.GetModuleBaseInfoByID(moduleID);
            BindQualifySetting();
        }

        private void BindQualifySetting()
        {
            if (this.fpSpread1_Sheet1.Rows.Count > 0)
            {
                this.fpSpread1_Sheet1.RemoveRows(0, this.fpSpread1_Sheet1.Rows.Count);
            }
            if (module != null)
            {
                if (module.QualifySettings != null)
                {
                    foreach (QualifySetting ms in module.QualifySettings)
                    {
                        this.fpSpread1.ActiveSheet.AddRows(this.fpSpread1.ActiveSheet.RowCount, 1);
                        this.fpSpread1.ActiveSheet.Rows[this.fpSpread1.ActiveSheet.RowCount - 1].Tag = ms.SheetID;
                        this.fpSpread1.ActiveSheet.Rows[this.fpSpread1.ActiveSheet.RowCount - 1].HorizontalAlignment = CellHorizontalAlignment.Center;
                        this.fpSpread1.ActiveSheet.Rows[this.fpSpread1.ActiveSheet.RowCount - 1].VerticalAlignment = CellVerticalAlignment.Center;
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 0].Value = ms.CellName;
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
            CellSelector cs = new CellSelector(moduleID, "", Guid.Empty);
            if (cs.ShowDialog() == DialogResult.OK)
            {
                QualifySetting qs = new QualifySetting();
                qs.CellName = cs.CellName;
                qs.SheetID = cs.SheetID;
                if (module.QualifySettings == null)
                {
                    module.QualifySettings = new List<QualifySetting>();
                }
                module.QualifySettings.Add(qs);
                BindQualifySetting();
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
                String Msg = "是否删除所选检查项？";
                if (DialogResult.Yes == MessageBox.Show(Msg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                     if (module != null)
                    {
                        if (module.QualifySettings != null)
                        {
                            foreach (QualifySetting ms in module.QualifySettings)
                            {
                                if (ms.CellName == this.fpSpread1.ActiveSheet.Cells[rowIndex, 0].Value.ToString())
                                {
                                    module.QualifySettings.Remove(ms);
                                    break;
                                }
                            }
                            
                        }
                    }
                     BindQualifySetting();
                }
            }
        }

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fpSpread1_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            QualifySetting item = null;
            
            if (module != null)
            {
                if (module.QualifySettings != null)
                {
                    foreach (QualifySetting ms in module.QualifySettings)
                    {
                        if (ms.CellName == this.fpSpread1.ActiveSheet.Cells[e.Row, 0].Value.ToString())
                        {
                            item = ms;
                        }
                    }
                }
            }
            CellSelector cs = new CellSelector(moduleID, "", Guid.Empty);
            if (cs.ShowDialog() == DialogResult.OK)
            {
                item.CellName = cs.CellName;
                item.SheetID = cs.SheetID;
                BindQualifySetting();
            }
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            if (module != null)
            {
                ModuleHelperClient.UpdateModuleSetting(module);
            }
            this.Close();
        }
    }
}
