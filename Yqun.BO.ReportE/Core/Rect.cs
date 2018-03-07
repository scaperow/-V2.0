using System;
using System.Collections.Generic;
using System.Text;

namespace Yqun.BO.ReportE.Core
{
    internal class Rect
    {
        internal int left;
        internal int top;
        internal int right;
        internal int bottom;

        public Rect(int Left, int Top, int Right, int Bottom)
        {
            this.left = Left;
            this.top = Top;
            this.right = Right;
            this.bottom = Bottom;
        }

        internal void union(Rect paramRect)
        {
            if (paramRect == null)
                return;
            this.left = Math.Min(this.left, paramRect.left);
            this.top = Math.Min(this.top, paramRect.top);
            this.right = Math.Max(this.right, paramRect.right);
            this.bottom = Math.Max(this.bottom, paramRect.bottom);
        }

        public override String ToString()
        {
            return "{[" + this.left + "→" + this.right + "],[" + this.top + "↓" + this.bottom + "]}";
        }
    }
}
