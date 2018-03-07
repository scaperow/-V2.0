using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Common.ContextCache;

namespace Yqun.BO.BusinessManager
{
    public class PXJZDataManager : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private DataTable GetInvalidExt(DateTime start, DateTime end, int type)
        {
            var helper = new TestRoomCodeHelper();
            var codes = helper.GetValidTestRoomCodeList();
            var result = new DataTable();

            foreach (var code in codes)
            {
                var sql = string.Format(@" EXEC dbo.sp_pxjz_report @testcode = '{0}'
                                    , @ftype = {1}
                                    , @startdate = '{2}'
                                    , @enddate = '{3}'
                                    , @pageSize = 9999
                                    , @page = 1
                                    , @fldSort = '1'
                                    , @Sort = 0
                                    , @pageCount = 9999
                                    , @Counts = 0", code, type, start, end);

                var table = GetDataTable(sql);
                if (table != null)
                {
                    result.Merge(table, true, MissingSchemaAction.Add);
                }
            }

            return result;
        }

        public DataTable GetInvalidJZ(DateTime start, DateTime end)
        {
            return GetInvalidExt(start, end, 2);
        }

        public DataTable GetInvalidPX(DateTime start, DateTime end)
        {
            return GetInvalidExt(start, end, 1);
        }

        public DataTable GetPXReportRelation(String PXTestRoomCode, Guid ModuleID, DateTime StartTime, DateTime EndTime, string BGBH)
        {
            BGBH = BGBH.Replace("'", "");
            String sql = string.Format(@"SELECT sgc.标段名称 AS 施工标段,sgc.单位名称 AS 施工单位,sgc.试验室名称 AS 施工试验室,pxd.WTBH AS 平行委托编号,pxd.BGBH AS 平行报告编号,pxd.BGRQ AS 平行报告日期,sgd.WTBH AS 施工委托编号,sgd.BGBH AS 施工报告编号,sgd.BGRQ AS 施工报告日期,r.PXTime AS 平行时间 FROM dbo.sys_document pxd
INNER JOIN dbo.sys_px_relation r ON pxd.ID = r.PXDataID AND pxd.ModuleID='{1}' AND pxd.TestRoomCode='{0}'
INNER JOIN dbo.sys_document sgd ON r.SGDataID=sgd.ID AND sgd.ModuleID='{1}'
INNER JOIN dbo.v_bs_codeName sgc ON sgd.TestRoomCode=sgc.试验室编码
WHERE sgd.BGRQ>='{2}' AND sgd.BGRQ<'{3}' and (sgd.BGBH like '%{4}%' or pxd.BGBH like '%{4}%')", PXTestRoomCode, ModuleID, StartTime, EndTime, BGBH);
            DataTable dt = new DataTable();
            dt = GetDataTable(sql);
            return dt;
        }


        public DataTable GetPXJZReportInfo(String segment, String company, String testroom, DateTime Start, DateTime End, Int32 fTpe)
        {
            String sql = "";
            DataTable dt = new DataTable();
            if (!String.IsNullOrEmpty(testroom))
            {
                sql = String.Format(@" EXEC dbo.sp_pxjz_report @testcode = '{0}', @ftype = {1}, @startdate = '{2}', @enddate = '{3}', @pageSize=9999, @page=1, @fldSort='segment', @Sort=0, @pageCount=9999,@Counts=0",
                    testroom,
                    fTpe,
                    Start.ToString("yyyy-MM-dd"),
                    End.AddDays(1).ToString("yyyy-MM-dd"));
                //logger.Error(sql);
                dt = GetDataTable(sql);
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
                int j = 0;
                foreach (var item in real2)
                {
                    if (item.Length == 16)
                    {
                        sql = String.Format(@" EXEC dbo.sp_pxjz_report @testcode = '{0}',  @ftype = {1}, @startdate = '{2}', @enddate = '{3}', @pageSize=9999, @page=1, @fldSort='segment', @Sort=0, @pageCount=9999,@Counts=0",
                        item,
                        fTpe,
                        Start.ToString("yyyy-MM-dd"),
                        End.AddDays(1).ToString("yyyy-MM-dd"));

                        DataTable Data = GetDataTable(sql);
                        //logger.Error(sql);
                        if (j == 0)
                        {
                            dt = Data.Clone();
                            j++;
                        }
                        foreach (DataRow row in Data.Rows)
                        {
                            dt.ImportRow(row);
                        }
                    }
                }


            }

            return dt;
        }

        public DataTable GetPXJZReportChart(String testRoomID, DateTime start, DateTime end, String modelID)
        {
            String sql = "";
            DataTable dt = new DataTable();
            if (!String.IsNullOrEmpty(modelID))
            {
                sql = String.Format(@" EXEC dbo.sp_px_chart @testRoomID = '{0}', @start = '{1}', @end = '{2}',@modelID='{3}'",
                    testRoomID,
                    start.ToString("yyyy-MM-dd"),
                    end.AddDays(1).ToString("yyyy-MM-dd"), modelID);
                dt = GetDataTable(sql);
            }
            return dt;
        }

        public void NewModel(String modelName, String modelCode, String modelID)
        {
            String sql = string.Format("EXEC dbo.sp_newPXJZ_ByNewModel @modelID = '{0}', @modelCode = '{1}', @modelName = '{2}' ",
                modelID, modelCode, modelName);
            ExcuteCommand(sql);
        }

        public void RenameModel(String newName, String modelID)
        {
            String sql = String.Format("UPDATE dbo.sys_biz_reminder_Itemfrequency SET ModelName='{0}' WHERE ModelIndex='{1}'",
                newName, modelID);
            ExcuteCommand(sql);
        }

        public void DeleteModel(String modelID)
        {
            String sql = String.Format("DELETE dbo.sys_biz_reminder_Itemfrequency WHERE ModelIndex='{0}' ", modelID);
            ExcuteCommand(sql);
        }

        public void DeleteByTestRoomCode(String code)
        {
            String sql = String.Format("DELETE dbo.sys_biz_reminder_Itemfrequency WHERE TestRoomCode='{0}' ", code);
            ExcuteCommand(sql);
        }

        public void NewByTestRoom(String testRoomCode, String testRoomID)
        {
            String sql = String.Format(@"INSERT INTO dbo.sys_biz_reminder_Itemfrequency ( ModelIndex ,ModelCode,TestRoomID ,TestRoomCode,ModelName ,
              Frequency ,
              FrequencyType ,
              IsActive
            )
        SELECT ID, CatlogCode,'{0}','{1}',Description,0,1,0 FROM dbo.sys_module", testRoomID, testRoomCode);
            ExcuteCommand(sql);

            sql = String.Format(@"INSERT INTO dbo.sys_biz_reminder_Itemfrequency ( ModelIndex ,ModelCode,TestRoomID ,TestRoomCode,ModelName ,
              Frequency ,
              FrequencyType ,
              IsActive
            )
        SELECT ID, CatlogCode,'{0}','{1}',Description,0,2,0 FROM dbo.sys_module", testRoomID, testRoomCode);
            ExcuteCommand(sql);
        }

        /// <summary>
        /// 获取实验标准数据集合
        /// </summary>
        /// <param name="type">实验类型</param>
        /// <returns>DateTable数据集</returns>
        public DataTable GetWitnessRateInfo(string type)
        {
            try
            {
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("select a.ID,b.Description,a.IsActive,a.Frequency from dbo.sys_pxjz_frequency as a");
                sbSql.Append(" join dbo.sys_module as b  on a.ModuleID=b.ID");
                sbSql.AppendFormat(" where a.FrequencyType={0} order by b.Description", type);
                return GetDataTable(sbSql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新表
        /// </summary>
        /// <param name="dt">更新的数据集</param>
        /// <returns>true更新成功，false更新失败</returns>
        public bool SetWinessRateInfo(DataTable dt)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string temp = string.Format("update sys_pxjz_frequency set IsActive={0},Frequency={1}  where ID='{2}' ", dt.Rows[i]["IsActive"].ToString(), dt.Rows[i]["Frequency"].ToString(), dt.Rows[i]["ID"].ToString());
                    ExcuteCommand(temp);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据报告编号或委托编号查询可被平行的报告编号
        /// </summary>
        /// <param name="BGBH"></param>
        /// <returns></returns>
        public DataTable GetEnPXedReport(Guid ModuleID, String BGBH)
        {
            BGBH = BGBH.Replace("'", "");
            String sql = @"SELECT d.ID,
d.ModuleID,
f.标段名称,
f.单位名称,
f.试验室名称,
d.DataName AS 名称,
d.TryType AS  试验类型,
d.WTBH as 委托编号,
d.BGBH as 报告编号,
d.BGRQ as 报告日期 FROM dbo.sys_document d
INNER JOIN dbo.sys_engs_Tree t ON d.CompanyCode=t.NodeCode
INNER JOIN dbo.sys_engs_CompanyInfo c ON t.RalationID=c.ID AND c.DepType='@unit_监理单位' AND c.Scdel=0 AND t.Scdel=0
JOIN dbo.v_bs_codeName f ON d.TestRoomCode=f.试验室编码
WHERE d.ModuleID='" + ModuleID + "' and d.ID NOT IN(SELECT PXDataID FROM dbo.sys_px_relation ) AND (d.WTBH like '%" + BGBH + "%' OR d.BGBH LIKE '%" + BGBH + "%')";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 生成平行对应关系
        /// </summary>
        /// <returns></returns>
        public int GeneratePXRelation(Guid SGDataID, Guid PXDataID)
        {
            int flag = 0;
            try
            {

                String sql = String.Format(@"INSERT INTO dbo.sys_px_relation( SGDataID, PXDataID, PXTime )VALUES  ( '{0}', '{1}',GETDATE());UPDATE dbo.sys_document SET TryType = '平行' WHERE ID='{1}'", SGDataID, PXDataID);
                int i = ExcuteCommand(sql);
                flag = 1;
            }
            catch (Exception ex)
            {
                logger.Error("GeneratePXRelation error:" + ex.Message);
            }
            return flag;
        }
        /// <summary>
        /// 删除平行对应关系
        /// </summary>
        /// <returns></returns>
        public int DeletePXRelation(Guid PXDataID)
        {
            int flag = 0;
            try
            {
                String sql = string.Format("UPDATE dbo.sys_document SET TryType = '抽检' WHERE ID='{0}';delete from sys_px_relation where PXDataID='{0}'", PXDataID);
                int i = ExcuteCommand(sql);
                flag = 1;
            }
            catch (Exception ex)
            {
                logger.Error("DeletePXRelation error:" + ex.Message);
            }
            return flag;
        }
    }
}
