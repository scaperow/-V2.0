using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FarPoint.Win.Spread.DrawingSpace;
using FarPoint.Win;
using ReportCommon.Chart;
using System.Xml;
using Yqun.Bases;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Model;

namespace ReportCommon
{
    /// <summary>
    /// 浮动元素
    /// </summary>
    [Serializable]
    public class FloatElement : ReportChartShape
    {
        private static int index = 0;
        private String name = "";
        private int row = 0;
        private int column = 0;
        private int leftDistance = 0;
        private int topDistance = 0;
        private Object value = null;

        public FloatElement()
            : this("")
        {
        }

        public FloatElement(Object value) 
            : this(0, 0, 0, 0, new Size(100, 100), value)
        {
        }

        public FloatElement(int column, int row, Object value) 
            : this(column, row, 0, 0, value)
        {
        }

        public FloatElement(int column, int row, int leftdistance, int topdistance, Object value) 
            : this(column, row, leftdistance, topdistance, new Size(100, 100), value)
        {
        }

        public FloatElement(int column, int row, int leftdistance, int topdistance, Size size, Object value)
        {
            setRow(row);
            setColumn(column);
            setLeftDistance(leftdistance);
            setTopDistance(topdistance);
            setSize(size);
            setValue(value);
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public String getName()
        {
            return this.name;
        }

        public int getRow()
        {
            return this.row;
        }

        public void setRow(int row)
        {
            this.row = row;
        }

        public int getColumn()
        {
            return this.column;
        }

        public void setColumn(int column)
        {
            this.column = column;
        }

        public int getLeftDistance()
        {
            return this.leftDistance;
        }

        public void setLeftDistance(int leftdistance)
        {
            this.leftDistance = leftdistance;
        }

        public int getTopDistance()
        {
            return this.topDistance;
        }

        public void setTopDistance(int topdistance)
        {
            this.topDistance = topdistance;
        }

        public Size getSize()
        {
            return this.Size;
        }

        public void setSize(Size size)
        {
            this.Size = size;
        }

        public Object getValue()
        {
            return this.value;
        }

        public void setValue(Object value)
        {
            this.value = value;
        }

        public override Image Picture
        {
            get
            {
                if (base.Picture == null && value is IPainter)
                {
                    IPainter Painter = value as IPainter;
                    Picture = Painter.Paint(this.Width, this.Height);
                }

                return base.Picture;
            }
            set
            {
                base.Picture = value;
            }
        }

        public override object Clone()
        {
            FloatElement localFloatElement = new FloatElement();
            localFloatElement.name = ("Float" + index++);
            localFloatElement.setRow(getRow());
            localFloatElement.setColumn(getColumn());
            localFloatElement.setLeftDistance(getLeftDistance());
            localFloatElement.setTopDistance(getTopDistance());

            Size size = new Size(getSize().Width, getSize().Height);
            localFloatElement.Location = Location;
            localFloatElement.setSize(size);

            if (getValue() != null)
            {
                if ((getValue() is ChartPainter))
                    localFloatElement.setValue(((ChartPainter)getValue()).Clone());
            }

            return localFloatElement;
        }

        protected override void PaintSpecialForeground(Graphics g, Rectangle r)
        {
            g.DrawImage(Picture, r);
        }

        protected override void PaintBorder(Graphics g, Rectangle r)
        {
            g.DrawRectangle(Pens.Transparent, r);
        }

        protected override void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Width" || e.PropertyName == "Height")
            {
                if (value is IPainter)
                {
                    IPainter Painter = value as IPainter;
                    Picture = Painter.Paint(this.Width, this.Height);
                    Update();
                }
            }
        }

        public override void SerializeProps(XmlTextWriter w)
        {
            base.SerializeProps(w);

            w.WriteElementString("Name", name);
            w.WriteElementString("Row", row.ToString());
            w.WriteElementString("Column", column.ToString());
            w.WriteElementString("LeftDistance", leftDistance.ToString());
            w.WriteElementString("TopDistance", topDistance.ToString());
            w.WriteElementString("Value", BinarySerializer.Serialize(value));
        }

