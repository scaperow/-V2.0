namespace BizComponents.查询
{
    partial class OrganizationChoose
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("节点1");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("节点0", new System.Windows.Forms.TreeNode[] {
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("节点2");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("节点4");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("节点5");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("节点6");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("节点3", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12,
            treeNode13});
            this.Trees = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // Trees
            // 
            this.Trees.CheckBoxes = true;
            this.Trees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Trees.FullRowSelect = true;
            this.Trees.ItemHeight = 20;
            this.Trees.Location = new System.Drawing.Point(0, 0);
            this.Trees.Name = "Trees";
            treeNode8.Name = "节点1";
            treeNode8.Text = "节点1";
            treeNode9.Name = "节点0";
            treeNode9.Text = "节点0";
            treeNode10.Name = "节点2";
            treeNode10.Text = "节点2";
            treeNode11.Name = "节点4";
            treeNode11.Text = "节点4";
            treeNode12.Name = "节点5";
            treeNode12.Text = "节点5";
            treeNode13.Name = "节点6";
            treeNode13.Text = "节点6";
            treeNode14.Name = "节点3";
            treeNode14.Text = "节点3";
            this.Trees.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode14});
            this.Trees.ShowLines = false;
            this.Trees.Size = new System.Drawing.Size(207, 335);
            this.Trees.TabIndex = 0;
            this.Trees.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Trees_AfterSelect);
            // 
            // OrganizationChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Trees);
            this.Name = "OrganizationChoose";
            this.Size = new System.Drawing.Size(207, 335);
            this.Load += new System.EventHandler(this.OrganizationChoose_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView Trees;
    }
}
