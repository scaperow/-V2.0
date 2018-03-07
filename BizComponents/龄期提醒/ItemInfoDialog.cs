using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread.CellType;
using BizCommon;
using FarPoint.Win.Spread;

namespace BizComponents
{
    public partial class ItemInfoDialog : Form
    {
        JZStadiumConfig config;
        Guid moduleID;
        Boolean isActive = false;
        StadiumConfigDialog staParent;

        public ItemInfoDialog(DataRow row, StadiumConfigDialog parent)
        {
            staParent = parent;
            InitializeComponent();
            if (row != null)
            {
                moduleID = new Guid(row["ID"].ToString());
                config = Newtonsoft.Json.JsonConvert.DeserializeObject<JZStadiumConfig>(row["StadiumConfig"].ToString());
                isActive = row["IsActive"].ToString() == "1" ? true : false;
                tb_moduleName.Text = row["Name"].ToString();
            }
        }

        private void ItemInfoDialog_Load(object sender, EventArgs e)
        {
            if (config != null)
            {
                txtStadiumRange.Text = config.StadiumRange.ToString();
                txtTemperature.Text = config.Temperature.ToString();
                if (config.fPH != null)
                {
                    Sheet_Columns.Cells[0, 1].Value = config.fPH.CellName;
                    Sheet_Columns.Cells[0, 1].Tag = config.fPH.SheetID;
                }
                if (config.fZJRQ != null)
                {
                    Sheet_Columns.Cells[1, 1].Value = config.fZJRQ.CellName;
                    Sheet_Columns.Cells[1, 1].Tag = config.fZJRQ.SheetID;
                }
                if (config.fSJBH != null)
                {
                    Sheet_Columns.Cells[2, 1].Value = config.fSJBH.CellName;
                    Sheet_Columns.Cells[2, 1].Tag = config.fSJBH.SheetID;
                }
                if (config.fSJSize != null)
                {
                    Sheet_Columns.Cells[3, 1].Value = config.fSJSize.CellName;
                    Sheet_Columns.Cells[3, 1].Tag = config.fSJSize.SheetID;
                }
                if (config.fBGBH != null)
                {
                    Sheet_Columns.Cells[4, 1].Value = config.fBGBH.CellName;
                    Sheet_Columns.Cells[4, 1].Tag = config.fBGBH.SheetID;
                }
                if (config.fWTBH != null)
                {
                    Sheet_Columns.Cells[5, 1].Value = config.fWTBH.CellName;
                    Sheet_Columns.Cells[5, 1].Tag = config.fWTBH.SheetID;
                }
                if (config.fAdded != null)
                {
                    Sheet_Columns.Cells[6, 1].Value = config.fAdded.CellName;
                    Sheet_Columns.Cells[6, 1].Tag = config.fAdded.SheetID;
                }
                if (config.ShuLiang != null)
                {
                    Sheet_Columns.Cells[7, 1].Value = config.ShuLiang.CellName;
                    Sheet_Columns.Cells[7, 1].Tag = config.ShuLiang.SheetID;
                }


                cb_active.Checked = isActive;

                //txtStadiumRange.Text = ModuleHelperClient.GetSearchRange(moduleID);
                int iDays = 0;
                for (int i = 0; i < config.DayList.Count; i++)
                {
                    sheetView1.Rows.Add(i, 1);

                    iDays = config.DayList[i].Days;
                    if (iDays % 24 == 0)
                    {//转换成天数
                        iDays = iDays / 24;
                    }
                    else
                    {//转换成小时数
                        iDays = -1 * iDays;
                    }
                    sheetView1.Cells[i, 0].Value = iDays.ToString();
                    sheetView1.Cells[i, 1].Value = config.DayList[i].ItemName.ToString();
                    sheetView1.Cells[i, 1].Tag = config.DayList[i].ItemID;
                    sheetView1.Cells[i, 3].Value = config.DayList[i].IsParameterDays;
                    if (config.DayList[i].PDays != null)
                    {
                        sheetView1.Cells[i, 4].Value = config.DayList[i].PDays.CellName;
                        sheetView1.Cells[i, 4].Tag = config.DayList[i].PDays.SheetID;
                    }
                    if (config.DayList[i].ValidInfo != null)
                    {
                        sheetView1.Cells[i, 6].Value = config.DayList[i].ValidInfo.CellName;
                        sheetView1.Cells[i, 6].Tag = config.DayList[i].ValidInfo.SheetID;
                    }
                }
            }
        }

