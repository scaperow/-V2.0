using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon
{
    public class LocationDim
    {
        public static byte ABSOLUTE = 0;
        public static byte PLUS = 1;
        public static byte MINUS = 2;
        private String columnrow;
        private int index;
        private byte op;

        public LocationDim(String paramColumnRow, byte op, int index)
        {
            this.columnrow = paramColumnRow;
            this.op = op;
            this.index = index;
        }

        public String getColumnrow()
        {
            return this.columnrow;
        }

        public void setIndex(int index)
        {
            this.index = index;
        }

        public int getIndex()
        {
            return this.index;
        }

        public byte getOp()
        {
            return this.op;
        }

        public override string ToString()
        {
            return this.columnrow + ":" + (this.op == 2 ? "-" : this.op == 1 ? "+" : "!") + this.index;
        }
    }
}
