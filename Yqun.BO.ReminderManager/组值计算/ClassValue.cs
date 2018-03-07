using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using System.Data;
using BizFunctionInfos;
using FarPoint.CalcEngine;
using Common.Logging;

namespace Yqun.BO.ReminderManager
{
    public class ClassValue:BOBase,IJob
    {
        private static ILog _log = LogManager.GetLogger(typeof(ClassValue));
        private bool isExcute = false;

        StringBuilder sbStr = new StringBuilder();
        StringBuilder sbReportStrItem = new StringBuilder();
        string sqlStr = string.Empty;

        #region IJob 成员

        /// <summary>
        /// 任务调度执行方法
        /// </summary>
        /// <param name="context"></param>
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                if (context.JobDetail.JobDataMap.GetString("isExcute") == "0")
                {
                    isExcute = false;
                }
                else
                {
                    isExcute = true;
                }
                _log.Info("ClassValue Start....");
                SetValue("col_norm_K6");
                SetValue("col_norm_K9");
                SetValue("col_norm_K12");
                SetValue("col_norm_K15");
                SetValue("col_norm_K18");
                _log.Info("ClassValue End....");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion




        #region 自定方法

        /// <summary>
        /// 获取组值为'/'的集合
        /// </summary>
        /// <param name="columnName">要查看的列明</param>
        /// <returns>返回集合</returns>
        private DataTable GetAllNull(string columnName)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("SELECT ID,{0} FROM dbo.[biz_norm_混凝土试件抗压强度试验记录] WHERE {1}='/' ", columnName, columnName);
            switch (columnName.ToUpper())
            {
                case "COL_NORM_K6":
                    {
                        sbStr.Append(" and COL_NORM_I6<>'/' and COL_NORM_I7<>'/' and COL_NORM_I8<>'/' ");
                        break;
                    }
                case "COL_NORM_K9":
                    {
                        sbStr.Append(" and COL_NORM_I9<>'/' and COL_NORM_I10<>'/' and COL_NORM_I11<>'/' ");
                        break;
                    }
                case "COL_NORM_K12":
                    {
                        sbStr.Append(" and COL_NORM_I12<>'/'  and COL_NORM_I13<>'/'  and COL_NORM_I14<>'/'  ");
                        break;
                    }
                case "COL_NORM_K15":
                    {
                        sbStr.Append(" and COL_NORM_I15<>'/'  and COL_NORM_I16<>'/'  and COL_NORM_I17<>'/'  ");
                        break;
                    }
                case "COL_NORM_K18":
                    {
                        sbStr.Append(" and COL_NORM_I18<>'/'  and COL_NORM_I19<>'/'  and COL_NORM_I20<>'/'  ");
                        break;
                    }
            }
            _log.Info(sbStr.ToString());
            return GetDataTable(sbStr.ToString());
        }

        /// <summary>
        /// 获取组值的项
        /// </summary>
        /// <param name="cell">组值单元格名称</param>
        /// <param name="id">当前组值ID</param>
        /// <returns>返回项</returns>
        private object[] GetItem(string cell, string id)
        {
            sbStr.Remove(0, sbStr.Length);
            sbReportStrItem.Remove(0, sbReportStrItem.Length);

