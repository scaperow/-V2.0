using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Threading;
using System.Data;
using Yqun.Common.ContextCache;

namespace Yqun.BO.BusinessManager
{
    public class StadiumHelper : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void InitStadium(Sys_Document docBase, JZDocument doc)
        {
            ThreadParameter p = new ThreadParameter();
            p.DocBase = docBase;
            p.Doc = doc;
            ThreadPool.QueueUserWorkItem(new WaitCallback(Execute), p);
        }

        private void Execute(object paremeter)
        {
            ThreadParameter p = paremeter as ThreadParameter;
            JZDocument doc = p.Doc;
            Guid moduleID = p.DocBase.ModuleID;
            if (doc == null)
                return;
            try
            {
                JZStadiumConfig config = GetStadiumCinfigByModuleID(moduleID);
                // 检查是否是设置龄期的模板
                if (config != null)
                {
                    DataTable stadiumData = GetStadiumDataByDataID(doc.ID);
                    //检查是否该资料已经进入龄期提醒表，不考虑龄期中是否已经完成；如果已经存在，需要考虑更新是否完成；
                    if (stadiumData == null || stadiumData.Rows.Count == 0)
                    {
                        SaveStadiumData(doc, config, p.DocBase);//保存龄期
                    }
                    else
                    {
                        //如果数据已经存在于龄期表，检查制件日期是否变更，并根据配置更新是否完成的字段
                        UpdateStadiumData(doc, config, stadiumData, p.DocBase);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("龄期提醒：error" + ex.Message);
            }
        }

        public JZStadiumConfig GetStadiumCinfigByModuleID(Guid modelID)
        {
            String sql = String.Format("SELECT StadiumConfig FROM dbo.sys_stadium_config WHERE ID='{0}'  AND IsActive=1", modelID);
            DataTable tb = GetDataTable(sql);
            if (tb != null && tb.Rows.Count == 1)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<JZStadiumConfig>(tb.Rows[0][0].ToString());
            }
            return null;
        }

        public JZStadiumConfig GetStadiumConfigByDocumentID(Guid documentID)
        {
            var sql = @"SELECT config.StadiumConfig FROM dbo.sys_module module JOIN dbo.sys_stadium_config config ON config.ID=module.ID
JOIN dbo.sys_document document ON document.ModuleID = module.ID WHERE document.ID = '" + documentID + "' AND config.IsActive = 1";

            var table = GetDataTable(sql);
            if (table != null && table.Rows.Count == 1)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<JZStadiumConfig>(table.Rows[0][0].ToString());
            }

            return null;
        }

        public DataTable GetStadiumDataByDataID(Guid dataID)
        {
            try
            {
                String sql = String.Format("SELECT * FROM dbo.sys_stadium WHERE DataID = '{0}' ", dataID);
                DataTable tb = GetDataTable(sql);

                return tb;
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
            }
            return null;
        }

        private void SaveStadiumData(JZDocument doc, JZStadiumConfig config, Sys_Document docBase)
        {
            Object obj = null;
            #region 字段赋值
            String fPH = "";
            if (config.fPH != null)
            {
                obj = JZCommonHelper.GetCellValue(doc, config.fPH.SheetID, config.fPH.CellName);
                fPH = config.fPH == null ? "" : (obj == null ? "" : (obj.ToString().Replace("'", "''")));
            }
            String fZJRQ = "";
            if (config.fZJRQ != null)
            {
                obj = JZCommonHelper.GetCellValue(doc, config.fZJRQ.SheetID, config.fZJRQ.CellName);
                fZJRQ = config.fZJRQ == null ? "" : (obj == null ? "" : obj.ToString());
            }
            String fSJBH = "";
            if (config.fSJBH != null)
            {
                obj = JZCommonHelper.GetCellValue(doc, config.fSJBH.SheetID, config.fSJBH.CellName);
                fSJBH = config.fSJBH == null ? "" : (obj == null ? "" : (obj.ToString().Replace("'", "''")));
            }
            String fSJSize = "";
            if (config.fSJSize != null)
            {
                obj = JZCommonHelper.GetCellValue(doc, config.fSJSize.SheetID, config.fSJSize.CellName);
                fSJSize = config.fSJSize == null ? "" : (obj == null ? "" : obj.ToString());
            }
            String fAdded = "";
            if (config.fAdded != null)
            {
                obj = JZCommonHelper.GetCellValue(doc, config.fAdded.SheetID, config.fAdded.CellName);
                fAdded = config.fAdded == null ? "" : (obj == null ? "" : obj.ToString());
            }

            String fBGBH = "";
            if (config.fBGBH != null)
            {
                obj = JZCommonHelper.GetCellValue(doc, config.fBGBH.SheetID, config.fBGBH.CellName);
                fBGBH = config.fBGBH == null ? "" : (obj == null ? "" : obj.ToString());
            }
            String fWTBH = "";
            if (config.fWTBH != null)
            {
                obj = JZCommonHelper.GetCellValue(doc, config.fWTBH.SheetID, config.fWTBH.CellName);
                fWTBH = config.fWTBH == null ? "" : (obj == null ? "" : obj.ToString());
            }
            if (String.IsNullOrEmpty(fZJRQ))
            {
                //无制件日期则不保存
                //logger.Error("无制件日期，无法保存龄期提醒数据: id=" + doc.ID + "; fBGBH=" + fBGBH);
                return;
            }
            float? ShuLiang = null;
            if (config.ShuLiang != null)
            {
                obj = JZCommonHelper.GetCellValue(doc, config.ShuLiang.SheetID, config.ShuLiang.CellName);
                try
                {
                    ShuLiang = float.Parse(obj.ToString());
                }
                catch
                {
                    logger.Error("代表数量格式不对 " + obj);
                    ShuLiang = 0;
                }
            }
            string strShuLiang = ShuLiang == null ? "null" : ShuLiang.ToString();

            //int Temperatures = 0;
            //if (config.Temperature > 0)
            //{ }
            DateTime zjrq;
            if (!DateTime.TryParse(fZJRQ, out zjrq))
            {
                logger.Error("制件日期格式不对 " + fZJRQ);
                return;
            }
            else
            {
                fZJRQ = zjrq.ToString("yyyy-MM-dd");
            }
            string DataName = docBase.DataName;
            #endregion
            #region 控制数据长度防止溢出
            if (fSJBH.Length > 128)
            {
                fSJBH = fSJBH.Substring(0, 128);
            }
            if (fSJSize.Length > 128)
            {
                fSJSize = fSJSize.Substring(0, 128);
            }
            if (fWTBH.Length > 128)
            {
                fWTBH = fWTBH.Substring(0, 128);
            }
            if (fBGBH.Length > 128)
            {
                fBGBH = fBGBH.Substring(0, 128);
            }
            if (fAdded.Length > 128)
            {
                fAdded = fAdded.Substring(0, 128);
            }
            if (DataName.Length > 128)
            {
                DataName = DataName.Substring(0, 128);
            }
            #endregion
            foreach (var item in config.DayList)
            {
                Int32 days = item.Days;
                if (item.IsParameterDays)
                {
                    obj = JZCommonHelper.GetCellValue(doc, item.PDays.SheetID, item.PDays.CellName);
                    String pDays = obj == null ? "" : obj.ToString().ToLower();
                    if (!int.TryParse(pDays.Replace("d", ""), out days))
                    {
                        continue;
                    }
                    //混凝土三级配特殊处理
                    switch (days)
                    {
                        case 1:
                            item.ItemID = "1";
                            item.ItemName = "混凝土抗压1天";
                            break;
                        case 3:
                            item.ItemID = "92";
                            item.ItemName = "混凝土抗压3天";
                            break;
                        case 7:
                            item.ItemID = "93";
                            item.ItemName = "混凝土抗压7天";
                            break;
                        case 28:
                            item.ItemID = "94";
                            item.ItemName = "混凝土抗压28天";
                            break;
                        case 56:
                            item.ItemID = "95";
                            item.ItemName = "混凝土抗压56天";
                            break;
                        default:
                            break;
                    }
                    days = days * 24;
                }
                float TemperatureSum = 0;
                DateTime StartTime = new DateTime();
                DateTime EndTime = new DateTime();
                StartTime = zjrq.AddHours(days);
                EndTime = StartTime.AddHours(config.StadiumRange);
                String insertSql = String.Format(@"INSERT INTO dbo.sys_stadium
                                        ( DataID ,
                                          DateSpan ,
                                          F_ItemId ,
                                          F_PH ,
                                          F_ZJRQ ,
                                          F_SJBH ,
                                          F_SJSize ,
                                          F_SYXM ,
                                          F_BGBH ,
                                          F_WTBH,
                                          F_Added,
                                          ModuleID,
                                          TestRoomCode,
                                          DataName,Temperatures,StadiumRange,
                                          TemperatureSum,StartTime,EndTime,ShuLiang
                                        )
                                VALUES  ( '{0}' , '{1}' ,'{2}' ,'{3}' ,'{4}' ,'{5}' ,'{6}' ,'{7}' ,
                                          '{8}' , '{9}' , '{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}',{19} )",
                                 doc.ID, days, item.ItemID, fPH, fZJRQ, fSJBH, fSJSize, item.ItemName, fBGBH, fWTBH, fAdded,
                                 docBase.ModuleID, docBase.TestRoomCode, DataName, config.Temperature, config.StadiumRange,
                                 TemperatureSum, StartTime, EndTime, strShuLiang);
                ExcuteCommand(insertSql);

                DocumentHelper dh = new DocumentHelper();
                int TemperatureType = dh.GetTemperatureTypeByDocumentID(doc.ID);
                SaveSingleStadiumTemperature(doc.ID, docBase.TestRoomCode, TemperatureType, config.Temperature, TemperatureSum, zjrq, days, config.StadiumRange);


            }

        }

