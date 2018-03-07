using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win.Spread;
using System.Collections;
using System.Data;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread.Model;
using ReportCommon;
using System.Windows.Forms;
using FarPoint.Win;
using System.Drawing;
using ReportCommon.Chart;

namespace Yqun.BO.ReportE.Core
{
    public class ReportEngine
    {
        //使用log4net.dll日志接口实现日志记录
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal SheetView report;
        internal DataSourceManager dataSourceManager;
        internal Calculator calculator;
        internal int rowCount;
        internal int columnCount;
        internal FamilyMember[,] genealogy;
        internal BEB[,] be_beb_2D;
        internal BE_BESet_M be_relation;
        internal Boolean[,] states;
        internal Grid2D grid2D;
        internal ArrayList executing_columnrow_list = new ArrayList();
        internal Hashtable columnrow_levels = new Hashtable();
        internal Hashtable parameterList = new Hashtable();

        internal static readonly Object CUR_RE = new Object();
        internal static readonly Object BE_CC_LIST = new Object();
        internal static readonly Object BE_CC_SET = new Object();
        internal static readonly Object CUR_CE_LIST = new Object();

        private Sort_gen_top_Comparator sort_gen_top_Comparator = new Sort_gen_top_Comparator();
        private Sort_gen_left_Comparator sort_gen_left_Comparator = new Sort_gen_left_Comparator();

        public ReportEngine(TableDataCollection DataSources, SheetView paramReport, Hashtable parameterList)
        {
            this.report = paramReport;
            this.parameterList = parameterList;
            this.dataSourceManager = new DataSourceManager(DataSources, parameterList);
            this.calculator = new Calculator(paramReport);
            this.calculator.setAttribute(CUR_RE, this);
            this.calculator.setAttribute(BE_CC_SET, new ArrayList());
            this.calculator.setAttribute(BE_CC_LIST, new ArrayList());
            this.calculator.setAttribute(CUR_CE_LIST, new ArrayList());
        }

        private CellRange getRectangle(int row, int col)
        {
            CellRange cr = report.GetSpanCell(row, col);
            if (cr == null)
            {
                Cell Cell = report.Cells[row,col];
                cr = new CellRange(Cell.Row.Index, Cell.Column.Index, Cell.RowSpan, Cell.ColumnSpan);
            }

            return cr;
        }

        public PageReport execute()
        {
            //GetLastNonEmptyRow方法获得是某一行或某一列的索引，所以行数(列数) = 行(列)索引号 + 1
            this.rowCount = Math.Max(report.GetLastNonEmptyRow(NonEmptyItemFlag.Data),report.GetLastNonEmptyRow(NonEmptyItemFlag.Style)) + 1;
            this.columnCount = Math.Max(report.GetLastNonEmptyColumn(NonEmptyItemFlag.Data), report.GetLastNonEmptyColumn(NonEmptyItemFlag.Style)) + 1;

            for (int m = 0; m < this.rowCount; m++)
            {
                for (int n = 0; n < this.columnCount; n++)
                {
                    if (report.Cells[m, n].Value is GridElement)
                    {
                        this.columnCount = Math.Max(this.columnCount, report.Cells[m, n].Column.Index + report.Cells[m, n].ColumnSpan);
                        this.rowCount = Math.Max(this.rowCount, report.Cells[m, n].Row.Index + report.Cells[m, n].RowSpan);
                    }
                }
            }

            foreach (IElement Element in report.DrawingContainer.ContainedObjects)
            {
                if (Element is FloatElement)
                {
                    FloatElement localFloatElement = Element as FloatElement;
                    Point[] arrayOfPoint = ReportHelper.calculateLastColumnAndRowOfFloatElement(this.report, localFloatElement);
                    this.columnCount = Math.Max(this.columnCount, arrayOfPoint[0].X);
                    this.rowCount = Math.Max(this.rowCount, arrayOfPoint[0].Y);
                }
            }

            Object[,] Elements = new Object[this.rowCount, this.columnCount];
            for (int m = 0; m < this.rowCount; m++)
            {
                for (int n = 0; n < this.columnCount; n++)
                {
                    if (report.Cells[m, n].Value is GridElement)
                    {
                        GridElement Element = report.Cells[m, n].Value as GridElement;

                        CellRange rect = getRectangle(m, n);
                        Element.Row = rect.Row;
                        Element.Column = rect.Column;
                        Element.RowSpan = rect.RowCount;
                        Element.ColumnSpan = rect.ColumnCount;

                        Element.Style.ForeColor = report.Cells[m, n].ForeColor;
                        Element.Style.BackColor = report.Cells[m, n].BackColor;
                        Element.Style.Font = report.Cells[m, n].Font;
                        Element.Style.HorizontalAlignment = Convert.ToInt32(report.Cells[m, n].HorizontalAlignment);
                        Element.Style.VerticalAlignment = Convert.ToInt32(report.Cells[m, n].VerticalAlignment);
                        Element.Style.Border = report.Cells[m, n].Border;

                        Element.Report = report;
                        Elements[m, n] = Element;
                    }
                }
            }
            dataSourceManager.InitTableData();
            SheetUtil.calculateReportDefaultParent(report, Elements);
            buildGenealogy(Elements);
            cc_columnrow_level(genealogy, columnrow_levels);
            this.be_beb_2D = new BEB[this.rowCount, this.columnCount];
            this.be_relation = new BE_BESet_M();
            int i = 0;
            int j = this.genealogy.GetLength(0);
            while (i < j)
            {
                int k = 0;
                int m = this.genealogy.GetLength(1);
                while (k < m)
                {
                    initBEB(this.genealogy[i, k] as FamilyMember);
                    k++;
                }
                i++;
            }
            this.states = new Boolean[this.rowCount, this.columnCount];
            for (int m = 0; m < Elements.GetLength(0); m++)
            {
                for (int n = 0; n < Elements.GetLength(1); n++)
                {
                    GridElement Element = Elements[m, n] as GridElement;
                    if (Element != null)
                    {
                        calculateCellElement(Element);
                    }
                }
            }

            this.grid2D = new Grid2D(this, this.rowCount + 1, this.columnCount + 1);
            expandGrid(this.grid2D);
            PageReport localObject1 = this.grid2D.ToReport(this.report, true, true, true);
            return localObject1;
        }

        private void buildGenealogy(Object[,] Elements)
        {
            this.genealogy = new FamilyMember[this.rowCount, this.columnCount];
            ArrayList localArrayList = new ArrayList();
            for (int i = 0; i < Elements.GetLength(0); i++)
            {
                for (int j = 0; j < Elements.GetLength(1); j++)
                {
                    GridElement Element = Elements[i, j] as GridElement;
                    if (Element != null)
                    {
                        buildFamilyMember(Element, localArrayList);
                    }
                }
            }
        }

        private FamilyMember buildFamilyMember(GridElement Element, ArrayList paramList)
        {
            if (Element == null)
                return null;

            if (this.genealogy[Element.Row, Element.Column] != null)
                return this.genealogy[Element.Row, Element.Column];
            if (paramList.Contains(Element))
            {
                paramList.Add(Element);
                throw new DeathCycleException("死循环出现在父元素: " + Coords.GetColumn_Row(Element.Row, Element.Column));
            }
            paramList.Add(Element);
            FamilyMember localFamilyMember1 = null;
            FamilyMember localFamilyMember2 = null;
            Object localObject;
            if (Element.ExpandOrientation.LeftParent != "" && report.Cells[Element.ExpandOrientation.LeftParent] != null)
            {
                localObject = report.Cells[Element.ExpandOrientation.LeftParent].Value as GridElement;
                localFamilyMember1 = buildFamilyMember((GridElement)localObject, paramList);
            }
            if (Element.ExpandOrientation.TopParent != "" && report.Cells[Element.ExpandOrientation.TopParent] != null)
            {
                localObject = report.Cells[Element.ExpandOrientation.TopParent].Value as GridElement;
                localFamilyMember2 = buildFamilyMember((GridElement)localObject, paramList);
            }
            localObject = new FamilyMember(Element, localFamilyMember1, localFamilyMember2);
            this.genealogy[Element.Row, Element.Column] = localObject as FamilyMember;
            paramList.Remove(Element);
            if (localFamilyMember1 != null)
            {
                if (localFamilyMember1.sons == null)
                    localFamilyMember1.sons = new ArrayList();
                localFamilyMember1.sons.Add(localObject);
            }
            if (localFamilyMember2 != null)
            {
                if (localFamilyMember2.sons == null)
                    localFamilyMember2.sons = new ArrayList();
                localFamilyMember2.sons.Add(localObject);
            }

            return (FamilyMember)localObject;
        }

        private void cc_columnrow_level(FamilyMember[,] genealogy, Hashtable columnrow_level)
        {
            int i = 0;
            int j = this.genealogy.GetLength(0);
            while (i < j)
            {
                int k = 0;
                int m = this.genealogy.GetLength(1);
                while (k < m)
                {
                    FamilyMember localFamilyMember = this.genealogy[i,k];
                    if (localFamilyMember != null)
                    {
                        cc_columnrow_level(localFamilyMember, columnrow_level);
                    }
                    k++;
                }
                i++;
            }
        }

        private int cc_columnrow_level(FamilyMember paramFamilyMember, Hashtable columnrow_level)
        {
            int localInteger = -1;
            String literalCell = paramFamilyMember.current.getLiteralCell();
            if (columnrow_level.Contains(literalCell))
            {
                localInteger = (int)columnrow_level[literalCell];
            }
            if (localInteger != -1)
                return localInteger;
            int i = 0;
            if (paramFamilyMember.leftParent != null)
                i = Math.Max(i, cc_columnrow_level(paramFamilyMember.leftParent, columnrow_level) + 1);
            if (paramFamilyMember.upParent != null)
                i = Math.Max(i, cc_columnrow_level(paramFamilyMember.upParent, columnrow_level) + 1);
            columnrow_level.Add(literalCell, i);
            return i;
        }

        private BEB initBEB(FamilyMember paramFamilyMember)
        {
            if (paramFamilyMember == null)
                return null;
            GridElement localElement = paramFamilyMember.current;
            BEB localBEB = this.be_beb_2D[localElement.Row, localElement.Column];
            if (localBEB != null)
                return localBEB;
            BE localBE1 = null;
            BE localBE2 = null;
            if (paramFamilyMember.leftParent != null)
                localBE1 = initBEB(paramFamilyMember.leftParent).get_be_array()[0];
            if (paramFamilyMember.upParent != null)
                localBE2 = initBEB(paramFamilyMember.upParent).get_be_array()[0];
            Object localObject;
            if (localElement.Value is LiteralText || localElement.Value is Slash || localElement.Value is ReportCommon.Picture)
                localObject = new BE(localBE1, localBE2);
            else
                localObject = new BE_Extend(localBE1, localBE2);
            localBEB = this.be_beb_2D[localElement.Row, localElement.Column] = new BEB(new BE[] { (BE)localObject }, localElement);
            this.be_relation.push_son(localBE1, (BE)localObject);
            this.be_relation.push_son(localBE2, (BE)localObject);
            return (BEB)localBEB;
        }

        internal BEB getBEB5BEB2D(GridElement Element, Boolean paramBoolean)
        {
            return getBEB5BEB2D(Element.Row, Element.Column, paramBoolean);
        }

        internal BEB getBEB5BEB2D(int paramRow, int paramColumn, Boolean paramBoolean)
        {
            BEB localBEB = null;
            if ((paramRow < this.rowCount) && (paramRow >= 0) && (paramColumn < this.columnCount) && (paramColumn >= 0))
                localBEB = this.be_beb_2D[paramRow, paramColumn];
            if ((localBEB != null) || (paramBoolean))
                return localBEB;
            return new BEB(new BE[0], null);
        }

        internal static Object cv_of_ce_array(CE[] paramArrayOfCE, Boolean paramBoolean)
        {
            if ((paramArrayOfCE == null) || (paramArrayOfCE.Length == 0))
                return null;

            if (paramArrayOfCE.Length == 1)
            {
                object localObject = paramArrayOfCE[0].ce_value();
                return localObject;
            }

            Object[] localObject1 = new Object[paramArrayOfCE.Length];
            for (int i = 0; i < localObject1.Length; i++)
            {
                Object localObject2 = paramArrayOfCE[i].ce_value();
                localObject1[i] = localObject2;
            }

            return localObject1;
        }

        private void calculateCellElement(GridElement Element)
        {
            if (Element == null)
                return;

            if (this.states[Element.Row, Element.Column] != false)
                return;

            String literalCell = Element.getLiteralCell();
            if (this.executing_columnrow_list.Contains(literalCell))
            {
                this.executing_columnrow_list.Add(literalCell);
                throw new DeathCycleException("死循环出现在: " + literalCell);
            }
            this.executing_columnrow_list.Add(literalCell);
            
            ArrayList localArrayList = new ArrayList();
            localArrayList.Clear();
            FamilyMember localFamilyMember = this.genealogy[Element.Row, Element.Column];
            if (localFamilyMember.leftParent != null)
                calculateCellElement(localFamilyMember.leftParent.current);
            if (localFamilyMember.upParent != null)
                calculateCellElement(localFamilyMember.upParent.current);
            BEB localBEB = getBEB5BEB2D(Element, false);
            BE[] arrayOfBE = localBEB.get_be_array();
            for (int i = 0; i < arrayOfBE.Length; i++)
            {
                arrayOfBE[i].cc_ce_array(this.calculator);
            }
            refresh_be_array_relation(arrayOfBE);
            this.executing_columnrow_list.Remove(literalCell);
            this.states[Element.Row, Element.Column] = true;
        }

