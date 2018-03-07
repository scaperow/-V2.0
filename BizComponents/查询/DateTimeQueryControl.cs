using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using BizCommon;

namespace BizComponents
{
    [Serializable]
    public partial class DateTimeQueryControl : UserControl, ISerializable
    {
        public DateTimeQueryControl()
        {
            InitializeComponent();
        }

        protected DateTimeQueryControl(SerializationInfo info, StreamingContext context)
        {
            InitializeComponent();

            Tag = info.GetString("TableName") + "." + info.GetString("FieldName");
        }

        public String Filter
        {
            get
            {
                String Result = "";
                if (FieldInfo != null)
                {
                    String start = string.Format("{0}-{1}-{2} 00:00:00", startdateTime.Value.Year, startdateTime.Value.Month, startdateTime.Value.Day);
                    String end = string.Format("{0}-{1}-{2} 23:59:59", enddateTime.Value.Year, enddateTime.Value.Month, enddateTime.Value.Day);
                    Result = string.Concat(FieldInfo.DocColumn, " >='", start, "' and ", FieldInfo.DocColumn, " <='", end, "'");
                }

                return Result;
            }
        }

        JZCustomView _FieldInfo;
        public JZCustomView FieldInfo
        {
            get
            {
                return _FieldInfo;
            }
            set
            {
                _FieldInfo = value;
                if (value != null)
                    FieldText.Text = value.Description + "：";
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //if (FieldInfo != null)
            //{
            //    info.AddValue("TableName", FieldInfo.TableName.Trim('[', ']'));
            //    info.AddValue("FieldName", FieldInfo.Contents.Trim('[', ']'));
            //}
        }
    }
}
