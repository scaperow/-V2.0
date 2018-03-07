using System;
using System.Windows.Forms;
using ReportCommon;

namespace ReportComponents
{
    public partial class SimpleFilterControl : UserControl
    {
        public SimpleFilterControl()
        {
            InitializeComponent();
        }

        private void SimpleFilterControl_Load(object sender, EventArgs e)
        {
            //加载比较运算符
            cBoxOperation.Items.Clear();
            cBoxOperation.Items.Add(CompareOperation.等于);
            cBoxOperation.Items.Add(CompareOperation.不等于);
            cBoxOperation.Items.Add(CompareOperation.大于);
            cBoxOperation.Items.Add(CompareOperation.大于或等于);
            cBoxOperation.Items.Add(CompareOperation.小于);
            cBoxOperation.Items.Add(CompareOperation.小于或等于);
            cBoxOperation.Items.Add(CompareOperation.包含);
            cBoxOperation.Items.Add(CompareOperation.不包含);
            cBoxOperation.Items.Add(CompareOperation.属于);
            cBoxOperation.Items.Add(CompareOperation.不属于);
            cBoxOperation.SelectedIndex = 0;

            cBoxValueStyle.SelectedIndex = 0;
        }

        private void cBoxValueStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBoxValueStyle.SelectedIndex == 0)
                panel2.BringToFront();
            else
                panel3.BringToFront();
        }
    }
}