        private void refresh_be_array_relation(BE[] paramArrayOfBE)
        {
            Hashtable localHashtable = new Hashtable();
            for (int i = 0; i < paramArrayOfBE.Length; i++)
                refresh_be_relation(paramArrayOfBE[i], localHashtable);
            for (int i = 0; i < paramArrayOfBE.Length; i++)
                this.be_relation.delete_sons(paramArrayOfBE[i]);
            foreach (Object obj in localHashtable.Keys)
            {
                GridElement Element = obj as GridElement;
                ArrayList localList = localHashtable[Element] as ArrayList;
                BEB localBEB = getBEB5BEB2D(Element.Row, Element.Column, true);
                if (localBEB == null)
                    continue;
                localBEB.set_be_array((BE[])localList.ToArray(typeof(BE)));
            }
        }

        private void refresh_be_relation(BE paramBE, Hashtable paramHashtable)
        {
            Hashtable localHashtable = new Hashtable();
            localHashtable.Add(paramBE, paramBE.ce_array);
            ArrayList localList = this.be_relation.posterity(paramBE);
            int i = paramBE.ce_array.Length;
            BE localObject1;
            BE[] localObject2;
            GridElement localObject3;
            ArrayList localObject4;
            foreach (Object obj in localList)
            {
                localObject1 = obj as BE;
                localObject2 = new BE[i];
                for (int j = 0; j < i; j++)
                {
                    try
                    {
                        localObject2[j] = ((BE)localObject1.Clone());
                    }
                    catch
                    {
                        localObject2[j] = localObject1;
                    }
                }

                localHashtable.Add(localObject1, localObject2);
                localObject3 = localObject1.get_ce_from();
                localObject4 = null;
                if (!paramHashtable.ContainsKey(localObject3))
                {
                    localObject4 = new ArrayList();
                    paramHashtable.Add(localObject3, localObject4);
                }
                else
                {
                    localObject4 = paramHashtable[localObject3] as ArrayList;
                }

                ((ArrayList)localObject4).AddRange(localObject2 as ICollection);
            }

            Object object1, object2;
            foreach (Object obj in localHashtable.Keys)
            {
                object1 = obj as BE;
                object2 = localHashtable[obj];
                if (!(object2 is BE[]))
                    continue;
                refresh_parent_relation(((BE)object1).left, (BE)object1, (BE[])object2, localHashtable, true);
                refresh_parent_relation(((BE)object1).up, (BE)object1, (BE[])object2, localHashtable, false);
            }
        }

        private void refresh_parent_relation(PE paramPE, BE paramBE, BE[] paramArrayOfBE, Hashtable paramMap, Boolean paramBoolean)
        {
            if (paramPE == null)
                return;

            Object localObject;
            if ((paramPE is CE))
            {
                localObject = (CE)paramPE;
                ((CE)localObject).replace_son_be(paramBE, paramArrayOfBE);
                return;
            }
            localObject = (BE)paramPE;
            PE[] arrayOfPE = null;
            int i;
            if (paramMap.ContainsKey(localObject))
            {
                arrayOfPE = paramMap[localObject] as PE[];
            }

            if (arrayOfPE != null)
            {
                this.be_relation.delete_sons((BE)localObject);
                for (i = 0; i < paramArrayOfBE.Length; i++)
                {
                    PE localPE = arrayOfPE[i];
                    BE localBE = paramArrayOfBE[i];
                    if (paramBoolean)
                        localBE.left = localPE;
                    else
                        localBE.up = localPE;
                    if (!(localPE is CE))
                        continue;
                    ((CE)localPE).store_son_be(localBE);
                }
                if ((arrayOfPE is BE[]))
                    for (i = 0; i < paramArrayOfBE.Length; i++)
                        this.be_relation.push_son((BE)arrayOfPE[i], paramArrayOfBE[i]);
            }
            else
            {
                this.be_relation.remove_son_be((BE)localObject, paramBE);
                for (i = 0; i < paramArrayOfBE.Length; i++)
                    this.be_relation.push_son((BE)localObject, paramArrayOfBE[i]);
            }
        }

        private void expandGrid(Grid2D paramGrid2D)
        {
            Hashtable localHashtable = new Hashtable();
            ArrayList[] arrayOfList = cc_genealogy_hi(localHashtable);
            for (int i = 0; i < arrayOfList.Length; i++)
                ex_cc_floor(arrayOfList[i], localHashtable, paramGrid2D);
        }

        private void ex_cc_floor(ArrayList paramList, Hashtable paramHashtable, Grid2D paramGrid2D)
        {
            ArrayList localArrayList1 = new ArrayList();
            ArrayList localArrayList2 = new ArrayList();
            ArrayList localArrayList3 = new ArrayList();
            int i = 0;
            int j = paramList.Count;
            while (i < j)
            {
                FamilyMember localFamilyMember = (FamilyMember)paramList[i];
                ArrayList localArrayList4;
                switch (localFamilyMember.current.ExpandOrientation.Orientation)
                {
                    case 3:
                        localArrayList4 = localArrayList1;
                        break;
                    case 2:
                        localArrayList4 = localArrayList2;
                        break;
                    default:
                        localArrayList4 = localArrayList3;
                        break;
                }

                BE[] arrayOfBE = getBEB5BEB2D(localFamilyMember.current, false).get_be_array();
                Hashtable localHashtable = new Hashtable();
                Rect localRect = (Rect)paramHashtable[localFamilyMember];
                for (int k = 0; k < arrayOfBE.Length; k++)
                {
                    BE localBE = arrayOfBE[k];
                    GridC localBoxC = be_column(localBE, localFamilyMember.current.Column, paramGrid2D);
                    GridR localBoxR = be_row(localBE, localFamilyMember.current.Row, paramGrid2D);
                    DyRectCEList localDyRectCEList = null;
                    if (localHashtable.Contains(Coords.GetColumn_Row(localBoxR.i, localBoxC.i)))
                    {
                        localDyRectCEList = (DyRectCEList)localHashtable[Coords.GetColumn_Row(localBoxR.i, localBoxC.i)];
                    }
                    if (localDyRectCEList == null)
                    {
                        localDyRectCEList = new DyRectCEList(localBoxC, localBoxR, be_column(localBE, localRect.left, paramGrid2D), be_row(localBE, localRect.top, paramGrid2D), be_column(localBE, localRect.right, paramGrid2D), be_row(localBE, localRect.bottom, paramGrid2D));
                        localHashtable.Add(Coords.GetColumn_Row(localBoxR.i, localBoxC.i), localDyRectCEList);
                        localArrayList4.Add(localDyRectCEList);
                    }

                    localDyRectCEList.push_ce_array(localBE.ce_array);
                }
                i++;
            }
            ex_t2b(localArrayList1, paramGrid2D);
            ex_l2r(localArrayList2, paramGrid2D);
            ex_steady(localArrayList3, paramGrid2D);
        }

        private ArrayList[] cc_genealogy_hi(Hashtable paramHashtable)
        {
            ArrayList localArrayList = new ArrayList();
            Hashtable localHashtable = new Hashtable();
            int i = 0;
            int j = this.genealogy.GetLength(0);
            while (i < j)
            {
                int k = 0;
                int m = this.genealogy.GetLength(1);
                while (k < m)
                {
                    FamilyMember localFamilyMember = this.genealogy[i,k];
                    if (localFamilyMember != null)
                    {
                        cc_member_hi(localFamilyMember, localHashtable, localArrayList);
                        cc_member_rect(localFamilyMember, paramHashtable);
                    }
                    k++;
                }
                i++;
            }
            return (ArrayList[])localArrayList.ToArray(typeof(ArrayList));
        }

        private Rect cc_member_rect(FamilyMember paramFamilyMember, Hashtable paramHashtable)
        {
            Rect localRect = null;
            if (paramHashtable.Contains(paramFamilyMember))
            {
                localRect = (Rect)paramHashtable[paramFamilyMember];
            }
            if (localRect != null)
                return localRect;
            GridElement localElement = paramFamilyMember.current;
            localRect = new Rect(localElement.Column, localElement.Row, localElement.Column + localElement.ColumnSpan - 1, localElement.Row + localElement.RowSpan - 1);
            ArrayList localList = paramFamilyMember.sons;
            int i = 0;
            int j = localList == null ? 0 : localList.Count;
            while (i < j)
            {
                localRect.union(cc_member_rect((FamilyMember)localList[i], paramHashtable));
                i++;
            }
            paramHashtable.Add(paramFamilyMember, localRect);
            return localRect;
        }

        private int cc_member_hi(FamilyMember paramFamilyMember, Hashtable paramHashtable, ArrayList paramList)
        {
            int localInteger = -1;
            if (paramHashtable.Contains(paramFamilyMember))
            {
                localInteger = (int)paramHashtable[paramFamilyMember];
            }
            if (localInteger != -1)
                return localInteger;
            int i = 0;
            if (paramFamilyMember.leftParent != null)
                i = Math.Max(i, cc_member_hi(paramFamilyMember.leftParent, paramHashtable, paramList) + 1);
            if (paramFamilyMember.upParent != null)
                i = Math.Max(i, cc_member_hi(paramFamilyMember.upParent, paramHashtable, paramList) + 1);
            paramHashtable.Add(paramFamilyMember, i);
            while (paramList.Count <= i)
                paramList.Add(new ArrayList());
            ((ArrayList)paramList[i]).Add(paramFamilyMember);
            return i;
        }

        private GridR be_row(BE paramBE, int paramInt, Grid2D paramGrid2D)
        {
            int i = paramInt;
            Grid localGrid = null;
            if (paramBE.left != null)
                localGrid = ((CE)paramBE.left).grid;
            if ((localGrid == null) && (paramBE.up != null))
                localGrid = ((CE)paramBE.up).grid;
            if (localGrid != null)
            {
                GridR localBoxR1 = localGrid.grid_row;
                if (paramInt == localBoxR1.oi)
                {
                    i = localBoxR1.i;
                }
                else
                {
                    int j;
                    if (paramInt > localBoxR1.oi)
                    {
                        j = localBoxR1.i + (paramInt - localBoxR1.oi);
                        int k = paramGrid2D.rows.Count;
                        while (j < k)
                        {
                            GridR localBoxR3 = (GridR)paramGrid2D.rows[j];
                            if (localBoxR3.oi == paramInt)
                            {
                                i = localBoxR3.i;
                                break;
                            }
                            j++;
                        }
                    }
                    if (paramInt < localBoxR1.oi)
                        for (j = localBoxR1.i - (localBoxR1.oi - paramInt); j >= 0; j--)
                        {
                            GridR localBoxR2 = (GridR)paramGrid2D.rows[j];
                            if (localBoxR2.oi != paramInt)
                                continue;
                            i = localBoxR2.i;
                            break;
                        }
                }
            }
            return (GridR)paramGrid2D.rows[i];
        }

        private GridC be_column(BE paramBE, int paramInt, Grid2D paramBoxer2D)
        {
            int i = paramInt;
            Grid localBox = null;
            if (paramBE.up != null)
                localBox = ((CE)paramBE.up).grid;
            if ((localBox == null) && (paramBE.left != null))
                localBox = ((CE)paramBE.left).grid;
            if (localBox != null)
            {
                GridC localBoxC1 = localBox.grid_column;
                if (paramInt == localBoxC1.oi)
                {
                    i = localBoxC1.i;
                }
                else
                {
                    int j;
                    if (paramInt > localBoxC1.oi)
                    {
                        j = localBoxC1.i + (paramInt - localBoxC1.oi);
                        int k = paramBoxer2D.columns.Count;
                        while (j < k)
                        {
                            GridC localBoxC3 = (GridC)paramBoxer2D.columns[j];
                            if (localBoxC3.oi == paramInt)
                            {
                                i = localBoxC3.i;
                                break;
                            }
                            j++;
                        }
                    }
                    if (paramInt < localBoxC1.oi)
                        for (j = localBoxC1.i - (localBoxC1.oi - paramInt); j >= 0; j--)
                        {
                            GridC localBoxC2 = (GridC)paramBoxer2D.columns[j];
                            if (localBoxC2.oi != paramInt)
                                continue;
                            i = localBoxC2.i;
                            break;
                        }
                }
            }
            return (GridC)paramBoxer2D.columns[i];
        }