        public override void DeserializeShapeProps(System.Xml.XmlNodeReader r)
        {
            XmlDocument document = new XmlDocument();
            r.MoveToElement();
            XmlNode node = document.ReadNode(r);
            XmlNodeReader reader = new XmlNodeReader(node);

            base.DeserializeShapeProps(r);

            bool flag = true;
            bool flag1 = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
        Label_1:
            if (flag)
            {
                if (!reader.Read())
                {
                    return;
                }
            }
            else
            {
                flag = true;
            }
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    if (reader.Name.Equals("Name"))
                    {
                        flag1 = true;
                    }
                    else if (reader.Name.Equals("Row"))
                    {
                        flag2 = true;
                    }
                    else if (reader.Name.Equals("Column"))
                    {
                        flag3 = true;
                    }
                    else if (reader.Name.Equals("LeftDistance"))
                    {
                        flag4 = true;
                    }
                    else if (reader.Name.Equals("TopDistance"))
                    {
                        flag5 = true;
                    }
                    else if (reader.Name.Equals("Value"))
                    {
                        flag6 = true;
                    }
                    goto Label_1;

                case XmlNodeType.Text:
                    if (flag1)
                    {
                        this.name = reader.Value;
                    }
                    else if (flag2)
                    {
                        this.row = Convert.ToInt32(reader.Value);
                    }
                    else if (flag3)
                    {
                        this.column = Convert.ToInt32(reader.Value);
                    }
                    else if (flag4)
                    {
                        this.leftDistance = Convert.ToInt32(reader.Value);
                    }
                    else if (flag5)
                    {
                        this.topDistance = Convert.ToInt32(reader.Value);
                    }
                    else if (flag6)
                    {
                        this.value = BinarySerializer.Deserialize(reader.Value);
                    }
                    goto Label_1;

                case XmlNodeType.EndElement:
                    if (flag1 && reader.Name.Equals("Name"))
                    {
                        flag1 = false;
                    }
                    else if (flag2 && reader.Name.Equals("Row"))
                    {
                        flag2 = false;
                    }
                    else if (flag3 && reader.Name.Equals("Column"))
                    {
                        flag3 = false;
                    }
                    else if (flag4 && reader.Name.Equals("LeftDistance"))
                    {
                        flag4 = false;
                    }
                    else if (flag5 && reader.Name.Equals("TopDistance"))
                    {
                        flag5 = false;
                    }
                    else if (flag6 && reader.Name.Equals("Value"))
                    {
                        flag6 = false;
                    }
                    goto Label_1;
            }
        }

        public override void DoMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && !Locked && Rectangle.Contains(e.X, e.Y))
            {
                if (value is ChartPainter)
                {
                    ChartEditor ChartEditor = new ChartEditor();
                    ChartEditor.ChartPainter = this.value as ChartPainter;
                    if (DialogResult.OK == ChartEditor.ShowDialog())
                    {
                        this.value = ChartEditor.ChartPainter;
                        (this.value as ChartPainter).UpdateDataSource();
                        IPainter Painter = this.value as IPainter;
                        Picture = Painter.Paint(this.Width, this.Height);
                        Update();
                    }
                }
            }
        }

        protected override void DoSize(int resizeHandle, Point pt)
        {
            FpSpread MyCell = Parent as FpSpread;
            base.DoSize(resizeHandle, pt);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            FpSpread MyCell = Parent as FpSpread;
            Rectangle RowHeaderRectangle = MyCell.GetRowHeaderRectangle(0);
            Rectangle ColumnHeaderRectangle = MyCell.GetColumnHeaderRectangle(0);
            Point point = new Point(this.Left + RowHeaderRectangle.Width, this.Top + ColumnHeaderRectangle.Height);
            CellRange range = MyCell.GetCellFromPixel(0, 0, point.X, point.Y);
            Rectangle r = MyCell.GetCellRectangle(0, 0, range.Row, range.Column);
            setRow(range.Row);
            setColumn(range.Column);
            setLeftDistance(point.X - r.Left);
            setTopDistance(point.Y - r.Top);

            base.OnMouseMove(e);
        }
    }
}
