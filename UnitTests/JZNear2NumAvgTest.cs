using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BizFunctionInfos;

namespace UnitTests
{
    [TestFixture]
    public class JZNear2NumAvgTest
    {
        [Test]
        public void JZNear2NumAvg()
        {
            JZNear2NumAvgFunctionInfo near2numavg = new JZNear2NumAvgFunctionInfo();
            String result = near2numavg.Evaluate(new object[] {1.1234567,1.5,1.1 }).ToString();//HexToFloat2("0a973c43").ToString();
            Assert.AreEqual("1.4", result);
        }

    }
}
