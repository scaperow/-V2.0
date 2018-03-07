using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using Yqun.Data.DataBase;
using System.Data;
using Yqun.Permissions.Runtime;
using Yqun.Permissions.Common;
using Yqun.Common.ContextCache;
using System.Reflection;

namespace Yqun.BO.BusinessManager
{
    public class ProjectManager : BOBase
    {
        ProjectCatlogManager ProjectCatlog = new ProjectCatlogManager();

        /// <summary>
        /// 查询所有工程信息
        /// </summary>
        /// <returns></returns>
        public List<Project> QueryProjects()
        {
            List<Project> ProjectsLists = new List<Project>();

            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("select b.ID,a.NodeCode,b.Description,b.PegFrom,b.PegTo,b.LineName,b.HigWayClassification,b.ToltalPrice,b.TotalDistance from sys_engs_Tree as a inner join sys_engs_ProjectInfo as b on a.RalationID = b.ID order by a.NodeCode");

            DataTable Data = GetDataTable(Sql_Select.ToString());

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
                    Project ProjectInfo = new Project();
                    ProjectInfo.Index = Row["Id"].ToString();
                    ProjectInfo.Code = Row["NodeCode"].ToString();
                    ProjectInfo.Description = Row["Description"].ToString();
                    ProjectInfo.PegFrom = Row["PegFrom"].ToString();
                    ProjectInfo.PegTo = Row["PegTo"].ToString();
                    ProjectInfo.LineName = Row["LineName"].ToString();
                    ProjectInfo.HigWayClassification = Row["HigWayClassification"].ToString();
                    ProjectInfo.ToltalPrice = Convert.ToDouble(Row["ToltalPrice"].ToString());
                    ProjectInfo.TotalDistance = Convert.ToDouble(Row["TotalDistance"].ToString());
                    ProjectsLists.Add(ProjectInfo);
                }
            }

