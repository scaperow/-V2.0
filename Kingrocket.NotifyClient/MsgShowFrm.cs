using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Kingrocket.NotifyClient
{
    public partial class MsgShowFrm : Form
    {
        public int MaxWidth = 250;
        public int MaxHeight = 170;
        public int StayTime = 5000;
        public string MessageContent;

        private System.Threading.Timer timerIn;
        private System.Threading.Timer timerStop;
        private System.Threading.Timer timerOut;

        public MsgShowFrm(string _MessageContent)
        {
            MessageContent = _MessageContent;
            InitializeComponent();
        }
        public void ScrollShow()
        {
            this.Width = MaxWidth;
            this.Height = 0;
            this.ShowDialog();
            //this.timerIn.Enabled = true;
        }
        private void ScrollUp()
        {
            if (Height < MaxHeight)
            {
                this.Height += 3;
                this.Location = new Point(this.Location.X, this.Location.Y - 3);
            }
            else
            {
                //this.timerIn.Enabled = false;
                //this.timerStop.Enabled = true;
                //timerStop = new System.Threading.Timer(p =>
                //{
                //    timerOut = new System.Threading.Timer(p1 =>
                //    {
                //        ScrollDown();
                //    }, null, 0, 10);
                //    timerStop.Dispose();

                //}, null, StayTime, 10);
                timerIn.Dispose();
            }
        }
        private void ScrollDown()
        {
            if (Height > 3)
            {
                this.Height -= 3;
                this.Location = new Point(this.Location.X, this.Location.Y + 3);
            }
            else
            {
                //this.timerOut.Enabled = false;
                this.Close();
            }
        }

        //private void timerIn_Tick(object sender, EventArgs e)
        //{
        //    ScrollUp();
        //}

        //private void timerStop_Tick(object sender, EventArgs e)
        //{
        //    timerStop.Enabled = false;
        //    timerOut.Enabled = true;
        //}

        //private void timerOut_Tick(object sender, EventArgs e)
        //{
        //    ScrollDown();
        //}

        private void MsgShowFrm_Load(object sender, EventArgs e)
        {
            Screen[] screens = Screen.AllScreens;
            Screen screen = screens[0];//获取屏幕变量
            this.Location = new Point(screen.WorkingArea.Width - MaxWidth - 20, screen.WorkingArea.Height - 40);//WorkingArea为Windows桌面的工作区
            //this.timerStop.Interval = StayTime;

            timerIn = new System.Threading.Timer(p =>
            {
                ScrollUp();
            }, null, 0, 10);

            lblMsg.Text = MessageContent;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ScrollDown();
            this.Close();
        }
        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }
    }
}