        private void UpdateStadiumData(JZDocument doc, JZStadiumConfig config, DataTable stadiumData, Sys_Document docBase)
        {
            Object obj = null;
            #region
            String fPH = "";
            if (config.fPH != null)
            {
                obj = JZCommonHelper.GetCellValue(doc, config.fPH.SheetID, config.fPH.CellName);
                fPH = config.fPH == null ? "" : (obj == null ? "" : (obj.ToString().Replace("'", "''")));
            }
            String fZJRQ = "";
            if (config.fZJRQ != null)
            {
                obj = JZCommonHelper.GetCellValue(doc, config.fZJRQ.SheetID, config.fZJRQ.CellName);
                fZJRQ = config.fZJRQ == null ? "" : (obj == null ? "" : obj.ToString());
            }
            String fSJBH = "";
            if (config.fSJBH != null)
            {
                obj = JZCommonHelper.GetCellValue(doc, config.fSJBH.SheetID, config.fSJBH.CellName);
                fSJBH = config.fSJBH == null ? "" : (obj == null ? "" : (obj.ToString().Replace("'", "''")));
            }
            String fSJSize = "";
            if (config.fSJSize != null)
            {
                obj = JZCommonHelper.GetCellValue(doc, config.fSJSize.SheetID, config.fSJSize.CellName);
                fSJSize = config.fSJSize == null ? "" : (obj == null ? "" : obj.ToString());
            }
            String fAdded = "";
            if (config.fAdded != null)
            {
                obj = JZCommonHelper.GetCellValue(doc, config.fAdded.SheetID, config.fAdded.CellName);
                fAdded = config.fAdded == null ? "" : (obj == null ? "" : obj.ToString());
            }

            String fBGBH = "";
            if (config.fBGBH != null)
            {
                obj = JZCommonHelper.GetCellValue(doc, config.fBGBH.SheetID, config.fBGBH.CellName);
                fBGBH = config.fBGBH == null ? "" : (obj == null ? "" : obj.ToString());
            }
            String fWTBH = "";
            if (config.fWTBH != null)
            {
                obj = JZCommonHelper.GetCellValue(doc, config.fWTBH.SheetID, config.fWTBH.CellName);
                fWTBH = config.fWTBH == null ? "" : (obj == null ? "" : obj.ToString());
            }

            if (String.IsNullOrEmpty(fZJRQ))
            {
                //无制件日期则不保存
                //logger.Error("无制件日期，无法更新龄期提醒数据: id=" + doc.ID + "; fBGBH=" + fBGBH);
                return;
            }
            float? ShuLiang = null;
            if (config.ShuLiang != null)
            {
                obj = JZCommonHelper.GetCellValue(doc, config.ShuLiang.SheetID, config.ShuLiang.CellName);
                try
                {
                    ShuLiang = float.Parse(obj.ToString());
                }
                catch
                {
                    logger.Error("代表数量格式不对 " + obj);
                }
            }
            string strShuLiang = ShuLiang == null ? "null" : ShuLiang.ToString();
            DateTime zjrq;
            if (!DateTime.TryParse(fZJRQ, out zjrq))
            {
                logger.Error("制件日期格式不对 " + fZJRQ);
                return;
            }
            else
            {
                fZJRQ = zjrq.ToString("yyyy-MM-dd");
            }
            #endregion
            #region 控制数据长度防止溢出
            if (fSJBH.Length > 128)
            {
                fSJBH = fSJBH.Substring(0, 128);
            }
            if (fSJSize.Length > 128)
            {
                fSJSize = fSJSize.Substring(0, 128);
            }
            if (fWTBH.Length > 128)
            {
                fWTBH = fWTBH.Substring(0, 128);
            }
            if (fBGBH.Length > 128)
            {
                fBGBH = fBGBH.Substring(0, 128);
            }
            if (fAdded.Length > 128)
            {
                fAdded = fAdded.Substring(0, 128);
            }
            #endregion
            foreach (DataRow row in stadiumData.Rows)
            {
                if (row["F_IsDone"] != DBNull.Value && row["F_IsDone"].ToString() == "1")
                {
                    //取龄期提醒的数据，可能是多条，过滤已经完成的。
                    logger.Error("取龄期提醒的数据，可能是多条，过滤已经完成的 F_IsDone=" + row["F_IsDone"].ToString());
                    continue;
                }

                foreach (var item in config.DayList)
                {
                    Int32 days = item.Days;
                    if (item.IsParameterDays)
                    {
                        #region 混凝土三级配特殊处理
                        obj = JZCommonHelper.GetCellValue(doc, item.PDays.SheetID, item.PDays.CellName);
                        String pDays = obj == null ? "" : obj.ToString().ToLower();
                        if (!int.TryParse(pDays.Replace("d", ""), out days))
                        {
                            continue;
                        }
                        //混凝土三级配特殊处理
                        switch (days)
                        {
                            case 1:
                                item.ItemID = "1";
                                item.ItemName = "混凝土抗压1天";
                                break;
                            case 3:
                                item.ItemID = "92";
                                item.ItemName = "混凝土抗压3天";
                                break;
                            case 7:
                                item.ItemID = "93";
                                item.ItemName = "混凝土抗压7天";
                                break;
                            case 28:
                                item.ItemID = "94";
                                item.ItemName = "混凝土抗压28天";
                                break;
                            case 56:
                                item.ItemID = "95";
                                item.ItemName = "混凝土抗压56天";
                                break;
                            default:
                                break;
                        }
                        days = days * 24;
                        #endregion
                    }
                    String updateSql = "";

                    float TemperatureSum = 0;
                    DateTime StartTime = new DateTime();
                    DateTime EndTime = new DateTime();
                    StartTime = zjrq.AddHours(days);
                    EndTime = StartTime.AddHours(config.StadiumRange);

                    if (item.IsParameterDays && config.DayList.Count == 1)
                    {
                        #region 三级配
                        DataTable dt2 = GetDataTable(String.Format("Select ID from dbo.sys_stadium WHERE DataID='{0}' ", doc.ID));
                        if (dt2 != null && dt2.Rows.Count > 0)
                        {
                            TemperatureSum = float.Parse(row["TemperatureSum"].ToString());
                            Int32 isDone = 0;// obj == null ? 0 : 1;
                            if (item.ValidInfo != null)
                            {
                                #region 验证数据来源
                                obj = JZCommonHelper.GetCellValue(doc, item.ValidInfo.SheetID, item.ValidInfo.CellName);
                                if (obj == null || string.IsNullOrEmpty(obj.ToString()) || obj.ToString() == "/")
                                {
                                    isDone = 0;
                                }
                                else
                                {
                                    isDone = 1;
                                }
                                if (TemperatureSum >= config.Temperature && config.Temperature > 0)
                                {
                                    updateSql = String.Format(@"UPDATE dbo.sys_stadium SET 
                                                F_BGBH='{0}',F_PH='{1}',F_SJBH='{2}',F_ItemId='{3}', F_SYXM='{4}',
                                                F_SJSize='{5}',F_WTBH='{6}',F_ZJRQ='{7}',LastUpdatedTime=GETDATE(),DateSpan={8}, F_Added='{9}'
, Temperatures='{11}', StadiumRange='{12}',ShuLiang={13},F_IsDone={14}
                                                WHERE DataID='{10}'",
                                    fBGBH, fPH, fSJBH, item.ItemID, item.ItemName, fSJSize, fWTBH, fZJRQ, days, fAdded, doc.ID, config.Temperature, config.StadiumRange, strShuLiang, isDone);
                                }
                                else
                                {
                                    updateSql = String.Format(@"UPDATE dbo.sys_stadium SET 
                                                F_BGBH='{0}',F_PH='{1}',F_SJBH='{2}',F_ItemId='{3}', F_SYXM='{4}',
                                                F_SJSize='{5}',F_WTBH='{6}',F_ZJRQ='{7}',LastUpdatedTime=GETDATE(),DateSpan={8}, F_Added='{9}'
, Temperatures='{11}', StadiumRange='{12}',StartTime='{13}',EndTime='{14}',ShuLiang={15},F_IsDone={16}
                                                WHERE DataID='{10}'",
                                    fBGBH, fPH, fSJBH, item.ItemID, item.ItemName, fSJSize, fWTBH, fZJRQ, days, fAdded, doc.ID, config.Temperature,
                                    config.StadiumRange, StartTime, EndTime, strShuLiang, isDone);
                                }
                                #endregion
                            }
                            else
                            {
                                #region 无验证数据来源
                                if (TemperatureSum >= config.Temperature && config.Temperature > 0)
                                {
                                    updateSql = String.Format(@"UPDATE dbo.sys_stadium SET 
                                                F_BGBH='{0}',F_PH='{1}',F_SJBH='{2}',F_ItemId='{3}', F_SYXM='{4}',
                                                F_SJSize='{5}',F_WTBH='{6}',F_ZJRQ='{7}',LastUpdatedTime=GETDATE(),DateSpan={8}, F_Added='{9}'
, Temperatures='{11}', StadiumRange='{12}',ShuLiang={13}
                                                WHERE DataID='{10}'",
                                    fBGBH, fPH, fSJBH, item.ItemID, item.ItemName, fSJSize, fWTBH, fZJRQ, days, fAdded, doc.ID, config.Temperature, config.StadiumRange, strShuLiang);
                                }
                                else
                                {
                                    updateSql = String.Format(@"UPDATE dbo.sys_stadium SET 
                                                F_BGBH='{0}',F_PH='{1}',F_SJBH='{2}',F_ItemId='{3}', F_SYXM='{4}',
                                                F_SJSize='{5}',F_WTBH='{6}',F_ZJRQ='{7}',LastUpdatedTime=GETDATE(),DateSpan={8}, F_Added='{9}'
, Temperatures='{11}', StadiumRange='{12}',StartTime='{13}',EndTime='{14}',ShuLiang={15}
                                                WHERE DataID='{10}'",
                                    fBGBH, fPH, fSJBH, item.ItemID, item.ItemName, fSJSize, fWTBH, fZJRQ, days, fAdded, doc.ID, config.Temperature,
                                    config.StadiumRange, StartTime, EndTime, strShuLiang);
                                }
                                #endregion
                            }
                            ExcuteCommand(updateSql);
                        }
                        else
                        {
                            SaveStadiumData(doc, config, docBase);
                        }
                        #endregion
                    }
                    else
                    {
                        DataTable dt = GetDataTable(String.Format("Select ID from dbo.sys_stadium WHERE DataID='{0}' AND DateSpan='{1}' ", doc.ID, days));
                        if ((dt == null || dt.Rows.Count == 0))
                        {
                            SaveStadiumData(doc, config, docBase);
                        }
                        else
                        {
                            TemperatureSum = float.Parse(row["TemperatureSum"].ToString());
                            if (item.ValidInfo == null)
                            {
                                #region 无数据源验证
                                if (TemperatureSum >= config.Temperature && config.Temperature > 0)
                                {
                                    updateSql = String.Format(@"UPDATE dbo.sys_stadium SET 
                                                F_BGBH='{0}',F_PH='{1}',F_SJBH='{2}',F_ItemId='{3}', F_SYXM='{4}',
                                                F_SJSize='{5}',F_WTBH='{6}',F_ZJRQ='{7}',LastUpdatedTime=GETDATE(), F_Added='{8}'
, Temperatures='{11}', StadiumRange='{12}',ShuLiang={13}
                                                WHERE DataID='{9}' AND DateSpan='{10}'",
                                     fBGBH, fPH, fSJBH, item.ItemID, item.ItemName, fSJSize, fWTBH, fZJRQ, fAdded, doc.ID, days, config.Temperature,
                                     config.StadiumRange, strShuLiang);
                                }
                                else
                                {
                                    updateSql = String.Format(@"UPDATE dbo.sys_stadium SET 
                                                F_BGBH='{0}',F_PH='{1}',F_SJBH='{2}',F_ItemId='{3}', F_SYXM='{4}',
                                                F_SJSize='{5}',F_WTBH='{6}',F_ZJRQ='{7}',LastUpdatedTime=GETDATE(), F_Added='{8}'
, Temperatures='{11}', StadiumRange='{12}',StartTime='{13}',EndTime='{14}',ShuLiang={15}
                                                WHERE DataID='{9}' AND DateSpan='{10}'",
                                     fBGBH, fPH, fSJBH, item.ItemID, item.ItemName, fSJSize, fWTBH, fZJRQ, fAdded, doc.ID, days, config.Temperature,
                                     config.StadiumRange, StartTime, EndTime, strShuLiang);
                                }
                                #endregion
                            }
                            else
                            {
                                #region 数据源验证
                                obj = JZCommonHelper.GetCellValue(doc, item.ValidInfo.SheetID, item.ValidInfo.CellName);
                                Int32 isDone = 0;// obj == null ? 0 : 1;
                                if (obj == null || string.IsNullOrEmpty(obj.ToString()) || obj.ToString() == "/")
                                {
                                    isDone = 0;
                                }
                                else
                                {
                                    isDone = 1;
                                }

                                //TemperatureSum = float.Parse(row["TemperatureSum"].ToString());
                                if (TemperatureSum > config.Temperature && config.Temperature > 0)
                                {
                                    updateSql = String.Format(@"UPDATE dbo.sys_stadium SET 
                                                F_BGBH='{0}',F_IsDone={1},F_PH='{2}',F_SJBH='{3}', F_ItemId='{4}', F_SYXM='{5}',
                                                F_SJSize='{6}',F_WTBH='{7}',F_ZJRQ='{8}',LastUpdatedTime=GETDATE(), F_Added='{9}'
, Temperatures='{12}', StadiumRange='{13}',ShuLiang={14}
                                                WHERE DataID='{10}' AND DateSpan='{11}'",
                                      fBGBH, isDone, fPH, fSJBH, item.ItemID, item.ItemName, fSJSize, fWTBH, fZJRQ, fAdded, doc.ID, days, config.Temperature,
                                      config.StadiumRange, strShuLiang);

                                }
                                else
                                {
                                    updateSql = String.Format(@"UPDATE dbo.sys_stadium SET 
                                                F_BGBH='{0}',F_IsDone={1},F_PH='{2}',F_SJBH='{3}', F_ItemId='{4}', F_SYXM='{5}',
                                                F_SJSize='{6}',F_WTBH='{7}',F_ZJRQ='{8}',LastUpdatedTime=GETDATE(), F_Added='{9}'
, Temperatures='{12}', StadiumRange='{13}',StartTime='{14}',EndTime='{15}',ShuLiang={16}
                                                WHERE DataID='{10}' AND DateSpan='{11}'",
                                      fBGBH, isDone, fPH, fSJBH, item.ItemID, item.ItemName, fSJSize, fWTBH, fZJRQ, fAdded, doc.ID, days, config.Temperature,
                                      config.StadiumRange, StartTime, EndTime, strShuLiang);
                                }
                                #endregion
                            }
                            ExcuteCommand(updateSql);

                            DocumentHelper dh = new DocumentHelper();
                            string TestRoomCode = row["TestRoomCode"].ToString();
                            int TemperatureType = dh.GetTemperatureTypeByDocumentID(doc.ID);
                            int Temperatures = int.Parse(row["Temperatures"].ToString());
                            int DateSpan = int.Parse(row["DateSpan"].ToString());
                            int StadiumRange = int.Parse(row["StadiumRange"].ToString());
                            DateTime dtZjrq = new DateTime();
                            if (DateTime.TryParse(row["F_ZJRQ"].ToString(), out dtZjrq) == true)
                            {
                                SaveSingleStadiumTemperature(doc.ID, TestRoomCode, TemperatureType, Temperatures, TemperatureSum, dtZjrq, DateSpan, StadiumRange);
                            }
                        }
                    }
                }
            }
        }

