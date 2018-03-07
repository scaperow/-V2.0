using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using Yqun.Common.ContextCache;
using Yqun.Common.Encoder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Yqun.BO.BusinessManager;
using BizCommon;
using TransferServiceCommon;
using System.IO;
using System.ServiceModel;

namespace WebTransferService
{
    /// <summary>
    /// MobileService 的摘要说明
    /// </summary>
    [WebService(Namespace = "WebTransferService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class MobileService : System.Web.Services.WebService
    {

        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region 拌合站

        [WebMethod(Description = "查询最新超标数量;参数：LineID(线路ID),LastDate(最后获取时间);ResultFlag：-2线路不存在,-1授权失败,0异常错误,1查询最新超标数量")]
        public string GetSuperscalarCount(string LineID, string LastDate, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    if (line != null)
                    {
                        //string strWhere = " ChaoBiaoDengJi>0  and ChaoBiaoDengJi<4  ";
                        //DateTime dtLastDate;
                        //bool bLastDate = DateTime.TryParse(LastDate, out dtLastDate);
                        ////bool bEnd = DateTime.TryParse(EndDate, out dtEnd);
                        //if (bLastDate)
                        //{
                        //    strWhere += " AND ChuLiaoShiJian>'" + dtLastDate.ToString("yyyy-MM-dd") + "' ";
                        //}
                        //else
                        //{
                        //    strWhere += "";
                        //}

                        UserHelper uh = new UserHelper();
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";

                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetSuperscalarCount",
                            new Object[] { LastDate });

                        string strCount = obj == null ? "0" : obj.ToString();
                        strResult = "{ResultFlag:'1',ResultData:'" + strCount + "'}";
                    }
                    else
                    {
                        logger.Error(string.Format("GetSuperscalarCount:{0}.", "线路不存在"));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                //logger.Error(string.Format("GetSuperscalarCount异常：{0}.", ex.Message));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.ToString());//登录异常消息
            }
            return strResult;
        }


        [WebMethod(Description = "产能分析;参数：LineID(线路ID),MachineCodes(机器编码,多个以英文的,分隔),StartDate(开始时间),EndDate(结束时间),PageSize(页大小),PageIndex(页序),FType(查询类型:周1，月2，季度3),ResultFlag：-2线路不存在,-1授权失败,0异常错误,1产能分析结果")]
        public string CapacityAnalysis(string LineID, string MachineCodes, string StartDate, int FType, string EndDate, int PageSize, int PageIndex, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    DataSet ds;
                    if (line != null)
                    {
                        DateTime dtStart, dtEnd;
                        bool bStart = DateTime.TryParse(StartDate, out dtStart);
                        bool bEnd = DateTime.TryParse(EndDate, out dtEnd);

                        UserHelper uh = new UserHelper();
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";

                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "CapacityAnalysis",
                            new Object[] { MachineCodes, dtStart, FType, dtEnd, PageSize, PageIndex });

                        ds = obj as DataSet;

                        //object objCount = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "CapacityAnalysisCount",
                        //new Object[] { MachineCodes, dtStart, FType, dtEnd });
                        //int iCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());

                        strResult = "{ResultFlag:'1',ResultData:" + JsonConvert.SerializeObject(ds.Tables[0]) + "}";//,TotalCount:" + iCount + "
                    }
                    else
                    {
                        logger.Error(string.Format("CapacityAnalysis:{0}.", "线路不存在"));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("CapacityAnalysis异常：{0}.", ex.Message));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }

        [WebMethod(Description = "更新拌合站超标详细的处理结果;参数：LineID(线路ID),PanID(超标详细ID),ChuLiYiJian(处理结果);ResultFlag：-2线路不存在,-1授权失败,0异常错误,1操作成功")]
        public string UpdateBhzPanDetailChuLiYiJian(string LineID, string PanID, string ChuLiYiJian, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    if (line != null)
                    {
                        UserHelper uh = new UserHelper();
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";

                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "UpdateBhzPanDetailChuLiYiJian",
                            new Object[] { PanID, ChuLiYiJian });
                        logger.Error("UpdateBhzPanDetailChuLiYiJian obj:" + obj);
                        //dt = obj as DataTable;
                        strResult = "{ResultFlag:'1',ResultData:'" + obj.ToString() + "'}";
                    }
                    else
                    {
                        logger.Error(string.Format("UpdateBhzPanDetailChuLiYiJian:{0}.", "线路不存在"));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("UpdateBhzPanDetailChuLiYiJian异常：{0}.", ex.Message));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }

