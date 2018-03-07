using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BizComponents;

namespace BizModules
{
    public partial class ReportIndexSettingDialog : Form
    {
        private Guid ModuleID;
        private int SheetCount;
        public ReportIndexSettingDialog(Guid moduleID, int sheetCount)
        {
            ModuleID = moduleID;
            SheetCount = sheetCount;
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int reportIndex = Convert.ToInt32(NumberIndex.Value);
            reportIndex--;
            if (reportIndex < 0)
            {
                MessageBox.Show("报告索引不能小于1");
                NumberIndex.Focus();
                return;
            }

            Boolean bResult = ModuleHelperClient.UpdateReportIndex(ModuleID, reportIndex, ComboCatlog.Text);
            if (bResult)
            {
                MessageBox.Show("设置成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("设置失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void ReportIndexSettingDialog_Load(object sender, EventArgs e)
        {
            //rbSDWai.Checked = true;
            var set = ModuleHelperClient.GetReportIndex(ModuleID);
            if (set == null)
            {
                return;
            }

            var setTable = set.Tables["Setting"];
            var catlogTable = set.Tables["Catlog"];

            if (catlogTable != null)
            {
                foreach (DataRow row in catlogTable.Rows)
                {
                    var catlog = Convert.ToString(row["StatisticsCatlog"]);
                    if (string.IsNullOrEmpty(catlog) == false)
                    {
                        if (ComboCatlog.Items.Contains(catlog) == false)
                        {
                            ComboCatlog.Items.Add(catlog);
                        }
                    }
                }
            }

            if (setTable != null && setTable.Rows != null && setTable.Rows.Count > 0)
            {
                var row = setTable.Rows[0];
                var reportIndex = Convert.ToInt32(row["ReportIndex"]);
                reportIndex++;
                NumberIndex.Value = reportIndex;
                if (reportIndex <= 0)
                {
                    MessageBox.Show("序号初始化异常，请谨慎操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                var catlog = Convert.ToString(row["StatisticsCatlog"]);
                ComboCatlog.Text = catlog;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ComboCatlog_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
