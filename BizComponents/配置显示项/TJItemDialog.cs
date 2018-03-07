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
using System.IO;
using FarPoint.Win.Spread.Model;

namespace BizComponents
{
    public partial class TJItemDialog : Form
    {
        public TJItemDialog()
        {
            InitializeComponent();
        }


        private void TemperatureDialog_Load(object sender, EventArgs e)
        {

            FpSpread_Info.Rows.Count = 0;
            FpSpread_Info.Columns.Count = 12;
            FpSpread_Info.ColumnHeader.Cells[0, 0].Text = "ID";
            FpSpread_Info.ColumnHeader.Cells[0, 1].Text = "项名称";
            FpSpread_Info.ColumnHeader.Cells[0, 2].Text = "项类型";
            FpSpread_Info.ColumnHeader.Cells[0, 3].Text = "权重";
            FpSpread_Info.ColumnHeader.Cells[0, 4].Text = "中午温度";
            FpSpread_Info.ColumnHeader.Cells[0, 5].Text = "傍晚温度";
            FpSpread_Info.ColumnHeader.Cells[0, 6].Text = "平均温度";
            FpSpread_Info.ColumnHeader.Cells[0, 7].Text = "备注";
            FpSpread_Info.ColumnHeader.Cells[0, 10].Text = "IsUpdated";


            FarPoint.Win.Spread.CellType.DateTimeCellType datetime = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            datetime.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDate;
            FarPoint.Win.Spread.CellType.NumberCellType number = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType text = new FarPoint.Win.Spread.CellType.TextCellType();
            //number
            FpSpread_Info.Columns[0].Visible = false;
            FpSpread_Info.Columns[1].Visible = false;
            FpSpread_Info.Columns[2].CellType = datetime;
            FpSpread_Info.Columns[2].Locked = true;
            FpSpread_Info.Columns[3].CellType = number;
            FpSpread_Info.Columns[4].CellType = number;
            FpSpread_Info.Columns[5].CellType = number;
            FpSpread_Info.Columns[6].CellType = number;
            FpSpread_Info.Columns[6].Locked = true;
            FpSpread_Info.Columns[7].CellType = text;
            FpSpread_Info.Columns[8].Visible = false;
            FpSpread_Info.Columns[9].Visible = false;
            FpSpread_Info.Columns[10].Visible = false;
            FpSpread_Info.Columns[11].Visible = false;

            int TemperatureType = 0;

            DataTable Data = ModuleHelperClient.GetTemperatureList("000", new DateTime(2014, 1, 2), new DateTime(2014, 1, 1), TemperatureType);
            FpSpread_Info.DataSource = Data;

            FpSpread_Info.Columns[2].Width = 100;
            FpSpread_Info.Columns[3].Width = 100;
            FpSpread_Info.Columns[4].Width = 100;
            FpSpread_Info.Columns[5].Width = 100;
            FpSpread_Info.Columns[6].Width = 100;
            FpSpread_Info.Columns[7].Width = 200;
        }

