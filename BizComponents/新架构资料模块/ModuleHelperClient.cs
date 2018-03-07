using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yqun.Services;
using BizCommon;
using System.Data;
using System.IO;
using Yqun.Client.BizUI;
using System.Windows.Forms;

namespace BizComponents
{
    public class ModuleHelperClient
    {
        /// <summary>
        /// 通过ID,Name集合
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        public static DataTable GetSheetIDAndName(string ModuleIDs)
        {
            if (string.IsNullOrEmpty(ModuleIDs))
            {
                ModuleIDs = "'00000000-0000-0000-0000-000000000000'";
            }
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetSheetIDAndName", new object[] { ModuleIDs }) as DataTable;
        }
        /// <summary>
        /// 通过SheetID找到该Sheet的XML结构
        /// </summary>
        /// <param name="SheetIndex"></param>
        /// <returns></returns>
        public static String GetSheetXMLByID(Guid SheetIndex)
        {
            String str = Agent.CallService("Yqun.BO.BusinessManager.dll", "GetSheetXMLByID", new object[] { SheetIndex }).ToString();

            if (IsContainerXml(str))
            {
                return str;
            }
            else
            {
                return JZCommonHelper.GZipDecompressString(str);
            }
        }

        /// <summary>
        /// 通过SheetID
        /// 找到该Sheet的基本内容
        /// </summary>
        /// <param name="SheetIndex"></param>
        /// <returns></returns>
        public static Sys_Sheet GetSheetItemByID(Guid SheetIndex)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetSheetItemByID", new object[] { SheetIndex }) as Sys_Sheet;
        }

