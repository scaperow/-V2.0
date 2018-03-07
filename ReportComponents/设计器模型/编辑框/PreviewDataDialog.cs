using System;
using System.Windows.Forms;
using ReportCommon;
using System.Data;
using FarPoint.Win.Spread;

namespace ReportComponents
{
    public partial class PreviewDataDialog : Form
    {
        TableData Source;
        IDataSourceAdapter DataAdapter;

        public PreviewDataDialog(TableData Source)
        {
            InitializeComponent();

            this.Source = Source;
            this.DataAdapter = new FrontDataAdapter();
        }

        private void PreviewDataDialog_Load(object sender, EventArgs e)
        {
            DataTable Data = new DataTable();
            this.Source.DataAdapter = this.DataAdapter;
            if (this.Source is ArrayTableData)
            {
                Data = this.Source.GetDataSource();
            }
            else if (this.Source is DbTableData)
            {
                String Command = (this.Source as DbTableData).GetQueryCommand();
                String sql_Command = string.Format("select Top 50 * from ({0}) as a", Command);
                Data = (this.Source as DbTableData).DataAdapter.ExecuteCommand(sql_Command);
            }
            else if (this.Source is JoinTableData)
            {
                String Command = (this.Source as JoinTableData).GetQueryCommand();
                String sql_Command = string.Format("select Top 50 * from ({0}) as a",Command);
                Data = (this.Source as JoinTableData).DataAdapter.ExecuteCommand(sql_Command);
            }

            if (Data.Rows.Count > 0)
            {
                FpSpread spread = fpSpread_DataView.ContainingViews[fpSpread_DataView.ContainingViews.Length - 1].Owner;
                spread.ShowRow(spread.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
            }

            fpSpread_DataView.Columns.Count = Data.Columns.Count;
            fpSpread_DataView.Rows.Count = Data.Rows.Count;
            int i, j;
            foreach (System.Data.DataColumn Column in Data.Columns)
            {
                i = Data.Columns.IndexOf(Column);
                fpSpread_DataView.Columns[i].VerticalAlignment = CellVerticalAlignment.Center;
                fpSpread_DataView.Columns[i].Label = Column.ColumnName;
                fpSpread_DataView.Columns[i].Width = 200;

                foreach (DataRow Row in Data.Rows)
                {
                    j = Data.Rows.IndexOf(Row);
                    fpSpread_DataView.Rows[j].HorizontalAlignment = CellHorizontalAlignment.Center;
                    fpSpread_DataView.Cells[j, i].Value = Row[Column.ColumnName].ToString();
                }
            }
        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
