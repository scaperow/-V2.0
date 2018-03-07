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
    public partial class ViewLoginLogDialog : Form
    {
        public int pagesize = 30;
        public int count = 0;
        public String segment = "";
        public String company = "";
        public String testroom = "";
        public DateTime StartDate = DateTime.Parse(string.Format("{0}-{1}-{2} 00:00:00", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
        public DateTime EndDate = DateTime.Parse(string.Format("{0}-{1}-{2} 23:59:59", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
        public ViewLoginLogDialog()
        {
            InitializeComponent();
            pagerControl1.LabelText = "登录总次数:{0}";
        }

        private void ViewLoginLogDialog_Load(object sender, EventArgs e)
        {
            ComboBox_Segments.Items.Clear();
            ComboBox_Segments.Items.Add("全部标段");
            ComboBox_Segments.SelectedIndex = 0; 
            pagerControl1.OnPageChanged += new EventHandler(pagerControl1_OnPageChanged);
           
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

            DataTable datatotal = LoginDataList.GetLoginLogInfos(segment, company, testroom, StartDate, EndDate, tb_username.Text.Trim(), 1, pagesize, 1);
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
            DataTable Data = LoginDataList.GetLoginLogInfos(segment, company, testroom, StartDate, EndDate, tb_username.Text.Trim(), pagerControl1.PageIndex, pagesize, 0);
            pagerControl1.DrawControl(count, pagesize);
            if (Data != null)
            {
                FpSpread.ShowRow(FpSpread.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
                FpSpread_Info.Rows.Count = Data.Rows.Count;
                if (Data.Rows.Count > 0)
                {
                    FpSpread_Info.Columns.Count = Data.Columns.Count;
                    FpSpread_Info.ColumnHeader.RowCount = 1;
                    for (int i = 0; i < Data.Columns.Count; i++)
                    {
                        FpSpread_Info.ColumnHeader.Cells[0, i].Text = Data.Columns[i].ColumnName;
                        switch (Data.Columns[i].ColumnName)
                        {
                            case "用户":
                                FpSpread_Info.Columns[i].Width = 120;
                                break;
                            case "IP地址":
                                FpSpread_Info.Columns[i].Width = 150;
                                break;
                            case "物理地址":
                                FpSpread_Info.Columns[i].Width = 150;
                                break;
                            case "机器名":
                                FpSpread_Info.Columns[i].Width = 120;
                                break;
                            case "操作系统":
                                FpSpread_Info.Columns[i].Width = 160;
                                break;
                            case "系统账户":
                                FpSpread_Info.Columns[i].Width = 120;
                                break;
                            case "项目":
                                FpSpread_Info.Columns[i].Width = 150;
                                break;
                            case "标段":
                                FpSpread_Info.Columns[i].Width = 150;
                                break;
                            case "单位":
                                FpSpread_Info.Columns[i].Width = 150;
                                break;
                            case "试验室":
                                FpSpread_Info.Columns[i].Width = 150;
                                break;
                            case "登录时间":
                                FpSpread_Info.Columns[i].Width = 170;
                                break;
                            case "退出时间":
                                FpSpread_Info.Columns[i].Width = 170;
                                break;
                            default:
                                break;
                        }
                    }
                    for (int i = 0; i < Data.Rows.Count; i++)
                    {
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
    }
}