        private class ThreadParameter
        {
            public JZDocument Doc;
            public Sys_Document DocBase;
        }

        public DataTable GetStadiumList(String segment, String company, String testroom, Int32 deviceType)
        {
            String sql = @"SELECT a.ID,
                a.DataID,
                a.ModuleID,
                a.DateSpan,
                f.标段名称,
                f.单位名称,
                f.试验室名称,
                a.DataName AS 名称,
                a.F_PH as 批号,
                a.F_ZJRQ as 制件日期,
                a.F_SJBH as 试件编号,
                a.F_Added AS 强度等级,
                a.F_SJSize as 试件尺寸,
                a.F_SYXM as 试验项目,
                a.ShuLiang as 代表数量,
                a.F_BGBH as 报告编号,
                a.F_WTBH as 委托编号 FROM dbo.sys_stadium a
                JOIN dbo.sys_module c ON a.ModuleID = c.ID
                JOIN dbo.sys_stadium_config d ON c.ID = d.ID AND
                 a.StartTime<=GETDATE() AND a.EndTime>=GETDATE()
                JOIN dbo.v_bs_codeName f ON a.TestRoomCode=f.试验室编码
                WHERE c.ModuleType=1 AND a.F_IsDone=0  ";

            try
            {
                if (deviceType > 0)
                {
                    sql += " AND c.DeviceType=" + deviceType;
                }
                if (testroom.Length == 16)
                {
                    sql += " AND a.TestRoomCode = '" + testroom + "'";
                }
                else
                {
                    TestRoomCodeHelper trch = new TestRoomCodeHelper();
                    List<String> list = trch.GetValidTestRoomCodeList();
                    if (list.Count == 0)
                    {
                        list.Add(testroom);
                    }
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
                    sql += " AND a.TestRoomCode in ('" + String.Join("','", real2.ToArray()) + "') ";
                }
                sql = sql + " Order By a.TestRoomCode, a.ModuleID, a.F_WTBH";
                //logger.Error("GetStadiumList sql:" + sql);
                return GetDataTable(sql);
            }
            catch (Exception e)
            {
                logger.Error("显示龄期错误：" + e.Message);
            }
            return null;
        }

