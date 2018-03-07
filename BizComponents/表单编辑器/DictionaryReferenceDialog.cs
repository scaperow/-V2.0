using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread.Model;
using Yqun.Common.ContextCache;
using BizCommon;
using BizCommon.Properties;
using Yqun.Common.Encoder;
using Yqun.Bases.ClassBases;
using Yqun.Bases;

namespace BizComponents
{
    public partial class DictionaryReferenceDialog : Form
    {
        DownListCellType CellType = null;
        int RowIndex;
        int ColumnIndex;

        public DictionaryReferenceDialog(int RowIndex,int ColumnIndex, DownListCellType CellType)
        {
            InitializeComponent();

            this.RowIndex = RowIndex;
            this.ColumnIndex = ColumnIndex;
            this.CellType = CellType;
        }

        private void ReferenceForm_Load(object sender, EventArgs e)
        {
            ImageList imageList = new ImageList();
            imageList.Images.Add(IcoResource.Forlder);
            imageList.Images.Add(IcoResource.Col);
            DictionaryView.ImageList = imageList;

            DictionaryView.Nodes.Clear();

            TreeNode TopNode = new TreeNode();
            TopNode.Text = "字典列表";
            TopNode.SelectedImageIndex = 0;
            TopNode.ImageIndex = 0;
            TopNode.Name = "";

            DictionaryView.Nodes.Add(TopNode);

            //加载所有字典
            DataTable dt = DictionaryManager.GetAllDictionary();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    String ID = row["ID"].ToString();
                    String CodeClass = row["CodeClass"].ToString();
                    String Description = row["DESCRIPTION"].ToString();
                    String Code = row["Code"].ToString();

                    if (row["Code"] == DBNull.Value)
                    {
                        TreeNode Node = new TreeNode();
                        Node.Name = CodeClass;
                        Node.Text = Description;
                        Node.SelectedImageIndex = 0;
                        Node.ImageIndex = 0;

                        Selection selection = new Selection();
                        selection.TypeFlag = "@Dictionary";
                        selection.ID = ID;
                        selection.Code = null;
                        selection.Value = Code;
                        Node.Tag = selection;

                        DictionaryView.TopNode.Nodes.Add(Node);
                    }
                    else
                    {
                        foreach (TreeNode ChildNode in DictionaryView.TopNode.Nodes)
                        {
                            if (row["CodeClass"].ToString() == ChildNode.Name)
                            {
                                TreeNode childNode = new TreeNode();
                                childNode.Name = Code;
                                childNode.Text = Description;
                                childNode.SelectedImageIndex = 1;
                                childNode.ImageIndex = 1;

                                Selection selection = new Selection();
                                selection.TypeFlag = "@DictionaryItem";
                                selection.ID = ID;
                                selection.Code = CodeClass;
                                selection.Value = Code;
                                childNode.Tag = selection;

                                ChildNode.Nodes.Add(childNode);
                            }
                        }
                    }
                }

                DictionaryView.TopNode.Expand();

