using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FarPoint.Win.Spread;
using System.Text.RegularExpressions;
using System.Collections;

namespace Yqun.BO.ReminderManager
{
    /// <summary>
    /// 表达式计算器
    /// </summary>
    public class Calculator
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static FpSpread fpSpread;
        static SheetView sheetView;
        static Cell cell;

        static Calculator()
        {
            fpSpread = new FpSpread();
            sheetView = new SheetView();
            fpSpread.Sheets.Add(sheetView);
            cell = sheetView.Cells[0, 0];
        }

        public static String CalcQualified(String Formula, DataSet Data)
        {
            if (string.IsNullOrEmpty(Formula))
            {
                logger.Error("传入的参数Formula为空");
                return Boolean.TrueString;
            }

            try
            {
                String NormalFormula = Formula.Replace(" ", "");

                //用于提取公式中的{ 表.列 }结构
                String Tokens = @"\(\)\*\+\-/,<>=':?";
                String Pattern = String.Format(@"(?:([{0}]|^))\{{.+?\}}(?=([{0}]|$))", Tokens);
                Regex r = new Regex(Pattern);
                MatchCollection Matches = r.Matches(NormalFormula);
                ArrayList variables = new ArrayList();
                foreach (Match m in Matches)
                {
                    String variable = m.Value.Trim(Tokens.ToCharArray());
                    if (!variables.Contains(variable))
                        variables.Add(variable);
                }

                String Result = NormalFormula;
                foreach (String key in variables)
                {
                    try
                    {
                        string[] words = key.Trim('{', '}').Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                        DataTable Table = Data.Tables[string.Format("[{0}]", words[0].Trim())];
                        object value = Table.Rows[0][words[1].Trim()];
                        String formatvalue = value.ToString();
                        if (formatvalue == "/" || formatvalue.Trim() == "")
                            goto End;

                        double v;
                        if (!double.TryParse(formatvalue, out v))
                            formatvalue = string.Format("\"{0}\"", value.ToString());                            
                        Result = Result.Replace(key, formatvalue);
                    }
                    catch(Exception ex)
                    {
                        logger.Error("qualified: ‘" + key + "’出错，错误原因：" + ex.Message);
                        continue;
                    }
                }
                cell.Formula = Result;

                if (cell.Text.ToLower() == "false")
                    logger.Error("qualified flase: "+cell.Formula);

                return cell.Text;
            }
            catch(Exception ex)
            {
                logger.Error("qualified: 解析公式‘" + Formula + "’出错，错误原因：" + ex.Message);
            }

            End:
            return Boolean.TrueString;
        }

        public static String CalcCondition(String Formula, DataSet Data)
        {
            if (string.IsNullOrEmpty(Formula))
            {
                logger.Error("传入的参数Formula为空");
                return String.Empty;
            }
            String NormalFormula = Formula.Replace(" ", "").Trim();
            ArrayList variables = new ArrayList();
            try
            {
                

                //用于提取公式中的{ 表.列 }结构
                String Tokens = @"\(\)\*\+\-/,<>=':?";
                String Pattern = String.Format(@"(?:([{0}]|^))\{{.+?\}}(?=([{0}]|$))", Tokens);
                Regex r = new Regex(Pattern);
                MatchCollection Matches = r.Matches(NormalFormula);
                
                foreach (Match m in Matches)
                {
                    String variable = m.Value.Trim(Tokens.ToCharArray());
                    if (!variables.Contains(variable))
                        variables.Add(variable);
                }
            }
            catch (Exception ex)
            {
                logger.Error("找条件：解析公式‘" + NormalFormula + "’出错，错误原因：" + ex.Message);
            }
            String Result = NormalFormula;
            try
            {

            
                
                foreach (String key in variables)
                {
                    try
                    {
                        string[] words = key.Trim('{', '}').Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                        DataTable Table = Data.Tables[string.Format("[{0}]", words[0].Trim())];
                        object value = Table.Rows[0][words[1].Trim()];
                        String formatvalue = value.ToString();
                        if (formatvalue == "/" || formatvalue.Trim() == "")
                            goto End;

                        double v;
                        if (!double.TryParse(formatvalue, out v))
                            formatvalue = string.Format("\"{0}\"", value.ToString());
                        Result = Result.Replace(key, formatvalue);
                    }
                    catch (Exception ex)
                    {
                        logger.Error("找条件：" + key + "’错，错误原因：" + ex.Message);
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("mid error: " + ex.Message);
            }

            try
            {

            
                if (variables.Count > 0)
                {
                    cell.Formula = Result;
                    return cell.Text;
                }
                else
                {
                    return Formula;
                }

            }
            catch (Exception ex)
            {
                logger.Error("farpoint error: " + ex.Message +"Formula: "+Result);
            }
            End:
            return String.Empty;
        }
    }
}
