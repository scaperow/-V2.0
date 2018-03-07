using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BizCommon;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using log4net.Repository.Hierarchy;
using System.Data.SqlTypes;
using System.IO;

namespace Yqun.BO.BusinessManager
{
    public class StatisticsHelper : BOBase
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable GetStatisticsList()
        {
            try
            {
                var sql = "SELECT ItemID,ItemName,ItemType,Weight,Columns FROM sys_TJ_Item WHERE Status > 0 ORDER BY Weight";
                return GetDataTable(sql);

            }
            catch (Exception e)
            {
                Logger.Error("在获取统计项列表时发生异常");
                Logger.Error(e);
            }

            return null;
        }

        public string NewStatistics(Sys_Statistics_Item model)
        {
            if (ExistsName(model.Name, model.ID))
            {
                return "项目名称不能重复";
            }

            var sql = @"INSERT INTO [dbo].[sys_TJ_Item]
                       ([ItemID]
                       ,[ItemName]
                       ,[Columns]
                       ,[ItemType]
                       ,[Weight]
                       ,[Status])
                 VALUES
                       (@ItemID
                       ,@ItemName
                       ,@Columns
                       ,@ItemType
                       ,@Weight
                       ,@Status)";

            var command = GetDbCommand(sql);
            command.Parameters.Add(new SqlParameter("@ItemID", model.ID));
            command.Parameters.Add(new SqlParameter("@ItemName", model.Name));
            command.Parameters.Add(new SqlParameter("@Columns", model.Columns));
            command.Parameters.Add(new SqlParameter("@ItemType", model.Type));
            command.Parameters.Add(new SqlParameter("@Weight", model.Weight));
            command.Parameters.Add(new SqlParameter("@Status", model.Status));

            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }
                command.ExecuteNonQuery();

