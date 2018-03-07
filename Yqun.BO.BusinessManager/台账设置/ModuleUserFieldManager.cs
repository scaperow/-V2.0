using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;
using System.Drawing;
using System.Reflection;

namespace Yqun.BO.BusinessManager
{
    public class ModuleUserFieldManager : BOBase
    {
        public Boolean NewModuleUserField(String ModuleID, String ModuleCode, ModuleField moduleField)
        {
            Boolean Result = false;

            StringBuilder Sql_Select = new StringBuilder();
            // 增加查询条件 Scdel=0     2013-10-19
            Sql_Select.Append("Select * From sys_biz_moduleview where Scdel=0 and ModuleID='");
            Sql_Select.Append(ModuleID);
            Sql_Select.Append("' and ModuleCode='");
            Sql_Select.Append(ModuleCode);
            Sql_Select.Append("' and ID = '");
            Sql_Select.Append(moduleField.Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count == 0)
            {
                DataRow Row = Data.NewRow();
                Row["ID"] = moduleField.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["ModuleCode"] = moduleField.ModuleCode;
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
                Data.Rows.Add(Row);
            }

            object r = Update(Data);
            Result = (Convert.ToInt32(r) == 1);

            return Result;
        }

        public Boolean DeleteModuleUserField(String ModuleID, String ModuleCode, String Index)
        {
            Boolean Result = false;

            StringBuilder Sql_Delete = new StringBuilder();
            // 增加查询条件 Scdel=0     2013-10-19
            //Sql_Delete.Append("Delete From sys_biz_moduleview where ModuleCode = '");
            //Sql_Delete.Append(ModuleCode);
            //Sql_Delete.Append("' and ModuleID = '");
            //Sql_Delete.Append(ModuleID);
            //Sql_Delete.Append("' and ID = '");
            //Sql_Delete.Append(Index);
            //Sql_Delete.Append("'");
            Sql_Delete.Append("Update sys_biz_moduleview Set Scts_1=Getdate(),Scdel=1 where ModuleCode = '");
            Sql_Delete.Append(ModuleCode);
            Sql_Delete.Append("' and ModuleID = '");
            Sql_Delete.Append(ModuleID);
            Sql_Delete.Append("' and ID = '");
            Sql_Delete.Append(Index);
            Sql_Delete.Append("'");

            object r = ExcuteCommand(Sql_Delete.ToString());
            Result = (Convert.ToInt32(r) == 1);

            return Result;
        }

        public Boolean UpdateModuleUserField(String ModuleID, String ModuleCode, ModuleField moduleField)
        {
            Boolean Result = false;

            StringBuilder Sql_Update = new StringBuilder();
            // 增加查询条件 Scdel=0     2013-10-19
            Sql_Update.Append("Select * From sys_biz_moduleview where Scdel=0 and ModuleID='");
            Sql_Update.Append(ModuleID);
            Sql_Update.Append("' and ModuleCode='");
            Sql_Update.Append(ModuleCode);
            Sql_Update.Append("' and ID = '");
            Sql_Update.Append(moduleField.Index);
            Sql_Update.Append("'");

            DataTable Data = GetDataTable(Sql_Update.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow Row = Data.Rows[0];
                Row["ID"] = moduleField.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["ModuleCode"] = moduleField.ModuleCode;
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

        public Boolean UpdateModuleUserField(String ModuleID, String ModuleCode, String[] Index, float[] Width)
        {
            Boolean Result = false;

            StringBuilder Sql_Update = new StringBuilder();
            // 增加查询条件 Scdel=0     2013-10-19
            Sql_Update.Append("Select ID,SCTS,ColumnWidth From sys_biz_moduleview where Scdel=0 and ModuleID='");
            Sql_Update.Append(ModuleID);
            Sql_Update.Append("' and ModuleCode='");
            Sql_Update.Append(ModuleCode);
            Sql_Update.Append("' and ID in ('");
            Sql_Update.Append(string.Join("','", Index));
            Sql_Update.Append("')");

            DataTable Data = GetDataTable(Sql_Update.ToString());
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    String ID = Row["ID"].ToString();
                    int i = Array.IndexOf(Index, ID);
                    Row["SCTS"] = DateTime.Now.ToString();
                    Row["ColumnWidth"] = Width[i];
                    Row["Scts_1"] = DateTime.Now.ToString();
                }
            }

            object r = Update(Data);
            Result = (Convert.ToInt32(r) == 1);

            return Result;
        }
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<ModuleField> GetModuleFields(String ModuleID, String ModuleCode)
        {
            List<ModuleField> Results = new List<ModuleField>();

            StringBuilder Sql_Select = new StringBuilder();
            // 增加查询条件 Scdel=0     2013-10-19
            Sql_Select.Append("select *,'true' as IsSystem from sys_moduleview  where Scdel=0 and ModuleID = '");
            Sql_Select.Append(ModuleID);
            Sql_Select.Append("' union ");
            // 增加查询条件 Scdel=0     2013-10-19
            Sql_Select.Append("select *,'false' as IsSystem from sys_biz_moduleview where Scdel=0 and ModuleID = '");
            Sql_Select.Append(ModuleID);
            Sql_Select.Append("' and ModuleCode = '");
            Sql_Select.Append(ModuleCode);
            Sql_Select.Append("' order by IsSystem desc,OrderIndex");
            DataTable Data = GetDataTable(Sql_Select.ToString());
            foreach (DataRowView Row in Data.DefaultView)
            {
                ModuleField field = new ModuleField();
                field.Index = Row["ID"].ToString();
                field.ModuleCode = Row["ModuleCode"].ToString();
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
                field.IsSystem = Convert.ToBoolean(Row["IsSystem"]);
                field.OrderIndex = Convert.ToInt32(Row["OrderIndex"]);

                Results.Add(field);
            }

            return Results;
        }

        public List<ModuleField> GetModuleUserFields(String ModuleID, String ModuleCode)
        {
            List<ModuleField> Results = new List<ModuleField>();

            StringBuilder Sql_Select = new StringBuilder();
            // 增加查询条件 Scdel=0     2013-10-19
            Sql_Select.Append("select * from sys_biz_moduleview where Scdel=0 and ModuleID = '");
            Sql_Select.Append(ModuleID);
            Sql_Select.Append("' and ModuleCode = '");
            Sql_Select.Append(ModuleCode);
            Sql_Select.Append("' order by OrderIndex");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            foreach (DataRow Row in Data.Rows)
            {
                ModuleField field = new ModuleField();
                field.Index = Row["ID"].ToString();
                field.ModuleCode = Row["ModuleCode"].ToString();
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
                field.IsSystem = false;
                field.OrderIndex = Convert.ToInt32(Row["OrderIndex"]);

                Results.Add(field);
            }
            return Results;
        }
    }
}
