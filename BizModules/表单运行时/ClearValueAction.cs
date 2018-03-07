using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Model;

namespace BizModules
{
    public class ClearValueAction : FarPoint.Win.Spread.Action
    {
        public override void PerformAction(object source)
        {

            if (source is SpreadView)
            {

                SpreadView spread = (SpreadView)source;

                SheetView sheet = spread.Sheets[spread.ActiveSheetIndex];

                CellRange cr = sheet.GetSelection(0);

                StyleInfo si = new StyleInfo();



                for (int r = 0; r < cr.RowCount; r++)
                {

                    for (int c = 0; c < cr.ColumnCount; c++)
                    {

                        //sheet.Models.Style.GetCompositeInfo(cr.Row + r, cr.Column + c, -1, si);

                        if (!si.Locked)
                        {

                            sheet.Cells[cr.Row + r, cr.Column + c].ResetValue();

                        }

                    }

                }

            }

        }
 
    }
}
