using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ShuXianCaiJiComponents;
using System.IO.Ports;
using System.Xml;
using ShuXianCaiJiModule;

namespace Kingrocket.CJ.Components
{
    /// <summary>
    /// 配置界面
    /// </summary>
    public partial class SystemSetupForm : Form
    {
        /// <summary>
        /// 操作配置文件
        /// </summary>
        ConfigOperation _ConfigOperation = null;

        /// <summary>
        /// 配置实例
        /// </summary>
        SXCJModule _SXCJModule = null;

        /// <summary>
        /// 日志
        /// </summary>
        private Logger log = null;

        CaijiCommHelper _CaijiCommHelper = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemSetupForm(Logger log, CaijiCommHelper TCaijiCommHelper)
        {
            InitializeComponent();
            this.log = log;
            _CaijiCommHelper = TCaijiCommHelper;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_CaijiCommHelper.GetClientConfigStatus(txtECode.Text.Trim()) == 0)
                {
                    MessageBox.Show("本地设备信息未在服务上注册，请查证！", "本地配置提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    return;
                }

                _SXCJModule.SpecialSetting.InvalidValueRangeList = new List<ValueRange>();
                for (int i = 0; i < ListBoxValidData.Items.Count; i++)
                {
                    ValueRange _ValueRange = new ValueRange();
                    _ValueRange.MinValue = Convert.ToDouble(ListBoxValidData.Items[i].ToString().Substring(0, ListBoxValidData.Items[i].ToString().IndexOf("----")));
                        
                    _ValueRange.MaxValue = Convert.ToDouble(ListBoxValidData.Items[i].ToString().Substring(ListBoxValidData.Items[i].ToString().IndexOf("----") + 4));
                    _SXCJModule.SpecialSetting.InvalidValueRangeList.Add(_ValueRange);
                }
                _SXCJModule.SpecialSetting.MachineCode = txtECode.Text;
                _SXCJModule.SpecialSetting.MachineCompany = cmbCJ.Text;
                _SXCJModule.SpecialSetting.MachineKeyCode = txtXLQM.Text;
                _SXCJModule.SpecialSetting.MachineType = rdbWNJ.Checked ? 2 : 1;
                _SXCJModule.SpecialSetting.MinFinishValue = Convert.ToDouble(txtFinishForce.Text ?? "0");
                _SXCJModule.SpecialSetting.MinValidValue = Convert.ToDouble(txtStartForce.Text ?? "0");
                if (rdbCOM.Checked)
                {
                    _SXCJModule.SpecialSetting.CommunicationType = 0;
                    _SXCJModule.SpecialSetting.PortBaud = Convert.ToInt32(cmbBaudRate.Text);
                    _SXCJModule.SpecialSetting.PortName = cmbSerialPort.Text;
                }
                else
                {
                    _SXCJModule.SpecialSetting.CommunicationType = 1;
                    _SXCJModule.SpecialSetting.LocalPort = ushort.Parse(txtMPort.Text);
                    _SXCJModule.SpecialSetting.LocalIP = txtMIP.Text;
                    _SXCJModule.SpecialSetting.RemotPort =ushort.Parse(txtDevicePort.Text);
                    _SXCJModule.SpecialSetting.RemotIP = txtDeviceIP.Text;

                }
                _SXCJModule.SpecialSetting.QFParameter = Convert.ToDouble(txtQFCondition.Text ?? "0");
                _SXCJModule.SpecialSetting.QFStartValueMPA = Convert.ToDouble(txtQFStart.Text ?? "0");
                _SXCJModule.SpecialSetting.QuFuType = short.Parse((rdbQFL.Checked ? 2 : 1).ToString());
                _SXCJModule.SpecialSetting.QFPoints = Convert.ToInt32(txtPoints.Text ?? "0");
                _SXCJModule.SpecialSetting.QFName = (cmbQFName.SelectedValue ?? "").ToString();
                _SXCJModule.SpecialSetting.TestRoomCode = txtTestCode.Text;
                _SXCJModule.SpecialSetting.XDefaultMaxValue = Convert.ToDouble(txtXDataMValue.Text ?? "0");

                _SXCJModule.SpecialSetting.YDefaultMaxValue = Convert.ToDouble(txtYDataMValue.Text ?? "0");
                _SXCJModule.SpecialSetting.DrawChartInterval = Convert.ToInt32(txtDrawChartInterval.Text ?? "0");
                _SXCJModule.SpecialSetting.ZeroParameters = float.Parse(txtZeroParameters.Text ?? "0");
                _SXCJModule.SpecialSetting.DPlace = float.Parse(tb_DPlace.Text ?? "0");
                _SXCJModule.SpecialSetting.SpecialD = float.Parse(tb_Special.Text ?? "0");
                _SXCJModule.SpecialSetting.PointNum = Convert.ToInt32(txtPointNum.Text ?? "0");

                _SXCJModule.SpecialSetting.IsDebug = cb_debug.Checked;
                _SXCJModule.SpecialSetting.ValidCount = Convert.ToInt32(tb_validCount.Text ?? "0");

                _SXCJModule.SpecialSetting.PrecisionGrade = int.Parse(txtPrecisionGrade.Text);
                _SXCJModule.SpecialSetting.BDValue = float.Parse(txtKentADBDValue.Text);
                _ConfigOperation.SaveModul(_SXCJModule);
                MessageBox.Show("保存成功");
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString(), true, true);
                MessageBox.Show("保存失败");
            }
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemSetupForm_Load(object sender, EventArgs e)
        {
            try
            {
                

                #region 初始化配置实例对象

                _ConfigOperation = new ConfigOperation();
                _SXCJModule = new SXCJModule();
                _ConfigOperation.GetSXCJModule(out _SXCJModule);

                #endregion


                #region 初始化通讯类型


                rdbInternet_CheckedChanged(sender, e);

                #endregion

                #region 绑定厂家信息

                cmbCJ.DataSource = _SXCJModule.CommonSetting.CJNameList;
                this.cmbCJ.DisplayMember = "Value";
                this.cmbCJ.ValueMember = "Key";
                this.cmbCJ.SelectedIndexChanged += new System.EventHandler(this.cmbCJ_SelectedIndexChanged);

                #endregion

                #region 绑定仪表配置

                for (int i = 0; i < cmbCJ.Items.Count; i++)
                {
                    if (cmbCJ.Items[i].ToString() == _SXCJModule.SpecialSetting.MachineCompany)
                    {
                        cmbCJ.SelectedIndex = i;
                        break;
                    }
                }

                if (_SXCJModule.SpecialSetting.MachineType == 1)
                {
                    rdbYLJ.Checked = true;
                    rdbWNJ.Checked = false;
                }
                else
                {
                    rdbYLJ.Checked = false;
                    rdbWNJ.Checked = true;
                }

                txtXLQM.Text = _SXCJModule.SpecialSetting.MachineKeyCode;
                txtTestCode.Text = _SXCJModule.SpecialSetting.TestRoomCode;
                txtECode.Text = _SXCJModule.SpecialSetting.MachineCode;


                #endregion

                #region 绑定采集设置1

                txtFinishForce.Text = _SXCJModule.SpecialSetting.MinFinishValue.ToString("00.00");
                txtStartForce.Text = _SXCJModule.SpecialSetting.MinValidValue.ToString("00.00");

                txtQFStart.Text = _SXCJModule.SpecialSetting.QFStartValueMPA.ToString("00.00");
                txtQFCondition.Text = _SXCJModule.SpecialSetting.QFParameter.ToString("00.00");

                txtXDataMValue.Text = _SXCJModule.SpecialSetting.XDefaultMaxValue.ToString("00.00");
                txtYDataMValue.Text = _SXCJModule.SpecialSetting.YDefaultMaxValue.ToString("00.00");

                txtZeroParameters.Text = _SXCJModule.SpecialSetting.ZeroParameters.ToString("00.00");

                txtPointNum.Text = _SXCJModule.SpecialSetting.PointNum.ToString();
                if (_SXCJModule.SpecialSetting.QuFuType == 1)
                {
                    rdbQFH.Checked = true;
                    rdbQFL.Checked = false;
                }
                else
                {
                    rdbQFH.Checked = false;
                    rdbQFL.Checked = true;
                }
                this.cmbQFName.DataSource = _SXCJModule.CommonSetting.QFNameList;
                this.cmbQFName.DisplayMember = "Value";
                this.cmbQFName.ValueMember = "Key";
                this.cmbQFName.SelectedValue = _SXCJModule.SpecialSetting.QFName;

                txtPoints.Text = _SXCJModule.SpecialSetting.QFPoints.ToString();
                
                #endregion


                #region 绑定采集设置2


                for (int i = 0; i < _SXCJModule.SpecialSetting.InvalidValueRangeList.Count; i++)
                {
                    ListBoxValidData.Items.Add(_SXCJModule.SpecialSetting.InvalidValueRangeList[i].MinValue.ToString() + "----" + _SXCJModule.SpecialSetting.InvalidValueRangeList[i].MaxValue.ToString());
                }

                txtDrawChartInterval.Text = (_SXCJModule.SpecialSetting.DrawChartInterval).ToString();

                cb_debug.Checked = _SXCJModule.SpecialSetting.IsDebug;
                tb_validCount.Text = _SXCJModule.SpecialSetting.ValidCount.ToString();
                txtPrecisionGrade.Text = _SXCJModule.SpecialSetting.PrecisionGrade.ToString();
                #endregion

                #region 绑定特殊设置专用
                tb_DPlace.Text = _SXCJModule.SpecialSetting.DPlace.ToString();
                tb_Special.Text = _SXCJModule.SpecialSetting.SpecialD.ToString();
                txtKentADBDValue.Text = _SXCJModule.SpecialSetting.BDValue.ToString();
                #endregion
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }


        #region 采集条件配置
        
        private void InputNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8 && e.KeyChar != (char)46 && e.KeyChar != (char)45)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        /// <summary>
        /// 添加验证数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtValidStart.Text != "" && txtValidFinish.Text != ""  && float.Parse(txtValidStart.Text) < float.Parse(txtValidFinish.Text))
                {
                    if (ListBoxValidData.Items.Contains(txtValidStart.Text + "----" + txtValidFinish.Text))
                    {
                        MessageBox.Show("要添加的值已存在集合中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        ListBoxValidData.Items.Add(txtValidStart.Text + "----" + txtValidFinish.Text);
                    }
                    txtValidStart.Text = string.Empty;
                    txtValidFinish.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("请输入正确的值", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        /// <summary>
        /// 删除验证数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < ListBoxValidData.SelectedItems.Count; i++)
                {
                    for (int j = 0; j < ListBoxValidData.Items.Count; j++)
                    {
                        if (ListBoxValidData.Items[j] == ListBoxValidData.SelectedItems[i])
                        {
                            ListBoxValidData.Items.RemoveAt(j);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        #endregion

        private void btnRecoderKey_Click(object sender, EventArgs e)
        {
            try
            {
                BioKeyRegisterForm _BioKeyRegisterForm = new BioKeyRegisterForm();
                _BioKeyRegisterForm.ShowDialog();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        private void cmbCJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCJ.SelectedValue.ToString() == "丰仪万能机")
            {
                rdbYLJ.Enabled = false;
                rdbWNJ.Checked = true;
                rdbYLJ.Enabled = false;
                cmbQFName.SelectedValue = "FYQF";
                cmbQFName.Enabled = false;
            }
            else
            {
                rdbYLJ.Enabled = true;
                cmbQFName.Enabled = true;
            }
        }

        private void rdbInternet_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCOM.Checked)
            {
                panelCOM.Show();
                panelCOM.Dock = DockStyle.Fill;
                panelInternet.Hide();
                rdbCOM.Checked = true;

                #region 初始化串口配置

                string[] portlist = SerialPort.GetPortNames();
                for (int i = 0; i < portlist.Length; i++)
                {
                    this.cmbSerialPort.Items.Add(portlist[i]);
                }
                if (portlist.Length > 0)
                {
                    cmbSerialPort.SelectedIndex = 0;
                }
                cmbBaudRate.SelectedIndex = 5;
                cmbDataBit.SelectedIndex = 0;
                cmbParity.SelectedIndex = 0;
                cmbStopBit.SelectedIndex = 0;

                #endregion

                #region 绑定串口配置数据

                cmbSerialPort.Text = _SXCJModule.SpecialSetting.PortName;
                for (int i = 0; i < cmbSerialPort.Items.Count; i++)
                {
                    if (cmbSerialPort.Items[i].ToString() == _SXCJModule.SpecialSetting.PortName)
                    {
                        cmbSerialPort.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < cmbBaudRate.Items.Count; i++)
                {
                    if (cmbBaudRate.Items[i].ToString() == _SXCJModule.SpecialSetting.PortBaud.ToString())
                    {
                        cmbBaudRate.SelectedIndex = i;
                        break;
                    }
                }
                cmbDataBit.SelectedIndex = 0;
                cmbParity.SelectedIndex = 0;
                cmbStopBit.SelectedIndex = 0;

                #endregion
            }
            else
            {
                panelInternet.Show();
                panelInternet.Dock = DockStyle.Fill;
                panelCOM.Hide();
                txtDeviceIP.Text = _SXCJModule.SpecialSetting.RemotIP;
                txtDevicePort.Text = _SXCJModule.SpecialSetting.RemotPort.ToString();
                txtMIP.Text = _SXCJModule.SpecialSetting.LocalIP;
                txtMPort.Text = _SXCJModule.SpecialSetting.LocalPort.ToString();
            }
        }
    }
}