        public DataTable GetTemperatureList(String TestRoomCode, DateTime Start, DateTime End, int TemperatureType)
        {
            if (End > DateTime.Now)
            {
                End = DateTime.Now;
            }

            String sql = string.Format(@"SELECT ID ,
                    TestRoomCode ,
                    a.DayID AS TestTime,
                    Temperature1 ,
                    Temperature2 ,
                    Temperature3 ,
                    ROUND(TemperatureAvg,1) AS TemperatureAvg,
                    Comment ,
                    LastEditUser ,
                    LastEditTime ,
                    0 as IsUpdated ,
                    TemperatureType
                     FROM dbo.sys_day as a LEFT JOIN 
            (SELECT * FROM dbo.sys_stadium_temperature WHERE TestRoomCode='{0}' and TemperatureType={3}) as b
            ON a.DayID=b.TestTime
            WHERE a.DayID>='{1}' AND a.DayID<'{2}' order by a.DayID desc", TestRoomCode, Start, End, TemperatureType);

            try
            {
                //logger.Error("GetTemperatureList sql:" + sql);
                return GetDataTable(sql);
            }
            catch (Exception e)
            {
                logger.Error("获取温度试验列表失败：" + e.Message);
            }
            return null;
        }

        public DataTable GetDocumentTemperaturesRange(string documentID)
        {
            var sql = @"DECLARE @StartTime DATE
                        DECLARE @EndTime DATE
                        DECLARE @TemperatureSum DECIMAL
                        DECLARE @TemperatureSumTemp DECIMAL
                        DECLARE @DataID UNIQUEIDENTIFIER
                        DECLARE @TemperatureType INT
                        DECLARE @Temperatures INT
                        DECLARE @TestRoomCode NVARCHAR(50)
                        SET @DataID = '" + documentID + @"'
                        SELECT  @TemperatureSum = TemperatureSum, @StartTime = F_ZJRQ,@EndTime = StartTime,@TestRoomCode=TestRoomCode,@Temperatures=Temperatures FROM dbo.sys_stadium WHERE DataID=@DataID
                        SELECT @TemperatureType = TemperatureType FROM dbo.sys_document_ext WHERE ID=@DataID
                        IF (@TemperatureSum> 0)
                        BEGIN
	                        SET @EndTime =DATEADD(DAY,-1,@EndTime)-- @EndTime-1
	                        SELECT @TemperatureSumTemp=ISNULL(SUM(TemperatureAvg),0) FROM dbo.sys_stadium_temperature WHERE TestRoomCode=@TestRoomCode AND TemperatureType=@TemperatureType AND TestTime>=@StartTime AND TestTime<=@EndTime
	                        IF(@TemperatureSumTemp<@Temperatures)
								SET @EndTime=DATEADD(DAY,1,@EndTime)--@EndTime+1
	                    END
	                        
                        ELSE
	                        SET @EndTime = GETDATE()

                        SELECT @StartTime AS  StartTime,@EndTime  AS EndTime, @TemperatureType AS TemperatureID";

            return GetDataTable(sql);
        }

        /// <summary>
        /// 保存温度记录
        /// </summary>
        /// <param name="dt">温度记录集</param>
        /// <returns>true更新成功，false更新失败</returns>
        public bool SaveTemperatures(string TestRoomCode, string LastEditUser, DataTable dt, int TemperatureType)
        {
            try
            {
                string strSQL = string.Empty;
                string ID = string.Empty;
                string Temperature1 = string.Empty;
                string Temperature2 = string.Empty;
                string Temperature3 = string.Empty;
                string TemperatureAvg = string.Empty;
                string Comment = string.Empty;
                string TestTime = string.Empty;
                //int TemperatureType = 0;
                #region 保存温度
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ID = dt.Rows[i]["ID"] == DBNull.Value ? "" : dt.Rows[i]["ID"].ToString();
                        Temperature1 = dt.Rows[i]["Temperature1"] == DBNull.Value ? null : dt.Rows[i]["Temperature1"].ToString();
                        if (string.IsNullOrEmpty(Temperature1))
                        {
                            Temperature1 = "null";
                        }
                        Temperature2 = dt.Rows[i]["Temperature2"] == DBNull.Value ? null : dt.Rows[i]["Temperature2"].ToString();
                        if (string.IsNullOrEmpty(Temperature2))
                        {
                            Temperature2 = "null";
                        }
                        Temperature3 = dt.Rows[i]["Temperature3"] == DBNull.Value ? null : dt.Rows[i]["Temperature3"].ToString();
                        if (string.IsNullOrEmpty(Temperature3))
                        {
                            Temperature3 = "null";
                        }
                        TemperatureAvg = dt.Rows[i]["TemperatureAvg"] == DBNull.Value ? "0" : dt.Rows[i]["TemperatureAvg"].ToString();
                        if (string.IsNullOrEmpty(TemperatureAvg))
                        {
                            TemperatureAvg = "0";
                        }
                        Comment = dt.Rows[i]["Comment"] == DBNull.Value ? "" : dt.Rows[i]["Comment"].ToString().Replace("'", "''");
                        TestTime = dt.Rows[i]["TestTime"] == DBNull.Value ? DateTime.Now.ToString() : dt.Rows[i]["TestTime"].ToString();
                        //TemperatureType = dt.Rows[i]["TemperatureType"] == DBNull.Value ? 0 :int.Parse( dt.Rows[i]["TemperatureAvg"].ToString());
                        if (TestTime == null || TemperatureAvg == null)
                        {
                            continue;
                        }
                        if (!string.IsNullOrEmpty(ID))
                        {
                            TestTime = DateTime.Parse(TestTime).ToString("yyyy-MM-dd");

                            #region 修改SQL
                            strSQL = string.Format(@"UPDATE dbo.sys_stadium_temperature SET
Temperature1={0},
Temperature2={1},
Temperature3={2},
TemperatureAvg={3},
Comment=N'{4}',
TestTime='{5}',
TemperatureType={7}
WHERE id='{6}' ", Temperature1, Temperature2, Temperature3, TemperatureAvg, Comment, TestTime, ID, TemperatureType);
                            #endregion
                            ExcuteCommand(strSQL);
                        }
                        else
                        {
                            if (IsExistTemperatureTypeAndTestTime(TemperatureType, TestRoomCode, TestTime) == false)
                            {
                                #region 添加SQL
                                strSQL = string.Format(@"INSERT INTO dbo.sys_stadium_temperature
        ( ID ,
          TestRoomCode ,
          Temperature1 ,
          Temperature2 ,
          Temperature3 ,
          TemperatureAvg ,
          Comment ,
          LastEditUser ,
          LastEditTime ,
          TestTime,
          TemperatureType
        )
VALUES  ( NEWID() , -- ID - uniqueidentifier
          N'{7}' , -- TestRoomCode - nvarchar(48)
          {0} , -- Temperature1 - float
          {1} , -- Temperature2 - float
          {2} , -- Temperature3 - float
          {3} , -- TemperatureAvg - float
          N'{4}' , -- Comment - nvarchar(1024)
          N'{8}' , -- LastEditUser - nvarchar(64)
          '{6}' , -- LastEditTime - datetime
          '{5}',  -- TestTime - datetime
          {9}
        )", Temperature1, Temperature2, Temperature3, TemperatureAvg, Comment, TestTime, DateTime.Now.ToString(), TestRoomCode, LastEditUser, TemperatureType);
                                #endregion
                                ExcuteCommand(strSQL);
                            }
                        }
                    }
                }
                #endregion
                #region 修改龄期
                DataTable dtUnDone = GetUnDoneStadiumAndHasTemperatures(TestRoomCode, TemperatureType);
                if (dtUnDone != null && dtUnDone.Rows.Count > 0)
                {
                    string StadiumID = string.Empty;
                    int Temperatures = 0;
                    float TemperatureSum = 0;
                    DateTime dtZjrq = new DateTime();

                    DateTime StartTime = new DateTime();
                    DateTime EndTime = new DateTime();
                    DataTable dtLatestTemperature = new DataTable();

                    foreach (DataRow row in dtUnDone.Rows)
                    {
                        StadiumID = row["ID"].ToString();
                        Temperatures = int.Parse(row["Temperatures"].ToString());
                        dtZjrq = DateTime.Parse(row["F_ZJRQ"].ToString());
                        TemperatureSum = float.Parse(row["TemperatureSum"].ToString());
                        if (TemperatureSum < Temperatures)
                        {
                            dtLatestTemperature = GetLatestTemperatureData(TestRoomCode, dtZjrq, Temperatures, TemperatureType);
                            StartTime = dtZjrq.AddHours(int.Parse(row["DateSpan"].ToString()));
                            EndTime = StartTime.AddHours(int.Parse(row["StadiumRange"].ToString()));
                            if (dtLatestTemperature != null && dtLatestTemperature.Rows.Count > 0)
                            {
                                TemperatureSum = float.Parse(dtLatestTemperature.Rows[0]["TRTSum"].ToString());
                                DateTime dtTemp = DateTime.Parse(dtLatestTemperature.Rows[0]["TestTime"].ToString());
                                if (dtTemp.CompareTo(StartTime) < 0)
                                {
                                    StartTime = dtTemp.AddDays(1);
                                    EndTime = StartTime.AddHours(int.Parse(row["StadiumRange"].ToString()));
                                }
                            }
                            strSQL = string.Format(@"UPDATE dbo.sys_stadium SET TemperatureSum={3},StartTime='{1}',EndTime='{2}' WHERE ID='{0}'", StadiumID, StartTime, EndTime, TemperatureSum);
                            ExcuteCommand(strSQL);
                        }
                    }
                }
                #endregion
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("SaveTemperatures异常错误:" + ex.ToString());
                return false;
                //throw ex;
            }
        }

        private Boolean IsExistTemperatureTypeAndTestTime(int TemperatureType, string TestRoomCode, string TestTime)
        {
            String sql = "SELECT ID FROM dbo.sys_stadium_temperature where TemperatureType=" + TemperatureType + " and TestTime='" + TestTime + "' and TestRoomCode='" + TestRoomCode + "' ";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取温度满足的起始时间
        /// </summary>
        /// <param name="TestRoomCode"></param>
        /// <param name="dtZJRQ"></param>
        /// <param name="Temperatures"></param>
        /// <returns></returns>
        public DataTable GetLatestTemperatureData(string TestRoomCode, DateTime dtZJRQ, int Temperatures, int TemperatureType)
        {
            string strSQL = string.Format(@"SELECT top 1 * FROM (
SELECT a.TestTime,a.TestRoomCode, 
( 
SELECT SUM(TemperatureAvg) FROM dbo.sys_stadium_temperature b WHERE 
a.TestTime>=b.TestTime AND a.TestRoomCode=b.TestRoomCode 
AND b.TestTime>='{1}' AND b.TestRoomCode='{0}' and b.TemperatureType={3} 
)AS TRTSum 
FROM dbo.sys_stadium_temperature a 
where TestTime>='{1}' AND TestRoomCode='{0}' and TemperatureType={3} 
)T WHERE TRTSum>={2} order by TestTime asc ", TestRoomCode, dtZJRQ, Temperatures, TemperatureType);

            return GetDataTable(strSQL);
        }
        public DataTable GetUnDoneStadiumAndHasTemperatures(string TestRoomCode, int TemperatureType)
        {
            string strSQL = string.Format(@"SELECT a.*,b.TemperatureType FROM dbo.sys_stadium a
Inner JOIN dbo.sys_document_ext b ON a.DataID=b.ID WHERE TestRoomCode='{0}' AND F_IsDone=0 AND Temperatures>0 and Temperatures>TemperatureSum and b.TemperatureType={1} ", TestRoomCode, TemperatureType);
            return GetDataTable(strSQL);
        }

        private void SaveSingleStadiumTemperature(Guid DataID, string TestRoomCode, int TemperatureType, int Temperatures, float TemperatureSum, DateTime dtZjrq, int DateSpan, int StadiumRange)
        {
            //string StadiumID = string.Empty;
            //int Temperatures = 0;
            //float TemperatureSum = 0;
            //DateTime dtZjrq = new DateTime();

            DateTime StartTime = new DateTime();
            DateTime EndTime = new DateTime();
            DataTable dtLatestTemperature = new DataTable();

            //StadiumID = row["ID"].ToString();
            //Temperatures = int.Parse(row["Temperatures"].ToString());
            //dtZjrq = DateTime.Parse(row["F_ZJRQ"].ToString());
            //TemperatureSum = float.Parse(row["TemperatureSum"].ToString());
            if (TemperatureSum < Temperatures)
            {
                dtLatestTemperature = GetLatestTemperatureData(TestRoomCode, dtZjrq, Temperatures, TemperatureType);
                StartTime = dtZjrq.AddHours(DateSpan);
                EndTime = StartTime.AddHours(StadiumRange);
                if (dtLatestTemperature != null && dtLatestTemperature.Rows.Count > 0)
                {
                    TemperatureSum = float.Parse(dtLatestTemperature.Rows[0]["TRTSum"].ToString());
                    DateTime dtTemp = DateTime.Parse(dtLatestTemperature.Rows[0]["TestTime"].ToString());
                    if (dtTemp.CompareTo(StartTime) < 0)
                    {
                        StartTime = dtTemp.AddDays(1);
                        EndTime = StartTime.AddHours(StadiumRange);
                    }
                }
                string strSQL = string.Format(@"UPDATE dbo.sys_stadium SET TemperatureSum={3},StartTime='{1}',EndTime='{2}' WHERE DataID='{0}'", DataID, StartTime, EndTime, TemperatureSum);
                ExcuteCommand(strSQL);
            }
        }

        public DataTable GetStadiumConfigList()
        {
            String sql = @"SELECT a.ID,b.Name,a.StadiumConfig,a.IsActive FROM dbo.sys_stadium_config a
                    JOIN dbo.sys_module b ON a.ID = b.ID WHERE b.IsActive = 1 ";
            return GetDataTable(sql);
        }

        public Boolean SaveStadiumConfig(Guid moduleID)
        {
            String sql = "SELECT ID FROM dbo.sys_stadium_config WHERE ID='" + moduleID + "' AND IsActive=1";
            Boolean flag = true;
            try
            {
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //已经存在龄期设置
                }
                else
                {
                    sql = "SELECT ID FROM dbo.sys_stadium_config WHERE ID='" + moduleID + "' AND IsActive=-1";
                    dt = GetDataTable(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        sql = String.Format("UPDATE dbo.sys_stadium_config SET IsActive=1, LastEditedUser='{0}',LastEditedTime=GETDATE() WHERE ID='{1}'",
                                    Yqun.Common.ContextCache.ApplicationContext.Current.UserName, moduleID);
                    }
                    else
                    {
                        sql = String.Format(@"INSERT INTO dbo.sys_stadium_config
                                                        ( ID ,
                                                          StadiumConfig ,
                                                          LastEditedUser ,
                                                          LastEditedTime ,
                                                          IsActive
                                                        )
                                                VALUES  ( '{0}' ,'','{1}', GETDATE(),1)",
                                                moduleID, Yqun.Common.ContextCache.ApplicationContext.Current.UserName);
                    }
                    ExcuteCommand(sql);
                }
            }
            catch (Exception e)
            {
                flag = false;
                logger.Error("save stadium config error: " + e.Message);
            }
            return flag;
        }

        public Boolean DeleteStadiumConfig(Guid moduleID)
        {
            String sql = String.Format("UPDATE dbo.sys_stadium_config SET IsActive=0, LastEditedUser='{0}',LastEditedTime=GETDATE() WHERE ID='{1}'",
                                    Yqun.Common.ContextCache.ApplicationContext.Current.UserName, moduleID);
            Boolean flag = true;
            try
            {
                ExcuteCommand(sql);
            }
            catch (Exception e)
            {
                flag = false;
                logger.Error("delete stadium error: " + e.Message);
            }
            return flag;
        }

        public Boolean UpdateStadiumConfig(Guid moduleID, String config, Boolean isActive)
        {
            Int32 active = isActive ? 1 : 0;
            String sql = String.Format(@"UPDATE dbo.sys_stadium_config SET LastEditedUser='{0}',LastEditedTime=GETDATE(),
                StadiumConfig='{1}', IsActive={2} WHERE ID='{3}'",
                                    Yqun.Common.ContextCache.ApplicationContext.Current.UserName, config.Replace("'", "''"), active, moduleID);
            Boolean flag = true;
            try
            {
                int i = ExcuteCommand(sql);
                //if (i == 1)
                //{
                //    ExcuteCommand("DELETE FROM dbo.sys_stadium_config_days WHERE ModuleID='"+moduleID+"'");
                //    String[] days = searchRange.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (String item in days)
                //    {
                //        sql = String.Format(@"INSERT INTO dbo.sys_stadium_config_days( ModuleID, Days ) VALUES  ( '{0}',{1})",
                //            moduleID, item);
                //        ExcuteCommand(sql);
                //    }
                //}
            }
            catch (Exception e)
            {
                flag = false;
                logger.Error("update stadium error: " + e.Message);
            }
            return flag;
        }

        public String GetSearchRange(Guid moduleID)
        {
            String sql = "select Days from sys_stadium_config_days where ModuleID='" + moduleID + "'";
            String flag = "";
            try
            {
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        flag += dt.Rows[i][0].ToString() + ",";
                    }

                }
                if (flag.Length > 0)
                {
                    flag = flag.TrimEnd(',');
                }
            }
            catch (Exception e)
            {
                logger.Error("get search range error: " + e.Message);
            }
            return flag;
        }

