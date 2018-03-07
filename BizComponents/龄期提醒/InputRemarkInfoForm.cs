using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BizComponents
{
    public partial class InputRemarkInfoForm : Form
    {
        private int optionType = 0;
        private string id = string.Empty;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="optionType">optionType=1原因分析;optionType=2监理意见;optionType=3领导意见</param>
        public InputRemarkInfoForm(int optionType,string id)
        {
            InitializeComponent();
            this.optionType = optionType;
            this.id = id;
            InputRemarkInfoFormInit();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRemarkInfo.Text))
            {
                switch (optionType)
                {
                    case 1:
                        {
                            DepositoryLabStadiumList.SetSGComment(txtRemarkInfo.Text, Yqun.Common.ContextCache.ApplicationContext.Current.UserCode,id);
                            break;
                        }
                    case 2:
                        {
                            DepositoryLabStadiumList.SetJLComment(txtRemarkInfo.Text, Yqun.Common.ContextCache.ApplicationContext.Current.UserCode, id);
                            break;
                        }
                    case 3:
                        {
                            DepositoryLabStadiumList.SetJLComment(txtRemarkInfo.Text, Yqun.Common.ContextCache.ApplicationContext.Current.UserCode, id);
                            break;
                        }
                    default:
                        { break; }
                }
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InputRemarkInfoFormInit()
        {
            switch (optionType)
            {
                case 1:
                    {
                        groupBoxRemarkInfo.Text = "原因分析";
                        this.Text = "龄期提醒原因分析";
                        txtRemarkInfo.Text = DepositoryLabStadiumList.GetSGComment(id);
                        break;
                    }
                case 2:
                    {
                        groupBoxRemarkInfo.Text = "监理意见";
                        this.Text = "龄期提醒监理意见";
                        txtRemarkInfo.Text = DepositoryLabStadiumList.GetJLComment(id);
                        break;
                    }
                case 3:
                    {
                        groupBoxRemarkInfo.Text = "领导意见";
                        this.Text = "龄期提醒领导意见";
                        txtRemarkInfo.Text = DepositoryLabStadiumList.GetJLComment(id);
                        break;
                    }
                default:
                    { break; }
            }
        }
    }
}
