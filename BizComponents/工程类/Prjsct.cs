using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Data.DataBase;
using Yqun.Services;
using BizCommon;

namespace BizComponents
{
    public class DepositoryPrjsctInfo
    {
        public static List<Prjsct> QueryPrjscts(String PrjCode)
        {
            List<Prjsct> PrjstLists = Agent.CallService("Yqun.BO.BusinessManager.dll", "QueryPrjscts", new object[] { PrjCode }) as List<Prjsct>;
            return PrjstLists;
        }

        public static Prjsct Query(String PrjsctCode)
        {
            Prjsct PrjstInfo = Agent.CallService("Yqun.BO.BusinessManager.dll", "QueryQueryPrjsct", new object[] { PrjsctCode }) as Prjsct;
            return PrjstInfo;
        }

        public static Boolean HavePrjstInfo(String PrjsctName)
        {
            Boolean IsHave = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HavePrjstInfo", new object[] { PrjsctName }));
            return IsHave;
        }

        /// <summary>
        /// 新建标段信息
        /// </summary>
        /// <param name="PrjstInfo"></param>
        /// <returns></returns>
        public static Boolean New(Prjsct PrjstInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "NewPrjstInfo", new object[] { PrjstInfo }));
            return Result;
        }

        /// <summary>
        /// 删除标段信息
        /// </summary>
        /// <param name="PrjsctCode"></param>
        /// <returns></returns>
        public static Boolean Delete(string PrjsctCode, string PrjsctId)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeletePrjsctInfo", new object[] { PrjsctCode, PrjsctId }));
            return Result;
        }


        /// <summary>
        /// 更新标段信息
        /// </summary>
        /// <param name="ProjectInfo"></param>
        /// <returns></returns>
        public static Boolean Update(Prjsct PrjstInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdatePrjsctInfo", new object[] { PrjstInfo }));
            return Result;
        }
    }
}