        public static Boolean SaveSheet(Sys_Sheet sheet)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveSheet", new object[] { sheet }));
        }

        /// <summary>
        /// 得到该模板的跨表公式
        /// </summary>
        /// <param name="moduleIndex"></param>
        /// <returns></returns>
        public static List<JZFormulaData> GetFormulaByModuleIndex(Guid moduleIndex)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetFormulaByModuleIndex", new object[] { moduleIndex }) as List<JZFormulaData>;
        }

        /// <summary>
        /// 得到该sheet中的跨表公式
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        public static List<JZFormulaData> GetFormulaBySheetIndex(Guid sheetIndex)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetFormulaBySheetIndex", new object[] { sheetIndex }) as List<JZFormulaData>;
        }

        /// <summary>
        /// 通过模板ID得到该模板的默认Document结构，用于新建资料时
        /// </summary>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        public static JZDocument GetDefaultDocument(Guid moduleID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetDefaultDocument", new object[] { moduleID }) as JZDocument;
        }

        public static Sys_Module GetModuleBaseInfoByID(Guid moduleID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetModuleBaseInfoByID", new object[] { moduleID }) as Sys_Module;
        }

        public static Boolean UpdateModuleSetting(Sys_Module module)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateModuleSetting", new object[] { module }));
        }

        public static Boolean CopySheet(Guid fromSheetID, Guid newSheetID, String sheetName)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "CopySheet", new object[] { fromSheetID, newSheetID, sheetName }));
        }

        public static Boolean IsSheetUsing(Guid sheetID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "IsSheetUsing", new object[] { sheetID }));
        }

        public static Boolean DeleteSheet(Guid sheetID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteSheet", new object[] { sheetID }));
        }

        public static Boolean UpdateSheetName(Guid sheetID, String sheetName)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateSheetName", new object[] { sheetID, sheetName }));
        }

        public static Boolean UpdateSheetCatelogName(Guid sheetID, String catelogName)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateSheetCatelogName", new object[] { sheetID, catelogName }));
        }

        public static DataTable InitSheetTreeByCategory(String category)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "InitSheetTreeByCategory", new object[] { category }) as DataTable;
        }

        public static List<Sys_Sheet> GetSheetsByModuleID(Guid moduleID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetSheetsByModuleID", new object[] { moduleID }) as List<Sys_Sheet>;
        }

        public static DataTable GetSheetCategory()
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetSheetCategory", new object[] { }) as DataTable;
        }

        public static DataSet GetSheetCategoryAndSheet()
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetSheetCategoryAndSheet", new object[] { }) as DataSet;
        }

        public static DataSet GetModuleCategoryAndModule()
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetModuleCategoryAndModule", new object[] { }) as DataSet;
        }

        public static Boolean SaveModule(Guid moduleID, List<Sys_Sheet> sheets, Dictionary<String, JZFormulaData> CrossSheetFormulaCache)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveModule", new object[] { moduleID, sheets, CrossSheetFormulaCache }));
        }

        public static Boolean IsModuleUsing(Guid moduleID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "IsModuleUsing", new object[] { moduleID }));
        }

        public static Boolean DeleteModule(Guid moduleID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteModule", new object[] { moduleID }));
        }

        public static Boolean UpdateModuleName(Guid moduleID, String moduleName)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateModuleName", new object[] { moduleID, moduleName }));
        }

        public static Boolean NewModule(Sys_Module module)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "NewModule", new object[] { module }));
        }

        public static DataTable InitModuleTreeByCategory(String key)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "InitModuleTreeByCategory", new object[] { key }) as DataTable;
        }

        public static DataTable GetModuleCategory()
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetModuleCategory", new object[] { }) as DataTable;
        }

        public static DataTable GetActiveModuleList()
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetActiveModuleList", new object[] { }) as DataTable;
        }

        public static DataTable GetStadiumConfigList()
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetStadiumConfigList", new object[] { }) as DataTable;
        }
        public static JZStadiumConfig GetStadiumCinfigByModuleID(Guid modelID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetStadiumCinfigByModuleID", new object[] { modelID }) as JZStadiumConfig;
        }

        public static JZStadiumConfig GetStadiumConfigByDocumentID(Guid documentID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetStadiumConfigByDocumentID", new object[] { documentID }) as JZStadiumConfig;
        }

        public static Boolean SaveStadiumConfig(Guid moduleID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveStadiumConfig", new object[] { moduleID }));
        }

        public static Boolean DeleteStadiumConfig(Guid moduleID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteStadiumConfig", new object[] { moduleID }));
        }

        public static Boolean UpdateStadiumConfig(Guid moduleID, String config, Boolean isActive)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateStadiumConfig", new object[] { moduleID, config, isActive }));
        }

        public static String GetSearchRange(Guid moduleID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetSearchRange", new object[] { moduleID }).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="company"></param>
        /// <param name="testroom"></param>
        /// <param name="deviceType">-1代表全部龄期</param>
        /// <returns></returns>
        public static DataTable GetStadiumList(String segment, String company, String testroom, Int32 deviceType)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetStadiumList", new object[] { segment, company, testroom, deviceType }) as DataTable;
        }
        public static DataTable GetTemperatureList(String TestRoomCode, DateTime Start, DateTime End, int TemperatureType)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetTemperatureList", new object[] { TestRoomCode, Start, End, TemperatureType }) as DataTable;
        }

        /// <summary>
        /// 保存温度记录
        /// </summary>
        /// <param name="dt">保存温度记录</param>
        /// <returns>true成功，false失败</returns>
        public static string SaveTemperatures(string TestRoomCode, string LastEditUser, DataTable dt, int TemperatureType)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveTemperatures", new object[] { TestRoomCode, LastEditUser, dt as object, TemperatureType }).ToString();
        }

        public static DataTable GetModuleConfigList()
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetModuleConfigList", new object[] { }) as DataTable;
        }

        public static Boolean DelectModuleConfig(Guid moduleID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DelectModuleConfig", new object[] { moduleID }));
        }

        public static Boolean SaveModuleConfig(Guid moduleID, int sNumber, String config, Int32 isActive)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveModuleConfig", new object[] { moduleID, sNumber, config, isActive }));
        }

        public static DataTable GetModuleConfigItemList(Guid moduleID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetModuleConfigItemList", new object[] { moduleID }) as DataTable;
        }

        public static Boolean UpdateModuleConfigInfo(Guid moduleID, String json, Boolean isActive, Int32 deviceType)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateModuleConfigInfo", new object[] { moduleID, json, isActive, deviceType }));
        }

        public static Boolean SaveLineFormula(Guid moduleID, Dictionary<string, JZFormulaData> CrossSheetFormulaCache)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveLineFormula", new object[] { moduleID, CrossSheetFormulaCache }));
        }


        public static List<JZFormulaData> GetLineFormulaByModuleIndex(Guid moduleIndex)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetLineFormulaByModuleIndex", new object[] { moduleIndex }) as List<JZFormulaData>;
        }

        public static JZWatermark GetWatermarkByModuleID(Guid moduleID)
        {
            JZWatermark watermark = null;
            DataTable dt = Agent.CallService("Yqun.BO.BusinessManager.dll", "GetWatermarkByModuleID", new object[] { moduleID }) as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                watermark = new JZWatermark();
                watermark.FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "watermark\\", dt.Rows[0]["FileName"].ToString());
                if (!File.Exists(watermark.FilePath))
                {
                    return null;
                }
                watermark.Opacity = Convert.ToInt32(dt.Rows[0]["WOpacity"]);
                watermark.Left = Convert.ToInt32(dt.Rows[0]["WLeft"]);
                watermark.Top = Convert.ToInt32(dt.Rows[0]["WTop"]);
                watermark.SheetID = new Guid(dt.Rows[0]["SheetID"].ToString());
                watermark.Width = Convert.ToInt32(dt.Rows[0]["Width"]);
                watermark.Height = Convert.ToInt32(dt.Rows[0]["Height"]);
            }
            return watermark;
        }

        public static DateTime GetServerDate()
        {
            DateTime dt = Convert.ToDateTime(Agent.CallService("Yqun.BO.BusinessManager.dll", "GetServerDate", new object[] { }));
            return dt;
        }

        public static bool IsContainerXml(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return false;
            }

            return content.Trim().StartsWith("<?xml");
        }

        /// <summary>
        /// 获取模板禁止发布的线路ID
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        public static List<string> GetLineModuleByModuleID(string ModuleID, int IsModule)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetLineModuleByModuleID", new object[] { ModuleID, IsModule }) as List<string>;
        }

        /// <summary>
        /// 保存模板禁止发布的线路信息
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        public static bool SaveLineModule(string ModuleID, List<string> lstLineIDs, int IsModule)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveLineModule", new object[] { ModuleID, lstLineIDs, IsModule }));
        }
        /// <summary>
        /// 保存模板禁止发布的线路信息
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        public static DataTable GetForbidLinesByModuleIDs(string moduleIDs, int IsModule)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetForbidLinesByModuleIDs", new object[] { moduleIDs, IsModule }) as DataTable;
        }

        /// <summary>
        /// 获取表单的单元格样式
        /// </summary>
        /// <param name="moduleIDs"></param>
        /// <param name="IsModule"></param>
        /// <returns></returns>
        public static DataTable GetCellStyleBySheetID(Guid SheetID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetCellStyleBySheetID", new object[] { SheetID }) as DataTable;
        }
        /// <summary>
        /// 保存表单的单元格样式
        /// </summary>
        /// <param name="moduleIDs"></param>
        /// <param name="IsModule"></param>
        /// <returns></returns>
        public static bool SaveCellStyle(Guid SheetID, string CellName, JZCellStyle CellStyle)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveCellStyle", new object[] { SheetID, CellName, CellStyle }));
        }

        /// <summary>
        /// 删除表单的单元格样式
        /// </summary>
        /// <param name="moduleIDs"></param>
        /// <param name="IsModule"></param>
        /// <returns></returns>
        public static bool DeleteCellStyle(Guid SheetID, string CellName)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteCellStyle", new object[] { SheetID, CellName }));
        }

        public static DataSet GetReportIndex(Guid moduleID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetReportIndex", new object[] { moduleID }) as DataSet;
        }

        public static bool UpdateReportIndex(Guid moduleID, int reportIndex, string statisticsCatlog)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateReportIndex", new object[] { moduleID, reportIndex, statisticsCatlog }));
        }
    }
}
