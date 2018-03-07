using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ReportCommon
{
    public interface IPainter
    {
        Image Paint(int width, int height);
    }
}
