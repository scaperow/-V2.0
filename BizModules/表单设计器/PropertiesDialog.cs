using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BizModules
{
    public partial class PropertiesDialog : Form
    {
        public PropertiesDialog()
        {
            InitializeComponent();
        }

        public void SelectObject(Object Object)
        {
            propertyGrid1.SelectedObject = Object;
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }
    }
}
