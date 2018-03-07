using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarPoint.Win.Spread;
using BizCommon;
using FarPoint.Win;

namespace TestDataUploadWS
{
    public class ModuleCatch
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Dictionary<Guid, FpSpread> moduleList = new Dictionary<Guid, FpSpread>();

        public static FpSpread GetModuleByID(Guid moduleID, JZDocument document, String DBName)
        {
            if (moduleList.ContainsKey(moduleID))
            {
                return moduleList[moduleID];
            }
            else
            {
                FpSpread fpSpread = new FpSpread();
                try
                {
                    
                    Dictionary<Guid, SheetView> SheetCollection = new Dictionary<Guid, SheetView>();
                    List<FarPoint.CalcEngine.FunctionInfo> Infos = DBHelper.CallLocalService("Yqun.BO.BusinessManager.dll", "GetFunctionInfosNew", new object[] { }, DBName) as List<FarPoint.CalcEngine.FunctionInfo>;
                    
                    foreach (JZSheet sheet in document.Sheets)
                    {
                        String xml = DBHelper.CallLocalService("Yqun.BO.BusinessManager.dll", "GetSheetXMLByID", new object[] { sheet.ID }, DBName).ToString();
                        String sheetXML = JZCommonHelper.GZipDecompressString(xml);
                        SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), sheetXML, "SheetView") as SheetView;
                        SheetView.Tag = sheet.ID;
                        SheetView.Cells[0, 0].Value = "";
                        SheetView.Protect = true;

                        fpSpread.Sheets.Add(SheetView);

                        SheetCollection.Add(sheet.ID, SheetView);

                        foreach (FarPoint.CalcEngine.FunctionInfo Info in Infos)
                        {
                            SheetView.AddCustomFunction(Info);
                        }
                    }
                    moduleList.Add(moduleID, fpSpread);
                }
                catch (Exception ex)
                {
                    logger.Error("采集构造Farpoint组件错误：" + ex.Message);
                    return null;
                }
                return fpSpread;
            }
        }
    }
}
