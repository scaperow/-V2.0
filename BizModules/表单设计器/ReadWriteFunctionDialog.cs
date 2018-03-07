using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BizComponents;
using BizCommon;

namespace BizModules
{
    public partial class ReadWriteFunctionDialog : Form
    {
        ReadFuncControl ReadFuncControl;
        WriteFuncControl WriteFuncControl;

        public ReadWriteFunctionDialog(ModuleConfiguration Module)
        {
            InitializeComponent();

            WriteFuncControl = new WriteFuncControl(Module);
            WriteFuncControl.Dock = DockStyle.Fill;
            WriteFuncPage.Controls.Add(WriteFuncControl);

            ReadFuncControl = new ReadFuncControl(Module);
            ReadFuncControl.Dock = DockStyle.Fill;
            ReadFuncPage.Controls.Add(ReadFuncControl);
        }

        private void ReadWriteFunctionDialog_Load(object sender, EventArgs e)
        {
            WriteFuncControl.LoadWriteFunc();
            ReadFuncControl.LoadReadFunc();
        }

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
