using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    public class SheetTitle : FunctionInfo
    {
        public override object Evaluate(object[] args)
        {
            return Yqun.Common.ContextCache.ApplicationContext.Current.SheetTitle;
        }

        public override int MaxArgs
        {
            get 
            { 
                return 0; 
            }
        }

        public override int MinArgs
        {
            get 
            {
                return 0;
            }
        }

        public override string Name
        {
            get 
            {
                return "SheetTitle";
            }
        }
    }
}
