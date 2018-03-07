using System;
using System.Drawing;
using System.Windows.Forms;
using ReportCommon;

namespace ReportComponents
{
    public partial class ImageEditor : Form
    {
        Report report;
        GridElement Element;
        String Index;
        public ImageEditor(Report report, String Index, GridElement Element)
        {
            InitializeComponent();

            this.report = report;
            this.Element = Element;
            this.Index = Index;
        }

        public GridElement ReportElement
        {
            get
            {
                return this.Element;
            }
        }

        private void ImageEditor_Load(object sender, EventArgs e)
        {
            if (Element != null && Element.Value is Picture)
            {
                Picture image = Element.Value as Picture;
                pictureBox1.Image = image.Image;
            }
        }

        private void Button_Browse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "图片文件 (*.jpg)|*.jpg";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            Picture Picture = null;
            if (Element == null || !(Element.Value is Picture))
            {
                Picture = new Picture();
            }
            else
            {
                Picture = Element.Value as Picture;
            }

            Picture.Image = pictureBox1.Image;

            this.DialogResult = DialogResult.OK;
            Close();
        }

    }
}
