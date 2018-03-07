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
using FarPoint.Win;

namespace BizComponents
{
    public partial class LogDialog : Form
    {
        Guid moduleID;
        Guid dataID;
        String optType;
        String modifyItem;
        String modifyBy;
        String modifyDate;
        Int64 logID;
        private Font defaultFont = new Font("宋体", 9f);

        public LogDialog(Int64 logID)
        {
            InitializeComponent();
            this.logID = logID;

            this.Disposed += new EventHandler(DataDialog_Disposed);
        }

        //清理释放的托管堆中的内存
        void DataDialog_Disposed(object sender, EventArgs e)
        {
            GC.Collect(3, GCCollectionMode.Forced);
        }

        private FpSpread FpSpread
        {
            get
            {
                return fpSpreadViewer1.FpSpread;
            }
        }

        private SheetView ActiveSheet
        {
            get
            {
                return FpSpread.ActiveSheet;
            }
        }

        Boolean _ReadOnly = true;
        public Boolean ReadOnly
        {
            get
            {
                return _ReadOnly;
            }
            set
            {
                _ReadOnly = value;
            }
        }

        private void LogDialog_Load(object sender, EventArgs e)
        {
            ProgressScreen.Current.ShowSplashScreen();
            this.AddOwnedForm(ProgressScreen.Current);
            InitLogCategoryInfo();
            Dictionary<Guid, SheetView> SheetCollection = new Dictionary<Guid, SheetView>();

            try
            {
                List<FarPoint.CalcEngine.FunctionInfo> Infos = FunctionItemInfoUtil.getFunctionItemInfos();
                FpSpread.Sheets.Clear();
                if (dataID == Guid.Empty)
                {
                    return;
                }
                JZDocument document = DocumentHelperClient.GetDocumentByID(dataID);
                JZDocument defaultDocument = ModuleHelperClient.GetDefaultDocument(moduleID);
                
                List<JZFormulaData> CrossSheetFormulaInfos = ModuleHelperClient.GetFormulaByModuleIndex(moduleID);

                foreach (JZSheet sheet in defaultDocument.Sheets)
                {
                    ProgressScreen.Current.SetStatus = "正在初始化表单‘" + sheet.Name + "’";
                    String sheetXML = ModuleHelperClient.GetSheetXMLByID(sheet.ID);
                    SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), sheetXML, "SheetView") as SheetView;
                    SheetView.Tag = sheet.ID;
                    SheetView.SheetName = sheet.Name;
                    SheetView.Cells[0, 0].Value = "";
                    SheetView.Protect = true;
                    FpSpread.Sheets.Add(SheetView);

                    SheetCollection.Add(sheet.ID, SheetView);
                    foreach (FarPoint.CalcEngine.FunctionInfo Info in Infos)
                    {
                        SheetView.AddCustomFunction(Info);
                    }
                    foreach (JZCell dataCellDefault in sheet.Cells)
                    {
                        Cell cell = SheetView.Cells[dataCellDefault.Name];
                        Object value = JZCommonHelper.GetCellValue(document, sheet.ID, dataCellDefault.Name);
                        Boolean hasValue = true;
                        if (value == null || value.ToString() == "")
                        {
                            hasValue = false;
                        }
                        if (cell != null)
                        {
                            cell.Font = defaultFont;
                            if (cell.CellType is DownListCellType)
                            {
                                DownListCellType CellType = cell.CellType as DownListCellType;
                                CellType.DropDownButton = false;
                                CellType.DesignMode = false;
                                cell.Value = value;
                            }
                            else if (cell.CellType is TextCellType)
                            {
                                TextCellType CellType = cell.CellType as TextCellType;
                                if (CellType.FieldType.Description == FieldType.Text.Description)
                                {
                                    CellType.Multiline = true;
                                    CellType.WordWrap = true;
                                }
                                CellType.MaxLength = CellType.FieldType.Length;
                                if (hasValue)
                                {
                                    cell.Value = value.ToString().Trim('\r', '\n'); ;
                                }
                            }
                            else if (cell.CellType is LongTextCellType)
                            {
                                LongTextCellType CellType = cell.CellType as LongTextCellType;
                                if (CellType.FieldType.Description == FieldType.LongText.Description)
                                {
                                    CellType.Multiline = true;
                                    CellType.WordWrap = true;
                                }
                                CellType.MaxLength = CellType.FieldType.Length;
                                if (hasValue)
                                {
                                    cell.Value = value.ToString().Trim('\r', '\n'); ;
                                }
                            }
                            else if (cell.CellType is DateTimeCellType)
                            {
                                DateTimeCellType CellType = cell.CellType as DateTimeCellType;
                                CellType.MinimumDate = new DateTime(1753, 1, 1);
                                CellType.MaximumDate = new DateTime(9999, 12, 31);
                                cell.Value = value;
                            }
                            else if (cell.CellType is RichTextCellType)
                            {
                                RichTextCellType CellType = cell.CellType as RichTextCellType;
                                CellType.Multiline = false;
                                CellType.WordWrap = false;
                                CellType.MaxLength = CellType.FieldType.Length;
                                if (hasValue)
                                {
                                    cell.Value = value.ToString().Trim('\r', '\n'); ;
                                }
                            }
                            else if (cell.CellType is NumberCellType)
                            {
                                NumberCellType CellType = cell.CellType as NumberCellType;
                                CellType.MaximumValue = 999999999.9999;
                                CellType.MinimumValue = -999999999.9999;
                                cell.Value = value;
                            }
                            else if (cell.CellType is MaskCellType)
                            {
                                MaskCellType CellType = cell.CellType as MaskCellType;
                                CellType.Mask = "00000000000000000000000000000000000";
                                if (CellType.CustomMaskCharacters != null && CellType.CustomMaskCharacters.Length > 0)
                                {
                                    CellType.CustomMaskCharacters[0] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-()（）复检";
                                }
                                cell.Value = value;
                            }
                            else if (cell.CellType is ImageCellType)
                            {
                                if (value != null)
                                {
                                    cell.Value = JZCommonHelper.StringToBitmap(value.ToString());
                                }
                            }
                            else
                            {
                                cell.Value = value;
                            }
                        }
                    }
                }

