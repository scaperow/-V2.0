using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BizCommon;
using BizComponents;

namespace BizComponents
{
    public partial class TemperatureEditor : Form
    {
        public Sys_Temperature Temperature;
        public string TestRoomCode;


        public TemperatureEditor(Sys_Temperature temperature)
        {
            InitializeComponent();
            Temperature = temperature;
        }

        public TemperatureEditor(string testRoomCode)
        {
            InitializeComponent();
            TestRoomCode = testRoomCode;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (Temperature == null)
            {
                New();
            }
            else
            {
                Modify();
            }
        }

        private void Modify()
        {
            if (string.IsNullOrEmpty(TextName.Text))
            {
                MessageBox.Show("请输入温度类型名称");
                TextName.Focus();
                return;
            }

            Temperature.Name = TextName.Text;
            var result = TemperatureHelperClient.RenameTemperature(Convert.ToString(Temperature.ID), Temperature.Name);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(result, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void New()
        {
            if (string.IsNullOrEmpty(TextName.Text))
            {
                MessageBox.Show("请输入温度类型名称");
                TextName.Focus();
                return;
            }

            Temperature = new Sys_Temperature()
                  {
                      IsSystem = (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator && CheckIsSystem.Checked) ? 1 : 0,
                      Name = TextName.Text,
                      CreateBy = Yqun.Common.ContextCache.ApplicationContext.Current.UserName,
                      CreateTime = DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"),
                  };

            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
            {
                if (string.IsNullOrEmpty(TestRoomCode))
                {
                    Temperature.TestRoomCode = "0000000000000000";
                }
                else
                {
                    Temperature.TestRoomCode = TestRoomCode;
                }

            }
            else
            {
                Temperature.TestRoomCode = Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code;
            }

            var result = TemperatureHelperClient.NewTemperature(Temperature);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(result, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void TemperatureEditor_Load(object sender, EventArgs e)
        {
            CheckIsSystem.Visible = Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator;
            if (Temperature != null)
            {
                TextName.Text = Temperature.Name;
                CheckIsSystem.Visible = false;
            }
        }
    }
}
