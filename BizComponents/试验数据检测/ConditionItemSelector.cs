using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using Yqun.Bases;
using BizCommon;
using FarPoint.Win;

namespace BizComponents
{
    public partial class ConditionItemSelector : Form
    {
        String SheetIndex;
        SheetConfiguration SheetInfo;

        public ConditionItemSelector(String SheetIndex)
        {
            InitializeComponent();

            this.SheetIndex = SheetIndex;
        }

        String _Expression;
        public String Expression
        {
            get
            {
                return _Expression;
            }
        }

        private void ConditionItemSelector_Load(object sender, EventArgs e)
        {
            String ErrorInfo = "";

            ProgressScreen.Current.ShowSplashScreen();
            this.AddOwnedForm(ProgressScreen.Current);

            try
            {
                SheetInfo = DepositorySheetConfiguration.InitConfiguration(SheetIndex);

                ProgressScreen.Current.SetStatus = "正在初始化表单‘" + SheetInfo.Description + "’";

                fpSpread1.Sheets.Clear();
                SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), SheetInfo.SheetStyle, "SheetView") as SheetView;
                SheetView.SheetName = SheetInfo.Description;
                SheetView.OperationMode = OperationMode.ReadOnly;
                fpSpread1.Sheets.Add(SheetView);

                if (SheetInfo.DataTableSchema.Schema != null)
                {
                    foreach (FieldDefineInfo field in SheetInfo.DataTableSchema.Schema.FieldInfos)
                    {
                        SheetView.Cells[field.RangeInfo].BackColor = Color.LightPink;
                        SheetView.Cells[field.RangeInfo].Tag = field;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorInfo = ex.Message;
            }

            this.RemoveOwnedForm(ProgressScreen.Current);
            ProgressScreen.Current.CloseSplashScreen();
            Activate();

            if (ErrorInfo != "")
                MessageBox.Show("加载表单出错！\r\n原因：" + ErrorInfo, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            FieldDefineInfo FieldInfo = fpSpread1.ActiveSheet.ActiveCell.Tag as FieldDefineInfo;
            if (FieldInfo == null)
            {
                return;
            }

            _Expression = string.Format("{{ {0}.{1} }}", FieldInfo.TableInfo.Name, FieldInfo.FieldName);
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
