using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yqun.Bases;
using System.IO;
using Yqun.Common.Encoder;
using System.Xml;
using Yqun.Services;

namespace Yqun.MainUI
{
    public partial class DataSourceDialog : Form
    {
        string FileName = string.Empty;
        DataSet infoSet = new DataSet("DataSourceSet");

        public DataSourceDialog()
        {
            InitializeComponent();
        }

        private void DataSourceDialog_Load(object sender, EventArgs e)
        {
            #region 数据库初始化
            FileName = Path.Combine(SystemFolder.DataSourceFolder, "DataSource.xml");
            if (File.Exists(FileName))
            {
                infoSet.ReadXml(FileName);

                string text1 = DataSetCoder.GetProperty(infoSet, 0, "name", "DataSource", "value").ToString();
                string text2 = DataSetCoder.GetProperty(infoSet, 0, "name", "DataInstance", "value").ToString();
                string text3 = DataSetCoder.GetProperty(infoSet, 0, "name", "DataUserName", "value").ToString();
                string text4 = DataSetCoder.GetProperty(infoSet, 0, "name", "DataPassword", "value").ToString();
                string text5 = DataSetCoder.GetProperty(infoSet, 0, "name", "DataIntegratedLogin", "value").ToString();
                string text6 = DataSetCoder.GetProperty(infoSet, 0, "name", "DataISAttach", "value").ToString();

                DataSourceTxt.Text = Yqun.Common.Encoder.EncryptSerivce.Dencrypt(text1);
                DataBaseTxt.Text = Yqun.Common.Encoder.EncryptSerivce.Dencrypt(text2);
                UserTxt.Text = Yqun.Common.Encoder.EncryptSerivce.Dencrypt(text3);
                PasswordTxt.Text = Yqun.Common.Encoder.EncryptSerivce.Dencrypt(text4);

                bool _DataISAttach = false;
                bool.TryParse(text6, out _DataISAttach);
                bool _DataIntegratedLogin = false;
                bool.TryParse(text5, out _DataIntegratedLogin);
                DataISAttach.Checked = _DataISAttach;
                DataIntegratedLogin.Checked = _DataIntegratedLogin;

                UserTxt.Enabled = !DataIntegratedLogin.Checked;
                PasswordTxt.Enabled = !DataIntegratedLogin.Checked;
            }
            else
            {
                DataTable TableSet = new DataTable("DataSource");
                TableSet.Columns.Add("name", typeof(string));
                TableSet.Columns.Add("value", typeof(string));
                infoSet.Tables.Add(TableSet);

                FileStream fs = new FileStream(FileName, FileMode.Create);
                XmlTextWriter xtw = new XmlTextWriter(fs, Encoding.UTF8);
                xtw.Formatting = Formatting.Indented;
                xtw.WriteStartDocument();
                infoSet.WriteXml(xtw);
                xtw.WriteEndDocument();
                xtw.Close();
            }
            #endregion

            #region 服务端地址初始化
            string EndPointAddress = BizCommon.ConfigHelper.GetEndpointAddress("TransferServiceEndPoint").ToString();
            string strAddressM = EndPointAddress.Replace("net.tcp://", "");
            strAddressM = strAddressM.Substring(0, strAddressM.IndexOf('/'));
            string strAddressE = EndPointAddress.Replace("net.tcp://", "").Replace(strAddressM, "");
            txtServerAddress.Text = strAddressM;
            #endregion
        }

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            Boolean r = SaveConnectionInfo();
            Boolean r1 = SaveNetworkConfig();
            String Message = string.Empty;
            if (r && r1)
            {
                Message = "保存配置信息成功。";
            }
            else if (r)
            {
                Message = "保存数据源配置信息成功,保存网络配置失败。" ;
            }
            else if (r1)
            {
                Message = "保存数据源配置信息失败,保存网络配置成功。";
            }
            else
            {
                Message = "保存配置信息失败。";
            }
            //Message = (r ? "保存数据源配置信息成功。" : "保存数据源配置信息失败！");
            MessageBox.Show(Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void DataIntegratedLogin_Click(object sender, EventArgs e)
        {
            UserTxt.Enabled = !DataIntegratedLogin.Checked;
            PasswordTxt.Enabled = !DataIntegratedLogin.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tControlConfigInfo.SelectedIndex == 0)
            {
                Boolean r = Agent.TestDbConnection(false, new object[]{ "SQLClient",
                                                                    "MSSQLServer2k5",
                                                                    DataIntegratedLogin.Checked,
                                                                    DataSourceTxt.Text,
                                                                    DataBaseTxt.Text,
                                                                    UserTxt.Text,
                                                                    PasswordTxt.Text,
                                                                    DataISAttach.Checked,
                                                                    Application.StartupPath
                                                                   });

                String Message = (r ? "本地数据库连接成功！" : "本地数据库连接失败！");
                MessageBoxIcon icon = (r ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                MessageBox.Show(Message, "提示", MessageBoxButtons.OK, icon);
            }
            else if(tControlConfigInfo.SelectedIndex==1)
            {
                string EndPointAddress = BizCommon.ConfigHelper.GetEndpointAddress("TransferServiceEndPoint").ToString();
                string strAddressM = EndPointAddress.Replace("net.tcp://", "");
                strAddressM = strAddressM.Substring(0, strAddressM.IndexOf('/'));
                string strAddressE = EndPointAddress.Replace("net.tcp://", "").Replace(strAddressM, "");
                //txtServerAddress.Text = strAddressM;
                EndPointAddress = "net.tcp://" + txtServerAddress.Text.Trim() + strAddressE;
                BizCommon.ConfigHelper.SetEndpointAddress("TransferServiceEndPoint", EndPointAddress);
                Boolean r = Yqun.Services.Agent.TestNetwork();

                String Message = (r ? "网络连接测试成功！" : "网络连接测试失败！");
                EndPointAddress = "net.tcp://" + strAddressM + strAddressE;
                BizCommon.ConfigHelper.SetEndpointAddress("TransferServiceEndPoint", EndPointAddress);

                MessageBoxIcon icon = (r ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                MessageBox.Show(Message, "提示", MessageBoxButtons.OK, icon);
            }
        }
        private bool SaveNetworkConfig()
        {
            try
            {
                string EndPointAddress = BizCommon.ConfigHelper.GetEndpointAddress("TransferServiceEndPoint").ToString();
                string strAddressM = EndPointAddress.Replace("net.tcp://", "");
                strAddressM = strAddressM.Substring(0, strAddressM.IndexOf('/'));
                string strAddressE = EndPointAddress.Replace("net.tcp://", "").Replace(strAddressM, "");
                strAddressM = txtServerAddress.Text.Trim();

                EndPointAddress = "net.tcp://" + strAddressM + strAddressE;
                BizCommon.ConfigHelper.SetEndpointAddress("TransferServiceEndPoint", EndPointAddress);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private Boolean SaveConnectionInfo()
        {
            //保存本地数据源配置信息
            string text1 = Yqun.Common.Encoder.EncryptSerivce.Encrypt(DataSourceTxt.Text);
            string text2 = Yqun.Common.Encoder.EncryptSerivce.Encrypt(DataBaseTxt.Text);
            string text3 = Yqun.Common.Encoder.EncryptSerivce.Encrypt(UserTxt.Text);
            string text4 = Yqun.Common.Encoder.EncryptSerivce.Encrypt(PasswordTxt.Text);
            string text5 = DataISAttach.Checked.ToString();
            string text6 = DataIntegratedLogin.Checked.ToString();

            int num1 = DataSetCoder.SetProperty(ref infoSet, 0, "name", "DataSource", "value", text1);
            int num2 = DataSetCoder.SetProperty(ref infoSet, 0, "name", "DataInstance", "value", text2);
            int num3 = DataSetCoder.SetProperty(ref infoSet, 0, "name", "DataUserName", "value", text3);
            int num4 = DataSetCoder.SetProperty(ref infoSet, 0, "name", "DataPassword", "value", text4);
            int num5 = DataSetCoder.SetProperty(ref infoSet, 0, "name", "DataAdapterType", "value", "SQLClient");
            int num6 = DataSetCoder.SetProperty(ref infoSet, 0, "name", "DataBaseType", "value", "MSSQLServer2k5");
            int num8 = DataSetCoder.SetProperty(ref infoSet, 0, "name", "DataIntegratedLogin", "value", text6);
            int num9 = DataSetCoder.SetProperty(ref infoSet, 0, "name", "DataISAttach", "value", text5);

            Boolean r = (num1 == 1 && num2 == 1 && num3 == 1 && num4 == 1 && num5 == 1 && num6 == 1 && num8 == 1 && num9 == 1);
            if (r)
            {
                infoSet.WriteXml(FileName, XmlWriteMode.IgnoreSchema);
            }

            return r;
        }
    }
}
