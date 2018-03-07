using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Yqun.Bases;
using Yqun.Common.ContextCache;
using Yqun.Interfaces;
using System.Windows.Forms;
using BizCommon;
using System.Data;
using FarPoint.Win.Spread;
using FarPoint.Win;
using FarPoint.Excel;
using Yqun.Client.BizUI;
using System.Drawing;

namespace BizComponents
{
    public class DataExportClass
    {
        BackgroundWorker worker;
        Form Owner;
        Guid moduleID;
        String testRoomCode;
        String moduleName;
        
        FolderBrowserDialog folderDialog;
        String path;

        public DataExportClass()
        {
            Owner = Cache.CustomCache[SystemString.主窗口] as Form;

            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.WorkerReportsProgress = true;


            if (DialogResult.OK == MessageBox.Show("请选择资料导出的文件夹！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                folderDialog = new FolderBrowserDialog();
                if (DialogResult.OK == folderDialog.ShowDialog())
                {
                    this.path = folderDialog.SelectedPath;
                }
            }
        }

        public void Run(Guid _moduleID,String _testRoomCode, String _moduleName)
        {
            moduleID = _moduleID;
            testRoomCode = _testRoomCode;
            moduleName = _moduleName;
            ProgressScreen.Current.ShowSplashScreen();
            Owner.AddOwnedForm(ProgressScreen.Current);
            worker.RunWorkerAsync(this);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Owner.RemoveOwnedForm(ProgressScreen.Current);
            ProgressScreen.Current.CloseSplashScreen();
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressScreen.Current.SetStatus = string.Format("已完成{0}%...", e.ProgressPercentage);
            ProgressScreen.Current.SetStep = e.ProgressPercentage;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            DataExportClass Params = e.Argument as DataExportClass;
            Font defaultFont = new Font("宋体", 9f);
            String Path = Params.path;

            if (string.IsNullOrEmpty(Path)) 
                return;

            MyCell fpSpread = new MyCell();
            fpSpread.Watermark = ModuleHelperClient.GetWatermarkByModuleID(moduleID);
            //初始化模板样式
            List<FarPoint.CalcEngine.FunctionInfo> Infos = FunctionItemInfoUtil.getFunctionItemInfos();
            List<JZFormulaData> CrossSheetFormulaInfos = ModuleHelperClient.GetFormulaByModuleIndex(moduleID);
            Dictionary<Sys_Document, JZDocument> list = DocumentHelperClient.GetDocumentDataListByModuleIDAndTestRoomCode(moduleID, testRoomCode);
            if (list.Count == 0)
            {
                return;
            }
            
            Dictionary<Sys_Document, JZDocument>.Enumerator em = list.GetEnumerator();
            int i = 0;
            while (em.MoveNext())
            {
                Sys_Document docBase = em.Current.Key;
                JZDocument doc = em.Current.Value;

                if (i == 0)
                {
                    foreach (JZSheet sheet in doc.Sheets)
                    {
                        ProgressScreen.Current.SetStatus = string.Format("正在准备表单{0}...", sheet.Name);
                        String sheetXML = ModuleHelperClient.GetSheetXMLByID(sheet.ID);
                        SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), sheetXML, "SheetView") as SheetView;
                        SheetView.Tag = sheet.ID;
                        SheetView.SheetName = sheet.Name;
                        SheetView.Cells[0, 0].Value = "";
                        SheetView.Protect = true;
                        fpSpread.Sheets.Add(SheetView);

                        foreach (FarPoint.CalcEngine.FunctionInfo Info in Infos)
                        {
                            SheetView.AddCustomFunction(Info);
                        }
                    }
                    fpSpread.LoadFormulas(true);
                }
                i = i + 1;
                
                worker.ReportProgress((int)(((float)i / (float)list.Count)*100));

                foreach (JZSheet sheet in doc.Sheets)
                {
                    SheetView sheetV = null;

                    for (int j = 0; j < fpSpread.Sheets.Count; j++)
                    {
                        if (new Guid(fpSpread.Sheets[j].Tag.ToString()) == sheet.ID)
                        {
                            sheetV = fpSpread.Sheets[j];
                            break;
                        }
                    }
                    if (sheetV == null)
                    {
                        break;
                    }
                    foreach (JZCell dataCell in sheet.Cells)
                    {
                        Cell cell = sheetV.Cells[dataCell.Name];

                        if (cell != null)
                        {
                            cell.Font = defaultFont;
                            if (cell.CellType is ImageCellType)
                            {
                                if (dataCell.Value != null)
                                {
                                    cell.Value = JZCommonHelper.StringToBitmap(dataCell.Value.ToString());
                                }
                            }
                            else
                            {
                                cell.Value = dataCell.Value;
                            }
                        }
                    }
                }
                try
                {
                    //保存资料到指定目录
                    String reportNumber = docBase.BGBH == "" ? "无报告编号" + i.ToString() : docBase.BGBH;
                    
                    ExcelWarningList ewl = new ExcelWarningList();
                    string fileName = Path + "\\" + moduleName + "-" + reportNumber + ".xls";
                    fpSpread.SaveExcel(fileName, ExcelSaveFlags.NoFlagsSet, ewl);
                }
                catch
                { }
            }
            
        }
    }
}
