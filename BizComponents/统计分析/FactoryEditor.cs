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
    public partial class FactoryEditor : Form
    {
        public bool SaveOnInvoker = false;
        public event EventHandler Saving;
        public Sys_Factory Factory = null;

        public FactoryEditor()
        {
            InitializeComponent();
        }

        public FactoryEditor(Sys_Factory factory, bool saveOnInvoker)
        {
            SaveOnInvoker = saveOnInvoker;
            Factory = factory;
            InitializeComponent();
        }

        private void FactoryEditor_Load(object sender, EventArgs e)
        {
            Text = "厂家编辑器";
            if (Factory != null)
            {
                TextFactoryName.Text = Factory.FactoryName;
                TextLinkMan.Text = Factory.LinkMan;
                TextAddress.Text = Factory.Address;
                TextTel.Text = Factory.Telephone;
                TextLongitude.Text = Factory.Longitude.ToString();
                TextLatitude.Text = Factory.Latitude.ToString();
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (SaveOnInvoker)
            {
                if (Saving != null)
                {
                    Saving(sender, e);
                }
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
