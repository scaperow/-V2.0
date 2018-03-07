using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Yqun.Permissions.Common
{
    public class FunctionInfoCollection : CollectionBase
    {
        public FunctionInfoCollection()
        {
        }

        public FunctionInfo this[int index]
        {
            get { return InnerList[index] as FunctionInfo; }
        }

        public int Add(FunctionInfo functionInfo)
        {
            if (InnerList.Contains(functionInfo))
                return InnerList.IndexOf(functionInfo);

            return InnerList.Add(functionInfo);
        }

        public void AddAt(FunctionInfo functionInfo, int index)
        {
            if (index < 0 || index > InnerList.Count - 1)
                return;

            if (Contains(functionInfo))
                return;

            InnerList.Insert(index, functionInfo);
        }

        public bool Contains(FunctionInfo functionInfo)
        {
            return InnerList.Contains(functionInfo);
        }

        public int IndexOf(FunctionInfo functionInfo)
        {
            return InnerList.IndexOf(functionInfo);
        }

        public void Remove(FunctionInfo functionInfo)
        {
            InnerList.Remove(functionInfo);
        }
    }
}
