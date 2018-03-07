using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Services;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using System.Runtime.Serialization;

namespace BizComponents
{
    public partial class FormatStyleForm : Form
    {
        Dictionary<FormatStyle, FormatStringGroup> Groups = null;
        public FormatStyleForm()
        {
            InitializeComponent();  

            lBox_FormatString.SelectedIndexChanged += new EventHandler(lBox_FormatString_SelectedIndexChanged);
        }

        FormatStyle _FormatStyle = FormatStyle.General;
        public FormatStyle FormatStyle
        {
            get
            {
                return _FormatStyle;
            }
            set
            {
                _FormatStyle = value;
            }
        }

        String _FormatString = "";
        public String FormatString
        {
            get
            {
                return _FormatString;
            }
            set
            {
                _FormatString = value;
            }
        }

        private void FormatStyleForm_Load(object sender, EventArgs e)
        {
            Groups = DepositoryReportFormat.InitReportFormats();
            
            //给每一类赋一个示例
            Groups[FormatStyle.General].Example = "";
            Groups[FormatStyle.Number].Example = "1234.56789";
            Groups[FormatStyle.Currency].Example = "1234.56789";
            Groups[FormatStyle.Date].Example = DateTime.Now.ToString();
            Groups[FormatStyle.Time].Example = DateTime.Now.ToString();
            Groups[FormatStyle.Percent].Example = "1234.56789";
            Groups[FormatStyle.ScientificCount].Example = "1234.56789";
            Groups[FormatStyle.Text].Example = "";


            if (_FormatStyle == FormatStyle.General)
            {
                rButton_General.Checked = true;
            }
            else if (_FormatStyle == FormatStyle.Number)
            {
                rButton_Number.Checked = true;
            }
            else if (_FormatStyle == FormatStyle.Currency)
            {
                rButton_Currency.Checked = true;
            }
            else if (_FormatStyle == FormatStyle.Percent)
            {
                rButton_Percent.Checked = true;
            }
            else if (_FormatStyle == FormatStyle.Date)
            {
                rButton_Date.Checked = true;
            }
            else if (_FormatStyle == FormatStyle.Time)
            {
                rButton_Time.Checked = true;
            }
            else if (_FormatStyle == FormatStyle.ScientificCount)
            {
                rButton_ScientificCount.Checked = true;
            }
            else if (_FormatStyle == FormatStyle.Text)
            {
                rButton_Text.Checked = true;
            }

            ShowFormatString(_FormatStyle);

            foreach (FormatString String in lBox_FormatString.Items)
            {
                if (String.FormatValue.ToLower() == FormatString.ToLower())
                {
                    lBox_FormatString.SelectedIndex = lBox_FormatString.Items.IndexOf(String);
                    break;
                }
            }
        }

        private void radioButton_Click(object sender, EventArgs e)
        {
            if (sender == rButton_General)
            {
                ShowFormatString(FormatStyle.General);                
            }
            else if (sender == rButton_Number)
            {
                ShowFormatString(FormatStyle.Number);                
            }
            else if (sender == rButton_Currency)
            {
                ShowFormatString(FormatStyle.Currency);                
            }
            else if (sender == rButton_Percent)
            {
                ShowFormatString(FormatStyle.Percent);                
            }
            else if (sender == rButton_Date)
            {
                ShowFormatString(FormatStyle.Date);                
            }
            else if (sender == rButton_Time)
            {
                ShowFormatString(FormatStyle.Time);                
            }
            else if (sender == rButton_ScientificCount)
            {
                ShowFormatString(FormatStyle.ScientificCount);               
            }
            else if (sender == rButton_Text)
            {
                ShowFormatString(FormatStyle.Text);               
            }
        }

        /// <summary>
        /// 加载指定类型的格式字符串到ListBox中
        /// </summary>
        /// <param name="formatStyle"></param>
        FormatStyle rButton_Style;
        private void ShowFormatString(FormatStyle formatStyle)
        {
            rButton_Style = formatStyle;

            #region 字典的实现

            lBox_FormatString.Items.Clear();
            tBox_FormatString.Text = "";
            label_Example.Text = "";

            FormatStringGroup NowformatStringGroup = Groups[formatStyle];
            if (NowformatStringGroup != null)
            {
                for (int i = 0; i < NowformatStringGroup.FormatStrings.Count; i++)
                {
                    lBox_FormatString.Items.Add(NowformatStringGroup.FormatStrings[i]);
                }

                if (lBox_FormatString.Items.Count > 0)
                {
                    lBox_FormatString.SelectedIndex = 0;
                }
            }

            #endregion 
        }

