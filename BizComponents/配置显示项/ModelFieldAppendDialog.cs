using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizCommon;

namespace BizComponents
{
    public partial class ModelFieldAppendDialog : Form
    {
        Guid moduleID;
        public JZCustomView view;
        Guid sheetID;

        public ModelFieldAppendDialog(Guid moduleID, JZCustomView view)
        {
            InitializeComponent();
            this.moduleID = moduleID;
            this.view = view;
        }

        private void ModelFieldAppendDialog_Load(object sender, EventArgs e)
        {
            if (view != null)
            {
                this.tBox_Description.Text = view.Description;
                this.cBox_IsVisible.Checked = true;
                this.cBox_IsEdit.Checked = view.IsEdit;
                this.cBox_IsNull.Checked = false;
                this.cBox_IsSystem.Checked = false;
                this.label_DataItemInfo.Text = view.CellName;
                this.sheetID = view.SheetID;
            }
            else
            {
                this.cBox_IsEdit.Checked = true;
            }
            this.rButton_UseData.Checked = true;
            this.rButton_UseFormula.Checked = false;
        }

        private void Button_Cell_Click(object sender, EventArgs e)
        {
            CellSelector cs = new CellSelector(moduleID, "", sheetID);
            if (cs.ShowDialog() == DialogResult.OK)
            {
                sheetID = cs.SheetID;
                label_DataItemInfo.Text = cs.CellName;
            }
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            if (label_DataItemInfo.Text == "" || tBox_Description.Text == "")
            {
                MessageBox.Show("缺少有效输入");
                return;
            }
            if (view == null)
            {
                view = new JZCustomView();
                view.ColumnWidth = 100;
            }
            view.CellName = label_DataItemInfo.Text;
            view.Description = tBox_Description.Text;
            view.DocColumn = "";
            view.SheetID = sheetID;
            view.IsEdit = cBox_IsEdit.Checked;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            Close();
        }
    }
}