        public Object GetTestObject(Guid stadiumID, Int32 deviceType)
        {
            Object obj = null;
            String sql = String.Format(@"SELECT a.ID AS DocumentID,a.ModuleID,b.ID AS StadiumID,c.Name,b.F_ItemId,
                                    b.F_WTBH,b.F_SJSize,b.F_Added,b.F_ItemIndex FROM dbo.sys_document a
                                    JOIN dbo.sys_stadium b ON a.ID = b.DataID
                                    JOIN dbo.sys_module c ON a.ModuleID = c.ID WHERE b.ID = '{0}'", stadiumID);
            try
            {
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count == 1)
                {
                    if (deviceType == 1)
                    {
                        //压力机
                        JZYLJ ylj = new JZYLJ()
                        {
                            DocumentID = new Guid(dt.Rows[0]["DocumentID"].ToString()),
                            ModuleID = new Guid(dt.Rows[0]["ModuleID"].ToString()),
                            StadiumID = new Guid(dt.Rows[0]["StadiumID"].ToString()),
                            WTBH = dt.Rows[0]["F_WTBH"].ToString(),
                            TestName = dt.Rows[0]["Name"].ToString(),
                            QDDJ = dt.Rows[0]["F_Added"].ToString(),
                            SJCC = dt.Rows[0]["F_SJSize"].ToString(),
                            SJZH = "",
                            YSBJ = "",
                            ItemIndex = Int32.Parse(dt.Rows[0]["F_ItemIndex"].ToString())
                        };
                        obj = ylj;
                    }
                    else if (deviceType == 2)
                    {
                        //万能机
                        JZWNJ wnj = new JZWNJ()
                        {
                            DocumentID = new Guid(dt.Rows[0]["DocumentID"].ToString()),
                            ModuleID = new Guid(dt.Rows[0]["ModuleID"].ToString()),
                            StadiumID = new Guid(dt.Rows[0]["StadiumID"].ToString()),
                            WTBH = dt.Rows[0]["F_WTBH"].ToString(),
                            TestName = dt.Rows[0]["Name"].ToString(),
                            JBDH = dt.Rows[0]["F_Added"].ToString(),
                            GJZJ = dt.Rows[0]["F_SJSize"].ToString(),
                            ItemIndex = Int32.Parse(dt.Rows[0]["F_ItemIndex"].ToString())
                        };
                        obj = wnj;
                    }
                }

            }
            catch (Exception e)
            {
                logger.Error("Get Test Object Error: " + e.Message);
            }

            return obj;
        }


