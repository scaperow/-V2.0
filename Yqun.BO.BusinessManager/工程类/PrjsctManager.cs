using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;
using Yqun.Data.DataBase;
using Yqun.Permissions.Common;
using Yqun.Permissions.Runtime;
using Yqun.Common.ContextCache;
using System.Reflection;

namespace Yqun.BO.BusinessManager
{
    public class PrjsctManager : BOBase
    {
        ProjectCatlogManager ProjectCatlogManager = new ProjectCatlogManager();

        public List<Prjsct> QueryPrjscts(String PrjCode)
        {
            List<Prjsct> PrjstLists = new List<Prjsct>();

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("select b.ID,a.NodeCode,b.Description,b.PegFrom,b.PegTo,b.Price from sys_engs_Tree as a inner join sys_engs_SectionInfo as b on a.RalationID = b.ID and a.Scdel=0 and a.NodeCode like '");
            Sql_Select.Append(PrjCode);
            Sql_Select.Append("%' LEFT JOIN dbo.Sys_Tree c ON a.NodeCode=c.NodeCode order by c.OrderID ");

            DataTable Data = GetDataTable(Sql_Select.ToString());

            //权限控制
            String TreeID = "6ED9D9CB-117E-4d8c-A63B-0157BD1F9DFD";
            IAuthPolicy AuthPolicy = AuthManager.GetTreeAuth(TreeID);//获取树权限
            DataTable NewData = null;

            if (Data != null)
            {
                if (!ApplicationContext.Current.IsAdministrator)
                {
                    NewData = Data.Clone();

                    foreach (DataRow Row in Data.Rows)
                    {
                        String Code = Row["NodeCode"].ToString();
                        if (AuthPolicy.HasAuth(Code))
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

            if (NewData != null && NewData.Rows.Count > 0)
            {
                foreach (DataRow Row in NewData.Rows)
                {
                    Prjsct PrjstInfo = new Prjsct();
                    PrjstInfo.Index = Row["ID"].ToString();
                    PrjstInfo.PrjsctCode = Row["NodeCode"].ToString();
                    PrjstInfo.PrjsctName = Row["Description"].ToString();
                    PrjstInfo.PegFrom = Row["PegFrom"].ToString();
                    PrjstInfo.PegTo = Row["PegTo"].ToString();

                    PrjstLists.Add(PrjstInfo);
                }
            }

            return PrjstLists;
        }

        public Prjsct QueryQueryPrjsct(String PrjsctCode)
        {
            Prjsct PrjstInfo = new Prjsct();
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("Select ID,Description,PegFrom,PegTo,Price from sys_engs_SectionInfo where Id in (Select RalationID from sys_engs_Tree where Scdel=0 and NodeCode ='");
            Sql_Select.Append(PrjsctCode);
            Sql_Select.Append("')");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow Row = Data.Rows[0];
                PrjstInfo.Index = Row["ID"].ToString();
                PrjstInfo.PrjsctName = Row["Description"].ToString();
                PrjstInfo.PegFrom = Row["PegFrom"].ToString();
                PrjstInfo.PegTo = Row["PegTo"].ToString();
                PrjstInfo.PrjsctCode = PrjsctCode;
                DataTable dtTree = GetDataTable("SELECT * FROM dbo.Sys_Tree where nodecode='" + PrjsctCode + "'");
                if (dtTree != null && dtTree.Rows.Count > 0)
                {
                    PrjstInfo.OrderID = dtTree.Rows[0]["OrderID"].ToString();
                }
            }

            return PrjstInfo;
        }

        public Boolean HavePrjstInfo(String PrjsctName)
        {
            StringBuilder Sql_Select = new StringBuilder();

            // 增加查询条件 Scdel=0  2013-10-17
            Sql_Select.Append("Select ID from sys_engs_SectionInfo where Scdel=0 and Description = '");
            Sql_Select.Append(PrjsctName);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            return (Data != null && Data.Rows.Count > 0);
        }

        /// <summary>
        /// 新建标段信息
        /// </summary>
        /// <param name="PrjstInfo"></param>
        /// <returns></returns>
        public Boolean NewPrjstInfo(Prjsct PrjstInfo)
        {
            Boolean Result = false;

            IDbConnection DbConnection = GetConntion();
            Transaction Transaction = new Transaction(DbConnection);

            try
            {
                //工程结构树
                StringBuilder Sql_Select = new StringBuilder();
                //增加查询条件  Scdel=0  2013-10-17
                Sql_Select.Append("Select ID,NodeCode,NodeType,RalationID From sys_engs_Tree Where Scdel=0 and RalationID='");
                Sql_Select.Append(PrjstInfo.Index);
                Sql_Select.Append("'");

                DataTable Data = GetDataTable(Sql_Select.ToString());
                if (Data != null && Data.Rows.Count == 0)
                {
                    DataRow Row = Data.NewRow();
                    Row["ID"] = Guid.NewGuid().ToString();
                    Row["NodeCode"] = PrjstInfo.PrjsctCode;
                    Row["NodeType"] = "@tenders";
                    Row["RalationID"] = PrjstInfo.Index;
                    Data.Rows.Add(Row);
                }

                object r = Update(Data, Transaction);
                Result = (Convert.ToInt32(r) == 1);

                //处理标段表                    
                Sql_Select = new StringBuilder();
                // 增加查询条件 Scdel=0  2013-10-17
                Sql_Select.Append("select ID,Description,PegFrom,PegTo,Price from sys_engs_SectionInfo where Scdel=0 and ID ='");
                Sql_Select.Append(PrjstInfo.Index);
                Sql_Select.Append("'");

                Data = GetDataTable(Sql_Select.ToString());
                if (Data != null && Data.Rows.Count == 0)
                {
                    DataRow Row = Data.NewRow();
                    Row["ID"] = PrjstInfo.Index;
                    Row["Description"] = PrjstInfo.PrjsctName;
                    Row["PegFrom"] = PrjstInfo.PegFrom;
                    Row["PegTo"] = PrjstInfo.PegTo;
                    Data.Rows.Add(Row);
                }

                r = Update(Data, Transaction);
                Result = Result & (Convert.ToInt32(r) == 1);

                if (Result)
                {
                    Transaction.Commit();
                }
                else
                {
                    Transaction.Rollback();
                }
            }
            catch
            {
                Transaction.Rollback();
            }
            PrjstInfo.OrderID = PrjstInfo.PrjsctCode;
            ProjectManager project = new ProjectManager();
            project.SyncSysTree(PrjstInfo.PrjsctCode, PrjstInfo.PrjsctName, "@tenders", PrjstInfo.OrderID, false);

            return Result;
        }

        /// <summary>
        /// 删除标段信息
        /// </summary>
        /// <param name="PrjsctCode"></param>
        /// <returns></returns>
        public Boolean DeletePrjsctInfo(string PrjsctCode, string PrjsctId)
        {
            Boolean Result = false;

            List<string> SheetList = ProjectCatlogManager.GetModuleTables(PrjsctCode);

            IDbConnection DbConnection = GetConntion();
            Transaction Transaction = new Transaction(DbConnection);

            try
            {
                List<String> sql_Commands = new List<string>();

                //工程结构树
                StringBuilder Sql_Delete = new StringBuilder();
                //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-17
                //Sql_Delete.Append("Delete From sys_engs_Tree Where NodeCode like '");
                //Sql_Delete.Append(PrjsctCode);
                //Sql_Delete.Append("%'");
                Sql_Delete.Append("Update sys_engs_Tree Set Scts_1=Getdate(),Scdel=1 Where NodeCode like '");
                Sql_Delete.Append(PrjsctCode);
                Sql_Delete.Append("%'");

                sql_Commands.Add(Sql_Delete.ToString());

                //标段相关数据
                Sql_Delete = new StringBuilder();
                //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-17
                //Sql_Delete.Append("Delete From sys_engs_SectionInfo Where ID  = '");
                //Sql_Delete.Append(PrjsctId);
                //Sql_Delete.Append("'");
                Sql_Delete.Append("Update sys_engs_SectionInfo Set Scts_1=Getdate(),Scdel=1 Where ID  = '");
                Sql_Delete.Append(PrjsctId);
                Sql_Delete.Append("'");

                sql_Commands.Add(Sql_Delete.ToString());

                //处理相关模板表的数据
                foreach (String SheetName in SheetList)
                {
                    Sql_Delete = new StringBuilder();
                    Sql_Delete.Append("Delete From ");
                    Sql_Delete.Append(SheetName);
                    Sql_Delete.Append(" where SCPT like '");
                    Sql_Delete.Append(PrjsctCode);
                    Sql_Delete.Append("%'");

                    sql_Commands.Add(Sql_Delete.ToString());
                }

                object r = ExcuteCommands(sql_Commands.ToArray(), Transaction);
                int[] ints = (int[])r;
                for (int i = 0; i < ints.Length; i++)
                {
                    if (i != 0)
                    {
                        Result = Result & (Convert.ToInt32(ints[i]) == 1);
                    }
                    else
                    {
                        Result = (Convert.ToInt32(ints[i]) == 1);
                    }
                }

                if (Result)
                {
                    Transaction.Commit();
                }
                else
                {
                    Transaction.Rollback();
                }
            }
            catch
            {
                Transaction.Rollback();
            }
            ProjectManager project = new ProjectManager();
            project.SyncSysTree(PrjsctCode, "", "@tenders", "", true);

            return Result;
        }

        /// <summary>
        /// 更新标段信息
        /// </summary>
        /// <param name="ProjectInfo"></param>
        /// <returns></returns>
        public Boolean UpdatePrjsctInfo(Prjsct PrjstInfo)
        {
            Boolean Result = false;
            try
            {
                //工程结构树
                StringBuilder Sql_Select = new StringBuilder();
                // 增加查询条件 Scdel=0  2013-10-17
                Sql_Select.Append("select Id,Description,PegFrom,PegTo,Price,Scts_1 from sys_engs_SectionInfo where Scdel=0 and ID ='");
                Sql_Select.Append(PrjstInfo.Index);
                Sql_Select.Append("'");

                DataTable Data = GetDataTable(Sql_Select.ToString());
                if (Data != null && Data.Rows.Count > 0)
                {
                    DataRow Row = Data.Rows[0];
                    Row["Description"] = PrjstInfo.PrjsctName;
                    Row["PegFrom"] = PrjstInfo.PegFrom;
                    Row["PegTo"] = PrjstInfo.PegTo;
                    Row["Scts_1"] = DateTime.Now.ToString();
                }

                object r = Update(Data);
                Result = (Convert.ToInt32(r) == 1);

                ProjectManager project = new ProjectManager();
                project.SyncSysTree(PrjstInfo.PrjsctCode, PrjstInfo.PrjsctName, "@tenders", PrjstInfo.OrderID, false);

            }
            catch
            { }

            return Result;
        }
    }
}
