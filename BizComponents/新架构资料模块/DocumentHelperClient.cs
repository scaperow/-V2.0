using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yqun.Services;
using BizCommon;
using System.Data;

namespace BizComponents
{
    public class DocumentHelperClient
    {
        public static JZDocument GetDocumentByID(Guid documentID)
        {
            String json = Agent.CallService("Yqun.BO.BusinessManager.dll", "GetDocumentByID", new object[] { documentID }).ToString();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(json);
        }

        /// <summary>
        /// 未保存单元格数据值，此单元格所在模板与表单
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="sheetID"></param>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        public static Boolean IsUnique(JZCellProperty property, JZCell cell, Guid sheetID, Guid moduleID, String testRoomCode, Guid documentID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "IsUnique", new object[] { property, cell, sheetID, moduleID, testRoomCode, documentID }));
        }

        /// <summary>
        /// 单元格的值是否唯一，返回不唯一的模板名
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="sheetID"></param>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        public static DataTable IsUniqueAndReturnDT(JZCellProperty property, JZCell cell, Guid sheetID, Guid moduleID, String testRoomCode, Guid documentID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "IsUniqueAndReturnDT", new object[] { property, cell, sheetID, moduleID, testRoomCode, documentID }) as DataTable;
        }

        /// <summary>
        /// 保存资料；pxDocumentID为被平行的施工单位资料ID
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="moduleID"></param>
        /// <param name="pxDocumentID"></param>
        /// <returns></returns>
        public static Guid SaveDocument(JZDocument doc, Sys_Document doc_base)
        {
            return new Guid(Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveDocument", new object[] { doc, doc_base }).ToString());
        }

        /// <summary>
        /// 获得台账列表
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="testRoomCode"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static Sys_TaiZhang GetDocumentList(Guid moduleID, String testRoomCode, String sortColumn, Int32 isDesc,
            Int32 pageIndex, Int32 pageCount, String filter)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetDocumentList",
                new object[] { 
                    moduleID, 
                    testRoomCode??"", 
                    sortColumn??"",
                    isDesc,
                    pageIndex,
                    pageCount,
                    filter??""
                }) as Sys_TaiZhang;
        }

        public static Sys_Document GetDocumentBaseInfoByID(Guid dataID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetDocumentBaseInfoByID", new object[] { dataID }) as Sys_Document;
        }

        public static void UpdateCustomViewWidth(Guid moduleID, String testRoomCode, Dictionary<JZCustomView, float> list)
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateCustomViewWidth", new object[] { moduleID, testRoomCode, list });
        }

        public static Boolean UpdateDocumentBaseInfo(Sys_Document doc, String updatedField)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateDocumentBaseInfo", new object[] { doc, updatedField }));
        }

        public static Boolean HasPXAlready(Guid sgDataID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HasPXAlready", new object[] { sgDataID }));
        }
        public static Boolean HasPXRelation(Guid sgDataID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HasPXRelation", new object[] { sgDataID }));
        }

        public static Boolean NewPXDocument(Guid sgDataID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "NewPXDocument", new object[] { sgDataID }));
        }

        public static Boolean DeleteDocument(Guid dataID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteDocument", new object[] { dataID }));
        }

        public static Boolean CopyDocument(Guid copyDataID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "CopyDocument", new object[] { copyDataID }));
        }

        public static List<JZCustomView> GetCustomViewList(Guid moduleID, String testRoomCode)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetCustomViewList", new object[] { moduleID, testRoomCode }) as List<JZCustomView>;
        }

        public static Boolean SaveCustomView(Guid moduleID, String testRoomCode, String viewJson)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveCustomView", new object[] { moduleID, testRoomCode, viewJson }));
        }

        public static Guid GetRequestChangeID(Guid documentID)
        {
            return new Guid(Agent.CallService("Yqun.BO.BusinessManager.dll", "GetRequestChangeID", new object[] { documentID }).ToString());
        }
        public static DataTable GetRequestChangeUnOP(Guid documentID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetRequestChangeUnOP", new object[] { documentID }) as DataTable;
        }

        public static Boolean NewRequestChange(Sys_RequestChange request)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "NewRequestChange", new object[] { request }));
        }

        public static DataTable GetRequestChangeList(String segment, String company, String testroom, DateTime start, DateTime end, String status, String content, String user)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetRequestChangeList", new object[] { segment, company, testroom, start, end, status, content, user }) as DataTable;
        }

        public static DataTable GetUnPreocessedRequestList()
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetUnPreocessedRequestList", new object[] { }) as DataTable;
        }


        public static DataTable GetInvalidDocumentList(String segment, String company, String testRoom, String sReportName, String sReportNumber, DateTime Start, DateTime End, String sTestItem)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetInvalidDocumentList", new object[] { segment, company, testRoom, sReportName, sReportNumber, Start, End, sTestItem }) as DataTable;
        }

        public static DataTable GetUndoInvalidDocumentList(Boolean sg)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetUndoInvalidDocumentList", new object[] { sg }) as DataTable;
        }

        public static void ApplyExtFields(Guid moduleID)
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "ApplyExtFields", new object[] { moduleID });
        }

        public static Dictionary<Sys_Document, JZDocument> GetDocumentDataListByModuleIDAndTestRoomCode(Guid moduleID, String testRoomCode)
        {
            Dictionary<Sys_Document, JZDocument> list = new Dictionary<Sys_Document, JZDocument>();
            DataTable dt = Agent.CallService("Yqun.BO.BusinessManager.dll", "GetDocumentDataListByModuleIDAndTestRoomCode", new object[] { moduleID, testRoomCode }) as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Sys_Document doc = new Sys_Document()
                    {
                        ID = new Guid(dt.Rows[i]["ID"].ToString()),
                        TestRoomCode = dt.Rows[i]["TestRoomCode"].ToString(),
                        Status = short.Parse(dt.Rows[i]["Status"].ToString()),
                        DataName = dt.Rows[i]["DataName"].ToString(),
                        TryType = dt.Rows[i]["TryType"].ToString(),
                        ModuleID = new Guid(dt.Rows[i]["ModuleID"].ToString()),
                        BGBH = dt.Rows[i]["BGBH"] == DBNull.Value ? "" : dt.Rows[i]["BGBH"].ToString()
                    };

                    list.Add(doc, Newtonsoft.Json.JsonConvert.DeserializeObject<JZDocument>(dt.Rows[i]["Data"].ToString()));
                }
            }
            return list;
        }

        public static Boolean GeneratePLDTable()
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "GeneratePLDTable", new object[] { }));
        }

        public static DataTable GetInvalidProcessInfo(String docID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetInvalidProcessInfo", new object[] { docID }) as DataTable;
        }

        public static DataTable GetInvalidImageList(String docID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetInvalidImageList", new object[] { docID }) as DataTable;
        }

        public static String SaveInvalidImage(String invalidID, JZFile file, String type)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveInvalidImage", new object[] { invalidID, file, type }).ToString();
        }

        public static DataTable SearchStadiumByWTBH(String wtbh)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "SearchStadiumByWTBH", new object[] { wtbh }) as DataTable;
        }

        public static Boolean ResetStadiumToToday(Guid stadiumID)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "ResetStadiumToToday", new object[] { stadiumID }));
        }

        public static Sys_Page GetTestOverTimeProcessed(int index, int size)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetTestOverTimeProcessed", new object[] { index, size }) as Sys_Page;
        }

        public static DataTable GetTestOverTimeData()
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetTestOverTimeData", new object[] { }) as DataTable;
        }

        public static void DeleteTestOverTime(List<string> dataIds)
        {
             Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteTestOverTime", new object[] { dataIds });
        }

        /// <summary>
        /// 保存资料温度类型
        /// </summary>
        /// <returns>true成功，false失败</returns>
        public static Boolean SaveDocumentTemperatureType(Guid docID, int TemperatureType)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveDocumentTemperatureType", new object[] { docID, TemperatureType }));
        }

        /// <summary>
        /// 获取资料扩展属性
        /// </summary>
        public static DataTable GetDocumentExt(Guid docID)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetDocumentExt", new object[] { docID }) as DataTable;
        }
    }
}
