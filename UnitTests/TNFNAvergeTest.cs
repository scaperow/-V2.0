using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BizFunctionInfos;

namespace UnitTests
{
    [TestFixture]
    public class TNFNAvergeTest
    {
        [Test]
        public void TNFNAverge()
        {

            var function = new TNFNAvergeFunctionInfo();

            var numbers = new object[] { 1, 5, 6, 3};
            var result = Math.Round((double)((5 + 6 + 3) / 3), 6);
            Assert.AreEqual(function.Evaluate(numbers).ToString(), result.ToString());

            numbers = new object[] { 133, 144, 156, 172};
            result = Math.Round((double)(133+144+156) /3,6);
            Assert.AreEqual(function.Evaluate(numbers).ToString(), result.ToString());

            numbers = new object[]{200,200,200,200};
            result = 200;
            Assert.AreEqual(function.Evaluate(numbers).ToString(), result.ToString());
       
            
            
        }
    }
}
