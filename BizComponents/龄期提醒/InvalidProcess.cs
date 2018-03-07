using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Yqun.Bases;
using BizCommon;
using System.Drawing.Imaging;

namespace BizComponents
{
    public partial class InvalidProcess : Form
    {
        PictureBox selectedImg = null;
        String invalidReportID;
        String documentTestRoomCode = "";
        int userType;

        DataTable dtRequest = new DataTable();
        Guid RequestID = Guid.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invalidReportID"></param>
        /// <param name="userType">施工方=0，监理方=1</param>
        public InvalidProcess(String invalidReportID, int userType)
        {
            this.invalidReportID = invalidReportID;
            this.userType = userType;
            InitializeComponent();
        }

        private void InvalidProcess_Load(object sender, EventArgs e)
        {
            BindProcess();
            LoadImage();
            BindState();
            RequestChange();
        }
        private void RequestChange()
        {
            RequestID = DocumentHelperClient.GetRequestChangeID(new Guid(invalidReportID));
            rdoYes.Enabled = false;
            rdoNo.Enabled = false;
            if (RequestID == Guid.Empty)
            {
                chkSGRequest.Enabled = true;
                chkSGRequest.Checked = false;
            }
            else
            {
                chkSGRequest.Checked = true;
                chkSGRequest.Enabled = false;
                dtRequest = DepositoryDataModificationInfo.HaveDataModificationInfoByID(RequestID.ToString());
                if (dtRequest != null && dtRequest.Rows.Count > 0)
                {
                    if (dtRequest.Rows[0]["State"].ToString() == "已提交")
                    {
                        if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_监理单位" ||
                            Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                        {
                            rdoYes.Enabled = true;
                            rdoNo.Enabled = true;
                        }
                    }
                    else if (dtRequest.Rows[0]["State"].ToString() == "通过")
                    {
                        rdoYes.Checked = true;
                    }
                    else if (dtRequest.Rows[0]["State"].ToString() == "不通过")
                    {
                        rdoNo.Checked = true;
                    }
                }
            }
        }
        private void BindState()
        {
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator ||
                Yqun.Common.ContextCache.ApplicationContext.Current.UserCode.Length == 8)
            {
                bt_save_yyfx.Visible = false;
                lb_yyfx.Visible = false;
                bt_save_cljg.Visible = false;
                lb_cljg.Visible = false;
                bt_save_jlyj.Visible = false;
                lb_jlyj.Visible = false;
            }
            else
            {
                SetContextMenu(2);
                if (documentTestRoomCode == Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code)
                {//自己的资料
                    bt_save_yyfx.Visible = true;
                    lb_yyfx.Visible = true;
                    SetContextMenu(0);
                    if (userType == 1)
                    {
                        bt_save_jlyj.Visible = true;
                        lb_jlyj.Visible = true;
                        SetContextMenu(1);
                    }
                    else
                    {
                        bt_save_jlyj.Visible = false;
                        lb_jlyj.Visible = false;
                    }
                }
                else
                {//人家的资料
                    bt_save_yyfx.Visible = false;
                    lb_yyfx.Visible = false;

                    if (userType == 1)
                    {
                        bt_save_jlyj.Visible = true;
                        lb_jlyj.Visible = true;
                        SetContextMenu(1);
                    }
                    else
                    {
                        bt_save_jlyj.Visible = false;
                        lb_jlyj.Visible = false;
                    }
                }
            }
        }

        private void SetContextMenu(Int32 index)
        {
            FlowLayoutPanel p = null;
            switch (index)
            {
                case 0:
                    p = fl_yyfx;
                    break;
                case 1:
                    p = fl_jlyj;
                    break;
                case 2:
                    p = fl_cljg;
                    break;
                default:
                    break;
            }
            if (p != null && p.Controls.Count > 0)
            {
                foreach (Control ctr in p.Controls)
                {
                    PictureBox pb = ctr as PictureBox;
                    if (pb != null)
                    {
                        pb.ContextMenuStrip = contextMenuStrip1;
                    }
                }
            }
        }

