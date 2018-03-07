namespace BizComponents
{
    partial class ModifyDevice
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
            this.label5 = new System.Windows.Forms.Label();
            this.Close = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Pressure = new System.Windows.Forms.RadioButton();
            this.Universal = new System.Windows.Forms.RadioButton();
            this.MachineCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TestRoom = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Unit = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Section = new System.Windows.Forms.ComboBox();
            this.IsActive = new System.Windows.Forms.CheckBox();
            this.Electro = new System.Windows.Forms.CheckBox();
            this.Comment = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.RemoteCode1 = new System.Windows.Forms.TextBox();
            this.RemoteCode2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Errors = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.GenerateMachineCode = new System.Windows.Forms.LinkLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.Quantum = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ComboCompany = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.Errors)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(44, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "设备编码";
            // 
            // Close
            // 
            this.Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Close.Location = new System.Drawing.Point(465, 19);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(75, 23);
            this.Close.TabIndex = 13;
            this.Close.Text = "关闭(&C)";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // Save
            // 
            this.Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Save.Location = new System.Drawing.Point(384, 19);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 13;
            this.Save.Text = "保存(&S)";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(44, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "厂家名称";
            // 
            // Pressure
            // 
            this.Pressure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Pressure.AutoSize = true;
            this.Pressure.Location = new System.Drawing.Point(478, 33);
            this.Pressure.Name = "Pressure";
            this.Pressure.Size = new System.Drawing.Size(59, 16);
            this.Pressure.TabIndex = 12;
            this.Pressure.Text = "压力机";
            this.Pressure.UseVisualStyleBackColor = true;
            // 
            // Universal
            // 
            this.Universal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Universal.AutoSize = true;
            this.Universal.Checked = true;
            this.Universal.Location = new System.Drawing.Point(413, 33);
            this.Universal.Name = "Universal";
            this.Universal.Size = new System.Drawing.Size(59, 16);
            this.Universal.TabIndex = 12;
            this.Universal.TabStop = true;
            this.Universal.Text = "万能机";
            this.Universal.UseVisualStyleBackColor = true;
            this.Universal.CheckedChanged += new System.EventHandler(this.UniversalRadio_CheckedChanged);
            // 
            // MachineCode
            // 
            this.MachineCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MachineCode.Location = new System.Drawing.Point(110, 107);
            this.MachineCode.Name = "MachineCode";
            this.MachineCode.Size = new System.Drawing.Size(184, 21);
            this.MachineCode.TabIndex = 15;
            this.MachineCode.TextChanged += new System.EventHandler(this.MachineCode_TextChanged);
            this.MachineCode.Validating += new System.ComponentModel.CancelEventHandler(this.MachineCode_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(57, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "试验室";
            // 
            // TestRoom
            // 
            this.TestRoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TestRoom.DisplayMember = "FolderName";
            this.TestRoom.FormattingEnabled = true;
            this.TestRoom.Location = new System.Drawing.Point(110, 81);
            this.TestRoom.Name = "TestRoom";
            this.TestRoom.Size = new System.Drawing.Size(246, 20);
            this.TestRoom.TabIndex = 14;
            this.TestRoom.ValueMember = "FolderCode";
            this.TestRoom.Validating += new System.ComponentModel.CancelEventHandler(this.TestRoom_Validating);
            this.TestRoom.SelectedIndexChanged += new System.EventHandler(this.TestRoom_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(70, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "单位";
            // 
            // Unit
            // 
            this.Unit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Unit.DisplayMember = "DepName";
            this.Unit.FormattingEnabled = true;
            this.Unit.Location = new System.Drawing.Point(110, 55);
            this.Unit.Name = "Unit";
            this.Unit.Size = new System.Drawing.Size(246, 20);
            this.Unit.TabIndex = 14;
            this.Unit.ValueMember = "DepCode";
            this.Unit.SelectedIndexChanged += new System.EventHandler(this.Unit_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(70, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "标段";
            // 
            // Section
            // 
            this.Section.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Section.DisplayMember = "PrjsctName";
            this.Section.FormattingEnabled = true;
            this.Section.Location = new System.Drawing.Point(110, 29);
            this.Section.Name = "Section";
            this.Section.Size = new System.Drawing.Size(246, 20);
            this.Section.TabIndex = 14;
            this.Section.ValueMember = "PrjsctCode";
            this.Section.SelectedIndexChanged += new System.EventHandler(this.Section_SelectedIndexChanged);
            // 
            // IsActive
            // 
            this.IsActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IsActive.AutoSize = true;
            this.IsActive.ForeColor = System.Drawing.Color.DimGray;
            this.IsActive.Location = new System.Drawing.Point(378, 215);
            this.IsActive.Name = "IsActive";
            this.IsActive.Size = new System.Drawing.Size(72, 16);
            this.IsActive.TabIndex = 16;
            this.IsActive.Text = "是否删除";
            this.IsActive.UseVisualStyleBackColor = true;
            // 
            // Electro
            // 
            this.Electro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Electro.AutoSize = true;
            this.Electro.ForeColor = System.Drawing.Color.DimGray;
            this.Electro.Location = new System.Drawing.Point(378, 193);
            this.Electro.Name = "Electro";
            this.Electro.Size = new System.Drawing.Size(108, 16);
            this.Electro.TabIndex = 16;
            this.Electro.Text = "是否为电液伺服";
            this.Electro.UseVisualStyleBackColor = true;
            this.Electro.CheckedChanged += new System.EventHandler(this.ElectroCheck_CheckedChanged);
            // 
            // Comment
            // 
            this.Comment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Comment.Location = new System.Drawing.Point(110, 251);
            this.Comment.Multiline = true;
            this.Comment.Name = "Comment";
            this.Comment.Size = new System.Drawing.Size(427, 79);
            this.Comment.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(70, 254);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "备注";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(376, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "类型";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(18, 169);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "公管中心编码";
            // 
            // RemoteCode1
            // 
            this.RemoteCode1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoteCode1.Location = new System.Drawing.Point(110, 161);
            this.RemoteCode1.Name = "RemoteCode1";
            this.RemoteCode1.Size = new System.Drawing.Size(246, 21);
            this.RemoteCode1.TabIndex = 15;
            // 
            // RemoteCode2
            // 
            this.RemoteCode2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoteCode2.Location = new System.Drawing.Point(110, 188);
            this.RemoteCode2.Name = "RemoteCode2";
            this.RemoteCode2.Size = new System.Drawing.Size(246, 21);
            this.RemoteCode2.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(18, 197);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "信息中心编码";
            // 
            // Errors
            // 
            this.Errors.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.Close);
            this.panel1.Controls.Add(this.Save);
            this.panel1.Location = new System.Drawing.Point(0, 349);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 56);
            this.panel1.TabIndex = 19;
            // 
            // GenerateMachineCode
            // 
            this.GenerateMachineCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateMachineCode.AutoSize = true;
            this.GenerateMachineCode.Location = new System.Drawing.Point(303, 111);
            this.GenerateMachineCode.Name = "GenerateMachineCode";
            this.GenerateMachineCode.Size = new System.Drawing.Size(53, 12);
            this.GenerateMachineCode.TabIndex = 20;
            this.GenerateMachineCode.TabStop = true;
            this.GenerateMachineCode.Text = "重新生成";
            this.GenerateMachineCode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GenerateMachineCode_LinkClicked);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.DimGray;
            this.label10.Location = new System.Drawing.Point(70, 224);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "量程";
            // 
            // Quantum
            // 
            this.Quantum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Quantum.Location = new System.Drawing.Point(110, 215);
            this.Quantum.Name = "Quantum";
            this.Quantum.Size = new System.Drawing.Size(207, 21);
            this.Quantum.TabIndex = 15;
            this.Quantum.Validating += new System.ComponentModel.CancelEventHandler(this.Quantum_Validating);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.DimGray;
            this.label11.Location = new System.Drawing.Point(330, 219);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 12);
            this.label11.TabIndex = 18;
            this.label11.Text = "MPA";
            // 
            // ComboCompany
            // 
            this.ComboCompany.FormattingEnabled = true;
            this.ComboCompany.Items.AddRange(new object[] {
            "欧凯",
            "丰依",
            "威海",
            "建依",
            "肯特"});
            this.ComboCompany.Location = new System.Drawing.Point(110, 134);
            this.ComboCompany.Name = "ComboCompany";
            this.ComboCompany.Size = new System.Drawing.Size(246, 20);
            this.ComboCompany.TabIndex = 21;
            // 
            // ModifyDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(552, 403);
            this.Controls.Add(this.ComboCompany);
            this.Controls.Add(this.GenerateMachineCode);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Comment);
            this.Controls.Add(this.Electro);
            this.Controls.Add(this.IsActive);
            this.Controls.Add(this.Quantum);
            this.Controls.Add(this.RemoteCode2);
            this.Controls.Add(this.RemoteCode1);
            this.Controls.Add(this.MachineCode);
            this.Controls.Add(this.Section);
            this.Controls.Add(this.Unit);
            this.Controls.Add(this.TestRoom);
            this.Controls.Add(this.Universal);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Pressure);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Name = "ModifyDevice";
            this.Text = "添加设备";
            this.Load += new System.EventHandler(this.ModifyDevice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Errors)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton Pressure;
        private System.Windows.Forms.RadioButton Universal;
        private System.Windows.Forms.TextBox MachineCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox TestRoom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Unit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox Section;
        private System.Windows.Forms.CheckBox IsActive;
        private System.Windows.Forms.CheckBox Electro;
        private System.Windows.Forms.TextBox Comment;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox RemoteCode1;
        private System.Windows.Forms.TextBox RemoteCode2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ErrorProvider Errors;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel GenerateMachineCode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox Quantum;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ComboCompany;
    }
}