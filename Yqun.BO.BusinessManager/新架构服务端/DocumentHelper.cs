using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BizCommon;
using System.Data.SqlClient;
using FarPoint.Win.Spread;
using FarPoint.Win;
using Yqun.Common.ContextCache;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data.SqlTypes;

namespace Yqun.BO.BusinessManager
{
    public class DocumentHelper : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 取资料表中的数据
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public String GetDocumentByID(Guid documentID)
        {
            String sql = "SELECT Data FROM dbo.sys_document WHERE ID=@0";
            DataSet ds = GetDataSet(sql, new Object[] { documentID });
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            return "";
        }

        public DataTable GetDocumentDataListByModuleIDAndTestRoomCode(Guid moduleID, String testRoomCode)
        {
            String sql = "SELECT Data,ID,TestRoomCode,Status,DataName,TryType,ModuleID,BGBH FROM dbo.sys_document WHERE moduleID='" + moduleID + "' and testRoomCode='" + testRoomCode + "' and Status>0";
            return GetDataTable(sql);
        }

        public String GetDocumentByTestRoomModuleAndWTBH(Guid moduleID, String testRoom, String wtbh)
        {
            String sql = "SELECT Data FROM dbo.sys_document WHERE moduleID=@0 and TestRoomCode=@1 and WTBH=@2";
            DataSet ds = GetDataSet(sql, new Object[] { moduleID, testRoom, wtbh });
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
            }
            return "";
        }

        public Guid GetDocumentIDByTestRoomModuleAndWTBH(Guid moduleID, String testRoom, String wtbh)
        {
            String sql = "SELECT ID FROM dbo.sys_document WHERE moduleID=@0 and TestRoomCode=@1 and WTBH=@2 and Status>0";
            DataSet ds = GetDataSet(sql, new Object[] { moduleID, testRoom, wtbh });
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return new Guid(ds.Tables[0].Rows[0][0].ToString());
                }
            }
            return Guid.Empty;
        }

        public int GetTemperatureTypeByDocumentID(Guid DocID)
        {
            String sql = "SELECT TemperatureType FROM dbo.sys_document_ext WHERE ID=@0";
            DataSet ds = GetDataSet(sql, new Object[] { DocID });
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return int.Parse(ds.Tables[0].Rows[0][0].ToString());
                }
            }
            return -1;
        }

        public Guid SaveDocument(JZDocument doc, Sys_Document docBase)
        {
            return SaveDocumentDetail(doc, docBase, true);
        }
        /// <summary>
        /// 保存或修改用户提交的资料
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        private Guid SaveDocumentDetail(JZDocument doc, Sys_Document docBase, Boolean needValid)
        {
            Guid flag = Guid.Empty;
            try
            {
                String sql = "SELECT * FROM dbo.sys_document WHERE ID='" + doc.ID + "'";
                DataTable dt = GetDataTable(sql);
                if (dt == null)
                {
                    logger.Error("保存资料时出错: " + sql);
                }
                else
                {
                    JZDocument oldDoc = null;
                    DataRow row = null;
                    ModuleHelper mh = new ModuleHelper();
                    Sys_Module module = mh.GetModuleBaseInfoByID(docBase.ModuleID);
                    Boolean relationRequestChange = false;
                    if (dt.Rows.Count == 0)
                    {
                        //new
                        row = dt.NewRow();
                        dt.Rows.Add(row);
                    }
                    else
                    {
                        //update
                        row = dt.Rows[0];
                        oldDoc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dt.Rows[0]["Data"].ToString());
                        if (row["Status"].ToString() == "2")
                        {
                            relationRequestChange = true;
                        }
                    }

                    if (doc.ID == Guid.Empty)
                    {
                        doc.ID = Guid.NewGuid();
                        row["ID"] = doc.ID;
                        row["SegmentCode"] = docBase.TestRoomCode.Substring(0, 8);
                        row["CompanyCode"] = docBase.TestRoomCode.Substring(0, 12);
                        row["TestRoomCode"] = docBase.TestRoomCode;
                        row["CreatedUser"] = Yqun.Common.ContextCache.ApplicationContext.Current.UserName;
                        row["CreatedTime"] = DateTime.Now;
                        row["ModuleID"] = docBase.ModuleID;
                        row["DataName"] = docBase.DataName;
                        row["SetDataNameUser"] = DBNull.Value;
                        row["SetDataNameTime"] = DBNull.Value;
                        row["TryType"] = docBase.TryType;
                        row["TryPerson"] = Yqun.Common.ContextCache.ApplicationContext.Current.UserName;
                        row["TryTime"] = DateTime.Now;
                    }
                    row["Data"] = Newtonsoft.Json.JsonConvert.SerializeObject(doc);
                    row["Status"] = 1;
                    row["NeedUpload"] = 1;
                    row["LastEditedTime"] = DateTime.Now;
                    int r = Update(dt);
                    if (r == 1)
                    {
                        flag = doc.ID;
                        if (ApplyModuleSetting(doc, module.ModuleSettings))//给modulesetting定义字段赋值
                        {

                            docBase = GetDocumentBaseInfoByID(doc.ID);

                            if (needValid)
                            {
                                //验证是否合格，并发短信
                                QualifyHelper qh = new QualifyHelper();
                                qh.Qualify(doc, module);

                                //修改日志
                                LogHelper lh = new LogHelper();
                                lh.SaveEditLog(oldDoc, doc, "修改", relationRequestChange, docBase);
                                //修改修改申请的报告编号added by Tan in 20140912
                                UpdateRequestChange(doc.ID);
                            }
                            //龄期提醒
                            StadiumHelper sh = new StadiumHelper();
                            sh.InitStadium(docBase, doc);

                            //生成配合比信息，仅考虑三级配模板
                            if (docBase.ModuleID == new Guid("F34C2B8B-DDBE-4C04-BD01-F08B0F479AE8"))
                            {
                                GenerateOnePLD(doc.ID.ToString());
                            }

                            //FpSpread fpSpread = new FpSpread();

                            //foreach (JZSheet sheet in doc.Sheets)
                            //{
                            //    String sheetXML = JZCommonHelper.GZipDecompressString(mh.GetSheetXMLByID(sheet.ID));
                            //    SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), sheetXML, "SheetView") as SheetView;
                            //    SheetView.Tag = sheet.ID;
                            //    SheetView.Cells[0, 0].Value = "";
                            //    SheetView.Protect = true;

                            //    fpSpread.Sheets.Add(SheetView);

                            //    foreach (JZCell dataCell in sheet.Cells)
                            //    {
                            //        Cell cell = SheetView.Cells[dataCell.Name];

                            //        if (cell != null)
                            //        {
                            //            cell.Value = dataCell.Value;
                            //        }
                            //    }
                            //}

                            ////自动生成Excel，图片和报告页的pdf
                            //SourceHelper sourceHelper = new SourceHelper();
                            //sourceHelper.CreateRalationFiles(fpSpread, doc.ID, module.ReportIndex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Save or Update Document: " + ex.Message);
            }
            return flag;
        }
        /// <summary>
        /// 复制资料扩展属性
        /// </summary>
        /// <param name="docID"></param>
        /// <returns></returns>
        private bool CopyDocumentExt(Guid CopyDataID, Guid NewDocID)
        {
            bool flag = true;
            try
            {
                SqlCommand cmd = new SqlCommand();
                String sql = "INSERT INTO dbo.sys_document_ext SELECT @NewDocID ,TemperatureType FROM dbo.sys_document_ext WHERE ID=@CopyDataID";

                cmd.Parameters.Add(new SqlParameter("@CopyDataID", CopyDataID));
                cmd.Parameters.Add(new SqlParameter("@NewDocID", NewDocID));
                cmd.CommandText = sql;
                ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd });
            }
            catch (Exception e)
            {
                logger.Error(string.Format("CopyDocumentExt:CopyDataID:{0} NewDocID:{1} error:" + e.Message, CopyDataID, NewDocID));
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 修改资料状态
        /// </summary>
        /// <param name="docID"></param>
        /// <returns></returns>
        public bool UpdateDocumentStatus(Guid DataID, int Status)
        {
            bool flag = true;
            try
            {
                SqlCommand cmd = new SqlCommand();
                String sql = "update dbo.sys_document set Status=@Status WHERE ID=@DataID";

                cmd.Parameters.Add(new SqlParameter("@DataID", DataID));
                cmd.Parameters.Add(new SqlParameter("@Status", Status));
                cmd.CommandText = sql;
                ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd });
            }
            catch (Exception e)
            {
                logger.Error(string.Format("UpdateDocumentStatus DataID:{0} Status:{1} error:" + e.Message, DataID, Status));
                flag = false;
            }
            return flag;
        }

        private bool UpdateRequestChange(Guid DocID)
        {
            bool flag = true;
            try
            {
                SqlCommand cmd = new SqlCommand();
                String sql = "UPDATE sys_request_change SET WTBH=b.WTBH,BGBH=b.BGBH FROM dbo.sys_request_change a,dbo.sys_document b WHERE a.DataID=b.ID AND b.ID=@ID";
                cmd.Parameters.Add(new SqlParameter("@ID", DocID));
                cmd.CommandText = sql;
                //logger.Error("ApplyModuleSetting cmd.CommandText:" + cmd.CommandText);
                ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd });

            }
            catch (Exception e)
            {
                logger.Error("UpdateRequestChangeBGBH error: " + e.Message);
                flag = false;
            }
            return flag;
        }

        public bool ApplyModuleSetting(JZDocument doc, List<ModuleSetting> moduleSettings)
        {
            bool flag = true;
            string tsql = "";
            try
            {
                if (moduleSettings != null && moduleSettings.Count > 0)
                {
                    SqlCommand cmd = new SqlCommand();
                    String sql = "UPDATE dbo.sys_document SET {0} WHERE ID=@ID";
                    String sub = "";
                    foreach (ModuleSetting item in moduleSettings)
                    {
                        if (String.IsNullOrEmpty(item.CellName) || String.IsNullOrEmpty(item.DocColumn))
                        {
                            logger.Error(doc.ID + "@" + item.DocColumn + ":" + item.DocColumn + "breaked");
                            continue;
                        }

                        sub += item.DocColumn + "=@" + item.DocColumn + ",";
                        foreach (JZSheet sheet in doc.Sheets)
                        {
                            if (item.SheetID == sheet.ID)
                            {
                                if (!cmd.Parameters.Contains("@" + item.DocColumn))
                                {
                                    object oCellValue = GetDBCellValue(sheet.Cells, item.CellName);
                                    logger.Error(doc.ID + "@" + item.DocColumn + ":" + oCellValue);
                                    if (item.DocColumn.ToUpper() == "BGRQ" || item.DocColumn.ToUpper() == "EXT21" || item.DocColumn.ToUpper() == "EXT22" || item.DocColumn.ToUpper() == "EXT23")
                                    {
                                        DateTime dtBGRQ = new DateTime();
                                        bool bBGRQ = false;
                                        if (oCellValue != null && !string.IsNullOrEmpty(oCellValue.ToString().Trim()))
                                        {
                                            if (oCellValue is decimal || oCellValue is float || oCellValue is double)
                                            {
                                                //oBGRQ = int.Parse(item.Value.ToString());
                                                double dRQ = double.Parse(oCellValue.ToString());
                                                try
                                                {
                                                    dtBGRQ = DateTime.FromOADate(dRQ);
                                                    bBGRQ = true;

                                                }
                                                catch (Exception)
                                                {
                                                    bBGRQ = false;
                                                }
                                            }
                                            else if (oCellValue is FarPoint.CalcEngine.CalcError)
                                            {
                                                oCellValue = DBNull.Value;
                                            }
                                            else
                                            {
                                                bBGRQ = DateTime.TryParse(oCellValue == null ? "" : oCellValue.ToString(), out dtBGRQ);
                                            }
                                            if (bBGRQ)
                                            {
                                                oCellValue = dtBGRQ.ToString("yyyy-MM-dd");
                                            }
                                            else
                                            {
                                                logger.Error(string.Format("ApplyModuleSetting 日期转换错误,DocID:{0} 原值:" + oCellValue, doc.ID));
                                                oCellValue = DBNull.Value;
                                            }
                                        }
                                        else
                                        {
                                            oCellValue = DBNull.Value;
                                        }
                                    }
                                    //logger.Error(string.Format("ApplyModuleSetting:{0},{1}", item.DocColumn, oCellValue));
                                    cmd.Parameters.Add(new SqlParameter("@" + item.DocColumn, oCellValue));
                                    //logger.Info(doc.ID + "--" + "@" + item.DocColumn+"--"+oCellValue);
                                    break;
                                }
                            }
                        }
                    }
                    cmd.Parameters.Add(new SqlParameter("@ID", doc.ID));
                    tsql = String.Format(sql, sub.Substring(0, sub.Length - 1));
                    cmd.CommandText = tsql;
                    //logger.Error("ApplyModuleSetting cmd.CommandText:" + cmd.CommandText);
                    ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd });
                }
            }
            catch (Exception e)
            {
                logger.Error("apply module sql:" + tsql);
                logger.Error(string.Format("apply module setting error,DocID:{0}  " + e.ToString(), doc.ID));
                flag = false;
            }
            return flag;
        }

        public void ApplyExtFields(Guid moduleID)
        {
            ModuleHelper mh = new ModuleHelper();
            Sys_Module module = mh.GetModuleBaseInfoByID(moduleID);
            if (module != null && module.ModuleSettings != null && module.ModuleSettings.Count > 0)
            {
                String sql = String.Format("SELECT Data FROM dbo.sys_document WHERE ModuleID='{0}'", moduleID);
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(row[0].ToString());
                        if (doc != null)
                        {
                            ApplyModuleSetting(doc, module.ModuleSettings);
                        }
                    }
                }
            }
        }

        private Object GetDBCellValue(List<JZCell> cells, String cellName)
        {
            foreach (JZCell cell in cells)
            {
                if (cell.Name == cellName)
                {
                    if (cell.Value == null)
                    {
                        return DBNull.Value;
                    }
                    else
                    {
                        if (cell.Value is FarPoint.CalcEngine.CalcError)
                        {
                            return DBNull.Value;
                        }
                        return cell.Value;
                    }
                }
            }
            return DBNull.Value;
        }

        public Sys_TaiZhang GetDocumentList(Guid moduleID, String testRoomCode, String sortColumn, Int32 isDesc,
            Int32 pageIndex, Int32 pageCount, String filter)
        {
            Sys_TaiZhang tz = new Sys_TaiZhang();
            try
            {
                tz.CustomViews = new List<JZCustomView>();
                ModuleHelper mh = new ModuleHelper();
                Sys_Module module = mh.GetModuleBaseInfoByID(moduleID);

                if (module != null)
                {
                    List<JZCustomView> userList = GetCustomViewList(moduleID, testRoomCode);

                    String display = "ID,DataName,TryType";
                    String where = " And TestRoomCode='" + testRoomCode + "' And ModuleID='" + moduleID + "' And Status>0 " + filter;
                    if (module.ModuleType == 1)
                    {
                        JZCustomView cv = new JZCustomView();
                        cv.Description = "资料名称";
                        cv.DocColumn = "DataName";
                        cv.ColumnWidth = 100;
                        tz.CustomViews.Add(cv);
                        foreach (var item in userList)
                        {
                            if (!String.IsNullOrEmpty(item.DocColumn) && item.DocColumn == cv.DocColumn)
                            {
                                cv.ColumnWidth = item.ColumnWidth;
                                userList.Remove(item);
                                break;
                            }
                        }
                        cv = new JZCustomView();
                        cv.Description = "试验类别";
                        cv.DocColumn = "TryType";
                        cv.ColumnWidth = 100;
                        tz.CustomViews.Add(cv);
                        foreach (var item in userList)
                        {
                            if (!String.IsNullOrEmpty(item.DocColumn) && item.DocColumn == cv.DocColumn)
                            {
                                cv.ColumnWidth = item.ColumnWidth;
                                userList.Remove(item);
                                break;
                            }
                        }
                    }

                    var ConvertColumns = new Dictionary<string, string>();

                    foreach (ModuleSetting ms in module.ModuleSettings)
                    {
                        if (String.IsNullOrEmpty(ms.DocColumn) || String.IsNullOrEmpty(ms.CellName) || !ms.IsShow)
                        {
                            continue;
                        }

                        JZCustomView cv = new JZCustomView();
                        cv.CellName = ms.CellName;
                        cv.Description = ms.Description;
                        cv.DocColumn = ms.DocColumn;
                        cv.SheetID = ms.SheetID;
                        cv.ColumnWidth = 100;
                        tz.CustomViews.Add(cv);
                        display += "," + ms.DocColumn;
                        if (ms.Description.IndexOf("时间") > -1 || ms.Description.IndexOf("日期") > -1)
                        {
                            ConvertColumns[ms.DocColumn] = ms.Description;
                        }

                        foreach (var item in userList)
                        {
                            if (!String.IsNullOrEmpty(item.DocColumn) && item.DocColumn == cv.DocColumn)
                            {
                                cv.ColumnWidth = item.ColumnWidth;
                                userList.Remove(item);
                                break;
                            }
                        }
                    }

                    if (userList != null && userList.Count > 0)
                    {
                        display += ", Data";
                        tz.CustomViews.AddRange(userList);
                    }
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "sp_document_list";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@strGetFields", display));
                    cmd.Parameters.Add(new SqlParameter("@fldName", sortColumn));
                    cmd.Parameters.Add(new SqlParameter("@PageSize", pageCount));
                    cmd.Parameters.Add(new SqlParameter("@PageIndex", pageIndex));
                    cmd.Parameters.Add(new SqlParameter("@OrderType", isDesc));
                    cmd.Parameters.Add(new SqlParameter("@strWhere", where));
                    SqlParameter sqlP = new SqlParameter("@totalCount", 0);
                    sqlP.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(sqlP);

                    DataTable dt = GetDataFromSP(cmd);
                    tz.TotalCount = Convert.ToInt32(sqlP.Value);

                    List<List<JZCell>> list = new List<List<JZCell>>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<string> lstPXedSGDataIDs = null;
                        if (module.ModuleType == 1)
                        {
                            StringBuilder strIDs = new StringBuilder();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (i < dt.Rows.Count - 1)
                                {
                                    strIDs.AppendFormat("'{0}',", dt.Rows[i]["ID"]);
                                }
                                else
                                {
                                    strIDs.AppendFormat("'{0}'", dt.Rows[i]["ID"]);
                                }
                            }
                            lstPXedSGDataIDs = GetPXedSGDataIDs(strIDs.ToString());
                        }
                        foreach (DataRow row in dt.Rows)
                        {
                            List<JZCell> dtRow = new List<JZCell>();
                            #region 系统台账列
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {

                                if (dt.Columns[i].ColumnName == "Data")
                                {
                                    continue;
                                }
                                JZCell cell = new JZCell();
                                cell.Name = dt.Columns[i].ColumnName;
                                if (ConvertColumns.ContainsKey(cell.Name))
                                {
                                    cell.Value = ConvertDateCell(row[cell.Name]);
                                }
                                else
                                {
                                    cell.Value = row[cell.Name];
                                }

                                dtRow.Add(cell);
                            }
                            #endregion
                            #region 用户台账列
                            if (userList != null && userList.Count > 0)
                            {
                                JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(row["Data"].ToString());
                                foreach (JZCustomView customView in userList)
                                {
                                    JZCell cell2 = new JZCell();
                                    cell2.Name = customView.Description;
                                    cell2.Value = JZCommonHelper.GetCellValue(doc, customView.SheetID, customView.CellName);
                                    dtRow.Add(cell2);
                                }
                            }
                            #endregion
                            #region 资料是否被平行
                            JZCell cell3 = new JZCell();
                            cell3.Name = "IsPXed";
                            if (lstPXedSGDataIDs != null && lstPXedSGDataIDs.Count > 0)
                            {
                                bool IsPXed = false;
                                foreach (string sgDataID in lstPXedSGDataIDs)
                                {
                                    if (sgDataID == row["ID"].ToString())
                                    {
                                        IsPXed = true;
                                        break;
                                    }
                                }
                                if (IsPXed == true)
                                {
                                    cell3.Value = 1;
                                }
                                else
                                {
                                    cell3.Value = 0;
                                }
                            }
                            else
                            {
                                cell3.Value = 0;
                            }
                            dtRow.Add(cell3);
                            #endregion
                            list.Add(dtRow);
                        }
                    }
                    tz.Data = list;
                }
            }
            catch (Exception e)
            {
                logger.Error("get document list error: " + e.Message + "; moduleID=" + moduleID + "; testroomcode=" +
                    testRoomCode + "; sortColumn=" + sortColumn + "; filter=" + filter);
            }
            return tz;
        }

        public List<string> GetPXedSGDataIDs(string strDataIDs)
        {
            List<string> list = new List<string>();
            if (strDataIDs.Length > 0)
            {
                String sql = "SELECT SGDataID FROM dbo.sys_px_relation WHERE SGDataID in({0})";
                DataTable dt = GetDataTable(String.Format(sql, strDataIDs));
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(row["SGDataID"].ToString());
                    }
                }
            }
            return list;
        }

        public object ConvertDateCell(object value)
        {
            if (value != null)
            {
                var s = Convert.ToString(value);
                var time = default(DateTime);

                if (string.IsNullOrEmpty(s) == false)
                {
                    if (DateTime.TryParse(s, out time))
                    {
                        return time.ToString("yyyy-MM-dd");
                    }
                }
            }

            return value;
        }

        public List<JZCustomView> GetCustomViewList(Guid moduleID, String testRoomCode)
        {
            List<JZCustomView> list = new List<JZCustomView>();
            String sql = "SELECT CustomView FROM dbo.sys_custom_view WHERE ModuleID='{0}' AND TestRoomCode='{1}'";
            DataTable dt = GetDataTable(String.Format(sql, moduleID, testRoomCode));
            if (dt != null && dt.Rows.Count > 0)
            {
                String json = dt.Rows[0]["CustomView"].ToString();
                list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZCustomView>>(json);
                if (list == null)
                {
                    list = new List<JZCustomView>();
                }
            }
            return list;
        }

        private DataTable GetDataFromSP(SqlCommand cmd)
        {
            try
            {
                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();
                }
                cmd.Connection = Connection as SqlConnection;
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                //logger.Error("GetDataFromSP:" + cmd.CommandText);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                logger.Error("run data sp error: " + ex.Message);
                return null;
            }
            finally
            {
                Connection.Close();
            }
        }

        public Sys_Document GetDocumentBaseInfoByID(Guid dataID)
        {
            String sql = "SELECT ID,TestRoomCode,Status,DataName,TryType,ModuleID,BGBH,LastEditedTime FROM dbo.sys_document WHERE ID='" + dataID + "' ";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count == 1)
            {
                DateTime? NullTime = null;
                Sys_Document doc = new Sys_Document()
                {
                    ID = new Guid(dt.Rows[0]["ID"].ToString()),
                    TestRoomCode = dt.Rows[0]["TestRoomCode"].ToString(),
                    Status = short.Parse(dt.Rows[0]["Status"].ToString()),
                    DataName = dt.Rows[0]["DataName"].ToString(),
                    TryType = dt.Rows[0]["TryType"].ToString(),
                    ModuleID = new Guid(dt.Rows[0]["ModuleID"].ToString()),
                    BGBH = dt.Rows[0]["BGBH"] == DBNull.Value ? "" : dt.Rows[0]["BGBH"].ToString(),
                    LastEditedTime = dt.Rows[0]["LastEditedTime"] == DBNull.Value ? NullTime : DateTime.Parse(dt.Rows[0]["LastEditedTime"].ToString())
                };
                return doc;
            }
            return null;
        }

        /// <summary>
        /// 更新用户自定义台账列宽度
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="testRoomCode"></param>
        /// <param name="list"></param>
        public void UpdateCustomViewWidth(Guid moduleID, String testRoomCode, Dictionary<JZCustomView, float> list)
        {
            List<JZCustomView> views = GetCustomViewList(moduleID, testRoomCode);
            JZCustomView v = null;
            Boolean finded = false;
            foreach (KeyValuePair<JZCustomView, float> item in list)
            {
                v = item.Key;
                v.ColumnWidth = item.Value;
                foreach (JZCustomView i in views)
                {
                    if (!String.IsNullOrEmpty(i.DocColumn))
                    {
                        if (i.DocColumn == item.Key.DocColumn)
                        {
                            i.ColumnWidth = item.Value;
                            finded = true;
                            break;
                        }
                    }
                    else
                    {
                        if (i.SheetID == item.Key.SheetID && i.CellName == item.Key.CellName)
                        {
                            i.ColumnWidth = item.Value;
                            finded = true;
                            break;
                        }
                    }
                }
            }
            if (!finded && v != null)
            {
                views.Add(v);
            }
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(views).Replace("'", "''");
            SaveCustomView(moduleID, testRoomCode, json);
        }

        public Boolean HasPXAlready(Guid sgDataID)
        {
            String sql = "SELECT a.ID FROM dbo.sys_px_relation a JOIN dbo.sys_document b ON a.PXDataID=b.ID WHERE a.SGDataID='" + sgDataID +
                "' AND b.TestRoomCode='" + Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code + "' ";
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
        public Boolean HasPXRelation(Guid sgDataID)
        {
            String sql = "SELECT ID FROM dbo.sys_px_relation where SGDataID='" + sgDataID + "' ";
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

        public Boolean NewPXDocument(Guid sgDataID)
        {
            Boolean flag = true;
            try
            {
                Sys_Document sgDocBase = GetDocumentBaseInfoByID(sgDataID);
                String json = GetDocumentByID(sgDataID);
                JZDocument sgDoc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(json);
                JZDocument pxDoc = new JZDocument();
                Sys_Document pxDocBase = new Sys_Document();
                pxDocBase.ModuleID = sgDocBase.ModuleID;
                pxDocBase.TestRoomCode = Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code;
                pxDocBase.DataName = "";
                pxDocBase.TryType = "平行";
                pxDoc.ID = Guid.Empty;
                ModuleHelper mh = new ModuleHelper();
                foreach (JZSheet sheet in sgDoc.Sheets)
                {
                    JZSheet pxSheet = new JZSheet();
                    pxSheet.ID = sheet.ID;
                    pxSheet.Name = sheet.Name;
                    pxDoc.Sheets.Add(pxSheet);
                    Sys_Sheet sheetBase = mh.GetSheetItemByID(sheet.ID);
                    List<CellLogic> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CellLogic>>(JZCommonHelper.GZipDecompressString(sheetBase.CellLogic));
                    foreach (JZCell dataCell in sheet.Cells)
                    {
                        JZCell pxCell = new JZCell();
                        pxSheet.Cells.Add(pxCell);
                        pxCell.Name = dataCell.Name;

                        CellLogic cell = GetCellLogic(dataCell.Name, list);
                        if (cell != null)
                        {

                            if (cell.IsPingxing)
                            {
                                pxCell.Value = dataCell.Value;
                            }

                        }

                    }
                }
                Guid newDataID = SaveDocumentDetail(pxDoc, pxDocBase, false);
                flag = Guid.Empty != newDataID;
                if (flag)
                {
                    String sql = String.Format(@"INSERT INTO dbo.sys_px_relation
                                    ( SGDataID, PXDataID, PXTime )
                            VALUES  ( '{0}',
                                      '{1}',
                                      GETDATE()
                                      )", sgDataID, newDataID);
                    int i = ExcuteCommand(sql);
                    flag = i == 1;
                }
            }
            catch (Exception ex)
            {
                flag = false;
                logger.Error("平行资料报错：" + ex.Message);
            }
            return flag;
        }

        public Boolean DeleteDocument(Guid dataID)
        {
            Boolean flag = true;
            String sql = "UPDATE dbo.sys_document SET Status=0 WHERE ID='" + dataID + "';UPDATE dbo.sys_request_change SET IsDelete=1 WHERE DataID='" + dataID + "';UPDATE dbo.sys_invalid_document SET Status=0 WHERE ID='" + dataID + "';  ";
            try
            {
                ExcuteCommand(sql);
                String json = GetDocumentByID(dataID);
                JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(json);
                Sys_Document docBase = GetDocumentBaseInfoByID(dataID);
                sql = "UPDATE dbo.sys_stadium SET F_IsDone=1 WHERE DataID='" + dataID + "'";
                ExcuteCommand(sql);
                LogHelper lh = new LogHelper();
                lh.SaveEditLog(doc, null, "删除", false, docBase);
            }
            catch (Exception e)
            {
                flag = false;
                logger.Error("删除资料失败：" + e.Message);
            }
            return flag;
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

        public Boolean CopyDocument(Guid copyDataID)
        {
            Boolean flag = true;
            try
            {
                Sys_Document sgDocBase = GetDocumentBaseInfoByID(copyDataID);
                String json = GetDocumentByID(copyDataID);
                JZDocument sgDoc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(json);
                JZDocument pxDoc = new JZDocument();
                Sys_Document pxDocBase = new Sys_Document();
                pxDocBase.ModuleID = sgDocBase.ModuleID;
                pxDocBase.TestRoomCode = sgDocBase.TestRoomCode;
                pxDocBase.DataName = "";
                if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_监理单位")
                {
                    pxDocBase.TryType = "抽检";
                }
                else
                {
                    pxDocBase.TryType = "自检";
                }

                pxDoc.ID = Guid.Empty;
                ModuleHelper mh = new ModuleHelper();
                foreach (JZSheet sheet in sgDoc.Sheets)
                {
                    JZSheet pxSheet = new JZSheet();
                    pxSheet.ID = sheet.ID;
                    pxSheet.Name = sheet.Name;
                    pxDoc.Sheets.Add(pxSheet);
                    Sys_Sheet sheetBase = mh.GetSheetItemByID(sheet.ID);
                    List<CellLogic> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CellLogic>>(JZCommonHelper.GZipDecompressString(sheetBase.CellLogic));
                    foreach (JZCell dataCell in sheet.Cells)
                    {
                        JZCell pxCell = new JZCell();
                        pxSheet.Cells.Add(pxCell);
                        pxCell.Name = dataCell.Name;
                        pxCell.Value = dataCell.Value;

                        CellLogic cell = GetCellLogic(dataCell.Name, list);

                        if (cell != null)
                        {
                            if (cell.IsNotCopy)
                            {
                                pxCell.Value = null;
                            }
                        }

                    }
                }
                Guid NewDocID = SaveDocumentDetail(pxDoc, pxDocBase, false);
                CopyDocumentExt(copyDataID, NewDocID);
                flag = Guid.Empty != NewDocID;
            }
            catch (Exception ex)
            {
                flag = false;
                logger.Error("拷贝资料报错：" + ex.ToString() + "; dataid=" + copyDataID);
            }
            return flag;
        }

        public Boolean UpdateDocumentBaseInfo(Sys_Document doc, String updatedField)
        {
            String sql = "";
            if (updatedField == "DataName")
            {
                sql = String.Format("UPDATE dbo.sys_stadium SET DataName='{0}' WHERE DataID='{1}'", doc.DataName.Replace("'", "''").Trim(), doc.ID);
                ExcuteCommand(sql);
                sql = String.Format("UPDATE dbo.sys_operate_log SET DataName='{0}' WHERE dataID='{1}'", doc.DataName.Replace("'", "''").Trim(), doc.ID);
                ExcuteCommand(sql);
                sql = String.Format("UPDATE dbo.sys_document SET DataName='{0}',SetDataNameUser='{1}',SetDataNameTime=GETDATE() WHERE ID='{2}'",
                    doc.DataName.Replace("'", "''").Trim(), Yqun.Common.ContextCache.ApplicationContext.Current.UserName, doc.ID);
            }
            else if (updatedField == "TryType")
            {
                sql = String.Format("UPDATE dbo.sys_document SET TryType='{0}',TryPerson='{1}',TryTime=GETDATE(),TryPersonTestRoomCode='{2}' WHERE ID='{3}'",
                    doc.TryType, Yqun.Common.ContextCache.ApplicationContext.Current.UserName, Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code, doc.ID);
            }
            bool flag = true;
            try
            {
                ExcuteCommand(sql);
            }
            catch (Exception e)
            {
                flag = false;
                logger.Error("更新" + updatedField + "出错： " + e.Message);
            }
            return flag;
        }

        public Boolean SaveCustomView(Guid moduleID, String testRoomCode, String viewJson)
        {
            String sql = String.Format("select ID from dbo.sys_custom_view where ModuleID='{0}' and TestRoomCode='{1}'",
                                moduleID, testRoomCode);
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count == 0)
            {
                sql = String.Format(@"INSERT INTO dbo.sys_custom_view
                                                ( ModuleID ,
                                                  TestRoomCode ,
                                                  CustomView ,
                                                  LastEditedUser ,
                                                  LastEditedTime
                                                )
                                        VALUES  ( '{0}' ,'{1}' ,'{2}' ,'{3}' ,GETDATE())",
                                                     moduleID, testRoomCode, viewJson,
                                                     Yqun.Common.ContextCache.ApplicationContext.Current.UserName);
            }
            else
            {
                sql = String.Format(@"UPDATE dbo.sys_custom_view SET CustomView='{0}',LastEditedUser='{1}',
                            LastEditedTime=GETDATE() WHERE ModuleID='{2}' AND TestRoomCode='{3}'",
                                                 viewJson, Yqun.Common.ContextCache.ApplicationContext.Current.UserName, moduleID, testRoomCode);
            }
            Boolean flag = true;
            try
            {
                ExcuteCommand(sql);
            }
            catch (Exception e)
            {
                flag = false;
                logger.Error("save custom view error: " + e.Message);
            }
            return flag;
        }

        public Guid GetRequestChangeID(Guid documentID)
        {
            String sql = @"SELECT ID FROM dbo.sys_request_change Where State='已提交' AND DataID='" + documentID + "'";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return new Guid(dt.Rows[0][0].ToString());
            }
            else
            {
                return Guid.Empty;
            }
        }
        /// <summary>
        /// 获取未处理的修改请求
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public DataTable GetRequestChangeUnOP(Guid documentID)
        {
            String sql = @"SELECT * FROM dbo.sys_request_change Where State='已提交' AND DataID='" + documentID + "'";
            DataTable dt = GetDataTable(sql);
            return dt;
        }

        public Guid GetLastApprovedRequestChangeID(Guid documentID)
        {
            String sql = @"SELECT ID FROM dbo.sys_request_change Where State='通过' AND DataID='" + documentID + "' order by ApproveTime desc";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return new Guid(dt.Rows[0][0].ToString());
            }
            else
            {
                return Guid.Empty;
            }
        }
        public Boolean NewRequestChange(Sys_RequestChange request)
        {
            string sql = string.Empty;
            if (request.ID == Guid.Empty)
            {
                sql = String.Format(@"INSERT INTO dbo.sys_request_change( ID ,DataID ,TestRoomCode ,ModuleID ,BGBH ,RequestBy ,RequestTime ,Caption ,Reason ,State,IsRequestStadium)SELECT  '{0}' ,'{1}',TestRoomCode,ModuleID,BGBH,'{2}',GETDATE(),'{3}','{4}','已提交',{5} FROM dbo.sys_document WHERE ID='{1}'", Guid.NewGuid(), request.DocumentID, Yqun.Common.ContextCache.ApplicationContext.Current.UserName, request.Caption.Replace("'", "''"), request.Reason.Replace("'", "''"), request.IsRequestStadium);
            }
            else
            {
                sql = String.Format(@"UPDATE dbo.sys_request_change SET Caption='{1}',Reason='{2}',IsRequestStadium={3},RequestTime=getdate() WHERE id='{0}'", request.ID, request.Caption.Replace("'", "''"), request.Reason.Replace("'", "''"), request.IsRequestStadium);
            }
            Boolean flag = true;
            try
            {
                ExcuteCommand(sql);
            }
            catch (Exception ex)
            {
                flag = false;
                logger.Error("" + ex.Message);
            }
            return flag;
        }

        public DataTable GetRequestChangeList(String segment, String company, String testroom, DateTime start, DateTime end, String status, String content, String user)
        {
            String sql = String.Format(@"SELECT a.ID,a.DataID,a.ModuleID,a.State AS 状态,c.标段名称,c.单位名称,c.试验室名称,
                                d.Name AS 模板名称,a.WTBH AS 委托编号,a.BGBH as 报告编号, a.RequestBy AS 申请者,a.RequestTime AS 申请日期,
                                a.Caption AS 内容,
                                a.Reason AS 原因,a.ProcessReason as 处理意见 FROM dbo.sys_request_change a
                                JOIN dbo.v_bs_codeName c ON a.TestRoomCode = c.试验室编码 AND a.IsDelete=0
                                JOIN dbo.sys_module d ON d.ID = a.ModuleID where 
                                a.RequestTime>='{0}' AND a.RequestTime<'{1}'",
               start.ToString("yyyy-MM-dd"),
               end.AddDays(1).ToString("yyyy-MM-dd"));

            if (testroom != "")
            {
                sql += " AND a.TestRoomCode='" + testroom + "' ";
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
                sql += " AND a.TestRoomCode in ('" + String.Join("','", real2.ToArray()) + "') ";
            }
            if (user != "")
            {
                sql += " AND a.RequestBy like '%" + user + "%' ";
            }
            if (content != "")
            {
                sql += " AND a.Caption like '%" + content + "%' ";
            }

            sql += " ORDER BY a.RequestTime DESC";
            //logger.Error(sql);
            DataTable Data = GetDataTable(sql);
            return Data;
        }


        public DataTable GetUnPreocessedRequestList()
        {

            TestRoomCodeHelper trch = new TestRoomCodeHelper();
            List<String> list = trch.GetValidTestRoomCodeList();
            if (list.Count == 0)
            {
                return null;
            }
            String sql = @"SELECT a.ID,a.DataID,a.ModuleID,a.State AS 状态,c.标段名称,c.单位名称,c.试验室名称,
                                d.Name AS 模板名称,a.BGBH as 报告编号, a.RequestBy AS 申请者,a.RequestTime AS 申请日期,
                                a.Caption AS 内容,
                                a.Reason AS 原因,a.ProcessReason as 处理意见 FROM dbo.sys_request_change a
                                JOIN dbo.v_bs_codeName c ON a.TestRoomCode = c.试验室编码 AND a.State='已提交'  AND a.TestRoomCode in ('" + String.Join("','", list.ToArray()) + @"') AND a.IsDelete=0
                                JOIN dbo.sys_module d ON d.ID = a.ModuleID ";
            //sql += " AND b.TestRoomCode in ('" + String.Join("','", list.ToArray()) + "') ";

            sql += " ORDER BY a.RequestTime DESC";
            //logger.Error(sql);
            DataTable Data = GetDataTable(sql);
            return Data;
        }
        public Boolean IsUnique(JZCellProperty property, JZCell cell, Guid sheetID, Guid moduleID,
            String testRoomCode, Guid documentID)
        {

            String sql = String.Format(@"SELECT ID FROM dbo.sys_document WHERE TestRoomCode='{0}' AND Status > 0 AND ID<>'{1}'", testRoomCode, documentID);
            if (property.Description == "委托编号")
            {
                sql += " AND WTBH='" + cell.Value + "'";
            }
            else if (property.Description == "报告编号")
            {
                sql += " AND BGBH='" + cell.Value + "'";
            }
            //else if (property.Description == "记录编号")
            //{
            //    sql += " AND Ext1='" + cell.Value + "'";
            //}
            else
            {
                return true;
            }
            DataTable dt = GetDataTable(sql);
            if (dt == null)
            {
                return true;
            }
            else
            {
                return dt.Rows.Count == 0;
            }
        }
        public DataTable IsUniqueAndReturnDT(JZCellProperty property, JZCell cell, Guid sheetID, Guid moduleID,
            String testRoomCode, Guid documentID)
        {

            String sql = String.Format(@"SELECT Name FROM dbo.sys_module WHERE id IN( SELECT ModuleID FROM dbo.sys_document WHERE TestRoomCode='{0}' AND Status > 0 AND ID<>'{1}' ", testRoomCode, documentID);
            if (property.Description == "委托编号")
            {
                sql += " AND WTBH='" + cell.Value + "')";
            }
            else if (property.Description == "报告编号")
            {
                sql += " AND BGBH='" + cell.Value + "')";
            }
            else
            {
                return null;
            }
            DataTable dt = GetDataTable(sql);
            if (dt == null)
            {
                return null;
            }
            else
            {
                return dt;
            }
        }

        public DataTable GetInvalidDocumentList(String segment, String company, String testroom, String sReportName, String sReportNumber, DateTime Start, DateTime End, String sTestItem)
        {
            DataTable DataList = new DataTable();
            DataList.Columns.Add("ID", typeof(string));
            DataList.Columns.Add("ModuleID", typeof(string));
            DataList.Columns.Add("是否合格", typeof(string));//Added by Tan 20140626
            DataList.Columns.Add("标段", typeof(string));
            DataList.Columns.Add("单位", typeof(string));
            DataList.Columns.Add("试验室", typeof(string));
            DataList.Columns.Add("试验报告", typeof(string));
            DataList.Columns.Add("委托编号", typeof(string));
            DataList.Columns.Add("报告编号", typeof(string));
            DataList.Columns.Add("报告日期", typeof(string));
            DataList.Columns.Add("不合格项目", typeof(string));
            DataList.Columns.Add("标准规定值", typeof(string));
            DataList.Columns.Add("实测值", typeof(string));
            DataList.Columns.Add("原因分析", typeof(string));
            DataList.Columns.Add("监理意见", typeof(string));
            DataList.Columns.Add("处理结果", typeof(string));//Added by Tan 20140309
            DataList.Columns.Add("合格时间", typeof(string));//Added by Tan 20140626
            DataList.Columns.Add("申请人", typeof(string));//Added by Tan 20140630
            DataList.Columns.Add("申请时间", typeof(string));//Added by Tan 20140630
            DataList.Columns.Add("批准人", typeof(string));//Added by Tan 20140630
            DataList.Columns.Add("批准时间", typeof(string));//Added by Tan 20140630

            //141行  增加查询条件 Scdel=0  2013-10-17
            //去掉a.AdditionalQualified=0 AND Modified by Tan In 20140626
            //
            String sql = String.Format(@"SELECT a.ID,a.ModuleID,c.标段名称,c.单位名称,c.试验室名称,
                                            d.Name,a.WTBH,a.BGBH,a.BGRQ,a.F_InvalidItem,a.SGComment,a.JLComment,a.DealResult,a.AdditionalQualified,QualifiedTime,e.RequestBy,e.RequestTime,e.ApprovePerson,e.ApproveTime
                                             FROM dbo.sys_invalid_document a
                                            JOIN dbo.v_bs_codeName c ON a.TestRoomCode = c.试验室编码
                                            JOIN dbo.sys_module d ON a.ModuleID = d.ID
                                            LEFT JOIN dbo.sys_request_change e ON a.ID=e.DataID AND e.ID = (SELECT TOP 1 ID FROM dbo.sys_request_change WHERE  DataID=a.ID ORDER BY RequestTime DESC)
                                            WHERE a.BGRQ>='{0}' AND a.BGRQ<'{1}' and a.Status>0",
                             Start.ToString(), End.AddDays(1).ToString());


            if (sReportName != "")
            {
                sql += " and d.Name like '%" + sReportName + "%' ";
            }

            if (sReportNumber != "")
            {
                sql += " and a.BGBH like '%" + sReportNumber + "%' ";
            }

            if (sTestItem != "")
            {
                sql += " and a.F_InvalidItem like '%" + sTestItem + "%' ";
            }

            if (testroom != "")
            {
                sql += " AND a.TestRoomCode='" + testroom + "' ";
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
                sql += " AND a.TestRoomCode in ('" + String.Join("','", real2.ToArray()) + "') ";
            }

            ApplicationContext AppContext = ApplicationContext.Current;
            if (!AppContext.IsAdministrator)
            {
                sql += " and a.F_InvalidItem NOT LIKE '%#VALUE%' ";
            }
            sql += " order by a.BGRQ desc ";
            DataTable Data = GetDataTable(sql);
            //logger.Error("invalid document list: "+sql);
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    String ID = Row["ID"].ToString();
                    String ModelIndex = Row["ModuleID"].ToString();
                    String SectionName = Row["标段名称"].ToString();
                    String CompanyName = Row["单位名称"].ToString();
                    String TestRoomName = Row["试验室名称"].ToString();
                    String ReportName = Row["Name"].ToString();
                    String WTBH = Row["WTBH"].ToString();
                    String ReportNumber = Row["BGBH"].ToString();
                    String ReportDate = Row["BGRQ"].ToString();
                    String InvalidItem = Row["F_InvalidItem"].ToString();
                    String SGComment = Row["SGComment"].ToString();
                    String JLComment = Row["JLComment"].ToString();
                    String DealResult = Row["DealResult"].ToString();
                    int AdditionalQualified = int.Parse(Row["AdditionalQualified"].ToString());
                    String QualifiedTime = Row["QualifiedTime"] == null ? "" : Row["QualifiedTime"].ToString();
                    String RequestBy = Row["RequestBy"] == null ? "" : Row["RequestBy"].ToString();
                    String RequestTime = Row["RequestTime"] == null ? "" : Row["RequestTime"].ToString();
                    String ApprovePerson = Row["RequestBy"] == null ? "" : Row["ApprovePerson"].ToString();
                    String ApproveTime = Row["ApproveTime"] == null ? "" : Row["ApproveTime"].ToString();
                    String[] Items = InvalidItem.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (String Item in Items)
                    {
                        String[] substrings = Item.Split(',');
                        if (substrings.Length < 3)
                        {
                            continue;
                        }
                        if (sTestItem != "")
                        {
                            if (!substrings[0].Contains(sTestItem))
                            {
                                continue;
                            }
                        }
                        DataRow ItemRow = DataList.NewRow();
                        ItemRow["ID"] = ID;
                        ItemRow["ModuleID"] = ModelIndex;
                        ItemRow["标段"] = SectionName;
                        ItemRow["单位"] = CompanyName;
                        ItemRow["试验室"] = TestRoomName;
                        ItemRow["试验报告"] = ReportName;
                        ItemRow["委托编号"] = WTBH;
                        ItemRow["报告编号"] = ReportNumber;
                        ItemRow["报告日期"] = ReportDate;
                        ItemRow["不合格项目"] = substrings[0];
                        ItemRow["标准规定值"] = substrings[1];
                        ItemRow["实测值"] = substrings[2];
                        ItemRow["原因分析"] = SGComment;
                        ItemRow["监理意见"] = JLComment;
                        ItemRow["处理结果"] = DealResult;
                        if (AdditionalQualified == 1)
                        {
                            ItemRow["是否合格"] = "已合格";
                            ItemRow["合格时间"] = QualifiedTime;
                            ItemRow["申请人"] = RequestBy;
                            ItemRow["申请时间"] = RequestTime;
                            ItemRow["批准人"] = ApprovePerson;
                            ItemRow["批准时间"] = ApproveTime;
                        }
                        else
                        {
                            ItemRow["是否合格"] = "不合格";
                            ItemRow["合格时间"] = "";
                            ItemRow["申请人"] = "";
                            ItemRow["申请时间"] = "";
                            ItemRow["批准人"] = "";
                            ItemRow["批准时间"] = "";
                        }
                        DataList.Rows.Add(ItemRow);
                    }
                }
            }

            return DataList;
        }

        public DataTable GetInvalidProcessInfo(String docID)
        {
            String sql = @"SELECT a.ID,b.TestRoomCode,a.SGComment,a.JLComment,a.DealResult
                                             FROM dbo.sys_invalid_document a
                                            JOIN dbo.sys_document b ON a.ID = b.ID
                                            WHERE a.ID='" + docID + "'";
            return GetDataTable(sql);
        }

        public DataTable GetInvalidImageList(String docID)
        {
            String sql = @"select * from  sys_biz_Image where ImgDataID='" + docID + "'";
            return GetDataTable(sql);
        }

        public DataTable GetUndoInvalidDocumentList(Boolean sg)
        {
            DataTable DataList = new DataTable();
            DataList.Columns.Add("ID", typeof(string));
            DataList.Columns.Add("ModuleID", typeof(string));
            DataList.Columns.Add("标段", typeof(string));
            DataList.Columns.Add("单位", typeof(string));
            DataList.Columns.Add("试验室", typeof(string));
            DataList.Columns.Add("试验报告", typeof(string));
            DataList.Columns.Add("报告编号", typeof(string));
            DataList.Columns.Add("报告日期", typeof(string));
            DataList.Columns.Add("不合格项目", typeof(string));
            DataList.Columns.Add("标准规定值", typeof(string));
            DataList.Columns.Add("实测值", typeof(string));
            DataList.Columns.Add("原因分析", typeof(string));
            DataList.Columns.Add("监理意见", typeof(string));
            DataList.Columns.Add("处理结果", typeof(string));//Added by Tan 20140309

            //141行  增加查询条件 Scdel=0  2013-10-17
            String sql = @"SELECT a.ID,a.ModuleID,c.标段名称,c.单位名称,c.试验室名称,
                                            d.Name,a.BGBH,a.BGRQ,a.F_InvalidItem,a.SGComment,a.JLComment,a.DealResult
                                             FROM dbo.sys_invalid_document a
                                            JOIN dbo.v_bs_codeName c ON a.TestRoomCode = c.试验室编码
                                            JOIN dbo.sys_module d ON a.ModuleID = d.ID
                                            WHERE a.AdditionalQualified=0 AND (a.SGComment='' OR a.SGComment IS NULL 
                                            OR a.DealResult='' OR a.DealResult IS NULL OR a.JLComment='' OR a.JLComment IS NULL) and a.Status>0";
            if (sg)
            {
                sql = @"SELECT a.ID,a.ModuleID,c.标段名称,c.单位名称,c.试验室名称,
                                            d.Name,a.BGBH,a.BGRQ,a.F_InvalidItem,a.SGComment,a.JLComment,a.DealResult
                                             FROM dbo.sys_invalid_document a
                                            JOIN dbo.v_bs_codeName c ON a.TestRoomCode = c.试验室编码
                                            JOIN dbo.sys_module d ON a.ModuleID = d.ID
                                            WHERE a.AdditionalQualified=0 AND (a.SGComment='' OR a.SGComment IS NULL 
                                            OR a.DealResult='' OR a.DealResult IS NULL) and a.Status>0";
            }

            TestRoomCodeHelper trch = new TestRoomCodeHelper();
            List<String> list = trch.GetValidTestRoomCodeList();

            if (list.Count == 0)
            {
                return null;
            }
            sql += " AND a.TestRoomCode in ('" + String.Join("','", list.ToArray()) + "') ";
            //logger.Error("testroomcode.Code=" + Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Code);

            ApplicationContext AppContext = ApplicationContext.Current;
            if (!AppContext.IsAdministrator)
            {
                sql += " and a.F_InvalidItem NOT LIKE '%#%' ";
            }
            DataTable Data = GetDataTable(sql);
            //logger.Error("invalid document list: " + sql);
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    String ID = Row["ID"].ToString();
                    String ModelIndex = Row["ModuleID"].ToString();
                    String SectionName = Row["标段名称"].ToString();
                    String CompanyName = Row["单位名称"].ToString();
                    String TestRoomName = Row["试验室名称"].ToString();
                    String ReportName = Row["Name"].ToString();
                    String ReportNumber = Row["BGBH"].ToString();
                    String ReportDate = Row["BGRQ"].ToString();
                    String InvalidItem = Row["F_InvalidItem"].ToString();
                    String SGComment = Row["SGComment"].ToString();
                    String JLComment = Row["JLComment"].ToString();
                    String DealResult = Row["DealResult"].ToString();

                    String[] Items = InvalidItem.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (String Item in Items)
                    {
                        String[] substrings = Item.Split(',');
                        if (substrings.Length < 3)
                        {
                            continue;
                        }

                        DataRow ItemRow = DataList.NewRow();
                        ItemRow["ID"] = ID;
                        ItemRow["ModuleID"] = ModelIndex;
                        ItemRow["标段"] = SectionName;
                        ItemRow["单位"] = CompanyName;
                        ItemRow["试验室"] = TestRoomName;
                        ItemRow["试验报告"] = ReportName;
                        ItemRow["报告编号"] = ReportNumber;
                        ItemRow["报告日期"] = ReportDate;
                        ItemRow["不合格项目"] = substrings[0];
                        ItemRow["标准规定值"] = substrings[1];
                        ItemRow["实测值"] = substrings[2];
                        ItemRow["原因分析"] = SGComment;
                        ItemRow["监理意见"] = JLComment;
                        ItemRow["处理结果"] = DealResult;
                        DataList.Rows.Add(ItemRow);
                    }
                }
            }

            return DataList;
        }

        public Boolean GeneratePLDTable()
        {
            Boolean flag = true;

            //三级配模板
            Guid moduleID = new Guid("F34C2B8B-DDBE-4C04-BD01-F08B0F479AE8");
            try
            {
                String sql = "SELECT ID, Data,TestRoomCode,BGBH FROM dbo.sys_document WHERE ModuleID='" + moduleID + "' AND Status>0";
                DataTable dt = GetDataTable(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        flag = flag & GenerateOnePLD(dt.Rows[i]["ID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("生成全部配料单表：" + ex.Message);
                flag = false;
            }

            return flag;
        }

        private Boolean GenerateOnePLD(String id)
        {
            Boolean flag = true;
            try
            {
                String sql = "SELECT Data,TestRoomCode,BGBH FROM dbo.sys_document WHERE ID='" + id + "' ";
                DataTable dt = GetDataTable(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dt.Rows[0]["Data"].ToString());
                    String testRoomCode = dt.Rows[0]["TestRoomCode"].ToString();
                    String bgbh = dt.Rows[0]["BGBH"].ToString();
                    if (doc != null)
                    {
                        Guid sheetID = new Guid("0FAF8816-1831-48BF-991A-881AD65B5BB4");

                        String ShuiNi1 = "1";

                        String ChanHeLiao1 = GetDecimalValue(doc, sheetID, "D34");
                        String ChanHeLiao2 = GetDecimalValue(doc, sheetID, "H34");
                        String XiGuLiao = GetDecimalValue(doc, sheetID, "K34");
                        String CuGuLiao1 = GetDecimalValue(doc, sheetID, "N34");
                        String CuGuLiao2 = GetDecimalValue(doc, sheetID, "P34");
                        String CuGuLiao3 = GetDecimalValue(doc, sheetID, "R34");

                        String WaiJiaJi1 = GetDecimalValue(doc, sheetID, "T34");
                        String WaiJiaJi2 = GetDecimalValue(doc, sheetID, "W34");
                        String Shui = GetDecimalValue(doc, sheetID, "AA34");

                        String QiangDuDengJi = GetPLDValue(doc, sheetID, "A10");
                        String TanLuoDu = GetPLDValue(doc, sheetID, "P10");
                        String ProjectName = GetPLDValue(doc, sheetID, "D6");
                        String ShiGongBuWei = GetPLDValue(doc, sheetID, "U6");
                        String ShengChanDate = GetPLDValue(doc, sheetID, "U7");
                        String FangLiang = GetDecimalValue(doc, sheetID, "K36");

                        //可能会取出一些类似于2014-08-12 星期二格式的时间，导致数据库插入失败
                        DateTime time = default(DateTime);
                        if (DateTime.TryParse(ShengChanDate, out time))
                        {
                            ShengChanDate = time.ToString("yy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            ShengChanDate = "NULL";
                        }

                        String delSql = "DELETE FROM dbo.bhz_Mix WHERE TestCode='" + testRoomCode + "' AND PeiBiCode='" + bgbh + "'";
                        ExcuteCommand(delSql);

                        sql = String.Format(@"INSERT INTO dbo.bhz_Mix
                                                            ( PeiBiCode ,
                                                              ShuiNi1 ,
                                                              ShuiNi2 ,
                                                              ChanHeLiao1 ,
                                                              ChanHeLiao2 ,
                                                              XiGuLiao ,
                                                              CuGuLiao1 ,
                                                              CuGuLiao2 ,
                                                              CuGuLiao3 ,
                                                              CuGuLiao4 ,
                                                              WaiJiaJi1 ,
                                                              WaiJiaJi2 ,
                                                              Shui ,
                                                              TestCode ,
                                                              QiangDuDengJi ,
                                                              TanLuoDu ,
                                                              ProjectName ,
                                                              ShiGongBuWei ,
                                                              ShengChanDate ,
                                                              FangLiang
                                                            )
                                                    VALUES  ( '{0}' , 
                                                              {1} ,
                                                              NULL ,
                                                              {2} ,
                                                              {3} , 
                                                              {4} , 
                                                              {5} ,
                                                              {6} ,
                                                              {7} ,
                                                              NULL ,
                                                              {8} ,
                                                              {9} ,
                                                              {10} ,
                                                              {11} , 
                                                              {12} ,
                                                              {13} ,
                                                              {14} ,
                                                              {15} ,
                                                              {16} ,
                                                              {17}) ",
                                          bgbh, ShuiNi1, ChanHeLiao1, ChanHeLiao2, XiGuLiao, CuGuLiao1, CuGuLiao2, CuGuLiao3,
                                          WaiJiaJi1, WaiJiaJi2, Shui, testRoomCode, QiangDuDengJi, TanLuoDu, ProjectName,
                                          ShiGongBuWei, ShengChanDate, FangLiang
                                            );

                        flag = (1 == ExcuteCommand(sql));
                    }
                }
            }
            catch (Exception ex)
            {
                flag = false;
                logger.Error("生成配料单表：" + ex.Message);
            }
            return flag;
        }

        private String GetPLDValue(JZDocument doc, Guid sheetID, String cellName)
        {
            String result = "null";
            try
            {
                Object sn1 = JZCommonHelper.GetCellValue(doc, sheetID, cellName);
                if (sn1 != null && sn1.ToString() != "" && sn1.ToString() != "/")
                {
                    result = "'" + sn1.ToString() + "'";
                }
            }
            catch
            {
            }

            return result;
        }

        private string GetDecimalValue(JZDocument document, Guid sheetID, string cellName)
        {
            var result = default(decimal);
            var value = JZCommonHelper.GetCellValue(document, sheetID, cellName);
            if (value != null)
            {
                decimal.TryParse(value.ToString(), out result);
            }

            return result + "";
        }

        public void SaveInvalidReportNote(String id, String note, int userType)
        {
            bool flag = true;
            StringBuilder sql_select = new StringBuilder();
            note = note.Replace("'", "''");
            if (userType == 0)
            {
                sql_select.Append("update sys_invalid_document set SGComment='" + note + "',LastSGUser='" + ApplicationContext.Current.UserName + "',LastSGTime=getdate()   ");
                sql_select.Append(" where  ID='" + id + "' ");
            }
            else if (userType == 1)
            {
                sql_select.Append("update sys_invalid_document set JLComment='" + note + "',LastJLUser='" + ApplicationContext.Current.UserName + "',LastJLTime=getdate()   ");
                sql_select.Append("where  ID='" + id + "' ");
            }
            else if (userType == 2)
            {
                sql_select.Append("update sys_invalid_document set DealResult='" + note + "',DealUser='" + ApplicationContext.Current.UserName + "',DealTime=getdate()   ");
                sql_select.Append("where  ID='" + id + "' ");
            }
            else
            {
                flag = false;
            }

            if (flag)
            {
                try
                {
                    ExcuteCommand(sql_select.ToString());
                }
                catch (Exception e)
                {
                    logger.Error(e.ToString());
                }
            }
        }

        public String GetInvalidReportNote(String id, int userType)
        {
            bool flag = true;
            string sResult = "";
            StringBuilder sql_select = new StringBuilder();
            if (userType == 0)
            {
                sql_select.Append("select SGComment from sys_invalid_document ");
                sql_select.Append("where  ID='" + id + "' ");
            }
            else if (userType == 1)
            {
                sql_select.Append("select JLComment from sys_invalid_document ");
                sql_select.Append("where  ID='" + id + "' ");
            }
            else if (userType == 2)
            {
                sql_select.Append("select DealResult from sys_invalid_document ");
                sql_select.Append("where  ID='" + id + "' ");
            }
            else
            {
                flag = false;
            }

            if (flag)
            {
                //logger.Error(sql_select.ToString());
                DataTable Data = GetDataTable(sql_select.ToString());
                if (Data != null && Data.Rows.Count > 0)
                {
                    try
                    {
                        sResult = Data.Rows[0][0].ToString();
                    }
                    catch (Exception e)
                    {

                        logger.Error(e.ToString());
                    }
                }
            }
            return sResult;
        }

        /// <summary>
        /// 保存原因分析附件图片
        /// </summary>
        /// <param name="dataID">数据id</param>
        /// <param name="ImgStream">字符流</param>
        /// <returns>保存是否成功</returns>
        public bool SaveImage(DataTable dt)
        {
            try
            {
                if (Update(dt) > 0)
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            finally
            {
            }
            return false;
        }

        /// <summary>
        /// 获取原因分析图片列表
        /// </summary>
        /// <param name="dataID"></param>
        /// <returns></returns>
        public DataTable GetImageTable(string dataID, string ImgRemark)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select * from  sys_biz_Image where ImgDataID='{0}' and (ImgRemark='{1}' or ImgRemark is null)", dataID, ImgRemark);
            DataTable dt = null;
            try
            {
                dt = GetDataTable(strSql.ToString());
            }
            catch (Exception ex)
            {
                logger.Error(strSql.ToString());
                logger.Error(ex.ToString());
            }
            finally
            {
            }
            return dt;
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="ImgID"></param>
        /// <returns></returns>
        public bool DelIamge(string ImgID)
        {
            string slqStr = "delete from sys_biz_Image where ImgID='" + ImgID + "'";
            try
            {
                if (ExcuteCommand(slqStr) > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return false;
        }

        public String SaveInvalidImage(String invalidID, JZFile file, String type)
        {
            String newID = Guid.NewGuid().ToString();
            String sql = "select * from sys_biz_Image where 1<>1";

            try
            {
                DataTable dt = GetDataTable(sql);
                DataRow row = dt.NewRow();
                row["ImgID"] = newID;
                row["ImgDataID"] = invalidID;
                row["ImgName"] = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
                row["ImgContent"] = file.FileData;
                row["ImgRemark"] = type;
                dt.Rows.InsertAt(row, 0);
                if (Update(dt) > 0)
                {
                    return newID;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return "";
        }
        #region 自动保存某个模板下的所有资料，同步不合格报表

        public void AutoSaveAllDocuments()
        {
            String sql = "SELECT ID FROM dbo.sys_module WHERE IsActive=1 and ID='F34C2B8B-DDBE-4C04-BD01-F08B0F479AE8'";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                logger.Error("AutoSaveAllDocuments " + sql);
                ModuleHelper mh = new ModuleHelper();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    logger.Error("auto save " + i);
                    sql = "SELECT ID,Data FROM dbo.sys_document WHERE Status=1 AND ModuleID='" + dt.Rows[i]["ID"].ToString() + "'";
                    DataTable data = GetDataTable(sql);
                    if (data != null && data.Rows.Count > 0)
                    {
                        Sys_Module module = mh.GetModuleBaseInfoByID(new Guid(dt.Rows[i]["ID"].ToString()));
                        for (int j = 0; j < data.Rows.Count; j++)
                        {
                            logger.Error("AutoSaveAllDocuments " + j);
                            JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(data.Rows[j]["Data"].ToString());
                            AutoSaveDocument(doc, module);
                        }


                    }
                }

            }
        }

        private void AutoSaveDocument(JZDocument document, Sys_Module module)
        {
            try
            {
                ModuleHelper mh = new ModuleHelper();
                List<JZFormulaData> CrossSheetLineFormulaInfos = mh.GetLineFormulaByModuleIndex(module.ID);
                Dictionary<Guid, SheetView> SheetCollection = new Dictionary<Guid, SheetView>();
                List<FarPoint.CalcEngine.FunctionInfo> Infos = GetFunctionInfos();
                FpSpread fpSpread = new FpSpread();

                foreach (JZSheet sheet in document.Sheets)
                {
                    String sheetXML = JZCommonHelper.GZipDecompressString(mh.GetSheetXMLByID(sheet.ID));
                    SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), sheetXML, "SheetView") as SheetView;
                    SheetView.Tag = sheet.ID;
                    SheetView.Cells[0, 0].Value = "";
                    SheetView.Protect = true;

                    fpSpread.Sheets.Add(SheetView);

                    SheetCollection.Add(sheet.ID, SheetView);

                    foreach (FarPoint.CalcEngine.FunctionInfo Info in Infos)
                    {
                        SheetView.AddCustomFunction(Info);
                    }
                    foreach (JZCell dataCell in sheet.Cells)
                    {
                        Cell cell = SheetView.Cells[dataCell.Name];

                        if (cell != null)
                        {
                            cell.Value = dataCell.Value;
                        }
                    }
                }

                fpSpread.LoadFormulas(true);
                foreach (JZFormulaData formula in CrossSheetLineFormulaInfos)
                {
                    if (SheetCollection.ContainsKey(formula.SheetIndex))
                    {
                        SheetView Sheet = SheetCollection[formula.SheetIndex];
                        try
                        {
                            Sheet.Cells[formula.RowIndex, formula.ColumnIndex].Formula = formula.Formula;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                        }
                    }
                }
                fpSpread.LoadFormulas(true);

                foreach (JZSheet sheet in document.Sheets)
                {
                    SheetView view = GetSheetViewByID(sheet.ID, fpSpread);
                    if (view == null)
                    {
                        continue;
                    }
                    foreach (JZCell dataCell in sheet.Cells)
                    {
                        Cell cell = view.Cells[dataCell.Name];
                        if (cell != null)
                        {
                            if (String.IsNullOrEmpty(cell.Formula))
                            {
                                continue;
                            }
                            IGetFieldType FieldTypeGetter = cell.CellType as IGetFieldType;
                            if (FieldTypeGetter != null && FieldTypeGetter.FieldType.Description == "图片")
                            {
                                continue;
                            }
                            else if (FieldTypeGetter != null && FieldTypeGetter.FieldType.Description == "数字")
                            {
                                if (cell.Value != null)
                                {
                                    Decimal d;
                                    if (Decimal.TryParse(cell.Value.ToString().Trim(' ', '\r', '\n'), out d))
                                    {
                                        dataCell.Value = d;
                                    }
                                    else
                                    {
                                        dataCell.Value = null;
                                    }
                                }
                            }
                            else
                            {
                                dataCell.Value = cell.Value;
                            }
                            if (dataCell.Value != null && dataCell.Value is String)
                            {
                                dataCell.Value = dataCell.Value.ToString().Trim(' ', '\r', '\n');
                            }
                        }
                    }
                }
                String json = Newtonsoft.Json.JsonConvert.SerializeObject(document).Replace("'", "''");
                String sql = "UPDATE dbo.sys_document SET Data=@Data,NeedUpload=1,LastEditedTime=getdate() WHERE ID=@ID";
                SqlCommand cmd1 = new SqlCommand(sql);
                cmd1.Parameters.Add(new SqlParameter("@ID", document.ID));
                cmd1.Parameters.Add(new SqlParameter("@Data", json));
                if (ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd1 }))
                {
                    DocumentHelper dh = new DocumentHelper();

                    if (dh.ApplyModuleSetting(document, module.ModuleSettings))//给modulesetting定义字段赋值
                    {
                        //验证是否合格，并发短信
                        //QualifyHelper qh = new QualifyHelper();
                        //qh.Qualify(document, module);
                    }
                }
                else
                {
                    logger.Error("未能自动计算公式值，并更新sys_document表：dataID=" + document.ID);
                }
            }
            catch (Exception ex)
            {
                logger.Error("采集自动发送短信报警错误：" + ex.Message);
            }
        }

        private object getInstance(string AssemblyName, string TypeName)
        {
            try
            {
                object ins = null;
                if (File.Exists(AssemblyName))
                {
                    Assembly a = Assembly.LoadFrom(AssemblyName);
                    ins = a.CreateInstance(TypeName);
                }

                return ins;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }
                else
                {
                    throw ex;
                }
            }
        }

        private List<FarPoint.CalcEngine.FunctionInfo> GetFunctionInfos()
        {
            List<FarPoint.CalcEngine.FunctionInfo> FunctionItems = new List<FarPoint.CalcEngine.FunctionInfo>();
            String sql = "select * from sys_biz_FunctionInfos where Scdel=0 ";

            DataTable Data = GetDataTable(sql);
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    try
                    {
                        String PathName = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"),
                            "bin/" + Row["AssemblyName"].ToString());
                        FarPoint.CalcEngine.FunctionInfo Info = getInstance(PathName, Row["FullClassName"].ToString())
                            as FarPoint.CalcEngine.FunctionInfo;
                        if (Info != null)
                            FunctionItems.Add(Info);
                    }
                    catch
                    {
                    }
                }
            }
            return FunctionItems;
        }

        private SheetView GetSheetViewByID(Guid sheetID, FpSpread fpSpread)
        {
            foreach (SheetView view in fpSpread.Sheets)
            {
                if (new Guid(view.Tag.ToString()) == sheetID)
                {
                    return view;
                }
            }
            return null;
        }

        #endregion

        public Boolean UpdateDocumentNeedUpload(string strIDs)
        {
            String sql = string.Format("UPDATE dbo.sys_document SET NeedUpload=0,WillUploadCount=WillUploadCount+1 WHERE id IN({0})", strIDs);
            bool flag = true;
            try
            {
                ExcuteCommand(sql);
            }
            catch (Exception e)
            {
                flag = false;
                logger.Error("更新上传标记出错： " + e.Message);
            }
            return flag;
        }
        public Boolean UpdateGGCDocumentNeedUpload(string strID, int GGCNeedUpload)
        {
            String sql = string.Format("UPDATE dbo.sys_document SET GGCNeedUpload={1} WHERE id='{0}'", strID, GGCNeedUpload);
            bool flag = true;
            try
            {
                //logger.Error("UpdateGGCDocumentNeedUpload sql:" + sql);
                ExcuteCommand(sql);
            }
            catch (Exception e)
            {
                flag = false;
                logger.Error("更新上传标记出错： " + e.Message);
            }
            return flag;
        }
        public Boolean UpdateXCDocumentNeedUpload(string strIDs)
        {
            String sql = string.Format("UPDATE dbo.sys_document SET Status=3 WHERE id IN({0})", strIDs);
            bool flag = true;
            try
            {
                ExcuteCommand(sql);
            }
            catch (Exception e)
            {
                flag = false;
                logger.Error("更新上传标记出错： " + e.Message);
            }
            return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testOverTimeID"></param>
        /// <param name="comment"></param>
        /// <param name="status">1:approvel, 2: disapprovel</param>
        public void ProcessTestOverTime(Guid[] testOverTimeIDs, String comment, Int32 status)
        {
            foreach (var testOverTimeID in testOverTimeIDs)
            {
                String sql = String.Format("UPDATE dbo.sys_test_overtime SET JLComment='{0}', ApprovedJLUser='{1}',ApprovedTime=GETDATE(),Status={3} WHERE ID='{2}'",
                    comment, ApplicationContext.Current.UserName, testOverTimeID, status);
                int result = ExcuteCommand(sql);
                if (result == 1 && status == 1)
                {
                    sql = "SELECT * FROM dbo.sys_test_overtime  WHERE ID='" + testOverTimeID + "'";
                    DataTable dt = GetDataTable(sql);
                    if (dt != null && dt.Rows.Count == 1)
                    {
                        CaiJiHelper cjh = new CaiJiHelper();
                        Guid caiJiID = new Guid(dt.Rows[0]["CaiJiID"].ToString());
                        Guid documentID = new Guid(dt.Rows[0]["DataID"].ToString());
                        Guid ModuleID = new Guid(dt.Rows[0]["ModuleID"].ToString());
                        Guid StadiumID = new Guid(dt.Rows[0]["StadiumID"].ToString());
                        String wtbh = dt.Rows[0]["WTBH"].ToString();
                        String TestRoomCode = dt.Rows[0]["TestRoomCode"].ToString();
                        Int32 SerialNumber = Int32.Parse(dt.Rows[0]["SerialNumber"].ToString());
                        String UserName = dt.Rows[0]["UserName"].ToString();
                        String TestData = dt.Rows[0]["TestData"].ToString();
                        String RealTimeData = dt.Rows[0]["RealTimeData"].ToString();
                        Int32 TotallNumber = Int32.Parse(dt.Rows[0]["TotallNumber"].ToString());
                        String MachineCode = dt.Rows[0]["MachineCode"].ToString();
                        String UploadInfo = dt.Rows[0]["UploadInfo"].ToString();
                        String UploadCode = dt.Rows[0]["UploadCode"].ToString();
                        cjh.ApplyUploadData(caiJiID, documentID, ModuleID, StadiumID, wtbh, TestRoomCode, SerialNumber, UserName, TestData,
                            RealTimeData, TotallNumber, MachineCode, UploadInfo, UploadCode);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testOverTimeID"></param>
        /// <param name="comment"></param>
        /// <param name="status">1:approvel, 2: disapprovel</param>
        public void UpdateTestOverTimeStatus(Guid[] testOverTimeIDs, Int32 status)
        {
            foreach (var testOverTimeID in testOverTimeIDs)
            {
                String sql = String.Format("UPDATE dbo.sys_test_overtime set Status={1} WHERE ID='{0}'",
                    testOverTimeID, status);
                int result = ExcuteCommand(sql);
            }
        }
        public void SubmitCommentMulti(Guid[] ids, String comment, Int32 status)
        {
            var builder = new StringBuilder();
            foreach (var id in ids)
            {
                builder = builder.Append(string.Format("'{0}',", id));
            }
            if (builder.Length > 0)
            {
                builder = builder.Remove(builder.Length - 1, 1);
            }
            builder = builder.Insert(0, "(");
            builder = builder.Append(")");

            String sql = String.Format("UPDATE dbo.sys_test_overtime SET SGComment='{0}', LastSGTime=GETDATE(), LastSGUser='{1}' WHERE ID IN {2}",
    comment, ApplicationContext.Current.UserName, builder.ToString());
            ExcuteCommand(sql);
        }

        public void SubmitComment(Guid testOverTimeID, String comment)
        {
            String sql = String.Format("UPDATE dbo.sys_test_overtime SET SGComment='{0}', LastSGTime=GETDATE(), LastSGUser='{1}' WHERE ID='{2}'",
                comment, ApplicationContext.Current.UserName, testOverTimeID);
            ExcuteCommand(sql);
        }

        public Sys_Page GetTestOverTimeProcessed(int index, int size)
        {
            var helper = new TestRoomCodeHelper();
            var list = helper.GetValidTestRoomCodeList();
            if (list.Count == 0)
            {
                return null;
            }

            var testRoomCode = " AND TestRoomCode in ('" + String.Join("','", list.ToArray()) + "') ";
            var all = 0;
            var orderType = 0;
            var result = new Sys_Page();
        BuildCMD:

            var cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandText = "sp_pager";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@tblname", " (SELECT DataID,MAX(CreatedTime)CreatedTime,TestRoomCode,Status FROM dbo.sys_test_overtime  GROUP BY DataID,TestRoomCode,Status )a "));//dbo.sys_test_overtime a  JOIN dbo.v_bs_codeName c ON a.TestRoomCode=c.试验室编码 JOIN dbo.sys_module d ON a.ModuleID=d.ID LEFT JOIN dbo.sys_stadium s ON s.DataID = a.DataID
            cmd.Parameters.Add(new SqlParameter("@strGetFields", " DataID "));//a.ID,a.ModuleID,a.DataID,a.TestData,c.标段名称,c.单位名称,c.试验室名称,a.UserName AS 试验员,a.CreatedTime AS 实际试验日期,a.SGComment AS 延时原因,d.Name AS 模板名称,a.WTBH,s.EndTime - 1 as 龄期到期日期,a.SerialNumber
            cmd.Parameters.Add(new SqlParameter("@fldName", " a.CreatedTime"));
            cmd.Parameters.Add(new SqlParameter("@PageSize", size));
            cmd.Parameters.Add(new SqlParameter("@PageIndex", index));
            cmd.Parameters.Add(new SqlParameter("@OrderType", orderType));
            cmd.Parameters.Add(new SqlParameter("@strWhere", " AND Status>0 " + testRoomCode + " "));
            cmd.Parameters.Add(new SqlParameter("@doCount", all));

            var table = GetDataFromSP(cmd);

            if (all == 0)
            {
                result.Index = index;
                result.Size = size;
                result.TotalCount = int.MaxValue;
                if (table != null && table.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    StringBuilder strDataIDs = new StringBuilder();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (i == table.Rows.Count - 1)
                        {
                            strDataIDs.AppendFormat("'{0}'", table.Rows[i]["DataID"].ToString());
                        }
                        else
                        {
                            strDataIDs.AppendFormat("'{0}',", table.Rows[i]["DataID"].ToString());
                        }
                    }
                    string strSQL = @"SELECT a.ID,a.ModuleID,a.DataID,a.TestData,c.标段名称,c.单位名称,c.试验室名称,a.UserName AS 试验员,a.CreatedTime AS 实际试验日期,a.SGComment AS 延时原因,d.Name AS 模板名称,a.WTBH,s.EndTime - 1 as 龄期到期日期,a.SerialNumber,a.Status  FROM 
 dbo.sys_test_overtime a  JOIN dbo.v_bs_codeName c ON a.TestRoomCode=c.试验室编码 JOIN dbo.sys_module d ON a.ModuleID=d.ID LEFT JOIN dbo.sys_stadium s ON s.DataID = a.DataID
 WHERE a.Status>0 and a.DataID IN(" + strDataIDs.ToString() + ")";
                    dt = GetDataTable(strSQL);
                    result.Source = dt;
                }
                else
                {
                    result.Source = null;
                }
                all = 1;
                goto BuildCMD;
            }
            else
            {
                if (table.Rows.Count > 0)
                {
                    var row = table.Rows[table.Rows.Count - 1];

                    if (row.ItemArray.Length > 0)
                    {
                        var value = row["Total"];
                        if (value != null)
                        {
                            int total = int.MaxValue;
                            int.TryParse(value.ToString(), out total);

                            result.TotalCount = total;
                        }
                    }
                }
            }


            return result;
        }

        public DataTable GetTestOverTimeData()
        {
            String sql = @"SELECT a.ID,a.ModuleID,a.DataID,a.TestData,c.标段名称,c.单位名称,c.试验室名称,
                                    a.UserName AS 试验员,a.CreatedTime AS 实际试验日期,a.SGComment AS 延时原因,
                                d.Name AS 模板名称,a.WTBH, s.EndTime - 1 as 龄期到期日期,a.SerialNumber,a.TestRoomCode
                                FROM dbo.sys_test_overtime a 
                                JOIN dbo.v_bs_codeName c ON a.TestRoomCode=c.试验室编码
                                JOIN dbo.sys_module d ON a.ModuleID=d.ID
                                LEFT JOIN dbo.sys_stadium s ON s.DataID = a.DataID
                                WHERE a.Status=0 ";

            TestRoomCodeHelper trch = new TestRoomCodeHelper();
            List<String> list = trch.GetValidTestRoomCodeList();

            if (list.Count == 0)
            {
                return null;
            }

            sql += "AND a.TestRoomCode in ('" + String.Join("','", list.ToArray()) + "' ) ";
            if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_监理单位")
            {
                //如果是监理单位，可以看到全部已填的信息
                sql += " AND ((a.SGComment IS NOT NULL AND a.SGComment<>'') OR (a.TestRoomCode='" + Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code + "'))";
            }

            sql += " ORDER BY a.CreatedTime DESC";

            DataTable Data = GetDataTable(sql);
            return Data;
        }

        public DataTable GetTestOverTimeByID(Guid id)
        {
            String sql = "SELECT * FROM dbo.sys_test_overtime WHERE ID='" + id + "'";
            return GetDataTable(sql);
        }

        public DataTable GetTestOverTimeByDataID(Guid dataID)
        {
            string strWhere = " a.DataID = '" + dataID + "' ";
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator == true)
            {
                strWhere += " and a.Status>=0 ";
            }
            else
            {
                strWhere += " and (a.Status=0 or a.Status=2)";
            }
            String sql = @"SELECT a.ID,a.ModuleID,a.DataID,a.TestData,c.标段名称,c.单位名称,c.试验室名称,
                                a.UserName AS 试验员,a.CreatedTime AS 实际试验日期,a.SGComment AS 延时原因,
                            d.Name AS 模板名称,a.WTBH, s.EndTime - 1 as 龄期到期日期,a.SerialNumber
                            FROM dbo.sys_test_overtime a 
                            JOIN dbo.v_bs_codeName c ON a.TestRoomCode=c.试验室编码
                            JOIN dbo.sys_module d ON a.ModuleID=d.ID
                            LEFT JOIN dbo.sys_stadium s ON s.DataID = a.DataID
                            WHERE " + strWhere;

            return GetDataTable(sql);
        }

        /// <summary>
        /// 保存资料温度类型
        /// </summary>
        /// <returns>true成功，false失败</returns>
        public Boolean SaveDocumentTemperatureType(Guid docID, int TemperatureType)
        {
            bool bResult = false;
            try
            {
                String sql = "SELECT ID,TemperatureType FROM dbo.sys_document_ext WHERE ID='" + docID + "'";
                DataTable dt = GetDataTable(sql);
                int TemperatureTypeOld = -1;
                if (dt != null && dt.Rows.Count > 0)
                {//修改
                    TemperatureTypeOld = int.Parse(dt.Rows[0]["TemperatureType"].ToString());

                    sql = string.Format("UPDATE dbo.sys_document_ext SET TemperatureType={1} WHERE id='{0}'", docID, TemperatureType);
                }
                else
                {//添加
                    sql = string.Format("INSERT INTO dbo.sys_document_ext( ID, TemperatureType )VALUES( '{0}',{1})", docID, TemperatureType);
                }
                if (TemperatureType != TemperatureTypeOld)
                {
                    ExcuteCommand(sql);

                    Sys_Document docBase = new Sys_Document();
                    JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(GetDocumentByID(docID));
                    docBase = GetDocumentBaseInfoByID(docID);

                    //龄期提醒
                    StadiumHelper sh = new StadiumHelper();
                    sh.InitStadium(docBase, doc);
                }
                bResult = true;
            }
            catch (Exception ex)
            {
                bResult = false;
                logger.Error("SaveDocumentTemperatureType error:" + ex.ToString());
            }
            return bResult;
        }
        /// <summary>
        /// 获取资料扩展属性
        /// </summary>
        public DataTable GetDocumentExt(Guid docID)
        {
            String sql = "SELECT ID,TemperatureType FROM dbo.sys_document_ext WHERE ID='" + docID + "'";
            return GetDataTable(sql);
        }

        public void DeleteTestOverTime(List<string> ids)
        {
            foreach (var id in ids)
            {
                ExcuteCommand("UPDATE dbo.sys_test_overtime SET Status = -1 WHERE DataID = '" + id + "'");
            }
        }
    }
}