        [WebMethod(Description = "获取超标详细;参数：LineID(线路ID),PanID(超标详细ID);ResultFlag：-2线路不存在,-1授权失败,0异常错误,1获取超标详细")]
        public string GetBhzPanDetail(string LineID, string PanID, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    DataTable dt;
                    if (line != null)
                    {
                        UserHelper uh = new UserHelper();
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";

                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzPanDetail",
                            new Object[] { PanID });

                        dt = obj as DataTable;
                        strResult = "{ResultFlag:'1',ResultData:" + JsonConvert.SerializeObject(dt) + "}";
                    }
                    else
                    {
                        logger.Error(string.Format("GetBhzPanDetail:{0}.", "线路不存在"));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("GetBhzPanDetail异常：{0}.", ex.Message));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }


        [WebMethod(Description = "超标查询;参数：LineID(线路ID),UserName(用户名),PageSize(页大小),LastRowNum(最后的行号,最开始为0),StartDate(开始时间),EndDate(结束时间),ProjectName(工程名称),JiaoZhuBuWei(浇筑部位),ChaoBiaoDengJi(超标等级:低级1,中级2,高级3;也可以组合起来如:1,2),MachineCode(机器编码,以英文,分隔);ResultFlag：-2线路不存在,-1授权失败,0异常错误,1超标查询结果")]
        public string SuperscalarSearch(string LineID, int PageSize, int LastRowNum, string StartDate, string EndDate, string ProjectName, string JiaoZhuBuWei, string ChaoBiaoDengJi, string MachineCode, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    DataTable dt;
                    if (line != null)
                    {
                        string strWhere = " ChaoBiaoDengJi>0  and ChaoBiaoDengJi<4  ";
                        DateTime dtStart, dtEnd;
                        bool bStart = DateTime.TryParse(StartDate, out dtStart);
                        bool bEnd = DateTime.TryParse(EndDate, out dtEnd);
                        if (bStart)
                        {
                            strWhere += "AND ChuLiaoShiJian>='" + dtStart.ToString("yyyy-MM-dd") + "' ";
                        }
                        if (bEnd)
                        {
                            strWhere += "AND ChuLiaoShiJian<'" + dtEnd.AddDays(1).ToString("yyyy-MM-dd") + "'";
                        }
                        strWhere += " and ProjectName like '%" + ProjectName + "%'";
                        strWhere += " and JiaoZhuBuWei like '%" + JiaoZhuBuWei + "%'";
                        if (!String.IsNullOrEmpty(MachineCode))
                        {
                            strWhere += " and MachineCode IN  (" + MachineCode + ") ";
                        }
                        ChaoBiaoDengJi = ChaoBiaoDengJi.TrimStart(',').TrimEnd(',');

                        UserHelper uh = new UserHelper();
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";

                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "SuperscalarSearch",
                            new Object[] { PageSize, LastRowNum, strWhere });

                        dt = obj as DataTable;

                        //object objCount = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "SuperscalarSearchCount",
                        //    new Object[] { strWhere });
                        //int iCount = int.Parse(objCount.ToString());

                        strResult = "{ResultFlag:'1',ResultData:" + JsonConvert.SerializeObject(dt) + "}";//,TotalCount:"+iCount+"
                    }
                    else
                    {
                        logger.Error(string.Format("SuperscalarSearch:{0}.", "线路不存在"));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("SuperscalarSearch异常：{0}.", ex.Message));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }
        
        [WebMethod(Description = "线路概况;参数：LineID(线路ID),StartDate(开始时间),EndDate(结束时间),MachineCode(机器编码,以英文,分隔);ResultFlag：-2线路不存在,-1授权失败,0异常错误,1线路概况结果")]
        public string GetBhzLineStatics(string LineID, string StartDate, string EndDate, string MachineCode, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    DataTable dt;
                    if (line != null)
                    {
                        DateTime dtStart, dtEnd;
                        bool bStart = DateTime.TryParse(StartDate, out dtStart);
                        bool bEnd = DateTime.TryParse(EndDate, out dtEnd);
                        if (!bStart)
                        {
                            logger.Error("GetBhzLineStatics error StartDate:" + StartDate);
                        }
                        if (!bEnd)
                        {
                            logger.Error("GetBhzLineStatics error EndDate:" + EndDate);
                        }
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";
                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzLineStatics",
                            new Object[] { dtStart, dtEnd, MachineCode });

                        dt = obj as DataTable;


                        strResult = "{ResultFlag:'1',ResultData:" + JsonConvert.SerializeObject(dt) + "}";
                    }
                    else
                    {
                        logger.Error(string.Format("GetBhzLineStatics:{0}.", "线路不存在"));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("GetBhzLineStatics异常：{0}.", ex.Message));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }


        [WebMethod(Description = "获取拌合站动态汇总展示;参数：LineID(线路ID);UserName(用户名);TestCode(机器编码,多个以英文的,分隔);ResultFlag：-3没有获取到汇总展示,-2线路不存在,-1授权失败,0异常错误,1动态汇总")]
        public string GetBhzIng(string LineID, string UserName, string TestCode, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    DataTable dt;
                    if (line != null)
                    {
                        UserHelper uh = new UserHelper();
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";
                        //string TestCode = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzUserTestCode",
                        //    new Object[] { UserName }).ToString();

                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzIng",
                            new Object[] { UserName, TestCode });
                        if (obj == null)
                        { strResult = "{ResultFlag:'-3',ResultData:'没有获取到汇总展示'}"; }
                        else
                        {
                            dt = obj as DataTable;
                            strResult = "{ResultFlag:'1',ResultData:" + JsonConvert.SerializeObject(dt) + "}";
                        }
                    }
                    else
                    {
                        logger.Error(string.Format("GetBhzIng:{0}.", "线路不存在"));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("GetBhzIng异常：{0}.参数如下：UserName:{1},LineID:{2}", ex.Message, UserName, LineID));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }
        [WebMethod(Description = "获取监理单位列表;参数：LineID(线路ID);ResultFlag：-3没有获取到汇总展示,-2线路不存在,-1授权失败,0异常错误,1获取监理单位列表")]
        public string GetBhzJLCompanyList(string LineID, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    DataTable dt;
                    if (line != null)
                    {
                        UserHelper uh = new UserHelper();
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";
                        //string TestCode = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzUserTestCode",
                        //    new Object[] { UserName }).ToString();

                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzJLCompanyList",
                            new Object[] { });
                        if (obj == null)
                        { strResult = "{ResultFlag:'-3',ResultData:'没有获取到监理单位'}"; }
                        else
                        {
                            dt = obj as DataTable;
                            strResult = "{ResultFlag:'1',ResultData:" + JsonConvert.SerializeObject(dt) + "}";
                        }
                    }
                    else
                    {
                        logger.Error(string.Format("GetBhzIng:{0}.", "线路不存在"));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("GetBhzJLCompanyList异常：{0}.", ex.Message));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }
        [WebMethod(Description = "获取施工单位列表;参数：LineID(线路ID);JLID(监理ID);ResultFlag：-3没有获取到汇总展示,-2线路不存在,-1授权失败,0异常错误,1获取施工单位列表")]
        public string GetBhzSGCompanyList(string LineID, string JLID, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    DataTable dt;
                    if (line != null)
                    {
                        UserHelper uh = new UserHelper();
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";
                        //string TestCode = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzUserTestCode",
                        //    new Object[] { UserName }).ToString();

                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzSGCompanyList",
                            new Object[] { JLID });
                        if (obj == null)
                        { strResult = "{ResultFlag:'-3',ResultData:'没有获取到监理单位'}"; }
                        else
                        {
                            dt = obj as DataTable;
                            strResult = "{ResultFlag:'1',ResultData:" + JsonConvert.SerializeObject(dt) + "}";
                        }
                    }
                    else
                    {
                        logger.Error(string.Format("GetBhzIng:{0}.", "线路不存在"));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("GetBhzSGCompanyList异常：{0}.", ex.Message));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }
        [WebMethod(Description = "获取拌合站列表;参数：LineID(线路ID);CompanyID(施工单位ID);ResultFlag：-3没有获取到汇总展示,-2线路不存在,-1授权失败,0异常错误,1获取拌合站列表")]
        public string GetBhzStationList(string LineID, string CompanyID, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    DataTable dt;
                    if (line != null)
                    {
                        UserHelper uh = new UserHelper();
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";
                        //string TestCode = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzUserTestCode",
                        //    new Object[] { UserName }).ToString();

                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzStationList",
                            new Object[] { CompanyID });
                        if (obj == null)
                        { strResult = "{ResultFlag:'-3',ResultData:'没有获取到拌合站列表'}"; }
                        else
                        {
                            dt = obj as DataTable;
                            strResult = "{ResultFlag:'1',ResultData:" + JsonConvert.SerializeObject(dt) + "}";
                        }
                    }
                    else
                    {
                        logger.Error(string.Format("GetBhzStationList:{0}.", "线路不存在"));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("GetBhzStationList异常：{0}.", ex.Message));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }
        [WebMethod(Description = "获取拌合站机器列表;参数：LineID(线路ID);StationID(拌合站ID);ResultFlag：-3没有获取到汇总展示,-2线路不存在,-1授权失败,0异常错误,1获取拌合站列表")]
        public string GetBhzMachineList(string LineID, string StationID, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    DataTable dt;
                    if (line != null)
                    {
                        UserHelper uh = new UserHelper();
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";
                        //string TestCode = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzUserTestCode",
                        //    new Object[] { UserName }).ToString();

                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzMachineList",
                            new Object[] { StationID });
                        if (obj == null)
                        { strResult = "{ResultFlag:'-3',ResultData:'没有获取到拌合站列表'}"; }
                        else
                        {
                            dt = obj as DataTable;
                            strResult = "{ResultFlag:'1',ResultData:" + JsonConvert.SerializeObject(dt) + "}";
                        }
                    }
                    else
                    {
                        logger.Error(string.Format("GetBhzMachineList:{0}.", "线路不存在"));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("GetBhzMachineList异常：{0}.", ex.Message));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }
        [WebMethod(Description = "获取拌合站带机器全部名称的列表;参数：LineID(线路ID);JLID(监理ID);TestCode(用户所拥有的机器编码);ResultFlag：-3没有获取到汇总展示,-2线路不存在,-1授权失败,0异常错误,1获取拌合站列表")]
        public string GetBhzMachineListWithFullName(string LineID, string JLID, string TestCode, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    DataTable dt;
                    if (line != null)
                    {
                        UserHelper uh = new UserHelper();
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";
                        //string TestCode = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzUserTestCode",
                        //    new Object[] { UserName }).ToString();

                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzMachineListWithFullName",
                            new Object[] { JLID, TestCode });
                        if (obj == null)
                        { strResult = "{ResultFlag:'-3',ResultData:'没有获取到拌合站列表'}"; }
                        else
                        {
                            dt = obj as DataTable;
                            strResult = "{ResultFlag:'1',ResultData:" + JsonConvert.SerializeObject(dt) + "}";
                        }
                    }
                    else
                    {
                        logger.Error(string.Format("GetBhzMachineListWithFullName:{0}.", "线路不存在"));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("GetBhzMachineListWithFullName异常：{0}.", ex.Message));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }

        #endregion
        #region 用户

        [WebMethod(Description = "获取拌合站用户登录日志清单;参数：LineID(线路ID),UserName(用户名),PageSize(页大小),LastRowNum(最后的行号,最开始为0),StartDate(开始时间),EndDate(结束时间);ResultFlag：-2线路不存在,-1授权失败,0异常错误,1获取拌合站用户登录日志清单")]
        public string GetBhzUserLoginLogList(string LineID, string UserName, int PageSize, int LastRowNum, string StartDate, string EndDate, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    DataTable dt;
                    if (line != null)
                    {
                        string strWhere = string.Empty;
                        DateTime dtStart, dtEnd;
                        bool bStart = DateTime.TryParse(StartDate, out dtStart);
                        bool bEnd = DateTime.TryParse(EndDate, out dtEnd);
                        if (bStart)
                        {
                            strWhere += "AND LastDateTime>='" + dtStart.ToString("yyyy-MM-dd") + "' ";
                        }
                        if (bEnd)
                        {
                            strWhere += "AND LastDateTime<'" + dtEnd.AddDays(1).ToString("yyyy-MM-dd") + "'";
                        }

                        UserHelper uh = new UserHelper();
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";

                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzUserLoginLogList",
                            new Object[] { UserName, PageSize, LastRowNum, strWhere });

                        dt = obj as DataTable;

                        object objCount = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzUserLoginLogListCount",
                        new Object[] { UserName, 0, strWhere });
                        int iCount = int.Parse(objCount.ToString());

                        strResult = "{ResultFlag:'1',TotalCount:" + iCount + ",ResultData:" + JsonConvert.SerializeObject(dt) + "}";
                    }
                    else
                    {
                        logger.Error(string.Format("GetBhzUserInfo:{0}.", "线路不存在"));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("GetBhzUserInfo异常：{0}.", ex.Message));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }

        [WebMethod(Description = "获取拌合站用户登录日志统计;参数：LineID(线路ID),PageSize(页大小),LastRowNum(最后的行号,最开始为0);ResultFlag：-2线路不存在,-1授权失败,0异常错误,1获取拌合站用户登录日志")]
        public string GetBhzUserLoginLog(string LineID, int PageSize, int LastRowNum, string StartDate, string EndDate, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    DataTable dt;
                    if (line != null)
                    {
                        string strWhere = string.Empty;
                        DateTime dtStart, dtEnd;
                        bool bStart = DateTime.TryParse(StartDate, out dtStart);
                        bool bEnd = DateTime.TryParse(EndDate, out dtEnd);
                        if (bStart)
                        {
                            strWhere += "AND LastDateTime>='" + dtStart.ToString("yyyy-MM-dd") + "' ";
                        }
                        else
                        {
                            logger.Error("GetBhzUserLoginLog error StartDate:" + StartDate);
                        }
                        if (bEnd)
                        {
                            strWhere += "AND LastDateTime<'" + dtEnd.AddDays(1).ToString("yyyy-MM-dd") + "'";
                        }
                        else
                        {
                            logger.Error("GetBhzUserLoginLog error EndDate:" + EndDate);
                        }

                        UserHelper uh = new UserHelper();
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";

                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzUserLoginLog",
                            new Object[] { PageSize, LastRowNum, strWhere });

                        dt = obj as DataTable;
                        //object objCount = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzUserLoginLogCount",
                        //new Object[] {  0, strWhere });
                        //int iCount = int.Parse(objCount.ToString());

                        strResult = "{ResultFlag:'1',ResultData:" + JsonConvert.SerializeObject(dt) + "}";//,TotalCount:" + iCount + "
                    }
                    else
                    {
                        logger.Error(string.Format("GetBhzUserInfo:{0}.", "线路不存在"));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("GetBhzUserInfo异常：{0}.", ex.Message));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }


        [WebMethod(Description = "获取拌合站用户信息;参数：LineID(线路ID);UserName(用户名);ResultFlag：-2线路不存在,-1授权失败,0异常错误,1用户信息")]
        public string GetBhzUserInfo(string LineID, string UserName, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    DataTable dt;
                    if (line != null)
                    {
                        UserHelper uh = new UserHelper();
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";

                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "GetBhzUserInfo",
                            new Object[] { UserName });

                        dt = obj as DataTable;
                        strResult = "{ResultFlag:'1',ResultData:" + JsonConvert.SerializeObject(dt) + "}";
                    }
                    else
                    {
                        logger.Error(string.Format("GetBhzUserInfo:{0}.", "线路不存在"));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("GetBhzUserInfo异常：{0}.", ex.Message));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }
        [WebMethod(Description = "更新拌合站用户最后选择的线路;参数：UserName(用户名),TestCode(试验室编码),LineID(线路ID),Description(线路名字);ResultFlag：-2线路不存在,-1授权失败,0异常错误,1操作成功")]
        public string UpdateBhzUserLastLine(string UserName, string TestCode, string LineID, string Description, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    if (line != null)
                    {
                        bool bFlag = false;
                        Yqun.BO.BusinessManager.UserHelper uh = new Yqun.BO.BusinessManager.UserHelper();
                        bFlag = uh.UpdateBhzUserLastLine(UserName, LineID, Description);
                        strResult = "{ResultFlag:'1',ResultData:'" + bFlag + "'}";
                    }
                    else
                    {
                        logger.Error(string.Format("UpdateBhzUserLastLine:{0}.", "线路不存在", UserName, LineID, Description));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("UpdateBhzUserLastLine异常：{0}.", ex.Message));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }
        [WebMethod(Description = "添加拌合站登录日志;参数：UserName(用户名),TestCode(试验室编码),LineID(线路ID);ResultFlag：-3操作日志添加失败,-2线路不存在,-1授权失败,0异常错误,1操作成功")]
        public string AddBhzLoginLog(string LineID, string UserName, string TestCode, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    LineHelper lh = new LineHelper();
                    Sys_Line line = lh.GetLineByID(new Guid(LineID));
                    if (line != null)
                    {
                        String lineAddress = "net.tcp://" + line.LineIP + ":" + line.LinePort + "/TransferService.svc";
                        object obj = CallRemoteServerMethod(lineAddress, "Yqun.BO.BusinessManager.dll", "AddBhzLoginLog",
                            new Object[] { UserName, "", TestCode, "" });
                        bool bFlag = false;
                        bFlag = obj == null ? false : System.Convert.ToBoolean(obj);
                        if (bFlag == false)
                        {
                            strResult = "{ResultFlag:'-3',ResultData:'登录日志添加失败'}";
                        }
                        else
                        {
                            strResult = "{ResultFlag:'1',ResultData:'登录日志添加成功'}";
                        }

                    }
                    else
                    {
                        logger.Error(string.Format("AddBhzLoginLog:{0}.", "线路不存在"));
                        strResult = "{ResultFlag:'-2',ResultData:'线路不存在'}";
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("AddBhzLoginLog异常：{0}.", ex.Message));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }
        [WebMethod(Description = "获取试验室管理系统用户可以操作的线路信息;参数：UserName(用户名);ResultFlag：-1授权失败,0异常错误,1用户能访问的所有线路信息")]
        public string GetSysUserLineList(string UserName, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    Yqun.BO.BusinessManager.UserHelper uh = new Yqun.BO.BusinessManager.UserHelper();
                    string strUserLineList = JsonConvert.SerializeObject(uh.GetSysUserLineList(UserName));
                    strResult = "{ResultFlag:'1',ResultData:'" + strUserLineList + "'}";
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("GetSysUserLineList异常：{0}.参数如下：UserName:{1}", ex.Message, UserName));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }

        [WebMethod(Description = "登录试验室管理系统;ResultFlag：-1授权失败,-2用户名密码错误,-3没有访问任何线路的权限,0异常错误,1登录成功并返回用户最后查看线路ID,2没有默认线路返回用户能访问的所有线路信息")]
        public string LoginSys(string UserName, string Password, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    Yqun.BO.LoginBO lb = new Yqun.BO.LoginBO();
                    //if (!Yqun.Services.Agent.CheckUser(UserName, Password, true))
                    if (!lb.CheckUserSys(UserName, Password))
                    {
                        strResult = @"{ResultFlag:'-2',ResultData:'用户名密码错误'}";//登录失败
                    }
                    else
                    {
                        Yqun.BO.BusinessManager.UserHelper uh = new Yqun.BO.BusinessManager.UserHelper();
                        string strLastLineID = uh.GetSysUserLastLineID(UserName);
                        if (!string.IsNullOrEmpty(strLastLineID))
                        {
                            strResult = @"{ResultFlag:'1',ResultData:'" + strLastLineID + "'}";//返回上次线路code
                        }
                        else
                        {
                            DataTable dt = uh.GetSysUserLineList(UserName);
                            if (dt == null || dt.Rows.Count == 0)
                            {
                                strResult = @"{ResultFlag:'-3',ResultData:'没有访问任何线路的权限'}";//没有默认，没有线路
                            }
                            else
                            {
                                strResult = @"{ResultFlag:'2',ResultData:" + JsonConvert.SerializeObject(dt) + "}";//返回所有线路信息
                            }
                        }
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }

            }
            catch (Exception ex)
            {
                logger.Error(string.Format("LoginSys异常：{4}.参数如下：UserName:{0},Password:{1},AuthName:{2},AuthPwd:{3}", UserName, Password, AuthName, AuthPwd, ex.Message));
                strResult = @"{ResultFlag:'0',ResultData:'" + ex.Message + "'}";//登录异常消息
            }
            //List<ResultInfo> lstR = JsonConvert.DeserializeObject<List<ResultInfo>>(strResult);
            //foreach (ResultInfo item in lstR)
            //{
            //    string s = item.ResultFlag;
            //    string s1 = item.ResultData;
            //}
            return strResult;
        }
        [WebMethod(Description = "获取拌合站用户可以操作的线路信息;参数：UserName(用户名);ResultFlag：-1授权失败,0异常错误,1用户能访问的所有线路信息")]
        public string GetBhzUserLineList(string UserName, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    Yqun.BO.BusinessManager.UserHelper uh = new Yqun.BO.BusinessManager.UserHelper();
                    string strUserLineList = JsonConvert.SerializeObject(uh.GetBhzUserLineList(UserName));
                    strResult = "{ResultFlag:'1',ResultData:" + strUserLineList + "}";
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("GetBhzUserLineList异常：{0}.参数如下：UserName:{1}", ex.Message, UserName));
                strResult = string.Format("{ResultFlag:'0',ResultData:'{0}'}", ex.Message);//登录异常消息
            }
            return strResult;
        }

        [WebMethod(Description = "登录拌合站;ResultFlag：-1授权失败,-2用户名密码错误,-3没有访问任何线路的权限,0异常错误,1登录成功并返回用户最后查看线路ID,2没有默认线路返回用户能访问的所有线路信息")]
        public string LoginBhz(string UserName, string Password, string AuthName, string AuthPwd)
        {
            string strResult = "";
            try
            {
                if (Login(AuthName, AuthPwd))
                {
                    Yqun.BO.LoginBO lb = new Yqun.BO.LoginBO();
                    //if (!Yqun.Services.Agent.CheckUser(UserName, Password, true))
                    if (!lb.CheckUserBhz(UserName, Password))
                    {
                        strResult = @"{ResultFlag:'-2',ResultData:'用户名密码错误'}";//登录失败
                    }
                    else
                    {
                        Yqun.BO.BusinessManager.UserHelper uh = new Yqun.BO.BusinessManager.UserHelper();
                        string strLastLineID = uh.GetBhzUserLastLineID(UserName);
                        if (!string.IsNullOrEmpty(strLastLineID))
                        {
                            strResult = @"{ResultFlag:'1',ResultData:'" + strLastLineID + "'}";//返回上次线路code
                        }
                        else
                        {
                            DataTable dt = uh.GetBhzUserLineList(UserName);
                            if (dt == null || dt.Rows.Count == 0)
                            {
                                strResult = @"{ResultFlag:'-3',ResultData:'没有访问任何线路的权限'}";//没有默认，没有线路
                            }
                            else
                            {
                                strResult = @"{ResultFlag:'2',ResultData:" + JsonConvert.SerializeObject(dt) + "}";//返回所有线路信息
                            }
                        }
                    }
                }
                else
                {
                    strResult = @"{ResultFlag:'-1',ResultData:'授权失败'}";//授权失败 
                }

            }
            catch (Exception ex)
            {
                logger.Error(string.Format("LoginBhz异常：{4}.参数如下：UserName:{0},Password:{1},AuthName:{2},AuthPwd:{3}", UserName, Password, AuthName, AuthPwd, ex.Message));
                strResult = @"{ResultFlag:'0',ResultData:'" + ex.Message + "'}";//登录异常消息
            }
            //List<ResultInfo> lstR = JsonConvert.DeserializeObject<List<ResultInfo>>(strResult);
            //foreach (ResultInfo item in lstR)
            //{
            //    string s = item.ResultFlag;
            //    string s1 = item.ResultData;
            //}
            return strResult;
        }

        #endregion

        #region 通用方法

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
        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}
        /// <summary>
        /// 配置连接
        /// </summary>
        static MobileService()
        {
            //读取配置信息
            String DataAdapterType = ConfigurationManager.AppSettings["DataAdapterType"];
            String DataBaseType = ConfigurationManager.AppSettings["DataBaseType"];
            String DataSource = ConfigurationManager.AppSettings["DataSource"];
            String DataInstance = ConfigurationManager.AppSettings["DataInstance"];
            String DataUserName = ConfigurationManager.AppSettings["DataUserName"];
            String DataPassword = ConfigurationManager.AppSettings["DataPassword"];
            String DataISAttach = ConfigurationManager.AppSettings["DataISAttach"];
            String AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];

            ServerLoginInfos.DBConnectionInfo.DataAdapterType = DataAdapterType;
            ServerLoginInfos.DBConnectionInfo.DataBaseType = DataBaseType;
            ServerLoginInfos.DBConnectionInfo.DataSource = EncryptSerivce.Dencrypt(DataSource);
            ServerLoginInfos.DBConnectionInfo.DataInstance = EncryptSerivce.Dencrypt(DataInstance);
            ServerLoginInfos.DBConnectionInfo.DataUserName = EncryptSerivce.Dencrypt(DataUserName);
            ServerLoginInfos.DBConnectionInfo.DataPassword = EncryptSerivce.Dencrypt(DataPassword);
            ServerLoginInfos.DBConnectionInfo.DataISAttach = DataISAttach;
            ServerLoginInfos.DBConnectionInfo.LocalStartPath = AppDomain.CurrentDomain.BaseDirectory + AssemblyPath.Trim("\\".ToCharArray()) + @"\";

            log4net.Config.XmlConfigurator.Configure();
            //LocalQuartzService.GetQuartzService().Start();
        }

        /// <summary>
        /// 验证授权
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        private bool Login(string UserName, string Password)
        {
            string userMessage = System.Configuration.ConfigurationManager.AppSettings[UserName];
            if (userMessage == UserName + "," + Password)
            {
                if (Yqun.Common.Encoder.EncryptSerivce.Dencrypt(Password) == UserName + "+KingRocket")
                {
                    return true;
                }
                else { return false; }
            }
            else { return false; }

        }
        #endregion
    }
    public class ResultInfo
    {
        public string ResultFlag { get; set; }
        public string ResultData { get; set; }

    }
}
