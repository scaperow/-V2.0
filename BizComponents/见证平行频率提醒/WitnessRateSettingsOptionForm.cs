using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BizCommon;
using Yqun.Bases;
using FarPoint.Win.Spread;

namespace BizComponents.见证平行频率提醒
{
    public partial class WitnessRateSettingsOptionForm : Form
    {
        public WitnessRateSettingsOptionForm()
        {
            InitializeComponent();
        }

        private void WitnessRateSettingsOptionForm_Load(object sender, EventArgs e)
        {
            FpSpread_Info.Columns.Count = 4;
            FpSpread_Info.ColumnHeader.RowCount = 1;
            FpSpread_Info.ColumnHeader.Cells[0, 0].Text = "编号";
            FpSpread_Info.ColumnHeader.Cells[0, 0].Tag = "ID";
            FpSpread_Info.Columns[0].Width = 100;
            

            FpSpread_Info.ColumnHeader.Cells[0, 1].Text = "实验描述";
            FpSpread_Info.ColumnHeader.Cells[0, 1].Tag = "Description";
            FpSpread_Info.Columns[1].Width = 400;

            FpSpread_Info.ColumnHeader.Cells[0, 2].Text = "是否激活";
            FpSpread_Info.ColumnHeader.Cells[0, 2].Tag = "IsActive";
            FpSpread_Info.Columns[2].Width = 100;

            FpSpread_Info.ColumnHeader.Cells[0, 3].Text = "见证频率(%)";
            FpSpread_Info.ColumnHeader.Cells[0, 3].Tag = "Frequency";
            FpSpread_Info.Columns[3].Width = 100;

            toolStripComboBox1.SelectedIndex = 0;
            BindData();
        }

        private void BindData()
        {
            try
            {
                ProgressScreen.Current.ShowSplashScreen();
                ProgressScreen.Current.SetStatus = "正在获取数据...";
                String type = "";
                if (toolStripComboBox1.SelectedIndex == 0)
                {
                    type = "1";
                }
                else if (toolStripComboBox1.SelectedIndex == 1)
                {
                    type = "2";
                }
                DataTable Data = PXJZReportDataList.GetWitnessRateInfos(type);
                if (Data != null)
                {
                    FpSpread_Info.DataSource = Data;
                    FpSpread.ShowRow(FpSpread.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
                    FpSpread_Info.Rows.Count = Data.Rows.Count;
                    if (Data.Rows.Count > 0)
                    {
                        for (int i = 0; i < Data.Rows.Count; i++)
                        {
                            FpSpread_Info.Rows[i].HorizontalAlignment = CellHorizontalAlignment.Center;
                            CheckBoxCellType checkBox = new CheckBoxCellType();
                            checkBox.Caption = "启用";
                            if (Data.Rows[i][2].ToString() == "0")
                            {
                                checkBox.ThreeState = false;
                            }
                            else
                            {
                                checkBox.ThreeState = true;
                            }
                            FpSpread_Info.Cells[i, 2].CellType = checkBox;
                        }
                        FpSpread_Info.Columns[0].Width = 100;
                        FpSpread_Info.Columns[0].Locked = true;
                        FpSpread_Info.Columns[1].Width = 400;
                        FpSpread_Info.Columns[1].Locked = true;
                        FpSpread_Info.Columns[2].Width = 100;
                        FpSpread_Info.Columns[3].Width = 100;
                        ProgressScreen.Current.CloseSplashScreen();
                    }
                    else
                    {
                        this.Activate();
                        MessageBox.Show("无数据，请重新选择条件!");
                    }
                }
                else
                {
                    ProgressScreen.Current.CloseSplashScreen();
                    this.Activate();
                    MessageBox.Show("无数据，请重新选择条件!");
                }
                this.Activate();
                if (Data == null)
                {
                    totalCount.Text = string.Format("共 {0} 条记录", 0);
                }
                else
                {
                    totalCount.Text = string.Format("共 {0} 条记录", Data.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.FpSpread_Info.SetActiveCell(-1, -1);
            try
            {
                if (bool.Parse(PXJZReportDataList.SetWitnessRateInfo(FpSpread_Info.GetDataView(true).Table)))
                {
                    MessageBox.Show("实验标准数据更新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("实验标准数据更新失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindData();
        }
    }
}
