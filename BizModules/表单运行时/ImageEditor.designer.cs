namespace BizModules
{
    partial class ImageEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageEditor));
            this.OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.SaveFile = new System.Windows.Forms.SaveFileDialog();
            this.Tools = new System.Windows.Forms.ToolStrip();
            this.ButtonChooseFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ButtonCancel = new System.Windows.Forms.ToolStripButton();
            this.ButtonSave = new System.Windows.Forms.ToolStripButton();
            this.PanelCrop = new System.Windows.Forms.Panel();
            this.Tracker = new System.Windows.Forms.TrackBar();
            this.Picture = new System.Windows.Forms.PictureBox();
            this.BoxContainer = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonResize = new System.Windows.Forms.ToolStripButton();
            this.Tools.SuspendLayout();
            this.PanelCrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tracker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.BoxContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenFile
            // 
            this.OpenFile.RestoreDirectory = true;
            // 
            // SaveFile
            // 
            this.SaveFile.RestoreDirectory = true;
            // 
            // Tools
            // 
            this.Tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ButtonChooseFile,
            this.toolStripButton1,
            this.ButtonCancel,
            this.ButtonSave,
            this.ButtonResize});
            this.Tools.Location = new System.Drawing.Point(0, 0);
            this.Tools.Name = "Tools";
            this.Tools.Size = new System.Drawing.Size(848, 33);
            this.Tools.TabIndex = 0;
            this.Tools.Text = "toolStrip1";
            this.Tools.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Tools_ItemClicked);
            // 
            // ButtonChooseFile
            // 
            this.ButtonChooseFile.Image = ((System.Drawing.Image)(resources.GetObject("ButtonChooseFile.Image")));
            this.ButtonChooseFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonChooseFile.Name = "ButtonChooseFile";
            this.ButtonChooseFile.Padding = new System.Windows.Forms.Padding(5);
            this.ButtonChooseFile.Size = new System.Drawing.Size(85, 30);
            this.ButtonChooseFile.Text = "选择文件";
            this.ButtonChooseFile.Click += new System.EventHandler(this.ButtonChooseFile_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Enabled = false;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Padding = new System.Windows.Forms.Padding(5);
            this.toolStripButton1.Size = new System.Drawing.Size(61, 30);
            this.toolStripButton1.Text = "重做";
            this.toolStripButton1.Visible = false;
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ButtonCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ButtonCancel.Image = ((System.Drawing.Image)(resources.GetObject("ButtonCancel.Image")));
            this.ButtonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(35, 30);
            this.ButtonCancel.Text = "取消";
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("ButtonSave.Image")));
            this.ButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Padding = new System.Windows.Forms.Padding(5);
            this.ButtonSave.Size = new System.Drawing.Size(61, 30);
            this.ButtonSave.Text = "保存";
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // PanelCrop
            // 
            this.PanelCrop.Controls.Add(this.Tracker);
            this.PanelCrop.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelCrop.Location = new System.Drawing.Point(0, 537);
            this.PanelCrop.Name = "PanelCrop";
            this.PanelCrop.Size = new System.Drawing.Size(848, 55);
            this.PanelCrop.TabIndex = 2;
            this.PanelCrop.Visible = false;
            // 
            // Tracker
            // 
            this.Tracker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Tracker.Location = new System.Drawing.Point(23, 7);
            this.Tracker.Maximum = 50;
            this.Tracker.Minimum = 1;
            this.Tracker.Name = "Tracker";
            this.Tracker.Size = new System.Drawing.Size(813, 45);
            this.Tracker.TabIndex = 54;
            this.Tracker.TickFrequency = 10;
            this.Tracker.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.Tracker.Value = 10;
            this.Tracker.ValueChanged += new System.EventHandler(this.Tracker_ValueChanged);
            // 
            // Picture
            // 
            this.Picture.Location = new System.Drawing.Point(23, 23);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(380, 340);
            this.Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Picture.TabIndex = 0;
            this.Picture.TabStop = false;
            this.Picture.DoubleClick += new System.EventHandler(this.Picture_DoubleClick);
            this.Picture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Picture_MouseMove);
            this.Picture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_MouseDown);
            this.Picture.Paint += new System.Windows.Forms.PaintEventHandler(this.Picture_Paint);
            this.Picture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Picture_MouseUp);
            this.Picture.SizeChanged += new System.EventHandler(this.Picture_SizeChanged);
            // 
            // BoxContainer
            // 
            this.BoxContainer.Controls.Add(this.label2);
            this.BoxContainer.Controls.Add(this.Picture);
            this.BoxContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BoxContainer.Location = new System.Drawing.Point(0, 33);
            this.BoxContainer.Name = "BoxContainer";
            this.BoxContainer.Padding = new System.Windows.Forms.Padding(20);
            this.BoxContainer.Size = new System.Drawing.Size(848, 504);
            this.BoxContainer.TabIndex = 3;
            this.BoxContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.BoxContainer_Paint);
            this.BoxContainer.Click += new System.EventHandler(this.BoxContainer_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(686, 445);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 39);
            this.label2.TabIndex = 1;
            this.label2.Text = "点击图片调整大小\r\n\r\n点击裁剪区域可重置位置\r\n";
            this.label2.Visible = false;
            // 
            // ButtonResize
            // 
            this.ButtonResize.Image = ((System.Drawing.Image)(resources.GetObject("ButtonResize.Image")));
            this.ButtonResize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonResize.Name = "ButtonResize";
            this.ButtonResize.Size = new System.Drawing.Size(171, 30);
            this.ButtonResize.Text = "将图片缩放至裁剪框的大小";
            this.ButtonResize.Click += new System.EventHandler(this.ButtonResize_Click);
            // 
            // ImageEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(848, 592);
            this.Controls.Add(this.BoxContainer);
            this.Controls.Add(this.PanelCrop);
            this.Controls.Add(this.Tools);
            this.DoubleBuffered = true;
            this.Name = "ImageEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图片编辑器";
            this.Load += new System.EventHandler(this.ImageEditor_Load);
            this.Tools.ResumeLayout(false);
            this.Tools.PerformLayout();
            this.PanelCrop.ResumeLayout(false);
            this.PanelCrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tracker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.BoxContainer.ResumeLayout(false);
            this.BoxContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog OpenFile;
        private System.Windows.Forms.SaveFileDialog SaveFile;
        private System.Windows.Forms.ToolStrip Tools;
        private System.Windows.Forms.Panel PanelCrop;
        internal System.Windows.Forms.TrackBar Tracker;
        private System.Windows.Forms.PictureBox Picture;
        private System.Windows.Forms.ToolStripButton ButtonChooseFile;
        private System.Windows.Forms.Panel BoxContainer;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton ButtonSave;
        private System.Windows.Forms.ToolStripButton ButtonCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton ButtonResize;
    }
}

