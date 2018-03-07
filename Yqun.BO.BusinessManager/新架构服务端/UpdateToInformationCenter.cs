using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Data;
using BizCommon;

namespace Yqun.BO.BusinessManager
{
    /// <summary>
    /// 数据是航船铁道部
    /// </summary>
    public class UpdateToInformationCenter : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 提交铁道部参数字符串
        /// </summary>
        StringBuilder _StringBuilder = new StringBuilder();

        /// <summary>
        /// 获取龄期帮助类
        /// </summary>
        StadiumHelper _StadiumHelper = new StadiumHelper();

        /// <summary>
        /// 实时数据保存
        /// </summary>
        StringBuilder _StringBuilderRealTimeData = new StringBuilder();

        /// <summary>
        /// 将要Post给铁道部的数据进行打包
        /// </summary>
        /// <param name="posturl">上传URL</param>
        /// <param name="documentID">资料ID</param>
        /// <param name="moduleID">模板ID</param>
        /// <param name="stadiumID">龄期ID</param>
        /// <param name="wtbh">委托编号</param>
        /// <param name="testRoomCode">实验室编码</param>
        /// <param name="seriaNumber">当前试验组数或根数</param>
        /// <param name="userName">用户名称</param>
        /// <param name="testData">试验数据</param>
        /// <param name="realTimeData">实时数据</param>
        /// <param name="totalNumber">总组数或根数</param>
        /// <returns></returns>
        public string PostDataToTDB(JZDocument doc, Guid moduleID, Guid stadiumID, String wtbh, String testRoomCode,
                Int32 seriaNumber, String userName, List<JZTestCell> cells, String realTimeData, string machineBH,string UpdloadInfo,string UploadCode)
        {


            string postUrl = string.Empty;
            string json = string.Empty;
            string QMjson = string.Empty;
            try
            {

                logger.Error("开始执行：PostData方法");

                //将具体的试验数据生成json字符串并进行64位加密
                string tempRealTimeData = GetYSLZ(realTimeData);
                if (_StringBuilder.Length > 0)
                {
                    _StringBuilder.Remove(0, _StringBuilder.Length);
                }

                logger.Error("开始拼接字符串");

                _StringBuilder.Append("MK=2&");
                string tempMachineType = GetModuleTypeByID(moduleID);
                if (tempMachineType != string.Empty)
                {

                    logger.Error("开始拼接机器类型和相关的数据JOSN");
                    if (tempMachineType == "1")
                    {
                        postUrl = System.Configuration.ConfigurationManager.AppSettings["YLJURL"];
                        _StringBuilder.Append("Gn=1&");
                    }
                    else
                    {
                        postUrl = System.Configuration.ConfigurationManager.AppSettings["WNJURL"];
                        _StringBuilder.Append("Gn=2&");
                    }
                }
                else
                {
                    return string.Empty;
                }
                _StringBuilder.Append("Bbh=1&");
                _StringBuilder.Append("kfsbh=北京金舟科技发展有限公司&");

                //设备编号
                _StringBuilder.Append("sbh=&");

                _StringBuilder.Append("fssj=" + DateTime.Now.ToString() + "&");
                _StringBuilder.Append("zwcd=" + json.Length + "&");
                _StringBuilder.Append("jmfs=1&");
                if (UploadCode == "0")
                {
                    _StringBuilder.Append("sfqm=1&"); }
                else
                {
                    _StringBuilder.Append("sfqm=0&");
                }
                _StringBuilder.Append("ylcs=2&");
                _StringBuilder.Append("sjjy=123&");
                _StringBuilder.Append("sjqm=&");
                _StringBuilder.Append("jmzw=" + UpdloadInfo);
                _StringBuilder.Append("&zsxh=");
                _StringBuilder.Append("&cfbs=0");
                _StringBuilder.Append("&kzbh=" + System.Configuration.ConfigurationManager.AppSettings["kzbh"]);

                logger.Error("字符串拼接完成");
                return UpdateToInfoCenter(postUrl);
            }
            catch (Exception ex)
            {
                logger.Error("拼接字符串错误:" + ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        ///  数据上传铁道部
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private string UpdateToInfoCenter(string postUrl)
        {
            Encoding encoding = System.Text.Encoding.Default;
            try
            {
                Stream outstream = null;
                Stream instream = null;
                StreamReader sr = null;
                HttpWebResponse response = null;
                HttpWebRequest request = null;

                logger.Error("URL：" + postUrl);
                logger.Error("参数：" + _StringBuilder.ToString());
                return string.Empty;

                byte[] data = encoding.GetBytes(_StringBuilder.ToString());

                request = WebRequest.Create(postUrl) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                //writeLog(content);
                return content;
            }
            catch (WebException ex)
            {
                logger.Error("上传铁道部数据失败：" + ex.ToString());
                HttpWebResponse res = (HttpWebResponse)ex.Response;
                StreamReader srerror = new StreamReader(res.GetResponseStream(), encoding);
                string strHtml = srerror.ReadToEnd();
                return strHtml;
            }
        }

        /// <summary>
        /// 根据模板ID获取实验类型
        /// </summary>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        private string GetModuleTypeByID(Guid moduleID)
        {
            try
            {
                DataTable dt = GetDataTable("select DeviceType from sys_module where ID='" + moduleID.ToString() + "'");
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取实验的实时力值和试验时间
        /// </summary>
        /// <param name="realTimeData">实验的实时力值JOSN</param>
        /// <returns>试验时间|实时力值</returns>
        public string GetYSLZ(string realTimeData)
        {
            try
            {
                if (_StringBuilderRealTimeData.Length > 0)
                {
                    _StringBuilderRealTimeData.Remove(0, _StringBuilderRealTimeData.Length);
                }
                List<JZRealTimeData> _JZRealTimeData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZRealTimeData>>(realTimeData);
                for (int i = 0; i < _JZRealTimeData.Count; i++)
                {
                    _StringBuilderRealTimeData.Append(_JZRealTimeData[i].Value);
                    if (i < _JZRealTimeData.Count - 1)
                    {
                        _StringBuilderRealTimeData.Append(",");
                    }
                }
                return _JZRealTimeData[_JZRealTimeData.Count - 1].Time.ToString() + "|" + _StringBuilderRealTimeData.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取铁道部模板Code
        /// </summary>
        /// <param name="ModuleID">模板ID</param>
        /// <returns>铁道部信息中心模板对应ID</returns>
        public string GetModuleInfoRTCode(Guid ModuleID)
        {
            try
            {
                DataTable dt = GetDataTable("select ModuleALT from sys_module where ID='" + ModuleID.ToString() + "'");
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return string.Empty;
        }

        /// <summary>
        /// 将具体的试验数据生成json字符串并进行64位加密
        /// </summary>
        /// <param name="NotBase64String"></param>
        /// <returns></returns>
        public string ToBase64(string NotBase64String)
        {
            //添加代码
            byte[] binaryData = System.Text.Encoding.UTF8.GetBytes(NotBase64String);
            string tobase = System.Convert.ToBase64String(binaryData);
            return tobase;
        }
    }
}
