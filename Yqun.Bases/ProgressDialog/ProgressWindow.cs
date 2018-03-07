using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace Yqun.Bases
{
	/// <summary>
	/// Summary description for ProgressWindow.
	/// </summary>
	public class ProgressWindow : System.Windows.Forms.Form, IProgressCallback
	{
        private static ProgressWindow progressWindow = null;

        private System.ComponentModel.Container components = null;
        private Button cancelButton;
		private System.Windows.Forms.Label label;
		private System.Windows.Forms.ProgressBar progressBar;

		public delegate void SetTextInvoker(String text);
		public delegate void IncrementInvoker( int val );
		public delegate void StepToInvoker( int val );
		public delegate void RangeInvoker( int minimum, int maximum );

		private String titleRoot = "";
		private System.Threading.ManualResetEvent initEvent = new System.Threading.ManualResetEvent(false);
		private System.Threading.ManualResetEvent abortEvent = new System.Threading.ManualResetEvent(false);
		private bool requiresClose = true;
        private bool disabledCancel;
        private object userState;

		public ProgressWindow()
		{
			InitializeComponent();
		}

        public static void ShowForm(String Caption, WaitCallback callBack, object userState)
        {
            ShowForm(Caption, Rectangle.Empty, false, false, callBack, userState);
        }

        public static void ShowForm(String Caption, Rectangle Bounds, Boolean TopMost, Boolean disabledCancel, WaitCallback callBack, object userState)
        {
            if (progressWindow == null)
                progressWindow = new ProgressWindow();
            progressWindow.Text = Caption;
            progressWindow.TopMost = TopMost;
            if (Bounds != Rectangle.Empty)
                progressWindow.Bounds = Bounds;
            progressWindow.disabledCancel = disabledCancel;
            ((IProgressCallback)progressWindow).UserState = userState;
            ThreadPool.QueueUserWorkItem(callBack, progressWindow);
            progressWindow.ShowDialog();
        }

        public static void ShowForm(String Caption, Rectangle Bounds, Boolean TopMost, Boolean disabledCancel, ParameterizedThreadStart callBack, object userState)
        {
            if (progressWindow == null)
                progressWindow = new ProgressWindow();
            progressWindow.Text = Caption;
            progressWindow.TopMost = TopMost;
            if (Bounds != Rectangle.Empty)
                progressWindow.Bounds = Bounds;
            progressWindow.disabledCancel = disabledCancel;
            ((IProgressCallback)progressWindow).UserState = userState;
            Thread thread = new Thread(callBack);
            thread.IsBackground = false;
            thread.Start(progressWindow);
            progressWindow.ShowDialog();
        }

		#region Implementation of IProgressCallback

		/// <summary>
		/// Call this method from the worker thread to initialize
		/// the progress meter.
		/// </summary>
		/// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
		/// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
		public void Begin( int minimum, int maximum )
		{
			initEvent.WaitOne();
			Invoke( new RangeInvoker( DoBegin ), new object[] { minimum, maximum } );
		}

		/// <summary>
		/// Call this method from the worker thread to initialize
		/// the progress callback, without setting the range
		/// </summary>
		public void Begin()
		{
			initEvent.WaitOne();
			Invoke( new MethodInvoker( DoBegin ) );
		}

		/// <summary>
		/// Call this method from the worker thread to reset the range in the progress callback
		/// </summary>
		/// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
		/// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
		/// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
		public void SetRange( int minimum, int maximum )
		{
			initEvent.WaitOne();
			Invoke( new RangeInvoker( DoSetRange ), new object[] { minimum, maximum } );
		}

		/// <summary>
		/// Call this method from the worker thread to update the progress text.
		/// </summary>
		/// <param name="text">The progress text to display</param>
        public void SetText(String text)
        {
            try
            {
                Invoke(new SetTextInvoker(DoSetText), new object[] { text });
            }
            catch
            {
            }
        }

		/// <summary>
		/// Call this method from the worker thread to increase the progress counter by a specified value.
		/// </summary>
		/// <param name="val">The amount by which to increment the progress indicator</param>
		public void Increment( int val )
		{
			Invoke( new IncrementInvoker( DoIncrement ), new object[] { val } );
		}

		/// <summary>
		/// Call this method from the worker thread to step the progress meter to a particular value.
		/// </summary>
		/// <param name="val"></param>
		public void StepTo( int val )
		{
			Invoke( new StepToInvoker( DoStepTo ), new object[] { val } );
		}

		/// <summary>
		/// If this property is true, then you should abort work
		/// </summary>
		public bool IsAborting
		{
			get
			{
				return abortEvent.WaitOne( 0, false );
			}
		}

		/// <summary>
		/// Call this method from the worker thread to finalize the progress meter
		/// </summary>
		public void End()
		{
			if( requiresClose )
			{
				Invoke( new MethodInvoker( DoEnd ) );
			}
		}

        /// <summary>
        /// 用户自定义状态
        /// </summary>
        public object UserState
        {
            get
            {
                return userState;
            }
            set
            {
                userState = value;
            }
        }

		#endregion

		#region Implementation members invoked on the owner thread

		private void DoSetText( String text )
		{
			label.Text = text;
		}

		private void DoIncrement( int val )
		{
			progressBar.Increment( val );
			UpdateStatusText();
		}

		private void DoStepTo( int val )
		{
			progressBar.Value = val;
			UpdateStatusText();
		}

		private void DoBegin( int minimum, int maximum )
		{
			DoBegin();
			DoSetRange( minimum, maximum );
		}

		private void DoBegin()
		{
			cancelButton.Enabled = !disabledCancel;
		}

		private void DoSetRange( int minimum, int maximum )
		{
			progressBar.Minimum = minimum;
			progressBar.Maximum = maximum;
			progressBar.Value = minimum;
            titleRoot = Text;
		}

		private void DoEnd()
		{
			Close();
            Dispose();
            progressWindow = null;
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Handles the form load, and sets an event to ensure that
		/// intialization is synchronized with the appearance of the form.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(System.EventArgs e)
		{
			base.OnLoad( e );
			ControlBox = false;
			initEvent.Set();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Handler for 'Close' clicking
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			requiresClose = false;
			AbortWork();
			base.OnClosing( e );
		}

		#endregion
		
		#region Implementation Utilities

		/// <summary>
		/// Utility function that formats and updates the title bar text
		/// </summary>
		private void UpdateStatusText()
		{
            Text = titleRoot; //+ String.Format( " - {0}%", (progressBar.Value * 100 ) / (progressBar.Maximum - progressBar.Minimum) );
		}
		
		/// <summary>
		/// Utility function to terminate the thread
		/// </summary>
		private void AbortWork()
		{
			abortEvent.Set();
		}

		#endregion

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(0, 85);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(270, 20);
            this.progressBar.TabIndex = 1;
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label.Location = new System.Drawing.Point(0, 1);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(346, 83);
            this.label.TabIndex = 0;
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(271, 85);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 20);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "取消";
            // 
            // ProgressWindow
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(345, 106);
            this.ControlBox = false;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ProgressWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ProgressWindow";
            this.ResumeLayout(false);

		}

		#endregion
    }
}
