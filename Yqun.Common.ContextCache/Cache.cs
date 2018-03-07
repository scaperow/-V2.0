using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace Yqun.Common.ContextCache
{
    public class Cache
    {
        static Hashtable _CustomCache = new Hashtable();
        public static Hashtable CustomCache
        {
            get
            {
                return _CustomCache;
            }
            set
            {
                _CustomCache = value;
            }
        }

        static DataSet _TableDictionary = null;
        public static DataSet TableDictionary
        {
            get
            {
                return _TableDictionary;
            }
            set
            {
                _TableDictionary = value;
            }
        }
    }
}
