using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Yqun.Bases
{
    public class ProgressScreen : Form
    {
        private static ProgressScreen SplashScreenForm;
        private static Thread SplashScreenThread;
        private static Color TransparentKey;
        private Label StatusLabel;
        private ProgressBar progressBar1;
        private static String Status;
        private static int Step;

        #region Public Properties & Methods

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

        public static ProgressScreen Current
        {
            get
            {
                if (SplashScreenForm == null)
                    SplashScreenForm = new ProgressScreen();
                return SplashScreenForm;
            }
        }

        public void ShowSplashScreen()
        {
            SplashScreenThread = new Thread(new ThreadStart(ShowForm));
            SplashScreenThread.IsBackground = true;
            SplashScreenThread.Name = "SplashScreenThread";
            SplashScreenThread.Start();
        }

        public void CloseSplashScreen()
        {
            if (SplashScreenForm != null)
            {
                try
                {
                    if (InvokeRequired)
                    {
                        MethodInvoker methodInvoker = new MethodInvoker(HideSplash);
                        Invoke(methodInvoker);
                    }
                    else
                    {
                        HideSplash();
                    }
                }
                catch 
                {
                    
                }
                
            }
        }
        #endregion

        public ProgressScreen()
        {
            InitializeComponent();
        }

        private static void ShowForm()
        {
            Application.Run(SplashScreenForm);
        }

        public String SetStatus
        {
            get { return Status; }
            set
            {
                Status = value;
                if (StatusLabel.InvokeRequired)
                {
                    MethodInvoker methodInvoker = new MethodInvoker(UpdateStatus);
                    Invoke(methodInvoker);
                }
                else
                {
                    UpdateStatus();
                }
            }
        }

        public int SetStep
        {
            get { return Step; }
            set
            {
                Step = value;
                if (progressBar1.InvokeRequired)
                {
                    MethodInvoker methodInvoker = new MethodInvoker(UpdateStep);
                    Invoke(methodInvoker);
                }
                else
                {
                    UpdateStep();
                }
            }
        }

        #region InitComponents

        private void InitializeComponent()
        {
            this.StatusLabel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // StatusLabel
            // 
            this.StatusLabel.Location = new System.Drawing.Point(0, 1);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Padding = new System.Windows.Forms.Padding(2);
            this.StatusLabel.Size = new System.Drawing.Size(350, 17);
            this.StatusLabel.TabIndex = 1;
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.Blue;
            this.progressBar1.Location = new System.Drawing.Point(0, 20);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(351, 14);
            this.progressBar1.TabIndex = 0;
            // 
            // ProgressScreen
            // 
            this.ClientSize = new System.Drawing.Size(352, 35);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.StatusLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProgressScreen";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private void SplashScreen_Load(object sender, EventArgs e)
        {
        }

        private void UpdateStatus()
        {
            StatusLabel.Text = SetStatus;
        }

        private void UpdateStep()
        {
            progressBar1.Value = SetStep;
        }

        private void HideSplash()
        {
            SplashScreenForm = null;
            Dispose();
        }
    }
}
