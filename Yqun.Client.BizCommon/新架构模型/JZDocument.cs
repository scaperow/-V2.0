using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class JZDocument
    {
        public Guid ID { get; set; }
        private List<JZSheet> _sheets = new List<JZSheet>();
        public List<JZSheet> Sheets
        {
            get
            {
                return _sheets;
            }
            set
            {
                _sheets = value;
            }
        }
    }
}
