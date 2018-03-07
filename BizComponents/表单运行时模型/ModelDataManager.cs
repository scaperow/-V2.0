using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Services;
using BizCommon;

namespace BizComponents
{
    public class ModelDataManager
    {
        //获得台账数据
        public DataSet GetData(String ModelCode, List<ModuleField> ModelFields, String Filter, List<ModuleField> GroupInfo, int ItemCount, int PageIndex)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetData", new object[] { ModelCode, ModelFields, Filter, GroupInfo, ItemCount, PageIndex }) as DataSet;
        }

        //获得台账数据条数
        public int GetCount(TableDefineInfo ExtentDataSchema, String ModuleCode)
        {
            return Convert.ToInt32(Agent.CallService("Yqun.BO.BusinessManager.dll", "GetCount", new object[] { ExtentDataSchema, ModuleCode }));
        }

        /// <summary>
        /// 获得台账数据条数（2013-07-31，解决数据显示和数据条数不相符问题）
        /// </summary>
        /// <param name="ModuleCode"></param>
        /// <param name="ModuleFields">显示字段集合</param>
        /// <returns></returns>
        public int GetCount( String ModuleCode, List<ModuleField> ModelFields)
        {
            return Convert.ToInt32(Agent.CallService("Yqun.BO.BusinessManager.dll", "GetCount", new object[] { ModuleCode, ModelFields }));
        }

        //获取多条模板的数据
        public DataSet GetData(ModuleConfiguration Module, String[] DataID, String DataCode)
        {
            List<TableDefineInfo> TableSchemas = new List<TableDefineInfo>();
            foreach (SheetConfiguration Sheet in Module.Sheets)
            {
                TableSchemas.Add(Sheet.DataTableSchema.Schema);
            }

            TableDefineInfo ExtentDataSchema = Module.ExtentDataSchema;

            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetData", new object[] { TableSchemas, ExtentDataSchema, DataID, DataCode }) as DataSet;
        }

        //获得一条模板的数据
        public DataSet GetData(ModuleConfiguration Module, String DataID, String DataCode)
        {
            List<TableDefineInfo> TableSchemas = new List<TableDefineInfo>();
            foreach (SheetConfiguration Sheet in Module.Sheets)
            {
                TableSchemas.Add(Sheet.DataTableSchema.Schema);
            }

            TableDefineInfo ExtentDataSchema = Module.ExtentDataSchema;

            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetData", new object[] { TableSchemas, ExtentDataSchema, DataID, DataCode }) as DataSet;
        }

        
        //获得一条操作日志的数据
        public DataTable GetOperateLogByID(String id)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetOperateLogByID", new object[] { id }) as DataTable;
        }

        //获得模板的全部数据
        public DataSet GetData(ModuleConfiguration Module, String DataCode)
        {
            List<TableDefineInfo> TableSchemas = new List<TableDefineInfo>();
            foreach (SheetConfiguration Sheet in Module.Sheets)
            {
                TableSchemas.Add(Sheet.DataTableSchema.Schema);
            }

            TableDefineInfo ExtentDataSchema = Module.ExtentDataSchema;

            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetData", new object[] { TableSchemas, ExtentDataSchema, DataCode }) as DataSet;
        }

        //删除与模板相关的数据
        public Boolean DeleteData(ModuleConfiguration Module, String ModelCode)
        {
            List<TableDefineInfo> TableSchemas = new List<TableDefineInfo>();
            foreach (SheetConfiguration Sheet in Module.Sheets)
            {
                TableSchemas.Add(Sheet.DataTableSchema.Schema);
            }

            TableDefineInfo ExtentDataSchema = Module.ExtentDataSchema;

            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteData", new object[] { TableSchemas, ExtentDataSchema, ModelCode }));
        }

        //删除数据
        public Boolean DeleteData(ModuleConfiguration Module, String DataID, String ModelCode)
        {
            List<TableDefineInfo> TableSchemas = new List<TableDefineInfo>();
            foreach (SheetConfiguration Sheet in Module.Sheets)
            {
                TableSchemas.Add(Sheet.DataTableSchema.Schema);
            }

            //模板的表外字段
            TableDefineInfo ExtentDataSchema = Module.ExtentDataSchema;

            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteData", new object[] { TableSchemas, ExtentDataSchema, Module.Index, DataID, ModelCode }));
        }

        //更新资料数据
        public Boolean UpdateData(String ModelIndex, DataSet Data, Boolean IsSaveOperation)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateData", new object[] { ModelIndex, Data, IsSaveOperation }));
        }

        public Boolean IsUniqueField(String TestRoomCode, String DataID, FieldDefineInfo Field, object FieldValue)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "IsUniqueField", new object[] { TestRoomCode, DataID, Field, FieldValue }));
        }

        public Boolean HasPingXingData(TableDefineInfo ExtentDataSchema, String RelationID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HasPingXingData", new object[] { RelationID }));
        }

        public void NewPingXingRelation(String dataID, String pxingID, String sgTestRoomCode)
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "NewPingXingRelation", new object[] { dataID, pxingID, sgTestRoomCode });
        }

        public Boolean UpdateExtentFields(String ModelCode, String TableName, String FieldName, String DataID, String Value)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateExtentFields", new object[] { ModelCode, TableName, FieldName, DataID, Value }));
        }
    }
}
