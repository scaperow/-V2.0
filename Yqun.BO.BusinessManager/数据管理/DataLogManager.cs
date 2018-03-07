using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Common.ContextCache;

namespace Yqun.BO.BusinessManager
{
    public class DataLogManager : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable GetLoginLogInfo(String segment, String company, String testroom, DateTime Start, DateTime End, String username, int pageindex, int PageSize, int doCount)
        {
//            @tblname VARCHAR(255), -- 表名
//@strGetFields nvarchar(1000) = "*", -- 需要返回的列
//@fldName varchar(255)='', -- 排序的字段名
//@PageSize int = 10, -- 页尺寸
//@PageIndex int = 1, -- 页码
//@doCount bit = 0, -- 返回, 非0 值则返回记录总数
//@OrderType bit = 0, -- 设置排序类型, 非0 值则降序
            //@strWhere varchar(1500) = '' -- 查询条件(注意: 不要加where)

            #region 显示字段
            string fileds = " * ";
            ApplicationContext AppContext = ApplicationContext.Current;
            if (AppContext.IsAdministrator)
            {
                fileds = @" UserName as 用户 ,
                ipAddress as IP地址,
                macAddress as 物理地址,
                machineName as 机器名,
                osVersion as 操作系统,
                osUserName as 系统账户,
                ProjectName as 项目,
                SegmentName as 标段,
                CompanyName as 单位,
                TestRoomName as 试验室,
                FirstAccessTime as 登录时间,
                LastAccessTime as 退出时间 ";
            }
            else
            {
                fileds = @" UserName as 用户 ,
                ProjectName as 项目 ,
                SegmentName as 标段,
                CompanyName as 单位,
                TestRoomName as 试验室,
                FirstAccessTime as 登录时间,
                LastAccessTime as 退出时间 ";
            }

            #endregion

            #region 查询条件
            String sql = String.Format(@" and FirstAccessTime>=''{0}'' AND FirstAccessTime<''{1}''",
               Start.ToString("yyyy-MM-dd"),
               End.AddDays(1).ToString("yyyy-MM-dd"));

            if (testroom != "")
            {
                sql += " AND TestRoomCode=''" + testroom + "'' ";
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
                sql += " AND TestRoomCode in (''" + String.Join("'',''", real2.ToArray()) + "'') ";
            }
            if (!String.IsNullOrEmpty(username))
            {
                sql += " And UserName like ''%" + username + "%'' ";
            }
            #endregion


            String sql_select = "";
            DataTable dt = new DataTable();

            sql_select = String.Format(@" EXEC dbo.sp_pager @tblname = '{0}', @strGetFields = '{1}', @fldName = '{2}', @PageSize = {3}, @PageIndex = {4},@doCount={5},@OrderType={6},@strWhere='{7}'",
                    "sys_loginlog",
                    fileds,
                    "FirstAccessTime",
                    PageSize,
                    pageindex,
                    doCount,
                    1, sql);
                //logger.Error("Login_QQ"+doCount.ToString()+":" + sql_select);
                dt = GetDataTable(sql_select);
         
            return dt;
        }

        public DataTable GetOperateLogInfo(String segment, String company, String testroom, DateTime Start, DateTime End, String username, int pageindex, int PageSize, int doCount)
        {

            #region 显示字段
            string fileds = " * ";
            fileds = @" ID,
            modifiedby AS 用户,
                modifiedDate AS 操作日期,
                optType AS 操作类型,
                modelIndex ,
                modelCode ,
                dataID ,
                segmentName AS 标段,
                companyName AS 单位,
                testRoom AS 试验室,
                modelName AS 模板,
                reportName AS 报告名称,
                reportNumber AS 报告编号,
                modifyItem,
                comment ";
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
                sql += " AND TestRoomCode in (''" + String.Join("'',''", real2.ToArray()) + "'') ";
            }

            ApplicationContext AppContext = ApplicationContext.Current;
            if (!AppContext.IsAdministrator)
            {
                sql += " AND modifiedby not in (''developer'') ";
            }
            if (!String.IsNullOrEmpty(username))
            {
                sql += " And modifiedby like ''%" + username + "%'' ";
            }
            #endregion

            String sql_select = "";
            DataTable dt = new DataTable();

            sql_select = String.Format(@" EXEC dbo.sp_pager @tblname = '{0}', @strGetFields = '{1}', @fldName = '{2}', @PageSize = {3}, @PageIndex = {4},@doCount={5},@OrderType={6},@strWhere='{7}'",
                    "sys_operatelog",
                    fileds,
                    "modifiedDate",
                    PageSize,
                    pageindex,
                    doCount,
                    1, sql);
            //logger.Error("Opear_QQ2:" + sql_select);
            dt = GetDataTable(sql_select);

            return dt;
        }


        public DataTable GetOperateLogByID(String id)
        {
            String sql = "SELECT ID,modifiedby,modifiedDate,modelIndex,modelCode,dataID,modifyItem FROM dbo.sys_operatelog where ID=" + id;
            DataTable Data = GetDataTable(sql);
            return Data;
        }

        public Int64 GetOperateLogIDByModifyID(String modifyID)
        {
            Int64 logid = 0;
            String sql = String.Format("SELECT ID FROM dbo.sys_operate_log WHERE requestID='{0}' ", modifyID);
            DataTable Data = GetDataTable(sql);
            if (Data != null && Data.Rows.Count > 0)
            {
                if (!Int64.TryParse(Data.Rows[0][0].ToString(), out logid))
                {
                    logid = 0;
                }
            }
            return logid;
        }
    }
}
