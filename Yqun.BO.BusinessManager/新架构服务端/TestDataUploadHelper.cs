using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LABTRANSINTERFACE;
using BizCommon;
using System.Data;
using System.Xml;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ServiceModel;
using FarPoint.Win.Spread;
using FarPoint.Win;
using System.Threading;
using System.DirectoryServices;

namespace Yqun.BO.BusinessManager
{
    public class TestDataUploadHelper : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///上传试验室综合情况
        /// </summary>
        public void UploadTestRoomInfo(string strTestRoomCodeMap, String uploadAddress, String jsdwCode)
        {
            try
            {

                Hashtable testRoomCodeMap = InitTestRoomCodeMap(strTestRoomCodeMap);
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
                        String path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), "Temp/TestDataUpload/试验室基本属性");
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
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            String testRoomCode = dt.Rows[i]["TestRoomCode"].ToString();
                            if (testRoomCodeMap.ContainsKey(testRoomCode))
                            {
                                testRoomCode = testRoomCodeMap[testRoomCode].ToString();
                            }

                            JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dt.Rows[i]["Data"].ToString());
                            String fileName = Path.Combine(path, testRoomCode + ".xml");
                            if (CreatXMLFile(set, fileName, doc, testRoomCode))
                            {
                                files.Add(fileName);
                            }
                            doc = null;
                            Thread.Sleep(3000);
                        }
                        if (files.Count > 0)
                        {
                            String zipFile = Path.Combine(path, "试验室基本属性.zip");
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
                                String result = UploadToServer(uploadAddress, buffer, ksign, jsdwCode);
                                if (result == "1")
                                {
                                    logger.Error("试验室综合情况数据上传成功");
                                }
                                else
                                {
                                    logger.Error("试验室综合情况数据上传失败，返回值为：" + result);
                                }
                            }
                        }
                    }
                    else
                    {
                        logger.Error("未找到试验室综合情况信息");
                    }
                }
                else
                {
                    logger.Error("未找到该模板对应的上传设置");
                }
            }
            catch (Exception ex)
            {
                logger.Error("UploadTestRoomInfo error:" + ex.ToString());
            }
        }

        /// <summary>
        /// 上传人员信息
        /// </summary>
        public void UploadPersonInfo(string strTestRoomCodeMap, String uploadAddress, String jsdwCode)
        {
            try
            {
                Hashtable testRoomCodeMap = InitTestRoomCodeMap(strTestRoomCodeMap);
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
                        String path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), "Temp/TestDataUpload/试验室人员");
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
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            String testRoomCode = dt.Rows[i]["TestRoomCode"].ToString();
                            DataRow[] drArr = dt.Select(string.Format("TestRoomCode='{0}'", testRoomCode));
                            if (testRoomCodeMap.ContainsKey(testRoomCode))
                            {
                                testRoomCode = testRoomCodeMap[testRoomCode].ToString();
                            }

                            String fileName = Path.Combine(path, testRoomCode + ".xml");
                            if (CreatMultiXMLFile(set, fileName, drArr, testRoomCode))
                            {
                                files.Add(fileName);
                                ProcessPersonPhoto(drArr, files, path);
                                foreach (DataRow row in drArr)
                                {
                                    dt.Rows.Remove(row);
                                }
                            }
                            drArr = null;
                            Thread.Sleep(3000);
                        }
                        if (files.Count > 0)
                        {
                            String zipFile = Path.Combine(path, "试验室人员.zip");
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
                                String result = UploadToServer(uploadAddress, buffer, ksign, jsdwCode);
                                if (result == "1")
                                {
                                    logger.Error("试验室人员上传成功");
                                }
                                else
                                {
                                    logger.Error("试验室人员上传失败，返回值为：" + result);
                                }
                            }
                        }
                    }
                    else
                    {
                        logger.Error("未找到试验人员信息");
                    }
                }
                else
                {
                    logger.Error("未找到该模板对应的上传设置");
                }
            }
            catch (Exception ex)
            {
                logger.Error("UploadPersonInfo error:" + ex.ToString());
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
        public void UploadTestDataInfo(string strTestRoomCodeMap, String uploadAddress, String jsdwCode)
        {
            try
            {
                Hashtable testRoomCodeMap = InitTestRoomCodeMap(strTestRoomCodeMap);
//                String sql = @"SELECT top 100 ID,TestRoomCode,Data,WillUploadCount,ModuleID,BGBH,CreatedTime FROM dbo.sys_document WHERE 
//            ModuleID NOT IN ('E77624E9-5654-4185-9A29-8229AAFDD68B','08899BA2-CC88-403E-9182-3EF73F5FB0CE',
//            'BA23C25D-7C79-4CB3-A0DC-ACFA6C285295') AND Status>0 AND NeedUpload=1 and BGBH is not null and BGBH<>''  ORDER BY CreatedTime ";//排除人员、设备、试验室
                String sql = @"SELECT top 100 ID FROM dbo.sys_document WHERE 
            ModuleID NOT IN ('E77624E9-5654-4185-9A29-8229AAFDD68B','08899BA2-CC88-403E-9182-3EF73F5FB0CE',
            'BA23C25D-7C79-4CB3-A0DC-ACFA6C285295') AND Status>0 AND NeedUpload=1 and BGBH is not null and BGBH<>''  ORDER BY CreatedTime ";//排除人员、设备、试验室
                DataTable dtIDs = GetDataTable(sql);
                ModuleHelper mh = new ModuleHelper();
                DocumentHelper dh = new DocumentHelper();
                if (dtIDs != null && dtIDs.Rows.Count > 0)
                {
                    String ksign = "05";//05表示试验数据
                    List<String> files = new List<String>();
                    #region 创建目录
                    String path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), "Temp/TestDataUpload/试验数据");
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
                    #region 回收应用程序池
                    //string strRealPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
                    //logger.Error("strRealPath:" + strRealPath);
                    //strRealPath = strRealPath.Substring(0, strRealPath.Length - 1);
                    //int LastIndex = strRealPath.LastIndexOf('\\');
                    //string AppPoolName = strRealPath.Substring(LastIndex + 1, strRealPath.Length - LastIndex - 1);
                    //logger.Error("AppPoolName:" + AppPoolName);
                    //string method = "Recycle";
                    //try
                    //{
                    //    DirectoryEntry appPool = new DirectoryEntry("IIS://localhost/W3SVC/AppPools");
                    //    DirectoryEntry findPool = appPool.Children.Find(AppPoolName, "IIsApplicationPool");
                    //    findPool.Invoke(method, null);
                    //    appPool.CommitChanges();
                    //    appPool.Close();
                    //    logger.Error("应用程序池名称回收成功");
                    //}
                    //catch (Exception ex)
                    //{
                    //    logger.Error("回收失败:" + ex.Message);
                    //}
                    #endregion
                    StringBuilder strIDs = new StringBuilder();
                    UploadSetting[] usArr = new UploadSetting[dtIDs.Rows.Count];
                    DataRow[] docRowArr = new DataRow[dtIDs.Rows.Count];
                    int[] wucArr = new int[dtIDs.Rows.Count];//WillUploadCount
                    #region 创建资料列表
                    for (int i = 0; i < dtIDs.Rows.Count; i++)
                    {
                        string strID = dtIDs.Rows[i]["ID"].ToString();
                        string strSQL = "SELECT ID,TestRoomCode,Data,WillUploadCount,ModuleID,BGBH,CreatedTime FROM dbo.sys_document where ID='" + strID + "'";
                        DataTable dt = GetDataTable(strSQL);
                        String testRoomCode = dt.Rows[0]["TestRoomCode"].ToString();
                        string strModuleID = dt.Rows[0]["ModuleID"].ToString();
                        string strBGBH = dt.Rows[0]["BGBH"].ToString();
                        string docDateDir = DateTime.Parse(dt.Rows[0]["CreatedTime"].ToString()).ToString("yyyy/MM/dd");
                        int WillUploadCount = int.Parse(dt.Rows[0]["WillUploadCount"].ToString());
                        strIDs.AppendFormat("'{0}'", strID);
                        if (i < dt.Rows.Count - 1)
                        {
                            strIDs.Append(",");
                        }
                        Sys_Module module = mh.GetModuleBaseInfoByID(new Guid(strModuleID));
                        UploadSetting set = module.UploadSetting;
                        if (set != null)
                        {
                            if (testRoomCodeMap.ContainsKey(testRoomCode))
                            {
                                testRoomCode = testRoomCodeMap[testRoomCode].ToString();
                            }
                            string strSourceFile = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), string.Format("source/{0}/{1}.pdf", docDateDir, strID));
                            string strDestFile = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), string.Format("Temp/TestDataUpload/试验数据/{0}_{1}.pdf", testRoomCode, strBGBH));
                            if (!File.Exists(strSourceFile))
                            {
                                //logger.Error(string.Format("{1} ID:{0}不存在，生成文件开始", strID, i));
                                #region 生成PDF和图片
                                JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dt.Rows[0]["Data"].ToString());
                                FpSpread fpSpread = new FpSpread();
                                //int rIndex = 0;
                                //foreach (JZSheet sheet in doc.Sheets)
                                //{
                                    //if (rIndex == module.ReportIndex)
                                    //{
                                String sheetXML = JZCommonHelper.GZipDecompressString(mh.GetSheetXMLByID(doc.Sheets[module.ReportIndex].ID));// mh.GetSheetXMLByID(sheet.ID);
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
                                    //}
                                    //else
                                    //{
                                    //    //fpSpread.Sheets.Add(null);
                                    //}
                                    //sheetXML = null;
                                    //SheetView = null;
                                //    rIndex++;
                                //}

                                //自动生成Excel，图片和报告页的pdf
                                SourceHelper sourceHelper = new SourceHelper();
                                sourceHelper.CreateRalationFilesSync(fpSpread, doc.ID, module.ReportIndex, docDateDir);
                                fpSpread.Dispose();
                                doc = null;
                                #endregion
                                logger.Error(string.Format("{1} ID:{0}不存在，生成文件成功", strID, i));
                                //Thread.Sleep(10000);
                                
                            }
                            else
                            {
                                logger.Error(string.Format("{1} ID:{0}存在，不需要生成文件", strID, i));
                            }
                            File.Copy(strSourceFile, strDestFile, true);
                            files.Add(strDestFile);

                            usArr[i] = set;
                            docRowArr[i] = dt.Rows[0];
                            wucArr[i] = WillUploadCount;
                            //JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dt.Rows[i]["Data"].ToString());
                            //String fileName = Path.Combine(path, dt.Rows[i]["ID"].ToString() + ".xml");
                            //if (CreatXMLFile(set, fileName, doc, testRoomCode))
                            //{
                            //    files.Add(fileName);
                            //}
                        }
                        else
                        {
                            usArr[i] = null;
                            docRowArr[i] = dt.Rows[0];
                            wucArr[i] = WillUploadCount;
                            logger.Error("未找到该模板对应的上传设置,资料ID:" + strID);
                        }
                    }
                    #endregion
                    //logger.Error("reportInfo 0");
                    String fileName = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~"), "Temp/TestDataUpload/试验数据/reportInfo.xml");
                    //生成reportInfo.xml
                    CreatReportInfoXMLFile(usArr, fileName, docRowArr, jsdwCode, wucArr);
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
                            String result = UploadToServer(uploadAddress, buffer, ksign, jsdwCode);
                            if (result == "1")
                            {
                                logger.Error("试验报告上传成功");
                                if (strIDs.Length > 0)
                                {
                                    dh.UpdateDocumentNeedUpload(strIDs.ToString());
                                }
                            }
                            else
                            {
                                logger.Error("试验报告上传失败，返回值为：" + result);
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    logger.Info("未找到试验数据信息");
                }
            }
            catch (Exception ex)
            {
                logger.Error("UploadTestDataInfo error:" + ex.ToString());
            }
        }

        public bool CreatXMLFile(UploadSetting set, String path, JZDocument doc, String trCode)
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
                        switch (item.Name)
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

        public bool CreatMultiXMLFile(UploadSetting set, String path, DataRow[] drArr, String trCode)
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
                    XmlElement root = xmldoc.CreateElement(set.Name);
                    xmldoc.DocumentElement.AppendChild(root);
                    JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dr["Data"].ToString());
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
                            switch (item.Name)
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

        public bool CreatReportInfoXMLFile(UploadSetting[] setArr, String path, DataRow[] drArr, String trCode, int[] wucArr)
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
            //logger.Error("CreatReportInfoXMLFile 0");
            for (int i = 0; i < drArr.Length; i++)
            {
                //logger.Error("CreatReportInfoXMLFile drArr i" + i);
                if (setArr[i] != null && setArr[i].Items != null)
                {
                    //logger.Error("CreatReportInfoXMLFile setArr i" + i);
                    XmlElement root = xmldoc.CreateElement(setArr[i].Name);
                    xmldoc.DocumentElement.AppendChild(root);
                    JZDocument doc = Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(drArr[i]["Data"].ToString());
                    XmlElement ele = null;
                    foreach (var item in setArr[i].Items)
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
                            switch (item.Name)
                            {
                                case "F_TRCODE"://试验室编号
                                    value = trCode;
                                    break;
                                case "F_NEWVERSION"://试验室编号
                                    value = wucArr[i].ToString();
                                    break;
                                default:
                                    break;
                            }
                        }
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

        private String UploadToServer(String address, byte[] testData, String ksign, String JSDWCode)
        {
            String result = "";
            logger.Error(string.Format("运行到UploadToServer"));
            return "1";
            try
            {
                using (ChannelFactory<IDataTransport> channelFactory =
                    new ChannelFactory<IDataTransport>("uploadEP", new EndpointAddress(address)))
                {

                    IDataTransport proxy = channelFactory.CreateChannel();
                    using (proxy as IDisposable)
                    {
                        result = proxy.ReciveTestData(testData, ksign, JSDWCode);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("调用远程服务错误：" + ex.Message);
            }
            return result;
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
                logger.Error(string.Format("删除文件夹里的文件出错,目录地址:{0},出错信息:{1}", dirRoot, ex.ToString()));
            }
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
    }
    //public class DocUploadModel
    //{
    //    public UploadSetting UploadSetting { get; set; }
    //    public UploadSetting UploadSetting { get; set; }
    //}
}
