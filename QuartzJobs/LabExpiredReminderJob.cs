using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using System.Data;
using BizComponents;
using FarPoint.Win.Spread;
using System.Drawing;
using BizCommon;

namespace QuartzJobs
{
    public class LabExpiredReminderJob : IJob
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Execute(IJobExecutionContext context)
        {
            //获得当前用户所在的试验室编码
            String Project = context.Scheduler.Context.GetString("project");
            String TestRoomCode = context.Scheduler.Context.GetString("currentLabCode");

            if (!string.IsNullOrEmpty(Project) && !string.IsNullOrEmpty(TestRoomCode))
            {
                DataTable Data = DepositoryLabStadiumList.GetLabStadiumList(TestRoomCode);
                if (Data != null)
                {
                    logger.Info(string.Format("[{0}]获取试验龄期提醒信息成功，共{1}条信息。", Project, Data.Rows.Count));

                    if (Data.Rows.Count > 0)
                    {
                        logger.Info(string.Format("[{0}]正在初始化试验龄期提醒信息...", Project));

                        StadiumReminderDialog Dialog = new StadiumReminderDialog();
                        Dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                        FpSpread FpSpread = Dialog.FpSpread;
                        SheetView FpSpread_Info = Dialog.FpSpread_Info;

                        FpSpread.ShowRow(FpSpread.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);

                        int HiddenColumnCount = 4;
                        FpSpread_Info.Columns.Count = Data.Columns.Count - HiddenColumnCount;
                        if (FpSpread_Info.Columns.Count > 0)
                        {
                            FpSpread_Info.Columns[0].Width = 160;
                            FpSpread_Info.Columns[1].Width = 110;
                            FpSpread_Info.Columns[2].Width = 80;
                            FpSpread_Info.Columns[3].Width = 120;
                            FpSpread_Info.Columns[4].Width = 100;
                            FpSpread_Info.Columns[5].Width = 120;
                            FpSpread_Info.Columns[6].Width = 190;
                            FpSpread_Info.Columns[7].Width = 190;

                            FpSpread_Info.Columns[0, FpSpread_Info.Columns.Count - 1].VerticalAlignment = CellVerticalAlignment.Center;
                            FpSpread_Info.Columns[0, FpSpread_Info.Columns.Count - 1].HorizontalAlignment = CellHorizontalAlignment.Center;
                        }

                        DateTimeCellType datetime = new DateTimeCellType();
                        datetime.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDate;
                        FarPoint.Win.Spread.CellType.TextCellType text = new FarPoint.Win.Spread.CellType.TextCellType();
                        text.Multiline = true;
                        text.WordWrap = true;

                        FpSpread_Info.Columns[2].CellType = datetime;
                        FpSpread_Info.Columns[5].CellType = text;
                        FpSpread_Info.Columns[6].CellType = text;
                        FpSpread_Info.Columns[7].CellType = text;

                        FpSpread_Info.Rows.Count = Data.Rows.Count;
                        if (Data.Rows.Count > 0)
                        {
                            FpSpread_Info.Rows[0, FpSpread_Info.Rows.Count - 1].Height = 35;
                            FpSpread_Info.Rows[0, FpSpread_Info.Rows.Count - 1].Locked = true;
                            FpSpread_Info.Rows[0, FpSpread_Info.Rows.Count - 1].HorizontalAlignment = CellHorizontalAlignment.Center;
                            FpSpread_Info.Rows[0, FpSpread_Info.Rows.Count - 1].VerticalAlignment = CellVerticalAlignment.Center;
                        }

                        int i, j;
                        foreach (System.Data.DataColumn Column in Data.Columns)
                        {
                            if (Column.ColumnName == "ID" || Column.ColumnName == "ModelCode" || Column.ColumnName == "ModelIndex" || Column.ColumnName == "DateSpan")
                                continue;

                            i = Data.Columns.IndexOf(Column);
                            FpSpread_Info.Columns[i - HiddenColumnCount].VerticalAlignment = CellVerticalAlignment.Center;
                            FpSpread_Info.Columns[i - HiddenColumnCount].Label = Column.ColumnName;

                            foreach (DataRow Row in Data.Rows)
                            {
                                j = Data.Rows.IndexOf(Row);
                                FpSpread_Info.Rows[j].HorizontalAlignment = CellHorizontalAlignment.Center;
                                FpSpread_Info.Cells[j, i - HiddenColumnCount].Value = Row[Column.ColumnName].ToString();
                            }
                        }

                        foreach (DataRow Row in Data.Rows)
                        {
                            j = Data.Rows.IndexOf(Row);
                            FpSpread_Info.Rows[j].Tag = Row["ModelCode"].ToString() + "," + Row["ModelIndex"].ToString() + "," + Row["ID"].ToString() + "," + Row["DateSpan"].ToString();
                        }

                        logger.Info(string.Format("[{0}]正在显示提醒窗口...", Project));

                        Dialog.ShowDialog();
                    }
                }
                else
                {
                    logger.Info(string.Format("[{0}]获取试验龄期提醒信息失败。", Project));
                }

                logger.Info(string.Format("[{0}]试验龄期提醒完毕，试验室编码：{1}", Project, TestRoomCode));
            }
        }
    }
}
