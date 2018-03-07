using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;

namespace Yqun.BO.BusinessManager
{
    public class FunctionItemInfoManager : BOBase
    {
        public List<FunctionItemInfo> InitFunctionItemInfos()
        {
            List<FunctionItemInfo> FunctionItemInfos = new List<FunctionItemInfo>();

            StringBuilder sql_Select = new StringBuilder();
            //增加查询条件Scdel=0     2013-10-19
            sql_Select.Append("select * from sys_biz_FunctionInfos where Scdel=0 ");

            DataTable Data = GetDataTable(sql_Select.ToString());
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    FunctionItemInfo ItemInfo = new FunctionItemInfo();
                    ItemInfo.Index = Row["ID"].ToString();
                    ItemInfo.AssemblyName = Row["AssemblyName"].ToString();
                    ItemInfo.FullClassName = Row["FullClassName"].ToString();
                    FunctionItemInfos.Add(ItemInfo);
                }
            }

            return FunctionItemInfos;
        }
    }
}
