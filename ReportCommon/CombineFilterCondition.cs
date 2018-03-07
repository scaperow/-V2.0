using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ReportCommon
{
    /// <summary>
    /// 组合条件
    /// </summary>
    [Serializable]
    public class CombineFilterCondition : ISerializable
    {
        public CombineFilterCondition()
        {
        }

        protected CombineFilterCondition(SerializationInfo info, StreamingContext context)
        {
            _FilterConditions = info.GetValue("FilterConditions", typeof(List<FilterCondition>)) as List<FilterCondition>;
        }

        List<FilterCondition> _FilterConditions = new List<FilterCondition>();
        public List<FilterCondition> FilterConditions
        {
            get
            {
                return _FilterConditions;
            }
        }

        #region ISerializable 成员

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FilterConditions", FilterConditions);
        }

        #endregion

        /// <summary>
        /// 获得条件中的表列信息
        /// </summary>
        /// <returns></returns>
        public List<string> GetTableColumnInfo()
        {
            List<string> tablecolumns = new List<string>();
            foreach (FilterCondition fc in FilterConditions)
            {
                if (!fc.IsFormula)
                {
                    tablecolumns.Add(fc.LeftItem.TableName + "." + fc.LeftItem.FieldName);
                    if (fc.RightItem.Style == FilterStyle.DataColumn)
                    {
                        tablecolumns.Add(fc.RightItem.TableName + "." + fc.RightItem.FieldName);
                    }
                }
            }

            return tablecolumns;
        }

        public override string ToString()
        {
            List<string> Filters = new List<string>();
            for (int i = 0; i < _FilterConditions.Count; i++)
            {
                FilterCondition Filter = _FilterConditions[i];
                if (i != 0)
                {
                    Filters.Add(Filter.ToString());
                }
                else
                {
                    String filter = Filter.ToString();
                    filter = filter.Replace(Filter.Operation.ToString(), "");
                    Filters.Add(filter);
                }
            }
            return string.Join("", Filters.ToArray());
        }
    }
}
