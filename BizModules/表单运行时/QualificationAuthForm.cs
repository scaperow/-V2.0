using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizComponents;

namespace BizModules
{
    public partial class QualificationAuthForm : Form
    {
        String FolderCode;
        public QualificationAuthForm(String FolderCode)
        {
            this.FolderCode = FolderCode;
            InitializeComponent();
        }

        private void QualificationAuthForm_Load(object sender, EventArgs e)
        {  
            DepositoryResourceCatlog.InitModuleCatlog(Modeltree);
            Modeltree.CollapseAll();

            List<String> Codes = DepositoryQualificationAuth.InitTestRoomQualificationAuth(FolderCode);
            if (Modeltree.Nodes.Count > 0)
            {
                TreeNode NextNode = Modeltree.Nodes[0];
                NextNode.Expand();

                while (NextNode != null)
                {
                    if (Codes.Contains(NextNode.Name))
                    {
                        if (NextNode.Parent != null)
                            NextNode.Parent.Expand();
                        NextNode.Checked = true;
                    }

                    if (NextNode.FirstNode != null)
                    {
                        NextNode = NextNode.FirstNode;
                    }
                    else if (NextNode.NextNode != null)
                    {
                        NextNode = NextNode.NextNode;
                    }
                    else
                    {
                        if (NextNode.Parent != null)
                        {
                            TreeNode tempNode = NextNode.Parent;
                            while (tempNode.NextNode == null)
                            {
                                if (tempNode.Parent == null)
                                    break;
                                tempNode = tempNode.Parent;
                            }

                            NextNode = tempNode.NextNode;
                        }
                        else
                        {
                            NextNode = NextNode.Parent;
                        }
                    }
                }
            }

            Modeltree.AfterCheck += new TreeViewEventHandler(Modeltree_AfterCheck);
        }

        void Modeltree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    foreach (TreeNode item in e.Node.Nodes)
                    {
                        item.Checked = e.Node.Checked;
                    }
                }
            }
        }
        private void button_Save_Click(object sender, EventArgs e)
        {
            List<String> Codes = new List<string>();
            if (Modeltree.Nodes.Count > 0)
            {
                TreeNode NextNode = Modeltree.Nodes[0];
                while (NextNode != null)
                {
                    if (NextNode.Checked)
                        Codes.Add(NextNode.Name);

                    if (NextNode.FirstNode != null)
                    {
                        NextNode = NextNode.FirstNode;
                    }
                    else if (NextNode.NextNode != null)
                    {
                        NextNode = NextNode.NextNode;
                    }
                    else
                    {
                        if (NextNode.Parent != null)
                        {
                            TreeNode tempNode = NextNode.Parent;
                            while (tempNode.NextNode == null)
                            {
                                if (tempNode.Parent == null)
                                    break;
                                tempNode = tempNode.Parent;
                            }

                            NextNode = tempNode.NextNode;
                        }
                        else
                        {
                            NextNode = NextNode.Parent;
                        }
                    }
                }
            }

            String strCodes = string.Join(",", Codes.ToArray());
            if (DepositoryQualificationAuth.UpdateQualificationAuth(FolderCode, strCodes))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("保存资质授权失败");
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
