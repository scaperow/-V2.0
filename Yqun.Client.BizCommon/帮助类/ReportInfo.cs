using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using FarPoint.Win.Spread;

namespace BizCommon.帮助类
{
    [DataContract]
    public class ReportInfo
    {
        //报表类型
        [DataMember]
        public string reportType { get; set; }

        //判断本地或服务器
        [DataMember]
        public bool local { get; set; }

        //起始统计时间
        [DataMember]
        public string beginTime { get; set; }

        //终止统计时间
        [DataMember]
        public string endTime { get; set; }

        //统计范围
        [DataMember]
        public List<String> reportRound { get; set; }

        //结点Code
        [DataMember]
        public string leftTreeCode { get; set; }

        //表中行信息
        [DataMember]
        public ModelList sheetInfo { get; set; }

        //表中行
        [DataMember]
        public Int32 RowIndex { get; set; }
    }
}
