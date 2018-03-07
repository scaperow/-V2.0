using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win.Spread;
using System.Data;
using System.Collections;
using ReportCommon;

namespace Yqun.BO.ReportE.Core
{
    internal class DataSourceManager 
    {
        TableDataCollection dataSourceInfo;
        Dictionary<string, DataTable> dataSources = new Dictionary<string, DataTable>();
        Hashtable parameterList;

        BOBase DataService = new BOBase();

        public DataSourceManager(TableDataCollection dataSourceInfo, Hashtable parameterList)
        {
            this.dataSourceInfo = dataSourceInfo;
            this.parameterList = parameterList;
        }

        private void AnalysisParameters(CombineFilterCondition DataFilter, Hashtable Parameters)
        {
            foreach (FilterCondition Filter in DataFilter.FilterConditions)
            {
                if (Filter.RightItem.Style == FilterStyle.Parameter)
                {
                    ReportParameter Parameter = Parameters[Filter.RightItem.ParameterName] as ReportParameter;
                    if (Filter.CompareOperation.ToString() == CompareOperation.属于.ToString() || Filter.CompareOperation.ToString() == CompareOperation.不属于.ToString())
                    {
                        Filter.RightItem.ParameterName = string.Concat("('", Parameter.Value.ToString().Replace(",", "','"), "')");
                    }
                    else if (Filter.CompareOperation.ToString() == CompareOperation.等于.ToString() || 
                             Filter.CompareOperation.ToString() == CompareOperation.不等于.ToString() ||
                             Filter.CompareOperation.ToString() == CompareOperation.大于.ToString() ||
                             Filter.CompareOperation.ToString() == CompareOperation.大于或等于.ToString() ||
                             Filter.CompareOperation.ToString() == CompareOperation.小于.ToString() ||
                             Filter.CompareOperation.ToString() == CompareOperation.小于或等于.ToString())
                    {
                        Filter.RightItem.ParameterName = string.Concat("'", Parameter.Value.ToString(), "'");
                    }
                }
            }
        }

        public void InitTableData()
        {
            dataSources.Clear();
            foreach (TableData Data in dataSourceInfo)
            {
                AnalysisParameters(Data.DataFilter, parameterList);

                if (Data is DbTableData)
                {
                    DbTableData data = Data as DbTableData;
                    data.DataAdapter = new BackstageDataAdapter();
                    dataSources.Add(data.GetTableText().ToLower(), data.GetDataSource());
                }
                else if (Data is JoinTableData)
                {
                    JoinTableData data = Data as JoinTableData;
                    data.DataAdapter = new BackstageDataAdapter();
                    dataSources.Add(data.GetTableText().ToLower(), data.GetDataSource());
                }
                else
                {
                    dataSources.Add(Data.GetTableText().ToLower(), Data.GetDataSource());
                }
            }
        }

        public DataTable GetDataTable(String TableData)
        {
            DataTable Result = null;
            if (dataSources.ContainsKey(TableData.ToLower()))
            {
                Result = dataSources[TableData.ToLower()];
                return Result;
            }

            throw new DataSourceNotFoundException("在报表数据集中没有找到数据表 ‘" + TableData + "’。");
        }

        public DataView FilterDataTable(BE_Extend paramBE_Extend, DataTable paramDataTable, GridElement Element)
        {
            DataTable ds = GetFilterTable(paramBE_Extend, paramDataTable, Element, true);
            DataTable paramData = (ds != null ? ds : paramDataTable);
            DataTable result = GetFilterTable(paramBE_Extend, paramData, Element, false);
            return result.DefaultView;
        }

