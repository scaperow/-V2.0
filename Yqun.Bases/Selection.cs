using System;
using System.Collections.Generic;
using System.Text;

namespace Yqun.Bases
{
    [Serializable]
    public class Selection : ICloneable
    {
        string _TypeFlag = "";
        public string TypeFlag
        {
            get
            {
                return _TypeFlag;
            }
            set
            {
                _TypeFlag = value;
            }
        }

        string _ID = "";
        public string ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        string _Code = "";
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

        string _Text = "";
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }

        string _Description = "";
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

        string _Value = "";
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        string _Value1 = "";
        public string Value1
        {
            get
            {
                return _Value1;
            }
            set
            {
                _Value1 = value;
            }
        }

        string _Value2 = "";
        public string Value2
        {
            get
            {
                return _Value2;
            }
            set
            {
                _Value2 = value;
            }
        }

        string _Flag = "";
        public string Flag
        {
            get
            {
                return _Flag;
            }
            set
            {
                _Flag = value;
            }
        }

        object _Tag = "";
        public object Tag
        {
            get
            {
                return _Tag;
            }
            set
            {
                _Tag = value;
            }
        }

        #region ICloneable 成员

        public object Clone()
        {
            Selection selection = new Selection();
            selection.TypeFlag = this.TypeFlag;
            selection.ID = this.ID;
            selection.Code = this.Code;
            selection.Text = this.Text;
            selection.Description = this.Description;
            selection.Value = this.Value;
            selection.Value1 = this.Value1;
            selection.Value2 = this.Value2;
            selection.Flag = this.Flag;
            selection.Tag = this.Tag;

            return selection;
        }

        #endregion
    }
}
