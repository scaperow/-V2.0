﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ReportComponents
{
    public partial class ReportModuleSelect : Form
    {
        public ReportModuleSelect()
        {
            InitializeComponent();
        }

        public string NoteContent
        {
            get
            {
                return NoteBox.Text;
            }
            set
            {
                NoteBox.Text = value;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
