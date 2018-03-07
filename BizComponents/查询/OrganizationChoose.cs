using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BizCommon;

namespace BizComponents.查询
{
    public partial class OrganizationChoose : UserControl
    {
        public static bool Loaded = false;
        public OrganizationChoose()
        {
            InitializeComponent();
        }

        private void Trees_AfterSelect(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes)
            {
                node.Checked = e.Node.Checked;
            }
        }

        private void Reload()
        {
            Trees.Nodes.Clear();

            List<Prjsct> segments = DepositoryPrjsctInfo.QueryPrjscts(Yqun.Common.ContextCache.ApplicationContext.Current.InProject.Code);
            foreach (var segment in segments)
            {
                var node = new TreeNode(segment.PrjsctName);
                node.Name = segment.PrjsctName;
                node.Tag = segment.PrjsctCode;

                var companies = DepositoryOrganInfo.QueryOrgans(segment.PrjsctCode, "");
                foreach (var company in companies)
                {

                }
                //ComboBox_Company.Items.AddRange(OrgInfos.ToArray());
                Trees.Nodes.Add(node);
            }


            //ComboBox_Segments.Items.Add("全部标段");
            //ComboBox_Segments.Items.AddRange(PrjsctList.ToArray());

            Loaded = true;
        }

        private void OrganizationChoose_Load(object sender, EventArgs e)
        {
            if (Loaded == false)
            {
                Reload();
            }
        }
    }
}