        private void ex_t2b(ArrayList paramList, Grid2D paramBoxer2D)
        {
            if (paramList.Count == 0)
                return;
            StaticCEList[] arrayOfStaticCEList = new StaticCEList[paramList.Count];
            int i = 0;
            int j = arrayOfStaticCEList.Length;
            while (i < j)
            {
                arrayOfStaticCEList[i] = DyRectCEList.To_static((DyRectCEList)paramList[i]);
                i++;
            }
            paramList.Clear();
            Array.Sort(arrayOfStaticCEList, sort_gen_top_Comparator);
            i = 0;
            j = 0;
            while (j < arrayOfStaticCEList.Length)
            {
                ArrayList localArrayList = new ArrayList();
                StaticCEList localStaticCEList1 = arrayOfStaticCEList[j];
                int k = localStaticCEList1.gen_bottom;
                for (StaticCEList localStaticCEList2 = localStaticCEList1; localStaticCEList2.gen_top <= k; localStaticCEList2 = arrayOfStaticCEList[j])
                {
                    k = Math.Max(k, localStaticCEList2.gen_bottom);
                    localArrayList.Add(localStaticCEList2);
                    j++;
                    if (j >= arrayOfStaticCEList.Length)
                        break;
                }
                i += ex_t2b_concurrent_list(localArrayList, i, paramBoxer2D);
            }
        }

        private int ex_t2b_concurrent_list(ArrayList paramList, int paramInt, Grid2D paramBoxer2D)
        {
            int i = 0;
            int j = 0;
            int k = paramList.Count;
            while (j < k)
            {
                StaticCEList localStaticCEList1 = (StaticCEList)paramList[j];
                i = Math.Max(i, localStaticCEList1.ce_list.Count);
                j++;
            }
            ArrayList localArrayList = new ArrayList();
            k = int.MaxValue;
            int m = int.MinValue;
            int n = 0;
            int i1 = paramList.Count;
            while (n < i1)
            {
                StaticCEList localStaticCEList2 = (StaticCEList)paramList[n];
                k = Math.Min(k, localStaticCEList2.gen_top);
                m = Math.Max(m, localStaticCEList2.gen_bottom);
                n++;
            }
            n = m - k + 1;
            for (i1 = 1; i1 < i; i1++)
                localArrayList.Add(new FT(k + paramInt, m + paramInt));
            i1 = 0;
            if (i > 1)
                i1 = paramBoxer2D.insertRows(paramInt + m + 1, (FT[])localArrayList.ToArray(typeof(FT)));
            for (int i2 = 0; i2 < i; i2++)
            {
                int i3 = 0;
                int i4 = paramList.Count;
                while (i3 < i4)
                {
                    StaticCEList localStaticCEList3 = (StaticCEList)paramList[i3];
                    CE localCE1 = null;
                    if (localStaticCEList3.ce_list.Count > i2)
                    {
                        localCE1 = (CE)localStaticCEList3.ce_list[i2];
                    }
                    else if (localStaticCEList3.ce_list.Count > 0)
                    {
                        CE localCE2 = (CE)localStaticCEList3.ce_list[0];
                        if (localCE2 != null)
                        {
                            BE localBE = localCE2.be_from;
                            localCE1 = new CE(null, localBE);
                            localBE.insert_more_ce(localCE1);
                            localCE1.store_son_be(localBE);
                            insertEmptyCE(localBE, localCE1, true);
                        }
                    }
                    int i5 = localStaticCEList3.column;
                    int i6 = localStaticCEList3.row + paramInt + n * i2;
                    if (localCE1 != null)
                        paramBoxer2D.nail(localCE1, i6, i5);
                    i3++;
                }
            }
            return i1;
        }

        private void ex_l2r(ArrayList paramList, Grid2D paramBoxer2D)
        {
            if (paramList.Count == 0)
                return;
            StaticCEList[] arrayOfStaticCEList = new StaticCEList[paramList.Count];
            int i = 0;
            int j = arrayOfStaticCEList.Length;
            while (i < j)
            {
                arrayOfStaticCEList[i] = DyRectCEList.To_static((DyRectCEList)paramList[i]);
                i++;
            }
            paramList.Clear();
            Array.Sort(arrayOfStaticCEList, sort_gen_left_Comparator);
            i = 0;
            j = 0;
            while (j < arrayOfStaticCEList.Length)
            {
                ArrayList localArrayList = new ArrayList();
                StaticCEList localStaticCEList1 = arrayOfStaticCEList[j];
                int k = localStaticCEList1.gen_right;
                for (StaticCEList localStaticCEList2 = localStaticCEList1; localStaticCEList2.gen_left <= k; localStaticCEList2 = arrayOfStaticCEList[j])
                {
                    k = Math.Max(k, localStaticCEList2.gen_right);
                    localArrayList.Add(localStaticCEList2);
                    j++;
                    if (j >= arrayOfStaticCEList.Length)
                        break;
                }
                i += ex_l2r_concurrent_list(localArrayList, i, paramBoxer2D);
            }
        }

        private int ex_l2r_concurrent_list(ArrayList paramList, int paramInt, Grid2D paramBoxer2D)
        {
            int i = 0;
            int j = 0;
            int k = paramList.Count;
            while (j < k)
            {
                StaticCEList localStaticCEList1 = (StaticCEList)paramList[j];
                i = Math.Max(i, localStaticCEList1.ce_list.Count);
                j++;
            }
            ArrayList localArrayList = new ArrayList();
            k = int.MaxValue;
            int m = int.MinValue;
            int n = 0;
            int i1 = paramList.Count;
            while (n < i1)
            {
                StaticCEList localStaticCEList2 = (StaticCEList)paramList[n];
                k = Math.Min(k, localStaticCEList2.gen_left);
                m = Math.Max(m, localStaticCEList2.gen_right);
                n++;
            }
            n = m - k + 1;
            for (i1 = 1; i1 < i; i1++)
                localArrayList.Add(new FT(k + paramInt, m + paramInt));
            i1 = 0;
            if (i > 1)
                i1 = paramBoxer2D.insertColumns(paramInt + m + 1, (FT[])localArrayList.ToArray(typeof(FT)));
            for (int i2 = 0; i2 < i; i2++)
            {
                int i3 = 0;
                int i4 = paramList.Count;
                while (i3 < i4)
                {
                    StaticCEList localStaticCEList3 = (StaticCEList)paramList[i3];
                    CE localCE1 = null;
                    if (localStaticCEList3.ce_list.Count > i2)
                    {
                        localCE1 = (CE)localStaticCEList3.ce_list[i2];
                    }
                    else if (localStaticCEList3.ce_list.Count > 0)
                    {
                        CE localCE2 = (CE)localStaticCEList3.ce_list[0];
                        if (localCE2 != null)
                        {
                            BE localBE = localCE2.be_from;
                            localCE1 = new CE(null, localBE);
                            localBE.insert_more_ce(localCE1);
                            localCE1.store_son_be(localBE);
                            insertEmptyCE(localBE, localCE1, false);
                        }
                    }
                    int i5 = localStaticCEList3.row;
                    int i6 = localStaticCEList3.column + paramInt + n * i2;
                    if (localCE1 != null)
                        paramBoxer2D.nail(localCE1, i5, i6);
                    i3++;
                }
            }
            return i1;
        }

        private void insertEmptyCE(BE paramBE, CE paramCE, Boolean paramBoolean)
        {
            GridElement localElement = paramBE.get_ce_from();
            FamilyMember localFamilyMember1 = this.genealogy[localElement.Row, localElement.Column];
            ArrayList localList = localFamilyMember1.sons;
            if (localList != null)
                for (int i = 0; i < localList.Count; i++)
                {
                    FamilyMember localFamilyMember2 = (FamilyMember)localList[i];
                    BE localBE = null;
                    if (paramBoolean)
                        localBE = new BE(paramCE, null);
                    else
                        localBE = new BE(null, paramCE);
                    BEB localBEB = getBEB5BEB2D(localFamilyMember2.current, true);
                    if (localBEB == null)
                        continue;
                    CE localCE = new CE(null, localBE);
                    localBE.insert_more_ce(localCE);
                    localBE.beb = localBEB;
                    paramCE.store_son_be(localBE);
                    localBE.beb.add_more_be(localBE);
                    insertEmptyCE(localBE, localCE, paramBoolean);
                }
        }

        private void ex_steady(ArrayList paramList, Grid2D paramBoxer2D)
        {
            foreach (DyRectCEList localDyRectCEList in paramList)
            {
                ArrayList localList = localDyRectCEList.ce_list;
                if (localList.Count == 1)
                {
                    paramBoxer2D.nail((CE)localList[0], localDyRectCEList.row.i, localDyRectCEList.column.i);
                    continue;
                }
                paramBoxer2D.nail((CE[])localList.ToArray(typeof(CE)), localDyRectCEList.row.i, localDyRectCEList.column.i);
            }
        }

        internal CE[] dealWithBEDSColumn(BE_Extend paramBE_Extend, GridElement paramElement)
        {
            List<CE> ce_List = new List<CE>();
            ReportCommon.DataColumn DataColumn = paramElement.Value as ReportCommon.DataColumn;
            String TableName = DataColumn.TableName;
            DataTable localTableData = dataSourceManager.GetDataTable(TableName);
            DataView localTableView = dealWithBEDSColumn_source_rows(paramBE_Extend, localTableData, paramElement);
            if (DataColumn.DataSetting == DataSetting.Aggregation)
            {
                DataTable temp = localTableView.ToTable();
                List<object> values = new List<object>();
                foreach (DataRow Row in temp.Rows)
                {
                    Object obj = Row[DataColumn.FieldName];
                    values.Add(obj);
                }

                DataView DataView = (localTableView.Count != 0? localTableView : null);
                object paramObject = DataColumn.FunctionInfo.Eval(values.ToArray());
                CE_Extend ce_extend = new CE_Extend(DataView, paramObject, paramBE_Extend);
                ce_List.Add(ce_extend);
            }
            else if (DataColumn.DataSetting == DataSetting.Group)
            {
                DataTable temp = localTableView.ToTable(true, new string[] { DataColumn.FieldName });
                ArrayList values = new ArrayList();
                foreach (DataRow Row in temp.Rows)
                {
                    Object obj = Row[DataColumn.FieldName];
                    DataTable obj_DataTable = temp.Clone();
                    obj_DataTable.ImportRow(Row);
                    CE_Extend ce_extend = new CE_Extend(obj_DataTable.DefaultView, obj, paramBE_Extend);
                    ce_List.Add(ce_extend);
                }

                if (localTableView.Count == 0)
                {
                    CE_Extend ce_extend = new CE_Extend(null, PrimitiveValue.NULL, paramBE_Extend);
                    ce_List.Add(ce_extend);
                }
            }
            else if (DataColumn.DataSetting == DataSetting.List)
            {
                DataTable temp = localTableView.ToTable();
                foreach (DataRow Row in temp.Rows)
                {
                    Object obj = Row[DataColumn.FieldName];
                    DataTable obj_DataTable = temp.Clone();
                    obj_DataTable.ImportRow(Row);
                    CE_Extend ce_extend = new CE_Extend(obj_DataTable.DefaultView, obj, paramBE_Extend);
                    ce_List.Add(ce_extend);
                }

                if (localTableView.Count == 0)
                {
                    CE_Extend ce_extend = new CE_Extend(null, PrimitiveValue.NULL, paramBE_Extend);
                    ce_List.Add(ce_extend);
                }
            }

            return ce_List.ToArray();
        }

        private DataView dealWithBEDSColumn_source_rows(BE_Extend paramBE_Extend, DataTable paramTableData, GridElement paramElement)
        {
            if (paramTableData == null)
                return null;
            paramBE_Extend.beb.sourceTableData = paramTableData;
            paramBE_Extend.rows = dataSourceManager.FilterDataTable(paramBE_Extend, paramTableData, paramElement);
            return paramBE_Extend.rows;
        }

        internal CE[] dealWithBENormal(BE_Extend paramBE_Extend, GridElement paramObject)
        {
            Object localObject1 = null;
            Object localObject2 = null;
            Object localObject3 = null;
            if (paramObject.Value is Formula)
            {
                localObject1 = (paramObject.Value as ICloneable).Clone() as Formula;

                try
                {
                    localObject2 = this.calculator.CalcFormula((Formula)localObject1, paramBE_Extend.left, paramBE_Extend.up);
                }
                catch (Exception localException)
                {
                    localObject3 = "计算公式时出现错误\n单元格是 " + paramBE_Extend.get_ce_from() + "\n公式表达式是 " + ((Formula)localObject1).Expression;
                    throw new ApplicationException((String)localObject3, localException);
                }

                ((Formula)localObject1).Result = localObject2.ToString();
            }
            else if (paramObject.Value is Variable)
            {
                localObject1 = paramObject.Value as Variable;
                try
                {
                    ReportEngine re = this.calculator.getAttribute(ReportEngine.CUR_RE) as ReportEngine;
                    localObject2 = re.parameterList[((Variable)localObject1).Name];
                }
                catch (Exception localException)
                {
                    localObject3 = "计算变量时出现错误\n单元格是 " + paramBE_Extend.get_ce_from() + "\n变量表达式是 " + ((Variable)localObject1).Name;
                    throw new ApplicationException((String)localObject3, localException);
                }

                ((Variable)localObject1).Value = (localObject2 as ReportParameter).Value.ToString();
            }
            else
            {
                localObject1 = paramObject.Value;
            }

            return new CE[] { new CE_Extend(paramBE_Extend.rows, localObject1, paramBE_Extend) };
        }

