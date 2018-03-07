using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Threading;
using Yqun.Common.ContextCache;
using System.Data;

namespace Yqun.BO.BusinessManager
{
    public class LogHelper : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void SaveEditLog(JZDocument oldDoc, JZDocument newDoc, String optType, Boolean relationRequestChange, Sys_Document docBase)
        {
            ThreadParameter p = new ThreadParameter();
            p.OldDoc = oldDoc;
            p.NewDoc = newDoc;
            ApplicationContext context = ApplicationContext.Current;
            p.UserName = context.UserName;
            p.IsAdministrator = context.IsAdministrator;
            p.OptType = optType;
            p.RelationRequestChange = relationRequestChange;
            p.DocBase = docBase;


            p.LineID = ApplicationContext.Current.InProject.Index;
            p.TestRoomCode = ApplicationContext.Current.InTestRoom.Code;
            p.SegmentName = ApplicationContext.Current.InSegment.Description;
            p.CompanyName = ApplicationContext.Current.InCompany.Description;
            p.TestRoomName = ApplicationContext.Current.InTestRoom.Description;
            p.LineName = ApplicationContext.Current.InProject.Description;

            ThreadPool.QueueUserWorkItem(new WaitCallback(Execute), p);
        }

        public CellLogic GetCellLogic(String name, List<CellLogic> list)
        {
            foreach (var item in list)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }

        private void Execute(object paremeter)
        {

            ThreadParameter p = paremeter as ThreadParameter;
            JZDocument oldDoc = p.OldDoc;
            JZDocument newDoc = p.NewDoc;

            if (oldDoc == null)
            {
                return;
            }
            String json = "";
            List<JZModifyItem> modifyList = new List<JZModifyItem>();
            List<CellChangedNotifyItem> notifyList = new List<CellChangedNotifyItem>();
            if (p.OptType == "修改")
            {
                #region 统计修改单元格
                if (newDoc == null || oldDoc.ID != newDoc.ID)
                {
                    return;
                }

                try
                {
                    ModuleHelper mh = new ModuleHelper();
                    CaiJiHelper caiji = new CaiJiHelper();
                    int CaiJiCount = caiji.CaiJiCountByDataID(p.DocBase.ID);
                    foreach (JZSheet sheet in oldDoc.Sheets)
                    {
                        Sys_Sheet sheetBase = mh.GetSheetItemByID(sheet.ID);
                        List<CellLogic> cellLogicList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CellLogic>>(JZCommonHelper.GZipDecompressString(sheetBase.CellLogic));
                        foreach (JZCell cell in sheet.Cells)
                        {
                            if (cell.Value != null &&
                                cell.Value.ToString().Length < 4000 &&
                                cell.Value.ToString() != "" &&
                                cell.Value.ToString() != "/")
                            {
                                Object obj = JZCommonHelper.GetCellValue(newDoc, sheet.ID, cell.Name);
                                String newValue = obj == null ? "" : obj.ToString();
                                if (cell.Value.ToString() != newValue)
                                {
                                    JZModifyItem item = new JZModifyItem()
                                    {
                                        CellPosition = cell.Name,
                                        SheetID = sheet.ID,
                                        OriginalValue = cell.Value.ToString(),
                                        CurrentValue = newValue
                                    };
                                    modifyList.Add(item);

                                    CellLogic cl = GetCellLogic(cell.Name, cellLogicList);
                                    if (cl != null)
                                    {
                                        if (cl.IsKey && cl.Description.Contains("编号") == false && CaiJiCount > 0 && p.IsAdministrator == false)
                                        {
                                            CellChangedNotifyItem ni = new CellChangedNotifyItem();
                                            ni.Description = cl.Description;
                                            ni.CellPosition = item.CellPosition;
                                            ni.CurrentValue = item.CurrentValue;
                                            ni.OriginalValue = item.OriginalValue;
                                            bool bIsExits = false;
                                            foreach (var ccni in notifyList)
                                            {
                                                if (ccni.Description == ni.Description)
                                                {
                                                    bIsExits = true;
                                                    break;
                                                }
                                            }
                                            if (bIsExits == false && !string.IsNullOrEmpty(ni.Description))
                                            {
                                                notifyList.Add(ni);
                                            }
                                            else if (string.IsNullOrEmpty(ni.Description))
                                            {
                                                logger.Info(string.Format("SheetName:{1},,CellPosition:{0}的Description为空", item.CellPosition, sheet.Name));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (modifyList.Count == 0)
                    {
                        return;
                    }
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(modifyList).Replace("'", "''");
                }
                catch (Exception ex)
                {
                    logger.Error("changes log modify:" + ex.Message);
                }
                #endregion
            }
            else if (p.OptType == "删除")
            {
                json = "";
            }
            else
            {
                return;
            }
            Guid requestID = Guid.Empty;
            try
            {
                #region 修改日志
                DocumentHelper dh = new DocumentHelper();
                if (p.RelationRequestChange)
                {
                    requestID = dh.GetLastApprovedRequestChangeID(newDoc.ID);
                }

                String sql = String.Format(@"INSERT INTO dbo.sys_operate_log
                                ( dataID ,
                                  requestID ,
                                  modifiedby ,
                                  modifiedDate ,
                                  optType ,
                                  modifyItem,
                                  moduleID ,
                                  testRoomCode ,
                                  BGBH ,
                                  DataName
                                )
                        VALUES  ( '{0}','{1}' ,'{2}', GETDATE(),'{3}','{4}','{5}','{6}','{7}','{8}')",
                       oldDoc.ID, requestID, p.UserName, p.OptType, json, p.DocBase.ModuleID,
                       p.DocBase.TestRoomCode, p.DocBase.BGBH, p.DocBase.DataName.Replace("'", "''"));

                ExcuteCommand(sql);
                if (modifyList.Count > 0)
                {
                    UploadHelper uh = new UploadHelper();
                    UploadSetting uploadSetting = uh.GetUploadSettingByModuleID(p.DocBase.ModuleID);
                    if (uploadSetting != null && uploadSetting.Items != null)
                    {
                        Boolean isChangedSpecialCell = false;
                        foreach (var item in modifyList)
                        {
                            if (isChangedSpecialCell)
                            {
                                break;
                            }
                            foreach (var uploadSettingItem in uploadSetting.Items)
                            {
                                if (item.SheetID == uploadSettingItem.SheetID &&
                                    item.CellPosition == uploadSettingItem.CellName)
                                {
                                    isChangedSpecialCell = true;
                                    break;
                                }
                            }
                        }
                        if (isChangedSpecialCell)
                        {
                            sql = "UPDATE dbo.sys_document SET NeedUpload=1 WHERE ID='" + oldDoc.ID + "'";
                            ExcuteCommand(sql);
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error("changes log big error:" + ex.Message);
            }

            if (notifyList.Count > 0 && requestID != Guid.Empty)
            {
                ///TODO: 用户修改了关键字段，需给业主提示
                CellChangedNotifyHelper ccnh = new CellChangedNotifyHelper();
                ccnh.NotifyLeader(p.OldDoc.ID, notifyList, p.UserName, p.IsAdministrator, requestID, p.LineID, p.TestRoomCode, p.SegmentName, p.CompanyName, p.TestRoomName, p.LineName);
                //DocumentHelper dh = new DocumentHelper();
                //dh.UpdateDocumentStatus(p.OldDoc.ID, 1);
            }
        }


        private class ThreadParameter
        {
            public JZDocument OldDoc;
            public JZDocument NewDoc;
            public String UserName;
            public String OptType;
            public Boolean RelationRequestChange;
            public bool IsAdministrator;
            public Sys_Document DocBase;
            public string LineID;
            public string TestRoomCode;
            public string SegmentName;
            public string CompanyName;
            public string TestRoomName;
            public string LineName;
        }

        public DataTable GetOperateBaseInfo(Int64 logID)
        {
            String sql = @"SELECT a.modifiedby,a.modifiedDate,a.modifyItem,a.optType,b.ModuleID,a.dataID
                 FROM dbo.sys_operate_log a
                JOIN dbo.sys_document b ON a.dataID = b.ID WHERE a.ID=" + logID;
            return GetDataTable(sql);
        }

        public DataTable GetOperateLogList(String segment, String company, String testroom, DateTime Start, DateTime End, String username, int pageindex, int PageSize, int doCount)
        {

            #region 显示字段
            string fileds = " * ";
            fileds = @" a.ID,a.modifiedby AS 用户,a.modifiedDate AS 操作日期,a.modifyItem,
                a.DataName AS 报告名称,a.BGBH AS 报告编号,c.Name AS 模板,a.optType AS 操作类型,
            d.标段名称,d.单位名称,d.试验室名称 ";
            #endregion

            #region 查询条件
            String sql = String.Format(@" and modifiedDate>=''{0}'' AND modifiedDate<''{1}''",
            Start.ToString("yyyy-MM-dd"),
            End.AddDays(1).ToString("yyyy-MM-dd"));

            if (testroom != "")
            {
                sql += " AND testRoomCode=''" + testroom + "'' ";
            }
            else
            {
                TestRoomCodeHelper trch = new TestRoomCodeHelper();
                List<String> list = trch.GetValidTestRoomCodeList();
                List<String> real1 = new List<string>();
                List<String> real2 = new List<string>();
                if (!String.IsNullOrEmpty(company))
                {
                    foreach (var item in list)
                    {
                        if (item.StartsWith(company))
                        {
                            real1.Add(item);
                        }
                    }
                }
                else
                {
                    real1.AddRange(list);
                }


                if (!String.IsNullOrEmpty(segment))
                {
                    foreach (var item in real1)
                    {
                        if (item.StartsWith(segment))
                        {
                            real2.Add(item);
                        }
                    }
                }
                else
                {
                    real2.AddRange(real1);
                }

                if (real2.Count == 0)
                {
                    return null;
                }
                sql += " AND testRoomCode in (''" + String.Join("'',''", real2.ToArray()) + "'') ";
            }

            ApplicationContext AppContext = ApplicationContext.Current;
            if (!AppContext.IsAdministrator)
            {
                sql += " AND modifiedby not in (''developer'') ";
            }
            if (!String.IsNullOrEmpty(username))
            {
                try
                {
                    string dataID = new Guid(username).ToString();
                    sql += " And dataID = ''" + dataID + "'' ";
                }
                catch
                {
                    sql += " And modifiedby like ''%" + username + "%'' ";
                }
            }
            #endregion

            String sql_select = "";
            DataTable dt = new DataTable();

            sql_select = String.Format(@" EXEC dbo.sp_pager @tblname = '{0}', @strGetFields = '{1}', @fldName = '{2}', @PageSize = {3}, @PageIndex = {4},@doCount={5},@OrderType={6},@strWhere='{7}'",
                    @"dbo.sys_operate_log a
                    JOIN dbo.sys_module c ON a.ModuleID = c.ID
                    JOIN dbo.v_bs_codeName d ON a.testRoomCode=d.试验室编码",
                    fileds,
                    "modifiedDate",
                    PageSize,
                    pageindex,
                    doCount,
                    1, sql);
            dt = GetDataTable(sql_select);

            return dt;
        }


        /// <summary>
        /// 添加拌合站登录日志
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="TestCode">试验室编码</param>
        /// <returns></returns>
        public bool AddBhzLoginLog(string UserName, string CompanyName, string TestCode, string Position)
        {
            String sql = string.Format(@"  INSERT INTO dbo.bhz_user_log
          ( UserName ,
            CompanyName ,
            TestCode ,
            Position ,
            LastDateTime
          )
  VALUES  ( N'{0}' ,
            N'{1}' ,
            N'{2}' , 
            N'{3}' , 
            '{4}' 
          )", UserName, CompanyName, TestCode, Position, DateTime.Now);
            return ExcuteCommand(sql) > 0 ? true : false;
        }


        /// <summary>
        /// 获取拌合站用户登录日志统计
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataTable GetBhzUserLoginLog(int PageSize, int LastRowNum, string strWhere)
        {
            String sql = string.Format(@"SELECT TOP {0} * FROM (SELECT row_number() over(order by Counts desc) as rownum,*
 FROM dbo.bhz_v_user_log)T WHERE rownum>{1} {2} ", PageSize, LastRowNum, strWhere);
            return GetDataTable(sql);
        }
        /// <summary>
        /// 获取拌合站用户登录日志统计
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public int GetBhzUserLoginLogCount(int LastRowNum, string strWhere)
        {
            String sql = string.Format(@"SELECT count(*) FROM (SELECT row_number() over(order by Counts desc) as rownum,*
 FROM dbo.bhz_v_user_log)T WHERE rownum>{0} {1} ", LastRowNum, strWhere);
            int iCount = 0;
            iCount = int.Parse(ExcuteScalar(sql).ToString());
            return iCount;
        }
        /// <summary>
        /// 获取拌合站用户登录日志清单
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataTable GetBhzUserLoginLogList(string UserName, int PageSize, int LastRowNum, string strWhere)
        {
            String sql = string.Format(@"SELECT TOP {0} * FROM (SELECT row_number() over(order by LastDateTime desc) as rownum,*
 FROM dbo.bhz_user_log WHERE UserName='{3}')T WHERE rownum>{1} {2} ", PageSize, LastRowNum, strWhere, UserName);
            return GetDataTable(sql);
        }
        /// <summary>
        /// 获取拌合站用户登录日志数量
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public int GetBhzUserLoginLogListCount(string UserName, int LastRowNum, string strWhere)
        {
            String sql = string.Format(@"SELECT count(*) FROM (SELECT row_number() over(order by LastDateTime desc) as rownum,*
 FROM dbo.bhz_user_log WHERE UserName='{2}')T WHERE rownum>{0} {1} ", LastRowNum, strWhere, UserName);
            int iCount = 0;
            iCount = int.Parse(ExcuteScalar(sql).ToString());
            return iCount;
        }
    }
}
