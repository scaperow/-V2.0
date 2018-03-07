using Yqun.UI.Controls;
namespace Yqun.MainUI
{
    public partial class NavBarExplorer
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
            this.Main_TabControl = new Yqun.UI.Controls.YaTabControl();
            this.SuspendLayout();
            // 
            // Main_TabControl
            // 
            this.Main_TabControl.ActiveColor = System.Drawing.SystemColors.Control;
            this.Main_TabControl.BackColor = System.Drawing.SystemColors.Control;
            this.Main_TabControl.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Main_TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Main_TabControl.ImageIndex = -1;
            this.Main_TabControl.ImageList = null;
            this.Main_TabControl.InactiveColor = System.Drawing.SystemColors.Window;
            this.Main_TabControl.Location = new System.Drawing.Point(0, 0);
            this.Main_TabControl.Name = "Main_TabControl";
            this.Main_TabControl.ScrollButtonStyle = Yqun.UI.Controls.YaScrollButtonStyle.Auto;
            this.Main_TabControl.TabDrawer = new XlTabDrawer();
            this.Main_TabControl.SelectedIndex = -1;
            this.Main_TabControl.SelectedTab = null;
            this.Main_TabControl.Size = new System.Drawing.Size(214, 462);
            this.Main_TabControl.TabDock = System.Windows.Forms.DockStyle.Left;
            this.Main_TabControl.TabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Main_TabControl.TabIndex = 0;
            this.Main_TabControl.Text = "Main_TabControl";
            this.Main_TabControl.TabChanged += new System.EventHandler(this.Main_TabControl_TabChanged);
            // 
            // NavBarExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Main_TabControl);
            this.Name = "NavBarExplorer";
            //this.Name = "SolutionExplorer";
            this.Size = new System.Drawing.Size(214, 462);
            this.Load += new System.EventHandler(this.NavBarExplorer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Yqun.UI.Controls.YaTabControl Main_TabControl;
    }
}
