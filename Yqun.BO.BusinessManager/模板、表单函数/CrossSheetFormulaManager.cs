using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;
using Yqun.Data.DataBase;
using System.Reflection;

namespace Yqun.BO.BusinessManager
{
    public class CrossSheetFormulaManager : BOBase
    {
        public Boolean HaveCrossSheetFormulaInfoByModel(string ModelIndex)
        {
            StringBuilder sql_Select = new StringBuilder();
            sql_Select.Append("select * from sys_biz_CrossSheetFormulas where ModelIndex = '");
            sql_Select.Append(ModelIndex);
            sql_Select.Append("'");
            //增加查询条件 判断此条记录是否标记为已删除  Scdel=1 代表已删除。  2013-10-15
            sql_Select.Append(" And (Scdel IS NULL or Scdel=0) ");

            DataTable Data = GetDataTable(sql_Select.ToString());
            return (Data != null && Data.Rows.Count > 0);
        }

        public List<CrossSheetFormulaInfo> getCrossSheetFormulaInfoByModel(string ModelIndex)
        {
            List<CrossSheetFormulaInfo> CrossSheetFormulas = new List<CrossSheetFormulaInfo>();

            StringBuilder sql_Select = new StringBuilder();
            sql_Select.Append("select * from sys_biz_CrossSheetFormulas where ModelIndex = '");
            sql_Select.Append(ModelIndex);
            sql_Select.Append("'");
            //增加查询条件 判断此条记录是否标记为已删除  Scdel=1 代表已删除。  2013-10-15
            sql_Select.Append(" And (Scdel IS NULL or Scdel=0) ");

            DataTable Data = GetDataTable(sql_Select.ToString());
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    CrossSheetFormulaInfo FormulaInfo = new CrossSheetFormulaInfo();
                    FormulaInfo.Index = Row["ID"].ToString();
                    FormulaInfo.ModelIndex = Row["ModelIndex"].ToString();
                    FormulaInfo.SheetIndex = Row["SheetIndex"].ToString();
                    FormulaInfo.RowIndex = Convert.ToInt32(Row["RowIndex"]);
                    FormulaInfo.ColumnIndex = Convert.ToInt32(Row["ColumnIndex"]);
                    FormulaInfo.Formula = Row["Formula"].ToString();

                    CrossSheetFormulas.Add(FormulaInfo);
                }
            }

