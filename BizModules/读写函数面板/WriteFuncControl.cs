using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BizComponents;
using BizCommon;

namespace BizModules
{
    public partial class WriteFuncControl : UserControl
    {
        WriteDataFunctionWizard FunctionDialog;
        ModuleConfiguration module;

        public WriteFuncControl(ModuleConfiguration Module)
        {
            InitializeComponent();
            
            module = Module;
            FunctionDialog = new WriteDataFunctionWizard(module, WriteFunctionList);
        }

        public void LoadWriteFunc()
        {
            WriteFunctionList.Nodes.Clear();
            if (module != null)
            {
                List<WriteDataFunctionInfo> FunctionInfos = DepositoryWriteFunction.InitByModelIndex(module.Index);
                foreach (WriteDataFunctionInfo Info in FunctionInfos)
                {
                    TreeNode Node = new TreeNode();
                    Node.Name = Info.Index;
                    Node.Text = Info.Name;
                    Node.Tag = Info;
                    Node.SelectedImageIndex = 1;
                    Node.ImageIndex = 1;
                    WriteFunctionList.Nodes.Add(Node);
                }
            }

            WriteFunctionList.ExpandAll();

            if (WriteFunctionList.Nodes.Count > 0)
                WriteFunctionList.SelectedNode = WriteFunctionList.TopNode;
        }

        private void Button_AppendWriteFun_Click(object sender, EventArgs e)
        {
            if (WriteFunctionList.SelectedNode == null)
                return;

            WriteDataFunctionInfo functionInfo = null;
            if (WriteFunctionList.SelectedNode != null)
            {
                functionInfo = WriteFunctionList.SelectedNode.Tag as WriteDataFunctionInfo;
            }
            else
            {
                functionInfo = new WriteDataFunctionInfo();
            }

            functionInfo.Index = Guid.NewGuid().ToString();
            functionInfo.Name = "请输入函数名称";
            FunctionDialog.InitFunctionInfo(functionInfo);
            FunctionDialog.ShowDialog(this);
        }

        private void Button_DeleteWriteFun_Click(object sender, EventArgs e)
        {
            if (WriteFunctionList.SelectedNode == null)
                return;

            WriteDataFunctionInfo Item = WriteFunctionList.SelectedNode.Tag as WriteDataFunctionInfo;
            string message = "你确定要删除写数函数‘" + Item.Name + "’吗？";
            if (DialogResult.Yes == MessageBox.Show(message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                Boolean Result = DepositoryWriteFunction.Delete(Item);
                if (Result)
                {
                    WriteFunctionList.SelectedNode.Remove();
                }
            }
        }

        private void WriteFunctionList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is WriteDataFunctionInfo)
            {
                WriteDataFunctionInfo functionInfo = e.Node.Tag as WriteDataFunctionInfo;
                FunctionDialog.InitDataItems();
                FunctionDialog.InitFunctionInfo(functionInfo);
                FunctionDialog.ShowDialog(this);
            }
        }
    }
}
