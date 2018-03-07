using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class StadiumConfig
    {
        private List<StadiumDay> _dayList = new List<StadiumDay>();
        /// <summary>
        /// 龄期提醒日期集合
        /// </summary>
        public List<StadiumDay> DayList
        {
            get
            {
                return _dayList;
            }
            set
            {
                _dayList = value;
            }
        }
        ///// <summary>
        ///// 同条件温度提醒值:表示温度累积需要达到的数值,0表示不设置
        ///// </summary>
        //public int Temperature { get; set; }
        ///// <summary>
        ///// 龄期提醒范围:表示仅在龄期到期后范围小时内提醒
        ///// </summary>
        //public int StadiumRange { get; set; }

        /// <summary>
        /// 制件日期
        /// </summary>
        public StadiumColumnInfo fZJRQ { get; set; }
        /// <summary>
        /// 试验编号
        /// </summary>
        public StadiumColumnInfo fSJBH { get; set; }

        /// <summary>
        /// 试件尺寸
        /// </summary>
        public StadiumColumnInfo fSJSize { get; set; }

        /// <summary>
        /// 报告编号
        /// </summary>
        public StadiumColumnInfo fBGBH { get; set; }

        /// <summary>
        /// 委托编号
        /// </summary>
        public StadiumColumnInfo fWTBH { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public StadiumColumnInfo fPH { get; set; }

        /// <summary>
        /// 附加字段，如钢筋，水泥等用来存放品种等级
        /// </summary>
        public StadiumColumnInfo fAdded { get; set; }
        
    }

    [Serializable]
    public class StadiumDay
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
        public StadiumColumnInfo PDays { get; set; }

        /// <summary>
        /// 保存时验证该龄期是否已经做了，用来更新龄期表的Is_Done字段
        /// </summary>
        public StadiumColumnInfo ValidInfo { get; set; }
    }

    [Serializable]
    public class StadiumColumnInfo
    {
        /// <summary>
        /// 列名
        /// </summary>
        public String ColumnName { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public String TableName { get; set; }
        

    }
}
