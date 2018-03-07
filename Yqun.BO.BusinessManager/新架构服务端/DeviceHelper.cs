using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data.SqlClient;
using Yqun.Common.ContextCache;
using System.Data;
using System.Data.Common;

namespace Yqun.BO.BusinessManager
{
    public class DeviceHelper : BOBase
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool AddDevice(Sys_Device device)
        {
            var sql = "INSERT INTO [dbo].[sys_devices]" +
           " ([ID]" +
           ", [MachineCode]" +
           ", [DeviceType]" +
           ", [CreateTime]" +
           ", [CreateBy]" +
           ", [DeviceCompany]" +
           ", [IsDYSF]" +
           ", [RemoteCode1]" +
           ", [RemoteCode2]" +
           ", [Comment]" +
           ", [IsActive]" +
           ", [LastEditBy]" +
           ", [LastEditTime]" +
           ", [TestRoomCode]" +
           ", [Quantum])" +
     " VALUES" +
           " (@ID" +
           " ,@MachineCode" +
           " ,@DeviceType" +
           " ,@CreateTime" +
           " ,@CreateBy" +
           " ,@DeviceCompany" +
           " ,@IsDYSF" +
           " ,@RemoteCode1" +
           " ,@RemoteCode2" +
           " ,@Comment" +
           " ,@IsActive" +
           " ,@LastEditBy" +
           " ,@LastEditTime" +
           " ,@TestRoomCode" +
           " ,@Quantum)";

            var command = GetDbCommand(sql);
            command.Parameters.Add(new SqlParameter("@ID", device.ID.ToString()));
            command.Parameters.Add(new SqlParameter("@MachineCode", device.MachineCode));
            command.Parameters.Add(new SqlParameter("@DeviceType", (int)device.DeviceType));
            command.Parameters.Add(new SqlParameter("@CreateTime", device.CreateTime));
            command.Parameters.Add(new SqlParameter("@CreateBy", device.CreateBy));
            command.Parameters.Add(new SqlParameter("@DeviceCompany", device.DeviceCompany));
            command.Parameters.Add(new SqlParameter("@IsDYSF", device.IsDYSF));
            command.Parameters.Add(new SqlParameter("@RemoteCode1", device.RemoteCode1));
            command.Parameters.Add(new SqlParameter("@RemoteCode2", device.RemoteCode2));
            command.Parameters.Add(new SqlParameter("@Comment", device.Comment));
            //command.Parameters.Add(new SqlParameter("@ClientConfig", device.ClientConfig));
            //command.Parameters.Add(new SqlParameter("@ConfigUpdateTime", null));
            //command.Parameters.Add(new SqlParameter("@ConfigStatus", device.ConfigStatus));
            command.Parameters.Add(new SqlParameter("@IsActive", device.IsActive));
            command.Parameters.Add(new SqlParameter("@LastEditBy", device.LastEditBy));
            command.Parameters.Add(new SqlParameter("@LastEditTime", device.LastEditTime));
            command.Parameters.Add(new SqlParameter("@TestRoomCode", device.TestRoomCode));
            command.Parameters.Add(new SqlParameter("@Quantum", device.Quantum));

            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                var result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            finally
            {
                try
                {
                    command.Connection.Close();
                }
                catch (Exception) { }
            }

            Logger.Error("添加设备没有成功。");
            return false;
        }

