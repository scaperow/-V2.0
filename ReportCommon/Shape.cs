using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win.Spread.DrawingSpace;
using FarPoint.Win.Spread;
using System.Drawing;
using System.Windows.Forms;
using FarPoint.Win;
using ReportCommon.Chart;
using System.Xml;
using Yqun.Bases;
using Steema.TeeChart.Styles;
using System.Data;

namespace ReportCommon
{
    /// <summary>
    /// 图表Shape
    /// </summary>
    [Serializable]
    public class ReportChartShape : RectangleShape
    {
        public ReportChartShape()
        {
            CanPrint = true;
            CanRotate = false;
            ShadowColor = Color.Transparent;
            ShapeOutlineColor = Color.Transparent;
            Border = new EmptyBorder();
        }

        public override bool Locked
        {
            get
            {
                return base.Locked;
            }
            set
            {
                base.Locked = value;

                if (value)
                {
                    CanMove = Moving.None;
                    CanSize = Sizing.None;
                }
                else
                {
                    CanMove = Moving.HorizontalAndVertical;
                    CanSize = Sizing.HeightAndWidth;
                }

                CanRotate = value;
            }
        }
    }
}