        /// <summary>
        /// 根据龄期ID获取龄期
        /// </summary>
        /// <param name="StadiumID"></param>
        /// <returns></returns>
        public DataTable GetStadiumByID(Guid StadiumID)
        {
            logger.Error("select * from sys_stadium where id='" + StadiumID.ToString() + "'");
            return GetDataTable("select * from sys_stadium where id='" + StadiumID.ToString() + "'");
        }


        public DataTable GetSingleStadium(String testRoomCode, String wtbh)
        {
            String sql = String.Format(@"SELECT a.ID,
                a.DataID,
                a.ModuleID,
                a.DateSpan,
                f.标段名称,
                f.单位名称,
                f.试验室名称,
                a.DataName AS 名称,
                a.F_PH as 批号,
                a.F_ZJRQ as 制件日期,
                a.F_SJBH as 试件编号,
                a.F_SJSize as 试件尺寸,
                a.F_SYXM as 试验项目,
                a.F_BGBH as 报告编号,
                a.F_WTBH as 委托编号 FROM dbo.sys_stadium a
                JOIN dbo.sys_module c ON a.ModuleID = c.ID
                JOIN dbo.sys_stadium_config d ON c.ID = d.ID  AND
                 a.StartTime<=GETDATE() AND a.EndTime>=GETDATE()
                JOIN dbo.v_bs_codeName f ON a.TestRoomCode=f.试验室编码
                WHERE c.ModuleType=1 AND a.F_IsDone=0  AND c.DeviceType=2 AND a.TestRoomCode='{0}' 
                AND a.F_WTBH='{1}'", testRoomCode, wtbh);
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable(sql);
            }
            catch
            {
                logger.Error("GetSingleStadium Error:" + sql);
                throw;
            }
            return dt;
        }

