using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ReportComponents
{
    public partial class EditColumnDialog : Form
    {
        public EditColumnDialog()
        {
            InitializeComponent();
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxColumn.Text.Trim()))
                return;

            this.DialogResult = DialogResult.OK;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
