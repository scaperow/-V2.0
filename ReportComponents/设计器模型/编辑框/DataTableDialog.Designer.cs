namespace ReportComponents
{
    partial class DataTableDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataTableDialog));
            this.Button_Ok = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.TableView = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TextBox_Table = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sql_CommandEditor = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Button_AnalysisSQL = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Button_Import = new System.Windows.Forms.ToolStripButton();
            this.Button_Export = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_Ok
            // 
            this.Button_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Ok.Location = new System.Drawing.Point(517, 506);
            this.Button_Ok.Name = "Button_Ok";
            this.Button_Ok.Size = new System.Drawing.Size(75, 23);
            this.Button_Ok.TabIndex = 0;
            this.Button_Ok.Text = "确定";
            this.Button_Ok.UseVisualStyleBackColor = true;
            this.Button_Ok.Click += new System.EventHandler(this.Button_Ok_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Cancel.Location = new System.Drawing.Point(596, 506);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 1;
            this.Button_Cancel.Text = "取消";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // TableView
            // 
            this.TableView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableView.FullRowSelect = true;
            this.TableView.HideSelection = false;
            this.TableView.Location = new System.Drawing.Point(3, 3);
            this.TableView.Name = "TableView";
            this.TableView.Size = new System.Drawing.Size(240, 445);
            this.TableView.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(8, 11);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(254, 477);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.TableView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(246, 451);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据库表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // TextBox_Table
            // 
            this.TextBox_Table.Location = new System.Drawing.Point(271, 31);
            this.TextBox_Table.Name = "TextBox_Table";
            this.TextBox_Table.Size = new System.Drawing.Size(489, 21);
            this.TextBox_Table.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(269, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "数据表名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(270, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "编辑SQL语句：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.sql_CommandEditor);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(271, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(488, 412);
            this.panel1.TabIndex = 14;
            // 
            // sql_CommandEditor
            // 
            this.sql_CommandEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sql_CommandEditor.Location = new System.Drawing.Point(0, 25);
            this.sql_CommandEditor.Multiline = true;
            this.sql_CommandEditor.Name = "sql_CommandEditor";
            this.sql_CommandEditor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.sql_CommandEditor.Size = new System.Drawing.Size(488, 387);
            this.sql_CommandEditor.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Button_AnalysisSQL,
            this.toolStripSeparator1,
            this.Button_Import,
            this.Button_Export});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(488, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Button_AnalysisSQL
            // 
            this.Button_AnalysisSQL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_AnalysisSQL.Image = ((System.Drawing.Image)(resources.GetObject("Button_AnalysisSQL.Image")));
            this.Button_AnalysisSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_AnalysisSQL.Name = "Button_AnalysisSQL";
            this.Button_AnalysisSQL.Size = new System.Drawing.Size(60, 22);
            this.Button_AnalysisSQL.Text = "分析语句";
            this.Button_AnalysisSQL.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // Button_Import
            // 
            this.Button_Import.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_Import.Image = ((System.Drawing.Image)(resources.GetObject("Button_Import.Image")));
            this.Button_Import.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_Import.Name = "Button_Import";
            this.Button_Import.Size = new System.Drawing.Size(36, 22);
            this.Button_Import.Text = "导入";
            this.Button_Import.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // Button_Export
            // 
            this.Button_Export.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_Export.Image = ((System.Drawing.Image)(resources.GetObject("Button_Export.Image")));
            this.Button_Export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_Export.Name = "Button_Export";
            this.Button_Export.Size = new System.Drawing.Size(36, 22);
            this.Button_Export.Text = "导出";
            this.Button_Export.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(8, 490);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(752, 9);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // DataTableDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 540);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextBox_Table);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataTableDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择数据库表";
            this.Load += new System.EventHandler(this.DataTableList_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_Ok;
        private System.Windows.Forms.Button Button_Cancel;
        internal System.Windows.Forms.TreeView TableView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox TextBox_Table;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox sql_CommandEditor;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Button_AnalysisSQL;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Button_Import;
        private System.Windows.Forms.ToolStripButton Button_Export;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

    }
}