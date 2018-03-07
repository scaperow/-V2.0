using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using BizComponents;
using FarPoint.Win;
using Yqun.Common.Encoder;
using BizCommon;
using Yqun.Bases;
using Yqun.Permissions.Common;
using System.Text.RegularExpressions;
using FarPoint.Win.Spread.Model;
using System.IO;
using System.Drawing.Imaging;
using Yqun.Client.BizUI;

namespace BizModules
{
    public partial class ModuleViewer : Form
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Dictionary<String, SheetView> Table_Sheet;

        private Guid moduleIndex = Guid.Empty;
        private Font defaultFont = new Font("宋体", 9f);
        private bool Saved = true;
        private JZDocument document = null;
        private JZDocument defaultDocument = null;
        private Guid documentID = Guid.Empty;
        private String testRoomCode = "";
        private Boolean askChanging = false;
        private DateTime? LastEditedTime = null;
        public Boolean isNewData = false;
        public DataViewControl viewControl = null;
        public JZStadiumConfig StadiumConfig = null;


        public ModuleViewer(Guid _documentID, Guid _moduleIndex, String testRoomCode)
        {
            documentID = _documentID;
            moduleIndex = _moduleIndex;
            this.testRoomCode = testRoomCode;
            InitializeComponent();

            FpSpread.EditMode = true;
            FpSpread.EditModeOn += new EventHandler(FpSpread_EditModeOn);
            FpSpread.EditModeOff += new EventHandler(FpSpread_EditModeOff);
            FpSpread.EditChange += new EditorNotifyEventHandler(FpSpread_EditChange);
            FpSpread.MouseUp += new MouseEventHandler(FpSpread_MouseUp);
            this.Disposed += new EventHandler(ModuleDesinger_Disposed);
            FpSpread.ClipboardPasting += new ClipboardPastingEventHandler(FpSpread_ClipboardPasting);
            Table_Sheet = new Dictionary<string, SheetView>();
        }


        void FpSpread_EditChange(object sender, EditorNotifyEventArgs e)
        {
            Saved = false;
        }

        void FpSpread_EditModeOn(object sender, EventArgs e)
        {
            FpSpread.IsEditing = true;
        }

        void FpSpread_EditModeOff(object sender, EventArgs e)
        {
            FpSpread.IsEditing = false;
        }

        void FpSpread_ClipboardPasting(object sender, ClipboardPastingEventArgs e)
        {
            try
            {
                int ColumnIndex = fpSpreadViewer1.ActiveSheet.ActiveColumnIndex;
                int Rowindex = fpSpreadViewer1.ActiveSheet.ActiveRowIndex;
                string[] tempRows = Regex.Split(Clipboard.GetText(), "\r\n", RegexOptions.IgnoreCase);
                Int32 tC = 0;
                if (tempRows.Length == 1)
                {
                    tC = 1;
                }
                else if (tempRows.Length > 1)
                {
                    tC = tempRows.Length - 1;
                }

                for (int i = 0; i < tC; i++)
                {
                    string[] tempColumns = tempRows[i].Split('\t');
                    Int32 rowSpan = 1;
                    for (int j = 0; j < tempColumns.Length; j++)
                    {
                        Cell cell = fpSpreadViewer1.ActiveSheet.Cells[Rowindex, ColumnIndex + j];
                        if (cell == null)
                        {
                            continue;
                        }
                        if (cell.Locked)
                        {
                            continue;
                        }
                        if (cell.CellType != null && cell.CellType.ToString() == "下拉框")
                        {
                            continue;
                        }
                        String value = tempColumns[j];
                        Saved = false;
                        if (!string.IsNullOrEmpty(value))
                        {
                            cell.Value = value;
                        }
                        else
                        {
                            cell.Value = null;
                        }
                        rowSpan = rowSpan > cell.RowSpan ? rowSpan : cell.RowSpan;
                    }
                    Rowindex++;
                }
                e.Handled = true;
            }
            catch
            {
            }
        }

        //清理释放的托管堆中的内存
        void ModuleDesinger_Disposed(object sender, EventArgs e)
        {
            GC.Collect(3, GCCollectionMode.Forced);
        }

        internal Boolean MainToolStripVisible
        {
            get
            {
                return toolStrip1.Visible;
            }
            set
            {
                toolStrip1.Visible = value;
            }
        }

        internal MyCell FpSpread
        {
            get
            {
                return fpSpreadViewer1.FpSpread;
            }
        }

        internal SheetView ActiveSheet
        {
            get
            {
                return FpSpread.ActiveSheet;
            }
        }

        Boolean _ReadOnly = false;
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

        String _TryType = "";
        public String TryType
        {
            get
            {
                return _TryType;
            }
            set
            {
                _TryType = value;
            }
        }

