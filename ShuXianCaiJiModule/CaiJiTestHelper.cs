using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BizCommon;

namespace ShuXianCaiJiModule
{
    public static class CaiJiTestHelper
    {
        public static List<JZRealTimeData> GetCaiJiTestData(String filePath)
        {
            List<JZRealTimeData> realTimeData = null;
            using (StreamReader sr = new StreamReader(filePath))
            {
                String line = sr.ReadToEnd();
                realTimeData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZRealTimeData>>(line);
            }
            return realTimeData;
        }
    }
}
