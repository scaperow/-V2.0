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
    public partial class StadiumItemSelector : Form
    {
        IndexDescriptionPair modelField;
        String ModelIndex;
        ModuleConfiguration ModelInfo;

        public StadiumItemSelector(String ModelIndex)
        {
            InitializeComponent();

            this.ModelIndex = ModelIndex;
        }

        public IndexDescriptionPair ModelField
        {
            get
            {
                if (modelField == null)
                    modelField = new IndexDescriptionPair();
                return modelField;
            }
            set
            {
                modelField = value;
            }
        }

        private void StadiumItemSelector_Load(object sender, EventArgs e)
        {
            String ErrorInfo = "";

            ProgressScreen.Current.ShowSplashScreen();
            this.AddOwnedForm(ProgressScreen.Current);

            ProgressScreen.Current.SetStatus = "正在加载模板...";
            ModelInfo = DepositoryModuleConfiguration.InitModuleConfiguration(ModelIndex);

            try
            {
                fpSpread1.Sheets.Clear();
                foreach (SheetConfiguration Sheet in ModelInfo.Sheets)
                {
                    if (Sheet == null)
                        continue;

                    ProgressScreen.Current.SetStatus = "正在初始化表单‘" + Sheet.Description + "’";

                    SheetView SheetView = Serializer.LoadObjectXml(typeof(SheetView), Sheet.SheetStyle, "SheetView") as SheetView;
                    SheetView.Tag = Sheet;
                    SheetView.SheetName = Sheet.Description;
                    SheetView.ZoomFactor = 1.0F;
                    fpSpread1.Sheets.Add(SheetView);

                    SheetConfiguration Configuration = SheetView.Tag as SheetConfiguration;
                    if (Configuration.DataTableSchema.Schema == null)
                        continue;

                    foreach (FieldDefineInfo field in Configuration.DataTableSchema.Schema.FieldInfos)
                    {
                        SheetView.Cells[field.RangeInfo].BackColor = Color.LightPink;
                        SheetView.Cells[field.RangeInfo].Tag = field;
                    }
                }

                lView_ExtentDataItems.Items.Clear();
                foreach (FieldDefineInfo fieldInfo in ModelInfo.ExtentDataSchema.FieldInfos)
                {
                    ListViewItem Item = new ListViewItem();
                    Item.Text = fieldInfo.FieldName;
                    Item.Tag = fieldInfo;
                    Item.SubItems.Add(fieldInfo.Description);
                    Item.SubItems.Add(fieldInfo.FieldType.Description);


                    lView_ExtentDataItems.Items.Add(Item);
                }
            }
            catch (Exception ex)
            {
                ErrorInfo = ex.Message;
            }

            this.RemoveOwnedForm(ProgressScreen.Current);
            ProgressScreen.Current.CloseSplashScreen();
            Activate();

            label2.BackColor = Color.LightPink;

            if (ErrorInfo != "")
                MessageBox.Show("加载模板出错！\r\n原因：" + ErrorInfo, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                FieldDefineInfo FieldInfo = fpSpread1.ActiveSheet.ActiveCell.Tag as FieldDefineInfo;
                if (FieldInfo == null)
                    return;

                ModelField.Index = string.Format("{0}.{1}", FieldInfo.TableInfo.Name, FieldInfo.FieldName);
                ModelField.Description = string.Format("{0}.{1}", FieldInfo.TableInfo.Description, FieldInfo.Description);
            }
            else if (tabControl1.SelectedIndex == 1 && lView_ExtentDataItems.SelectedItems.Count > 0)
            {
                FieldDefineInfo FieldInfo = lView_ExtentDataItems.SelectedItems[0].Tag as FieldDefineInfo;
                ModelField.Index = string.Format("{0}.{1}", ModelInfo.ExtentDataSchema.Name, FieldInfo.FieldName);
                ModelField.Description = string.Format("{0}.{1}", ModelInfo.ExtentDataSchema.Description, FieldInfo.Description);
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
