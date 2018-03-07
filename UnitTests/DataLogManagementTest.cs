using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Yqun.BO.BusinessManager;

namespace UnitTests
{
    public class DataLogManagementTest
    {
        [Test]
        public void TestLoginLog()
        {
            DataLogManager log = new DataLogManager();
            var dt = log.GetLoginLogInfo("","","",DateTime.Parse("2012-02-11"),DateTime.Parse("2013-11-01"),"aa",1,5,1);
        }
    }
}
