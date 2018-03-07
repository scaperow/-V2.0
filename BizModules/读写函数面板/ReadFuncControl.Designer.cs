namespace BizModules
{
    partial class ReadFuncControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReadFuncControl));
            this.ReadFunctionList = new System.Windows.Forms.TreeView();
            this.Button_AppendReadFun = new System.Windows.Forms.Button();
            this.Button_DeleteReadFun = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // ReadFunctionList
            // 
            this.ReadFunctionList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ReadFunctionList.FullRowSelect = true;
            this.ReadFunctionList.HideSelection = false;
            this.ReadFunctionList.ImageIndex = 0;
            this.ReadFunctionList.ImageList = this.imageList1;
            this.ReadFunctionList.Location = new System.Drawing.Point(8, 37);
            this.ReadFunctionList.Name = "ReadFunctionList";
            this.ReadFunctionList.SelectedImageIndex = 0;
            this.ReadFunctionList.Size = new System.Drawing.Size(530, 459);
            this.ReadFunctionList.TabIndex = 9;
            this.ReadFunctionList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ReadFunctionList_NodeMouseClick);
            // 
            // Button_AppendReadFun
            // 
            this.Button_AppendReadFun.Location = new System.Drawing.Point(10, 7);
            this.Button_AppendReadFun.Name = "Button_AppendReadFun";
            this.Button_AppendReadFun.Size = new System.Drawing.Size(95, 23);
            this.Button_AppendReadFun.TabIndex = 7;
            this.Button_AppendReadFun.Text = "添加读数函数";
            this.Button_AppendReadFun.UseVisualStyleBackColor = true;
            this.Button_AppendReadFun.Click += new System.EventHandler(this.Button_AppendReadFun_Click);
            // 
            // Button_DeleteReadFun
            // 
            this.Button_DeleteReadFun.Location = new System.Drawing.Point(109, 7);
            this.Button_DeleteReadFun.Name = "Button_DeleteReadFun";
            this.Button_DeleteReadFun.Size = new System.Drawing.Size(95, 23);
            this.Button_DeleteReadFun.TabIndex = 8;
            this.Button_DeleteReadFun.Text = "删除读数函数";
            this.Button_DeleteReadFun.UseVisualStyleBackColor = true;
            this.Button_DeleteReadFun.Click += new System.EventHandler(this.Button_DeleteReadFun_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "表单.png");
            this.imageList1.Images.SetKeyName(1, "函数.png");
            // 
            // ReadFuncControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ReadFunctionList);
            this.Controls.Add(this.Button_AppendReadFun);
            this.Controls.Add(this.Button_DeleteReadFun);
            this.Name = "ReadFuncControl";
            this.Size = new System.Drawing.Size(546, 504);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView ReadFunctionList;
        private System.Windows.Forms.Button Button_AppendReadFun;
        private System.Windows.Forms.Button Button_DeleteReadFun;
        private System.Windows.Forms.ImageList imageList1;
    }
}
