using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizComponents;
using BizCommon;
using FarPoint.Win.Spread;

namespace BizModules
{
    public partial class ReferenceSheetDialog : Form
    {
        ModelDesigner designer;

        public ReferenceSheetDialog(ModelDesigner _designer)
        {
            InitializeComponent();

            this.designer = _designer;
        }

        private void ReferenceSheetDialog_Load(object sender, EventArgs e)
        {
            DepositorySheetCatlog.InitSheetCatlog(SheetList);
            SheetList.TopNode.Expand();

            foreach (Sys_Sheet sheet in designer.sheetsList)
            {
                TreeNode[] treeNodes = SheetList.Nodes.Find(sheet.ID.ToString(), true);
                if (treeNodes.Length > 0)
                {
                    treeNodes[0].Checked = true;
                }
            }
        }

        private void SheetList_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByKeyboard ||
                e.Action == TreeViewAction.ByMouse)
            {
                foreach (TreeNode Node in e.Node.Nodes)
                {
                    Node.Checked = e.Node.Checked;
                }
            }

            if (Convert.ToBoolean(e.Node.Tag))
            {
                TreeNode[] ChildNodes = treeView1.Nodes.Find(e.Node.Name, true);
                if (e.Node.Checked && ChildNodes.Length == 0)
                {
                    TreeNode NewNode = new TreeNode(e.Node.Text);
                    NewNode.Name = e.Node.Name;
                    NewNode.Tag = e.Node.Tag;

                    treeView1.Nodes.Add(NewNode);
                }
                else if (!e.Node.Checked && ChildNodes.Length != 0)
                {
                    treeView1.Nodes.RemoveByKey(e.Node.Name);
                }
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            TreeNode Node = treeView1.SelectedNode;
            if (Node == null || Node.PrevNode == null)
                return;

            TreeNode NewPrevNode = Node.Clone() as TreeNode;
            object tempTagInfo = NewPrevNode.Tag;
            NewPrevNode.Tag = Node.PrevNode.Tag;
            Node.PrevNode.Tag = tempTagInfo;

            treeView1.Nodes.Insert(Node.PrevNode.Index, NewPrevNode);
            treeView1.Nodes.Remove(Node);

            treeView1.SelectedNode = NewPrevNode;
        }

        /// <summary>
        /// down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            TreeNode Node = treeView1.SelectedNode;
            if (Node == null || Node.NextNode == null)
                return;

            TreeNode NewNextNode = Node.Clone() as TreeNode;
            object tempTagInfo = NewNextNode.Tag;
            NewNextNode.Tag = Node.NextNode.Tag;
            Node.NextNode.Tag = tempTagInfo;

            treeView1.Nodes.Insert(Node.NextNode.Index + 1, NewNextNode);
            treeView1.Nodes.Remove(Node);

            treeView1.SelectedNode = NewNextNode;
        }

        public List<Sys_Sheet> GetSheetList()
        {
            List<Sys_Sheet> list = new List<Sys_Sheet>();
            foreach (TreeNode Node in treeView1.Nodes)
            {
                Sys_Sheet Sheet = new Sys_Sheet();
                Sheet.ID = new Guid(Node.Name);
                Sheet.Name = Node.Text;
                list.Add(Sheet);
            }
            return list;
        }
    }
}
