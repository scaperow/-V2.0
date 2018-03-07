using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;
using System.Reflection;

namespace Yqun.BO.BusinessManager
{
    public class DataAreaInfoManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Boolean NewDataAreaInfo(string SheetID, TableDefineInfo TableInfo)
        {
            if (string.IsNullOrEmpty(SheetID) || TableInfo == null)
                return false;

            Boolean Result = false;

            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("Select ID,SCTS,SheetID,TableName,ColumnName,Range,Scts_1,Scdel From sys_biz_DataArea Where SheetID='");
            Sql_Select.Append(SheetID);
            Sql_Select.Append("' and TableName='");
            Sql_Select.Append(TableInfo.Name);
            Sql_Select.Append("'");
            //增加查询条件 判断此条记录是否标记为已删除  Scdel=1 代表已删除。 2013-10-15
            Sql_Select.Append(" And (Scdel IS NULL or Scdel=0) ");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null)
            {
                foreach (FieldDefineInfo FieldInfo in TableInfo.FieldInfos)
                {
                    DataRow Row = Data.NewRow();
                    Row["ID"] = Guid.NewGuid().ToString();
                    Row["SCTS"] = DateTime.Now.ToString();
                    Row["SheetID"] = SheetID;
                    Row["TableName"] = TableInfo.Name;
                    Row["ColumnName"] = FieldInfo.FieldName;
                    Row["Range"] = FieldInfo.RangeInfo;

                    Row["Scts_1"] = DateTime.Now.ToString();
                    Row["Scdel"] = 0;
                    Data.Rows.Add(Row);
                }

                object r = Update(Data);
                Result = (Convert.ToInt32(r) == 1);
            }

            return Result;
        }

        public Boolean DeleteDataAreaInfo(string SheetID, TableDefineInfo TableInfo)
        {
            if (string.IsNullOrEmpty(SheetID) || TableInfo == null)
                return false;

            Boolean Result = false;

            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，便于数据同步   2013-10-15
            StringBuilder Sql_Delete = new StringBuilder();
            //Sql_Delete.Append("Delete From sys_biz_DataArea Where SheetID = '");
            //Sql_Delete.Append(SheetID);
            //Sql_Delete.Append("' and TableName='");
            //Sql_Delete.Append(TableInfo.Name);
            //Sql_Delete.Append("'");
            Sql_Delete.Append("Update sys_biz_DataArea Set Scts_1=Getdate(),Scdel=1");
            Sql_Delete.Append(" Where SheetID='");
            Sql_Delete.Append(SheetID);
            Sql_Delete.Append("' and TableName='");
            Sql_Delete.Append(TableInfo.Name);
            Sql_Delete.Append("'");

            object r = ExcuteCommand(Sql_Delete.ToString());
            Result = (Convert.ToInt32(r) == 1);

            return Result;
        }

        public Boolean UpdateDataAreaInfo(string SheetID, TableDefineInfo TableInfo)
        {
            if (string.IsNullOrEmpty(SheetID) || TableInfo == null)
                return false;

            Boolean Result = false;

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            Sql_Select.Append("Select ID,SCTS,SheetID,TableName,ColumnName,Range,Scts_1 From sys_biz_DataArea Where Scdel=0 and SheetID='");
            Sql_Select.Append(SheetID);
            Sql_Select.Append("' and TableName='");
            Sql_Select.Append(TableInfo.Name);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null)
            {
                foreach (FieldDefineInfo FieldInfo in TableInfo.FieldInfos)
                {
                    DataRow[] Rows = Data.Select("ColumnName='" + FieldInfo.FieldName + "'");
                    if (Rows.Length > 0)
                    {
                        DataRow Row = Rows[0];
                        Row["SCTS"] = DateTime.Now.ToString();
                        Row["SheetID"] = SheetID;
                        Row["TableName"] = TableInfo.Name;
                        Row["ColumnName"] = FieldInfo.FieldName;
                        Row["Range"] = FieldInfo.RangeInfo;
                        //增加字段Scts_1    2013-10-15
                        Row["Scts_1"] = DateTime.Now.ToString();
                    }
                    else
                    {
                        DataRow Row = Data.NewRow();
                        Row["ID"] = Guid.NewGuid().ToString();
                        Row["SCTS"] = DateTime.Now.ToString();
                        Row["SheetID"] = SheetID;
                        Row["TableName"] = TableInfo.Name;
                        Row["ColumnName"] = FieldInfo.FieldName;
                        Row["Range"] = FieldInfo.RangeInfo;
                        //增加字段Scts_1    2013-10-15
                        Row["Scts_1"] = DateTime.Now.ToString();
                        Data.Rows.Add(Row);
                    }
                }

                try
                {
                    object r = Update(Data);
                    Result = (Convert.ToInt32(r) == 1);
                }
                catch (Exception ex)
                {
                    logger.Error(String.Format("UpdateDataAreaInfo(string SheetID, TableDefineInfo TableInfo) 更新表‘sys_biz_DataArea’出错，原因：{0}", ex.Message));
                    return false;
                }
            }

            return Result;
        }
    }
}
