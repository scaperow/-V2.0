using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ReportComponents
{
    public partial class ColumnOrderDialog : Form
    {
        DataColumnCollection Columns;

        public ColumnOrderDialog(DataColumnCollection Columns)
        {
            InitializeComponent();

            this.Columns = Columns;
        }

        private void ColumnOrderDialog_Load(object sender, EventArgs e)
        {
            if (Columns == null)
                return;

            TreeView_Columns.Nodes.Clear();
            foreach (DataColumn Column in Columns)
            {
                TreeNode Node = new TreeNode(Column.ColumnName);
                Node.Tag = Column;
                TreeView_Columns.Nodes.Add(Node);
            }
        }

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == Button_Prev)
            {
                TreeNode Node = TreeView_Columns.SelectedNode;
                if (Node.PrevNode == null)
                    return;

                TreeNode NewPrevNode = Node.Clone() as TreeNode;

                DataColumn column = NewPrevNode.Tag as DataColumn;
                column.SetOrdinal(Node.PrevNode.Index);

                TreeView_Columns.Nodes.Insert(Node.PrevNode.Index, NewPrevNode);
                TreeView_Columns.Nodes.Remove(Node);

                TreeView_Columns.SelectedNode = NewPrevNode;
            }
            else if (sender == Button_Next)
            {
                TreeNode Node = TreeView_Columns.SelectedNode;
                if (Node.NextNode == null)
                    return;

                TreeNode NewNextNode = Node.Clone() as TreeNode;

                DataColumn column = NewNextNode.Tag as DataColumn;
                column.SetOrdinal(Node.NextNode.Index + 1);

                TreeView_Columns.Nodes.Insert(Node.NextNode.Index + 1, NewNextNode);
                TreeView_Columns.Nodes.Remove(Node);

                TreeView_Columns.SelectedNode = NewNextNode;
            }
        }

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
