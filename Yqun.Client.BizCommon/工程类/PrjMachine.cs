using System;

namespace BizCommon
{
    [Serializable]
    public class PrjFolder
    {
        public PrjFolder()
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

        string _FolderCode;
        public String FolderCode
        {
            get
            {
                return _FolderCode;
            }
            set
            {
                _FolderCode = value;
            }
        }

        string _FolderName;
        public String FolderName
        {
            get
            {
                return _FolderName;
            }
            set
            {
                _FolderName = value;
            }
        }
        public override string ToString()
        {
            return _FolderName;
        }

        string _FolderType;
        public String FolderType
        {
            get
            {
                return _FolderType;
            }
            set
            {
                _FolderType = value;
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
