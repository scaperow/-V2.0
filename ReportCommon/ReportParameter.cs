using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win.Spread.CellType;

namespace ReportCommon
{
    [Serializable]
    public class ReportParameter
    {
        String index;
        String reportindex;
        String name;
        String displayname;
        Object value = null;

        public ReportParameter() 
            : this("")
        {

        }

        public ReportParameter(String name) 
            : this(name, "")
        {
        }

        public ReportParameter(String name, Object value)
        {
            this.Index = Guid.NewGuid().ToString();
            this.Name = name;
            this.Value = value;
        }

        public String Index
        {
            get
            {
                return this.index;
            }
            set
            {
                this.index = value;
            }
        }

        public String ReportIndex
        {
            get
            {
                return this.reportindex;
            }
            set
            {
                this.reportindex = value;
            }
        }

        public String Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public String DisplayName
        {
            get
            {
                return this.displayname;
            }
            set
            {
                this.displayname = value;
            }
        }

        public Object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        public override string ToString()
        {
            return this.name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    [Serializable]
    public enum EditorType : byte
    {
        None,
        DateTime,
        Text,
        Number,
        Float,
        DownList
    }

    public interface IEditorType
    {

    }
}
