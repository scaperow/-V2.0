using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BizComponents.Properties;

namespace BizComponents.试验数据检测
{
    public partial class ImageShowForm : Form
    {
        InvalidReportNote _InvalidReportNote = null;
        public int CurrentNum = 0;

        public ImageShowForm(InvalidReportNote temp)
        {
            InitializeComponent();
            _InvalidReportNote = temp;
        }

        private void ImageShowForm_Load(object sender, EventArgs e)
        {
            this.pictureBoxLeft.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxRight.BackColor = System.Drawing.Color.Transparent;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBoxLeft_Click(object sender, EventArgs e)
        {
            CurrentNum--;
            _InvalidReportNote.SetImageShow(CurrentNum,false);
        }

        private void pictureBoxRight_Click(object sender, EventArgs e)
        {
            CurrentNum++;
            _InvalidReportNote.SetImageShow(CurrentNum,false);
        }

        private void pictureBoxLeft_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxLeft.Image = null;
        }

        private void pictureBoxLeft_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBoxLeft.Image = Resources.PrevButton;
        }

        private void pictureBoxRight_MouseMove(object sender, MouseEventArgs e)
        {

            pictureBoxRight.Image = Resources.NextButton;
        }

        private void pictureBoxRight_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxRight.Image = null;
        }

        private void pictureBoxClose_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBoxClose.Image = Resources.del;
        }

        private void pictureBoxClose_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxClose.Image = null;
        }
    }
}
