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
    public partial class TemperatureDialog : Form
    {
        string TestRoomCode;
        public TemperatureDialog(string _TestRoomCode)
        {
            TestRoomCode = _TestRoomCode;
            InitializeComponent();
        }

        public void ShowWithDocument(string documentID)
        {
            Show();

            var table = TemperatureHelperClient.GetDocumentTemperaturesRange(documentID);
            if (table != null && table.Rows.Count > 0)
            {
                var row = table.Rows[0];
                if (Convert.IsDBNull(row["TemperatureID"]))
                {
                    MessageBox.Show("请为该报告设置温度类型后再查看", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Convert.IsDBNull(row["StartTime"]))
                {
                    MessageBox.Show("不能获取龄期的制件时间, 请重试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Convert.IsDBNull(row["EndTime"]))
                {
                    MessageBox.Show("不能获取龄期的开始时间, 请重试", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var start = Convert.ToDateTime(row["StartTime"]);
                var end = Convert.ToDateTime(row["EndTime"]);
                var temperature = Convert.ToInt32(row["TemperatureID"]);

                DateTimePicker_Start.Value = start;
                DateTimePicker_End.Value = end;
                ComboTemperature.SelectedValue = temperature;

                Button_Query_Click(this, EventArgs.Empty);
            }
        }

        private void TemperatureDialog_Load(object sender, EventArgs e)
        {

            ComboTemperature.DataSource = TemperatureHelperClient.GetTemperatureTypes(TestRoomCode);

            FpSpread_Info.Rows.Count = 0;
            FpSpread_Info.Columns.Count = 12;
            FpSpread_Info.ColumnHeader.Cells[0, 0].Text = "ID";
            FpSpread_Info.ColumnHeader.Cells[0, 1].Text = "试验室编码";
            FpSpread_Info.ColumnHeader.Cells[0, 2].Text = "试验日期";
            FpSpread_Info.ColumnHeader.Cells[0, 3].Text = "早晨温度";
            FpSpread_Info.ColumnHeader.Cells[0, 4].Text = "中午温度";
            FpSpread_Info.ColumnHeader.Cells[0, 5].Text = "傍晚温度";
            FpSpread_Info.ColumnHeader.Cells[0, 6].Text = "平均温度";
            FpSpread_Info.ColumnHeader.Cells[0, 7].Text = "备注";
            FpSpread_Info.ColumnHeader.Cells[0, 8].Text = "LastEditUser";
            FpSpread_Info.ColumnHeader.Cells[0, 9].Text = "LastEditTime";
            FpSpread_Info.ColumnHeader.Cells[0, 10].Text = "IsUpdated";
            FpSpread_Info.ColumnHeader.Cells[0, 11].Text = "TemperatureType";


            FarPoint.Win.Spread.CellType.DateTimeCellType datetime = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            datetime.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDate;
            FarPoint.Win.Spread.CellType.NumberCellType number = new FarPoint.Win.Spread.CellType.NumberCellType();
            number.DecimalPlaces = 1;
            number.FixedPoint = true;
            FarPoint.Win.Spread.CellType.TextCellType text = new FarPoint.Win.Spread.CellType.TextCellType();
            //number

            var TemperatureType = Convert.ToInt32(ComboTemperature.SelectedValue);

            DataTable Data = ModuleHelperClient.GetTemperatureList("000", new DateTime(2014, 1, 2), new DateTime(2014, 1, 1), TemperatureType);
            FpSpread_Info.DataSource = Data;

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

            FpSpread_Info.Columns[2].Width = 100;
            FpSpread_Info.Columns[3].Width = 100;
            FpSpread_Info.Columns[4].Width = 100;
            FpSpread_Info.Columns[5].Width = 100;
            FpSpread_Info.Columns[6].Width = 100;
            FpSpread_Info.Columns[7].Width = 200;
            DateTimePicker_Start.Value = DateTime.Now.AddMonths(-1);
            DateTimePicker_End.Value = DateTime.Now;

            SetDefaultStyle();
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
            DateTime StartDate = DateTime.Parse(string.Format("{0}-{1}-{2} 00:00:00", DateTimePicker_Start.Value.Year, DateTimePicker_Start.Value.Month, DateTimePicker_Start.Value.Day));
            DateTime EndDate = DateTime.Parse(string.Format("{0}-{1}-{2} 00:00:00", DateTimePicker_End.Value.Year, DateTimePicker_End.Value.Month, DateTimePicker_End.Value.Day));

            DateTimePicker_End.Value = EndDate;
            ProgressScreen.Current.ShowSplashScreen();
            ProgressScreen.Current.SetStatus = "正在获取数据...";
            String testroom = TestRoomCode;// Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code;

            EndDate = EndDate.AddDays(1);
            var TemperatureType = Convert.ToInt32(ComboTemperature.SelectedValue);
            DataTable Data = ModuleHelperClient.GetTemperatureList(testroom, StartDate, EndDate, TemperatureType);
            if (Data != null)
            {
                FarPoint.Win.Spread.CellType.TextCellType text = new FarPoint.Win.Spread.CellType.TextCellType();
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
                            if ((i == 3 || i == 4 || i == 5 || i == 6) && Row[Column.ColumnName] != null)
                            {
                                bool bIsNumber = false;
                                float fValue = 0;
                                bIsNumber = float.TryParse(Row[Column.ColumnName].ToString(), out fValue);
                                if (bIsNumber == true)
                                {
                                    FpSpread_Info.Cells[j, i].Value = fValue.ToString("F1");
                                }
                                else
                                {
                                    FpSpread_Info.Cells[j, i].Value = Row[Column.ColumnName];
                                }
                            }
                            else
                            {
                                FpSpread_Info.Cells[j, i].Value = Row[Column.ColumnName];
                            }
                        }
                    }
                    #region 计算总温度与锁定两天以前添加的温度
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
                    lblTemperatureSum.Text = string.Format("总温度：{0}℃", fTemperatureSum.ToString("f1"));
                    #endregion
                }
                else
                {
                    ProgressScreen.Current.CloseSplashScreen();
                    this.Activate();
                    MessageBox.Show("无数据，请重新选择条件!");
                }
            }
            else
            {
                ProgressScreen.Current.CloseSplashScreen();
                this.Activate();
                MessageBox.Show("无数据，请重新选择条件!");
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
            string UserName = Yqun.Common.ContextCache.ApplicationContext.Current.UserName;
            try
            {
                //using (DataTable dt = FpSpread_Info.GetDataView(true).Table)
                //{
                DataTable dt = new DataTable();
                #region 构建dt
                dt.Columns.Add("ID", System.Type.GetType("System.String"));
                dt.Columns.Add("TestRoomCode", System.Type.GetType("System.String"));
                dt.Columns.Add("TestTime", System.Type.GetType("System.String"));
                dt.Columns.Add("Temperature1", System.Type.GetType("System.String"));
                dt.Columns.Add("Temperature2", System.Type.GetType("System.String"));
                dt.Columns.Add("Temperature3", System.Type.GetType("System.String"));
                dt.Columns.Add("TemperatureAvg", System.Type.GetType("System.String"));
                dt.Columns.Add("Comment", System.Type.GetType("System.String"));
                dt.Columns.Add("LastEditUser", System.Type.GetType("System.String"));
                dt.Columns.Add("LastEditTime", System.Type.GetType("System.String"));
                dt.Columns.Add("IsUpdated", System.Type.GetType("System.String"));
                dt.Columns.Add("TemperatureType", System.Type.GetType("System.String"));
                #endregion
                DataRow row;
                int IsUpdated = 0;
                for (int i = 0; i < FpSpread_Info.Rows.Count; i++)
                {
                    IsUpdated = int.Parse(FpSpread_Info.Cells[i, 10].Value.ToString());
                    if (IsUpdated == 1)
                    {
                        row = dt.NewRow();
                        row["ID"] = FpSpread_Info.Cells[i, 0].Value;
                        row["TestRoomCode"] = FpSpread_Info.Cells[i, 1].Value;
                        row["TestTime"] = FpSpread_Info.Cells[i, 2].Value;
                        row["Temperature1"] = FpSpread_Info.Cells[i, 3].Value;
                        row["Temperature2"] = FpSpread_Info.Cells[i, 4].Value;
                        row["Temperature3"] = FpSpread_Info.Cells[i, 5].Value;
                        row["TemperatureAvg"] = FpSpread_Info.Cells[i, 6].Value;
                        row["Comment"] = FpSpread_Info.Cells[i, 7].Value;
                        row["LastEditUser"] = FpSpread_Info.Cells[i, 8].Value;
                        row["LastEditTime"] = FpSpread_Info.Cells[i, 9].Value;
                        row["IsUpdated"] = FpSpread_Info.Cells[i, 10].Value;
                        row["TemperatureType"] = FpSpread_Info.Cells[i, 11].Value;
                        dt.Rows.Add(row);
                        //dt.Rows.Remove(dt.Rows[i]);
                        //i--;
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    int TemperatureType = Convert.ToInt32(ComboTemperature.SelectedValue);
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
                //}
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
            FpSpread_Info.Cells[i, 6].Value = TemperatureAvg.ToString("F1");
            FpSpread_Info.Cells[i, 10].Value = 1;


        }

        private void LinkCustom_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new TemperatureEditor(TestRoomCode);
            if (form.ShowDialog() == DialogResult.OK)
            {
                ComboTemperature.DataSource = TemperatureHelperClient.GetTemperatureTypes(TestRoomCode);
            }
        }

        private void LinkDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("删除后将不可恢复, 继续吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                var id = Convert.ToString(ComboTemperature.SelectedValue);
                var result = TemperatureHelperClient.DeleteTemperature(id);
                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ComboTemperature.DataSource = TemperatureHelperClient.GetTemperatureTypes(TestRoomCode);
                }
                else
                {
                    MessageBox.Show(result, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void LineRename_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var row = ComboTemperature.SelectedItem as DataRowView;

            if (row == null)
            {
                return;
            }


            var temperature = new Sys_Temperature()
            {
                ID = Convert.ToInt32(row.Row["ID"]),
                TestRoomCode = Convert.ToString(row.Row["TestRoomCode"]),
                CreateTime = Convert.ToString(row.Row["CreateTime"]),
                CreateBy = Convert.ToString(row.Row["CreateBy"]),
                IsSystem = Convert.ToInt32(row.Row["IsSystem"]),
                Name = Convert.ToString(row.Row["Name"])
            };

            var form = new TemperatureEditor(temperature);
            if (form.ShowDialog() == DialogResult.OK)
            {
                ComboTemperature.DataSource = TemperatureHelperClient.GetTemperatureTypes(TestRoomCode);
            }
        }

        private void ComboTemperature_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = ComboTemperature.SelectedItem as DataRowView;
            if (row != null)
            {
                var system = Convert.ToBoolean(row.Row["IsSystem"]);
                LineRename.Visible = LinkDelete.Visible = !system;
            }
        }

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            PrintSheet();

        }

        private void SetPage_Click(object sender, EventArgs e)
        {
            ShowPageSettingDialog();
        }

        /// <summary>
        /// 显示页面设置对话框
        /// </summary>
        private void ShowPageSettingDialog()
        {
            PrintInfoDialog PrintInfoDialog = new PrintInfoDialog();
            PrintInfoDialog.PrintSet = FpSpread.ActiveSheet.PrintInfo;
            if (PrintInfoDialog.ShowDialog() == DialogResult.OK)
            {
                FpSpread.ActiveSheet.PrintInfo = PrintInfoDialog.PrintSet;
            }
        }

        private void SetDefaultStyle()
        {
            FpSpread.ActiveSheet.PrintInfo.ShowColumnHeader = PrintHeader.Show;
            FpSpread.ActiveSheet.PrintInfo.ShowRowHeader = PrintHeader.Show;
            FpSpread.ActiveSheet.PrintInfo.Margin = new PrintMargin(80, 80, 0, 0, 0, 0);
        }

        /// <summary>
        /// 打印当前表单
        /// </summary>
        private void PrintSheet()
        {
            FpSpread.ActiveSheet.PrintInfo.Preview = false;
            FpSpread.ActiveSheet.PrintInfo.ShowPrintDialog = true;

            int SheetIndex = FpSpread.Sheets.IndexOf(FpSpread.ActiveSheet);
            FpSpread.PrintSheet(SheetIndex);
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        private void PrintPreview()
        {
            FpSpread.ActiveSheet.PrintInfo.Preview = true;
            FpSpread.ActiveSheet.PrintInfo.ShowPrintDialog = true;

            int SheetIndex = FpSpread.Sheets.IndexOf(FpSpread.ActiveSheet);
            FpSpread.PrintSheet(SheetIndex);

        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {
            PrintPreview();
        }
    }
}
