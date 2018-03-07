using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShuXianCaiJiModule
{
    [Serializable]
    public class ComboBoxItem
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public ComboBoxItem(string pKey, string pValue)
        {
            this.Key = pKey;
            this.Value = pValue;
        }
        public override string ToString()
        {
            return this.Value;
        } 
    }
}
