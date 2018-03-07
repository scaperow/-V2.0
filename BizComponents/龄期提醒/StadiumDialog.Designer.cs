namespace BizComponents
{
    partial class StadiumDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StadiumDialog));
            this.ListView_StadiumInfo = new System.Windows.Forms.ListView();
            this.ItemModelText = new System.Windows.Forms.ColumnHeader();
            this.ItemDays = new System.Windows.Forms.ColumnHeader();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Button_AddItem = new System.Windows.Forms.ToolStripButton();
            this.Button_DeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.bt_moduleMove = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ButtonExit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_createTest = new System.Windows.Forms.Button();
            this.bt_delete = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.b_bgrq = new System.Windows.Forms.Button();
            this.b_IsQualified = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bt_UpdateModel = new System.Windows.Forms.Button();
            this.ButtonDeviceConvert = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListView_StadiumInfo
            // 
            this.ListView_StadiumInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ItemModelText,
            this.ItemDays});
            this.ListView_StadiumInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView_StadiumInfo.FullRowSelect = true;
            this.ListView_StadiumInfo.Location = new System.Drawing.Point(3, 42);
            this.ListView_StadiumInfo.Name = "ListView_StadiumInfo";
            this.ListView_StadiumInfo.Size = new System.Drawing.Size(759, 482);
            this.ListView_StadiumInfo.TabIndex = 2;
            this.ListView_StadiumInfo.UseCompatibleStateImageBehavior = false;
            this.ListView_StadiumInfo.View = System.Windows.Forms.View.Details;
            this.ListView_StadiumInfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_StadiumInfo_MouseDoubleClick);
            // 
            // ItemModelText
            // 
            this.ItemModelText.Text = "试验名称";
            this.ItemModelText.Width = 272;
            // 
            // ItemDays
            // 
            this.ItemDays.Text = "龄期列表";
            this.ItemDays.Width = 262;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Button_AddItem,
            this.Button_DeleteItem,
            this.toolStripSeparator1,
            this.toolStripButton7,
            this.toolStripButton1,
            this.bt_moduleMove,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripButton8,
            this.toolStripButton9,
            this.toolStripButton10,
            this.ButtonDeviceConvert});
            this.toolStrip1.Location = new System.Drawing.Point(3, 17);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(759, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Button_AddItem
            // 
            this.Button_AddItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_AddItem.Image = ((System.Drawing.Image)(resources.GetObject("Button_AddItem.Image")));
            this.Button_AddItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_AddItem.Name = "Button_AddItem";
            this.Button_AddItem.Size = new System.Drawing.Size(60, 22);
            this.Button_AddItem.Text = "添加试验";
            this.Button_AddItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // Button_DeleteItem
            // 
            this.Button_DeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_DeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("Button_DeleteItem.Image")));
            this.Button_DeleteItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_DeleteItem.Name = "Button_DeleteItem";
            this.Button_DeleteItem.Size = new System.Drawing.Size(60, 22);
            this.Button_DeleteItem.Text = "删除试验";
            this.Button_DeleteItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(84, 22);
            this.toolStripButton7.Text = "维护龄期模板";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(96, 22);
            this.toolStripButton1.Text = "生成配料单信息";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // bt_moduleMove
            // 
            this.bt_moduleMove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bt_moduleMove.Image = ((System.Drawing.Image)(resources.GetObject("bt_moduleMove.Image")));
            this.bt_moduleMove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bt_moduleMove.Name = "bt_moduleMove";
            this.bt_moduleMove.Size = new System.Drawing.Size(65, 22);
            this.bt_moduleMove.Text = "autoSave";
            this.bt_moduleMove.Click += new System.EventHandler(this.bt_moduleMove_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "资料迁移";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "修改日志迁移";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "不合格报表迁移";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "审批修改迁移";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "龄期迁移";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton8.Text = "上传管理系统更新包";
            this.toolStripButton8.ToolTipText = "模拟采集上传";
            this.toolStripButton8.Visible = false;
            this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton9.Image")));
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton9.Text = "导单个模板到模板库";
            this.toolStripButton9.Click += new System.EventHandler(this.toolStripButton9_Click);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton10.Image")));
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton10.Text = "通过模板导资料";
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "表单.bmp");
            // 
            // ButtonExit
            // 
            this.ButtonExit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ButtonExit.Location = new System.Drawing.Point(668, 6);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(95, 23);
            this.ButtonExit.TabIndex = 2;
            this.ButtonExit.Text = "关闭";
            this.ButtonExit.UseVisualStyleBackColor = true;
            this.ButtonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ListView_StadiumInfo);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Location = new System.Drawing.Point(10, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(765, 527);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // bt_createTest
            // 
            this.bt_createTest.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bt_createTest.Location = new System.Drawing.Point(5, 6);
            this.bt_createTest.Name = "bt_createTest";
            this.bt_createTest.Size = new System.Drawing.Size(85, 23);
            this.bt_createTest.TabIndex = 2;
            this.bt_createTest.Text = "生成龄期测试数据";
            this.bt_createTest.UseVisualStyleBackColor = true;
            this.bt_createTest.Visible = false;
            this.bt_createTest.Click += new System.EventHandler(this.bt_createTest_Click);
            // 
            // bt_delete
            // 
            this.bt_delete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bt_delete.Location = new System.Drawing.Point(193, 6);
            this.bt_delete.Name = "bt_delete";
            this.bt_delete.Size = new System.Drawing.Size(91, 23);
            this.bt_delete.TabIndex = 2;
            this.bt_delete.Text = "删除错误龄期数据";
            this.bt_delete.UseVisualStyleBackColor = true;
            this.bt_delete.Click += new System.EventHandler(this.bt_delete_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(96, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "西成三标专用";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // b_bgrq
            // 
            this.b_bgrq.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.b_bgrq.Location = new System.Drawing.Point(290, 6);
            this.b_bgrq.Name = "b_bgrq";
            this.b_bgrq.Size = new System.Drawing.Size(95, 23);
            this.b_bgrq.TabIndex = 4;
            this.b_bgrq.Text = "复制报告日期";
            this.b_bgrq.UseVisualStyleBackColor = true;
            this.b_bgrq.Click += new System.EventHandler(this.b_bgrq_Click);
            // 
            // b_IsQualified
            // 
            this.b_IsQualified.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.b_IsQualified.Location = new System.Drawing.Point(391, 6);
            this.b_IsQualified.Name = "b_IsQualified";
            this.b_IsQualified.Size = new System.Drawing.Size(98, 23);
            this.b_IsQualified.TabIndex = 5;
            this.b_IsQualified.Text = "更新不合格标记";
            this.b_IsQualified.UseVisualStyleBackColor = true;
            this.b_IsQualified.Click += new System.EventHandler(this.b_IsQualified_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.Controls.Add(this.bt_UpdateModel, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.bt_createTest, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.b_IsQualified, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.ButtonExit, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.b_bgrq, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.bt_delete, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 532);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(768, 36);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // bt_UpdateModel
            // 
            this.bt_UpdateModel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bt_UpdateModel.Location = new System.Drawing.Point(495, 6);
            this.bt_UpdateModel.Name = "bt_UpdateModel";
            this.bt_UpdateModel.Size = new System.Drawing.Size(98, 23);
            this.bt_UpdateModel.TabIndex = 6;
            this.bt_UpdateModel.Text = "模板版本更新";
            this.bt_UpdateModel.UseVisualStyleBackColor = true;
            this.bt_UpdateModel.Click += new System.EventHandler(this.bt_UpdateModel_Click);
            // 
            // ButtonDeviceConvert
            // 
            this.ButtonDeviceConvert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ButtonDeviceConvert.Image = ((System.Drawing.Image)(resources.GetObject("ButtonDeviceConvert.Image")));
            this.ButtonDeviceConvert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonDeviceConvert.Name = "ButtonDeviceConvert";
            this.ButtonDeviceConvert.Size = new System.Drawing.Size(60, 22);
            this.ButtonDeviceConvert.Text = "设备转换";
            this.ButtonDeviceConvert.Click += new System.EventHandler(this.ButtonDeviceConvert_Click);
            // 
            // StadiumDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 577);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StadiumDialog";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "试验龄期设置";
            this.Load += new System.EventHandler(this.StadiumDialog_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ListView ListView_StadiumInfo;
        private System.Windows.Forms.ToolStripButton Button_AddItem;
        private System.Windows.Forms.ToolStripButton Button_DeleteItem;
        private System.Windows.Forms.ColumnHeader ItemModelText;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button ButtonExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader ItemDays;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bt_moduleMove;
        private System.Windows.Forms.Button bt_createTest;
        private System.Windows.Forms.Button bt_delete;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button b_bgrq;
        private System.Windows.Forms.Button b_IsQualified;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button bt_UpdateModel;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStripButton ButtonDeviceConvert;
    }
}