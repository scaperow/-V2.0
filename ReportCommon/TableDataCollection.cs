using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ReportCommon
{
    [Serializable]
    public class TableDataCollection : CollectionBase
    {
        public TableDataCollection()
        {
        }

        public TableData this[int index]
        {
            get { return InnerList[index] as TableData; }
        }

        public int Add(TableData TableData)
        {
            if (InnerList.Contains(TableData))
                return InnerList.IndexOf(TableData);

            return InnerList.Add(TableData);
        }

        internal void AddAt(TableData TableData, int index)
        {
            if (index < 0 || index > InnerList.Count - 1)
                return;

            if (Contains(TableData))
                return;

            InnerList.Insert(index, TableData);
        }

        public bool Contains(TableData TableData)
        {
            return InnerList.Contains(TableData);
        }

        public int IndexOf(TableData TableData)
        {
            return InnerList.IndexOf(TableData);
        }

        public int IndexOf(String TableName)
        {
            int Result = -1;
            foreach (TableData Data in InnerList)
            {
                if (Data.GetTableName().ToLower() == TableName.ToLower())
                {
                    Result = IndexOf(Data);
                    break;
                }
            }

            return Result;
        }

        public void Remove(TableData TableData)
        {
            InnerList.Remove(TableData);
        }

        public void Remove(String TableName)
        {
            int Index = IndexOf(TableName);
            if (Index != -1)
            {
                Remove(this[Index]);
            }
        }
    }
}
