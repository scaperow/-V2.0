using System;

namespace BizCommon
{
    [Serializable]
    public class Project
    {
        public Project()
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

        string _Description;
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
        

        public override string ToString()
        {
            return _Description;
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

        string _LineName;
        public String LineName
        {
            get
            {
                return _LineName;
            }
            set
            {
                _LineName = value;
            }
        }

        string _HigWayClassification;
        public String HigWayClassification
        {
            get
            {
                return _HigWayClassification;
            }
            set
            {
                _HigWayClassification = value;
            }
        }

        double _TotalDistance;
        public Double TotalDistance
        {
            get
            {
                return _TotalDistance;
            }
            set
            {
                _TotalDistance = value;
            }
        }

        double _ToltalPrice;
        public Double ToltalPrice
        {
            get
            {
                return _ToltalPrice;
            }
            set
            {
                _ToltalPrice = value;
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
