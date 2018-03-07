using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;

namespace Yqun.BO.ReminderManager
{
    public class TestItemManager : BOBase
    {
        public List<String> GetTestItemList(String Type)
        {
            List<String> Items = new List<String>();

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select * from sys_biz_reminder_testitem where type=");
            sql_select.Append(Type);
            sql_select.Append(" order by ID");

            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    String Index = Row["ID"].ToString();

                    Items.Add(Index);
                }
            }

            return Items;
        }

        public List<TestItemInfo> GetTestItemInfos()
        {
            List<TestItemInfo> Items = new List<TestItemInfo>();

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select * from sys_biz_reminder_testitem order by ID");

            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    String Index = Row["ID"].ToString();
                    String Description = Row["ItemName"].ToString();

                    TestItemInfo Info = new TestItemInfo();
                    Info.Index = Index;
                    Info.Description = Description;
                    Items.Add(Info);
                }
            }

            return Items;
        }

        public DataTable GetTestItemData()
        {
            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select * from sys_biz_reminder_testitem order by ID");

            return GetDataTable(sql_select.ToString());
        }

        public Boolean UpdateTestItemData(DataTable ItemData)
        {
            Boolean Result = false;

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select * from sys_biz_reminder_testitem order by ID");

            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null)
            {
                foreach (DataRow row in Data.Rows)
                {
                    String Index = row["ID"].ToString();
                    String ItemName = row["ItemName"].ToString();
                    String TestCount = row["TestCount"].ToString();
                    String Type = row["Type"].ToString();

                    DataRow Row = null;
                    DataRow[] DataRows = Data.Select("ID='" + Index + "'");
                    if (DataRows.Length > 0)
                        Row = DataRows[0];
                    else
                    {
                        Row = Data.NewRow();
                        Data.Rows.Add(Row);
                    }

                    Row["ID"] = Index;
                    Row["ItemName"] = ItemName;
                    Row["TestCount"] = TestCount;
                    Row["Type"] = Type;
                }

                try
                {
                    int r = Update(Data);
                    Result = (r == 1);
                }
                catch
                {
                }
            }

            return Result;
        }
    }
}
