using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UpdaterCommon;
using UpdaterComponents;
using FarPoint.Win.Spread.CellType;
using System.IO;

namespace UpdaterManager
{
    public partial class UpdaterManagerFrom : Form
    {
        public UpdaterManagerFrom()
        {
            InitializeComponent();

            MaskCellType mask = new MaskCellType();
            mask.Mask = "#.#.####";
            OldFile_Spread_Sheet1.Columns[1].CellType = mask;
            NewFile_Spread_Sheet1.Columns[1].CellType = mask;
            NewFile_Spread.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpSpread_NewFile_ButtonClicked);
        }

        private void SelectFileForm_Load(object sender, EventArgs e)
        {
            RefreshButton.PerformClick();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            List<UpdaterFileInfo> OldFilesInfo = ServerFileVersionManager.GetExistingFile();
            if (OldFilesInfo != null && OldFilesInfo.Count > 0)
            {
                OldFile_Spread_Sheet1.Rows.Count = OldFilesInfo.Count;
                for (int i = 0; i < OldFilesInfo.Count; i++)
                {
                    OldFile_Spread_Sheet1.Cells[i, 0].Value = OldFilesInfo[i].FileName;
                    OldFile_Spread_Sheet1.Cells[i, 0].Locked = true;
                    OldFile_Spread_Sheet1.Cells[i, 1].Value = OldFilesInfo[i].FileVersion.ToString();
                    OldFile_Spread_Sheet1.Cells[i, 1].Locked = true;
                }
            }
        }

        void fpSpread_NewFile_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            NewFile_Spread_Sheet1.ActiveRow.Remove();
        }

        private void BrowseFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog SelectFile = new OpenFileDialog();
            SelectFile.Multiselect = true;
            String Path = string.Empty;
            int i = NewFile_Spread_Sheet1.Rows.Count;

            if (SelectFile.ShowDialog() == DialogResult.OK)
            {
                NewFile_Spread_Sheet1.Rows.Add(NewFile_Spread_Sheet1.Rows.Count, SelectFile.FileNames.Length);
                foreach (String FileName in SelectFile.FileNames)
                {
                    Path = System.IO.Path.GetDirectoryName(FileName);
                    break;
                }

                foreach (String FileName in SelectFile.SafeFileNames)
                {
                    NewFile_Spread_Sheet1.Cells[i, 0].Text = FileName;

                    FarPoint.Win.Spread.CellType.ButtonCellType ButtonDCType = new ButtonCellType();
                    ButtonDCType.Text = "删除";
                    NewFile_Spread_Sheet1.Cells[i, 2].CellType = ButtonDCType;
                    NewFile_Spread_Sheet1.Rows[i].Tag = Path;
                    i++;
                }

                i = 0;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            List<UpdaterFileInfo> UpdaterFiles = new List<UpdaterFileInfo>();
            for (int i = 0; i < NewFile_Spread_Sheet1.Rows.Count; i++)
            {
                String FileName = Path.Combine(NewFile_Spread_Sheet1.Rows[i].Tag.ToString(), NewFile_Spread_Sheet1.Cells[i, 0].Text);

                UpdaterFileInfo UpdaterFile = new UpdaterFileInfo();
                UpdaterFile.FileName = NewFile_Spread_Sheet1.Cells[i, 0].Text;
                UpdaterFile.FileData = File.ReadAllBytes(FileName);
                UpdaterFile.FileVersion = NewFile_Spread_Sheet1.Cells[i, 1].Text.Replace('_','0');

                UpdaterFiles.Add(UpdaterFile);
            }

            Boolean Result = ServerFileVersionManager.SaveUpdaterFile(UpdaterFiles);
            String Message = (Result ? "保存成功。" : "保存失败！");
            MessageBoxIcon Icon = (Result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            MessageBox.Show(Message, "提示", MessageBoxButtons.OK, Icon);
        }
    }

    public class MaskCellType : FarPoint.Win.Spread.CellType.MaskCellType
    {
        public override string Format(object obj)
        {
            String s = base.Format(obj);
            return s.Replace('_', '0');
        }

        public override object GetEditorValue()
        {
            object s = base.GetEditorValue();
            return s.ToString().Replace('_','0');
        }
    }
}
