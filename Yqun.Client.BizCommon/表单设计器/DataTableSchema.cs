using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class DataTableSchema
    {
        private TableDefineInfo _Schema = null;
        public TableDefineInfo Schema
        {
            get
            {
                return _Schema;
            }
            set
            {
                _Schema = value;
            }
        }

        public static string GetFieldList(TableDefineInfo Info)
        {
            if (Info == null)
                return "";

            List<string> Fields = new List<string>();
            foreach(FieldDefineInfo FieldInfo in Info.FieldInfos)
            {
                Fields.Add("[" + FieldInfo.FieldName + "]");
            }

            return string.Join(",", Fields.ToArray());
        }

        public FieldDefineInfo GetFieldInfo(String TableName,String FieldName)
        {
            FieldDefineInfo FieldInfo = null;
            TableDefineInfo TableInfo = null;
            if (Schema != null && Schema.Name.ToLower() == TableName.ToLower())
            {
                TableInfo = Schema;
            }

            if (TableInfo != null)
            {
                foreach (FieldDefineInfo Info in TableInfo.FieldInfos)
                {
                    if (Info.FieldName.ToLower() == FieldName.ToLower())
                    {
                        FieldInfo = Info;
                        break;
                    }
                }
            }

            return FieldInfo;
        }

        public Boolean HaveDataItem(string Range)
        {
            if (Schema == null)
                return false;

            Boolean Result = false;
            foreach (FieldDefineInfo Info in Schema.FieldInfos)
            {
                if (Info.RangeInfo == Range)
                {
                    Result = true;
                    break;
                }
            }

            return Result;
        }

        public FieldDefineInfo GetDataItem(string Range)
        {
            FieldDefineInfo Result = null;

            if (Schema == null)
                return Result;

            foreach (FieldDefineInfo Info in Schema.FieldInfos)
            {
                if (Info.RangeInfo == Range)
                {
                    Result = Info;
                    break;
                }
            }

            return Result;
        }

        public string[] Split(string Range)
        {
            int DigitIndex = Range.Length - 1;
            char[] chars = Range.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (Char.IsDigit(chars[i]))
                {
                    DigitIndex = i;
                    break;
                }
            }

            List<string> Parts = new List<string>();
            Parts.Add(Range.Substring(0, DigitIndex));
            Parts.Add(Range.Substring(DigitIndex));
            return Parts.ToArray();
        }

        public Boolean HaveDataTable(String TableName)
        {
            Boolean Result = false;
            if (_Schema != null)
                Result = (_Schema.Name == TableName);

            return Result;
        }
    }
}
