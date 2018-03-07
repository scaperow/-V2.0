using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using BizCommon;
using Yqun.Interfaces;
using BizComponents;
using Yqun.Bases;

namespace BizComponents
{
    public partial class TestRoomCodeView : Form
    {
        public TestRoomCodeView()
        {
            InitializeComponent();
        }

        private void QuerySponsorModifyDialog_Load(object sender, EventArgs e)
        {
            DataTable Data = ProjectHelperClient.GetTestRoomCodeView();
            if (Data != null)
            {
                FpSpread FpSpread = fpSpread1;
                SheetView FpSpread_Info = fpSpread1_Sheet;

                FpSpread.ShowRow(FpSpread.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);

                FpSpread_Info.Columns.Count = 8;
                FpSpread_Info.Columns[0].Width = 100;
                FpSpread_Info.Columns[1].Width = 100;
                FpSpread_Info.Columns[2].Width = 100;
                FpSpread_Info.Columns[3].Width = 100;
                FpSpread_Info.Columns[4].Width = 100;
                FpSpread_Info.Columns[5].Width = 100;
                FpSpread_Info.Columns[6].Width = 100;
                FpSpread_Info.Columns[7].Width = 100;

                FpSpread_Info.Rows.Count = Data.Rows.Count;
              
                int i, j;
                foreach (System.Data.DataColumn Column in Data.Columns)
                {
                    i = Data.Columns.IndexOf(Column);
                    FpSpread_Info.Columns[i].VerticalAlignment = CellVerticalAlignment.Center;
                    FpSpread_Info.Columns[i].Label = Column.ColumnName;

                    foreach (DataRow Row in Data.Rows)
                    {
                        j = Data.Rows.IndexOf(Row);
                        FpSpread_Info.Rows[j].HorizontalAlignment = CellHorizontalAlignment.Center;
                        FpSpread_Info.Cells[j, i ].Value = Row[Column.ColumnName].ToString();
                    }
                }
            }
        }

        
    }
}
