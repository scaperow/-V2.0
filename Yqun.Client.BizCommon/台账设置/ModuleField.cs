using System;
using System.Drawing;

namespace BizCommon
{
    [Serializable]
    public enum ContentType : byte
    {
        单元格 = 1,
        公式
    }

    [Serializable]
    public class ModuleField
    {
        public ModuleField()
        {}

        String m_Index = Guid.NewGuid().ToString();
        public String Index
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

        String _ModuleIndex;
        public String ModuleIndex
        {
            get
            {
                return _ModuleIndex;
            }
            set
            {
                _ModuleIndex = value;
            }
        }

        String _ModuleCode;
        public String ModuleCode
        {
            get
            {
                return _ModuleCode;
            }
            set
            {
                _ModuleCode = value;
            }
        }

        String _TableName;
        public String TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value;
            }
        }

        String _TableText;
        public String TableText
        {
            get
            {
                return _TableText;
            }
            set
            {
                _TableText = value;
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

        ContentType _ContentType = ContentType.单元格;
        public ContentType ContentType
        {
            get
            {
                return _ContentType;
            }
            set
            {
                _ContentType = value;
            }
        }

        String _ContentFieldType;
        public String ContentFieldType
        {
            get
            {
                return _ContentFieldType;
            }
            set
            {
                _ContentFieldType = value;
            }
        }

        String _ContentText;
        public String ContentText
        {
            get
            {
                return _ContentText;
            }
            set
            {
                _ContentText = value;
            }
        }

        String _Contents;
        public String Contents
        {
            get
            {
                return _Contents;
            }
            set
            {
                _Contents = value;
            }
        }

        Color _ForeColor = Color.Black;
        public Color ForeColor
        {
            get
            {
                return _ForeColor;
            }
            set
            {
                _ForeColor = value;
            }
        }

        Color _BgColor = Color.White;
        public Color BgColor
        {
            get
            {
                return _BgColor;
            }
            set
            {
                _BgColor = value;
            }
        }

        String _DisplayStyle;
        public String DisplayStyle
        {
            get
            {
                return _DisplayStyle;
            }
            set
            {
                _DisplayStyle = value;
            }
        }

        float _ColumnWidth = 100f;
        public float ColumnWidth
        {
            get
            {
                return _ColumnWidth;
            }
            set
            {
                _ColumnWidth = value;
            }
        }

        Boolean _IsVisible = true;
        public Boolean IsVisible
        {
            get
            {
                return _IsVisible;
            }
            set
            {
                _IsVisible = value;
            }
        }

        Boolean _IsEdit = false;
        public Boolean IsEdit
        {
            get
            {
                return _IsEdit;
            }
            set
            {
                _IsEdit = value;
            }
        }

        Boolean _IsNull = false;
        public Boolean IsNull
        {
            get
            {
                return _IsNull;
            }
            set
            {
                _IsNull = value;
            }
        }

        Boolean _IsSystem = false;
        public Boolean IsSystem
        {
            get
            {
                return _IsSystem;
            }
            set
            {
                _IsSystem = value;
            }
        }

        int _OrderIndex;
        public int OrderIndex
        {
            get
            {
                return _OrderIndex;
            }
            set
            {
                _OrderIndex = value;
            }
        }
    }
}
