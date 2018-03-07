using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Yqun.Interfaces;
using System.IO;
using System.Drawing;
using Yqun.Common.ContextCache;
using Yqun.Bases.ClassBases;

namespace Yqun.Bases
{
    public class ControlStandard : ControlBases.ControlBase, IDockingInfo
    {
        private System.ComponentModel.IContainer components;

        public ControlStandard()
        {
            InitializeComponent();

            LoadMenu();
            LoadTool();

            LinkEvents(ToolStrip_Main);
            LinkEvents(ContextMenuStrip_Main);
            LinkEvents(MenuStrip_Main);
        }

        #region 窗口设计器

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ControlStandard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "ControlStandard";
            this.Size = new System.Drawing.Size(588, 397);
            this.ResumeLayout(false);

        }

        #endregion 窗口设计器

        #region IDockingInfo 成员

        /// <summary>
        /// 保存窗口布局信息
        /// </summary>
        public virtual void UpdateDockingInfo()
        {
            
        }

        #endregion
    }
}
