using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BizCommon;
using ShuXianCaiJiComponents;

namespace Kingrocket.CJ.Components
{
    public partial class BioKeyInputForm : Form
    {
        Boolean IsEnd;

        private String _Name;

        private String UserCode;

        public String Name
        {
            get
            { return _Name; }
            set
            {
                _Name = value;
            }
        }

        public delegate void SetControlTextDel(Control _Control,string Text,bool _IsAppText);

        public BioKeyInputForm(String code)
        {
            InitializeComponent();
            this.UserCode = code;
        }

        void DrawPanel()
        {
            try
            {
                Graphics canvas = panel1.CreateGraphics();
                AxZKFPEngX1.PrintImageAt(canvas.GetHdc().ToInt32(), 0, 0, panel1.Width, panel1.Height);
                canvas.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void AxZKFPEngX1_OnCapture(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnCaptureEvent e)
        {
            try
            {
                List<BioKeyInfo> BioKeyInfos = new List<BioKeyInfo>();
                BioKeyInfos = CaijiKeyHelper.GetTemplates(this.UserCode);
                if (BioKeyInfos != null)
                {
                    foreach (BioKeyInfo BioKeyInfo in BioKeyInfos)
                    {
                        bool b = false;
                        Object obj = BioKeyInfo.Template;
                        if (!IsEnd)
                        {
                            if (AxZKFPEngX1.VerFinger(ref obj, e.aTemplate, false, ref b))
                            {
                                IsEnd = true;
                                this.label1.Text = "";
                                this.richTextBox1.SelectedText += "指纹对比通过" + "\r\n";
                                this.richTextBox1.SelectedText += "当前监理见证人为：" + BioKeyInfo.RegisterName;
                                MessageBox.Show("指纹对比通过，当前监理见证人为：" + BioKeyInfo.RegisterName);
                                this.Name = BioKeyInfo.RegisterName;
                                this.DialogResult = DialogResult.OK;
                                break;
                            }
                        }
                    }
                    if (!IsEnd)
                    {
                        this.richTextBox1.SelectedText += "指纹对比不通过,请重新输入指纹！" + "\r\n";
                    }
                }
                else
                {
                    this.richTextBox1.SelectedText += "读取服务器指纹记录失败,请重新输入指纹！" + "\r\n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void AxZKFPEngX1_OnImageReceived(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEvent e)
        {
            Thread UpData = new Thread(DrawPanel);
            UpData.IsBackground = true;
            UpData.Start();
        }

        private void BioKeyInputForm_Load(object sender, EventArgs e)
        {
            if (AxZKFPEngX1.InitEngine() != 0)
            {
                MessageBox.Show("指纹仪初始化失败");
                this.DialogResult = DialogResult.No;
                this.Close();
            }
            else
            {
                if (AxZKFPEngX1.IsRegister)
                {
                    AxZKFPEngX1.CancelEnroll();
                }
                AxZKFPEngX1.BeginCapture();
                Thread UpData = new Thread(BeginCountDown);
                UpData.IsBackground = true;
                UpData.Start();
            }
        }
        private void BioKeyInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AxZKFPEngX1.EndEngine();
            AxZKFPEngX1.Dispose();
        }


        private void BeginCountDown()
        {
            SetControlText(this.richTextBox1,"请在限定时间内进行指纹验证",true);
            for (int i = 180; i > 0; i--)
            {
                if (IsEnd)
                {
                    break;
                }
                else
                {
                    try
                    {
                        SetControlText(this.label1 ,i.ToString() + "S",false);
                        Thread.Sleep(1000);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            if (!IsEnd)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        public void SetControlText(Control _Control, string Text,bool _IsAppText)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetControlTextDel(SetControlText), new object[] { _Control, Text ,_IsAppText});
            }
            else
            {
                if (_IsAppText)
                {
                    _Control.Text += Text + System.Environment.NewLine;
                }
                else
                {
                    _Control.Text = Text + System.Environment.NewLine;
                }
            }
        }
    }
}
