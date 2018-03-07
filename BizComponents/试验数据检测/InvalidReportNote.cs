using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BizComponents.试验数据检测;
using FarPoint.Win.Spread;
using Yqun.Bases;

namespace BizComponents
{
    public partial class InvalidReportNote : Form
    {
        String invalidReportID;
        int userType;
        String oldContent;
        DataTable dtImage = null;
        ImageShowForm _ImageShowForm = null;
        MemoryStream _tempMemoryStream = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invalidReportID"></param>
        /// <param name="userType">施工方=0，监理方=1</param>
        public InvalidReportNote(String invalidReportID, int userType)
        {
            this.invalidReportID = invalidReportID;
            this.userType = userType;
            InitializeComponent();
        }

        private void InvalidReportNote_Load(object sender, EventArgs e)
        {
            InitNoteByUserType();
            GetImageDataTable();
        }

        private void InitNoteByUserType()
        {
            string strNote = string.Empty;
            if (this.Text == "监理意见")
            {
                strNote = DepositoryEvaluateDataList.GetInvalidReportNote(invalidReportID, 1);
            }
            else if (this.Text == "原因分析")
            {
                strNote = DepositoryEvaluateDataList.GetInvalidReportNote(invalidReportID, 0);
            }
            else if (this.Text == "处理结果")
            {
                strNote = DepositoryEvaluateDataList.GetInvalidReportNote(invalidReportID, 2);
                userType = 2;
            }
            string strCode = Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code;
            //if (strCode == "-1" || strCode == "-2")
            //有值了，则不能修改
            if (!string.IsNullOrEmpty(strNote) && strCode != "-1" && strCode != "-2")
            {
                tb_note.Enabled = false;
            }
            else
            {
                tb_note.Enabled = true;
            }
            tb_note.Text = strNote;
            oldContent = tb_note.Text;
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            if (tb_note.Text.Trim() != oldContent)
            {
                if (MessageBox.Show("关闭对话框会丢失您做的更改，是否关闭？", "关闭确定对话框", MessageBoxButtons.OKCancel)
                    == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            bool flag = false;
            if (tb_note.Text.Trim() == "")
            {
                if (MessageBox.Show("当前内容为空，您确定保存吗？", "保存确定对话框", MessageBoxButtons.OKCancel)
                    == DialogResult.OK)
                {
                    ProgressScreen.Current.ShowSplashScreen();
                    flag = true;
                }
                else
                {
                    return;
                }
            }
            else
            {
                flag = true;
            }
            if (flag)
            {

                ProgressScreen.Current.SetStatus = "正在上传数据，请稍等......";
                DepositoryEvaluateDataList.SaveInvalidReportNote(invalidReportID, tb_note.Text.Trim(), userType);
               
                SaveIamge();
                ProgressScreen.Current.CloseSplashScreen();
                this.Close();
            }
        }

        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog _OpenFileDialog = new OpenFileDialog();
            _OpenFileDialog.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg|*.gif|*.bmp";
            if (_OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.fpSpread1.ActiveSheet.AddRows(0, 1);
                this.fpSpread1.ActiveSheet.Cells[0, 0].Value = System.Guid.NewGuid();
                this.fpSpread1.ActiveSheet.Cells[0, 1].Value = _OpenFileDialog.FileName;
                this.fpSpread1.ActiveSheet.Cells[0, 0].Locked = true;
                this.fpSpread1.ActiveSheet.Cells[0, 1].Locked = true;
                MemoryStream tempms = new MemoryStream(File.ReadAllBytes(this.fpSpread1.ActiveSheet.Cells[0,1].Value.ToString()));
                DataRow row = dtImage.NewRow();
                row["ImgID"] = this.fpSpread1.ActiveSheet.Cells[0, 0].Value;
                row["ImgDataID"] = invalidReportID;
                row["ImgName"] = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
                row["ImgContent"] = tempms.ToArray();
                row["ImgRemark"] = this.Text;
                dtImage.Rows.InsertAt(row,0);
                tempms.Close();
                tempms.Dispose();
            }
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        public void SaveIamge()
        {
            if (dtImage != null)
            {
                DepositoryEvaluateDataList.SaveIamge(dtImage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void GetImageDataTable()
        {
            dtImage = null;
            dtImage = DepositoryEvaluateDataList.GetImage(invalidReportID, this.Text);
            if (dtImage != null && dtImage.Rows.Count > 0)
            {
                for (int i = 0; i < dtImage.Rows.Count; i++)
                {
                    this.fpSpread1.ActiveSheet.AddRows(0, 1);
                    this.fpSpread1.ActiveSheet.Cells[0, 0].Value = dtImage.Rows[i]["ImgID"].ToString();
                    this.fpSpread1.ActiveSheet.Cells[0, 1].Value = dtImage.Rows[i]["ImgName"].ToString();
                    this.fpSpread1.ActiveSheet.Cells[0, 0].Locked = true;
                    this.fpSpread1.ActiveSheet.Cells[0, 1].Locked = true;

                }
            }
            //隐藏图片删除按钮
            string strCode = Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code;
            if (strCode == "-1" || strCode == "-2")
            {
                fpSpread1.ActiveSheet.Columns[2].Visible = true;
            }
            else
            {
                fpSpread1.ActiveSheet.Columns[2].Visible = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (MessageBox.Show("确认要删除吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.fpSpread1.ActiveSheet.Rows.Remove(e.Row, 1);
                DelImg(dtImage.Rows[e.Row]["ImgID"].ToString());
                dtImage.Rows.RemoveAt(e.Row);
            }
        }

        /// <summary>
        /// 显示图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            SetImageShow(e.Row,true);
        }

        public void SetImageShow(int num,bool IsThis)
        {
            if (num < 0)
            {
                num = 0;
            }

            if (num > dtImage.Rows.Count - 1)
            {
                num = dtImage.Rows.Count - 1;
            }

            if (_ImageShowForm == null)
            {
                _ImageShowForm = new ImageShowForm(this);
                _tempMemoryStream = new MemoryStream((byte[])dtImage.Rows[num]["ImgContent"]);
                _ImageShowForm.BackgroundImage = Image.FromStream(_tempMemoryStream);
            }
            else
            {
                _tempMemoryStream.Close();
                _tempMemoryStream.Dispose();
                _tempMemoryStream = new MemoryStream((byte[])dtImage.Rows[num]["ImgContent"]);
                _ImageShowForm.BackgroundImage = Image.FromStream(_tempMemoryStream);
            }
            _ImageShowForm.CurrentNum = num;
            if (IsThis)
            {
                _ImageShowForm.ShowDialog();
            }
            else
            {
                _ImageShowForm.Refresh();
            }
        }

        /// <summary>
        /// 删除图片信息
        /// </summary>
        /// <param name="Imgeid">图片ID</param>
        private void DelImg(string Imgeid)
        {
            DepositoryEvaluateDataList.DelIamge(Imgeid);
        }
    }
}
