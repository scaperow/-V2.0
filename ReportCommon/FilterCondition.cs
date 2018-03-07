using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ReportCommon
{
    /// <summary>
    /// 公式过滤条件
    /// </summary>
    [Serializable]
    public class FilterCondition : ISerializable
    {
        public FilterCondition()
        {
        }

        protected FilterCondition(SerializationInfo info, StreamingContext context)
        {
            _LeftItem = (Item)info.GetValue("LeftItem", typeof(Item));
            _RightItem = (Item)info.GetValue("RightItem", typeof(Item));
            _compareOperation = (CompareOperation)info.GetValue("CompareOperation", typeof(CompareOperation));

            _IsFormula = (Boolean)info.GetBoolean("IsFormula");
            _Operation = (BooleanOperation)info.GetValue("Operation", typeof(BooleanOperation));
        }

        BooleanOperation _Operation = BooleanOperation.And;
        public BooleanOperation Operation
        {
            get
            {
                return _Operation;
            }
            set
            {
                _Operation = value;
            }
        }

        Boolean _IsFormula = false;
        public Boolean IsFormula
        {
            get
            {
                return _IsFormula;
            }
            set
            {
                _IsFormula = value;
            }
        }

        Item _LeftItem = new Item();
        public Item LeftItem
        {
            get
            {
                return _LeftItem;
            }
            set
            {
                _LeftItem = value;
            }
        }

        CompareOperation _compareOperation = CompareOperation.等于;
        public CompareOperation CompareOperation
        {
            get
            {
                return _compareOperation;
            }
            set
            {
                _compareOperation = value;
            }
        }

        Item _RightItem = new Item();
        public Item RightItem
        {
            get
            {
                return _RightItem;
            }
            set
            {
                _RightItem = value;
            }
        }

        String _Formula = "";
        public String Formula
        {
            get
            {
                return _Formula;
            }
            set
            {
                _Formula = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Operation.ToString());
            sb.Append(" ");
            if (!IsFormula)
            {
                if (LeftItem.TableName != "")
                {
                    sb.Append(LeftItem.TableName);
                    sb.Append(".");
                }
                sb.Append(LeftItem.FieldName);
                sb.Append(" ");
                sb.Append(CompareOperation.Value);
                sb.Append(" ");

                if (RightItem.Style == FilterStyle.DataColumn)
                {
                    if (RightItem.TableName != "")
                    {
                        sb.Append(RightItem.TableName);
                        sb.Append(".");
                    }
                    sb.Append(RightItem.FieldName);
                }
                else if (RightItem.Style == FilterStyle.Value)
                {
                    sb.Append(RightItem.IsNull ? "null" : RightItem.Value);
                }
                else if (RightItem.Style == FilterStyle.Parameter)
                {
                    sb.Append(RightItem.ParameterName);
                }
            }
            else
            {
                sb.Append(" ");
                sb.Append(Formula);
            }

            return sb.ToString();
        }

        #region ISerializable 成员

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("LeftItem", _LeftItem);
            info.AddValue("RightItem", _RightItem);
            info.AddValue("CompareOperation", _compareOperation);

            info.AddValue("IsFormula", _IsFormula);
            info.AddValue("Operation", Operation);
        }

        #endregion
    }
}
