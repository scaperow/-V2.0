using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class JZFile
    {
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        /// <summary>
        /// all为全部更新，modules为几个模板
        /// </summary>
        public String FileType { get; set; }
    }
}
