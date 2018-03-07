using System;

namespace BizCommon
{
    [Serializable]
    public class Prjsct
    {
        public Prjsct()
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

        string _PrjsctCode;
        public String PrjsctCode
        {
            get
            {
                return _PrjsctCode;
            }
            set
            {
                _PrjsctCode = value;
            }
        }

        string _PrjsctName;
        public String PrjsctName
        {
            get
            {
                return _PrjsctName;
            }
            set
            {
                _PrjsctName = value;
            }
        }

        public override string ToString()
        {
            return _PrjsctName;
        }

        string _PegFrom;
        public String PegFrom
        {
            get
            {
                return _PegFrom;
            }
            set
            {
                _PegFrom = value;
            }
        }

        string _PegTo;
        public String PegTo
        {
            get
            {
                return _PegTo;
            }
            set
            {
                _PegTo = value;
            }
        }

        string _Price;
        public String Price
        {
            get
            {
                return _Price;
            }
            set
            {
                _Price = value;
            }
        }

        string _OrderID;
        public String OrderID
        {
            get
            {
                return _OrderID;
            }
            set
            {
                _OrderID = value;
            }
        }
    }
}
