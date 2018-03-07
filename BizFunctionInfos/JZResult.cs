using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    public class JZResult : FunctionInfo
    {
        public override object Evaluate(object[] args)
        {
            if (args[0] == null)
            {
                return "";
            }
            if (args[0].ToString().Trim() == "")
            {
                return "";
            }
            String result = "";
            try
            {
                String[] list = args[0].ToString().Trim().Split(new String[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (String item in list)
                {
                    String[] arr = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length > 0)
                    {
                        result += arr[0] + ",";
                    }
                }
                
            }
            catch
            {
            }
            if (result.Length > 0 && result.EndsWith(","))
            {
                result = result.TrimEnd(',');
            }
            return result;
        }

        public override int MaxArgs
        {
            get
            {
                return 1;
            }
        }

        public override int MinArgs
        {
            get
            {
                return 1;
            }
        }

        public override string Name
        {
            get
            {
                return "JZRESULT";
            }
        }
    }
}
