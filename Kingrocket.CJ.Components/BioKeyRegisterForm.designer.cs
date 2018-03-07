
namespace Kingrocket.CJ.Components
{
    partial class BioKeyRegisterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BioKeyRegisterForm));
            this.button_Register = new System.Windows.Forms.Button();
            this.AxZKFPEngX1 = new AxZKFPEngXControl.AxZKFPEngX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.AxZKFPEngX1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Register
            // 
            this.button_Register.Location = new System.Drawing.Point(556, 412);
            this.button_Register.Name = "button_Register";
            this.button_Register.Size = new System.Drawing.Size(89, 23);
            this.button_Register.TabIndex = 0;
            this.button_Register.Text = "点击开始注册";
            this.button_Register.UseVisualStyleBackColor = true;
            this.button_Register.Click += new System.EventHandler(this.button_Register_Click);
            // 
            // AxZKFPEngX1
            // 
            this.AxZKFPEngX1.Enabled = true;
            this.AxZKFPEngX1.Location = new System.Drawing.Point(2, 420);
            this.AxZKFPEngX1.Name = "AxZKFPEngX1";
            this.AxZKFPEngX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("AxZKFPEngX1.OcxState")));
            this.AxZKFPEngX1.Size = new System.Drawing.Size(24, 24);
            this.AxZKFPEngX1.TabIndex = 2;
            this.AxZKFPEngX1.OnImageReceived += new AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEventHandler(this.AxZKFPEngX1_OnImageReceived);
            this.AxZKFPEngX1.OnFeatureInfo += new AxZKFPEngXControl.IZKFPEngXEvents_OnFeatureInfoEventHandler(this.AxZKFPEngX1_OnFeatureInfo);
            this.AxZKFPEngX1.OnEnroll += new AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEventHandler(this.AxZKFPEngX1_OnEnroll);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(367, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 378);
            this.panel1.TabIndex = 3;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(13, 13);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(348, 377);
            this.treeView1.TabIndex = 4;
            // 
            // BioKeyRegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 447);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button_Register);
            this.Controls.Add(this.AxZKFPEngX1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BioKeyRegisterForm";
            this.Text = "登记指纹";
            this.Load += new System.EventHandler(this.BioKeyRegisterForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BioKeyRegisterForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.AxZKFPEngX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Register;
        private AxZKFPEngXControl.AxZKFPEngX AxZKFPEngX1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView1;
    }
}