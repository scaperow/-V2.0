using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class JZSheet
    {
        public Guid ID { get; set; }
        public String Name { get; set; }
        private List<JZCell> _cells = new List<JZCell>();
        public List<JZCell> Cells
        {
            get
            {
                return _cells;
            }
            set
            {
                _cells = value;
            }
        }
    }
}
