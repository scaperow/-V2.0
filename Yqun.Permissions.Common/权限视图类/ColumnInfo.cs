using System;
using System.Collections.Generic;
using System.Text;

namespace Yqun.Permissions.Common
{
    public class ColumnInfo
    {
        string m_Text;
        public string Text
        {
            get
            {
                return m_Text;
            }
            set
            {
                m_Text = value;
            }
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

        string _Name;
        public string Name
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


    }
}
