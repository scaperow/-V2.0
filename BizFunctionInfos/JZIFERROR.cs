using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    /// <summary>
    /// 如果报错，返回传入的值
    /// </summary>
    public class JZIFERROR : FunctionInfo
    {
        public override object Evaluate(object[] args)
        {
            if (args[0] == null || args[0].ToString() == "")
            {
                return "";
            }
            Boolean flag = false;
            Object first = null;
            try
            {
                first = args[0];
                List<String> list = new List<string>();
                list.Add("#N/A");
                list.Add("#VALUE!");
                list.Add("#REF!");
                list.Add("#DIV/0!");
                list.Add("#NUM!");
                list.Add("#NAME?");
                list.Add("#NULL!");
                if (list.Contains(args[0].ToString().ToUpper()))
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            catch
            {
                flag = false;
            }
            if (flag)
            {
                return first;
            }
            else
            {
                return args[1];
            }
        }

        public override int MaxArgs
        {
            get
            {
                return 2;
            }
        }

        public override int MinArgs
        {
            get
            {
                return 2;
            }
        }

        public override string Name
        {
            get
            {
                return "JZIFERROR";
            }
        }
    }
}