                //显示配置的字典
                DictionaryReference Reference = CellType.ReferenceInfo as DictionaryReference;
                if (Reference != null)
                {
                    TreeNode[] Nodes = DictionaryView.Nodes.Find(Reference.DictionaryIndex, true);
                    if (Nodes.Length > 0)
                    {
                        DictionaryView.SelectedNode = Nodes[0];
                    }
                }
            }
        }

        void MenuItem_Click(object sender, System.EventArgs e)
        {
            if (sender == MenuItem_NewFolder)
            {
                NewFolder();
            }
            if (sender == MenuItem_ModifyFolder)
            {
                ModifyFolder();
            }
            if (sender == MenuItem_DeleteFolder)
            {
                DeleteFolder();
            }
            if (sender == MenuItem_EditItem)
            {
                EditItem();
            }
        }

        private void EditItem()
        {
            Boolean Result = true;

            TreeNode Node = DictionaryView.SelectedNode;
            DictionaryReferenceItemEditor ItemEditor = new DictionaryReferenceItemEditor(Node);
            if (DialogResult.OK == ItemEditor.ShowDialog())
            {
                foreach (TreeNode SubNode in Node.Nodes)
                    Result = Result && DictionaryManager.DeleteDictionary(SubNode);

                Node.Nodes.Clear();
                String[] Lines = ItemEditor.TextBox_Items.Lines;
                foreach (String Line in Lines)
                {
                    if (string.IsNullOrEmpty(Line))
                        continue;

                    TreeNode SubNode = new TreeNode();
                    String Code = DictionaryManager.GetNextCode(Node.Name);
                    SubNode.Name = Code;
                    SubNode.Text = Line;
                    SubNode.SelectedImageIndex = 1;
                    SubNode.ImageIndex = 1;
                    Selection sele = new Selection();
                    sele.TypeFlag = "@DictionaryItem";
                    sele.ID = Guid.NewGuid().ToString();
                    sele.Code = Node.Name;
                    sele.Value = Code;
                    SubNode.Tag = sele;

                    Node.Nodes.Add(SubNode);
                }

                if (Node.Nodes.Count > 0)
                    Result = Result & DictionaryManager.SaveDictionary(DictionaryView.Nodes[0]);

                if (!Result)
                    MessageBox.Show("保存字典失败！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void NewFolder()
        {
            TreeNode ParentNode = DictionaryView.SelectedNode;
            TreeNode Node = new TreeNode();
            String Code = DictionaryManager.GetNextCode(ParentNode.Name);
            Node.Name = Code;
            Node.Text = "新建分类";
            Node.SelectedImageIndex = 0;
            Node.ImageIndex = 0;
            Selection sele = new Selection();
            sele.TypeFlag = "@Dictionary";
            sele.ID = Guid.NewGuid().ToString();
            sele.Code = null;
            sele.Value = Code;
            Node.Tag = sele;

            ParentNode.Nodes.Add(Node);
            Boolean Result = DictionaryManager.SaveDictionary(DictionaryView.Nodes[0]);
            if (!Result)
            {
                MessageBox.Show("字典配置保存失败");
                Node.Remove();
            }
        }

        private void DeleteFolder()
        {
            Boolean Result = false;
            TreeNode node = DictionaryView.SelectedNode;
            Result = DictionaryManager.DeleteDictionary(node);

            if (Result)
            {
                node.Remove();
                MessageBox.Show("已删除字典分类");
            }
            else
            {
                MessageBox.Show("删除失败");
            }
        }

        private void ModifyFolder()
        {
            TreeNode node = DictionaryView.SelectedNode;
            if (node != DictionaryView.Nodes[0])
            {
                node.TreeView.LabelEdit = true;
                node.BeginEdit();
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            TreeNode node = DictionaryView.SelectedNode;
            Selection sele = node.Tag as Selection;

            if (node.Text == "字典列表")
            {
                this.MenuItem_EditItem.Enabled = false;
                this.MenuItem_ModifyFolder.Enabled = false;
                this.MenuItem_DeleteFolder.Enabled = false;
                this.MenuItem_NewFolder.Enabled = true;
            }
            else if (sele.TypeFlag == "@Dictionary")
            {
                this.MenuItem_NewFolder.Enabled = false;
                this.MenuItem_DeleteFolder.Enabled = true;
                this.MenuItem_ModifyFolder.Enabled = true;
                this.MenuItem_EditItem.Enabled = true;
            }
            else if (sele.TypeFlag == "@DictionaryItem")
            {
                this.MenuItem_EditItem.Enabled = false;
                this.MenuItem_ModifyFolder.Enabled = false;
                this.MenuItem_DeleteFolder.Enabled = false;
                this.MenuItem_NewFolder.Enabled = false;
            }
        }

        private void DictionaryView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pt = DictionaryView.PointToClient(Cursor.Position);
                TreeNode Node = DictionaryView.GetNodeAt(pt);
                DictionaryView.SelectedNode = Node;
                contextMenuStrip1.Show(DictionaryView, pt);
            }
        }

        private void DictionaryView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new char[] { '@', ',', '!' }) == -1)
                    {
                        e.Node.EndEdit(false);
                        Selection sele = e.Node.Tag as Selection;
                        if (sele.TypeFlag == "@DictionaryItem")
                        {
                            Boolean result = DictionaryManager.ModifyDictionaryItem(e.Node,e.Label);
                        }
                        if (sele.TypeFlag == "@Dictionary")
                        {
                            Boolean result = DictionaryManager.ModifyDictionary(e.Node,e.Label);
                        }	
                    }
                    else
                    {
                        e.CancelEdit = true;
                        MessageBox.Show("无效的树节点文本.\n" + "无效的字符是: '@', ',', '!'", "编辑节点");
                        e.Node.BeginEdit();
                    }
                }
                else
                {
                    e.CancelEdit = true;
                    MessageBox.Show("无效的树节点文本.\n文本不能为空", "编辑节点");
                    e.Node.BeginEdit();
                }

                e.Node.TreeView.LabelEdit = false;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            Boolean Result = DictionaryManager.SaveDictionary(DictionaryView.Nodes[0]);
            String Messsage = (Result ? "字典配置保存完成" : "字典配置保存失败");
            MessageBox.Show(Messsage);
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            TreeNode Node = DictionaryView.SelectedNode;
            Selection Selection = Node.Tag as Selection;
            if (Selection.TypeFlag != "@Dictionary")
            {
                this.DialogResult = DialogResult.None;
                return;
            }

            CellType.ReferenceStyle = BizCommon.ReferenceStyle.Dictionary;
            DictionaryReference Reference = CellType.ReferenceInfo as DictionaryReference;
            String Cell = Arabic_Numerals_Convert.Excel_Word_Numerals(ColumnIndex) + (RowIndex + 1).ToString();
            //bool HaveItem = SheetConfiguration.DataTableSchema.HaveDataItem(Cell);
            //if (HaveItem)
            //{
            //    FieldDefineInfo FieldInfo = SheetConfiguration.DataTableSchema.GetDataItem(Cell);
            //    if (FieldInfo != null)
            //    {
            //        Reference.TableName = FieldInfo.TableInfo.Name;
            //        Reference.ColumnName = FieldInfo.FieldName;
            //    }
            //}

            Reference.DictionaryIndex = Node.Name;
            Reference.ReferenceXml = Node.Name;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
