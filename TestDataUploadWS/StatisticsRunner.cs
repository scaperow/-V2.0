using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Collections;
using System.ServiceModel;
using TransferServiceCommon;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading;
using BizCommon;
using FarPoint.Win.Spread;
using FarPoint.Win;
using Newtonsoft.Json;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Yqun.Services;

namespace TestDataUploadWS
{
    public class StatisticsRunner
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string DLL = "Yqun.BO.BusinessManager.dll";
        public string LineName, IPAddress, Port, DBAddress, DBUser, DBPassword, DBName;
        public event EventHandler Finish;
        public Dictionary<string, Sys_Factory> Factories = new Dictionary<string, Sys_Factory>();
        public Dictionary<string, StatisticsSetting> Statistics = new Dictionary<string, StatisticsSetting>();
        public Dictionary<Guid, Sys_Module> Modules = new Dictionary<Guid, Sys_Module>();
        public Dictionary<Guid, List<Sys_Sheet>> Sheets = new Dictionary<Guid, List<Sys_Sheet>>();
        public Dictionary<Guid, DataRow> References = new Dictionary<Guid, DataRow>();
        public string TimeSet = "";
        public bool Stop = false;

        public string Uri
        {
            get
            {
                return "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
            }
        }


        private void StartStatistics()
        {
            List<Sys_Line> lstLine;
            string strLocalServerIP = ConfigurationManager.AppSettings["LocalServerIP"];
            String libAddress = "net.tcp://lib.kingrocket.com:8066/TransferService.svc";
            object objLib = CallRemoteServerMethod(libAddress, "Yqun.BO.BusinessManager.dll", "GetStatisticsLinesByIP",
                new Object[] { strLocalServerIP });

            if (objLib != null)
            {
                lstLine = objLib as List<Sys_Line>;
                foreach (Sys_Line line in lstLine)
                {
                    Logger.Info("开始同步线路 " + line.LineName + " 的数据");

                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        var runner = new StatisticsRunner(line);
                        runner.Start();
                    });
                }
            }
            else
            {
                Logger.Info(string.Format("没有需要同步的线路"));
            }
        }

        public StatisticsRunner(Sys_Line line)
        {
            LineName = line.LineName;
            Port = line.LinePort;
            IPAddress = line.LineIP;
            DBAddress = line.LineIP;
            DBUser = line.UserName;
            DBPassword = line.PassWord;
            DBName = line.DataBaseName;
            TimeSet = ConfigurationManager.AppSettings["StatisticsTimeSpan"];
        }

        public static bool Running = false;
        public void StartApplyQueue()
        {

            ThreadPool.QueueUserWorkItem(delegate
            {
                Logger.Info("开始执行线路, 路径: " + Uri);

                try
                {
                    Start();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }

                if (Finish != null)
                {
                    Finish(this, EventArgs.Empty);
                }

                Running = false;
            });
        }

        private DataTable GetDocumentsToStart(int pageIndex, string lastTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(string.Format("Data Source={0}; Initial Catalog={1}; Persist Security Info=True; User ID={2}; Password={3};", new object[]
			{
				this.DBAddress,
				this.DBName,
				this.DBUser,
				this.DBPassword
			})))
                {
                    
                    var all = 0;
                    var order = 1;
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    cmd.Parameters.Clear();
                    cmd.CommandText = "sp_pager";
                    cmd.CommandTimeout = 1000 * 60 * 10;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@tblname", "sys_document  d join sys_TJ_Item_Module m on d.moduleid = m.moduleid "));
                    cmd.Parameters.Add(new SqlParameter("@strGetFields", " d.ID "));//a.ID,a.ModuleID,a.DataID,a.TestData,c.标段名称,c.单位名称,c.试验室名称,a.UserName AS 试验员,a.CreatedTime AS 实际试验日期,a.SGComment AS 延时原因,d.Name AS 模板名称,a.WTBH,s.EndTime - 1 as 龄期到期日期,a.SerialNumber
                    cmd.Parameters.Add(new SqlParameter("@fldName", " d.LastEditedTime "));
                    cmd.Parameters.Add(new SqlParameter("@PageSize", 1000));
                    cmd.Parameters.Add(new SqlParameter("@PageIndex", ++pageIndex));
                    cmd.Parameters.Add(new SqlParameter("@OrderType", order));
                    //cmd.Parameters.Add(new SqlParameter("@strWhere", " AND d.LastEditedTime >= '" + lastTime + "' AND d.Status > 0"));
                    cmd.Parameters.Add(new SqlParameter("@strWhere", " AND d.Status > 0"));
                    cmd.Parameters.Add(new SqlParameter("@doCount", all));

                    var adapter = new SqlDataAdapter(cmd);
                    var dataset = new DataSet();
                    adapter.Fill(dataset);
                    connection.Close();
                    return dataset.Tables[0];

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Logger.Error(e.ToString());
            }

            return null;
        }

        public void Start()
        {
            int num = 0;
            int index = 0;
            var time = CallRemoteServerMethod(Uri, "Yqun.BO.BusinessManager.dll", "GetLastEditDocument", new object[] { });
            var last = DateTime.MinValue;
            var l = "1999-01-01";
            if (DateTime.TryParse(time.ToString(), out last))
            {
                if (last != DateTime.MinValue && last > DateTime.MinValue)
                {
                    l = last.ToString();
                }
            }

            while (true)
            {
                var table = GetDocumentsToStart(++index, l);
                if (table == null || table.Rows == null || table.Rows.Count == 0)
                {
                    Console.WriteLine("获取到空数据");
                    Logger.Info("获取到空数据");
                    break;
                }

                foreach (DataRow row in table.Rows)
                {
                    try
                    {
                        num++;
                        string text = @"SELECT document.SegmentCode,document.CompanyCode,document.TestRoomCode,document.CreatedTime,document.ModuleID,document.Data,document.BGRQ,document.WTBH,document.BGBH,document.LastEditedTime
                                        FROM sys_document document WHERE document.ID = '" + row["ID"] + "' AND (BGBH IS NOT NULL  OR WTBH IS NOT NULL) AND DataName NOT LIKE '%作废%' AND DataName NOT LIKE '%测试%'";
                        var dataTable = this.CallLocalMethod(this.Uri, "Yqun.BO.BusinessManager.dll", "GetDocumentWithStatistics", new object[]
						{
							new Guid(Convert.ToString(row["ID"]))
						}) as DataTable;

                        if (dataTable != null && dataTable.Rows != null && dataTable.Rows.Count > 0)
                        {
                            DataRow dataRow = dataTable.Rows[0];
                            if (!(bool)this.CallLocalMethod(this.Uri, "Yqun.BO.BusinessManager.dll", "DocumentNotExistsOrNew", new object[]
							{
								dataRow["ID"],
								dataRow["LastEditedTime"]
							}))
                            {
                                Console.WriteLine("正在处理第[" + num + "]条数据");
                                StatisticsRunner.Logger.Info("正在处理第[" + num + "]条数据");
                                this.StartDocument(this.Uri, dataRow);
                                GC.Collect();
                                Thread.Sleep(10);
                                continue;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        StatisticsRunner.Logger.Error(ex);
                    }

                    Console.WriteLine("已跳过第[" + num + "]条数据");
                    Thread.Sleep(0);
                }
            }


            Console.WriteLine(this.LineName + " 数据同步完毕");
            StatisticsRunner.Logger.Info(this.LineName + " 数据同步完毕");
        }

        private void StartDocument(string uri, DataRow reader)
        {
            try
            {
                var id = new Guid(Convert.ToString(reader["ID"]));
                if (Guid.Empty == id)
                {
                    Logger.Error(reader["ID"] + "解析到的ID为空");
                    return;
                }

                Logger.Info("开始处理编号为" + id + "的文档");

                var data = Convert.ToString(reader["Data"]);
                var moduleID = new Guid(Convert.ToString(reader["ModuleID"]));

                Sys_Module module = null;
                if (Modules.ContainsKey(moduleID))
                {
                    module = Modules[moduleID];
                }
                else
                {
                    module = CallLocalMethod(uri, DLL, "GetModuleBaseInfoByID", new object[] { moduleID }) as Sys_Module;
                    if (module != null)
                    {
                        Modules[moduleID] = module;
                    }
                }

                if (module == null)
                {
                    Logger.Info("获取到的模板为空, ModuleID = " + moduleID);
                }

                if (module == null)
                {
                    Logger.Error("获取到的模板为空:ModuleID" + moduleID);
                    return;
                }

                if (module.StatisticsSettings == null)
                {
                    Logger.Error("模板的设置为空");
                    return;
                }

                var document = JsonConvert.DeserializeObject<JZDocument>(data);
                if (document == null)
                {
                    Logger.Error("报告的 Data 没有成功转换成 JZDocument");
                    return;
                }

                List<Sys_Sheet> sheets = null;
                if (Sheets.ContainsKey(moduleID))
                {
                    sheets = Sheets[moduleID];
                }
                else
                {
                    sheets = CallLocalMethod(uri, DLL, "GetSheetsByModuleID", new object[] { moduleID }) as List<Sys_Sheet>;
                    if (sheets != null)
                    {
                        Sheets[moduleID] = sheets;
                    }
                }

                if (sheets == null)
                {
                    Logger.Info("通过模板获取到的Sheet为空, ModuleID = " + moduleID);
                    return;
                }

                var map = new Dictionary<string, Object>();
                DataRow statistics = null;
                if (References.ContainsKey(moduleID))
                {
                    statistics = References[moduleID];
                }
                else
                {
                    var statisticses = CallLocalMethod(uri, DLL, "GetStatisticsReference", new object[] { moduleID }) as DataTable;
                    if (statisticses == null || statisticses.Rows.Count == 0)
                    {
                        Logger.Error("获取到的关联统计项列表为空, ModuleID:" + moduleID);
                    }

                    if (statisticses.Rows.Count > 0)
                    {
                        statistics = statisticses.Rows[0];
                        References[moduleID] = statistics;
                    }

                    if (statisticses.Rows.Count > 1)
                    {
                        Logger.Info("获取到多个关联的统计项，将取第一个, ModuleID:" + moduleID);
                    }
                }

                if (statistics == null)
                {
                    return;
                }

                var items = Convert.ToString(statistics["Columns"]);
                var columns = JsonConvert.DeserializeObject<List<StatisticsSetting>>(items);
                if (columns == null)
                {
                    columns = new List<StatisticsSetting>();
                }

                map["DataID"] = id;
                map["ModuleID"] = moduleID;
                map["SegmentCode"] = reader["SegmentCode"];
                map["CompanyCode"] = reader["CompanyCode"];
                map["TestRoomCode"] = reader["TestRoomCode"];
                map["ItemType"] = statistics["ItemType"];
                map["ItemID"] = statistics["ItemID"];
                map["LastEditedTime"] = reader["LastEditedTime"];
                map["BGRQ"] = reader["BGRQ"];
                map["BGBH"] = reader["BGBH"];

                foreach (var set in module.StatisticsSettings)
                {
                    if (ProcessExtension(document, set, columns, reader, module, map))
                    {
                        continue;
                    }

                    var column = columns.Find(m => m.ItemName == set.StatisticsItemName);
                    var sheet = document.Sheets.Find(m => m.ID == set.SheetID);
                    if (sheet == null)
                    {
                        Logger.Info("SheetID " + set.SheetID + " 在模板关联中不存在");
                        continue;
                    }

                    var value = GetCellValue(sheet, set.CellName);
                    if (value == null)
                    {

                        Logger.Info("CellName " + set.CellName + "SheetName " + set.SheetName + "SheetID " + set.SheetID + " 获取到的数据为空");
                        continue;
                    }

                    var formatedValue = FormatValue(set.StatisticsItemName, value);
                    if (string.IsNullOrEmpty(formatedValue))
                    {
                        Logger.Info("CellName " + set.CellName + "SheetName " + set.SheetName + "SheetID " + set.SheetID + " 获取到的数据不合法, 数据为" + value);
                        continue;
                    }

                    if (column == null)
                    {
                        map[set.BindField] = formatedValue;
                    }
                    else
                    {
                        map[column.BindField] = formatedValue;
                    }
                }

                var message = CallLocalMethod(uri, DLL, "NewStatisticsResult", new object[] { map }) as string;
                if (string.IsNullOrEmpty(message))
                {
                    Logger.Info("成功入库 DataID " + id);
                }
                else
                {
                    Logger.Info("入库失败 DataID " + id);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public string FormatValue(string name, object value)
        {
            if (value == null)
            {
                return null;
            }

            var vs = Convert.ToString(value);
            if (string.IsNullOrEmpty(vs))
            {
                return null;
            }

            if (name == "型号")
            {
                vs = vs.Replace("P.", "P·");
                vs = vs.Replace("S.", "S·");
                vs = vs.Replace("~", "～");
            }

            vs = vs.Replace("#VALUE!", "");
            vs = vs.Replace(@"/", "");
            vs = vs.Replace("#N/A", "");
            vs = vs.Replace("##REF!", "");
            vs = vs.Replace("##DIV/0!", "");
            vs = vs.Replace("##NUM!", "");
            vs = vs.Replace("##NAME?", "");
            vs = vs.Replace("##NULL!", "");
            vs = vs.Replace("False", "");
            vs = vs.Replace("True", "");
            vs = vs.Replace("false", "");
            vs = vs.Replace("true", "");

            return vs;
        }

        public object GetCellValue(JZSheet sheet, string name)
        {
            if (string.IsNullOrEmpty(name) || sheet == null)
            {
                return null;
            }

            foreach (var cell in sheet.Cells)
            {
                if (cell.Name == name)
                {
                    return cell.Value;
                }
            }

            return null;
        }

        public bool ProcessExtension(JZDocument sheet, StatisticsModuleSetting map, List<StatisticsSetting> columns, DataRow document, Sys_Module module, Dictionary<string, Object> result)
        {
            var documentID = new Guid(Convert.ToString(document["ID"]));

            switch (map.StatisticsItemName)
            {
                case "报告日期":
                    return true;

                case "厂家名称":
                    return ProcessFactory(documentID, module, result, sheet);

                case "试验人员":
                case "设备厂家":
                case "设备型号":
                case "仪表型号":
                    var device = CallLocalMethod(Uri, DLL, "PickOneAcquisition", new object[] { documentID }) as DataTable;
                    if (device == null || device.Rows.Count == 0)
                    {
                        Logger.Info("获取 " + map.StatisticsItemName + " 到空值");
                        return true;
                    }

                    var userName = Convert.ToString(device.Rows[0]["UserName"]);
                    var machineCode = Convert.ToString(device.Rows[0]["MachineCode"]);

                    var item = columns.Find(m => m.ItemName == map.StatisticsItemName);
                    if (item == null)
                    {
                        Logger.Info("在统计项中获取名为 " + map.StatisticsItemName + " 的项为空");
                        return true;
                    }

                    switch (map.StatisticsItemName)
                    {
                        case "试验人员":
                            result[item.BindField] = userName;
                            break;

                        default:
                            result[item.BindField] = machineCode;
                            break;
                    }
                    return true;
            }

            return false;
        }

        public bool ProcessFactory(Guid documentID, Sys_Module module, Dictionary<string, Object> result, JZDocument document)
        {

            var factoryName = "";
            var factorySet = module.StatisticsSettings.Find(m => m.BindField == "FactoryName");
            if (factorySet == null)
            {
                Logger.Error("获取到的厂家名称配置项为空");
                return true;
            }

            factoryName = GetCellValue(document.Sheets.Find(m => m.ID == factorySet.SheetID), factorySet.CellName) as string;
            if (string.IsNullOrEmpty(factoryName))
            {
                Logger.Error("获取到的厂家名称为空, DocumentID:" + documentID);
                return true;
            }

            Sys_Factory factory = null;
            if (Factories.ContainsKey(factoryName))
            {
                factory = Factories[factoryName];
            }
            else
            {
                var table = CallLocalMethod(Uri, DLL, "GetFactoryByName", new object[] { factoryName }) as DataTable;
                if (table == null || table.Rows.Count == 0)
                {
                    factory = new Sys_Factory();
                    factory.FactoryID = Guid.NewGuid();
                    factory.FactoryName = factoryName;
                    factory.CreateTime = DateTime.Now;
                    var resultNewFactory = CallLocalMethod(Uri, DLL, "NewFactory", new object[] { factory }) as string;
                    if (string.IsNullOrEmpty(resultNewFactory))
                    {
                        Factories[factoryName] = factory;
                    }
                    else
                    {
                        Logger.Error("添加厂家时失败, 信息:" + resultNewFactory);
                    }
                }
                else
                {
                    factory = new Sys_Factory();
                    factory.FactoryID = new Guid(Convert.ToString(table.Rows[0]["FactoryID"]));
                    factory.FactoryName = Convert.ToString(table.Rows[0]["FactoryName"]);
                }
            }

            if (factory != null)
            {
                result["FactoryID"] = factory.FactoryID;
                result["FactoryName"] = factoryName;
            }

            return true;
        }

        public object CallLocalMethod(string address, string AssemblyName, string methodName, object[] parameters)
        {
            return Transfer.CallLocalServiceWithDBArgs(AssemblyName, methodName, parameters, IPAddress, DBName, DBUser, DBPassword);
        }

        public object CallRemoteServerMethod(string address, string AssemblyName, string MethodName, object[] Parameters)
        {
            object obj = null;
            try
            {
                using (ChannelFactory<ITransferService> channelFactory = new ChannelFactory<ITransferService>("sClient", new EndpointAddress(address)))
                {

                    ITransferService proxy = channelFactory.CreateChannel();
                    using (proxy as IDisposable)
                    {
                        Hashtable hashtable = new Hashtable();
                        hashtable["assembly_name"] = AssemblyName;
                        hashtable["method_name"] = MethodName;
                        hashtable["method_paremeters"] = Parameters;

                        Stream source_stream = Yqun.Common.Encoder.Serialize.SerializeToStream(hashtable);
                        Stream zip_stream = Yqun.Common.Encoder.Compression.CompressStream(source_stream);
                        source_stream.Dispose();
                        Stream stream_result = proxy.InvokeMethod(zip_stream);
                        zip_stream.Dispose();
                        Stream ms = ReadMemoryStream(stream_result);
                        stream_result.Dispose();
                        Stream unzip_stream = Yqun.Common.Encoder.Compression.DeCompressStream(ms);
                        ms.Dispose();
                        Hashtable Result = Yqun.Common.Encoder.Serialize.DeSerializeFromStream(unzip_stream) as Hashtable;

                        obj = Result["return_value"];
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("call remote server method  [" + MethodName + "] error: " + ex.ToString());
            }
            return obj;
        }

        private MemoryStream ReadMemoryStream(Stream Params)
        {
            MemoryStream serviceStream = new MemoryStream();
            byte[] buffer = new byte[10000];
            int bytesRead = 0;
            int byteCount = 0;

            do
            {
                bytesRead = Params.Read(buffer, 0, buffer.Length);
                serviceStream.Write(buffer, 0, bytesRead);

                byteCount = byteCount + bytesRead;
            } while (bytesRead > 0);

            serviceStream.Position = 0;

            return serviceStream;
        }

    }
}
