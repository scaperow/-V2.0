using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Yqun.Common.Encoder;
using FarPoint.Win.Spread;
using System.IO;
using FarPoint.Win.Spread.CellType;
using System.Drawing;
using System.Text.RegularExpressions;

namespace BizComponents
{
    public class SymbolManager
    {
        FpSpread _FpSpread;
        ToolStrip _SymbolBar;

        public SymbolManager(FpSpread FpSpread, ToolStrip SymbolBar)
        {
            _FpSpread = FpSpread;
            _SymbolBar = SymbolBar;
        }

        public FpSpread FpSpread
        {
            get
            {
                return _FpSpread;
            }
        }

        public ToolStrip SymbolBar
        {
            get
            {
                return _SymbolBar;
            }
        }

        public void ShowSymbolDialog()
        {
            SymbolDialog SymbolDialog = new SymbolDialog(this);
            Form form = FpSpread.FindForm();
            SymbolDialog.Show(form);
        }

        /// <summary>
        /// 初始化符号工具栏
        /// </summary>
        public void InitSymbolBar(ToolStrip SymbolBar)
        {
            String SymbollibFile = Path.Combine(Application.StartupPath, "Symbollib.dll");
            String Symbols;
            if (File.Exists(SymbollibFile))
            {
                IniFile iniFile = new IniFile(SymbollibFile);
                Symbols = iniFile.IniReadValue("symbolbar", "Value");

                SymbolBar.Items.Clear();
                if (Symbols.IndexOf("\\n") >= 0)
                {
                    String[] strings = Symbols.Split(new string[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (String s in strings)
                    {
                        ToolStripButton Button = new ToolStripButton();
                        Button.Text = ConvertHexToChar(s);
                        Button.Click += new EventHandler(Button_Click);
                        SymbolBar.Items.Add(Button);
                    }
                }
                else
                {
                    char[] chars = Symbols.ToCharArray();
                    foreach (char s in chars)
                    {
                        ToolStripButton Button = new ToolStripButton();
                        Button.Text = s.ToString();// ConvertHexToChar(s);
                        Button.Click += new EventHandler(Button_Click);
                        SymbolBar.Items.Add(Button);
                    }
                }
            }
            else
            {
                MessageBox.Show("未找到符号库文件 ‘" + SymbollibFile + "’。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        String ConvertHexToChar(String s)
        {
            if (s.Length == 1)
                return s;

            String Result = s;
            String Pattern = @"\\[xX][0-9a-fA-F]{2}";
            Regex r = new Regex(Pattern);
            MatchCollection ms = r.Matches(s);
            foreach (Match m in ms)
            {
                String v = m.Value.TrimStart('x','X','\\');
                String rChar = System.Convert.ToChar(Byte.Parse(v, System.Globalization.NumberStyles.HexNumber)).ToString();
                Result = Result.Replace(m.Value, rChar);
            }

            return Result;
        }

        void Button_Click(object sender, EventArgs e)
        {
            ToolStripButton Button = sender as ToolStripButton;
            if (FpSpread.EditingControl is GeneralEditor)
            {
                GeneralEditor generaleditor = FpSpread.EditingControl as GeneralEditor;
                generaleditor.SelectedText = generaleditor.SelectedText + Button.Text;
            }
            else if (FpSpread.EditingControl is FarPoint.Win.Spread.CellType.RichTextEditor)
            {
                FarPoint.Win.Spread.CellType.RichTextEditor richTextEditor = FpSpread.EditingControl as FarPoint.Win.Spread.CellType.RichTextEditor;
                richTextEditor.SelectedText = richTextEditor.SelectedText + Button.Text;
            }

            FpSpread.StartCellEditing(null, false);
        }

        /// <summary>
        /// 刷新符号工具栏
        /// </summary>
        public void RefreshSymbolBar(ToolStrip SymbolBar)
        {
            String SymbollibFile = Path.Combine(Application.StartupPath, "Symbollib.dll");
            String Symbols;
            if (File.Exists(SymbollibFile))
            {
                IniFile iniFile = new IniFile(SymbollibFile);
                Symbols = iniFile.IniReadValue("symbolbar", "Value");

                Char[] chars = Symbols.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    SymbolBar.Items[i].Text = chars[i].ToString();
                }
            }
            else
            {
                MessageBox.Show("未找到符号库文件 ‘" + SymbollibFile + "’。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
