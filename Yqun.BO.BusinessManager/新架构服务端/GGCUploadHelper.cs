using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using BizCommon;
using System.Threading;
using System.Xml;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;
using System.Collections;
using FarPoint.Win.Spread;
using FarPoint.Win;
using System.IO.Compression;

namespace Yqun.BO.BusinessManager
{
    public class GGCUploadHelper : BOBase
    {
        //[DllImport("DESDll.dll")]
        //public static extern string Encrypt(string Text, string key, string iv);

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        UploadHelper uh = new UploadHelper();
        DocumentHelper dh = new DocumentHelper();
        #region 上传文档
        public void UploadLabDocBasic(JZDocument doc, Sys_Module ModuleBase, string UserName, string TestRoomCode,string MachineCode)
        {
            //if (ModuleBase.ID == new Guid("68F05EBC-5D34-49C5-9B57-49B688DF24F7"))
            //{//钢筋试验报告
            #region 上传报告文档
            //string strSet = string.Empty;
            #region strSet
            //strSet = "{\"Name\":\"REP\",\"Items\":[{\"Name\":\"F_GUID\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"\",\"Description\":\"唯一ID\",\"NeedSetting\":false},{\"Name\":\"F_SBCODE\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"C16\",\"Description\":\"设备编码（由铁科院提供）\",\"NeedSetting\":false},{\"Name\":\"SYTYPE\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"K5\",\"Description\":\"试验类型编码\",\"NeedSetting\":true},{\"Name\":\"WTBH\",\"SheetID\":\"61460056-693C-4D71-B82B-DD1BE919563B\",\"CellName\":\"H5\",\"Description\":\"委托编号\",\"NeedSetting\":true},{\"Name\":\"JLBH\",\"SheetID\":\"270A1DA6-2045-405A-AE77-18C0C98C1EDD\",\"CellName\":\"L5\",\"Description\":\"记录编号\",\"NeedSetting\":false},{\"Name\":\"BGBH\",\"SheetID\":\"A982544A-A2DA-4224-A4F3-FC7D5DA0A114\",\"CellName\":\"K5\",\"Description\":\"报告编号\",\"NeedSetting\":true},{\"Name\":\"WTDW\",\"SheetID\":\"61460056-693C-4D71-B82B-DD1BE919563B\",\"CellName\":\"I11\",\"Description\":\"委托单位\",\"NeedSetting\":true},{\"Name\":\"PRJNAME\",\"SheetID\":\"61460056-693C-4D71-B82B-DD1BE919563B\",\"CellName\":\"D24\",\"Description\":\"工程名称\",\"NeedSetting\":false},{\"Name\":\"GGTYPE\",\"SheetID\":\"61460056-693C-4D71-B82B-DD1BE919563B\",\"CellName\":\"D29\",\"Description\":\"规格种类\",\"NeedSetting\":true},{\"Name\":\"DBQUANTITY\",\"SheetID\":\"61460056-693C-4D71-B82B-DD1BE919563B\",\"CellName\":\"D27\",\"Description\":\"代表数量\",\"NeedSetting\":false},{\"Name\":\"DBUNIT\",\"SheetID\":\"61460056-693C-4D71-B82B-DD1BE919563B\",\"CellName\":\"D31\",\"Description\":\"代表数量单位\",\"NeedSetting\":false},{\"Name\":\"SYDATE\",\"SheetID\":\"A982544A-A2DA-4224-A4F3-FC7D5DA0A114\",\"CellName\":\"K9\",\"Description\":\"报告日期\",\"NeedSetting\":false},{\"Name\":\"SYRERSON\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"I6\",\"Description\":\"创建人\",\"NeedSetting\":false},{\"Name\":\"ISSUCCESS\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"I7\",\"Description\":\"是否合格\",\"NeedSetting\":false},{\"Name\":\"WDMC\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"I7\",\"Description\":\"文档名称\",\"NeedSetting\":false},{\"Name\":\"SYJLX\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"\",\"Description\":\"试验机类型\",\"NeedSetting\":false},{\"Name\":\"NOSUCCESSINFO\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"\",\"Description\":\"不合格原因\",\"NeedSetting\":false}]}";
            #endregion

            GGCUploadSetting set = uh.GetGGCDocUploadSettingByModuleID(ModuleBase.ID);// Newtonsoft.Json.JsonConvert.DeserializeObject<GGCUploadSetting>(strSet);
            LabDocBasicInfo mdlLDBI = GetLabDocBasicInfo(doc, ModuleBase, UserName, set, TestRoomCode,MachineCode);
            logger.Info("UploadCaiJiData Execute1 YHX-eccode：" + mdlLDBI.SBCODE);
            string strData = Newtonsoft.Json.JsonConvert.SerializeObject(mdlLDBI);
            string errMsg = string.Empty;

            //string strSQL = "SELECT * FROM dbo.sys_ggc_UserAuth WHERE YHMC=@0";
            //DataSet dsYHMC = GetDataSet(strSQL, new object[] { UserName });
            //if (dsYHMC != null && dsYHMC.Tables.Count > 0 && dsYHMC.Tables[0].Rows.Count > 0)
            //{
            //    string IDCARDNUM = dsYHMC.Tables[0].Rows[0]["IDCARDNUM"].ToString();
            //    string LABKEY = dsYHMC.Tables[0].Rows[0]["LABKEY"].ToString();
            //    string LABIV = dsYHMC.Tables[0].Rows[0]["LABIV"].ToString();

            string IDCARDNUM = "316835199905186688";
            string LABKEY = "5JJQIjh0Zv8=";
            string LABIV = "PEK+gn96Rd0=";
            strData = Encrypt(strData, LABKEY, LABIV);
            int result = UploadLabDocBasicInfoDES(IDCARDNUM, strData, out errMsg);
            if (result == 0)
            {
                dh.UpdateGGCDocumentNeedUpload(doc.ID.ToString(), -1);
                logger.Info("UploadLabDocBasicInfoDES errMsg:" + errMsg + " LABKEY:" + LABKEY + " LABIV:" + LABIV + " strData:" + Decrypt(strData, LABKEY, LABIV));
            }
            else if (!string.IsNullOrEmpty(errMsg))
            {
                dh.UpdateGGCDocumentNeedUpload(doc.ID.ToString(), -1);
                logger.Info("UploadLabDocBasicInfoDES Success!errMsg:" + errMsg + " LABKEY:" + LABKEY + " LABIV:" + LABIV + " strData:" + Decrypt(strData, LABKEY, LABIV));
                //logger.Info("UploadLabDocBasicInfoDES Success!errMsg:" + errMsg + " strData:" + strData);
            }
            else
            {
                logger.Info("UploadLabDocBasicInfoDES Success!");
                string excelFile = string.Empty;
                #region 生成Excel
                //JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dt.Rows[i]["Data"].ToString());
                FpSpread fpSpread = new FpSpread();
                ModuleHelper mh = new ModuleHelper();
                String sheetXML = JZCommonHelper.GZipDecompressString(mh.GetSheetXMLByID(doc.Sheets[ModuleBase.ReportIndex].ID));// mh.GetSheetXMLByID(sheet.ID);
                SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), sheetXML, "SheetView") as SheetView;
                SheetView.Tag = doc.Sheets[ModuleBase.ReportIndex].ID;
                SheetView.Cells[0, 0].Value = "";
                SheetView.Protect = true;
                foreach (JZCell dataCell in doc.Sheets[ModuleBase.ReportIndex].Cells)
                {
                    Cell cell = SheetView.Cells[dataCell.Name];

                    if (cell != null)
                    {
                        cell.Value = dataCell.Value;
                    }
                }
                fpSpread.Sheets.Add(SheetView);
                string docDateDir = "GGCUploadData";
                excelFile = CreateRalationFilesSync(fpSpread, doc.ID, docDateDir);
                fpSpread.Dispose();
                doc = null;
                #endregion
                if (!string.IsNullOrEmpty(excelFile))
                {
                    strData = GetLabDocContent(mdlLDBI, excelFile);
                    strData = Encrypt(strData, LABKEY, LABIV);
                    result = UploadLabDocContentDES(IDCARDNUM, strData, out errMsg);
                    if (result == 0)
                    {
                        dh.UpdateGGCDocumentNeedUpload(doc.ID.ToString(), -1);
                        logger.Info("UploadLabDocContentDES errMsg:" + errMsg + "; strData：" + Decrypt(strData, LABKEY, LABIV));
                    }
                    else if (!string.IsNullOrEmpty(errMsg))
                    {
                        dh.UpdateGGCDocumentNeedUpload(doc.ID.ToString(), -1);
                        logger.Info("UploadLabDocContentDES Success!errMsg:" + errMsg + "; strData：" + Decrypt(strData, LABKEY, LABIV));
                    }
                    else
                    {
                        logger.Info("UploadLabDocContentDES Success!");
                    }
                }
                else
                {
                    logger.Info("create excel file failure:" + doc.ID);
                }
            }
            //}
            #endregion
            //}
        }
        public string GetLabDocContent(LabDocBasicInfo mdlDocBasic, string excelFile)
        {
            //LabDocContent mdlDocContent = new LabDocContent();
            #region 生成XML文件
            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><product></product>");//<product></product>

            //XmlElement eleDesc = xmldoc.CreateElement("description");
            //eleDesc.InnerText = "文档上传";
            //xmldoc.DocumentElement.AppendChild(eleDesc);

            //XmlElement eleFileList = xmldoc.CreateElement("filelist");
            //XmlAttribute attr = xmldoc.CreateAttribute("count");
            //attr.Value = "1";
            //eleFileList.Attributes.Append(attr);
            //attr = xmldoc.CreateAttribute("sourcepath");
            //attr.Value = "./Update/";
            //eleFileList.Attributes.Append(attr);
            //xmldoc.DocumentElement.AppendChild(eleFileList);

            //XmlElement eleItem = xmldoc.CreateElement("item");
            //attr = xmldoc.CreateAttribute("name");
            //attr.Value = mdlDocBasic.WDMC;
            //eleItem.Attributes.Append(attr);

            //attr = xmldoc.CreateAttribute("size");
            //attr.Value = mdlDocBasic.WDMC.Length.ToString();
            //eleItem.Attributes.Append(attr);
            //eleFileList.AppendChild(eleItem);

            //XmlElement eleValue = xmldoc.CreateElement("value");
            //eleValue.InnerText = Newtonsoft.Json.JsonConvert.SerializeObject(mdlDocBasic);
            //eleItem.AppendChild(eleValue);
            #endregion
            #region 生成XML字符串
            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><product></product>");//<product></product>
            FileStream stream = null;
            stream = new FileInfo(excelFile).OpenRead();
            Byte[] buffer = new Byte[stream.Length];
            stream.Read(buffer, 0, Convert.ToInt32(stream.Length));

            string ss = System.Text.Encoding.UTF8.GetString(buffer);

            string strDocBase = Newtonsoft.Json.JsonConvert.SerializeObject(mdlDocBasic);
            StringBuilder strXml = new StringBuilder();
            strXml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            strXml.Append("<product>");
            strXml.Append("<description>文档上传</description>");
            strXml.Append("<filelist count=\"1\" sourcepath=\"./Update/\">");
            strXml.AppendFormat("<item name=\"{0}\" size=\"{1}\">", mdlDocBasic.WDMC, strDocBase.Length);
            strXml.Append("<value>");
            strXml.Append(Convert.ToBase64String(Encoding.UTF8.GetBytes(ss)));//Convert.ToBase64String(Encoding.UTF8.GetBytes(strDocBase))
            strXml.Append("</value>");
            strXml.Append("</item>");
            strXml.Append("</filelist>");
            strXml.Append("</product>");
            //XmlElement eleDesc = xmldoc.CreateElement("description");
            //eleDesc.InnerText = "文档上传";
            //xmldoc.DocumentElement.AppendChild(eleDesc);

            //XmlElement eleFileList = xmldoc.CreateElement("filelist");
            //XmlAttribute attr = xmldoc.CreateAttribute("count");
            //attr.Value = "1";
            //eleFileList.Attributes.Append(attr);
            //attr = xmldoc.CreateAttribute("sourcepath");
            //attr.Value = "./Update/";
            //eleFileList.Attributes.Append(attr);
            //xmldoc.DocumentElement.AppendChild(eleFileList);

            //XmlElement eleItem = xmldoc.CreateElement("item");
            //attr = xmldoc.CreateAttribute("name");
            //attr.Value = mdlDocBasic.WDMC;
            //eleItem.Attributes.Append(attr);

            //attr = xmldoc.CreateAttribute("size");
            //attr.Value = mdlDocBasic.WDMC.Length.ToString();
            //eleItem.Attributes.Append(attr);
            //eleFileList.AppendChild(eleItem);

            //XmlElement eleValue = xmldoc.CreateElement("value");
            //eleValue.InnerText = Newtonsoft.Json.JsonConvert.SerializeObject(mdlDocBasic);
            //eleItem.AppendChild(eleValue);
            #endregion
            return strXml.ToString();
            //mdlDocContent.DocContent = strXml.ToString();
            //return mdlDocContent;
        }

        /// <summary>
        /// 获取工管中心设别编码
        /// </summary>
        /// <param name="ECode"></param>
        /// <returns></returns>
        public string GetECode(string ECode)
        {
            DataTable _dt = GetDataTable("select RemoteCode1 from dbo.sys_devices where  MachineCode='" + ECode + "'");
            if (_dt != null && _dt.Rows.Count > 0)
            {
                return _dt.Rows[0][0].ToString();
            }
            return ECode;
        }

        /// <summary>
        /// 生成文档基本接口上传模型
        /// </summary>
        public LabDocBasicInfo GetLabDocBasicInfo(JZDocument doc, Sys_Module ModuleBase, string UserName, GGCUploadSetting set, string TestRoomCode,string MacineCode)
        {
            LabDocBasicInfo mdlLDBI = new LabDocBasicInfo();
            #region 不合格判断

            string sql = String.Format("select ID,F_InvalidItem from sys_invalid_document where ID='{0}' and AdditionalQualified=0 ", doc.ID);
            DataTable dtInvalid = GetDataTable(sql);
            string ISSUCCESS = string.Empty;
            string NOSUCCESSINFO = string.Empty;
            if (dtInvalid != null && dtInvalid.Rows.Count > 0)
            {//不合格   第一根屈服点,≥335,18||第一根抗拉强度,≥455,9||第一根断后伸长率,≥17,-95.0||
                //:(第一组)的实测值为8.5，标准值为>=15；报告编号为12345
                ISSUCCESS = "0";
                string strInvalidItem = dtInvalid.Rows[0]["F_InvalidItem"].ToString().Trim('|');
                string[] arrII = strInvalidItem.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arrII.Length; i++)
                {
                    string ss = arrII[i];
                    string[] arrSS = ss.Split(',');
                    if (arrSS.Length == 3)
                    {
                        NOSUCCESSINFO += arrSS[0] + ",实测值为" + arrSS[2] + ",标准值为" + arrSS[1] + ";";
                    }
                }
            }
            else
            {
                ISSUCCESS = "1";
                NOSUCCESSINFO = "";
            }
            #endregion
            string GGCLabCodeName = uh.GetGGCLabCodeName(ModuleBase.ModuleALTGG);
            #region 设备编码
            string sbbm = "";
            string GGCTestRoomCodeMap = System.Configuration.ConfigurationManager.AppSettings["GGCTestRoomCodeMap"];
            Hashtable testRoomCodeMap = new Hashtable();
            testRoomCodeMap = InitTestRoomCodeMap(GGCTestRoomCodeMap);
            sbbm = testRoomCodeMap[TestRoomCode].ToString();
            #endregion
            if (set != null && set.Items != null)
            {
                foreach (var item in set.Items)
                {
                    switch (item.Name.ToUpper())
                    {
                        #region
                        case "F_GUID"://唯一id（流水号）
                            mdlLDBI.F_GUID = doc.ID.ToString();
                            break;
                        case "SBCODE":
                            #region 设备编码（由铁科院提供）
                            mdlLDBI.SBCODE = GetECode(MacineCode);
                            break;
                            #endregion
                        case "SYTYPE":
                            #region 试验类型编码
                            mdlLDBI.SYTYPE = ModuleBase.ModuleALTGG;
                            break;
                            #endregion
                        case "WTBH":
                            #region 委托编号
                            Object objWTBH = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                            if (objWTBH != null)
                            {
                                mdlLDBI.WTBH = objWTBH.ToString().Replace("\r\n", "");
                            }
                            else
                            {
                                mdlLDBI.WTBH = "";
                            }
                            break;
                            #endregion
                        case "JLBH":
                            #region 记录编号
                            Object objJLBH = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                            if (objJLBH != null)
                            {
                                mdlLDBI.JLBH = objJLBH.ToString().Replace("\r\n", "");
                            }
                            else
                            {
                                mdlLDBI.JLBH = "";
                            }
                            break;
                            #endregion
                        case "BGBH":
                            #region 报告编号
                            Object objBGBH = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                            if (objBGBH != null)
                            {
                                mdlLDBI.BGBH = objBGBH.ToString().Replace("\r\n", "");
                            }
                            else
                            {
                                mdlLDBI.BGBH = "";
                            }
                            break;
                            #endregion
                        case "WTDW":
                            #region 委托单位
                            Object objWTDW = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                            if (objWTDW != null)
                            {
                                mdlLDBI.WTDW = objWTDW.ToString().Replace("\r\n", "");
                            }
                            else
                            {
                                mdlLDBI.WTDW = "";
                            }
                            break;
                            #endregion
                        case "PRJNAME":
                            #region 工程名称

                            Object objPRJNAME = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                            if (objPRJNAME != null)
                            {
                                mdlLDBI.PRJNAME = objPRJNAME.ToString().Replace("\r\n", "");
                            }
                            else
                            {
                                mdlLDBI.PRJNAME = "";
                            }
                            break;
                            #endregion
                        case "GGTYPE":
                            #region 规格种类
                            Object objGGTYPE = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                            if (objGGTYPE != null)
                            {
                                mdlLDBI.GGTYPE = objGGTYPE.ToString().Replace("\r\n", "");
                            }
                            else
                            {
                                mdlLDBI.GGTYPE = "";
                            }
                            break;
                            #endregion
                        case "DBQUANTITY":
                            #region 代表数量
                            Object objDBQUANTITY = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                            if (objDBQUANTITY != null)
                            {
                                mdlLDBI.DBQUANTITY = objDBQUANTITY.ToString().Replace("\r\n", "");
                            }
                            else
                            {
                                mdlLDBI.DBQUANTITY = "";
                            }
                            break;
                            #endregion
                        case "DBUNIT":
                            #region 代表数量单位
                            Object objDBUNIT = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                            if (objDBUNIT != null)
                            {
                                mdlLDBI.DBUNIT = objDBUNIT.ToString().Replace("\r\n", "");
                            }
                            else
                            {
                                mdlLDBI.DBUNIT = "";
                            }
                            break;
                            #endregion
                        case "SYDATE":
                            #region 报告日期
                            Object objSYDATE = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                            if (objSYDATE != null)
                            {
                                mdlLDBI.SYDATE = objSYDATE.ToString().Replace("\r\n", "");
                            }
                            else
                            {
                                mdlLDBI.SYDATE = "";
                            }
                            break;
                            #endregion
                        case "SYRERSON":
                            #region 创建人
                            mdlLDBI.SYRERSON = UserName;
                            break;
                            #endregion
                        case "ISSUCCESS"://是否合格    
                            #region 是否合格
                            mdlLDBI.ISSUCCESS = ISSUCCESS;
                            break;
                            #endregion
                        case "WDMC"://文档名称  钢筋试验{3MX1D1BD01ZX01YL010253_20131123161500}.xls 
                            //试验类型名称+{设备编码+试验类型编码+’_’+当前时间戳精确到秒}+文件后缀名
                            mdlLDBI.WDMC = GGCLabCodeName + "{" + sbbm + ModuleBase.ModuleALTGG + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "}.xls";
                            break;
                        case "SYJLX"://试验机类型
                            if (ModuleBase.DeviceType == 1)
                            {
                                mdlLDBI.SYJLX = "YL";
                            }
                            else if (ModuleBase.DeviceType == 2)
                            {
                                mdlLDBI.SYJLX = "WN";
                            }
                            break;
                        case "NOSUCCESSINFO"://不合格原因
                            if (NOSUCCESSINFO.Length > 0)
                            {
                                mdlLDBI.NOSUCCESSINFO = NOSUCCESSINFO + "报告编号为" + mdlLDBI.BGBH;
                            }
                            break;
                        case "F_VENDER"://软件厂家名称
                            mdlLDBI.F_VENDER = item.CellName;
                            break;
                        default:
                            break;
                        #endregion
                    }
                }
            }
            return mdlLDBI;
        }
        #endregion
        #region 上传采集数据
        public void UploadCaiJiData(JZDocument doc, Sys_Module ModuleBase, Guid TestDataID, string MachineCode, int SeriaNumber, string TestData, string UserName, string RealTimeData, string TestRoomCode)
        {
            string strRealTimeData = string.Empty;
            try
            {
                strRealTimeData = BizCommon.JZCommonHelper.GZipDecompressString(RealTimeData);
            }
            catch
            {
                strRealTimeData = RealTimeData;
            }
            ThreadParameter p = new ThreadParameter();
            p.Doc = doc;
            p.ModuleBase = ModuleBase;
            p.TestDataID = TestDataID;
            p.MachineCode = MachineCode;
            p.SeriaNumber = SeriaNumber;
            p.TestData = TestData;
            p.UserName = UserName;
            p.RealTimeData = RealTimeData;
            p.TestRoomCode = TestRoomCode;
            ThreadPool.QueueUserWorkItem(new WaitCallback(Execute), p);
        }

        private void Execute(object paremeter)
        {
            ThreadParameter p = paremeter as ThreadParameter;
            try
            {
                string TestRoomCode = p.TestRoomCode;
                if (p.ModuleBase.DeviceType == 1)
                {
                    //if (p.ModuleBase.ID == new Guid("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B"))
                    //{//混凝土检查试件抗压强度试验报告
                    #region 上传压力机数据
                    //string strSet = string.Empty;
                    #region strSet
                    ////strSet = "{\"Name\":\"REP\",\"Items\":[{\"Name\":\"F_GUID\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"\",\"Description\":\"唯一id（流水号）\",\"NeedSetting\":false},{\"Name\":\"F_SBCODE\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"C16\",\"Description\":\"设备编码（由铁科院提供）\",\"NeedSetting\":false},{\"Name\":\"F_RTCODE\",\"SheetID\":\"15BB2F31-F62A-479A-9346-7FFE4D32114E\",\"CellName\":\"R5\",\"Description\":\"试验报告编号\",\"NeedSetting\":true},{\"Name\":\"F_WTBH\",\"SheetID\":\"AEEC19FA-A539-4A52-8900-690BB51E757B\",\"CellName\":\"H5\",\"Description\":\"委托编号\",\"NeedSetting\":true},{\"Name\":\"F_SJBH\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"\",\"Description\":\"试件序号\",\"NeedSetting\":false},{\"Name\":\"F_QDDJ\",\"SheetID\":\"AEEC19FA-A539-4A52-8900-690BB51E757B\",\"CellName\":\"C9\",\"Description\":\"混凝土强度等级\",\"NeedSetting\":true},{\"Name\":\"F_SJCC\",\"SheetID\":\"AEEC19FA-A539-4A52-8900-690BB51E757B\",\"CellName\":\"C11\",\"Description\":\"试件尺寸\",\"NeedSetting\":true},{\"Name\":\"F_ISWJJ\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"D6\",\"Description\":\"是否掺外加剂混凝土\",\"NeedSetting\":false},{\"Name\":\"F_LQ\",\"SheetID\":\"303C05A6-4FF4-4F15-8118-69968988EE3B\",\"CellName\":\"E6\",\"Description\":\"龄期(d)\",\"NeedSetting\":true},{\"Name\":\"F_KYLZ\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"C21\",\"Description\":\"最大抗压力值(KN)\",\"NeedSetting\":false},{\"Name\":\"F_SYSJ\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"E6\",\"Description\":\"采集时间\",\"NeedSetting\":false},{\"Name\":\"F_OPERATOR\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"I6\",\"Description\":\"操作员\",\"NeedSetting\":false},{\"Name\":\"F_YSKYLZ\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"I7\",\"Description\":\"原始抗压力值过程数据（逗号分割）\",\"NeedSetting\":false},{\"Name\":\"F_SOFTCOM\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"北京金舟神创科技发展有限公司\",\"Description\":\"厂家名称\",\"NeedSetting\":false},{\"Name\":\"F_COLCOM\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"\",\"Description\":\"控制器厂家名称\",\"NeedSetting\":false}]}";
                    //strSet = uh.GetGGCUploadSettingByModuleID(p.ModuleBase.ID.ToString());
                    #endregion

                    GGCUploadSetting set = uh.GetGGCUploadSettingByModuleID(p.ModuleBase.ID);// Newtonsoft.Json.JsonConvert.DeserializeObject<GGCUploadSetting>(strSet);
                    PressureData mdlPD = GetPressureData(p.Doc, p.ModuleBase, p.TestDataID, p.MachineCode, p.SeriaNumber, p.TestData, p.UserName, p.RealTimeData, set, TestRoomCode);
                    logger.Info("UploadCaiJiData Execute1 YHX-eccode：" + mdlPD.F_SBCODE);
                    string strData = Newtonsoft.Json.JsonConvert.SerializeObject(mdlPD);
                    string errMsg = string.Empty;
                    //string strSQL = "SELECT * FROM dbo.sys_ggc_UserAuth WHERE YHMC=@0";
                    //DataSet dsYHMC = GetDataSet(strSQL, new object[] { p.UserName });
                    //if (dsYHMC != null && dsYHMC.Tables.Count > 0 && dsYHMC.Tables[0].Rows.Count > 0)
                    //{
                    //    string IDCARDNUM = dsYHMC.Tables[0].Rows[0]["IDCARDNUM"].ToString();
                    //    string LABKEY = dsYHMC.Tables[0].Rows[0]["LABKEY"].ToString();
                    //    string LABIV = dsYHMC.Tables[0].Rows[0]["LABIV"].ToString();
                    logger.Info("UploadCaiJiData Execute1 YHX; strData：" + strData);
                    string IDCARDNUM = "316835199905186688";
                    string LABKEY = "5JJQIjh0Zv8=";
                    string LABIV = "PEK+gn96Rd0=";

                    strData = Encrypt(strData, LABKEY, LABIV);
                    int result = UploadPressureDataDES(IDCARDNUM, strData, out errMsg);
                    if (result == 0)
                    {
                        dh.UpdateGGCDocumentNeedUpload(p.Doc.ID.ToString(), -1);
                        logger.Info("UploadCaiJiData Execute1 errMsg:" + errMsg + "; strData：" + Decrypt(strData, LABKEY, LABIV));
                    }
                    else if (!string.IsNullOrEmpty(errMsg))
                    {
                        dh.UpdateGGCDocumentNeedUpload(p.Doc.ID.ToString(), -1);
                        logger.Info("UploadCaiJiData Execute1 success!errMsg:" + errMsg + "; strData：" + Decrypt(strData, LABKEY, LABIV));
                    }
                    else
                    {
                        logger.Info("UploadCaiJiData Execute1 success!TestDataID:" + p.TestDataID);
                    }
                    //}
                    #endregion
                    //}
                }
                else if (p.ModuleBase.DeviceType == 2)
                {
                    //if (p.ModuleBase.ID == new Guid("68F05EBC-5D34-49C5-9B57-49B688DF24F7"))
                    //{//钢筋试验报告
                    #region 上传万能机数据
                    //string strSet = string.Empty;
                    #region strSet
                    //strSet = "{\"Name\":\"REP\",\"Items\":[{\"Name\":\"F_GUID\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"\",\"Description\":\"唯一id（流水号）\",\"NeedSetting\":false},{\"Name\":\"F_SBCODE\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"C16\",\"Description\":\"设备编码（由铁科院提供）\",\"NeedSetting\":false},{\"Name\":\"F_RTCODE\",\"SheetID\":\"A982544A-A2DA-4224-A4F3-FC7D5DA0A114\",\"CellName\":\"K5\",\"Description\":\"试验报告编号\",\"NeedSetting\":true},{\"Name\":\"F_WTBH\",\"SheetID\":\"61460056-693C-4D71-B82B-DD1BE919563B\",\"CellName\":\"H5\",\"Description\":\"委托编号\",\"NeedSetting\":true},{\"Name\":\"F_SJBH\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"\",\"Description\":\"试件序号\",\"NeedSetting\":false},{\"Name\":\"F_PZCODE\",\"SheetID\":\"61460056-693C-4D71-B82B-DD1BE919563B\",\"CellName\":\"C11\",\"Description\":\"牌号\",\"NeedSetting\":true},{\"Name\":\"F_GCZJ\",\"SheetID\":\"61460056-693C-4D71-B82B-DD1BE919563B\",\"CellName\":\"I11\",\"Description\":\"公称直径(mm)\",\"NeedSetting\":true},{\"Name\":\"F_AREA\",\"SheetID\":\"270A1DA6-2045-405A-AE77-18C0C98C1EDD\",\"CellName\":\"D24\",\"Description\":\"截面面积(mm2)\",\"NeedSetting\":false},{\"Name\":\"F_LZ\",\"SheetID\":\"270A1DA6-2045-405A-AE77-18C0C98C1EDD\",\"CellName\":\"D29\",\"Description\":\"最大抗拉力值(KN)\",\"NeedSetting\":true},{\"Name\":\"F_QFLZ\",\"SheetID\":\"270A1DA6-2045-405A-AE77-18C0C98C1EDD\",\"CellName\":\"D27\",\"Description\":\"屈服力值(KN)\",\"NeedSetting\":false},{\"Name\":\"F_SCL\",\"SheetID\":\"270A1DA6-2045-405A-AE77-18C0C98C1EDD\",\"CellName\":\"D31\",\"Description\":\"伸长率\",\"NeedSetting\":false},{\"Name\":\"F_SYSJ\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"E6\",\"Description\":\"采集时间\",\"NeedSetting\":false},{\"Name\":\"F_OPERATOR\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"I6\",\"Description\":\"操作员\",\"NeedSetting\":false},{\"Name\":\"F_YSKLLZ\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"I7\",\"Description\":\"抗拉力值过程数据\",\"NeedSetting\":false},{\"Name\":\"F_WY\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"I7\",\"Description\":\"位移值过程数据\",\"NeedSetting\":false},{\"Name\":\"F_SOFTCOM\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"北京金舟神创科技发展有限公司\",\"Description\":\"厂家名称\",\"NeedSetting\":false},{\"Name\":\"F_COLCOM\",\"SheetID\":\"00000000-0000-0000-0000-000000000000\",\"CellName\":\"\",\"Description\":\"控制器厂家名称\",\"NeedSetting\":false}]}";
                    #endregion

                    GGCUploadSetting set = uh.GetGGCUploadSettingByModuleID(p.ModuleBase.ID);// Newtonsoft.Json.JsonConvert.DeserializeObject<GGCUploadSetting>(strSet);
                    UniversalData mdlUD = GetUniversalData(p.Doc, p.ModuleBase, p.TestDataID, p.MachineCode, p.SeriaNumber, p.TestData, p.UserName, p.RealTimeData, set, TestRoomCode);
                    string strData = Newtonsoft.Json.JsonConvert.SerializeObject(mdlUD);
                    string errMsg = string.Empty;

                    //string strSQL = "SELECT * FROM dbo.sys_ggc_UserAuth WHERE YHMC=@0";
                    //DataSet dsYHMC = GetDataSet(strSQL, new object[] { p.UserName });
                    //if (dsYHMC != null && dsYHMC.Tables.Count > 0 && dsYHMC.Tables[0].Rows.Count > 0)
                    //{
                    //    string IDCARDNUM = dsYHMC.Tables[0].Rows[0]["IDCARDNUM"].ToString();
                    //    string LABKEY = dsYHMC.Tables[0].Rows[0]["LABKEY"].ToString();
                    //    string LABIV = dsYHMC.Tables[0].Rows[0]["LABIV"].ToString();

                    string IDCARDNUM = "316835199905186688";
                    string LABKEY = "5JJQIjh0Zv8=";
                    string LABIV = "PEK+gn96Rd0=";
                    strData = Encrypt(strData, LABKEY, LABIV);
                    int result = UploadUniversalDataDES(IDCARDNUM, strData, out errMsg);
                    if (result == 0)
                    {
                        dh.UpdateGGCDocumentNeedUpload(p.Doc.ID.ToString(), -1);
                        logger.Info("UploadCaiJiData Execute2 errMsg:" + errMsg + "; strData：" + Decrypt(strData, LABKEY, LABIV));
                    }
                    else if (!string.IsNullOrEmpty(errMsg))
                    {
                        dh.UpdateGGCDocumentNeedUpload(p.Doc.ID.ToString(), -1);
                        logger.Info("UploadCaiJiData Execute2 success!errMsg:" + errMsg + "; strData：" + Decrypt(strData, LABKEY, LABIV));
                    }
                    else
                    {
                        logger.Info("UploadCaiJiData Execute2 success!TestDataID:" + p.TestDataID);
                    }
                    //}
                    #endregion
                    //}
                }
            }
            catch (Exception ex)
            {
                logger.Info("UploadCaiJiData Execute error:TestDataID:" + p.TestDataID + " " + ex.ToString());
            }
        }

        /// <summary>
        /// 生成压力机上传模型
        /// </summary>
        public PressureData GetPressureData(JZDocument doc, Sys_Module ModuleBase, Guid TestDataID, string MachineCode, int SeriaNumber, string TestData, string UserName, string RealTimeData, GGCUploadSetting set, string TestRoomCode)
        {
            PressureData mdlPD = new PressureData();
            try
            {
                #region 设备编码
                //string sbbm = "";
                //string GGCTestRoomCodeMap = System.Configuration.ConfigurationManager.AppSettings["GGCTestRoomCodeMap"];
                //Hashtable testRoomCodeMap = new Hashtable();
                //testRoomCodeMap = InitTestRoomCodeMap(GGCTestRoomCodeMap);
                //if (ModuleBase.DeviceType == 1)
                //{
                //    sbbm = testRoomCodeMap[TestRoomCode].ToString() + "YL01";
                //}
                //else if (ModuleBase.DeviceType == 2)
                //{
                //    sbbm = testRoomCodeMap[TestRoomCode].ToString() + "WN01";
                //}
                #endregion
                //logger.Info("sbbm:" + sbbm);
                if (set != null && set.Items != null)
                {
                    foreach (var item in set.Items)
                    {
                        switch (item.Name.ToUpper())
                        {
                            #region
                            case "F_GUID"://唯一id（流水号）
                                mdlPD.F_GUID = TestDataID.ToString();
                                break;
                            case "F_SBCODE":
                                #region 设备编码（由铁科院提供）
                                mdlPD.F_SBCODE = GetECode(MachineCode);
                                break;
                                #endregion
                            case "F_RTCODE":
                                #region 试验报告编号
                                Object objF_RTCODE = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                if (objF_RTCODE != null)
                                {
                                    mdlPD.F_RTCODE = objF_RTCODE.ToString().Replace("\r\n", "");
                                }
                                else
                                {
                                    mdlPD.F_RTCODE = "";
                                }
                                break;
                                #endregion
                            case "F_WTBH":
                                #region 委托编号
                                Object objF_WTBH = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                if (objF_WTBH != null)
                                {
                                    mdlPD.F_WTBH = objF_WTBH.ToString().Replace("\r\n", "");
                                }
                                else
                                {
                                    mdlPD.F_WTBH = "";
                                }
                                break;
                                #endregion
                            case "F_SJBH":
                                #region 试件编号
                                mdlPD.F_SJBH = SeriaNumber.ToString();
                                break;
                                #endregion
                            case "F_QDDJ":
                                #region 强度等级
                                Object objF_QDDJ = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                if (objF_QDDJ != null)
                                {
                                    mdlPD.F_QDDJ = objF_QDDJ.ToString().Replace("\r\n", "");
                                }
                                else
                                {
                                    mdlPD.F_QDDJ = "";
                                }
                                break;
                                #endregion
                            case "F_SJCC":
                                #region 试件尺寸
                                Object objF_SJCC = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                if (objF_SJCC != null)
                                {
                                    mdlPD.F_SJCC = objF_SJCC.ToString().Replace("\r\n", "");
                                }
                                else
                                {
                                    mdlPD.F_SJCC = "";
                                }
                                break;
                                #endregion
                            case "F_ISWJJ":
                                #region 是否掺外加剂混凝土
                                if (ModuleBase.Name.IndexOf("外加剂") >= 0)
                                {
                                    mdlPD.F_ISWJJ = "1";
                                }
                                else
                                {
                                    mdlPD.F_ISWJJ = "0";
                                }
                                break;
                                #endregion
                            case "F_LQ":
                                #region 龄期
                                string LQCellName = item.CellName[0].ToString() + (int.Parse(item.CellName.Remove(0, 1)) + (SeriaNumber - 1) / 3 * 3) + "";
                                Object objF_LQ = JZCommonHelper.GetCellValue(doc, item.SheetID, LQCellName);
                                if (objF_LQ != null)
                                {
                                    try
                                    {
                                        mdlPD.F_LQ = int.Parse(objF_LQ.ToString());
                                    }
                                    catch
                                    {
                                        logger.Info("GetPressureData F_LQ转换失败:" + objF_LQ);
                                    }
                                }
                                else
                                {
                                    mdlPD.F_LQ = 0;
                                }
                                break;
                                #endregion
                            case "F_KYLZ":
                                #region 最大抗压力值(KN)
                                List<JZTestCell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZTestCell>>(TestData);
                                if (cells.Count > 0)
                                {
                                    try
                                    {
                                        mdlPD.F_KYLZ = float.Parse(cells[0].Value.ToString());
                                    }
                                    catch
                                    {
                                        logger.Info("GetPressureData F_KYLZ转换失败:" + cells[0].Value);
                                    }
                                }
                                else
                                {
                                    mdlPD.F_KYLZ = 0;
                                }
                                break;
                                #endregion

                            case "F_SYSJ":
                                #region 采集时间
                                mdlPD.F_SYSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                break;
                                #endregion
                            case "F_OPERATOR":
                                #region 操作员
                                mdlPD.F_OPERATOR = UserName;
                                break;
                                #endregion
                            case "F_YSKYLZ"://原始抗压力值过程数据（逗号分割）    
                                #region
                                List<JZRealTimeData> rtlCells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZRealTimeData>>(BizCommon.JZCommonHelper.GZipDecompressString(RealTimeData));
                                string F_YSKYLZ = string.Empty;
                                if (rtlCells.Count > 0)
                                {
                                    foreach (JZRealTimeData rtd in rtlCells)
                                    {
                                        F_YSKYLZ += rtd.Value + ",";
                                    }
                                }
                                else
                                {
                                    F_YSKYLZ = "";
                                }
                                if (F_YSKYLZ.Length > 0)
                                {
                                    F_YSKYLZ = F_YSKYLZ.Substring(0, F_YSKYLZ.Length - 1);
                                }
                                mdlPD.F_YSKYLZ = F_YSKYLZ;
                                #endregion
                                break;
                            case "F_SOFTCOM"://厂家名称
                                mdlPD.F_SOFTCOM = item.CellName;
                                break;
                            case "F_COLCOM"://控制器厂家名称
                                mdlPD.F_COLCOM = "";
                                break;
                            default:
                                break;
                            #endregion
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                mdlPD = null;
                logger.Info("GetPressureData error:" + TestDataID + " " + ex.ToString());
            }
            return mdlPD;
        }
        /// <summary>
        /// 生成万能机上传模型
        /// </summary>
        public UniversalData GetUniversalData(JZDocument doc, Sys_Module ModuleBase, Guid TestDataID, string MachineCode, int SeriaNumber, string TestData, string UserName, string RealTimeData, GGCUploadSetting set, string TestRoomCode)
        {
            UniversalData mdlUD = new UniversalData();
            try
            {
                #region 设备编码
                string sbbm = "";
                string GGCTestRoomCodeMap = System.Configuration.ConfigurationManager.AppSettings["GGCTestRoomCodeMap"];
                Hashtable testRoomCodeMap = new Hashtable();
                testRoomCodeMap = InitTestRoomCodeMap(GGCTestRoomCodeMap);
                if (ModuleBase.DeviceType == 1)
                {
                    sbbm = testRoomCodeMap[TestRoomCode].ToString() + "YL01";
                }
                else if (ModuleBase.DeviceType == 2)
                {
                    sbbm = testRoomCodeMap[TestRoomCode].ToString() + "WN01";
                }
                #endregion
                List<JZTestCell> lstJZTC = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZTestCell>>(TestData);
                if (set != null && set.Items != null)
                {
                    foreach (var item in set.Items)
                    {
                        switch (item.Name.ToUpper())
                        {
                            #region
                            case "F_GUID"://唯一id（流水号）
                                mdlUD.F_GUID = TestDataID.ToString();
                                break;
                            case "F_SBCODE":
                                #region 设备编码（由铁科院提供）
                                mdlUD.F_SBCODE = GetECode(MachineCode);
                                break;
                                #endregion
                            case "F_RTCODE":
                                #region 试验报告编号
                                Object objF_RTCODE = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                if (objF_RTCODE != null)
                                {
                                    mdlUD.F_RTCODE = objF_RTCODE.ToString().Replace("\r\n", "");
                                }
                                else
                                {
                                    mdlUD.F_RTCODE = "";
                                }
                                break;
                                #endregion
                            case "F_WTBH":
                                #region 委托编号
                                Object objF_WTBH = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                if (objF_WTBH != null)
                                {
                                    mdlUD.F_WTBH = objF_WTBH.ToString().Replace("\r\n", "");
                                }
                                else
                                {
                                    mdlUD.F_WTBH = "";
                                }
                                break;
                                #endregion
                            case "F_SJBH":
                                #region 试件编号
                                mdlUD.F_SJBH = SeriaNumber.ToString();
                                break;
                                #endregion
                            case "F_PZCODE":
                                #region 牌号
                                Object objF_PZCODE = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                if (objF_PZCODE != null)
                                {
                                    mdlUD.F_PZCODE = objF_PZCODE.ToString().Replace("\r\n", "");
                                }
                                else
                                {
                                    mdlUD.F_PZCODE = "";
                                }
                                break;
                                #endregion
                            case "F_GCZJ":
                                #region 公称直径(mm)
                                Object objF_GCZJ = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                if (objF_GCZJ != null)
                                {
                                    try
                                    {
                                        mdlUD.F_GCZJ = float.Parse(objF_GCZJ.ToString());
                                    }
                                    catch
                                    {
                                        logger.Info("GetUniversalData objF_GCZJ转换失败:" + objF_GCZJ);
                                    }
                                }
                                else
                                {
                                    mdlUD.F_GCZJ = 0;
                                }
                                break;
                                #endregion
                            case "F_AREA":
                                #region 截面面积(mm2)
                                Object objF_AREA = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                if (objF_AREA != null)
                                {
                                    try
                                    {
                                        mdlUD.F_AREA = float.Parse(objF_AREA.ToString());
                                    }
                                    catch
                                    {
                                        logger.Info("GetUniversalData F_AREA转换失败:" + objF_AREA);
                                    }
                                }
                                else
                                {
                                    mdlUD.F_AREA = 0;
                                }
                                break;
                                #endregion
                            case "F_LZ":
                                #region 最大抗拉力值(KN)
                                if (!string.IsNullOrEmpty(item.CellName))
                                {
                                    string F_LZCellName = (Convert.ToChar(item.CellName[0] + (SeriaNumber - 1) * 3)).ToString() + item.CellName.Remove(0, 1) + "";
                                    Object objF_LZ = JZCommonHelper.GetCellValue(doc, item.SheetID, F_LZCellName);
                                    if (objF_LZ != null)
                                    {
                                        try
                                        {
                                            mdlUD.F_LZ = float.Parse(objF_LZ.ToString());
                                        }
                                        catch
                                        {
                                            logger.Info("GetUniversalData F_LZ转换失败:" + objF_LZ);
                                        }
                                    }
                                    else
                                    {
                                        mdlUD.F_LZ = 0;
                                    }
                                }
                                else
                                {
                                    mdlUD.F_QFLZ = 0;
                                }
                                break;
                                #endregion

                            case "F_QFLZ":
                                #region 屈服力值(KN)
                                if (!string.IsNullOrEmpty(item.CellName))
                                {
                                    string F_QFLZCellName = (Convert.ToChar(item.CellName[0] + (SeriaNumber - 1) * 3)).ToString() + item.CellName.Remove(0, 1) + "";
                                    Object objF_QFLZ = JZCommonHelper.GetCellValue(doc, item.SheetID, F_QFLZCellName);
                                    if (objF_QFLZ != null)
                                    {
                                        try
                                        {
                                            mdlUD.F_QFLZ = float.Parse(objF_QFLZ.ToString());
                                        }
                                        catch
                                        {
                                            logger.Info("GetUniversalData F_QFLZ转换失败:" + objF_QFLZ);
                                        }
                                    }
                                    else
                                    {
                                        mdlUD.F_QFLZ = 0;
                                    }
                                }
                                else
                                {
                                    mdlUD.F_QFLZ = 0;
                                }
                                break;
                                #endregion
                            case "F_SCL":
                                #region 伸长率
                                if (!string.IsNullOrEmpty(item.CellName))
                                {
                                    string F_SCLCellName = (Convert.ToChar(item.CellName[0] + (SeriaNumber - 1) * 3)).ToString() + item.CellName.Remove(0, 1) + "";
                                    Object objF_SCL = JZCommonHelper.GetCellValue(doc, item.SheetID, F_SCLCellName);
                                    if (objF_SCL != null)
                                    {
                                        try
                                        {
                                            mdlUD.F_SCL = float.Parse(objF_SCL.ToString());
                                        }
                                        catch
                                        {
                                            logger.Info("GetUniversalData F_SCL转换失败:" + objF_SCL);
                                        }
                                    }
                                    else
                                    {
                                        mdlUD.F_SCL = 0;
                                    }
                                }
                                else
                                {
                                    mdlUD.F_QFLZ = 0;
                                }
                                break;
                                #endregion
                            case "F_SYSJ":
                                #region 采集时间
                                mdlUD.F_SYSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                break;
                                #endregion
                            case "F_OPERATOR":
                                #region 操作员
                                mdlUD.F_OPERATOR = UserName;
                                break;
                                #endregion
                            case "F_YSKLLZ"://抗拉力值过程数据    
                                #region
                                List<JZRealTimeData> rtlCells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZRealTimeData>>(BizCommon.JZCommonHelper.GZipDecompressString(RealTimeData));
                                string F_YSKLLZ = string.Empty;
                                if (rtlCells.Count > 0)
                                {
                                    foreach (JZRealTimeData rtd in rtlCells)
                                    {
                                        F_YSKLLZ += rtd.Value + ",";
                                    }
                                }
                                else
                                {
                                    F_YSKLLZ = "";
                                }
                                if (F_YSKLLZ.Length > 0)
                                {
                                    F_YSKLLZ = F_YSKLLZ.Substring(0, F_YSKLLZ.Length - 1);
                                }
                                mdlUD.F_YSKLLZ = F_YSKLLZ;
                                #endregion
                                break;
                            case "F_WY"://位移值过程数据
                                mdlUD.F_WY = "0";
                                break;
                            case "F_SOFTCOM"://厂家名称
                                mdlUD.F_SOFTCOM = item.CellName;
                                break;
                            case "F_COLCOM"://控制器厂家名称
                                mdlUD.F_COLCOM = "";
                                break;
                            default:
                                break;
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mdlUD = null;
                logger.Info("GetUniversalData error:" + TestDataID + " " + ex.ToString());
            }
            return mdlUD;
        }
        private class ThreadParameter
        {
            public JZDocument Doc;
            public Sys_Module ModuleBase;
            public Guid TestDataID;
            public string MachineCode;
            public int SeriaNumber;
            public string TestData;
            public string UserName;
            public string RealTimeData;
            public string TestRoomCode;
        }
        #endregion
        #region 同步工管中心用户登录验证数据
        public bool SyncGGCUserAuth()
        {
            bool bSuccess = true;
            try
            {
                string GGCTestRoomCodeMap = System.Configuration.ConfigurationManager.AppSettings["GGCTestRoomCodeMap"];
                Hashtable testRoomCodeMap = new Hashtable();
                testRoomCodeMap = InitTestRoomCodeMap(GGCTestRoomCodeMap);
                foreach (KeyValuePair<string, string> item in testRoomCodeMap)
                {
                    logger.Info("同步工管中心用户登录验证数据,试验室编码：" + item.Key);
                    string sysbm = item.Value;//"3SJ1D0BD09ZX01";
                    string jsonUserinfo = string.Empty;
                    string errMsg = string.Empty;
                    int result = AuthentificationUser(sysbm, out jsonUserinfo, out errMsg);
                    if (result == 1)
                    { //成功
                        //logger.Info("jsonUserinfo:"+jsonUserinfo);
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonUserinfo);
                        #region
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string IDCARDNUM = dt.Rows[i]["IDCARDNUM"].ToString();
                                string YHMC = dt.Rows[i]["YHMC"].ToString();
                                string LABKEY = dt.Rows[i]["LABKEY"].ToString();
                                string LABIV = dt.Rows[i]["LABIV"].ToString();
                                if (string.IsNullOrEmpty(IDCARDNUM) || string.IsNullOrEmpty(YHMC) || string.IsNullOrEmpty(LABKEY) || string.IsNullOrEmpty(LABIV))
                                {
                                    logger.Info("GetGGUserAuth continue:" + string.Format("IDCARDNUM:{0},YHMC:{1},LABKEY:{2},LABIV:{3}", IDCARDNUM, YHMC, LABKEY, LABIV));
                                    continue;
                                }
                                else
                                {
                                    string strSQL = "SELECT * FROM dbo.sys_ggc_UserAuth WHERE IDCARDNUM=@0";
                                    DataSet dsExist = GetDataSet(strSQL, new object[] { IDCARDNUM });
                                    if (dsExist != null && dsExist.Tables.Count > 0)
                                    {
                                        if (dsExist.Tables[0].Rows.Count > 0)
                                        {//更新 
                                            strSQL = "UPDATE dbo.sys_ggc_UserAuth SET YHMC='{1}',LABKEY='{2}',LABIV='{3}' WHERE IDCARDNUM='{0}'";
                                        }
                                        else
                                        {//插入
                                            strSQL = "INSERT INTO dbo.sys_ggc_UserAuth( IDCARDNUM  ,YHMC ,LABKEY ,LABIV)VALUES( '{0}', '{1}' ,'{2}' ,'{3}')";
                                        }
                                        strSQL = string.Format(strSQL, IDCARDNUM, YHMC, LABKEY, LABIV);
                                        int r = ExcuteCommand(strSQL);
                                        if (r == 1)
                                        {
                                        }
                                        else
                                        {
                                            bSuccess = false;
                                        }
                                    }
                                    else
                                    {
                                        bSuccess = false;
                                        logger.Info("GetGGUserAuth strSQL:" + strSQL);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        bSuccess = false;
                        logger.Info("GetGGUserAuth errMsg:" + errMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                bSuccess = false;
                logger.Info("GetGGUserAuth error:" + ex.ToString());
            }
            return bSuccess;
        }
        #endregion
        #region 上传到工管中心方法
        /// <summary>
        /// 返回值1代表正常，异常返回-1
        /// </summary>
        /// <param name="sysbm">试验室编码</param>
        /// <param name="jsonUserinfo">用户身份证号及密钥信息的datatable对应的json串</param>
        /// <param name="errMsg">正常返回空字符串””,异常返回错误信息</param>
        /// <returns></returns>
        private int AuthentificationUser(string sysbm, out string jsonUserinfo, out string errMsg)
        {
            int result = 0;
            try
            {
                LabMS.LabManagerServiceClient erty = new LabMS.LabManagerServiceClient("WSHttpBinding_ILabManagerService");
                erty.Open();
                result = erty.AuthentificationUser(out jsonUserinfo, out errMsg, sysbm);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                jsonUserinfo = "";
                logger.Error("AuthentificationUser error:" + ex.ToString());
            }

            return result;
        }


        private int UploadPressureDataDES(string idcardnum, string jsonPressure, out string errMsg)
        {
            int result = 0;
            try
            {
                LabMS.LabManagerServiceClient erty = new LabMS.LabManagerServiceClient("WSHttpBinding_ILabManagerService");
                erty.Open();
                result = erty.UploadPressureDataDES(out errMsg, idcardnum, jsonPressure);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                jsonPressure = "";
                logger.Error("UploadPressureDataDES error:" + ex.ToString());
            }

            return result;
        }

        private int UploadUniversalDataDES(string idcardnum, string jsonUniversal, out string errMsg)
        {
            int result = 0;
            try
            {
                LabMS.LabManagerServiceClient erty = new LabMS.LabManagerServiceClient("WSHttpBinding_ILabManagerService");
                erty.Open();
                result = erty.UploadUniversalDataDES(out errMsg, idcardnum, jsonUniversal);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                jsonUniversal = "";
                logger.Error("UploadUniversalDataDES error:" + ex.ToString());
            }

            return result;
        }


        private int UploadLabDocBasicInfoDES(string idcardnum, string jsonLabdata, out string errMsg)
        {
            int result = 0;
            try
            {
                LabMS.LabManagerServiceClient erty = new LabMS.LabManagerServiceClient("WSHttpBinding_ILabManagerService");
                erty.Open();
                result = erty.UploadLabDocBasicInfoDES(out errMsg, idcardnum, jsonLabdata);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                jsonLabdata = "";
                logger.Error("UploadLabDocBasicInfoDES error:" + ex.ToString());
            }

            return result;
        }


        private int UploadLabDocContentDES(string idcardnum, string jsonLabDocContent, out string errMsg)
        {
            int result = 0;
            try
            {
                LabMS.LabManagerServiceClient erty = new LabMS.LabManagerServiceClient("WSHttpBinding_ILabManagerService");
                erty.Open();
                result = erty.UploadLabDocContentDES(out errMsg, idcardnum, jsonLabDocContent);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                jsonLabDocContent = "";
                logger.Error("UploadLabDocContentDES error:" + ex.ToString());
            }

            return result;
        }
        #endregion
        #region 通用方法
        protected string CreateRalationFilesSync(FpSpread fpSpread, Guid documentID, string docDateDir)
        {
            //String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
            //CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "CreateRalationFilesSync",
            //   new Object[] { fpSpread, documentID, reportIndex, docDateDir });

            if (documentID == null || fpSpread == null)
            {
                return "";
            }
            try
            {
                String path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), "Temp/" + docDateDir);
                String excelPath = Path.Combine(path, documentID.ToString() + ".xls");
                if (File.Exists(excelPath))
                {
                    File.Delete(excelPath);
                }
                JZCommonHelper.SaveToExcel(documentID, fpSpread, path);
                //JZCommonHelper.FarpointToImage0(documentID, fpSpread, path, reportIndex);
                //String pdfFilePath = Path.Combine(path, documentID.ToString() + ".pdf");
                //JZCommonHelper.FarpointToPDF0(fpSpread, pdfFilePath, reportIndex);
                return excelPath;
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("CreateRalationFilesSync error:" + ex.Message));
            }
            return "";
        }
        public string GetServerDirectory(string strDBName)
        {
            string strDirectoryPath = string.Empty;
            string strDirectory = System.AppDomain.CurrentDomain.BaseDirectory;//Directory.GetCurrentDirectory();
            strDirectory = strDirectory.Substring(0, strDirectory.Length - 1);
            int LastIndex = strDirectory.LastIndexOf('\\');
            string strUploadDirectoryName = strDirectory.Substring(LastIndex + 1, strDirectory.Length - LastIndex - 1);
            strDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory.Replace(strUploadDirectoryName, string.Format("TransferService({0})", strDBName.Replace("SYGLDB_", "")));
            return strDirectoryPath;
        }

        

        public static string Encrypt(string sourceString, string key, string iv)
        {
            try
            {
                //byte[] btKey = Encoding.UTF8.GetBytes(key);
                byte[] btKey = Convert.FromBase64String(key);

                //byte[] btIV = Encoding.UTF8.GetBytes(iv);
                byte[] btIV = Convert.FromBase64String(iv);

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] inData = Encoding.UTF8.GetBytes(sourceString);
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);

                        cs.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("加密出错 sourceString:{0},key:{1},iv:{2},ex:{3}", sourceString, key, iv, ex.ToString()));
            }

            return "";
        }
        public static string Decrypt(string encryptedString, string key, string iv)
        {
            //byte[] btKey = Encoding.UTF8.GetBytes(key);
            byte[] btKey = Convert.FromBase64String(key);

            //byte[] btIV = Encoding.UTF8.GetBytes(iv);
            byte[] btIV = Convert.FromBase64String(iv);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Convert.FromBase64String(encryptedString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);

                        cs.FlushFinalBlock();
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
                catch (Exception ex)
                {
                    logger.Error(string.Format("解密出错 encryptedString:{0},key:{1},iv:{2},ex:{3}", encryptedString, key, iv, ex.ToString()));
                    return encryptedString;
                }
            }
        }

        private Hashtable InitTestRoomCodeMap(String map)
        {
            Hashtable testRoomCodeMap = new Hashtable();
            if (!String.IsNullOrEmpty(map))
            {
                String[] item = map.Split(new Char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (item.Length > 0)
                {
                    foreach (var sub in item)
                    {
                        String[] subItem = sub.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (subItem.Length == 2)
                        {
                            testRoomCodeMap.Add(subItem[0], subItem[1]);
                        }
                    }
                }
            }
            return testRoomCodeMap;
        }
        public string GetNeedUploadTestRoomCode()
        {
            string GGCTestRoomCodeMap = System.Configuration.ConfigurationManager.AppSettings["GGCTestRoomCodeMap"];
            Hashtable testRoomCodeMap = new Hashtable();
            testRoomCodeMap = InitTestRoomCodeMap(GGCTestRoomCodeMap);

            StringBuilder strTestRoomCodes = new StringBuilder();
            foreach (DictionaryEntry item in testRoomCodeMap)
            {
                strTestRoomCodes.AppendFormat("'{0}',", item.Key);
            }
            if (strTestRoomCodes.Length > 0)
            {
                strTestRoomCodes = strTestRoomCodes.Remove(strTestRoomCodes.Length - 1, 1);
            }
            return strTestRoomCodes.ToString();
        }
        #endregion
    }
}
