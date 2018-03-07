using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    /// <summary>
    /// 龄期
    /// </summary>
    [Serializable]
    public class StadiumInfo
    {
        /// <summary>
        /// 委托编号
        /// </summary>
        string _DelCode;
        public string DelCode
        {
            get
            {
                return _DelCode;
            }
            set
            {
                _DelCode = value;
            }
        }

        /// <summary>
        /// 模板编号
        /// </summary>
        string _ModelCode;
        public string ModelCode
        {
            get
            {
                return _ModelCode;
            }
            set
            {
                _ModelCode = value;
            }
        }

        /// <summary>
        /// 制件日期
        /// </summary>
        DateTime _CreatDate;
        public DateTime CreatDate
        {
            get
            {
                return _CreatDate;
            }
            set
            {
                _CreatDate = value;
            }
        }

        /// <summary>
        /// 试件编号
        /// </summary>
        String _ItemCode;
        public String ItemCode
        {
            get
            {
                return _ItemCode;
            }
            set
            {
                _ItemCode = value;
            }
        }

        /// <summary>
        /// 试验名称
        /// </summary>
        String _TestName;
        public String TestName
        {
            get
            {
                return _TestName;
            }
            set
            {
                _TestName = value;
            }
        }

        /// <summary>
        /// 报告编号
        /// </summary>
        String _ReportCode;
        public String ReportCode
        {
            get
            {
                return _ReportCode;
            }
            set
            {
                _ReportCode = value;
            }
        }

        /// <summary>
        /// 数据表名
        /// </summary>
        String _TableName;
        public String TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value;
            }
        }

        /// <summary>
        /// 委托单表名
        /// </summary>
        String _SourceTableName;
        public String SourceTableName
        {
            get
            {
                return _SourceTableName;
            }
            set
            {
                _SourceTableName = value;
            }
        }

        /// <summary>
        /// 录入字段名
        /// </summary>
        String _ColumnName;
        public String ColumnName
        {
            get
            {
                return _ColumnName;
            }
            set
            {
                _ColumnName = value;
            }
        }

        /// <summary>
        /// 试件序号
        /// </summary>
        String _ItemIndex;
        public String ItemIndex
        {
            get
            {
                return _ItemIndex;
            }
            set
            {
                _ItemIndex = value;
            }
        }

        /// <summary>
        /// 委托编号字段名
        /// </summary>
        String _DelColumn;
        public String DelColumn
        {
            get
            {
                return _DelColumn;
            }
            set
            {
                _DelColumn = value;
            }
        }

        /// <summary>
        /// 数据ID
        /// </summary>
        String _DataID;

        /// <summary>
        /// 龄期ID
        /// </summary>
        public String DataID
        {
            get
            {
                return _DataID;
            }
            set
            {
                _DataID = value;
            }
        }

        /// <summary>
        /// 屈服力值字段名
        /// </summary>
        String _QFColumn;
        public String QFColumn
        {
            get
            {
                return _QFColumn;
            }
            set
            {
                _QFColumn = value;
            }
        }

        /// <summary>
        /// 断后标距字段名
        /// </summary>
        String _BJColumn;
        public String BJColumn
        {
            get
            {
                return _BJColumn;
            }
            set
            {
                _BJColumn = value;
            }
        }

        /// <summary>
        /// 断后标距
        /// </summary>
        String _DHBJ;
        public String DHBJ
        {
            get
            {
                return _DHBJ;
            }
            set
            {
                _DHBJ = value;
            }
        }

        /// <summary>
        /// 设备类型
        /// </summary>
        String _MachineType;
        public String MachineType
        {
            get
            {
                return _MachineType;
            }
            set
            {
                _MachineType = value;
            }
        }

        /// <summary>
        /// 设备编码
        /// </summary>
        String _EquipmentCode;
        public String EquipmentCode
        {
            get
            {
                return _EquipmentCode;
            }
            set
            {
                _EquipmentCode = value;
            }
        }

        /// <summary>
        /// 设备编码
        /// </summary>
        String _MachineCode;
        public String MachineCode
        {
            get
            {
                return _MachineCode;
            }
            set
            {
                _MachineCode = value;
            }
        }

        /// <summary>
        /// 试验项目
        /// </summary>
        String _SYXM;
        public String SYXM
        {
            get
            {
                return _SYXM;
            }
            set
            {
                _SYXM = value;
            }
        }
        /// <summary>
        /// 试验龄期
        /// </summary>
        String _DateSpan;
        public String DateSpan
        {
            get
            {
                return _DateSpan;
            }
            set
            {
                _DateSpan = value;
            }
        }
        /// <summary>
        /// 试件面积
        /// </summary>
        String _SJSize;
        public String SJSize
        {
            get
            {
                return _SJSize;
            }
            set
            {
                _SJSize = value;
            }
        }

        /// <summary>
        /// 强度级别
        /// </summary>
        String _SJLevel;
        public String SJLevel
        {
            get
            {
                return _SJLevel;
            }
            set
            {
                _SJLevel = value;
            }
        }

        /// <summary>
        /// 是否完成试验
        /// </summary>
        Boolean _IsDone;
        public Boolean IsDone
        {
            get
            {
                return _IsDone;
            }
            set
            {
                _IsDone = value;
            }
        }
        // 2013.7.23 gyf 新增stadiuminfo 字段 PostTrue 用于标识数据上传铁道部是否成功 begin
        String _PostTrue;
        public String PostTrue
        {
            get
            {
                return _PostTrue;
            }
            set
            {
                _PostTrue = value;
            }
        }
        // 2013.7.23 gyf 新增stadiuminfo 字段 PostTrue 用于标识数据上传铁道部是否成功 end

        string _DocID;

        /// <summary>
        /// 资料ID
        /// </summary>
        public string DocID
        {
            get { return _DocID; }
            set { _DocID = value; }
        }

        string _ModelID;

        /// <summary>
        /// 模板ID
        /// </summary>
        public string ModelID
        {
            get { return _ModelID; }
            set { _ModelID = value; }
        }

        String _Index;
        public String Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
        }


        String _Description;
        public String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        String _F_ZJRQ;
        public String F_ZJRQ
        {
            get
            {
                return _F_ZJRQ;
            }
            set
            {
                _F_ZJRQ = value;
            }
        }

        String _F_List;
        public String F_List
        {
            get
            {
                return _F_List;
            }
            set
            {
                _F_List = value;
            }
        }

        List<String> _F_Columns = new List<string>();
        public List<string> F_Columns
        {
            get
            {
                return _F_Columns;
            }
            set
            {
                _F_Columns = value;
            }
        }
    }
}
