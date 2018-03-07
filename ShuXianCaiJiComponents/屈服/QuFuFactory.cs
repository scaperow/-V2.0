using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShuXianCaiJiComponents;

namespace ShuXianCaiJiComponents
{
    public static class QuFuFactory
    {
        public static QuFuBase GetQuFuModule(String qufuType)
        {
            QuFuBase qf = null;
            switch (qufuType)
            {
                case "通用":
                    qf = new CommonQuFu();
                    break;
                case "新通用":
                    qf = new NewCommonQuFu();
                    break;
                case "OKEQF":
                    qf = new OKEQF();
                    break;
                case "通用0722":
                    qf = new QFNewCommon();
                    break;
                case "FYQF":
                    qf = new FYQF();
                    break;
                default:
                    qf = new CommonQuFu();
                    break;
            }
            return qf;
        }
    }
}
