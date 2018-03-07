using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Common.ContextCache;
using Yqun.BO.QualificationManager;

namespace Yqun.BO.BusinessManager
{
    public class ModelResourceManager : BOBase
    {
        QualificationAuthManager QualificationAuthManager = new QualificationAuthManager();

        public DataTable InitModelResource()
        {
            //增加字段,Scts_1,Scdel  2012-10-15
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("Select ID,CatlogCode,Description,'true' as IsModel,Scts_1,Scdel from sys_biz_Module ");
            sql_select.Append(" union ");
            sql_select.Append("Select ID,CatlogCode,CatlogName as Description,'false' as IsModel,Scts_1,Scdel from sys_biz_ModuleCatlog ");
            sql_select.Append("order by CatlogCode");

            DataTable Data = GetDataTable(sql_select.ToString());
            DataTable NewData = new DataTable();
            if (Data != null)
            {
                if (!ApplicationContext.Current.IsAdministrator && !string.IsNullOrEmpty(ApplicationContext.Current.InTestRoom.Code))
                {
                    String FolderCode = ApplicationContext.Current.InTestRoom.Code;
                    List<string> Codes = QualificationAuthManager.InitQualificationAuthInfo(FolderCode);

                    NewData = Data.Clone();

                    foreach (DataRow Row in Data.Rows)
                    {
                        String Code = Row["CatlogCode"].ToString();
                        if (HasAuth(Codes, Code))
                        {
                            NewData.ImportRow(Row);
                        }
                    }
                }
                else
                {
                    NewData = Data;
                }
            }

            return NewData;
        }

        private bool HasAuth(List<String> Codes, string Code)
        {
            Boolean Result = false;

            foreach (String Element in Codes)
            {
                if (Code.StartsWith(Element) || Element.StartsWith(Code))
                {
                    Result = true;
                    break;
                }
            }

            return Result;
        }
    }
}
