using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;
using Yqun.Bases;
using Yqun.Data.DataBase;
using System.Reflection;

namespace Yqun.BO.ReminderManager
{
    public class StadiumInfoManager : BOBase
    {
        public List<IndexDescriptionPair> GetModels()
        {
            List<IndexDescriptionPair> Pairs = new List<IndexDescriptionPair>();

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select * from sys_biz_reminder_stadiummodel order by orderIndex");

            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    String Index = Row["ID"].ToString();
                    String Text = Row["Description"].ToString();

                    IndexDescriptionPair Pair = new IndexDescriptionPair();
                    Pair.Index = Index;
                    Pair.Description = Text;
                    Pairs.Add(Pair);
                }
            }

            return Pairs;
        }

        public List<StadiumInfo> InitStadiumInfos()
        {
            List<StadiumInfo> Infos = new List<StadiumInfo>();

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select a.ID,b.Description,a.F_ZJRQ,a.F_Days,F_Columns from sys_biz_reminder_stadiumInfo as a,sys_biz_reminder_stadiummodel as b where a.ID = b.ID");

            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    String Index = Row["ID"].ToString();
                    String Description = Row["Description"].ToString();
                    String F_ZJRQ = Row["F_ZJRQ"].ToString();
                    String F_Days = Row["F_Days"].ToString();
                    String F_Columns = Row["F_Columns"].ToString();

                    StadiumInfo stadiumInfo = new StadiumInfo();
                    stadiumInfo.Index = Index;
                    stadiumInfo.Description = Description;
                    stadiumInfo.F_ZJRQ = F_ZJRQ;
                    stadiumInfo.F_List = F_Days;
                    stadiumInfo.F_Columns.AddRange(F_Columns.Split('^'));
                    Infos.Add(stadiumInfo);
                }
            }

            return Infos;
        }

        public StadiumInfo InitStadiumInfo(String Index)
        {
            StadiumInfo stadiumInfo = null;

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select * from sys_biz_reminder_stadiumInfo");

            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null)
            {
                foreach (DataRow Row in Data.Rows)
                {
                    String ID = Row["ID"].ToString();
                    String Description = Row["Description"].ToString();
                    String F_ZJRQ = Row["F_ZJRQ"].ToString();
                    String F_Days = Row["F_Days"].ToString();
                    String F_Columns = Row["F_Columns"].ToString();

                    stadiumInfo = new StadiumInfo();
                    stadiumInfo.Index = ID;
                    stadiumInfo.Description = Description;
                    stadiumInfo.F_ZJRQ = F_ZJRQ;
                    stadiumInfo.F_List = F_Days;
                    stadiumInfo.F_Columns.AddRange(F_Columns.Split('^'));
                }
            }

            return stadiumInfo;
        }

        public Boolean DeleteStadiumInfo(String Index)
        {
            Boolean result = false;

            StringBuilder sql_Delete = new StringBuilder();
            sql_Delete.Append("delete from sys_biz_reminder_stadiumInfo where ID='");
            sql_Delete.Append(Index);
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

        public Boolean UpdateStadiumInfo(StadiumInfo Info)
        {
            Boolean Result = false;

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select * from sys_biz_reminder_stadiumInfo where ID='");
            sql_select.Append(Info.Index);
            sql_select.Append("'");

            DataTable Data = GetDataTable(sql_select.ToString());
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

                Row["ID"] = Info.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["F_ZJRQ"] = Info.F_ZJRQ;
                Row["F_Days"] = Info.F_List;
                Row["F_Columns"] = string.Join("^", Info.F_Columns.ToArray());

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

        public DataTable InitStadiumConfig()
        {
            string sql = @"SELECT b.ID,b.Description,a.IsActive,a.SearchRange,a.StadiumConfig FROM dbo.sys_biz_reminder_stadiumInfo a
            JOIN dbo.sys_biz_Module b ON a.ID = b.ID";

            return GetDataTable(sql);
        }

        public Boolean UpdateStadiumConfig(string modelID, string searRange, string config, bool isactive)
        {
            Boolean Result = false;

            StringBuilder sql_select = new StringBuilder();
            sql_select.Append("select * from sys_biz_reminder_stadiumInfo where ID='");
            sql_select.Append(modelID);
            sql_select.Append("'");

            DataTable Data = GetDataTable(sql_select.ToString());
            if (Data != null)
            {
                DataRow Row = null;
                if (Data.Rows.Count > 0)
                    Row = Data.Rows[0];
                else
                {
                    Row = Data.NewRow();
                    Row["ID"] = modelID;
                    Data.Rows.Add(Row);
                }

                //Row["ID"] = Info.Index;
                Row["SCTS"] = DateTime.Now.ToString();
                Row["IsActive"] = isactive ? 1 : 0;
                Row["SearchRange"] = searRange;
                Row["StadiumConfig"] = config;

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