                return null;
            }
            catch (Exception)
            {
                Logger.Error("在新增统计项时发生错误");
            }
            finally
            {
                try
                {
                    command.Connection.Close();
                    command.Connection.Dispose();

                }
                catch (Exception) { }
            }

            return "添加失败, 服务器发生错误";
        }

        public string ModifyStatistics(Sys_Statistics_Item model)
        {
            if (ExistsName(model.Name, model.ID))
            {
                return "项目名称不能重复";
            }

            var sql = @"UPDATE [dbo].[sys_TJ_Item] SET 
                        [ItemName] = @ItemName
                       ,[Columns] = @Columns
                       ,[ItemType] = @ItemType
                       ,[Weight] = @Weight
                       ,[Status] = @Status
                        WHERE ItemID = @ItemID ";

            var command = GetDbCommand(sql);
            command.Parameters.Add(new SqlParameter("@ItemID", model.ID));
            command.Parameters.Add(new SqlParameter("@ItemName", model.Name));
            command.Parameters.Add(new SqlParameter("@Columns", model.Columns));
            command.Parameters.Add(new SqlParameter("@ItemType", model.Type));
            command.Parameters.Add(new SqlParameter("@Weight", model.Weight));
            command.Parameters.Add(new SqlParameter("@Status", model.Status));

            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                command.ExecuteNonQuery();
                return null;
            }
            catch (Exception e)
            {
                Logger.Error("在修改统计项时发生异常");
                Logger.Error(e);
            }
            finally
            {
                try
                {
                    command.Connection.Close();
                    command.Connection.Dispose();
                }
                catch (Exception) { }
            }

            return "保存失败, 服务器发生错误";
        }

        public bool ExistsName(string name, Guid id)
        {
            var sql = "SELECT COUNT(ItemID) FROM sys_TJ_Item WHERE ItemName = '" + name + "' AND ItemID <> '" + id + "' AND Status > 0";
            var scalar = ExcuteScalar(sql);
            if (scalar == null)
            {
                return false;
            }

            var count = default(int);
            if (int.TryParse(scalar.ToString(), out count))
            {
                return count > 0;
            }

            return false;
        }

        public string DeleteStatistics(Guid[] IDs)
        {
            var ids = new List<string>();
            foreach (var id in IDs)
            {
                ids.Add(id.ToString());
            }

            try
            {
                var sql = "UPDATE sys_TJ_Item SET Status = 0 WHERE ItemID IN ('" + string.Join("','", ids.ToArray()) + "')";
                ExcuteCommand(sql);

                return null;
            }
            catch (Exception e)
            {
                Logger.Error("在删除统计项时发生异常");
                Logger.Error(e);
            }

            return "服务器发生错误, 可能没有删除成功";
        }

        public DataTable GetStatistics(Guid id)
        {
            var sql = "SELECT ItemID,ItemName,ItemType,Weight,Columns,Status FROM sys_TJ_Item WHERE ItemID = '" + id + "'";

            try
            {
                var table = GetDataTable(sql);

                return table;
            }
            catch (Exception e)
            {
                Logger.Error("在获取统计项时发生异常");
                Logger.Error(e);
            }

            return null;
        }

        public DataTable GetStatisticsReference(Guid moduleID)
        {
            var sql = @"SELECT item.* FROM dbo.sys_TJ_Item item JOIN dbo.sys_TJ_Item_Module map
                        ON item.ItemID = map.ItemID AND item.Status > 0
                        WHERE map.ModuleID = '" + moduleID + "' ORDER BY item.ItemID";

            try
            {
                var table = GetDataTable(sql);
                return table;
            }
            catch (Exception e)
            {
                Logger.Error("在获取统计项关联的模板信息时发生异常");
                Logger.Error(e);
            }

            return null;
        }

        public string GetStatisticsMapOnModule(Guid moduleID)
        {
            var sql = "SELECT StatisticsMap FROM sys_module WHERE ID = '" + moduleID + "'";

            try
            {
                var scalar = ExcuteScalar(sql);

                return Convert.ToString(scalar);
            }
            catch (Exception e)
            {
                Logger.Error("在获取统计项关联的模板信息时发生异常");
                Logger.Error(e);
            }

            return null;
        }

        public string UpdateStatisticsMapOnModule(Guid moduleID, string map)
        {
            try
            {
                var sql = "UPDATE sys_module SET StatisticsMap = '" + map + "' WHERE ID = '" + moduleID + "'";
                ExcuteCommand(sql);

                return null;
            }
            catch (Exception e)
            {
                Logger.Error("在修改模板的统计项设置时发生了异常");
                Logger.Error(e);
            }

            return "服务器发生了错误, 可能没有保存成功";
        }

        public DataTable GetStatisticsModules(Guid itemID)
        {
            var sql = @"SELECT tim.ItemID,tim.ModuleID,module.Name,module.StatisticsMap FROM dbo.sys_TJ_Item_Module tim
            LEFT JOIN dbo.sys_module module ON module.ID = tim.ModuleID AND module.IsActive > 0
            WHERE tim.ItemID = '" + itemID + "' ORDER BY tim.ItemID";

            try
            {
                return GetDataTable(sql);
            }
            catch (Exception e)
            {
                Logger.Error("在获取统计项对应的模板时发生了异常");
                Logger.Error(e);
            }

            return null;
        }

        public string NewStatisticsReference(Guid statisticsID, Guid moduleID)
        {
            var sql = @"SELECT COUNT(ID) FROM dbo.sys_TJ_Item_Module WHERE ItemID = '" + statisticsID + "' AND ModuleID = '" + moduleID + "'";

            var result = ExcuteScalar(sql);
            if (result != null)
            {
                var count = Convert.ToInt32(result);

                if (count == 0)
                {
                    sql = @"INSERT INTO dbo.sys_TJ_Item_Module
                                ( ItemID, ModuleID )
                        VALUES  ( '" + statisticsID + @"', 
                                  '" + moduleID + @"'  -- ModuleID - uniqueidentifier
                                  )";

                    try
                    {
                        ExcuteCommand(sql);
                        return null;
                    }
                    catch (Exception e)
                    {
                        Logger.Error("在设置模板与统计项关联时发生了异常");
                        Logger.Error(e);
                    }
                }
            }

            return null;
        }

        public string SynchronousStatistics(Guid statisticsID, List<StatisticsSetting> columns)
        {
            return null;
            var sql = @"SELECT map.ItemID AS ItemID, module.ID AS ModuleID,module.StatisticsMap 
                       FROM dbo.sys_TJ_Item_Module map JOIN dbo.sys_module module ON module.ID = map.ModuleID
                       WHERE map.ItemID = '" + statisticsID + "'";

            var table = GetDataTable(sql);
            if (table == null || table.Rows.Count == 0)
            {
                return null;
            }

            foreach (DataRow row in table.Rows)
            {
                var moduleID = new Guid(Convert.ToString(row["ModuleID"]));
                var statisticsMap = Convert.ToString(row["StatisticsMap"]);

                var settings = JsonConvert.DeserializeObject<List<StatisticsModuleSetting>>(statisticsMap);

                if (settings == null)
                {
                    foreach (var column in columns)
                    {
                        settings.Add(new StatisticsModuleSetting()
                        {
                            BindField = column.BindField,
                            StatisticsItemName = column.ItemName
                        });
                    }
                }
                else
                {
                    foreach (var column in columns)
                    {
                        if (settings.Find(m => m.StatisticsItemName == column.ItemName) == null)
                        {
                            settings.Add(new StatisticsModuleSetting()
                            {
                                BindField = column.BindField,
                                StatisticsItemName = column.ItemName,
                            });
                        }
                    }

                    var sets = settings.ToArray();
                    foreach (var set in sets)
                    {
                        var column = columns.Find(m => set.StatisticsItemName == m.ItemName);
                        if (column == null)
                        {
                            settings.Remove(set);
                        }
                    }
                }
            }

            return null;
        }

        public string RemoveStatisticsReference(Guid statisticsID, Guid moduleID)
        {
            var sql = "DELETE FROM sys_TJ_Item_Module WHERE ItemID = '" + statisticsID + "' AND ModuleID = '" + moduleID + "'";
            try
            {
                ExcuteCommand(sql);
                return null;
            }
            catch (Exception e)
            {
                Logger.Error("在删除模板与统计项关联时发生了异常");
                Logger.Error(e);
            }

            return null;
        }

        public string NewStatisticsResult(Dictionary<string, Object> result)
        {
            if (result.Keys.Count == 0)
            {
                return null;
            }

            var builder = new StringBuilder();
            var fields = new List<string>();
            var parameters = new List<string>();
            var command = GetDbCommand("");

            var id = Convert.ToString(result["DataID"]);
            var count = 0;

            try
            {
                var countScalar = "SELECT COUNT(DataID) FROM sys_TJ_MainData WHERE DataID = '" + id + "'";
                count = Convert.ToInt32(ExcuteScalar(countScalar));
            }
            catch (Exception e)
            {
                Logger.Error("获取重复的 DataID 数量出现问题" + e);
                return "添加失败";
            }

            if (count == 0)
            {
                foreach (var key in result.Keys)
                {
                    fields.Add(key);
                    parameters.Add("@" + key);
                    command.Parameters.Add(new SqlParameter("@" + key, result[key]));
                }

                var fs = string.Join(",", fields.ToArray());
                var ps = string.Join(",", parameters.ToArray());

                builder.Append("INSERT INTO sys_TJ_MainData (" + fs + ") VALUES (" + ps + ")");
                command.CommandText = builder.ToString();
            }
            else
            {
                builder.Append("UPDATE sys_TJ_MainData SET ");
                foreach (var key in result.Keys)
                {
                    fields.Add(string.Format("{0} = @{0}", key));
                    parameters.Add("@" + key);
                    command.Parameters.Add(new SqlParameter("@" + key, result[key]));
                }

                builder.Append(string.Join(",", fields.ToArray()));
                builder.Append(" WHERE DataID = '" + id + "'");
                command.CommandText = builder.ToString();
            }

            if (command.Connection.State != ConnectionState.Open)
            {
                try
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();

                    return null;
                }
                catch (Exception e)
                {
                    Logger.Error("在添加统计项结果时发生了异常");
                    Logger.Error(command.CommandText);
                    Logger.Error(JsonConvert.SerializeObject(result));
                    Logger.Error(e);
                }
                finally
                {
                    try
                    {
                        command.Connection.Close();
                        command.Connection.Dispose();
                    }
                    catch (Exception) { }
                }
            }

            return null;
        }

        public string NewFactory(Sys_Factory factory)
        {
            var sql = @"INSERT INTO [dbo].[sys_TJ_Factory]
                               ([FactoryID]
                               ,[FactoryName]
                               ,[Address]
                               ,[LinkMan]
                               ,[Telephone]
                               ,[Longitude]
                               ,[Latitude]
                               ,[Remark]
                               ,[Status]
                               ,[CreateTime])
                         VALUES
                               (@FactoryID
                               ,@FactoryName
                               ,@Address
                               ,@LinkMan
                               ,@Telephone
                               ,@Longitude
                               ,@Latitude
                               ,@Remark
                               ,@Status
                               ,@CreateTime)";

            var command = GetDbCommand(sql);
            command.Parameters.Add(new SqlParameter("@FactoryID", factory.FactoryID));
            command.Parameters.Add(new SqlParameter("@FactoryName", factory.FactoryName));
            command.Parameters.Add(new SqlParameter("@Address", string.IsNullOrEmpty(factory.Address) ? "" : factory.Address));
            command.Parameters.Add(new SqlParameter("@LinkMan", string.IsNullOrEmpty(factory.LinkMan) ? "" : factory.LinkMan));
            command.Parameters.Add(new SqlParameter("@Telephone", string.IsNullOrEmpty(factory.Telephone) ? "" : factory.Telephone));
            command.Parameters.Add(new SqlParameter("@Longitude", factory.Longitude));
            command.Parameters.Add(new SqlParameter("@Latitude", factory.Latitude));
            command.Parameters.Add(new SqlParameter("@Remark", string.IsNullOrEmpty(factory.Remark) ? "" : factory.Remark));
            command.Parameters.Add(new SqlParameter("@Status", 1));
            command.Parameters.Add(new SqlParameter("@CreateTime", DateTime.Now));
            if (command.Connection.State != ConnectionState.Open)
            {
                try
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();

                    return null;
                }
                catch (Exception e)
                {
                    Logger.Error("在添加厂家信息时发生了异常");
                    Logger.Error(e);
                }
                finally
                {
                    try
                    {
                        command.Connection.Close();
                        command.Connection.Dispose();
                    }
                    catch (Exception) { }
                }
            }

            return "添加失败";
        }

        public DataTable GetFactoryByName(String factoryName)
        {
            try
            {
                var sql = "SELECT * FROM sys_TJ_Factory WHERE FactoryName = '" + factoryName + "' AND Status = 1 ORDER BY FactoryName";
                var table = GetDataTable(sql);

                return table;
            }
            catch (Exception e)
            {
                Logger.Error("在获取厂家信息时发生了异常");
                Logger.Error(e);
            }

            return null;
        }

        public DateTime GetLastEditDocument()
        {
            var sql = "SELECT MAX(LastEditedTime) FROM sys_TJ_MainData";
            try
            {
                var scalar = ExcuteScalar(sql);

                if (scalar != DBNull.Value)
                {
                    return Convert.ToDateTime(scalar);
                }

            }
            catch (Exception e)
            {
                Logger.Error("在统计内容表中获取最后的修改时间时发生了异常");
            }

            return DateTime.MinValue;
        }

        public string GetStandardValue(Guid itemID, string itemName, string model)
        {
            var sql = "SELECT StandardValue FROM dbo.sys_TJ_StandardValue WHERE ItemID = '" + itemID + "' AND ItemName='" + itemName + "'";
            try
            {
                var scalar = ExcuteScalar(sql);

                return Convert.ToString(scalar);
            }
            catch (Exception e)
            {
                Logger.Error("获取标准值时发生异常");
                Logger.Error(e);
            }

            return null;
        }

        public string UpdateStatisticsColumns(Guid itemID, string columns)
        {
            var sql = "UPDATE sys_TJ_Item SET Columns = '" + columns + "' WHERE ItemID = '" + itemID + "'";
            try
            {
                ExcuteCommand(sql);

                return null;
            }
            catch (Exception e)
            {
                Logger.Error("在更新统计项设置时发生了异常");
                Logger.Error(e);
            }

            return "保存失败";
        }

        public DataTable GetModulesSetting()
        {
            var sql = "SELECT ID,StatisticsMap FROM sys_module where StatisticsMap IS NOT null";
            try
            {
                return GetDataTable(sql);
            }
            catch (Exception e)
            {
                Logger.Error("在查询所有模板的统计项设置时发生了异常");
                Logger.Error(e);
            }

            return null;
        }

        public DataTable GetStatisticsItems()
        {
            var sql = "SELECT * FROM sys_TJ_Item";
            try
            {
                return GetDataTable(sql);
            }
            catch (Exception e)
            {
                Logger.Error("在查询所有统计项时发生了异常");
                Logger.Error(e);
            }

            return null;
        }

        public DataTable PickOneAcquisition(Guid documentID)
        {
            var sql = "SELECT TOP 1 UserName,MachineCode FROM dbo.sys_test_data WHERE DataID = '" + documentID + "' ORDER BY CreatedTime ASC";

            try
            {
                return GetDataTable(sql);
            }
            catch (Exception e)
            {
                Logger.Error("在获取一条采集信息时发生了异常");
                Logger.Error(e);
            }

            return null;
        }

        public DataTable GetFactories(string columns, string order)
        {
            var sql = "SELECT " + columns + " FROM sys_TJ_Factory WHERE Status = 1 ORDER BY " + order;
            return GetDataTable(sql);
        }

        public Sys_Factory GetFactory(string columns, Guid id)
        {
            var sql = "SELECT " + columns + " FROM sys_TJ_Factory WHERE FactoryID  = '" + id + "'";


            try
            {
                var table = GetDataTable(sql);

                if (table != null && table.Rows.Count > 0)
                {
                    return Sys_Factory.Parse(table.Rows[0]);
                }
            }
            catch (Exception e)
            {
                Logger.Error("在获取厂家信息时发生异常, FactoryID:" + id);
                Logger.Error(e);
            }

            return null;

        }

        public string MergeFactory(string[] ids, Sys_Factory factory)
        {
            var sql1 = "UPDATE sys_TJ_Factory SET [Status] = 0 WHERE FactoryID IN ('" + string.Join("','", ids) + "')";

            var sql2 = "UPDATE sys_TJ_MainData SET FactoryName = '" + factory.FactoryName + "', FactoryID = '" + factory.FactoryID + "' WHERE FactoryID IN ('" + string.Join("','", ids) + "')";

            var sql3 = string.Format("UPDATE sys_TJ_Factory SET FactoryName = '{0}', Latitude = '{1}', Longitude = '{2}', Remark = '{3}', Telephone = '{4}', LinkMan = '{5}', Address = '{6}', [Status] = 1 WHERE FactoryID = '" + factory.FactoryID + "' ",
                factory.FactoryName, factory.Latitude, factory.Longitude, factory.Remark, factory.Telephone, factory.LinkMan, factory.Address);

            ExcuteCommands(new string[] { sql1, sql2, sql3 });

            return null;
        }

