using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;
using Yqun.Data.DataBase;
using System.Reflection;

namespace Yqun.BO.ReminderManager
{
    public class EvaluateConditionManager : BOBase
    {
        public ReportEvaluateCondition InitEvaluateConditions(String ModelIndex, String SheetIndex)
        {
            ReportEvaluateCondition r = null;

            //增加查询条件 Scdel=0 2013-10-17
            StringBuilder sql_Select = new StringBuilder();
            sql_Select.Append("select ID, Description,ReportNumber,ReportDate from sys_biz_reminder_evaluatecondition where Scdel=0 and modelIndex='");
            sql_Select.Append(ModelIndex);
            sql_Select.Append("' and SheetIndex='");
            sql_Select.Append(SheetIndex);
            sql_Select.Append("'");

            //增加查询条件 Scdel=0 2013-10-17
            StringBuilder sql_Item = new StringBuilder();
            sql_Item.Append("select ID,Text,Specifiedvalue,Truevalue,Expression from sys_biz_reminder_Itemcondition where Scdel=0 and EvaluateIndex='");
            sql_Item.Append(string.Concat(ModelIndex, "_", SheetIndex));
            sql_Item.Append("' order by OrderIndex");

            DataSet ds = GetDataSet(new string[]{sql_Select.ToString(),sql_Item.ToString()});
            if (ds != null)
            {
                DataTable ReportEvaluateTable = ds.Tables["sys_biz_reminder_evaluatecondition"];
                DataTable ItemTable = ds.Tables["sys_biz_reminder_Itemcondition"];

                if (ReportEvaluateTable.Rows.Count > 0)
                {
                    DataRow Row = ReportEvaluateTable.Rows[0];
                    String ID = Row["ID"].ToString();
                    String Description = Row["Description"].ToString();
                    String ReportNumber = Row["ReportNumber"].ToString();
                    String ReportDate = Row["ReportDate"].ToString();

                    r = new ReportEvaluateCondition();
                    r.Index = ID;
                    r.ModelIndex = ModelIndex;
                    r.SheetIndex = SheetIndex;
                    r.Description = Description;
                    r.ReportNumber = ReportNumber;
                    r.ReportDate = ReportDate;

                    foreach (DataRow ItemRow in ItemTable.Rows)
                    {
                        String Index = ItemRow["ID"].ToString();
                        String Text = ItemRow["Text"].ToString();
                        String Specifiedvalue = ItemRow["Specifiedvalue"].ToString();
                        String Truevalue = ItemRow["Truevalue"].ToString();
                        String Expression = ItemRow["Expression"].ToString();
                        
                        ItemCondition Condition = new ItemCondition();
                        Condition.Index = Index;
                        Condition.EvaluateIndex = string.Concat(ModelIndex, "_", SheetIndex);
                        Condition.Text = Text;
                        Condition.Specifiedvalue = Specifiedvalue;
                        Condition.TrueValue = Truevalue;
                        Condition.Expression = Expression;
                        
                        r.Items.Add(Condition);
                    }
                }
            }

            return r;
        }

