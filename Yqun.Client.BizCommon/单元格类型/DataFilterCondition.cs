using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class DataFilterCondition
    {
        List<DataFilterItem> _Items = new List<DataFilterItem>();
        public List<DataFilterItem> Items
        {
            get
            {
                return _Items;
            }
        }

        public Boolean IsValidExpression()
        {
            return Items.Count > 0;
        }

        public String GetExpression()
        {
            List<String> FilterStrings = new List<string>();
            foreach (DataFilterItem Item in Items)
            {
                int Index = Items.IndexOf(Item);
                if (Index == 0)
                {
                    FilterStrings.Add(Item.ToExpression());
                }
                else
                {
                    FilterStrings.Add(string.Format("{0} {1}", Item.ConditionalOperator, Item.ToExpression()));
                }
            }

            return String.Concat("(", String.Join(" ", FilterStrings.ToArray()), ")");
        }
    }
}
