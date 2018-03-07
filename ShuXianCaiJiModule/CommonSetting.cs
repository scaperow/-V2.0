using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShuXianCaiJiModule
{
    [Serializable]
    public class CommonSetting
    {
        /// <summary>
        /// 钢筋级别
        /// </summary>
        public List<ComboBoxItem> GJJBList { get; set; }

        /// <summary>
        /// 钢筋数量
        /// </summary>
        public List<ComboBoxItem> GJSLList { get; set; }

        /// <summary>
        /// 钢筋尺寸
        /// </summary>
        public List<ComboBoxItem> GJCCLList { get; set; }

        /// <summary>
        /// 混凝土级别
        /// </summary>
        public List<ComboBoxItem> HNTJBList { get; set; }

        /// <summary>
        /// 混凝土数量
        /// </summary>
        public List<ComboBoxItem> HNTSLList { get; set; }

        /// <summary>
        /// 混凝土尺寸
        /// </summary>
        public List<ComboBoxItem> HNTCCList { get; set; }

        /// <summary>
        /// 屈服名称集合
        /// </summary>
        public List<ComboBoxItem> QFNameList { get; set; }

        /// <summary>
        /// 仪表厂家
        /// </summary>
        public List<ComboBoxItem> CJNameList { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        public string MachineCode = string.Empty;

        /// <summary>
        /// 工管中心设备编码
        /// </summary>
        public string EMachineCode = string.Empty;

    }
}