            sbStr.Append("SELECT ");
            sbReportStrItem.Append("update ");
            switch (cell.ToUpper())
            {
                case "COL_NORM_K6":
                    {
                        sbStr.Append("COL_NORM_I6,COL_NORM_I7,COL_NORM_I8 ");
                        sbReportStrItem.Append("dbo.[biz_norm_混凝土检查试件抗压强度试验报告三级配] set col_norm_p34='{0}',col_norm_p35='{1}',col_norm_p36='{2}',col_norm_s34='{3}' ");
                        break;
                    }
                case "COL_NORM_K9":
                    {
                        sbStr.Append("COL_NORM_I9,COL_NORM_I10,COL_NORM_I11 ");
                        sbReportStrItem.Append("dbo.[biz_norm_混凝土检查试件抗压强度试验报告三级配] set col_norm_p37='{0}',col_norm_p38='{1}',col_norm_p39='{2}',col_norm_s37='{3}' ");
                        break;
                    }
                case "COL_NORM_K12":
                    {
                        sbStr.Append("COL_NORM_I12,COL_NORM_I13,COL_NORM_I14 ");
                        sbReportStrItem.Append("dbo.[biz_norm_混凝土检查试件抗压强度试验报告三级配] set col_norm_p40='{0}',col_norm_p41='{1}',col_norm_p42='{2}',col_norm_s40='{3}' ");
                        break;
                    }
                case "COL_NORM_K15":
                    {
                        sbStr.Append("COL_NORM_I15,COL_NORM_I16,COL_NORM_I17 ");
                        sbReportStrItem.Append("dbo.[biz_norm_混凝土检查试件抗压强度试验报告三级配] set col_norm_p43='{0}',col_norm_p44='{1}',col_norm_p45='{2}',col_norm_s43='{3}' ");
                        break;
                    }
                case "COL_NORM_K18":
                    {
                        sbStr.Append("COL_NORM_I18,COL_NORM_I19,COL_NORM_I20 ");
                        break;
                    }
            }
            sbStr.AppendFormat("FROM dbo.[biz_norm_混凝土试件抗压强度试验记录] WHERE id='{0}'", id);
            sbReportStrItem.Append(" where id='{4}'");
            _log.Info(sbStr.ToString());
            DataTable dt = GetDataTable(sbStr.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() != "/" && dt.Rows[0][1].ToString() != "/" && dt.Rows[0][2].ToString() != "/")
                {
                    return new object[] { dt.Rows[0][0], (System.Object)dt.Rows[0][1], dt.Rows[0][2], 0.15 };
                }
            }
            return null;

        }
        /// <summary>
        /// 设置组值
        /// </summary>
        /// <param name="columnName">组值列名</param>
        /// <param name="value">值</param>
        /// <returns>true成功，false失败</returns>
        private bool SetNull(string columnName, string value, string id)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("update dbo.[biz_norm_混凝土试件抗压强度试验记录] set {0}='{1}' where id='{2}'", columnName, value, id);
            _log.Info(sbStr.ToString());
            _log.Info(sqlStr);
            if (isExcute)
            {
                if (ExcuteCommand(sbStr.ToString()) > 0)
                {
                    ExcuteCommand(sqlStr);
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// 获取计算结果值
        /// </summary>
        /// <param name="values">组值项</param>
        /// <returns></returns>
        private string GetValue(object[] values, string columnName, string id)
        {
            if (values != null)
            {
                NumModifyFunctionInfo numModifyFunctionInfo = new NumModifyFunctionInfo();
                TongThreeAvergeFunctionInfo tongThreeAvergeFunctionInfo = new TongThreeAvergeFunctionInfo();
                string temp = numModifyFunctionInfo.Evaluate(new object[] { Convert.ToDouble(tongThreeAvergeFunctionInfo.Evaluate(values)) * GetNum(columnName, id), -1, 0 }).ToString();
                sqlStr = string.Format(sbReportStrItem.ToString(), values[0].ToString(), values[1].ToString(), values[2].ToString(), temp, id);
                return temp;
            }
            return "/";
        }

        /// <summary>
        /// 设置单个组值
        /// </summary>
        /// <param name="columnName"></param>
        private void SetValue(string columnName)
        {
            DataTable dt = GetAllNull(columnName);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SetNull(columnName, GetValue(GetItem(columnName, dt.Rows[i]["id"].ToString()), columnName, dt.Rows[i]["id"].ToString()), dt.Rows[i]["id"].ToString());
                }
            }
        }

        /// <summary>
        /// 获取J列对应的值
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private double GetNum(string columnName, string id)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.Append("SELECT ");
            switch (columnName.ToUpper())
            {
                case "COL_NORM_K6":
                    {
                        sbStr.Append("COL_NORM_J6 ");
                        break;
                    }
                case "COL_NORM_K9":
                    {
                        sbStr.Append("COL_NORM_J9 ");
                        break;
                    }
                case "COL_NORM_K12":
                    {
                        sbStr.Append("COL_NORM_J12 ");
                        break;
                    }
                case "COL_NORM_K15":
                    {
                        sbStr.Append("COL_NORM_J15 ");
                        break;
                    }
                case "COL_NORM_K18":
                    {
                        sbStr.Append("COL_NORM_J18 ");
                        break;
                    }
            }
            sbStr.AppendFormat("FROM dbo.[biz_norm_混凝土试件抗压强度试验记录] WHERE id='{0}'", id);
            _log.Info(sbStr.ToString());
            DataTable dt = GetDataTable(sbStr.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToDouble(dt.Rows[0][0].ToString());
            }
            return 1.0;
        }

        #endregion
    }
}
