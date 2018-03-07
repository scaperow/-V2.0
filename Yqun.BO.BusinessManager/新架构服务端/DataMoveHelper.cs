using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BizCommon;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using FarPoint.Win.Spread;
using FarPoint.Win;
using System.Collections;
using Newtonsoft.Json;
using System.Threading;

namespace Yqun.BO.BusinessManager
{
    public class DataMoveHelper : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// module数据的迁移
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        public void MoveModule()
        {
            //导入sys_module表
            String sql = @"
            INSERT INTO dbo.sys_module
                            ( ID ,
                              Name ,
                              Description ,
                              CatlogCode ,
                              ModuleSetting,
                              CreatedUser ,
                              CreatedTime ,
                              LastEditedUser ,
                              LastEditedTime ,
                              IsActive
                            )
                    SELECT ID ,
                    Description,Description,
                            CatlogCode ,'','developer',SCTS ,'developer',SCTS ,1 FROM dbo.sys_biz_Module
                    WHERE Scdel=0";
            //ExcuteCommand(sql);

            //导入sys_sheet表
            sql = @" INSERT INTO dbo.sys_sheet
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
            SELECT ID ,  Description ,CatlogCode ,SheetStyle ,
                    '' ,'developer',SCTS,'developer',SCTS,1 FROM dbo.sys_biz_Sheet";
            //ExcuteCommand(sql);

            //更新sys_sheet表的SheetData字段
            sql = " SELECT ID FROM dbo.sys_sheet ";
            DataTable dt = GetDataTable(sql);
            ModuleHelper mh = new ModuleHelper();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    String sheetxml = mh.GetSheetXMLByID(new Guid(row[0].ToString()));
                    SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView),
                       sheetxml, "SheetView") as SheetView;
                    SheetView.LoadFormulas(false);
                    List<JZCell> cells = new List<JZCell>();
                    sql = @"SELECT a.Range,b.IsKeyField,b.SCPT,b.COLType FROM dbo.sys_biz_DataArea a
                        JOIN dbo.sys_columns b ON a.TableName = b.TABLENAME AND a.Range = b.DESCRIPTION
                        WHERE a.SheetID='" + row[0].ToString() + "'";
                    DataTable dataAreaTB = GetDataTable(sql);
                    if (dataAreaTB != null)
                    {
                        foreach (DataRow r1 in dataAreaTB.Rows)
                        {
                            JZCell cell = new JZCell();
                            cell.Name = r1["Range"].ToString();
                            cells.Add(cell);

                            Cell c = SheetView.Cells[cell.Name];
                            if (c != null)
                            {
                                JZCellProperty p = new JZCellProperty();
                                p.IsUnique = (r1["IsKeyField"] != DBNull.Value ? Convert.ToBoolean(r1["IsKeyField"]) : false);
                                String scpt = r1["SCPT"].ToString();
                                p.IsNotNull = ((scpt.Length > 0 ? scpt.Substring(0, 1) : "0") == "1" ? true : false);
                                p.IsNotCopy = ((scpt.Length > 1 ? scpt.Substring(1, 1) : "0") == "1" ? true : false);
                                p.IsPingxing = ((scpt.Length > 2 ? scpt.Substring(2, 1) : "0") == "1" ? true : false);
                                p.IsReadOnly = ((scpt.Length > 3 ? scpt.Substring(3, 1) : "0") == "1" ? true : false);
                                //String ColType = r1["COLType"].ToString();
                                //c.CellType = CellTypeFactory.CreateCellType(ColType) as ICellType;
                                c.Tag = p;
                            }
                        }
                    }
                    String xml = Serializer.GetObjectXml(SheetView, "SheetView").Replace("'", "''");
                    String json = Newtonsoft.Json.JsonConvert.SerializeObject(cells);
                    sql = "UPDATE dbo.sys_sheet SET SheetData='" + json + "', SheetXML='" + xml + "' WHERE ID='" + row[0].ToString() + "'";
                    ExcuteCommand(sql);
                }
            }

            //导入sys_module_sheet表数据
            //ExcuteCommand("DELETE dbo.sys_module_sheet;");
            sql = "SELECT ID,Sheets FROM dbo.sys_biz_Module";
            dt = GetDataTable(sql);

            if (dt != null)
            {
                UploadHelper uh = new UploadHelper();
                foreach (DataRow row in dt.Rows)
                {
                    String id = row[0].ToString();
                    String sheets = row[1].ToString();
                    String[] sheetIDs = sheets.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < sheetIDs.Length; i++)
                    {
                        sql = String.Format(@"INSERT INTO dbo.sys_module_sheet
                                ( ModuleID ,
                                  SheetID ,
                                  SortIndex ,
                                  LastEditedUser ,
                                  LastEditedTime
                                )
                        VALUES  ( '{0}' ,
                                  '{1}' , 
                                  {2} , 
                                  'developer' , 
                                  '2013-12-17 01:17:11' 
                                )", id, sheetIDs[i], i + 1);
                        ExcuteCommand(sql);
                    }

                    //更新sys_module中ModuleSetting字段
                    sql = @"SELECT a.ModuleID,a.Description,a.Contents,b.ID AS sheetID FROM dbo.sys_moduleview a 
                        JOIN dbo.sys_biz_Sheet b ON a.TableName=b.DataTable where a.ModuleID='" + id + "'";
                    DataTable dt2 = GetDataTable(sql);
                    List<ModuleSetting> mSettings = new List<ModuleSetting>();
                    if (dt2 != null && dt2.Rows.Count > 0)
                    {
                        foreach (DataRow r2 in dt2.Rows)
                        {
                            ModuleSetting ms = new ModuleSetting();
                            ms.SheetID = new Guid(r2["sheetID"].ToString());
                            String contents = r2["Contents"].ToString();
                            String desc = r2["Description"].ToString();
                            if (contents.StartsWith("col_norm_"))
                            {
                                ms.CellName = contents.Substring(9);
                                ms.Description = desc;
                                switch (desc)
                                {
                                    case "报告编号":
                                        ms.DocColumn = "BGBH";
                                        break;
                                    case "报告日期":
                                        ms.DocColumn = "BGRQ";
                                        break;
                                    case "代表数量":
                                        ms.DocColumn = "ShuLiang";
                                        break;
                                    case "委托编号":
                                        ms.DocColumn = "WTBH";
                                        break;
                                    case "强度等级":
                                        ms.DocColumn = "QDDJ";
                                        break;
                                    default:
                                        ms.DocColumn = "";
                                        break;
                                }
                            }
                            else
                            {
                                continue;
                            }
                            mSettings.Add(ms);
                        }
                    }
                    String js = Newtonsoft.Json.JsonConvert.SerializeObject(mSettings).Replace("'", "''");
                    String us = uh.GetDefaultUploadSetting(id);
                    ExcuteCommand("update dbo.sys_module set ModuleSetting='" + js + "', UploadSetting='" + us + "' where id='" + id + "'");
                }
            }


            //导入sys_formulas表数据
            sql = @"
                DELETE sys_biz_CrossSheetFormulas WHERE SheetIndex NOT IN (SELECT ID FROM dbo.sys_biz_Sheet);
                DELETE sys_biz_CrossSheetFormulas WHERE ModelIndex NOT IN (SELECT ID FROM dbo.sys_biz_Module);
                UPDATE sys_biz_CrossSheetFormulas SET SCTS=GETDATE() WHERE SCTS IS NULL;
                INSERT INTO dbo.sys_formulas
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
                SELECT ID ,
                        ModelIndex ,
                        SheetIndex ,
                        RowIndex ,
                        ColumnIndex ,
                        Formula ,
                        'developer',
                        SCTS ,'developer',    
                        SCTS,
                        (CASE Scdel WHEN 0 THEN 1 ELSE 0 END) FROM dbo.sys_biz_CrossSheetFormulas";
            ExcuteCommand(sql);
        }

        public void MoveDocument()
        {
            //ExcuteCommand("DELETE FROM  dbo.sys_document");

            String sql = "select ID,ModuleSetting from sys_module";

            DataTable dt = GetDataTable(sql);
            if (dt != null)
            {
                int ii = 0;
                foreach (DataRow row in dt.Rows)
                {

                    String dataSql = "select * from dbo.[biz_norm_extent_" + row[0].ToString() + "]";
                    List<ModuleSetting> moduleSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModuleSetting>>(row[1].ToString());
                    DataTable dataDT = null;
                    try
                    {
                        dataDT = GetDataTable(dataSql);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }

                    if (dataDT != null)
                    {
                        foreach (DataRow r1 in dataDT.Rows)
                        {
                            String did = r1["ID"].ToString();
                            String esql = @"select ID from sys_document where ID='" + did + "'";
                            DataTable dtHad = GetDataTable(esql);
                            if (dtHad.Rows.Count == 1)
                            {
                                logger.Error("has data " + did + " " + (ii++).ToString());
                                continue;
                            }
                            String segmentCode = r1["SCPT"].ToString().Substring(0, 8);
                            String companyCode = r1["SCPT"].ToString().Substring(0, 12);
                            String testRoomCode = r1["SCPT"].ToString().Substring(0, 16);
                            String dataName = "";

                            if (dataDT.Columns.Contains("DataName") && r1["DataName"] != DBNull.Value)
                            {
                                dataName = r1["DataName"].ToString();
                            }


                            DateTime scts = DateTime.Parse(r1["SCTS"].ToString());
                            Guid moduleID = new Guid(row[0].ToString());

                            String tryType = "";
                            if (dataDT.Columns.Contains("TryType") && r1["TryType"] != DBNull.Value)
                            {
                                tryType = r1["TryType"].ToString();
                            }

                            JZDocument doc = new JZDocument();
                            doc.ID = new Guid(r1["ID"].ToString());
                            sql = @"SELECT a.ID,b.SheetID,c.DataTable,c.Description FROM dbo.sys_module a
                    JOIN dbo.sys_module_sheet b ON a.ID = b.ModuleID
                    JOIN dbo.sys_biz_sheet c ON b.SheetID = c.ID where a.ID='" + row[0].ToString() + "' order by b.SortIndex";

                            DataTable sheetDT = GetDataTable(sql);
                            if (sheetDT != null)
                            {
                                foreach (DataRow r2 in sheetDT.Rows)
                                {
                                    JZSheet sheet = GetSheetInfo(r2["SheetID"].ToString(), r2["Description"].ToString(),
                                        r2["DataTable"].ToString(), doc.ID.ToString());
                                    if (sheet != null)
                                    {
                                        doc.Sheets.Add(sheet);
                                    }
                                }
                            }
                            try
                            {
                                SaveDocument(doc, moduleSettings, segmentCode, companyCode, testRoomCode, scts, moduleID,
                                dataName, tryType);
                            }
                            catch (Exception exx)
                            {
                                logger.Info("导入失败：dataID=" + doc.ID + "; moduleID=" + moduleID);
                            }

                        }
                    }
                }
            }
        }

        private JZSheet GetSheetInfo(String sheetID, String sheetName, String tableName, String dataID)
        {

            JZSheet sheet = new JZSheet();
            sheet.ID = new Guid(sheetID);
            sheet.Name = sheetName;
            String sql = "select sheetdata from sys_sheet where ID='" + sheetID + "'";
            DataTable dt = GetDataTable(sql);
            List<JZCell> cells = new List<JZCell>();
            if (dt != null)
            {
                cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZCell>>(dt.Rows[0][0].ToString());
            }
            sql = "select * from [" + tableName + "] where ID='" + dataID + "'";
            dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count == 1)
            {
                foreach (JZCell cell in cells)
                {
                    if (dt.Columns.Contains("col_norm_" + cell.Name))
                    {
                        JZCell c = new JZCell();
                        c.Name = cell.Name;
                        if (sheetID == "FB22AB5D-C579-4001-B0B3-F82CE72EA69B" && cell.Name == "L6")
                        {//试验人员头像单元格
                            c.Value = BitmapToString(dt.Rows[0]["col_norm_" + c.Name]);
                        }
                        else
                        {
                            c.Value = dt.Rows[0]["col_norm_" + c.Name];
                        }
                        sheet.Cells.Add(c);
                    }
                }
            }
            return sheet;
        }

        private void SaveDocument(JZDocument doc, List<ModuleSetting> moduleSettings, String segmentCode, String companyCode, String testRoomCode,
            DateTime scts, Guid moduleID, String dataName, String tryType)
        {
            logger.Error("dataID=" + doc.ID + "; moduleID=" + moduleID);

            String sql = @"INSERT INTO dbo.sys_document
        ( ID ,
          SegmentCode ,
          CompanyCode ,
          TestRoomCode ,
          CreatedUser ,
          CreatedTime ,
          ModuleID ,
          Data ,
          DataName ,
          SetDataNameUser ,
          SetDataNameTime ,
          TryType ,
          TryPerson ,
          TryTime ,
          Status ,
          ShuLiang ,
          QDDJ ,
          BGRQ ,
          WTBH ,
          BGBH ,
          Ext1 ,
          Ext2 ,
          Ext3 ,
          Ext4 ,
          Ext5 ,
          Ext6 ,
          Ext7 ,
          Ext8 ,
          Ext9 ,
          Ext10 ,
          Ext11 ,
          Ext12 ,
          Ext13 ,
          Ext14 ,
          Ext15 ,
          Ext16 ,
          Ext17 ,
          Ext18 ,
          Ext19 ,
          Ext20,
          NeedUpload,
          WillUploadCount
        )
VALUES  ( @ID,
          @SegmentCode,
          @CompanyCode,
          @TestRoomCode,
          @CreatedUser,
          @CreatedTime,
          @ModuleID,
          @Data,
          @DataName,
          @SetDataNameUser,
          @SetDataNameTime,
          @TryType,
          @TryPerson,
          @TryTime,
          @Status,
          @ShuLiang,
          @QDDJ,
          @BGRQ,
          @WTBH,
          @BGBH,
          @Ext1,
          @Ext2,
          @Ext3,
          @Ext4,
          @Ext5,
          @Ext6,
          @Ext7,
          @Ext8,
          @Ext9,
          @Ext10,
          @Ext11,
          @Ext12,
          @Ext13,
          @Ext14,
          @Ext15,
          @Ext16,
          @Ext17,
          @Ext18,
          @Ext19,
          @Ext20,
          @NeedUpload,
          @WillUploadCount
        )";
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(doc);
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add(new SqlParameter("@ID", doc.ID));
            cmd.Parameters.Add(new SqlParameter("@SegmentCode", segmentCode));
            cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
            cmd.Parameters.Add(new SqlParameter("@TestRoomCode", testRoomCode));
            cmd.Parameters.Add(new SqlParameter("@CreatedUser", ""));
            cmd.Parameters.Add(new SqlParameter("@CreatedTime", scts));
            cmd.Parameters.Add(new SqlParameter("@ModuleID", moduleID));
            cmd.Parameters.Add(new SqlParameter("@Data", json));
            cmd.Parameters.Add(new SqlParameter("@DataName", dataName));
            cmd.Parameters.Add(new SqlParameter("@SetDataNameUser", ""));
            cmd.Parameters.Add(new SqlParameter("@SetDataNameTime", DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@TryType", tryType));
            cmd.Parameters.Add(new SqlParameter("@TryPerson", ""));
            cmd.Parameters.Add(new SqlParameter("@TryTime", DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@Status", 1));


            if (moduleSettings != null && moduleSettings.Count > 0)
            {
                foreach (ModuleSetting item in moduleSettings)
                {
                    if (String.IsNullOrEmpty(item.CellName) || String.IsNullOrEmpty(item.DocColumn))
                    {
                        continue;
                    }
                    foreach (JZSheet sheet in doc.Sheets)
                    {
                        if (item.SheetID == sheet.ID)
                        {
                            if (!cmd.Parameters.Contains("@" + item.DocColumn))
                            {
                                cmd.Parameters.Add(new SqlParameter("@" + item.DocColumn, GetCellValue(sheet.Cells, item.CellName)));
                            }
                        }
                    }
                }
            }

            if (!cmd.Parameters.Contains("@ShuLiang"))
            {
                cmd.Parameters.Add(new SqlParameter("@ShuLiang", DBNull.Value));
            }
            if (!cmd.Parameters.Contains("@QDDJ"))
            {
                cmd.Parameters.Add(new SqlParameter("@QDDJ", ""));
            }
            if (!cmd.Parameters.Contains("@BGRQ"))
            {
                SqlParameter sp = new SqlParameter("@BGRQ", SqlDbType.DateTime);
                sp.Value = DBNull.Value;
                cmd.Parameters.Add(sp);

            }
            if (!cmd.Parameters.Contains("@WTBH"))
            {
                cmd.Parameters.Add(new SqlParameter("@WTBH", DBNull.Value));
            }
            if (!cmd.Parameters.Contains("@BGBH"))
            {
                cmd.Parameters.Add(new SqlParameter("@BGBH", DBNull.Value));
            }
            if (!cmd.Parameters.Contains("@Ext1"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext1", ""));
            }
            if (!cmd.Parameters.Contains("@Ext2"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext2", ""));
            }
            if (!cmd.Parameters.Contains("@Ext3"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext3", ""));
            }
            if (!cmd.Parameters.Contains("@Ext4"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext4", ""));
            }
            if (!cmd.Parameters.Contains("@Ext5"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext5", ""));
            }
            if (!cmd.Parameters.Contains("@Ext6"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext6", ""));
            }
            if (!cmd.Parameters.Contains("@Ext7"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext7", ""));
            }
            if (!cmd.Parameters.Contains("@Ext8"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext8", ""));
            }
            if (!cmd.Parameters.Contains("@Ext9"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext9", ""));
            }
            if (!cmd.Parameters.Contains("@Ext10"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext10", ""));
            }
            if (!cmd.Parameters.Contains("@Ext11"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext11", ""));
            }
            if (!cmd.Parameters.Contains("@Ext12"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext12", ""));
            }
            if (!cmd.Parameters.Contains("@Ext13"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext13", ""));
            }
            if (!cmd.Parameters.Contains("@Ext14"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext14", ""));
            }
            if (!cmd.Parameters.Contains("@Ext15"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext15", ""));
            }
            if (!cmd.Parameters.Contains("@Ext16"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext16", DBNull.Value));
            }
            if (!cmd.Parameters.Contains("@Ext17"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext17", DBNull.Value));
            }
            if (!cmd.Parameters.Contains("@Ext18"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext18", DBNull.Value));
            }
            if (!cmd.Parameters.Contains("@Ext19"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext19", DBNull.Value));
            }
            if (!cmd.Parameters.Contains("@Ext20"))
            {
                cmd.Parameters.Add(new SqlParameter("@Ext20", DBNull.Value));
            }
            Int32 needUpload = 1;
            Int32 willUploadCount = 1;
            NeedUploadByDataID(doc.ID.ToString(), out needUpload, out willUploadCount);
            if (!cmd.Parameters.Contains("@NeedUpload"))
            {
                cmd.Parameters.Add(new SqlParameter("@NeedUpload", needUpload));
            }
            if (!cmd.Parameters.Contains("@WillUploadCount"))
            {
                cmd.Parameters.Add(new SqlParameter("@WillUploadCount", willUploadCount));
            }
            ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd });
        }

        private void NeedUploadByDataID(String id, out Int32 needUpload, out Int32 willUploadCount)
        {
            needUpload = 1;
            willUploadCount = 1;
            String sql = "select SCRU,SCRC from sys_biz_DataUpload where ID='" + id + "'";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0] != DBNull.Value)
                {
                    needUpload = Convert.ToInt32(dt.Rows[0][0]) == 0 ? 1 : 0;
                }
                else
                {
                    needUpload = 1;
                }
                if (dt.Rows[0][1] != DBNull.Value)
                {
                    willUploadCount = Convert.ToInt32(dt.Rows[0][1]) + 1;
                }
                else
                {
                    willUploadCount = 1;
                }
            }
        }

        private Object GetCellValue(List<JZCell> cells, String cellName)
        {
            foreach (JZCell cell in cells)
            {
                if (cell.Name == cellName)
                {
                    return cell.Value;
                }
            }
            return DBNull.Value;
        }

        private String BitmapToString(Object imageData)
        {
            string bitmapString = null;
            byte[] bitmapBytes = (byte[])imageData;

            bitmapString = System.Convert.ToBase64String(bitmapBytes, Base64FormattingOptions.InsertLineBreaks);

            return bitmapString;
        }

        public void MoveEditLog()
        {
            //迁移修改日志
            for (int i = 0; i < 30; i++)
            {
                String sql = @"SELECT top 10000 ID, dataID ,
                                modifiedby ,
                                modifiedDate ,
                                optType ,
                                modifyItem ,
                                comment,modelIndex,reportName,reportNumber,left(modelcode,16) as testRoomCode FROM dbo.sys_operatelog where flag=0";
                DataTable dt = GetDataTable(sql);
                //logger.Error(sql);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        List<ModifyItem> mItems = null;
                        try
                        {
                            mItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModifyItem>>(row["modifyItem"].ToString());
                        }
                        catch
                        {
                            mItems = null;
                        }

                        List<JZModifyItem> jzList = new List<JZModifyItem>();
                        String json = "";
                        if (mItems != null)
                        {
                            foreach (var mItem in mItems)
                            {
                                JZModifyItem jzMI = new JZModifyItem()
                                {
                                    CellPosition = mItem.CellPosition,
                                    CurrentValue = mItem.CurrentValue,
                                    OriginalValue = mItem.OriginalValue
                                };
                                sql = "SELECT ID FROM dbo.sys_biz_Sheet WHERE DataTable = 'biz_norm_" + mItem.SheetName.Trim('[', ']') + "'";
                                DataTable dt2 = GetDataTable(sql);
                                if (dt2 != null && dt2.Rows.Count > 0)
                                {
                                    jzMI.SheetID = new Guid(dt2.Rows[0][0].ToString());
                                }
                                jzList.Add(jzMI);
                            }

                            json = Newtonsoft.Json.JsonConvert.SerializeObject(jzList).Replace("'", "''");
                        }
                        String comment = row["comment"].ToString();
                        String requestID = Guid.Empty.ToString();
                        if (comment.Length == 36)
                        {
                            requestID = comment;
                        }
                        sql = String.Format(@"INSERT INTO dbo.sys_operate_log
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
                                    VALUES  ( '{0}' ,'{1}','{2}','{3}' ,'{4}','{5}','{6}','{7}','{8}','{9}')",
                                        row["dataID"], requestID, row["modifiedby"], row["modifiedDate"], row["optType"], json,
                                        row["modelIndex"], row["testRoomCode"], row["reportNumber"],
                                        row["reportName"].ToString().Replace("'", "''"));
                        ExcuteCommand(sql);
                        ExcuteCommand("update sys_operatelog set flag=1 where ID=" + row["ID"]);
                    }
                }
            }
        }

        public void MoveModifyChange()
        {
            //迁移申请修改
            String sql = @"INSERT INTO dbo.sys_request_change
                                                ( ID ,
                                                  DataID ,
                                                  RequestBy ,
                                                  RequestTime ,
                                                  Caption ,
                                                  Reason ,
                                                  ApprovePerson ,
                                                  ApproveTime ,
                                                  ProcessReason ,
                                                  State
                                                )

                                SELECT ID ,
                                DataID ,
                                SponsorPerson ,
                                SponsorDate ,
                                Caption ,
                                Reason ,
                                ApprovePerson ,
                                ApproveDate ,
                                ProcessReason ,
                                State FROM dbo.sys_biz_DataModification";

            ExcuteCommand(sql);
        }

        public void MoveStadiumData()
        {
            //迁移龄期设置
            String sql = "SELECT ID ,Scts ,SearchRange ,StadiumConfig  FROM dbo.sys_biz_reminder_stadiumInfo";
            //            DataTable dt = GetDataTable(sql);
            //            if (dt != null)
            //            {
            //                foreach (DataRow row in dt.Rows)
            //                {
            //                    StadiumConfig oldC = Newtonsoft.Json.JsonConvert.DeserializeObject<StadiumConfig>(row["StadiumConfig"].ToString());
            //                    JZStadiumConfig config = new JZStadiumConfig();
            //                    config.fZJRQ = GetQSFromStadiumCloumn(oldC.fZJRQ);
            //                    config.fWTBH = GetQSFromStadiumCloumn(oldC.fWTBH);
            //                    config.fSJSize = GetQSFromStadiumCloumn(oldC.fSJSize);
            //                    config.fSJBH = GetQSFromStadiumCloumn(oldC.fSJBH);
            //                    config.fPH = GetQSFromStadiumCloumn(oldC.fPH);
            //                    config.fBGBH = GetQSFromStadiumCloumn(oldC.fBGBH);
            //                    config.fAdded = GetQSFromStadiumCloumn(oldC.fAdded);
            //                    config.DayList = new List<JZStadiumDay>();
            //                    if (oldC.DayList != null)
            //                    {
            //                        foreach (StadiumDay day in oldC.DayList)
            //                        {
            //                            JZStadiumDay d = new JZStadiumDay();
            //                            d.Days = day.Days;
            //                            d.IsParameterDays = day.IsParameterDays;
            //                            d.ItemID = day.ItemID;
            //                            d.ItemName = day.ItemName;
            //                            d.PDays = GetQSFromStadiumCloumn(day.PDays);
            //                            d.ValidInfo = GetQSFromStadiumCloumn(day.ValidInfo);
            //                            config.DayList.Add(d);
            //                        }
            //                    }
            //                    String srange = row["SearchRange"].ToString();
            //                    String[] arr = srange.Split(',');
            //                    foreach (var item in arr)
            //                    {
            //                        sql = String.Format(@"INSERT INTO dbo.sys_stadium_config_days ( ModuleID, Days )
            //                                    VALUES  ( '{0}',{1} )", row["ID"], item);
            //                        ExcuteCommand(sql);
            //                    }
            //                    String json = Newtonsoft.Json.JsonConvert.SerializeObject(config).Replace("'", "''");
            //                    sql = String.Format(@"INSERT INTO dbo.sys_stadium_config
            //                                                    ( ID ,
            //                                                      StadiumConfig ,
            //                                                      LastEditedUser ,
            //                                                      LastEditedTime ,
            //                                                      IsActive
            //                                                    )
            //                                            VALUES  ( '{0}' ,'{1}','developer' ,'{2}' ,1)", row["ID"], json, row["Scts"]);
            //                    ExcuteCommand(sql);
            //                }
            //            }

            //迁移龄期数据
            sql = @"INSERT INTO dbo.sys_stadium
                            ( ID ,
                              DataID ,
                              LastUpdatedTime ,
                              DateSpan ,
                              F_ItemId ,
                              F_PH ,
                              F_ZJRQ ,
                              F_SJBH ,
                              F_SJSize ,
                              F_SYXM ,
                              F_BGBH ,
                              F_WTBH ,
                              F_Added ,
                              F_IsDone ,
                              F_ItemIndex ,
                              SGComment ,
                              LastSGUser ,
                              LastSGTime ,
                              JLComment ,
                              LastJLUser ,
                              LastJLTime,
                              TestRoomCode,
                              ModuleID,
                              DataName
                            )
                    SELECT NEWID() ,
                            DataID ,
                            Scts ,
                            DateSpan ,
                            F_ItemId ,
                            F_PH ,
                            F_ZJRQ ,
                            F_SJBH ,
                            F_SJSize ,
                            F_SYXM ,
                            F_BGBH ,
                            F_WTBH ,
                            F_Added ,
                            ISNULL(F_IsDone,0) ,
                            ISNULL(F_ItemIndex,0) ,
                            SGComment ,
                            LastSGUser ,
                            LastSGTime ,
                            JLComment ,
                            LastJLUser ,
                            LastJLTime,
                            LEFT(ModelCode,16),ModelIndex,isnull(F_Name,'') FROM dbo.sys_biz_reminder_stadiumData WHERE DataID IN (SELECT ID FROM dbo.sys_document)";
            ExcuteCommand(sql);
        }

        private QualifySetting GetQSFromStadiumCloumn(StadiumColumnInfo info)
        {
            if (info != null)
            {
                QualifySetting qs = new QualifySetting();
                qs.CellName = info.ColumnName.Replace("col_norm_", "");
                String sql = "SELECT ID FROM dbo.sys_biz_Sheet WHERE DataTable = '" + info.TableName.Trim('[', ']') + "'";
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    qs.SheetID = new Guid(dt.Rows[0][0].ToString());
                }
                return qs;
            }
            return null;
        }

        public void MoveInvalidDocument()
        {
            //迁移不合格报告
            String sql = @"INSERT INTO dbo.sys_invalid_document
                                    ( ID ,
          ModuleID ,
          TestRoomCode ,
          BGBH ,
          BGRQ ,
          Status,
                                      LastEditedTime ,
                                      F_InvalidItem ,
                                      SentCount ,
                                      LastSentStatus ,
                                      LastSentTime ,
                                      AdditionalQualified ,
                                      QualifiedTime ,
                                      SGComment ,
                                      LastSGUser ,
                                      LastSGTime ,
                                      JLComment ,
                                      LastJLUser ,
                                      LastJLTime,
                                      DealResult,
                                      DealUser,
                                      DealTime
                                    )
                            SELECT ID ,
          ModelIndex ,
          LEFT(ModelCode,16) ,
          ReportNumber ,
          ReportDate ,
          1,
                                    Scts ,
                                    F_InvalidItem ,
                                    SentCount ,
                                    LastSentStatus ,
                                    LastSentTime ,
                                    AdditionalQualified ,
                                    QualifiedTime ,
                                    SGComment ,
                                    LastSGUser ,
                                    LastSGTime ,
                                    JLComment ,
                                    LastJLUser ,
                                    LastJLTime ,
                                    DealResult,
                                    DealUser,
                                    DealTime FROM dbo.sys_biz_reminder_evaluateData";
            ExcuteCommand(sql);
            MovePXData();
        }

        public void MovePXData()
        {
            String sql = @"INSERT INTO dbo.sys_px_relation
                                ( SGDataID, PXDataID, PXTime )
                        SELECT  SGDataID ,
                                PXDataID ,
                                PXTime FROM dbo.biz_px_relation";
            ExcuteCommand(sql);
        }

        private Boolean HasSheetByID(String sheetID)
        {
            String sql = "SELECT ID FROM dbo.sys_sheet WHERE ID='" + sheetID + "'";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public void MoveModuleByID(String moduleID)
        {
            //导入sys_module表
            //ExcuteCommand("DELETE FROM dbo.sys_module");
            //ExcuteCommand("DELETE FROM dbo.sys_sheet");
            String sql = @"
            INSERT INTO dbo.sys_module
                            ( ID ,
                              Name ,
                              Description ,
                              CatlogCode ,
                              ModuleSetting,
                              CreatedUser ,
                              CreatedTime ,
                              LastEditedUser ,
                              LastEditedTime ,
                              IsActive
                            )
                    SELECT ID ,
                    Description,Description,
                            CatlogCode ,'','developer',SCTS ,'developer',SCTS ,1 FROM dbo.sys_biz_Module
                    WHERE ID='" + moduleID + "'";
            int resule = ExcuteCommand(sql);
            if (resule == 1)
            {
                sql = "SELECT Sheets FROM dbo.sys_biz_Module where ID='" + moduleID + "'";
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count == 1)
                {
                    String[] arr = dt.Rows[0]["Sheets"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length > 0)
                    {
                        List<String> sidL = new List<String>();

                        for (int k = 0; k < arr.Length; k++)
                        {
                            if (!HasSheetByID(arr[k]))
                            {
                                sidL.Add(arr[k]);
                            }
                        }
                        String sheetIDList = "'" + String.Join("','", sidL.ToArray()) + "'";

                        sql = @" INSERT INTO dbo.sys_sheet
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
                        SELECT ID ,  Description ,CatlogCode ,SheetStyle ,
                                '' ,'developer',SCTS,'developer',SCTS,1 FROM dbo.sys_biz_Sheet 
                        Where ID in (" + sheetIDList + ")";
                        logger.Error(sql);
                        if (ExcuteCommand(sql) > 0)
                        {
                            //更新sys_sheet表的SheetData字段
                            sql = @" SELECT ID FROM dbo.sys_biz_Sheet WHERE ID IN 
                                (" + sheetIDList + ")";
                            dt = GetDataTable(sql);
                            ModuleHelper mh = new ModuleHelper();

                            if (dt != null)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    String sheetxml = mh.GetSheetXMLByID(new Guid(row[0].ToString()));
                                    SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView),
                                       sheetxml, "SheetView") as SheetView;
                                    SheetView.LoadFormulas(false);
                                    List<JZCell> cells = new List<JZCell>();
                                    sql = @"SELECT a.Range,b.IsKeyField,b.SCPT,b.COLType FROM dbo.sys_biz_DataArea a
                        JOIN dbo.sys_columns b ON a.TableName = b.TABLENAME AND a.Range = b.DESCRIPTION
                        WHERE a.SheetID='" + row[0].ToString() + "'";
                                    DataTable dataAreaTB = GetDataTable(sql);
                                    if (dataAreaTB != null)
                                    {
                                        foreach (DataRow r1 in dataAreaTB.Rows)
                                        {
                                            JZCell cell = new JZCell();
                                            cell.Name = r1["Range"].ToString();
                                            cells.Add(cell);

                                            Cell c = SheetView.Cells[cell.Name];
                                            if (c != null)
                                            {
                                                JZCellProperty p = new JZCellProperty();
                                                p.IsUnique = (r1["IsKeyField"] != DBNull.Value ? Convert.ToBoolean(r1["IsKeyField"]) : false);
                                                String scpt = r1["SCPT"].ToString();
                                                p.IsNotNull = ((scpt.Length > 0 ? scpt.Substring(0, 1) : "0") == "1" ? true : false);
                                                p.IsNotCopy = ((scpt.Length > 1 ? scpt.Substring(1, 1) : "0") == "1" ? true : false);
                                                p.IsPingxing = ((scpt.Length > 2 ? scpt.Substring(2, 1) : "0") == "1" ? true : false);
                                                p.IsReadOnly = ((scpt.Length > 3 ? scpt.Substring(3, 1) : "0") == "1" ? true : false);
                                                //String ColType = r1["COLType"].ToString();
                                                //c.CellType = CellTypeFactory.CreateCellType(ColType) as ICellType;
                                                c.Tag = p;
                                            }
                                        }
                                    }
                                    String xml = Serializer.GetObjectXml(SheetView, "SheetView").Replace("'", "''");
                                    String json = Newtonsoft.Json.JsonConvert.SerializeObject(cells);
                                    sql = "UPDATE dbo.sys_sheet SET SheetData='" + json + "', SheetXML='" + xml + "' WHERE ID='" + row[0].ToString() + "'";
                                    ExcuteCommand(sql);
                                }
                            }

                            //导入sys_module_sheet表数据
                            //ExcuteCommand("DELETE dbo.sys_module_sheet;");
                            sql = "SELECT ID,Sheets FROM dbo.sys_biz_Module Where ID='" + moduleID + "'";
                            dt = GetDataTable(sql);

                            if (dt != null)
                            {
                                UploadHelper uh = new UploadHelper();
                                foreach (DataRow row in dt.Rows)
                                {
                                    String id = row[0].ToString();
                                    String sheets = row[1].ToString();
                                    String[] sheetIDs = sheets.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                    for (int i = 0; i < sheetIDs.Length; i++)
                                    {
                                        sql = String.Format(@"INSERT INTO dbo.sys_module_sheet
                                                    ( ModuleID ,
                                                      SheetID ,
                                                      SortIndex ,
                                                      LastEditedUser ,
                                                      LastEditedTime
                                                    )
                                            VALUES  ( '{0}' ,
                                                      '{1}' , 
                                                      {2} , 
                                                      'developer' , 
                                                      '2013-12-17 01:17:11' 
                                                    )", id, sheetIDs[i], i + 1);
                                        ExcuteCommand(sql);
                                    }

                                    //更新sys_module中ModuleSetting字段
                                    sql = @"SELECT a.ModuleID,a.Description,a.Contents,b.ID AS sheetID FROM dbo.sys_moduleview a 
                        JOIN dbo.sys_biz_Sheet b ON a.TableName=b.DataTable where a.ModuleID='" + id + "'";
                                    DataTable dt2 = GetDataTable(sql);
                                    List<ModuleSetting> mSettings = new List<ModuleSetting>();
                                    if (dt2 != null && dt2.Rows.Count > 0)
                                    {
                                        foreach (DataRow r2 in dt2.Rows)
                                        {
                                            ModuleSetting ms = new ModuleSetting();
                                            ms.SheetID = new Guid(r2["sheetID"].ToString());
                                            String contents = r2["Contents"].ToString();
                                            String desc = r2["Description"].ToString();
                                            if (contents.StartsWith("col_norm_"))
                                            {
                                                ms.CellName = contents.Substring(9);
                                                ms.Description = desc;
                                                switch (desc)
                                                {
                                                    case "报告编号":
                                                        ms.DocColumn = "BGBH";
                                                        break;
                                                    case "报告日期":
                                                        ms.DocColumn = "BGRQ";
                                                        break;
                                                    case "代表数量":
                                                        ms.DocColumn = "ShuLiang";
                                                        break;
                                                    case "委托编号":
                                                        ms.DocColumn = "WTBH";
                                                        break;
                                                    case "强度等级":
                                                        ms.DocColumn = "QDDJ";
                                                        break;
                                                    default:
                                                        ms.DocColumn = "";
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                            mSettings.Add(ms);
                                        }
                                    }
                                    String js = Newtonsoft.Json.JsonConvert.SerializeObject(mSettings).Replace("'", "''");
                                    String us = uh.GetDefaultUploadSetting(id);
                                    ExcuteCommand("update dbo.sys_module set ModuleSetting='" + js + "', UploadSetting='" + us + "' where id='" + id + "'");
                                }
                            }


                            //导入sys_formulas表数据
                            sql = @"
                INSERT INTO dbo.sys_formulas
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
                SELECT ID ,
                        ModelIndex ,
                        SheetIndex ,
                        RowIndex ,
                        ColumnIndex ,
                        Formula ,
                        'developer',
                        SCTS ,'developer',    
                        SCTS,
                        (CASE Scdel WHEN 0 THEN 1 ELSE 0 END) FROM dbo.sys_biz_CrossSheetFormulas Where ModelIndex='" + moduleID + "'";
                            ExcuteCommand(sql);

                        }
                    }
                }
            }
        }

        public void MoveDocumentByModuleID2(String mid)
        {
            String sql = "select ID,ModuleSetting from sys_module where id='" + mid + "'";
            logger.Error("sql=" + sql);

            DataTable dt = GetDataTable(sql);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {

                    String dataSql = "select * from dbo.[biz_norm_extent_" + row[0].ToString() + "]  ";// where  SCPT = ''
                    List<ModuleSetting> moduleSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModuleSetting>>(row[1].ToString());
                    DataTable dataDT = GetDataTable(dataSql);
                    logger.Error("dataSql=" + dataSql);
                    if (dataDT != null)
                    {
                        foreach (DataRow r1 in dataDT.Rows)
                        {
                            String segmentCode = r1["SCPT"].ToString().Substring(0, 8);
                            String companyCode = r1["SCPT"].ToString().Substring(0, 12);
                            String testRoomCode = r1["SCPT"].ToString().Substring(0, 16);
                            String dataName = "";

                            if (dataDT.Columns.Contains("DataName") && r1["DataName"] != DBNull.Value)
                            {
                                dataName = r1["DataName"].ToString();
                            }


                            DateTime scts = DateTime.Parse(r1["SCTS"].ToString());
                            Guid moduleID = new Guid(row[0].ToString());

                            String tryType = "";
                            if (dataDT.Columns.Contains("TryType") && r1["TryType"] != DBNull.Value)
                            {
                                tryType = r1["TryType"].ToString();
                            }

                            JZDocument doc = new JZDocument();
                            doc.ID = new Guid(r1["ID"].ToString());
                            sql = @"SELECT a.ID,b.SheetID,c.DataTable,c.Description FROM dbo.sys_module a
                    JOIN dbo.sys_module_sheet b ON a.ID = b.ModuleID
                    JOIN dbo.sys_biz_Sheet c ON b.SheetID = c.ID where a.ID='" + row[0].ToString() + "' order by b.SortIndex";

                            DataTable sheetDT = GetDataTable(sql);
                            if (sheetDT != null)
                            {
                                foreach (DataRow r2 in sheetDT.Rows)
                                {
                                    JZSheet sheet = GetSheetInfo(r2["SheetID"].ToString(), r2["Description"].ToString(),
                                        r2["DataTable"].ToString(), doc.ID.ToString());
                                    if (sheet != null)
                                    {
                                        doc.Sheets.Add(sheet);
                                    }
                                }
                            }
                            sql = @"select ID from sys_document where ID='" + doc.ID + "'";
                            DataTable dtHad = GetDataTable(sql);
                            logger.Error("dt.Rows.Count=" + dt.Rows.Count + "sql=" + sql);
                            if (dtHad.Rows.Count == 0)
                            {

                                SaveDocument(doc, moduleSettings, segmentCode, companyCode, testRoomCode, scts, moduleID,
                                    dataName, tryType);
                            }
                        }
                    }
                }
            }
        }
        #region 自定义Move方法
        /// <summary>
        /// 添加平行关系
        /// </summary>
        /// <param name="mid"></param>
        private void AddPXRelation(string mid)
        {
            string strBHLists = "BL-JL4-XGL1306002-1,BL-XGL1305002|BL-JL4-XGL1306004-1,BL-XGL1304001|BL-JL4-XGL1306005-1,BL-XGL1305020|BL-JL4-XGL1306007-1,BL-XGL1306015|BL-JL4-XGL1307004-1,BL-XGL1306010|BL-JL4-XGL1308007-1,BL-XGL1307018|BL-JL4-XGL1308008-1,BL-XGL1308001|BL-JL4-XGL1308009-1,BL-XGL1308016|BL-JL4-XGL1308011-1,BL-XGL1308004|BL-JL4-XGL1308013-1,BL-XGL1308014|BL-JL4-XGL1309007-1,BL-XGL1308036|BL-JL4-XGL1309008-1,BL-XGL1309026|BL-JL4-XGL1309009-1,BL-XGL1309030|BL-JL4-XGL1309011-1,BL-XGL1309033|BL-JL4-XGL1310001-1,BL-XGL1309033|BL-JL4-XGL1310002-1,BL-XGL1310003|BL-JL4-XGL1310008-1,BL-XGL1310007|BL-JL4-XGL1310010-1,BL-XGL1310011|BL-JL4-XGL1310012-1,BL-XGL1310009|BL-JL4-XGL1310014-1,BL-XGL1310004|BL-JL4-XGL1310016-1,BL-XGL1310001|BL-JL4-XGL1310017-1,BL-XGL1310015|BL-JL4-XGL1311004-1,BL-XGL1311017|BL-JL4-XGL1311005-1,BL-XGL1311006|BL-JL4-XGL1312004-1,BL-XGL1312006|BL-JL4-XGL1311011-1,BL-XGL1311037|BL-JL4-XGL1312001-1,BL-XGL1312001|BL-JL4-XGL1312002-1,BL-XGL1312003|BL-JL4-XGL1312006-1,BL-XGL1312002|BL-JL4-XGL1312010-1,BL-XGL1312017|BL-JL4-XGL1312011-1,BL-XGL1312020|BL-JL4-XGL1312014-1,BL-XGL1312027|BL-JL4-XGL1306008-1,BL-XGL1305006-1|BL-JL4-XGL1306009-1,BL-XGL1306012-4|BL-JL4-XGL1307001-1,BL-XGL1306004-2|BL-JL4-XGL1307002-1,BL-XGL1307009-4|BL-JL4-XGL1307003-1,BL-XGL1307003-3|BL-JL4-XGL1306003-1,BL-XGL1306006-4|BL-JL4-XGL1306006-1,BL-XGL1305002-1|BL-JL4-XGL1307005-1,BL-XGL1307015-4|BL-JL4-XGL1307006-1,BL-XGL1307005-1|BL-JL4-XGL1307007-1,BL-XGL1307007-2|BL-JL4-XGL1307008-1,BL-XGL1306010-1|BL-JL4-XGL1308001-1,BL-XGL1307001-1|BL-JL4-XGL1308002-1,BL-XGL1307004-1|BL-JL4-XGL1308003-1,BL-XGL1307007-1|BL-JL4-XGL1308004-1,BL-XGL1308003-3|BL-JL4-XGL1308005-1,BL-XCL1308014-2|BL-JL4-XGL1308006-1,BL-XGL1308006-1|BL-JL4-XGL1308010-1,BL-XGL1308005-4|BL-JL4-XGL1308012-1,BL-XGL1308009-4|BL-JL4-XGL1309001-1,BL-XCL1308010-2|BL-JL4-XGL1309002-1,BL-XGL1308007-1|BL-JL4-XGL1309003-1,BL-XGL1309003-3|BL-JL4-XGL1309004-1,BL-XGL1309005-1|BL-JL4-XGL1309005-1,BL-XGL1309006-1|BL-JL4-XGL1309006-1,BL-XGL1309028-4|BL-JL4-XGL1309010-1,BL-XCL1309003-2|BL-JL4-XGL1310003-1,BL-XGL1309009-1|BL-JL4-XGL1310004-1,BL-XGL1310002-3|BL-JL4-XGL1310005-1,BL-XGL1310007-1|BL-JL4-XGL1310006-1,BL-XGL1310008-1|BL-JL4-XGL1310007-1,BL-XGL1310027-4|BL-JL4-XGL1310009-1,BL-XGL1310029-4|BL-JL4-XGL1310011-1,BL-XGL1310028-4|BL-JL4-XGL1310013-1,BL-XGL1310030-4|BL-JL4-XGL1310015-1,BL-XGL1310031-4|BL-JL4-XGL1311001-1,BL-XGL1310037-3|BL-JL4-XGL1311002-1,BL-XGL1310036-3|BL-JL4-XGL1311003-1,BL-XGL1311014-4|BL-JL4-XGL1311006-1,BL-XGL1311012-3|BL-JL4-XGL1311007-1,BL-XGL1311031-4|BL-JL4-XGL1311008-1,BL-XGL1311006-1|BL-JL4-XGL1311009-1,BL-XGL1311034-4|BL-JL4-XGL1311010-1,BL-XGL1311013-3|BL-JL4-XGL1312003-1,BL-XGL1312010-4|BL-JL4-XGL1312005-1,BL-XGL1312005-3|BL-JL4-XGL1312007-1,BL-XCL1310004-2|BL-JL4-XGL1312008-1,BL-XGL1312007-2|BL-JL4-XGL1312009-1,BL-XGL1312003-1|BL-JL4-XGL1312012-1,BL-XGL1312006-3|BL-JL4-XGL1312013-1,BL-XGL1312032-4|BL-JL4-XGL1310018-1,BL-XGL1310002-L1|BL-JL4-TKY1307001-1,BL-YGTKY1304001-1|BL-JL4-TKY1307003-1,BL-YGTKY1305001-1|BL-JL4-TKY1307004-1,BL-YGTKY1305001-3|BL-JL4-TKY1308001-1,BL-YGTKY1305002-3|BL-JL4-TKY1308006-1,BL-YGTKY1305002-1|BL-JL4-TKY1308023-1,BL-CQTKY1306001-3|BL-JL4-TKY1307005-1,BL-TKY1307001-1|BL-JL4-TKY1307006-1,BL-TKY1307001-4|BL-JL4-TKY1308013-1,BL-TKY1308001-4|BL-JL4-TKY1307002-1,BL-TKY1307001|BL-JL4-TKY1307007-1,BL-KY1307086|BL-JL4-TKY1307008-1,BL-KY1307085|BL-JL4-TKY1308002-1,BL-TKY1308001-1|BL-JL4-TKY1308003-1,BL-TKY1307001-1|BL-JL4-TKY1308004-1,BL-TKY1308004-1|BL-JL4-TKY1308005-1,BL-TKY1308003-1|BL-JL4-TKY1308007-1,BL-TKY1308016-L1|BL-JL4-TKY1308008-1,BL-TKY1308017-L1|BL-JL4-TKY1308009-1,BL-TKY1308018-L1|BL-JL4-TKY1308010-1,BL-TKY1308019-L1|BL-JL4-TKY1308011-1,BL-TKY1308020-L1|BL-JL4-TKY1308012-1,BL-TKY1308021-L1|BL-JL4-TKY1308014-1,BL-TKY1308025-L1|BL-JL4-TKY1308015-1,BL-TKY1308026-L1|BL-JL4-TKY1308016-1,BL-TKY1308027-L1|BL-JL4-TKY1308017-1,BL-TKY1308031-L1|BL-JL4-TKY1308018-1,BL-TKY1308032-L1|BL-JL4-TKY1308019-1,BL-TKY1308033-L1|BL-JL4-TKY1308020-1,BL-TKY1308034-L1|BL-JL4-TKY1308021-1,BL-TKY1308035-L1|BL-JL4-TKY1308022-1,BL-TKY1308036-L1|BL-JL4-TKY1309001-1,BL-TKY1309001-4|BL-JL4-TKY1309002-1,BL-TKY1309002-4|BL-JL4-TKY1309003-1,BL-TKY1309001-1|BL-JL4-TKY1309005-1,BL-TKY1309002-1|BL-JL4-TKY1310003-1,BL-TKY1310001-1|BL-JL4-TKY1311001-1,BL-TKY1311001-4|BL-JL4-TKY1311002-1,BL-TKY1311001-1|BL-JL4-TKY1309004-1,BL-YGTKY1307009-3|BL-JL4-TKY1309006-1,BL-CQTKY1307007-3|BL-JL4-TKY1309007-1,BL-ECTKY1307002-2|BL-JL4-TKY1310001-1,BL-DSTKY1308001-3|BL-JL4-TKY1310002-1,BL-DSTKY1308002-3|BL-JL4-TKY1310004-1,BL-CQTKY1308010-3|BL-JL4-TKY1311003-1,BL-DSTKY1308009-3|BL-JL4-TKY1311004-1,BL-TKY1311023-L1|BL-JL4-TKY1311005-1,BL-TKY1311024-L1|BL-JL4-TKY1311006-1,BL-TKY1311025-L1|BL-JL4-TKY1311007-1,BL-TKY1311037-L1|BL-JL4-TKY1311008-1,BL-TKY1311038-L1|BL-JL4-TKY1311009-1,BL-TKY1311039-L1|BL-JL4-TKY1311010-1,BL-TKY1311045-L1|BL-JL4-TKY1311011-1,BL-TKY1311046-L1|BL-JL4-TKY1311012-1,BL-TKY1311047-L1|BL-JL4-TKY1312001-1,BL-TKY1312003-L1|BL-JL4-TKY1312002-1,BL-TKY1312004-L1|BL-JL4-TKY1312003-1,BL-TKY1312005-L1|BL-JL4-TKY1312004-1,BL-TKY1312007-L1|BL-JL4-TKY1312005-1,BL-TKY1312008-L1|BL-JL4-TKY1312006-1,BL-TKY1312009-L1|BL-JL4-TKY1312007-1,BL-TKY1312010-L1|BL-JL4-TKY1312008-1,BL-TKY1312011-L1|BL-JL4-TKY1312009-1,BL-TKY1312012-L1|BL-JL4-TKY1312010-1,BL-TKY1312013-L1|BL-JL4-TKY1312011-1,BL-TKY1312014-L1|BL-JL4-TKY1312012-1,BL-TKY1312015-L1|BL-JL4-TKY1312013-1,BL-TKY1312016-L1|BL-JL4-TKY1312014-1,BL-TKY1312017-L1|BL-JL4-TKY1312015-1,BL-TKY1312018-L1|BL-JL4-TKY1312018-1,BL-TKY1312019-L1|BL-JL4-TKY1312019-1,BL-TKY1312020-L1|BL-JL4-TKY1312020-1,BL-TKY1312021-L1|BL-JL4-TKY1312027-1,BL-TKY1312022-L1|BL-JL4-TKY1312028-1,BL-TKY1312023-L1|BL-JL4-TKY1312029-1,BL-TKY1312024-L1|BL-JL4-TKY1312030-1,BL-TKY1312025-L1|BL-JL4-TKY1312031-1,BL-TKY1312026-L1|BL-JL4-TKY1312032-1,BL-TKY1312027-L1|BL-JL4-TKY1312035-1,BL-TKY1312028-L1|BL-JL4-TKY1312036-1,BL-TKY1312029-L1|BL-JL4-TKY1312037-1,BL-TKY1312030-L1|BL-JL4-TKY1312042-1,BL-TKY1312031-L1|BL-JL4-TKY1312043-1,BL-TKY1312032-L1|BL-JL4-TKY1312044-1,BL-TKY1312033-L1|BL-JL4-TKY1312016-1,BL-TKY13112001|BL-JL4-TKY1312017-1,BL-TKY1312002|BL-JL4-TKY1312021-1,BL-TKY1312003|BL-JL4-TKY1312022-1,BL-TKY1312004|BL-JL4-TKY1312023-1,BL-TKY1312007|BL-JL4-TKY1312024-1,BL-TKY1312008|BL-JL4-TKY1312025-1,BL-TKY1312005|BL-JL4-TKY1312026-1,BL-TKY1312006|BL-JL4-TKY1312033-1,BL-TKY1312009|BL-JL4-TKY1312034-1,BL-TKY1312010|BL-JL4-TKY1312038-1,BL-TKY1312011|BL-JL4-TKY1312039-1,BL-TKY1312012|BL-JL4-TKY1312040-1,BL-TKY1312013|BL-JL4-TKY1312041-1,BL-TKY1312014|BL-JL4-TKY1312045-1,BL-TKY1312015|BL-JL4-TKY1312046-1,BL-TKY1312016|BL-JL4-FMH1401001-1,BL-FMH1401001|BL-JL4-FMH1401002-1,BL-F1401001-1|BL-JL4-FMH1402001-1,BL-F1312002-1|BL-JL4-FMH1403001-1,BL-F1402002-2|BL-JL4-FMH1403006-1,BL-F1403005-3|BL-JL4-FMH1403007-1,BL-F1403003-1|BL-JL4-FMH1403008-1,BL-FMH1403012|BL-JL4-FMH1403010-1,BL-FMH1403007|BL-JL4-FMH1403011-1,BL-FMH1403021|BL-JL4-FMH1403012-1,BL-F1403002-2|BL-JL4-FMH1404002-1,BL-FMH1404004|BL-JL4-FMH1404003-1,BL-F1404005-3|BL-JL4-FMH1404004-1,BL-F1404003-2|BL-JL4-FMH1405002-1,BL-FMH1405002|BL-JL4-FMH1405003-1,BL-FMH1405003|BL-JL4-FMH1405004-1,BL-F1405004-3|BL-JL4-FMH1405005-1,BL-FMH1405006|BL-JL4-FMH1406003-1,BL-FMH1406004|BL-JL4-FMH1406004-1,BL-FMH1406007|BL-JL4-FMH1406005-1,BL-F1406005-1|BL-JL4-FMH1406006-1,BL-FMH1406008|BL-JL4-FMH1407001-1,BL-FMH1406013|BL-JL4-FMH1407002-1,BL-FMH1407001|BL-JL4-FMH1407006-1,BL-FMH1407008|BL-JL4-FMH1407007-1,BL-FMH1407009|BL-JL4-FMH1407009-1,BL-FMH1407011|BL-JL4-FMH1408001-1,BL-F1407006-2|BL-JL4-FMH1408002-1,BL-F1408001-1|BL-JL4-FMH1408004-1,BL-F1407006-1|BL-JL4-FMH1408005-1,BL-FMH1408004|BL-JL4-FMH1408006-1,BL-FMH1408005|BL-JL4-FMH1401003-1,BL-FMH1401003-4|BL-JL4-FMH1403002-1,BL-FMH1403005-4|BL-JL4-FMH1403003-1,BL-FMH1403006-4|BL-JL4-FMH1403009-1,BL-FMH1403023-4|BL-JL4-FMH1407005-1,BL-FMH1407002-4|BL-JL4-FMH1408007-1,BL-FMH1407007-4|BL-JL4-FMH1408008-1,BL-FMH1408005-4|BL-JL4-FMH1403004-1,BL-FMH1403002-L2|BL-JL4-FMH1405001-1,BL-FMH1404006-L1|BL-JL4-FMH1406001-1,BL-FMH1405002-L2|BL-JL4-FMH1407003-1,BL-FMH1407001-L1|BL-JL4-FMH1407004-1,BL-FMH1406006-L2|BL-JL4-FMH1407008-1,BL-FMH1406007-L2|BL-JL4-FMH1408003-1,BL-FMH1408002-L2|BL-JL4-FMH1408009-1,BL-FMH1408006-L1|BL-JL4-FMH1408010-1,BL-FMH1408003-L2|BL-JL4-FMH1406002-1,BL-F1406003|BL-JL4-FMH1403005-1,BL-F1403006|BL-JL4-FMH1408011-1,BL-F1408006|BL-JL4-TKY1401004-1,BL-TKY1401001|BL-JL4-TKY1401005-1,BL-TKY1401002|BL-JL4-TKY1401006-1,BL-TKY1401003|BL-JL4-TKY1401007-1,BL-TKY1401004|BL-JL4-TKY1401011-1,BL-TKY1401007|BL-JL4-TKY1401012-1,BL-TKY1401008|BL-JL4-TKY1401013-1,BL-TKY1401005|BL-JL4-TKY1401014-1,BL-TKY1401006|BL-JL4-TKY1401020-1,BL-TKY1401009|BL-JL4-TKY1401021-1,BL-TKY1401010|BL-JL4-TKY1401022-1,BL-TKY1401011|BL-JL4-TKY1401023-1,BL-TKY1401012|BL-JL4-TKY1401024-1,BL-TKY1401013|BL-JL4-TKY1401025-1,BL-TKY1401014|BL-JL4-TKY1401029-1,BL-TKY1401015|BL-JL4-TKY1401030-1,BL-TKY1401016|BL-JL4-TKY1401031-1,BL-TKY1401017|BL-JL4-TKY1401032-1,BL-TKY1401018|BL-JL4-TKY1401033-1,BL-TKY1401019|BL-JL4-TKY1401034-1,BL-TKY1401020|BL-JL4-TKY1401041-1,BL-TKY1401021|BL-JL4-TKY1401042-1,BL-TKY1401022|BL-JL4-TKY1401043-1,BL-TKY1401023|BL-JL4-TKY1401044-1,BL-TKY1401024|BL-JL4-TKY1401048-1,BL-TKY1401025|BL-JL4-TKY1401049-1,BL-TKY1401026|BL-JL4-TKY1401050-1,BL-TKY1401027|BL-JL4-TKY1401051-1,BL-TKY1401028|BL-JL4-TKY1401055-1,BL-TKY1401029|BL-JL4-TKY1401056-1,BL-TKY1401030|BL-JL4-TKY1401057-1,BL-TKY1401031|BL-JL4-TKY1401058-1,BL-TKY1401032|BL-JL4-TKY1401065-1,BL-TKY1401033|BL-JL4-TKY1401066-1,BL-TKY1401034|BL-JL4-TKY1401067-1,BL-TKY1401035|BL-JL4-TKY1401068-1,BL-TKY1401036|BL-JL4-TKY1402001-1,BL-TKY1402014|BL-JL4-TKY1402002-1,BL-TKY1402015|BL-JL4-TKY1402003-1,BL-TKY1402016|BL-JL4-TKY1402004-1,BL-TKY1402017|BL-JL4-TKY1402005-1,BL-TKY1402018|BL-JL4-TKY1402006-1,BL-TKY1402019|BL-JL4-TKY1402007-1,BL-TKY1402020|BL-JL4-TKY1402008-1,BL-TKY1402021|BL-JL4-TKY1402009-1,BL-TKY1402022|BL-JL4-TKY1402010-1,BL-TKY1402023|BL-JL4-TKY1402011-1,BL-TKY1402024|BL-JL4-TKY1402012-1,BL-TKY1402025|BL-JL4-TKY1402013-1,BL-TKY1402026|BL-JL4-TKY1402014-1,BL-TKY1402027|BL-JL4-TKY1402015-1,BL-TKY1402028|BL-JL4-TKY1402016-1,BL-TKY1402029|BL-JL4-TKY1402017-1,BL-TKY1402030|BL-JL4-TKY1402018-1,BL-TKY1402031|BL-JL4-TKY1402019-1,BL-TKY1402032|BL-JL4-TKY1402020-1,BL-TKY1402033|BL-JL4-TKY1402021-1,BL-TKY1402034|BL-JL4-TKY1402022-1,BL-TKY1402037|BL-JL4-TKY1402023-1,BL-TKY1402038|BL-JL4-TKY1402024-1,BL-TKY1402035|BL-JL4-TKY1402025-1,BL-TKY1402036|BL-JL4-TKY1402026-1,BL-TKY1402039|BL-JL4-TKY1402027-1,BL-TKY1402040|BL-JL4-TKY1402028-1,BL-TKY1402041|BL-JL4-TKY1402029-1,BL-TKY1402042|BL-JL4-TKY1402030-1,BL-TKY1402043|BL-JL4-TKY1402031-1,BL-TKY1402044|BL-JL4-TKY1402032-1,BL-TKY1402045|BL-JL4-TKY1402033-1,BL-TKY1402046|BL-JL4-TKY1402034-1,BL-TKY1402047|BL-JL4-TKY1402035-1,BL-TKY1402052|BL-JL4-TKY1403001-1,BL-TKY1403005|BL-JL4-TKY1401001-1,BL-TKY1401001-L1|BL-JL4-TKY1401002-1,BL-TKY1401002-L1|BL-JL4-TKY1401003-1,BL-TKY1401003-L1|BL-JL4-TKY1401008-1,BL-TKY1401004-L1|BL-JL4-TKY1401009-1,BL-TKY1401005-L1|BL-JL4-TKY1401010-1,BL-TKY1401006-L1|BL-JL4-TKY1401026-1,BL-TKY1401013-L1|BL-JL4-TKY1401027-1,BL-TKY1401014-L1|BL-JL4-TKY1401028-1,BL-TKY1401015-L1|BL-JL4-TKY1401035-1,BL-TKY1401016-L1|BL-JL4-TKY1401036-1,BL-TKY1401017-L1|BL-JL4-TKY1401037-1,BL-TKY1401018-L1|BL-JL4-TKY1401038-1,BL-TKY1401019-L1|BL-JL4-TKY1401039-1,BL-TKY1401020-L1|BL-JL4-TKY1401040-1,BL-TKY1401021-L1|BL-JL4-TKY1401045-1,BL-TKY1401022-L1|BL-JL4-TKY1401046-1,BL-TKY1401023-L1|BL-JL4-TKY1401047-1,BL-TKY1401024-L1|BL-JL4-TKY1401052-1,BL-TKY1401025-L1|BL-JL4-TKY1401053-1,BL-TKY1401026-L1|BL-JL4-TKY1401054-1,BL-TKY1401027-L1|BL-JL4-TKY1401059-1,BL-TKY1401028-L1|BL-JL4-TKY1401060-1,BL-TKY1401029-L1|BL-JL4-TKY1401061-1,BL-TKY1401030-L1|BL-JL4-TKY1401062-1,BL-TKY1401031-L1|BL-JL4-TKY1401063-1,BL-TKY1401032-L1|BL-JL4-TKY1401064-1,BL-TKY1401033-L1|BL-JL4-TKY1406020-1,BL-TKY1406094-L1|BL-JL4-TKY1406021-1,BL-TKY1406095-L1|BL-JL4-TKY1406022-1,BL-TKY1406096-L1|BL-JL4-TKY1407015-1,BL-TKY1407167-L1|BL-JL4-TKY1407016-1,BL-TKY1407100-L1|BL-JL4-TKY1407017-1,BL-TKY1407101-L1|BL-JL4-TKY1407018-1,BL-TKY1407102-L1|BL-JL4-TKY1408005-1,BL-TKY1408014-L1|BL-JL4-TKY1408006-1,BL-TKY1408015-L1|BL-JL4-TKY1408007-1,BL-TKY1408016-L1|BL-JL4-TKY1408008-1,BL-TKY1408019-L1|BL-JL4-TKY1408009-1,BL-TKY1408020-L1|BL-JL4-TKY1408010-1,BL-TKY1408021-L1|BL-JL4-TKY1408029-1,BL-TKY1408240-L1|BL-JL4-TKY1408030-1,BL-TKY1408241-L1|BL-JL4-TKY1409002-1,BL-TKY1409050-L1|BL-JL4-TKY1409003-1,BL-TKY1409051-L1|BL-JL4-TKY1409004-1,BL-TKY1409059-L1|BL-JL4-TKY1409005-1,BL-TKY1409060-L1|BL-JL4-TKY1409006-1,BL-TKY1409061-L1|BL-JL4-TKY1401015-1,BL-XJLTKY1311002-3|BL-JL4-TKY1401016-1,BL-XJLTKY1311002-3|BL-JL4-TKY1401017-1,BL-XJLTKY1311002-3|BL-JL4-TKY1405001-1,BL-XGTKY1405002-4|BL-JL4-TKY1406016-1,BL-XGTKY1406003-4|BL-JL4-TKY1401018-1,BL-TKY1401005-4|BL-JL4-TKY1401019-1,BL-TKY1401008-4|BL-JL4-TKY1403002-1,BL-TKY1403007-4|BL-JL4-TKY1403003-1,BL-TKY1403008-4|BL-JL4-TKY1403004-1,BL-TKY1403009-4|BL-JL4-TKY1403005-1,BL-TKY1403010-4|BL-JL4-TKY1403006-1,BL-TKY1403011-4|BL-JL4-TKY1403007-1,BL-TKY1403022-4|BL-JL4-TKY1403008-1,BL-TKY1403023-4|BL-JL4-TKY1403009-1,BL-TKY1403045-4|BL-JL4-TKY1403010-1,BL-TKY1403049-4|BL-JL4-TKY1406001-1,BL-TKY1406094-4|BL-JL4-TKY1406002-1,BL-TKY1406095-4|BL-JL4-TKY1406003-1,BL-TKY1406092-4|BL-JL4-TKY1406004-1,BL-TKY1406107-4|BL-JL4-TKY1406005-1,BL-TKY1406111-4|BL-JL4-TKY1406006-1,BL-TKY1406110-4|BL-JL4-TKY1406007-1,BL-TKY1406118-4|BL-JL4-TKY1406008-1,BL-TKY1406113-4|BL-JL4-TKY1406009-1,BL-TKY1406115-4|BL-JL4-TKY1406010-1,BL-TKY1406116-4|BL-JL4-TKY1406013-1,BL-TKY1406136-4|BL-JL4-TKY1406014-1,BL-TKY1406131-4|BL-JL4-TKY1406015-1,BL-TKY1406134-4|BL-JL4-TKY1406017-1,BL-TKY1406153-4|BL-JL4-TKY1406018-1,BL-TKY1406155-4|BL-JL4-TKY1406019-1,BL-TKY1406158-4|BL-JL4-TKY1407001-1,BL-TKY1407003-4|BL-JL4-TKY1407002-1,BL-TKY1407005-4|BL-JL4-TKY1407007-1,BL-TKY1407031-4|BL-JL4-TKY1407008-1,BL-TKY1407035-4|BL-JL4-TKY1407009-1,BL-TKY1407033-4|BL-JL4-TKY1407010-1,BL-TKY1407049-4|BL-JL4-TKY1407011-1,BL-TKY1407048-4|BL-JL4-TKY1407013-1,BL-ECTKY1405001|BL-JL4-TKY1407019-1,BL-TKY1407076-4|BL-JL4-TKY1407020-1,BL-TKY1407075-4|BL-JL4-TKY1407021-1,BL-TKY1407077-4|BL-JL4-TKY1407022-1,BL-TKY1407078-4|BL-JL4-TKY1407025-1,BL-TKY1407098-4|BL-JL4-TKY1407026-1,BL-TKY1407099-4|BL-JL4-TKY1407027-1,BL-TKY1407100-4|BL-JL4-TKY1407029-1,BL-TKY1407129-4|BL-JL4-TKY1407030-1,BL-TKY1407130-4|BL-JL4-TKY1407031-1,BL-TKY1407128-4|BL-JL4-TKY1407032-1,BL-ECTKY1405002|BL-JL4-TKY1407033-1,BL-TKY1407002-3|BL-JL4-TKY1407034-1,BL-TKY1407003-3|BL-JL4-TKY1407035-1,BL-TKY1407004-3|BL-JL4-TKY1408001-1,BL-TKY1408001-3|BL-JL4-TKY1408002-1,BL-TKY1408007-4|BL-JL4-TKY1408003-1,BL-TKY1408006-4|BL-JL4-TKY1408011-1,BL-TKY1408027-4|BL-JL4-TKY1408012-1,BL-TKY1408028-4|BL-JL4-TKY1408013-1,BL-TKY1408046-4|BL-JL4-TKY1408014-1,BL-TKY1408045-4|BL-JL4-TKY1408015-1,BL-YGTKY1406001|BL-JL4-TKY1408021-1,BL-ECTKY1406001|BL-JL4-TKY1404001-1,BL-TKY1404012|BL-JL4-TKY1404002-1,BL-TKY1404019|BL-JL4-TKY1404003-1,BL-TKY1404020|BL-JL4-TKY1404004-1,BL-TKY1404023|BL-JL4-TKY1406011-1,BL-TKY1406127|BL-JL4-TKY1406012-1,BL-TKY1406131|BL-JL4-TKY1406023-1,BL-TKY1406170|BL-JL4-TKY1407005-1,BL-TKY1407033|BL-JL4-TKY1407006-1,BL-TKY1407001|BL-JL4-TKY1407003-1,BL-QA31.5ZY-018-15-L2|BL-JL4-TKY1407004-1,BL-QA31.5XZ-019-15-L2|BL-JL4-TKY1407012-1,BL-QA31.5ZY-025-015-L2|BL-JL4-TKY1407014-1,BL-QA31.5ZY-026-015-L2|BL-JL4-TKY1407023-1,BL-QA31.5ZY-027-015-L2|BL-JL4-TKY1407024-1,BL-QA31.5ZY-027-015-L2|BL-JL4-TKY1408004-1,BL-QA31.5ZY-036-015-L2|BL-JL4-TKY1408016-1,BL-QA31.5ZY-039-015-L2|BL-JL4-TKY1408017-1,BL-QA31.5ZY-040-015-L2|BL-JL4-TKY1408018-1,BL-QA31.5ZY-042-015-L2|BL-JL4-TKY1408019-1,BL-QA31.5ZY-043-015-L2|BL-JL4-TKY1408020-1,BL-YGTKY1406001-2|BL-JL4-TKY1407028-1,BL-XGTKY1407003-4|BL-JL4-TKY1408022-1,BL-CQTKY1406004-3|BL-JL4-TKY1408023-1,BL-QA31.5ZY-045-015-L2|BL-JL4-TKY1408024-1,BL-QA31.5ZY-046-015-L2|BL-JL4-TKY1408025-1,BL-QA31.5ZY-047-015-L2|BL-JL4-TKY1408028-1,BL-QA31.5ZY-053-015-L2|BL-JL4-TKY1408026-1,BL-TKY1408061-4|BL-JL4-TKY1408027-1,BL-TKY1408062-4|BL-JL4-TKY1408031-1,BL-TKY1408090-4|BL-JL4-TKY1408032-1,BL-TKY1408091-4|BL-JL4-TKY1409001-1,BL-TKY1408002-3|BL-JL4-TKY1409007-1,BL-TKY1409007-4|BL-JL4-TKY1409008-1,BL-TKY1409008-4";
            string[] sUTS = strBHLists.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in sUTS)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    string[] ss = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (ss.Length == 2)
                    {
                        string PXBH = ss[0];
                        string SGBH = ss[1];
                        DataTable dtSG = GetDataTable("SELECT ID FROM dbo.sys_document WHERE Status>0 and BGBH='" + SGBH + "'");
                        DataTable dtPX = GetDataTable("SELECT ID FROM dbo.sys_document WHERE Status>0 and BGBH='" + PXBH + "'");
                        if (dtSG != null && dtSG.Rows.Count == 1 && dtPX != null && dtPX.Rows.Count == 1)
                        {
                            string SGID = dtSG.Rows[0][0].ToString();
                            string PXID = dtPX.Rows[0][0].ToString();
                            DataTable dtPXGX = GetDataTable("SELECT ID FROM dbo.sys_px_relation WHERE SGDataID='" + SGID + "' and PXDataID='" + PXID + "'");
                            if (dtPXGX != null && dtPXGX.Rows.Count < 1)
                            {
                                string sql = string.Format("INSERT INTO dbo.sys_px_relation( SGDataID, PXDataID, PXTime )VALUES( @SGDataID,@PXDataID,'2014-10-10 00:00:00')");
                                SqlCommand cmd1 = new SqlCommand(sql);
                                cmd1.Parameters.Add(new SqlParameter("@SGDataID", SGID));
                                cmd1.Parameters.Add(new SqlParameter("@PXDataID", PXID));
                                ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd1 });
                                logger.Error("success item:" + s);
                            }
                            else
                            {
                                logger.Error("errer dtPXGX null or count=1 item:" + s);
                            }
                        }
                        else
                        {
                            logger.Error("errer dtSG,dtPX null item:" + s);
                        }
                    }
                    else
                    {
                        logger.Error("errer ss.Length<>2 item:" + s);
                    }
                }
            }
        }
        /// <summary>
        /// 批量修改单元格的值
        /// </summary>
        /// <param name="mid"></param>
        private void UpdateCellValueAll(string mid)
        {

            String sql = @"SELECT ID FROM dbo.sys_document WHERE  ModuleID='C9A1DD95-79BF-4543-924B-94362381E705' AND Status>0";
            DataTable dt = GetDataTable(sql);
            if (dt != null)
            {
                Int32 i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    i++;
                    String id = row["ID"].ToString();
                    string strSqlDoc = "select ID,Data from sys_document where ID='" + id + "'";
                    DataTable dtDoc = GetDataTable(strSqlDoc);
                    if (dtDoc != null && dtDoc.Rows.Count > 0)
                    {
                        JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dtDoc.Rows[0]["Data"].ToString());
                        //JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(row["Data"].ToString());
                        if (doc != null)
                        {
                            JZSheet s = null;
                            foreach (JZSheet sheet in doc.Sheets)
                            {
                                if (sheet.ID == new Guid("5086D926-BE72-4B82-B7DA-9FC7BDED253E"))
                                {
                                    s = sheet;
                                    break;
                                }
                            }
                            if (s == null)
                            {
                                continue;
                            }
                            logger.Error("UpdateCellValueAll i:" + i);

                            //DeleteSheetCell(ref s, "L18");
                            object oY24 = JZCommonHelper.GetCellValue(doc, s.ID, "E6");
                            if (oY24 != null && !string.IsNullOrEmpty(oY24.ToString()))
                            {
                                CreateSheetCell(ref s, "E6", oY24.ToString().Replace("d",""));
                            }
                            //object oAB24 = JZCommonHelper.GetCellValue(doc, s.ID, "AB24");
                            //if (oAB24 != null && !string.IsNullOrEmpty(oAB24.ToString()) && oAB24.ToString() == "北京瑞帝斯")
                            //{
                            //    CreateSheetCell(ref s, "AB24", "北京瑞帝斯建材有限公司");
                            //}
                            //CreateSheetCell(ref s, "E25", JZCommonHelper.GetCellValue(doc, s.ID, "E24"));
                            //CreateSheetCell(ref s, "E24", JZCommonHelper.GetCellValue(doc, s.ID, "E23"));
                            //CreateSheetCell(ref s, "E23", "/");

                            String json = Newtonsoft.Json.JsonConvert.SerializeObject(doc).Replace("'", "''");
                            sql = "UPDATE dbo.sys_document SET Data=@Data WHERE ID=@ID";
                            SqlCommand cmd1 = new SqlCommand(sql);
                            cmd1.Parameters.Add(new SqlParameter("@ID", id));
                            cmd1.Parameters.Add(new SqlParameter("@Data", json));
                            ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd1 });
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 重新保存并且删除不合格资料
        /// </summary>
        private void ReSaveAndDeleteInvalidDoc()
        {
            String sql = @"SELECT ID,ModuleID FROM dbo.sys_invalid_document WHERE F_InvalidItem LIKE '%#NAME%' AND id IN(SELECT Dataid FROM dbo.sys_test_data WHERE CreatedTime>'2014-09-15') ";
            DataTable dt = GetDataTable(sql);
            ModuleHelper mh = new ModuleHelper();
            CaiJiHelper caiji = new CaiJiHelper();
            DocumentHelper dh = new DocumentHelper();

            if (dt != null)
            {
                Int32 i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    i++;
                    String id = row["ID"].ToString();
                    Guid ModuleID = new Guid(row["ModuleID"].ToString());
                    string strSqlDoc = "select ID,Data from sys_document where ID='" + id + "'";
                    DataTable dtDoc = GetDataTable(strSqlDoc);
                    if (dtDoc != null && dtDoc.Rows.Count > 0)
                    {
                        JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dtDoc.Rows[0]["Data"].ToString());
                        Sys_Module module = mh.GetModuleBaseInfoByID(ModuleID);
                        caiji.GenerateDoc(doc, ModuleID);

                        String json = Newtonsoft.Json.JsonConvert.SerializeObject(doc).Replace("'", "''");
                        sql = "UPDATE dbo.sys_document SET Data=@Data WHERE ID=@ID;DELETE FROM dbo.sys_invalid_document WHERE ID=@ID";
                        SqlCommand cmd1 = new SqlCommand(sql);
                        cmd1.Parameters.Add(new SqlParameter("@ID", id));
                        cmd1.Parameters.Add(new SqlParameter("@Data", json));
                        ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd1 });

                        if (dh.ApplyModuleSetting(doc, module.ModuleSettings))//给modulesetting定义字段赋值
                        {
                            //验证是否合格，并发短信
                            QualifyHelper qh = new QualifyHelper();
                            qh.Qualify(doc, module);
                        }

                    }
                }
            }
        }

        private void ReSaveSheetData()
        {
            String sql = @"SELECT ID FROM dbo.sys_sheet";
            DataTable dt = GetDataTable(sql);

            if (dt != null)
            {
                Int32 i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    i++;
                    String id = row["ID"].ToString();
                    string strSqlDoc = "select ID,SheetData from sys_sheet where ID='" + id + "'";
                    DataTable dtDoc = GetDataTable(strSqlDoc);
                    if (dtDoc != null && dtDoc.Rows.Count > 0)
                    {
                        List<JZCell> dataCells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZCell>>(dtDoc.Rows[0]["SheetData"].ToString());

                        List<JZCell> cells = new List<JZCell>();
                        #region CellLogic
                        foreach (JZCell cell in dataCells)
                        {
                            if (cell == null)
                            {
                                continue;
                            }

                            JZCell c = new JZCell();
                            c.Name = cell.Name;
                            c.Value = cell.Value;
                            bool bHasExist = false;
                            foreach (JZCell cc in cells)
                            {
                                if (cc.Name == c.Name)
                                {
                                    bHasExist = true;
                                    break;
                                }
                            }
                            if (bHasExist == true)
                            {
                                continue;
                            }
                            cells.Add(c);
                        }
                        #endregion

                        //sheet.SheetData = Newtonsoft.Json.JsonConvert.SerializeObject(cells);

                        String json = Newtonsoft.Json.JsonConvert.SerializeObject(cells);

                        sql = "UPDATE dbo.sys_sheet SET SheetData=@SheetData WHERE ID=@ID;";
                        SqlCommand cmd1 = new SqlCommand(sql);
                        cmd1.Parameters.Add(new SqlParameter("@ID", id));
                        cmd1.Parameters.Add(new SqlParameter("@SheetData", json));
                        ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd1 });

                    }
                }
            }
        }

        /// <summary>
        /// 批量移动单元格
        /// </summary>
        /// <param name="mid"></param>
        public void MoveDocumentByModuleIDBatch(string mid)
        {
            String sql = @"select ID from sys_document where ModuleID='" + mid + "' ";

            DataTable dt = GetDataTable(sql);
            if (dt != null)
            {
                Int32 i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    logger.Error("" + i.ToString());
                    i++;
                    String id = row["ID"].ToString();
                    string strSqlDoc = "select ID,Data from sys_document where ID='" + id + "'";
                    DataTable dtDoc = GetDataTable(strSqlDoc);
                    if (dtDoc != null && dtDoc.Rows.Count > 0)
                    {
                        JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dtDoc.Rows[0]["Data"].ToString());
                        if (doc != null)
                        {
                            JZSheet s = null;
                            foreach (JZSheet sheet in doc.Sheets)
                            {
                                if (sheet.ID == new Guid("72E4B9B3-CA49-4CBD-BC07-CEB0F1B54708"))
                                {
                                    s = sheet;
                                    break;
                                }
                            }
                            if (s == null)
                            {
                                continue;
                            }
                            //CreateSheetCell(ref s, "E25", JZCommonHelper.GetCellValue(doc, s.ID, "E24"));
                            //CreateSheetCell(ref s, "E24", JZCommonHelper.GetCellValue(doc, s.ID, "E23"));
                            //CreateSheetCell(ref s, "E23", "/");
                            //C I O T 50-->52
                            CreateSheetCell(ref s, "C52", JZCommonHelper.GetCellValue(doc, s.ID, "C50"));
                            CreateSheetCell(ref s, "I52", JZCommonHelper.GetCellValue(doc, s.ID, "I50"));
                            CreateSheetCell(ref s, "O52", JZCommonHelper.GetCellValue(doc, s.ID, "O50"));
                            CreateSheetCell(ref s, "T52", JZCommonHelper.GetCellValue(doc, s.ID, "T50"));
                            DeleteSheetCell(ref s, "C50");
                            DeleteSheetCell(ref s, "I50");
                            DeleteSheetCell(ref s, "O50");
                            DeleteSheetCell(ref s, "T50");
                            //A F I L S 39-->41
                            CreateSheetCell(ref s, "A41", JZCommonHelper.GetCellValue(doc, s.ID, "A39"));
                            CreateSheetCell(ref s, "F41", JZCommonHelper.GetCellValue(doc, s.ID, "F39"));
                            CreateSheetCell(ref s, "I41", JZCommonHelper.GetCellValue(doc, s.ID, "I39"));
                            CreateSheetCell(ref s, "L41", JZCommonHelper.GetCellValue(doc, s.ID, "L39"));
                            CreateSheetCell(ref s, "S41", JZCommonHelper.GetCellValue(doc, s.ID, "S39"));
                            DeleteSheetCell(ref s, "A39");
                            DeleteSheetCell(ref s, "F39");
                            DeleteSheetCell(ref s, "I39");
                            DeleteSheetCell(ref s, "L39");
                            DeleteSheetCell(ref s, "S39");
                            //A F I L S 36-->38
                            CreateSheetCell(ref s, "A38", JZCommonHelper.GetCellValue(doc, s.ID, "A36"));
                            CreateSheetCell(ref s, "F38", JZCommonHelper.GetCellValue(doc, s.ID, "F36"));
                            CreateSheetCell(ref s, "I38", JZCommonHelper.GetCellValue(doc, s.ID, "I36"));
                            CreateSheetCell(ref s, "L38", JZCommonHelper.GetCellValue(doc, s.ID, "L36"));
                            CreateSheetCell(ref s, "S38", JZCommonHelper.GetCellValue(doc, s.ID, "S36"));
                            DeleteSheetCell(ref s, "A36");
                            DeleteSheetCell(ref s, "F36");
                            DeleteSheetCell(ref s, "I36");
                            DeleteSheetCell(ref s, "L36");
                            DeleteSheetCell(ref s, "S36");
                            //A F I L S 33-->35
                            CreateSheetCell(ref s, "A35", JZCommonHelper.GetCellValue(doc, s.ID, "A33"));
                            CreateSheetCell(ref s, "F35", JZCommonHelper.GetCellValue(doc, s.ID, "F33"));
                            CreateSheetCell(ref s, "I35", JZCommonHelper.GetCellValue(doc, s.ID, "I33"));
                            CreateSheetCell(ref s, "L35", JZCommonHelper.GetCellValue(doc, s.ID, "L33"));
                            CreateSheetCell(ref s, "S35", JZCommonHelper.GetCellValue(doc, s.ID, "S33"));
                            DeleteSheetCell(ref s, "A33");
                            DeleteSheetCell(ref s, "F33");
                            DeleteSheetCell(ref s, "I33");
                            DeleteSheetCell(ref s, "L33");
                            DeleteSheetCell(ref s, "S33");
                            //A F I L S 30-->32
                            CreateSheetCell(ref s, "A32", JZCommonHelper.GetCellValue(doc, s.ID, "A30"));
                            CreateSheetCell(ref s, "F32", JZCommonHelper.GetCellValue(doc, s.ID, "F30"));
                            CreateSheetCell(ref s, "I32", JZCommonHelper.GetCellValue(doc, s.ID, "I30"));
                            CreateSheetCell(ref s, "L32", JZCommonHelper.GetCellValue(doc, s.ID, "L30"));
                            CreateSheetCell(ref s, "S32", JZCommonHelper.GetCellValue(doc, s.ID, "S30"));
                            DeleteSheetCell(ref s, "A30");
                            DeleteSheetCell(ref s, "F30");
                            DeleteSheetCell(ref s, "I30");
                            DeleteSheetCell(ref s, "L30");
                            DeleteSheetCell(ref s, "S30");
                            //D P 41-43……30-->32
                            CreateSheetCell(ref s, "D43", JZCommonHelper.GetCellValue(doc, s.ID, "D41"));
                            CreateSheetCell(ref s, "P43", JZCommonHelper.GetCellValue(doc, s.ID, "P41"));
                            CreateSheetCell(ref s, "D42", JZCommonHelper.GetCellValue(doc, s.ID, "D40"));
                            CreateSheetCell(ref s, "P42", JZCommonHelper.GetCellValue(doc, s.ID, "P40"));
                            CreateSheetCell(ref s, "D41", JZCommonHelper.GetCellValue(doc, s.ID, "D39"));
                            CreateSheetCell(ref s, "P41", JZCommonHelper.GetCellValue(doc, s.ID, "P39"));
                            CreateSheetCell(ref s, "D40", JZCommonHelper.GetCellValue(doc, s.ID, "D38"));
                            CreateSheetCell(ref s, "P40", JZCommonHelper.GetCellValue(doc, s.ID, "P38"));
                            CreateSheetCell(ref s, "D39", JZCommonHelper.GetCellValue(doc, s.ID, "D37"));
                            CreateSheetCell(ref s, "P39", JZCommonHelper.GetCellValue(doc, s.ID, "P37"));
                            CreateSheetCell(ref s, "D38", JZCommonHelper.GetCellValue(doc, s.ID, "D36"));
                            CreateSheetCell(ref s, "P38", JZCommonHelper.GetCellValue(doc, s.ID, "P36"));
                            CreateSheetCell(ref s, "D37", JZCommonHelper.GetCellValue(doc, s.ID, "D35"));
                            CreateSheetCell(ref s, "P37", JZCommonHelper.GetCellValue(doc, s.ID, "P35"));
                            CreateSheetCell(ref s, "D36", JZCommonHelper.GetCellValue(doc, s.ID, "D34"));
                            CreateSheetCell(ref s, "P36", JZCommonHelper.GetCellValue(doc, s.ID, "P34"));
                            CreateSheetCell(ref s, "D35", JZCommonHelper.GetCellValue(doc, s.ID, "D33"));
                            CreateSheetCell(ref s, "P35", JZCommonHelper.GetCellValue(doc, s.ID, "P33"));
                            CreateSheetCell(ref s, "D34", JZCommonHelper.GetCellValue(doc, s.ID, "D32"));
                            CreateSheetCell(ref s, "P34", JZCommonHelper.GetCellValue(doc, s.ID, "P32"));
                            CreateSheetCell(ref s, "D33", JZCommonHelper.GetCellValue(doc, s.ID, "D31"));
                            CreateSheetCell(ref s, "P33", JZCommonHelper.GetCellValue(doc, s.ID, "P31"));
                            CreateSheetCell(ref s, "D32", JZCommonHelper.GetCellValue(doc, s.ID, "D30"));
                            CreateSheetCell(ref s, "P32", JZCommonHelper.GetCellValue(doc, s.ID, "P30"));
                            DeleteSheetCell(ref s, "D30");
                            DeleteSheetCell(ref s, "P30");
                            DeleteSheetCell(ref s, "D31");
                            DeleteSheetCell(ref s, "P31");

                            //E I N S 26-->28
                            CreateSheetCell(ref s, "E28", JZCommonHelper.GetCellValue(doc, s.ID, "E26"));
                            CreateSheetCell(ref s, "I28", JZCommonHelper.GetCellValue(doc, s.ID, "I26"));
                            CreateSheetCell(ref s, "N28", JZCommonHelper.GetCellValue(doc, s.ID, "N26"));
                            CreateSheetCell(ref s, "S28", JZCommonHelper.GetCellValue(doc, s.ID, "S26"));
                            //E I N S 25-->26
                            CreateSheetCell(ref s, "E26", JZCommonHelper.GetCellValue(doc, s.ID, "E25"));
                            CreateSheetCell(ref s, "I26", JZCommonHelper.GetCellValue(doc, s.ID, "I25"));
                            CreateSheetCell(ref s, "N26", JZCommonHelper.GetCellValue(doc, s.ID, "N25"));
                            CreateSheetCell(ref s, "S26", JZCommonHelper.GetCellValue(doc, s.ID, "S25"));
                            //E I N S 24-->25
                            CreateSheetCell(ref s, "E25", JZCommonHelper.GetCellValue(doc, s.ID, "E24"));
                            CreateSheetCell(ref s, "I25", JZCommonHelper.GetCellValue(doc, s.ID, "I24"));
                            CreateSheetCell(ref s, "N25", JZCommonHelper.GetCellValue(doc, s.ID, "N24"));
                            CreateSheetCell(ref s, "S25", JZCommonHelper.GetCellValue(doc, s.ID, "S24"));
                            //E I N S 24 清空
                            CreateSheetCell(ref s, "E24", "");
                            CreateSheetCell(ref s, "I24", "");
                            CreateSheetCell(ref s, "N24", "");
                            CreateSheetCell(ref s, "S24", "");

                            String json = Newtonsoft.Json.JsonConvert.SerializeObject(doc).Replace("'", "''");
                            sql = "UPDATE dbo.sys_document SET Data=@Data WHERE ID=@ID";
                            SqlCommand cmd1 = new SqlCommand(sql);
                            cmd1.Parameters.Add(new SqlParameter("@ID", id));
                            cmd1.Parameters.Add(new SqlParameter("@Data", json));
                            ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd1 });
                        }
                    }
                }
            }
        }

        public void DeleteRepeatTestData()
        { 
        }
        #endregion
        public void MoveDocumentByModuleID(string mid)
        {
            //UpdateCellValueAll(mid);
            //AddPXRelation(mid);
            MoveDocumentByModuleIDBatch(mid);
        }
        public void MoveDocumentByModuleID6(string id)
        {
            var sql = "SELECT * from temp_document";
            var table = GetDataTable(sql);

            if (table == null)
            {
                logger.Error("Table 为空");
            }

            foreach (DataRow row in table.Rows)
            {
                var documentId = row["ID"].ToString();
                var document = JsonConvert.DeserializeObject<JZDocument>(row["Data"].ToString());

                if (document == null)
                {
                    logger.Error("ID 为 " + documentId + " 的 JSON 文档为空");
                    continue;
                }

                JZSheet s;
                foreach (JZSheet sheet in document.Sheets)
                {
                    s = sheet;
                    //钢筋试验记录
                    if (sheet.ID == new Guid("270A1DA6-2045-405A-AE77-18C0C98C1EDD"))
                    {
                        sheet.ID = new Guid("5A878CC3-8D95-44A5-ABC9-29520DA78E90");
                        sheet.Name = "钢筋试验记录（线下）";
                        var cells = new string[] { "B40", "A39", "D37", "G37", "J37", "M37", "D36", "G36", "J36", "M36", "D35", "J35", "D34", "J34", "D33", "G33", "J33", "M33", "D32", "G32", "J32", "M32", "D31", "G31", "J31", "M31", "D30", "G30", "J30", "M30", "D29", "G29", "J29", "M29", "D28", "G28", "J28", "M28", "D27", "G27", "J27", "M27", "D26", "G26", "J26", "M26", "D25", "J25", "D24", "J24", "D23", "J23" };
                        this.UpdateCell(document, sheet, sheet.ID, 10, cells);

                        DeleteSheetCell(ref s, "D23", "G23", "J23", "M23", "O23", "D24", "G24", "J24", "M24", "O24", "D25", "G32", "D32", "G32", "J32", "M32");
                    }

                    //钢筋试验报告
                    if (sheet.ID == new Guid("A982544A-A2DA-4224-A4F3-FC7D5DA0A114"))
                    {
                        sheet.ID = new Guid("8A08B511-163E-4E52-A53B-754C572A5112");
                        sheet.Name = "钢筋试验报告（线  下）";
                        var cells = new string[] { "B31", "E31", "I31", "N31", "A28", "I28", "D26", "F26", "I26", "L26", "N26", "D25", "F25", "I25", "L25", "N25", "D24", "F24", "L24", "D23", "F23", "L23", "D22", "F22", "I22", "L22", "N22", "D21", "F21", "I21", "L21", "N21", "D20", "F20", "I20", "L20", "N20", "D19", "F19", "I19", "L19", "N19", "D18", "F18", "I18", "L18", "N18", "D17", "F17", "L17", "D16", "F16", "L16", "D15", "F15", "L15" };
                        this.UpdateCell(document, sheet, sheet.ID, 8, cells);

                        DeleteSheetCell(ref s, "D15", "F15", "I15", "K15", "M15", "O15", "D16", "F16", "D20", "F20", "I20", "K20", "M20", "O20", "D21", "F21", "I21", "K21", "M21", "O21", "D22", "F22", "I22", "K22", "M22", "O22");
                    }

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(document).Replace("'", "''");
                    sql = "UPDATE dbo.sys_document SET Data = @Data WHERE ID = @ID";
                    SqlCommand cmd1 = new SqlCommand(sql);
                    cmd1.Parameters.Add(new SqlParameter("@ID", documentId));
                    cmd1.Parameters.Add(new SqlParameter("@Data", json));
                    ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd1 });
                }
            }
        }

        private void UpdateCell(JZDocument document, JZSheet sheet, Guid sheetID, int offset, params string[] oldcells)
        {
            foreach (var cell in oldcells)
            {
                var newcell = cell[0].ToString() + (int.Parse(cell.Remove(0, 1)) + offset) + "";
                this.CreateSheetCell(ref sheet, newcell, JZCommonHelper.GetCellValue(document, sheetID, cell));
            }
        }

        public void MoveDocumentByModuleID5(String mid)
        {
            String sql = @"select ID,Data from sys_document where ModuleID='" + mid + "' and CreatedTime<='2014-05-26 13:00:00'";
            //String sql = @" SELECT ID,col_norm_D10 FROM dbo.biz_norm_试验人员档案";// WHERE SCPT LIKE '0001000100010002%'


            DataTable dt = GetDataTable(sql);
            if (dt != null)
            {
                Int32 i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    i++;
                    String id = row["ID"].ToString();
                    //string strSqlDoc = "select ID,Data from sys_document where ID='" + id + "'";
                    //DataTable dtDoc = GetDataTable(strSqlDoc);
                    //if (dtDoc != null && dtDoc.Rows.Count > 0)
                    //{
                    //    JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dtDoc.Rows[0]["Data"].ToString());
                    JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(row["Data"].ToString());
                    if (doc != null)
                    {
                        JZSheet s = null;
                        foreach (JZSheet sheet in doc.Sheets)
                        {
                            if (sheet.ID == new Guid("fb22ab5d-c579-4001-b0b3-f82ce72ea69b"))
                            {
                                s = sheet;
                                break;
                            }
                        }
                        if (s == null)
                        {
                            continue;
                        }

                        //CreateSheetCell(ref s, "D10", row["col_norm_D10"]);
                        //logger.Error(i.ToString() + " D10:" + JZCommonHelper.GetCellValue(doc, s.ID, "D10"));
                        CreateSheetCell(ref s, "B50", JZCommonHelper.GetCellValue(doc, s.ID, "B40"));
                        //CreateSheetCell(ref s, "E24", JZCommonHelper.GetCellValue(doc, s.ID, "E23"));
                        //CreateSheetCell(ref s, "E23", "/");

                        String json = Newtonsoft.Json.JsonConvert.SerializeObject(doc).Replace("'", "''");
                        sql = "UPDATE dbo.sys_document SET Data=@Data WHERE ID=@ID";
                        SqlCommand cmd1 = new SqlCommand(sql);
                        cmd1.Parameters.Add(new SqlParameter("@ID", id));
                        cmd1.Parameters.Add(new SqlParameter("@Data", json));
                        ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd1 });
                    }
                    //}
                }
            }
        }

        /// <summary>
        /// 创建表单中的单元格
        /// </summary>
        /// <param name="s"></param>
        /// <param name="CellName"></param>
        /// <param name="CellValue"></param>
        private void CreateSheetCell(ref JZSheet s, string CellName, object CellValue)
        {
            bool bHasCell = false;//是否存在单元格
            CellName = CellName.ToUpper();
            foreach (JZCell cell in s.Cells)
            {
                if (cell.Name.ToUpper() == CellName)
                {//存在单元格
                    bHasCell = true;
                    if (CellValue != null)
                    {//要赋的值不为空，则修改，否则不变
                        if (cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()))
                        {//如果原单元格不存在值，则修改
                            cell.Value = CellValue;
                            //logger.Error("存在单元格" + CellName + ",赋值：" + CellValue);
                        }
                        else
                        {
                            cell.Value = CellValue;
                            //logger.Error("存在单元格" + CellName + ",不赋值");
                            //logger.Error("存在单元格" + CellName + ",赋值：" + CellValue);
                        }
                    }
                    else
                    {
                        cell.Value = CellValue;
                    }
                    break;
                }
            }
            if (bHasCell == false)
            {//不存在单元格，则创建
                //logger.Error("不存在单元格" + CellName + "，则创建,值：" + CellValue);
                JZCell cell = new JZCell();
                cell.Name = CellName;
                cell.Value = CellValue;
                s.Cells.Add(cell);
            }
        }

        private void DeleteSheetCell(ref JZSheet s, params string[] CellName)
        {
            for (int i = 0; i < CellName.Length; i++)
            {
                DeleteSheetCell(ref s, CellName[i]);
            }
        }
        /// <summary>
        /// 删除表单中的单元格
        /// </summary>
        /// <param name="s"></param>
        /// <param name="CellName"></param>
        /// <param name="CellValue"></param>
        private void DeleteSheetCell(ref JZSheet s, string CellName)
        {
            CellName = CellName.ToUpper();
            foreach (JZCell cell in s.Cells)
            {
                if (cell.Name.ToUpper() == CellName)
                {//存在单元格
                    s.Cells.Remove(cell);
                    break;
                }
            }
        }

        public void MoveDocumentByModuleID3(String mid)
        {
            String sql = "select ID from sys_sheet ";
            DataTable dt = GetDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                logger.Error("progress 1: " + i.ToString() + "; ID=" + dt.Rows[i][0].ToString());
                sql = "select SheetXML from sys_sheet where id='" + dt.Rows[i][0].ToString() + "' ";
                DataTable dt2 = GetDataTable(sql);
                String source = dt2.Rows[0][0].ToString();
                if (source.StartsWith("<?"))
                {
                    logger.Error("progress 2: " + i.ToString() + "; ID=" + dt.Rows[i][0].ToString());
                    String xml = JZCommonHelper.GZipCompressString(source);
                    sql = String.Format("update sys_sheet set SheetXML='{0}' where ID='{1}'", xml, dt.Rows[i][0].ToString());
                    ExcuteCommand(sql);
                }
                dt2 = null;
            }
        }

        public void MoveDocumentByModuleID4(String mid)
        {
            String sql = @"select ID from sys_document where ModuleID='" + mid + "' ";
            //String sql = @" SELECT ID,col_norm_记录编号 FROM [dbo].[biz_norm_水泥试验记录（一）_1]";

            DataTable dt = GetDataTable(sql);
            if (dt != null)
            {
                Int32 i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    logger.Error("Delete Cell P35:" + i.ToString());
                    i++;
                    String id = row["ID"].ToString();
                    string strSqlDoc = "select ID,Data from sys_document where ID='" + id + "'";
                    DataTable dtDoc = GetDataTable(strSqlDoc);
                    if (dtDoc != null && dtDoc.Rows.Count > 0)
                    {
                        JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dtDoc.Rows[0]["Data"].ToString());
                        //JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(row["Data"].ToString());
                        if (doc != null)
                        {
                            JZSheet s = null;
                            foreach (JZSheet sheet in doc.Sheets)
                            {
                                if (sheet.ID == new Guid("0843E6D9-BE66-4775-9750-15F222FC82AE"))
                                {
                                    s = sheet;
                                    break;
                                }
                            }
                            if (s == null)
                            {
                                continue;
                            }

                            DeleteSheetCell(ref s, "P35");
                            //CreateSheetCell(ref s, "E25", JZCommonHelper.GetCellValue(doc, s.ID, "E24"));
                            //CreateSheetCell(ref s, "E24", JZCommonHelper.GetCellValue(doc, s.ID, "E23"));
                            //CreateSheetCell(ref s, "E23", "/");

                            String json = Newtonsoft.Json.JsonConvert.SerializeObject(doc).Replace("'", "''");
                            sql = "UPDATE dbo.sys_document SET Data=@Data WHERE ID=@ID";
                            SqlCommand cmd1 = new SqlCommand(sql);
                            cmd1.Parameters.Add(new SqlParameter("@ID", id));
                            cmd1.Parameters.Add(new SqlParameter("@Data", json));
                            ExcuteCommandsWithTransaction(new List<IDbCommand>() { cmd1 });
                        }
                    }
                }
            }
        }

        public void MoveSheetCellLogic()
        {
            String sql = "select ID from sys_sheet where CellLogic='' ";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                ModuleHelper mh = new ModuleHelper();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    logger.Error(i + " start move sheet cell logic " + dt.Rows[i][0].ToString());
                    String sheetXML = JZCommonHelper.GZipDecompressString(mh.GetSheetXMLByID(new Guid(dt.Rows[i][0].ToString())));
                    SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), sheetXML, "SheetView") as SheetView;
                    List<CellLogic> cellLogicList = new List<CellLogic>();
                    if (SheetView != null)
                    {
                        Sys_Sheet sheetBase = mh.GetSheetItemByID(new Guid(dt.Rows[i][0].ToString()));


                        List<JZCell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZCell>>(sheetBase.SheetData);
                        foreach (JZCell cell in cells)
                        {
                            Cell c = SheetView.Cells[cell.Name];
                            CellLogic cl = new CellLogic();
                            cl.Name = cell.Name;
                            if (c != null)
                            {
                                JZCellProperty p = c.Tag as JZCellProperty;
                                if (p != null)
                                {
                                    cl.Description = p.Description;
                                    cl.IsKey = p.IsKey;
                                    cl.IsNotCopy = p.IsNotCopy;
                                    cl.IsNotNull = p.IsNotNull;
                                    cl.IsPingxing = p.IsPingxing;
                                    cl.IsReadOnly = p.IsReadOnly;
                                    cl.IsUnique = p.IsUnique;
                                }
                            }
                            cellLogicList.Add(cl);
                        }
                    }
                    String json = JZCommonHelper.GZipCompressString(Newtonsoft.Json.JsonConvert.SerializeObject(cellLogicList));
                    sql = "UPDATE dbo.sys_sheet SET CellLogic = '" + json + "' WHERE ID='" + dt.Rows[i][0].ToString() + "'";
                    ExcuteCommand(sql);
                    logger.Error("cell logic end");
                }
            }
        }

        public void MoveSheetFormulas()
        {
            String sql = "select ID from sys_sheet where  Formulas='' or Formulas is null ";//
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                ModuleHelper mh = new ModuleHelper();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    logger.Error(i + " start move sheet Formulas: " + dt.Rows[i][0].ToString());
                    String sheetXML = JZCommonHelper.GZipDecompressString(mh.GetSheetXMLByID(new Guid(dt.Rows[i][0].ToString())));
                    SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), sheetXML, "SheetView") as SheetView;
                    SheetView.LoadFormulas(false);
                    List<JZFormulaData> cellFormulasList = new List<JZFormulaData>();
                    if (SheetView != null)
                    {
                        for (int j = 0; j < SheetView.ColumnCount; j++)
                        {
                            for (int m = 0; m < SheetView.RowCount; m++)
                            {
                                Cell c = SheetView.Cells[m, j];
                                JZFormulaData cl = new JZFormulaData();
                                if (c != null)
                                {
                                    if (!string.IsNullOrEmpty(c.Formula))
                                    {
                                        //logger.Error(string.Format("Name:{0} ColumnIndex:{1} RowIndex:{2} Formula:{3} ", ((char)('A' + c.Column.Index)).ToString() + (c.Row.Index + 1).ToString(), c.Column.Index, c.Row.Index, c.Formula));
                                        cl.ColumnIndex = c.Column.Index;
                                        cl.RowIndex = c.Row.Index;
                                        cl.Formula = c.Formula;
                                        cellFormulasList.Add(cl);
                                    }
                                }

                            }
                        }
                    }
                    String json = JZCommonHelper.GZipCompressString(Newtonsoft.Json.JsonConvert.SerializeObject(cellFormulasList));
                    sql = "UPDATE dbo.sys_sheet SET Formulas = '" + json + "' WHERE ID='" + dt.Rows[i][0].ToString() + "'";
                    ExcuteCommand(sql);
                    logger.Error("Formulas end");
                }
            }
        }

        public void GenerateCellLogic()
        {
            String sql = "select CellLogic,Name from sys_sheet";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                ModuleHelper mh = new ModuleHelper();
                using (StreamWriter outfile = new StreamWriter(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), "CellLogic.txt")))//"c:\CellLogic.txt"
                {


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        List<CellLogic> cellLogicList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CellLogic>>(
                          JZCommonHelper.GZipDecompressString(dt.Rows[i][0].ToString()));
                        String name = dt.Rows[i][1].ToString();
                        foreach (var item in cellLogicList)
                        {
                            if (item.IsKey && String.IsNullOrEmpty(item.Description))
                            {
                                outfile.WriteLine(name + "----" + item.Name);
                            }
                        }

                    }
                }
            }
        }


    }
}
