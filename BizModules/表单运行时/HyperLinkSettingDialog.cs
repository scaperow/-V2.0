using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using System.IO;
using BizCommon;
using BizComponents;

namespace BizModules
{
    public partial class HyperLinkSettingDialog : Form
    {
        Bitmap Img;
        Cell ActiveCell = null;
        //string ShowText = string.Empty;
        //string LinkUrl = string.Empty;
        public HyperLinkSettingDialog(Cell _ActiveCell)//, string _ShowText, string _LinkUrl
        {
            InitializeComponent();
            ActiveCell = _ActiveCell;
            //ShowText = _ShowText;
            //LinkUrl = _LinkUrl;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtShowText.Text))
            {
                MessageBox.Show("请输入显示文字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLinkUrl.Focus();
                return;
            }

            if (ActiveCell.CellType is FarPoint.Win.Spread.CellType.HyperLinkCellType)
            {
                FarPoint.Win.Spread.CellType.HyperLinkCellType hlnkCell = ActiveCell.CellType as FarPoint.Win.Spread.CellType.HyperLinkCellType;

                if (RadioAttach.Checked)
                {
                    if (Img == null)
                    {
                        MessageBox.Show("请选择附件文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    ActiveCell.Value = "附件";
                    hlnkCell.Link = JZCommonHelper.BitmapToString(Img);
                }

                if (RadioLink.Checked)
                {
                    ActiveCell.Value = "超链接";
                    hlnkCell.Link = txtLinkUrl.Text;
                }

                hlnkCell.Text = txtShowText.Text;

                ActiveCell.CellType = hlnkCell;
                MessageBox.Show("设置成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void HyperLinkSettingDialog_Load(object sender, EventArgs e)
        {
            if (ActiveCell.CellType is FarPoint.Win.Spread.CellType.HyperLinkCellType)
            {
                FarPoint.Win.Spread.CellType.HyperLinkCellType hlnkCell = ActiveCell.CellType as FarPoint.Win.Spread.CellType.HyperLinkCellType;

                var tag = ActiveCell.Value as string;
                var model = "超链接";

                if (!string.IsNullOrEmpty(tag))
                {
                    model = tag;
                }

                var text = hlnkCell.Text;
                var link = hlnkCell.Link;

                switch (model)
                {
                    case "超链接":
                        txtLinkUrl.Text = link;
                        break;

                    case "附件":
                        Img = new Bitmap(JZCommonHelper.StringToBitmap(Convert.ToString(link)));
                        break;
                }

                if (Img == null)
                {
                    var bitmap = new Bitmap(Preview.Width, Preview.Height);
                    var graphics = Graphics.FromImage(bitmap);
                    var brush = new SolidBrush(Color.Gray);
                    var format = new StringFormat();

                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    graphics.DrawString("当前无预览", this.Font, brush, Preview.ClientRectangle, format);     //这里文字以0,0为中心居中
                    graphics.Dispose();

                    Preview.Image = bitmap;
                }
                else
                {
                    Preview.Image = Img;
                }

                txtShowText.Text = text;
                RadioAttach.Checked = model == "附件";
                RadioLink.Checked = model == "超链接";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void RadioLink_CheckedChanged(object sender, EventArgs e)
        {
            PanelHyperlink.Visible = RadioLink.Checked;
        }

        private void RadioAttach_CheckedChanged(object sender, EventArgs e)
        {
            ButtonUpload.Visible = PanelAttach.Visible = RadioAttach.Checked;
        }

        private void ButtonUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.Filter = "图片文件(*.bmp,*.jpg,*.png)|*.bmp,*.jpg,*.png|所有文件(*.*)|*.*";
            FileDialog.FilterIndex = 2;
            FileDialog.InitialDirectory = Application.StartupPath;
            FileDialog.RestoreDirectory = true;
            if (DialogResult.OK == FileDialog.ShowDialog())
            {
                try
                {
                    Bitmap bitmap = new Bitmap(FileDialog.FileName);
                    int iCellWidth = 800;// (int)view.Cells[cell.Row.Index, cell.Column.Index, cell.Row.Index + cell.RowSpan, cell.Column.Index + cell.ColumnSpan].Column.Width;
                    int iCellHeight = 600;// (int)view.Cells[cell.Row.Index, cell.Column.Index, cell.Row.Index + cell.RowSpan, cell.Column.Index + cell.ColumnSpan].Row.Height;

                    float fCellRate = (float)iCellHeight / iCellWidth;
                    int imgWidth, imgHeight, imgCutWidth, imgCutHeight;
                    imgWidth = bitmap.Width;
                    imgHeight = bitmap.Height;
                    if (imgWidth <= iCellWidth && imgHeight <= iCellHeight)
                    {
                        imgCutWidth = imgWidth;
                        imgCutHeight = imgHeight;
                    }
                    else
                    {
                        float imgRate;
                        imgRate = (float)imgHeight / imgWidth;
                        if (imgRate < fCellRate)
                        {//高小了
                            if (imgWidth < iCellWidth)
                            {
                                imgCutWidth = imgWidth;
                            }
                            else
                            {
                                imgCutWidth = iCellWidth;
                            }
                            imgCutHeight = (int)(imgCutWidth * imgRate);
                        }
                        else
                        {
                            if (imgHeight < iCellHeight)
                            {
                                imgCutHeight = imgHeight;
                            }
                            else
                            {
                                imgCutHeight = iCellHeight;
                            }
                            imgCutWidth = (int)(imgCutHeight / imgRate);
                        }
                    }

                    Img = JZCommonHelper.KiResizeImage(bitmap, imgCutWidth, imgCutHeight, 0);
                    Preview.Image = Img;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Preview_Click(object sender, EventArgs e)
        {
            if (Img != null)
            {
                var detail = new ImgDetail(Img);
                detail.ShowDialog();
            }
        }
    }
}
