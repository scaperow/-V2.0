using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BizFunctionInfos;

namespace UnitTests
{
    public class TongThreeAvergeFunctionInfoTest
    {
        [Test]
        public void TongThreeAvergeTest()
        {
            TongThreeAvergeFunctionInfo tta = new TongThreeAvergeFunctionInfo();
            Assert.AreEqual("2", tta.Evaluate(new object[] { 5.0, 6.9, 4.2, 0.15 }).ToString());
            //Assert.AreEqual("15.283", Math.Round(Convert.ToSingle(SNSixAverge.Evaluate(new object[] { 15.9, 15.8, 14.5, 14.7, 15.5, 15.3, 0.15 })), 3).ToString());
        }

        [Test]
        public void TongThreeAverageTest2()
        {
            TongThreeAvergeFunctionInfo tta = new TongThreeAvergeFunctionInfo();
            Assert.AreEqual("4.9", tta.Evaluate(new object[] { 5.4, 4.8, 4.5, 0.15 }).ToString());
        }
    }
}
