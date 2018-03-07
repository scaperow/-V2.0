using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizCommon;
using Yqun.Bases;
using FarPoint.Win.Spread;
using FarPoint.Win;

namespace BizComponents
{
    public partial class CellSelector : Form
    {
        Guid ModuleID;
        public String CellName;
        public Guid SheetID;
        public String SheetName;
        public SheetView[] LoadSheets;
        public JZDocument LoadDocument;

        public CellSelector(Guid moduleID, String CellName, Guid SheetIndex)
        {
            InitializeComponent();

            this.ModuleID = moduleID;
            this.CellName = CellName;
            this.SheetID = SheetIndex;

            fpSpread1.CellDoubleClick += new CellClickEventHandler(myCell1_CellDoubleClick);
        }

        public void Preloading(SheetView[] sheets, JZDocument document)
        {
            LoadSheets = sheets;
            LoadDocument = document;
        }

        private SheetView GetLoadedSheet(string sheetName)
        {
            if (LoadSheets != null)
            {
                foreach (var sheet in LoadSheets)
                {
                    if (sheet.SheetName == sheetName)
                    {
                        return sheet;
                    }
                }
            }

            return null;
        }

        private void DataItemSelector_Load(object sender, EventArgs e)
        {
            ProgressScreen.Current.ShowSplashScreen();
            this.AddOwnedForm(ProgressScreen.Current);

            try
            {
                fpSpread1.Sheets.Clear();

                if (LoadDocument == null)
                {
                    LoadDocument = ModuleHelperClient.GetDefaultDocument(ModuleID);
                }

                var sheetsCache = new List<SheetView>();
                foreach (var sheet in LoadDocument.Sheets)
                {
                    ProgressScreen.Current.SetStatus = "正在初始化表单‘" + sheet.Name + "’";
                    var sheetView = GetLoadedSheet(sheet.Name);

                    if (sheetView == null)
                    {
                        var sheetXML = ModuleHelperClient.GetSheetXMLByID(sheet.ID);
                        sheetView = Serializer.LoadObjectXml(typeof(SheetView), sheetXML, "SheetView") as SheetView;
                        sheetView.SheetName = sheet.Name;
                    }

                    sheetView.Tag = sheet.ID;
                    sheetView.OperationMode = OperationMode.ReadOnly;        
                    fpSpread1.Sheets.Add(sheetView);
                    sheetsCache.Add(sheetView);

                    foreach (JZCell dataCell in sheet.Cells)
                    {
                        sheetView.Cells[dataCell.Name].BackColor = Color.LightPink;
                    }
                }

                LoadSheets = sheetsCache.ToArray();

                //if (CellName != ""&&SheetID!=Guid.Empty)
                //{
                //    SheetView s = null;
                //    foreach (SheetView sheet in fpSpread1.Sheets)
                //    {
                //        if (new Guid(sheet.Tag.ToString()) == SheetID)
                //        {
                //            s = sheet;
                //            break;
                //        }
                //    }
                //    if (s != null)
                //    {
                //        fpSpread1.ActiveSheet = s;
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载模板出错！\r\n原因：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.RemoveOwnedForm(ProgressScreen.Current);
            ProgressScreen.Current.CloseSplashScreen();
            Activate();
        }

        void myCell1_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            Button_Ok.PerformClick();
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            if (fpSpread1.ActiveSheet.ActiveCell == null)
            {
                MessageBox.Show("请选择数据区");
                return;
            }
            else
            {
                CellName = fpSpread1.ActiveSheet.ActiveCell.Column.Label + fpSpread1.ActiveSheet.ActiveCell.Row.Label;
                SheetID = new Guid(fpSpread1.ActiveSheet.Tag.ToString());
                SheetName = fpSpread1.ActiveSheet.SheetName;
                
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
