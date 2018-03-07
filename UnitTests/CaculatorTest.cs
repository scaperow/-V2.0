using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Collections;

namespace UnitTests
{
    public class CaculatorTest
    {
        [Test]
        public void RegexTest()
        {
            String NormalFormula = "IF(Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"hrb335\",\">=17%\",IF(Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"hrb335e\",\">=17%\",IF(Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"hrb335f\",\">=17%\",IF(Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"hrb400\",\">=16%\",IF(Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"hrb400e\",\">=16%\",IF(Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"hrb500\",\">=15%\",IF(Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"hrb500e\",\">=15%\",IF(Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"hpb235\",\">=25%\",IF(Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"hpb300\",\">=25%\",IF(Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"psb830\",\">=6%\",IF(Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"psb785\",\">=7%\",IF(AND(OR(Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"q235\",Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"q235-a\",Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"q235-b\"),Value({ biz_norm_钢筋试验任务委托单.col_norm_I11 }) <=40),\">=26%\",IF(AND(OR(Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"q235\",Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"q235-a\",Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"q235-b\"),Value({ biz_norm_钢筋试验任务委托单.col_norm_I11 })>40,Value({ biz_norm_钢筋试验任务委托单.col_norm_I11 })<=60),\">=25%\",IF(AND(OR(Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"q235\",Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"q235-a\",Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"q235-b\"),Value({ biz_norm_钢筋试验任务委托单.col_norm_I11 })>60,Value({ biz_norm_钢筋试验任务委托单.col_norm_I11 })<=100),\">=24%\",IF(AND(OR(Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"q235\",Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"q235-a\",Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"q235-b\"),Value({ biz_norm_钢筋试验任务委托单.col_norm_I11 })>100,Value({ biz_norm_钢筋试验任务委托单.col_norm_I11 })<=150),\">=22%\",IF(AND(OR(Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"q235\",Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"q235-a\",Lower({ biz_norm_钢筋试验任务委托单.col_norm_C11 })=\"q235-b\"),Value({ biz_norm_钢筋试验任务委托单.col_norm_I11 })>150,Value({ biz_norm_钢筋试验任务委托单.col_norm_I11 })<=200),\">=21%\",{ biz_norm_钢筋试验任务委托单.col_norm_C11 })))))))))))))))))";
            NormalFormula += @"

";
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
            Assert.AreEqual(variables.Count, 2);
        }

        [Test]
        public void TestCGL()
        {
            //XGLGrainMate
        }
    }
}
