using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace UpdaterComponents
{
    class SQLResolver
    {
        public static string ReplaceTextValueWithBlank(string Expression)
        {
            bool Check = CheckUpDot(Expression);
            if (!Check)
            {
                return "";
            }

            string str = "";
            int dotNum = 0;
            Stack stack = new Stack();
            for (int i = 0; i < Expression.Length; i++)
            {
                string s = Expression.Substring(i, 1);

                if (s == "'")
                {
                    dotNum++;
                    stack.Push(" ");
                }
                else
                {
                    if (dotNum % 2 == 0)
                    {
                        stack.Push(s);
                    }
                    else
                    {
                        stack.Push(" ");
                    }
                }
            }

            int k = stack.Count;
            for (int i = 0; i < k; i++)
            {
                str = stack.Pop().ToString() + str;
            }

            return str;
        }

        public static string[] GetColNameFromSelectCommand(string SelectCommandText)
        {
            SelectCommandText = ReplaceTextValueWithBlank(SelectCommandText);
            string Upper = SelectCommandText.ToUpper().Trim();
            int x = Upper.IndexOf(" FROM ");
            string s1 = Upper;
            if (x != -1)
            {
                s1 = Upper.Substring(0, x);
            }
            int k = s1.IndexOf("SELECT ");
            string s2 = s1;
            if (k != -1)
            {
                s2 = s1.Substring(k + 7, s1.Length - k - 7);
            }
            s2 = s2.Trim();

            string[] retStr = GetColWords(s2);
            for (int i = 0; i < retStr.Length; i++)
            {
                string str = retStr[i].Trim().ToUpper();
                int x1 = str.IndexOf(" AS ");
                if (x1 != -1)
                {
                    str = str.Substring(0, x1);
                }


                retStr[i] = str;
            }
            return retStr;
        }

        public static string[] GetColWords(string CommandText)
        {
            bool check = CheckBracket(CommandText);
            if (!check)
            {
                return new string[0];
            }

            Stack s = new Stack();
            Stack Words = new Stack();
            int brkLeft = 0;
            int brkRight = 0;
            for (int i = 0; i < CommandText.Length; i++)
            {
                string str = CommandText.Substring(i, 1);

                if (str == "(")
                {
                    brkLeft++;
                }

                if (str == ")")
                {
                    brkRight++;
                }

                if (str != "," || (brkLeft != brkRight))
                {
                    s.Push(str);
                }

                if ((str == "," && (brkLeft == brkRight)) || i == CommandText.Length - 1)
                {

                    string word = "";
                    int len = s.Count;
                    for (int j = 0; j < len; j++)
                    {

                        word = s.Pop().ToString() + word;
                    }


                    s.Clear();
                    brkLeft = 0;
                    brkRight = 0;
                    Words.Push(word);
                }
            }

            string[] retStr = new string[Words.Count];
            for (int i = Words.Count - 1; i >= 0; i--)
            {

                retStr[i] = Words.Pop().ToString();
            }

            return retStr;
        }

        public static bool CheckBracket(string CommandText)
        {
            int brkLeft = 0;
            int brkRight = 0;
            for (int i = 0; i < CommandText.Length; i++)
            {
                string str = CommandText.Substring(i, 1);
                if (str == "(")
                {
                    brkLeft++;
                }

                if (str == ")")
                {
                    brkRight++;
                }
            }

            if (brkLeft == brkRight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckUpDot(string Expression)
        {
            int dot = 0;
            for (int i = 0; i < Expression.Length; i++)
            {
                string str = Expression.Substring(i, 1);
                if (str == "'")
                {
                    dot++;
                }
            }

            if (dot % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string Revert(string Word)
        {
            string word = "";
            for (int i = Word.Length - 1; i >= 0; i--)
            {
                string str = Word.Substring(i, 1);
                word += str;
            }
            return word;
        }

        public static string GetTableNamesFromSeleCommandText(string SeleCommandText)
        {
            string tempStr = SeleCommandText.ToLower();
            int x = tempStr.IndexOf(" from ");
            if (x != -1)
            {
                tempStr = tempStr.Substring(x + 6, tempStr.Length - x - 6);
            }
            tempStr = tempStr.Trim();

            tempStr = tempStr.Replace(" order by ", " where ");
            tempStr = tempStr.Replace(" group by ", " where ");
            int x1 = tempStr.IndexOf(" where ");
            string table = tempStr;
            if (x1 != -1)
            {
                table = tempStr.Substring(0, x1);
            }
            table = table.Trim();

            return table;


        }

        public static int GetFirstOffset(string str, string[] symbol)
        {
            int k = -1;
            for (int j = 0; j < symbol.Length; j++)
            {
                int m = str.IndexOf(symbol[j]);
                if (m != -1)
                {
                    if (k == -1 || k > m)
                    {
                        k = m;
                    }
                }

            }

            return k;
        }
    }
        
}
