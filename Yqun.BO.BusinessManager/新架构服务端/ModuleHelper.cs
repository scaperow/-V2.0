using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BizCommon;
using System.Transactions;

namespace Yqun.BO.BusinessManager
{
    public class ModuleHelper : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 通过ID取Sheet的XML内容
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        public String GetSheetXMLByID(Guid sheetIndex)
        {
            try
            {
                String sql = "SELECT SheetXML FROM dbo.sys_sheet WHERE ID=@0";
                DataSet ds = GetDataSet(sql, new Object[] { sheetIndex });
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var xml = ds.Tables[0].Rows[0][0].ToString();
                    var zip = ProcessSheetLikeXML(sheetIndex.ToString(), xml);
                    return zip;
                }
            }
            catch (Exception e)
            {
                logger.Error("GetSheetXMLByID error: " + e.Message + "; sheetID=" + sheetIndex);
                return "";
            }

            return "";
        }
        /// <summary>
        /// 通过ID取Sheet的XML内容
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        public DataTable GetSheetSFormulasByIDs(string strSheeIDs)
        {
            DataTable dt;
            try
            {
                String sql = "SELECT ID,Name,Formulas FROM dbo.sys_sheet WHERE id in(" + strSheeIDs + ")";
                dt = GetDataTable(sql);

            }
            catch (Exception e)
            {
                logger.Error("GetSheetSFormulasByIDs error: " + e.Message + "; strSheeIDs=" + strSheeIDs);
                return null;
            }

            return dt;
        }

        private string ProcessSheetLikeXML(string id, string xml)
        {
            if (IsContainerXml(xml))
            {
                var zip = JZCommonHelper.GZipCompressString(xml);
                ExcuteCommand("UPDATE dbo.sys_sheet SET SheetXML = '" + zip + "' WHERE ID='" + id + "'");
                return zip;
            }

            return xml;
        }


        /// <summary>
        /// 通过ID,Name集合
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        public DataTable GetSheetIDAndName(string ModuleIDs)
        {
            String sql = string.Format(@" SELECT id,Name FROM dbo.sys_sheet WHERE id IN (
 SELECT SheetID FROM dbo.sys_module_sheet
 WHERE ModuleID IN({0})
 )", ModuleIDs);
            DataTable dt = GetDataTable(sql);
            return dt;
        }

        public Sys_Sheet GetSheetItemByID(Guid sheetIndex)
        {
            String sql = "SELECT ID,Name,CatlogCode,SheetXML,SheetData,CellLogic FROM dbo.sys_sheet WHERE ID=@0";
            DataSet ds = GetDataSet(sql, new Object[] { sheetIndex });
            if (ds != null && ds.Tables.Count > 0)
            {
                Sys_Sheet sheet = new Sys_Sheet();
                sheet.ID = new Guid(ds.Tables[0].Rows[0]["ID"].ToString());
                sheet.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                sheet.CatlogCode = ds.Tables[0].Rows[0]["CatlogCode"].ToString();
                var xml = ds.Tables[0].Rows[0]["SheetXML"].ToString();
                var zip = ProcessSheetLikeXML(sheetIndex.ToString(), xml);
                sheet.SheetXML = zip;
                sheet.SheetData = ds.Tables[0].Rows[0]["SheetData"].ToString();
                sheet.CellLogic = ds.Tables[0].Rows[0]["CellLogic"].ToString();
                return sheet;
            }
            return null;
        }

        public Sys_Module GetModuleBaseInfoByID(Guid moduleID)
        {
            String sql = "SELECT ID,Name,ModuleSetting,CatlogCode,Description,QualifySetting,ModuleType,DeviceType,ModuleALT,ReportIndex,UploadSetting,ModuleALTGG,StatisticsMap FROM dbo.sys_module WHERE ID='" + moduleID + "'";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                Sys_Module m = new Sys_Module();
                m.ID = new Guid(dt.Rows[0]["ID"].ToString());
                m.Name = dt.Rows[0]["Name"].ToString();
                m.Description = dt.Rows[0]["Description"].ToString();
                m.CatlogCode = dt.Rows[0]["CatlogCode"].ToString();
                m.ModuleType = Int16.Parse(dt.Rows[0]["ModuleType"].ToString());
                m.DeviceType = Int16.Parse(dt.Rows[0]["DeviceType"].ToString());
                m.ReportIndex = Int32.Parse(dt.Rows[0]["ReportIndex"].ToString());
                m.ModuleALT = dt.Rows[0]["ModuleALT"] == null ? "" : dt.Rows[0]["ModuleALT"].ToString();
                m.QualifySettings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<QualifySetting>>(dt.Rows[0]["QualifySetting"].ToString());
                m.ModuleSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModuleSetting>>(dt.Rows[0]["ModuleSetting"].ToString());
                m.UploadSetting = Newtonsoft.Json.JsonConvert.DeserializeObject<UploadSetting>(dt.Rows[0]["UploadSetting"].ToString());
                m.StatisticsSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StatisticsModuleSetting>>(Convert.ToString(dt.Rows[0]["StatisticsMap"]));
                m.ModuleALTGG = dt.Rows[0]["ModuleALTGG"] == null ? "" : dt.Rows[0]["ModuleALTGG"].ToString();
                return m;
            }
            return null;
        }

        /// <summary>
        /// 保存或修改表单内容
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public Boolean SaveSheet(Sys_Sheet sheet)
        {
            Boolean flag = true;
            try
            {
                String sql = "SELECT ID FROM dbo.sys_sheet WHERE ID=@0";
                DataSet ds = GetDataSet(sql, new Object[] { sheet.ID });
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //update
                    sql = String.Format(@"Update dbo.sys_sheet set Name='{0}', SheetXML='{1}', CellLogic='{5}',Formulas='{6}',
                    SheetData='{2}',LastEditedUser='{3}',LasteditedTime=getdate(),IsActive=1 where ID='{4}'  ",
                      sheet.Name, sheet.SheetXML.Replace("'", "''"), sheet.SheetData,
                      Yqun.Common.ContextCache.ApplicationContext.Current.UserName, sheet.ID, sheet.CellLogic, sheet.Formulas);

                }
                else
                {
                    //insert
                    sql = String.Format(@"INSERT INTO dbo.sys_sheet
                    ( ID ,
                      Name ,
                      CatlogCode ,
                      SheetXML ,
                      SheetData ,
                      CreatedUser ,
                      CreatedTime ,
                      LastEditedUser ,
                      LasteditedTime ,
                      IsActive,
                      CellLogic
                    )
            VALUES  ( '{0}' ,
                      '{1}' ,
                      '{2}' ,
                      '{3}' ,
                      '{4}' ,
                      '{5}' ,
                      getdate(),
                      '{6}' ,
                      getdate() ,
                      1,
                      '{7}'
                    )", sheet.ID, sheet.Name, sheet.CatlogCode, sheet.SheetXML.Replace("'", "''"), sheet.SheetData,
                          Yqun.Common.ContextCache.ApplicationContext.Current.UserName,
                          Yqun.Common.ContextCache.ApplicationContext.Current.UserName, sheet.CellLogic);

                }
                ExcuteCommand(sql);
            }
            catch (Exception ex)
            {
                logger.Error("Save Sheet Style Error: " + ex.Message);
                flag = false;
            }
            return flag;
        }

        public List<JZFormulaData> GetFormulaBySheetIndex(Guid sheetIndex)
        {
            List<JZFormulaData> list = new List<JZFormulaData>();
            String sql = "SELECT * FROM dbo.sys_formulas WHERE IsActive=1 and SheetID='" + sheetIndex + "'";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    JZFormulaData formula = new JZFormulaData();
                    formula.ColumnIndex = Convert.ToInt32(row["ColumnIndex"]);
                    formula.Formula = row["Formula"].ToString();
                    formula.ModelIndex = new Guid(row["ModuleID"].ToString());
                    formula.SheetIndex = new Guid(row["SheetID"].ToString());
                    formula.RowIndex = Convert.ToInt32(row["RowIndex"]);
                    list.Add(formula);
                }
            }
            return list;
        }

        public JZDocument GetDefaultDocument(Guid moduleID)
        {
            String sql = @"SELECT a.ID,b.SheetID,c.SheetData,c.Name FROM dbo.sys_module a
                    JOIN dbo.sys_module_sheet b ON a.ID = b.ModuleID
                    JOIN dbo.sys_sheet c ON b.SheetID = c.ID where a.ID='" + moduleID + "' order by b.SortIndex";

            JZDocument doc = new JZDocument();
            try
            {
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    doc.ID = Guid.Empty;
                    foreach (DataRow row in dt.Rows)
                    {
                        JZSheet sheet = new JZSheet();
                        sheet.ID = new Guid(row["SheetID"].ToString());
                        sheet.Name = row["Name"].ToString();
                        sheet.Cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZCell>>(row["SheetData"].ToString());
                        doc.Sheets.Add(sheet);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return doc;
        }

        public Boolean CopySheet(Guid fromSheetID, Guid newSheetID, String sheetName)
        {
            //logger.Error("CopySheet:" + Yqun.Common.ContextCache.ApplicationContext.Current.UserName);
            String sql = String.Format(@"INSERT INTO dbo.sys_sheet
                        ( ID ,
                          Name ,
                          CatlogCode ,
                          SheetXML ,
                          SheetData ,
                          CreatedUser ,
                          CreatedTime ,
                          LastEditedUser ,
                          LasteditedTime ,
                          IsActive
                        )
                SELECT '{0}' ,
                        '{1}' ,
                        CatlogCode ,
                        SheetXML ,
                        SheetData ,
                        '{2}' ,
                        getdate() ,
                        '{3}' ,
                        getdate(),
                        1 FROM dbo.sys_sheet WHERE ID='{4}'", newSheetID, sheetName,
                         Yqun.Common.ContextCache.ApplicationContext.Current.UserName,
                         Yqun.Common.ContextCache.ApplicationContext.Current.UserName, fromSheetID);
            Boolean flag = true;
            try
            {
                ExcuteCommand(sql);
            }
            catch (Exception ex)
            {
                flag = false;
                logger.Error("copy sheet error: " + ex.Message);
            }
            return flag;
        }

        public Boolean IsSheetUsing(Guid sheetID)
        {
            String sql = "select count(1) from sys_module_sheet where SheetID='" + sheetID + "'";
            int count = int.Parse(ExcuteScalar(sql).ToString());
            return count > 0;
        }

        public Boolean DeleteSheet(Guid sheetID)
        {
            String sql = "update sys_sheet set IsActive=0,LastEditedUser='" +
                Yqun.Common.ContextCache.ApplicationContext.Current.UserName +
                "' ,LasteditedTime=getdate() where ID='" + sheetID + "'";
            Boolean flag = true;
            try
            {
                ExcuteCommand(sql);
            }
            catch (Exception ex)
            {
                flag = false;
                logger.Error("delete sheet error: " + ex.Message);
            }
            return flag;
        }

        public Boolean UpdateSheetName(Guid sheetID, String sheetName)
        {
            String sql = "update sys_sheet set [name]='" + sheetName + "',LastEditedUser='" +
                Yqun.Common.ContextCache.ApplicationContext.Current.UserName +
                "' ,LasteditedTime=getdate() where ID='" + sheetID + "'";
            Boolean flag = true;
            try
            {
                ExcuteCommand(sql);
            }
            catch (Exception ex)
            {
                flag = false;
                logger.Error("update sheet name error: " + ex.Message);
            }
            return flag;
        }

        public DataTable InitSheetTreeByCategory(String key)
        {
            String sql = "SELECT ID,Name FROM dbo.sys_sheet WHERE IsActive=1 AND CatlogCode like '" + key + "%' Order by Name";
            return GetDataTable(sql);
        }

        public DataTable GetSheetCategory()
        {
            String sql = "SELECT ID, CatlogCode ,CatlogName FROM dbo.sys_biz_SheetCatlog WHERE Scdel=0 order by CatlogCode";
            return GetDataTable(sql);
        }

        public DataSet GetSheetCategoryAndSheet()
        {
            String sql1 = "SELECT ID, CatlogCode ,CatlogName FROM dbo.sys_biz_SheetCatlog WHERE Scdel=0 order by CatlogCode";
            String sql2 = "SELECT ID,Name,CatlogCode FROM dbo.sys_sheet WHERE IsActive=1 ";
            return GetDataSet(new String[] { sql1, sql2 });
        }

        public DataSet GetModuleCategoryAndModule()
        {
            String sql1 = "SELECT ID, CatlogCode ,CatlogName FROM dbo.sys_biz_ModuleCatlog WHERE Scdel=0 order by CatlogCode";
            String sql2 = "SELECT ID,Name,CatlogCode FROM dbo.sys_module WHERE IsActive=1 ";
            return GetDataSet(new String[] { sql1, sql2 });
        }

        public List<Sys_Sheet> GetSheetsByModuleID(Guid moduleID)
        {
            String sql = @"SELECT a.ID,a.Name FROM dbo.sys_sheet a
                JOIN dbo.sys_module_sheet b ON a.ID = b.SheetID
                WHERE b.ModuleID='" + moduleID + "' ORDER BY b.SortIndex";
            List<Sys_Sheet> list = new List<Sys_Sheet>();
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Sys_Sheet s = new Sys_Sheet();
                    s.ID = new Guid(row["ID"].ToString());
                    s.Name = row["Name"].ToString();
                    list.Add(s);
                }
            }
            return list;
        }

        public Boolean SaveModule(Guid moduleID, List<Sys_Sheet> sheets, Dictionary<String, JZFormulaData> CrossSheetFormulaCache)
        {
            Boolean flag = true;
            try
            {
                String sql = "DELETE FROM dbo.sys_module_sheet WHERE ModuleID='" + moduleID + "' ";
                Int32 result = 0;
                Boolean noError = true;
                using (TransactionScope ts = new TransactionScope())
                {
                    result = ExcuteCommand(sql);
                    if (result == -1)
                    {
                        noError = false;
                    }
                    sql = @"INSERT INTO dbo.sys_module_sheet
                                    ( ModuleID ,
                                      SheetID ,
                                      SortIndex ,
                                      LastEditedUser ,
                                      LastEditedTime
                                    )
                            VALUES  ( '{0}' ,
                                      '{1}' ,
                                      {2} ,
                                      '{3}' ,
                                      GETDATE())";
                    int i = 1;
                    foreach (Sys_Sheet sheet in sheets)
                    {
                        result = ExcuteCommand(String.Format(sql, moduleID, sheet.ID, i, Yqun.Common.ContextCache.ApplicationContext.Current.UserName));
                        if (result == -1)
                        {
                            noError = false;
                        }
                        i++;
                    }
                    sql = "DELETE FROM dbo.sys_formulas WHERE ModuleID='" + moduleID + "' ";
                    result = ExcuteCommand(sql);
                    if (result == -1)
                    {
                        noError = false;
                    }
                    sql = @"INSERT INTO dbo.sys_formulas
                                    ( ID ,
                                      ModuleID ,
                                      SheetID ,
                                      RowIndex ,
                                      ColumnIndex ,
                                      Formula ,
                                      CreatedUser ,
                                      CreatedTime ,
                                      LastEditedUser ,
                                      LasteditedTime ,
                                      IsActive
                                    )
                            VALUES  ( NEWID() ,
                                      '{0}' ,
                                      '{1}' ,
                                      {2} ,
                                      {3} ,
                                      '{4}' ,
                                      '{5}' ,
                                      GETDATE() ,
                                      '{6}' ,
                                      GETDATE() ,
                                      1
                                    )";

                    foreach (JZFormulaData item in CrossSheetFormulaCache.Values)
                    {
                        result = ExcuteCommand(String.Format(sql, item.ModelIndex, item.SheetIndex, item.RowIndex, item.ColumnIndex,
                            item.Formula.Replace("'", "''"),
                            Yqun.Common.ContextCache.ApplicationContext.Current.UserName,
                            Yqun.Common.ContextCache.ApplicationContext.Current.UserName));
                        if (result == -1)
                        {
                            noError = false;
                        }
                    }

                    if (noError)
                    {
                        ts.Complete();
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("save module error: " + ex.Message);
                flag = false;
            }

            return flag;
        }

        public Boolean SaveLineFormula(Guid moduleID, Dictionary<string, JZFormulaData> CrossSheetFormulaCache)
        {
            IDbConnection DbConnection = GetConntion();
            Yqun.Data.DataBase.Transaction Transaction = new Yqun.Data.DataBase.Transaction(DbConnection);
            Boolean noError = true;
            String sql = "DELETE FROM dbo.sys_line_formulas WHERE ModuleID='" + moduleID + "' ";
            try
            {
                Int32 result = ExcuteCommand(sql);
                if (result == -1)
                {
                    noError = false;
                }
                sql = @"INSERT INTO dbo.sys_line_formulas
                                    ( ID ,
                                      ModuleID ,
                                      SheetID ,
                                      RowIndex ,
                                      ColumnIndex ,
                                      Formula ,
                                      CreatedUser ,
                                      CreatedTime ,
                                      LastEditedUser ,
                                      LasteditedTime ,
                                      IsActive
                                    )
                            VALUES  ( NEWID() ,
                                      '{0}' ,
                                      '{1}' ,
                                      {2} ,
                                      {3} ,
                                      '{4}' ,
                                      '{5}' ,
                                      GETDATE() ,
                                      '{6}' ,
                                      GETDATE() ,
                                      1
                                    )";
                foreach (JZFormulaData item in CrossSheetFormulaCache.Values)
                {
                    result = ExcuteCommand(String.Format(sql, item.ModelIndex, item.SheetIndex, item.RowIndex, item.ColumnIndex,
                        item.Formula.Replace("'", "''"),
                        Yqun.Common.ContextCache.ApplicationContext.Current.UserName,
                        Yqun.Common.ContextCache.ApplicationContext.Current.UserName));
                    if (result == -1)
                    {
                        noError = false;
                    }
                }
                if (noError)
                {
                    Transaction.Commit();
                }
                else
                {
                    Transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                logger.Error("SaveLineFormula error: " + moduleID + ", error content is: " + ex.Message);
            }
            return noError;
        }

        public Boolean IsModuleUsing(Guid moduleID)
        {
            StringBuilder Sql_Have = new StringBuilder();
            // 增加查询条件 Scdel=0      2013-10-17
            Sql_Have.Append("Select Count(ID) From sys_engs_Tree Where Scdel=0 and RalationID = '");
            Sql_Have.Append(moduleID);
            Sql_Have.Append("'");

            object r = ExcuteScalar(Sql_Have.ToString());
            Boolean Result = (Convert.ToInt32(r) != 0);
            return Result;
        }

        public Boolean DeleteModule(Guid moduleID)
        {
            String sql = "update sys_module set IsActive=0,LastEditedUser='" +
                Yqun.Common.ContextCache.ApplicationContext.Current.UserName +
                "' ,LasteditedTime=getdate() where ID='" + moduleID + "'";
            Boolean flag = true;
            try
            {
                ExcuteCommand(sql);
                sql = "DELETE dbo.sys_pxjz_frequency WHERE ModuleID='" + moduleID + "'";
                ExcuteCommand(sql);
            }
            catch (Exception ex)
            {
                flag = false;
                logger.Error("delete module error: " + ex.Message);
            }
            return flag;
        }

        public Boolean UpdateModuleName(Guid moduleID, String moduleName)
        {
            String sql = "update sys_module set [name]='" + moduleName + "',LastEditedUser='" +
                Yqun.Common.ContextCache.ApplicationContext.Current.UserName +
                "' ,LasteditedTime=getdate() where ID='" + moduleID + "'";
            Boolean flag = true;
            try
            {
                ExcuteCommand(sql);
            }
            catch (Exception ex)
            {
                flag = false;
                logger.Error("update module name error: " + ex.Message);
            }
            return flag;
        }

        public Boolean UpdateModuleSetting(Sys_Module module)
        {
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(module.ModuleSettings).Replace("'", "''");
            String qsJson = Newtonsoft.Json.JsonConvert.SerializeObject(module.QualifySettings).Replace("'", "''");
            String sql = "update sys_module set ModuleSetting='" + json + "', QualifySetting='" + qsJson + "',LastEditedUser='" +
                Yqun.Common.ContextCache.ApplicationContext.Current.UserName +
                "' ,LasteditedTime=getdate(), ModuleType=" + module.ModuleType + " where ID='" + module.ID + "'";
            Boolean flag = true;
            try
            {
                ExcuteCommand(sql);
            }
            catch (Exception ex)
            {
                flag = false;
                logger.Error("update module setting error: " + ex.Message);
            }
            return flag;
        }

        public Boolean NewModule(Sys_Module module)
        {
            String sql = @"INSERT INTO dbo.sys_module
                                ( ID ,
                                  Name ,
                                  Description ,
                                  CatlogCode ,
                                  ModuleSetting ,
                                  QualifySetting ,
                                  CreatedUser ,
                                  CreatedTime ,
                                  LastEditedUser ,
                                  LastEditedTime ,
                                  IsActive,
                                  UploadSetting
                                )
                        VALUES  ( '{0}' ,
                                  '{1}' ,
                                  '{2}' ,
                                  '{3}' ,
                                  '{4}' ,
                                  '{5}' ,
                                  '{6}' ,
                                  GETDATE(),
                                  '{7}' ,
                                  GETDATE(),
                                  1,
                                  '{8}'
                                )";
            Boolean flag = true;
            try
            {
                UploadHelper uh = new UploadHelper();
                String json = Newtonsoft.Json.JsonConvert.SerializeObject(module.ModuleSettings).Replace("'", "''");
                String qsJson = Newtonsoft.Json.JsonConvert.SerializeObject(module.QualifySettings).Replace("'", "''");
                sql = String.Format(sql, module.ID, module.Name, module.Description, module.CatlogCode, json, qsJson,
                    Yqun.Common.ContextCache.ApplicationContext.Current.UserName,
                    Yqun.Common.ContextCache.ApplicationContext.Current.UserName,
                    uh.GetDefaultUploadSetting(module.ID.ToString()));
                ExcuteCommand(sql);
                sql = @"INSERT INTO dbo.sys_pxjz_frequency
                            ( ID ,ModuleID ,Frequency ,FrequencyType IsActive )
                    VALUES  ( NEWID() '" + module.ID + "' ,0.0 ,1 ,0)";
                ExcuteCommand(sql);
                sql = @"INSERT INTO dbo.sys_pxjz_frequency
                            ( ID ,ModuleID ,Frequency ,FrequencyType IsActive )
                    VALUES  ( NEWID() '" + module.ID + "' ,0.0 ,2 ,0)";
                ExcuteCommand(sql);
            }
            catch (Exception ex)
            {
                flag = false;
                logger.Error("new module error: " + ex.Message);
            }
            return flag;
        }

        public DataTable InitModuleTreeByCategory(String key)
        {
            String sql = "SELECT ID,Name FROM dbo.sys_module WHERE IsActive=1 AND CatlogCode like '" + key + "%' Order By Name";
            return GetDataTable(sql);
        }

        public DataTable GetModuleCategory()
        {
            String sql = "SELECT ID, CatlogCode ,CatlogName FROM dbo.sys_biz_ModuleCatlog WHERE Scdel=0 order by CatlogCode";
            return GetDataTable(sql);
        }

        public DataTable GetActiveModuleList()
        {
            String sql = "SELECT ID, Name FROM dbo.sys_module WHERE IsActive=1 Order by Name";
            return GetDataTable(sql);
        }

        public String GetModuleConfigByModuleIDAndSerialNumber(Guid moduleID, Int32 serialNumber)
        {
            String sql = String.Format("SELECT Config FROM dbo.sys_module_config WHERE ModuleID='{0}' AND SerialNumber={1} AND IsActive=1",
                moduleID, serialNumber);
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }

        public DataTable GetModuleConfigByModuleID(Guid moduleID)
        {
            String sql = String.Format("SELECT * FROM dbo.sys_module_config WHERE ModuleID='{0}' AND IsActive=1 order by SerialNumber",
                moduleID);
            return GetDataTable(sql);
        }

        public DataTable GetModuleConfigList()
        {
            String sql = @"SELECT DISTINCT b.Name,a.ModuleID,a.IsActive FROM dbo.sys_module_config a 
                JOIN dbo.sys_module b ON a.ModuleID = b.ID AND a.IsActive=1 ORDER BY b.Name";
            return GetDataTable(sql);
        }

        public Boolean DelectModuleConfig(Guid moduleID)
        {
            String sql = String.Format(@"UPDATE dbo.sys_module_config SET IsActive=-1 WHERE ModuleID='{0}'", moduleID);
            int i = ExcuteCommand(sql);
            return i == 1;
        }

        public Boolean SaveModuleConfig(Guid moduleID, int sNumber, String config, Int32 isActive)
        {
            String sql = String.Format(@"INSERT INTO dbo.sys_module_config
                    ( ModuleID ,
                      SerialNumber ,
                      Config ,
                      IsActive
                    )
            VALUES  ( '{0}','{1}' ,'{2}' ,'{3}')", moduleID, sNumber, config, isActive);
            int i = ExcuteCommand(sql);
            return i == 1;
        }

        public DataTable GetModuleConfigItemList(Guid moduleID)
        {
            String sql = String.Format(@"SELECT ID ,SerialNumber ,Config  FROM 
                dbo.sys_module_config WHERE ModuleID='{0}' AND IsActive=1 ORDER BY SerialNumber", moduleID);
            return GetDataTable(sql);
        }

        public Boolean UpdateModuleConfigInfo(Guid moduleID, String json, Boolean isActive, Int32 deviceType)
        {
            List<JZTestConfig> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZTestConfig>>(json);
            Boolean flag = true;
            if (list != null)
            {
                IDbConnection DbConnection = GetConntion();
                Yqun.Data.DataBase.Transaction Transaction = new Yqun.Data.DataBase.Transaction(DbConnection);

                try
                {
                    Int32 active = isActive ? 1 : 0;
                    String sql = String.Format(@"Delete dbo.sys_module_config WHERE ModuleID='{0}' ", moduleID);
                    flag = flag && (ExcuteCommand(sql) == 1);
                    sql = String.Format("UPDATE dbo.sys_module SET DeviceType={0} WHERE ID='{1}'", deviceType, moduleID);
                    flag = flag && (ExcuteCommand(sql) == 1);
                    foreach (var item in list)
                    {
                        String config = Newtonsoft.Json.JsonConvert.SerializeObject(item.Config);
                        flag = flag && SaveModuleConfig(moduleID, item.SerialNumber, config, active);
                    }
                    if (flag)
                    {
                        Transaction.Commit();
                    }
                    else
                    {
                        Transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    logger.Error("update module config error: " + ex.Message);
                }
            }

            return flag;
        }

        public List<JZFormulaData> GetFormulaByModuleIndex(Guid moduleIndex)
        {
            List<JZFormulaData> list = new List<JZFormulaData>();
            String sql = "SELECT * FROM dbo.sys_formulas WHERE IsActive=1 and ModuleID='" + moduleIndex + "'";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    JZFormulaData formula = new JZFormulaData();
                    formula.ColumnIndex = Convert.ToInt32(row["ColumnIndex"]);
                    formula.Formula = row["Formula"].ToString();
                    formula.ModelIndex = new Guid(row["ModuleID"].ToString());
                    formula.SheetIndex = new Guid(row["SheetID"].ToString());
                    formula.RowIndex = Convert.ToInt32(row["RowIndex"]);
                    list.Add(formula);
                }
            }
            return list;
        }

        public List<JZFormulaData> GetLineFormulaByModuleIndex(Guid moduleIndex)
        {
            List<JZFormulaData> list = new List<JZFormulaData>();
            String sql = "exec sp_getFormulas @moduleID='" + moduleIndex + "'";
            DataTable dt = GetDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    JZFormulaData formula = new JZFormulaData();
                    formula.ColumnIndex = Convert.ToInt32(row["ColumnIndex"]);
                    formula.Formula = row["Formula"].ToString();//.Replace("'","")
                    formula.ModelIndex = new Guid(row["ModuleID"].ToString());
                    formula.SheetIndex = new Guid(row["SheetID"].ToString());
                    formula.RowIndex = Convert.ToInt32(row["RowIndex"]);
                    formula.FormulaType = Convert.ToInt32(row["fromLine"]);
                    list.Add(formula);
                }
            }
            return list;
        }

        public DataTable GetWatermarkByModuleID(Guid moduleID)
        {
            String sql = "SELECT * FROM dbo.sys_module_watermark WHERE ModuleID='" + moduleID + "' AND IsActive=1";
            DataTable dt = null;
            try
            {
                dt = GetDataTable(sql);
            }
            catch (Exception e)
            {
                logger.Error("" + e.Message + "; error sql=" + sql);
            }
            return dt;
        }

        public Boolean HasDataByModuleIDAndTestRoomCode(String moduleID, String testRoomCode)
        {
            String sql = "SELECT count(1) FROM dbo.sys_document WHERE Status>0 AND ModuleID='" + moduleID + "' AND TestRoomCode='" + testRoomCode + "' ";
            DataTable dt = GetDataTable(sql);
            Boolean flag = false;
            if (dt != null && dt.Rows.Count > 0)
            {
                flag = Convert.ToInt32(dt.Rows[0][0]) > 0;
            }
            return flag;
        }

        public Boolean RemoveModuleFromTestRoom(String moduleID, String moduleCode)
        {
            String sql = "Update sys_engs_Tree Set Scts_1=Getdate(),Scdel=1 Where NodeCode = '" + moduleCode + "' and RalationID='" + moduleID + "'";
            Boolean flag = (1 == ExcuteCommand(sql));
            return flag;
        }

        public DateTime GetServerDate()
        {
            DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            return now;
        }
        /// <summary>
        /// 获取模板禁止发布的线路ID
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public List<string> GetLineModuleByModuleID(string ModuleID, int IsModule)
        {
            List<string> lstLineIDs = new List<string>();
            try
            {
                string LineID = string.Empty;
                String sql = " SELECT  ID ,LineID ,ModuleID FROM dbo.sys_line_module WHERE ModuleID='" + ModuleID + "' and  IsModule=" + IsModule;
                //logger.Error("GetLineModuleByModuleID sql:" + sql);
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LineID = dt.Rows[i]["LineID"].ToString();
                        lstLineIDs.Add(LineID);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetLineModuleByModuleID error:" + ex.ToString());
            }
            return lstLineIDs;
        }
        /// <summary>
        /// 保存模板禁止发布的线路信息
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="lstLineIDs"></param>
        /// <returns></returns>
        public bool SaveLineModule(string ModuleID, List<string> lstLineIDs, int IsModule)
        {
            bool bSuccess = false;
            try
            {
                String sql = "DELETE FROM dbo.sys_line_module WHERE ModuleID='" + ModuleID + "' and  IsModule=" + IsModule;
                Int32 result = 0;
                Boolean noError = true;
                using (TransactionScope ts = new TransactionScope())
                {
                    result = ExcuteCommand(sql);
                    if (result == -1)
                    {
                        noError = false;
                    }
                    sql = @"INSERT INTO dbo.sys_line_module ( LineID, ModuleID, IsModule )VALUES( '{0}','{1}',{2})";
                    int i = 1;
                    foreach (string LineID in lstLineIDs)
                    {
                        result = ExcuteCommand(String.Format(sql, LineID, ModuleID, IsModule));
                        if (result == -1)
                        {
                            noError = false;
                        }
                        i++;
                    }
                    if (noError)
                    {
                        ts.Complete();
                        bSuccess = true;
                    }
                    else
                    {
                        bSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("SaveLineModule error: " + ex.Message);
                bSuccess = false;
            }

            return bSuccess;

        }

        /// <summary>
        /// 根据模板ID获取被禁止的线路IDs
        /// </summary>
        /// <param name="moduleIDs"></param>
        /// <param name="IsModule"></param>
        /// <returns></returns>
        public DataTable GetForbidLinesByModuleIDs(string moduleIDs, int IsModule)
        {
            String sql = "SELECT LineID,ModuleID FROM dbo.sys_line_module WHERE ModuleID in(" + moduleIDs + ") AND IsModule=" + IsModule;
            DataTable dt = null;
            try
            {
                dt = GetDataTable(sql);
            }
            catch (Exception e)
            {
                logger.Error("" + e.Message + "; error sql=" + sql);
            }
            return dt;
        }

        /// <summary>
        /// 获取表单的单元格样式
        /// </summary>
        /// <param name="moduleIDs"></param>
        /// <param name="IsModule"></param>
        /// <returns></returns>
        public DataTable GetCellStyleBySheetID(Guid SheetID)
        {
            String sql = "SELECT CSID ,SheetID ,CellName ,CellStyle FROM dbo.sys_line_cellstyle WHERE SheetID ='" + SheetID + "' ";
            DataTable dt = null;
            try
            {
                dt = GetDataTable(sql);
            }
            catch (Exception e)
            {
                logger.Error("GetCellStyleBySheetID error:" + e.Message + "; error sql=" + sql);
            }
            return dt;
        }
        /// <summary>
        /// 保存表单的单元格样式
        /// </summary>
        /// <param name="moduleIDs"></param>
        /// <param name="IsModule"></param>
        /// <returns></returns>
        public bool SaveCellStyle(Guid SheetID, string CellName, JZCellStyle CellStyle)
        {
            bool bSuccess = false;
            String sql = "SELECT CSID ,SheetID ,CellName ,CellStyle FROM dbo.sys_line_cellstyle WHERE SheetID ='" + SheetID + "' and CellName='" + CellName + "' ";
            DataTable dt = null;
            try
            {
                dt = GetDataTable(sql);
                if (dt == null)
                {
                    logger.Error("保存单元格样式时出错: " + sql);
                }
                else
                {
                    DataRow row = null;
                    if (dt.Rows.Count == 0)
                    {
                        //new
                        row = dt.NewRow();
                        dt.Rows.Add(row);
                        row["SheetID"] = SheetID;
                        row["CellName"] = CellName;
                    }
                    else
                    {
                        //update
                        row = dt.Rows[0];
                    }
                    row["CellStyle"] = Newtonsoft.Json.JsonConvert.SerializeObject(CellStyle);
                    int r = Update(dt);
                    if (r == 1)
                    {
                        bSuccess = true;
                    }
                    else
                    {
                        bSuccess = false;
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("GetCellStyleBySheetID error:" + e.Message + "; error sql=" + sql);
            }
            return bSuccess;
        }
        /// <summary>
        /// 删除表单的单元格样式
        /// </summary>
        /// <param name="moduleIDs"></param>
        /// <param name="IsModule"></param>
        /// <returns></returns>
        public bool DeleteCellStyle(Guid SheetID, string CellName)
        {
            bool bSuccess = false;
            String sql = "delete FROM dbo.sys_line_cellstyle WHERE SheetID ='" + SheetID + "' and CellName='" + CellName + "' ";
            try
            {
                ExcuteCommand(sql);
                bSuccess = true;
            }
            catch (Exception e)
            {
                logger.Error("DeleteCellStyle error:" + e.Message + "; error sql=" + sql);
            }
            return bSuccess;
        }

        public static bool IsContainerXml(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return false;
            }

            return content.Trim().StartsWith("<?xml");
        }

        public DataSet GetReportIndex(Guid moduleID)
        {
            var set = new DataSet();
            var settingTable = GetDataTable(string.Format(@"SELECT ReportIndex,ReportSheetID,StatisticsCatlog FROM dbo.sys_module WHERE ID='{0}'", moduleID));
            settingTable.TableName = "Setting";

            var catlogTable = GetDataTable("SELECT DISTINCT StatisticsCatlog FROM dbo.sys_module");
            catlogTable.TableName = "Catlog";

            set.Tables.Add(settingTable.Copy());
            set.Tables.Add(catlogTable.Copy());

            return set;
        }

        public Boolean UpdateReportIndex(Guid moduleID, Guid reportSheetID, int reportIndex, string statisticsCatlog)
        {
            String sql = "update sys_module set ReportIndex=" + reportIndex + " , ReportSheetID = '" + reportSheetID + "', StatisticsCatlog = '" + statisticsCatlog + "', LastEditedUser='" +
                Yqun.Common.ContextCache.ApplicationContext.Current.UserName +
                "' ,LasteditedTime=getdate() where ID='" + moduleID + "'";
            Boolean flag = true;
            try
            {
                ExcuteCommand(sql);
            }
            catch (Exception ex)
            {
                flag = false;
                logger.Error("UpdateReportIndex error: " + ex.Message);
            }
            return flag;
        }
    }
}
