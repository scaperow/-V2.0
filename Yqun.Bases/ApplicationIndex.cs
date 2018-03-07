using System;
using System.Collections.Generic;
using System.Text;

namespace Yqun.Bases
{
    public class ApplicationIndex
    {
        static string _Identity;
        private static readonly object singletonLock = new object();
        public static string Value
        {
            get
            {
                if (string.IsNullOrEmpty(_Identity))
                {
                    lock (singletonLock)
                    {
                        if (string.IsNullOrEmpty(_Identity))
                            _Identity = Guid.NewGuid().ToString();
                    }
                }

                return _Identity;
            }
        }

        public static String AppIndex
        {
            get
            {
                return "LXRailway";
            }
        }
    }
}
