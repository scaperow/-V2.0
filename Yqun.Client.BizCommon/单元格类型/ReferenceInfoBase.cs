using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class ReferenceInfoBase
    {
        private string _TableName;
        public String TableName
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

        private string _ColumnName;
        public String ColumnName
        {
            get
            {
                return _ColumnName;
            }
            set
            {
                _ColumnName = value;
            }
        }

        private string referencexml;
        public String ReferenceXml
        {
            get
            {
                return referencexml;
            }
            set
            {
                referencexml = value;
            }
        }
    }

    [Serializable]
    public class DictionaryReference : ReferenceInfoBase
    {
        private String _Index;
        public String DictionaryIndex
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
        }
    }

    [Serializable]
    public class SheetReference : ReferenceInfoBase
    {
        List<ReferenceItem> referenceItems = new List<ReferenceItem>();
        public List<ReferenceItem> ReferenceItems
        {
            get
            {
                return referenceItems;
            }
            set
            {
                referenceItems = value;
            }
        }

        public ReferenceItem RootReferenceItem
        {
            get
            {
                foreach (ReferenceItem Item in ReferenceItems)
                {
                    if (Item.TableName == TableName && Item.ColumnName == ColumnName)
                    {
                        return Item;
                    }
                }

                return null;
            }
        }

        DataFilterCondition _DataFilter = new DataFilterCondition();
        public DataFilterCondition DataFilter
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
    }
}
