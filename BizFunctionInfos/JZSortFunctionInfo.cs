using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    public class JZSortFunctionInfo : FunctionInfo
    {
        public override object Evaluate(object[] args)
        {
            Double a1 = 0;
            Double a2 = 0;
            Double a3 = 0;

            if (args[0] == null)
            {
                a1 = 0;
            }
            else if (args[0].ToString().Trim() == "" || args[0].ToString().Trim() == "/")
            {
                a1 = 0;
            }
            else
            {
                a1 = Convert.ToDouble(args[0]);
            }

            if (args[1] == null)
            {
                a2 = 0;
            }
            else if (args[1].ToString().Trim() == "" || args[1].ToString().Trim() == "/")
            {
                a2 = 0;
            }
            else
            {
                a2 = Convert.ToDouble(args[1]);
            }

            if (args[2] == null)
            {
                a3 = 0;
            }
            else if (args[2].ToString().Trim() == "" || args[2].ToString().Trim() == "/")
            {
                a3 = 0;
            }
            else
            {
                a3 = Convert.ToDouble(args[2]);
            }

            Object result = null;
            try
            {
                List<Double> list = new List<Double>();
                Int32 index0 = 0;
                Int32 index1 = 0;
                Int32 index2 = 0;

                list.Add(a1);
                list.Add(a2);
                list.Add(a3);

                if (list[0] < list[1])
                {
                    if (list[0] <= list[2])
                    {
                        index0 = 0;
                        if (list[1] < list[2])
                        {
                            index1 = 1;
                            index2 = 2;
                        }
                        else
                        {
                            index2 = 1;
                            index1 = 2;
                        }
                    }
                    else
                    {
                        index0 = 2;
                        index1 = 0;
                        index2 = 1;
                    }
                }
                else if (list[1] < list[2])
                {
                    index0 = 1;
                    if (list[0] <= list[2])
                    {
                        index1 = 0;
                        index2 = 2;
                    }
                    else
                    {
                        index1 = 2;
                        index2 = 0;
                    }
                }
                else
                {
                    index0 = 2;
                    index1 = 1;
                    index2 = 0;
                }
                Int32 index = 0;
                if (args[3].ToString() == "0")
                {
                    index = index0;
                }
                else if (args[3].ToString() == "1")
                {
                    index = index1;
                }
                else if (args[3].ToString() == "2")
                {
                    index = index2;
                }
                result = args[index];
            }
            catch
            {
            }

            return result;
        }

        public override int MaxArgs
        {
            get
            {
                return 4;
            }
        }

        public override int MinArgs
        {
            get
            {
                return 4;
            }
        }

        public override string Name
        {
            get
            {
                return "JZSORT";
            }
        }
    }
}
