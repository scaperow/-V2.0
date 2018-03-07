using System;
using System.Collections.Generic;
using System.Text;
using Yqun.Client.BizUI;
using FarPoint.Win.Spread;
using BizComponents;
using Yqun.Bases;
using BizCommon;
using FarPoint.Win;
using System.Data;
using System.Drawing;

namespace BizModules
{
    public class JZModuleCatch
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Guid _moduleID = Guid.Empty;
        public Guid ModuleID
        {
            get
            {
                return _moduleID;
            }
            set
            {
                if (value != _moduleID)
                {
                    LoadSpread(value);
                    _moduleID = value;
                }
            }
        }
        public MyCell FpSpread { get; set; }

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

        private void LoadSpread(Guid moduleID)
        {
            try
            {
                ProgressScreen.Current.ShowSplashScreen();
                ProgressScreen.Current.SetStatus = "正在打开模板...";
                FpSpread = new MyCell();
                //增加水印、图章
                FpSpread.Watermark = ModuleHelperClient.GetWatermarkByModuleID(moduleID);
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
                FpSpread.LoadFormulas(false);
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
                FpSpread.LoadFormulas(true);
                ProgressScreen.Current.SetStatus = "正在显示资料...";
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());   
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
        }
    }
}
