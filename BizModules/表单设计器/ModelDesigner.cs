using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizCommon;
using BizComponents;
using FarPoint.Win.Spread;
using Yqun.Bases;
using FarPoint.Win;
using Yqun.Permissions.Common;

namespace BizModules
{
    public partial class ModelDesigner : Form
    {
        Guid moduleID;
        Dictionary<String, JZFormulaData> CrossSheetFormulaCache = null;
        public List<Sys_Sheet> sheetsList = null;
        private Boolean _forLine = false;
        public Boolean ForLine
        {

            get
            {
                return _forLine;
            }
            set
            {
                _forLine = value;
            }
        }

        public ModelDesigner(Guid _moduleID)
        {
            InitializeComponent();
            moduleID = _moduleID;
            CrossSheetFormulaCache = new Dictionary<String, JZFormulaData>();
            fpSpreadEditor1.UserFormulaEntered += new EventHandler<UserFormulaEnteredEventArgs>(fpSpreadEditor1_UserFormulaEntered);
            fpSpreadEditor1.UserFormulaCanceled += new EventHandler<UserFormulaEnteredEventArgs>(fpSpreadEditor1_UserFormulaCanceled);
        }

        internal FpSpreadEditor Editor
        {
            get
            {
                return fpSpreadEditor1;
            }
        }

        public FpSpread FpSpread
        {
            get
            {
                return fpSpreadEditor1.FpSpread;
            }
        }

        private void ModelDesigner_Load(object sender, EventArgs e)
        {
            sheetsList = ModuleHelperClient.GetSheetsByModuleID(moduleID);
            ImportSheetButton.Visible = !ForLine;
            RemoveSheetButton.Visible = !ForLine;
            WriteFunctionButton.Visible = !ForLine;
            toolStripSeparator2.Visible = !ForLine;
            toolStripSeparator3.Visible = !ForLine;
            LoadSheets();
        }

