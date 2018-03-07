namespace BizComponents
{
    partial class ModelDialog
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage_Models = new System.Windows.Forms.TabPage();
            this.fpSpread_Models = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread_Models_Sheet = new FarPoint.Win.Spread.SheetView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonExit = new System.Windows.Forms.Button();
            this.BindingSource_StadiumModel = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1.SuspendLayout();
            this.TabPage_Models.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_Models)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_Models_Sheet)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource_StadiumModel)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabPage_Models);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(762, 516);
            this.tabControl1.TabIndex = 0;
            // 
            // TabPage_Models
            // 
            this.TabPage_Models.Controls.Add(this.fpSpread_Models);
            this.TabPage_Models.Location = new System.Drawing.Point(4, 22);
            this.TabPage_Models.Name = "TabPage_Models";
            this.TabPage_Models.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Models.Size = new System.Drawing.Size(754, 490);
            this.TabPage_Models.TabIndex = 1;
            this.TabPage_Models.Text = "模板列表";
            this.TabPage_Models.UseVisualStyleBackColor = true;
            // 
            // fpSpread_Models
            // 
            this.fpSpread_Models.AccessibleDescription = "";
            this.fpSpread_Models.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread_Models.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread_Models.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread_Models.Location = new System.Drawing.Point(3, 3);
            this.fpSpread_Models.Name = "fpSpread_Models";
            this.fpSpread_Models.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread_Models.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread_Models_Sheet});
            this.fpSpread_Models.Size = new System.Drawing.Size(748, 484);
            this.fpSpread_Models.TabIndex = 0;
            this.fpSpread_Models.TabStripInsertTab = false;
            this.fpSpread_Models.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.fpSpread_Models.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpSpread_Models_Sheet
            // 
            this.fpSpread_Models_Sheet.Reset();
            this.fpSpread_Models_Sheet.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread_Models_Sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread_Models_Sheet.ColumnCount = 2;
            this.fpSpread_Models_Sheet.RowCount = 0;
            this.fpSpread_Models_Sheet.ColumnHeader.Cells.Get(0, 0).Value = "ID";
            this.fpSpread_Models_Sheet.ColumnHeader.Cells.Get(0, 1).Value = "模板名称";
            this.fpSpread_Models_Sheet.Columns.Get(0).Label = "ID";
            this.fpSpread_Models_Sheet.Columns.Get(0).Width = 256F;
            this.fpSpread_Models_Sheet.Columns.Get(1).Label = "模板名称";
            this.fpSpread_Models_Sheet.Columns.Get(1).Width = 366F;
            this.fpSpread_Models_Sheet.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpSpread_Models_Sheet.RowHeader.Visible = false;
            this.fpSpread_Models_Sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread_Models.SetActiveViewport(0, 1, 0);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(7, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(762, 516);
            this.panel1.TabIndex = 1;
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(518, 533);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(75, 23);
            this.ButtonSave.TabIndex = 2;
            this.ButtonSave.Text = "保存";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ButtonExit
            // 
            this.ButtonExit.Location = new System.Drawing.Point(622, 533);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(75, 23);
            this.ButtonExit.TabIndex = 3;
            this.ButtonExit.Text = "关闭";
            this.ButtonExit.UseVisualStyleBackColor = true;
            this.ButtonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // ModelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 567);
            this.Controls.Add(this.ButtonExit);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModelDialog";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "维护龄期模板";
            this.Load += new System.EventHandler(this.ModelDialog_Load);
            this.tabControl1.ResumeLayout(false);
            this.TabPage_Models.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_Models)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_Models_Sheet)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource_StadiumModel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TabPage_Models;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Button ButtonExit;
        private FarPoint.Win.Spread.FpSpread fpSpread_Models;
        private FarPoint.Win.Spread.SheetView fpSpread_Models_Sheet;
        private System.Windows.Forms.BindingSource BindingSource_StadiumModel;
    }
}