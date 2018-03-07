using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace BizComponents
{
    public partial class ExpressionControl : UserControl
    {
        ComparisonOperatorConvertor Convertor = new ComparisonOperatorConvertor();

        public ExpressionControl()
        {
            InitializeComponent();
        }

        private void ExpressionControl_Load(object sender, EventArgs e)
        {
            cBox_ComparisonOperator.Items.Add("介于");
            cBox_ComparisonOperator.Items.Add("未介于");
            cBox_ComparisonOperator.Items.Add("等于");
            cBox_ComparisonOperator.Items.Add("不等于");
            cBox_ComparisonOperator.Items.Add("大于");
            cBox_ComparisonOperator.Items.Add("小于");
            cBox_ComparisonOperator.Items.Add("大于或等于");
            cBox_ComparisonOperator.Items.Add("小于或等于");

            cBox_ComparisonOperator.SelectedIndex = 0;
        }

        private void cBox_ComparisonOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_ComparisonOperator.SelectedItem != null && (cBox_ComparisonOperator.SelectedItem.ToString() == "介于" || cBox_ComparisonOperator.SelectedItem.ToString() == "未介于"))
            {
                Panel1.BringToFront();
                TextBox3.Text = "";
            }
            else
            {
                Panel2.BringToFront();
                TextBox1.Text = "";
                TextBox2.Text = "";
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ComparisonOperator ComparisonOperator
        {
            get
            {
                return Convertor.ConvertToComparisonOperator(cBox_ComparisonOperator.SelectedItem.ToString());
            }
            set
            {
                String Text = Convertor.ConvertToText(value);
                cBox_ComparisonOperator.SelectedIndex = cBox_ComparisonOperator.Items.IndexOf(Text);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public String StartValue
        {
            get
            {
                if (cBox_ComparisonOperator.SelectedItem.ToString() == "介于" ||
                cBox_ComparisonOperator.SelectedItem.ToString() == "未介于")
                {
                    return TextBox1.Text;
                }
                else
                {
                    return TextBox3.Text;
                }
            }
            set
            {
                if (cBox_ComparisonOperator.SelectedItem.ToString() == "介于" ||
                cBox_ComparisonOperator.SelectedItem.ToString() == "未介于")
                {
                    TextBox1.Text = value;
                }
                else
                {
                    TextBox3.Text = value;
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public String EndValue
        {
            get
            {
                if (cBox_ComparisonOperator.SelectedItem.ToString() == "介于" ||
                cBox_ComparisonOperator.SelectedItem.ToString() == "未介于")
                {
                    return TextBox2.Text;
                }
                else
                {
                    return TextBox3.Text;
                }
            }
            set
            {
                if (cBox_ComparisonOperator.SelectedItem.ToString() == "介于" ||
                cBox_ComparisonOperator.SelectedItem.ToString() == "未介于")
                {
                    TextBox2.Text = value;
                }
                else
                {
                    TextBox3.Text = value;
                }
            }
        }
    }

    public class ComparisonOperatorConvertor
    {
        public ComparisonOperator ConvertToComparisonOperator(String Text)
        {
            ComparisonOperator Result = ComparisonOperator.IsEmpty;
            switch (Text)
            {
                case "介于":
                    Result = ComparisonOperator.Between;
                    break;
                case "未介于":
                    Result = ComparisonOperator.NotBetween;
                    break;
                case "等于":
                    Result = ComparisonOperator.EqualTo;
                    break;
                case "不等于":
                    Result = ComparisonOperator.NotEqualTo;
                    break;
                case "大于":
                    Result = ComparisonOperator.GreaterThan;
                    break;
                case "小于":
                    Result = ComparisonOperator.LessThan;
                    break;
                case "大于或等于":
                    Result = ComparisonOperator.GreaterThanOrEqualTo;
                    break;
                case "小于或等于":
                    Result = ComparisonOperator.LessThanOrEqualTo;
                    break;
            }

            return Result;
        }

        public String ConvertToText(ComparisonOperator ComparisonOperator)
        {
            String Result = "";
            switch (ComparisonOperator)
            {
                case ComparisonOperator.Between:
                    Result = "介于";
                    break;
                case ComparisonOperator.NotBetween:
                    Result = "未介于";
                    break;
                case ComparisonOperator.EqualTo:
                    Result = "等于";
                    break;
                case ComparisonOperator.NotEqualTo:
                    Result = "不等于";
                    break;
                case ComparisonOperator.GreaterThan:
                    Result = "大于";
                    break;
                case ComparisonOperator.LessThan:
                    Result = "小于";
                    break;
                case ComparisonOperator.GreaterThanOrEqualTo:
                    Result = "大于或等于";
                    break;
                case ComparisonOperator.LessThanOrEqualTo:
                    Result = "小于或等于";
                    break;
            }

            return Result;
        }
    }
}
