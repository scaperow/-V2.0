using System;
using System.Collections.Generic;
using System.Text;

namespace BizComponents
{
    public class ComboItem
    {
        private String _Name;
        public String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        private String _Text;
        public String Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }

        public override string ToString()
        {
            return Text;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