        internal ChartPainter dealWithChartPainter(Grid paramBox, ChartPainter paramChartPainter)
        {
            if (paramChartPainter == null)
                return null;

            try
            {
                ChartPainter chartPainter = (ChartPainter)paramChartPainter.Clone();
                foreach (ChartAxisAtt att in chartPainter.ChartAxisAtts)
                {
                    if (att.DataDefinition is ChartDataDefinition)
                    {
                        ChartDataDefinition Definition = att.DataDefinition as ChartDataDefinition;

                        String xTableName = "", xFieldName = "", yTableName = "", yFieldName = "";
                        DataTable xtable = null, ytable = null;
                        ArrayList localCatlogValues = new ArrayList();
                        ArrayList localSeriesValues = new ArrayList();
                        if (paramBox == null || paramBox.location_tar() == null)
                        {
                            if (Definition.CatlogAxises.Count > 0)
                            {
                                xTableName = Definition.CatlogAxises[0].Split('.')[0];
                                xFieldName = Definition.CatlogAxises[0].Split('.')[1];

                                xtable = dataSourceManager.GetDataTable(xTableName);
                                foreach (DataRow row in xtable.Rows)
                                {
                                    String value = row[xFieldName].ToString();
                                    if (Definition.DistinctCatlog)
                                    {
                                        if (!localCatlogValues.Contains(value))
                                            localCatlogValues.Add(value);
                                    }
                                    else
                                    {
                                        localCatlogValues.Add(value);
                                    }
                                }
                            }

                            yTableName = Definition.SeriesAxis.Split('.')[0];
                            yFieldName = Definition.SeriesAxis.Split('.')[1];

                            ytable = xtable;
                            foreach (String catlogValue in localCatlogValues)
                            {
                                DataRow[] Rows;

                                Type type = xtable.Columns[xFieldName].DataType;
                                if (!type.FullName.ToLower().Contains("int") && !type.FullName.ToLower().Contains("decimal"))
                                    Rows = ytable.Select(xFieldName + "='" + catlogValue + "'");
                                else
                                    Rows = ytable.Select(xFieldName + "=" + catlogValue);

                                List<string> temps = new List<string>();
                                foreach (DataRow row in Rows)
                                {
                                    string temp = row[yFieldName].ToString();
                                    temps.Add(temp);
                                }

                                localSeriesValues.Add(Definition.FunctionInfo.Eval(temps.ToArray()));
                            }
                        }
                        else
                        {
                            if (Definition.CatlogAxises.Count > 0)
                                localCatlogValues = parseString2TableData(Definition.CatlogAxises[0], paramBox);
                            localSeriesValues = parseString2TableData(Definition.SeriesAxis, paramBox);
                        }

                        if (att.DataList == null)
                            att.DataList = new DataList();
                        else
                            att.DataList.DeleteData();

                        for (int i = 0; i < localCatlogValues.Count; i++)
                        {
                            double Xtemp = 0, Ytemp = 0;

                            if (!double.TryParse(localCatlogValues[i].ToString(), out Xtemp))
                                Xtemp = i + 1;
                            if (!double.TryParse(localSeriesValues[i].ToString(), out Ytemp))
                                Ytemp = i + 1;

                            att.DataList.AddData(Xtemp, Ytemp, localCatlogValues[i].ToString());
                        }
                    }

                    if (att.DataDefinition is ReportDataDefinition)
                    {
                        ReportDataDefinition Definition = att.DataDefinition as ReportDataDefinition;

                        Grid[] localCatlogGrids = parseString2BoxArray(Definition.CatlogArea, paramBox);
                        ArrayList localCatlogValues = new ArrayList();
                        for (int i = 0; i < localCatlogGrids.Length; i++)
                        {
                            Object localObject6 = localCatlogGrids[i].cc_grid_cv();
                            localCatlogValues.Add(localObject6);
                        }

                        Grid[] localNameGrids = parseString2BoxArray(Definition.SeriesNameArea, paramBox);
                        ArrayList localNameValues = new ArrayList();
                        for (int i = 0; i < localNameGrids.Length; i++)
                        {
                            Object localObject6 = localNameGrids[i].cc_grid_cv();
                            localNameValues.Add(localObject6);
                        }

                        Grid[] localSeriesGrids = parseString2BoxArray(Definition.SeriesArea, paramBox);
                        ArrayList localSeriesValues = new ArrayList();
                        ArrayList localSeriesValues1 = null;
                        int k = -1;
                        for (int m = 0; m < localSeriesGrids.Length; m++)
                        {
                            Grid localGrid = localSeriesGrids[m];
                            int n = localGrid.getRow();
                            if (n != k)
                            {
                                if (localSeriesValues1 != null)
                                    localSeriesValues.AddRange(localSeriesValues1);
                                localSeriesValues1 = new ArrayList();
                                k = n;
                            }
                            Object localObject8 = localGrid.cc_grid_cv();
                            localSeriesValues1.Add(localObject8);
                        }
                        if (localSeriesValues1 != null)
                            localSeriesValues.AddRange(localSeriesValues1);

                        att.SeriesTitle = (localNameValues.Count > 0 ? localNameValues[0].ToString() : "");

                        if (att.DataList == null)
                            att.DataList = new DataList();
                        else
                            att.DataList.DeleteData();

                        for (int i = 0; i < localCatlogValues.Count; i++)
                        {
                            double Xtemp = 0, Ytemp = 0;

                            try
                            {
                                double.TryParse(localCatlogValues[i].ToString(), out Xtemp);
                            }
                            catch
                            {
                                Xtemp = i + 1;
                            }

                            try
                            {
                                double.TryParse(localSeriesValues[i].ToString(), out Ytemp);
                            }
                            catch
                            {
                                Ytemp = i + 1;
                            }

                            att.DataList.AddData(Xtemp, Ytemp, localCatlogValues[i].ToString());
                        }
                    }
                }

                return chartPainter;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("复制图表元素失败，原因：" + ex.Message);
            }
        }

        private ArrayList parseString2TableData(String DataFiledName, Grid paramBox)
        {
            ArrayList localValueList = new ArrayList();
            if (string.IsNullOrEmpty(DataFiledName))
                return localValueList;

            if (paramBox == null || paramBox.location_tar() == null)
                return localValueList;

            String TableName = DataFiledName.Split('.')[0];
            String FieldName = DataFiledName.Split('.')[1];

            CE localCE = paramBox.location_tar();
            BE_Extend be_extend = localCE.be_from as BE_Extend;
            DataTable source = dataSourceManager.GetDataTable(TableName);
            DataView localDataView = dataSourceManager.FilterDataTable(be_extend, source, be_extend.beb.ce_from);
            if (localDataView != null)
            {
                foreach(DataRowView Row in localDataView)
                {
                    localValueList.Add(Row[FieldName].ToString());
                }
            }

            return localValueList;
        }

        /// <summary>
        /// Cell参数形如A1:B2
        /// </summary>
        /// <param name="Cell"></param>
        /// <param name="paramGrid"></param>
        /// <returns></returns>
        private Grid[] parseString2BoxArray(String Cell, Grid paramGrid)
        {
            if (Cell == null)
                return new Grid[0];

            string[] arrayOfColumnRow = ChartUtils.convertStringToColumnRow(Cell);
            List<Grid> localArrayList = new List<Grid>();
            for (int i = 0; i < arrayOfColumnRow.Length; i++)
                localArrayList.AddRange(resolveBoxColumnRowBoxArray(paramGrid, arrayOfColumnRow[i]));
            Grid[] arrayOfBox = localArrayList.ToArray();
            return arrayOfBox;
        }

        private Grid[] resolveBoxColumnRowBoxArray(Grid paramGrid, String paramColumnRow)
        {
            Hashtable localHashtable = new Hashtable();
            if (paramGrid != null)
            {
                Object localObject = paramGrid.location_tar();
                if (localObject != null)
                    cc_ce_location((CE)localObject, localHashtable);
            }
            CE[] localCEs = resolveColumnRow(paramColumnRow, localHashtable);
            List<Grid> localArrayList = new List<Grid>();
            for (int i = 0; i < localCEs.Length; i++)
                localArrayList.Add(localCEs[i].grid);
            return localArrayList.ToArray();
        }

        private void cc_be_location(BE paramBE, Hashtable paramHashtable)
        {
            cc_ce_location((CE)paramBE.left, paramHashtable);
            cc_ce_location((CE)paramBE.up, paramHashtable);
        }

        private void cc_ce_location(CE paramCE, Hashtable paramHashtable)
        {
            if (paramCE == null)
                return;
            int i = paramCE.get_result_index();
            GridElement localGridElement = paramCE.be_from.get_ce_from();
            paramHashtable.Add(Coords.GetColumn_Row(localGridElement.Column, localGridElement.Row), i);
            cc_be_location(paramCE.be_from, paramHashtable);
        }

        private CE[] resolveColumnRow(String paramColumnRow, Hashtable paramHashtable)
        {
            return resolveColumnRow(paramColumnRow, null, null, paramHashtable);
        }

        private CE[] resolveColumnRow(String paramColumnRow, ColumnRowLocation paramColumnRowLocation1, ColumnRowLocation paramColumnRowLocation2, Hashtable paramHashtable)
        {
            GridElement localGridElement = this.report.Cells[paramColumnRow].Value as GridElement;
            if (localGridElement == null)
                return new CE[0];

            if (!this.executing_columnrow_list.Contains(localGridElement.getLiteralCell()))
                calculateCellElement(localGridElement);

            FamilyMember localFamilyMember1 = this.genealogy[localGridElement.Row, localGridElement.Column];
            ArrayList localArrayList1 = new ArrayList();
            localArrayList1.Add(paramColumnRow);
            FamilyMember localFamilyMember2 = localFamilyMember1;
            while (localFamilyMember2.leftParent != null)
            {
                localFamilyMember2 = localFamilyMember2.leftParent;
                localArrayList1.Add(localFamilyMember2.current.getLiteralCell());
            }
            localArrayList1.Reverse();
            ArrayList localArrayList2 = new ArrayList();
            localArrayList2.Add(paramColumnRow);
            localFamilyMember2 = localFamilyMember1;
            while (localFamilyMember2.upParent != null)
            {
                localFamilyMember2 = localFamilyMember2.upParent;
                localArrayList2.Add(localFamilyMember2.current.getLiteralCell());
            }
            localArrayList2.Reverse();

            BE[] localObject1;
            paramColumnRowLocation1 = mod_columnrow_location(null, paramHashtable, localArrayList1);
            paramColumnRowLocation2 = mod_columnrow_location(null, paramHashtable, localArrayList2);
            if (paramColumnRowLocation1 == ColumnRowLocation.ALL)
            {
                localObject1 = cc_be_array(localArrayList2, paramColumnRowLocation2, null);
            }
            else if (paramColumnRowLocation2 == ColumnRowLocation.ALL)
            {
                localObject1 = cc_be_array(localArrayList1, paramColumnRowLocation1, null);
            }
            else
            {
                BE[] arrayOfBE = cc_be_array(localArrayList1, paramColumnRowLocation1, null);
                localObject1 = cc_be_array(localArrayList2, paramColumnRowLocation2, arrayOfBE);
            }

            LocationDim[] localObject2;
            int j = 1;
            int k = 0;
            if (paramColumnRowLocation1 != ColumnRowLocation.ALL)
            {
                localObject2 = paramColumnRowLocation1.getDims();
                for (int m = 0; m < localObject2.Length; m++)
                {
                    if (!localObject2[m].getColumnrow().ToLower().Equals(paramColumnRow.ToLower()))
                        continue;
                    k = localObject2[m].getIndex();
                    j = 0;
                    break;
                }
            }

            if (paramColumnRowLocation2 != ColumnRowLocation.ALL)
            {
                localObject2 = paramColumnRowLocation2.getDims();
                for (int m = 0; m < localObject2.Length; m++)
                {
                    if (!localObject2[m].getColumnrow().ToLower().Equals(paramColumnRow.ToLower()))
                        continue;
                    k = localObject2[m].getIndex();
                    j = 0;
                    break;
                }
            }

            List<CE> localObject5 = new List<CE>();
            CE localObject4;
            for (int m = 0; m < localObject1.Length; m++)
            {
                if (j != 0)
                {
                    localObject5.AddRange(localObject1[m].cc_ce_array(this.calculator));
                }
                else
                {
                    localObject4 = localObject1[m].cc_index_ce(k, this.calculator);
                    if (localObject4 == null)
                        continue;
                    localObject5.Add(localObject4);
                }
            }

            return localObject5.ToArray();
        }

