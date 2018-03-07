using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace BizComponents
{
    public class DataGridViewEx : DataGridView
    {
        int _CharCount;

        public DataGridViewEx()
        {
            SetStyle(ControlStyles.Selectable, false);
        }

        [Browsable(false)]
        public int CharCount
        {
            get
            {
                return _CharCount;
            }
            set
            {
                _CharCount = value;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            HitTestInfo TestInfo = HitTest(e.X, e.Y);
            int Count = TestInfo.RowIndex * Columns.Count + TestInfo.ColumnIndex;
            if (TestInfo.Type == DataGridViewHitTestType.Cell && Count < CharCount)
            {
                base.OnMouseDown(e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            HitTestInfo TestInfo = HitTest(e.X, e.Y);
            int Count = TestInfo.RowIndex * Columns.Count + TestInfo.ColumnIndex;
            if (TestInfo.Type == DataGridViewHitTestType.Cell && Count < CharCount)
            {
                base.OnMouseMove(e);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            HitTestInfo TestInfo = HitTest(e.X, e.Y);
            int Count = TestInfo.RowIndex * Columns.Count + TestInfo.ColumnIndex;
            if (TestInfo.Type == DataGridViewHitTestType.Cell && Count < CharCount)
            {
                base.OnMouseClick(e);
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            HitTestInfo TestInfo = HitTest(e.X, e.Y);
            int Count = TestInfo.RowIndex * Columns.Count + TestInfo.ColumnIndex;
            if (TestInfo.Type == DataGridViewHitTestType.Cell && Count < CharCount)
            {
                base.OnMouseDoubleClick(e);
            }
        }
    }
}
