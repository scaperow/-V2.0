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
    public partial class ReadFuncControl : UserControl
    {
        ReadDataFunctionWizard FunctionDialog;
        ModuleConfiguration module;

        public ReadFuncControl(ModuleConfiguration Module)
        {
            InitializeComponent();

            this.module = Module;
            FunctionDialog = new ReadDataFunctionWizard(Module, ReadFunctionList);
        }

        public void LoadReadFunc()
        {
            ReadFunctionList.Nodes.Clear();
            if (module != null)
            {
                List<ReadDataFunctionInfo> FunctionInfos = DepositoryReadFunction.InitByModelIndex(module.Index);
                foreach (ReadDataFunctionInfo Info in FunctionInfos)
                {
                    TreeNode Node = new TreeNode();
                    Node.Name = Info.Index;
                    Node.Text = Info.Name;
                    Node.Tag = Info;
                    Node.SelectedImageIndex = 1;
                    Node.ImageIndex = 1;
                    ReadFunctionList.Nodes.Add(Node);
                }
            }

            ReadFunctionList.ExpandAll();

            if (ReadFunctionList.Nodes.Count > 0)
                ReadFunctionList.SelectedNode = ReadFunctionList.TopNode;
        }

        private void Button_AppendReadFun_Click(object sender, EventArgs e)
        {
            if (ReadFunctionList.SelectedNode == null)
                return;

            ReadDataFunctionInfo functionInfo = null;
            if (ReadFunctionList.SelectedNode != null)
            {
                functionInfo = ReadFunctionList.SelectedNode.Tag as ReadDataFunctionInfo;
            }
            else
            {
                functionInfo = new ReadDataFunctionInfo();
            }

            functionInfo.Index = Guid.NewGuid().ToString();
            functionInfo.Name = "请输入函数名称";
            FunctionDialog.InitFunctionInfo(functionInfo);
            FunctionDialog.ShowDialog(this);
        }

        private void Button_DeleteReadFun_Click(object sender, EventArgs e)
        {
            if (ReadFunctionList.SelectedNode == null)
                return;

            ReadDataFunctionInfo Item = ReadFunctionList.SelectedNode.Tag as ReadDataFunctionInfo;
            string message = "你确定要删除读数函数‘" + Item.Name + "’吗？";
            if (DialogResult.Yes == MessageBox.Show(message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                Boolean Result = DepositoryReadFunction.Delete(Item);
                if (Result)
                {
                    ReadFunctionList.SelectedNode.Remove();
                }
            }
        }

        private void ReadFunctionList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is ReadDataFunctionInfo)
            {
                ReadDataFunctionInfo functionInfo = e.Node.Tag as ReadDataFunctionInfo;
                FunctionDialog.InitDataItems();
                FunctionDialog.InitFunctionInfo(functionInfo);
                FunctionDialog.ShowDialog(this);
            }
        }
    }
}
