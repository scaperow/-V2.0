using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReportCommon;
using BizComponents;
using BizCommon;
using FarPoint.Win.Spread;
using Yqun.Services;
using Yqun.Interfaces;
using Yqun.Common.ContextCache;
using System.IO;
using Yqun.Bases;

namespace ReportComponents
{
    public partial class ReportConfigDialog : Form
    {
        public ReportConfigDialog()
        {
            InitializeComponent();
        }

        private void CustomDataTableDialog_Load(object sender, EventArgs e)
        {
            DataTable dt = DepositoryReportConfiguration.GetReportConfig();
            if (dt != null)
            {
                FpSpread_Panel.Columns.Count = 4;
                FpSpread_Panel.ColumnHeader.Cells[0, 0].Text = "ID";
                FpSpread_Panel.ColumnHeader.Cells[0, 1].Text = "项目";
                FpSpread_Panel.ColumnHeader.Cells[0, 2].Text = "单位";
                FpSpread_Panel.ColumnHeader.Cells[0, 3].Text = "频率";

                FpSpread_Panel.Columns[0].Width = 80;
                FpSpread_Panel.Columns[1].Width = 200;
                FpSpread_Panel.Columns[2].Width = 100;
                FpSpread_Panel.Columns[3].Width = 100;
                FpSpread_Panel.Rows.Count = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    FpSpread_Panel.Cells[i, 0].Value = dt.Rows[i]["ID"].ToString();
                    FpSpread_Panel.Cells[i, 1].Value = dt.Rows[i]["TestName"].ToString();
                    FpSpread_Panel.Cells[i, 2].Value = dt.Rows[i]["UnitName"].ToString();
                    FpSpread_Panel.Cells[i, 3].Value = dt.Rows[i]["Frequency"].ToString();
                    FpSpread_Panel.Rows[i].Tag = dt.Rows[i]["ModuleID"].ToString();
                    CellNote.SetNoteInformation(FpSpread_Panel.Cells[i, 0], dt.Rows[i]["ModuleName"].ToString());
                }
            }
        }

        private void Button_AddRow_Click(object sender, EventArgs e)
        {
            //int Index = FpSpread_Panel.ActiveRowIndex;
            //if (Index >= 0)
            //{
            //    TableData.AddRow(Index + 1);
            //    ShowJoinTableData();
            //    FpSpread_Panel.ActiveRowIndex = FpSpread_Panel.RowCount - 1;
            //}
        }

        private void Button_DelRow_Click(object sender, EventArgs e)
        {
            //int Index = FpSpread_Panel.ActiveRowIndex;
            //if (Index >= 0)
            //{
            //    TableData.RemoveRow(Index);
            //    ShowJoinTableData();
            //}
        }

        private void FpSpread_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //Button_FieldSetting.Enabled = (e.Row >= 0 && e.Column >= 0);
        }

        private void FpSpread_EditModeOff(object sender, EventArgs e)
        {
            //if (FpSpread_Panel.ActiveCell.Value == null)
            //    FpSpread_Panel.ActiveCell.Value = DBNull.Value;

            //if (FpSpread_Panel.ActiveRowIndex >= 0 && FpSpread_Panel.ActiveColumnIndex >= 0)
            //{
            //    DataRow Row = TableData.GetSchema().Rows[FpSpread_Panel.ActiveRowIndex];
            //    Row[FpSpread_Panel.ActiveColumnIndex] = FpSpread_Panel.ActiveCell.Value;
            //}
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {

        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
