using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizComponents;
using ReportCommon;
using Yqun.Client.BizUI;
using FarPoint.Win.Spread;
using FarPoint.Win;
using Yqun.Bases;
using Yqun.Common.ContextCache;

namespace ReportComponents
{
    public partial class TestRoomReportDialog : Form
    {
        public TestRoomReportDialog()
        {
            InitializeComponent();
        }

        private void PeriodsReportDialog_Load(object sender, EventArgs e)
        {
            //从数据库中的sys_biz_ReportRTConfig获得手工录入的报表名称
            cBox_Types.Items.Clear();
            List<String> Reports = DepositoryRTReportConfig.GetRTReports("试验室");
            cBox_Types.Items.AddRange(Reports.ToArray());
            if (cBox_Types.Items.Count > 0)
                cBox_Types.SelectedIndex = 0;

            DepositoryProjectCatlog.InitProjectCatlogWithoutModel(treeView1);
            //treeView1.TopNode.Checked = true;
            //treeView1_AfterCheck(null, new TreeViewEventArgs(treeView1.TopNode, TreeViewAction.ByMouse));
            //treeView1.TopNode.Checked = false;
            //treeView1_AfterCheck(null, new TreeViewEventArgs(treeView1.TopNode, TreeViewAction.ByMouse));
        }