        public void UpdateChart()
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
                        }
                    }
                }

                //支持浮动的图表
                foreach (IElement Element in Sheet.DrawingContainer.ContainedObjects)
                {
                    if (Element is ChartShape)
                    {
                        ChartShape Shape = Element as ChartShape;
                        Shape.ActiveSheet = Sheet;
                        Shape.Locked = false;
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
                        Shape.Locked = false;
                    }
                }
            }
        }

        void fpSpreadEditor1_UserFormulaCanceled(object sender, UserFormulaEnteredEventArgs e)
        {
            SheetView Sheet = FpSpread.Sheets[e.View.ActiveSheetIndex];
            string key = string.Format("{0}_{1}_{2}", Sheet.Tag.ToString(), e.Row, e.Column);
            if (CrossSheetFormulaCache.ContainsKey(key))
            {
                CrossSheetFormulaCache.Remove(key);
                if (ForLine)
                {
                    FpSpread.ActiveSheet.ActiveCell.BackColor = Color.White;
                }
            }

        }

        void fpSpreadEditor1_UserFormulaEntered(object sender, UserFormulaEnteredEventArgs e)
        {
            SheetView Sheet = FpSpread.Sheets[e.View.ActiveSheetIndex];

            string key = string.Format("{0}_{1}_{2}", Sheet.Tag.ToString(), e.Row, e.Column);
            if (string.IsNullOrEmpty(Sheet.Cells[e.Row, e.Column].Formula))
            {
                if (CrossSheetFormulaCache.ContainsKey(key))
                {
                    CrossSheetFormulaCache.Remove(key);
                    FpSpread.ActiveSheet.ActiveCell.BackColor = Color.White;
                }
                return;
            }

            if (!CrossSheetFormulaCache.ContainsKey(key))
            {
                JZFormulaData Formula = new JZFormulaData();

                Formula.ModelIndex = moduleID;
                Formula.SheetIndex = new Guid(Sheet.Tag.ToString());
                Formula.RowIndex = e.Row;
                Formula.ColumnIndex = e.Column;
                Formula.Formula = Sheet.Cells[e.Row, e.Column].Formula;
                CrossSheetFormulaCache.Add(key, Formula);
                if (ForLine)
                {
                    FpSpread.ActiveSheet.ActiveCell.BackColor = Color.Blue;
                }
            }
            else
            {
                JZFormulaData Formula = CrossSheetFormulaCache[key];
                Formula.Formula = Sheet.Cells[e.Row, e.Column].Formula;
            }
        }

        #region 工具栏

        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == ImportSheetButton)
            {
                ImportSheet();
            }
            else if (sender == RemoveSheetButton)
            {
                RemoveSheet();
            }
            else if (sender == SaveModuleButton)
            {
                SaveModule();
            }
            else if (sender == WriteFunctionButton)
            {
                SettingFunctions();
            }
        }

        /// <summary>
        /// 导入表单
        /// </summary>
        private void ImportSheet()
        {
            ReferenceSheetDialog Dialog = new ReferenceSheetDialog(this);
            if (DialogResult.OK == Dialog.ShowDialog())
            {
                Dialog.Close();
                Update();
                sheetsList = Dialog.GetSheetList();
                LoadSheets();
            }
        }

        /// <summary>
        /// 移除表单
        /// </summary>
        private void RemoveSheet()
        {
            String Msg = "确定移除表单 ‘" + FpSpread.ActiveSheet.SheetName + "’吗？";
            if (DialogResult.Yes == MessageBox.Show(Msg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                string strActiveSheetID = FpSpread.ActiveSheet.Tag.ToString();
                FpSpread.Sheets.Remove(FpSpread.ActiveSheet);
                foreach (Sys_Sheet item in sheetsList)
                {
                    if (item.ID == new Guid(strActiveSheetID))
                    {
                        sheetsList.Remove(item);
                        break;
                    }
                }
                if (FpSpread.Sheets.Count > 0)
                {
                    FpSpread.ActiveSheet = FpSpread.Sheets[FpSpread.Sheets.Count - 1];
                }
            }
        }

        private void LoadSheets()
        {
            ProgressScreen.Current.ShowSplashScreen();
            this.AddOwnedForm(ProgressScreen.Current);
            Dictionary<Guid, SheetView> SheetCollection = new Dictionary<Guid, SheetView>();
            List<FarPoint.CalcEngine.FunctionInfo> Infos = FunctionItemInfoUtil.getFunctionItemInfos();

            try
            {
                CrossSheetFormulaCache.Clear();
                FpSpread.Sheets.Clear();

                List<JZFormulaData> CrossSheetLineFormulaInfos = ModuleHelperClient.GetLineFormulaByModuleIndex(moduleID);

                foreach (Sys_Sheet sheet in sheetsList)
                {
                    ProgressScreen.Current.SetStatus = "正在初始化表单‘" + sheet.Name + "’";
                    String sheetXML = ModuleHelperClient.GetSheetXMLByID(sheet.ID);
                    SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), sheetXML, "SheetView") as SheetView;
                    SheetView.Tag = sheet.ID;
                    SheetView.SheetName = sheet.Name;
                    SheetView.ZoomFactor = 1.0F;
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

                    foreach (FarPoint.CalcEngine.FunctionInfo Info in Infos)
                    {
                        SheetView.AddCustomFunction(Info);
                    }
                    if (!SheetCollection.ContainsKey(sheet.ID))
                        SheetCollection.Add(sheet.ID, SheetView);
                }
                FpSpread.LoadFormulas(true);
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
                        if (formula.Formula.ToUpper().Trim() == "NA()")
                        {
                            cell.Formula = "";
                        }
                        else
                        {
                            cell.Formula = formula.Formula;
                        }
                        if (formula.FormulaType == 1)
                        {
                            cell.BackColor = Color.Blue;
                        }
                        string key = string.Format("{0}_{1}_{2}", formula.SheetIndex, formula.RowIndex, formula.ColumnIndex);
                        if (ForLine)
                        {//线路公式
                            if (formula.FormulaType == 1)
                            {
                                if (!CrossSheetFormulaCache.ContainsKey(key))
                                {
                                    CrossSheetFormulaCache.Add(key, formula);
                                }
                                else
                                {
                                    CrossSheetFormulaCache[key] = formula;
                                }

                            }
                        }
                        else
                        {
                            if (!CrossSheetFormulaCache.ContainsKey(key))
                            {
                                CrossSheetFormulaCache.Add(key, formula);
                            }
                            else
                            {
                                CrossSheetFormulaCache[key] = formula;
                            }
                        }
                    }
                }

                FpSpread.LoadFormulas(true);
                fpSpreadEditor1.EnableToolStrip(true);
                UpdateChart();
                UpdateEquation();
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载模板出错！\r\n原因：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.RemoveOwnedForm(ProgressScreen.Current);
                ProgressScreen.Current.CloseSplashScreen();
                this.Activate();
            }
        }

        /// <summary>
        /// 保存模块
        /// </summary>
        private void SaveModule()
        {
            Boolean Result = false;
            if (ForLine)
            {
                Result = ModuleHelperClient.SaveLineFormula(moduleID, CrossSheetFormulaCache);
            }
            else
            {
                Result = ModuleHelperClient.SaveModule(moduleID, sheetsList, CrossSheetFormulaCache);
            }

            String msg = Result ? "保存模板成功。" : "保存模板失败。";
            MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReportIndexSettingDialog dlgRIS = new ReportIndexSettingDialog(moduleID, sheetsList.Count);
            dlgRIS.ShowDialog();
        }

        /// <summary>
        /// 配置写数函数
        /// </summary>
        private void SettingFunctions()
        {
            //ReadWriteFunctionDialog FuncDialog = new ReadWriteFunctionDialog(Module);
            //FuncDialog.ShowDialog();
        }

        #endregion 工具栏

        #region 快捷菜单

        private void InitContextMenu()
        {
            ToolStripMenuItem MenuItem = new ToolStripMenuItem("设置公式");
            MenuItem.Tag = "@SetFormula";
            contextMenuStrip1.Items.Add(MenuItem);
            MenuItem.Click += new EventHandler(ToolStripMenuItem_Click);

            MenuItem = new ToolStripMenuItem("清除公式");
            MenuItem.Tag = "@ClearFormula";
            contextMenuStrip1.Items.Add(MenuItem);
            MenuItem.Click += new EventHandler(ToolStripMenuItem_Click);
        }

        #endregion 快捷菜单

        void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem Item = sender as ToolStripItem;
            switch (Item.Tag.ToString().ToLower())
            {
                case "@setformula":
                    fpSpreadEditor1.ShowFormulaDialog();
                    break;
                case "@clearformula":
                    fpSpreadEditor1.ClearFormula();
                    break;
            }
        }

        void UpdateModuleCache()
        {
            //if (this.Owner.Parent != null)
            //{
            //    BizWindow BizWindow = this.Owner.Parent as BizWindow;
            //    ProjectCatlogContent Content = BizWindow.ProjectCatlogContent;
            //    if (Content.ActiveModule != null && Content.ActiveModule.Index == Module.Index)
            //    {
            //        Content.ActiveModule = Module;
            //        Content.ActiveModuleFields = DepositoryModuleUserFields.GetModuleFields(Content.ActiveModule.Index, Content.ActiveNodeCode);
            //    }
            //}
        }
    }
}