        private ColumnRowLocation mod_columnrow_location(ColumnRowLocation paramColumnRowLocation, Hashtable localHashtable, ArrayList paramList)
        {
            Object localObject1;
            List<LocationDim> localArrayList;
            if (paramColumnRowLocation == null)
            {
                localArrayList = new List<LocationDim>();
                int i = 0;
                int j = paramList.Count;
                while (i < j)
                {
                    String localColumnRow = paramList[i].ToString();
                    localObject1 = localHashtable[localColumnRow];
                    if (localObject1 != null)
                        localArrayList.Add(new LocationDim(localColumnRow, 0, Convert.ToInt32(localObject1)));
                    i++;
                }
                if (localArrayList.Count == 0)
                    paramColumnRowLocation = ColumnRowLocation.ALL;
                else
                    paramColumnRowLocation = new ColumnRowLocation(localArrayList.ToArray());
            }

            if (paramColumnRowLocation == ColumnRowLocation.ALL)
                return ColumnRowLocation.ALL;

            localArrayList = new List<LocationDim>();
            LocationDim[] arrayOfLocationDim = paramColumnRowLocation.getDims();
            int l = 0;
            int k = paramList.Count;
            while (l < k)
            {
                localObject1 = paramList[l].ToString();
                Object localObject2 = null;
                for (int m = 0; m < arrayOfLocationDim.Length; m++)
                {
                    LocationDim localLocationDim = arrayOfLocationDim[m];
                    if (!localLocationDim.getColumnrow().ToLower().Equals(localObject1.ToString().ToLower()))
                        continue;
                    object localInteger2 = (object)localHashtable[localObject1.ToString()];
                    int n = (localInteger2 == null ? 0 : Convert.ToInt32(localInteger2));
                    switch (localLocationDim.getOp())
                    {
                        case 1:
                            localObject2 = new LocationDim(localObject1.ToString(), 0, n + localLocationDim.getIndex());
                            break;
                        case 2:
                            localObject2 = new LocationDim(localObject1.ToString(), 0, n - localLocationDim.getIndex());
                            break;
                        default:
                            if (localLocationDim.getIndex() == 0)
                                localObject2 = new LocationDim(localObject1.ToString(), 0, n);
                            else
                                localObject2 = localLocationDim;
                            break;
                    }
                    if (((LocationDim)localObject2).getIndex() != 0)
                        break;
                    ((LocationDim)localObject2).setIndex(int.MaxValue);
                    break;
                }
                if (localObject2 == null)
                {
                    Object localInteger1 = (Object)localHashtable[localObject1.ToString()];
                    if (localInteger1 != null)
                        localObject2 = new LocationDim(localObject1.ToString(), 0, Convert.ToInt32(localInteger1));
                }
                if ((localObject2 != null) && (((LocationDim)localObject2).getIndex() != 0))
                    localArrayList.Add((LocationDim)localObject2);
                l++;
            }
            return new ColumnRowLocation(localArrayList.ToArray());
        }

        private BE[] cc_be_array(ArrayList paramList, ColumnRowLocation paramColumnRowLocation, BE[] paramArrayOfBE)
        {
            List<BE> localObject1;
            Object localObject2;
            Object localObject4;
            Object localObject5;
            Object localObject6;
            int i = 0;
            int k = 0;
            int i1, i2;

            if (paramArrayOfBE != null)
            {
                if (paramColumnRowLocation == ColumnRowLocation.ALL)
                    return paramArrayOfBE;
                localObject1 = new List<BE>();
                for (i = 0; i < paramArrayOfBE.Length; i++)
                {
                    BE localBE1 = paramArrayOfBE[i];
                    localObject2 = localBE1;
                    int n = 1;
                    i1 = paramList.Count;
                    for (i2 = i1 - 2; i2 >= 0; i2--)
                    {
                        localObject4 = paramList[i2].ToString();
                        localObject5 = null;
                        CE localCE2 = null;
                        if (((BE)localObject2).left != null)
                        {
                            localObject6 = ((CE)((BE)localObject2).left).be_from;
                            if ((((BE)localObject6).get_ce_from().Column == Coords.ConvertColumn_Row(localObject4.ToString())[0] && 
                                (((BE)localObject6).get_ce_from().Row == Coords.ConvertColumn_Row(localObject4.ToString())[1])))
                            {
                                localObject5 = localObject6;
                                localCE2 = (CE)((BE)localObject2).left;
                            }
                        }
                        if (((BE)localObject2).up != null)
                        {
                            localObject6 = ((CE)((BE)localObject2).up).be_from;
                            if ((localObject5 == null) && (((BE)localObject6).get_ce_from().Column == Coords.ConvertColumn_Row(localObject4.ToString())[0] && 
                                (((BE)localObject6).get_ce_from().Row == Coords.ConvertColumn_Row(localObject4.ToString())[1])))
                            {
                                localObject5 = localObject6;
                                localCE2 = (CE)((BE)localObject2).up;
                            }
                        }
                        localObject2 = localObject5;
                        CE localCE1 = localCE2;
                        localObject6 = ((BE)localObject2).ce_array;
                        int i5 = cc_location_columnrow_index(paramColumnRowLocation, localObject4.ToString());
                        if ((i5 == 0) || 
                            ((i5 > 0) && (i5 <= ((CE[])localObject6).Length) && (((CE[])localObject6)[(i5 - 1)] == localCE1)) || 
                            ((i5 < 0) && (i5 + ((CE[])localObject6).Length >= 0) && (((CE[])localObject6)[(((CE[])localObject6).Length + i5)] == localCE1)))
                            continue;
                        n = 0;
                        break;
                    }

                    if (n == 0)
                        continue;

                    ((List<BE>)localObject1).Add(localBE1);
                }
                return ((List<BE>)localObject1).ToArray();
            }

            if (paramColumnRowLocation == ColumnRowLocation.ALL)
                return cc_be_list_all(paramList[paramList.Count - 1].ToString());

            List<CE> localCEs = null;
            int m;
            int j = paramList.Count;
            Object localObject3;
            while (i < j - 1)
            {
                localObject2 = paramList[i].ToString();
                m = cc_location_columnrow_index(paramColumnRowLocation, localObject2.ToString());
                if (localCEs == null)
                {
                    localCEs = cc_ce_array_all(localObject2.ToString(), m);
                }
                else
                {
                    localObject3 = new List<CE>();
                    i1 = 0;
                    i2 = localCEs.Count;
                    while (i1 < localCEs.Count)
                    {
                        localObject4 = localCEs[i1];
                        localObject5 = ((CE)localObject4).bring_son_be_list(this.calculator, localObject2.ToString());
                        int i3 = 0;
                        int i4 = ((ArrayList)localObject5).Count;
                        while (i3 < i4)
                        {
                            BE localBE2 = (BE)((ArrayList)localObject5)[i3];
                            CE[] arrayOfCE = localBE2.cc_ce_array(this.calculator);
                            if (m == 0)
                                ((List<CE>)localObject3).AddRange(arrayOfCE);
                            else if ((m > 0) && (m <= arrayOfCE.Length))
                                ((List<CE>)localObject3).Add(arrayOfCE[(m - 1)]);
                            else if ((m < 0) && (m + arrayOfCE.Length >= 0))
                                ((List<CE>)localObject3).Add(arrayOfCE[(m + arrayOfCE.Length)]);
                            i3++;
                        }
                        i1++;
                    }
                    localCEs = (List<CE>)localObject3;
                }
                i++;
            }

            String localColumnRow = paramList[paramList.Count - 1].ToString();
            if (localCEs == null)
                return cc_be_list_all(localColumnRow);

            ArrayList localArrayList = new ArrayList();
            k = 0;
            m = localCEs.Count;
            while (k < m)
            {
                localArrayList.AddRange(localCEs[k].bring_son_be_list(this.calculator, localColumnRow));
                k++;
            }

            List<BE> localBEList = new List<BE>();
            foreach (object localobject in localArrayList)
                localBEList.Add(localobject as BE);
            return localBEList.ToArray();
        }

        private static int cc_location_columnrow_index(ColumnRowLocation paramColumnRowLocation, String paramColumnRow)
        {
            LocationDim[] arrayOfLocationDim = paramColumnRowLocation.getDims();
            for (int i = 0; i < arrayOfLocationDim.Length; i++)
                if (arrayOfLocationDim[i].getColumnrow().ToLower().Equals(paramColumnRow.ToLower()))
                    return arrayOfLocationDim[i].getIndex();
            return 0;
        }

        private BE[] cc_be_list_all(String paramColumnRow)
        {
            GridElement gridElement = this.report.Cells[paramColumnRow].Value as GridElement;
            BEB localBEB = getBEB5BEB2D(gridElement, false);
            return localBEB.get_be_array();
        }

        private List<CE> cc_ce_array_all(String paramColumnRow, int Index)
        {
            List<CE> localArrayList = new List<CE>();
            GridElement gridElement = this.report.Cells[paramColumnRow].Value as GridElement;
            BEB localBEB = getBEB5BEB2D(gridElement, false);
            BE[] arrayOfBE = localBEB.get_be_array();
            for (int i = 0; i < arrayOfBE.Length; i++)
            {
                CE[] arrayOfCE = arrayOfBE[i].cc_ce_array(this.calculator);
                if (Index == 0)
                {
                    localArrayList.AddRange(arrayOfCE);
                }
                else if ((Index > 0) && (Index <= arrayOfCE.Length))
                {
                    localArrayList.Add(arrayOfCE[(Index - 1)]);
                }
                else
                {
                    if ((Index >= 0) || (Index + arrayOfCE.Length < 0))
                        continue;
                    localArrayList.Add(arrayOfCE[(Index + arrayOfCE.Length)]);
                }
            }
            return localArrayList;
        }

        internal CE[] dealWithFormulaVariable(PE PE_left, PE PE_up, String Cell)
        {
            List<CE> ce_list = new List<CE>();

            CE ce_left = PE_left as CE;
            CE ce_up = PE_up as CE;

            if (ce_left != null && ce_up != null)
            {
                List<CE> list_ce = get_left_ce_array(ce_left, Cell);
                foreach (CE ce in list_ce)
                {
                    CE temp = ce;
                    while (temp != null && temp != ce_up) temp = temp.be_from.up as CE;

                    if (temp == ce_up)
                    {
                        ce_list.Add(ce);
                    }
                }
            }
            else if (ce_left != null)
            {
                ce_list.AddRange(get_left_ce_array(ce_left,Cell));
            }
            else if (ce_up != null)
            {
                ce_list.AddRange(get_up_ce_array(ce_up, Cell));
            }
            else
            {
                ce_list.AddRange(get_cell_ce_array(Cell));
            }

            return ce_list.ToArray();
        }

        private List<CE> get_cell_ce_array(String Cell)
        {
            List<CE> list_ce = new List<CE>();

            int[] column_row = Coords.ConvertColumn_Row(Cell);
            BEB beb = getBEB5BEB2D(column_row[1], column_row[0], false);
            if (beb != null)
            {
                BE[] be_array = beb.get_be_array();
                foreach (BE be in be_array)
                {
                    if (be.ce_array == null)
                        continue;

                    foreach (CE ce in be.ce_array)
                    {
                        String ce_cell = ce.be_from.beb.ce_from.getLiteralCell();
                        if (ce_cell.ToLower() == Cell.ToLower())
                        {
                            list_ce.Add(ce);
                        }
                    }
                }
            }

            return list_ce;
        }

        private List<CE> get_left_ce_array(CE left_parent, String Cell)
        {
            List<CE> list_ce = new List<CE>();

            int[] column_row = Coords.ConvertColumn_Row(Cell);
            string left_literalcell = left_parent.be_from.beb.ce_from.getLiteralCell();
            int left_level = Convert.ToInt32(columnrow_levels[left_literalcell]);
            int cell_level = Convert.ToInt32(columnrow_levels[Cell]);
            if (left_level >= cell_level)
            {
                CE temp = left_parent;
                while (temp.be_from.left != null)
                {
                    if (temp.be_from.beb.ce_from.getLiteralCell().ToLower() == Cell.ToLower())
                    {
                        list_ce.Add(temp);
                        break;
                    }

                    temp = temp.be_from.left as CE;
                }
            }
            else
            {
                ArrayList localList = new ArrayList();
                List<CE> local_listce = new List<CE>();

                FamilyMember familymember = genealogy[column_row[1], column_row[0]];
                while (familymember != null)
                {
                    if (familymember.current.getLiteralCell() == left_literalcell)
                        break;

                    localList.Add(familymember.current.getLiteralCell());
                    familymember = familymember.leftParent;
                }

                localList.Reverse();
                local_listce.Add(left_parent);
                foreach (String literalCell in localList)
                {
                    List<CE> temp = new List<CE>();
                    foreach (CE ce in local_listce)
                    {
                        ArrayList be_list = ce.bring_son_be_list(this.calculator, literalCell);
                        foreach (BE be in be_list)
                        {
                            if (be.ce_array == null)
                                continue;
                            temp.AddRange(be.ce_array);
                        }
                    }

                    local_listce = temp;
                }

                list_ce.AddRange(local_listce);
            }

            return list_ce;
        }