//        public string NewStatisticsResult(Dictionary<string, Object> result)
//        {
//            if (result.Keys.Count == 0)
//            {
//                return null;
//            }

//            var builder = new StringBuilder();
//            var fields = new List<string>();
//            var parameters = new List<string>();
//            var command = GetDbCommand("");

//            var id = Convert.ToString(result["DataID"]);
//            var count = 0;

//            try
//            {
//                var countScalar = "SELECT COUNT(DataID) FROM sys_TJ_MainData WHERE DataID = '" + id + "'";
//                count = Convert.ToInt32(ExcuteScalar(countScalar));
//            }
//            catch (Exception e)
//            {
//                Logger.Error("获取重复的 DataID 数量出现问题" + e);
//                return "添加失败";
//            }

//            if (count == 0)
//            {
//                foreach (var key in result.Keys)
//                {
//                    fields.Add(key);
//                    parameters.Add("'" + result[key] + "'");
//                }

//                var fs = string.Join(",", fields.ToArray());
//                var ps = string.Join(",", parameters.ToArray());

//                builder.Append("INSERT INTO sys_TJ_MainData (" + fs + ") VALUES (" + ps + ")");
//            }
//            else
//            {
//                builder.Append("UPDATE sys_TJ_MainData SET ");
//                foreach (var key in result.Keys)
//                {
//                    fields.Add(string.Format("{0} = '{1}'", key, result[key]));
//                }

