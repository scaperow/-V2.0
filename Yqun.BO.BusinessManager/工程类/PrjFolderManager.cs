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
    public class PrjFolderManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ProjectCatlogManager ProjectCatlogManager = new ProjectCatlogManager();

        public List<PrjFolder> QueryFolders(string OrgInfoId)
        {
            List<PrjFolder> FolderList = new List<PrjFolder>();

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0     2013-10-17
            Sql_Select.Append("select NodeCode,NodeType,RalationID from sys_engs_Tree where Scdel=0 and RalationID = '");
            Sql_Select.Append(OrgInfoId);
            Sql_Select.Append("'");

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
                string PrjsctCode = NewData.Rows[0]["NodeCode"].ToString();
                FolderList = QueryPrjFolders(PrjsctCode, "@folder");
            }

            return FolderList;
        }

        /// <summary>
        /// 查找单位下面所有的文件夹
        /// </summary>
        /// <param name="DepCode"></param>
        /// <returns></returns>
        public List<PrjFolder> QueryPrjFolders(String OrgInfoCode)
        {
            return QueryPrjFolders(OrgInfoCode, "");
        }

        /// <summary>
        /// 查找单位下某种文件夹
        /// </summary>
        /// <param name="DepCode"></param>
        /// <param name="MachineType"></param>
        /// <returns></returns>
        public List<PrjFolder> QueryPrjFolders(String OrgInfoCode, String FolderType)
        {
            List<PrjFolder> FolderList = new List<PrjFolder>();
            StringBuilder Sql_Select = new StringBuilder();
            if (FolderType == "")
            {
                //增加查询条件  a.Scdel=0  2013-10-17
                Sql_Select.Append("select b.ID,a.NodeCode,b.Description,b.ItemType from sys_engs_Tree as a inner join sys_engs_ItemInfo as b on a.RalationID = b.ID and a.NodeType = b.ItemType and a.Scdel=0 and a.NodeCode like '");
                Sql_Select.Append(OrgInfoCode);
                Sql_Select.Append("%' LEFT JOIN dbo.Sys_Tree c ON a.NodeCode=c.NodeCode order by c.OrderID");
            }
            else
            {
                //增加查询条件  Scdel=0  2013-10-17
                Sql_Select.Append("select b.ID,a.NodeCode,b.Description,b.ItemType from sys_engs_Tree as a inner join sys_engs_ItemInfo as b on a.RalationID = b.ID and a.NodeType = b.ItemType and a.Scdel=0 and a.NodeCode like '");
                Sql_Select.Append(OrgInfoCode);
                Sql_Select.Append("%' and a.NodeType ='");
                Sql_Select.Append(FolderType);
                Sql_Select.Append("' LEFT JOIN dbo.Sys_Tree c ON a.NodeCode=c.NodeCode order by c.OrderID");
            }

            //logger.Error(Sql_Select.ToString());

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    PrjFolder FolderInfo = new PrjFolder();
                    FolderInfo.Index = Row["ID"].ToString();
                    FolderInfo.FolderCode = Row["NodeCode"].ToString();
                    FolderInfo.FolderName = Row["Description"].ToString();
                    FolderInfo.FolderType = Row["ItemType"].ToString();
                    FolderList.Add(FolderInfo);
                }

            }

            return FolderList;
        }

        /// <summary>
        /// 是否存在某个文件夹
        /// </summary>
        /// <param name="DepCode"></param>
        /// <param name="MachineId"></param>
        /// <returns></returns>
        public Boolean HaveFolderInfo(String OrgInfoCode, String FolderId)
        {
            StringBuilder Sql_Select = new StringBuilder();

            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("select RalationID from sys_engs_Tree where Scdel=0 and (NodeCode like'");
            Sql_Select.Append(OrgInfoCode);
            Sql_Select.Append("%' and RalationID = '");
            Sql_Select.Append(FolderId);
            Sql_Select.Append("')");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            return (Data != null && Data.Rows.Count > 0);
        }

        /// <summary>
        /// 获得某个文件夹
        /// </summary>
        /// <param name="MachineCode"></param>
        /// <returns></returns>
        public PrjFolder QueryPrjFolder(String FolderCode)
        {
            PrjFolder FolderInfo = new PrjFolder();

            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("select b.ID,a.NodeCode,b.Description,b.ItemType from sys_engs_Tree as a inner join sys_engs_ItemInfo as b on a.RalationID = b.ID and a.Scdel=0 and a.NodeCode = '");
            Sql_Select.Append(FolderCode);
            Sql_Select.Append("' ");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow Row = Data.Rows[0];
                FolderInfo.Index = Row["ID"].ToString();
                FolderInfo.FolderCode = Row["NodeCode"].ToString();
                FolderInfo.FolderName = Row["Description"].ToString();
                FolderInfo.FolderType = Row["ItemType"].ToString();

                logger.Error("QueryPrjFolder FolderCode:" + FolderCode);
                DataTable dtTree = GetDataTable("SELECT * FROM dbo.Sys_Tree where nodecode='" + FolderCode + "'");
                if (dtTree != null && dtTree.Rows.Count > 0)
                {
                    FolderInfo.OrderID = dtTree.Rows[0]["OrderID"].ToString();
                }
                logger.Error("QueryPrjFolder FolderInfo.OrderID:" + FolderInfo.OrderID);
            }

            return FolderInfo;
        }

        public Boolean ToUsePrjFolder(String FolderId)
        {
            return ToUsePrjFolder(FolderId, "");
        }

        public Boolean ToUsePrjFolder(String FolderId, String PrjsctCode)
        {
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            Sql_Select.Append("Select Id,NodeCode,NodeType,RalationID From sys_engs_Tree Where Scdel=0 and RalationID='");
            Sql_Select.Append(FolderId);
            Sql_Select.Append("'");
            if (PrjsctCode != "")
            {
                Sql_Select.Append(" and NodeCode <> '");
                Sql_Select.Append(PrjsctCode);
                Sql_Select.Append("'");
            }

            DataTable Data = GetDataTable(Sql_Select.ToString());
            return (Data != null && Data.Rows.Count > 0);
        }

        public Boolean NewPrjFolder(PrjFolder FolderInfo)
        {
            Boolean Result = false;

            IDbConnection Connection = GetConntion();
            Transaction Transaction = new Transaction(Connection);
            Boolean flag = false;
            try
            {
                //工程结构树
                StringBuilder Sql_Nodes = new StringBuilder();
                //增加查询条件  Scdel=0  2013-10-17
                Sql_Nodes.Append("Select Id,NodeCode,NodeType,RalationID From sys_engs_Tree Where Scdel=0 and RalationID='");
                Sql_Nodes.Append(FolderInfo.Index);
                Sql_Nodes.Append("'");

                StringBuilder Sql_Items = new StringBuilder();
                //增加查询条件  Scdel=0  2013-10-17
                Sql_Items.Append("Select Id,Description,ItemType From  sys_engs_ItemInfo Where Scdel=0 and Id='");
                Sql_Items.Append(FolderInfo.Index);
                Sql_Items.Append("'");

                List<string> sql_Commands = new List<string>();
                sql_Commands.Add(Sql_Nodes.ToString());
                sql_Commands.Add(Sql_Items.ToString());

                DataSet dataset = GetDataSet(sql_Commands.ToArray());
                if (dataset != null)
                {
                    DataTable TableNodes = dataset.Tables["sys_engs_Tree"];
                    DataTable TableItems = dataset.Tables["sys_engs_ItemInfo"];

                    if (TableNodes != null && TableNodes.Rows.Count == 0)
                    {
                        DataRow Row = TableNodes.NewRow();
                        Row["ID"] = Guid.NewGuid().ToString();
                        Row["NodeCode"] = FolderInfo.FolderCode;
                        Row["NodeType"] = "@folder";
                        Row["RalationID"] = FolderInfo.Index;
                        TableNodes.Rows.Add(Row);
                    }

                    if (TableItems != null && TableItems.Rows.Count == 0)
                    {
                        DataRow Row = TableItems.NewRow();
                        Row["ID"] = FolderInfo.Index;
                        Row["Description"] = FolderInfo.FolderName;
                        Row["ItemType"] = "@folder";
                        TableItems.Rows.Add(Row);
                    }

                    object r = Update(dataset, Transaction);
                    Result = (Convert.ToInt32(r) == 1);

                    if (Result)
                    {
                        Transaction.Commit();
                        flag = true;
                    }
                    else
                    {
                        Transaction.Rollback();
                    }
                }
            }
            catch
            {
                Transaction.Rollback();
            }
            if (flag)
            {
                try
                {
                    //PXJZDataManager pxjz = new PXJZDataManager();
                    //pxjz.NewByTestRoom(FolderInfo.FolderCode, FolderInfo.Index);

                    FolderInfo.OrderID = FolderInfo.FolderCode;
                    ProjectManager project = new ProjectManager();
                    project.SyncSysTree(FolderInfo.FolderCode, FolderInfo.FolderName, "@folder", FolderInfo.OrderID, false);
                }
                catch
                {
                }
            }
            return Result;
        }

        public Boolean SelectPrjFolder(PrjFolder FolderInfo)
        {
            Boolean Result = false;
            try
            {
                //工程结构树
                StringBuilder Sql_Select = new StringBuilder();
                //增加查询条件  Scdel=0  2013-10-17
                Sql_Select.Append("Select Id,NodeCode,NodeType,RalationID From sys_engs_Tree Where Scdel=0 and RalationID='");
                Sql_Select.Append(FolderInfo.Index);
                Sql_Select.Append("'");

                DataTable Data = GetDataTable(Sql_Select.ToString());
                if (Data != null)
                {
                    DataRow Row = Data.NewRow();
                    Row["ID"] = Guid.NewGuid().ToString();
                    Row["NodeCode"] = FolderInfo.FolderCode;
                    Row["NodeType"] = FolderInfo.FolderType;
                    Row["RalationID"] = FolderInfo.Index;
                    Data.Rows.Add(Row);
                }

                object r = Update(Data);
                Result = (Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public Boolean DeletePrjFolderByCode(string FolderCode)
        {
            return DeletePrjFolder(FolderCode, "");
        }

        public Boolean DeletePrjFolder(string FolderCode, string FolderId)
        {
            Boolean Result = false;
            List<string> SheetList = ProjectCatlogManager.GetModuleTables(FolderCode);

            IDbConnection Connection = GetConntion();
            Transaction Transaction = new Transaction(Connection);
            Boolean flag = false;
            try
            {

                List<string> sql_Commands = new List<string>();

                //工程结构树
                StringBuilder Sql_Delete = new StringBuilder();
                //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-17
                //Sql_Delete.Append("Delete From sys_engs_Tree Where NodeCode like '");
                //Sql_Delete.Append(FolderCode);
                //Sql_Delete.Append("%'");
                Sql_Delete.Append("Update sys_engs_Tree Set Scts_1=Getdate(),Scdel=1 Where NodeCode like '");
                Sql_Delete.Append(FolderCode);
                Sql_Delete.Append("%'");

                sql_Commands.Add(Sql_Delete.ToString());

                if (FolderId != "")
                {
                    //删除机组
                    Sql_Delete = new StringBuilder();
                    //增加查询条件  Scdel=0  2013-10-17
                    //Sql_Delete.Append("Delete From sys_engs_ItemInfo Where Id = '");
                    //Sql_Delete.Append(FolderId);
                    //Sql_Delete.Append("'");
                    Sql_Delete.Append("update sys_engs_ItemInfo Set Scts_1=Getdate(),Scdel=1 Where Id = '");
                    Sql_Delete.Append(FolderId);
                    Sql_Delete.Append("'");

                    sql_Commands.Add(Sql_Delete.ToString());
                }

                //处理相关模板表的数据 
                var deleteSql = string.Format("UPDATE sys_document SET Status = 0 where TestRoomCode = '" + FolderCode + "'");
                sql_Commands.Add(deleteSql);

                if (String.IsNullOrEmpty(FolderCode))
                {
                    return false;
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
                    flag = true;
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
            if (flag)
            {
                try
                {
                    //PXJZDataManager pxjz = new PXJZDataManager();
                    //pxjz.DeleteByTestRoomCode(FolderCode);

                    ProjectManager project = new ProjectManager();
                    project.SyncSysTree(FolderCode, "", "@folder", "", true);
                }
                catch
                {
                }
            }
            return Result;
        }

        public Boolean DeletePrjFolderByID(string FolderId)
        {
            Boolean Result = false;

            //删除机组
            StringBuilder Sql_Delete = new StringBuilder();
            //增加查询条件  Scdel=0  2013-10-17
            //Sql_Delete.Append("Delete From sys_engs_ItemInfo Where Id = '");
            //Sql_Delete.Append(FolderId);
            //Sql_Delete.Append("'");
            Sql_Delete.Append("Update sys_engs_ItemInfo Set Scts_1=Getdate(),Scdel=1 Where Id = '");
            Sql_Delete.Append(FolderId);
            Sql_Delete.Append("'");

            object r = ExcuteCommand(Sql_Delete.ToString());
            Result = (Convert.ToInt32(r) == 1);

            return Result;
        }


        public Boolean UpdatePrjFolder(PrjFolder FolderInfo)
        {
            Boolean Result = false;
            try
            {
                //工程结构树
                StringBuilder Sql_Select = new StringBuilder();
                //增加查询条件  Scdel=0  2013-10-17
                Sql_Select.Append("Select Id,Description,ItemType,Scts_1 From  sys_engs_ItemInfo Where Scdel=0 and Id='");
                Sql_Select.Append(FolderInfo.Index);
                Sql_Select.Append("'");

                DataTable Data = GetDataTable(Sql_Select.ToString());
                if (Data != null && Data.Rows.Count > 0)
                {
                    DataRow Row = Data.Rows[0];
                    Row["Description"] = FolderInfo.FolderName;
                    Row["ItemType"] = FolderInfo.FolderType;

                    //增加条件  Scts_1=Getdate()  2013-10-17
                    Row["Scts_1"] = DateTime.Now.ToString();
                }

                object r = Update(Data);
                Result = (Convert.ToInt32(r) == 1);

                ProjectManager project = new ProjectManager();
                project.SyncSysTree(FolderInfo.FolderCode, FolderInfo.FolderName, "@folder", FolderInfo.OrderID, false);
            }
            catch (Exception ex)
            {
                logger.Error("UpdatePrjFolder异常:" + ex.Message);
            }

            return Result;
        }
    }
}
