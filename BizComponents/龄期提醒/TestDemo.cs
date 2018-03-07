using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BizComponents
{
    public partial class TestDemo : Form
    {
        public TestDemo()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_sent_Click(object sender, EventArgs e)
        {
            try
            {
                string phones, context;
                phones = txtPhones.Text.Trim();
                context = txtContent.Text.Trim();
                if (!string.IsNullOrEmpty(phones) && !string.IsNullOrEmpty(context))
                {
                    DataMoveHelperClient dmhc = new DataMoveHelperClient();
                    string strResult = dmhc.TestSMSSend(phones, context);
                    MessageBox.Show(strResult);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error:" + ex.ToString());
            }
        }

        private void TestDemo_Load(object sender, EventArgs e)
        {
        }
    }
}
