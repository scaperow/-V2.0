using System;
using System.Collections.Generic;
using System.Text;
using ReportCommon;
using System.Data;

namespace Yqun.BO.ReportManager
{
    public class ReportFormatManager : BOBase
    {
        public FormatStringGroup InitReportFormats(FormatStyle FormatStyle)
        {
            String formatStyle = FormatStyle.ToString();
            FormatStringGroup fsg = new FormatStringGroup();
            StringBuilder Sql_Select = new StringBuilder();
            //增加查询条件 Scdel=0  判断数据是否已删除  2013-10-17
            Sql_Select.Append("Select ID,FormatStyle,FormatString From sys_biz_ReportFormatStrings Where Scdel=0 and FormatStyle='");
            Sql_Select.Append(formatStyle.ToString());
            Sql_Select.Append("'");
            Sql_Select.Append(" ORDER BY FormatString ASC ");

            DataTable Data = GetDataTable(Sql_Select.ToString());
            if (Data != null && Data.Rows.Count > 0)
            {
                for (int i = 0; i < Data.Rows.Count; i++)
                {
                    FormatInfo fs = new FormatInfo();
                    DataRow Row = Data.Rows[i];
                    fs.Index = Row["ID"].ToString();
                    String FormatS = Row["FormatStyle"].ToString();
                    fs.Style = (FormatStyle)Enum.Parse(typeof(FormatStyle), FormatS);
                    fs.Format = Row["FormatString"].ToString();
                    fsg.FormatInfos.Add(fs);
                }
            }

            return fsg;
        }

        public Boolean UpdateReportFormats(FormatInfo String)
        {
            //增加查询条件 Scdel=0  判断数据是否已删除  2013-10-17
            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("Select ID,FormatStyle,FormatString From sys_biz_ReportFormatStrings Where Scdel=0 and ID='");
            Sql_Select.Append(String.Index);
            Sql_Select.Append("'");

            DataTable Data = GetDataTable(Sql_Select.ToString()); 
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow Row = Data.Rows[0];
                Row["ID"] = String.Index;
                Row["FormatStyle"] = String.Style;
                Row["FormatString"] = String.Format;

                Row["Scts_1"] = DateTime.Now.ToString();
            }
            else
            {
                DataRow Row = Data.NewRow();
                Row["ID"] = String.Index;
                Row["FormatStyle"] = String.Style;
                Row["FormatString"] = String.Format;

                Row["Scts_1"] = DateTime.Now.ToString();
                Data.Rows.Add(Row);
            }

            Boolean Result = false;

            try
            {
                object r = Update(Data);
                Result = (Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public Boolean DeleteReportFormats(FormatInfo String)
        {
            //增加字段Scts_1,Scdel 之后 删除操作只做伪删除，用于数据同步     2013-10-17
            StringBuilder Sql_Delete = new StringBuilder();
            //Sql_Delete.Append("Delete From sys_biz_ReportFormatStrings Where FormatString ='");
            //Sql_Delete.Append(String.Format);
            //Sql_Delete.Append("'");
            Sql_Delete.Append("Update sys_biz_ReportFormatStrings Set Scts_1=Getdate(),Scdel=1 Where FormatString ='");
            Sql_Delete.Append(String.Format);
            Sql_Delete.Append("'");

            Boolean Result = false;

            try
            {
                object r = ExcuteCommand(Sql_Delete.ToString());
                Result = (Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }
    }
}
