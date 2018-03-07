using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BizCommon;
using Yqun.Data.DataBase;
using System.Reflection;

namespace Yqun.BO.BusinessManager
{
    public class FieldDefineInfoManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<FieldDefineInfo> GetFieldDefineInfos(TableDefineInfo TableInfo)
        {
            List<FieldDefineInfo> FieldInfos = new List<FieldDefineInfo>();
            if (TableInfo != null)
            {
                StringBuilder Sql_Select = new StringBuilder();
                Sql_Select.Append("Select * from sys_Columns where TableName ='");
                Sql_Select.Append(TableInfo.Name);
                Sql_Select.Append("' and (Scdel IS NULL or Scdel=0) order by scts");//增加查询条件 (Scdel IS NULL or Scdel=0) 2013-10-15

                DataTable Data = GetDataTable(Sql_Select.ToString());
                foreach (DataRow row in Data.Rows)
                {
                    FieldDefineInfo FieldInfo = new FieldDefineInfo(TableInfo);
                    FieldInfo.Index = row["ID"].ToString();
                    FieldInfo.Description = row["DESCRIPTION"].ToString();
                    FieldInfo.FieldName = row["COLNAME"].ToString();
                    FieldInfo.IsKeyField = Convert.ToBoolean(row["IsKeyField"]);

                    String scpt = row["scpt"].ToString();
                    FieldInfo.IsNotNull = ((scpt.Length > 0 ? scpt.Substring(0, 1) : "0") == "1"? true:false);
                    FieldInfo.IsNotCopy = ((scpt.Length > 1 ? scpt.Substring(1, 1) : "0") == "1" ? true : false);
                    FieldInfo.IsPingxing = ((scpt.Length > 2 ? scpt.Substring(2, 1) : "0") == "1" ? true : false);
                    FieldInfo.IsReadOnly = ((scpt.Length > 3 ? scpt.Substring(3, 1) : "0") == "1" ? true : false);

                    String ColType = row["COLType"].ToString();
                    FieldInfo.FieldType = FieldType.GetFieldType(ColType);
                    FieldInfos.Add(FieldInfo);
                }
            }

            return FieldInfos;
        } 

        public bool NewFieldDefineInfo(String TableName,FieldDefineInfo Info)
        {
            if (Info == null)
                return false;

            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("Select ID,SCTS,SCPT,COLNAME,DESCRIPTION,COLTYPE,TABLENAME,IsKeyField,Scts_1,Scdel From sys_columns Where ID='");
            Sql_Select.Append(Info.Index);
            Sql_Select.Append("' and (Scdel IS NULL or Scdel=0) ");//增加查询条件 (Scdel IS NULL or Scdel=0) 2013-10-15

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null)
            {
                DataRow Row = Data.NewRow();
                Row["ID"] = Info.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["SCPT"] = string.Concat(Info.IsNotNull ? "1" : "0", Info.IsNotCopy ? "1" : "0", Info.IsPingxing ? "1" : "0", Info.IsReadOnly ? "1" : "0");
                Row["COLNAME"] = Info.FieldName;
                Row["DESCRIPTION"] = Info.Description;
                Row["COLTYPE"] = Info.FieldType.Description;
                Row["TABLENAME"] = TableName;
                Row["IsKeyField"] = Info.IsKeyField;

                Row["Scts_1"] = DateTime.Now.ToString();
                Row["Scdel"] = 0;
                
                Data.Rows.Add(Row);
            }

            Boolean Result = false;

            try
            {
                object r = Update(Data);
                Result = Result & (Convert.ToInt32(r) == 1);
            }
            catch (Exception ex)
            {
                logger.Error(String.Format("NewFieldDefineInfo(String TableName,FieldDefineInfo Info) 创建表信息‘sys_Columns’出错，原因：{0}", ex.Message));
                return false;
            }

            return Result;
        }

        public Boolean DeleteFieldDefineInfo(FieldDefineInfo Info)
        {
            if (Info == null)
                return false;

            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-15
            StringBuilder Sql_Delete = new StringBuilder();
            //Sql_Delete.Append("Delete From sys_Columns Where ID ='");
            //Sql_Delete.Append(Info.Index);
            //Sql_Delete.Append("'");
            Sql_Delete.Append("Update sys_Columns Set Scts_1=Getdate(),Scdel=1");
            Sql_Delete.Append(" Where ID='");
            Sql_Delete.Append(Info.Index);
            Sql_Delete.Append("'");

            Boolean Result = false;

            try
            {
                object r = ExcuteCommand(Sql_Delete.ToString());
                Result = (Convert.ToInt32(r) == 1);
            }
            catch (Exception ex)
            {
                logger.Error(String.Format("DeleteFieldDefineInfo(FieldDefineInfo Info) 删除表信息‘sys_Columns’出错，原因：{0}", ex.Message));
                return false;
            }

            return Result;
        }

        public Boolean UpdateFieldDefineInfo(String TableName,FieldDefineInfo Info)
        {
            if (Info == null)
                return false;

            StringBuilder Sql_Select = new StringBuilder();
            //增加字段Scts_1  2013-10-15
            Sql_Select.Append("Select ID,SCTS,SCPT,COLNAME,DESCRIPTION,COLTYPE,TABLENAME,IsKeyField,Scts_1 From sys_Columns Where ID='");
            Sql_Select.Append(Info.Index);
            Sql_Select.Append("' and (Scdel IS NULL or Scdel=0) ");//增加查询条件 (Scdel IS NULL or Scdel=0) 2013-10-15

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow Row = Data.Rows[0];
                Row["SCTS"] = DateTime.Now.ToString();
                Row["SCPT"] = string.Concat(Info.IsNotNull ? "1" : "0", Info.IsNotCopy ? "1" : "0", Info.IsPingxing ? "1" : "0", Info.IsReadOnly ? "1" : "0");
                Row["COLNAME"] = Info.FieldName;
                Row["DESCRIPTION"] = Info.Description;
                Row["COLTYPE"] = Info.FieldType.Description;
                Row["TABLENAME"] = TableName;
                Row["IsKeyField"] = Info.IsKeyField;
                
                Row["Scts_1"] = DateTime.Now.ToString();
            }
            else
            {
                DataRow Row = Data.NewRow();
                Row["ID"] = Info.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["SCPT"] = string.Concat(Info.IsNotNull ? "1" : "0", Info.IsNotCopy ? "1" : "0", Info.IsPingxing ? "1" : "0", Info.IsReadOnly ? "1" : "0");
                Row["COLNAME"] = Info.FieldName;
                Row["DESCRIPTION"] = Info.Description;
                Row["COLTYPE"] = Info.FieldType.Description;
                Row["TABLENAME"] = TableName;
                Row["IsKeyField"] = Info.IsKeyField;
                //增加字段Scts_1  2013-10-15
                Row["Scts_1"] = DateTime.Now.ToString();
                Data.Rows.Add(Row);
            }

            Boolean Result = false;

            try
            {
                object r = Update(Data);
                Result = Result & (Convert.ToInt32(r) == 1);
            }
            catch (Exception ex)
            {
                logger.Error(String.Format("UpdateFieldDefineInfo(String TableName,FieldDefineInfo Info) 更新表‘sys_Columns’出错，原因：{0}", ex.Message));
                return false;
            }

            return Result;
        }
    }
}
