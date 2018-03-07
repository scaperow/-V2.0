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
using Yqun.Bases;

namespace BizComponents
{
    public partial class ModuleLineDialog : Form
    {
        BackgroundWorker worker;
        private Boolean isModule = true;
        private Boolean isRelationSheet = true;

        public ModuleLineDialog()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.WorkerReportsProgress = true;
        }

        private void ReferenceSheetDialog_Load(object sender, EventArgs e)
        {
            LoadLines();
            treeModule.AfterCheck += new TreeViewEventHandler(treeModule_AfterCheck);
            rb_module.Checked = true;
            //LoadModules();

        }

        private void LoadModules()
        {
            DepositoryResourceCatlog.InitModuleCatlog(treeModule);

        }

        void treeModule_AfterCheck(object sender, TreeViewEventArgs e)
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

        private void LoadLines()
        {
            DataTable dt = LineHelperClient.GetLineList();
            if (dt != null)
            {
                treeLines.Nodes.Clear();
                TreeNode TopNode = new TreeNode();
                TopNode.Name = "";
                TopNode.Text = "线路列表";
                TopNode.SelectedImageIndex = 1;
                TopNode.ImageIndex = 0;
                treeLines.Nodes.Add(TopNode);
                foreach (DataRow row in dt.Rows)
                {
                    TreeNode n = new TreeNode();
                    n.Name = row["ID"].ToString();
                    n.Text = row["LineName"].ToString();
                    n.SelectedImageIndex = 2;
                    n.ImageIndex = 2;
                    TopNode.Nodes.Add(n);
                }
            }
            treeLines.ExpandAll();
            treeLines.AfterCheck += new TreeViewEventHandler(treeLines_AfterCheck);
        }

        void treeLines_AfterCheck(object sender, TreeViewEventArgs e)
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

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            isModule = rb_module.Checked;
            isRelationSheet = cb_relation.Checked;
            ProgressScreen.Current.ShowSplashScreen();
            this.AddOwnedForm(ProgressScreen.Current);
            worker.RunWorkerAsync(this);

        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.RemoveOwnedForm(ProgressScreen.Current);
            ProgressScreen.Current.CloseSplashScreen();
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressScreen.Current.SetStep = e.ProgressPercentage;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Dictionary<Guid, String> lineIDs = new Dictionary<Guid, String>();
            List<String> moduleIDs = new List<String>();
            string strModuleIDs = string.Empty;
            Dictionary<Guid, String> dModuleIDs = new Dictionary<Guid, String>();

            foreach (TreeNode node in treeLines.Nodes[0].Nodes)
            {
                if (node.Checked)
                {
                    lineIDs.Add(new Guid(node.Name), node.Text);
                }
            }

            foreach (TreeNode node in treeModule.Nodes[0].Nodes)
            {
                foreach (TreeNode n in node.Nodes)
                {
                    if (n.Checked)
                    {
                        moduleIDs.Add(n.Name);
                        strModuleIDs += string.Format("'{0}',", n.Name);
                        dModuleIDs.Add(new Guid(n.Name), n.Text);
                    }
                }
            }
            if (moduleIDs.Count == 0 || lineIDs.Count == 0)
            {
                MessageBox.Show("请至少选则一条线路或一个模板！");
                return;
            }
            strModuleIDs = strModuleIDs.Substring(0, strModuleIDs.Length - 1);

            string strFMsg = string.Empty;
            using (DataTable dtForbidLines = ModuleHelperClient.GetForbidLinesByModuleIDs(strModuleIDs, isModule ? 1 : 0))
            {
                foreach (KeyValuePair<Guid, string> pair in lineIDs)
                {
                    DataRow[] drModuleIDs = dtForbidLines.Select(string.Format("LineID='{0}'",pair.Key));
                    if (drModuleIDs != null&&drModuleIDs.Length>0)
                    {
                        bool bHasForbidModule = false;
                        for (int j = 0; j < drModuleIDs.Length; j++)
                        {
                            string strFModuleID = drModuleIDs[j]["ModuleID"].ToString();
                            foreach (string moduleID in moduleIDs)
                            {
                                if (strFModuleID == moduleID)
                                {//禁止发布
                                    bHasForbidModule = true;
                                    if (string.IsNullOrEmpty(strFMsg))
                                    {
                                        strFMsg = isModule ? "模板" : "表单";
                                    }
                                    strFMsg += string.Format("<{0}>",dModuleIDs[new Guid(moduleID)]);
                                }
                            }
                        }
                        if (bHasForbidModule)
                        {
                            strFMsg += string.Format("已经设置不能发布到线路【{0}】",pair.Value);
                            strFMsg += "\n";
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(strFMsg))
            {
                MessageBox.Show(strFMsg);
                return;
            }
            

            Boolean flag = true;
            String msg = "";
            BackgroundWorker worker = sender as BackgroundWorker;
            int i = 0;
            foreach (KeyValuePair<Guid, string> pair in lineIDs)
            {
                try
                {
                    i = i + 1;
                    ProgressScreen.Current.SetStatus = string.Format("正在发布线路：{0}...", pair.Value);
                    worker.ReportProgress((int)(((float)i / (float)lineIDs.Count) * 100));
                    Boolean subFlag = LineHelperClient.SyncLineAndModule(moduleIDs, new List<Guid>() { pair.Key }, isModule, isRelationSheet);
                    flag = flag & subFlag;
                    if (!subFlag)
                    {
                        msg += "[" + pair.Value + "] ";
                    }
                }
                catch
                {
                    flag = false;
                }
            }

            String str = flag ? "全部更新成功" : "线路" + msg + "更新失败";
            MessageBox.Show(str);
        }

        private void rb_sheet_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_module.Checked)
            {
                DepositoryResourceCatlog.InitModuleCatlog(treeModule);
            }
            else if (rb_sheet.Checked)
            {
                DepositorySheetCatlog.InitSheetCatlog(treeModule);
            }
        }
    }
}
