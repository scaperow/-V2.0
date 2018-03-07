using System;
using System.Collections.Generic;
using System.ComponentModel;
using BizCommon;
using System.Xml.Serialization;

namespace BizCommon
{
    /// <summary>
    /// 字段类型接口，数据库中字段的实际类型
    /// </summary>
    [Serializable]
    public class FieldType
    {
        public static FieldType Number = new FieldType("数字", "DECIMAL", 9, 4, "DECIMAL", "NULL", "");
        public static FieldType Text = new FieldType("文本", "NVARCHAR", 4000, 0, "STRING", "NULL", "");
        public static FieldType LongText = new FieldType("长文本", "NVARCHAR", 4000, 0, "STRING", "NULL", "");
        public static FieldType Percent = new FieldType("百分号", "DECIMAL", 9, 4, "DECIMAL", "NULL", "");
        public static FieldType Image = new FieldType("图片", "VARBINARY(MAX)", 0, 0, "PICTURE", "NULL", "");
        public static FieldType HyperLink = new FieldType("超链接", "NVARCHAR", 60, 0, "HYPERLINK", "NULL", "");
        public static FieldType Currency = new FieldType("货币", "DECIMAL", 20, 4, "DECIMAL", "NULL", "");
        public static FieldType DateTime = new FieldType("日期时间", "DATETIME", 0, 0, "DATETIME", "NULL", "");
        public static FieldType CheckBox = new FieldType("复选框", "SMALLINT", 0, 0, "BOOLEAN", "NULL", "");
        public static FieldType RichText = new FieldType("上下标", "NVARCHAR(MAX)", 0, 0, "STRING", "NULL", "");
        public static FieldType BarCode = new FieldType("条形码", "DECIMAL", 18, 4, "DECIMAL", "NULL", "");
        public static FieldType DownList = new FieldType("下拉框", "NVARCHAR", 20, 0, "STRING", "NULL", "");
        public static FieldType Mask = new FieldType("掩码", "NVARCHAR", 50, 0, "MASK", "NULL", "");
        public static FieldType DeleteLine = new FieldType("删除线", "NVARCHAR", 4000, 0, "STRING", "NULL", "");

        public FieldType()
        {}

        public FieldType(string Description,
                         string BasicDataType,
                         int Length,
                         int Decimals,
                         string DisplayType,
                         string NullAble,
                         string CellType)
        {
            _Description = Description;
            _BasicDataType = BasicDataType;
            _Length = Length;
            _Decimals = Decimals;
            _DisplayType = DisplayType;
            _NullAble = NullAble;
            _CellType = CellType;
        }

        string _Description;
        public string Description
        {
            get
            {
                return _Description;
            }
        }

        string _BasicDataType;
        public string BasicDataType
        {
            get
            {
                return _BasicDataType;
            }
        }

        int _Length;
        public int Length
        {
            get
            {
                return _Length;
            }
        }

        int _Decimals;
        public int Decimals
        {
            get
            {
                return _Decimals;
            }
        }

        string _DisplayType;
        public string DisplayType
        {
            get
            {
                return _DisplayType;
            }
        }

        string _NullAble;
        public string NullAble
        {
            get
            {
                return _NullAble;
            }
        }

        string _CellType;
        public string CellType
        {
            get
            {
                return _CellType;
            }
        }

        public override string ToString()
        {
            return Description;
        }

        public static FieldType GetFieldType(String description)
        {
            if (description == "数字")
                return Number;
            else if (description == "百分号")
                return Percent;
            else if (description == "图片")
                return Image;
            else if (description == "超链接")
                return HyperLink;
            else if (description == "货币")
                return Currency;
            else if (description == "日期时间")
                return DateTime;
            else if (description == "复选框")
                return CheckBox;
            else if (description == "上下标")
                return RichText;
            else if (description == "条形码")
                return BarCode;
            else if (description == "下拉框")
                return DownList;
            else if (description == "掩码")
                return Mask;
            else if (description == "长文本")
                return LongText;
            else if (description == "删除线")
                return DeleteLine;
            else
                return Text;
        }
    }

    /// <summary>
    /// 获得字段类型
    /// </summary>
    public interface IGetFieldType
    {
        FieldType FieldType
        {
            get;
        }
    }

    /// <summary>
    /// 列权限接口，支持可编辑和可查看权限
    /// </summary>
    public interface IColumnItemAuth
    {
        Boolean Editable
        {
            get;
            set;
        }

        Boolean Viewable
        {
            get;
            set;
        }
    }

    public interface IConvertable
    {
        Boolean ConvertTo(FieldType FieldType);
        Boolean EqualsBasicDataType(FieldType FieldType);
    }

    public interface IReferenceData
    {
        Dictionary<string, string> Data
        {
            get;
        }
    }

    /// <summary>
    /// 参照单元格类型的下拉样式
    /// </summary>
    [Serializable]
    public enum DropDownStyle
    {
        TreeList,
        MultiColumnList
    }

    /// <summary>
    /// 参照类型
    /// </summary>
    [Serializable]
    public enum ReferenceStyle
    {
        Dictionary,
        Sheet
    }

    /// <summary>
    /// 列宽或行高的类型
    /// </summary>
    public enum AutoSizeFlags
    {
        Contents,
        Header,
    }
}
