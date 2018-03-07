using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    public class KLJPFunctionInfo : FunctionInfo
    {
        public override object Evaluate(object[] args)
        {
            if (args.Length != 24)
            {
                return true;
            }
            
            Boolean result = true;
            try
            {
                for (int i = 0; i < 12; i++)
                {
                    if (!ValidKLJP((args[i] ?? "").ToString(), (args[i + 12] ?? "").ToString()))
                    {
                        result = false;
                        break;
                    }
                }
            }
            catch
            {
            }
            
            return result;
        }

        private Boolean ValidKLJP(String bz, String value)
        {
            Boolean flag = false;
            if (String.IsNullOrEmpty(bz) || bz == "/")
            {
                flag = true;
            }
            else
            {
                String[] arr = bz.Split(new String[] { "～" }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length == 2)
                {
                    float start, end, v;
                    if (float.TryParse(arr[0], out start) &&
                        float.TryParse(arr[1], out end) &&
                        float.TryParse(value, out v))
                    {
                        if (v < start || v > end)
                        {
                            flag = false;
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                }
            }
            return flag;
        }

        public override int MaxArgs
        {
            get
            {
                return 24;
            }
        }

        public override int MinArgs
        {
            get
            {
                return 24;
            }
        }

        public override string Name
        {
            get
            {
                return "KLJP";
            }
        }
    }
}
