using System;
using System.Collections.Generic;
using System.Drawing;
using FarPoint.Win.Spread;
using ReportCommon;

namespace ReportCommon
{
    [Serializable]
    public class DataColumn : ICloneable
    {
        String _TableName = "";
        public string TableName
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

        String _TableText = "";
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

        String _FieldName = "";
        public string FieldName
        {
            get
            {
                return _FieldName;
            }
            set
            {
                _FieldName = value;
            }
        }

        String _FieldText = "";
        public String FieldText
        {
            get
            {
                return _FieldText;
            }
            set
            {
                _FieldText = value;
            }
        }

        DataSetting _DataSetting = DataSetting.Group;
        public DataSetting DataSetting
        {
            get
            {
                return _DataSetting;
            }
            set
            {
                _DataSetting = value;
            }
        }

        #region 自定义分组

        Boolean _IsUserDefinedGroup = false;
        public bool IsUserDefinedGroup
        {
            get
            {
                return _IsUserDefinedGroup;
            }
            set
            {
                _IsUserDefinedGroup = value;
            }
        }

        List<CombineFilterCondition> _DefinedGroups = new List<CombineFilterCondition>();
        public List<CombineFilterCondition> DefinedGroups
        {
            get
            {
                return _DefinedGroups;
            }
        }

        #endregion 自定义分组

        #region 汇总函数

        FunctionInfo _FunctionInfo = FunctionInfo.Sum;
        public FunctionInfo FunctionInfo
        {
            get
            {
                return _FunctionInfo;
            }
            set
            {
                _FunctionInfo = value;
            }
        }

        #endregion 汇总函数

        #region 过滤条件

        Boolean _UseParentCellFilter = true;
        public bool UseParentCellFilter
        {
            get
            {
                return _UseParentCellFilter;
            }
            set
            {
                _UseParentCellFilter = value;
            }
        }

        CombineFilterCondition _DataFilter;
        public CombineFilterCondition DataFilter
        {
            get
            {
                return _DataFilter;
            }
            set
            {
                _DataFilter = value;
            }
        }

        #endregion 过滤条件

        /// <summary>
        /// 插入的空白行
        /// </summary>
        int _BlankCount = 0;
        public int BlankCount
        {
            get
            {
                return _BlankCount;
            }
            set
            {
                _BlankCount = value;
            }
        }

        private GridElement GetElement(SheetView Report, string Index)
        {
            Cell Cell = Report.GetCellFromTag(null, Index);
            GridElement Element = Cell.Value as GridElement;
            return Element;
        }

        public override string ToString()
        {
            return this.TableText + "[" + this.FieldText + "]";
        }

        #region ICloneable 成员

        public object Clone()
        {
            DataColumn DataColumn = new DataColumn();
            DataColumn.TableName = this.TableName;
            DataColumn.TableText = this.TableText;
            DataColumn.FieldName = this.FieldName;
            DataColumn.FieldText = this.FieldText;
            DataColumn.DataSetting = this.DataSetting;
            DataColumn.IsUserDefinedGroup = this.IsUserDefinedGroup;
            DataColumn._DefinedGroups = this.DefinedGroups;
            DataColumn.FunctionInfo = this.FunctionInfo;
            DataColumn.UseParentCellFilter = this.UseParentCellFilter;
            DataColumn.DataFilter = this.DataFilter;
            DataColumn.BlankCount = this.BlankCount;

            return DataColumn;
        }

        #endregion
    }

    [Serializable]
    public class Formula : ICloneable
    {
        String _Expression = "";
        public string Expression
        {
            get
            {
                return _Expression;
            }
            set
            {
                _Expression = value;
            }
        }

        object _Result = "";
        public object Result
        {
            get
            {
                return _Result;
            }
            set
            {
                _Result = value;
            }
        }

        public override string ToString()
        {
            return this.Expression;
        }

        #region ICloneable 成员

        public object Clone()
        {
            Formula Formula = new Formula();
            Formula.Expression = this.Expression;
            Formula.Result = this.Result;
            return Formula;
        }

        #endregion
    }

    [Serializable]
    public class LiteralText : ICloneable
    {
        string _Text = "";
        public String Text
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

        #region ICloneable 成员

        public object Clone()
        {
            LiteralText LiteralText = new LiteralText();
            LiteralText.Text = this.Text;
            return LiteralText;
        }

        #endregion

        public override string ToString()
        {
            return Text;
        }
    }

    [Serializable]
    public class Slash : ICloneable
    {
        RotationStyle _RotationStyle = RotationStyle.Counterclockwise;
        public RotationStyle RotationStyle
        {
            get
            {
                return _RotationStyle;
            }
            set
            {
                _RotationStyle = value;
            }
        }

        String _Text = "";
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

        #region ICloneable 成员

        public object Clone()
        {
            Slash Slash = new Slash();
            Slash.RotationStyle = this.RotationStyle;
            Slash.Text = this.Text;
            return Slash;
        }

        #endregion
    }

    [Serializable]
    public class Picture : ICloneable
    {
        Image _Image = null;
        public System.Drawing.Image Image
        {
            get
            {
                return _Image;
            }
            set
            {
                _Image = value;
            }
        }

        #region ICloneable 成员

        public object Clone()
        {
            Picture Picture = new Picture();
            Picture.Image = Image.Clone() as Image;
            return Picture;
        }

        #endregion
    }

    [Serializable]
    public class Variable : ICloneable
    {
        String _Name = "";
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

        object _Value = "";
        public object Value
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

        public override string ToString()
        {
            return this.Name;
        }

        #region ICloneable 成员

        public object Clone()
        {
            Variable Variable = new Variable();
            Variable.Name = this.Name;
            Variable.Value = this.Value;
            return Variable;
        }

        #endregion
    }
}
