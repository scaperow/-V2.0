namespace BizComponents
{
    partial class SystemSettingDlg
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
            this.btn_uploadGL = new System.Windows.Forms.Button();
            this.btn_uploadCJ = new System.Windows.Forms.Button();
            this.btn_uploadDY = new System.Windows.Forms.Button();
            this.bt_uploadGL_All = new System.Windows.Forms.Button();
            this.bt_LD = new System.Windows.Forms.Button();
            this.btnSyncGGUsers = new System.Windows.Forms.Button();
            this.btnTestAllLines = new System.Windows.Forms.Button();
            this.btnStartMQ = new System.Windows.Forms.Button();
            this.btnNotifyAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_uploadGL
            // 
            this.btn_uploadGL.Location = new System.Drawing.Point(45, 29);
            this.btn_uploadGL.Name = "btn_uploadGL";
            this.btn_uploadGL.Size = new System.Drawing.Size(127, 23);
            this.btn_uploadGL.TabIndex = 0;
            this.btn_uploadGL.Text = "本线路管理更新";
            this.btn_uploadGL.UseVisualStyleBackColor = true;
            this.btn_uploadGL.Click += new System.EventHandler(this.btn_uploadGL_Click);
            // 
            // btn_uploadCJ
            // 
            this.btn_uploadCJ.Location = new System.Drawing.Point(45, 67);
            this.btn_uploadCJ.Name = "btn_uploadCJ";
            this.btn_uploadCJ.Size = new System.Drawing.Size(127, 23);
            this.btn_uploadCJ.TabIndex = 0;
            this.btn_uploadCJ.Text = "上传数显采集更新";
            this.btn_uploadCJ.UseVisualStyleBackColor = true;
            this.btn_uploadCJ.Click += new System.EventHandler(this.btn_uploadCJ_Click);
            // 
            // btn_uploadDY
            // 
            this.btn_uploadDY.Location = new System.Drawing.Point(45, 108);
            this.btn_uploadDY.Name = "btn_uploadDY";
            this.btn_uploadDY.Size = new System.Drawing.Size(127, 23);
            this.btn_uploadDY.TabIndex = 0;
            this.btn_uploadDY.Text = "上传电液伺服更新";
            this.btn_uploadDY.UseVisualStyleBackColor = true;
            this.btn_uploadDY.Click += new System.EventHandler(this.btn_uploadDY_Click);
            // 
            // bt_uploadGL_All
            // 
            this.bt_uploadGL_All.Location = new System.Drawing.Point(45, 147);
            this.bt_uploadGL_All.Name = "bt_uploadGL_All";
            this.bt_uploadGL_All.Size = new System.Drawing.Size(127, 23);
            this.bt_uploadGL_All.TabIndex = 0;
            this.bt_uploadGL_All.Text = "所有线路管理更新";
            this.bt_uploadGL_All.UseVisualStyleBackColor = true;
            this.bt_uploadGL_All.Click += new System.EventHandler(this.bt_uploadGL_All_Click);
            // 
            // bt_LD
            // 
            this.bt_LD.Location = new System.Drawing.Point(45, 182);
            this.bt_LD.Name = "bt_LD";
            this.bt_LD.Size = new System.Drawing.Size(127, 23);
            this.bt_LD.TabIndex = 0;
            this.bt_LD.Text = "上传领导版管理系统";
            this.bt_LD.UseVisualStyleBackColor = true;
            this.bt_LD.Click += new System.EventHandler(this.bt_LD_Click);
            // 
            // btnSyncGGUsers
            // 
            this.btnSyncGGUsers.Location = new System.Drawing.Point(45, 215);
            this.btnSyncGGUsers.Name = "btnSyncGGUsers";
            this.btnSyncGGUsers.Size = new System.Drawing.Size(127, 23);
            this.btnSyncGGUsers.TabIndex = 0;
            this.btnSyncGGUsers.Text = "同步工管中心用户";
            this.btnSyncGGUsers.UseVisualStyleBackColor = true;
            this.btnSyncGGUsers.Click += new System.EventHandler(this.btnSyncGGUsers_Click);
            // 
            // btnTestAllLines
            // 
            this.btnTestAllLines.Location = new System.Drawing.Point(45, 244);
            this.btnTestAllLines.Name = "btnTestAllLines";
            this.btnTestAllLines.Size = new System.Drawing.Size(127, 23);
            this.btnTestAllLines.TabIndex = 0;
            this.btnTestAllLines.Text = "测试线路连通性";
            this.btnTestAllLines.UseVisualStyleBackColor = true;
            this.btnTestAllLines.Click += new System.EventHandler(this.btnTestAllLines_Click);
            // 
            // btnStartMQ
            // 
            this.btnStartMQ.Location = new System.Drawing.Point(45, 273);
            this.btnStartMQ.Name = "btnStartMQ";
            this.btnStartMQ.Size = new System.Drawing.Size(127, 23);
            this.btnStartMQ.TabIndex = 0;
            this.btnStartMQ.Text = "启动所有队列";
            this.btnStartMQ.UseVisualStyleBackColor = true;
            this.btnStartMQ.Click += new System.EventHandler(this.btnStartMQ_Click);
            // 
            // btnNotifyAll
            // 
            this.btnNotifyAll.Location = new System.Drawing.Point(228, 29);
            this.btnNotifyAll.Name = "btnNotifyAll";
            this.btnNotifyAll.Size = new System.Drawing.Size(127, 23);
            this.btnNotifyAll.TabIndex = 0;
            this.btnNotifyAll.Text = "发送消息(在线所有)";
            this.btnNotifyAll.UseVisualStyleBackColor = true;
            this.btnNotifyAll.Click += new System.EventHandler(this.btnNotifyAll_Click);
            // 
            // SystemSettingDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 322);
            this.Controls.Add(this.btn_uploadDY);
            this.Controls.Add(this.btn_uploadCJ);
            this.Controls.Add(this.btnStartMQ);
            this.Controls.Add(this.btnNotifyAll);
            this.Controls.Add(this.btnTestAllLines);
            this.Controls.Add(this.btnSyncGGUsers);
            this.Controls.Add(this.bt_LD);
            this.Controls.Add(this.bt_uploadGL_All);
            this.Controls.Add(this.btn_uploadGL);
            this.Name = "SystemSettingDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设置";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_uploadGL;
        private System.Windows.Forms.Button btn_uploadCJ;
        private System.Windows.Forms.Button btn_uploadDY;
        private System.Windows.Forms.Button bt_uploadGL_All;
        private System.Windows.Forms.Button bt_LD;
        private System.Windows.Forms.Button btnSyncGGUsers;
        private System.Windows.Forms.Button btnTestAllLines;
        private System.Windows.Forms.Button btnStartMQ;
        private System.Windows.Forms.Button btnNotifyAll;
    }
}