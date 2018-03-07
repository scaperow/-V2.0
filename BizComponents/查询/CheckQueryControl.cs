using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using BizCommon;

namespace BizComponents.查询统计
{
    public partial class CheckQueryControl : UserControl
    {
         public CheckQueryControl()
        {
            InitializeComponent();

            InitData();
        }

         protected CheckQueryControl(SerializationInfo info, StreamingContext context)
        {
            InitializeComponent();

            InitData();

            Tag = info.GetString("TableName") + "." + info.GetString("FieldName");
            SOperation.SelectedIndex = info.GetInt32("SOperation");
            
        }

        private void InitData()
        {
            SOperation.Items.Clear();
            SOperation.Items.Add("等于");
        }

        private void CheckQueryControl_Resize(object sender, EventArgs e)
        {
            FieldText.Left = 0;
            FieldText.Top = (this.Height - FieldText.Height) / 2;

            SOperation.Left = FieldText.Left + FieldText.Width;
            SOperation.Top = (this.Height - SOperation.Height) / 2;

           
            DeleteButton.Top = (this.Height - DeleteButton.Height) / 2;

            this.Width = DeleteButton.Left + DeleteButton.Width;
        }

        public String Filter
        {
            get
            {
                String Result = "";
                if (FieldInfo != null)
                {
                    if (FieldInfo.ContentFieldType == FieldType.Text.Description ||
                        FieldInfo.ContentFieldType == FieldType.HyperLink.Description ||
                        FieldInfo.ContentFieldType == FieldType.DownList.Description ||
                        FieldInfo.ContentFieldType == FieldType.CheckBox.Description)
                    {
                        
                            switch (SOperation.Text)
                            {
                                case "等于":
                                    if (checkBox1.Checked)
                                    {
                                        Result = "[" + FieldInfo.TableName.Trim('[', ']') + "].[" + FieldInfo.Contents.Trim('[', ']') + "] = 1";
                                    }
                                    if (!checkBox1.Checked)
                                    {
                                        Result = "[" + FieldInfo.TableName.Trim('[', ']') + "].[" + FieldInfo.Contents.Trim('[', ']') + "] = 0 or [" + FieldInfo.TableName.Trim('[', ']') + "].[" + FieldInfo.Contents.Trim('[', ']') + "] is null";
                                    }
                                    
                                    break;                               
                            }
                        
                    }
                }

                return Result;
            }
        }

        ModuleField _FieldInfo;
        public ModuleField FieldInfo
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
            if (FieldInfo != null)
            {
                info.AddValue("TableName", FieldInfo.TableName.Trim('[',']'));
                info.AddValue("FieldName", FieldInfo.Contents.Trim('[', ']'));
            }

            info.AddValue("SOperation", SOperation.SelectedIndex);
            if (checkBox1.Checked)
            {
                info.AddValue("checkBox1", 1);
            }
            else if (!checkBox1.Checked)
            {
                info.AddValue("checkBox1", 0);
            }
        }
    }
}
