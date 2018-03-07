using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Yqun.Permissions.Common
{
    public class SubModuleInfoCollection : CollectionBase
    {
        public SubModuleInfoCollection()
        {
        }

        public SubModuleInfo this[int index]
        {
            get { return InnerList[index] as SubModuleInfo; }
        }

        public int Add(SubModuleInfo subModuleInfo)
        {
            if (InnerList.Contains(subModuleInfo))
                return InnerList.IndexOf(subModuleInfo);

            return InnerList.Add(subModuleInfo);
        }

        public void AddAt(SubModuleInfo subModuleInfo, int index)
        {
            if (index < 0 || index > InnerList.Count - 1)
                return;

            if (Contains(subModuleInfo))
                return;

            InnerList.Insert(index, subModuleInfo);
        }

        public bool Contains(SubModuleInfo subModuleInfo)
        {
            return InnerList.Contains(subModuleInfo);
        }

        public int IndexOf(SubModuleInfo subModuleInfo)
        {
            return InnerList.IndexOf(subModuleInfo);
        }

        public void Remove(SubModuleInfo subModuleInfo)
        {
            InnerList.Remove(subModuleInfo);
        }
    }
}
