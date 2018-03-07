using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using BizComponents;
using System.Data;
using FarPoint.Win.Spread;

namespace QuartzJobs
{
    public class DataModificationJob : IJob
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

                //DataTable Data = DepositoryDataModificationInfo.InitDataModificationList(RoomCodes.ToArray());
                //if (Data != null)
                //{
                //    logger.Info(string.Format("[{0}]获取审批修改信息成功，共{1}条信息。", Project, Data.Rows.Count));

                //    if (Data.Rows.Count > 0)
                //    {
                //        logger.Info(string.Format("[{0}]正在初始化审批修改信息...", Project));

                        //DataModificationView Dialog = new DataModificationView();

                        //QuerySponsorModifyDialog Dialog = new QuerySponsorModifyDialog();
                        //FpSpread FpSpread = Dialog.FpSpread;
                        //SheetView FpSpread_Info = Dialog.FpSpread_Info;

                        //FpSpread.ShowRow(FpSpread.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);

                        //FpSpread_Info.Columns.Count = 7;
                        //FpSpread_Info.Columns[0].Width = 200;
                        //FpSpread_Info.Columns[1].Width = 150;
                        //FpSpread_Info.Columns[2].Width = 240;
                        //FpSpread_Info.Columns[3].Width = 80;
                        //FpSpread_Info.Columns[4].Width = 130;
                        //FpSpread_Info.Columns[5].Width = 120;
                        //FpSpread_Info.Columns[6].Width = 250;

                        //FarPoint.Win.Spread.CellType.TextCellType text = new FarPoint.Win.Spread.CellType.TextCellType();
                        //text.Multiline = true;
                        //text.WordWrap = true;

                        //FpSpread_Info.Columns[0, 2].CellType = text;

                        //FpSpread_Info.Rows.Count = Data.Rows.Count;
                        //FpSpread_Info.Rows[0, FpSpread_Info.Rows.Count - 1].Height = 35;
                        //FpSpread_Info.Rows[0, FpSpread_Info.Rows.Count - 1].Locked = true;

                        //FpSpread_Info.Columns[0, FpSpread_Info.Columns.Count - 1].VerticalAlignment = CellVerticalAlignment.Center;
                        //FpSpread_Info.Rows[0, FpSpread_Info.Rows.Count - 1].HorizontalAlignment = CellHorizontalAlignment.Center;

                        //int i, j;
                        //foreach (System.Data.DataColumn Column in Data.Columns)
                        //{
                        //    if (Column.ColumnName == "ID" || Column.ColumnName == "DataID" || Column.ColumnName == "ModelCode" || Column.ColumnName == "ModelIndex")
                        //        continue;

                        //    i = Data.Columns.IndexOf(Column);
                        //    FpSpread_Info.Columns[i - 4].VerticalAlignment = CellVerticalAlignment.Center;
                        //    FpSpread_Info.Columns[i - 4].Label = Column.ColumnName;

                        //    foreach (DataRow Row in Data.Rows)
                        //    {
                        //        j = Data.Rows.IndexOf(Row);
                        //        FpSpread_Info.Rows[j].HorizontalAlignment = CellHorizontalAlignment.Center;
                        //        FpSpread_Info.Cells[j, i - 4].Value = Row[Column.ColumnName].ToString();
                        //    }
                        //}

                        //foreach (DataRow Row in Data.Rows)
                        //{
                        //    j = Data.Rows.IndexOf(Row);
                        //    FpSpread_Info.Rows[j].Tag = Row["ID"].ToString() + "," + Row["DataID"].ToString() + "," + Row["ModelCode"].ToString() + "," + Row["ModelIndex"].ToString();
                        //}

                    //    logger.Info(string.Format("[{0}]正在显示审批修改窗口...", Project));

                    //    Dialog.ShowDialog();
                    //}
                //}
            }
        }
    }
}