        private void Button_Query_Click(object sender, EventArgs e)
        {
            BindDayList();
        }

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BindDayList()
        {

                ProgressScreen.Current.ShowSplashScreen();
                ProgressScreen.Current.SetStatus = "正在获取数据...";

                DataTable Data = ModuleHelperClient.GetTemperatureList(testroom, StartDate, EndDate, TemperatureType);
                if (Data != null)
                {
                    FpSpread_Info.DataSource = Data;
                    FpSpread_Info.Columns[2].Width = 100;
                    FpSpread_Info.Columns[3].Width = 100;
                    FpSpread_Info.Columns[4].Width = 100;
                    FpSpread_Info.Columns[5].Width = 100;
                    FpSpread_Info.Columns[6].Width = 100;
                    FpSpread_Info.Columns[7].Width = 200;
                    FpSpread.ShowRow(FpSpread.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
                    FpSpread_Info.Rows.Count = Data.Rows.Count;
                    if (Data.Rows.Count > 0)
                    {
                        int i, j;
                        foreach (System.Data.DataColumn Column in Data.Columns)
                        {
                            i = Data.Columns.IndexOf(Column);

                            FpSpread_Info.Columns[i].Width = 100;
                            foreach (DataRow Row in Data.Rows)
                            {
                                j = Data.Rows.IndexOf(Row);
                                FpSpread_Info.Rows[j].HorizontalAlignment = CellHorizontalAlignment.Center;
                                FpSpread_Info.Cells[j, i].Value = Row[Column.ColumnName];
                            }
                        }
                        double fTemperatureSum = 0;
                        object TemperatureAvg = null;
                        object oLastEditTime = null;
                        DateTime dtLastEditTime = new DateTime();
                        foreach (DataRow Row in Data.Rows)
                        {
                            j = Data.Rows.IndexOf(Row);
                            FpSpread_Info.Rows[j].Tag = Row["ID"] == DBNull.Value ? "" : Row["ID"].ToString();
                            TemperatureAvg = Row["TemperatureAvg"];
                            oLastEditTime = Row["LastEditTime"];
                            if (TemperatureAvg != DBNull.Value)
                            {
                                fTemperatureSum += Convert.ToDouble(TemperatureAvg);
                            }
                            if (oLastEditTime != DBNull.Value)
                            {
                                dtLastEditTime = DateTime.Parse(oLastEditTime.ToString());
                                if (dtLastEditTime.AddDays(2).CompareTo(DateTime.Now.Date) < 0 && !Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                                {
                                    FpSpread_Info.Rows[j].Locked = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        ProgressScreen.Current.CloseSplashScreen();
                        this.Activate();
                    }
                }
                else
                {
                    ProgressScreen.Current.CloseSplashScreen();
                    this.Activate();
                }

                ProgressScreen.Current.CloseSplashScreen();
                this.Activate();
                if (Data == null)
                    TotalCount.Text = string.Format("本次查询共 {0} 条记录", 0);
                else
                    TotalCount.Text = string.Format("本次查询共 {0} 条记录", Data.Rows.Count);
            
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            //String TestRoomCode = Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code;
            string UserName = Yqun.Common.ContextCache.ApplicationContext.Current.UserName;
            try
            {
                using (DataTable dt = FpSpread_Info.GetDataView(true).Table)
                {
                    int IsUpdated = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IsUpdated = int.Parse(dt.Rows[i][10].ToString());
                        if (IsUpdated == 0)
                        {
                            dt.Rows.Remove(dt.Rows[i]);
                            i--;
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        int TemperatureType = 0;
                        if (rbSDNei.Checked)
                        {
                            TemperatureType = 1;
                        }
                        if (bool.Parse(ModuleHelperClient.SaveTemperatures(TestRoomCode, UserName, dt, TemperatureType)))// FpSpread_Info.GetDataView(true).Table
                        {
                            MessageBox.Show("实验记录保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("实验记录保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("没有更改任何数据，不需要保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    BindDayList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FpSpread_EditChange(object sender, EditorNotifyEventArgs e)
        {
            Int32 i = FpSpread_Info.ActiveRowIndex;

            object Temperature1 = null;
            object Temperature2 = null;
            object Temperature3 = null;
            float TemperatureAvg = 0;
            int iCount = 0;
            float TemperatureSum = 0;
            Temperature1 = FpSpread_Info.Cells[i, 3].Value;
            if (Temperature1 != null && !string.IsNullOrEmpty(Temperature1.ToString()))
            {
                iCount++;
                TemperatureSum += float.Parse(Temperature1.ToString());
            }
            Temperature2 = FpSpread_Info.Cells[i, 4].Value;
            if (Temperature2 != null && !string.IsNullOrEmpty(Temperature2.ToString()))
            {
                iCount++;
                TemperatureSum += float.Parse(Temperature2.ToString());
            }
            Temperature3 = FpSpread_Info.Cells[i, 5].Value;
            if (Temperature3 != null && !string.IsNullOrEmpty(Temperature3.ToString()))
            {
                iCount++;
                TemperatureSum += float.Parse(Temperature3.ToString());
            }
            //TemperatureAvg = FpSpread_Info.Cells[i, 6].Value.ToString();
            if (iCount > 0 && iCount == 3)
            {
                TemperatureAvg = TemperatureSum / iCount;
            }
            else
            {
                TemperatureAvg = 0;
            }
            if (TemperatureAvg < 0)
            {
                TemperatureAvg = 0;
            }
            FpSpread_Info.Cells[i, 6].Value = TemperatureAvg;
            FpSpread_Info.Cells[i, 10].Value = 1;


        }


    }
}
