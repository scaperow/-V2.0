using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class Sys_Document
    {
        public Guid ID { get; set; }
        public String TestRoomCode { get; set; }
        /// <summary>
        /// 0: 删除
        /// 1：正式保存
        /// 2：表示允许修改
        /// 3：表示草稿
        /// </summary>
        public Int16 Status { get; set; }
        public String DataName { get; set; }
        public String TryType { get; set; }
        public Guid ModuleID { get; set; }
        public String BGBH { get; set; }
        public DateTime? LastEditedTime { get; set; }
    }
}
