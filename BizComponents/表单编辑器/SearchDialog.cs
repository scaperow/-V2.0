using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Bases;

namespace BizComponents
{
    public partial class SearchDialog : Form
    {
        TreeView Tree;

        public SearchDialog(TreeView Tree)
        {
            InitializeComponent();

            this.Tree = Tree;
        }

        private void SearchDialog_Load(object sender, EventArgs e)
        {
            TextBox_Search.Focus();
        }

        /// <summary>
        /// 汉字转拼音缩写
        /// </summary>
        /// <param name="str">要转换的汉字字符串</param>
        /// <returns>拼音缩写</returns>
        private string GetPinYinString(string chineseString)
        {
            string string_tempStr = "";
            foreach (char c in chineseString)
            {
                //字母和符号原样保留
                if ((int)c >= 33 && (int)c <= 126)
                {
                    string_tempStr += c.ToString();
                }
                else//累加拼音声母        
                {
                    string_tempStr += GetPinYinChar(c.ToString());
                }
            }
            return string_tempStr;
        }

        /// <summary>
        /// 取单个字符的拼音声母     
        /// </summary>
        /// <param name="c">要转换的单个汉字</param>
        /// <returns>拼音声母</returns>
        private string GetPinYinChar(string c)
        {
            if (c.Trim() == string.Empty) 
                return "*";

            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c.ToLower());

            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));

            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "a";
            if (i < 0xB2C1) return "b";
            if (i < 0xB4EE) return "c";
            if (i < 0xB6EA) return "d";
            if (i < 0xB7A2) return "e";
            if (i < 0xB8C1) return "f";
            if (i < 0xB9FE) return "g";
            if (i < 0xBBF7) return "h";
            if (i < 0xBFA6) return "g";
            if (i < 0xC0AC) return "k";
            if (i < 0xC2E8) return "l";
            if (i < 0xC4C3) return "m";
            if (i < 0xC5B6) return "n";
            if (i < 0xC5BE) return "o";
            if (i < 0xC6DA) return "p";
            if (i < 0xC8BB) return "q";
            if (i < 0xC8F6) return "r";
            if (i < 0xCBFA) return "s";
            if (i < 0xCDDA) return "t";
            if (i < 0xCEF4) return "w";
            if (i < 0xD1B9) return "x";
            if (i < 0xD4D1) return "y";
            if (i < 0xD7FA) return "z";

            return "*";
        }

        private void FindNodeByPinYin(string pinyin)
        {
            if (pinyin == "")
                return;

            TreeNode NextNode = Tree.TopNode;
            while (NextNode != null)
            {
                Selection Selection = NextNode.Tag as Selection;
                if (Selection != null && Convert.ToBoolean(Selection.TypeFlag) && GetPinYinString(NextNode.Text).ToLower().StartsWith(pinyin.ToLower()))
                {
                    NextNode.ForeColor = Color.Red;
                }
                else
                {
                    NextNode.ForeColor = Color.Black;
                }

                if (NextNode.FirstNode != null)
                {
                    NextNode = NextNode.FirstNode;
                }
                else if (NextNode.NextNode != null)
                {
                    NextNode = NextNode.NextNode;
                }
                else
                {
                    if (NextNode.Parent != null)
                    {
                        TreeNode tempNode = NextNode.Parent;
                        while (tempNode.NextNode == null)
                        {
                            if (tempNode.Parent == null)
                                break;
                            tempNode = tempNode.Parent;
                        }

                        NextNode = tempNode.NextNode;
                    }
                    else
                    {
                        NextNode = NextNode.Parent;
                    }
                }
            }
        }

        private void FindNodeByKey(string key)
        {
            if (key == "")
                return;

            TreeNode NextNode = Tree.TopNode;
            while (NextNode != null)
            {
                Selection Selection = NextNode.Tag as Selection;
                if (Selection != null && Convert.ToBoolean(Selection.TypeFlag) && NextNode.Text.ToLower().StartsWith(key.ToLower()))
                {
                    NextNode.ForeColor = Color.Red;
                }
                else
                {
                    NextNode.ForeColor = Color.Black;
                }

                if (NextNode.FirstNode != null)
                {
                    NextNode = NextNode.FirstNode;
                }
                else if (NextNode.NextNode != null)
                {
                    NextNode = NextNode.NextNode;
                }
                else
                {
                    if (NextNode.Parent != null)
                    {
                        TreeNode tempNode = NextNode.Parent;
                        while (tempNode.NextNode == null)
                        {
                            if (tempNode.Parent == null)
                                break;
                            tempNode = tempNode.Parent;
                        }

                        NextNode = tempNode.NextNode;
                    }
                    else
                    {
                        NextNode = NextNode.Parent;
                    }
                }
            }
        }
        
        private void TextBox_Search_TextChanged(object sender, EventArgs e)
        {
            if (radioButton_JP.Checked)
            {
                FindNodeByPinYin(TextBox_Search.Text.ToLower().Trim());
            }
            else
            {
                FindNodeByKey(TextBox_Search.Text.Trim());
            }
        }

        private void SearchDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            TreeNode NextNode = Tree.TopNode;
            while (NextNode != null)
            {
                NextNode.ForeColor = Color.Black;

                if (NextNode.FirstNode != null)
                {
                    NextNode = NextNode.FirstNode;
                }
                else if (NextNode.NextNode != null)
                {
                    NextNode = NextNode.NextNode;
                }
                else
                {
                    if (NextNode.Parent != null)
                    {
                        TreeNode tempNode = NextNode.Parent;
                        while (tempNode.NextNode == null)
                        {
                            if (tempNode.Parent == null)
                                break;
                            tempNode = tempNode.Parent;
                        }

                        NextNode = tempNode.NextNode;
                    }
                    else
                    {
                        NextNode = NextNode.Parent;
                    }
                }
            }
        }
    }
}
