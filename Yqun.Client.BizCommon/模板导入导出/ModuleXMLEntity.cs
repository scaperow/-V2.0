using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class ModuleXMLEntity
    {
        public String ID { get; set; }
        public String SCTS { get; set; }
        public String Description { get; set; }
        public String CatlogCode { get; set; }
        public String Sheets { get; set; }
        public String ExtentSheet { get; set; }
        public List<ModuleXMLSheets> SheetsList { get; set; }
        public List<ModuleXMLCrossSheetFormulas> CrossSheetFormulasList { get; set; }
        public List<ModuleXMLModuleView> ModuleViewList { get; set; }
        public ModuleXMLExtTable ExtTable { get; set; }
    }

    [Serializable]
    public class ModuleXMLSheets
    {
        public String ID { get; set; }
        public String SCTS { get; set; }
        public String Description { get; set; }
        public String CatlogCode { get; set; }
        public String DataTable { get; set; }
        public String SheetStyle { get; set; }
        public ModuleXMLTables TableEntity { get; set; }
        public List<ModuleXMLColumn> ColumnList { get; set; }
        public List<ModuleXMLDataArea> DataAreaList { get; set; }
        
    }

    [Serializable]
    public class ModuleXMLTables
    {
        public String ID { get; set; }
        public String SCTS { get; set; }
        public String Description { get; set; }
        public String SCPT { get; set; }
        public String TABLENAME { get; set; }
        public String TABLETYPE { get; set; }
    }

    [Serializable]
    public class ModuleXMLColumn
    {
        public String ID { get; set; }
        public String SCTS { get; set; }
        public String Description { get; set; }
        public String SCPT { get; set; }
        public String ColName { get; set; }
        public String TABLENAME { get; set; }
        public String ColType { get; set; }
        public Int32 IsKeyField { get; set; }
    }

    [Serializable]
    public class ModuleXMLCrossSheetFormulas 
    {
        public String ID { get; set; }
        public String SCTS { get; set; }
        public String ModuleID { get; set; }
        public String SheetID { get; set; }
        public String RowIndex { get; set; }
        public String ColumnIndex { get; set; }
        public String Formula { get; set; }
    }

    [Serializable]
    public class ModuleXMLDataArea
    {
        public String ID { get; set; }
        public String SCTS { get; set; }
        public String TableName { get; set; }
        public String SheetID { get; set; }
        public String ColumnName { get; set; }
        public String Range { get; set; }
    }

    [Serializable]
    public class ModuleXMLModuleView
    {
        public String ID { get; set; }
        public String SCTS { get; set; }
        public String ModuleCode { get; set; }
        public String ModuleID { get; set; }
        public String TableName { get; set; }
        public String TableText { get; set; }
        public String Description { get; set; }
        public String ContentType { get; set; }
        public String ContentFieldType { get; set; }
        public String ContentText { get; set; }
        public String Contents { get; set; }
        public Int32? ForeColor { get; set; }
        public Int32? BgColor { get; set; }
        public String DisplayStyle { get; set; }
        public float? ColumnWidth { get; set; }
        public int? IsVisible { get; set; }
        public int? IsEdit { get; set; }
        public int? IsNull { get; set; }
        public int? OrderIndex { get; set; }
    }

    [Serializable]
    public class ModuleXMLExtTable
    {
        public ModuleXMLTables TableEntity { get; set; }
        public List<ModuleXMLColumn> ColumnList { get; set; }
    }
}
