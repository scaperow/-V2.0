using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class Sys_Module
    {
        public Guid ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String CatlogCode { get; set; }
        /// <summary>
        /// 1：试验模板
        /// 2：人员、设备等
        /// </summary>
        public Int16 ModuleType { get; set; }
        /// <summary>
        /// 1：压力机
        /// 2：万能机
        /// </summary>
        public Int16 DeviceType { get; set; }
        /// <summary>
        /// 模板别名，用于上传设置
        /// </summary>
        public String ModuleALT { get; set; }
        public List<ModuleSetting> ModuleSettings { get; set; }
        public List<QualifySetting> QualifySettings { get; set; }
        public List<StatisticsModuleSetting> StatisticsSettings { set; get; }

        /// <summary>
        /// 上传设置
        /// </summary>
        public UploadSetting UploadSetting { get; set; }
        /// <summary>
        /// 报表页所在索引
        /// </summary>
        public Int32 ReportIndex { get; set; }
        /// <summary>
        /// 试验类型编码，用于工管中心上传设置
        /// </summary>
        public String ModuleALTGG { get; set; }
    }
}
