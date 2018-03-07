using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Yqun.Permissions.Common
{
    public class ColumnInfoCollection : CollectionBase
    {
        public ColumnInfoCollection()
        {
        }

        public ColumnInfo this[int index]
        {
            get { return InnerList[index] as ColumnInfo; }
        }

        public int Add(ColumnInfo columnInfo)
        {
            if (InnerList.Contains(columnInfo))
                return InnerList.IndexOf(columnInfo);

            return InnerList.Add(columnInfo);
        }

        public void AddAt(ColumnInfo columnInfo, int index)
        {
            if (index < 0 || index > InnerList.Count - 1)
                return;

            if (Contains(columnInfo))
                return;

            InnerList.Insert(index, columnInfo);
        }

        public bool Contains(ColumnInfo columnInfo)
        {
            return InnerList.Contains(columnInfo);
        }

        public int IndexOf(ColumnInfo columnInfo)
        {
            return InnerList.IndexOf(columnInfo);
        }

        public void Remove(ColumnInfo columnInfo)
        {
            InnerList.Remove(columnInfo);
        }
    }
}