        private List<CE> get_up_ce_array(CE up_parent, String Cell)
        {
            List<CE> list_ce = new List<CE>();

            int[] column_row = Coords.ConvertColumn_Row(Cell);
            string up_literalcell = up_parent.be_from.beb.ce_from.getLiteralCell();
            int up_level = Convert.ToInt32(columnrow_levels[up_literalcell]);
            int cell_level = Convert.ToInt32(columnrow_levels[Cell]);
            if (up_level >= cell_level)
            {
                CE temp = up_parent;
                while (temp.be_from.up != null)
                {
                    if (temp.be_from.beb.ce_from.getLiteralCell().ToLower() == Cell.ToLower())
                    {
                        list_ce.Add(temp);
                        break;
                    }

                    temp = temp.be_from.up as CE;
                }
            }
            else
            {
                ArrayList localList = new ArrayList();
                List<CE> local_listce = new List<CE>();

                FamilyMember familymember = genealogy[column_row[1], column_row[0]];
                while (familymember != null)
                {
                    if (familymember.current.getLiteralCell() == up_literalcell)
                        break;

                    localList.Add(familymember.current.getLiteralCell());
                    familymember = familymember.upParent;
                }

                localList.Reverse();
                local_listce.Add(up_parent);
                foreach (String literalCell in localList)
                {
                    List<CE> temp = new List<CE>();
                    foreach (CE ce in local_listce)
                    {
                        ArrayList be_list = ce.bring_son_be_list(this.calculator, literalCell);
                        foreach (BE be in be_list)
                        {
                            if (be.ce_array == null)
                                continue;
                            temp.AddRange(be.ce_array);
                        }
                    }

                    local_listce = temp;
                }

                list_ce.AddRange(local_listce);
            }

            return list_ce;
        }
    }

    internal class FamilyMember
    {
        internal GridElement current;
        internal FamilyMember leftParent;
        internal FamilyMember upParent;
        internal ArrayList sons;

        public FamilyMember(GridElement Element, FamilyMember paramFamilyMember1, FamilyMember paramFamilyMember2)
        {
            this.current = Element;
            this.leftParent = paramFamilyMember1;
            this.upParent = paramFamilyMember2;
        }

        public override string ToString()
        {
            StringBuilder localStringBuilder = new StringBuilder();
            localStringBuilder.Append("{自身:").Append(this.current);
            if (this.leftParent != null)
                localStringBuilder.Append(", 左:").Append(this.leftParent.current);
            if (this.upParent != null)
                localStringBuilder.Append(", 上:").Append(this.upParent.current);
            localStringBuilder.Append("}");
            return localStringBuilder.ToString();
        }
    }

    internal class BE_BESet_M
    {
        private Dictionary<BE, ArrayList> relations = new Dictionary<BE, ArrayList>();

        public BE_BESet_M()
        {
        }

        public ArrayList posterity(BE paramBE)
        {
            ArrayList localArrayList = new ArrayList();
            ArrayList localList = null;
            if (this.relations.ContainsKey(paramBE))
            {
                localList = (ArrayList)this.relations[paramBE];
            }
            if (localList == null)
                return localArrayList;
            localArrayList.AddRange(localList);
            foreach (Object Object in localList)
            {
                localArrayList.AddRange(posterity((BE)Object));
            }

            return localArrayList;
        }

        public void push_son(BE paramBE1, BE paramBE2)
        {
            if (paramBE1 == null)
                return;
            Object localObject = null;
            if (this.relations.ContainsKey(paramBE1))
            {
                localObject = this.relations[paramBE1];
            }

            if (localObject == null)
            {
                localObject = new ArrayList();
                this.relations.Add(paramBE1, (ArrayList)localObject);
            }
            ((ArrayList)localObject).Add(paramBE2);
        }

        public void delete_sons(BE paramBE)
        {
            if (paramBE == null)
                return;
            this.relations.Remove(paramBE);
        }

        public void remove_son_be(BE paramBE1, BE paramBE2)
        {
            if (paramBE1 == null)
                return;

            ArrayList localList = null;
            if (this.relations.ContainsKey(paramBE1))
            {
                localList = (ArrayList)this.relations[paramBE1];
                if (localList != null)
                    localList.Remove(paramBE2);
            }
            
        }
    }

    internal class BEB
    {
        internal DataTable sourceTableData;
        BE[] be_array;
        internal GridElement ce_from;

        public BEB(BE[] paramArrayOfBE, GridElement paramCellElement)
        {
            for (int i = 0; i < paramArrayOfBE.Length; i++)
                paramArrayOfBE[i].beb = this;
            set_be_array(paramArrayOfBE);
            this.ce_from = paramCellElement;
        }

        public void add_more_be(BE paramBE)
        {
            if (paramBE == null)
                return;
            if (this.be_array == null)
            {
                this.be_array = new BE[1];
            }
            else
            {
                BE[] arrayOfBE = this.be_array;
                this.be_array = new BE[arrayOfBE.Length + 1];
                Array.Copy(arrayOfBE, 0, this.be_array, 0, arrayOfBE.Length);
            }
            this.be_array[this.be_array.Length - 1] = paramBE;
            paramBE.beb = this;
        }

        public String columnrow_from()
        {
            return ce_from.getLiteralCell();
        }

        public BE[] get_be_array()
        {
            return this.be_array;
        }

        public void set_be_array(BE[] paramArrayOfBE)
        {
            this.be_array = paramArrayOfBE;
        }

        public override string ToString()
        {
            return "BEB[" + columnrow_from() + ":" + (this.be_array == null ? 0 : this.be_array.Length) + "]";
        }
    }

    internal interface PE
    {
    }

    internal class BE : PE, ICloneable
    {
        internal PE left;
        internal PE up;
        internal BEB beb;
        internal CE[] ce_array;

        public BE(PE paramPE1, PE paramPE2)
        {
            this.left = paramPE1;
            this.up = paramPE2;
        }

        protected virtual CE[] _cc_ce_array(Calculator paramCalculator)
        {
            return new CE[] { new CE(get_ce_from(), this) };
        }

        internal CE[] cc_ce_array(Calculator paramCalculator)
        {
            if (this.ce_array == null)
            {
                ArrayList localList = (ArrayList)paramCalculator.getAttribute(ReportEngine.BE_CC_SET);
                if (localList.Contains(this))
                    throw new DeathCycleException("Death cycle exists at calculating " + get_ce_from());
                localList.Add(this);
                ArrayList localArrayList = (ArrayList)paramCalculator.getAttribute(ReportEngine.BE_CC_LIST);
                localArrayList.Add(this);
                this.ce_array = _cc_ce_array(paramCalculator);
                for (int j = 0; j < this.ce_array.Length; j++)
                    this.ce_array[j].mark_result_index(j + 1);
                localList.Remove(this);
                localArrayList.RemoveAt(localArrayList.Count - 1);
            }
            return this.ce_array;
        }

        internal CE cc_index_ce(int index, Calculator paramCalculator)
        {
            CE[] arrayOfCE = cc_ce_array(paramCalculator);
            if ((index > 0) && (index <= arrayOfCE.Length))
                return arrayOfCE[(index - 1)];
            if ((index < 0) && (index + arrayOfCE.Length >= 0))
                return arrayOfCE[(index + arrayOfCE.Length)];
            return null;
        }

        internal void insert_more_ce(CE paramCE)
        {
            if (paramCE == null)
                return;
            if (this.ce_array == null)
            {
                this.ce_array = new CE[] { paramCE };
            }
            else
            {
                CE[] arrayOfCE = this.ce_array;
                this.ce_array = new CE[this.ce_array.Length + 1];
                Array.Copy(arrayOfCE, 0, this.ce_array, 0, arrayOfCE.Length);
                this.ce_array[(this.ce_array.Length - 1)] = paramCE;
            }
            paramCE.mark_result_index(this.ce_array.Length);
        }

        public GridElement get_ce_from()
        {
            return this.beb.ce_from;
        }

