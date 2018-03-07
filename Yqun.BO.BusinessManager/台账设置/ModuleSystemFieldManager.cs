using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BizCommon;
using System.Drawing;
using System.Reflection;

namespace Yqun.BO.BusinessManager
{
    public class ModuleSystemFieldManager : BOBase
    {
        public Boolean NewModuleSystemField(String ModuleID, String ModuleCode, ModuleField moduleField)
        {
            Boolean Result = false;
            //增加查询条件 Scdel=0      2013-10-17
            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("Select * From sys_moduleview where ModuleID='");
            Sql_Select.Append(ModuleID);
            Sql_Select.Append("' and Scdel=0 and ID = '");
            Sql_Select.Append(moduleField.Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count == 0)
            {
                DataRow Row = Data.NewRow();
                Row["ID"] = moduleField.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["ModuleID"] = moduleField.ModuleIndex;
                Row["Description"] = moduleField.Description;
                Row["TableName"] = moduleField.TableName;
                Row["TableText"] = moduleField.TableText;
                Row["ContentType"] = moduleField.ContentType.ToString();
                Row["ContentFieldType"] = moduleField.ContentFieldType;
                Row["ContentText"] = moduleField.ContentText;
                Row["Contents"] = moduleField.Contents;
                Row["ForeColor"] = moduleField.ForeColor.ToArgb();
                Row["BgColor"] = moduleField.BgColor.ToArgb();
                Row["DisplayStyle"] = moduleField.DisplayStyle;
                Row["ColumnWidth"] = moduleField.ColumnWidth;
                Row["IsVisible"] = moduleField.IsVisible;
                Row["IsEdit"] = moduleField.IsEdit;
                Row["IsNull"] = moduleField.IsNull;
                Row["OrderIndex"] = moduleField.OrderIndex;
                Row["Scts_1"] = DateTime.Now.ToString();
                

                Data.Rows.Add(Row);
            }

            object r = Update(Data);
            Result = (Convert.ToInt32(r) == 1);

            return Result;
        }

        public Boolean DeleteModuleSystemField(String ModuleID, String ModuleCode, String Index)
        {
            Boolean Result = false;
            //增加字段Scts_1,Scdel  删除操作只做伪删除，用于数据同步     2013-10-17
            StringBuilder Sql_Delete = new StringBuilder();
            //Sql_Delete.Append("Delete From sys_moduleview where ");
            //Sql_Delete.Append("ModuleID = '");
            //Sql_Delete.Append(ModuleID);
            //Sql_Delete.Append("' and ID = '");
            //Sql_Delete.Append(Index);
            //Sql_Delete.Append("'");
            Sql_Delete.Append("Update sys_moduleview set Scts_1=Getdate(),Scdel=1 where ");
            Sql_Delete.Append("ModuleID = '");
            Sql_Delete.Append(ModuleID);
            Sql_Delete.Append("' and ID = '");
            Sql_Delete.Append(Index);
            Sql_Delete.Append("'");

            object r = ExcuteCommand(Sql_Delete.ToString());
            Result = (Convert.ToInt32(r) == 1);

            return Result;
        }

        public Boolean UpdateModuleSystemField(String ModuleID, String ModuleCode, ModuleField moduleField)
        {
            Boolean Result = false;

            StringBuilder Sql_Update = new StringBuilder();
            Sql_Update.Append("Select * From sys_moduleview where Scdel=0 and ModuleID='");
            Sql_Update.Append(ModuleID);
            Sql_Update.Append("' and ID = '");
            Sql_Update.Append(moduleField.Index);
            Sql_Update.Append("'");

            DataTable Data = GetDataTable(Sql_Update.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow Row = Data.Rows[0];
                Row["ID"] = moduleField.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["ModuleID"] = moduleField.ModuleIndex;
                Row["Description"] = moduleField.Description;
                Row["TableName"] = moduleField.TableName;
                Row["TableText"] = moduleField.TableText;
                Row["ContentType"] = moduleField.ContentType.ToString();
                Row["ContentFieldType"] = moduleField.ContentFieldType;
                Row["ContentText"] = moduleField.ContentText;
                Row["Contents"] = moduleField.Contents;
                Row["ForeColor"] = moduleField.ForeColor.ToArgb();
                Row["BgColor"] = moduleField.BgColor.ToArgb();
                Row["DisplayStyle"] = moduleField.DisplayStyle;
                Row["ColumnWidth"] = moduleField.ColumnWidth;
                Row["IsVisible"] = moduleField.IsVisible;
                Row["IsEdit"] = moduleField.IsEdit;
                Row["IsNull"] = moduleField.IsNull;
                Row["OrderIndex"] = moduleField.OrderIndex;
                Row["Scts_1"] = DateTime.Now.ToString();
            }

            object r = Update(Data);
            Result = (Convert.ToInt32(r) == 1);

            return Result;
        }