        private DataTable GetFilterTable(BE_Extend paramBE_Extend, DataTable paramDataTable, GridElement Element, Boolean IsLeftParent)
        {
            if (paramBE_Extend == null)
            {
                return new DataTable();
            }
            else
            {
                List<DataTable> ParentDataTable = new List<DataTable>();
                List<DataTable> SubDataTable = new List<DataTable>();
                List<String> ParentPrimaryColumns = new List<string>();
                List<String> ChildPrimaryColumns = new List<string>();
                Dictionary<String, List<String>> RowFilters = new Dictionary<string, List<string>>();

                ReportCommon.DataColumn DataColumn = Element.Value as ReportCommon.DataColumn;

                GridElement ChildElement = Element;
                ReportCommon.DataColumn ChildColumn = ChildElement.Value as ReportCommon.DataColumn;
                PE paramPE = paramBE_Extend;

                while (paramPE != null)
                {
                    GridElement ParentElement = null;
                    ReportCommon.DataColumn ParentColumn = null;
                    if (paramPE is BE_Extend)
                    {
                        BE_Extend temp = paramPE as BE_Extend;
                        ParentElement = temp.beb.ce_from as GridElement;
                        ParentColumn = ParentElement.Value as ReportCommon.DataColumn;
                    }
                    else if (paramPE is CE_Extend)
                    {
                        CE_Extend temp = paramPE as CE_Extend;
                        ParentElement = temp.be_from.beb.ce_from as GridElement;
                        ParentColumn = ParentElement.Value as ReportCommon.DataColumn;
                    }

                    if (ParentElement == null || ParentColumn == null)
                        break;

                    if (IsLeftParent)
                    {
                        if (Element.ExpandOrientation.Orientation == ExpandOrientation.TopToBottom &&
                            ParentElement.ExpandOrientation.Orientation == ExpandOrientation.LeftToRight)
                            break;
                    }
                    else
                    {
                        if (Element.ExpandOrientation.Orientation == ExpandOrientation.LeftToRight &&
                            ParentElement.ExpandOrientation.Orientation == ExpandOrientation.TopToBottom)
                            break;
                    }

                    if (!RowFilters.ContainsKey(ParentColumn.TableName.ToLower()))
                    {
                        RowFilters.Add(ParentColumn.TableName.ToLower(), new List<String>());
                    }

                    DataTable ds = (ParentColumn.TableName.ToLower() == paramDataTable.TableName.ToLower() ? paramDataTable : dataSources[ParentColumn.TableName.ToLower()]);
                    ParentDataTable.Add(ds);

                    String strFilter = "";
                    if (paramPE is CE_Extend)
                    {
                        CE_Extend temp = paramPE as CE_Extend;
                        strFilter = GetField_ValueString(ds, ParentColumn.FieldName, CompareOperation.等于, temp.obj);
                    }
                    RowFilters[ParentColumn.TableName.ToLower()].Add(strFilter);

                    DataTable subds = (ChildColumn.TableName.ToLower() == paramDataTable.TableName.ToLower() ? paramDataTable : dataSources[ChildColumn.TableName.ToLower()]);
                    SubDataTable.Add(subds);

                    List<FilterCondition> FilterConditions = ChildColumn.DataFilter.FilterConditions;
                    Boolean HaveRelation = true;
                    foreach (FilterCondition FilterCondition in FilterConditions)
                    {
                        String str;
                        Item Item = FilterCondition.RightItem;
                        switch (Item.Style)
                        {
                            case FilterStyle.DataColumn:
                                ChildPrimaryColumns.Add(FilterCondition.LeftItem.FieldName);
                                ParentPrimaryColumns.Add(FilterCondition.RightItem.FieldName);
                                break;
                            case FilterStyle.Value:
                                str = GetField_ValueString(subds, FilterCondition.LeftItem.FieldName, FilterCondition.CompareOperation, Item.Value);
                                RowFilters[ChildColumn.TableName.ToLower()].Add(str);
                                break;
                            case FilterStyle.Parameter:
                                ReportParameter param = parameterList[Item.ParameterName] as ReportParameter;
                                str = GetField_ValueString(subds, FilterCondition.LeftItem.FieldName, FilterCondition.CompareOperation, param.Value);
                                RowFilters[ChildColumn.TableName.ToLower()].Add(str);
                                break;
                        }

                        HaveRelation = HaveRelation && Item.Style == FilterStyle.DataColumn;
                    }

                    if (!HaveRelation || ChildColumn.DataFilter.FilterConditions.Count == 0)
                    {
                        ChildPrimaryColumns.Add("");
                        ParentPrimaryColumns.Add("");
                    }

                    ChildElement = ParentElement;
                    ChildColumn = ChildElement.Value as ReportCommon.DataColumn;

                    if (paramPE is BE_Extend)
                    {
                        BE_Extend temp = paramPE as BE_Extend;
                        paramPE = (IsLeftParent ? temp.left : temp.up);
                    }
                    else if (paramPE is CE_Extend)
                    {
                        CE_Extend temp = paramPE as CE_Extend;
                        paramPE = (IsLeftParent ? temp.be_from.left : temp.be_from.up);
                    }
                }

                if (ParentDataTable.Count > 0)
                {
                    DataTable TemTable = ParentDataTable[ParentDataTable.Count - 1];
                    for (int i = ParentDataTable.Count - 1; i >= 0; i--)
                    {
                        List<String> Filters = RowFilters[TemTable.TableName.ToLower()];
                        List<String> temp = new List<string>();
                        foreach (String str in Filters)
                        {
                            if (str != "")
                                temp.Add(str);
                        }
                        String strFilters = "";
                        if (temp.Count > 0)
                            strFilters = string.Join(" and ", temp.ToArray());

                        TemTable = GetRelationTable(TemTable, SubDataTable[i], ParentPrimaryColumns[i], ChildPrimaryColumns[i], strFilters);
                    }

                    DataTable Result = TemTable.DefaultView.ToTable();
                    return Result;
                }
                else
                {
                    DataTable Result = dataSources[DataColumn.TableName.ToLower()].DefaultView.ToTable();
                    return Result;
                }
            }
        }

