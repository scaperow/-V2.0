using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon
{
    public class ColumnRowLocation
    {
        public static ColumnRowLocation ALL = new ColumnRowLocation(null);
        private LocationDim[] dims;

        public ColumnRowLocation(LocationDim[] paramArrayOfLocationDim)
        {
            this.dims = paramArrayOfLocationDim;
        }

        public LocationDim[] getDims()
        {
            return this.dims;
        }

        public override string ToString()
        {
            if (this == ALL)
                return "`0";
            StringBuilder localStringBuilder = new StringBuilder();
            for (int i = 0; i < this.dims.Length; i++)
            {
                if (i > 0)
                    localStringBuilder.Append(",");
                localStringBuilder.Append(this.dims[i]);
            }
            return localStringBuilder.ToString();
        }
    }
}
