using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections;
using FarPoint.Win.Spread;
using ReportCommon;

namespace Yqun.BO.ReportE.Core
{
    internal class Calculator
    {
        SheetView report;
        Hashtable attributes = new Hashtable();
        static Object REPORT = new Object();
        static Object COLUMNROW = new Object();

        public Calculator(SheetView paramReport)
        {
            this.report = paramReport;
        }

        public void setAttribute(Object paramObject1, Object paramObject2)
        {
            this.attributes.Add(paramObject1, paramObject2);
        }

        public Object getAttribute(Object paramObject)
        {
            return this.attributes[paramObject];
        }

        public void setCurrentReport(SheetView paramReport)
        {
            this.attributes.Add(REPORT, paramReport);
        }

        public Report getCurrentReport()
        {
            return (Report)this.attributes[REPORT];
        }

        public String CalcFormula(Formula Formula, PE PE_left, PE PE_up)
        {
            if (string.IsNullOrEmpty(Formula.Expression))
                return String.Empty;

            String result = Formula.Expression;

            //用于提取公式中的单元格位置
            String Pattern = @"(?:([\(\*\+\-/,<]|^))[A-Za-z]+\d+(?=([\)\*\+\-/,<]|$))";
            Regex r = new Regex(Pattern);
            MatchCollection Matches = r.Matches(result);
            ArrayList Variables = new ArrayList();
            foreach (Match m in Matches)
            {
                String Cell = m.Value.Trim('(', ')', '+', '-', '*', '/',',','<');
                if (!Variables.Contains(Cell))
                    Variables.Add(Cell);
            }

            Hashtable Variable_CEList = new Hashtable();
            ReportEngine re = getAttribute(ReportEngine.CUR_RE) as ReportEngine;

            foreach (String Cell in Variables)
            {
                CE[] ce_list = re.dealWithFormulaVariable(PE_left, PE_up, Cell);

                if (!Variable_CEList.ContainsKey(Cell))
                    Variable_CEList.Add(Cell, new List<object>());

                foreach (CE ce in ce_list)
                {
                    ((List<object>)Variable_CEList[Cell]).Add(ce.obj);
                }
            }

            String localObject1 = result;
            foreach (String var in Variables)
            {
                List<object> values = Variable_CEList[var] as List<object>;
                List<string> str_values = new List<string>();
                foreach (object obj in values)
                {
                    if (obj is Formula)
                    {
                        Formula formula = obj as Formula;
                        str_values.Add(formula.Result.ToString());
                    }
                    else
                    {
                        if (obj != null)
                        {
                            str_values.Add(obj.ToString());
                        }
                    }
                }

                if (str_values.Count == 0)
                    continue;

                String str_value_list = string.Join(",", str_values.ToArray());
                if (str_value_list == "") str_value_list = "0.0";
                localObject1 = localObject1.Replace(var, str_value_list);
            }

            return localObject1;
        }
    }
}