        public override string ToString()
        {
            return "BE:" + this.beb.ce_from.getLiteralCell() + "<" + this.GetHashCode() + ">";
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    internal class BE_Extend : BE
    {
        internal DataView rows;

        public BE_Extend(PE paramPE1, PE paramPE2) :
            base(paramPE1, paramPE2)
        {
        }

        protected override CE[] _cc_ce_array(Calculator paramCalculator)
        {
            Object localObject = get_ce_from();
            ReportEngine localRE = (ReportEngine)paramCalculator.getAttribute(ReportEngine.CUR_RE);
            if (((GridElement)localObject).Value is ReportCommon.DataColumn)
                return localRE.dealWithBEDSColumn(this, (GridElement)localObject);
            return localRE.dealWithBENormal(this, (GridElement)localObject);
        }

        public override string ToString()
        {
            return "BE_Extend:" + this.get_ce_from().getLiteralCell() + "<" + this.GetHashCode() + ">";
        }
    }

    internal class CE : PE
    {
        internal Object obj;
        internal BE be_from;
        internal int result_index;
        internal Dictionary<String, ArrayList> son_ColumnRow_BEList_M = null;
        internal Grid grid;

        public CE(Object paramObject, BE paramBE)
        {
            this.obj = paramObject;
            this.be_from = paramBE;
        }

        public void mark_result_index(int paramInt)
        {
            this.result_index = paramInt;
        }

        public int get_result_index()
        {
            return this.result_index;
        }

        public void store_son_be(BE paramBE)
        {
            if (this.son_ColumnRow_BEList_M == null)
                return;
            GridElement localElement = paramBE.get_ce_from();
            String Cell = localElement.getLiteralCell();
            ArrayList localList = null;
            if (this.son_ColumnRow_BEList_M.ContainsKey(Cell))
                localList = this.son_ColumnRow_BEList_M[Cell] as ArrayList;
            if (localList == null)
                return;
            localList.Add(paramBE);
        }

        public void replace_son_be(BE paramBE, BE[] paramArrayOfBE)
        {
            if (this.son_ColumnRow_BEList_M == null)
                return;
            GridElement localElement = paramBE.get_ce_from();
            String Cell = localElement.getLiteralCell();
            ArrayList localList = null;
            if (this.son_ColumnRow_BEList_M.ContainsKey(Cell))
                localList = this.son_ColumnRow_BEList_M[Cell] as ArrayList;
            if (localList != null)
            {
                localList.Remove(paramBE);
                localList.AddRange(paramArrayOfBE);
            }
        }

        public ArrayList bring_son_be_list(Calculator paramCalculator, String Cell)
        {
            if (this.son_ColumnRow_BEList_M == null)
                this.son_ColumnRow_BEList_M = new Dictionary<String, ArrayList>();
            Object localObject = null;
            if (this.son_ColumnRow_BEList_M.ContainsKey(Cell))
                localObject = this.son_ColumnRow_BEList_M[Cell] as ArrayList;
            if (localObject == null)
            {
                int[] column_row = Coords.ConvertColumn_Row(Cell);
                BEB localBEB = ((ReportEngine)paramCalculator.getAttribute(ReportEngine.CUR_RE)).getBEB5BEB2D(column_row[1], column_row[0], false);
                ArrayList localArrayList = new ArrayList();
                BE[] arrayOfBE = localBEB.get_be_array();
                for (int i = 0; i < arrayOfBE.Length; i++)
                {
                    BE localBE = arrayOfBE[i];
                    if ((localBE.left != this) && (localBE.up != this))
                        continue;
                    localArrayList.Add(localBE);
                }
                localObject = new ArrayList(localArrayList.Count);
                ((ArrayList)localObject).AddRange(localArrayList);
                this.son_ColumnRow_BEList_M.Add(Cell, (ArrayList)localObject);
            }
            return (ArrayList)localObject;
        }

        public GridElement dest_ce()
        {
            return this.grid != null ? this.grid.gridElement : null;
        }

        public Object ce_value()
        {
            if ((this.grid != null) && (this.grid.gridElement != null))
                return this.grid.gridElement.Value;
            return this.obj;
        }

        public override string ToString()
        {
            return "CE:" + this.be_from.get_ce_from().getLiteralCell() + "<" + this.GetHashCode() + ">";
        }
    }

    internal class CE_Extend : CE
    {
        internal DataView rows;

        public CE_Extend(DataView paramArrayOfInt, Object paramObject, BE paramBE) :
            base(paramObject, paramBE)
        {
            this.rows = paramArrayOfInt;
        }

        public override String ToString()
        {
            return "CE:" + this.be_from.beb.ce_from.getLiteralCell() + "<" + this.GetHashCode() + ">";
        }
    }

    internal class StaticCEList
    {
        internal ArrayList ce_list;
        internal int column;
        internal int row;
        internal int gen_left;
        internal int gen_top;
        internal int gen_right;
        internal int gen_bottom;

        public StaticCEList()
        {
        }

        public override string ToString()
        {
            return this.ce_list.Count + "*[" + this.column + "," + this.row + "]" + "★" + "[" + this.gen_left + "," + this.gen_top + "," + this.gen_right + "," + this.gen_bottom + "]";
        }
    }

    internal class DyRectCEList
    {
        internal ArrayList ce_list;
        internal GridC column;
        internal GridR row;
        internal GridC gen_left;
        internal GridC gen_right;
        internal GridR gen_top;
        internal GridR gen_bottom;

        public DyRectCEList(GridC paramBoxC1, GridR paramBoxR1, GridC paramBoxC2, GridR paramBoxR2, GridC paramBoxC3, GridR paramBoxR3)
        {
            this.column = paramBoxC1;
            this.row = paramBoxR1;
            this.gen_left = paramBoxC2;
            this.gen_right = paramBoxC3;
            this.gen_top = paramBoxR2;
            this.gen_bottom = paramBoxR3;
        }

        internal void push_ce_array(CE[] paramArrayOfCE)
        {
            if (this.ce_list == null)
                this.ce_list = new ArrayList(paramArrayOfCE.Length);
            this.ce_list.AddRange(paramArrayOfCE);
        }

        internal StaticCEList To_static()
        {
            StaticCEList localStaticCEList = new StaticCEList();
            localStaticCEList.ce_list = this.ce_list;
            localStaticCEList.column = this.column.i;
            localStaticCEList.row = this.row.i;
            localStaticCEList.gen_left = this.gen_left.i;
            localStaticCEList.gen_right = this.gen_right.i;
            localStaticCEList.gen_top = this.gen_top.i;
            localStaticCEList.gen_bottom = this.gen_bottom.i;
            return localStaticCEList;
        }

        internal static StaticCEList To_static(DyRectCEList paramDyRectCEList)
        {
            StaticCEList localStaticCEList = new StaticCEList();
            localStaticCEList.ce_list = paramDyRectCEList.ce_list;
            localStaticCEList.column = paramDyRectCEList.column.i;
            localStaticCEList.row = paramDyRectCEList.row.i;
            localStaticCEList.gen_left = paramDyRectCEList.gen_left.i;
            localStaticCEList.gen_right = paramDyRectCEList.gen_right.i;
            localStaticCEList.gen_top = paramDyRectCEList.gen_top.i;
            localStaticCEList.gen_bottom = paramDyRectCEList.gen_bottom.i;
            return localStaticCEList;
        }

        public override String ToString()
        {
            return this.ce_list.Count + "*[" + this.column.i + "," + this.row.i + "," + "]" + "★" + "[" + this.gen_left.i + "," + this.gen_top.i + "," + this.gen_right.i + "," + this.gen_bottom.i + "]";
        }
    }

    internal class Grid2D
    {
        internal ReportEngine re;
        internal ArrayList rows;
        internal ArrayList columns;

        public Grid2D(ReportEngine RE, int paramInt1,int paramInt2)
        {
            this.re = RE;

            this.rows = new ArrayList(paramInt1);
            for (int j = 0; j < paramInt1; j++)
                this.rows.Add(new GridR(j, j));
            int i = paramInt2;
            this.columns = new ArrayList();
            for (int j = 0; j < i; j++)
                this.columns.Add(new GridC(j, j));
        }

        public Grid getBox(int paramInt1, int paramInt2)
        {
            GridR localBoxR = null;
            if ((paramInt1 >= 0) && (paramInt1 < this.rows.Count))
                localBoxR = (GridR)this.rows[paramInt1];
            if (localBoxR != null)
                return localBoxR.get_grid(paramInt2);
            return null;
        }

        public Grid nail(CE paramCE, int paramInt1, int paramInt2)
        {
            GridR localBoxR = (GridR)this.rows[paramInt1];
            GridC localBoxC = (GridC)this.columns[paramInt2];
            GridCE localBoxCE = new GridCE(paramCE, localBoxR, localBoxC);
            localBoxR.add_grid(localBoxCE);
            return localBoxCE;
        }

        public Grid nail(CE[] paramArrayOfCE, int paramInt1, int paramInt2)
        {
            GridR localBoxR = (GridR)this.rows[paramInt1];
            GridC localBoxC = (GridC)this.columns[paramInt2];
            GridCES localBoxCES = new GridCES(paramArrayOfCE, localBoxR, localBoxC);
            localBoxR.add_grid(localBoxCES);
            return localBoxCES;
        }

        public int insertRows(int paramInt, FT[] paramArrayOfFT)
        {
            if (paramArrayOfFT == null)
                return 0;
            List<int> localIntList = new List<int>();
            for (int i = 0; i < paramArrayOfFT.Length; i++)
            {
                FT localFT = paramArrayOfFT[i];
                for (int j = localFT.from; j <= localFT.to; j++)
                {
                    GridR localBoxR = (GridR)this.rows[j];
                    localIntList.Add(localBoxR.oi);
                }
            }
            return insertRows(paramInt, localIntList.ToArray());
        }

        public int insertRows(int paramInt, int[] paramArrayOfInt)
        {
            GridR[] arrayOfBoxR = new GridR[paramArrayOfInt.Length];
            int i = 0;
            for (i = 0; i < paramArrayOfInt.Length; i++)
                arrayOfBoxR[i] = new GridR(paramArrayOfInt[i], paramInt + i);
            this.rows.InsertRange(paramInt, arrayOfBoxR);
            i = arrayOfBoxR.Length;
            int j = paramInt + i;
            int k = this.rows.Count;
            while (j < k)
            {
                GridR localBoxR = (GridR)this.rows[j];
                localBoxR.i += i;
                j++;
            }
            return paramArrayOfInt.Length;
        }

        public int insertColumns(int paramInt, FT[] paramArrayOfFT)
        {
            if (paramArrayOfFT == null)
                return 0;
            List<int> localIntList = new List<int>();
            for (int i = 0; i < paramArrayOfFT.Length; i++)
            {
                FT localFT = paramArrayOfFT[i];
                for (int j = localFT.from; j <= localFT.to; j++)
                {
                    GridC localBoxC = (GridC)this.columns[j];
                    localIntList.Add(localBoxC.oi);
                }
            }
            return insertColumns(paramInt, localIntList.ToArray());
        }

        public int insertColumns(int paramInt, int[] paramArrayOfInt)
        {
            GridC[] arrayOfBoxC = new GridC[paramArrayOfInt.Length];
            int i = 0;
            for (i = 0; i < paramArrayOfInt.Length; i++)
                arrayOfBoxC[i] = new GridC(paramArrayOfInt[i], paramInt + i);
            this.columns.InsertRange(paramInt, arrayOfBoxC);
            i = arrayOfBoxC.Length;
            int j = paramInt + i;
            int k = this.columns.Count;
            while (j < k)
            {
                GridC localBoxC = (GridC)this.columns[j];
                localBoxC.i += i;
                j++;
            }
            return paramArrayOfInt.Length;
        }

        public PageReport ToReport(SheetView paramWorkSheet, Boolean paramBoolean1, Boolean paramBoolean2, Boolean paramBoolean3)
        {
            Grid[,] arrayOfBox = ToArray();

            int gridRowCount = this.rows.Count;
            int gridColCount = this.columns.Count;

            //GetLastNonEmptyRow方法获得是某一行或某一列的索引，所以行数(列数) = 行(列)索引号 + 1
            int RowCount = Math.Max(paramWorkSheet.GetLastNonEmptyRow(NonEmptyItemFlag.Data), paramWorkSheet.GetLastNonEmptyRow(NonEmptyItemFlag.Style)) + 1;
            int ColumnCount = Math.Max(paramWorkSheet.GetLastNonEmptyColumn(NonEmptyItemFlag.Data), paramWorkSheet.GetLastNonEmptyColumn(NonEmptyItemFlag.Style)) + 1;

            int m, n;
            for (m = 0; m < RowCount; m++)
            {
                for (n = 0; n < ColumnCount; n++)
                {
                    if (paramWorkSheet.Cells[m, n].Value is GridElement)
                    {
                        ColumnCount = Math.Max(ColumnCount, paramWorkSheet.Cells[m, n].Column.Index + paramWorkSheet.Cells[m, n].ColumnSpan);
                        RowCount = Math.Max(RowCount, paramWorkSheet.Cells[m, n].Row.Index + paramWorkSheet.Cells[m, n].RowSpan);
                    }
                }
            }

            byte[,] arrayOfByte = new byte[RowCount, ColumnCount];
            for (m = 0; m < RowCount; m++)
            {
                for (n = 0; n < ColumnCount; n++)
                {
                    if (paramWorkSheet.Cells[m, n].Value is GridElement)
                    {
                        GridElement Object1 = paramWorkSheet.Cells[m, n].Value as GridElement;
                        arrayOfByte[Object1.Row, Object1.Column] = Convert.ToByte(Object1.ExpandOrientation.Orientation);
                    }
                }
            }

            SheetView SheetView2D = new SheetView();
            SheetView2D.PrintInfo.CopyFrom(paramWorkSheet.PrintInfo);
            foreach (IElement Shape in paramWorkSheet.DrawingContainer.ContainedObjects)
            {
                if (Shape is FloatElement)
                {
                    FloatElement floatElement = Shape as FloatElement;
                    FloatElement tmpfloatElement = floatElement.Clone() as FloatElement;
                    tmpfloatElement.Locked = true;
                    SheetView2D.AddShape(tmpfloatElement);
                }
            }

            SheetView2D.Rows.Count = gridRowCount;
            for (int i = 0; i < SheetView2D.Rows.Count; i++)
            {
                if (i < paramWorkSheet.Rows.Count)
                {
                    SheetView2D.Rows[i].Height = paramWorkSheet.Rows[i].Height;
                }
            }

            SheetView2D.Columns.Count = gridColCount;
            for (int i = 0; i < SheetView2D.Columns.Count; i++)
            {
                if (i < paramWorkSheet.Columns.Count)
                {
                    SheetView2D.Columns[i].Width = paramWorkSheet.Columns[i].Width;
                }
            }

            GridElement Element;
            for (int k = 0; k < arrayOfBox.GetLength(0); k++)
            {
                for (n = 0; n < arrayOfBox.GetLength(1); n++)
                {
                    Element = grid2GridElement(arrayOfBox[k, n], arrayOfBox, arrayOfByte, gridRowCount, gridColCount);
                    if (Element == null)
                        continue;

                    SheetView2D.Cells[Element.Row, Element.Column].BackColor = Element.Style.BackColor;
                    SheetView2D.Cells[Element.Row, Element.Column].ForeColor = Element.Style.ForeColor;
                    SheetView2D.Cells[Element.Row, Element.Column].Font = Element.Style.Font;
                    SheetView2D.Cells[Element.Row, Element.Column].ColumnSpan = Element.ColumnSpan;
                    SheetView2D.Cells[Element.Row, Element.Column].RowSpan = Element.RowSpan;
                    SheetView2D.Cells[Element.Row, Element.Column].HorizontalAlignment = (CellHorizontalAlignment)Enum.Parse(typeof(CellHorizontalAlignment), Enum.GetName(typeof(CellHorizontalAlignment), Element.Style.HorizontalAlignment));
                    SheetView2D.Cells[Element.Row, Element.Column].VerticalAlignment = (CellVerticalAlignment)Enum.Parse(typeof(CellVerticalAlignment), Enum.GetName(typeof(CellVerticalAlignment), Element.Style.VerticalAlignment));
                    SheetView2D.Cells[Element.Row, Element.Column].Border = Element.Style.Border;

                    SheetView2D.Cells[Element.Row, Element.Column].Tag = Element;

                    if (Element.Value is Formula)
                    {
                        Formula Formula = Element.Value as Formula;
                        SheetView2D.Cells[Element.Row, Element.Column].Formula = Formula.Result.ToString();

                        ICellType celltype = CellFormatManager.CreateInstance(Element.Style.FormatInfo);
                        SheetView2D.Cells[Element.Row, Element.Column].CellType = celltype;
                    }
                    else if (Element.Value is Slash)
                    {
                        Slash slash = Element.Value as Slash;
                        SheetView2D.Cells[Element.Row, Element.Column].Value = slash;

                        SheetView2D.Cells[Element.Row, Element.Column].CellType = new SlashCellType();
                    }
                    else if (Element.Value is ReportCommon.Picture)
                    {
                        ReportCommon.Picture picture = Element.Value as ReportCommon.Picture;
                        SheetView2D.Cells[Element.Row, Element.Column].Value = picture.Image;

                        SheetView2D.Cells[Element.Row, Element.Column].CellType = new ImageCellType();
                    }
                    else if (Element.Value is LiteralText)
                    {
                        LiteralText literalText = Element.Value as LiteralText;
                        SheetView2D.Cells[Element.Row, Element.Column].Value = literalText.Text;

                        ICellType celltype = CellFormatManager.CreateInstance(Element.Style.FormatInfo);
                        SheetView2D.Cells[Element.Row, Element.Column].CellType = celltype;
                    }
                    else if (Element.Value is Variable)
                    {
                        Variable Variable = Element.Value as Variable;
                        SheetView2D.Cells[Element.Row, Element.Column].Value = Variable.Value.ToString();

                        ICellType celltype = CellFormatManager.CreateInstance(Element.Style.FormatInfo);
                        SheetView2D.Cells[Element.Row, Element.Column].CellType = celltype;
                    }
                    else if (Element.Value is ChartPainter)
                    {
                        ChartPainter ChartPainter = Element.Value as ChartPainter;
                        SheetView2D.Cells[Element.Row, Element.Column].Value = ChartPainter;
                        ChartPainter.UpdateDataSource();

                        ICellType celltype = CellFormatManager.CreateInstance(Element.Style.FormatInfo);
                        SheetView2D.Cells[Element.Row, Element.Column].CellType = new ChartCellType();
                    }
                    else
                    {
                        SheetView2D.Cells[Element.Row, Element.Column].Value = Element.Value;

                        ICellType celltype = CellFormatManager.CreateInstance(Element.Style.FormatInfo);
                        SheetView2D.Cells[Element.Row, Element.Column].CellType = celltype;
                    }
                }
            }

            foreach (IElement Shape in SheetView2D.DrawingContainer.ContainedObjects)
            {
                if (Shape is FloatElement)
                {
                    FloatElement localFloatElement = Shape as FloatElement;
                    localFloatElement.setColumn(fresh_column_index(localFloatElement.getColumn(), true));
                    localFloatElement.setRow(fresh_row_index(localFloatElement.getRow(), true));
                    Object localObject = localFloatElement.getValue();
                    if (!(localObject is ChartPainter))
                        continue;
                    ChartPainter localChartPainter = re.dealWithChartPainter(null, (ChartPainter)localObject);
                    localChartPainter.UpdateDataSource();
                    localFloatElement.setValue(localChartPainter);
                    localFloatElement.Update();
                }
            }

            m = 0;
            int i1 = this.rows.Count;
            while (m < i1)
            {
                GridR localBoxR = (GridR)this.rows[m];
                SheetView2D.Rows[m].Height = SheetView2D.Rows[localBoxR.oi].Height;
                m++;
            }
            m = 0;
            i1 = this.columns.Count;
            while (m < i1)
            {
                GridC localBoxC = (GridC)this.columns[m];
                SheetView2D.Columns[m].Width = SheetView2D.Columns[localBoxC.oi].Width;
                m++;
            }

            //FpSpread f = new FpSpread();
            //f.Sheets.Add(SheetView2D);
            //f.Dock = DockStyle.Fill;
            //Form form = new Form();
            //form.WindowState = FormWindowState.Maximized;
            //form.Controls.Add(f);
            //form.ShowDialog();

            this.re.calculator.setCurrentReport(SheetView2D);
            for (m = 0; m < RowCount; m++)
            {
                for (n = 0; n < ColumnCount; n++)
                {
                    if (paramWorkSheet.Cells[m, n].Value is GridElement)
                    {
                        GridElement Object1 = paramWorkSheet.Cells[m, n].Value as GridElement;
                        postfix(null);
                    }
                }
            }

            dealWithHeaderFooter(SheetView2D);

            PageReport PageReport = new PageReport(SheetView2D);
            List<int> localIntList = PageReport.getRowOIIntList();
            int i3 = 0;
            int i4 = this.re.grid2D.rows.Count;
            while (i3 < i4)
            {
                GridR localBoxR = (GridR)this.re.grid2D.rows[i3];
                localIntList.Add(localBoxR.oi);
                i3++;
            }
            localIntList = PageReport.getColumnOIIntList();
            i4 = 0;
            int i5 = this.re.grid2D.columns.Count;
            while (i4 < i5)
            {
                GridC localBoxC = (GridC)this.re.grid2D.columns[i4];
                localIntList.Add(localBoxC.oi);
                i4++;
            }

            return PageReport;
        }

        public void dealWithHeaderFooter(SheetView paramReport)
        {
            if (paramReport == null)
                return;
        }

        public Grid[,] ToArray()
        {
            Grid[,] arrayOfGrid = new Grid[this.rows.Count, this.columns.Count];
            int i = 0;
            int j = this.rows.Count;
            while (i < j)
            {
                GridR localBoxR = (GridR)this.rows[i];
                ArrayList localList = localBoxR.grid_list;
                int k = 0;
                int m = localList.Count;
                while (k < m)
                {
                    Grid localGrid = (Grid)localList[k];
                    arrayOfGrid[localBoxR.i, localGrid.grid_column.i] = localGrid;
                    k++;
                }
                i++;
            }
            return arrayOfGrid;
        }

        public int fresh_row_index(int paramInt, Boolean paramBoolean)
        {
            if (paramInt < 0)
                return paramInt;
            int i = paramInt;
            int j = paramInt;
            int k = this.rows.Count;
            while (j < k)
            {
                GridR localBoxR = (GridR)this.rows[j];
                if (localBoxR.oi == paramInt)
                {
                    i = localBoxR.i;
                    if (paramBoolean)
                        break;
                }
                j++;
            }
            return i;
        }

        public int fresh_column_index(int paramInt, Boolean paramBoolean)
        {
            if (paramInt < 0)
                return paramInt;
            int i = paramInt;
            int j = paramInt;
            int k = this.columns.Count;
            while (j < k)
            {
                GridC localGridC = (GridC)this.columns[j];
                if (localGridC.oi == paramInt)
                {
                    i = localGridC.i;
                    if (paramBoolean)
                        break;
                }
                j++;
            }
            return i;
        }

        public GridElement grid2GridElement(Grid paramGrid, Grid[,] paramArrayOfGrid, byte[,] paramArrayOfByte, int paramInt1, int paramInt2)
        {
            if (paramGrid == null)
                return null;
            int i = paramGrid.getRowSpan();
            int j = paramGrid.getColumnSpan();
            int k = paramGrid.grid_row.oi;
            int m = k + i - 1;
            int n = paramGrid.grid_column.oi;
            int i1 = n + j - 1;
            int i2;
            int i3;
            int i4;
            Object localObject2;
            if (paramArrayOfByte[k, n] == 3)
            {
                i2 = 1;
                i3 = paramGrid.grid_column.i;
                for (i4 = paramGrid.grid_row.i + 1; (i4 < paramInt1) && (paramArrayOfGrid[i4, i3] == null); i4++)
                {
                    localObject2 = (GridR)this.rows[i4];
                    if ((((GridR)localObject2).oi > m) || (((GridR)localObject2).oi < k))
                        break;
                    i2++;
                }
                i = i2;
            }
            if (paramArrayOfByte[k, n] == 2)
            {
                i2 = 1;
                i3 = paramGrid.grid_row.i;
                for (i4 = paramGrid.grid_column.i + 1; (i4 < paramInt2) && (paramArrayOfGrid[i3,i4] == null); i4++)
                {
                    localObject2 = (GridC)this.columns[i4];
                    if ((((GridC)localObject2).oi > i1) || (((GridC)localObject2).oi < n))
                        break;
                    i2++;
                }
                j = i2;
            }
            Object localObject1 = paramGrid.cc_grid_cv();
            GridElement localGridElement = paramGrid.baseGridElement(i, j);
            if (localObject1 is ChartPainter)
                localObject1 = re.dealWithChartPainter(paramGrid, (ChartPainter)localObject1);
            localGridElement.Value = localObject1;

            return localGridElement;
        }

        public void postfix(GridElement paramGridElement)
        {
            if (paramGridElement == null)
                return;
        }
    }

    internal abstract class Grid
    {
        internal GridR grid_row;
        internal GridC grid_column;
        internal GridElement gridElement;

        public Grid(GridR paramGridR, GridC paramGridC)
        {
            this.grid_row = paramGridR;
            this.grid_column = paramGridC;
        }

        public int getColumn()
        {
            return this.grid_column.i;
        }

        public int getRow()
        {
            return this.grid_row.i;
        }

        public abstract int getColumnSpan();
        public abstract int getRowSpan();
        public abstract CE location_tar();
        public abstract GridElement baseGridElement(int RowSpan, int ColumnSpan);
        public abstract Object cc_grid_cv();

        public override string ToString()
        {
            return "Grid[" + Coords.GetColumn_Row(this.grid_row.i, this.grid_column.i) + "]";
        }
    }

    internal class GridR
    {
        internal int i = 0;
        internal int oi = 0;
        internal ArrayList grid_list = new ArrayList();
        Boolean sorted = true;
        static readonly IComparer grid_list_sort_com = new Sort_Comparator();
        static readonly IComparer grid_list_search_com = new Search_Comparator();

        public GridR(int oi, int i)
        {
            this.oi = oi;
            this.i = i;
        }

        public void add_grid(Grid paramGrid)
        {
            if (this.grid_list == null)
                this.grid_list = new ArrayList();
            this.grid_list.Add(paramGrid);
            this.sorted = false;
        }

        public Grid get_grid(int paramInt)
        {
            if (!this.sorted)
            {
                this.grid_list.Sort(grid_list_sort_com);
                this.sorted = true;
            }
            int j = this.grid_list.BinarySearch(paramInt, grid_list_search_com);
            if (j >= 0)
                return (Grid)this.grid_list[j];
            return null;
        }

        public override String ToString()
        {
            return "ROW:[" + this.oi + "->" + this.i + "]";
        }
    }

    internal class GridC
    {
        internal int i = 0;
        internal int oi = 0;

        public GridC(int paramInt1, int paramInt2)
        {
            this.oi = paramInt1;
            this.i = paramInt2;
        }

        public override string ToString()
        {
            return "COL:[" + this.oi + "->" + this.i + "]";
        }
    }

    internal class GridCES : Grid
    {
        CE[] ce_array;

        public GridCES(CE[] paramArrayOfCE, GridR paramGridR, GridC paramGridC) :
            base(paramGridR, paramGridC)
        {
            this.ce_array = paramArrayOfCE;
            this.gridElement = paramArrayOfCE[0].be_from.beb.ce_from;
            for (int i = 0; i < paramArrayOfCE.Length; i++)
                paramArrayOfCE[i].grid = this;
        }

        public override int getColumnSpan()
        {
            GridElement localElement = this.ce_array[0].be_from.get_ce_from();
            return localElement.ColumnSpan;
        }

        public override int getRowSpan()
        {
            GridElement localElement = this.ce_array[0].be_from.get_ce_from();
            return localElement.RowSpan;
        }

        public override CE location_tar()
        {
            return this.ce_array[0];
        }

        public override GridElement baseGridElement(int RowSpan, int ColumnSpan)
        {
            GridElement Element = this.gridElement.Clone() as GridElement;
            Element.Row = this.grid_row.i;
            Element.Column = this.grid_column.i;
            Element.RowSpan = RowSpan;
            Element.ColumnSpan = ColumnSpan;

            return Element;
        }

        public override Object cc_grid_cv()
        {
            Object localObject;
            if (this.gridElement != null)
                localObject = this.gridElement.Value;
            else
                localObject = ReportEngine.cv_of_ce_array(this.ce_array, true);
            return localObject;
        }
    }

    internal class GridCE : Grid
    {
        CE ce;

        public GridCE(CE paramCE, GridR paramGridR, GridC paramGridC) :
            base(paramGridR, paramGridC)
        {
            this.ce = paramCE;
            this.gridElement = paramCE.be_from.beb.ce_from;
            paramCE.grid = this;
        }

        public override int getColumnSpan()
        {
            GridElement localElement = this.ce.be_from.get_ce_from();
            return localElement.ColumnSpan;
        }

        public override int getRowSpan()
        {
            GridElement localElement = this.ce.be_from.get_ce_from();
            return localElement.RowSpan;
        }

        public override CE location_tar()
        {
            return this.ce;
        }

        public override GridElement baseGridElement(int RowSpan, int ColumnSpan)
        {
            GridElement Element = this.gridElement.Clone() as GridElement;
            Element.Row = this.grid_row.i;
            Element.Column = this.grid_column.i;
            Element.RowSpan = RowSpan;
            Element.ColumnSpan = ColumnSpan;

            return Element;
        }

        public override Object cc_grid_cv()
        {
            Object localObject = null;
            if (this.gridElement.Value is ReportCommon.DataColumn || this.gridElement.Value is Formula)
            {
                return this.ce.obj;
            }
            else
            {
                if (this.gridElement != null)
                    localObject = this.gridElement.Value;
            }

            return localObject;
        }
    }

    internal class Sort_Comparator : IComparer
    {
        public int Compare(object x, object y)
        {
            Grid localGrid1 = (Grid)x;
            Grid localGrid2 = (Grid)y;
            return localGrid1.getColumn() - localGrid2.getColumn();
        }
    }

    internal class Search_Comparator : IComparer
    {
        public int Compare(object x, object y)
        {
            Grid localGrid1 = (Grid)x;
            Grid localGrid2 = (Grid)y;
            return localGrid1.getColumn() - localGrid2.getColumn();
        }
    }

    internal class Sort_gen_top_Comparator : IComparer
    {
        public int Compare(object x, object y)
        {
            StaticCEList paramObject1 = (StaticCEList)x;
            StaticCEList paramObject2 = (StaticCEList)y;
            return paramObject1.gen_top - paramObject2.gen_top;
        }
    }

    internal class Sort_gen_left_Comparator : IComparer
    {
        public int Compare(object x, object y)
        {
            StaticCEList paramObject1 = (StaticCEList)x;
            StaticCEList paramObject2 = (StaticCEList)y;
            return paramObject1.gen_left - paramObject2.gen_left;
        }
    }

    public class FT
    {
        public int from;
        public int to;

        public FT(int from, int to)
        {
            this.from = from;
            this.to = to;
        }

        public int getFrom()
        {
            return this.from;
        }

        public void setFrom(int from)
        {
            this.from = from;
        }

        public int getTo()
        {
            return this.to;
        }

        public void setTo(int to)
        {
            this.to = to;
        }

        public override String ToString()
        {
            return this.from + " -> " + this.to;
        }
    }
}
