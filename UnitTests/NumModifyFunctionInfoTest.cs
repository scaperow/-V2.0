using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BizFunctionInfos;
using System.Diagnostics;


namespace UnitTests
{
    public class NumModifyFunctionInfoTest
    {
        /// <summary>
        /// 四舍六入五单双的测试用例
        /// </summary>
        [Test]
        public void NumModifyTest()
        {
            NumModifyFunctionInfo numModify = new NumModifyFunctionInfo();

            Assert.AreEqual("1.10", numModify.Evaluate(new object[] { 1.111, -2, 0.5 }).ToString());
            Assert.AreEqual("1.15", numModify.Evaluate(new object[] { 1.151, -2, 0.5 }).ToString());
            Assert.AreEqual("1.0", numModify.Evaluate(new object[] { 1.234, -1, 0.5 }).ToString());
            Assert.AreEqual("#VALUE!", numModify.Evaluate(new object[] { "数据错误", -1, 0.5 }).ToString());
            Object obj = numModify.Evaluate(new object[] { 25.21960, -1, 0 });

            Assert.AreEqual("252", (float.Parse(obj.ToString()) * 10).ToString());


            NumericalRound numericalRound = new NumericalRound();

            Assert.AreEqual(1.11, numericalRound.Evaluate(1.114, -2));
            Assert.AreEqual(1.24, numericalRound.Evaluate(1.236, -2));
            Assert.AreEqual(1.5, numericalRound.Evaluate(1.505000, -2));
            Assert.AreEqual(1.51, numericalRound.Evaluate(1.505001, -2));
            Assert.AreEqual(1.52, numericalRound.Evaluate(1.515000, -2));
            
            /*
             60.25 120.50 120 60.0?
60.38 120.76 121 60.5?
-60.75 -121.50 -122 -61.0?
             * 14.995
             */
            NumericalRound05 n5 = new NumericalRound05(numericalRound);
            Assert.AreEqual(6.00, n5.Evaluate(6.025, -2));
            Assert.AreEqual(60.5, n5.Evaluate(60.38, -1));
            Assert.AreEqual(-6.0, n5.Evaluate(-6.075, -1));
            Assert.AreEqual(15, n5.Evaluate(14.995, -2));
            Assert.AreEqual(1250, n5.Evaluate(1234, 1));

            /*
             830 4150 4.2×103 8.4×102
842 4210 4.2×103 8.4×102
-930 -4650 -4.6×103 -9.2×102
             * 12.32   12.38   11.25   11.23
             */
            NumericalRound02 n2 = new NumericalRound02(numericalRound);
            Assert.AreEqual(840, n2.Evaluate(830, 1));
            Assert.AreEqual(840, n2.Evaluate(842, 1));
            Assert.AreEqual(-920, n2.Evaluate(-930, 1));
            Assert.AreEqual(1.06, n2.Evaluate(1.051, -2));
            Assert.AreEqual(840, n2.Evaluate(841, 0));

            Assert.AreEqual(12.4, n2.Evaluate(12.32, -1));
            Assert.AreEqual(12.4, n2.Evaluate(12.38, -1));
            Assert.AreEqual(11.2, n2.Evaluate(11.25, -1));
            Assert.AreEqual(11.2, n2.Evaluate(11.23, -1));
        }

        [Test]
        public void JZSortTest()
        {
            JZSortFunctionInfo jzSort = new JZSortFunctionInfo();

            Assert.AreEqual("1", jzSort.Evaluate(new object[] { 1, 2, 3, 0 }).ToString());
            Assert.AreEqual("2", jzSort.Evaluate(new object[] { 1, 2, 3, 1 }).ToString());
            Assert.AreEqual("3", jzSort.Evaluate(new object[] { 1, 2, 3, 2 }).ToString());

            Assert.AreEqual("1", jzSort.Evaluate(new object[] { 1, 1, 1, 0 }).ToString());
            Assert.AreEqual("3", jzSort.Evaluate(new object[] { 0, 8, 3, 1 }).ToString());
            Assert.AreEqual("3", jzSort.Evaluate(new object[] { "/", "", 3, 2 }).ToString());

            Assert.AreEqual("/", jzSort.Evaluate(new object[] { 1, "/", "/", 1}).ToString());

            Assert.AreEqual("/", jzSort.Evaluate(new object[] { "/", 2, 3, 0 }).ToString());

            Assert.AreEqual("1", jzSort.Evaluate(new object[] { 3, 1, "/", 1 }).ToString());
        }
    }
}
