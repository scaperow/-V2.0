using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Yqun.Common.Encoder;
using System.Security.Cryptography;

namespace SerialGenerator
{
    public partial class Generator : Form
    {
        public Generator()
        {
            InitializeComponent();
        }

        private void Btn_Generator_Click(object sender, EventArgs e)
        {
            string regkey = GetHashCode(GetSerialNumber());
            textBox3.Text = regkey;
            textBox4.Text = "你的序列号已经成功生成! \r\n\r\n用户信息如下:\r\n\r\n用户: " + textBox1.Text + "\r\n\r\n激活码: " + regkey + "\r\n\r\n如果你使用这些信息碰到问题,请联系我们,电话：010-51916122转601，029-62763213。";
            textBox4.Focus();
            textBox4.SelectAll(); 
        }

        public string GetHashCode(string SerialNumber)
        {
            SerialNumber = Reverse(SerialNumber);

            Hasher hs = new Hasher();
            hs.HasherText = SerialNumber;
            return hs.MD5Hasher();
        }

        private string Reverse(string str)
        {
            char[] _chars = str.ToCharArray();
            Array.Reverse(_chars);
            string newstr = new string(_chars);
            return newstr;
        }

        private string GetSerialNumber()
        {
            StringBuilder Str = new StringBuilder();
            Str.Append(textBox1.Text.ToUpper());

            return Str.ToString();
        }

        private void Button_Copy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text))
                Clipboard.SetText(textBox3.Text);
        }
    }

    public class Hasher
    {
        public string _HasherText;

        public string HasherText
        {
            set 
            { 
                _HasherText = value; 
            }
            get 
            { 
                return _HasherText; 
            }
        }


        public string MD5Hasher()
        {
            byte[] MD5Data = System.Text.Encoding.UTF8.GetBytes(HasherText);
            MD5 MD5 = new MD5CryptoServiceProvider();
            byte[] Result = MD5.ComputeHash(MD5Data);
            return System.Convert.ToBase64String(Result);
        }
    }
}
