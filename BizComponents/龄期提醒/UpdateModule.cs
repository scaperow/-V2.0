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
    public partial class UpdateModule : Form
    {
        public UpdateModule()
        {
            InitializeComponent();
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            DataMoveHelperClient dmh = new DataMoveHelperClient();
            if (textBox1.Text != "")
            {
                dmh.MoveModuleByID(textBox1.Text);
                textBox1.Text = "";
                MessageBox.Show("一个模板导入完成，请到模板库中查看");
            }
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
