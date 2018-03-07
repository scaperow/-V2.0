using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Yqun.BO;
using Yqun.Data.AccessHelper;

namespace ShuXianCaiJiComponents
{
    public class SqlLocalHlper : OleDbDataHelper
    {
        /// <summary>
        /// 获取本地未上传数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetLocalData(string SqlStr)
        {
            try
            {
                return GetDataTable(SqlStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public bool Execute(string sqlStr)
        {
            try
            {
                return ExcuteCommand(sqlStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
