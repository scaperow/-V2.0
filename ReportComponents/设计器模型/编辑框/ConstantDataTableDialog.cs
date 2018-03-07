using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReportCommon;
using FarPoint.Win.Spread;
using Yqun.Common.Encoder;
using System.IO;
using Yqun.Bases;
using Yqun.Common.ContextCache;
using Yqun.Interfaces;
using FarPoint.Win.Spread.Model;
using BizCommon;

namespace ReportComponents
{
    public partial class ConstantDataTableDialog : Form
    {
        ArrayTableData _ArrayData;

        BizComponents.TextCellType TextType;
        BizComponents.NumberCellType IntegerType;
        BizComponents.NumberCellType FloatType;
        BizComponents.DateTimeCellType DateType;

        public ConstantDataTableDialog()
        {
            InitializeComponent();

            TextType = new BizComponents.TextCellType();
            IntegerType = new BizComponents.NumberCellType();
            IntegerType.DecimalPlaces = 0;
            FloatType = new BizComponents.NumberCellType();
            DateType = new BizComponents.DateTimeCellType();
        }

        public ArrayTableData TableData
        {
            get
            {
                if (_ArrayData == null)
                    _ArrayData = new ArrayTableData();
                return _ArrayData;
            }
            set
            {
                _ArrayData = value;
                if (value != null)
                    TextBoxTableName.Text = value.GetTableText();
            }
        }

