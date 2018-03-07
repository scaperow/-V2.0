using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizCommon;
using Yqun.Bases;

namespace BizComponents
{
    public partial class DictionaryReferenceItemEditor : Form
    {
        TreeNode Node;

        public DictionaryReferenceItemEditor(TreeNode Node)
        {
            InitializeComponent();

            this.Node = Node;
        }

        private void ReferenceItemEditor_Load(object sender, EventArgs e)
        {
            TextBox_Items.Text = "";
            TextBox_Items.SelectionStart = 0;

            Selection selection = Node.Tag as Selection;
            if (selection.TypeFlag == "@Dictionary")
            {
                foreach (TreeNode SubNode in Node.Nodes)
                {
                    TextBox_Items.SelectedText = TextBox_Items.SelectedText + SubNode.Text + "\r\n";
                }
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
