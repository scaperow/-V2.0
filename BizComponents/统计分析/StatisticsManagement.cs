using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using Newtonsoft.Json;
using BizCommon;
using Yqun.Bases;
using FarPoint.Win;

namespace BizComponents
{
    public partial class StatisticsManagement : Form
    {
        public JZDocument DocumentCache;
        public SheetView[] SheetsCache;
        public Guid ModuleID;
        public DataTable Modules;
        public DataTable Statistics;
        public Sys_Module CurrentModule;
        public bool NeedSynchronous;
        public Dictionary<string, string> Map;
        public Sys_Statistics_Item CurrentStatistics;
        public Guid SelectionModuleID;
        public DataRow SelectionModule;

        public Guid GetSelectionModuleID()
        {
            var rows = FarPointExtensions.GetSelectionRows(TableModules.ActiveSheet);
            if (rows != null && rows.Length > 0)
            {
                var tag = TableModules.ActiveSheet.Cells[rows[0].Index, 0].Tag;
                return new Guid(Convert.ToString(tag));
            }

            return Guid.Empty;
        }

        public StatisticsManagement(Guid moduleID, string node)
        {
            InitializeComponent();
            ModuleID = moduleID;
            TextModuleName.Text = node;
            CurrentModule = ModuleHelperClient.GetModuleBaseInfoByID(ModuleID);
        }

        private void SetFieldsMap()
        {
            Map = new Dictionary<string, string>();
            Map["项目类型"] = "ItemType";
            Map["厂家名称"] = "FactoryName";
            Map["报告编号"] = "BGBH";
            Map["报告日期"] = "BGRQ";
            Map["强度等级"] = "QDDJ";
            Map["品种等级"] = "PZDJ";
            Map["施工部位"] = "SGBW";
            Map["数量"] = "ShuLiang";
            Map["组值"] = "ZuZhi";
            Map["小数点1"] = "f1";
            Map["小数点2"] = "f2";
            Map["小数点3"] = "f3";
            Map["小数点4"] = "f4";
            Map["小数点5"] = "f5";
            Map["小数点6"] = "f6";
            Map["小数点7"] = "f7";
            Map["小数点8"] = "f8";
            Map["小数点9"] = "f9";
            Map["小数点10"] = "f10";
            Map["小数点11"] = "f11";
            Map["小数点12"] = "f12";
            Map["小数点13"] = "f13";
            Map["小数点14"] = "f14";
            Map["小数点15"] = "f15";
            Map["小数点16"] = "f16";
            Map["小数点17"] = "f17";
            Map["小数点18"] = "f18";
            Map["小数点19"] = "f19";
            Map["时间1"] = "d1";
            Map["时间2"] = "d2";
            Map["时间3"] = "d3";
            Map["时间4"] = "d4";
            Map["时间5"] = "d5";
            Map["文本1"] = "t1";
            Map["文本2"] = "t2";
            Map["文本3"] = "t3";
            Map["文本4"] = "t4";
            Map["文本5"] = "t5";
            Map["文本6"] = "t6";
            Map["文本7"] = "t7";
            Map["文本8"] = "t8";
            Map["文本9"] = "t9";
            Map["文本10"] = "t10";
            Map["文本11"] = "t11";
            Map["文本12"] = "t12";
            Map["文本13"] = "t13";
            Map["文本14"] = "t14";
            Map["文本15"] = "t15";
            Map["文本16"] = "t16";
        }

        private void StatisticsManagement_Load(object sender, EventArgs e)
        {
            LoadStatistics(null);
            SetFieldsMap();
            ComboModules_SelectedIndexChanged(ComboModules, EventArgs.Empty);
        }

        public void LoadStatistics(Sys_Statistics_Item select)
        {
            ComboModules.Items.Clear();
            Statistics = StatisticsHelperClient.GetStatisticsList();
            if (Statistics == null || Statistics.Rows.Count == 0)
            {
                return;
            }

            ComboModules.SelectedIndexChanged -= new EventHandler(ComboModules_SelectedIndexChanged);
            {
                for (var i = 0; i < Statistics.Rows.Count; i++)
                {
                    var row = Statistics.Rows[i];
                    ComboModules.Items.Add(row["ItemName"] as string);
                }

                if (select == null)
                {
                    ComboModules.SelectedIndex = 0;
                }
                else
                {
                    ComboModules.SelectedItem = select.Name;
                }
            }
            ComboModules.SelectedIndexChanged += new EventHandler(ComboModules_SelectedIndexChanged);
        }