        private void BindProcess()
        {
            DataTable dt = DocumentHelperClient.GetInvalidProcessInfo(invalidReportID);
            if (dt != null && dt.Rows.Count > 0)
            {
                documentTestRoomCode = dt.Rows[0]["TestRoomCode"].ToString();
                tb_yyfx.Text = dt.Rows[0]["SGComment"] == DBNull.Value ? "" : dt.Rows[0]["SGComment"].ToString();
                tb_jlyj.Text = dt.Rows[0]["JLComment"] == DBNull.Value ? "" : dt.Rows[0]["JLComment"].ToString();
                tb_cljg.Text = dt.Rows[0]["DealResult"] == DBNull.Value ? "" : dt.Rows[0]["DealResult"].ToString();
            }
            #region 控制原因分析,监理意见,处理结果的填写顺序
            if (string.IsNullOrEmpty(tb_yyfx.Text))
            {
                tb_jlyj.Enabled = false;
                tb_cljg.Enabled = false;
                bt_save_jlyj.Enabled = false;
                bt_save_cljg.Enabled = false;
            }
            else
            {
                tb_jlyj.Enabled = true;
                tb_cljg.Enabled = true;
                bt_save_jlyj.Enabled = true;
                bt_save_cljg.Enabled = true;
            }
            if (string.IsNullOrEmpty(tb_jlyj.Text))
            {
                tb_cljg.Enabled = false;
                bt_save_cljg.Enabled = false;
                tb_yyfx.Enabled = true;
                bt_save_yyfx.Enabled = true;
            }
            else
            {
                tb_cljg.Enabled = true;
                bt_save_cljg.Enabled = true;
                tb_yyfx.Enabled = false;
                bt_save_yyfx.Enabled = false;
            }
            #endregion
        }

