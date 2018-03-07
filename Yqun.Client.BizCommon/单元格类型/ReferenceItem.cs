using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    /// <summary>
    /// 参照类
    /// </summary>
    [Serializable]
    public class ReferenceItem
    {
        public String TableName;
        public String ColumnName;
        public String ReferenceTableText;
        public String ReferenceTableName;
        public String ReferenceColumnText;
        public String ReferenceColumnName;
    }
}