        private void LoadCurrentModule(SheetView sheet)
        {
            sheet.Rows.Add(0, 1);
            sheet.Cells[0, 0].Value = " * " + CurrentModule.Name;
            sheet.Cells[0, 0].Tag = CurrentModule.ID;
            sheet.Cells[0, 1].CellType = new TextCellType();
            sheet.Cells[0, 1].Value = "";
            sheet.Cells[0, 1].Tag = "#";
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (CurrentStatistics == null)
            {
                var model = SaveNewStatistics();
                if (model != null)
                {
                    if (SaveSetting(CurrentStatistics))
                    {
                        MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ComboModules_SelectedIndexChanged(this, EventArgs.Empty);
                    }
                }
            }
            else
            {
                if (SaveSetting(CurrentStatistics))
                {
                    MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ComboModules_SelectedIndexChanged(this, EventArgs.Empty);
                }
            }


            var sheet = CloneSheet(TableStatistics.ActiveSheet, 0);
            TableStatistics.Sheets.Clear();
            TableStatistics.Sheets.Add(sheet);
            NeedSynchronous = false;
        }

        private Sys_Statistics_Item SaveNewStatistics()
        {
            var form = new NewStatistics();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var model = form.Result;
                var result = StatisticsHelperClient.NewStatistics(model);
                if (string.IsNullOrEmpty(result) == false)
                {
                    MessageBox.Show(result, "添加统计项失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }

                LoadStatistics(model);

                return model;
            }

            return null;
        }

        private bool SaveSetting(Sys_Statistics_Item statistics)
        {
            var statisticsID = statistics.ID;
            var selectionModuleID = GetSelectionModuleID();

            if (statisticsID == Guid.Empty || selectionModuleID == Guid.Empty)
            {
                return false;
            }

            var statisticsSettings = new List<StatisticsSetting>();
            try
            {
                var settting = JsonConvert.DeserializeObject<List<StatisticsSetting>>(CurrentStatistics.Columns);
                if (settting != null)
                {
                    statisticsSettings = settting;
                }
            }
            catch (Exception)
            {
                var m = JsonConvert.DeserializeObject<Dictionary<string, string>>(CurrentStatistics.Columns);
                foreach (var key in m.Keys)
                {
                    statisticsSettings.Add(new StatisticsSetting()
                    {
                        ItemName = key,
                        BindField = m[key],
                    });
                }
            }
            StatisticsModuleSetting model = null;
            StatisticsSetting item = null;
            var settings = new List<StatisticsModuleSetting>();
            for (var i = 0; i < this.TableStatistics.ActiveSheet.Rows.Count; i++)
            {
                model = new StatisticsModuleSetting();
                item = new StatisticsSetting();
                var itemName = TableStatistics.ActiveSheet.Cells[i, 0].Value;
                var itemType = TableStatistics.ActiveSheet.Cells[i, 1].Value;
                var sheetName = TableStatistics.ActiveSheet.Cells[i, 2].Value;
                var sheetID = TableStatistics.ActiveSheet.Cells[i, 2].Tag;
                var cellName = TableStatistics.ActiveSheet.Cells[i, 3].Value;
                if (itemName == null || itemType == null)
                {
                    continue;
                }

                var itn = Convert.ToString(itemName);
                var it = Map[Convert.ToString(itemType)];
                var sn = Convert.ToString(sheetName);
                var si = new Guid(Convert.ToString(sheetID));
                var cn = Convert.ToString(cellName);

                model.BindField = it;
                model.StatisticsItemName = itn;
                model.CellName = cn;
                model.SheetName = sn;
                model.SheetID = si;

                item = statisticsSettings.Find(m => m.BindField == it);
                if (item == null)
                {
                    item = new StatisticsSetting();
                    statisticsSettings.Add(item);
                }

                item.BindField = it;
                item.ItemName = itn;
                settings.Add(model);

            }

            var map = JsonConvert.SerializeObject(settings);
            var result = StatisticsHelperClient.UpdateStatisticsMapOnModule(selectionModuleID, map);
            if (string.IsNullOrEmpty(result) == false)
            {
                MessageBox.Show(result, "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            statistics.Columns = JsonConvert.SerializeObject(statisticsSettings);
            result = StatisticsHelperClient.ModifyStatistics(statistics);
            if (string.IsNullOrEmpty(result) == false)
            {
                MessageBox.Show(result, "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (NeedSynchronous)
            {
                result = StatisticsHelperClient.SynchronousStatistics(statistics.ID, statisticsSettings);
            }

            result = StatisticsHelperClient.NewStatisticsReference(statistics.ID, selectionModuleID);
            if (string.IsNullOrEmpty(result) == false)
            {
                MessageBox.Show(result, "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void ComboModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            var SetCurrentModule = true;
            Modules = null;
            NeedSynchronous = false;
            TableModules_Sheet1.Rows.Count = 0;
            LinkModifyStatistics.Enabled = false;
            LinkDeleteStatistics.Enabled = false;

            var sheet = CloneSheet(TableModules.ActiveSheet, 0);

            if (Statistics != null && Statistics.Rows.Count > 0)
            {
                var row = Statistics.Rows[ComboModules.SelectedIndex];
                CurrentStatistics = new Sys_Statistics_Item()
                {
                    ID = new Guid(Convert.ToString(row["ItemID"])),
                    Columns = Convert.ToString(row["Columns"]),
                    Name = Convert.ToString(row["ItemName"]),
                    Type = Convert.ToInt32(row["ItemType"]),
                    Weight = Convert.ToInt32(row["Weight"]),
                    Status = 1
                };

                LinkDeleteStatistics.Enabled = true;
                LinkModifyStatistics.Enabled = true;
                Modules = StatisticsHelperClient.GetStatisticsModules(CurrentStatistics.ID);
                Modules.PrimaryKey = new DataColumn[] { Modules.Columns["ModuleID"] };
                if (Modules != null && Modules.Rows.Count > 0)
                {
                    var currentModuleID = Guid.Empty;
                    if (CurrentModule != null)
                    {
                        currentModuleID = CurrentModule.ID;
                    }

                    sheet.Rows.Count = Modules.Rows.Count;
                    for (var i = 0; i < Modules.Rows.Count; i++)
                    {
                        row = Modules.Rows[i];

                        var id = new Guid(Convert.ToString(row["ModuleID"]));
                        if (currentModuleID == id)
                        {
                            SetCurrentModule = false;
                        }

                        sheet.Cells[i, 0].Value = row["Name"];
                        sheet.Cells[i, 0].Tag = row["ModuleID"];
                    }
                }
            }

            if (SetCurrentModule)
            {
                LoadCurrentModule(sheet);
            }

            TableModules.Sheets.Clear();
            TableModules.Sheets.Add(sheet);

        }

        private void TableModules_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            DocumentCache = null;
            SheetsCache = null;
            SelectionModule = null;
            NeedSynchronous = false;
            SelectionModuleID = GetSelectionModuleID();

            if (Modules != null)
            {
                SelectionModule = Modules.Rows.Find(SelectionModuleID);
            }

            SetStatisticsTable();
        }

        private void ShowStatusTip(string message)
        {
            ProgressScreen.Current.ShowSplashScreen();
            AddOwnedForm(ProgressScreen.Current);
            ProgressScreen.Current.SetStatus = message;
        }

        private void CloseStatusTip()
        {
            RemoveOwnedForm(ProgressScreen.Current);
            ProgressScreen.Current.CloseSplashScreen();
            Activate();
        }

        private void SetStatisticsTable()
        {
            var map = "";
            var columns = new List<StatisticsSetting>();
            var settings = new List<StatisticsModuleSetting>();

            if (CurrentStatistics != null)
            {
                try
                {
                    columns = JsonConvert.DeserializeObject<List<StatisticsSetting>>(CurrentStatistics.Columns);
                }
                catch (Exception e)
                {
                    var m = JsonConvert.DeserializeObject<Dictionary<string, string>>(CurrentStatistics.Columns);
                    columns = new List<StatisticsSetting>();
                    foreach (var key in m.Keys)
                    {
                        columns.Add(new StatisticsSetting()
                        {
                            ItemName = key,
                            BindField = m[key],

                        });
                    }
                }
            }
            if (columns == null)
            {
                columns = new List<StatisticsSetting>();
            }

            if (SelectionModule != null)
            {
                map = Convert.ToString(SelectionModule["StatisticsMap"]);
                settings = JsonConvert.DeserializeObject<List<StatisticsModuleSetting>>(map);
            }

            if (settings == null)
            {
                settings = new List<StatisticsModuleSetting>();
            }

            var fixIndex = 0;
            foreach (var column in columns)
            {
                var setting = settings.Find(m => m.StatisticsItemName == column.ItemName);
                if (setting == null)
                {
                    settings.Insert(0, new StatisticsModuleSetting()
                    {
                        StatisticsItemName = column.ItemName,
                        BindField = column.BindField
                    });

                    fixIndex++;
                }
            }

            var sheet = CloneSheet(TableStatistics.ActiveSheet, settings.Count);
            var index = 0;

            foreach (var set in settings)
            {
                if (index < fixIndex)
                {
                    FarPointExtensions.LockCell(sheet.Cells[index, 6], "#");
                    FarPointExtensions.LockCell(sheet.Cells[index, 1], "#");
                    FarPointExtensions.LockCell(sheet.Cells[index, 0], "#");
                }

                sheet.Cells[index, 0].Value = set.StatisticsItemName;
                sheet.Cells[index, 1].Value = GetKey(set.BindField);
                sheet.Cells[index, 2].Value = set.SheetName;
                sheet.Cells[index, 2].Tag = set.SheetID;
                sheet.Cells[index, 3].Value = set.CellName;
                sheet.Cells[index, 3].Tag = set.CellName;

                index += 1;
            }

            TableStatistics.Sheets.Clear();
            TableStatistics.Sheets.Add(sheet);

            sheet.CellChanged += new SheetViewEventHandler(TableStatistics_Sheet1_CellChanged);
        }

        private SheetView CloneSheet(SheetView sheet, int rowCount)
        {
            var n = new SheetView();
            n.Rows.Count = rowCount;
            n.Columns.Count = sheet.Columns.Count;
            n.DefaultStyle = sheet.DefaultStyle;
            n.OperationMode = sheet.OperationMode;
            var index = 0;
            foreach (Column c in sheet.Columns)
            {
                n.Columns[index].Label = c.Label;
                n.Columns[index].CellType = c.CellType;
                n.Columns[index].Width = c.Width;

                index++;
            }

            return n;
        }

        private string GetKey(string value)
        {
            foreach (var key in Map.Keys)
            {
                if (Map[key] == value)
                {
                    return key;
                }
            }

            return "";
        }

        private void ButtonNew_Click(object sender, EventArgs e)
        {
            SaveNewStatistics();
        }

        private void ButtonModify_Click(object sender, EventArgs e)
        {
            var form = new NewStatistics(CurrentStatistics);
            if (form.ShowDialog() == DialogResult.OK)
            {
                var model = form.Result;

                var result = StatisticsHelperClient.ModifyStatistics(model);
                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStatistics(model);
                    ComboModules.SelectedItem = model.Name;
                }
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除该分类吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                StatisticsHelperClient.DeleteStatistics(new Guid[] { CurrentStatistics.ID });

                MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadStatistics(null);
            }
        }

        private void TableStatistics_CellClick(object sender, CellClickEventArgs e)
        {
            var tag = TableStatistics.ActiveSheet.Cells[e.Row, e.Column].Tag;
            if (Convert.ToString(tag) == "#")
            {
                return;
            }

            switch (e.Column)
            {
                case 5:
                    TableStatistics.ActiveSheet.Rows.Add(e.Row, 1);
                    NeedSynchronous = true;
                    break;

                case 6:
                    var value = TableStatistics.ActiveSheet.Cells[e.Row, e.Column].Value;
                    if (MessageBox.Show("确定要删除吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {

                        TableStatistics.ActiveSheet.Rows.Remove(e.Row, 2);
                        NeedSynchronous = true;
                    }
                    break;
            }
        }

        private void TableModules_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {

        }

        public void SetColumnFromSelector(int row, int sheetColumn, int cellColumn)
        {
            if (DocumentCache == null)
            {
                DocumentCache = ModuleHelperClient.GetDefaultDocument(SelectionModuleID);

                if (SheetsCache == null && DocumentCache != null)
                {
                    SheetsCache = new SheetView[DocumentCache.Sheets.Count];
                    for (var i = 0; i < SheetsCache.Length; i++)
                    {
                        var xml = ModuleHelperClient.GetSheetXMLByID(DocumentCache.Sheets[i].ID);
                        var sheet = Serializer.LoadObjectXml(typeof(SheetView), xml, "SheetView") as SheetView;
                        sheet.SheetName = DocumentCache.Sheets[i].Name;
                        SheetsCache[i] = sheet;
                    }
                }
            }

            var selector = new CellSelector(CurrentModule.ID, "", Guid.Empty);
            selector.Preloading(SheetsCache, DocumentCache);

            if (DialogResult.OK == selector.ShowDialog())
            {
                TableStatistics.ActiveSheet.Cells[row, sheetColumn].Value = selector.SheetName;
                TableStatistics.ActiveSheet.Cells[row, sheetColumn].Tag = selector.SheetID;
                TableStatistics.ActiveSheet.Cells[row, cellColumn].Value = selector.CellName;
                TableStatistics.ActiveSheet.Cells[row, cellColumn].Tag = selector.CellName;
            }
        }

        private void SetStatisticsCell(int row, int cell)
        {
            SetColumnFromSelector(row, 2, 3);
        }

        private void SetStandardCell(int row, int cell)
        {
            SetColumnFromSelector(row, 4, 5);
        }

        private void TableStatistics_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            switch (e.Column)
            {
                case 4:
                    SetStatisticsCell(e.Row, e.Column);
                    break;
            }
        }

        private void TableModules_CellClick(object sender, CellClickEventArgs e)
        {
            switch (e.Column)
            {
                case 1:
                    var cell = this.TableModules.ActiveSheet.Cells[e.Row, e.Column];
                    var idCell = this.TableModules.ActiveSheet.Cells[e.Row, 0];
                    if (Convert.ToString(cell.Tag) == "#")
                    {
                        return;
                    }

                    var id = new Guid(Convert.ToString(idCell.Tag));

                    if (MessageBox.Show("删除后将不可恢复, 是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        var result = StatisticsHelperClient.RemoveStatisticsReference(CurrentStatistics.ID, id);
                        if (string.IsNullOrEmpty(result))
                        {
                            this.ComboModules_SelectedIndexChanged(this, EventArgs.Empty);
                            MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(result, "删除错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    break;
            }
        }

        private void LinkDeleteStatistics_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ButtonDelete_Click(sender, EventArgs.Empty);
        }

        private void LinkModifyStatistics_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ButtonModify_Click(sender, EventArgs.Empty);
        }

        private void LinkNewStatistics_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ButtonNew_Click(sender, EventArgs.Empty);
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TableStatistics_Change(object sender, ChangeEventArgs e)
        {

        }

        private void TableStatistics_Sheet1_CellChanged(object sender, SheetViewEventArgs e)
        {
            //if (e.Row % 2 == 0)
            //{
            //    switch (e.Column)
            //    {
            //        case 0:
            //            TableStatistics.ActiveSheet.Cells[e.Row + 1, e.Column].Value = TableStatistics.ActiveSheet.Cells[e.Row, e.Column].Value + "(标准值)";
            //            break;
            //    }
            //}
        }
    }
}
