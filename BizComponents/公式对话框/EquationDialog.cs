using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BizComponents
{
    public partial class EquationDialog : Form
    {
        public EquationDialog()
        {
            InitializeComponent();
        }

        public String GetSerializeRtf()
        {
            return this.rTextBox.Rtf;
        }

        public void DeserializeRtf(String rtf)
        {
            this.rTextBox.Rtf = rtf;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.rTextBox.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender == CutObjectMenuItem)
            {
                rTextBox.Cut();
            }
            else if (sender == CopyObjectMenuItem)
            {
                rTextBox.Copy();
            }
            else if (sender == PasteObjectMenuItem)
            {
                rTextBox.Paste();
            }
            else if (sender == ClearMenuItem)
            {
                rTextBox.Clear();
            }
        }
    }
}
