using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    /// <summary>
    /// 压力试验机接口数据结构体(PressureData)（一个试件信息）
    /// </summary>
    [Serializable]
    public class PressureData
    {
        /// <summary>
        /// 唯一id（流水号） 用于数据重发
        /// </summary>
        public string F_GUID { get; set; }
        /// <summary>
        /// 设备编码（由铁科院提供） 系统编码规范
        /// </summary>
        public string F_SBCODE { get; set; }
        /// <summary>
        /// 试验报告编号 当次试验报告编号
        /// </summary>
        public string F_RTCODE { get; set; }
        /// <summary>
        /// 委托编号 同一试验室不重复
        /// </summary>
        public string F_WTBH { get; set; }
        /// <summary>
        /// 试件序号
        /// </summary>
        public string F_SJBH { get; set; }
        /// <summary>
        /// 混凝土强度等级，如C70、C80
        /// </summary>
        public string F_QDDJ { get; set; }
        /// <summary>
        /// 试件实际尺寸长度如150X150X150
        /// </summary>
        public string F_SJCC { get; set; }
        /// <summary>
        /// 是否掺外加剂混凝土 ‘1’外加剂‘0’非外加剂
        /// </summary>
        public string F_ISWJJ { get; set; }
        /// <summary>
        /// 龄期(d) 整数如7、28、56等
        /// </summary>
        public int F_LQ { get; set; }
        /// <summary>
        /// 最大抗压力值(KN)
        /// </summary>
        public float F_KYLZ { get; set; }
        /// <summary>
        /// 采集时间 YYYY-MM-DD HH: MM:SS
        /// </summary>
        public string F_SYSJ { get; set; }
        /// <summary>
        /// 操作员 实名制用户名
        /// </summary>
        public string F_OPERATOR { get; set; }
        /// <summary>
        /// 原始抗压力值过程数据（逗号分割）
        /// </summary>
        public string F_YSKYLZ { get; set; }
        /// <summary>
        /// 厂家名称 如中国铁道科学研究院
        /// </summary>
        public string F_SOFTCOM { get; set; }
        /// <summary>
        /// 控制器厂家名称 如丰仪
        /// </summary>
        public string F_COLCOM { get; set; }

    }
    /// <summary>
    /// 万能试验机接口数据结构体(UniversalData)（一个试件信息）
    /// </summary>
    [Serializable]
    public class UniversalData
    {
        /// <summary>
        /// 唯一id（流水号） 用于数据重发
        /// </summary>
        public string F_GUID { get; set; }
        /// <summary>
        /// 设备编码（由铁科院提供） 系统编码规范
        /// </summary>
        public string F_SBCODE { get; set; }
        /// <summary>
        /// 试验报告编号 当次试验报告编号
        /// </summary>
        public string F_RTCODE { get; set; }
        /// <summary>
        /// 委托编号 同一试验室不重复
        /// </summary>
        public string F_WTBH { get; set; }
        /// <summary>
        /// 试件序号
        /// </summary>
        public string F_SJBH { get; set; }
        /// <summary>
        /// 牌号 如HRB400
        /// </summary>
        public string F_PZCODE { get; set; }
        /// <summary>
        /// 公称直径(mm)
        /// </summary>
        public float F_GCZJ { get; set; }
        /// <summary>
        /// 截面面积(mm2))
        /// </summary>
        public float F_AREA { get; set; }
        /// <summary>
        /// 最大抗拉力值(KN)
        /// </summary>
        public float F_LZ { get; set; }
        /// <summary>
        /// 屈服力值(KN)
        /// </summary>
        public float F_QFLZ { get; set; }
        /// <summary>
        /// 伸长率
        /// </summary>
        public float F_SCL { get; set; }
        /// <summary>
        /// 采集时间 YYYY-MM-DD HH: MM:SS
        /// </summary>
        public string F_SYSJ { get; set; }
        /// <summary>
        /// 操作员 实名制用户名
        /// </summary>
        public string F_OPERATOR { get; set; }
        /// <summary>
        /// 所有抗拉力值过程数据
        /// </summary>
        public string F_YSKLLZ { get; set; }
        /// <summary>
        /// 位移值过程数据 无数据传0
        /// </summary>
        public string F_WY { get; set; }
        /// <summary>
        /// 厂家名称 如中国铁道科学研究院
        /// </summary>
        public string F_SOFTCOM { get; set; }
        /// <summary>
        /// 控制器厂家名称 如丰仪
        /// </summary>
        public string F_COLCOM { get; set; }
    }

    /// <summary>
    /// 压力试验机接口数据结构体(PressureData)（一个试件信息）
    /// </summary>
    [Serializable]
    public class LabDocBasicInfo
    {
        /// <summary>
        /// 唯一id（流水号） 用于数据重发
        /// </summary>
        public string F_GUID { get; set; }
        /// <summary>
        /// 设备编码（由铁科院提供） 系统编码规范
        /// </summary>
        public string SBCODE { get; set; }
        /// <summary>
        /// 试验类型编码
        /// </summary>
        public string SYTYPE { get; set; }
        /// <summary>
        /// 委托编号 同一试验室不重复
        /// </summary>
        public string WTBH { get; set; }
        /// <summary>
        /// 试验记录编号
        /// </summary>
        public string JLBH { get; set; }
        /// <summary>
        /// 试验报告编号
        /// </summary>
        public string BGBH { get; set; }
        /// <summary>
        /// 委托单位
        /// </summary>
        public string WTDW { get; set; }
        /// <summary>
        /// 工程名称
        /// </summary>
        public string PRJNAME { get; set; }
        /// <summary>
        /// 规格种类
        /// </summary>
        public string GGTYPE { get; set; }
        /// <summary>
        /// 代表数量
        /// </summary>
        public string DBQUANTITY { get; set; }
        /// <summary>
        /// 代表数量单位
        /// </summary>
        public string DBUNIT { get; set; }
        /// <summary>
        /// 报告日期 YYYY-MM-DD
        /// </summary>
        public string SYDATE { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string SYRERSON { get; set; }
        /// <summary>
        /// 是否合格 0：不合格；1：合格；2无效
        /// </summary>
        public string ISSUCCESS { get; set; }
        /// <summary>
        /// 文档名称  用于服务器文件路径的存放 文档名称组成规则,试验类型名称+{设备编码+试验类型编码+’_’+当前时间戳精确到秒}+文件后缀名。如：钢筋试验{3MX1D1BD01ZX01YL010253_20131123161500}.xls
        /// </summary> 
        public string WDMC { get; set; }
        /// <summary>
        /// 试验机类型如果为压力机则为YL,如果为万能机则为WN,其它则为空
        /// </summary>
        public string SYJLX { get; set; }
        /// <summary>
        /// 对不合格试验简要说明，如:(第一组)的实测值为8.5，标准值为>=15；报告编号为12345
        /// </summary>
        public string NOSUCCESSINFO { get; set; }
        /// <summary>
        /// 软件厂家名称
        /// </summary>
        public string F_VENDER { get; set; }
    }

    [Serializable]
    public class LabDocContent 
    {
        /// <summary>
        /// 文档字符串 文件所对应的xml字符串
        /// </summary>
        public string DocContent { get; set; }
    }


    /// <summary>
    /// 工管中心上传设置
    /// </summary>
    [Serializable]
    public class GGCUploadSetting
    {
        public List<UploadSettingItem> Items { get; set; }
        public string Name { get; set; }
    }
}
