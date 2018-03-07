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
using Yqun.Client.BizUI;

namespace BizComponents
{
    public partial class DataDialog : Form
    {
        Guid DataID;
        Guid ModuleID;
        private Font defaultFont = new Font("宋体", 9f);

        public DataDialog(Guid dataID, Guid moduleID)
        {
            InitializeComponent();
            this.DataID = dataID;
            this.ModuleID = moduleID;
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

        private void DataDialog_Load(object sender, EventArgs e)
        {

            ProgressScreen.Current.ShowSplashScreen();
            this.AddOwnedForm(ProgressScreen.Current);

            Dictionary<Guid, SheetView> SheetCollection = new Dictionary<Guid, SheetView>();
            JZDocument document = DocumentHelperClient.GetDocumentByID(DataID);
            JZDocument defaultDocument = ModuleHelperClient.GetDefaultDocument(ModuleID);
            List<JZFormulaData> CrossSheetFormulaInfos = ModuleHelperClient.GetLineFormulaByModuleIndex(ModuleID);
            FpSpread.Sheets.Clear();
            LoadSpread(FpSpread, ModuleID);
            #region 初始化表单
            foreach (JZSheet sheet in defaultDocument.Sheets)
            {
                SheetView SheetView = GetSheet(sheet.ID);
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
                        #region 处理单元格
                        cell.Locked = false;
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
                            else
                            {
                                cell.Value = value;
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
                            else
                            {
                                cell.Value = value;
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
                                cell.Value = value.ToString().Trim('\r', '\n');
                            }
                            else
                            {
                                cell.Value = value;
                            }
                        }
                        else if (cell.CellType is DeleteLineCellType)
                        {//删除线
                            DeleteLineCellType CellType = cell.CellType as DeleteLineCellType;
                            CellType.Multiline = true;
                            CellType.WordWrap = true;
                            CellType.MaxLength = CellType.FieldType.Length;
                            cell.CellType = CellType;
                            object objOld = cell.Text;
                            if (hasValue)
                            {
                                cell.Value = new System.Text.RegularExpressions.Regex("'+").Replace(value.ToString(), "'"); //value.ToString();
                                if (string.IsNullOrEmpty(cell.Text))
                                {
                                    RichTextBox rt = new RichTextBox();
                                    rt.Text = objOld == null ? "" : objOld.ToString();
                                    rt.Font = new Font("宋体", 10.5f, FontStyle.Regular);
                                    cell.Value = rt.Rtf;
                                }
                            }
                            else
                            {
                                RichTextBox rt = new RichTextBox();
                                rt.Text = objOld == null ? "" : objOld.ToString();
                                rt.Font = new Font("宋体", 10.5f, FontStyle.Regular);
                                cell.Value = rt.Rtf;
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
                            for (int i = CellType.Mask.Length; i < CellType.FieldType.Length; i++)
                            {
                                CellType.Mask += "0";
                            }


                            if (CellType.CustomMaskCharacters != null && CellType.CustomMaskCharacters.Length > 0)
                            {
                                CellType.CustomMaskCharacters[0] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-()ⅠⅡⅢⅣⅤⅥⅦⅧⅨⅩ（）复检";
                            }
                            if (value == null || value.ToString().Trim() == "")
                            {
                                cell.Value = null;
                            }
                            else
                            {
                                cell.Value = value.ToString().Trim();
                            }
                        }
                        else if (cell.CellType is ImageCellType)
                        {
                            if (value != null)
                            {
                                cell.Value = JZCommonHelper.StringToBitmap(value.ToString());
                            }
                            else
                            {
                                cell.Value = null;
                            }
                        }
                        else if (cell.CellType is HyperLinkCellType)
                        {
                            if (value != null)
                            {
                                List<string> lstLink = new List<string>();
                                HyperLinkCellType hlnkCell = new HyperLinkCellType();
                                try
                                {
                                    lstLink = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(value.ToString());
                                    hlnkCell.Text = lstLink[0];
                                    hlnkCell.Link = lstLink[1];
                                }
                                catch
                                {
                                    hlnkCell.Text = value.ToString();
                                    hlnkCell.Link = "";
                                }

                                cell.CellType = hlnkCell;
                                //cell.Value = value;
                            }
                            else
                            {
                                HyperLinkCellType hlnkCell = new HyperLinkCellType();
                                cell.CellType = hlnkCell;
                            }
                        }
                        else
                        {
                            cell.Value = value;
                        }
                        #endregion
                        JZCellProperty p = cell.Tag as JZCellProperty;
                        #region 处理单元格属性
                        if (p != null)
                        {
                            cell.Locked = false;
                        }
                        else
                        {
                            //  logger.Error("未能设置数据区信息：单元格" + dataCellDefault.Name + "，表单：" + sheet.Name);
                        }
                        #endregion
                    }
                }
                #region 线路单元格样式
                DataTable dtCellStyle = ModuleHelperClient.GetCellStyleBySheetID(sheet.ID);
                for (int i = 0; i < dtCellStyle.Rows.Count; i++)
                {
                    if (dtCellStyle.Rows[i]["CellStyle"] != null)
                    {
                        JZCellStyle CurrentCellStyle = Newtonsoft.Json.JsonConvert.DeserializeObject<JZCellStyle>(dtCellStyle.Rows[i]["CellStyle"].ToString());
                        if (CurrentCellStyle != null)
                        {
                            string strCellName = dtCellStyle.Rows[i]["CellName"].ToString();
                            Cell cell = SheetView.Cells[strCellName];
                            cell.ForeColor = CurrentCellStyle.ForColor;
                            cell.BackColor = CurrentCellStyle.BackColor;
                            cell.Font = new Font(CurrentCellStyle.FamilyName, CurrentCellStyle.FontSize, CurrentCellStyle.FontStyle);
                        }
                    }
                }
                #endregion
            }
            #endregion

            UpdateChart();
            UpdateEquation();

            //设置只读模式
            if (ReadOnly)
            {
                foreach (SheetView sheet in FpSpread.Sheets)
                {
                    sheet.OperationMode = OperationMode.ReadOnly;
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

        public SheetView GetSheet(Guid sheetID)
        {
            foreach (SheetView sheet in FpSpread.Sheets)
            {
                if (sheetID == new Guid(sheet.Tag.ToString()))
                {
                    return sheet;
                }
            }

            return null;
        }

        private FpSpread LoadSpread(FpSpread spread, Guid moduleID)
        {
            try
            {
                ProgressScreen.Current.ShowSplashScreen();
                ProgressScreen.Current.SetStatus = "正在打开模板...";
                //增加水印、图章
                //spread.Watermark = ModuleHelperClient.GetWatermarkByModuleID(moduleID);
                Dictionary<Guid, SheetView> SheetCollection = new Dictionary<Guid, SheetView>();
                List<FarPoint.CalcEngine.FunctionInfo> Infos = FunctionItemInfoUtil.getFunctionItemInfos();
                JZDocument defaultDocument = ModuleHelperClient.GetDefaultDocument(moduleID);
                List<JZFormulaData> CrossSheetLineFormulaInfos = ModuleHelperClient.GetLineFormulaByModuleIndex(moduleID);

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

                    DataTable dtCellStyle = ModuleHelperClient.GetCellStyleBySheetID(sheet.ID);
                    for (int i = 0; i < dtCellStyle.Rows.Count; i++)
                    {
                        if (dtCellStyle.Rows[i]["CellStyle"] != null)
                        {
                            JZCellStyle CurrentCellStyle = Newtonsoft.Json.JsonConvert.DeserializeObject<JZCellStyle>(dtCellStyle.Rows[i]["CellStyle"].ToString());
                            if (CurrentCellStyle != null)
                            {
                                string strCellName = dtCellStyle.Rows[i]["CellName"].ToString();
                                Cell cell = SheetView.Cells[strCellName];
                                cell.ForeColor = CurrentCellStyle.ForColor;
                                cell.BackColor = CurrentCellStyle.BackColor;
                                cell.Font = new Font(CurrentCellStyle.FamilyName, CurrentCellStyle.FontSize, CurrentCellStyle.FontStyle);
                            }
                        }
                    }

                    SheetCollection.Add(sheet.ID, SheetView);
                    foreach (FarPoint.CalcEngine.FunctionInfo Info in Infos)
                    {
                        SheetView.AddCustomFunction(Info);
                    }
                }
                ProgressScreen.Current.SetStatus = "正在初始化跨表公式...";
                spread.LoadFormulas(false);
                foreach (JZFormulaData formula in CrossSheetLineFormulaInfos)
                {
                    if (!SheetCollection.ContainsKey(formula.SheetIndex))
                    {
                        continue;
                    }
                    SheetView Sheet = SheetCollection[formula.SheetIndex];

                    Cell cell = Sheet.Cells[formula.RowIndex, formula.ColumnIndex];
                    if (cell != null)
                    {
                        try
                        {
                            if (formula.Formula.ToUpper().Trim() == "NA()")
                            {
                                cell.Formula = "";
                            }
                            else
                            {
                                cell.Formula = formula.Formula;
                            }
                        }
                        catch
                        {
                        }
                    }
                }

                spread.LoadFormulas(true);
                ProgressScreen.Current.SetStatus = "正在显示资料...";

                return spread;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                try
                {
                    //this.RemoveOwnedForm(ProgressScreen.Current);
                    ProgressScreen.Current.CloseSplashScreen();
                }
                catch
                {
                }
            }

            return null;
        }
    }
}
