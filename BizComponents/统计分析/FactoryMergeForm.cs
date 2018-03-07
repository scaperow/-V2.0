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
    public partial class FactoryMergeForm : Form
    {
        public FactoryMergeForm()
        {
            InitializeComponent();
        }

        private void Detail_Click(object sender, EventArgs e)
        {

        }

        private void FactoryManagement_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            TreeAll.Nodes.Clear();
            TreeMerge.Nodes.Clear();

            var factories = StatisticsHelperClient.GetFactories("FactoryID, FactoryName", "FactoryName ASC");
            if (factories == null || factories.Rows.Count == 0)
            {
                TreeAll.Nodes.Add("没有数据");
                return;
            }

            foreach (DataRow row in factories.Rows)
            {
                TreeAll.Nodes.Add(Convert.ToString(row["FactoryID"]), Convert.ToString(row["FactoryName"]));
            }
        }

        private void ButtonRight_Click(object sender, EventArgs e)
        {
            var node = TreeAll.SelectedNode;
            TreeAll.Nodes.Remove(node);
            TreeMerge.Nodes.Insert(0, node);
        }

        private void ButtonLeft_Click(object sender, EventArgs e)
        {
            var node = TreeMerge.SelectedNode;
            TreeAll.Nodes.Remove(node);
            TreeAll.Nodes.Insert(0, node);
        }

        private void TextSearchAll_TextChanged(object sender, EventArgs e)
        {

        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (TreeMerge.Nodes.Count == 0)
            {
                MessageBox.Show("请从所有厂家选择要合并的厂家后再执行此操作", "提示", MessageBoxButtons.OK);
                return;
            }

            var node = TreeMerge.Nodes[0];
            var factory = StatisticsHelperClient.GetFactory("*", new Guid(node.Name));
            if (factory == null)
            {
                MessageBox.Show("获取厂家信息时失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var ids = new List<string>();
            foreach (TreeNode n in TreeMerge.Nodes)
            {
                ids.Add(n.Name);
            }

            var editor = new FactoryEditor(factory, true);
            editor.Saving += new EventHandler(delegate(object o, EventArgs se)
            {
                var result = StatisticsHelperClient.MergeFactory(ids.ToArray(), editor.Factory);
                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("合并成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TreeAll.Nodes.Insert(0, editor.Factory.FactoryID.ToString(), editor.Factory.FactoryName);
                    TreeMerge.Nodes.Clear();
                }
                else
                {
                    MessageBox.Show(result, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            });

            editor.Show();
        }

        private void TreeAll_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void TreeAll_DoubleClick(object sender, EventArgs e)
        {
            ButtonRight_Click(sender, e);
        }

        private void TreeMerge_DoubleClick(object sender, EventArgs e)
        {
            ButtonLeft_Click(sender, e);
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
