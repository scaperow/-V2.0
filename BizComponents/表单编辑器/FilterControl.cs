using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BizCommon;

namespace BizComponents
{
    public partial class FilterControl : UserControl
    {
        public FilterControl()
        {
            InitializeComponent();
        }

        public void InitializeControls()
        {
            //加载比较运算符
            ComboBoxOperations.Items.Add(CompareOperation.等于);
            ComboBoxOperations.Items.Add(CompareOperation.不等于);
            ComboBoxOperations.Items.Add(CompareOperation.大于);
            ComboBoxOperations.Items.Add(CompareOperation.大于或等于);
            ComboBoxOperations.Items.Add(CompareOperation.小于);
            ComboBoxOperations.Items.Add(CompareOperation.小于或等于);
            ComboBoxOperations.Items.Add(CompareOperation.包含);
            ComboBoxOperations.Items.Add(CompareOperation.不包含);

            ComboBoxOperations.SelectedIndex = 0;

            cBoxValueStyle.Items.Add("值");
            cBoxValueStyle.Items.Add("参数");
            cBoxValueStyle.SelectedIndex = 0;

            ComboBoxParameters.Items.Add(new FirstDayInCurrentMonth());
            ComboBoxParameters.Items.Add(new LastDayInCurrentMonth());
        }

        private void cBoxValueStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBoxValueStyle.SelectedIndex == 0)
                TextBox_Value.BringToFront();
            else if (cBoxValueStyle.SelectedIndex == 1)
                ComboBoxParameters.BringToFront();
        }
    }
}