        void lBox_FormatString_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lBox_FormatString.SelectedIndex != -1)
            {
                tBox_FormatString.Text = lBox_FormatString.SelectedItem.ToString();
                if (rButton_Style != FormatStyle.Date && rButton_Style != FormatStyle.Time)
                {
                    label_Example.Text = Demonstration.Format(Groups[rButton_Style].Example, lBox_FormatString.SelectedItem.ToString());
                }
                else
                {
                    label_Example.Text = DateTimeDemonstration.Format(Groups[rButton_Style].Example, lBox_FormatString.SelectedItem.ToString()); 
                }
            }
        }

        private void Button_Append_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                return;
            }

            Boolean Found = false;
            FormatStringGroup NowformatStringGroup = Groups[rButton_Style];
            for (int i = 0; i < NowformatStringGroup.FormatStrings.Count; i++)
            {
                if (NowformatStringGroup.FormatStrings[i].ToString() == textBox1.Text)
                {
                    Found = true;
                    break;
                }               
            }

            if (!Found)
            {
                FormatString fs = new FormatString();
                fs.FormatValue = textBox1.Text;
                fs.FormatStyle = rButton_Style;

                DepositoryReportFormat.UpdateReportFormats(fs);
                NowformatStringGroup.FormatStrings.Add(fs);
                lBox_FormatString.Items.Add(fs);
                tBox_FormatString.Text = lBox_FormatString.Items[0].ToString();
                textBox1.Text = "";
            }
        }

        private void Button_Delete_Click(object sender, EventArgs e)
        {
            FormatString fs = (FormatString)lBox_FormatString.SelectedItem;
            FormatStringGroup Fsg = Groups[rButton_Style];
            if (fs != null)
            {
                if (DialogResult.Yes == MessageBox.Show("确定删除格式串 ‘" + fs.FormatValue + "’ 吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    DepositoryReportFormat.DeleteReportFormats(fs);
                    lBox_FormatString.Items.Remove(lBox_FormatString.SelectedItem);
                    Fsg.FormatStrings.Remove(fs);
                    if (lBox_FormatString.Items.Count > 0)
                    {
                        lBox_FormatString.SelectedIndex = 0;
                    }
                    else
                    {
                        tBox_FormatString.Text = ""; 
                    }
                }
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            _FormatStyle = rButton_Style;
            _FormatString = (lBox_FormatString.SelectedItem != null ? lBox_FormatString.SelectedItem.ToString() : "");
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }

    public class Demonstration
    {
        static FpSpread fpSpread;
        static SheetView sheetView;
        static GeneralCellType cellType;
        static Demonstration()
        {
            fpSpread = new FpSpread();
            sheetView = new SheetView();
            fpSpread.Sheets.Add(sheetView);
            cellType = new GeneralCellType();
            sheetView.Cells[0, 0].CellType = cellType;            
        }

        public static String Format(String Value, String FormatString)
        {
            cellType.FormatString = FormatString;
            sheetView.Cells[0, 0].Text = Value; 
            return sheetView.Cells[0, 0].Text;
        }
    }

    public class DateTimeDemonstration
    {
        static FpSpread fpSpread;
        static SheetView sheetView;
        static FarPoint.Win.Spread.CellType.DateTimeCellType cellType;
        static DateTimeDemonstration()
        {
            fpSpread = new FpSpread();
            sheetView = new SheetView();
            fpSpread.Sheets.Add(sheetView);
            cellType = new DateTimeCellType();
            sheetView.Cells[0, 0].CellType = cellType;            
        }

        public static String Format(String Value, String FormatString)
        {
            cellType.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined; 
            cellType.UserDefinedFormat = FormatString;
            sheetView.Cells[0, 0].Value = Value; 
            return sheetView.Cells[0, 0].Text;
        }
 
    }

    /// <summary>
    /// 格式类型
    /// </summary>
    public enum FormatStyle : byte
    {
        /// <summary>
        /// 常规
        /// </summary>
        General,

        /// <summary>
        /// 数字
        /// </summary>
        Number,

        /// <summary>
        /// 货币
        /// </summary>
        Currency,

        /// <summary>
        /// 百分比
        /// </summary>
        Percent,

        /// <summary>
        /// 科学计数法
        /// </summary>
        ScientificCount,

        /// <summary>
        /// 日期型
        /// </summary>
        Date,

        /// <summary>
        /// 时间型
        /// </summary>
        Time,

        /// <summary>
        /// 文本型
        /// </summary>
        Text
    }

    /// <summary>
    /// 报表元素格式接口
    /// </summary>
    public interface IFormat
    {
        /// <summary>
        /// 元素显示样式
        /// </summary>
        FormatStyle FormatStyle
        {
            get;
            set;
        }

        /// <summary>
        /// 格式字符串
        /// </summary>
        String FormatValue
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 报表元素样式对象
    /// </summary>
    [Serializable]
    public class FormatString : IFormat, ISerializable
    {
        public FormatString()
        {
        }

        protected FormatString(SerializationInfo info, StreamingContext context)
        {
            _FormatStyle = (FormatStyle)info.GetValue("FormatStyle", typeof(FormatStyle));
            _FormatValue = info.GetString("FormatValue");
        }

        String _Index = Guid.NewGuid().ToString();
        public String Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
        }

        FormatStyle _FormatStyle = FormatStyle.General;
        public FormatStyle FormatStyle
        {
            get
            {
                return _FormatStyle;
            }
            set
            {
                _FormatStyle = value;
            }
        }

        String _FormatValue = "";
        public string FormatValue
        {
            get
            {
                return _FormatValue;
            }
            set
            {
                _FormatValue = value;
            }
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(IFormat))
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        #region ISerializable 成员

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FormatStyle", FormatStyle);
            info.AddValue("FormatValue", FormatValue);
        }

        #endregion

        public override string ToString()
        {
            return FormatValue;
        }
    }

    /// <summary>
    /// 一类格式字符串,例如数字类，货币类等等
    /// </summary>
    public class FormatStringGroup
    {
        String _Example = "";
        public String Example
        {
            get
            {
                return _Example;
            }
            set
            {
                _Example = value;
            }
        }

        List<FormatString> _FormatStrings = new List<FormatString>();
        public List<FormatString> FormatStrings
        {
            get
            {
                return _FormatStrings;
            }
        }
    }

    public class DepositoryReportFormat
    {
        public static Dictionary<FormatStyle, FormatStringGroup> InitReportFormats()
        {
            Dictionary<FormatStyle, FormatStringGroup> Groups = new Dictionary<FormatStyle, FormatStringGroup>();
            foreach (FormatStyle Format in Enum.GetValues(typeof(FormatStyle)))
            {
                FormatStringGroup group = InitReportFormats(Format);
                Groups.Add(Format, group);
            }

            return Groups;
        }

        public static FormatStringGroup InitReportFormats(FormatStyle FormatStyle)
        {

            String formatStyle = FormatStyle.ToString();
            FormatStringGroup Fsg = new FormatStringGroup();
            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("Select ID,FormatStyle,FormatString From sys_biz_ModuleFormatStrings Where FormatStyle='");
            Sql_Select.Append(formatStyle.ToString());
            Sql_Select.Append("'");
            Sql_Select.Append(" ORDER BY FormatString ASC ");
            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                for (int i = 0; i < Data.Rows.Count; i++)
                {
                    FormatString Fs = new FormatString();
                    DataRow Row = Data.Rows[i];
                    Fs.Index = Row["ID"].ToString();
                    String FormatS = Row["FormatStyle"].ToString();
                    Fs.FormatStyle = (FormatStyle)Enum.Parse(typeof(FormatStyle), FormatS);
                    Fs.FormatValue = Row["FormatString"].ToString();
                    Fsg.FormatStrings.Add(Fs);
                }
            }

            return Fsg;
        }

        public static Boolean UpdateReportFormats(FormatString String)
        {
            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("Select ID,FormatStyle,FormatString From sys_biz_ModuleFormatStrings Where ID='");
            Sql_Select.Append(String.Index);
            Sql_Select.Append("'");

            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                DataRow Row = Data.Rows[0];
                Row["ID"] = String.Index;
                Row["FormatStyle"] = String.FormatStyle;
                Row["FormatString"] = String.FormatValue;
            }
            else
            {
                DataRow Row = Data.NewRow();
                Row["ID"] = String.Index;
                Row["FormatStyle"] = String.FormatStyle;
                Row["FormatString"] = String.FormatValue;
                Data.Rows.Add(Row);
            }

            Boolean Result = false;

            try
            {
                object r = Agent.CallService("Yqun.BO.LoginBO.dll", "Update", new object[] { Data });
                Result = (Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }

        public static Boolean DeleteReportFormats(FormatString String)
        {
            StringBuilder Sql_Delete = new StringBuilder();
            Sql_Delete.Append("Delete From sys_biz_ModuleFormatStrings Where FormatString ='");
            Sql_Delete.Append(String.FormatValue);
            Sql_Delete.Append("'");

            Boolean Result = false;

            try
            {
                object r = Agent.CallService("Yqun.BO.LoginBO.dll", "ExcuteCommand", new object[] { Sql_Delete.ToString() });
                Result = (Convert.ToInt32(r) == 1);
            }
            catch
            { }

            return Result;
        }
    }
}
