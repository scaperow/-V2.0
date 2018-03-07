using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace BizModules
{
    public partial class ImageEditor : Form
    {
        Bitmap Img;
        public Bitmap Result { private set; get; }
        Rectangle ShadeRectangle;
        Rectangle CropRectangle;
        Region ShadedRegion;
        Color ShadeColor;

        PickBox Pick = new PickBox();
        int ShadeColorAlpha = 120;
        bool IsMouseDown = false;
        bool AllowCrop = false;
        int WidthOfContainer = 600;
        int HeightOfContainer = 800;
        int x = 0;
        int y = 0;
        int w = 0;
        int h = 0;

        public void SetImage(Bitmap image)
        {
            Result = null;
            Img = image;
            Picture.Width = image.Width;
            Picture.Height = image.Height;
            Picture.Image = image;
            ShowCropRegion(0, 0);
            Pick.Enable();
            w = Img.Width;
            h = Img.Height;
        }

        public void SetSizeOfContainer(int targetWidth, int targetHeight)
        {
            WidthOfContainer = targetWidth;
            HeightOfContainer = targetHeight;

            /*
            var widthScalar = 0f;
            var heightScalar = 0f;

            WidthOfContainer = targetWidth;
            HeightOfContainer = targetHeight;

            if (WidthOfContainer < 600)
            {
                widthScalar = 600f / WidthOfContainer;
            }

            if (HeightOfContainer < 800)
            {
                heightScalar = 800f / HeightOfContainer;
            }

            if (widthScalar != 0 && heightScalar != 0)
            {
                Tracker.Maximum = (int)(Tracker.Maximum * (Math.Max(widthScalar, heightScalar)));
                Tracker.Value = 100;
            }
             */
        }

        public ImageEditor()
        {
            InitializeComponent();
        }

        private void ImageEditor_Load(object sender, EventArgs e)
        {
            ShadeToColorAlpha(ShadeColorAlpha);
            ShadeColor = Color.FromArgb(ShadeColorAlpha, Color.DimGray);
            ShowCropRegion(0, 0);
            Pick.WireControl(Picture);
            Pick.Enable();
        }


        public void shadeToColorAlpha(int i)
        {
            ShadeColorAlpha = i;
            ShadeColor = Color.FromArgb(ShadeColorAlpha, Color.DimGray);// background color
            Picture.Refresh();
        }

        private void ShowCropRegion(int x, int y)
        {
            if (x < 0 || x > Picture.Width)
            {
                x = 0;
            }

            if (y < 0 || y > Picture.Height)
            {
                y = 0;
            }

            var w = WidthOfContainer;
            var h = HeightOfContainer;

            if (Img != null)
            {
                if (x + w >= Picture.Width)
                {
                    w = Picture.Width - x;
                }

                if (y + h >= Picture.Height)
                {
                    h = Picture.Height - y;
                }
            }
            CropRectangle = new Rectangle(x, y, w, h);
            ShadeRectangle = Picture.DisplayRectangle;
            ShadedRegion = new Region(ShadeRectangle);
            ShadedRegion.Xor(CropRectangle);

            if (Picture != null)
            {
                Picture.Refresh();
            }

            /*
            return;
            var scalar = Tracker.Value * 0.01;
            var cropWidth = WidthOfContainer * scalar;
            var cropHeight = HeightOfContainer * scalar;

            if (x < 0)
            {
                x = 0;
            }

            if (y < 0)
            {
                y = 0;
            }


            if (Img != null)
            {
                if (cropWidth + x > Picture.Width + Picture.Left)
                {
                    cropWidth = Img.Width - x;
                }


                if (cropHeight + y > Picture.Height + Picture.Top)
                {
                    cropHeight = Img.Height - y;
                }
            }

            //var w = (int)(cropWidth * scalar) / 2;
            //var h = (int)(cropHeight * scalar) / 2;

            CropRectangle = new Rectangle(x, y, (int)(cropWidth * scalar), (int)(cropHeight * scalar));
            ShadeRectangle = Picture.DisplayRectangle;
            ShadedRegion = new Region(ShadeRectangle);
            ShadedRegion.Xor(CropRectangle);

            cx = x;
            cy = y;

            if (Picture != null)
            {
                Picture.Refresh();
            }
             * */
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }


        public void ShadeToColorAlpha(int i)
        {
            ShadeColorAlpha = i;
            ShadeColor = Color.FromArgb(ShadeColorAlpha, Color.DimGray);// background color
            Picture.Refresh();
        }

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;

            if (CropRectangle.Contains(new Point(e.X, e.Y)))
            {
                AllowCrop = true;
                Pick.Disable();
                ShowCropRegion(e.X, e.Y);
            }
            else
            {
                AllowCrop = false;
                Pick.Enable();
            }

            x = e.X;
            y = e.Y;
        }


        private void Picture_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                if (AllowCrop)
                {
                    ShowCropRegion(e.X, e.Y);
                }
                else
                {
                    ShowCropRegion(0, 0);
                }
            }
        }

        private void Picture_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
        }

        private void Picture_Paint(object sender, PaintEventArgs e)
        {
            if (ShadedRegion != null)
            {
                Brush myBrush = new SolidBrush(ShadeColor);
                e.Graphics.FillRegion(myBrush, ShadedRegion);// draw shaded region
            }
        }


        private void Tracker_ValueChanged(object sender, EventArgs e)
        {
            var tracker = Tracker.Value * 0.1;
            if (Img != null)
            {
                Picture.Width = (int)(w * tracker);
                Picture.Height = (int)(h * tracker);
            }
        }

        private void Picture_SizeChanged(object sender, EventArgs e)
        {
            w = Picture.Width;
            h = Picture.Height;
            ShowCropRegion(0, 0);
        }


        private void ButtonZoomIn_MouseClick(object sender, MouseEventArgs e)
        {
            if (Tracker.Value > Tracker.Minimum)
            {
                Tracker.Value--;
            }
        }

        private void ButtonZoomOut_Click(object sender, EventArgs e)
        {
            if (Tracker.Value < Tracker.Maximum)
            {
                Tracker.Value++;
            }
        }

        private void ButtonChooseFile_Click(object sender, EventArgs e)
        {
            OpenFile.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
            if (OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //var img = Image.FromFile(OpenFile.FileName);
                //var bmp = new Bitmap(img.Width, img.Height);
                //var graphics = Graphics.FromImage(bmp);
                //graphics.DrawImage(img,0,0);
                //graphics.Dispose();

                SetImage(new Bitmap(Image.FromFile(OpenFile.FileName)));
            }
        }


        private void ButtonSmall_Click(object sender, EventArgs e)
        {

        }

        private void ButtonBig_Click(object sender, EventArgs e)
        {

        }

        private void Tools_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void Picture_DoubleClick(object sender, EventArgs e)
        {
            if (CropRectangle != Rectangle.Empty)
            {
                var image = new Bitmap(CropRectangle.Width, CropRectangle.Height);
                Picture.DrawToBitmap(image, new Rectangle(0, 0, CropRectangle.Width, CropRectangle.Height));
                SetImage(image);
                Result = image;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (CropRectangle != Rectangle.Empty)
            {
                AllowCrop = false;
                Picture.Refresh();

                var imageAll = new Bitmap(Picture.Width, Picture.Height);
                var rectangle = new Rectangle();
                rectangle.X = CropRectangle.X;
                rectangle.Y = CropRectangle.Y;
                if (CropRectangle.X + CropRectangle.Width > Picture.Width)
                {
                    rectangle.Width = Picture.Width - x;
                }
                else
                {
                    rectangle.Width = CropRectangle.Width;
                }

                if (CropRectangle.Y + CropRectangle.Height > Picture.Height)
                {
                    rectangle.Height = Picture.Height - y;
                }
                else
                {
                    rectangle.Height = CropRectangle.Height;
                }

                Picture.DrawToBitmap(imageAll, Picture.ClientRectangle);

                /*
                using (var imageSpecial = new Bitmap(rectangle.Width, rectangle.Height, System.Drawing.Imaging.PixelFormat.Undefined))
                {
                    var graphics = Graphics.FromImage(imageSpecial);

                    imageSpecial.SetResolution(imageAll.HorizontalResolution, imageAll.VerticalResolution);
                    graphics.Clear(Color.White);
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.DrawImage(imageAll, rectangle);
                    graphics.Dispose();

                    Result = imageSpecial;
                }
                 */

                var imageSpecial = imageAll.Clone(rectangle, System.Drawing.Imaging.PixelFormat.DontCare);
                Result = imageSpecial;
            }

            DialogResult = DialogResult.OK;
        }

        private void BoxContainer_Click(object sender, EventArgs e)
        {
            AllowCrop = false;
            Pick.Enable();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BoxContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ButtonResize_Click(object sender, EventArgs e)
        {
            Picture.Width = WidthOfContainer;
            Picture.Height = HeightOfContainer;
        }
    }
}
