using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class Sys_Temperature
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string CreateBy { set; get; }
        public string CreateTime { set; get; }
        /// <summary>
        /// 是否系统级别
        /// 1：是
        /// 0：否
        /// </summary>
        public int IsSystem { set; get; }
        public string TestRoomCode { set; get; }
    }
}
