using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    /// <summary>
    /// 颗粒分析的试验结果粗细程度
    /// </summary>
    public class ThickThinDegree : FunctionInfo
    {
        public override string Name 
        { 
            get 
            { 
                return "ThickThinDegree"; 
            } 
        }

        //最少1个参数
        public override int MinArgs 
        { 
            get 
            { 
                return 1; 
            } 
        }

        //最多1个参数
        public override int MaxArgs 
        {
            get 
            { 
                return 1; 
            } 
        }

        #region

        public override object Evaluate(object[] args)
        {
            try
            {
                #region 参数

                double c1 = 0;
                CalcReference r = (CalcReference)args[0];//从单元格获取参数
                try
                {
                    if (args[0].ToString().Trim() == string.Empty)
                        return DBNull.Value;
                    else
                        c1 = CalcConvert.ToDouble(args[0]);
                }
                catch
                {
                    if (r.GetValue(r.Row, r.Column) == null || r.GetValue(r.Row, r.Column) == DBNull.Value || r.GetValue(r.Row, r.Column).ToString().Trim() == "" || r.GetValue(r.Row, r.Column).ToString().Trim() == "/")
                    {
                        return DBNull.Value;
                    }
                    else
                    {
                        c1 = CalcConvert.ToDouble(r.GetValue(r.Row, r.Column).ToString());
                    }
                }

                #endregion

                #region 数据

                string c2 = string.Empty;
                if (c1 <= 3.7 && c1 >= 3.1)
                {
                    c2 = "粗砂";
                }
                if (c1 <= 3.0 && c1 >= 2.3)
                {
                    c2 = "中砂";
                }
                if (c1 <= 2.2 && c1 >= 1.6)
                {
                    c2 = "细砂";
                }
                return c2;

                #endregion
            }
            catch (Exception ee)
            {
                return DBNull.Value;
            }
        }

        #endregion

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
