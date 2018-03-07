using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;
using Yqun.Common.Encoder;
using System.Xml;

namespace Yqun.BO.BusinessManager
{
    public class DataTableConsistencyManager : BOBase
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        TableDefineInfoManager TableDefineInfoManager = new TableDefineInfoManager();

        public List<String> GetErrorFieldList(String SheetIndex, String DataTableName)
        {
            List<String> ErrorList = new List<String>();

            StringBuilder sql_select = new StringBuilder();
            //增加查询条件Scdel=0   2013-10-19
            sql_select.Append("select sheetstyle from sys_biz_sheet where Scdel=0 and ID = '");
            sql_select.Append(SheetIndex);
            sql_select.Append("'");

            DataTable SheetData = GetDataTable(sql_select.ToString());
            if (SheetData == null || SheetData.Rows.Count == 0)
            {
                logger.Error(string.Format("没有找到表单的样式xml。", SheetIndex));
                return ErrorList;
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(SheetData.Rows[0]["sheetstyle"].ToString());

            sql_select = new StringBuilder();
            sql_select.Append("SELECT sysobjects.Name as tb_name, syscolumns.Name as col_name, SysTypes.Name as col_type, syscolumns.Length as col_len, isnull(sys.extended_properties.Value,syscolumns.Name) as col_memo,");
            sql_select.Append("       case when syscolumns.name in (select primarykey=a.name FROM syscolumns a inner join sysobjects b on a.id=b.id  and b.xtype='U' and b.name<>'dtproperties' where  exists(SELECT 1 FROM sysobjects where xtype='PK' and name in (SELECT name FROM sysindexes WHERE indid in(SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid))) and b.name=sysobjects.Name)");
            sql_select.Append("       then 1 else 0 end as is_key ");
            sql_select.Append("FROM sysobjects,SysTypes,syscolumns LEFT JOIN sys.extended_properties ON (Syscolumns.Id = sys.extended_properties.major_id AND Syscolumns.Colid = sys.extended_properties.minor_id) ");
            sql_select.Append("WHERE (sysobjects.Xtype ='u' OR Sysobjects.Xtype ='v') AND Sysobjects.Id = Syscolumns.Id AND SysTypes.XType = Syscolumns.XType AND SysTypes.Name <> 'sysname' AND ");
            sql_select.Append("      Sysobjects.Name = '");
            sql_select.Append(DataTableName);
            sql_select.Append("' ");
            sql_select.Append("ORDER By SysObjects.Name, SysColumns.colid");

            DataTable DataSchema = GetDataTable(sql_select.ToString());
            if (DataSchema != null && DataSchema.Rows.Count > 0)
            {
                sql_select = new StringBuilder();
                //增加查询条件Scdel=0     2013-10-19
                sql_select.Append("select * from sys_columns where Scdel=0 and tablename='");
                sql_select.Append(DataTableName);
                sql_select.Append("'");

                TableDefineInfo TableDefineInfo = TableDefineInfoManager.GetTableDefineInfo(SheetIndex, DataTableName);
                if (TableDefineInfo != null)
                {
                    foreach (FieldDefineInfo Field in TableDefineInfo.FieldInfos)
                    {
                        String FieldName = Field.FieldName;
                        FieldType FieldType = Field.FieldType;

                        if (string.IsNullOrEmpty(Field.RangeInfo))
                        {
                            string error = string.Format("表单中的字段数据(FieldName={0})没有数据区！", Field.FieldName);
                            logger.Error(error);
                            ErrorList.Add(error);
                            continue;
                        }

                        int[] Locations = ConvertIntFromRange(Field.RangeInfo);
                        String XPath = String.Format("//CellStyle[@Row='{0}'][@Column='{1}']/CellType", Locations[1], Locations[0]);
                        XmlNode Node = doc.SelectSingleNode(XPath);
                        if (Node == null)
                        {
                            string error = string.Format("使用xpath表达式({0})在表单中查找字段数据(FieldName={1})的单元格类型失败！", XPath, Field.FieldName);
                            logger.Error(error);
                            ErrorList.Add(error);
                            continue;
                        }

                        if (Node.Attributes["class"] == null)
                        {
                            string error = string.Format("表单中的字段(FieldName={0})单元格类型已找到，获取该节点的class属性失败！", Field.FieldName);
                            logger.Error(error);
                            ErrorList.Add(error);
                            continue;
                        }

                        DataRow[] SchemaRows = DataSchema.Select("col_name='" + FieldName + "'");
                        if (SchemaRows.Length > 0)
                        {
                            String col_type = SchemaRows[0]["col_type"].ToString();
                            if (!FieldType.BasicDataType.ToLower().StartsWith(col_type.ToLower()))
                            {
                                string error = string.Format("{0}:[syscolumns]{1} != [sys_columns]{2}", Field.RangeInfo, col_type.ToLower(), FieldType.BasicDataType.ToLower());
                                logger.Error(error);
                                ErrorList.Add(error);
                            }

                            string celltype = GetFieldTypeDescription(Node.Attributes["class"].Value);
                            if (celltype != Field.FieldType.Description)
                            {
                                string error = string.Format("{0}:[sys_columns]{1} != [sheet]{2}", Field.RangeInfo, celltype, Field.FieldType.Description);
                                logger.Error(error);
                                ErrorList.Add(error);
                            }
                        }
                        else
                        {
                            logger.Error(string.Format("sys_columns 中的列 ‘{0}.{1}’没有在物理表 syscolumns 中找到对应项", Field.TableInfo.Name, Field.FieldName));
                        }
                    }
                }
            }
            else
            {
                logger.Error(string.Format("数据表 {0}的字段信息未找到", DataTableName));
            }

            return ErrorList;
        }

        private int[] ConvertIntFromRange(string Range)
        {
            int DigitIndex = Range.Length - 1;
            char[] chars = Range.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (Char.IsDigit(chars[i]))
                {
                    DigitIndex = i;
                    break;
                }
            }

            List<int> Parts = new List<int>();
            Parts.Add(System.Convert.ToInt32(Arabic_Numerals_Convert.ToNumerals(Range.Substring(0, DigitIndex))));
            Parts.Add(System.Convert.ToInt32(Range.Substring(DigitIndex)) - 1);
            return Parts.ToArray();
        }

