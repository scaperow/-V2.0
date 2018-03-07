using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BizComponents
{
    public partial class ImgDetail : Form
    {
        public ImgDetail(Image img)
        {
            InitializeComponent();
            this.BackgroundImage = img;
            #region 判断要显示的图片的高度
            int MAX_IMAGE_WIDTH = 800;//图片最大宽度
            int MAX_IMAGE_HEIGHT = 600;//图片最大高度
            float MAX_RATE = (float)MAX_IMAGE_HEIGHT / MAX_IMAGE_WIDTH;//高宽比
            int imgWidth, imgHeight, imgCutWidth, imgCutHeight;
            imgCutWidth = img.Width;
            imgCutHeight = img.Height;
            //imgWidth = img.Width;
            //imgHeight = img.Height;
            //if (imgWidth <= MAX_IMAGE_WIDTH && imgHeight <= MAX_IMAGE_HEIGHT)
            //{
            //    imgCutWidth = imgWidth;
            //    imgCutHeight = imgHeight;
            //}
            //else
            //{
            //    float imgRate;
            //    imgRate = (float)imgHeight / imgWidth;
            //    if (imgRate < MAX_RATE)
            //    {//高小了
            //        if (imgHeight < MAX_IMAGE_HEIGHT)
            //        {
            //            imgCutHeight = imgHeight;
            //        }
            //        else
            //        {
            //            imgCutHeight = MAX_IMAGE_HEIGHT;
            //        }
            //        imgCutWidth = (int)(imgCutHeight / imgRate);
            //    }
            //    else
            //    {
            //        if (imgWidth < MAX_IMAGE_WIDTH)
            //        {
            //            imgCutWidth = imgWidth;
            //        }
            //        else
            //        {
            //            imgCutWidth = MAX_IMAGE_WIDTH;
            //        }
            //        imgCutHeight = (int)(imgCutWidth * imgRate);
            //    }
            //}
            #endregion
            this.Width = imgCutWidth ;
            this.Height = imgCutHeight;
        }

        private void ImgDetail_Load(object sender, EventArgs e)
        {

        }
    }
}