        private Boolean IsNullStatLocation()
        {
            Boolean Result = true;

            TreeNode NextNode = treeView1.TopNode;
            while (NextNode != null)
            {
                if (NextNode.Checked)
                {
                    Result = false;
                    break;
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

            return Result;
        }

        private Dictionary<string, List<string>> getStatLocation()
        {
            Dictionary<string, string> TypeConverter = new Dictionary<string, string>();
            TypeConverter.Add("@eng", "@project");
            TypeConverter.Add("@unit", "@company");
            TypeConverter.Add("@tenders", "@segment");
            TypeConverter.Add("@folder", "@testlab");
            TypeConverter.Add("@module", "@model");

            Dictionary<string, List<string>> CodeArray = new Dictionary<string, List<string>>();
            CodeArray.Add("@project", new List<string>());
            CodeArray.Add("@company", new List<string>());
            CodeArray.Add("@segment", new List<string>());
            CodeArray.Add("@testlab", new List<string>());
            CodeArray.Add("@model", new List<string>());
            CodeArray.Add("@modelIndex", new List<string>());

            TreeNode NextNode = treeView1.TopNode;
            while (NextNode != null)
            {
                if (NextNode.Checked)
                {
                    Selection Selection = NextNode.Tag as Selection;
                    string Flag = Selection.Tag.ToString().ToLower();
                    if (TypeConverter.ContainsKey(Flag))
                    {
                        string Type = TypeConverter[Flag];
                        if (!CodeArray[Type].Contains(NextNode.Name))
                            CodeArray[Type].Add(NextNode.Name);

                        if (Type == "@testlab")
                        {
                            string[] modules = Selection.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            CodeArray["@model"].AddRange(modules);
                            string[] moduleIndexs = Selection.Value1.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            CodeArray["@modelIndex"].AddRange(moduleIndexs);
                        }
                    }
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

            return CodeArray;
        }

        /// <summary>
        /// 获得选中的单位名称
        /// </summary>
        /// <returns></returns>
        private string getCompanyName()
        {
            List<string> List = new List<string>();

            TreeNode NextNode = treeView1.TopNode;
            while (NextNode != null)
            {
                if (NextNode.Checked)
                {
                    Selection Selection = NextNode.Tag as Selection;
                    string Flag = Selection.Tag.ToString().ToLower();
                    if (Flag.StartsWith("@unit"))
                    {
                        List.Add(NextNode.Text);
                        break;
                    }
                    else if (Flag != "@eng")
                    {
                        TreeNode tempNode = NextNode;
                        while (tempNode != null)
                        {
                            Selection = tempNode.Tag as Selection;
                            Flag = Selection.Tag.ToString().ToLower();
                            if (Flag.StartsWith("@unit"))
                            {
                                List.Add(tempNode.Text);
                                break;
                            }

                            tempNode = tempNode.Parent;
                        }
                    }
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

            return string.Join(",", List.ToArray());
        }

        /// <summary>
        /// 获得选中的标段名称
        /// </summary>
        /// <returns></returns>
        private string getSegmentName()
        {
            List<string> List = new List<string>();

            TreeNode NextNode = treeView1.TopNode;
            while (NextNode != null && List.Count == 0)
            {
                if (NextNode.Checked)
                {
                    Selection Selection = NextNode.Tag as Selection;
                    string Flag = Selection.Tag.ToString().ToLower();
                    if (Flag == "@tenders")
                    {
                        List.Add(NextNode.Text);
                        break;
                    }
                    else if (Flag != "@eng")
                    {
                        TreeNode tempNode = NextNode;
                        while (tempNode != null)
                        {
                            Selection = tempNode.Tag as Selection;
                            Flag = Selection.Tag.ToString().ToLower();
                            if (Flag == "@tenders")
                            {
                                List.Add(tempNode.Text);
                                break;
                            }

                            tempNode = tempNode.Parent;
                        }
                    }
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

            return string.Join(",", List.ToArray());
        }

        private String GetFirstSelectInfo(out String segmentName, out String companyName)
        {
            TreeNode NextNode = treeView1.TopNode;
            TreeNode segmentNode = null;
            TreeNode companyNode = null;
            Boolean hasTestRoom = false;
            String testRoomCode = "";
            TreeNode TopNode = treeView1.TopNode;
            if (TopNode.Name.Length == 4&&TopNode.Parent!=null)
            {
                TopNode = TopNode.Parent;
            }

            foreach (TreeNode node in TopNode.Nodes)
            {
                foreach (TreeNode segNode in node.Nodes)
                {
                    foreach (TreeNode item in segNode.Nodes)
                    {
                        foreach (TreeNode subNode in item.Nodes)
                        {
                            if (subNode.Checked)
                            {
                                hasTestRoom = true;
                                testRoomCode += "''" + subNode.Name + "'',";
                            }
                        }
                    }
                    if (hasTestRoom)
                    {
                        segmentNode = segNode;
                        companyNode = segNode.Nodes[0];
                    }

                    if (hasTestRoom)
                    {
                        break;
                    }
                }



            }
            segmentName = segmentNode == null ? "" : segmentNode.Text;
            companyName = companyNode == null ? "" : companyNode.Text;
            return testRoomCode.TrimEnd(',');
        }

        private void Button_Query_Click(object sender, EventArgs e)
        {
            if (IsNullStatLocation())
            {
                MessageBox.Show("请选择一个统计范围！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ProgressScreen.Current.ShowSplashScreen();
            this.AddOwnedForm(ProgressScreen.Current);

            ProgressScreen.Current.SetStatus = "正在初始化报表参数......";
            String descriptor = cBox_Types.SelectedItem.ToString();

            Guid reportID = ReportService.GetReportIDByName(descriptor);
            List<ReportParameter> Parameters = ReportService.GetReportParameters(reportID);
            String SegmentName = "";
            String CompanyName = "";
            String testRoomCodes = "";
            testRoomCodes = GetFirstSelectInfo(out SegmentName, out CompanyName);
            foreach (ReportParameter parameter in Parameters)
            {
                if (parameter.Name.ToLower() == "@startdate")
                {
                    parameter.Value = string.Format("{0}-{1}-{2}", StartDateTimePicker.Value.Year, StartDateTimePicker.Value.Month, StartDateTimePicker.Value.Day);
                }
                else if (parameter.Name.ToLower() == "@enddate")
                {
                    parameter.Value = string.Format("{0}-{1}-{2}", EndDateTimePicker.Value.Year, EndDateTimePicker.Value.Month, EndDateTimePicker.Value.Day);
                }
                else if (parameter.Name.ToLower() == "@segmentname")
                {
                    parameter.Value = SegmentName;
                }
                else if (parameter.Name.ToLower() == "@companyname")
                {
                    parameter.Value = CompanyName;
                }
                else if (parameter.Name.ToLower() == "@now")
                {
                    parameter.Value = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }

            ProgressScreen.Current.SetStatus = "正在分析并生成报表......";
            String ReportXml = ReportService.GetReportString(reportID,
                string.Format("{0}-{1}-{2} 00:00:00", StartDateTimePicker.Value.Year, StartDateTimePicker.Value.Month, StartDateTimePicker.Value.Day),
                string.Format("{0}-{1}-{2} 23:59:59", EndDateTimePicker.Value.Year, EndDateTimePicker.Value.Month, EndDateTimePicker.Value.Day),
                testRoomCodes,
                Parameters);
            if (ReportXml != "")
            {
                ProgressScreen.Current.SetStatus = "正在显示报表......";
                FpSpread ReportSpread = new FpSpread();
                SheetView sv = Serializer.LoadObjectXml(typeof(SheetView), ReportXml, "SheetView") as SheetView;
                ReportSpread.Sheets.Add(sv);
                OwnerPrintDocument Document = new OwnerPrintDocument(ReportSpread);
                Document.ShowPrinterDialog = false;
                reportViewer1.UseAntiAlias = true;
                reportViewer1._FpSpreed = ReportSpread;
                reportViewer1.Document = Document;
            }

            this.RemoveOwnedForm(ProgressScreen.Current);
            ProgressScreen.Current.CloseSplashScreen();
            this.Activate();

            if (ReportXml == "")
                MessageBox.Show("报表‘" + descriptor + "’没有找到！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //e.Action = TreeViewAction.ByMouse;
            if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                //选中全部后代子孙
                TreeNode NextNode = e.Node.FirstNode;
                Boolean IsChecked = e.Node.Checked;
                Boolean IsEnd = false;
                while (NextNode != null && !IsEnd)
                {
                    NextNode.Checked = IsChecked;

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

                    TreeNode Descendants = NextNode;
                    Boolean IsDescendants = false;
                    while (Descendants != null)
                    {
                        if (Descendants == e.Node)
                        {
                            IsDescendants = true;
                            break;
                        }

                        Descendants = Descendants.Parent;
                    }

                    IsEnd = !IsDescendants;
                }

                //判断是否选中祖先节点
                TreeNode Parent = e.Node.Parent;
                while (Parent != null)
                {
                    Boolean isChecked = IsChecked;
                    foreach (TreeNode Node in Parent.Nodes)
                    {
                        isChecked = isChecked && Node.Checked;
                    }

                    Parent.Checked = isChecked;
                    Parent = Parent.Parent;
                }
            }
        }
    }
}
