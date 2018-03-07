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
    public class OrgInfoManager : BOBase
    {
        ProjectCatlogManager ProjectCatlogManager = new ProjectCatlogManager();

        public string GetOrgInfoNextCode(string ParentCode)
        {
            List<int> Values = new List<int>();
            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select = new StringBuilder();
            Sql_Select.Append("Select DepCode from sys_engs_orginfo where DepCode like '");
            Sql_Select.Append(ParentCode);
            Sql_Select.Append("____'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string Value = row["DepCode"].ToString();
                    int Int = int.Parse(Value.Remove(0, ParentCode.Length));
                    Values.Add(Int);
                }
            }

            int i = 1;
            while (Values.Contains(i)) ++i;
            return ParentCode + i.ToString("0000");
        }

        public List<Orginfo> QueryOrgans(String PrjsctCode, String DepType)
        {
            List<Orginfo> OrginfoLists = new List<Orginfo>();

            StringBuilder Sql_Select = new StringBuilder();
            if (!string.IsNullOrEmpty(PrjsctCode))
            {
                Sql_Select.Append("select b.ID,a.NodeCode,b.Description,b.DepType,b.DepAbbrev,b.ConstructionCompany from sys_engs_Tree as a inner join sys_engs_CompanyInfo as b on a.RalationID = b.ID and a.NodeType = b.DepType and a.NodeCode like '");
                Sql_Select.Append(PrjsctCode);
                Sql_Select.Append("%'");
                if (!string.IsNullOrEmpty(DepType))
                {
                    Sql_Select.Append(" and a.NodeType ='");
                    Sql_Select.Append(DepType);
                    Sql_Select.Append("'");
                }
                //增加查询条件  a.Scdel=0  2013-10-17
                Sql_Select.Append(" and a.Scdel=0 LEFT JOIN dbo.Sys_Tree c ON a.NodeCode=c.NodeCode order by c.OrderID");

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

                if (NewData != null)
                {
                    foreach (DataRow Row in NewData.Rows)
                    {
                        Orginfo OrgInfo = new Orginfo();
                        OrgInfo.Index = Row["ID"].ToString();
                        OrgInfo.DepCode = Row["NodeCode"].ToString();
                        OrgInfo.DepName = Row["Description"].ToString();
                        OrgInfo.DepType = Row["DepType"].ToString();
                        OrgInfo.DepAbbrev = Row["DepAbbrev"].ToString();
                        OrgInfo.ConstructionCompany = Row["ConstructionCompany"].ToString();
                        OrginfoLists.Add(OrgInfo);
                    }
                }
            }
            else
            {
                //增加查询条件  Scdel=0  2013-10-17
                Sql_Select.Append("select ID,Description,DepType,DepAbbrev,ConstructionCompany from sys_engs_CompanyInfo ");
                if (!string.IsNullOrEmpty(DepType))
                {
                    Sql_Select.Append(" where Scdel=0 and DepType='");
                    Sql_Select.Append(DepType);
                    Sql_Select.Append("'");
                }

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
                        Orginfo OrgInfo = new Orginfo();
                        OrgInfo.Index = Row["ID"].ToString();
                        OrgInfo.DepName = Row["Description"].ToString();
                        OrgInfo.DepType = Row["DepType"].ToString();
                        OrgInfo.DepAbbrev = Row["DepAbbrev"].ToString();
                        OrgInfo.ConstructionCompany = Row["ConstructionCompany"].ToString();
                        OrginfoLists.Add(OrgInfo);
                    }
                }
            }
            return OrginfoLists;
        }

        public List<Orginfo> QueryOrgansTree(String PrjsctCode, String DepType)
        {
            List<Orginfo> OrginfoLists = new List<Orginfo>();

            StringBuilder Sql_Select = new StringBuilder();
            if (!string.IsNullOrEmpty(PrjsctCode) && !string.IsNullOrEmpty(DepType))
            {
                //增加查询条件  Scdel=0     2013-10-17
                Sql_Select.Append("select b.ID,a.NodeCode,b.Description,a.NodeType as DepType,b.DepAbbrev,b.ConstructionCompany from sys_engs_Tree as a inner join sys_engs_CompanyInfo as b on a.RalationID = b.ID  and a.Scdel=0 and a.NodeCode like '");
                Sql_Select.Append(PrjsctCode);
                Sql_Select.Append("%' and a.NodeType like'");
                Sql_Select.Append(DepType);
                Sql_Select.Append("%' order by a.NodeCode");

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
                        Orginfo OrgInfo = new Orginfo();
                        OrgInfo.Index = Row["ID"].ToString();
                        OrgInfo.DepCode = Row["NodeCode"].ToString();
                        OrgInfo.DepName = Row["Description"].ToString();
                        OrgInfo.DepType = Row["DepType"].ToString();
                        OrgInfo.DepAbbrev = Row["DepAbbrev"].ToString();
                        OrgInfo.ConstructionCompany = Row["ConstructionCompany"].ToString();
                        OrginfoLists.Add(OrgInfo);
                    }
                }
            }
            else
            {
                //增加查询条件  Scdel=0  2013-10-17
                Sql_Select.Append("select ID,Description,DepType,DepAbbrev,ConstructionCompany from sys_engs_CompanyInfo Where Scdel=0 ");

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
                        Orginfo OrgInfo = new Orginfo();
                        OrgInfo.Index = Row["ID"].ToString();
                        OrgInfo.DepName = Row["Description"].ToString();
                        OrgInfo.DepType = Row["DepType"].ToString();
                        OrgInfo.DepAbbrev = Row["DepAbbrev"].ToString();
                        OrgInfo.ConstructionCompany = Row["ConstructionCompany"].ToString();
                        OrginfoLists.Add(OrgInfo);
                    }
                }
            }

            return OrginfoLists;
        }

        public List<Orginfo> QueryMultimediaOrgans(String PrjCode)
        {
            List<Orginfo> OrginfoLists = new List<Orginfo>();

            StringBuilder Sql_Select = new StringBuilder();
            if (!string.IsNullOrEmpty(PrjCode))
            {
                //增加查询条件  Scdel=0     2013-10-17
                Sql_Select.Append("select b.ID,a.NodeCode,b.Description,b.DepType,b.DepAbbrev,b.ConstructionCompany from sys_engs_Tree as a inner join sys_engs_CompanyInfo as b on a.RalationID = b.ID and a.NodeType = b.DepType and a.Scdel=0 and a.NodeCode like '");
                Sql_Select.Append(PrjCode);
                Sql_Select.Append("%' order by a.NodeCode");

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
                        Orginfo OrgInfo = new Orginfo();
                        OrgInfo.Index = Row["ID"].ToString();
                        OrgInfo.DepCode = Row["NodeCode"].ToString();
                        OrgInfo.DepName = Row["Description"].ToString();
                        OrgInfo.DepType = Row["DepType"].ToString();
                        OrgInfo.DepAbbrev = Row["DepAbbrev"].ToString();
                        OrgInfo.ConstructionCompany = Row["ConstructionCompany"].ToString();
                        OrginfoLists.Add(OrgInfo);
                    }
                }
            }
            else
            {
                //增加查询条件  Scdel=0  2013-10-17
                Sql_Select.Append("select ID,Description,DepType,DepAbbrev,ConstructionCompany from sys_engs_CompanyInfo Where Scdel=0 ");

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
                        Orginfo OrgInfo = new Orginfo();
                        OrgInfo.Index = Row["ID"].ToString();
                        OrgInfo.DepName = Row["Description"].ToString();
                        OrgInfo.DepType = Row["DepType"].ToString();
                        OrgInfo.DepAbbrev = Row["DepAbbrev"].ToString();
                        OrgInfo.ConstructionCompany = Row["ConstructionCompany"].ToString();
                        OrginfoLists.Add(OrgInfo);
                    }
                }
            }
            return OrginfoLists;
        }

        /// <summary>
        /// 判断是否包含单位信息
        /// </summary>
        /// <param name="DepCode"></param>
        /// <returns></returns>
        public Boolean HaveOrganInfo(String Description)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("Select Id From sys_engs_CompanyInfo Where Scdel=0 and Description='");
            Sql_Select.Append(Description);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            return (Data != null && Data.Rows.Count > 0);
        }

        /// <summary>
        /// 判断是否包含单位信息
        /// </summary>
        /// <param name="DepCode"></param>
        /// <returns></returns>
        public Boolean HaveOrganInfo(String DepId, String DepCode)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0     2013-10-17
            Sql_Select.Append("Select Id,NodeCode,NodeType,RalationID From sys_engs_Tree Where Scdel=0 and RalationID='");
            Sql_Select.Append(DepId);
            Sql_Select.Append("' and NodeCode like '");
            Sql_Select.Append(DepCode);
            Sql_Select.Append("%'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            return (Data != null && Data.Rows.Count > 0);
        }
        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <param name="DepCode"></param>
        /// <returns></returns>
        public Orginfo QueryOrginfo(String DepId)
        {
            Orginfo OrgInfo = new Orginfo();
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("Select Id,Description,DepType,DepAbbrev,ConstructionCompany From sys_engs_CompanyInfo Where Scdel=0 and Id='");
            Sql_Select.Append(DepId);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow Row = Data.Rows[0];
                OrgInfo.Index = Row["Id"].ToString();
                OrgInfo.DepName = Row["Description"].ToString();
                OrgInfo.DepType = Row["DepType"].ToString();
                OrgInfo.DepAbbrev = Row["DepAbbrev"].ToString();
                OrgInfo.ConstructionCompany = Row["ConstructionCompany"].ToString();

                DataTable dtTree = GetDataTable("SELECT a.NodeCode,b.OrderID FROM dbo.sys_engs_Tree a LEFT JOIN dbo.Sys_Tree b ON a.NodeCode=b.NodeCode WHERE RalationID='" + DepId + "' AND Scdel=0");
                if (dtTree != null && dtTree.Rows.Count > 0)
                {
                    OrgInfo.DepCode = dtTree.Rows[0]["nodecode"].ToString();
                    OrgInfo.OrderID = dtTree.Rows[0]["OrderID"].ToString();
                }
            }

            return OrgInfo;
        }

        public Boolean SelectOrginfo(Orginfo OrgInfo)
        {
            Boolean Result = false;

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0     2013-10-17
            Sql_Select.Append("Select Id,NodeCode,NodeType,RalationID From sys_engs_Tree Where Scdel=0 and RalationID='");
            Sql_Select.Append(OrgInfo.Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null)
            {
                DataRow Row = Data.NewRow();
                Row["ID"] = Guid.NewGuid().ToString();
                Row["NodeCode"] = OrgInfo.DepCode;
                Row["NodeType"] = OrgInfo.DepType;
                Row["RalationID"] = OrgInfo.Index;
                Data.Rows.Add(Row);
            }

            try
            {
                object r = Update(Data);
                Result = (Convert.ToInt32(r) == 1);
            }
            catch
            { }

            OrgInfo.OrderID = OrgInfo.DepCode;
            ProjectManager project = new ProjectManager();
            project.SyncSysTree(OrgInfo.DepCode, OrgInfo.DepName, OrgInfo.DepType, OrgInfo.OrderID, false);

            return Result;
        }

        /// <summary>
        /// 新建单位
        /// </summary>
        /// <param name="OrgInfo"></param>
        /// <returns></returns>
        public Boolean NewOrginfo(Orginfo OrgInfo)
        {
            Boolean Result = false;

            try
            {
                //单位表                    
                StringBuilder Sql_Select = new StringBuilder();
                //增加查询条件  Scdel=0  2013-10-17
                Sql_Select.Append("Select Id,Description,DepType,DepAbbrev,ConstructionCompany From sys_engs_CompanyInfo where Scdel=0 and Id='");
                Sql_Select.Append(OrgInfo.Index);
                Sql_Select.Append("'");

                DataTable Data = GetDataTable(Sql_Select.ToString());
                if (Data != null && Data.Rows.Count == 0)
                {
                    DataRow Row = Data.NewRow();
                    Row["ID"] = OrgInfo.Index;
                    Row["Description"] = OrgInfo.DepName;
                    Row["DepType"] = OrgInfo.DepType;
                    Row["DepAbbrev"] = OrgInfo.DepAbbrev;
                    Row["ConstructionCompany"] = OrgInfo.ConstructionCompany;
                    Data.Rows.Add(Row);

                    object r = Update(Data);
                    Result = (Convert.ToInt32(r) == 1);
                }
            }
            catch
            { }

            return Result;
        }

        /// <summary>
        /// 删除单位
        /// </summary>
        /// <param name="DepCode"></param>
        /// <returns></returns>
        public Boolean DeleteOrginfo(string DepCode)
        {
            Boolean Result = false;

            List<String> SheetList = ProjectCatlogManager.GetModuleTables(DepCode);

            IDbConnection DbConnection = GetConntion();
            Transaction Transaction = new Transaction(DbConnection);

            try
            {
                List<String> sql_Commands = new List<string>();

                //工程结构树
                StringBuilder Sql_Delete = new StringBuilder();
                //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-17
                //Sql_Delete.Append("Delete From sys_engs_Tree Where NodeCode like '");
                //Sql_Delete.Append(DepCode);
                //Sql_Delete.Append("%'");
                Sql_Delete.Append("Update sys_engs_Tree Set Scts_1=Getdate(),Scdel=1 Where NodeCode like '");
                Sql_Delete.Append(DepCode);
                Sql_Delete.Append("%'");

                sql_Commands.Add(Sql_Delete.ToString());

                //处理相关模板表的数据
                //foreach (String SheetName in SheetList)
                //{
                //    Sql_Delete = new StringBuilder();
                //    Sql_Delete.Append("Delete From [");
                //    Sql_Delete.Append(SheetName);
                //    Sql_Delete.Append("] where SCPT like '");
                //    Sql_Delete.Append(DepCode);
                //    Sql_Delete.Append("%'");

                //    sql_Commands.Add(Sql_Delete.ToString());
                //}

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
            project.SyncSysTree(DepCode, "", "", "", true);
            return Result;
        }

        public Boolean UpdateOrginfo(Orginfo OrgInfo)
        {
            Boolean Result = false;

            IDbConnection DbConnection = GetConntion();
            Transaction Transaction = new Transaction(DbConnection);

            try
            {
                //单位信息表
                StringBuilder Sql_Select = new StringBuilder();
                //增加查询条件  Scdel=0  2013-10-17
                Sql_Select.Append("Select Id,Description,DepType,DepAbbrev,ConstructionCompany From sys_engs_CompanyInfo where Scdel=0 and Id='");
                Sql_Select.Append(OrgInfo.Index);
                Sql_Select.Append("'");

                DataTable Data = GetDataTable(Sql_Select.ToString());
                if (Data != null && Data.Rows.Count > 0)
                {
                    DataRow Row = Data.Rows[0];
                    Row["Description"] = OrgInfo.DepName;
                    Row["DepType"] = OrgInfo.DepType;
                    Row["DepAbbrev"] = OrgInfo.DepAbbrev;
                    Row["ConstructionCompany"] = OrgInfo.ConstructionCompany;
                }

                object r = Update(Data, Transaction);
                Result = (Convert.ToInt32(r) == 1);

                Sql_Select = new StringBuilder();
                //增加查询条件  Scdel=0     2013-10-17
                Sql_Select.Append("Select Id,NodeCode,NodeType,RalationID From sys_engs_Tree Where Scdel=0 and RalationID='");
                Sql_Select.Append(OrgInfo.Index);
                Sql_Select.Append("'");

                Data = GetDataTable(Sql_Select.ToString());
                if (Data != null && Data.Rows.Count > 0)
                {
                    DataRow Row = Data.Rows[0];
                    Row["NodeType"] = OrgInfo.DepType;
                }

                r = Update(Data, Transaction);
                Result = (Convert.ToInt32(r) == 1);

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
            project.SyncSysTree(OrgInfo.DepCode, OrgInfo.DepName, OrgInfo.DepType, OrgInfo.OrderID, false);

            return Result;
        }

        /// <summary>
        /// 判断单位是不是在使用
        /// </summary>
        /// <param name="DepID"></param>
        /// <returns></returns>
        public Boolean ToUseOrginfo(string DepID)
        {
            return ToUseOrginfo(DepID, "");
        }

        /// <summary>
        /// 判断单位是不是在该标段下存在
        /// </summary>
        /// <param name="DepID"></param>
        /// <returns></returns>
        public Boolean ToUseOrginfo(string DepID, string PrjsctCode)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0     2013-10-17
            Sql_Select.Append("Select Id,NodeCode,NodeType,RalationID From sys_engs_Tree Where Scdel=0 and RalationID='");
            Sql_Select.Append(DepID);
            Sql_Select.Append("'");
            if (PrjsctCode != "")
            {
                Sql_Select.Append(" and ");
                Sql_Select.Append("NodeCode  like '");
                Sql_Select.Append(PrjsctCode);
                Sql_Select.Append("%'");

            }

            DataTable Data = GetDataTable(Sql_Select.ToString());
            return (Data != null && Data.Rows.Count > 0);
        }

        public Boolean DeleteDepName(string DepId)
        {
            //处理单位表 
            Boolean Result = false;

            try
            {
                StringBuilder Sql_Delete = new StringBuilder();
                Sql_Delete = new StringBuilder();
                //Sql_Delete.Append("Delete From  sys_engs_CompanyInfo Where Id = '");
                //Sql_Delete.Append(DepId);
                //Sql_Delete.Append("'");
                // 2013-10-17
                Sql_Delete.Append("Update sys_engs_CompanyInfo Set Scts_1=Getdate(),Scdel=1 Where Id = '");
                Sql_Delete.Append(DepId);
                Sql_Delete.Append("'");

                object r = ExcuteCommand(Sql_Delete.ToString());
                Result = (Convert.ToInt32(r) == 1);

                return Result;
            }
            catch
            { }

            return Result;
        }
    }
}
