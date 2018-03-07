using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BizFunctionInfos;

namespace UnitTests
{
    public class SNSixAvergeFunctionInfoTest
    {
        [Test]
        public void SNSixAvergeTest()
        {
            //SNSixAvergeFunctionInfo SNSixAverge = new SNSixAvergeFunctionInfo();
           // Assert.AreEqual("2", SNSixAverge.Evaluate(new object[]{2, 2, 2, 2, 2, 2, 0.15}).ToString());
           // Assert.AreEqual("15.283", Math.Round(Convert.ToSingle(SNSixAverge.Evaluate(new object[] { 15.9, 15.8, 14.5, 14.7, 15.5, 15.3, 0.15 })),3).ToString());
            //7.1   6.7   6.9  6.6  7.2  7.8

            //Assert.AreEqual("2", SNSixAverge.Evaluate(new object[] { 7, 7.6, 7.9, 8.4, 9.2, 9, 0.1 }).ToString());
            SNSixAvergeFunctionInfoGeneral sg = new SNSixAvergeFunctionInfoGeneral();
            Assert.AreEqual("7.0", sg.Evaluate(new object[] { 7.1, 6.7, 6.9, 6.6, 7.2, 7.8, 0.1, 4 }).ToString());
        }
    }
}
