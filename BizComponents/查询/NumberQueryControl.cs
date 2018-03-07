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
    public partial class NumberQueryControl : UserControl, ISerializable
    {
        public NumberQueryControl()
        {
            InitializeComponent();
        }

        protected NumberQueryControl(SerializationInfo info, StreamingContext context)
        {
            InitializeComponent();

            Tag = info.GetString("TableName") + "." + info.GetString("FieldName");
            STextBox.Text = info.GetString("STextBox");
            ETextBox.Text = info.GetString("ETextBox");
        }

        public String Filter
        {
            get
            {
                double StartNumber = 0, EndNumber = 0;
                String Result = "";
                if (FieldInfo != null)
                {
                    if (double.TryParse(STextBox.Text, out StartNumber) && !double.TryParse(ETextBox.Text, out EndNumber))
                    {
                        Result = FieldInfo.DocColumn + " >=" + StartNumber;

                    }

                    if (double.TryParse(ETextBox.Text, out EndNumber) &&  !double.TryParse(STextBox.Text, out StartNumber))
                    {
                        Result =  FieldInfo.DocColumn + " <=" + EndNumber;
                    }

                    if (double.TryParse(STextBox.Text, out StartNumber) &&  double.TryParse(ETextBox.Text, out EndNumber))
                    {
                        Result =  FieldInfo.DocColumn + " >=" + StartNumber + " and " + FieldInfo.DocColumn + " <=" + EndNumber;
                    }
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

            //info.AddValue("STextBox", STextBox.Text);
            //info.AddValue("ETextBox", ETextBox.Text);
        }
    }
}
