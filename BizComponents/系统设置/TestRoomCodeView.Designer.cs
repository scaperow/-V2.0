namespace BizComponents
{
    partial class TestRoomCodeView
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet = new FarPoint.Win.Spread.SheetView();
            this.collapsibleSplitter1 = new Yqun.Controls.CollapsibleSplitter();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fpSpread1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(16, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(985, 558);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "试验室编码";
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(3, 17);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet});
            this.fpSpread1.Size = new System.Drawing.Size(979, 538);
            this.fpSpread1.TabIndex = 1;
            this.fpSpread1.TabStripInsertTab = false;
            this.fpSpread1.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpSpread1_Sheet
            // 
            this.fpSpread1_Sheet.Reset();
            this.fpSpread1_Sheet.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet.ColumnCount = 5;
            this.fpSpread1_Sheet.RowCount = 0;
            this.fpSpread1_Sheet.ColumnHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            this.fpSpread1_Sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // collapsibleSplitter1
            // 
            this.collapsibleSplitter1.AnimationDelay = 20;
            this.collapsibleSplitter1.AnimationStep = 20;
            this.collapsibleSplitter1.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.collapsibleSplitter1.ControlToHide = null;
            this.collapsibleSplitter1.ExpandParentForm = false;
            this.collapsibleSplitter1.Location = new System.Drawing.Point(8, 8);
            this.collapsibleSplitter1.Name = "collapsibleSplitter1";
            this.collapsibleSplitter1.TabIndex = 2;
            this.collapsibleSplitter1.TabStop = false;
            this.collapsibleSplitter1.UseAnimations = false;
            this.collapsibleSplitter1.VisualStyle = Yqun.Controls.VisualStyles.DoubleDots;
            // 
            // TestRoomCodeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 574);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.collapsibleSplitter1);
            this.Name = "TestRoomCodeView";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "试验室编码查询";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.QuerySponsorModifyDialog_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet;
        private Yqun.Controls.CollapsibleSplitter collapsibleSplitter1;
    }
}