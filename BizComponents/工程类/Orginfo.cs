using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Data.DataBase;
using Yqun.Services;
using BizCommon;

namespace BizComponents
{
    public class DepositoryOrganInfo
    {
        public static string GetNextCode(string ParentCode)
        {
            String Code = Agent.CallService("Yqun.BO.BusinessManager.dll", "GetOrgInfoNextCode", new object[] { ParentCode }) as String;
            return Code;
        }

        public static List<Orginfo> QueryOrgans(String PrjsctCode, String DepType)
        {
            List<Orginfo> OrginfoLists = Agent.CallService("Yqun.BO.BusinessManager.dll", "QueryOrgans", new object[] { PrjsctCode, DepType }) as List<Orginfo>;
            return OrginfoLists;
        }

        public static List<Orginfo> QueryMultimediaOrgans(String PrjCode)
        {
            List<Orginfo> OrginfoLists = Agent.CallService("Yqun.BO.BusinessManager.dll", "QueryMultimediaOrgans", new object[] { PrjCode }) as List<Orginfo>;
            return OrginfoLists;
        }


        public static List<Orginfo> QueryOrgansTree(String PrjsctCode, String DepType)
        {
            List<Orginfo> OrginfoLists = Agent.CallService("Yqun.BO.BusinessManager.dll", "QueryOrgansTree", new object[] { PrjsctCode, DepType }) as List<Orginfo>;
            return OrginfoLists;
        }

        /// <summary>
        /// 判断是否包含单位信息
        /// </summary>
        /// <param name="DepCode"></param>
        /// <returns></returns>
        public static Boolean HaveOrganInfo(String Description)
        {
            Boolean IsHave = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HaveOrganInfo", new object[] { Description }));
            return IsHave;
        }

        public static Boolean HaveOrganInfo(String DepId, String DepCode)
        {
            Boolean IsHave = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HaveOrganInfo", new object[] { DepId, DepCode }));
            return IsHave;
        }

        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <param name="DepCode"></param>
        /// <returns></returns>
        public static Orginfo Query(String DepId)
        {
            Orginfo OrgInfo = Agent.CallService("Yqun.BO.BusinessManager.dll", "QueryOrginfo", new object[] { DepId }) as Orginfo;
            return OrgInfo;
        }

        public static Boolean Select(Orginfo OrgInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "SelectOrginfo", new object[] { OrgInfo }));
            return Result;
        }

        /// <summary>
        /// 新建单位表
        /// </summary>
        /// <param name="OrgInfo"></param>
        /// <returns></returns>
        public static Boolean New(Orginfo OrgInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "NewOrginfo", new object[] { OrgInfo }));
            return Result;
        }

        /// <summary>
        /// 删除单位表
        /// </summary>
        /// <param name="DepCode"></param>
        /// <returns></returns>
        public static Boolean Delete(string DepCode)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteOrginfo", new object[] { DepCode }));
            return Result;
        }



        public static Boolean Update(Orginfo OrgInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateOrginfo", new object[] { OrgInfo }));
            return Result;
        }

        /// <summary>
        /// 判断单位是不是在使用
        /// </summary>
        /// <param name="DepID"></param>
        /// <returns></returns>
        public static Boolean ToUser(string DepID)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "ToUseOrginfo", new object[] { DepID }));
            return Result;
        }

        /// <summary>
        /// 判断单位是不是在该标段下存在
        /// </summary>
        /// <param name="DepID"></param>
        /// <returns></returns>
        public static Boolean ToUser(string DepID, string PrjsctCode)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "ToUseOrginfo", new object[] { DepID, PrjsctCode }));
            return Result;
        }

        public static Boolean DeleteDepName(string DepId)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteDepName", new object[] { DepId }));
            return Result;
        }
    }
}
