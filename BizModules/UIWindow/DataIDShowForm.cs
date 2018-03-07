using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BizModules.UIWindow
{
    public partial class DataIDShowForm : Form
    {

        private string _DataID;

        public string DataID
        {
            get { return _DataID; }
            set
            {
                _DataID = value;
                txtDataID.Text = _DataID;
            }
        }

        public DataIDShowForm(string text)
        {
            InitializeComponent();
            DataID = text;
        }

        private void btnCopyAndClose_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(txtDataID.Text);
            this.Close();
        }
    }
}
