using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using BizComponents;
using FarPoint.Win.Spread;
using System.Data;

namespace QuartzJobs
{
    public class LabUnqualifiedReportReminderJob : IJob
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Execute(IJobExecutionContext context)
        {
            //获得当前用户名称和单位类型
            String Project = context.Scheduler.Context.GetString("project");
            String CompanyIndex = context.Scheduler.Context.GetString("currentCompanyIndex");
            String CompanyType = context.Scheduler.Context.GetString("currentCompanyType");
            Boolean IsAdmin = context.Scheduler.Context.GetBoolean("isAdmin");

            if (!string.IsNullOrEmpty(Project) && !string.IsNullOrEmpty(CompanyType) && CompanyType != "@unit_施工单位")
            {
                List<String> RoomCodes = DepositoryEvaluateDataList.GetTestRoomList(IsAdmin, CompanyIndex, CompanyType);

                logger.Info(string.Format("[{0}]获得监管的试验室编码完成，编码列表为：{1}", Project, string.Join(",", RoomCodes.ToArray())));

                DataTable Data = DepositoryEvaluateDataList.GetReminderInfos(RoomCodes.ToArray(), DateTime.Now, DateTime.Now);
                if (Data != null)
                {
                    logger.Info(string.Format("[{0}]获取不合格报告信息成功，共{1}条信息。", Project, Data.Rows.Count));

                    if (Data.Rows.Count > 0)
                    {
                        logger.Info(string.Format("[{0}]正在初始化不合格报告信息...", Project));

                        ReportEvaluateDialog Dialog = new ReportEvaluateDialog(Project);
                        FpSpread FpSpread = Dialog.FpSpread;
                        SheetView FpSpread_Info = Dialog.FpSpread_Info;

                        FpSpread.ShowRow(FpSpread.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);

                        FarPoint.Win.Spread.CellType.TextCellType text = new FarPoint.Win.Spread.CellType.TextCellType();
                        text.Multiline = true;
                        text.WordWrap = true;

                        FpSpread_Info.Columns[0,2].CellType = text;

                        FpSpread_Info.Rows.Count = Data.Rows.Count;
                        FpSpread_Info.Rows[0, FpSpread_Info.Rows.Count - 1].Height = 25;
                        FpSpread_Info.Rows[0, FpSpread_Info.Rows.Count - 1].Locked = true;

                        FpSpread_Info.Columns[0, FpSpread_Info.Columns.Count - 1].VerticalAlignment = CellVerticalAlignment.Center;
                        FpSpread_Info.Rows[0, FpSpread_Info.Rows.Count - 1].HorizontalAlignment = CellHorizontalAlignment.Center;

                        int i, j;
                        foreach (System.Data.DataColumn Column in Data.Columns)
                        {
                            if (Column.ColumnName == "ID" || Column.ColumnName == "DataIndex" || Column.ColumnName == "ModelCode" || Column.ColumnName == "ModelIndex")
                                continue;

                            i = Data.Columns.IndexOf(Column);
                            FpSpread_Info.Columns[i - 4].VerticalAlignment = CellVerticalAlignment.Center;
                            FpSpread_Info.Columns[i - 4].Label = Column.ColumnName;

                            foreach (DataRow Row in Data.Rows)
                            {
                                j = Data.Rows.IndexOf(Row);
                                FpSpread_Info.Rows[j].HorizontalAlignment = CellHorizontalAlignment.Center;
                                FpSpread_Info.Cells[j, i - 4].Value = Row[Column.ColumnName].ToString();
                            }
                        }

                        foreach (DataRow Row in Data.Rows)
                        {
                            j = Data.Rows.IndexOf(Row);
                            FpSpread_Info.Rows[j].Tag = Row["DataIndex"].ToString() + "," + Row["ModelCode"].ToString() + "," + Row["ModelIndex"].ToString() + "," + Row["ID"].ToString();
                        }

                        logger.Info(string.Format("[{0}]正在显示提醒窗口...", Project));

                        Dialog.ShowDialog();
                    }
                }
                else
                {
                    logger.Info(string.Format("[{0}]获取不合格报告信息失败。", Project));
                }

                logger.Info(string.Format("[{0}]不合格报告提醒完毕", Project));
            }
        }
    }
}
