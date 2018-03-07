using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;

namespace Yqun.BO.ReminderManager
{
    public class SamplingFrequencyInfoManager : BOBase
    {
        public SamplingFrequencyInfo InitSamplingFrequencyInfo(String Index)
        {
            SamplingFrequencyInfo r = null;

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select Persons from sys_biz_reminder_samplingFrequency where ID='");
            sql_select.Append(Index);
            sql_select.Append("'");

            StringBuilder sql_select1 = new StringBuilder();
            sql_select1.Append("select * from sys_biz_reminder_Itemfrequency where FrequencyInfoIndex='");
            sql_select1.Append(Index);
            sql_select1.Append("'");

            DataSet dataset = GetDataSet(new string[] { sql_select.ToString(), sql_select1.ToString() });
            if (dataset != null)
            {
                DataTable sys_biz_reminder_samplingFrequency = dataset.Tables["sys_biz_reminder_samplingFrequency"];
                DataTable sys_biz_reminder_Itemfrequency = dataset.Tables["sys_biz_reminder_Itemfrequency"];

                if (sys_biz_reminder_samplingFrequency.Rows.Count > 0)
                {
                    DataRow Row = sys_biz_reminder_samplingFrequency.Rows[0];

                    r.Index = Index;

                    foreach (DataRow ItemRow in sys_biz_reminder_Itemfrequency.Rows)
                    {
                        String ID = ItemRow["ID"].ToString();
                        String ModelIndex = ItemRow["ModelIndex"].ToString();
                        String ModelName = ItemRow["ModelName"].ToString();
                        float JianZheng = Convert.ToSingle(ItemRow["JianZheng"]);
                        float PingXing = Convert.ToSingle(ItemRow["PingXing"]);

                        ItemFrequency Item = new ItemFrequency();
                        Item.ModelIndex = ModelIndex;
                        Item.ModelName = ModelName;
                        Item.FrequencyInfoIndex = Index;
                        Item.JianZhengFrequency = JianZheng;
                        Item.PingXingFrequency = PingXing;

                        r.ItemFrequencys.Add(Item);
                    }
                }
            }

            return r;
        }

        public Boolean UpdateSamplingFrequencyInfo(SamplingFrequencyInfo Info)
        {
            Boolean Result = false;

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select ID from sys_biz_reminder_samplingFrequency where ID='");
            sql_select.Append(Info.Index);
            sql_select.Append("'");

            StringBuilder sql_select1 = new StringBuilder();
            sql_select1.Append("select * from sys_biz_reminder_Itemfrequency where FrequencyInfoIndex='");
            sql_select1.Append(Info.Index);
            sql_select1.Append("'");

            DataSet dataset = GetDataSet(new string[] { sql_select.ToString(), sql_select1.ToString() });
            if (dataset != null)
            {
                DataTable sys_biz_reminder_samplingFrequency = dataset.Tables["sys_biz_reminder_samplingFrequency"];
                DataTable sys_biz_reminder_Itemfrequency = dataset.Tables["sys_biz_reminder_Itemfrequency"];

                DataRow Row = null;
                if (sys_biz_reminder_samplingFrequency.Rows.Count > 0)
                {
                    Row = sys_biz_reminder_samplingFrequency.Rows[0];
                }
                else
                {
                    Row = sys_biz_reminder_samplingFrequency.NewRow();
                    sys_biz_reminder_samplingFrequency.Rows.Add(Row);
                }

                Row["ID"] = Info.Index;

                foreach (ItemFrequency Item in Info.ItemFrequencys)
                {
                    DataRow ItemRow = null;
                    DataRow[] ItemRows = sys_biz_reminder_Itemfrequency.Select("ModelIndex='" + Item.ModelIndex + "' and FrequencyInfoIndex='" + Info.Index + "'");
                    if (ItemRows.Length > 0)
                        ItemRow = ItemRows[0];
                    else
                    {
                        ItemRow = sys_biz_reminder_Itemfrequency.NewRow();
                        sys_biz_reminder_Itemfrequency.Rows.Add(ItemRow);
                    }

                    ItemRow["FrequencyInfoIndex"] = Item.FrequencyInfoIndex;
                    ItemRow["ModelIndex"] = Item.ModelIndex;
                    ItemRow["ModelName"] = Item.ModelName;
                    ItemRow["JianZheng"] = Item.JianZhengFrequency;
                    ItemRow["PingXing"] = Item.PingXingFrequency;
                }

                try
                {
                    object r = Update(dataset);
                    Result = (Convert.ToInt32(r) == 1);
                }
                catch
                {
                }
            }

            return Result;
        }
    }
}