            return CrossSheetFormulas;
        }

        public List<CrossSheetFormulaInfo> getCrossSheetFormulaInfoBySheet(string SheetIndex)
        {
            List<CrossSheetFormulaInfo> CrossSheetFormulas = new List<CrossSheetFormulaInfo>();

            StringBuilder sql_Select = new StringBuilder();
            sql_Select.Append("select * from sys_biz_CrossSheetFormulas where SheetIndex = '");
            sql_Select.Append(SheetIndex);
            sql_Select.Append("'");
            //增加查询条件 判断此条记录是否标记为已删除  Scdel=1 代表已删除。  2013-10-15
            sql_Select.Append(" And (Scdel IS NULL or Scdel=0) ");

            DataTable Data = GetDataTable(sql_Select.ToString());
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    CrossSheetFormulaInfo FormulaInfo = new CrossSheetFormulaInfo();
                    FormulaInfo.Index = Row["ID"].ToString();
                    FormulaInfo.ModelIndex = Row["ModelIndex"].ToString();
                    FormulaInfo.SheetIndex = Row["SheetIndex"].ToString();
                    FormulaInfo.RowIndex = Convert.ToInt32(Row["RowIndex"]);
                    FormulaInfo.ColumnIndex = Convert.ToInt32(Row["ColumnIndex"]);
                    FormulaInfo.Formula = Row["Formula"].ToString();

                    CrossSheetFormulas.Add(FormulaInfo);
                }
            }

            return CrossSheetFormulas;
        }
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Boolean saveCrossSheetFormulaInfos(string ModelIndex, List<CrossSheetFormulaInfo> Infos)
        {
            Boolean Result = false;
            
            try
            {
                //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，便于数据同步     2013-10-15
                StringBuilder Sql_Delete = new StringBuilder();
                //Sql_Delete.Append("delete from sys_biz_CrossSheetFormulas where ModelIndex='");
                //Sql_Delete.Append(ModelIndex);
                //Sql_Delete.Append("'");
                Sql_Delete.Append("Update sys_biz_CrossSheetFormulas Set Scts_1='" + DateTime.Now + "',Scdel=1");
                Sql_Delete.Append(" Where ModelIndex='");
                Sql_Delete.Append(ModelIndex);
                Sql_Delete.Append("'");

                object r = ExcuteCommand(Sql_Delete.ToString());
                Result = (Convert.ToInt32(r) == 1);

                StringBuilder sql_Select = new StringBuilder();
                //增加条件，判断数据是否已删除     2013-10-15
                sql_Select.Append("select * from sys_biz_CrossSheetFormulas where ModelIndex='"+ModelIndex+"'");
                DataTable Data = GetDataTable(sql_Select.ToString());
                
                if (Data != null)
                {
                    DataRow Row;
                    foreach (CrossSheetFormulaInfo Info in Infos)
                    {
                        DataRow[] Rows = Data.Select("ID='" + Info.Index + "'");
                        if (Rows.Length > 0)
                        {
                            Row = Rows[0];
                        }
                        else
                        {
                            Row = Data.NewRow();
                            Data.Rows.Add(Row);
                            Row["ID"] = Info.Index;
                            Row["SCTS"] = DateTime.Now.ToString();
                        }
                        
                        Row["ModelIndex"] = Info.ModelIndex;
                        Row["SheetIndex"] = Info.SheetIndex;
                        Row["RowIndex"] = Info.RowIndex;
                        Row["ColumnIndex"] = Info.ColumnIndex;
                        Row["Formula"] = Info.Formula;
                        Row["Scts_1"] = DateTime.Now.ToString();
                        Row["Scdel"] = 0;
                    }
                    r = Update(Data);
                    Result = (Convert.ToInt32(r) == 1);
                }
            }
            catch(Exception e)
            {
                logger.Error(e.ToString());

            }

            return Result;
        }

        public Boolean saveCrossSheetFormulaInfos(List<CrossSheetFormulaInfo> Infos)
        {
            Boolean Result = false;

            List<string> IndexCollection = new List<string>();
            foreach (CrossSheetFormulaInfo Info in Infos)
            {
                IndexCollection.Add(Info.Index);
            }

            StringBuilder sql_Select = new StringBuilder();
            sql_Select.Append("select * from sys_biz_CrossSheetFormulas");
            
            if (IndexCollection.Count > 0)
            {
                sql_Select.Append(" where ");
                sql_Select.Append(string.Concat("ID in ('", string.Join("','", IndexCollection.ToArray()), "')"));
            }
            else
            {
                sql_Select.Append(" where 1<>1 ");
            }
            //增加条件，判断数据是否已删除     2013-10-15
            //sql_Select.Append(" and (Scdel IS NULL or Scdel=0) ");
          
            DataTable Data = GetDataTable(sql_Select.ToString());
            if (Data != null)
            {
                DataRow Row;
                foreach (CrossSheetFormulaInfo Info in Infos)
                {
                    DataRow[] Rows = Data.Select("ID='" + Info.Index + "'");
                    if (Rows.Length > 0)
                        Row = Rows[0];
                    else
                    {
                        Row = Data.NewRow();
                        Data.Rows.Add(Row);
                        Row["ID"] = Info.Index;
                    }

                   
                    Row["SCTS"] = DateTime.Now.ToString();
                    Row["ModelIndex"] = Info.ModelIndex;
                    Row["SheetIndex"] = Info.SheetIndex;
                    Row["RowIndex"] = Info.RowIndex;
                    Row["ColumnIndex"] = Info.ColumnIndex;
                    Row["Formula"] = Info.Formula;

                    Row["Scts_1"] = DateTime.Now.ToString();
                    Row["Scdel"] = 0;
                }

                object r = Update(Data);
                Result = (Convert.ToInt32(r) == 1);
            }

            return Result;
        }

        public Boolean removeCrossSheetFormulaInfos(String ModelIndex)
        {
            Boolean Result = false;

            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，便于数据同步     2013-10-15
            StringBuilder Sql_Delete = new StringBuilder();
            //Sql_Delete.Append("delete from sys_biz_CrossSheetFormulas where ModelIndex='");
            //Sql_Delete.Append(ModelIndex);
            //Sql_Delete.Append("'");
            Sql_Delete.Append("Update sys_biz_CrossSheetFormulas Set Scts_1='" + DateTime.Now + "',Scdel=1");
            Sql_Delete.Append(" Where ModelIndex='");
            Sql_Delete.Append(ModelIndex);
            Sql_Delete.Append("'");

            try
            {
                object r = ExcuteCommand(Sql_Delete.ToString());
                Result = Convert.ToInt32(r) == 1;
            }
            catch
            {
            }

            return Result;
        }

        public Boolean removeCrossSheetFormulaInfos(String ModelIndex, String SheetIndex)
        {
            Boolean Result = false;
            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，便于数据同步     2013-10-15
            StringBuilder Sql_Delete = new StringBuilder();
            //Sql_Delete.Append("delete from sys_biz_CrossSheetFormulas where ModelIndex='");
            //Sql_Delete.Append(ModelIndex);
            //Sql_Delete.Append("' and SheetIndex='");
            //Sql_Delete.Append(SheetIndex);
            //Sql_Delete.Append("'");
            Sql_Delete.Append("Update sys_biz_CrossSheetFormulas Set Scts_1='" + DateTime.Now + "',Scdel=1");
            Sql_Delete.Append(" Where ModelIndex='");
            Sql_Delete.Append(ModelIndex);
            Sql_Delete.Append("' and SheetIndex='");
            Sql_Delete.Append(SheetIndex);
            Sql_Delete.Append("'");

            try
            {
                object r = ExcuteCommand(Sql_Delete.ToString());
                Result = Convert.ToInt32(r) == 1;
            }
            catch
            {
            }

            return Result;
        }
    }
}
