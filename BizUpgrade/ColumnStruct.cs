using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADOX;

namespace BizUpgrade
{
    /// <summary>
    /// 创建列对象
    /// </summary>
    public class ColumnStruct
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string _ColumnName;
        /// <summary>
        /// 数据类型
        /// </summary>
        public DataTypeEnum _DataTypeEnum;
        /// <summary>
        /// 数据长度
        /// </summary>
        public int _ColumnLong;
        /// <summary>
        /// 是否主键，True主键；False非主键
        /// </summary>
        public bool _IsKeyPrimary;

        /// <summary>
        /// 初始化值
        /// </summary>
        /// <param name="_ColumnName"></param>
        /// <param name="_DataTypeEnum"></param>
        /// <param name="_ColumnLong"></param>
        /// <param name="_IsKeyPrimary"></param>
        public ColumnStruct(string _ColumnName, DataTypeEnum _DataTypeEnum, int _ColumnLong, bool _IsKeyPrimary)
        {
            this._ColumnName = _ColumnName;
            this._DataTypeEnum = _DataTypeEnum;
            this._ColumnLong = _ColumnLong;
            this._IsKeyPrimary = _IsKeyPrimary;
        }
    }
}
