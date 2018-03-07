using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class UploadSetting
    {
        public String Name { get; set; }
        public List<UploadSettingItem> Items { get; set; }
    }

    [Serializable]
    public class UploadSettingItem
    {
        public String Name { get; set; }
        public Guid SheetID { get; set; }
        public String CellName { get; set; }
        public String Description { get; set; }
        public Boolean NeedSetting { get; set; }
    }

}
