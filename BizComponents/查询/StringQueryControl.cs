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
    public partial class StringQueryControl : UserControl, ISerializable
    {
        public StringQueryControl()
        {
            InitializeComponent();
        }

        protected StringQueryControl(SerializationInfo info, StreamingContext context)
        {
            InitializeComponent();

            Tag = info.GetString("TableName") + "." + info.GetString("FieldName");
            STextBox.Text = info.GetString("STextBox");
        }

        public String Filter
        {
            get
            {
                String Result = "";
                if (FieldInfo != null)
                {
                    if (STextBox.Enabled)
                    {
                        Result = string.Concat( FieldInfo.DocColumn, " like '%", STextBox.Text, "%'");
                    }
                    else
                    {
                        Result = string.Concat(FieldInfo.DocColumn, " is null ");
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
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            STextBox.Enabled = !checkBox1.Checked;
        }
    }
}
