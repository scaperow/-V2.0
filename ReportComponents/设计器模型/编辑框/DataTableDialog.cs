using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Client;
using Yqun.Services;
using ReportCommon;
using Yqun.Bases;
using FarPoint.Win.Spread;
using FarPoint.Win;
using BizCommon;
using BizComponents;
using System.IO;

namespace ReportComponents
{
    public partial class DataTableDialog : Form
    {
        DbTableData _DbData;

        public DataTableDialog()
        {
            InitializeComponent();
        }

        public DbTableData TableData
        {
            get
            {
                if (_DbData == null)
                {
                    _DbData = new DbTableData();
                    _DbData.DataAdapter = new FrontDataAdapter();
                }
                return _DbData;
            }
            set
            {
                _DbData = value;
            }
        }

        private void DataTableList_Load(object sender, EventArgs e)
        {
            DataTableLoader.LoadTableList(TableView);

            if (TableData != null)
            {
                TextBox_Table.Text = TableData.GetTableName();
                sql_CommandEditor.Text = TableData.GetQueryCommand();
            }
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox_Table.Text))
            {
                TextBox_Table.SelectAll();
                TextBox_Table.Focus();
                return;
            }

            if (!DepositoryDataTable.AnalysisSQLCommand(sql_CommandEditor.Text))
            {
                sql_CommandEditor.SelectAll();
                sql_CommandEditor.Focus();
                return;
            }

            TableData.SetTableName(TextBox_Table.Text);
            TableData.SetTableText(TextBox_Table.Text);

            TableData.SetQueryCommand(sql_CommandEditor.Text);

            //将数据列添加到TableData中
            List<string> DataColumns = DepositoryDataTable.GetFields(sql_CommandEditor.Text);
            foreach (string col in DataColumns)
            {
                int Index = TableData.AddStringColumn(col);
                TableData.SetColumnText(Index, col);
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender == Button_AnalysisSQL)
            {
                Boolean r = DepositoryDataTable.AnalysisSQLCommand(sql_CommandEditor.Text);
                String msg = (r ? "语句正确！" : "语句有错误！");
                MessageBoxIcon BoxIcon = (r? MessageBoxIcon.Information : MessageBoxIcon.Error);
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, BoxIcon);
            }
            else if (sender == Button_Import)
            {
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "sql files (*.sql)|*.sql";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.InitialDirectory = Application.StartupPath;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (TextReader tr = new StreamReader(openFileDialog1.FileName))
                    {
                        sql_CommandEditor.Text = tr.ReadToEnd();
                    }
                }
            }
            else if (sender == Button_Export)
            {
                saveFileDialog1.FileName = "";
                saveFileDialog1.Filter = "sql files (*.sql)|*.sql";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.InitialDirectory = Application.StartupPath;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (TextWriter sw = new StreamWriter(saveFileDialog1.FileName))
                    {
                        sw.Write(sql_CommandEditor.Text);
                        sw.Close();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 加载所有的数据表
    /// </summary>
    public class DataTableLoader
    {
        public static void LoadTableList(TreeView View)
        {
            View.Nodes.Clear();

            List<string> Tables = DepositoryDataTable.GetTables();
            foreach (string table in Tables)
            {
                TreeNode TableNode = new TreeNode(table);
                TableNode.Name = table;
                TableNode.SelectedImageIndex = 0;
                TableNode.ImageIndex = 0;
                View.Nodes.Add(TableNode);
            }

            DataTable Data = DepositoryDataTable.GetFields(Tables.ToArray());
            foreach (DataRow Row in Data.Rows)
            {
                String TableName = Row["TableName"].ToString();
                String ColumnName = Row["ColumnName"].ToString();

                int Index = View.Nodes.IndexOfKey(TableName);
                TreeNode TableNode = View.Nodes[Index];

                TreeNode ColNode = new TreeNode(ColumnName);
                ColNode.Name = ColumnName;
                ColNode.SelectedImageIndex = 0;
                ColNode.ImageIndex = 0;
                TableNode.Nodes.Add(ColNode);
            }
        }
    }
}
