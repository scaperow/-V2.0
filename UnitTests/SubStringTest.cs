using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace UnitTests
{
    public class SubStringTest
    {
        [Test]
        public void RunSubStringTest()
        {
            String smsContent = "你好倒萨飞色飞洒刘继芬减肥绿色<>43dfa发的色飞洒地方额发生的粉色发的是粉色盛大方法是的》》==9087爱的色放45范德萨发大水发大水A34的飞洒";
            String subContent = smsContent;
            int i = 1;
            while (subContent.Length > 70)
            {
                subContent = "(" + i + ")" + subContent;
                string content = subContent.Substring(0, 70);
                //errorMsg += SMSAgent.CallRemoteService(cells, content);
                subContent = subContent.Replace(content, "");
                i++;
            }
            if (subContent.Length > 0)
            {
                subContent = "(" + i + ")" + subContent;
                //errorMsg += SMSAgent.CallRemoteService(cells,  subContent);
            }
            //34的飞洒
            Assert.AreEqual(subContent, "(2)大水A34的飞洒");
        }

        [Test]
        public void DateTimeSubTest()
        {
            //(DateTime.Now - zjrq).Days > days
            Int32 days = 28;
            DateTime zjrq = DateTime.Parse("2013-06-19");
            Assert.IsTrue((DateTime.Now - zjrq).Days > days);
        }
    }
}
