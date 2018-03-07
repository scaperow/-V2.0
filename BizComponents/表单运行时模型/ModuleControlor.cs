using System;
using System.Collections.Generic;
using System.Data;
using BizCommon;
using System.Windows.Forms;

namespace BizComponents
{
    public class ModuleControlor
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ModelDataManager DataManager;

        public ModuleControlor(ModelDataManager dataManager)
        {
            DataManager = dataManager;
        }

        //获得资料数据
        public DataSet GetData(ModuleConfiguration module, String DataID, String DataCode)
        {
            return DataManager.GetData(module, DataID, DataCode);
        }

        //获得一条操作日志的数据
        public DataTable GetOperateLogByID(String id)
        {
            return DataManager.GetOperateLogByID(id);
        }

        //新建资料
        public DataSet NewData(ModuleConfiguration module, String DataID, String DataCode, String TryType)
        {
            DataSet Data = DataManager.GetData(module, DataID, DataCode);
            foreach (DataTable DataTable in Data.Tables)
            {
                DataRow Row = DataTable.NewRow();
                Row["ID"] = DataID;
                Row["SCPT"] = DataCode;
                Row["SCTS"] = DateTime.Now.ToString();
                DataTable.Rows.Add(Row);
            }

            //自动录入委托日期字段
            foreach (SheetConfiguration Sheet in module.Sheets)
            {
                if (Sheet.DataTableSchema.Schema == null)
                    continue;

                String TableName = string.Format("[{0}]", Sheet.DataTableSchema.Schema.Name);
                DataTable DataTable = Data.Tables[TableName];
                DataRow dataRow = DataTable.Rows[0];
                foreach (FieldDefineInfo Field in Sheet.DataTableSchema.Schema.FieldInfos)
                {
                    if (Field.Description == FieldStrings.委托日期)
                    {
                        dataRow[Field.FieldName] = DateTime.Now;
                    }
                }
            }

            //自动判断资料的试验类别
            DataTable ExtentData = Data.Tables[string.Format("[biz_norm_extent_{0}]", module.Index)];
            foreach (DataColumn col in ExtentData.Columns)
            {
                if (col.ColumnName == "TryType")
                    ExtentData.Rows[0]["TryType"] = TryType;
            }

            return Data;
        }

        //产生监理平行资料
        public DataSet NewPingXingData(ModuleConfiguration module, String DataID, String DataCode, String RelationCode, String RelationID)
        {
            DataSet CopyData = DataManager.GetData(module, RelationID, RelationCode);
            DataSet Data = CopyData.Clone();
            foreach (DataTable data in CopyData.Tables)
            {
                DataRow newRow = Data.Tables[data.TableName].NewRow();
                Data.Tables[data.TableName].Rows.Add(newRow);

                newRow.ItemArray = data.Rows[0].ItemArray;
                newRow["ID"] = DataID;
                newRow["SCPT"] = DataCode;
                newRow["SCTS"] = DateTime.Now.ToString();
                newRow["SCCT"] = RelationCode;
                newRow["SCCS"] = RelationID;
            }

            foreach (SheetConfiguration Sheet in module.Sheets)
            {
                if (Sheet.DataTableSchema.Schema == null)
                    continue;

                String TableName = string.Format("[{0}]", Sheet.DataTableSchema.Schema.Name);
                if (CopyData.Tables.Contains(TableName) && Data.Tables.Contains(TableName))
                {
                    DataTable CopyDataTable = Data.Tables[TableName];
                    DataRow Row = CopyDataTable.Rows[0];
                    DataTable DataTable = Data.Tables[TableName];
                    DataRow newRow = DataTable.Rows[0];
                    foreach (FieldDefineInfo Field in Sheet.DataTableSchema.Schema.FieldInfos)
                    {
                        if (!Field.IsPingxing)
                            newRow[Field.FieldName] = DBNull.Value;
                    }
                }
                else if (!CopyData.Tables.Contains(TableName))
                {
                    logger.Error(string.Format("试验‘{0}’中未找到表‘{1}’的数据行。", module.Description, TableName));
                }
            }

            //自动录入委托日期字段
            foreach (SheetConfiguration Sheet in module.Sheets)
            {
                if (Sheet.DataTableSchema.Schema == null)
                    continue;

                String TableName = string.Format("[{0}]", Sheet.DataTableSchema.Schema.Name);
                DataTable DataTable = Data.Tables[TableName];
                DataRow dataRow = DataTable.Rows[0];
                foreach (FieldDefineInfo Field in Sheet.DataTableSchema.Schema.FieldInfos)
                {
                    if (Field.Description == FieldStrings.委托日期)
                    {
                        dataRow[Field.FieldName] = DateTime.Now;
                    }
                }
            }

            DataTable ExtentData = Data.Tables[string.Format("[biz_norm_extent_{0}]", module.Index)];
            foreach (DataColumn col in ExtentData.Columns)
            {
                if (col.ColumnName == "TryType")
                    ExtentData.Rows[0]["TryType"] = "平行";
            }

            return Data;
        }

