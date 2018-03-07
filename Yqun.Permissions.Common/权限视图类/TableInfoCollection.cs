using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Yqun.Permissions.Common
{
    public class TableInfoCollection : CollectionBase
    {
        public TableInfoCollection()
        {
        }

        public TableInfo this[int index]
        {
            get { return InnerList[index] as TableInfo; }
        }

        public int Add(TableInfo tableInfo)
        {
            if (InnerList.Contains(tableInfo))
                return InnerList.IndexOf(tableInfo);

            return InnerList.Add(tableInfo);
        }

        public void AddAt(TableInfo tableInfo, int index)
        {
            if (index < 0 || index > InnerList.Count - 1)
                return;

            if (Contains(tableInfo))
                return;

            InnerList.Insert(index, tableInfo);
        }

        public bool Contains(TableInfo tableInfo)
        {
            return InnerList.Contains(tableInfo);
        }

        public int IndexOf(TableInfo tableInfo)
        {
            return InnerList.IndexOf(tableInfo);
        }

        public void Remove(TableInfo tableInfo)
        {
            InnerList.Remove(tableInfo);
        }
    }
}