        public DataTable SearchStadiumByWTBH(String wtbh)
        {
            String sql = @"SELECT a.ID,
                a.DataID,
                a.ModuleID,
                a.DateSpan,
                f.标段名称,
                f.单位名称,
                f.试验室名称,
                a.DataName AS 名称,
                a.F_PH as 批号,
                a.F_ZJRQ as 制件日期,
                a.F_SJBH as 试件编号,
                a.F_Added AS 强度等级,
                a.F_SJSize as 试件尺寸,
                a.F_SYXM as 试验项目,
                a.F_BGBH as 报告编号,
                a.F_WTBH as 委托编号 FROM dbo.sys_stadium a
                JOIN dbo.sys_module c ON a.ModuleID = c.ID
                JOIN dbo.sys_stadium_config d ON c.ID = d.ID 
                JOIN dbo.v_bs_codeName f ON a.TestRoomCode=f.试验室编码
                WHERE a.F_WTBH like '%" + wtbh + "%'";
            return GetDataTable(sql);
        }

        public Boolean ResetStadiumToToday(Guid stadiumID)
        {
            Boolean flag = false;
            try
            {
                String sql = "UPDATE dbo.sys_stadium SET StartTime=GETDATE(),EndTime=DATEADD(DAY,1,GETDATE()),F_IsDone=0 WHERE ID='" +
                    stadiumID + "'";
                int i = ExcuteCommand(sql);
                flag = (i == 1);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return flag;
        }

        public Boolean ResetStadiumToTodayByDataID(string DataID)
        {
            Boolean flag = false;
            try
            {
                String sql = "UPDATE dbo.sys_stadium SET StartTime=GETDATE(),EndTime=DATEADD(DAY,1,GETDATE()),F_IsDone=0 WHERE ID =(SELECT TOP 1 ID FROM dbo.sys_stadium WHERE DataID='" + DataID + "' AND StartTime<GETDATE() ORDER BY StartTime DESC)";
                int i = ExcuteCommand(sql);
                flag = (i >= 1);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return flag;
        }

    }
}
