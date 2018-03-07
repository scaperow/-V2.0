using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yqun.Services;

namespace BizComponents
{
    public class DataMoveHelperClient
    {
        /// <summary>
        /// 迁移模板等数据
        /// </summary>
        public void AutoSaveAllDocuments()
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "AutoSaveAllDocuments", new object[] { });
        }

        /// <summary>
        /// 迁移资料数据
        /// </summary>
        public void MoveDocument()
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "MoveDocument", new object[] { });
        }

        public static void ConvertDevice()
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "ConvertDevice", new object[] { });
        }

        public void MoveDocumentByModuleID(String moduleID)
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "MoveDocumentByModuleID", new object[] { moduleID });
        }

        public void MoveInvalidDocument()
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "MoveInvalidDocument", new object[] { });
        }

        public void MoveStadiumData()
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "MoveStadiumData", new object[] { });
        }

        public void MoveModifyChange()
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "MoveModifyChange", new object[] { });
        }

        public void MoveEditLog()
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "MoveEditLog", new object[] { });
        }

        public object GetStadiumObj(Guid stadiumID, Int32 deviceType)
        {
            return Agent.CallService("Yqun.BO.BusinessManager.dll", "GetTestObject", new object[] { stadiumID, deviceType });
        }

        public Boolean UploadTestData(Guid documentID, Guid moduleID, Guid stadiumID, String wtbh, String testRoomCode,
                Int32 seriaNumber, String userName, String testData, String realTimeData, Int32 totalNumber, String machineCode)
        {
            return Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UploadTestData",
                new object[] { documentID, moduleID,stadiumID,  wtbh,  testRoomCode,
                 seriaNumber,  userName,  testData,  realTimeData,  totalNumber,  machineCode }));
        }
        public string TestSMSSend(string phones, string context)
        {
            return Agent.CallService("Yqun.BO.ReminderManager.dll", "TestSMSSend",
                new object[] { phones, context }).ToString();
        }

        public void MoveModuleByID(String moduleID)
        {
            Agent.CallService("Yqun.BO.BusinessManager.dll", "MoveModuleByID", new object[] { moduleID });
        }
    }
}
