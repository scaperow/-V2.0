using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace UnitTests
{
    
    public class HexToFloat
    {
        // 16进制字符串,转化为浮点数
        public float HexToFloat2(String hexString)
        {
            uint num = uint.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier);
            byte[] floatVals = BitConverter.GetBytes(num);
            return BitConverter.ToSingle(floatVals, 0);
        }

        [Test]
        public void HexToFloatTest()
        {
            String result = HexToFloat2("0a973c43").ToString();
            Assert.AreEqual("432",result);

            float f = 188.59f;//由浮点数转十六进制数组
            String test = BitConverter.ToString(BitConverter.GetBytes(f)).Replace("-", " ");
            Assert.AreEqual("432", test);
        }
    }
}