        public bool EditDevice(Sys_Device device)
        {
            var sql = "UPDATE [dbo].[sys_devices]" +
             " SET " +
             "  [DeviceType] = @DeviceType" +
             " ,[IsDYSF] = @IsDYSF" +
             " ,[RemoteCode1] = @RemoteCode1" +
             " ,[RemoteCode2] = @RemoteCode2" +
             " ,[DeviceCompany] = @DeviceCompany" +
             " ,[Comment] = @Comment" +
             " ,[IsActive] = @IsActive" +
             " ,[LastEditBy] = @LastEditBy" +
             " ,[LastEditTime] = @LastEditTime" +
             " ,[TestRoomCode] = @TestRoomCode" +
             " ,[Quantum] = @Quantum" +
             " ,[MachineCode] = @MachineCode" +
             " WHERE ID = @ID";

            var command = GetDbCommand(sql);
            command.Parameters.Add(new SqlParameter("@ID", device.ID.ToString()));
            command.Parameters.Add(new SqlParameter("@DeviceType", (int)device.DeviceType));
            command.Parameters.Add(new SqlParameter("@IsDYSF", device.IsDYSF));
            command.Parameters.Add(new SqlParameter("@RemoteCode1", device.RemoteCode1));
            command.Parameters.Add(new SqlParameter("@RemoteCode2", device.RemoteCode2));
            command.Parameters.Add(new SqlParameter("@DeviceCompany", device.DeviceCompany));
            command.Parameters.Add(new SqlParameter("@Comment", device.Comment));
            command.Parameters.Add(new SqlParameter("@IsActive", device.IsActive));
            command.Parameters.Add(new SqlParameter("@LastEditBy", device.LastEditBy));
            command.Parameters.Add(new SqlParameter("@LastEditTime", device.LastEditTime));
            command.Parameters.Add(new SqlParameter("@TestRoomCode", device.TestRoomCode));
            command.Parameters.Add(new SqlParameter("@Quantum", device.Quantum));
            command.Parameters.Add(new SqlParameter("@MachineCode", device.MachineCode));

            try
            {
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                var result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            finally
            {
                try
                {
                    command.Connection.Close();
                }
                catch (Exception) { }
            }

            Logger.Error("添加设备没有成功。ID:" + device.ID);
            return false;
        }

        public bool DeleteDevice(List<string> IDs)
        {
            try
            {
                foreach (var id in IDs)
                {
                    ExcuteCommand("UPDATE [dbo].[sys_devices] SET [IsActive] = 1 WHERE ID = '" + id + "'");
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return false;
        }

        public Sys_Page GetDeviceList(int index, int size, string query)
        {
            //SELECT d.*,c.单位名称,c.标段名称,c.试验室名称 FROM dbo.sys_devices d LEFT JOIN dbo.v_bs_codeName c ON c.试验室编码 = d.TestRoomCode
            var cmd = GetDbCommand("sp_pager");
            var counter = DocountEnum.GetData;
            var result = new Sys_Page()
            {
                Index = index,
                Size = size,
                TotalCount = 0
            };
        BUILDCMD:
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@fldName", "d.MachineCode"));
            cmd.Parameters.Add(new SqlParameter("@PageSize", size));
            cmd.Parameters.Add(new SqlParameter("@PageIndex", index));
            cmd.Parameters.Add(new SqlParameter("@OrderType", OrderEnum.Desc));
            cmd.Parameters.Add(new SqlParameter("@strWhere", query));
            cmd.Parameters.Add(new SqlParameter("@tblname", "dbo.sys_devices d LEFT JOIN dbo.v_bs_codeName c ON c.试验室编码 = d.TestRoomCode"));
            cmd.Parameters.Add(new SqlParameter("@strGetFields", "d.*,c.单位名称,c.标段名称,c.试验室名称,c.单位编码,c.标段编码,c.试验室编码"));
            cmd.Parameters.Add(new SqlParameter("@doCount", counter));

            if (counter == DocountEnum.GetData)
            {
                result.Source = GetTable(cmd);
                counter = DocountEnum.GetTotalCount;

                goto BUILDCMD;
            }
            else
            {
                var table = GetTable(cmd);
                if (table.Rows.Count > 0)
                {
                    var value = table.Rows[0][0];
                    if (value != null)
                    {
                        var cell = value.ToString();
                        var total = 0;
                        int.TryParse(cell, out total);

                        result.TotalCount = total;
                    }
                }
            }

            return result;
        }

        private DataTable GetTable(IDbCommand cmd)
        {
            try
            {
                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();
                }

                cmd.Connection = Connection as SqlConnection;
                var adapter = new SqlDataAdapter(cmd as SqlCommand);
                var set = new DataSet();
                adapter.Fill(set);
                return set.Tables[0];
            }
            catch (Exception ex)
            {
                Logger.Error("获取数据表时发生了错误。" + ex.Message);
                return null;
            }
            finally
            {
                if (Connection.State != ConnectionState.Closed)
                {
                    try
                    {
                        Connection.Close();
                    }
                    catch (Exception) { }
                }
            }
        }

        public string GenerateMachineCode(string testRoomCode)
        {
            var sql = string.Format("SELECT MAX(MachineCode) FROM dbo.sys_devices WHERE TestRoomcode = '{0}'", testRoomCode);
            var scalar = ExcuteScalar(sql);
            var code = scalar.ToString();

            if (scalar == null || string.IsNullOrEmpty(code) || code.Length < 4)
            {
                return string.Format("{0}{1}", testRoomCode, "0001");
            }

            var substring = code.Substring(code.Length - 4, 4);
            var number = 0;
            if (int.TryParse(substring, out number))
            {
                if (number == 9999)
                {
                    Logger.Error("设备编码已超过 9999，无法为以下设备分配编码。");
                }
                else
                {
                    return string.Format("{0}{1}", testRoomCode, (++number).ToString().PadLeft(4, '0'));
                }
            }
            else
            {
                Logger.Error("在生成 TestRoomCode 时出现错误，查到的实验室编码不正确。 TestRoomCode : " + code);
            }

            return "";
        }

        public DataTable GetDevice(string machineCode)
        {
            var sql = "SELECT * FROM sys_devices WHERE MachineCode = '" + machineCode + "'";
            return GetDataTable(sql);
        }

        public DataTable LoadDeviceRoles()
        {
            var sql = @"SELECT 
                        code.标段编码,
                        code.标段名称,
                        code.单位编码,
                        code.单位名称,
                        code.试验室编码,
                        code.试验室名称,
                        device.ID 设备ID,
                        device.MachineCode 设备编码,
                        device.IsDYSF 电液伺服,
                        device.DeviceType 设备类型
                        FROM dbo.v_bs_codeName code LEFT JOIN dbo.sys_devices device
                        ON code.试验室编码 = device.TestRoomCode AND device.IsActive = 0";

            return GetDataTable(sql);
        }

        public string[] GetUserDeviceRole(string userID)
        {
            var sql = "SELECT Devices FROM sys_auth_Users WHERE ID = '" + userID + "'";
            var scalar = ExcuteScalar(sql);
            var roles = scalar == null ? "" : scalar.ToString();

            return roles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public int SaveDeviceRoles(string userID, string[] devicesID)
        {
            var code = Yqun.Common.ContextCache.ApplicationContext.Current.UserCode;
            var sql = "UPDATE sys_auth_Users SET Devices = '" + string.Join(",", devicesID) + "' WHERE ID = '" + userID + "'";
            return ExcuteCommand(sql);
        }

        /// <summary>
        /// 獲取設備情況數據
        /// </summary>
        /// <returns></returns>
        public DataSet GetDeviceSummary(string testRoomCode, int meet, int pageIndex, int pageSize)
        {

            var filter = " AND Status>0  ";
            var sql = @"DECLARE @Page int
                        DECLARE @PageSize int
                        SET @Page = {1}
                        SET @PageSize = {2}
                        SET NOCOUNT ON
                        DECLARE @TempTable TABLE (IndexId int identity, _keyID varchar(200))
                        INSERT INTO @TempTable
                        (
	                        _keyID
                        )
                        select id from  sys_document join Sys_Tree t1 on t1.NodeCode = sys_document.SegmentCode   where ModuleID in('A0C51954-302D-43C6-931E-0BAE2B8B10DB') {0}  order by t1.OrderID asc

                         select  
                    id,
                    t1.description as '标段名称',
                    t2.description as '单位名称',
                    t3.description as '试验室名称',
                    t3.nodecode as '试验室编码',
                    Ext2 as '管理编号',
                    Ext1 as '设备名称',
                    Ext3 as '生产厂家',
                    Ext4 as '规格型号',
                    Ext9 as '数量',
                    Ext11 as '检定情况',
                    Ext12 as '检定证书编号',
                    Ext21 as '上次校验日期',
                    Ext22 as '预计下次校验日期',
                    Ext15 as '检定周期'
                     from  sys_document 
                     join Sys_Tree t1 on t1.NodeCode = sys_document.SegmentCode
                     join Sys_Tree t2 on t2.NodeCode = sys_document.CompanyCode
                     join Sys_Tree t3 on t3.NodeCode = sys_document.TestRoomCode
                        INNER JOIN @TempTable t ON sys_document.id = t._keyID
                        WHERE t.IndexId BETWEEN ((@Page - 1) * @PageSize + 1) AND (@Page * @PageSize)
                        AND ModuleID in('A0C51954-302D-43C6-931E-0BAE2B8B10DB') 
                         {0}        
                        Order By  t1.OrderID  asc
                        DECLARE @C int
                        select @C=Count(id) from  sys_document   where ModuleID in('A0C51954-302D-43C6-931E-0BAE2B8B10DB') {0}
                        select @C ";

            //A14 I14
            switch (meet)
            {
                case 1:
                    filter += " AND Ext22 <'" + DateTime.Now.AddDays(1).AddSeconds(-1).ToString() + "' ";
                    break;
                case 2:
                    filter += " AND Ext22 >'" + DateTime.Now.AddDays(1).AddSeconds(-1).ToString() + "' ";
                    break;
            }

            if (string.IsNullOrEmpty(testRoomCode) == false)
            {
                filter += " and TestRoomCode = '" + testRoomCode + "'";
            }

            var cmd = string.Format(sql, filter, pageIndex, pageSize);
            var dataset = GetDataSet(cmd);
            return dataset;
        }
    }
}