//                builder.Append(string.Join(",", fields.ToArray()));
//                builder.Append(" WHERE DataID = '" + id + "'");
//            }

//            File.AppendAllText("statistics.txt", Environment.NewLine + builder.ToString());

//            return null;
//        }

//        public string NewFactory(Sys_Factory factory)
//        {
//            var sql = @"INSERT INTO [dbo].[sys_TJ_Factory]
//                       ([FactoryID]
//                       ,[FactoryName]
//                       ,[Address]
//                       ,[LinkMan]
//                       ,[Telephone]
//                       ,[Longitude]
//                       ,[Latitude]
//                       ,[Remark]
//                       ,[Status]
//                       ,[CreateTime])
//                 VALUES
//                       ('" + factory.FactoryID + @"'
//                       ,'" + factory.FactoryName + @"'
//                       ,'" + factory.Address + @"'
//                       ,'" + factory.LinkMan + @"'
//                       ,'" + factory.Telephone + @"'
//                       ,'" + factory.Longitude + @"'
//                       ,'" + factory.Latitude + @"'
//                       ,'" + factory.Remark + @"'
//                       ,'" + factory.Status + @"'
//                       ,'" + factory.CreateTime + "')";

//            sql = Environment.NewLine + sql;
//            File.AppendAllText("factory.txt", sql);

