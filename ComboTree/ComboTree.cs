using System;
using System.ComponentModel;
using System.Windows.Forms;

using AT.STO.UI.Win;

namespace Yqun.Controls
{
	public class ComboTree : DropDownPanel
	{
        #region Constructor / Destructor
		public ComboTree() : base()
		{
			// The base class's property must be set because
			// this derived implementation hides the setter.
			base.DropDownControl = new DropDownTree();
			this.DropDownControl.BorderStyle = BorderStyle.None;
		}
	
        #endregion
	
        #region Public Properties
		/// <summary>
		/// Returns the DateRangePicker, that is displayed in the dropdown.
        /// new is on purpose to change the property's data type and to hide the setter.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new DropDownTree DropDownControl	
		{
			get
			{ 
				if (base.DropDownControl != null)					
				{
					return base.DropDownControl as DropDownTree;
				}
				
				return null;
			}
		}
		
		public new DropDownNode Value
		{
			get 
			{
				if (this.DropDownControl != null)
				{
					return this.DropDownControl.Value as DropDownNode;
				}
				
				return null;
			}
			set { base.Value = value; }
		}

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ComboTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "ComboTree";
            this.Size = new System.Drawing.Size(106, 20);
            this.ResumeLayout(false);

        }
	}
}
