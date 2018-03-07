using System;

namespace BizCommon
{
    [Serializable]
    public class PrjDictionary
    {
        public PrjDictionary()
        {
            m_Index = Guid.NewGuid().ToString();
        }

        string m_Index;
        public string Index
        {
            get
            {
                return m_Index;
            }
            set
            {
                m_Index = value;
            }
        }

        string _Dictionary;
        public string Dictionary
        {
            get
            {
                return _Dictionary;
            }
            set
            {
                _Dictionary = value;
            }
        }

        string _Description;
        public string Description
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

        public override string ToString()
        {
            return Description;
        }

        string _Code;
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }

        string _ParentCode;
        public string ParentCode
        {
            get
            {
                return _ParentCode;
            }
            set
            {
                _ParentCode = value;
            }
        }
    }
}
