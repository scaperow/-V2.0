using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    /// <summary>
    /// 颗粒分析的试验结果级配区属
    /// </summary>
    public class XGLMatchingArea : FunctionInfo
    {
        public override string Name 
        { 
            get 
            { 
                return "XGLMatchingArea"; 
            }
        }

        //最少参数
        public override int MinArgs 
        { 
            get 
            { 
                return 24; 
            } 
        }

        //最多参数
        public override int MaxArgs 
        { 
            get 
            { 
                return 24; 
            } 
        }

        #region 细骨料报告级配区属

        public override object Evaluate(object[] args)
        {
            try
            {
                #region 参数

                string[] strA = new string[24];
                CalcReference r;
                for (int i = 0; i < 24; i++)
                {
                    r = (CalcReference)args[i];
                    if (r.GetValue(r.Row, r.Column) == null || r.GetValue(r.Row, r.Column) == DBNull.Value || r.GetValue(r.Row, r.Column).ToString().Trim() == "" || r.GetValue(r.Row, r.Column).ToString().Trim() == "/" || r.GetValue(r.Row, r.Column).ToString().Trim() == "#NAME?")
                    {
                        strA[i] = string.Empty;
                    }
                    else
                    {
                        strA[i] = CalcConvert.ToString(r.GetValue(r.Row, r.Column).ToString());
                    }
                }

                #endregion

                //如果没有数据,则不作判断直接返回
                if (strA[23] == string.Empty)
                {
                    return "/";
                }

                //设置个整数变量,整体超出值是否大于5; 
                int n = 0;

                //先比较0.63(方孔) 12,13,14与15
                #region 12__Ⅰ区
                int[] iData = SplitData(strA[12]);
                try
                {
                    if (iData[0] >= Convert.ToInt32(strA[15].Trim()) && Convert.ToInt32(strA[15].Trim()) >= iData[1])
                    {
                        //只与Ⅰ区的相比较;
                        //比较5.00(圆孔)0(1,2)与3 
                        iData = SplitData(strA[0]);
                        if (iData[0] < Convert.ToInt32(strA[3].Trim()))
                        {
                            n += Convert.ToInt32(strA[3].Trim()) - iData[0];
                        }
                        else if (Convert.ToInt32(strA[3].Trim()) < iData[1])
                        {
                            n += iData[1] - Convert.ToInt32(strA[3].Trim());
                        }

                        //比较2.50(圆孔)4(5,6)与7 
                        iData = SplitData(strA[4]);
                        if (iData[0] < Convert.ToInt32(strA[7].Trim()))
                        {
                            n += Convert.ToInt32(strA[7].Trim()) - iData[0];
                        }
                        else if (Convert.ToInt32(strA[7].Trim()) < iData[1])
                        {
                            n += iData[1] - Convert.ToInt32(strA[7].Trim());
                        }

                        //比较1.25(方孔)8(9,10)与11 
                        iData = SplitData(strA[8]);
                        if (iData[0] < Convert.ToInt32(strA[11].Trim()))
                        {
                            n += Convert.ToInt32(strA[11].Trim()) - iData[0];
                        }
                        else if (Convert.ToInt32(strA[11].Trim()) < iData[1])
                        {
                            n += iData[1] - Convert.ToInt32(strA[11].Trim());
                        }

                        //比较0.315(方孔)16(17,18)与19 
                        iData = SplitData(strA[16]);
                        if (iData[0] < Convert.ToInt32(strA[19].Trim()))
                        {
                            n += Convert.ToInt32(strA[19].Trim()) - iData[0];
                        }
                        else if (Convert.ToInt32(strA[19].Trim()) < iData[1])
                        {
                            n += iData[1] - Convert.ToInt32(strA[19].Trim());
                        }

                        //比较0.160(方孔)20(21,22)与23  
                        iData = SplitData(strA[20]);
                        if (iData[0] < Convert.ToInt32(strA[23].Trim()))
                        {
                            n += Convert.ToInt32(strA[23].Trim()) - iData[0];
                        }
                        else if (Convert.ToInt32(strA[23].Trim()) < iData[1])
                        {
                            n += iData[1] - Convert.ToInt32(strA[23].Trim());
                        }
                        if (n > 5)
                        {
                            return "匹配错误";
                        }
                        else
                        {
                            return "Ⅰ区";
                        }
                    }
                }
                catch 
                { 
                }

                #endregion

                #region 13__Ⅱ区

                iData = SplitData(strA[13]);

                try
                {
                    if (iData[0] >= Convert.ToInt32(strA[15].Trim()) && Convert.ToInt32(strA[15].Trim()) >= iData[1])
                    {
                        //只与Ⅱ区的相比较;
                        //比较5.00(圆孔)    1(0,2)与3 
                        iData = SplitData(strA[1]);
                        if (iData[0] < Convert.ToInt32(strA[3].Trim()))
                        {
                            n += Convert.ToInt32(strA[3].Trim()) - iData[0];
                        }
                        else if (Convert.ToInt32(strA[3].Trim()) < iData[1])
                        {
                            n += iData[1] - Convert.ToInt32(strA[3].Trim());
                        }
                        //比较2.50(圆孔)    5(4,6)与7 
                        iData = SplitData(strA[5]);
                        if (iData[0] < Convert.ToInt32(strA[7].Trim()))
                        {
                            n += Convert.ToInt32(strA[7].Trim()) - iData[0];
                        }
                        else if (Convert.ToInt32(strA[7].Trim()) < iData[1])
                        {
                            n += iData[1] - Convert.ToInt32(strA[7].Trim());
                        }
                        //比较1.25(方孔)   9(8,10)与11 
                        iData = SplitData(strA[9]);
                        if (iData[0] < Convert.ToInt32(strA[11].Trim()))
                        {
                            n += Convert.ToInt32(strA[11].Trim()) - iData[0];
                        }
                        else if (Convert.ToInt32(strA[11].Trim()) < iData[1])
                        {
                            n += iData[1] - Convert.ToInt32(strA[11].Trim());
                        }
                        //比较0.315(方孔)17(16,18)与19 
                        iData = SplitData(strA[17]);
                        if (iData[0] < Convert.ToInt32(strA[19].Trim()))
                        {
                            n += Convert.ToInt32(strA[19].Trim()) - iData[0];
                        }
                        else if (Convert.ToInt32(strA[19].Trim()) < iData[1])
                        {
                            n += iData[1] - Convert.ToInt32(strA[19].Trim());
                        }
                        //比较0.160(方孔)21(20,22)与23  
                        iData = SplitData(strA[21]);
                        if (iData[0] < Convert.ToInt32(strA[23].Trim()))
                        {
                            n += Convert.ToInt32(strA[23].Trim()) - iData[0];
                        }
                        else if (Convert.ToInt32(strA[23].Trim()) < iData[1])
                        {
                            n += iData[1] - Convert.ToInt32(strA[23].Trim());
                        }
                        if (n > 5)
                        {
                            return "匹配错误";
                        }
                        else
                        {
                            return "Ⅱ区";
                        }
                    }
                }
                catch
                {
                }

                #endregion

                #region 14__Ⅲ区

                iData = SplitData(strA[14]);

                try
                {
                    if (iData[0] >= Convert.ToInt32(strA[15].Trim()) && Convert.ToInt32(strA[15].Trim()) >= iData[1])
                    {
                        //只与Ⅲ区的相比较;
                        //比较5.00(圆孔) (0,1)   2与3 
                        iData = SplitData(strA[2]);
                        if (iData[0] < Convert.ToInt32(strA[3].Trim()))
                        {
                            n += Convert.ToInt32(strA[3].Trim()) - iData[0];
                        }
                        else if (Convert.ToInt32(strA[3].Trim()) < iData[1])
                        {
                            n += iData[1] - Convert.ToInt32(strA[3].Trim());
                        }
                        //比较2.50(圆孔) (4,5)   6与7 
                        iData = SplitData(strA[6]);
                        if (iData[0] < Convert.ToInt32(strA[7].Trim()))
                        {
                            n += Convert.ToInt32(strA[7].Trim()) - iData[0];
                        }
                        else if (Convert.ToInt32(strA[7].Trim()) < iData[1])
                        {
                            n += iData[1] - Convert.ToInt32(strA[7].Trim());
                        }
                        //比较1.25(方孔) (8,9)  10与11 
                        iData = SplitData(strA[10]);
                        if (iData[0] < Convert.ToInt32(strA[11].Trim()))
                        {
                            n += Convert.ToInt32(strA[11].Trim()) - iData[0];
                        }
                        else if (Convert.ToInt32(strA[11].Trim()) < iData[1])
                        {
                            n += iData[1] - Convert.ToInt32(strA[11].Trim());
                        }
                        //比较0.315(方孔)(16,17)18与19 
                        iData = SplitData(strA[18]);
                        if (iData[0] < Convert.ToInt32(strA[19].Trim()))
                        {
                            n += Convert.ToInt32(strA[19].Trim()) - iData[0];
                        }
                        else if (Convert.ToInt32(strA[19].Trim()) < iData[1])
                        {
                            n += iData[1] - Convert.ToInt32(strA[19].Trim());
                        }
                        //比较0.160(方孔)(20,21)22与23  
                        iData = SplitData(strA[22]);
                        if (iData[0] < Convert.ToInt32(strA[23].Trim()))
                        {
                            n += Convert.ToInt32(strA[23].Trim()) - iData[0];
                        }
                        else if (Convert.ToInt32(strA[23].Trim()) < iData[1])
                        {
                            n += iData[1] - Convert.ToInt32(strA[23].Trim());
                        }
                        if (n > 5)
                        {
                            return "匹配\n\r错误";
                        }
                        else
                        {
                            return "Ⅲ区";
                        }
                    }
                }
                catch
                {
                }

                #endregion

                return "数据\n\r过大";
            }
            catch
            {
                return DBNull.Value;
            }
        }

        #endregion

        //拆分出数据（例如50～10）
        private int[] SplitData(string str)
        {
            int[] iArea = new int[2];
            string[] w = str.Split('～');
            if (w.Length > 1)
            {
                iArea[0] = Convert.ToInt32(w[0].Trim());
                iArea[1] = Convert.ToInt32(w[1].Trim());
            }
            return iArea;
        }

        public override bool AcceptsReference(int i)
        {
            return true;
        }

        public override bool AcceptsMissingArgument(int i)
        {
            return true;
        }

        public override bool AcceptsError(int i)
        {
            return true;
        }
    }
}