        private void fpSpread2_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            if (e.Column == 2)
            {
                CellSelector cellSelector = new CellSelector(moduleID, "", Guid.Empty);
                if (DialogResult.OK == cellSelector.ShowDialog())
                {
                    Sheet_Columns.Cells[e.Row, 1].Value = cellSelector.CellName;
                    Sheet_Columns.Cells[e.Row, 1].Tag = cellSelector.SheetID;
                }
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            #region config
            JZStadiumConfig config = new JZStadiumConfig();
            config.DayList = new List<JZStadiumDay>();
            config.Temperature = int.Parse(txtTemperature.Text);//同条件温度提醒值
            config.StadiumRange = int.Parse(txtStadiumRange.Text);//龄期提醒范围
            //fPH  批号
            if (Sheet_Columns.Cells[0, 1].Value != null && Sheet_Columns.Cells[0, 1].Value.ToString().Trim() != "")
            {
                config.fPH = new QualifySetting()
                {
                    SheetID = new Guid(Sheet_Columns.Cells[0, 1].Tag.ToString()),
                    CellName = Sheet_Columns.Cells[0, 1].Value.ToString().Trim()
                };
            }
            else
            {
                config.fPH = null;
            }
            //fZJRQ  制件日期
            if (Sheet_Columns.Cells[1, 1].Value != null && Sheet_Columns.Cells[1, 1].Value.ToString().Trim() != "")
            {
                config.fZJRQ = new QualifySetting()
                {
                    SheetID = new Guid(Sheet_Columns.Cells[1, 1].Tag.ToString()),
                    CellName = Sheet_Columns.Cells[1, 1].Value.ToString().Trim()
                };
            }
            else
            {

                config.fZJRQ = null;
            }
            //fSJBH 试件编号
            if (Sheet_Columns.Cells[2, 1].Value != null && Sheet_Columns.Cells[2, 1].Value.ToString().Trim() != "")
            {
                config.fSJBH = new QualifySetting()
                {
                    SheetID = new Guid(Sheet_Columns.Cells[2, 1].Tag.ToString()),
                    CellName = Sheet_Columns.Cells[2, 1].Value.ToString().Trim()
                };
            }
            else
            {
                config.fSJBH = null;
            }
            //fSJSize  试件尺寸
            if (Sheet_Columns.Cells[3, 1].Value != null && Sheet_Columns.Cells[3, 1].Value.ToString().Trim() != "")
            {
                config.fSJSize = new QualifySetting()
                {
                    SheetID = new Guid(Sheet_Columns.Cells[3, 1].Tag.ToString()),
                    CellName = Sheet_Columns.Cells[3, 1].Value.ToString().Trim()
                };
            }
            else
            {
                config.fSJSize = null;
            }
            //fBGBH  报告编号
            if (Sheet_Columns.Cells[4, 1].Value != null && Sheet_Columns.Cells[4, 1].Value.ToString().Trim() != "")
            {
                config.fBGBH = new QualifySetting()
                {
                    SheetID = new Guid(Sheet_Columns.Cells[4, 1].Tag.ToString()),
                    CellName = Sheet_Columns.Cells[4, 1].Value.ToString().Trim()
                };
            }
            else
            {
                config.fBGBH = null;
            }
            //fWTBH  委托编号
            if (Sheet_Columns.Cells[5, 1].Value != null && Sheet_Columns.Cells[5, 1].Value.ToString().Trim() != "")
            {
                config.fWTBH = new QualifySetting()
                {
                    SheetID = new Guid(Sheet_Columns.Cells[5, 1].Tag.ToString()),
                    CellName = Sheet_Columns.Cells[5, 1].Value.ToString().Trim()
                };

            }
            else
            {
                config.fWTBH = null;
            }
            //fAdded  附件信息
            if (Sheet_Columns.Cells[6, 1].Value != null && Sheet_Columns.Cells[6, 1].Value.ToString().Trim() != "")
            {
                config.fAdded = new QualifySetting()
                {
                    SheetID = new Guid(Sheet_Columns.Cells[6, 1].Tag.ToString()),
                    CellName = Sheet_Columns.Cells[6, 1].Value.ToString().Trim()
                };

            }
            else
            {
                config.fAdded = null;
            }
            //ShuLiang  代表数量
            if (Sheet_Columns.Cells[7, 1].Value != null && Sheet_Columns.Cells[7, 1].Value.ToString().Trim() != "")
            {
                config.ShuLiang = new QualifySetting()
                {
                    SheetID = new Guid(Sheet_Columns.Cells[7, 1].Tag.ToString()),
                    CellName = Sheet_Columns.Cells[7, 1].Value.ToString().Trim()
                };

            }
            else
            {
                config.ShuLiang = null;
            }

            for (int i = 0; i < sheetView1.Rows.Count; i++)
            {
                JZStadiumDay sta = new JZStadiumDay();
                int iDays = Int32.Parse(sheetView1.Cells[i, 0].Value.ToString());
                if (iDays >= 0)
                {//大于0，表示天数，要转换成小时
                    iDays = iDays * 24;
                }
                else
                {//小于0表示小时数，直接转换成小时数
                    iDays = -1 * iDays;
                }
                sta.Days = iDays;
                if (sheetView1.Cells[i, 3].Value == null)
                {
                    sta.IsParameterDays = false;
                }
                else
                {
                    sta.IsParameterDays = Boolean.Parse(sheetView1.Cells[i, 3].Value.ToString());
                }

                if (sheetView1.Cells[i, 4].Value != null && sheetView1.Cells[i, 4].Value.ToString().Trim() != "")
                {
                    sta.PDays = new QualifySetting()
                    {
                        SheetID = new Guid(sheetView1.Cells[i, 4].Tag.ToString()),
                        CellName = sheetView1.Cells[i, 4].Value.ToString().Trim()
                    };
                }
                else
                {
                    sta.PDays = null;
                }
                if (sheetView1.Cells[i, 6].Value != null && sheetView1.Cells[i, 6].Value.ToString().Trim() != "")
                {
                    sta.ValidInfo = new QualifySetting()
                    {
                        SheetID = new Guid(sheetView1.Cells[i, 6].Tag.ToString()),
                        CellName = sheetView1.Cells[i, 6].Value.ToString().Trim()
                    };
                }
                else
                {
                    sta.ValidInfo = null;
                }
                if (sheetView1.Cells[i, 1].Tag != null)
                {
                    sta.ItemID = sheetView1.Cells[i, 1].Tag.ToString();
                    sta.ItemName = sheetView1.Cells[i, 1].Value.ToString();
                }
                else
                {
                    sta.ItemID = "";
                    sta.ItemName = "";
                }
                config.DayList.Add(sta);
            }
            #endregion
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(config);
            ModuleHelperClient.UpdateStadiumConfig(moduleID, json, cb_active.Checked);
            staParent.BindModelList();
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_AddDays_Click(object sender, EventArgs e)
        {
            sheetView1.Rows.Add(sheetView1.Rows.Count, 1);
        }

        private void fpSpread1_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            if (e.Column == 2)//试验项目
            {
                TestProjectDialog Dialog = new TestProjectDialog();
                if (DialogResult.OK == Dialog.ShowDialog())
                {
                    if (Dialog.SelectedItem != null)
                    {
                        sheetView1.Cells[e.Row, 1].Value = Dialog.SelectedItem.Description.Trim();
                        sheetView1.Cells[e.Row, 1].Tag = Dialog.SelectedItem.Index;

                    }

                }
            }

            if (e.Column == 5)//龄期变量
            {
                CellSelector cellSelector = new CellSelector(moduleID, "", Guid.Empty);
                if (DialogResult.OK == cellSelector.ShowDialog())
                {
                    sheetView1.Cells[e.Row, 4].Value = cellSelector.CellName;
                    sheetView1.Cells[e.Row, 4].Tag = cellSelector.SheetID;
                }
            }

            if (e.Column == 7)//验证数据来源
            {
                CellSelector cellSelector = new CellSelector(moduleID, "", Guid.Empty);
                if (DialogResult.OK == cellSelector.ShowDialog())
                {
                    sheetView1.Cells[e.Row, 6].Value = cellSelector.CellName;
                    sheetView1.Cells[e.Row, 6].Tag = cellSelector.SheetID;
                }
            }

            if (e.Column == 8)//删除
            {
                sheetView1.Rows.Remove(e.Row, 1);
            }
        }

        private void txtStadiumRange_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue >= '0' && e.KeyValue <= '9') || ((Keys)(e.KeyValue) == Keys.Back))
            { e.Handled = false; }
            else
            {
                e.Handled = true;
                MessageBox.Show("只能输入数字");
            }
        }
    }
}
