using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class IndexDescriptionPair
    {
        String _Index;
        public String Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
        }

        String _Scts;
        public String Scts
        {
            get
            {
                return _Scts;
            }
            set
            {
                _Scts = value;
            }
        }

        String _Description;
        public String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        String _DataTable;
        public String DataTable
        {
            get
            {
                return _DataTable;
            }
            set
            {
                _DataTable = value;
            }
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