        public Boolean DeleteEvaluateCondition(String ModelIndex, String SheetIndex)
        {
            Boolean result = false;

            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-17
            StringBuilder sql_Delete = new StringBuilder();
            //sql_Delete.Append("delete from sys_biz_reminder_evaluatecondition where modelIndex='");
            //sql_Delete.Append(ModelIndex);
            //sql_Delete.Append("' and SheetIndex='");
            //sql_Delete.Append(SheetIndex);
            //sql_Delete.Append("'");
            sql_Delete.Append("update sys_biz_reminder_evaluatecondition Set Scts_1=Getdate(),Scdel=1 where modelIndex='");
            sql_Delete.Append(ModelIndex);
            sql_Delete.Append("' and SheetIndex='");
            sql_Delete.Append(SheetIndex);
            sql_Delete.Append("'");

            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-17
            StringBuilder sql_Delete1 = new StringBuilder();
            //sql_Delete1.Append("delete from sys_biz_reminder_Itemcondition where EvaluateIndex='");
            //sql_Delete1.Append(string.Concat(ModelIndex, "_", SheetIndex));
            //sql_Delete1.Append("'");

            sql_Delete1.Append("Update sys_biz_reminder_Itemcondition Set Scts_1=Getdate(),Scdel=1 where EvaluateIndex='");
            sql_Delete1.Append(string.Concat(ModelIndex, "_", SheetIndex));
            sql_Delete1.Append("'");

            IDbConnection Connection = GetConntion();
            Transaction Transaction = new Transaction(Connection);

            try
            {
                int r = ExcuteCommand(sql_Delete.ToString(), Transaction);
                result = (r == 1);
                r = ExcuteCommand(sql_Delete1.ToString(), Transaction);
                result = result && (r == 1);

                if (result)
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

            return result;
        }

        public Boolean UpdateEvaluateCondition(ReportEvaluateCondition Condition)
        {
            Boolean result = false;

            StringBuilder sql_Select = new StringBuilder();
            //增加查询条件 Scdel=0 2013-10-17
            sql_Select.Append("select ID,SCTS,ModelIndex,SheetIndex,Description,ReportNumber,ReportDate,Scts_1 from sys_biz_reminder_evaluatecondition where Scdel=0 and modelIndex='");
            sql_Select.Append(Condition.ModelIndex);
            sql_Select.Append("' and SheetIndex='");
            sql_Select.Append(Condition.SheetIndex);
            sql_Select.Append("'");

            DataTable Data = GetDataTable(sql_Select.ToString());
            if (Data != null)
            {
                DataRow Row = null;
                if (Data.Rows.Count > 0)
                    Row = Data.Rows[0];
                else
                {
                    Row = Data.NewRow();
                    Data.Rows.Add(Row);
                }

                Row["ID"] = Condition.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["ModelIndex"] = Condition.ModelIndex;
                Row["SheetIndex"] = Condition.SheetIndex;
                Row["Description"] = Condition.Description;
                Row["ReportNumber"] = Condition.ReportNumber;
                Row["ReportDate"] = Condition.ReportDate;
                //新增字段Scts_1    2013-10-17
                Row["Scts_1"] = DateTime.Now.ToString();
                try
                {
                    object r = Update(Data);
                    result = (Convert.ToInt32(r) == 1);
                }
                catch
                {
                }
            }

            return result;
        }

        public Boolean UpdateEvaluateItemCondition(String ModelIndex, String SheetIndex, ItemCondition Item, int OrderIndex)
        {
            Boolean result = false;

            StringBuilder sql_Item = new StringBuilder();
            //增加查询条件 Scdel=0 2013-10-17
            sql_Item.Append("select ID,SCTS,EvaluateIndex,Text,Specifiedvalue,Truevalue,Expression,OrderIndex,Scts_1 from sys_biz_reminder_Itemcondition where Scdel=0 and EvaluateIndex='");
            sql_Item.Append(string.Concat(ModelIndex, "_", SheetIndex));
            sql_Item.Append("' and ID='");
            sql_Item.Append(Item.Index);
            sql_Item.Append("'");

            DataTable Data = GetDataTable(sql_Item.ToString());
            if (Data != null)
            {
                DataRow ItemRow = null;
                if (Data.Rows.Count > 0)
                {
                    ItemRow = Data.Rows[0];
                }
                else
                {
                    ItemRow = Data.NewRow();
                    Data.Rows.Add(ItemRow);
                }
                
                ItemRow["ID"] = Item.Index;
                ItemRow["SCTS"] = DateTime.Now.ToString();
                ItemRow["EvaluateIndex"] = string.Concat(ModelIndex, "_", SheetIndex);
                ItemRow["Text"] = Item.Text;
                ItemRow["Specifiedvalue"] = Item.Specifiedvalue;
                ItemRow["Truevalue"] = Item.TrueValue;
                ItemRow["Expression"] = Item.Expression;
                ItemRow["OrderIndex"] = OrderIndex;

                //新增字段Scts_1    2013-10-17
                ItemRow["Scts_1"] = DateTime.Now.ToString();
                
                try
                {
                    object r = Update(Data);
                    result = (Convert.ToInt32(r) == 1);
                }
                catch
                {
                }
            }

            return result;
        }

        public Boolean DeleteEvaluateItemCondition(String ItemIndex)
        {
            Boolean result = false;

            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-17
            StringBuilder sql_Delete = new StringBuilder();
            //sql_Delete.Append("Delete from sys_biz_reminder_Itemcondition where ID='");
            //sql_Delete.Append(ItemIndex);
            //sql_Delete.Append("'");

            sql_Delete.Append("Update sys_biz_reminder_Itemcondition Set Scts_1=Getdate(),Scdel=1 where ID='");
            sql_Delete.Append(ItemIndex);
            sql_Delete.Append("'");

            try
            {
                int r = ExcuteCommand(sql_Delete.ToString());
                result = (r == 1);
            }
            catch
            {
            }

            return result;
        }
    }
}
