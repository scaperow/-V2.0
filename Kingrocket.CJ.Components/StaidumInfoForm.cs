using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BizCommon;
using ShuXianCaiJiModule;
using ShuXianCaiJiComponents;

namespace Kingrocket.CJ.Components
{
    public partial class StaidumInfoForm : Form
    {
        private JZTestData _JZTestData = null;

        public JZTestData JZTestData
        {
            get
            { return _JZTestData; }
            set
            {
                _JZTestData = value;
            }
        }

        private SXCJModule module = null;

        public String SJCC { get; set; }

        public String QDDJ { get; set; }

        private Logger log = null;

        public String LQ { get; set; }

        public StaidumInfoForm(SXCJModule module, Logger log)
        {
            InitializeComponent();
            this.module = module;
            this.log = log;
        }

        private void ShowReadyTestForm_Load(object sender, EventArgs e)
        {
            try
            {
                CaijiCommHelper _CaijiCommHelper = new CaijiCommHelper(log, module);
                DataTable dt = _CaijiCommHelper.GetStadiumList(module.SpecialSetting.TestRoomCode, module.SpecialSetting.MachineType);
                if (dt != null && dt.Rows.Count > 0)
                {
                    TreeNode TopNode = new TreeNode();
                    TopNode.Text = "待做试验项目";
                    this.treeView_Select.Nodes.Add(TopNode);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TreeNode Node = new TreeNode();
                        Node.Text = dt.Rows[i]["名称"].ToString() + dt.Rows[i]["委托编号"].ToString();
                        Node.Tag = dt.Rows[i];
                        this.treeView_Select.TopNode.Nodes.Add(Node);
                    }
                }
                else
                {
                    this.treeView_Select.Visible = false;
                    Label NotHaveData = new Label();
                    NotHaveData.Text = "当前没有待做试验";
                    NotHaveData.Dock = DockStyle.Fill;
                    this.Controls.Add(NotHaveData);
                }
                this.treeView_Select.ExpandAll();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void treeView_Select_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                if (e.Node == this.treeView_Select.TopNode)
                {
                    e.Node.Checked = false;
                }
                else
                {
                    foreach (TreeNode Node in this.treeView_Select.TopNode.Nodes)
                    {
                        if (e.Node != Node)
                        {
                            Node.Checked = false;
                        }
                    }
                }
            }
        }

        private void StaidumInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetJZTestData();
        }

        private void SetJZTestData()
        {
            _JZTestData = new JZTestData();
            _JZTestData.RealTimeData = new List<JZRealTimeData>();
            _JZTestData.TestCells = new List<JZTestCell>();
            _JZTestData.TestRoomCode = module.SpecialSetting.TestRoomCode;
            _JZTestData.UserName = Yqun.Common.ContextCache.ApplicationContext.Current.UserName;
            _JZTestData.DocumentID = Guid.Empty;
            _JZTestData.ModuleID = Guid.Empty;
            _JZTestData.StadiumID = Guid.Empty;
            _JZTestData.WTBH = "";
            SJCC = "";
            QDDJ = "";
            if (this.treeView_Select.Visible)
            {
                foreach (TreeNode Node in treeView_Select.Nodes[0].Nodes)
                {
                    if (Node.Checked)
                    {
                        DataRow row = Node.Tag as DataRow;
                        if (row != null)
                        {                            
                            _JZTestData.DocumentID = new Guid(row["DataID"].ToString());
                            _JZTestData.ModuleID = new Guid(row["ModuleID"].ToString());
                            _JZTestData.StadiumID = new Guid(row["ID"].ToString());
                            _JZTestData.WTBH = row["委托编号"].ToString();
                            SJCC = row["试件尺寸"].ToString();
                            QDDJ = row["强度等级"].ToString();
                            LQ = (Convert.ToInt32(row["DateSpan"]) / 24).ToString();
                        }
                        break;
                    }
                }
            }
        }
    }
}
