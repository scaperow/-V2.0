using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BizCommon;

namespace BizComponents
{
    public partial class TestProjectDialog : Form
    {
        public TestItemInfo SelectedItem { get; set; }

        public TestProjectDialog()
        {
            InitializeComponent();
        }

        private void TestProjectDialog_Load(object sender, EventArgs e)
        {
            listBox_TestItems.Items.Clear();
            List<TestItemInfo> Items = DepositoryTestItemInfo.GetTestItemInfos();
            if (Items != null && Items.Count > 0)
                foreach (TestItemInfo Info in Items)
                {
                    listBox_TestItems.Items.Add(Info);
                }
        }

        private void listBox_TestItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_TestItems.SelectedItem is TestItemInfo)
            {
                SelectedItem = listBox_TestItems.SelectedItem as TestItemInfo;

            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
