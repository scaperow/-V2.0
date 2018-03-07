using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    #region 粗骨料颗粒级配 

    [Serializable]
    public class XGLGrainMate : FunctionInfo
    {
        public override string Name { get { return "XGLGrainMate"; } }
        //最少参数
        public override int MinArgs { get { return 2; } }
        //最多参数
        public override int MaxArgs { get { return 2; } }

        #region 粗骨料颗粒级配
        public override object Evaluate(object[] args)
        {
            try
            {
                #region 参数
                string s = string.Empty;
                CalcReference r = (CalcReference)args[0];
                if (r.GetValue(r.Row, r.Column) == null || r.GetValue(r.Row, r.Column) == DBNull.Value || r.GetValue(r.Row, r.Column).ToString().Trim() == "" || r.GetValue(r.Row, r.Column).ToString().Trim() == "/" || r.GetValue(r.Row, r.Column).ToString().Trim() == "#NAME?")
                {
                    s = string.Empty;
                }
                else
                {
                    s = CalcConvert.ToString(r.GetValue(r.Row, r.Column).ToString());
                    s = s.Trim();
                    s = s.Substring(0, s.Length - 2);
                }
                double d = 0;
                r = (CalcReference)args[1];
                if (r.GetValue(r.Row, r.Column) == null || r.GetValue(r.Row, r.Column) == DBNull.Value || r.GetValue(r.Row, r.Column).ToString().Trim() == "" || r.GetValue(r.Row, r.Column).ToString().Trim() == "/" || r.GetValue(r.Row, r.Column).ToString().Trim() == "#NAME?")
                {
                    d = 0;
                }
                else
                {
                    d = CalcConvert.ToDouble(r.GetValue(r.Row, r.Column).ToString());
                }
                #endregion

                if (s == string.Empty || d == 0)
                {
                    return DBNull.Value;
                }
                try
                {
                    #region 一(5～10)
                    if (s == "5～10")
                    {
                        //1
                        if (d == 2.36)
                        {
                            return "95～100";
                        }
                        //2
                        if (d == 4.75)
                        {
                            return "80～100";
                        }
                        //3
                        if (d == 9.5)
                        {
                            return "0～15";
                        }
                        //4
                        if (d == 16.0)
                        {
                            return "0";
                        }
                        //5,6,7,8,9,10,11,12
                        if (d == 19.0 || d == 26.5 || d == 31.5 || d == 37.5 || d == 53.0 || d == 63.0 || d == 75.0 || d == 90)
                        {
                            return "/";
                        }


                    }
                    #endregion

                    #region 二(5～16)
                    if (s == "5～16")
                    {
                        //1
                        if (d == 2.36)
                        {
                            return "95～100";
                        }
                        //2
                        if (d == 4.75)
                        {
                            return "85～100";
                        }
                        //3
                        if (d == 9.5)
                        {
                            return "30～60";
                        }
                        //4
                        if (d == 16.0)
                        {
                            return "0～10";
                        }
                        //5
                        if (d == 19.0)
                        {
                            return "0";
                        }
                        //6,7,8,9,10,11,12
                        if (d == 26.5 || d == 31.5 || d == 37.5 || d == 53.0 || d == 63.0 || d == 75.0 || d == 90)
                        {
                            return "/";
                        }

                    }
                    #endregion

                    #region 三(5～20)
                    if (s == "5～20")
                    {
                        //1
                        if (d == 2.36)
                        {
                            return "95～100";
                        }
                        //2
                        if (d == 4.75)
                        {
                            return "90～100";
                        }
                        //3
                        if (d == 9.5)
                        {
                            return "40～80";
                        }
                        //4
                        if (d == 16.0)
                        {
                            return "/";
                        }
                        //5
                        if (d == 19.0)
                        {
                            return "0～10";
                        }
                        //6
                        if (d == 26.5)
                        {
                            return "0";
                        }
                        //7,8,9,10,11,12
                        if (d == 31.5 || d == 37.5 || d == 53.0 || d == 63.0 || d == 75.0 || d == 90)
                        {
                            return "/";
                        }
                    }
                    #endregion

                    #region 四(5～25)
                    if (s == "5～25")
                    {
                        //1
                        if (d == 2.36)
                        {
                            return "95～100";
                        }
                        //2
                        if (d == 4.75)
                        {
                            return "90～100";
                        }
                        //3
                        if (d == 9.5)
                        {
                            return "/";
                        }
                        //4
                        if (d == 16.0)
                        {
                            return "30～70";
                        }
                        //5
                        if (d == 19.0)
                        {
                            return "/";
                        }
                        //6
                        if (d == 26.5)
                        {
                            return "0～5";
                        }
                        //7
                        if (d == 31.5)
                        {
                            return "0";
                        }
                        //8,9,10,11,12
                        if (d == 37.5 || d == 53.0 || d == 63.0 || d == 75.0 || d == 90)
                        {
                            return "/";
                        }
                    }
                    #endregion

                    #region 五(5～31.5)
                    if (s == "5～31.5")
                    {
                        //1
                        if (d == 2.36)
                        {
                            return "95～100";
                        }
                        //2
                        if (d == 4.75)
                        {
                            return "90～100";
                        }
                        //3
                        if (d == 9.5)
                        {
                            return "70～90";
                        }
                        //4
                        if (d == 16.0)
                        {
                            return "/";
                        }
                        //5
                        if (d == 19.0)
                        {
                            return "15～45";
                        }
                        //6
                        if (d == 26.5)
                        {
                            return "/";
                        }
                        //7
                        if (d == 31.5)
                        {
                            return "0～5";
                        }
                        //8
                        if (d == 37.5)
                        {
                            return "0";
                        }
                        //9,10,11,12
                        if (d == 53.0 || d == 63.0 || d == 75.0 || d == 90)
                        {
                            return "/";
                        }
                    }
                    #endregion

                    #region 六(5～40)
                    if (s == "5～40")
                    {
                        //1
                        if (d == 2.36)
                        {
                            return "/";
                        }
                        //2
                        if (d == 4.75)
                        {
                            return "95～100";
                        }
                        //3
                        if (d == 9.5)
                        {
                            return "70～90";
                        }
                        //4
                        if (d == 16.0)
                        {
                            return "/";
                        }
                        //5
                        if (d == 19.0)
                        {
                            return "30～65";
                        }
                        //6
                        if (d == 26.5)
                        {
                            return "/";
                        }
                        //7
                        if (d == 31.5)
                        {
                            return "/";
                        }
                        //8
                        if (d == 37.5)
                        {
                            return "0～5";
                        }
                        //9
                        if (d == 53.0)
                        {
                            return "0";
                        }
                        //10,11,12
                        if (d == 63.0 || d == 75.0 || d == 90)
                        {
                            return "/";
                        }
                    }
                    #endregion

                    #region 七(10～20)
                    if (s == "10～20")
                    {
                        //1
                        if (d == 2.36)
                        {
                            return "/";
                        }
                        //2
                        if (d == 4.75)
                        {
                            return "95～100";
                        }
                        //3
                        if (d == 9.5)
                        {
                            return "85～100";
                        }
                        //4
                        if (d == 16.0)
                        {
                            return "/";
                        }
                        //5
                        if (d == 19.0)
                        {
                            return "0～15";
                        }
                        //6
                        if (d == 26.5)
                        {
                            return "0";

                        }
                        //7,8,9,10,11,12
                        if (d == 31.5 || d == 37.5 || d == 53.0 || d == 63.0 || d == 75.0 || d == 90)
                        {
                            return "/";
                        }
                    }
                    #endregion

                    #region 八(16～31.5)
                    if (s == "16～31.5")
                    {
                        //2
                        if (d == 4.75)
                        {
                            return "95～100";
                        }
                        //4
                        if (d == 16.0)
                        {
                            return "85～100";
                        }
                        //7
                        if (d == 31.5)
                        {
                            return "0～10";
                        }
                        //8
                        if (d == 37.5)
                        {
                            return "0";
                        }
                        //1,3,5,6,9,10,11,12 
                        if (d == 2.36 || d == 9.5 || d == 19.0 || d == 26.5 || d == 53.5 || d == 63.0 || d == 75.0 || d == 90)
                        {
                            return "/";
                        }
                    }
                    #endregion

                    #region 九(20～40)
                    if (s == "20～40")
                    {
                        //3
                        if (d == 9.5)
                        {
                            return "95～100";
                        }
                        //5
                        if (d == 19.0)
                        {
                            return "80～100";
                        }
                        //8
                        if (d == 37.5)
                        {
                            return "0～10";
                        }
                        //9
                        if (d == 53.0)
                        {
                            return "0";
                        }
                        //1,2,4,6,7,10,11,12
                        if (d == 2.36 || d == 4.75 || d == 16.0 || d == 26.5 || d == 31.5 || d == 63.0 || d == 75.0 || d == 90)
                        {
                            return "/";
                        }

                    }
                    #endregion

                    #region 十(31.5～63)
                    if (s == "31.5～63")
                    {
                        //4
                        if (d == 16.0)
                        {
                            return "95～100";
                        }

                        //7
                        if (d == 31.5)
                        {
                            return "75～100";
                        }
                        //8
                        if (d == 37.5)
                        {
                            return "45～75";
                        }

                        //10
                        if (d == 63.0)
                        {
                            return "0～10";
                        }
                        //11
                        if (d == 75.0)
                        {
                            return "0";
                        }
                        //1,2,3,5,6,9,12
                        if (d == 2.36 || d == 4.75 || d == 9.5 || d == 19.0 || d == 26.5 || d == 53.0 || d == 90)
                        {
                            return "/";
                        }
                    }
                    #endregion

                    #region 十一(40～80)
                    if (s == "40～80")
                    {
                        //5
                        if (d == 19.0)
                        {
                            return "95～100";
                        }

                        //8
                        if (d == 37.5)
                        {
                            return "70～100";
                        }
                        //10
                        if (d == 63.0)
                        {
                            return "30～60";
                        }
                        //11
                        if (d == 75.0)
                        {
                            return "0～10";
                        }
                        //12
                        if (d == 90)
                        {
                            return "0";
                        }
                        //1,2,3,4,6,7,9
                        if (d == 2.36 || d == 4.75 || d == 9.5 || d == 16.0 || d == 26.5 || d == 31.5 || d == 53.0)
                        {
                            return "/";
                        }
                    }
                    #endregion

                    #region 十二(16～25)
                    if (s == "16～25")
                    {
                        //5
                        if (d == 9.5)
                        {
                            return "95～100";
                        }

                        //8
                        if (d == 16.0)
                        {
                            return "55～70";
                        }
                        //10
                        if (d == 19.0)
                        {
                            return "25～40";
                        }
                        //11
                        if (d == 26.5)
                        {
                            return "0～10";
                        }
                        
                        //1,2,3,4,6,7,9
                        if (d == 2.36 || d == 4.75 || d == 31.5 || d == 37.5 || d == 53.0|| d == 63.0 || d == 75.0 || d == 90)
                        {
                            return "/";
                        }
                    }
                    #endregion
                }
                catch { }

                return "/";
            }
            catch (Exception ee)
            {

                return DBNull.Value;
            }
        }
        #endregion

        #region 通用
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
        #endregion
    }
    #endregion
}
