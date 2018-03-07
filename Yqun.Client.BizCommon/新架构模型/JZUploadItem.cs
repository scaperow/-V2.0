using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class JZUploadItem
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String Value { get; set; }
    }

    [Serializable]
    public class JZUpload
    {
        public String Name { get; set; }
        public List<JZUploadItem> Items { get; set; }
    }
}
