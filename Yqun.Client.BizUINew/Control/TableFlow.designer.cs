namespace Yqun.Client.BizUI
{
    partial class TableFlow
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.addFlow_Tables = new Lassalle.Flow.AddFlow();
            this.SuspendLayout();
            // 
            // addFlow_Tables
            // 
            this.addFlow_Tables.AutoScroll = true;
            this.addFlow_Tables.AutoScrollMinSize = new System.Drawing.Size(745, 460);
            this.addFlow_Tables.BackColor = System.Drawing.Color.White;
            this.addFlow_Tables.DefLinkProp.Line = new Lassalle.Flow.Line(Lassalle.Flow.LineStyle.Database, false, false, false);
            this.addFlow_Tables.DefNodeProp.Alignment = Lassalle.Flow.Alignment.CenterTOP;
            this.addFlow_Tables.DefNodeProp.Dock = System.Windows.Forms.DockStyle.Top;
            this.addFlow_Tables.DefNodeProp.DrawWidth = 2;
            this.addFlow_Tables.DefNodeProp.FillColor = System.Drawing.Color.LightYellow;
            this.addFlow_Tables.DefNodeProp.HighlightChildren = false;
            this.addFlow_Tables.DefNodeProp.InLinkable = false;
            this.addFlow_Tables.DefNodeProp.LabelEdit = false;
            this.addFlow_Tables.DefNodeProp.OutLinkable = false;
            this.addFlow_Tables.DefNodeProp.Shape = new Lassalle.Flow.Shape(Lassalle.Flow.ShapeStyle.Rectangle, Lassalle.Flow.ShapeOrientation.so_0);
            this.addFlow_Tables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addFlow_Tables.Location = new System.Drawing.Point(0, 0);
            this.addFlow_Tables.Name = "addFlow_Tables";
            this.addFlow_Tables.Size = new System.Drawing.Size(573, 336);
            this.addFlow_Tables.TabIndex = 1;
            this.addFlow_Tables.ReadXMLNodeExtraData += new Lassalle.Flow.AddFlow.ReadXMLNodeExtraDataEventHandler(this.addFlow_Tables_ReadXMLNodeExtraData);
            this.addFlow_Tables.AfterAddLink += new Lassalle.Flow.AddFlow.AfterAddLinkEventHandler(this.addFlow_Tables_AfterAddLink);
            this.addFlow_Tables.MouseDown += new System.Windows.Forms.MouseEventHandler(this.addFlow_Tables_MouseDown);
            this.addFlow_Tables.AfterResize += new Lassalle.Flow.AddFlow.AfterResizeEventHandler(this.addFlow_Tables_AfterResize);
            this.addFlow_Tables.WriteXMLNodeExtraData += new Lassalle.Flow.AddFlow.WriteXMLNodeExtraDataEventHandler(this.addFlow_Tables_WriteXMLNodeExtraData);
            // 
            // TableFlow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addFlow_Tables);
            this.Name = "TableFlow";
            this.Size = new System.Drawing.Size(573, 336);
            this.ResumeLayout(false);

        }

        #endregion

        public Lassalle.Flow.AddFlow addFlow_Tables;


    }
}
