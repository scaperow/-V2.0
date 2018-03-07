using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class CrossSheetFormulaInfo
    {
        public CrossSheetFormulaInfo()
        {
            m_Index = Guid.NewGuid().ToString();
        }

        private String m_Index;
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

        private String m_ModelIndex;
        public String ModelIndex
        {
            get
            {
                return m_ModelIndex;
            }
            set
            {
                m_ModelIndex = value;
            }
        }

        private String m_SheetIndex;
        public String SheetIndex
        {
            get
            {
                return m_SheetIndex;
            }
            set
            {
                m_SheetIndex = value;
            }
        }

        private int m_RowIndex;
        public int RowIndex
        {
            get
            {
                return m_RowIndex;
            }
            set
            {
                m_RowIndex = value;
            }
        }

        private int m_ColumnIndex;
        public int ColumnIndex
        {
            get
            {
                return m_ColumnIndex;
            }
            set
            {
                m_ColumnIndex = value;
            }
        }

        private String m_Formula;
        public String Formula
        {
            get
            {
                return m_Formula;
            }
            set
            {
                m_Formula = value;
            }
        }
    }
}
