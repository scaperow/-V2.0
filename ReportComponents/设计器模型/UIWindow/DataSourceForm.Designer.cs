namespace ReportComponents
{
    partial class DataSourceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Button_AppendDataTable = new System.Windows.Forms.ToolStripDropDownButton();
            this.MenuItem_DataTable = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_ConstantDataTable = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_CustomDataTable = new System.Windows.Forms.ToolStripMenuItem();
            this.Button_EditDataTable = new System.Windows.Forms.ToolStripButton();
            this.Button_DeleteDataTable = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Button_PreviewDataTable = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Button_DataSourceFilter = new System.Windows.Forms.ToolStripButton();
            this.DataSourceView = new System.Windows.Forms.TreeView();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Button_AppendDataTable,
            this.Button_EditDataTable,
            this.Button_DeleteDataTable,
            this.toolStripSeparator1,
            this.Button_PreviewDataTable,
            this.toolStripSeparator2,
            this.Button_DataSourceFilter});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(303, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Button_AppendDataTable
            // 
            this.Button_AppendDataTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_AppendDataTable.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_DataTable,
            this.MenuItem_ConstantDataTable,
            this.MenuItem_CustomDataTable});
            this.Button_AppendDataTable.Enabled = false;
            this.Button_AppendDataTable.Image = global::ReportComponents.Properties.Resources.新建;
            this.Button_AppendDataTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_AppendDataTable.Name = "Button_AppendDataTable";
            this.Button_AppendDataTable.Size = new System.Drawing.Size(29, 22);
            this.Button_AppendDataTable.Text = "toolStripDropDownButton1";
            // 
            // MenuItem_DataTable
            // 
            this.MenuItem_DataTable.Name = "MenuItem_DataTable";
            this.MenuItem_DataTable.Size = new System.Drawing.Size(152, 22);
            this.MenuItem_DataTable.Text = "数据库表";
            this.MenuItem_DataTable.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // MenuItem_ConstantDataTable
            // 
            this.MenuItem_ConstantDataTable.Name = "MenuItem_ConstantDataTable";
            this.MenuItem_ConstantDataTable.Size = new System.Drawing.Size(152, 22);
            this.MenuItem_ConstantDataTable.Text = "内置数据表";
            this.MenuItem_ConstantDataTable.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // MenuItem_CustomDataTable
            // 
            this.MenuItem_CustomDataTable.Name = "MenuItem_CustomDataTable";
            this.MenuItem_CustomDataTable.Size = new System.Drawing.Size(152, 22);
            this.MenuItem_CustomDataTable.Text = "定制数据表";
            this.MenuItem_CustomDataTable.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // Button_EditDataTable
            // 
            this.Button_EditDataTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_EditDataTable.Enabled = false;
            this.Button_EditDataTable.Image = global::ReportComponents.Properties.Resources.修改;
            this.Button_EditDataTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_EditDataTable.Name = "Button_EditDataTable";
            this.Button_EditDataTable.Size = new System.Drawing.Size(23, 22);
            this.Button_EditDataTable.Text = "编辑";
            this.Button_EditDataTable.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // Button_DeleteDataTable
            // 
            this.Button_DeleteDataTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_DeleteDataTable.Enabled = false;
            this.Button_DeleteDataTable.Image = global::ReportComponents.Properties.Resources.删除;
            this.Button_DeleteDataTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_DeleteDataTable.Name = "Button_DeleteDataTable";
            this.Button_DeleteDataTable.Size = new System.Drawing.Size(23, 22);
            this.Button_DeleteDataTable.Text = "删除数据表";
            this.Button_DeleteDataTable.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // Button_PreviewDataTable
            // 
            this.Button_PreviewDataTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_PreviewDataTable.Enabled = false;
            this.Button_PreviewDataTable.Image = global::ReportComponents.Properties.Resources.预览;
            this.Button_PreviewDataTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_PreviewDataTable.Name = "Button_PreviewDataTable";
            this.Button_PreviewDataTable.Size = new System.Drawing.Size(23, 22);
            this.Button_PreviewDataTable.Text = "预览数据";
            this.Button_PreviewDataTable.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // Button_DataSourceFilter
            // 
            this.Button_DataSourceFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_DataSourceFilter.Enabled = false;
            this.Button_DataSourceFilter.Image = global::ReportComponents.Properties.Resources.配置;
            this.Button_DataSourceFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_DataSourceFilter.Name = "Button_DataSourceFilter";
            this.Button_DataSourceFilter.Size = new System.Drawing.Size(23, 22);
            this.Button_DataSourceFilter.Text = "条件编辑器";
            this.Button_DataSourceFilter.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // DataSourceView
            // 
            this.DataSourceView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataSourceView.HideSelection = false;
            this.DataSourceView.Location = new System.Drawing.Point(0, 25);
            this.DataSourceView.Name = "DataSourceView";
            this.DataSourceView.Size = new System.Drawing.Size(303, 462);
            this.DataSourceView.TabIndex = 1;
            this.DataSourceView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // DataSourceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 487);
            this.CloseButton = false;
            this.Controls.Add(this.DataSourceView);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "DataSourceForm";
            this.Text = "报表数据集";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Button_DeleteDataTable;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Button_PreviewDataTable;
        private System.Windows.Forms.TreeView DataSourceView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton Button_DataSourceFilter;
        private System.Windows.Forms.ToolStripDropDownButton Button_AppendDataTable;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_DataTable;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_ConstantDataTable;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_CustomDataTable;
        private System.Windows.Forms.ToolStripButton Button_EditDataTable;
    }
}