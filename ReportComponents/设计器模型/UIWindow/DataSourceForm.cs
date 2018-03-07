using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ReportCommon;
using System.Drawing;
using Yqun.Interfaces;
using Yqun.Common.ContextCache;

namespace ReportComponents
{
    public partial class DataSourceForm : DockContent
    {
        public DataSourceForm()
        {
            InitializeComponent();
        }

        public ReportWindow ReportWindow
        {
            get
            {
                return DockPanel.Parent as ReportWindow;
            }
        }

        public Report Report
        {
            get
            {
                return ReportWindow.report;
            }
        }

        protected void UpdateMenuUI(TreeNode Node)
        {
            Button_DeleteDataTable.Enabled = false;
            Button_PreviewDataTable.Enabled = true;
            Button_DataSourceFilter.Enabled = false;
        }

        internal void EnableToolStripButton(Boolean isEnabled)
        {
            Button_AppendDataTable.Enabled = false;
            Button_EditDataTable.Enabled = false;
            Button_DeleteDataTable.Enabled = false;
            Button_PreviewDataTable.Enabled = true;
            Button_DataSourceFilter.Enabled = false;
        }

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == MenuItem_DataTable) //添加数据表
            {
                AppendDataTable();
            }
            else if (sender == MenuItem_ConstantDataTable)
            {
                AppendConstantDataTable();
            }
            else if (sender == MenuItem_CustomDataTable)
            {
                AppendCustomDataTable();
            }
            else if (sender == Button_EditDataTable) //编辑数据表
            {
                EditDataTable();
            }
            else if (sender == Button_DeleteDataTable) //删除数据表
            {
                DeleteDataTable();
            }
            else if (sender == Button_PreviewDataTable) //预览数据
            {
                PreviewDataTable();
            }
            else if (sender == Button_DataSourceFilter) //显示数据源条件编辑窗口
            {
                ShowDataSourceFilterDialog();
            }
        }

        /// <summary>
        /// 显示数据源条件编辑窗口
        /// </summary>
        private void ShowDataSourceFilterDialog()
        {
            TreeNode Node = DataSourceView.SelectedNode;
            if (Node == null)
                return;

            TableData Source = Node.Tag as TableData;
            TableDataFilterDialog Dialog = new TableDataFilterDialog(Report.Configuration.Index, Source);
            Dialog.Owner = this;
            Dialog.ShowDialog();

        }

        /// <summary>
        /// 显示所有数据源的字段
        /// </summary>
        /// <param name="Node"></param>
        internal void ShowTableFields()
        {
            TreeNode Node = new TreeNode();
            Node.Name = "周报、月报数据源";
            Node.Text = "周报、月报数据源";
            DataSourceView.Nodes.Add(Node);
            if (DataSourceView.TopNode != null)
            {
                DataSourceView.TopNode.ExpandAll();
            }
        }

        /// <summary>
        /// 移除所有数据源
        /// </summary>
        internal void RemoveAll()
        {
            DataSourceView.Nodes.Clear();
        }

        /// <summary>
        /// 添加物理数据表
        /// </summary>
        private void AppendDataTable()
        {
            DataTableDialog TableDialog = new DataTableDialog();
            if (DialogResult.OK == TableDialog.ShowDialog())
            {
                if (Report.Configuration.DataSources.IndexOf(TableDialog.TableData) == -1)
                    Report.Configuration.DataSources.Add(TableDialog.TableData);

                ShowTableFields();
            }
        }

        /// <summary>
        /// 添加内置数据表
        /// </summary>
        private void AppendConstantDataTable()
        {
            ConstantDataTableDialog ConstantDataTableDialog = new ConstantDataTableDialog();
            if (DialogResult.OK == ConstantDataTableDialog.ShowDialog())
            {
                if (Report.Configuration.DataSources.IndexOf(ConstantDataTableDialog.TableData) == -1)
                    Report.Configuration.DataSources.Add(ConstantDataTableDialog.TableData);

                ShowTableFields();
            }
        }

        /// <summary>
        /// 添加自定义数据表
        /// </summary>
        private void AppendCustomDataTable()
        {
            CustomDataTableDialog CustomDataTableDialog = new CustomDataTableDialog();
            Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            CustomDataTableDialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
            CustomDataTableDialog.Size = Owner.ClientRectangle.Size;
            if (DialogResult.OK == CustomDataTableDialog.ShowDialog())
            {
                if (Report.Configuration.DataSources.IndexOf(CustomDataTableDialog.TableData) == -1)
                    Report.Configuration.DataSources.Add(CustomDataTableDialog.TableData);

                ShowTableFields();
            }
        }

        /// <summary>
        /// 编辑数据表
        /// </summary>
        private void EditDataTable()
        {
            TreeNode Node = DataSourceView.SelectedNode;
            if (Node == null)
                return;

            TableData Source = Node.Tag as TableData;
            if (Source is DbTableData)//当TableData为DbTableData类型时
            {
                DataTableDialog TableDialog = new DataTableDialog();
                TableDialog.TableData = (Source as DbTableData);
                if (DialogResult.OK == TableDialog.ShowDialog())
                {
                    if (Report.Configuration.DataSources.IndexOf(TableDialog.TableData) == -1)
                        Report.Configuration.DataSources.Add(TableDialog.TableData);

                    ShowTableFields();
                }
            }
            else if (Source is ArrayTableData)//当TableData为ArrayTableData类型时
            {
                ConstantDataTableDialog ConstantDataTableDialog = new ConstantDataTableDialog();
                ConstantDataTableDialog.TableData = (Source as ArrayTableData);
                if (DialogResult.OK == ConstantDataTableDialog.ShowDialog())
                {
                    if (Report.Configuration.DataSources.IndexOf(ConstantDataTableDialog.TableData) == -1)
                        Report.Configuration.DataSources.Add(ConstantDataTableDialog.TableData);

                    ShowTableFields();
                }
            }
            else if (Source is JoinTableData)//当TableData为JoinTableData类型时
            {
                CustomDataTableDialog CustomDataTableDialog = new CustomDataTableDialog();
                Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
                CustomDataTableDialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
                CustomDataTableDialog.Size = Owner.ClientRectangle.Size;
                CustomDataTableDialog.TableData = (Source as JoinTableData);
                if (DialogResult.OK == CustomDataTableDialog.ShowDialog())
                {
                    if (Report.Configuration.DataSources.IndexOf(CustomDataTableDialog.TableData) == -1)
                        Report.Configuration.DataSources.Add(CustomDataTableDialog.TableData);

                    ShowTableFields();
                }
            }
        }

        /// <summary>
        /// 删除数据表
        /// </summary>
        private void DeleteDataTable()
        {
            TreeNode Node = DataSourceView.SelectedNode;
            if (Node == null)
                return;

            TableData Source = Node.Tag as TableData;
            if (Source != null)
            {
                String message = "你是否要删除数据表 ‘" + Node.Text + "’ 吗？";
                if (DialogResult.Yes == MessageBox.Show(message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
                {
                    Report.Configuration.DataSources.Remove(Node.Name);
                    Node.Remove();
                }
            }
        }

        /// <summary>
        /// 预览数据
        /// </summary>
        private void PreviewDataTable()
        {
            //TreeNode Node = DataSourceView.SelectedNode;
            //if (Node == null)
            //    return;

            //TableData Source = Node.Tag as TableData;
            //if (Source != null)
            //{
            //    PreviewDataDialog PreviewDataDialog = new PreviewDataDialog(Source);
            //    Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
            //    PreviewDataDialog.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
            //    PreviewDataDialog.Size = Owner.ClientRectangle.Size;
            //    PreviewDataDialog.ShowDialog();
            //}
            ReportConfigDataDialog rcd = new ReportConfigDataDialog();
            rcd.ShowDialog();
        }

        /// <summary>
        /// 更新工具栏按钮的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //UpdateMenuUI(e.Node);
        }
    }
}
