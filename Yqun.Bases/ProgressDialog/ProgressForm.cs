using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Yqun.Bases
{
    /// <summary>
    /// Simple progress form.
    /// </summary>
    public partial class ProgressForm : Form
    {
        public ProgressBar ProgressBar 
        { 
            get 
            { 
                return progressBar; 
            } 
        }

        public object Argument 
        { 
            get; 
            set;
        }

        public RunWorkerCompletedEventArgs Result 
        { 
            get; 
            private set; 
        }

        public bool CancellationPending
        {
            get 
            { 
                return worker.CancellationPending; 
            }
        }

        public string CancellingText 
        { 
            get; 
            set; 
        }

        public string DefaultStatusText
        {
            get
            {
                return labelStatus.Text;
            }
            set
            {
                labelStatus.Text = value;
            }
        }

        public delegate void DoWorkEventHandler(ProgressForm sender, DoWorkEventArgs e);

        public event DoWorkEventHandler DoWork;

        public ProgressForm()
        {
            InitializeComponent();

            DefaultStatusText = "请等待...";
            CancellingText = "取消操作...";

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        public void SetProgress(string status)
        {
            if (status != lastStatus && !worker.CancellationPending)
            {
                lastStatus = status;
                worker.ReportProgress(progressBar.Minimum - 1, status);
            }
        }

        public void SetProgress(int percent)
        {
            if (percent != lastPercent)
            {
                lastPercent = percent;
                worker.ReportProgress(percent);
            }
        }

        public void SetProgress(int percent, string status)
        {
            if (percent != lastPercent || (status != lastStatus && !worker.CancellationPending))
            {
                lastPercent = percent;
                lastStatus = status;
                worker.ReportProgress(percent, status);
            }
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            Result = null;
            buttonCancel.Enabled = true;
            progressBar.Value = progressBar.Minimum;
            labelStatus.Text = DefaultStatusText;
            lastStatus = DefaultStatusText;
            lastPercent = progressBar.Minimum;

            worker.RunWorkerAsync(Argument);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            worker.CancelAsync();

            buttonCancel.Enabled = false;
            labelStatus.Text = CancellingText;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (DoWork != null)
                DoWork(this, e);
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage >= progressBar.Minimum && e.ProgressPercentage <= progressBar.Maximum)
                progressBar.Value = e.ProgressPercentage;
            
            if (e.UserState != null && !worker.CancellationPending)
                labelStatus.Text = e.UserState.ToString();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Result = e;
            if (e.Error != null)
                DialogResult = DialogResult.Abort;
            else if (e.Cancelled)
                DialogResult = DialogResult.Cancel;
            else
                DialogResult = DialogResult.OK;
            Close();
        }

        BackgroundWorker worker;
        int lastPercent;
        string lastStatus;
    }
}
