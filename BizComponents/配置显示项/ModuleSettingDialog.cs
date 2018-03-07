using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BizCommon;

namespace BizComponents
{
    public partial class ModuleSettingDialog : Form
    {
        public ModuleSetting Item = null;
        private Guid sheetID;
        private Guid moduleID;

        public ModuleSettingDialog(ModuleSetting item, Guid moduleID)
        {
            InitializeComponent();
            Item = item;
            this.moduleID = moduleID;
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || tb_cell.Text == "" || tb_description.Text == "")
            {
                MessageBox.Show("缺少有效输入");
                return;
            }
            if (Item == null)
            {
                Item = new ModuleSetting();
            }
            Item.CellName = tb_cell.Text;
            Item.Description = tb_description.Text;
            Item.DocColumn = comboBox1.Text;
            Item.SheetID = sheetID;
            Item.IsShow = cb_show.Checked;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void bt_cellSelect_Click(object sender, EventArgs e)
        {
            String cellName = Item == null ? "" : Item.CellName;
            CellSelector cs = new CellSelector(moduleID, cellName, sheetID);
            if (cs.ShowDialog() == DialogResult.OK)
            {
                sheetID = cs.SheetID;
                tb_cell.Text = cs.CellName;
            }
        }

        private void ModuleSettingDialog_Load(object sender, EventArgs e)
        {
            BindDocColumns();
            if (Item != null)
            {
                comboBox1.Text = Item.DocColumn;
                tb_cell.Text = Item.CellName;
                tb_description.Text = Item.Description;
                sheetID = Item.SheetID;
                cb_show.Checked = Item.IsShow;
            }
        }

        private void BindDocColumns()
        {
            comboBox1.Items.Add("BGBH");
            comboBox1.Items.Add("BGRQ");
            comboBox1.Items.Add("WTBH");
            comboBox1.Items.Add("ShuLiang");
            comboBox1.Items.Add("QDDJ");
            comboBox1.Items.Add("Ext1");
            comboBox1.Items.Add("Ext2");
            comboBox1.Items.Add("Ext3");
            comboBox1.Items.Add("Ext4");
            comboBox1.Items.Add("Ext5");
            comboBox1.Items.Add("Ext6");
            comboBox1.Items.Add("Ext7");
            comboBox1.Items.Add("Ext8");
            comboBox1.Items.Add("Ext9");
            comboBox1.Items.Add("Ext10");
            comboBox1.Items.Add("Ext11");
            comboBox1.Items.Add("Ext12");
            comboBox1.Items.Add("Ext13");
            comboBox1.Items.Add("Ext14");
            comboBox1.Items.Add("Ext15");
            comboBox1.Items.Add("Ext16");
            comboBox1.Items.Add("Ext17");
            comboBox1.Items.Add("Ext18");
            comboBox1.Items.Add("Ext19");
            comboBox1.Items.Add("Ext20");
            comboBox1.Items.Add("Ext21");
            comboBox1.Items.Add("Ext22");
            comboBox1.Items.Add("Ext23");
        }
    }
}
