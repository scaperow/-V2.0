using System;
using System.Windows.Forms;

using AT.STO.UI.Win;

namespace Yqun.Controls
{
	public class DropDownNode : TreeNode, ILookupItem<long>
	{
	#region Private Variable Declarations
		private long _id = 0;
	#endregion
	#region Constructor / Destructor
		/// <summary>
		/// Default constructor.
		/// </summary>
		public DropDownNode() : base()
		{
		}
		
		/// <summary>
		/// /// <summary>
		/// Some constructors initializing the node with Id.
		/// </summary>
		/// </summary>
		/// <param name="Id"></param>
		/// <param name="Text"></param>
		public DropDownNode(long Id, string Text) : base(Text)
		{
			_id = Id;
		}
		
		public DropDownNode(long Id, string Text, DropDownNode[] Children) : base(Text, Children)
		{
			_id = Id;
		}
		
		public DropDownNode(long Id, string Text, int ImageIndex, int SelectedImageIndex) : base(Text, ImageIndex, SelectedImageIndex)
		{
			_id = Id;
		}
		
		public DropDownNode(long Id, string Text, int ImageIndex, int SelectedImageIndex, DropDownNode[] Children) : base(Text, ImageIndex, SelectedImageIndex, Children)
		{
			_id = Id;
		}
	#endregion
	#region Public Methods
		public override string ToString()
		{
			return this.GetType().Name + " (Id=" + Id.ToString() + ", Name=" + Text + ")";
		}
	#endregion
	#region ILookupItem<long> Implementation
		public long Id
		{
			get { return _id; }
		}
		
		public new string Text
		{
			get { return base.Text; }
		}
	#endregion
	}
}
