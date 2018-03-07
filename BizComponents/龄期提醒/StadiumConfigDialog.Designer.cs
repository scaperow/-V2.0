namespace BizComponents
{
    partial class StadiumConfigDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StadiumConfigDialog));
            this.ListView_StadiumInfo = new System.Windows.Forms.ListView();
            this.ItemModelText = new System.Windows.Forms.ColumnHeader();
            this.ItemDays = new System.Windows.Forms.ColumnHeader();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Button_AddItem = new System.Windows.Forms.ToolStripButton();
            this.Button_DeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonExit = new System.Windows.Forms.Button();
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
            this.ItemDays.Text = "是否启用";
            this.ItemDays.Width = 262;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Button_AddItem,
            this.Button_DeleteItem,
            this.toolStripSeparator1});
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
            this.Button_AddItem.Text = "添加龄期";
            this.Button_AddItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // Button_DeleteItem
            // 
            this.Button_DeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_DeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("Button_DeleteItem.Image")));
            this.Button_DeleteItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_DeleteItem.Name = "Button_DeleteItem";
            this.Button_DeleteItem.Size = new System.Drawing.Size(60, 22);
            this.Button_DeleteItem.Text = "删除龄期";
            this.Button_DeleteItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "表单.bmp");
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
            this.tableLayoutPanel1.Controls.Add(this.ButtonExit, 7, 0);
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
            // StadiumConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 577);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StadiumConfigDialog";
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader ItemDays;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button ButtonExit;
    }
}