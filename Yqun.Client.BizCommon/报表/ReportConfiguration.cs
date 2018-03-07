using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win.Spread;
using FarPoint.Win;

namespace BizCommon
{
    [Serializable]
    public class ReportConfiguration
    {
        static String blankSheet;
        public ReportConfiguration()
        {
            m_Index = Guid.NewGuid().ToString();

            SheetView SheetView = new SheetView();
            SheetView.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            blankSheet = Serializer.GetObjectXml(SheetView, "SheetView");
        }

        public static String BlankSheet
        {
            get
            {
                return blankSheet;
            }
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

        private String m_Description;
        public String Description
        {
            get
            {
                return m_Description;
            }
            set
            {
                m_Description = value;
            }
        }

        private String m_ReportStyle;
        public String ReportStyle
        {
            get
            {
                return m_ReportStyle;
            }
            set
            {
                m_ReportStyle = value;
            }
        }

        private String m_RowTag;
        public String RowTag
        {
            get
            {
                return m_RowTag;
            }
            set
            {
                m_RowTag = value;
            }
        }
    }

    [Serializable]
    public class ModelList : List<String>
    {
    }
}
