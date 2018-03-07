using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon
{
    /// <summary>
    /// 数据设置
    /// </summary>
    [Serializable]
    public enum DataSetting : byte
    {
        Group,
        List,
        Aggregation
    }

    /// <summary>
    /// 聚合方式
    /// </summary>
    [Serializable]
    public enum AggregationStyle : byte
    {
        Sum,
        Avg,
        Max,
        Min,
        None
    }

    /// <summary>
    /// 旋转方式
    /// </summary>
    [Serializable]
    public enum RotationStyle : byte
    {
        Clockwise,
        Counterclockwise
    }

    /// <summary>
    /// 逻辑运算符
    /// </summary>
    [Serializable]
    public enum BooleanOperation : byte
    {
        And,
        Or
    }

    /// <summary>
    /// 过滤条件的类型
    /// </summary>
    [Serializable]
    public enum FilterStyle : byte
    {
        DataColumn,
        Value,
        Parameter
    }
}
