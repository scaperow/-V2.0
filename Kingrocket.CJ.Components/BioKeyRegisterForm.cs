using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yqun.Services;
using BizCommon;
using ShuXianCaiJiComponents;

namespace Kingrocket.CJ.Components
{
    public partial class BioKeyRegisterForm : Form
    {
        public BioKeyRegisterForm()
        {
            InitializeComponent();
        }

        private void button_Register_Click(object sender, EventArgs e)
        {
            //Boolean Result = BioKeyComponents.IsHaveName(this.treeView1.SelectedNode.Text);
            //if (Result)
            //{
                if (AxZKFPEngX1.IsRegister)
                {
                    AxZKFPEngX1.CancelEnroll();
                }
                AxZKFPEngX1.BeginEnroll();
            //}
            //else
            //{
            //    MessageBox.Show("当前用户已经有指纹备案！");
            //}
            
        }

        private void BioKeyRegisterForm_Load(object sender, EventArgs e)
        {
            if (AxZKFPEngX1.InitEngine() != 0)
            {
                MessageBox.Show("指纹仪初始化失败");
            }
            CaijiKeyHelper.Init(this.treeView1);
        }

        void AxZKFPEngX1_OnImageReceived(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEvent e)
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
        void AxZKFPEngX1_OnFeatureInfo(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnFeatureInfoEvent e)
        {
            
        }       

        void AxZKFPEngX1_OnEnroll(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEvent e)
        {
            Selection Selection = this.treeView1.SelectedNode.Parent.Parent.Tag as Selection;
            BioKeyInfo BioKeyInfo = new BioKeyInfo();
            BioKeyInfo.RegisterID = Guid.NewGuid().ToString();
            BioKeyInfo.RegisterName = this.treeView1.SelectedNode.Text;
            BioKeyInfo.Template = e.aTemplate;
            BioKeyInfo.UserCode = this.treeView1.SelectedNode.Name;
            BioKeyInfo.RalationID = Selection.ID;

            if (CaijiKeyHelper.IsExist(BioKeyInfo.UserCode))
            {
                MessageBox.Show("指纹已经存在！");
                return;
            }

            Boolean Result = CaijiKeyHelper.SaveRegister(BioKeyInfo);
            if (Result)
            {
                MessageBox.Show("指纹登记成功");
            }
            else
            {
                MessageBox.Show("指纹登记失败");
            }
        }

        private void BioKeyRegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AxZKFPEngX1.EndEngine();
            AxZKFPEngX1.Dispose();
        }

    }
}
