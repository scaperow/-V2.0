using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon
{
    [Serializable]
    public class ExpandOrientation : ICloneable
    {
        public readonly static ExpandOrientation Default = new ExpandOrientation();

        public readonly static int None = 1;
        public readonly static int LeftToRight = 2;
        public readonly static int TopToBottom = 3;

        int _Orientation = ExpandOrientation.None;
        public int Orientation
        {
            get
            {
                return _Orientation;
            }
            set
            {
                _Orientation = value;
            }
        }

        String leftParent = "";
        public String LeftParent
        {
            get
            {
                return leftParent;
            }
            set
            {
                leftParent = value;
            }
        }

        String topParent = "";
        public String TopParent
        {
            get
            {
                return topParent;
            }
            set
            {
                topParent = value;
            }
        }

        Boolean _isDefaultLeftParent = true;
        public Boolean isDefaultLeftParent
        {
            get
            {
                return _isDefaultLeftParent;
            }
            set
            {
                _isDefaultLeftParent = value;
            }
        }

        Boolean _isDefaultTopParent = true;
        public Boolean isDefaultTopParent
        {
            get
            {
                return _isDefaultTopParent;
            }
            set
            {
                _isDefaultTopParent = value;
            }
        }

        public object Clone()
        {
            ExpandOrientation Orientation = new ExpandOrientation();
            Orientation.isDefaultLeftParent = this.isDefaultLeftParent;
            Orientation.isDefaultTopParent = this.isDefaultTopParent;
            Orientation.LeftParent = this.LeftParent;
            Orientation.TopParent = this.TopParent;

            return Orientation;
        }
    }
}