        private void ConstantDataTableDialog_Load(object sender, EventArgs e)
        {
            ShowArrayTableData();
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender == MenuItem_String)
            {
                EditColumnDialog Dialog = new EditColumnDialog();
                if (DialogResult.OK == Dialog.ShowDialog())
                {
                    try
                    {
                        TableData.AddStringColumn(Dialog.TextBoxColumn.Text);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (sender == MenuItem_Integer)
            {
                EditColumnDialog Dialog = new EditColumnDialog();
                if (DialogResult.OK == Dialog.ShowDialog())
                {
                    try
                    {
                        TableData.AddIntegerColumn(Dialog.TextBoxColumn.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (sender == MenuItem_Float)
            {
                EditColumnDialog Dialog = new EditColumnDialog();
                if (DialogResult.OK == Dialog.ShowDialog())
                {
                    try
                    {
                        TableData.AddFloatColumn(Dialog.TextBoxColumn.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (sender == MenuItem_Date)
            {
                EditColumnDialog Dialog = new EditColumnDialog();
                if (DialogResult.OK == Dialog.ShowDialog())
                {
                    try
                    {
                        TableData.AddDateColumn(Dialog.TextBoxColumn.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (sender == Button_RenameColumn)
            {
                String Column = FpSpread_Panel.ActiveColumn.Tag.ToString();
                EditColumnDialog Dialog = new EditColumnDialog();
                Dialog.TextBoxColumn.Text = Column;
                if (DialogResult.OK == Dialog.ShowDialog())
                {
                    try
                    {
                        TableData.EditColumn(Column, Dialog.TextBoxColumn.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (sender == Button_DeleteColumn)
            {
                String Column = FpSpread_Panel.ActiveColumn.Tag.ToString();
                if (DialogResult.OK == MessageBox.Show("你确定要删除列‘" + Column + "’吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                {
                    TableData.DeleteColumn(Column);
                }
            }
            else if (sender == Button_ModelField)
            {
                DataFieldSelector DataFieldSelector = new DataFieldSelector();
                Form Owner = Cache.CustomCache[SystemString.主窗口] as Form;
                DataFieldSelector.Location = Owner.PointToScreen(Owner.ClientRectangle.Location);
                DataFieldSelector.Size = Owner.ClientRectangle.Size;
                if (DialogResult.OK == DataFieldSelector.ShowDialog())
                {
                    try
                    {
                        DataTable Data = TableData.GetSchema();
                        List<String> ModelFields = DataFieldSelector.ModelFields;
                        List<CellRange> CellRanges = FpSpread.GetSelections(FpSpread_Panel);
                        if (ModelFields.Count > 0 && CellRanges.Count > 0)
                        {
                            CellRange Range = CellRanges[0];
                            for (int i = 0; i < Range.ColumnCount; i++)
                            {
                                if (i < ModelFields.Count)
                                {
                                    Data.Rows[Range.Row][Range.Column + i] = ModelFields[i];
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (sender == Button_ImportData)
            {
                openFileDialog1.Filter = "xml files (*.xml)|*.xml";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.InitialDirectory = Application.StartupPath;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (TextReader tr = new StreamReader(openFileDialog1.FileName))
                    {
                        object obj = BinarySerializer.Deserialize(tr.ReadToEnd());
                        if (obj is ArrayTableData)
                        {
                            TableData = obj as ArrayTableData;
                        }
                        else
                        {
                            MessageBox.Show("选择的文件的数据格式有错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            else if (sender == Button_ExportData)
            {
                saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.InitialDirectory = Application.StartupPath;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (TextWriter sw = new StreamWriter(saveFileDialog1.FileName))
                    {
                        sw.Write(BinarySerializer.Serialize(TableData));
                        sw.Close();
                    }
                }
            }

            ShowArrayTableData();
        }

        /// <summary>
        /// 添加新行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_AddRow_Click(object sender, EventArgs e)
        {
            int Index = FpSpread_Panel.ActiveRowIndex;
            if (Index >= 0)
            {
                TableData.AddRow(Index + 1);
                ShowArrayTableData();
                FpSpread_Panel.ActiveRowIndex = FpSpread_Panel.RowCount-1;
            }
        }

        /// <summary>
        /// 删除指定的行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_DelRow_Click(object sender, EventArgs e)
        {
            int Index = FpSpread_Panel.ActiveRowIndex;
            if (Index >= 0)
            {
                TableData.RemoveRow(Index);
                ShowArrayTableData();
            }
        }

        /// <summary>
        /// 显示数据表
        /// </summary>
        private void ShowArrayTableData()
        {
            DataTable Schema = TableData.GetSchema();

            //绑定数据时保证显示第一行，以免报错“System.ArgumentOutOfRangeException: Invalid low bound argument”
            if (FpSpread_Panel.Rows.Count > 0)
            {
                FpSpread.ShowRow(FpSpread.GetActiveRowViewportIndex(), 0, VerticalPosition.Top);
            }

            FpSpread_Panel.Reset();
            FpSpread_Panel.RowHeaderVisible = false;
            FpSpread_Panel.RowCount = Schema.Rows.Count;
            FpSpread_Panel.ColumnCount = Schema.Columns.Count;

            Graphics g = FpSpread.CreateGraphics();
            for (int k = 0; k < Schema.Columns.Count; k++)
            {
                FpSpread_Panel.Columns[k].VerticalAlignment = CellVerticalAlignment.Center;
                FpSpread_Panel.Columns[k].HorizontalAlignment = CellHorizontalAlignment.Center;

                FpSpread_Panel.Columns[k].Tag = Schema.Columns[k].ColumnName;

                FpSpread_Panel.ColumnHeader.Columns[k].Label = string.Concat(Schema.Columns[k].ColumnName, "(", TableData.GetDataTypeAbbr(k), ")");
                FpSpread_Panel.ColumnHeader.Columns[k].Width = g.MeasureString(FpSpread_Panel.ColumnHeader.Columns[k].Label, FpSpread.Font).Width + 20;

                if (Schema.Columns[k].DataType == typeof(string))
                {
                    FpSpread_Panel.Columns[k].CellType = TextType;
                }
                else if (Schema.Columns[k].DataType == typeof(long))
                {
                    FpSpread_Panel.Columns[k].CellType = IntegerType;
                }
                else if (Schema.Columns[k].DataType == typeof(double))
                {
                    FpSpread_Panel.Columns[k].CellType = FloatType;
                }
                else if (Schema.Columns[k].DataType == typeof(DateTime))
                {
                    FpSpread_Panel.Columns[k].CellType = DateType;
                }
            }
            g.Dispose();

            for (int j = 0; j < FpSpread_Panel.Columns.Count; j++)
            {
                for (int i = 0; i < FpSpread_Panel.Rows.Count; i++)
                {
                    if (FpSpread_Panel.Columns[j].Tag != null)
                    {
                        FpSpread_Panel.Cells[i, j].Value = Schema.Rows[i][FpSpread_Panel.Columns[j].Tag.ToString()];
                    }
                }
            }

            if (FpSpread_Panel.ColumnHeader.Columns.Count > 0)
                AutoColumnsWidth(FpSpread_Panel, 0, FpSpread_Panel.ColumnHeader.Columns.Count - 1, AutoSizeFlags.Contents, 150);
        }

        private void AutoColumnsWidth(SheetView Sheet, AutoSizeFlags flags, int Minimum)
        {
            AutoColumnsWidth(Sheet, 0, Sheet.ColumnHeader.Columns.Count - 1, flags, Minimum);
        }

        private void AutoColumnsWidth(SheetView Sheet, int startcolumn, int endcolumn, AutoSizeFlags flags, int Minimum)
        {
            int start = startcolumn < 0 ? 0 : startcolumn;
            int end = endcolumn > Sheet.ColumnHeader.Columns.Count ? Sheet.ColumnHeader.Columns.Count - 1 : endcolumn;

            int temp;
            if (start > end)
            {
                temp = start;
                end = temp;
                start = end;
            }

            Graphics g = FpSpread.CreateGraphics();
            if (flags == AutoSizeFlags.Header)
            {
                for (int i = start; i <= end; i++)
                {
                    SizeF sizef = g.MeasureString(Sheet.ColumnHeader.Columns[i].Label, FpSpread.Font);
                    float WideWidth = (sizef.Width + 50 < Minimum ? Minimum : sizef.Width + 50);
                    Sheet.ColumnHeader.Columns[i].Width = WideWidth;
                }
            }
            else
            {
                for (int i = start; i <= end; i++)
                {
                    float Width = 0;
                    for (int j = 0; j < Sheet.Rows.Count; j++)
                    {
                        SizeF TextSize = g.MeasureString(Sheet.Cells[j, i].Text, FpSpread.Font);

                        if (TextSize.Width > Width)
                        {
                            Width = TextSize.Width;
                        }
                    }

                    float WideWidth = (Width < Minimum ? Minimum : Width);
                    Sheet.ColumnHeader.Columns[i].Width = WideWidth;
                }
            }
            g.Dispose();
        }

        private void FpSpread_EditModeOff(object sender, EventArgs e)
        {
            if (FpSpread_Panel.ActiveCell.Value == null)
                FpSpread_Panel.ActiveCell.Value = DBNull.Value;

            if (FpSpread_Panel.ActiveRowIndex >= 0 && FpSpread_Panel.ActiveColumnIndex >= 0)
            {
                DataRow Row = TableData.GetSchema().Rows[FpSpread_Panel.ActiveRowIndex];
                Row[FpSpread_Panel.ActiveColumnIndex] = FpSpread_Panel.ActiveCell.Value;
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxTableName.Text.Trim()))
            {
                TextBoxTableName.SelectAll();
                TextBoxTableName.Focus();
                return;
            }

            TableData.SetTableName(TextBoxTableName.Text);
            TableData.SetTableText(TextBoxTableName.Text);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
