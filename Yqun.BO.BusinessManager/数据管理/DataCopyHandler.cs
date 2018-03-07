using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;

namespace Yqun.BO.BusinessManager
{
    public class DataCopyHandler : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 将试验报告中的报告日期复制到表外数据表的BGRQ字段中
        /// </summary>
        public void Function(List<string> IDList, String ModelIndex, DataSet Data)
        {
            if (IDList.Count == 0)
                return;

            ThreadParameter p = new ThreadParameter();
            p.ModelIndex = ModelIndex;
            p.Data = Data;

            ThreadPool.QueueUserWorkItem(new WaitCallback(Execute), p);
        }

        private void Execute(object paremeter)
        {
            logger.Error("DataCopyHandler::Function");

            Boolean Result = false;

            ThreadParameter p = paremeter as ThreadParameter;
            String ModelIndex = p.ModelIndex;
            DataSet Data = p.Data;

            StringBuilder sql_Select = new StringBuilder();
            sql_Select.Append("Select TableName,Contents from sys_moduleview where ModuleID ='");
            sql_Select.Append(ModelIndex);
            sql_Select.Append("' and Description='报告日期'");

            DataTable Table = GetDataTable(sql_Select.ToString());
            if (Table != null && Table.Rows.Count > 0)
            {
                DataRow Row = Table.Rows[0];

                String TableName = Row["TableName"].ToString();
                String FieldName = Row["Contents"].ToString();

                DataTable TempData = Data.Tables[string.Format("[{0}]", TableName)];
                String DataID = TempData.Rows[0]["ID"].ToString();
                object ReportDate = TempData.Rows[0][FieldName].ToString();

                try
                {
                    StringBuilder sql_Update = new StringBuilder();
                    sql_Update.Append("Update ");
                    sql_Update.Append(string.Format("[biz_norm_extent_{0}]", ModelIndex));
                    sql_Update.Append(" set BGRQ='");
                    sql_Update.Append(ReportDate);
                    sql_Update.Append("' where ID='");
                    sql_Update.Append(DataID);
                    sql_Update.Append("'");

                    object r = ExcuteCommand(sql_Update.ToString());
                    Result = (Convert.ToInt32(r) == 1);
                }
                catch(Exception ex)
                {
                    logger.Error("DataCopyHandler::Function出错，原因是" + ex.Message);
                }

                String Msg = (Result? "复制报告日期成功。":"复制报告日期失败");
                logger.Error("DataCopyHandler::Function消息：" + Msg);
            }
            else
            {
                logger.Error(string.Format("模板ID‘{0}’在 sys_moduleview 中未找到“报告日期”显示项。", ModelIndex));
            }
        }

        private class ThreadParameter
        {
            public String ModelIndex;
            public DataSet Data;
        }
    }
}
