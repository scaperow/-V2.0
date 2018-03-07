using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BizComponents
{
    public partial class MoveDocumentDialog : Form
    {
        public MoveDocumentDialog()
        {
            InitializeComponent();
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            DataMoveHelperClient dmh = new DataMoveHelperClient();
            if (textBox1.Text != "")
            {
                dmh.MoveDocumentByModuleID(textBox1.Text);
                textBox1.Text = "";
                MessageBox.Show("该模板资料导入完成");
            }
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
