using System;
using System.Windows.Forms;
using BizCommon;
using System.Collections.Generic;

namespace BizComponents
{
    public partial class FormCreatDep : Form
    {         
        private Orginfo _UnitInfo;
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

        public FormCreatDep()
        {
            InitializeComponent();
        }

        private void FormCreatUnit_Load(object sender, EventArgs e)
        {
            TreeView_Company.Nodes.Clear();
            List<Orginfo> UnitInfos = DepositoryOrganInfo.QueryOrgans("", "@unit_施工单位");
            foreach (Orginfo dep in UnitInfos)
            {
                TreeNode Node = new TreeNode();
                Node.Name = dep.Index;
                Node.Tag = dep;
                Node.Text = dep.DepName;
                TreeView_Company.Nodes.Add(Node);
            }

            this.txtDEPNAME.Text = UnitInfo.DepName;
            this.txtEXPLAIN.Text = UnitInfo.DepAbbrev;

            int Index = this.combBDEPNAME.Items.IndexOf(UnitInfo.DepType.Replace("@unit_", ""));
            this.combBDEPNAME.SelectedIndex = (Index == -1 ? 0 : Index);

            if (UnitInfo.ConstructionCompany == null)
                UnitInfo.ConstructionCompany = "";
            string[] companys = UnitInfo.ConstructionCompany.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (TreeNode Node in TreeView_Company.Nodes)
            {
                int ArrayIndex = Array.IndexOf(companys, Node.Name);
                Node.Checked = (ArrayIndex != -1);
            }

            txtOrderID.Text = UnitInfo.OrderID;
            //if (string.IsNullOrEmpty(UnitInfo.OrderID))
            //{
            //    txtOrderID.Enabled = false;
            //}
            //else
            //{
            //    txtOrderID.Enabled = true;
            //}
        }

        private void DepTypeCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            label4.Visible = (combBDEPNAME.SelectedIndex == 1);
            TreeView_Company.Visible = (combBDEPNAME.SelectedIndex == 1);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtDEPNAME.Text.Trim() == string.Empty)
            {
                MessageBox.Show("单位名称不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }
            //if (!string.IsNullOrEmpty(UnitInfo.DepCode) && txtOrderID.Text.Trim().Length != UnitInfo.DepCode.Length)
            //{
            //    MessageBox.Show("排序长度必须是" + UnitInfo.DepCode.Length + "位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    this.DialogResult = DialogResult.None;
            //    return;
            //}

            UnitInfo.DepName = txtDEPNAME.Text;
            UnitInfo.DepAbbrev = txtEXPLAIN.Text;
            UnitInfo.DepType = "@unit_" + this.combBDEPNAME.Text;

            List<string> Companys = new List<string>();
            foreach (TreeNode Node in TreeView_Company.Nodes)
                if (Node.Checked)
                    Companys.Add(Node.Name);
            UnitInfo.ConstructionCompany = string.Join(",", Companys.ToArray());
            UnitInfo.OrderID = txtOrderID.Text.Trim();

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }       
    }
}
