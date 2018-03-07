using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BizCommon
{
    [Serializable]
    public class WriteDataFunctionInfo
    {
        string _Index = Guid.NewGuid().ToString();
        public string Index
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

        string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        private string _WriteInTableText;
        public string WriteInTableText
        {
            get
            {
                return _WriteInTableText;
            }
            set
            {
                _WriteInTableText = value;
            }
        }

        /// <summary>
        /// 将数据写入到指定表单里面
        /// </summary>
        private string _WriteInTableIndex;
        public string WriteInTableIndex
        {
            get
            {
                return _WriteInTableIndex;
            }
            set
            {
                _WriteInTableIndex = value;
            }
        }

        /// <summary>
        /// 从模板里面读出数据
        /// </summary>
        private string _ReadOutTableIndex;
        public string ReadOutTableIndex
        {
            get
            {
                return _ReadOutTableIndex;
            }
            set
            {
                _ReadOutTableIndex = value;
            }
        }

        public string ConditionIDs
        {
            get
            {
                List<string> IDs = new List<string>();
                foreach (ExpressionInfo Info in Conditions)
                {
                    IDs.Add(Info.Index);
                }

                return string.Join(",", IDs.ToArray());
            }
        }

        public string ModificationIDs
        {
            get
            {
                List<string> IDs = new List<string>();
                foreach (ExpressionInfo Info in Modifications)
                {
                    IDs.Add(Info.Index);
                }

                return string.Join(",", IDs.ToArray());
            }
        }

        private List<ExpressionInfo> _Conditions = new List<ExpressionInfo>();
        public List<ExpressionInfo> Conditions
        {
            get
            {
                return _Conditions;
            }
        }

        private List<ExpressionInfo> _Modifications = new List<ExpressionInfo>();
        public List<ExpressionInfo> Modifications
        {
            get
            {
                return _Modifications;
            }
        }

        public string ReportInfo()
        {
            StringBuilder Infomation = new StringBuilder();
            Infomation.Append("函数名称： " + Name + "\r\n\r\n");
            Infomation.Append("写入数据的表单名称： " + WriteInTableText + "\r\n\r\n");
            Infomation.Append("筛选数据的条件：\r\n");

            foreach (ExpressionInfo Info in Conditions)
            {
                Infomation.Append("条件" + Conditions.IndexOf(Info) + "：");
                Infomation.Append(Info.DataItem.Text);
                Infomation.Append(Info.Operation);
                Infomation.Append(Info.DataValue.Text);
                Infomation.Append("\r\n");
            }

            Infomation.Append("\r\n");
            Infomation.Append("修改的数据项：\r\n");

            foreach (ExpressionInfo Info in Modifications)
            {
                Infomation.Append("修改" + Modifications.IndexOf(Info) + "：");
                Infomation.Append(Info.DataItem.Text);
                Infomation.Append(Info.Operation);
                Infomation.Append(Info.DataValue.Text);
                Infomation.Append("\r\n");
            }

            return Infomation.ToString();
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
