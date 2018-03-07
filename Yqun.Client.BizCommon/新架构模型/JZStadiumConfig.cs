using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class JZStadiumConfig
    {
        public List<JZStadiumDay> DayList { get; set; }
        /// <summary>
        /// 同条件温度提醒值:表示温度累积需要达到的数值,0表示不设置
        /// </summary>
        public int Temperature { get; set; }
        /// <summary>
        /// 龄期提醒范围:表示仅在龄期到期后范围小时内提醒
        /// </summary>
        public int StadiumRange { get; set; }
        public QualifySetting fAdded { get; set; }
        public QualifySetting fBGBH { get; set; }
        public QualifySetting fPH { get; set; }
        public QualifySetting fSJBH { get; set; }
        public QualifySetting fSJSize { get; set; }
        public QualifySetting fWTBH { get; set; }
        public QualifySetting fZJRQ { get; set; }
        public QualifySetting ShuLiang { get; set; }
    }

    [Serializable]
    public class JZStadiumDay
    {
        /// <summary>
        /// dbo.sys_biz_reminder_testitem主键
        /// </summary>
        public String ItemID { get; set; }

        /// <summary>
        /// dbo.sys_biz_reminder_testitem名称，描述试验项目
        /// </summary>
        public String ItemName { get; set; }

        /// <summary>
        /// 龄期天数
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// 龄期天数是否是变量
        /// </summary>
        public Boolean IsParameterDays { get; set; }

        /// <summary>
        /// 描述龄期天数从哪个表的哪个字段获取
        /// </summary>
        public QualifySetting PDays { get; set; }

        /// <summary>
        /// 保存时验证该龄期是否已经做了，用来更新龄期表的Is_Done字段
        /// </summary>
        public QualifySetting ValidInfo { get; set; }
    }
}
