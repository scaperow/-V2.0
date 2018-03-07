using System;
using System.Windows.Forms;

using AT.STO.UI.Win;

namespace Yqun.Controls
{
	public class DropDownTree : TreeView, IDropDownAware
	{
	#region DropDownTree
		public DropDownTree()
		{
            this.HideSelection = false;
		}
	#endregion
	#region TreeView Events
		/// <summary>
		/// Allow keeping track of the editing process.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnAfterSelect(TreeViewEventArgs e)
		{
			base.OnAfterSelect(e);
			
			if (ValueChanged != null)
			{
				ValueChanged(this, new DropDownValueChangedEventArgs(e.Node));
			}
		}
		
		/// <summary>
		/// A double click on a node counts as finish editing.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDoubleClick(EventArgs e)
		{
			base.OnDoubleClick(e);
			
			TreeNode node = HitTest(PointToClient(Cursor.Position)).Node;
			
			if ((FinishEditing != null) && (node != null))
			{
				FinishEditing(this, new DropDownValueChangedEventArgs(node));
			}
		}

		/// <summary>
		/// ENNTER counts as finish editing, ESC as cancel (null is returned).
		/// </summary>
		/// <param name="e"></param>
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
	
			if (FinishEditing != null)
			{			
				switch (e.KeyCode)
				{
					case Keys.Enter:
						FinishEditing(this, new DropDownValueChangedEventArgs(Value));
						break;
					case Keys.Escape:
						FinishEditing(this, new DropDownValueChangedEventArgs(null));
						break;
				}
			}
		}
	#endregion
	#region IDropDownAware Implementation
		public event DropDownValueChangedEventHandler FinishEditing;
		public event DropDownValueChangedEventHandler ValueChanged;

		public object Value
		{
			get { return base.SelectedNode; }
			set 
			{ 
				if (value is DropDownNode)
				{
					base.SelectedNode = value as DropDownNode; 
				}
			}
		}

		#endregion
	}
}
