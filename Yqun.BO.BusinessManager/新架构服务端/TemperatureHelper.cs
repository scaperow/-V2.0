using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BizCommon;
using System.Data.SqlClient;

namespace Yqun.BO.BusinessManager
{
    public class TemperatureHelper : BOBase
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public DataTable GetTemperatureTypes(string testRoomCode)
        {
            var sql = "SELECT * FROM dbo.sys_temperature_types  WHERE TestRoomCode = '" + testRoomCode + "' OR IsSystem = 1 ";

            return GetDataTable(sql);
        }

        public string NewTemperature(Sys_Temperature temperature)
        {
            if (ExistsTemperatureName(temperature.TestRoomCode, temperature.Name))
            {
                return "温度类型的名称不可以重复";
            }

            var sql = @"INSERT INTO [dbo].[sys_temperature_types]
                       ([ID]
	                   ,[TestRoomCode]
                       ,[CreateBy]
                       ,[CreateTime]
                       ,[Name]
                       ,[IsSystem])
                 VALUES(
                       (SELECT MAX(ID) FROM dbo.sys_temperature_types) + 1 ,
                       @TestRoomCode,
                       @CreateBy, 
                       @CreateTime,
                       @Name, 
                       @IsSystem)";

            var connection = GetConntion();
            connection.Open();

            var command = GetDbCommand(sql, connection);
            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("@TestRoomCode", temperature.TestRoomCode));
            command.Parameters.Add(new SqlParameter("@CreateBy", temperature.CreateBy));
            command.Parameters.Add(new SqlParameter("@CreateTime", DateTime.Now));
            command.Parameters.Add(new SqlParameter("@IsSystem", temperature.IsSystem));
            command.Parameters.Add(new SqlParameter("@Name", temperature.Name));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return "添加失败";
            }

            return null;
        }

        public bool ExistsTemperatureName(string testRoomCode, string name)
        {
            var sql = @"SELECT COUNT(ID) FROM dbo.sys_temperature_types
                        WHERE (TestRoomCode = '" + testRoomCode + "' OR IsSystem = 1) AND Name='" + name + "'";

            var scalar = ExcuteScalar(sql);

            if (scalar == null)
            {
                return false;
            }

            return int.Parse(scalar.ToString()) > 0;
        }

        public string DeleteTemperature(string id)
        {
            var sqlDocumentRelation = @"SELECT COUNT(d.ID) FROM dbo.sys_document_ext e JOIN dbo.sys_document d ON d.ID=e.ID
WHERE d.Status > 0 AND e.TemperatureType = " + id;

            var scalarDocumentRelation = ExcuteScalar(sqlDocumentRelation);

            if (scalarDocumentRelation != null && int.Parse(scalarDocumentRelation.ToString()) > 0)
            {
                return "该温度类型已在其他的报告中使用, 不能删除";
            }

            ExcuteCommand("DELETE FROM [dbo].[sys_temperature_types] WHERE ID = " + id);
            return null;
        }

        public string RenameTemperature(string id, string newName)
        {
            var testRoomCode = Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code;
            if (ExistsTemperatureName(testRoomCode, newName))
            {
                return "温度类型的名称不可以重复";
            }

            var sql = "UPDATE sys_temperature_types SET Name = '" + newName + "' WHERE ID = " + id;
            ExcuteCommand(sql);
            return null;
        }
    }
}
