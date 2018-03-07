using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ShuXianCaiJiModule;
using ShuXianCaiJiComponents;
using Yqun.Services;

namespace Gettestnum
{
    public class Program
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));
            log4net.Config.XmlConfigurator.ConfigureAndWatch(file);

            logger.Info(string.Format("Gettestnum:{0}", DateTime.Now.ToString()));

            Thread.Sleep(3000);
            DoMain();
            Thread.Sleep(1000);
        }

        public static void DoMain()
        {
            Logger log = new Logger(Path.Combine(Directory.GetCurrentDirectory(), "Log"));
            Boolean r;
            try
            {
                String itemId = "";
                String itemName = "";
                String testCount = "";
                //2014.2.12 yuhongxi 新增参数
                String Type = "";
                List<String> modelList = new List<string>();

                StringBuilder sql_select = new StringBuilder();
                sql_select.Append("select Top 1 * from sys_testselecteditem");

                DataTable Data = Agent.CallLocalService("Yqun.BO.BOBase.dll", "GetDataTable", new object[] { sql_select.ToString() }) as DataTable;
                if (Data != null && Data.Rows.Count > 0)
                {
                    DataRow Row = Data.Rows[0];
                    itemId = Row["Id"].ToString();
                    itemName = Row["ItemName"].ToString();
                    testCount = Row["testCount"].ToString();
                    //2014.2.12 yuhongxi 新增参数
                    Type = Row["Type"].ToString();
                }
                else
                {
                    logger.Error("获取选中的试验项目时失败！");
                }

                logger.Info(string.Format("当前选中的试验项目是{0},编号为{1}", itemName, itemId));

                //从服务器端查询当前试验室所选试验项目的所有委托编号
                if (itemId != "")
                {
                    //清空sys_testnum数据表
                    r = ClearTestNumInfo();
                    logger.Info(string.Format("清空sys_testnum数据表返回的状态值是{0}", r));

                    //获取与该试验项目相关的委托编号
                    if (!ConfigurationManager.ConfigurationManager.AppSettings.ContainsKey("TestCode") || Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["TestCode"]) == "")
                        throw new Exception("应用配置错误，获取标记为TestCode的信息失败");

                    String TestCode = Convert.ToString(ConfigurationManager.ConfigurationManager.AppSettings["TestCode"]);

                    #region OldCode
                    //DataTable WTBHData = Agent.CallRemoteService("Yqun.BO.DataTransportManager.dll", "GetRemoteWTBHDataTable", new object[] { itemId, TestCode }) as DataTable;
                    #endregion 

                    //2014.2.12 yuhongxi 新架构获取提醒信息表
                    logger.Info(Type.ToString());
                    CaijiCommHelper _CaijiCommHelper = new CaijiCommHelper(log);
                    DataTable WTBHData = _CaijiCommHelper.GetStadiumList(TestCode, int.Parse(Type));
                    logger.Info(string.Format("当前提醒总共{0}条",WTBHData.Rows.Count));
                    if (WTBHData == null || WTBHData.Columns.Count == 0)
                        logger.Info(string.Format("获取选中试验项目{0}关联的委托编号失败", itemName));
                    else
                    {
                        sql_select = new StringBuilder();
                        sql_select.Append("select * from sys_testnum where 1<>1");
                        DataTable TestNumData = Agent.CallLocalService("Yqun.BO.BOBase.dll", "GetDataTable", new object[] { sql_select.ToString() }) as DataTable;
                        if (TestNumData == null || TestNumData.Columns.Count == 0)
                            logger.Info(string.Format("获取选中试验项目{0}关联的委托编号数据表结构失败", itemName));
                        else
                        {
                            if (WTBHData.Rows.Count > 0)
                            {
                                logger.Info(string.Format("获取选中试验项目{0}关联的委托编号个数为{1}", itemName, WTBHData.Rows.Count));

                                foreach (DataRow Row in WTBHData.Rows)
                                {
                                    #region OldCode
                                    //String ModelIndex = Row["ModelIndex"].ToString();
                                    //String ModelName = "";
                                    //String WTBH = Row["F_WTBH"].ToString();
                                    //String SJSize = Row["F_SJSize"].ToString();
                                    //String F_ZJRQ = Row["F_ZJRQ"].ToString();
                                    //String TestCount = testCount;
                                    //String lq = Row["DateSpan"].ToString();
                                    #endregion

                                    //2014.2.12 yuhongxi 
                                    String ModelIndex = Row["ModuleID"].ToString();
                                    String ModelName = "";
                                    String WTBH = Row["委托编号"].ToString();
                                    String SJSize = Row["试件尺寸"].ToString();
                                    String F_ZJRQ = Row["制件日期"].ToString();
                                    String TestCount = testCount;
                                    String lq = Row["DateSpan"].ToString();

                                    DataRow NewRow = TestNumData.NewRow();
                                    NewRow["ItemId"] = itemId;
                                    NewRow["modelId"] = ModelIndex;
                                    NewRow["modelName"] = ModelName;
                                    NewRow["synum"] = WTBH;
                                    NewRow["testCount"] = TestCount;
                                    NewRow["size"] = SJSize;
                                    NewRow["zjdate"] = F_ZJRQ;
                                    NewRow["lq"] = lq;
                                    TestNumData.Rows.Add(NewRow);
                                }

                                object o = Agent.CallLocalService("Yqun.BO.BOBase.dll", "Update", new object[] { TestNumData });
                                r = (Convert.ToInt32(o) == 1);
                                logger.Info(string.Format("插入选中试验项目{0}关联的委托编号的返回状态为{1}", itemName, r));
                            }
                            else
                            {
                                logger.Info(string.Format("没有与选中试验项目{0}关联的委托编号", itemName));
                            }
                        }
                    }
                }
                else
                {
                    logger.Error(string.Format("当前所选试验项目的ID为{0}", itemId));
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    logger.Error(ex.InnerException.Message);

                //访问网络失败，清空sys_testnum数据表
                r = ClearTestNumInfo();
                logger.Error(string.Format("应用出错：{0}，清空sys_testnum数据表，返回的状态值是{1}", ex.Message, r));
            }
        }

        /// <summary>
        /// 清空sys_testnum数据表
        /// </summary>
        private static Boolean ClearTestNumInfo()
        {
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("delete from sys_testnum");

            object o = Agent.CallLocalService("Yqun.BO.BOBase.dll", "ExcuteCommand", new object[] { sql_select.ToString() });
            return Convert.ToInt32(o) == 1;
        }
    }
}
