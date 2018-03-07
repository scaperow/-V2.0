using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;
using System.IO;
using System.ServiceModel;
using TransferServiceCommon;
using System.Collections;

namespace Yqun.BO.BusinessManager
{
    public class UploadHelper : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UploadSetting GetUploadSettingByModuleID(Guid moduleID)
        {
            String sql = "SELECT UploadSetting FROM dbo.sys_module WHERE ID='" + moduleID + "'";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count == 1)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<UploadSetting>(dt.Rows[0][0].ToString());
            }
            return null;
        }

        public Boolean UpdateUploadSetting(Guid moduleID, String json)
        {
            String sql = String.Format("UPDATE dbo.sys_module SET UploadSetting='{0}' WHERE ID='{1}'", json.Replace("'", "''"), moduleID);
            return ExcuteCommand(sql) == 1;
        }

        public String GetDefaultUploadSetting(String moduleID)
        {
            String result = "";
            UploadSetting us = new UploadSetting();
            us.Items = new List<UploadSettingItem>();
            if (moduleID.ToUpper() == "E77624E9-5654-4185-9A29-8229AAFDD68B")
            {
                //testroom
                us.Name = "TestInfo";

                UploadSettingItem item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "TestRoomCode",
                    Description = "试验室编号",
                    Name = "F_TRCODE",
                    NeedSetting = false
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "检测区段",
                    Name = "F_JCQD",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "通讯地址",
                    Name = "F_ADDRESS",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "邮编",
                    Name = "F_POSTCODE",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "传真",
                    Name = "F_FAX",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "联系电话",
                    Name = "F_TEL",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "母体试验室名称",
                    Name = "F_MOTHROOM",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "母体试验室认证编号",
                    Name = "F_MOTHAUTH",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "母体试验室认证有效期’yyyy-mm-dd’",
                    Name = "F_MOTHVALIDDATE",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "本试验室检测项目",
                    Name = "F_TESTRANGE",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "委外机构名称",
                    Name = "F_WWORG",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "委外机构地址",
                    Name = "F_WWADDRESS",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "委外资质认证编号",
                    Name = "F_WWAUTH",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "委外机构认证有效期",
                    Name = "F_VALIDDATE",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "委外机构检测项目",
                    Name = "F_WWRANGE",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "描述信息",
                    Name = "F_DESC",
                    NeedSetting = true
                };
                us.Items.Add(item);
                result = Newtonsoft.Json.JsonConvert.SerializeObject(us);
            }
            else if (moduleID.ToUpper() == "08899BA2-CC88-403E-9182-3EF73F5FB0CE")
            {
                //person
                us.Name = "PERSON";
                UploadSettingItem item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "人员姓名",
                    Name = "F_TRPNAME",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "人员年龄",
                    Name = "F_TRPAGE",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "性别“1”男，“0”女",
                    Name = "F_TRPSEX",
                    NeedSetting = true
                };
                us.Items.Add(item);
                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "专业",
                    Name = "F_TRPSPECIAL",
                    NeedSetting = true
                };
                us.Items.Add(item);
                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "毕业学校",
                    Name = "F_TRPSCOOL",
                    NeedSetting = true
                };
                us.Items.Add(item);
                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "学历",
                    Name = "F_TRPXL",
                    NeedSetting = true
                };
                us.Items.Add(item);
                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "联系电话",
                    Name = "F_TRPTEL",
                    NeedSetting = true
                };
                us.Items.Add(item);
                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "工作时间",
                    Name = "F_WORKDATE",
                    NeedSetting = true
                };
                us.Items.Add(item);
                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "技术职称“0”助理工程师，“1”工程师，“2”高级工程师，“3”教授级高工，“4”其它",
                    Name = "F_JSZC",
                    NeedSetting = true
                };
                us.Items.Add(item);
                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "岗位/职务“0”试验室主任，“1”技术负责人，“2”试验员，“3”其它",
                    Name = "F_ZW",
                    NeedSetting = true
                };
                us.Items.Add(item);
                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "从事本工种年限",
                    Name = "F_POSTIME",
                    NeedSetting = true
                };
                us.Items.Add(item);
                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "教育经历",
                    Name = "F_JYJL",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "工作经历",
                    Name = "F_GZJL",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "试验室编码",
                    Name = "F_TRCODE",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "身份证号",
                    Name = "F_IDENTITYCODE",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "照片的文件名，没有为空",
                    Name = "F_PHOTO",
                    NeedSetting = true
                };
                us.Items.Add(item);
                result = Newtonsoft.Json.JsonConvert.SerializeObject(us);
            }
            else
            {
                //test document
                us.Name = "REP";
                UploadSettingItem item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "试验室编码[按照统一编码]",
                    Name = "F_TRCODE",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "报告类型[按照统一编码]",
                    Name = "F_RTCODE",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "委托编号",
                    Name = "F_WTBH",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "试验报告编号",
                    Name = "F_BGBH",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "试验报告名称",
                    Name = "F_BGMC",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "表号",
                    Name = "F_BH",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "批准文号",
                    Name = "F_PZWH",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "报告日期[格式为yyyy-mm-dd]",
                    Name = "F_BGRQ",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "样品编号",
                    Name = "F_YPBH",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "记录编号",
                    Name = "F_JLBH",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "生产批号[如果报告表头无此项为空]",
                    Name = "F_SCPH",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "委托单位",
                    Name = "F_WTDW",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "工程名称",
                    Name = "F_GCMC",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "施工部位",
                    Name = "F_SGBW",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "试验对象等级[用于区分试验对象的等级和规格品类。例如：混凝土：强度等级；金属材料：规格型号；砂石：规格种类]",
                    Name = "F_DXDJ",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "代表数量",
                    Name = "F_DBSL",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "代表数量对应的计量单位",
                    Name = "F_DW",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "样品产地[没有的为空]",
                    Name = "F_YPCD",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "是否合格[1合格，0不合格]",
                    Name = "F_SFHG",
                    NeedSetting = false
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "报告类型[0自检，1见证，2监理平检，3其它]",
                    Name = "F_BGZT",
                    NeedSetting = false
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "试验结论",
                    Name = "F_SYJL",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "不合格项目[“名称_标准规定值_项目实测值^名称_标准规定值_项目实测值”， 名称、标准规定值和项目实测值间以英文”_”分隔多项不合格，信息以“^”分隔，无不合格信息的此项为空]",
                    Name = "F_BHGX",
                    NeedSetting = false
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "涉及的试验仪器 ‘0’默认：不涉及,’1’：涉及压力机‘2’：涉及万能机",
                    Name = "F_DEVTYPE",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "厂商名称",
                    Name = "F_SOFTCOM",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "试验人",
                    Name = "F_SYR",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "复核人",
                    Name = "F_FHR",
                    NeedSetting = true
                };
                us.Items.Add(item);

                item = new UploadSettingItem()
                {
                    SheetID = Guid.Empty,
                    CellName = "",
                    Description = "数字版本号，新上传默认版本号为1，再次上传版本号为上版本号+1，版本号由上传端控制，接收端不做合理性判断",
                    Name = "F_NEWVERSION",
                    NeedSetting = false
                };
                us.Items.Add(item);
                result = Newtonsoft.Json.JsonConvert.SerializeObject(us);
            }
            result = result.Replace("'", "''");
            return result;
        }

        #region 工管中心采集上传
        public GGCUploadSetting GetGGCUploadSettingByModuleID(Guid moduleID)
        {
            String sql = "SELECT GGCUploadSetting FROM dbo.sys_module WHERE ID='" + moduleID + "'";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count == 1)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<GGCUploadSetting>(dt.Rows[0][0].ToString());
            }
            return null;
        }

        public Boolean UpdateGGCUploadSetting(Guid moduleID, String json)
        {
            String sql = String.Format("UPDATE dbo.sys_module SET GGCUploadSetting='{0}' WHERE ID='{1}'", json.Replace("'", "''"), moduleID);
            return ExcuteCommand(sql) == 1;
        }
        #endregion
        #region 工管中心文档上传
        public GGCUploadSetting GetGGCDocUploadSettingByModuleID(Guid moduleID)
        {
            String sql = "SELECT GGCDocUploadSetting FROM dbo.sys_module WHERE ID='" + moduleID + "'";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count == 1)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<GGCUploadSetting>(dt.Rows[0][0].ToString());
            }
            return null;
        }

        public Boolean UpdateGGCDocUploadSetting(Guid moduleID, String json)
        {
            String sql = String.Format("UPDATE dbo.sys_module SET GGCDocUploadSetting='{0}' WHERE ID='{1}'", json.Replace("'", "''"), moduleID);
            return ExcuteCommand(sql) == 1;
        }
        #endregion
        public string GetGGCLabCodeName(string Code)
        {
            String moduleLibAddress = "net.tcp://Lib.kingrocket.com:8066/TransferService.svc";
            string strSQL = string.Format("SELECT  CodeName FROM dbo.sys_ggc_LabTypeCode WHERE Code='{0}'",Code);
            DataTable dt = CallRemoteServerMethod(moduleLibAddress, "Yqun.BO.BusinessManager.dll", "GetDataTable",
                            new Object[] { strSQL }) as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        private object CallRemoteServerMethod(String address, string AssemblyName, string MethodName, object[] Parameters)
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
                logger.Error("call remote server method error: " + ex.Message);
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
