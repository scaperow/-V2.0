using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace YqunMainAppBase
{
	public class SplashScreen : Form
	{
		private const double OpacityDecrement = .08;
		private const double OpacityIncrement = .05;
		private const int TimerInterval = 50;
		private static Boolean FadeMode;
		private static Boolean FadeInOut;
		private static Image BGImage;
		private static String Status;
        private static Boolean FormVisible;
		private static SplashScreen SplashScreenForm;
		private static Thread SplashScreenThread;
		private static Color TransparentKey;
        private System.Windows.Forms.Timer SplashTimer;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Label ReserveAllRight;
        private Label StatusLabel;
        private Label label1;
		private IContainer components;

		#region Public Properties & Methods

        public Boolean SetVisible
        {
            get 
            { 
                return FormVisible; 
            }
            set
            {
                FormVisible = value;
                UpdateVisible();
            }
        }

		public String SetStatus
		{
			get { return Status; }
			set
			{
				Status = value;
                UpdateStatus();
			}
		}

		public Image SetBackgroundImage
		{
			get { return BGImage; }
			set
			{
				BGImage = value;
				if (value != null)
				{
					pictureBox1.Image = BGImage;
				}
			}
		}

		public Color SetTransparentKey
		{
			get { return TransparentKey; }
			set
			{
				TransparentKey = value;
				if (value != Color.Empty)
					TransparencyKey = SetTransparentKey;
			}
		}

		public Boolean SetFade
		{
			get { return FadeInOut; }
			set
			{
				FadeInOut = value;
				Opacity = value ? .00 : 1.00;
			}
		}

		public static SplashScreen Current
		{
			get
			{
				if (SplashScreenForm == null ||
                    SplashScreenForm.IsDisposed)
					SplashScreenForm = new SplashScreen();
				return SplashScreenForm;
			}
		}

		public void SetStatusLabel(Point StatusLabelLocation, Int32 StatusLabelWidth, Int32 StatusLabelHeight)
		{
			if (StatusLabelLocation != Point.Empty)
				StatusLabel.Location = StatusLabelLocation;
			if (StatusLabelWidth == 0 && StatusLabelHeight == 0)
				StatusLabel.AutoSize = true;
			else
			{
				if (StatusLabelWidth > 0)
					StatusLabel.Width = StatusLabelWidth;
				if (StatusLabelHeight > 0)
					StatusLabel.Height = StatusLabelHeight;
			}
		}

		public void ShowSplashScreen()
		{
			SplashScreenThread = new Thread(new ThreadStart(ShowForm));
            SplashScreenThread.Priority = ThreadPriority.AboveNormal;
			SplashScreenThread.IsBackground = true;
			SplashScreenThread.Name = "SplashScreenThread";
			SplashScreenThread.Start();
		}

		public void CloseSplashScreen()
		{
			if (SplashScreenForm != null)
			{
				if(InvokeRequired)
				{
                    Delegate ClosingDelegate = new MethodInvoker(HideSplash);
					Invoke(ClosingDelegate);
				}
				else
				{
					HideSplash();
				}
			}
		}
		#endregion

		public SplashScreen()
		{
			InitializeComponent();
		}

		private static void ShowForm()
		{
            Application.Run(SplashScreenForm);
		}

        private void UpdateVisible()
        {
            if (this.InvokeRequired)
            {
                Delegate VisibleUpdate = new MethodInvoker(UpdateVisible);
                Invoke(VisibleUpdate);
            }
            else
            {
                this.Visible = SetVisible;
            }
        }

		private void UpdateStatus()
		{
            if (this.InvokeRequired)
            {
                Delegate StatusUpdate = new MethodInvoker(UpdateStatus);
                Invoke(StatusUpdate);
            }
            else
            {
                StatusLabel.Text = SetStatus;
            }
		}

		private void SplashTimer_Tick(object sender, EventArgs e)
		{
			if(FadeMode) // Form is opening (Increment)
			{
				if (Opacity < 1.00)
					Opacity += OpacityIncrement;
				else
					SplashTimer.Stop();
			}
			else // Form is closing (Decrement)
			{
				if(Opacity > .00)
					Opacity -= OpacityDecrement;
				else
					Dispose();
			}
			
		}

		#region InitComponents

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.SplashTimer = new System.Windows.Forms.Timer(this.components);
            this.StatusLabel = new System.Windows.Forms.Label();
            this.ReserveAllRight = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // SplashTimer
            // 
            this.SplashTimer.Tick += new System.EventHandler(this.SplashTimer_Tick);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(28, 298);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 12);
            this.StatusLabel.TabIndex = 11;
            // 
            // ReserveAllRight
            // 
            this.ReserveAllRight.AutoSize = true;
            this.ReserveAllRight.BackColor = System.Drawing.Color.Transparent;
            this.ReserveAllRight.Font = new System.Drawing.Font("宋体", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ReserveAllRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ReserveAllRight.Location = new System.Drawing.Point(267, 313);
            this.ReserveAllRight.Name = "ReserveAllRight";
            this.ReserveAllRight.Size = new System.Drawing.Size(275, 12);
            this.ReserveAllRight.TabIndex = 10;
            this.ReserveAllRight.Text = "Copyright © 北京金舟神创科技发展有限公司 2014";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::YqunMainAppBase.Resource.RedBar;
            this.pictureBox2.Location = new System.Drawing.Point(0, 285);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(556, 9);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(556, 285);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(266, 332);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "保留所有权利";
            // 
            // SplashScreen
            // 
            this.ClientSize = new System.Drawing.Size(556, 361);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ReserveAllRight);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private void SplashScreen_Load(object sender, EventArgs e)
		{
			if (SetFade)
			{
				FadeMode = true;
				SplashTimer.Interval = TimerInterval;
				SplashTimer.Start();
			}
		}

		private void HideSplash()
		{
			if(SetFade)
			{
				FadeMode = false;
				SplashTimer.Start();
			}
			else
				Dispose();
		}
	}
}