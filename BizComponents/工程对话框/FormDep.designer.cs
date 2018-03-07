namespace BizComponents
{
    partial class FormDep
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DepTree = new System.Windows.Forms.TreeView();
            this.btnDele = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.DepTypeCmb = new System.Windows.Forms.ComboBox();
            this.DepAbbrevTxt = new System.Windows.Forms.TextBox();
            this.DepNameTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TreeView_Company = new System.Windows.Forms.TreeView();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DepTree);
            this.groupBox3.Location = new System.Drawing.Point(8, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(272, 424);
            this.groupBox3.TabIndex = 61;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "单位列表";
            // 
            // DepTree
            // 
            this.DepTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DepTree.FullRowSelect = true;
            this.DepTree.HideSelection = false;
            this.DepTree.Location = new System.Drawing.Point(3, 17);
            this.DepTree.Name = "DepTree";
            this.DepTree.Size = new System.Drawing.Size(266, 404);
            this.DepTree.TabIndex = 1;
            this.DepTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DepTree_AfterSelect);
            // 
            // btnDele
            // 
            this.btnDele.Location = new System.Drawing.Point(157, 385);
            this.btnDele.Name = "btnDele";
            this.btnDele.Size = new System.Drawing.Size(65, 23);
            this.btnDele.TabIndex = 7;
            this.btnDele.Text = "删除";
            this.btnDele.UseVisualStyleBackColor = true;
            this.btnDele.Click += new System.EventHandler(this.btnDele_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TreeView_Company);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnEdit);
            this.groupBox2.Controls.Add(this.btnNew);
            this.groupBox2.Controls.Add(this.DepTypeCmb);
            this.groupBox2.Controls.Add(this.DepAbbrevTxt);
            this.groupBox2.Controls.Add(this.DepNameTxt);
            this.groupBox2.Controls.Add(this.btnDele);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(288, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(340, 421);
            this.groupBox2.TabIndex = 62;
            this.groupBox2.TabStop = false;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(84, 385);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(65, 23);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "编辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(10, 385);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(65, 23);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "新增";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // DepTypeCmb
            // 
            this.DepTypeCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DepTypeCmb.FormattingEnabled = true;
            this.DepTypeCmb.Items.AddRange(new object[] {
            "施工单位",
            "监理单位",
            "建设单位"});
            this.DepTypeCmb.Location = new System.Drawing.Point(79, 79);
            this.DepTypeCmb.Name = "DepTypeCmb";
            this.DepTypeCmb.Size = new System.Drawing.Size(249, 20);
            this.DepTypeCmb.TabIndex = 4;
            this.DepTypeCmb.SelectedIndexChanged += new System.EventHandler(this.DepTypeCmb_SelectedIndexChanged);
            // 
            // DepAbbrevTxt
            // 
            this.DepAbbrevTxt.Location = new System.Drawing.Point(79, 48);
            this.DepAbbrevTxt.Name = "DepAbbrevTxt";
            this.DepAbbrevTxt.Size = new System.Drawing.Size(249, 21);
            this.DepAbbrevTxt.TabIndex = 3;
            // 
            // DepNameTxt
            // 
            this.DepNameTxt.Location = new System.Drawing.Point(79, 17);
            this.DepNameTxt.Name = "DepNameTxt";
            this.DepNameTxt.Size = new System.Drawing.Size(249, 21);
            this.DepNameTxt.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "单位简称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "单位类型：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "单位名称：";
            // 
            // btnSubmit
            // 
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(453, 438);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 8;
            this.btnSubmit.Text = "关 闭";
            this.btnSubmit.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "所辖单位：";
            this.label4.Visible = false;
            // 
            // TreeView_Company
            // 
            this.TreeView_Company.CheckBoxes = true;
            this.TreeView_Company.FullRowSelect = true;
            this.TreeView_Company.HideSelection = false;
            this.TreeView_Company.Location = new System.Drawing.Point(79, 111);
            this.TreeView_Company.Name = "TreeView_Company";
            this.TreeView_Company.Size = new System.Drawing.Size(249, 262);
            this.TreeView_Company.TabIndex = 10;
            this.TreeView_Company.Visible = false;
            // 
            // FormDep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 472);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDep";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单位维护";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormDep_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDele;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox DepTypeCmb;
        private System.Windows.Forms.TextBox DepAbbrevTxt;
        private System.Windows.Forms.TextBox DepNameTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TreeView DepTree;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TreeView TreeView_Company;
    }
}