using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShuXianCaiJiModule.parse
{
    public interface JZParseBase
    {
        float Parse(object obj);
        void SetModel(SXCJModule _SXCJModule);
    }
}
