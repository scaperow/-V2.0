using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizCommon;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Model;

namespace BizComponents
{
    public partial class ModelUserFieldSettingDialog : Form 
    {
        Guid moduleID;
        String testRoomCode;
        List<JZCustomView> views = null;
        String IsNo = Convert.ToChar(0x00FB).ToString();
        String IsYes = Convert.ToChar(0x00FC).ToString();

        Font font_Wingdings = new Font("Wingdings", 9f, FontStyle.Bold);

        public ModelUserFieldSettingDialog(Guid moduleID, String testRoomCode)
        {
            InitializeComponent();

            this.testRoomCode = testRoomCode;
            this.moduleID = moduleID;
        }

        private void ModelFieldSettingDialog_Load(object sender, EventArgs e)
        {
            views = DocumentHelperClient.GetCustomViewList(moduleID, testRoomCode);
            LoadCustomView();
        }

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == AppendButton)
            {
                AppendItem();
            }
            else if (sender == RemoveButton)
            {
                RemoveItem();
            }
            else if (sender == MovePrevButton)
            {
                MoveItemToPrev();
            }
            else if (sender == MoveNextButton)
            {
                MoveItemToNext();
            }
        }

        private int GetMaxOrder()
        {
            int MaxValue = -1;
            foreach (Row row in this.fpSpread1.ActiveSheet.Rows)
            {
                ModuleField modelField = row.Tag as ModuleField;
                if (modelField == null)
                    continue;

                if (modelField.OrderIndex > MaxValue)
                    MaxValue = modelField.OrderIndex;
            }

            return MaxValue + 1;
        }

        /// <summary>
        /// 添加显示项
        /// </summary>
        private void AppendItem()
        {
            ModelFieldAppendDialog ModelFieldAppendDialog = new ModelFieldAppendDialog(moduleID, null);
            
            if (DialogResult.OK == ModelFieldAppendDialog.ShowDialog())
            {
                if (ModelFieldAppendDialog.view != null)
                {
                    views.Add(ModelFieldAppendDialog.view);
                    LoadCustomView();
                }
            }
        }

        /// <summary>
        /// 移除显示项
        /// </summary>
        private void RemoveItem()
        {
            if (this.fpSpread1.ActiveSheet.RowCount > 0)
            {
                Row row = this.fpSpread1.ActiveSheet.ActiveRow;
                String Msg = "是否删除所选显示项吗？";
                if (DialogResult.Yes == MessageBox.Show(Msg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    JZCustomView view = row.Tag as JZCustomView;
                    if (view != null)
                    {
                        views.Remove(view);
                        LoadCustomView();
                    }
                }
            }
        }

        /// <summary>
        /// 移动到上一个
        /// </summary>
        private void MoveItemToPrev()
        {
            if (this.fpSpread1.ActiveSheet.RowCount > 0)
            {
                Row row = this.fpSpread1.ActiveSheet.ActiveRow;
                if (row.Index == 0)
                    return;
                JZCustomView view = row.Tag as JZCustomView;
                if (view != null)
                {
                    views.RemoveAt(row.Index);
                    views.Insert(row.Index - 1, view);
                    LoadCustomView();
                }
                
            }
        }

        /// <summary>
        /// 移动到下一个
        /// </summary>
        private void MoveItemToNext()
        {
            if (this.fpSpread1.ActiveSheet.ActiveRow.Index + 1 < this.fpSpread1.ActiveSheet.RowCount)
            {
                Row row = this.fpSpread1.ActiveSheet.ActiveRow;
                if (row.Index == this.fpSpread1.ActiveSheet.RowCount - 1)
                    return;

                JZCustomView view = row.Tag as JZCustomView;
                if (view != null)
                {
                    views.RemoveAt(row.Index);
                    views.Insert(row.Index + 1, view);
                    LoadCustomView();
                }
            }
        }

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fpSpread1_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            Row row = this.fpSpread1.ActiveSheet.ActiveRow;
            if (row.Tag != null)
            {
                JZCustomView view = row.Tag as JZCustomView;
                if (view != null)
                {
                    ModelFieldAppendDialog ModelFieldAppendDialog = new ModelFieldAppendDialog(moduleID, view);
                    if (DialogResult.OK == ModelFieldAppendDialog.ShowDialog())
                    {
                        LoadCustomView();
                    }
                }
            }
        }

        private void LoadCustomView()
        {
            if (this.fpSpread1_Sheet1.Rows.Count > 0)
            {
                this.fpSpread1_Sheet1.RemoveRows(0, this.fpSpread1_Sheet1.Rows.Count);
            }
            foreach (JZCustomView view in views)
            {
                this.fpSpread1.ActiveSheet.AddRows(this.fpSpread1.ActiveSheet.RowCount, 1);
                this.fpSpread1.ActiveSheet.Rows[this.fpSpread1.ActiveSheet.RowCount - 1].Tag = view;
                this.fpSpread1.ActiveSheet.Rows[this.fpSpread1.ActiveSheet.RowCount - 1].HorizontalAlignment = CellHorizontalAlignment.Center;
                this.fpSpread1.ActiveSheet.Rows[this.fpSpread1.ActiveSheet.RowCount - 1].VerticalAlignment = CellVerticalAlignment.Center;

                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 0].Value = view.Description;
                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 1].Value = view.CellName;
                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 2].Value = "文本";

                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 3].Value = IsYes;
                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 3].Font = font_Wingdings;
                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 3].ForeColor = Color.Blue;

                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 4].Value = (view.IsEdit ? IsYes : IsNo);
                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 4].Font = font_Wingdings;
                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 4].ForeColor = Color.Blue;

                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 5].Value =  IsYes;
                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 5].Font = font_Wingdings;
                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 5].ForeColor = Color.Blue;

                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 6].Value = IsYes;
                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 6].Font = font_Wingdings;
                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 6].ForeColor = Color.Blue;

                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 7].BackColor = Color.Black;
                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 8].BackColor = Color.White;

                this.fpSpread1_Sheet1.Cells[this.fpSpread1.ActiveSheet.RowCount - 1, 9].Value = "";
            }
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(views).Replace("'","''");
            Boolean flag = DocumentHelperClient.SaveCustomView(moduleID, testRoomCode, json);
            if (!flag)
            {
                MessageBox.Show("保存失败，请稍后再试");
            }
            else
            {
                this.Close();
            }
        }
    }
}
