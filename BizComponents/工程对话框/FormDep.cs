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
    public partial class FormDep : Form
    {
        Orginfo _UnitInfo;
        public Orginfo UnitInfo
        {
            get
            {
                return _UnitInfo;
            }
            set
            {
                _UnitInfo = value;
            }
        }

        bool IsNew;
        bool IsEdit;
        bool IsDele;

        ComboBox DepComboBox;
        public FormDep(ComboBox DepboList)
        {
            InitializeComponent();
            DepComboBox = DepboList;           
        }

        private void FormDep_Load(object sender, EventArgs e)
        {
            DepTree.Nodes.Clear();
            List<Orginfo> UnitInfos = DepositoryOrganInfo.QueryOrgans("", "");
            foreach (Orginfo dep in UnitInfos)
            {
                TreeNode Node = new TreeNode();
                Node.Tag = dep;
                Node.Text = dep.DepName;
                DepTree.Nodes.Add(Node);
            }

            TreeView_Company.Nodes.Clear();
            UnitInfos = DepositoryOrganInfo.QueryOrgans("", "@unit_施工单位");
            foreach (Orginfo dep in UnitInfos)
            {
                TreeNode Node = new TreeNode();
                Node.Name = dep.Index;
                Node.Tag = dep;
                Node.Text = dep.DepName;
                TreeView_Company.Nodes.Add(Node);
            }

            IsNew = true;
            IsDele = false;
            IsEdit = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (IsNew && (!IsDele || !IsEdit))
            {
                UnitInfo = new Orginfo();
                UnitInfo.DepName = DepNameTxt.Text;
                UnitInfo.DepAbbrev = DepAbbrevTxt.Text;
                UnitInfo.DepType = "@unit_" + DepTypeCmb.Text;

                List<string> Companys = new List<string>();
                foreach (TreeNode Node in TreeView_Company.Nodes)
                    if (Node.Checked)
                        Companys.Add(Node.Name);
                UnitInfo.ConstructionCompany = string.Join(",", Companys.ToArray());

                if (UnitInfo.DepName == string.Empty || UnitInfo.DepAbbrev == string.Empty || UnitInfo.DepType == string.Empty)
                {
                    if (MessageBox.Show("单位信息不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    else
                    {
                        this.Close();
                    }
                }

                if (DepTree.Nodes.Count > 0)
                {
                    foreach (TreeNode n in DepTree.Nodes)
                    {
                        if (n.Text == UnitInfo.DepName)
                        {
                            MessageBox.Show("已经存在该单位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                Boolean Result = DepositoryOrganInfo.New(UnitInfo);
                if (Result)
                {
                    DepComboBox.Items.Add(UnitInfo);

                    TreeNode Node = new TreeNode();
                    Node.Tag = UnitInfo;
                    Node.Text = UnitInfo.DepName;
                    DepTree.Nodes.Add(Node);

                    DepNameTxt.Clear();
                    DepAbbrevTxt.Clear();

                    foreach (TreeNode CompanyNode in TreeView_Company.Nodes)
                        CompanyNode.Checked = false;
                }
            }
            else if (IsDele || IsEdit)
            {
                IsNew = true;
                IsDele = false;
                IsEdit = false;

                DepNameTxt.Clear();
                DepAbbrevTxt.Clear();

                foreach (TreeNode Node in TreeView_Company.Nodes)
                    Node.Checked = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                UnitInfo.DepName = DepNameTxt.Text;
                UnitInfo.DepAbbrev = DepAbbrevTxt.Text;
                UnitInfo.DepType = "@unit_" + DepTypeCmb.Text;

                List<string> Companys = new List<string>();
                foreach (TreeNode Node in TreeView_Company.Nodes)
                    if (Node.Checked)
                        Companys.Add(Node.Name);
                UnitInfo.ConstructionCompany = string.Join(",", Companys.ToArray());

                if (DepTree.Nodes.Count > 0)
                {
                    foreach (TreeNode n in DepTree.Nodes)
                    {
                        Orginfo TempDep = n.Tag as Orginfo;
                        if ((TempDep.DepName == UnitInfo.DepName) && (TempDep.Index != UnitInfo.Index))
                        {
                            MessageBox.Show("已经存在该单位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                Boolean Result = DepositoryOrganInfo.Update(UnitInfo);
                if (Result)
                {
                    for (int i = 0; i < DepComboBox.Items.Count; i++)
                    {
                        Orginfo dep = DepComboBox.Items[i] as Orginfo;
                        if (dep.Index == UnitInfo.Index)
                        {
                            dep.DepName = UnitInfo.DepName;
                            dep.DepAbbrev = UnitInfo.DepAbbrev;
                            dep.DepCode = UnitInfo.DepCode;
                            dep.DepType = UnitInfo.DepType;
                        }
                    }

                    TreeNode Node = DepTree.SelectedNode;
                    Node.Tag = UnitInfo;
                    Node.Text = UnitInfo.DepName;
                }
            }

        }

        private void btnDele_Click(object sender, EventArgs e)
        {
            if (IsDele)
            {
                if (!DepositoryOrganInfo.ToUser(UnitInfo.Index))
                {
                    Boolean Result = DepositoryOrganInfo.DeleteDepName(UnitInfo.Index);
                    if (Result)
                    {
                        TreeNode Node = DepTree.SelectedNode;
                        UnitInfo = Node.Tag as Orginfo;
                        for (int i = 0; i < DepComboBox.Items.Count; i++)
                        {
                            Orginfo dep = DepComboBox.Items[i] as Orginfo;
                            if (dep.Index == UnitInfo.Index)
                            {
                                DepComboBox.Items.Remove(dep);
                                DepComboBox.Refresh();
                            }
                        }
                        DepTree.Nodes.Remove(Node);                        
                    }
                }
                else
                {
                    MessageBox.Show("该单位正在使用，不能删除！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("你还没有选中要删除的单位，\n\r请在右边的树列表中选择一个你要删除的单位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }

        }

        private void DepTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            IsNew = false;
            IsDele = true;
            IsEdit = true;

            UnitInfo = e.Node.Tag as Orginfo;
            DepNameTxt.Text = UnitInfo.DepName;
            DepAbbrevTxt.Text = UnitInfo.DepAbbrev;
            DepTypeCmb.Text = UnitInfo.DepType.ToString().Substring(6);

            string[] companys = UnitInfo.ConstructionCompany.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (TreeNode Node in TreeView_Company.Nodes)
            {
                int ArrayIndex = Array.IndexOf(companys, Node.Name);
                Node.Checked = (ArrayIndex != -1);
            }
        }

        private void DepTypeCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            label4.Visible = (DepTypeCmb.SelectedIndex == 1);
            TreeView_Company.Visible = (DepTypeCmb.SelectedIndex == 1);
        }
    }
}
