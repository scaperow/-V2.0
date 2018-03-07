using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Services;
using System.Data;
using System.IO;

namespace BizComponents
{
    public class DepositoryEvaluateDataList
    {
        public static List<string> GetTestRoomList()
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetValidTestRoomCodeList", new object[] { }) as List<string>;
        }

        public static DataTable GetReminderInfos(String[] TestRoomCode, DateTime Start, DateTime End)
        {
            return Agent.CallService("Yqun.BO.ReminderManager.dll", "GetEvaluateReminderInfos", new object[] { TestRoomCode, Start, End }) as DataTable;
        }

        public static DataTable GetInvalidDocumentList(String segmentCode, String campanyCode, String testRoomCode, String sReportName, String sReportNumber, DateTime Start, DateTime End, String sTestItem)
        {
            return Agent.CallService("Yqun.BO.ReminderManager.dll", "GetInvalidDocumentList", new object[] { segmentCode, campanyCode, testRoomCode, sReportName, sReportNumber, Start, End, sTestItem }) as DataTable;
        }

        public Boolean HasLabEvaluateDataList(String[] TestRoomCode, DateTime Start, DateTime End)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.ReminderManager.dll", "HasLabEvaluateDataList", new object[] { TestRoomCode, Start, End }));
        }

        public static String GetInvalidReportNote(String id, int userType)
        {
            Object obj = Agent.CallService("Yqun.BO.BusinessManager.dll", "GetInvalidReportNote", new object[] { id, userType });
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }

        public static void SaveInvalidReportNote(String id, String note, int userType)
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveInvalidReportNote", new object[] { id, note, userType });
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="dataID"></param>
        /// <param name="ImgStream"></param>
        public static void SaveIamge(DataTable dt)
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveImage", new object[] { dt });
        }

        /// <summary>
        /// 获取原因分析列表
        /// </summary>
        /// <param name="dataID"></param>
        /// <returns></returns>
        public static DataTable GetImage(string dataID,string ImgRemark)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetImageTable", new object[] { dataID, ImgRemark }) as DataTable;
        }

        /// <summary>
        /// 删除图片信息
        /// </summary>
        /// <param name="dataID"></param>
        public static void DelIamge(string dataID)
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "DelIamge", new object[] { dataID });
        }
    }
}
