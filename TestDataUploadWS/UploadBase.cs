using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizCommon;
using System.ServiceModel;
using System.Collections;
using TransferServiceCommon;
using System.Xml;
using LABTRANSINTERFACE;
using System.IO;
using System.Data;
using FarPoint.Win.Spread;
using FarPoint.Win;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;

namespace TestDataUploadWS
{
    /// <summary>
    /// 上传到信息中心
    /// </summary>
    public class UploadBase
    {
        private string IPAddress = string.Empty;
        private string Port = string.Empty;
        private string DataBaseName = string.Empty;
        private string UploadAddress = string.Empty;
        private string JSDWCode = string.Empty;
        private Hashtable testRoomCodeMap = new Hashtable();
        //private Hashtable ModuleCodeMap = new Hashtable();
        private String LineName = "";
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UploadBase(String _LineName, String _IPAddress, String _Port, String _DataBaseName, String _UploadAddress,
             String _TestRoomCodeMap, String _JSDWCode)//, string _ModuleCodeMap
        {
            IPAddress = _IPAddress;
            Port = _Port;
            DataBaseName = _DataBaseName;
            UploadAddress = _UploadAddress;
            LineName = _LineName;
            UploadAddress = _UploadAddress;
            JSDWCode = _JSDWCode;
            InitTestRoomCodeMap(_TestRoomCodeMap);
            //InitModuleCodeMap(_ModuleCodeMap);
        }
        public void StartUpload()
        {
            UploadTestRoomInfo();
            UploadPersonInfo();
            UploadTestDataInfo();
        }
        #region 试验资料上传