        private DataTable GetRelationTable(DataTable PrimaryTable, DataTable SubTable, String PrimaryColumns, String SubColumns, String RowFilterString)
        {
            if (PrimaryTable.TableName.ToLower() == SubTable.TableName.ToLower() ||
                (PrimaryColumns == "" && SubColumns == ""))
            {
                PrimaryTable.DefaultView.RowFilter = RowFilterString;
                return PrimaryTable.DefaultView.ToTable();
            }
            else
            {
                DataTable primaryTable = PrimaryTable.Copy();
                DataTable subTable = SubTable.Copy();

                DataSet newSet = new DataSet();
                newSet.Tables.Add(primaryTable);
                newSet.Tables.Add(subTable);
                DataRelation TableRelation;
                TableRelation = new DataRelation("", primaryTable.Columns[PrimaryColumns], subTable.Columns[SubColumns]);
                DataTable NewTable = subTable.Clone();

                primaryTable.DefaultView.RowFilter = RowFilterString;
                for (int i = 0; i < primaryTable.DefaultView.Count; i++)
                {
                    DataRowView drv = primaryTable.DefaultView[i];
                    DataView d = drv.CreateChildView(TableRelation);
                    foreach (DataRow Row in d.ToTable().Rows)
                    {
                        NewTable.ImportRow(Row);
                    }
                }
                return NewTable;
            }
        }

        private String GetField_ValueString(DataTable source, String field, CompareOperation compareOperation, object value)
        {
            String Result = "";
            if (value != null)
            {
                if (value != PrimitiveValue.NULL)
                {
                    Type type = source.Columns[field].DataType;
                    if (value == DBNull.Value)
                    {
                        Result = string.Format("[{0}] is null", field);
                    }
                    else if (type.FullName.ToLower().Contains("int") || type.FullName.ToLower().Contains("decimal"))
                    {
                        Result = string.Format("[{0}]{1}{2}", field, compareOperation.Value, Convert.ToDouble(value));
                    }
                    else if (type.FullName.ToLower().Contains("datetime"))
                    {
                        DateTime datetime = Convert.ToDateTime(value);
                        string operation = "=";
                        if (compareOperation.ToString() == CompareOperation.大于.ToString())
                        {
                            operation = ">=";
                            //datetime = datetime.AddDays(1);
                        }
                        else if (compareOperation.ToString() == CompareOperation.大于或等于.ToString())
                        {
                            operation = ">=";
                        }
                        else if (compareOperation.ToString() == CompareOperation.小于.ToString())
                        {
                            operation = "<";
                        }
                        else if (compareOperation.ToString() == CompareOperation.小于或等于.ToString())
                        {
                            operation = "<";
                            //datetime = datetime.AddDays(1);
                        }

                        Result = string.Format("[{0}]{1}'{2}'", field, operation, datetime.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                    }
                    else if (type.FullName.ToLower().Contains("dbnull"))
                    {
                        Result = string.Format("[{0}] is null", field);
                    }
                    else
                    {
                        String Format;
                        if (compareOperation.ToString() == CompareOperation.包含.ToString() || compareOperation.ToString() == CompareOperation.不包含.ToString())
                        {
                            Format = "[{0}] {1} '%{2}%'";
                            String[] values = Convert.ToString(value).Split(',');
                            List<string> expressions = new List<string>();
                            foreach (String v in values)
                            {
                                expressions.Add(string.Format(Format, field, compareOperation.Value, v));
                            }

                            Result = string.Concat("(", string.Join(" or ", expressions.ToArray()), ")");
                        }
                        else if (compareOperation.ToString() == CompareOperation.始于.ToString() || compareOperation.ToString() == CompareOperation.不始于.ToString())
                        {
                            Format = "[{0}] {1} '{2}%'";
                            String[] values = Convert.ToString(value).Split(',');
                            List<string> expressions = new List<string>();
                            foreach (String v in values)
                            {
                                expressions.Add(string.Format(Format, field, compareOperation.Value, v));
                            }

                            Result = string.Concat("(", string.Join(" or ", expressions.ToArray()), ")");
                        }
                        else if (compareOperation.ToString() == CompareOperation.是.ToString() || compareOperation.ToString() == CompareOperation.不是.ToString())
                        {
                            Result = string.Concat(field, compareOperation.Value, " null ");
                        }
                        else
                        {
                            Format = "[{0}]{1}'{2}'";
                            Result = string.Format(Format, field, compareOperation.Value, Convert.ToString(value).Trim('\'', '\"'));
                        }
                    }
                }
                else
                {
                    Result = "1<>1";
                }
            }

            return Result;
        }
    }
}
