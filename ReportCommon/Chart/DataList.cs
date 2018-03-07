using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ReportCommon.Chart
{
    public class DataList
    {
        private DataTable sourceTable;
        private System.Data.DataColumn colXData;
        private System.Data.DataColumn colYData;
        private System.Data.DataColumn colZData;
        private System.Data.DataColumn colLabels;

        public DataList()
        {
            // Create DataTable
            sourceTable = new DataTable("sourceTable");
            colXData = new System.Data.DataColumn("XData", Type.GetType("System.Double"));
            colYData = new System.Data.DataColumn("YData", Type.GetType("System.Double"));
            colZData = new System.Data.DataColumn("ZData", Type.GetType("System.Double"));
            colLabels = new System.Data.DataColumn("Labels", Type.GetType("System.String"));

            sourceTable.Columns.Add(colXData);
            sourceTable.Columns.Add(colYData);
            sourceTable.Columns.Add(colZData);
            sourceTable.Columns.Add(colLabels);
        }

        public void SetData(DataTable Data)
        {
            sourceTable = Data;
        }

        public void AddData(double x, string xLabel)
        {
            DataRow NewRow = sourceTable.NewRow();
            NewRow["XData"] = x;
            NewRow["Labels"] = xLabel;
            sourceTable.Rows.Add(NewRow);
        }

        public void AddData(double x, double y, string xLabel)
        {
            DataRow NewRow = sourceTable.NewRow();
            NewRow["XData"] = x;
            NewRow["YData"] = y;
            NewRow["Labels"] = xLabel;
            sourceTable.Rows.Add(NewRow);
        }

        public void AddData(double x, double y, double z, string xLabel)
        {
            DataRow NewRow = sourceTable.NewRow();
            NewRow["XData"] = x;
            NewRow["YData"] = y;
            NewRow["ZData"] = z;
            NewRow["Labels"] = xLabel;
            sourceTable.Rows.Add(NewRow);
        }

        public void DeleteData(int Index)
        {
            if (Index < sourceTable.Rows.Count)
            {
                sourceTable.Rows.RemoveAt(Index);
            }
        }

        public void DeleteData()
        {
            sourceTable.Rows.Clear();
        }

        public DataTable Data
        {
            get
            {
                return sourceTable;
            }
        }

        public System.Data.DataColumn ColXData
        {
            get
            {
                return colXData;
            }
        }

        public System.Data.DataColumn ColYData
        {
            get
            {
                return colYData;
            }
        }

        public System.Data.DataColumn ColZData
        {
            get
            {
                return colZData;
            }
        }

        public System.Data.DataColumn ColLabels
        {
            get
            {
                return colLabels;
            }
        }
    }
}
