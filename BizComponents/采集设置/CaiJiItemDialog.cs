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
    public partial class CaiJiItemDialog : Form
    {
        Guid moduleID;
        Boolean isActive = false;
        CaiJiConfigDialog staParent;

        public CaiJiItemDialog(Guid moduleID, Boolean isActive, CaiJiConfigDialog parent)
        {
            staParent = parent;
            InitializeComponent();
            this.isActive = isActive;
            this.moduleID = moduleID;
        }

        private void ItemInfoDialog_Load(object sender, EventArgs e)
        {
            Sys_Module module = ModuleHelperClient.GetModuleBaseInfoByID(moduleID);
            tb_moduleName.Text = module.Name;
            cb_active.Checked = isActive;
            DataTable dt = ModuleHelperClient.GetModuleConfigItemList(moduleID);
            if (module.DeviceType == 1)
            {
                rb_ylj.Checked = true;
                if (dt != null)
                {
                    sheetView1.Rows.Count = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sheetView1.Rows[i].Tag = dt.Rows[i]["ID"];
                        sheetView1.Cells[i, 1].Value = dt.Rows[i]["SerialNumber"];
                        List<JZTestCell> config = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZTestCell>>(dt.Rows[i]["Config"].ToString());
                        if (config != null)
                        {
                            JZTestCell phhz = GetSpecialCell(config, JZTestEnum.PHHZ);
                            if (phhz != null)
                            {
                                sheetView1.Cells[i, 2].Value = phhz.CellName;
                                sheetView1.Cells[i, 2].Tag = phhz.SheetID;
                            }
                        }
                    }
                }
            }
            else if (module.DeviceType == 2)
            {
                rb_wnj.Checked = true;
                if (dt != null)
                {
                    SheetView2.Rows.Count = dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SheetView2.Rows[i].Tag = dt.Rows[i]["ID"];
                        SheetView2.Cells[i, 1].Value = dt.Rows[i]["SerialNumber"];
                        List<JZTestCell> config = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZTestCell>>(dt.Rows[i]["Config"].ToString());
                        if (config != null)
                        {
                            JZTestCell qfl = GetSpecialCell(config, JZTestEnum.QFL);
                            JZTestCell ldzdl = GetSpecialCell(config, JZTestEnum.LDZDL);
                            JZTestCell dhbj = GetSpecialCell(config, JZTestEnum.DHBJ);

                            if (qfl != null)
                            {
                                SheetView2.Cells[i, 2].Value = qfl.CellName;
                                SheetView2.Cells[i, 2].Tag = qfl.SheetID;

                            }
                            if (ldzdl != null)
                            {
                                SheetView2.Cells[i, 4].Value = ldzdl.CellName;
                                SheetView2.Cells[i, 4].Tag = ldzdl.SheetID;
                            }
                            if (dhbj != null)
                            {
                                SheetView2.Cells[i, 6].Value = dhbj.CellName;
                                SheetView2.Cells[i, 6].Tag = dhbj.SheetID;
                            }
                        }
                    }
                }
            }
            else
            {
                radioButton1.Checked = true;
            }
            PanelShow();
        }

        private JZTestCell GetSpecialCell(List<JZTestCell> config, JZTestEnum type)
        {
            foreach (var item in config)
            {
                if (item.Name == type)
                {
                    return item;
                }
            }
            return null;
        }

        private void fpSpread2_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            if (rb_ylj.Checked)
            {
                if (e.Column == 3)
                {
                    CellSelector cellSelector = new CellSelector(moduleID, "", Guid.Empty);
                    if (DialogResult.OK == cellSelector.ShowDialog())
                    {
                        sheetView1.Cells[e.Row, 2].Value = cellSelector.CellName;
                        sheetView1.Cells[e.Row, 2].Tag = cellSelector.SheetID;
                    }
                }
                else if (e.Column == 0)
                {
                    sheetView1.Rows.Remove(e.Row, 1);
                }
            }
            else if (rb_wnj.Checked)
            {
                if (e.Column == 0)
                {
                    SheetView2.Rows.Remove(e.Row, 1);
                }
                else if (e.Column == 3)
                {
                    CellSelector cellSelector = new CellSelector(moduleID, "", Guid.Empty);
                    if (DialogResult.OK == cellSelector.ShowDialog())
                    {
                        SheetView2.Cells[e.Row, 2].Value = cellSelector.CellName;
                        SheetView2.Cells[e.Row, 2].Tag = cellSelector.SheetID;
                    }
                }
                else if (e.Column == 5)
                {
                    CellSelector cellSelector = new CellSelector(moduleID, "", Guid.Empty);
                    if (DialogResult.OK == cellSelector.ShowDialog())
                    {
                        SheetView2.Cells[e.Row, 4].Value = cellSelector.CellName;
                        SheetView2.Cells[e.Row, 4].Tag = cellSelector.SheetID;
                    }
                }
                else if (e.Column == 7)
                {
                    CellSelector cellSelector = new CellSelector(moduleID, "", Guid.Empty);
                    if (DialogResult.OK == cellSelector.ShowDialog())
                    {
                        SheetView2.Cells[e.Row, 6].Value = cellSelector.CellName;
                        SheetView2.Cells[e.Row, 6].Tag = cellSelector.SheetID;
                    }
                }
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            Int32 deviceType = 0;
            List<JZTestConfig> list = new List<JZTestConfig>();
            if (rb_ylj.Checked)
            {
                deviceType = 1;
                for (int i = 0; i < sheetView1.Rows.Count; i++)
                {
                    Cell rowCell = sheetView1.Cells[i, 2];

                    JZTestConfig c = new JZTestConfig();
                    c.ModuleID = moduleID;
                    c.SerialNumber = Convert.ToInt32(sheetView1.Cells[i, 1].Value);
                    c.Config = new List<JZTestCell>();
                    if (rowCell.Value != null && rowCell.Value.ToString().Trim() != "")
                    {
                        JZTestCell cell = new JZTestCell();
                        cell.Name = JZTestEnum.PHHZ;
                        cell.CellName = sheetView1.Cells[i, 2].Value.ToString();
                        cell.SheetID = new Guid(sheetView1.Cells[i, 2].Tag.ToString());
                        c.Config.Add(cell);
                        list.Add(c);
                    }
                }
            }
            else if (rb_wnj.Checked)
            {
                deviceType = 2;
                for (int i = 0; i < SheetView2.Rows.Count; i++)
                {
                    Cell rowCell = SheetView2.Cells[i, 2];

                    JZTestConfig c = new JZTestConfig();
                    c.ModuleID = moduleID;
                    c.SerialNumber = Convert.ToInt32(SheetView2.Cells[i, 1].Value);
                    c.Config = new List<JZTestCell>();
                    JZTestCell cell = null;
                    if (rowCell.Value != null && rowCell.Value.ToString().Trim() != "")
                    {
                        cell = new JZTestCell();
                        cell.Name = JZTestEnum.QFL;
                        cell.CellName = SheetView2.Cells[i, 2].Value.ToString();
                        cell.SheetID = new Guid(SheetView2.Cells[i, 2].Tag.ToString());
                        c.Config.Add(cell);
                    }
                    rowCell = SheetView2.Cells[i, 4];
                    if (rowCell.Value != null && rowCell.Value.ToString().Trim() != "")
                    {
                        cell = new JZTestCell();
                        cell.Name = JZTestEnum.LDZDL;
                        cell.CellName = SheetView2.Cells[i, 4].Value.ToString();
                        cell.SheetID = new Guid(SheetView2.Cells[i, 4].Tag.ToString());
                        c.Config.Add(cell);
                    }
                    rowCell = SheetView2.Cells[i, 6];
                    if (rowCell.Value != null && rowCell.Value.ToString().Trim() != "")
                    {
                        cell = new JZTestCell();
                        cell.Name = JZTestEnum.DHBJ;
                        cell.CellName = SheetView2.Cells[i, 6].Value.ToString();
                        cell.SheetID = new Guid(SheetView2.Cells[i, 6].Tag.ToString());
                        c.Config.Add(cell);
                       
                    }
                    list.Add(c);
                }
            }

            String json = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            ModuleHelperClient.UpdateModuleConfigInfo(moduleID, json, cb_active.Checked, deviceType);
            staParent.BindModelList();
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_AddDays_Click(object sender, EventArgs e)
        {
            if (rb_ylj.Checked)
            {
                sheetView1.Rows.Add(sheetView1.Rows.Count, 1);
            }
            else if (rb_wnj.Checked)
            {
                SheetView2.Rows.Add(SheetView2.Rows.Count, 1);
            }
            else
            {
                MessageBox.Show("请选择试验类别");
            }
        }

        private void rb_ylj_CheckedChanged(object sender, EventArgs e)
        {
            PanelShow();
        }

        private void PanelShow()
        {
            if (rb_ylj.Checked)
            {
                fpSpread1.Visible = true;
                fpSpread2.Visible = false;
            }
            if (rb_wnj.Checked)
            {
                fpSpread1.Visible = false;
                fpSpread2.Visible = true;
            }
            if (radioButton1.Checked)
            {
                fpSpread1.Visible = false;
                fpSpread2.Visible = false;
            }
        }
    }
}