        public List<ModuleField> GetModuleSystemFields(String ModuleID, String ModuleCode)
        {
            List<ModuleField> Results = new List<ModuleField>();

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件 Scdel=0      2013-10-17
            Sql_Select.Append("select * from sys_moduleview  where Scdel=0 and ModuleID = '");
            Sql_Select.Append(ModuleID);
            Sql_Select.Append("' order by OrderIndex");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            foreach (DataRow Row in Data.Rows)
            {
                ModuleField field = new ModuleField();
                field.Index = Row["ID"].ToString();
                field.ModuleIndex = Row["ModuleID"].ToString();
                field.Description = Row["Description"].ToString();
                field.TableName = Row["TableName"].ToString();
                field.TableText = Row["TableText"].ToString();
                field.ContentType = (ContentType)Enum.Parse(typeof(ContentType), Row["ContentType"].ToString());
                field.ContentFieldType = Row["ContentFieldType"].ToString();
                field.ContentText = Row["ContentText"].ToString();
                field.Contents = Row["Contents"].ToString();
                field.ForeColor = Color.FromArgb(Convert.ToInt32(Row["ForeColor"]));
                field.BgColor = Color.FromArgb(Convert.ToInt32(Row["BgColor"]));
                field.DisplayStyle = Row["DisplayStyle"].ToString();
                field.ColumnWidth = Convert.ToInt32(Row["ColumnWidth"]);
                field.IsVisible = Convert.ToBoolean(Row["IsVisible"]);
                field.IsEdit = Convert.ToBoolean(Row["IsEdit"]);
                field.IsNull = Convert.ToBoolean(Row["IsNull"]);
                field.IsSystem = true;
                field.OrderIndex = Convert.ToInt32(Row["OrderIndex"]);

                Results.Add(field);
            }

            return Results;
        }

        public Boolean RemoveModuleSystemFields(String moduleIndex)
        {
            Boolean Result = false;

            StringBuilder sql_delete = new StringBuilder();
            sql_delete.Append("delete sys_moduleview where ModuleID='");
            sql_delete.Append(moduleIndex);
            sql_delete.Append("'");

            try
            {
                object r = ExcuteCommand(sql_delete.ToString());
                Result = Convert.ToInt32(r) == 1;
            }
            catch
            {
            }

            return Result;
        }

        public Boolean RemoveModuleSystemFields(String moduleIndex, String TableName)
        {
            Boolean Result = false;
            //增加字段Scts_1,Scdel之后  删除操作只做伪删除，用于数据同步     2013-10-17
            StringBuilder sql_delete = new StringBuilder();
            //sql_delete.Append("delete sys_moduleview where ModuleID='");
            //sql_delete.Append(moduleIndex);
            //sql_delete.Append("' and TableName='");
            //sql_delete.Append(TableName);
            //sql_delete.Append("'");
            sql_delete.Append("update sys_moduleview Set Scts_1=Getdate(),Scdel=1 where ModuleID='");
            sql_delete.Append(moduleIndex);
            sql_delete.Append("' and TableName='");
            sql_delete.Append(TableName);
            sql_delete.Append("'");

            try
            {
                object r = ExcuteCommand(sql_delete.ToString());
                Result = Convert.ToInt32(r) == 1;
            }
            catch
            {
            }

            return Result;
        }
    }
}