        /// <summary>
        ///上传试验室综合情况
        /// </summary>
        public void UploadTestRoomInfo()
        {
            //logger.Info("UploadTestRoomInfo 0");
            try
            {
                //Hashtable testRoomCodeMap = InitTestRoomCodeMap(strTestRoomCodeMap);
                String moduleID = "E77624E9-5654-4185-9A29-8229AAFDD68B"; //试验室综合情况
                String ksign = "01";//01表示试验室
                UploadSetting set = GetUploadSettingByModuleID(moduleID);
                if (set != null)
                {
                    String sql = "SELECT TestRoomCode,Data FROM dbo.sys_document WHERE ModuleID='" + moduleID + "' AND Status>0";
                    DataTable dt = GetDataTable(sql);
                    List<String> files = new List<string>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        String path = Path.Combine(GetServerDirectory(DataBaseName), "Temp/TestDataUpload/试验室基本属性");
                        //DeleteDirAllFile(path);//删除所有文件，正式发布时注释
                        if (!Directory.Exists(path))
                        {
                            try
                            {
                                Directory.CreateDirectory(path);
                            }
                            catch (Exception ee)
                            {
                                logger.Error(ee.Message);
                            }
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            String testRoomCode = dt.Rows[i]["TestRoomCode"].ToString();
                            if (testRoomCodeMap.ContainsKey(testRoomCode))
                            {
                                testRoomCode = testRoomCodeMap[testRoomCode].ToString();
                            }

                            JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dt.Rows[i]["Data"].ToString());
                            String fileName = Path.Combine(path, testRoomCode + ".xml");
                            if (File.Exists(fileName))
                            {
                                try
                                {
                                    File.Delete(fileName);
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex.Message);
                                }
                            }
                            if (CreatTestRoomInfoXMLFile(set, fileName, doc, testRoomCode))
                            {
                                files.Add(fileName);
                            }
                            doc = null;
                        }

                        if (files.Count > 0)
                        {
                            String zipFile = Path.Combine(path, "试验室基本属性.zip");
                            if (File.Exists(zipFile))
                            {

                                System.IO.FileInfo file = new System.IO.FileInfo(zipFile);

                                DateTime dtLastWriteTime = file.LastWriteTime;//最后修改时间
                                if (dtLastWriteTime.Date.CompareTo(DateTime.Now.Date) == 0)
                                {//每天上传一次
                                    logger.Info(string.Format("【{0}】试验室基本属性今天已经上传过了", LineName));
                                    return;
                                }
                                else
                                {
                                    try
                                    {
                                        File.Delete(zipFile);
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Error(ex.Message);
                                    }
                                }
                            }
                            if (JZCommonHelper.CreateZipFile(files, zipFile))
                            {
                                FileStream stream = null;
                                stream = new FileInfo(zipFile).OpenRead();
                                Byte[] buffer = new Byte[stream.Length];
                                stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                                stream.Close();
                                String result = UploadToServer(UploadAddress, buffer, ksign, JSDWCode);

                                if (result == "1")
                                {
                                    logger.Info(string.Format("【{0}】试验室综合情况数据上传成功", LineName));
                                }
                                else
                                {
                                    logger.Info(string.Format("【{0}】试验室综合情况数据上传失败，返回值为：" + result, LineName));
                                }
                            }
                        }
                    }
                    else
                    {
                        logger.Info(string.Format("【{0}】未找到试验室综合情况信息", LineName));
                    }
                }
                else
                {
                    logger.Info(string.Format("【{0}】未找到该模板对应的上传设置,ModuleID:" + moduleID, LineName));
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("【{0}】UploadTestRoomInfo error:" + ex.ToString(), LineName));
            }
        }
        /// <summary>
        /// 上传人员信息
        /// </summary>
        public void UploadPersonInfo()
        {
            try
            {
                //Hashtable testRoomCodeMap = InitTestRoomCodeMap(strTestRoomCodeMap);
                String moduleID = "08899BA2-CC88-403E-9182-3EF73F5FB0CE"; //试验人员
                String ksign = "02";//02表示试验人员
                UploadSetting set = GetUploadSettingByModuleID(moduleID);
                if (set != null)
                {
                    String sql = "SELECT ID,TestRoomCode,Data FROM dbo.sys_document WHERE ModuleID='" + moduleID + "' AND Status>0";
                    DataTable dt = GetDataTable(sql);
                    List<String> files = new List<String>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        String path = Path.Combine(GetServerDirectory(DataBaseName), "Temp/TestDataUpload/试验室人员");
                        if (!Directory.Exists(path))
                        {
                            try
                            {
                                Directory.CreateDirectory(path);
                            }
                            catch (Exception ee)
                            {
                                logger.Error(ee.Message);
                            }
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            String testRoomCode = dt.Rows[i]["TestRoomCode"].ToString();
                            DataRow[] drArr = dt.Select(string.Format("TestRoomCode='{0}'", testRoomCode));
                            if (testRoomCodeMap.ContainsKey(testRoomCode))
                            {
                                testRoomCode = testRoomCodeMap[testRoomCode].ToString();
                            }

                            String fileName = Path.Combine(path, testRoomCode + ".xml");
                            if (File.Exists(fileName))
                            {
                                try
                                {
                                    File.Delete(fileName);
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex.Message);
                                }
                            }
                            ProcessPersonPhoto(drArr, files, path);
                            if (CreatPersonInfoXMLFile(set, fileName, drArr, testRoomCode))
                            {
                                files.Add(fileName);
                                foreach (DataRow row in drArr)
                                {
                                    dt.Rows.Remove(row);
                                }
                            }
                            drArr = null;
                        }
                        if (files.Count > 0)
                        {
                            String zipFile = Path.Combine(path, "试验室人员.zip");

                            if (File.Exists(zipFile))
                            {

                                System.IO.FileInfo file = new System.IO.FileInfo(zipFile);

                                DateTime dtLastWriteTime = file.LastWriteTime;//最后修改时间
                                if (dtLastWriteTime.Date.CompareTo(DateTime.Now.Date) == 0)
                                {//每天上传一次
                                    logger.Info(string.Format("【{0}】试验室人员资料今天已经上传过了", LineName));
                                    return;
                                }
                                else
                                {
                                    try
                                    {
                                        File.Delete(zipFile);
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Error(ex.Message);
                                    }
                                }
                            }
                            if (JZCommonHelper.CreateZipFile(files, zipFile))
                            {
                                FileStream stream = null;
                                stream = new FileInfo(zipFile).OpenRead();
                                Byte[] buffer = new Byte[stream.Length];
                                stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                                stream.Close();
                                String result = UploadToServer(UploadAddress, buffer, ksign, JSDWCode);

                                if (result == "1")
                                {
                                    logger.Info(string.Format("【{0}】试验室人员上传成功", LineName));
                                }
                                else
                                {
                                    logger.Info(string.Format("【{0}】试验室人员上传失败，返回值为：" + result, LineName));
                                }
                            }
                        }
                    }
                    else
                    {
                        logger.Info(string.Format("【{0}】未找到试验人员信息", LineName));
                    }
                }
                else
                {
                    logger.Info(string.Format("【{0}】未找到该模板对应的上传设置,ModuleID:" + moduleID, LineName));
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("【{0}】UploadPersonInfo error:" + ex.ToString(), LineName));
            }
        }

        /// <summary>
        /// 生成人员头像(硬编码身份证与头像单元格位置)
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="files"></param>
        /// <param name="path"></param>
        private void ProcessPersonPhoto(DataRow[] drArr, List<String> files, String path)
        {
            //logger.Error("ProcessPersonPhoto 0");
            foreach (DataRow dr in drArr)
            {
                JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dr["Data"].ToString());
                String personID = "";
                foreach (var item in doc.Sheets[0].Cells)
                {
                    if (item.Name == "D10")//身份证
                    {
                        if (item.Value != null)
                        {
                            personID = item.Value.ToString();
                            break;
                        }
                    }
                }
                //logger.Error("personID:" + personID);
                if (personID != "")
                {
                    String photoFile = Path.Combine(path, personID + ".jpg");
                    if (!files.Contains(photoFile))
                    {
                        try
                        {
                            Boolean saved = false;
                            foreach (var item in doc.Sheets[0].Cells)
                            {
                                if (item.Name == "L6")//照片
                                {
                                    if (item.Value != null)
                                    {
                                        if (File.Exists(photoFile))
                                        {
                                            try
                                            {
                                                File.Delete(photoFile);
                                            }
                                            catch (Exception ex)
                                            {
                                                logger.Error(ex.Message);
                                            }
                                        }

                                        byte[] bitmapBytes = System.Convert.FromBase64String(item.Value.ToString());
                                        using (MemoryStream memoryStream = new MemoryStream(bitmapBytes))
                                        {
                                            Image image = Image.FromStream(memoryStream);
                                            image.Save(photoFile, ImageFormat.Jpeg);
                                        }

                                        saved = true;
                                    }
                                    break;
                                }
                            }
                            if (saved)
                            {
                                files.Add(photoFile);
                            }
                        }
                        catch (Exception e)
                        {
                            logger.Error(e.Message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 上传试验数据
        /// </summary>
        public void UploadTestDataInfo()
        {
            try
            {
                //Hashtable testRoomCodeMap = InitTestRoomCodeMap(strTestRoomCodeMap);
                String sql = @" SELECT top 100 ID,TestRoomCode,Data,WillUploadCount,ModuleID,BGBH,TryType,CreatedTime FROM dbo.sys_document WHERE ModuleID NOT IN ('E77624E9-5654-4185-9A29-8229AAFDD68B','08899BA2-CC88-403E-9182-3EF73F5FB0CE','BA23C25D-7C79-4CB3-A0DC-ACFA6C285295') AND Status>0 AND NeedUpload=1 and BGBH is not null and BGBH<>''
 AND ModuleID IN( SELECT ID FROM dbo.sys_module WHERE ModuleALT<>'')
  ORDER BY CreatedTime";//排除人员、设备、试验室和没有设置RTCODE的模板
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    String ksign = "05";//05表示试验数据
                    List<String> files = new List<String>();
                    #region 创建目录
                    String path = Path.Combine(GetServerDirectory(DataBaseName), "Temp/TestDataUpload/试验数据");
                    DeleteDirAllFile(path);
                    if (!Directory.Exists(path))
                    {
                        try
                        {
                            Directory.CreateDirectory(path);
                        }
                        catch (Exception ee)
                        {
                            logger.Error(ee.Message);
                        }
                    }
                    #endregion

                    StringBuilder strIDs = new StringBuilder();
                    UploadSetting[] arrSet = new UploadSetting[dt.Rows.Count];//上传设置
                    DataRow[] arrDocRow = new DataRow[dt.Rows.Count];//资料数据
                    int[] arrWUC = new int[dt.Rows.Count];//WillUploadCount  数字版本号
                    int[] arrSFHG = new int[dt.Rows.Count];//是否合格[1合格，0不合格]
                    string[] arrBHGX = new string[dt.Rows.Count];//不合格项目[“名称_标准规定值_项目实测值^名称_标准规定值_项目实测值”， 名称、标准规定值和项目实测值间以英文”_”分隔多项不合格，信息以“^”分隔，无不合格信息的此项为空]
                    int[] arrDevType = new int[dt.Rows.Count];//涉及的试验仪器 ‘0’默认：不涉及,’1’：涉及压力机‘2’：涉及万能机
                    int[] arrBGZT = new int[dt.Rows.Count];//  报告类型[0自检，1见证，2监理平检，3其它]
                    string[] arrTRCODE = new string[dt.Rows.Count];//试验室编码
                    string[] arrRTCODE = new string[dt.Rows.Count];//报告类型

                    #region 创建资料列表
                    logger.Info(string.Format("【{0}】生成报告文件开始", LineName));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string strID = dt.Rows[i]["ID"].ToString();
                        String testRoomCode = dt.Rows[i]["TestRoomCode"].ToString();
                        string strModuleID = dt.Rows[i]["ModuleID"].ToString();
                        string strBGBH = dt.Rows[i]["BGBH"].ToString();
                        string docDateDir = DateTime.Parse(dt.Rows[i]["CreatedTime"].ToString()).ToString("yyyy/MM/dd");
                        int WillUploadCount = int.Parse(dt.Rows[i]["WillUploadCount"].ToString());
                        string TryType = dt.Rows[i]["WillUploadCount"] == null ? "" : dt.Rows[i]["WillUploadCount"].ToString();
                        strIDs.AppendFormat("'{0}'", strID);
                        if (i < dt.Rows.Count - 1)
                        {
                            strIDs.Append(",");
                        }
                        Sys_Module module = GetModuleBaseInfoByID(new Guid(strModuleID));
                        UploadSetting set = module.UploadSetting;
                        if (set != null)
                        {
                            if (testRoomCodeMap.ContainsKey(testRoomCode))
                            {
                                testRoomCode = testRoomCodeMap[testRoomCode].ToString();
                            }
                            else
                            {
                                logger.Info(string.Format("【{0}】试验室编码[{1}]未配置对应的上传代码", LineName,testRoomCode));
                            }
                            //if (ModuleCodeMap.ContainsKey(strModuleID))
                            //{
                            //    strModuleID = testRoomCodeMap[strModuleID].ToString();
                            //}
                            string strSourceFile = Path.Combine(GetServerDirectory(DataBaseName), string.Format("source/{0}/{1}.pdf", docDateDir, strID));
                            string strDestFile = Path.Combine(GetServerDirectory(DataBaseName), string.Format("Temp/TestDataUpload/试验数据/{0}_{1}.pdf", testRoomCode, strBGBH));
                            #region 生成PDF和图片  暂时删除
                            if (File.Exists(strSourceFile))
                            {
                                File.Delete(strSourceFile);
                            }
                            JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dt.Rows[i]["Data"].ToString());
                            FpSpread fpSpread = new FpSpread();
                            String sheetXML = JZCommonHelper.GZipDecompressString(GetSheetXMLByID(doc.Sheets[module.ReportIndex].ID));// mh.GetSheetXMLByID(sheet.ID);
                            SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), sheetXML, "SheetView") as SheetView;
                            SheetView.Tag = doc.Sheets[module.ReportIndex].ID;
                            SheetView.Cells[0, 0].Value = "";
                            SheetView.Protect = true;
                            foreach (JZCell dataCell in doc.Sheets[module.ReportIndex].Cells)
                            {
                                Cell cell = SheetView.Cells[dataCell.Name];

                                if (cell != null)
                                {
                                    cell.Value = dataCell.Value;
                                }
                            }
                            fpSpread.Sheets.Add(SheetView);

                            CreateRalationFilesSync(fpSpread, doc.ID, module.ReportIndex, docDateDir);
                            fpSpread.Dispose();
                            doc = null;
                            #endregion
                            File.Copy(strSourceFile, strDestFile, true);
                            files.Add(strDestFile);
                            #region 数组赋值
                            arrSet[i] = set;
                            arrDocRow[i] = dt.Rows[i];
                            arrWUC[i] = WillUploadCount;
                            arrDevType[i] = module.DeviceType;
                            arrRTCODE[i] = module.ModuleALT;
                            arrTRCODE[i] = testRoomCode;
                            switch (TryType)
                            {
                                case "自检":
                                    arrBGZT[i] = 0;
                                    break;
                                case "见证":
                                    arrBGZT[i] = 1;
                                    break;
                                case "平检":
                                    arrBGZT[i] = 2;
                                    break;
                                default:
                                    arrBGZT[i] = 3;
                                    break;
                            }
                            #endregion
                            #region 不合格判断

                            sql = String.Format("select ID,F_InvalidItem from sys_invalid_document where ID='{0}' and AdditionalQualified=0 ", strID);
                            DataTable dtInvalid = GetDataTable(sql);
                            if (dt != null && dtInvalid.Rows.Count > 0)
                            {//不合格
                                arrSFHG[i] = 0;
                                string strInvalidItem = dtInvalid.Rows[0]["F_InvalidItem"].ToString().Trim('|');
                                strInvalidItem = strInvalidItem.Replace("||", "");
                                strInvalidItem = strInvalidItem.Replace(",", "_");
                                arrBHGX[i] = strInvalidItem;
                                //string[] arrInvalidItem = strInvalidItem.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                                //for (int j = 0; j < arrInvalidItem.Length; j++)
                                //{

                                //}
                            }
                            else
                            {
                                arrSFHG[i] = 1;
                                arrBHGX[i] = "";
                            }
                            #endregion
                        }
                        else
                        {
                            arrSet[i] = null;
                            arrDocRow[i] = dt.Rows[i];
                            arrWUC[i] = WillUploadCount;
                            logger.Info(string.Format("【{0}】未找到该模板对应的上传设置,资料ID:" + strID, LineName));
                        }
                    }
                    logger.Info(string.Format("【{0}】生成报告文件结束", LineName));
                    #endregion
                    String fileName = Path.Combine(GetServerDirectory(DataBaseName), "Temp/TestDataUpload/试验数据/reportInfo.xml");
                    //生成reportInfo.xml
                    CreatReportInfoXMLFile(arrSet, fileName, arrDocRow, arrTRCODE, arrWUC, arrSFHG, arrBHGX, arrDevType, arrBGZT, arrRTCODE);
                    //logger.Error("reportInfo 1");
                    files.Add(fileName);

                    #region 上传资料
                    if (files.Count > 0)
                    {
                        String zipFile = Path.Combine(path, "试验报告.zip");
                        if (File.Exists(zipFile))
                        {
                            try
                            {
                                File.Delete(zipFile);
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);
                            }
                        }
                        if (JZCommonHelper.CreateZipFile(files, zipFile))
                        {
                            FileStream stream = null;
                            stream = new FileInfo(zipFile).OpenRead();
                            Byte[] buffer = new Byte[stream.Length];
                            stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                            stream.Close();
                            String result = UploadToServer(UploadAddress, buffer, ksign, JSDWCode);

                            //MemoryStream zipFileStream = new MemoryStream();
                            //ExportToFile(zipFile,out zipFileStream);
                            //String result = UploadToServer(UploadAddress, zipFileStream.ToArray(), ksign, JSDWCode);
                            if (result == "1")
                            {
                                if (strIDs.Length > 0)
                                {
                                    UpdateDocumentNeedUpload(strIDs.ToString());
                                    logger.Info("试验报告上传成功 strIDs:" + strIDs);
                                    //logger.Info(string.Format("strIDs:{0}", strIDs));
                                }
                            }
                            else
                            {
                                logger.Info(string.Format("【{0}】试验报告上传失败，返回值为：" + result, LineName));
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    logger.Info(string.Format("【{0}】未找到试验数据信息", LineName));
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("【{0}】UploadTestDataInfo error:" + ex.ToString(), LineName));
            }
        }
        #endregion
        #region 上传公用方法
        Boolean UpdateDocumentNeedUpload(string strIDs)
        {
            String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
            object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "UpdateDocumentNeedUpload",
               new Object[] { strIDs });

            if (obj != null)
            {
                return Boolean.Parse(obj.ToString());
            }
            else
            {
                return false;
            }
        }
        protected String GetSheetXMLByID(Guid sheetIndex)
        {
            String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
            object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetSheetXMLByID",
               new Object[] { sheetIndex });

            return obj.ToString();
        }
        protected Sys_Module GetModuleBaseInfoByID(Guid moduleID)
        {
            String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
            object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetModuleBaseInfoByID",
               new Object[] { moduleID });
            if (obj != null)
            {
                return obj as Sys_Module;
            }
            else
            {
                return null;
            }
        }
        protected void CreateRalationFilesSync(FpSpread fpSpread, Guid documentID, Int32 reportIndex, string docDateDir)
        {
            //String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
            //CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "CreateRalationFilesSync",
            //   new Object[] { fpSpread, documentID, reportIndex, docDateDir });

            if (documentID == null || fpSpread == null)
            {
                return;
            }
            try
            {
                String path = Path.Combine(GetServerDirectory(DataBaseName), "source/" + docDateDir);
                JZCommonHelper.SaveToExcel(documentID, fpSpread, path);
                JZCommonHelper.FarpointToImage0(documentID, fpSpread, path, reportIndex);
                //String excelPath = Path.Combine(path,documentID.ToString() + ".xls");
                String pdfFilePath = Path.Combine(path, documentID.ToString() + ".pdf");
                JZCommonHelper.FarpointToPDF0(fpSpread, pdfFilePath, reportIndex);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("【{0}】create ralation files:" + ex.Message, LineName));
            }
        }
        /// <summary>
        /// 返回应用程序的实际路径
        /// </summary>
        /// <param name="xmlFile">文件虚拟路径</param>
        /// <returns></returns>
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
        private UploadSetting GetUploadSettingByModuleID(String moduleID)
        {
            String sql = "SELECT UploadSetting  FROM dbo.sys_module WHERE ID='" + moduleID + "'";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count == 1)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<UploadSetting>(dt.Rows[0]["UploadSetting"].ToString());
            }
            return null;
        }
        protected DataTable GetDataTable(String strSQL)
        {

            DataTable dt = new DataTable();
            String lineAddress = "net.tcp://" + IPAddress + ":" + Port + "/TransferService.svc";
            dt = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetDataTable",
               new Object[] { strSQL }) as DataTable;
            return dt;
        }
        /// <summary>  
        /// 递归(删除文件夹下的所有文件)  
        /// </summary>  
        /// <param name="dirRoot">目录地址</param>  
        public void DeleteDirAllFile(string dirRoot)
        {
            //string deleteFileName = "_desktop.ini";//要删除的文件名称  
            try
            {
                if (Directory.Exists(dirRoot))
                {
                    string[] rootDirs = Directory.GetDirectories(dirRoot); //当前目录的子目录：  
                    string[] rootFiles = Directory.GetFiles(dirRoot);        //当前目录下的文件：  

                    foreach (string s2 in rootFiles)
                    {
                        File.Delete(s2);                      //删除文件                     
                    }
                    foreach (string s1 in rootDirs)
                    {
                        DeleteDirAllFile(s1);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("【{2}】删除文件夹里的文件出错,目录地址:{0},出错信息:{1}", dirRoot, ex.ToString(), LineName));
            }
        }
        private String UploadToServer(String address, byte[] testData, String ksign, String JSDWCode)
        {
            String result = "";
            try
            {
                //using (ChannelFactory<IDataTransport> channelFactory =
                //    new ChannelFactory<IDataTransport>("uploadEP", new EndpointAddress(address)))
                //{

                //    IDataTransport proxy = channelFactory.CreateChannel();
                //    using (proxy as IDisposable)
                //    {
                //        //logger.Error(string.Format("【{0}】调用远程服务开始，参数：testData:不打印,ksign:{1},JSDWCode:{2}", LineName, ksign, JSDWCode));
                //        result = proxy.ReciveTestData(testData, ksign, JSDWCode);
                //    }
                //}
                //logger.Error(string.Format("【{0}】调用远程服务开始，参数：testData:不打印,ksign:{1},JSDWCode:{2}", LineName, ksign, JSDWCode));
                string SiteName = DataBaseName.Replace("SYGLDB_", "");
                switch (SiteName)
                {
                    case "WLMQXinKeZhan":
                        WLMQXinKeZhanSR.DataTransportClient WLMQXinKeZhanErty = new TestDataUploadWS.WLMQXinKeZhanSR.DataTransportClient("WLMQXinKeZhanUploadEP");
                        WLMQXinKeZhanErty.Open();
                        result = WLMQXinKeZhanErty.ReciveTestData(testData, ksign, JSDWCode);
                        break;
                    default:
                        WLMQXinKeZhanSR.DataTransportClient erty = new TestDataUploadWS.WLMQXinKeZhanSR.DataTransportClient("WLMQXinKeZhanUploadEP", address);
                        erty.Open();
                        result = erty.ReciveTestData(testData, ksign, JSDWCode);
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("【{0}】调用远程服务错误：" + ex.ToString(), LineName));
            }

            return result;
        }
        /// <summary>
        /// 创建试验室基本信息xml
        /// </summary>
        /// <param name="set">上传设置</param>
        /// <param name="path">文件保存路径</param>
        /// <param name="doc">文档</param>
        /// <param name="trCode">试验室编码</param>
        /// <returns></returns>
        public bool CreatTestRoomInfoXMLFile(UploadSetting set, String path, JZDocument doc, String trCode)
        {
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml("<?xml version=\"1.0\" encoding=\"gb2312\"?><ROOT></ROOT>");
            XmlElement root = xmldoc.CreateElement(set.Name);
            xmldoc.DocumentElement.AppendChild(root);

            if (set.Items != null)
            {
                XmlElement ele = null;
                foreach (var item in set.Items)
                {
                    ele = xmldoc.CreateElement(item.Name);
                    String value = "";
                    if (item.NeedSetting)
                    {
                        Object obj = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                        if (obj != null)
                        {
                            value = obj.ToString();
                        }
                    }
                    else
                    {
                        switch (item.Name.ToUpper())
                        {
                            case "F_TRCODE"://试验室编号
                                value = trCode;
                                break;
                            default:
                                break;
                        }
                    }
                    ele.InnerText = value;
                    root.AppendChild(ele);
                }
            }
            try
            {
                xmldoc.Save(path);
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return false;
            }

        }
        /// <summary>
        /// 创建人员信息xml
        /// </summary>
        /// <param name="set"></param>
        /// <param name="path"></param>
        /// <param name="drArr"></param>
        /// <param name="trCode"></param>
        /// <returns></returns>
        public bool CreatPersonInfoXMLFile(UploadSetting set, String path, DataRow[] drArr, String trCode)
        {
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml("<?xml version=\"1.0\" encoding=\"gb2312\"?><ROOT></ROOT>");

            if (set.Items != null)
            {
                foreach (DataRow dr in drArr)
                {
                    JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dr["Data"].ToString());
                    String personID = "";
                    foreach (var item in doc.Sheets[0].Cells)
                    {
                        if (item.Name == "D10")//身份证
                        {
                            if (item.Value != null)
                            {
                                personID = item.Value.ToString();
                                break;
                            }
                        }
                    }
                    //logger.Error("personID:" + personID);
                    if (personID != "")
                    {
                        XmlElement root = xmldoc.CreateElement(set.Name);
                        xmldoc.DocumentElement.AppendChild(root);
                        XmlElement ele = null;
                        foreach (var item in set.Items)
                        {
                            ele = xmldoc.CreateElement(item.Name);
                            String value = "";
                            //if (item.NeedSetting)
                            //{
                            #region 处理数据
                            switch (item.Name.ToUpper())
                            {
                                case "F_TRPSEX":
                                    #region 性别“1”男，“0”女
                                    Object objSex = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                    value = "1";
                                    if (objSex != null)
                                    {
                                        switch (objSex.ToString())
                                        {
                                            case "男":
                                                value = "1";
                                                break;
                                            case "女":
                                                value = "0";
                                                break;
                                            default:
                                                value = "1";
                                                break;
                                        }
                                    }
                                    #endregion
                                    break;
                                case "F_JSZC":
                                    #region 技术职称“0”助理工程师，“1”工程师，“2”高级工程师，“3”教授级高工，“4”其它
                                    Object objJSZC = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                    value = "4";
                                    if (objJSZC != null)
                                    {
                                        switch (objJSZC.ToString())
                                        {
                                            case "助理工程师":
                                                value = "0";
                                                break;
                                            case "工程师":
                                                value = "1";
                                                break;
                                            case "高级工程师":
                                                value = "2";
                                                break;
                                            case "教授级高工":
                                                value = "3";
                                                break;
                                            default://其它
                                                value = "4";
                                                break;
                                        }
                                    }
                                    #endregion
                                    break;
                                case "F_ZW":
                                    #region 岗位/职务“0”试验室主任，“1”技术负责人，“2”试验员，“3”其它
                                    Object objZW = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                    value = "3";
                                    if (objZW != null)
                                    {
                                        switch (objZW.ToString())
                                        {
                                            case "试验室主任":
                                                value = "0";
                                                break;
                                            case "技术负责人":
                                                value = "1";
                                                break;
                                            case "试验员":
                                                value = "2";
                                                break;
                                            default://其它
                                                value = "3";
                                                break;
                                        }
                                    }
                                    #endregion
                                    break;
                                case "F_PHOTO":
                                    #region 身份证图片
                                    Object objPHOTO = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                    if (objPHOTO != null)
                                    {
                                        //string strPhoto = objPHOTO.ToString() + ".jpg";
                                        String PhotoFile = Path.Combine(GetServerDirectory(DataBaseName), "Temp/TestDataUpload/试验室人员/" + objPHOTO.ToString() + ".jpg");
                                        if (File.Exists(PhotoFile))
                                        {
                                            value = objPHOTO.ToString() + ".jpg";
                                        }
                                    }
                                    #endregion
                                    break;
                                case "F_TRCODE"://试验室编号
                                    value = trCode;
                                    break;
                                case "F_WORKDATE"://工作时间
                                    Object objWORKDATE = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                    if (objWORKDATE != null)
                                    {
                                        DateTime dtWORKDATE;
                                        bool bWORKDATE = DateTime.TryParse(objWORKDATE.ToString(), out dtWORKDATE);
                                        value = dtWORKDATE.ToString("yyyy-MM-dd");
                                        if (!bWORKDATE)
                                        {
                                            logger.Error(string.Format("CreatReportInfoXMLFile ID:{0} F_WORKDATE:{1}", doc.ID, objWORKDATE));
                                        }
                                    }
                                    break;
                                default:
                                    Object obj = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                    if (obj != null)
                                    {
                                        value = obj.ToString();
                                    }
                                    break;
                            }
                            #endregion
                            //}
                            //else
                            //{
                            //    switch (item.Name)
                            //    {
                            //    }
                            //}
                            ele.InnerText = value;
                            root.AppendChild(ele);
                        }
                    }
                }
            }
            try
            {
                xmldoc.Save(path);
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return false;
            }
        }
        /// <summary>
        /// 创建报告信息xml
        /// </summary>
        /// <param name="setArr"></param>
        /// <param name="path"></param>
        /// <param name="drArr"></param>
        /// <param name="trCode"></param>
        /// <param name="wucArr"></param>
        /// <returns></returns>
        public bool CreatReportInfoXMLFile(UploadSetting[] arrSet, String path, DataRow[] arrDocRow, string[] arrTRCODE, int[] arrWUC, int[] arrSFHG, string[] arrBHGX, int[] arrDevType, int[] arrBGZT, string[] arrRTCODE)
        {

            //int[] arrSFHG = new int[dt.Rows.Count];//是否合格
            //string[] arrBHGX = new string[dt.Rows.Count];//不合格项目
            //int[] arrDevType = new int[dt.Rows.Count];//涉及的试验仪器
            //int[] arrBGZT = new int[dt.Rows.Count];//  报告类型

            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml("<?xml version=\"1.0\" encoding=\"gb2312\"?><ROOT></ROOT>");
            //logger.Error("CreatReportInfoXMLFile 0");
            for (int i = 0; i < arrDocRow.Length; i++)
            {
                //logger.Error("CreatReportInfoXMLFile drArr i" + i);
                if (arrSet[i] != null && arrSet[i].Items != null)
                {
                    //logger.Error("CreatReportInfoXMLFile setArr i" + i);
                    XmlElement root = xmldoc.CreateElement(arrSet[i].Name);
                    xmldoc.DocumentElement.AppendChild(root);
                    JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(arrDocRow[i]["Data"].ToString());
                    XmlElement ele = null;
                    foreach (var item in arrSet[i].Items)
                    {
                        ele = xmldoc.CreateElement(item.Name);
                        String value = "";
                        //if (item.NeedSetting)
                        //{
                        switch (item.Name.ToUpper())
                        {
                            case "F_BGRQ"://报告日期[格式为yyyy-mm-dd]
                                Object objBGRQ = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                if (objBGRQ != null)
                                {
                                    DateTime dtBQRQ;
                                    bool bBGRQ = DateTime.TryParse(objBGRQ.ToString(), out dtBQRQ);
                                    value = dtBQRQ.ToString("yyyy-MM-dd");
                                    if (!bBGRQ)
                                    {
                                        logger.Error(string.Format("CreatReportInfoXMLFile ID:{0} F_BGRQ:{1}", doc.ID, objBGRQ));
                                    }
                                }
                                break;
                            case "F_TRCODE"://试验室编码[按照统一编码]
                                value = arrTRCODE[i];
                                break;
                            case "F_RTCODE"://报告类型[按照统一编码]
                                value = arrRTCODE[i];
                                break;
                            case "F_NEWVERSION"://数字版本号
                                value = arrWUC[i].ToString();
                                break;
                            case "F_SOFTCOM"://厂商名称
                                value = ConfigurationManager.AppSettings["F_SOFTCOM"];
                                break;
                            case "F_SFHG"://是否合格
                                value = arrSFHG[i].ToString();
                                break;
                            case "F_BGZT"://报告类型
                                value = arrBGZT[i].ToString();
                                break;
                            case "F_BHGX"://不合格项目
                                value = arrBHGX[i];
                                break;
                            case "F_DEVTYPE"://涉及的试验仪器
                                value = arrDevType[i].ToString();
                                break;
                            default:
                                Object obj = JZCommonHelper.GetCellValue(doc, item.SheetID, item.CellName);
                                if (obj != null)
                                {
                                    value = obj.ToString();
                                }
                                break;
                        }
                        //}
                        //else
                        //{
                        #region
                        //switch (item.Name.ToUpper())
                        //{
                        //    case "F_TRCODE"://试验室编码[按照统一编码]
                        //        value = trCode;
                        //        break;
                        //    case "F_RTCODE"://报告类型[按照统一编码]
                        //        value = trCode;
                        //        break;
                        //    case "F_NEWVERSION"://数字版本号
                        //        value = arrWUC[i].ToString();
                        //        break;
                        //    case "F_SOFTCOM"://厂商名称
                        //        value = ConfigurationManager.AppSettings["F_SOFTCOM"];
                        //        break;
                        //    case "F_SFHG"://是否合格
                        //        value = arrSFHG[i].ToString();
                        //        break;
                        //    case "F_BGZT"://报告类型
                        //        value = arrBGZT[i].ToString();
                        //        break;
                        //    case "F_BHGX"://不合格项目
                        //        value = arrBHGX[i];
                        //        break;
                        //    case "F_DEVTYPE"://涉及的试验仪器
                        //        value = arrDevType[i].ToString();
                        //        break;
                        //    default:
                        //        break;
                        //}
                        #endregion
                        //}
                        ele.InnerText = value;
                        root.AppendChild(ele);
                    }
                }
            }
            try
            {
                xmldoc.Save(path);
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return false;
            }
        }
        private void InitTestRoomCodeMap(String map)
        {
            //Hashtable testRoomCodeMap = new Hashtable();
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
            //return testRoomCodeMap;
        }
        //private void InitModuleCodeMap(String map)
        //{
        //    if (!String.IsNullOrEmpty(map))
        //    {
        //        String[] item = map.Split(new Char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        //        if (item.Length > 0)
        //        {
        //            foreach (var sub in item)
        //            {
        //                String[] subItem = sub.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //                if (subItem.Length == 2)
        //                {
        //                    ModuleCodeMap.Add(subItem[0], subItem[1]);
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion
        #region 通用方法

        //[System.Diagnostics.Conditional("DEBUG")]
        private void ExportToFile(string zipFile, out MemoryStream zipFileStream)
        {
            //int length = (int)fs.Length;
            //byte[] data = new byte[length];
            //fs.Position = 0;
            //fs.Read(data, 0, length);
            //MemoryStream ms = new MemoryStream(data);

            FileStream fs = new FileStream(zipFile, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];// zipFileStream.ToArray();
            fs.Position = 0;
            fs.Write(bytes, 0, bytes.Length);
            zipFileStream = new MemoryStream(bytes);
            fs.Close();
        }
        public object CallRemoteServerMethod(String address, string AssemblyName, string MethodName, object[] Parameters)
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
        #endregion
    }
}
