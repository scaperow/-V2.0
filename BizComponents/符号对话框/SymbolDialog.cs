using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Common.Encoder;
using System.IO;
using Yqun.Bases;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;

namespace BizComponents
{
    public partial class SymbolDialog : Form
    {
        const int WindowHeight = 280;
        IniFile iniFile = null;
        int PrevRowIndex = 0;
        int PrevColumnIndex = 0;
        TabPage PrevTabPage = null;
        SymbolManager _SymbolManager;

        public SymbolDialog(SymbolManager SymbolManager)
        {
            InitializeComponent();

            _SymbolManager = SymbolManager;

            String SymbollibFile = Path.Combine(Application.StartupPath, "Symbollib.dll");
            if (File.Exists(SymbollibFile))
            {
                iniFile = new IniFile(SymbollibFile);
            }
            else
            {
                MessageBox.Show("未找到符号库文件 ‘" + SymbollibFile + "’。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            InitWindowSize();
            InitGridView();
            InitSymbolBar();
        }

        private void InitWindowSize()
        {
            this.Height = WindowHeight;
            this.Button_ShowSymbolBar.Text = "显示符号栏";
            this.Button_ResetSymbolBar.Enabled = false;
        }

        /// <summary>
        /// 初始化DataGridView
        /// </summary>
        private void InitGridView()
        {
            InitGridView(Grid_BiaoDian);
            InitGridView(Grid_TeShu);
            InitGridView(Grid_ShuXue);
            InitGridView(Grid_PinYin);
            InitGridView(Grid_DanWei);
            InitGridView(Grid_ShuZiXuHao);

            tabControl1.SelectedIndex = 0;
            PrevTabPage = tabControl1.SelectedTab;
        }

        /// <summary>
        /// 初始化DataGridView
        /// </summary>
        /// <param name="GridView"></param>
        private void InitGridView(DataGridView GridView)
        {
            GridView.Location = new Point(8, 7);
            GridView.Size = new Size(257, 185);

            GridView.AllowUserToAddRows = false;
            GridView.AllowUserToDeleteRows = false;
            GridView.AllowUserToOrderColumns = false;
            GridView.AllowUserToResizeColumns = false;
            GridView.AllowUserToResizeRows = false;
            GridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            GridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            GridView.BorderStyle = BorderStyle.Fixed3D;
            GridView.ColumnHeadersVisible = false;
            GridView.RowHeadersVisible = false;
            GridView.GridColor = Color.Black;
            GridView.ReadOnly = true;
            GridView.MultiSelect = false;
            GridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            GridView.BackgroundColor = Color.White;

            GridView.Columns.Clear();
            GridView.Columns.Add(new DataGridViewTextBoxColumn());
            GridView.Columns.Add(new DataGridViewTextBoxColumn());
            GridView.Columns.Add(new DataGridViewTextBoxColumn());
            GridView.Columns.Add(new DataGridViewTextBoxColumn());
            GridView.Columns.Add(new DataGridViewTextBoxColumn());
            GridView.Columns.Add(new DataGridViewTextBoxColumn());
            GridView.Columns.Add(new DataGridViewTextBoxColumn());
            GridView.Columns.Add(new DataGridViewTextBoxColumn());
            GridView.Columns.Add(new DataGridViewTextBoxColumn());
            GridView.Columns.Add(new DataGridViewTextBoxColumn());
            GridView.Columns.Add(new DataGridViewTextBoxColumn());
            GridView.Columns.Add(new DataGridViewTextBoxColumn());

            GridView.Rows.Clear();
            GridView.Rows.Add();
            GridView.Rows.Add();
            GridView.Rows.Add();
            GridView.Rows.Add();
            GridView.Rows.Add();
            GridView.Rows.Add();
            GridView.Rows.Add();
            GridView.Rows.Add();
            GridView.Rows.Add(); 
        }

        private void InitSymbolBar()
        {
            Char[] CharList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            String[] ValueList = null;
            if (iniFile != null)
            {
                String temp = iniFile.IniReadValue("symbolbar", "Value");
                ValueList = temp.Split(new string[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries);
            }

            Font font = new Font("新宋体", 9f);
            Point StartPos = new Point(1, 3);
            Size Size = new Size(24, 22);
            int HSpacing = 8;
            int VSpacing = 1;

            int RowCount = 3;
            int ColumnCount = 12;

            int Left = StartPos.X;
            int Top = StartPos.Y;

            BarPanel.Controls.Clear();
            for (int i = 0; i < RowCount; i++)
            {                
                for (int j = 0; j < ColumnCount; j++)
                {
                    RadioButtonEx rButton = new RadioButtonEx();
                    rButton.Name = (i * ColumnCount + j).ToString();
                    rButton.Left = Left + (Size.Width + HSpacing) * j;
                    rButton.Top = Top + (2 * Size.Height + VSpacing) * i;
                    rButton.Size = Size;
                    rButton.Font = font;
                    try
                    {
                        rButton.Text = ValueList[i * ColumnCount + j].ToString();
                    }
                    catch
                    {
                        rButton.Text = " ";
                    }
                    rButton.Click += new EventHandler(rButton_Click);
                    BarPanel.Controls.Add(rButton);
                    Label label = new Label();
                    label.Left = rButton.Left;
                    label.Top = rButton.Top + rButton.Height;
                    label.AutoSize = false;
                    label.Size = Size;
                    label.TextAlign = ContentAlignment.MiddleCenter;

                    label.Text = CharList[i * ColumnCount + j].ToString();
                    BarPanel.Controls.Add(label);
                }
            }
        }

        void rButton_Click(object sender, EventArgs e)
        {
            RadioButtonEx rButton = sender as RadioButtonEx;
            DataGridViewEx gridview = tabControl1.SelectedTab.Controls[0] as DataGridViewEx;
            rButton.Text = gridview.CurrentCell.Value.ToString();
        }

        private void Button_ShowSymbolBar_Click(object sender, EventArgs e)
        {
            this.Button_ShowSymbolBar.Text = (this.Height == WindowHeight ? "显示符号栏" : "隐藏符号栏");
            this.Button_ResetSymbolBar.Enabled = (this.Height == WindowHeight);
            this.Height = (this.Height == WindowHeight ? this.Height + BarPanel.Height : this.Height - BarPanel.Height);
        }

        private void SymbolDialog_Load(object sender, EventArgs e)
        {
            if (iniFile != null)
            {
                String temp = iniFile.IniReadValue("symbollib", "标点符号");
                String[] strings = temp.Split(new string[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries);
                Grid_BiaoDian.CharCount = strings.Length;
                for (int i = 0; i < strings.Length; i++)
                {
                    int row = i / Grid_BiaoDian.Columns.Count;
                    int col = i % Grid_BiaoDian.Columns.Count;
                    Grid_BiaoDian.Rows[row].Cells[col].Value = strings[i];
                }

                temp = iniFile.IniReadValue("symbollib", "单位符号");
                strings = temp.Split(new string[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries);
                Grid_DanWei.CharCount = strings.Length;
                for (int i = 0; i < strings.Length; i++)
                {
                    int row = i / Grid_DanWei.Columns.Count;
                    int col = i % Grid_DanWei.Columns.Count;
                    Grid_DanWei.Rows[row].Cells[col].Value = strings[i];
                }

                temp = iniFile.IniReadValue("symbollib", "拼音");
                strings = temp.Split(new string[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries);
                Grid_PinYin.CharCount = strings.Length;
                for (int i = 0; i < strings.Length; i++)
                {
                    int row = i / Grid_PinYin.Columns.Count;
                    int col = i % Grid_PinYin.Columns.Count;
                    Grid_PinYin.Rows[row].Cells[col].Value = strings[i];
                }

                temp = iniFile.IniReadValue("symbollib", "数学符号");
                strings = temp.Split(new string[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries);
                Grid_ShuXue.CharCount = strings.Length;
                for (int i = 0; i < strings.Length; i++)
                {
                    int row = i / Grid_ShuXue.Columns.Count;
                    int col = i % Grid_ShuXue.Columns.Count;
                    Grid_ShuXue.Rows[row].Cells[col].Value = strings[i];
                }

                temp = iniFile.IniReadValue("symbollib", "数字序号");
                strings = temp.Split(new string[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries);
                Grid_ShuZiXuHao.CharCount = strings.Length;
                for (int i = 0; i < strings.Length; i++)
                {
                    int row = i / Grid_ShuZiXuHao.Columns.Count;
                    int col = i % Grid_ShuZiXuHao.Columns.Count;
                    Grid_ShuZiXuHao.Rows[row].Cells[col].Value = strings[i];
                }

                temp = iniFile.IniReadValue("symbollib", "特殊符号");
                strings = temp.Split(new string[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries);
                Grid_TeShu.CharCount = strings.Length;
                for (int i = 0; i < strings.Length; i++)
                {
                    int row = i / Grid_TeShu.Columns.Count;
                    int col = i % Grid_TeShu.Columns.Count;
                    Grid_TeShu.Rows[row].Cells[col].Value = strings[i];
                }
            }
        }

        private void DataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridView gridview = sender as DataGridView;
                Char c = System.Convert.ToChar(gridview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                ImageChar.Image = GetImageChar(c);
            }
            catch
            {
                ImageChar.Image = null;
            }
        }

        private Image GetImageChar(Char c)
        {
            Bitmap bmp = new Bitmap(ImageChar.Width, ImageChar.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Font font = new Font("新宋体", 30f, FontStyle.Regular);
                SizeF sizeF = g.MeasureString(c.ToString(), font);
                float left = (ImageChar.Width-sizeF.Width)/2.0f;
                float top = (ImageChar.Height-sizeF.Height)/2.0f;
                RectangleF rectf = new RectangleF(left, top, sizeF.Width, sizeF.Height);
                g.DrawString(c.ToString(), font, Brushes.Blue, rectf);
            }

            return bmp;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            DataGridView gridView = PrevTabPage.Controls[0] as DataGridView;
            PrevRowIndex = gridView.CurrentCell.RowIndex;
            PrevColumnIndex = gridView.CurrentCell.ColumnIndex;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            DataGridViewEx gridView = e.TabPage.Controls[0] as DataGridViewEx;
            int Count = (PrevRowIndex + 1) * gridView.Columns.Count + PrevColumnIndex + 1;
            if (Count < gridView.CharCount)
            {
                gridView.CurrentCell = gridView.Rows[PrevRowIndex].Cells[PrevColumnIndex];
            }
            else
            {
                gridView.CurrentCell = gridView.Rows[0].Cells[0];
            }

            PrevTabPage = e.TabPage;
        }

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewEx gridview = sender as DataGridViewEx;
            if (_SymbolManager != null)
            {
                _SymbolManager.FpSpread.StartCellEditing(null, false);
                if (_SymbolManager.FpSpread.EditingControl is GeneralEditor)
                {
                    GeneralEditor generaleditor = _SymbolManager.FpSpread.EditingControl as GeneralEditor;
                    generaleditor.SelectedText = generaleditor.SelectedText + gridview.CurrentCell.Value.ToString();
                }
                else if (_SymbolManager.FpSpread.EditingControl is FarPoint.Win.Spread.CellType.RichTextEditor)
                {
                    FarPoint.Win.Spread.CellType.RichTextEditor richTextEditor = _SymbolManager.FpSpread.EditingControl as FarPoint.Win.Spread.CellType.RichTextEditor;
                    richTextEditor.SelectedText = richTextEditor.SelectedText + gridview.CurrentCell.Value.ToString();
                }
            }
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            List<string> CharList = new List<string>();
            for (int i = 0; i < 36; i++)
            {
                RadioButtonEx rButton = BarPanel.Controls.Find(i.ToString(), false)[0] as RadioButtonEx;
                CharList.Add(rButton.Text);
            }

            String Value = string.Join("", CharList.ToArray());
            iniFile.IniWriteValue("symbolbar", "Value", Value);

            _SymbolManager.RefreshSymbolBar(_SymbolManager.SymbolBar);

            Close();
        }

        private void Button_ResetSymbolBar_Click(object sender, EventArgs e)
        {
            if (iniFile != null)
            {
                Char[] ValueList = iniFile.IniReadValue("symbolbar", "Default").ToCharArray();
                for (int i = 0; i < 36; i++)
                {
                    RadioButtonEx rButton = BarPanel.Controls.Find(i.ToString(), false)[0] as RadioButtonEx;
                    rButton.Text = ValueList[i].ToString();
                }

                String symbols = new string(ValueList);
                iniFile.IniWriteValue("symbolbar", "Value", symbols);
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 处理键盘操作
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        const int WM_KEYDOWN = 0x100;
        const int WM_SYSKEYDOWN = 0x104;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == WM_KEYDOWN || msg.Msg == WM_SYSKEYDOWN)
            {
                if (keyData >= Keys.A && keyData <= Keys.Z ||
                    keyData >= Keys.D0 && keyData <= Keys.D9)
                {
                    Char[] CharList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
                    Char symbol = System.Convert.ToChar(keyData.ToString());
                    int Index = Array.IndexOf(CharList, symbol);
                    Control[] controls = BarPanel.Controls.Find(Index.ToString(), false);
                    if (controls != null && controls.Length >= 1)
                    {
                        RadioButtonEx rButton = controls[0] as RadioButtonEx;
                        DataGridViewEx gridview = tabControl1.SelectedTab.Controls[0] as DataGridViewEx;
                        rButton.Text = gridview.CurrentCell.Value.ToString();
                    }

                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