        private void LoadImage()
        {
            DataTable dtImage = DocumentHelperClient.GetInvalidImageList(invalidReportID);
            if (dtImage != null && dtImage.Rows.Count > 0)
            {
                for (int i = 0; i < dtImage.Rows.Count; i++)
                {
                    MemoryStream _tempMemoryStream = new MemoryStream(
                        (byte[])dtImage.Rows[i]["ImgContent"]);
                    if (dtImage.Rows[i]["ImgRemark"] == DBNull.Value || dtImage.Rows[i]["ImgRemark"].ToString() == "")
                    {
                        AddImage(Image.FromStream(_tempMemoryStream), 0, dtImage.Rows[i]["ImgID"], false);
                        AddImage(Image.FromStream(_tempMemoryStream), 1, dtImage.Rows[i]["ImgID"], false);
                        AddImage(Image.FromStream(_tempMemoryStream), 2, dtImage.Rows[i]["ImgID"], false);
                    }
                    else
                    {
                        switch (dtImage.Rows[i]["ImgRemark"].ToString())
                        {
                            case "原因分析":
                                AddImage(Image.FromStream(_tempMemoryStream), 0, dtImage.Rows[i]["ImgID"], false);
                                break;
                            case "监理意见":
                                AddImage(Image.FromStream(_tempMemoryStream), 1, dtImage.Rows[i]["ImgID"], false);
                                break;
                            case "处理结果":
                                AddImage(Image.FromStream(_tempMemoryStream), 2, dtImage.Rows[i]["ImgID"], false);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void AddImage(Image img, Int32 selectedIndex, object imgID, bool addedMenu)
        {
            PictureBox pb = new PictureBox();
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Margin = new Padding(10);
            pb.Width = 100;
            pb.Height = 100;
            pb.Image = img;
            pb.BorderStyle = BorderStyle.FixedSingle;
            pb.Cursor = System.Windows.Forms.Cursors.Hand;
            if (addedMenu)
            {
                pb.ContextMenuStrip = contextMenuStrip1;
            }
            pb.Tag = imgID;
            pb.MouseDown += new MouseEventHandler(pb_MouseDown);
            pb.Click += new EventHandler(pb_Click);
            switch (selectedIndex)
            {
                case 0:
                    fl_yyfx.Controls.Add(pb);
                    break;
                case 1:
                    fl_jlyj.Controls.Add(pb);
                    break;
                case 2:
                    fl_cljg.Controls.Add(pb);
                    break;
                default:
                    break;
            }
        }

        void pb_Click(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            if (p != null)
            {
                ImgDetail i = new ImgDetail(p.Image);
                i.ShowDialog();
            }
        }

        void pb_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                selectedImg = sender as PictureBox;
            }
        }

        private string GetSeletedText()
        {
            string str = "原因分析";
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    str = "原因分析";
                    break;
                case 1:
                    str = "监理意见";
                    break;
                case 2:
                    str = "处理结果";
                    break;
                default:
                    break;
            }
            return str;
        }

        private void bt_save_cljg_Click(object sender, EventArgs e)
        {
            bool flag = false;
            if (tb_cljg.Text.Trim() == "")
            {
                if (MessageBox.Show("当前内容为空，您确定保存吗？", "保存确定对话框", MessageBoxButtons.OKCancel)
                    == DialogResult.OK)
                {
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
                DepositoryEvaluateDataList.SaveInvalidReportNote(invalidReportID, tb_cljg.Text.Trim(), 2);
                MessageBox.Show("保存成功");
            }
        }

        private void bt_save_jlyj_Click(object sender, EventArgs e)
        {
            bool flag = false;

            bool IsRequest = false;
            dtRequest = DepositoryDataModificationInfo.HaveDataModificationInfoByID(RequestID.ToString());
            if (dtRequest != null && dtRequest.Rows.Count > 0)
            {
                if (dtRequest.Rows[0]["State"].ToString() == "已提交")
                {
                    if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_监理单位" ||
                        Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                    {
                        IsRequest = true;
                    }
                }
            }
            if (string.IsNullOrEmpty(tb_jlyj.Text.Trim()) && IsRequest == true)
            {
                MessageBox.Show("监理意见不能为空！");
                return;
            }
            if (tb_jlyj.Text.Trim() == "")
            {
                if (MessageBox.Show("当前内容为空，您确定保存吗？", "保存确定对话框", MessageBoxButtons.OKCancel)
                    == DialogResult.OK)
                {
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
                DepositoryEvaluateDataList.SaveInvalidReportNote(invalidReportID, tb_jlyj.Text.Trim(), 1);
                if (dtRequest != null && dtRequest.Rows.Count > 0 && dtRequest.Rows[0]["State"].ToString() == "已提交")
                {
                    if (Yqun.Common.ContextCache.ApplicationContext.Current.InCompany.Type == "@unit_监理单位" ||
                        Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                    {
                        string strYesOrNo = "";
                        if (rdoYes.Checked)
                        {
                            strYesOrNo = rdoYes.Text;
                        }
                        else if (rdoNo.Checked)
                        {
                            strYesOrNo = rdoNo.Text;
                        }
                        if (!string.IsNullOrEmpty(strYesOrNo))
                        {
                            Boolean Result = DepositoryDataModificationInfo.UpdateDataModificationInfo(new string[] { RequestID.ToString() }, Yqun.Common.ContextCache.ApplicationContext.Current.UserName, strYesOrNo, tb_jlyj.Text.Trim());
                        }
                    }
                }

                MessageBox.Show("保存成功");
                //Close();
            }
        }

        private void bt_save_yyfx_Click(object sender, EventArgs e)
        {
            bool flag = false;
            RequestID = DocumentHelperClient.GetRequestChangeID(new Guid(invalidReportID));
            if (chkSGRequest.Checked && RequestID == Guid.Empty && string.IsNullOrEmpty(tb_yyfx.Text.Trim()))
            {
                MessageBox.Show("原因分析不能为空！");
                return;
            }
            if (tb_yyfx.Text.Trim() == "")
            {
                if (MessageBox.Show("当前内容为空，您确定保存吗？", "保存确定对话框", MessageBoxButtons.OKCancel)
                    == DialogResult.OK)
                {
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
                DepositoryEvaluateDataList.SaveInvalidReportNote(invalidReportID, tb_yyfx.Text.Trim(), 0);
                if (RequestID == Guid.Empty && chkSGRequest.Checked == true)
                {
                    Sys_RequestChange Info = new Sys_RequestChange();

                    Info.DocumentID = new Guid(invalidReportID);
                    Info.Caption = "";// TextBox_Content.Text;
                    Info.Reason = tb_yyfx.Text;

                    Boolean Result = DocumentHelperClient.NewRequestChange(Info);

                }
                MessageBox.Show("保存成功");
                //this.Close();
            }
        }

        private void lb_img_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog _OpenFileDialog = new OpenFileDialog();
            _OpenFileDialog.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg|*.gif|*.bmp";
            //int MAX_IMAGE_SIZE = 100;//图片最大上传大小,单位KB
            int MAX_IMAGE_WIDTH = 800;//图片最大宽度
            int MAX_IMAGE_HEIGHT = 600;//图片最大高度
            float MAX_RATE = (float)MAX_IMAGE_HEIGHT / MAX_IMAGE_WIDTH;//高宽比
            if (_OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                MemoryStream tempms = new MemoryStream(File.ReadAllBytes(_OpenFileDialog.FileName));
                //long size = tempms.Length / 1024;
                //if (size > MAX_IMAGE_SIZE)
                //{
                //    MessageBox.Show(string.Format("图片大小不能超出{0}KB", MAX_IMAGE_SIZE), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                Bitmap bitmap = new Bitmap(tempms);
                #region 判断要裁剪的图片的高度
                int imgWidth, imgHeight, imgCutWidth, imgCutHeight;
                imgWidth = bitmap.Width;
                imgHeight = bitmap.Height;
                if (imgWidth <= MAX_IMAGE_WIDTH && imgHeight <= MAX_IMAGE_HEIGHT)
                {
                    imgCutWidth = imgWidth;
                    imgCutHeight = imgHeight;
                }
                else
                {
                    float imgRate;
                    imgRate = (float)imgHeight / imgWidth;
                    if (imgRate < MAX_RATE)
                    {//高小了
                        if (imgWidth < MAX_IMAGE_WIDTH)
                        {
                            imgCutWidth = imgWidth;
                        }
                        else
                        {
                            imgCutWidth = MAX_IMAGE_WIDTH;
                        }
                        imgCutHeight = (int)(imgCutWidth * imgRate);
                    }
                    else
                    {
                        if (imgHeight < MAX_IMAGE_HEIGHT)
                        {
                            imgCutHeight = imgHeight;
                        }
                        else
                        {
                            imgCutHeight = MAX_IMAGE_HEIGHT;
                        }
                        imgCutWidth = (int)(imgCutHeight / imgRate);
                    }
                }
                #endregion
                Bitmap imgCut = JZCommonHelper.KiResizeImage(bitmap, imgCutWidth, imgCutHeight, 0);

                //ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

                //System.Drawing.Imaging.Encoder myEncoder =
                //    System.Drawing.Imaging.Encoder.Quality;

                //EncoderParameters myEncoderParameters = new EncoderParameters(1);

                //EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                //myEncoderParameters.Param[0] = myEncoderParameter;

                //using (MemoryStream stream = new MemoryStream())
                //{
                //    b.Save(stream, jgpEncoder, myEncoderParameters);
                //    b = new Bitmap(stream);
                //}
                MemoryStream stream = new MemoryStream();
                imgCut.Save(stream, ImageFormat.Jpeg);

                JZFile file = new JZFile();
                file.FileData = stream.ToArray();
                String newImgID = DocumentHelperClient.SaveInvalidImage(invalidReportID, file, GetSeletedText());
                if (newImgID != "")
                {
                    //AddImage(new Bitmap(tempms), tabControl1.SelectedIndex, newImgID, true);
                    AddImage(new Bitmap(stream), tabControl1.SelectedIndex, newImgID, true);
                    tempms.Close();
                    tempms.Dispose();
                    stream.Close();
                    stream.Dispose();
                }
                else
                {
                    MessageBox.Show("图片上传失败");
                }
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        private void 删除图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedImg != null)
            {
                if (selectedImg.Tag != null)
                {
                    DepositoryEvaluateDataList.DelIamge(selectedImg.Tag.ToString());
                }
                FlowLayoutPanel p = selectedImg.Parent as FlowLayoutPanel;
                if (p != null)
                {
                    p.Controls.Remove(selectedImg);
                    selectedImg = null;
                }
            }
        }
    }
}
