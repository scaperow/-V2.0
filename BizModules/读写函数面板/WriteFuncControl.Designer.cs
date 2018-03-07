namespace BizModules
{
    partial class WriteFuncControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WriteFuncControl));
            this.Button_AppendWriteFun = new System.Windows.Forms.Button();
            this.WriteFunctionList = new System.Windows.Forms.TreeView();
            this.Button_DeleteWriteFun = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // Button_AppendWriteFun
            // 
            this.Button_AppendWriteFun.Location = new System.Drawing.Point(10, 7);
            this.Button_AppendWriteFun.Name = "Button_AppendWriteFun";
            this.Button_AppendWriteFun.Size = new System.Drawing.Size(95, 23);
            this.Button_AppendWriteFun.TabIndex = 5;
            this.Button_AppendWriteFun.Text = "添加写数函数";
            this.Button_AppendWriteFun.UseVisualStyleBackColor = true;
            this.Button_AppendWriteFun.Click += new System.EventHandler(this.Button_AppendWriteFun_Click);
            // 
            // WriteFunctionList
            // 
            this.WriteFunctionList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.WriteFunctionList.FullRowSelect = true;
            this.WriteFunctionList.HideSelection = false;
            this.WriteFunctionList.ImageIndex = 0;
            this.WriteFunctionList.ImageList = this.imageList1;
            this.WriteFunctionList.Location = new System.Drawing.Point(8, 37);
            this.WriteFunctionList.Name = "WriteFunctionList";
            this.WriteFunctionList.SelectedImageIndex = 0;
            this.WriteFunctionList.Size = new System.Drawing.Size(530, 459);
            this.WriteFunctionList.TabIndex = 7;
            this.WriteFunctionList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.WriteFunctionList_NodeMouseDoubleClick);
            // 
            // Button_DeleteWriteFun
            // 
            this.Button_DeleteWriteFun.Location = new System.Drawing.Point(109, 7);
            this.Button_DeleteWriteFun.Name = "Button_DeleteWriteFun";
            this.Button_DeleteWriteFun.Size = new System.Drawing.Size(95, 23);
            this.Button_DeleteWriteFun.TabIndex = 6;
            this.Button_DeleteWriteFun.Text = "删除写数函数";
            this.Button_DeleteWriteFun.UseVisualStyleBackColor = true;
            this.Button_DeleteWriteFun.Click += new System.EventHandler(this.Button_DeleteWriteFun_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "表单.png");
            this.imageList1.Images.SetKeyName(1, "函数.png");
            // 
            // WriteFuncControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Button_AppendWriteFun);
            this.Controls.Add(this.WriteFunctionList);
            this.Controls.Add(this.Button_DeleteWriteFun);
            this.Name = "WriteFuncControl";
            this.Size = new System.Drawing.Size(546, 504);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button_AppendWriteFun;
        private System.Windows.Forms.TreeView WriteFunctionList;
        private System.Windows.Forms.Button Button_DeleteWriteFun;
        private System.Windows.Forms.ImageList imageList1;
    }
}