        private void ModuleViewer_Load(object sender, EventArgs e)
        {
            if (JZApplicationCatch.CurrentModule == null)
            {
                JZApplicationCatch.CurrentModule = new JZModuleCatch();
            }
            JZApplicationCatch.CurrentModule.ModuleID = moduleIndex;
            ProgressScreen.Current.ShowSplashScreen();
            //Modified by Tan In 20141016 西安和谐预览水印不出现
            fpSpreadViewer1.FpSpread.Watermark = JZApplicationCatch.CurrentModule.FpSpread.Watermark;

            this.AddOwnedForm(ProgressScreen.Current);
            FpSpread.Sheets.Clear();
            SponsorModifyButton.Visible = (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_施工单位"
                || Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator);
            toolStripSeparator1.Visible = SponsorModifyButton.Visible;
            ProgressScreen.Current.SetStatus = "正在显示资料...";
            Dictionary<Guid, SheetView> SheetCollection = new Dictionary<Guid, SheetView>();
            bool isError = false;
            try
            {
                Boolean isSameTestRoom = true;
                Boolean isAskChanging = true;
                defaultDocument = ModuleHelperClient.GetDefaultDocument(moduleIndex);
                StadiumConfig = ModuleHelperClient.GetStadiumCinfigByModuleID(moduleIndex);
                if (StadiumConfig != null)
                {
                    TemperatureListButton.Visible = SelectTemperatureTypeButton.Visible = StadiumConfig.Temperature > 0 ? true : false;
                }
                else
                {
                    TemperatureListButton.Visible = SelectTemperatureTypeButton.Visible = false;
                }
                if (documentID == Guid.Empty)
                {
                    document = defaultDocument;
                }
                else
                {
                    document = DocumentHelperClient.GetDocumentByID(documentID);
                    isSameTestRoom = testRoomCode == Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code;
                    Sys_Document doc = DocumentHelperClient.GetDocumentBaseInfoByID(documentID);
                    if (doc != null)
                    {
                        isAskChanging = doc.Status == 2;
                        askChanging = isAskChanging;
                        LastEditedTime = doc.LastEditedTime;
                    }
                }
                #region 初始化表单
                foreach (JZSheet sheet in defaultDocument.Sheets)
                {
                    SheetView SheetView = JZApplicationCatch.CurrentModule.GetSheet(sheet.ID);
                    FpSpread.Sheets.Add(SheetView);
                    foreach (JZCell dataCellDefault in sheet.Cells)
                    {
                        Cell cell = SheetView.Cells[dataCellDefault.Name];
                        Boolean hasValue = true;
                        bool hasCell = true;
                        Object value = JZCommonHelper.GetCellValueAndHasCell(document, sheet.ID, dataCellDefault.Name, out hasCell);
                        if (value == null || value.ToString() == "")
                        {
                            hasValue = false;
                        }
                        if (cell != null)
                        {
                            #region 处理单元格
                            cell.Locked = false;
                            if (cell.CellType is DownListCellType)
                            {
                                //cell.Font = defaultFont;
                                #region DownListCellType
                                DownListCellType CellType = cell.CellType as DownListCellType;
                                CellType.DropDownButton = false;
                                CellType.DesignMode = false;
                                CellType.ReferenceFinished += new EventHandler<ReferenceEventArgs>(DownListCellType_ReferenceFinished);
                                cell.Value = value;
                                #endregion

                            }
                            else if (cell.CellType is TextCellType)
                            {
                                //cell.Font = defaultFont;
                                #region TextCellType
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
                                    if (hasCell)
                                        cell.Value = value;
                                }
                                #endregion
                            }
                            else if (cell.CellType is LongTextCellType)
                            {
                                //cell.Font = defaultFont;
                                #region LongTextCellType
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
                                #endregion
                            }
                            else if (cell.CellType is DateTimeCellType)
                            {
                                //cell.Font = defaultFont;
                                DateTimeCellType CellType = cell.CellType as DateTimeCellType;
                                CellType.MinimumDate = new DateTime(1753, 1, 1);
                                CellType.MaximumDate = new DateTime(9999, 12, 31);
                                cell.Value = value;
                            }
                            else if (cell.CellType is RichTextCellType)
                            {
                                //cell.Font = defaultFont;
                                #region RichTextCellType
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
                                #endregion
                            }
                            else if (cell.CellType is DeleteLineCellType)
                            {
                                //cell.Font = defaultFont;
                                #region 删除线
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
                                #endregion
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
                                cell.Font = defaultFont;
                                #region MaskCellType
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
                                #endregion
                            }
                            else if (cell.CellType is ImageCellType)
                            {
                                //cell.Font = defaultFont;
                                if (value != null && hasValue)
                                {
                                    try
                                    {
                                        cell.Value = JZCommonHelper.StringToBitmap(value.ToString());
                                    }
                                    catch (Exception e1)
                                    {
                                        logger.Error(e1);
                                    }
                                }
                                else
                                {
                                    cell.Value = null;
                                }
                            }
                            else if (cell.CellType is HyperLinkCellType)
                            {
                                //cell.Font = defaultFont;
                                #region HyperLinkCellType
                                if (value != null)
                                {
                                    List<string> lstLink = new List<string>();
                                    HyperLinkCellType hlnkCell = new HyperLinkCellType();
                                    try
                                    {
                                        lstLink = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(value.ToString());
                                    }
                                    catch { }


                                    if (lstLink == null)
                                    {
                                        lstLink = new List<string>(new string[] { "", "", "超链接" });
                                    }

                                    hlnkCell.Links = lstLink.ToArray();

                                    hlnkCell.Text = "";
                                    if (lstLink.Count >= 1)
                                    {
                                        hlnkCell.Text = lstLink[0];
                                    }

                                    hlnkCell.Link = "";
                                    if (lstLink.Count >= 2)
                                    {
                                        hlnkCell.Link = lstLink[1];
                                    }


                                    cell.Value = "超链接";
                                    if (lstLink.Count >= 3)
                                    {
                                        cell.Value = lstLink[2];
                                    }

                                    cell.CellType = hlnkCell;
                                }
                                else
                                {
                                    HyperLinkCellType hlnkCell = new HyperLinkCellType();
                                    cell.CellType = hlnkCell;
                                }
                                #endregion
                            }
                            else
                            {
                                cell.Font = defaultFont;
                                cell.Value = value;
                            }
                            #endregion
                            JZCellProperty p = cell.Tag as JZCellProperty;
                            #region 处理单元格属性
                            if (p != null)
                            {
                                if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                                {//管理员可修改所有
                                    cell.Locked = false;
                                }
                                else
                                {
                                    if (isSameTestRoom)
                                    {
                                        if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_施工单位")
                                        {
                                            if (p.IsReadOnly)
                                            {//施工单位对只读字段不能修改
                                                cell.Locked = true;
                                            }
                                            else
                                            {
                                                if (isAskChanging)
                                                {//若申请修改批准，允许修改
                                                    cell.Locked = false;
                                                }
                                                else
                                                {
                                                    if (hasValue && p.IsKey)
                                                    {//如果是关键值，不允许修改
                                                        cell.Locked = true;
                                                    }
                                                    else if (hasValue && (cell.CellType is NumberCellType))
                                                    {//不是关键字段，但已经填写过内容，还是数字，不可修改
                                                        bool isNumber = false;
                                                        try
                                                        {
                                                            decimal d = decimal.Parse(value.ToString());
                                                            isNumber = true;
                                                        }
                                                        catch
                                                        {

                                                        }
                                                        if (isNumber)
                                                        {
                                                            cell.Locked = true;
                                                        }
                                                        else
                                                        {
                                                            cell.Locked = false;
                                                        }
                                                    }
                                                    else
                                                    {//其它可修改
                                                        cell.Locked = false;
                                                    }
                                                }
                                            }
                                        }
                                        else if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_监理单位")
                                        {
                                            if (p.IsReadOnly)
                                            {//监理单位对只读字段不能修改，其它字段可自行修改
                                                cell.Locked = true;
                                            }
                                            else
                                            {
                                                cell.Locked = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //打开其它试验室的资料，不允许任何修改
                                        cell.Locked = true;
                                    }
                                }
                            }
                            else
                            {
                                logger.Error("未能设置数据区信息：单元格" + dataCellDefault.Name + "，表单：" + sheet.Name);
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
                if (isAskChanging && !isNewData)
                    MessageBox.Show("您上次的修改申请监理已同意，本次修改有效，请谨慎保存！");
            }
            catch (TimeoutException ex1)
            {
                isError = true;
                logger.Error(ex1.StackTrace);
                MessageBox.Show("网络原因造成数据无法访问，请检查本机网络连接，或稍后再试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                isError = true;
                logger.Error(ex.ToString());
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
                catch (Exception ex1)
                {
                    logger.Error("modeleviewer finally error:" + ex1.ToString());
                }

            }
            if (isError == true)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 参照事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DownListCellType_ReferenceFinished(object sender, ReferenceEventArgs e)
        {
            if (e.ReferenceInfo is SheetReference)
            {
                SheetReference Reference = e.ReferenceInfo as SheetReference;
                SheetView ActiveSheet = Table_Sheet[Reference.TableName];
                SheetConfiguration SheetConfiguration = ActiveSheet.Tag as SheetConfiguration;
                TableDefineInfo TableSchema = SheetConfiguration.DataTableSchema.Schema;
                foreach (ReferenceItem Item in Reference.ReferenceItems)
                {
                    FieldDefineInfo FieldInfo = TableSchema.GetFieldDefineInfo(Item.ColumnName);
                    ActiveSheet.Cells[FieldInfo.RangeInfo].Text = e.ReferenceData[Item.ReferenceColumnName];
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

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == SaveDataButton)
            {
                SaveData();
            }
            else if (sender == SponsorModifyButton)
            {
                SponsorModifyData();
            }
            else if (sender == SelectTemperatureTypeButton)
            {
                SelectTemperatureType();
            }
            else if (sender == TemperatureListButton)
            {
                TemperatureList();
            }
        }

        private void TemperatureList()
        {
            if (documentID != Guid.Empty)
            {
                var dialog = new TemperatureDialog(testRoomCode);
                dialog.ShowWithDocument(System.Convert.ToString(documentID));
            }
            else
            {
                MessageBox.Show("请先保存数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectTemperatureType()
        {
            if (documentID != Guid.Empty)
            {
                TemperatureUseDialog tud = new TemperatureUseDialog(documentID, testRoomCode);
                tud.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先保存数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 保存资料 
        /// </summary>
        private bool SaveData()
        {
            //结束对单元格的编辑
            FpSpread.StopCellEditing();
            List<string> KeyInfos = new List<string>();//保存关键值
            List<string> KeyModuleNames = new List<string>();//保存关键值存在的哪些模板中
            List<string> NullInfos = new List<string>();//保存不能为空的值
            List<String> dateConditions = new List<string>();
            String error = "";
            JZDocument doc = ModuleHelperClient.GetDefaultDocument(moduleIndex);
            DateTime serverDate = ModuleHelperClient.GetServerDate();
            doc.ID = document.ID;
            Boolean isInvalid = false;
            //bool bInvalidImageSize = false;//图片是否超出最大设置值
            //int MAX_IMAGE_SIZE = 100;//图片最大上传大小,单位KB
            foreach (JZSheet sheet in doc.Sheets)
            {
                SheetView view = GetSheetViewByID(sheet.ID);
                if (view == null)
                {
                    continue;
                }
                Boolean hasSheetName = false;
                foreach (JZCell dataCell in sheet.Cells)
                {
                    #region 循环处理单元格
                    Cell cell = view.Cells[dataCell.Name];
                    if (cell != null)
                    {
                        IGetFieldType FieldTypeGetter = cell.CellType as IGetFieldType;
                        dataCell.Value = cell.Value;
                        if (dataCell.Value != null && dataCell.Value is String)
                        {
                            dataCell.Value = dataCell.Value.ToString().Trim(' ', '\r', '\n');
                        }
                        JZCellProperty property = cell.Tag as JZCellProperty;
                        if (property != null)
                        {
                            if (FieldTypeGetter != null && FieldTypeGetter.FieldType.Description == "图片")
                            {
                                if (cell.Value != null && cell.Value is Bitmap)
                                {
                                    //创建一个bitmap类型的bmp变量来读取文件。
                                    Bitmap bitmap = cell.Value as Bitmap;
                                    #region 注释
                                    //int iCellWidth = 0;// (int)view.Cells[cell.Row.Index, cell.Column.Index, cell.Row.Index + cell.RowSpan, cell.Column.Index + cell.ColumnSpan].Column.Width;
                                    //int iCellHeight = 0;// (int)view.Cells[cell.Row.Index, cell.Column.Index, cell.Row.Index + cell.RowSpan, cell.Column.Index + cell.ColumnSpan].Row.Height;
                                    //#region 计算单元格的宽高
                                    //for (int i = 0; i < cell.ColumnSpan; i++)
                                    //{
                                    //    iCellWidth += (int)view.Cells[cell.Row.Index, cell.Column.Index + i].Column.Width;
                                    //}
                                    //for (int i = 0; i < cell.RowSpan; i++)
                                    //{
                                    //    iCellHeight += (int)view.Cells[cell.Row.Index + i, cell.Column.Index].Row.Height;
                                    //}
                                    //#endregion

                                    //float fCellRate = (float)iCellHeight / iCellWidth;//高宽比

                                    //#region 判断要裁剪的图片的高度
                                    //int imgWidth, imgHeight, imgCutWidth, imgCutHeight;
                                    //imgWidth = bitmap.Width;
                                    //imgHeight = bitmap.Height;
                                    //if (imgWidth <= iCellWidth && imgHeight <= iCellHeight)
                                    //{
                                    //    imgCutWidth = imgWidth;
                                    //    imgCutHeight = imgHeight;
                                    //}
                                    //else
                                    //{
                                    //    float imgRate;
                                    //    imgRate = (float)imgHeight / imgWidth;
                                    //    if (imgRate < fCellRate)
                                    //    {//高小了
                                    //        if (imgWidth < iCellWidth)
                                    //        {
                                    //            imgCutWidth = imgWidth;
                                    //        }
                                    //        else
                                    //        {
                                    //            imgCutWidth = iCellWidth;
                                    //        }
                                    //        imgCutHeight = (int)(imgCutWidth * imgRate);
                                    //        //if (imgHeight < iCellHeight)
                                    //        //{
                                    //        //    imgCutHeight = imgHeight;
                                    //        //}
                                    //        //else
                                    //        //{
                                    //        //    imgCutHeight = iCellHeight;
                                    //        //}
                                    //        //imgCutWidth = (int)(imgCutHeight / imgRate);
                                    //    }
                                    //    else
                                    //    {
                                    //        if (imgHeight < iCellHeight)
                                    //        {
                                    //            imgCutHeight = imgHeight;
                                    //        }
                                    //        else
                                    //        {
                                    //            imgCutHeight = iCellHeight;
                                    //        }
                                    //        imgCutWidth = (int)(imgCutHeight / imgRate);
                                    //        //if (imgWidth < iCellWidth)
                                    //        //{
                                    //        //    imgCutWidth = imgWidth;
                                    //        //}
                                    //        //else
                                    //        //{
                                    //        //    imgCutWidth = iCellWidth;
                                    //        //}
                                    //        //imgCutHeight = (int)(imgCutWidth * imgRate);
                                    //    }
                                    //}
                                    //#endregion
                                    //Bitmap imgCut = JZCommonHelper.KiResizeImage(bitmap, imgCutWidth, imgCutHeight, 0);


                                    //using (MemoryStream memoryStream = new MemoryStream())
                                    //{
                                    //    bitmap2.Save(memoryStream, ImageFormat.Jpeg);
                                    //    byte[] bitmapBytes = memoryStream.GetBuffer();
                                    //    double size = (bitmapBytes.Length / 1024);
                                    //    logger.Error("bitmap2 size:" + size);
                                    //    if (size > MAX_IMAGE_SIZE)
                                    //    {
                                    //        bInvalidImageSize = true;
                                    //        break;
                                    //    }
                                    //}
                                    #endregion
                                    //新建第二个bitmap类型的bmp2变量，我这里是根据我的程序需要设置的。
                                    Bitmap bitmap2 = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format16bppRgb555);//PixelFormat.Format16bppRgb555
                                    //将第一个bmp拷贝到bmp2中
                                    Graphics draw = Graphics.FromImage(bitmap2);
                                    draw.DrawImage(bitmap, 0, 0);
                                    dataCell.Value = JZCommonHelper.BitmapToString(bitmap2);
                                }
                            }
                            else if (FieldTypeGetter != null && FieldTypeGetter.FieldType.Description == "数字")
                            {
                                if (cell.Value != null)
                                {
                                    Decimal d;
                                    if (Decimal.TryParse(cell.Value.ToString().Trim(' ', '\r', '\n'), out d))
                                    {
                                        dataCell.Value = d;
                                    }
                                    else
                                    {
                                        dataCell.Value = null;
                                    }
                                }
                            }
                            else if (FieldTypeGetter != null && FieldTypeGetter.FieldType.Description == "超链接")
                            {

                                var hlnkCell = cell.CellType as HyperLinkCellType;
                                var arrLink = new string[3];
                                arrLink[0] = hlnkCell.Text;
                                arrLink[1] = hlnkCell.Link;
                                arrLink[2] = cell.Value as string;
                                dataCell.Value = Newtonsoft.Json.JsonConvert.SerializeObject(arrLink);
                            }
                            else if (FieldTypeGetter != null && FieldTypeGetter.FieldType.Description == "删除线")
                            {
                                if (cell.Value != null && cell.Value.ToString().Length < FieldTypeGetter.FieldType.Length)
                                {
                                    //new System.Text.RegularExpressions.Regex("[\\s]+").Replace(str, " ");
                                    dataCell.Value = new System.Text.RegularExpressions.Regex("'+").Replace(cell.Value.ToString(), "'");// cell.Value;
                                }
                                else
                                {
                                    dataCell.Value = null;
                                }
                            }
                            if (property.IsUnique)
                            {
                                if (cell.Value != null && cell.Value.ToString().Replace("/", "").Trim(' ', '\r', '\n') != "")
                                {
                                    if (!KeyInfos.Contains(property.Description))
                                    {
                                        DataTable dtModuleName = DocumentHelperClient.IsUniqueAndReturnDT(property, dataCell, sheet.ID, moduleIndex, testRoomCode, document.ID);
                                        if (dtModuleName != null && dtModuleName.Rows.Count > 0)
                                        {
                                            KeyInfos.Add(property.Description);
                                            for (int m = 0; m < dtModuleName.Rows.Count; m++)
                                            {
                                                KeyModuleNames.Add(dtModuleName.Rows[m]["Name"].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                            if (property.IsNotNull)
                            {
                                if (cell.Value == null || cell.Value.ToString() == "")
                                {
                                    NullInfos.Add(property.Description);
                                }
                            }
                            if (cell.Value != null && askChanging == false &&
                                Yqun.Common.ContextCache.ApplicationContext.Current.UserCode.Length >= 4)
                            {
                                ConditionalFormat[] conditions = view.GetConditionalFormats(cell.Row.Index, cell.Column.Index);
                                if (conditions != null && conditions.Length > 0)
                                {
                                    for (int i = 0; i < conditions.Length; i++)
                                    {
                                        ConditionalFormat condition = conditions[i];

                                        switch (condition.ComparisonOperator)
                                        {
                                            case ComparisonOperator.Between:
                                                break;
                                            case ComparisonOperator.EqualTo:
                                                if (condition.FirstCondition.ToLower() == "@now" && documentID == Guid.Empty)
                                                {
                                                    DateTime cellDate = System.Convert.ToDateTime(cell.Value);
                                                    if (cellDate != null && serverDate != null)
                                                    {
                                                        if (cellDate != serverDate)
                                                        {
                                                            dateConditions.Add(property.Description);
                                                        }
                                                    }
                                                }
                                                break;
                                            case ComparisonOperator.GreaterThan:

                                                break;
                                            case ComparisonOperator.GreaterThanOrEqualTo:
                                                if (condition.FirstCondition.ToLower() == "@now" && documentID == Guid.Empty)
                                                {
                                                    DateTime cellDate = System.Convert.ToDateTime(cell.Value);
                                                    if (cellDate != null && serverDate != null)
                                                    {
                                                        if (cellDate < serverDate)
                                                        {
                                                            dateConditions.Add(property.Description);
                                                        }
                                                    }
                                                }
                                                break;
                                            case ComparisonOperator.IsEmpty:
                                                break;
                                            case ComparisonOperator.IsFalse:
                                                break;
                                            case ComparisonOperator.IsTrue:
                                                break;
                                            case ComparisonOperator.LessThan:
                                                break;
                                            case ComparisonOperator.LessThanOrEqualTo:
                                                break;
                                            case ComparisonOperator.NotBetween:
                                                break;
                                            case ComparisonOperator.NotEqualTo:
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                        if (dataCell.Value != null)
                        {
                            if (dataCell.Value.ToString().Contains("#VALUE") ||
                                dataCell.Value.ToString().Contains("#N/A") ||
                                dataCell.Value.ToString().Contains("#REF!") ||
                                dataCell.Value.ToString().Contains("#DIV/0!") ||
                                dataCell.Value.ToString().Contains("#NUM!") ||
                                dataCell.Value.ToString().Contains("#NAME?") ||
                                dataCell.Value.ToString().Contains("#NULL!"))
                            {
                                if (!hasSheetName)
                                {
                                    hasSheetName = true;
                                    if (error == "")
                                    {
                                        error += sheet.Name + ":" + dataCell.Name;
                                    }
                                    else
                                    {
                                        error += ";" + sheet.Name + ":" + dataCell.Name;
                                    }
                                }
                                else
                                {
                                    error += "," + dataCell.Name;
                                }
                            }
                        }
                    }
                    #endregion
                }
                //if (bInvalidImageSize == true)
                //{
                //    break;
                //}
            }
            //if (bInvalidImageSize == true)
            //{
            //    MessageBox.Show(string.Format("图片大小不能超出{0}KB", MAX_IMAGE_SIZE), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            if (dateConditions.Count > 0)
            {
                MessageBox.Show(string.Format("{0}必须为当前日期！", String.Join(",", dateConditions.ToArray())), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (NullInfos.Count > 0)
            {
                MessageBox.Show(string.Format("{0}不能为空，请核实后再保存！", String.Join(",", NullInfos.ToArray())), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (KeyInfos.Count > 0)
            {
                if (KeyModuleNames.Count > 0)
                {
                    MessageBox.Show(string.Format("{0}不唯一，在模板<{1}>中已存在，请核实后再保存！", String.Join(",", KeyInfos.ToArray()), string.Join(">,<", KeyModuleNames.ToArray())), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(string.Format("{0}不唯一，请核实后再保存！", String.Join(",", KeyInfos.ToArray())), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return false;
            }

            String msg = "";
            Sys_Document docBase = new Sys_Document();
            docBase.ModuleID = moduleIndex;
            docBase.DataName = "";
            docBase.TestRoomCode = testRoomCode;
            docBase.TryType = TryType;
            Boolean saveflag = true;
            if (error != "")
            {
                //验证不合格
                if (MessageBox.Show("此资料出现异常，位置为：" + error + "，是否继续保存？", "异常提醒", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    saveflag = false;
                }
            }
            if (!CheckQualify(doc) && saveflag)
            {
                //验证不合格
                if (MessageBox.Show("此资料为不合格资料，是否继续保存？", "不合格提醒", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    saveflag = false;
                }
                else
                {
                    isInvalid = true;
                }
            }
            if (!CheckTimestamp() && saveflag)
            {
                //验证是否被修改过
                if (MessageBox.Show("此资料已经被修改过了，继续保存将会丢失已经保存过的数据，是否继续保存？", "保存提醒", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    saveflag = false;
                }
            }
            if (saveflag)
            {
                Guid savedID = DocumentHelperClient.SaveDocument(doc, docBase);
                if (savedID != Guid.Empty)
                {
                    doc.ID = savedID;
                    document = doc;
                    documentID = savedID;
                    msg = "资料保存成功。";
                    Saved = true;
                    //Added By Tan in 20140531 平行过来的资料，编辑后台帐显示为抽检
                    //docBase = DocumentHelperClient.GetDocumentBaseInfoByID(savedID);
                    if (viewControl != null)
                        viewControl.SyncData(docBase.TryType, document, isNewData);
                    MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (isNewData && StadiumConfig != null && StadiumConfig.Temperature > 0)
                    {
                        Boolean bResult = DocumentHelperClient.SaveDocumentTemperatureType(documentID, 0);
                        if (bResult == false)
                        {
                            MessageBox.Show("设置默认温度类型失败，请手工设置资料所使用的温度类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        TemperatureUseDialog tud = new TemperatureUseDialog(savedID, testRoomCode);
                        tud.ShowDialog();
                    }
                    isNewData = false;
                    Sys_Document basedoc = DocumentHelperClient.GetDocumentBaseInfoByID(documentID);
                    if (basedoc != null)
                    {
                        LastEditedTime = basedoc.LastEditedTime;
                    }
                    if (isInvalid)
                    {
                        String userType = DepositoryLabStadiumList.GetUserType(
                            Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code);
                        Int32 t = 0;
                        if (userType.IndexOf("监理") > 0)
                        {
                            t = 1;
                        }
                        InvalidProcess ip = new InvalidProcess(savedID.ToString(), t);
                        ip.ShowDialog();
                    }
                    return true;
                }
                else
                {
                    msg = "资料保存失败。";
                    MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

            }
            else
            {
                return false;
            }
            return true;
        }

        private Boolean CheckQualify(JZDocument doc)
        {
            Boolean flag = true;
            Sys_Module module = ModuleHelperClient.GetModuleBaseInfoByID(moduleIndex);
            if (module != null)
            {
                if (module.QualifySettings != null && module.QualifySettings.Count > 0)
                {
                    foreach (QualifySetting qs in module.QualifySettings)
                    {
                        Object obj = JZCommonHelper.GetCellValue(doc, qs.SheetID, qs.CellName);
                        if (obj != null && obj.ToString() != "")
                        {
                            return false;
                        }
                    }
                }
            }
            return flag;
        }
        /// <summary>
        /// 检查资料是否被修改过
        /// </summary>
        /// <returns></returns>
        private Boolean CheckTimestamp()
        {
            Boolean flag = false;
            Sys_Document doc = DocumentHelperClient.GetDocumentBaseInfoByID(documentID);
            if (doc != null)
            {
                if (doc.LastEditedTime == null)
                {
                    if (LastEditedTime == null)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else
                {
                    flag = doc.LastEditedTime.Equals(LastEditedTime);
                }
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        private SheetView GetSheetViewByID(Guid sheetID)
        {
            foreach (SheetView view in FpSpread.Sheets)
            {
                if (new Guid(view.Tag.ToString()) == sheetID)
                {
                    return view;
                }
            }
            return null;
        }

        //申请资料修改
        private void SponsorModifyData()
        {
            Sys_Module module = ModuleHelperClient.GetModuleBaseInfoByID(moduleIndex);
            if (documentID != Guid.Empty)
            {
                SponsorModificationDialog Dialog = new SponsorModificationDialog(documentID, module);
                Dialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("未保存资料无需申请修改！");
            }
        }

        private void ModuleViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Saved)
            {
                DialogResult Result = MessageBox.Show("是否保存对资料的更改？", "资料编辑器", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (Result == DialogResult.Yes)
                {
                    if (SaveData() == false)
                    {
                        e.Cancel = true;
                    }
                }
                else if (Result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void FpSpread_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CellRange r = FpSpread.GetCellFromPixel(FpSpread.GetActiveRowViewportIndex(), FpSpread.GetActiveColumnViewportIndex(), e.X, e.Y);
                if (r.Row > -1 && r.Column > -1)
                {
                    Cell cell = FpSpread.ActiveSheet.Cells[r.Row, r.Column];
                    CellRange spancell = FpSpread.ActiveSheet.GetSpanCell(r.Row, r.Column);
                    if (spancell != null)
                    {
                        cell = FpSpread.ActiveSheet.Cells[spancell.Row, spancell.Column];
                    }

                    FpSpread.ActiveSheet.SetActiveCell(cell.Row.Index, cell.Column.Index);
                    if (FpSpread.ActiveSheet.Cells[cell.Row.Index, cell.Column.Index].CellType is ImageCellType)
                    {
                        if (testRoomCode == Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code ||
                            Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                        {
                            contextMenuStrip1.Show(FpSpread, new Point(e.X, e.Y));
                        }
                    }
                    else if (FpSpread.ActiveSheet.Cells[cell.Row.Index, cell.Column.Index].CellType is HyperLinkCellType)
                    {
                        if (testRoomCode == Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code ||
                            Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                        {
                            contextMenuStrip2.Show(FpSpread, new Point(e.X, e.Y));
                        }
                    }
                }
            }
        }

        private void MenuItem_RemoveImage_Click(object sender, EventArgs e)
        {
            FpSpread.ActiveSheet.ActiveCell.Value = null;
        }

        private void MenuItem_AddImage_Click(object sender, EventArgs e)
        {
            var activeCell = FpSpread.ActiveSheet.ActiveCell;
            var image = activeCell.Value as Bitmap;
         
            var width = 0f;
            var height = 0f;

            for (var w = 0; w < activeCell.ColumnSpan;w++ )
            {
                width += FpSpread.ActiveSheet.Columns[activeCell.Column.Index +w].Width;
            }

            for (var h = 0; h < activeCell.RowSpan; h++)
            {
                height += FpSpread.ActiveSheet.Rows[activeCell.Row.Index + h].Height;
            }

            var editor = new ImageEditor();
            editor.SetSizeOfContainer((int)width, (int)height);

            if (image != null)      
            {
                editor.SetImage(image);
            }

            if (editor.ShowDialog() == DialogResult.OK)
            {
                activeCell.HorizontalAlignment = CellHorizontalAlignment.Center;
                activeCell.VerticalAlignment = CellVerticalAlignment.Center;
                activeCell.Value = editor.Result;
            }
           
        }


        private void MenuItem_SetHyperLink_Click(object sender, EventArgs e)
        {
            if (FpSpread.ActiveSheet.ActiveCell.CellType is FarPoint.Win.Spread.CellType.HyperLinkCellType)
            {
                Cell cell = FpSpread.ActiveSheet.ActiveCell;

                HyperLinkSettingDialog linkDialog = new HyperLinkSettingDialog(cell);
                linkDialog.ShowDialog();
            }
        }

        private void ButtonSetImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.Filter = "图片文件(*.bmp,*.jpg,*.png)|*.bmp,*.jpg,*.png|所有文件(*.*)|*.*";
            FileDialog.FilterIndex = 2;
            FileDialog.InitialDirectory = Application.StartupPath;
            FileDialog.RestoreDirectory = true;
            if (DialogResult.OK == FileDialog.ShowDialog())
            {
                try
                {
                    //Image image = new Bitmap(FileDialog.FileName);

                    SheetView view = FpSpread.ActiveSheet;
                    //Sheet.ActiveCell.Value = image;
                    Cell cell = view.ActiveCell;
                    Bitmap bitmap = new Bitmap(FileDialog.FileName);
                    int iCellWidth = 0;// (int)view.Cells[cell.Row.Index, cell.Column.Index, cell.Row.Index + cell.RowSpan, cell.Column.Index + cell.ColumnSpan].Column.Width;
                    int iCellHeight = 0;// (int)view.Cells[cell.Row.Index, cell.Column.Index, cell.Row.Index + cell.RowSpan, cell.Column.Index + cell.ColumnSpan].Row.Height;
                    #region 计算单元格的宽高
                    for (int i = 0; i < cell.ColumnSpan; i++)
                    {
                        iCellWidth += (int)view.Cells[cell.Row.Index, cell.Column.Index + i].Column.Width;
                    }
                    for (int i = 0; i < cell.RowSpan; i++)
                    {
                        iCellHeight += (int)view.Cells[cell.Row.Index + i, cell.Column.Index].Row.Height;
                    }
                    #endregion

                    float fCellRate = (float)iCellHeight / iCellWidth;//高宽比

                    #region 判断要裁剪的图片的高度
                    int imgWidth, imgHeight, imgCutWidth, imgCutHeight;
                    imgWidth = bitmap.Width;
                    imgHeight = bitmap.Height;
                    if (imgWidth <= iCellWidth && imgHeight <= iCellHeight)
                    {
                        imgCutWidth = imgWidth;
                        imgCutHeight = imgHeight;
                    }
                    else
                    {
                        float imgRate;
                        imgRate = (float)imgHeight / imgWidth;
                        if (imgRate < fCellRate)
                        {//高小了
                            if (imgWidth < iCellWidth)
                            {
                                imgCutWidth = imgWidth;
                            }
                            else
                            {
                                imgCutWidth = iCellWidth;
                            }
                            imgCutHeight = (int)(imgCutWidth * imgRate);
                        }
                        else
                        {
                            if (imgHeight < iCellHeight)
                            {
                                imgCutHeight = imgHeight;
                            }
                            else
                            {
                                imgCutHeight = iCellHeight;
                            }
                            imgCutWidth = (int)(imgCutHeight / imgRate);
                        }
                    }
                    #endregion
                    Bitmap imgCut = JZCommonHelper.KiResizeImage(bitmap, imgCutWidth, imgCutHeight, 0);
                    cell.HorizontalAlignment = CellHorizontalAlignment.Center;
                    cell.VerticalAlignment = CellVerticalAlignment.Center;
                    cell.Value = null;
                    cell.Value = imgCut;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        
    }
}