//            //var command = GetDbCommand(sql);
//            //command.Parameters.Add(new SqlParameter("@FactoryID", factory.FactoryID));
//            //command.Parameters.Add(new SqlParameter("@FactoryName", factory.FactoryName));
//            //command.Parameters.Add(new SqlParameter("@Address", string.IsNullOrEmpty(factory.Address) ? "" : factory.Address));
//            //command.Parameters.Add(new SqlParameter("@LinkMan", string.IsNullOrEmpty(factory.LinkMan) ? "" : factory.LinkMan));
//            //command.Parameters.Add(new SqlParameter("@Telephone", string.IsNullOrEmpty(factory.Telephone) ? "" : factory.Telephone));
//            //command.Parameters.Add(new SqlParameter("@Longitude", factory.Longitude));
//            //command.Parameters.Add(new SqlParameter("@Latitude", factory.Latitude));
//            //command.Parameters.Add(new SqlParameter("@Remark", string.IsNullOrEmpty(factory.Remark) ? "" : factory.Remark));
//            //command.Parameters.Add(new SqlParameter("@Status", 1));
//            //command.Parameters.Add(new SqlParameter("@CreateTime", DateTime.Now));
//            //if (command.Connection.State != ConnectionState.Open)
//            //{
//            //    try
//            //    {
//            //        command.Connection.Open();
//            //        command.ExecuteNonQuery();

