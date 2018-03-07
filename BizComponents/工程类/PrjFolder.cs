using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Services;
using System.Data;
using BizCommon;

namespace BizComponents
{
    public class DepositoryFolderInfo
    {
        /// <summary>
        /// 获得某个单位下所有的文件夹
        /// </summary>
        /// <param name="PrjsctId">单位的Index</param>
        /// <returns></returns>
        public static List<PrjFolder> QueryFolders(string OrgInfoId)
        {
            List<PrjFolder> FolderList = Agent.CallService("Yqun.BO.BusinessManager.dll", "QueryFolders", new object[] { OrgInfoId }) as List<PrjFolder>;
            return FolderList;            
        }

        /// <summary>
        /// 查找某个单位下所有的文件夹
        /// </summary>
        /// <param name="PrjsctCode">单位的编码</param>
        /// <returns></returns>
        public static List<PrjFolder> QueryPrjFolders(String OrgInfoCode)
        {
            List<PrjFolder> FolderList = Agent.CallService("Yqun.BO.BusinessManager.dll", "QueryPrjFolders", new object[] { OrgInfoCode }) as List<PrjFolder>;
            return FolderList; 
        }

        /// <summary>
        /// 查找某个单位下具有指定类型的文件夹
        /// </summary>
        /// <param name="PrjsctCode">单位的编码</param>
        /// <param name="FolderType">文件夹的类型</param>
        /// <returns></returns>
        public static List<PrjFolder> QueryPrjFolders(String OrgInfoCode, String FolderType)
        {
            List<PrjFolder> FolderList = Agent.CallService("Yqun.BO.BusinessManager.dll", "QueryPrjFolders", new object[] { OrgInfoCode, FolderType }) as List<PrjFolder>;
            return FolderList;
        }

        public static Boolean HaveFolderInfo(String OrgInfoCode, String FolderId)
        {
            Boolean IsHave = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HaveFolderInfo", new object[] { OrgInfoCode, FolderId }));
            return IsHave;
        }

        public static PrjFolder Query(String FolderCode)
        {
            PrjFolder FolderInfo = Agent.CallService("Yqun.BO.BusinessManager.dll", "QueryPrjFolder", new object[] { FolderCode }) as PrjFolder;
            return FolderInfo;

        }

        public static Boolean ToUser(String FolderId)
        {
            Boolean IsHave = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "ToUsePrjFolder", new object[] { FolderId }));
            return IsHave;
        }

        public static Boolean ToUser(String FolderId, String OrgInfoCode)
        {
            Boolean IsHave = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "ToUsePrjFolder", new object[] { FolderId, OrgInfoCode }));
            return IsHave;
        }

        public static Boolean New(PrjFolder FolderInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "NewPrjFolder", new object[] { FolderInfo }));
            return Result;     
        }

        public static Boolean Select(PrjFolder FolderInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SelectPrjFolder", new object[] { FolderInfo }));
            return Result;
        }

        public static Boolean Delete(string FolderCode)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeletePrjFolderByCode", new object[] { FolderCode }));
            return Result;
        }

        public static Boolean Delete(string FolderCode, string FolderId)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeletePrjFolder", new object[] { FolderCode, FolderId }));
            return Result;     
        }

        public static Boolean DeleteMachine(string FolderId)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeletePrjFolderByID", new object[] { FolderId }));
            return Result; 
        }


        public static Boolean Update(PrjFolder FolderInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdatePrjFolder", new object[] { FolderInfo }));
            return Result;
        }
    }
}