            return ProjectsLists;
        }

        public Project QueryProject(String PrjCode)
        {
            try
            {
                Project ProjectInfo = new Project();
                StringBuilder Sql_Select = new StringBuilder();
                //增加查询条件  Scdel=0  2013-10-17
                Sql_Select.Append("Select Id,Description,PegFrom,PegTo,LineName,HigWayClassification,ToltalPrice,TotalDistance from sys_engs_ProjectInfo where Id in (Select RalationID from sys_engs_Tree where Scdel=0 and NodeCode ='");
                Sql_Select.Append(PrjCode);
                Sql_Select.Append("')");

                DataTable Data = GetDataTable(Sql_Select.ToString());
                if (Data != null && Data.Rows.Count > 0)
                {
                    DataRow Row = Data.Rows[0];
                    ProjectInfo.Index = Row["Id"].ToString();
                    ProjectInfo.Description = Row["Description"].ToString();
                    ProjectInfo.PegFrom = Row["PegFrom"].ToString();
                    ProjectInfo.PegTo = Row["PegTo"].ToString();
                    ProjectInfo.LineName = Row["LineName"].ToString();
                    ProjectInfo.HigWayClassification = Row["HigWayClassification"].ToString();
                    ProjectInfo.ToltalPrice = Convert.ToDouble(Row["ToltalPrice"].ToString());
                    ProjectInfo.TotalDistance = Convert.ToDouble(Row["TotalDistance"].ToString());
                    ProjectInfo.Code = PrjCode;
                    DataTable dtTree = GetDataTable("SELECT * FROM dbo.Sys_Tree where nodecode='" + PrjCode + "'");
                    if (dtTree != null && dtTree.Rows.Count > 0)
                    {
                        ProjectInfo.OrderID = dtTree.Rows[0]["OrderID"].ToString();
                    }
                }

                return ProjectInfo;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 判断是否存在工程信息
        /// </summary>
        /// <param name="PrjCode"></param>
        /// <returns></returns>
        public Boolean HaveProjectInfo(String PrjCode)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("Select Id from sys_engs_ProjectInfo where Id in (Select RalationID from sys_engs_Tree where Scdel=0 and NodeCode ='");
            Sql_Select.Append(PrjCode);
            Sql_Select.Append("')");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            return (Data != null && Data.Rows.Count > 0);
        }

        /// <summary>
        /// 新建工程信息
        /// </summary>
        /// <param name="ProjectInfo"></param>
        /// <returns></returns>
        public Boolean NewProject(Project ProjectInfo)
        {
            Boolean Result = false;

            IDbConnection DbConnection = GetConntion();
            Transaction Transaction = new Transaction(DbConnection);

            try
            {
                //工程结构树
                StringBuilder Sql_Select = new StringBuilder();
                //增加查询条件  Scdel=0  2013-10-17
                Sql_Select.Append("Select Id,NodeCode,NodeType,RalationID From sys_engs_Tree Where Scdel=0 and RalationID='");
                Sql_Select.Append(ProjectInfo.Index);
                Sql_Select.Append("'");

                DataTable Data = GetDataTable(Sql_Select.ToString());
                if (Data != null && Data.Rows.Count == 0)
                {
                    DataRow Row = Data.NewRow();
                    Row["ID"] = Guid.NewGuid().ToString();
                    Row["NodeCode"] = ProjectInfo.Code;
                    Row["NodeType"] = "@eng";
                    Row["RalationID"] = ProjectInfo.Index;
                    Data.Rows.Add(Row);
                }

                object r = Update(Data, Transaction);
                Result = (Convert.ToInt32(r) == 1);

                //处理工程表
                Sql_Select = new StringBuilder();
                Sql_Select.Append("Select ID,Description,PegFrom,PegTo,LineName,HigWayClassification,TotalDistance,ToltalPrice From sys_engs_ProjectInfo Where ID='");
                Sql_Select.Append(ProjectInfo.Index);
                Sql_Select.Append("'");

                Data = GetDataTable(Sql_Select.ToString());
                if (Data != null && Data.Rows.Count == 0)
                {
                    DataRow Row = Data.NewRow();
                    Row["ID"] = ProjectInfo.Index;
                    Row["Description"] = ProjectInfo.Description;
                    Row["HigWayClassification"] = ProjectInfo.HigWayClassification;
                    Row["LineName"] = ProjectInfo.LineName;
                    Row["PegFrom"] = ProjectInfo.PegFrom;
                    Row["PegTo"] = ProjectInfo.PegTo;
                    Row["ToltalPrice"] = ProjectInfo.ToltalPrice;
                    Row["TotalDistance"] = ProjectInfo.TotalDistance;

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
            ProjectInfo.OrderID = ProjectInfo.Code;
            SyncSysTree(ProjectInfo.Code, ProjectInfo.Description, "@eng", ProjectInfo.OrderID, false);
            return Result;
        }

        /// <summary>
        /// 删除工程信息
        /// </summary>
        /// <param name="PrjCode"></param>
        /// <returns></returns>
        public Boolean DeleteProject(string ProjCode, string ProjId)
        {
            Boolean Result = false;

            List<String> SheetList = ProjectCatlog.GetModuleTables(ProjCode);

            IDbConnection DbConnection = GetConntion();
            Transaction Transaction = new Transaction(DbConnection);

            try
            {
                List<string> sql_Commands = new List<string>();

                //工程结构树
                StringBuilder Sql_Delete = new StringBuilder();
                //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-17
                //Sql_Delete.Append("Delete From sys_engs_Tree Where NodeCode like '");
                //Sql_Delete.Append(ProjCode);
                //Sql_Delete.Append("%'");
                Sql_Delete.Append("Update sys_engs_Tree Set Scts_1=Getdate(),Scdel=1 Where NodeCode like '");
                Sql_Delete.Append(ProjCode);
                Sql_Delete.Append("%'");

                sql_Commands.Add(Sql_Delete.ToString());

                //工程相关数据
                Sql_Delete = new StringBuilder();
                //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-17
                //Sql_Delete.Append("Delete From sys_engs_ProjectInfo Where ID = '");
                //Sql_Delete.Append(ProjId);
                //Sql_Delete.Append("'");
                Sql_Delete.Append("Update sys_engs_ProjectInfo Set Scts_1=Getdate(),Scdel=1 Where ID = '");
                Sql_Delete.Append(ProjId);
                Sql_Delete.Append("'");

                sql_Commands.Add(Sql_Delete.ToString());

                //处理相关模板表的数据
                //foreach (String SheetName in SheetList)
                //{
                //    Sql_Delete = new StringBuilder();
                //    Sql_Delete.Append("Delete From ");
                //    Sql_Delete.Append(SheetName);
                //    Sql_Delete.Append(" where SCPT like '");
                //    Sql_Delete.Append(ProjCode);
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

            SyncSysTree(ProjCode, "", "@eng", "", true);
            return Result;
        }

        /// <summary>
        /// 更新工程信息
        /// </summary>
        /// <param name="ProjectInfo"></param>
        /// <returns></returns>
        public Boolean UpdateProjectInfo(Project ProjectInfo)
        {
            Boolean Result = false;
            try
            {
                StringBuilder Sql_Select = new StringBuilder();
                //增加查询条件  Scdel=0   2013-10-17
                Sql_Select.Append("Select * from sys_engs_ProjectInfo where Scdel=0 and Id='");
                Sql_Select.Append(ProjectInfo.Index);
                Sql_Select.Append("'");

                DataTable Data = GetDataTable(Sql_Select.ToString());
                if (Data != null && Data.Rows.Count > 0)
                {
                    DataRow Row = Data.Rows[0];
                    Row["Description"] = ProjectInfo.Description;
                    Row["PegFrom"] = ProjectInfo.PegFrom;
                    Row["PegFrom"] = ProjectInfo.PegTo;
                    Row["LineName"] = ProjectInfo.LineName;
                    Row["HigWayClassification"] = ProjectInfo.HigWayClassification;
                    Row["ToltalPrice"] = ProjectInfo.ToltalPrice;
                    Row["TotalDistance"] = ProjectInfo.TotalDistance;
                    Row["Scts_1"] = DateTime.Now.ToString();
                }

                object r = Update(Data);
                Result = (Convert.ToInt32(r) == 1);
                SyncSysTree(ProjectInfo.Code, ProjectInfo.Description, "@eng", ProjectInfo.OrderID, false);
            }
            catch
            { }

            return Result;
        }

        public bool SyncSysTree(string NodeCode, string Description, string DepType, string OrderID, bool IsDelete)
        {
            string strSQL = string.Empty;
            if (IsDelete == true)
                strSQL = string.Format(@"DELETE FROM dbo.Sys_Tree WHERE NodeCode='{0}';", NodeCode);
            else
                strSQL = string.Format(@"DELETE FROM dbo.Sys_Tree WHERE NodeCode='{0}';INSERT INTO dbo.Sys_Tree(NodeCode,DESCRIPTION,DepType,OrderID)VALUES('{0}','{1}','{2}','{3}');", NodeCode, Description, DepType, OrderID);
            ExcuteCommand(strSQL);
            return true;
        }
    }

}
