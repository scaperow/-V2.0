using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Collections;
using System.Data;

namespace UnitTests
{
    public class SqlParaTest
    {
        [Test]
        public void SqlPrarmeterTest()
        {
            String sql = @" EXEC dbo.sp_pager @p0, @p1,@p2, @p3, @p4,@p5,@p6,@p7 ";

            ArrayList arrayList = new ArrayList();
            arrayList.Add("'biz_norm_试验室综合情况登记表'");
            arrayList.Add(" 'col_norm_d4, col_norm_d5, col_norm_d6, col_norm_d7'");
            arrayList.Add(" 'col_norm_d4' ");
            arrayList.Add(10);
            arrayList.Add(1);
            arrayList.Add(0);
            arrayList.Add(1);
            arrayList.Add("''");
            Yqun.Data.DataBase.DataService db = new Yqun.Data.DataBase.DataService();
            DataSet ds = db.GetDataSet(sql, arrayList.ToArray());
            Assert.AreEqual(1, ds.Tables.Count);
            //DataSet ds = GetDataSet(sql_select, arrayList.ToArray());
        }
    }
}
