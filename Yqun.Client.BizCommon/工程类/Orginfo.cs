using System;

namespace BizCommon
{
    [Serializable]
    public class Orginfo
    {
        public Orginfo()
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

        string _DepCode;
        public String DepCode
        {
            get
            {
                return _DepCode;
            }
            set
            {
                _DepCode = value;
            }
        }

        string _DepName;
        public String DepName
        {
            get
            {
                return _DepName;
            }
            set
            {
                _DepName = value;
            }
        }
        public override string ToString()
        {
            return _DepName;
        }


        string _DepType = "";
        public String DepType
        {
            get
            {
                return _DepType;
            }
            set
            {
                _DepType = value;
            }
        }

        string _DepAbbrev;
        public String DepAbbrev
        {
            get
            {
                return _DepAbbrev;
            }
            set
            {
                _DepAbbrev = value;
            }
        }

        string _ConstructionCompany;
        public String ConstructionCompany
        {
            get
            {
                return _ConstructionCompany;
            }
            set
            {
                _ConstructionCompany = value;
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
