using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Yqun.BO.ReminderManager;
using NUnit.Framework;
using System.Data.Common;
using System.Data.SqlClient;
using Yqun.BO.BusinessManager;
 
 


namespace UnitTests
{
    public class StadiumReminder
    {
        [Test]
        public void test()
        {
            LabStadiumListManager dal = new LabStadiumListManager();
            //需要修改dataservice文件，让public IDbConnection Connection 属性返回本机的OLDDBConnection
            DataTable Data = dal.GetLabStadiumReminderInfos("0001000700010001");
        }

        [Test]
        public void MoveOldStadiumData()
        {
            StadiumManager sm = new StadiumManager();
            sm.MoveOldStadiumData();
        }
    }
}
