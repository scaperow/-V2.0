using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Bases;
using FarPoint.Win;
using BizCommon;
using FarPoint.Win.Spread;
using BizComponents;

namespace ReportComponents
{
    public partial class DataFieldSelector : Form
    {
        ModuleConfiguration modelInfo;
        String modelIndex;
        String modelText;
        List<String> modelFields = new List<string>();

        public DataFieldSelector()
        {
            InitializeComponent();
        }

        public String ModelIndex
        {
            get
            {
                return modelIndex;
            }
        }

        public String ModelText
        {
            get
            {
                return modelText;
            }
        }

        public List<String> ModelFields
        {
            get
            {
                return modelFields;
            }
        }

        private void DataFieldSelector_Load(object sender, EventArgs e)
        {
            DepositoryResourceCatlog.InitModuleCatlog(ModelView);
            ModelView.SelectedNode = ModelView.TopNode;

            label2.BackColor = Color.LightPink;

            FpSpread.CellClick += new CellClickEventHandler(FpSpread_CellClick);
        }

        private void ModelView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ProgressScreen.Current.ShowSplashScreen();
            this.AddOwnedForm(ProgressScreen.Current);

            modelInfo = DepositoryModuleConfiguration.InitModuleConfiguration(e.Node.Name);

            FpSpread.Sheets.Clear();
            foreach (SheetConfiguration Sheet in modelInfo.Sheets)
            {
                if (Sheet == null)
                    continue;

                ProgressScreen.Current.SetStatus = "正在初始化表单‘" + Sheet.Description + "’";

                SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), Sheet.SheetStyle, "SheetView") as SheetView;
                SheetView.Tag = Sheet;
                SheetView.SheetName = Sheet.Description;
                SheetView.OperationMode = OperationMode.ReadOnly;
                FpSpread.Sheets.Add(SheetView);

                if (Sheet.DataTableSchema.Schema == null)
                    continue;

                foreach (FieldDefineInfo field in Sheet.DataTableSchema.Schema.FieldInfos)
                {
                    SheetView.Cells[field.RangeInfo].BackColor = Color.LightPink;
                    SheetView.Cells[field.RangeInfo].Tag = field;
                }
            }

            //将表外字段的数据表显示在当前窗口中
            TableDefineInfo Info = modelInfo.ExtentDataSchema;
            SheetView ExtentSheet = new SheetView("系统表（含表外字段）");
            ExtentSheet.OperationMode = OperationMode.RowMode;
            ExtentSheet.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            ExtentSheet.Protect = true;
            ExtentSheet.ColumnCount = 3;
            ExtentSheet.Columns[0].Label = "字段名称";
            ExtentSheet.Columns[0].Width = 100;
            ExtentSheet.Columns[0].Locked = true;
            ExtentSheet.Columns[0].HorizontalAlignment = CellHorizontalAlignment.Center;
            ExtentSheet.Columns[0].VerticalAlignment = CellVerticalAlignment.Center;
            ExtentSheet.Columns[1].Label = "字段描述";
            ExtentSheet.Columns[1].Width = 100;
            ExtentSheet.Columns[1].Locked = true;
            ExtentSheet.Columns[1].HorizontalAlignment = CellHorizontalAlignment.Center;
            ExtentSheet.Columns[1].VerticalAlignment = CellVerticalAlignment.Center;
            ExtentSheet.Columns[2].Label = "数据类型";
            ExtentSheet.Columns[2].Width = 100;
            ExtentSheet.Columns[2].Locked = true;
            ExtentSheet.Columns[2].HorizontalAlignment = CellHorizontalAlignment.Center;
            ExtentSheet.Columns[2].VerticalAlignment = CellVerticalAlignment.Center;

            ExtentSheet.RowCount = Info.FieldInfos.Count;
            foreach (FieldDefineInfo FieldInfo in Info.FieldInfos)
            {
                int index = Info.FieldInfos.IndexOf(FieldInfo);
                ExtentSheet.Cells[index, 0].Text = FieldInfo.FieldName;
                ExtentSheet.Cells[index, 1].Text = FieldInfo.Description;
                ExtentSheet.Cells[index, 2].Text = FieldInfo.FieldType.DisplayType;
            }

            ExtentSheet.Tag = Info.Name;
            FpSpread.Sheets.Add(ExtentSheet);

            this.RemoveOwnedForm(ProgressScreen.Current);
            ProgressScreen.Current.CloseSplashScreen();
            Activate();
        }

        void FpSpread_CellClick(object sender, CellClickEventArgs e)
        {
            FpSpread FpSpread = sender as FpSpread;
            if (e.Row >= 0 && e.Row < FpSpread.ActiveSheet.RowCount && e.Column >= 0 && e.Column < FpSpread.ActiveSheet.ColumnCount)
            {
                if (!FpSpread.ActiveSheet.SheetName.StartsWith("系统表"))
                {
                    Cell Cell = FpSpread.ActiveSheet.Cells[e.Row, e.Column];
                    if (Cell.BackColor == Color.LightPink)
                        Cell.BackColor = Color.Blue;
                    else if (Cell.BackColor == Color.Blue)
                        Cell.BackColor = Color.LightPink;
                }
                else
                {
                    Row Row = FpSpread.ActiveSheet.Rows[e.Row];
                    if (Row.BackColor.ToArgb() == 0)
                        Row.BackColor = Color.Blue;
                    else if (Row.BackColor == Color.Blue)
                        Row.BackColor = Color.FromArgb(0);
                }
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (modelInfo == null)
                return;

            modelIndex = modelInfo.Index;
            modelText = modelInfo.Description;

            ModelFields.Clear();

            if (CheckBox_ModelIndex.Checked)
            {
                ModelFields.Add(string.Concat("'", modelIndex, "'"));
            }
            if (CheckBox_ModelName.Checked)
            {
                ModelFields.Add(string.Concat("'", modelText, "'"));
            }
            if (CheckBox_SCTS.Checked)
            {
                ModelFields.Add(string.Concat("biz_norm_extent_", modelIndex, ".SCTS"));
            }
            if (CheckBox_SCPT.Checked)
            {
                ModelFields.Add(string.Concat("biz_norm_extent_", modelIndex, ".SCPT"));
            }
            if (CheckBox_SCCT.Checked)
            {
                ModelFields.Add(string.Concat("biz_norm_extent_", modelIndex, ".SCCT"));
            }

            foreach (SheetView Sheet in FpSpread.Sheets)
            {
                if (Sheet.Tag is SheetConfiguration)
                {
                    SheetConfiguration sheetConfiguration = Sheet.Tag as SheetConfiguration;
                    TableDefineInfo TableSchema = sheetConfiguration.DataTableSchema.Schema;
                    if (TableSchema != null)
                    {
                        foreach (FieldDefineInfo FieldInfo in TableSchema.FieldInfos)
                        {
                            if (Sheet.Cells[FieldInfo.RangeInfo].BackColor == Color.Blue)
                            {
                                ModelFields.Add(string.Concat("[",TableSchema.Name, "].[", FieldInfo.FieldName,"]"));
                            }
                        }
                    }
                }
                else
                {
                    foreach (Row Row in Sheet.Rows)
                    {
                        if (Row.BackColor == Color.Blue)
                        {
                            ModelFields.Add(string.Concat("[", Sheet.Tag.ToString(), "].[", Sheet.Cells[Row.Index,0].Text, "]"));
                        }
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
