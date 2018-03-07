using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kingrocket.CJ.Components
{
    public partial class DHBJForm : Form
    {

        private Object _DHBJ;
        public Object DHBJ
        {
            get
            { return _DHBJ; }
            set
            {
                _DHBJ = value;
            }
        }

        private float _diameter;
        private int _CrrNum;
        public DHBJForm(float diameter,int crrentNum)
        {
            InitializeComponent();
            _diameter = diameter;
            _CrrNum = crrentNum;
            label1.Text = "请输入第" + _CrrNum+ "根钢筋拉断后的断后标距";
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                MessageBox.Show("断后标距为空，请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }
            else
            {
                if (float.Parse(textBox1.Text) < 5 * _diameter)
                {
                    
                    if (MessageBox.Show("断后标距小于5倍直径，请确认是否正确，若重新输入请点击确定！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)==DialogResult.Yes)
                    {
                        textBox1.Text = "";
                        return;
                    }
                }
                this.DHBJ = this.textBox1.Text;
                
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