//            //        return null;
//            //    }
//            //    catch (Exception e)
//            //    {
//            //        Logger.Error("在添加厂家信息时发生了异常");
//            //        Logger.Error(e);
//            //    }
//            //    finally
//            //    {
//            //        try
//            //        {
//            //            command.Connection.Close();
//            //            command.Connection.Dispose();
//            //        }
//            //        catch (Exception) { }
//            //    }
//            //}

//            return null;
//        }

        public DataTable GetDocumentWithStatistics(Guid documentID)
        {
            var sql = @"SELECT document.ID,document.SegmentCode,document.CompanyCode,document.TestRoomCode,document.CreatedTime,document.ModuleID,document.Data,document.BGRQ,document.WTBH,document.BGBH,document.LastEditedTime
                FROM sys_document document JOIN sys_TJ_Item_Module module ON module.ModuleID = document.ModuleID
                WHERE document.ID = '" + documentID + "'";

            return GetDataTable(sql);
        }

        public bool DocumentNotExistsOrNew(Guid documentID, DateTime lastEditTime)
        {
            var sql = "SELECT COUNT(1) FROM sys_TJ_MainData WHERE DataID = '" + documentID + "' AND LastEditedTime >= '" + lastEditTime + "'";
            var rows =  Convert.ToInt32(ExcuteScalar(sql));
            return rows > 0;
        }
    }
}