        //产生监理平行资料
        public DataSet NewPingXingData(ModuleConfiguration module, String DataCode, String RelationCode, String RelationID)
        {
            String DataID = Guid.NewGuid().ToString();
            return NewPingXingData(module, DataID, DataCode, RelationCode, RelationID);
        }

        //删除资料
        public Boolean DeleteData(ModuleConfiguration module, String DataID, String ModelCode)
        {
            return DataManager.DeleteData(module, DataID, ModelCode);
        }

        //删除资料
        public Boolean DeleteData(String ModuleCode, ModuleConfiguration module)
        {
            return DataManager.DeleteData(module, ModuleCode);
        }

        //复制资料
        public DataSet CopyData(ModuleConfiguration module, String[] DataID, String DataCode)
        {
            DataSet Data = DataManager.GetData(module, DataID, DataCode);
            DataSet NewData = Data.Copy();

            List<String> DataList = new List<string>();
            for (int i = 0; i < DataID.Length; i++)
            {
                DataList.Add(Guid.NewGuid().ToString());
            }

            foreach (DataTable DataTable in NewData.Tables)
            {
                for (int i = 0; i < DataTable.Rows.Count; i++)
                {
                    DataTable.Rows[i]["ID"] = DataList[i];
                    DataTable.Rows[i]["SCCS"] = Guid.NewGuid().ToString();
                }
            }

            Dictionary<string, List<string>> Fields = new Dictionary<string, List<string>>();
            foreach (SheetConfiguration sheetConfiguration in module.Sheets)
            {
                if (sheetConfiguration.DataTableSchema.Schema == null)
                    continue;

                TableDefineInfo TableInfo = sheetConfiguration.DataTableSchema.Schema;
                foreach (FieldDefineInfo field in TableInfo.FieldInfos)
                {
                    if (field.IsKeyField)
                    {
                        if (!Fields.ContainsKey(TableInfo.Name))
                            Fields.Add(TableInfo.Name, new List<string>());

                        Fields[TableInfo.Name].Add(field.FieldName);
                    }
                }
            }

            foreach (DataTable DataTable in NewData.Tables)
            {
                if (!Fields.ContainsKey(DataTable.TableName))
                    continue;

                for (int i = 0; i < DataTable.Rows.Count; i++)
                {
                    foreach (String key in Fields[DataTable.TableName])
                    {
                        DataTable.Rows[i][key] = "";
                    }
                }
            }

            if (DataManager.UpdateData(module.Index, NewData, false))
            {
                return Data;
            }
            else
            {
                return new DataSet();
            }
        }

        //保存资料
        public Boolean SaveData(ModuleConfiguration module, DataSet dataset, String TryType)
        {
            DataTable ExtentData = dataset.Tables[string.Format("[biz_norm_extent_{0}]", module.Index)];
            foreach (DataColumn col in ExtentData.Columns)
            {
                if (col.ColumnName == "TryType" && Convert.ToString(ExtentData.Rows[0]["TryType"]) == "")
                    ExtentData.Rows[0]["TryType"] = TryType;
            }

            return DataManager.UpdateData(module.Index, dataset, true);
        }
    }
}