        private String GetFieldTypeDescription(String fullname)
        {
            if (fullname == "BizComponents.NumberCellType")
            {
                return FieldType.Number.Description;
            }
            else if (fullname == "BizComponents.TextCellType")
            {
                return FieldType.Text.Description;
            }
            else if (fullname == "BizComponents.LongTextCellType")
            {
                return FieldType.LongText.Description;
            }
            else if (fullname == "BizComponents.PercentCellType")
            {
                return FieldType.Percent.Description;
            }
            else if (fullname == "BizComponents.ImageCellType")
            {
                return FieldType.Image.Description;
            }
            else if (fullname == "BizComponents.HyperLinkCellType")
            {
                return FieldType.HyperLink.Description;
            }
            else if (fullname == "BizComponents.CurrencyCellType")
            {
                return FieldType.Currency.Description;
            }
            else if (fullname == "BizComponents.DateTimeCellType")
            {
                return FieldType.DateTime.Description;
            }
            else if (fullname == "BizComponents.CheckBoxCellType")
            {
                return FieldType.CheckBox.Description;
            }
            else if (fullname == "BizComponents.RichTextCellType")
            {
                return FieldType.RichText.Description;
            }
            else if (fullname == "BizComponents.BarCodeCellType")
            {
                return FieldType.BarCode.Description;
            }
            else if (fullname == "BizComponents.DownListCellType")
            {
                return FieldType.DownList.Description;
            }
            else if (fullname == "BizComponents.MaskCellType")
            {
                return FieldType.Mask.Description;
            }

            return "";
        }
    }
}
