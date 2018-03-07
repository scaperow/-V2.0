using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizCommon;

namespace BizComponents
{
    public partial class ReadDataFunctionWizard : WizardDialog
    {
        private ModuleConfiguration Module;
        private TreeView FunctionList;

        public ReadDataFunctionWizard(ModuleConfiguration Module, TreeView FunctionList)
        {
            this.Module = Module;
            this.FunctionList = FunctionList;

            InitializeComponent();
        }

        /// <summary>
        ///加载模板中所有表单的数据项值
        /// </summary>
        /// <param name="Schemas"></param>
        public void InitDataItems()
        {
        }

        /// <summary>
        /// 初始化读数函数
        /// </summary>
        /// <param name="FunctionInfo"></param>
        public void InitFunctionInfo(ReadDataFunctionInfo FunctionInfo)
        {

        }
    }
}
