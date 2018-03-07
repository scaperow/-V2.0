using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using Yqun.Common.ContextCache;
using Yqun.Interfaces;
using BizCommon;
using Yqun.Bases;

namespace BizComponents
{
    public partial class ViewOperateLogDialog : Form
    {
        public int pagesize = 30;
        public int count = 0;
        public String segment = "";
        public String company = "";
        public String testroom = "";
        public DateTime StartDate = DateTime.Parse(string.Format("{0}-{1}-{2} 00:00:00", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
        public DateTime EndDate = DateTime.Parse(string.Format("{0}-{1}-{2} 23:59:59", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
        public ViewOperateLogDialog()
        {
            InitializeComponent();
            pagerControl1.OnPageChanged += new EventHandler(pagerControl1_OnPageChanged);
            FpSpread_Info.OperationMode = OperationMode.ReadOnly;
        }

        private void QueryReportEvaluateDialog_Load(object sender, EventArgs e)
        {
            FpSpread_Info.Columns.Count = 9;
            FpSpread_Info.ColumnHeader.RowCount = 1;
            FpSpread_Info.ColumnHeader.Cells[0, 0].Text = "用户";
            FpSpread_Info.Columns[0].Width = 120;

            FpSpread_Info.ColumnHeader.Cells[0, 1].Text = "标段名称";
            FpSpread_Info.Columns[1].Width = 120;

            FpSpread_Info.ColumnHeader.Cells[0, 2].Text = "单位名称";
            FpSpread_Info.Columns[2].Width = 120;

            FpSpread_Info.ColumnHeader.Cells[0, 3].Text = "试验室名称";
            FpSpread_Info.Columns[3].Width = 120;

            FpSpread_Info.ColumnHeader.Cells[0, 4].Text = "操作日期";
            FpSpread_Info.Columns[4].Width = 150;

            FpSpread_Info.ColumnHeader.Cells[0, 5].Text = "操作类型";
            FpSpread_Info.Columns[5].Width = 150;

            FpSpread_Info.ColumnHeader.Cells[0, 6].Text = "模板";
            FpSpread_Info.Columns[6].Width = 220;

            FpSpread_Info.ColumnHeader.Cells[0, 7].Text = "报告名称";
            FpSpread_Info.Columns[7].Width = 150;

            FpSpread_Info.ColumnHeader.Cells[0, 8].Text = "报告编号";
            FpSpread_Info.Columns[8].Width = 150;

            ComboBox_Segments.Items.Clear();
            ComboBox_Segments.Items.Add("全部标段");
            ComboBox_Segments.SelectedIndex = 0;
        }

        private void ComboBox_Segments_DropDown(object sender, EventArgs e)
        {
            ComboBox_Segments.Items.Clear();
            List<Prjsct> PrjsctList = DepositoryPrjsctInfo.QueryPrjscts(Yqun.Common.ContextCache.ApplicationContext.Current.InProject.Code);
            ComboBox_Segments.Items.Add("全部标段");
            ComboBox_Segments.Items.AddRange(PrjsctList.ToArray());
        }

        private void ComboBox_Segments_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox_Company.Items.Clear();
            ComboBox_Company.Items.Add("全部单位");

            if (ComboBox_Segments.SelectedItem is Prjsct)
            {
                Prjsct prjsct = ComboBox_Segments.SelectedItem as Prjsct;
                List<Orginfo> OrgInfos = DepositoryOrganInfo.QueryOrgans(prjsct.PrjsctCode, "");
                ComboBox_Company.Items.AddRange(OrgInfos.ToArray());
            }

            if (ComboBox_Company.Items.Count > 1)
                ComboBox_Company.SelectedIndex = 1;
            else
                ComboBox_Company.SelectedIndex = 0;

        }

        private void ComboBox_Company_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox_TestRooms.Items.Clear();
            ComboBox_TestRooms.Items.Add("全部试验室");

            if (ComboBox_Company.SelectedItem is Orginfo)
            {
                Orginfo Orginfo = ComboBox_Company.SelectedItem as Orginfo;
                List<PrjFolder> PrjFolders = DepositoryFolderInfo.QueryPrjFolders(Orginfo.DepCode, "");
                ComboBox_TestRooms.Items.AddRange(PrjFolders.ToArray());
            }

            if (ComboBox_TestRooms.Items.Count > 1)
                ComboBox_TestRooms.SelectedIndex = 1;
            else
                ComboBox_TestRooms.SelectedIndex = 0;
        }

        private void Button_Query_Click(object sender, EventArgs e)
        {
            pagerControl1.PageIndex = 1;
            ProgressScreen.Current.ShowSplashScreen();
            ProgressScreen.Current.SetStatus = "正在获取数据...";
            if (ComboBox_Segments.SelectedItem is Prjsct)
            {
                segment = (ComboBox_Segments.SelectedItem as Prjsct).PrjsctCode;
            }
            else
            {
                segment = "";
            }
            if (ComboBox_Company.SelectedItem is Orginfo)
            {
                company = (ComboBox_Company.SelectedItem as Orginfo).DepCode;
            }
            else
            {
                company = "";
            }
            if (ComboBox_TestRooms.SelectedItem is PrjFolder)
            {
                testroom = (ComboBox_TestRooms.SelectedItem as PrjFolder).FolderCode;
            }
            else
            {
                testroom = "";
            }
             StartDate = DateTime.Parse(string.Format("{0}-{1}-{2} 00:00:00", DateTimePicker_Start.Value.Year, DateTimePicker_Start.Value.Month, DateTimePicker_Start.Value.Day));
             EndDate = DateTime.Parse(string.Format("{0}-{1}-{2} 23:59:59", DateTimePicker_End.Value.Year, DateTimePicker_End.Value.Month, DateTimePicker_End.Value.Day));

             DataTable datatotal = LoginDataList.GetOperateLogList(segment, company, testroom, StartDate, EndDate, tb_username.Text.Trim(), 1, 30, 1);

            if (datatotal != null && datatotal.Rows.Count == 1)
            {
                count = int.Parse(datatotal.Rows[0][0].ToString());
            }           
            Bind();
         
        }
        void pagerControl1_OnPageChanged(object sender, EventArgs e)
        {

            Bind();
        }
        private void Bind()
        {
            DataTable Data = LoginDataList.GetOperateLogList(segment, company, testroom, StartDate, EndDate, tb_username.Text.Trim(), pagerControl1.PageIndex, pagesize, 0);
            pagerControl1.DrawControl(count, pagesize);
            if (Data != null)
            {
                FpSpread.ShowRow(FpSpread.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
                FpSpread_Info.Rows.Count = Data.Rows.Count;
                if (Data.Rows.Count > 0)
                {
                    for (int i = 0; i < Data.Rows.Count; i++)
                    {
                        DataRow Row = Data.Rows[i];
                        FpSpread_Info.Rows[i].Tag = Row["ID"].ToString();
                    }

                    for (int i = 0; i < Data.Rows.Count; i++)
                    {
                        FpSpread_Info.Rows[i].Tag = Data.Rows[i]["ID"].ToString();
                      
                        FpSpread_Info.Rows[i].HorizontalAlignment = CellHorizontalAlignment.Center;
                        for (int j = 0; j < FpSpread_Info.ColumnHeader.Columns.Count; j++)
                        {
                            FpSpread_Info.Cells[i, j].Value = Data.Rows[i][FpSpread_Info.ColumnHeader.Cells[0, j].Text].ToString();
                        }
                    }
                }
                else
                {
                    pagerControl1.DrawControl(count, pagesize);
                    ProgressScreen.Current.CloseSplashScreen();
                    this.Activate();
                    MessageBox.Show("无数据，请重新选择条件!");
                }
            }
            else
            {
                pagerControl1.DrawControl(count, pagesize);
                ProgressScreen.Current.CloseSplashScreen();
                this.Activate();
                MessageBox.Show("无数据，请重新选择条件!");
            }
            ProgressScreen.Current.CloseSplashScreen();
            this.Activate();  
        }

        private void FpSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            Row Row = FpSpread.ActiveSheet.ActiveRow;
            if (Row != null && Row.Tag is String && Row.Tag.ToString() != "")
            {
                LogDialog Dialog = new LogDialog(Int64.Parse(Row.Tag.ToString()));
                Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
                Dialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
                Dialog.Size = Owner.ClientRectangle.Size;
                Dialog.ReadOnly = true;
                Dialog.ShowDialog(Owner);
            }
        }
    }
}
