using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace ReportCommon
{
    public class SQLCommandBuilder
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static String GetCommand(DataTable Schema)
        {
            List<string> Subquerys = new List<string>();
            foreach (DataRow Row in Schema.Rows)
            {
                DataColumnCollection DataColumns = Schema.Columns;
                String[] ItemArray = new String[Row.ItemArray.Length];
                for (int i = 0; i < Row.ItemArray.Length; i++)
                    ItemArray[i] = Row.ItemArray[i].ToString();

                List<string> FieldNames = new List<string>();
                List<string> TableNames = new List<string>();
                foreach (string Item in ItemArray)
                {
                    int index = Array.IndexOf(ItemArray, Item);

                    if (Item.Contains("."))
                    {
                        string[] Tokens = Item.Split('.');
                        string Name = string.Format("[{0}]", Tokens[0].Trim('[', ']'));
                        if (!TableNames.Contains(Name))
                            TableNames.Add(Name);
                        FieldNames.Add(string.Format("[{0}].[{1}] as [{2}]", Tokens[0].Trim('[', ']'), Tokens[1].Trim('[', ']'), DataColumns[index].Caption));
                    }
                    else
                    {
                        String Tokens = @"\(\)\*\+\-/,<>=':?";
                        String Pattern = String.Format(@"(?:([{0}]|^))\{{.+?\}}(?=([{0}]|$))", Tokens);
                        Regex r = new Regex(Pattern);
                        MatchCollection Matches = r.Matches(Item.Replace(" ", ""));
                        if (Matches.Count > 0)
                        {
                            String Result = Item.Replace(" ", "");
                            foreach (Match m in Matches)
                            {
                                String variable = m.Value.Trim(Tokens.ToCharArray());
                                int i = DataColumns.IndexOf(variable.Trim('{', '}').Trim());
                                if (i != -1)
                                {
                                    String formatvalue = "";
                                    String value = ItemArray[i];
                                    string[] tokens = value.Split('.');
                                    if (tokens.Length > 1)
                                    {
                                        formatvalue = string.Format("[{0}].[{1}]", tokens[0].Trim('[', ']'), tokens[1].Trim('[', ']'));
                                    }
                                    else
                                    {
                                        formatvalue = string.Format("'{0}'", tokens[0].Trim('[', ']'));
                                    }

                                    Result = Result.Replace(variable, formatvalue);
                                }
                            }

                            FieldNames.Add(string.Format("{0} as [{1}]", Result, DataColumns[index].Caption));
                        }
                        else
                        {
                            FieldNames.Add(string.Format("'{0}' as [{1}]", Item.Trim('"', '\''), DataColumns[index].Caption));
                        }
                    }
                }

                List<String> sql_wheres = new List<String>();
                for (int i = 1; i < TableNames.Count; i++)
                {

                    String PrevTable = TableNames[i - 1];
                    String Table = TableNames[i];

                    sql_wheres.Add(PrevTable + ".ID = " + Table + ".ID");
                }

                StringBuilder sql_query = new StringBuilder();
                sql_query.Append("select ");
                sql_query.Append(string.Join(",", FieldNames.ToArray()));
                sql_query.Append(" from ");
                sql_query.Append(string.Join(",", TableNames.ToArray()));

                if (sql_wheres.Count > 0)
                {
                    sql_query.Append(" where ");
                    sql_query.Append(string.Join(" and ", sql_wheres.ToArray()));
                }

                Subquerys.Add(sql_query.ToString());
            }

            if (Subquerys.Count > 0)
            {
                return string.Join(" union all ", Subquerys.ToArray());
            }
            else
            {
                return "";
            }
        }
    }
}