                ProgressScreen.Current.SetStatus = "正在初始化跨表公式...";

                foreach (JZFormulaData formula in CrossSheetFormulaInfos)
                {
                    SheetView Sheet = SheetCollection[formula.SheetIndex];
                    try
                    {
                        Sheet.Cells[formula.RowIndex, formula.ColumnIndex].Formula = formula.Formula;
                    }
                    catch
                    {
                       
                    }
                }

                FpSpread.LoadFormulas(true);

                ProgressScreen.Current.SetStatus = "正在显示资料...";

                UpdateChart();
                UpdateEquation();
                SetNotes();
                //设置只读模式
                if (ReadOnly)
                {
                    foreach (SheetView sheet in FpSpread.Sheets)
                    {
                        sheet.OperationMode = OperationMode.ReadOnly;
                    }
                }
            }
            catch (TimeoutException ex1)
            {
                MessageBox.Show("网络原因造成数据无法访问，请检查本机网络连接，或稍后再试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载试验模板出错！\r\n原因：" + (ex.InnerException != null ? ex.InnerException.Message : ex.Message), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                try
                {
                    this.RemoveOwnedForm(ProgressScreen.Current);
                    ProgressScreen.Current.CloseSplashScreen();
                    this.Activate();
                }
                catch (Exception ex1) { }
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitLogCategoryInfo()
        {

            DataTable tb = LoginDataList.GetOperateBaseInfo(logID);
            if (tb != null && tb.Rows.Count == 1)
            {
                dataID = new Guid(tb.Rows[0]["dataID"].ToString());
                moduleID = new Guid(tb.Rows[0]["ModuleID"].ToString());
                optType = tb.Rows[0]["optType"].ToString();
                modifyItem = tb.Rows[0]["modifyItem"].ToString();
                modifyBy = tb.Rows[0]["modifiedby"].ToString();
                modifyDate = tb.Rows[0]["modifiedDate"].ToString();
            }
        }

        /// <summary>
        /// 在cell中添加变更注释
        /// </summary>
        private void SetNotes()
        {
            List<JZModifyItem> modifyItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZModifyItem>>(modifyItem);
            if (modifyItemList != null)
            {
                foreach (var item in modifyItemList)
                {
                    String note = modifyBy + "于" + modifyDate + "将此处内容从\"" + item.OriginalValue +
                        "\"改为\"" + item.CurrentValue + "\"";

                    foreach (SheetView view in FpSpread.Sheets)
                    {
                        if (new Guid(view.Tag.ToString()) == item.SheetID)
                        {
                            CellNote.SetNoteInformation(view.Cells[item.CellPosition], note);
                            break;
                        }
                    }
                }
            }
        }

        void UpdateChart()
        {
            foreach (SheetView Sheet in FpSpread.Sheets)
            {
                //支持嵌入的图表
                int RowCount = Sheet.GetLastNonEmptyRow(NonEmptyItemFlag.Style);
                int ColumnCount = Sheet.GetLastNonEmptyColumn(NonEmptyItemFlag.Style);
                for (int i = 0; i <= RowCount; i++)
                {
                    for (int j = 0; j <= ColumnCount; j++)
                    {
                        if (Sheet.Cells[i, j].CellType is ChartCellType)
                        {
                            ChartCellType ChartType = Sheet.Cells[i, j].CellType as ChartCellType;
                            Rectangle r = FpSpread.GetCellRectangle(0, 0, i, j);
                            ChartType.ChartSize = r.Size;
                            ChartType.ActiveSheet = Sheet;
                            ChartType.UpdateChart();
                            Sheet.Cells[i, j].Invalidate();
                        }
                    }
                }

                //支持浮动的图标
                foreach (IElement Element in Sheet.DrawingContainer.ContainedObjects)
                {
                    if (Element is ChartShape)
                    {
                        ChartShape Shape = Element as ChartShape;
                        Shape.ActiveSheet = Sheet;
                        Shape.Locked = true;
                        Shape.UpdateChart();
                    }
                }
            }
        }

        public void UpdateEquation()
        {
            foreach (SheetView Sheet in FpSpread.Sheets)
            {
                //支持浮动的公式
                foreach (IElement Element in Sheet.DrawingContainer.ContainedObjects)
                {
                    if (Element is EquationShape)
                    {
                        EquationShape Shape = Element as EquationShape;
                        Shape.Locked = true;
                    }
                }
            }
        }
    }
}
