using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BizCommon;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using ShuXianCaiJiComponents;
using ShuXianCaiJiModule;
using System.IO;
using FarPoint.Win.Spread;
using System.Collections;

namespace Kingrocket.CJ.Components
{
    public partial class MainForm : Form
    {
       
        #region 全局变量
        /// <summary>
        /// 试件面积
        /// </summary>
        Double Area = 1;
        /// <summary>
        /// 龄期
        /// </summary>
        String LQ = "";
        /// <summary>
        /// 断后标距
        /// </summary>
        Object dhbj = null;

        /// <summary>
        /// 用户待做的试验序号集合
        /// </summary>
        List<Int32> indexList = new List<int>();

        /// <summary>
        /// 是否加载总块数索引改变事件
        /// </summary>
        Boolean isLoad = false;

        /// <summary>
        /// 试验初始化配置信息
        /// </summary>
        private SXCJModule module = null;

        /// <summary>
        /// 日志对象，从Program传递过来
        /// </summary>
        private Logger log = null;

        /// <summary>
        /// 机器设备对象
        /// </summary>
        private MachineBase machine = null;

        /// <summary>
        /// 是否单元测试
        /// </summary>
        private Boolean IsUnitTest = false;

        /// <summary>
        /// 最大力值
        /// </summary>
        float MaxForce;

        /// <summary>
        /// 威海屏显记录上次力值
        /// </summary>
        double OldMaxForce;

        /// <summary>
        /// 记录标准屈服力值
        /// </summary>
        double QFLimit;

        /// <summary>
        /// 实验数据对象
        /// </summary>
        JZTestData _JZTestData;

        /// <summary>
        /// 模板配置信息
        /// </summary>
        List<JZTestConfig> configList = null;

        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime _StartTime;

        /// <summary>
        /// 试件个数
        /// </summary>
        int _TotalNumber = 0;

        /// <summary>
        /// CaijiCommHelper对象
        /// </summary>
        CaijiCommHelper _CaijiCommHelper;

        /// <summary>
        /// 结果显示列表
        /// </summary>
        DataTable resultDataSource = new DataTable();

        /// <summary>
        /// 设置GridView和弹出断后标距委托
        /// </summary>
        private delegate void SetGridViewValue();

        private delegate void SetDHBJDel(float d, int c);

        private bool _IsUploadFinish = true;

        private double _TempMinValidValue = 0.00;

        DateTime _ServerDateTime;
        DateTime _CrrTime = DateTime.MinValue;
        bool _TimerIsFinish = true;

        

        /// <summary>
        /// 画图委托
        /// </summary>
        /// <param name="XValue"></param>
        /// <param name="YValue"></param>
        private delegate void SetChartLine(double XValue,double YValue);

        private delegate void InterfaceChangedHandle(Boolean flag);

        /// <summary>
        /// 获取控件的Text值
        /// </summary>
        /// <param name="_Control"></param>
        /// <returns></returns>
        private delegate string GetControlText(Control _Control,bool _IsValue);

        private delegate void SetControlText(Control _Control, string text);


        private delegate void SetLineColor(int IsSucceed,int LineNum);

        /// <summary>
        /// 上传铁数据结构
        /// </summary>
        private UploadWNJInfo _UploadWNJInfo = null;
        private UploadYLJInfo _UploadYLJInfo = null;


        /// <summary>
        ///  上传铁道部Json
        /// </summary>
        string _UploadJson = string.Empty;

        /// <summary>
        ///  返回签名Code
        /// </summary>
        int _UploadCode;

        //[DllImport("DESDll.dll")]
        //private static extern string Encrypt(string Text, string key, string iv);

        #endregion

        public MainForm(Logger log, SXCJModule _Module, CaijiCommHelper _CaijiCommHelper)
        {
            try
            {
                InitializeComponent();
                this.log = log;
                this._CaijiCommHelper = _CaijiCommHelper;
                module = _Module;
                if (module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("module=" + Newtonsoft.Json.JsonConvert.SerializeObject(module), true, false);
                }
                if (Yqun.Common.ContextCache.ApplicationContext.Current.UserCode.Length >= 4)
                {
                    ToolStripMenuItemSystemSet.Visible = false;
                }
                else
                {
                    ToolStripMenuItemSystemSet.Visible = true;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            #if UNITTEST
                IsUnitTest=true;
            #endif
            try
            {
                log.UserName = Yqun.Common.ContextCache.ApplicationContext.Current.UserName;
                log.TestRoom = module.SpecialSetting.TestRoomCode;
                log.MachineCode = module.SpecialSetting.MachineCode;

                machine = MachineFactory.GetMachine(module.SpecialSetting.MachineCompany, module.SpecialSetting.MachineType, IsUnitTest);
                
                
                if (machine == null)
                {
                    MessageBox.Show("系统配置有误，无法获取有效设备信息，请联系管理员");
                    return;
                }
                if (module.SpecialSetting.MachineCompany == "威海屏显")
                {
                    this.lblTitleTime.Text = "速率";
                }
                machine.log = log;
                machine.IsUnitTest = IsUnitTest;
                machine.Module = module;
                machine.IsFinished = false;
                machine.IsValidData = false;
                machine.DataReceive += new DataReceiveDelegate(DataReceive);
                machine.TestFinished += new TestFinishedDelegate(TestFinished);
                _TempMinValidValue = module.SpecialSetting.MinValidValue;
                if (module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("machine init finished", true, false);
                }

                //初始化界面
                BindView();
                
                //设置曲线图基本信息
                InitChart();
                isLoad = true;

                if (!Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
                {
                    module.SpecialSetting.TestRoomCode = Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code;
                }

                //显示龄期列表
                ShowStadiumInfo();

                timer_UploadLocalData.Start();
                Thread _ThreadTime = new Thread(GetServerDataTime);
                _ThreadTime.Start();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        private void InitChart()
        {
            //this.ChartLineControl.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
            //this.ChartLineControl.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
            //ChartLineControl.Series[0].ToolTip = "力值: #VALY1";
            this.ChartLineControl.Series[0].Points.Clear();
            this.ChartLineControl.ChartAreas[0].AxisX.Maximum = module.SpecialSetting.XDefaultMaxValue;
            this.ChartLineControl.ChartAreas[0].AxisY.Maximum = 50;
            this.ChartLineControl.ChartAreas[0].AxisX.Minimum = 0;
            this.ChartLineControl.ChartAreas[0].AxisX.Title = "时间";
            this.ChartLineControl.ChartAreas[0].AxisX.Interval = 20;
            this.ChartLineControl.ChartAreas[0].AxisY.Title = "力值";
            this.ChartLineControl.ChartAreas[0].Name = "曲线图";
            ChartLine(0, 0);
        }

        private void BindView()
        {
            try
            {
                InitDefaultResultView();
                btnEndTest.Enabled = false;
                if (module.SpecialSetting.MachineType == 1)
                {
                    _UploadYLJInfo = new UploadYLJInfo();
                    this.comboBox_SJCC.DataSource = module.CommonSetting.HNTCCList;
                    this.comboBox_SJJB.DataSource = module.CommonSetting.HNTJBList;
                    this.comboBox_Count.DataSource = module.CommonSetting.HNTSLList;
                    rb_sqf.Visible = false;
                    rb_xqf.Visible = false;
                    cb_jz.Visible = false;
                }
                else if (module.SpecialSetting.MachineType == 2)
                {
                    _UploadWNJInfo = new UploadWNJInfo();
                    this.comboBox_SJCC.DataSource = module.CommonSetting.GJCCLList;
                    this.comboBox_SJJB.DataSource = module.CommonSetting.GJJBList;
                    this.comboBox_Count.DataSource = module.CommonSetting.GJSLList;
                    if (module.SpecialSetting.QuFuType == 1)
                    {
                        rb_sqf.Checked = true;
                    }
                    else if (module.SpecialSetting.QuFuType == 2)
                    {
                        rb_xqf.Checked = true;
                    }
                    rb_sqf.Visible = true;
                    rb_xqf.Visible = true;
                    cb_jz.Visible = true;
                }

                this.comboBox_SJCC.DisplayMember = "Value";
                this.comboBox_SJCC.ValueMember = "Key";
                this.comboBox_SJJB.DisplayMember = "Value";
                this.comboBox_SJJB.ValueMember = "Key";
                this.comboBox_Count.DisplayMember = "Value";
                this.comboBox_Count.ValueMember = "Key";

                DataTable st = _CaijiCommHelper.GetLocalTestConfig(module.SpecialSetting.MachineType.ToString());

                comboBox_Name.DataSource = st;
                comboBox_Name.DisplayMember = "ModuleName";
                comboBox_Name.ValueMember = "ModuleID";
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }

        }

        private void InitDefaultResultView()
        {
            resultDataSource.Columns.Clear();
            resultDataSource.Rows.Clear();
            
            DataColumn ColumnXH = new DataColumn();
            ColumnXH.ColumnName = "序号";

            resultDataSource.Columns.Add(ColumnXH);
            if (module.SpecialSetting.MachineType == 1)
            {
                DResultView.ColumnCount = 3;
                DResultView.Columns[0].Label = "序号";
                DResultView.Columns[1].Label = "破坏荷载";
                DResultView.Columns[2].Label = "抗压强度";
                DResultView.Columns[0].Width = (float)(fpSpread1.Width*0.2);
                DResultView.Columns[1].Width = (float)(fpSpread1.Width * 0.37);
                DResultView.Columns[2].Width = (float)(fpSpread1.Width * 0.37);
                DataColumn ColumnLZ = new DataColumn();
                ColumnLZ.ColumnName = "破坏荷载";
                resultDataSource.Columns.Add(ColumnLZ);

                DataColumn ColumnQD = new DataColumn(); ;
                ColumnQD.ColumnName = "抗压强度";
                resultDataSource.Columns.Add(ColumnQD);
            }
            else if (module.SpecialSetting.MachineType == 2)
            {
                DResultView.ColumnCount = 5;
                DResultView.Columns[0].Label = "序号";
                DResultView.Columns[1].Label = "拉断力值";
                DResultView.Columns[2].Label = "抗拉强度";
                DResultView.Columns[3].Label = "屈服力值";
                DResultView.Columns[4].Label = "屈服强度";


                DResultView.Columns[0].Width = (float)(fpSpread1.Width * 0.1);
                DResultView.Columns[1].Width = (float)(fpSpread1.Width * 0.21);
                DResultView.Columns[2].Width = (float)(fpSpread1.Width * 0.21);
                DResultView.Columns[3].Width = (float)(fpSpread1.Width * 0.21);
                DResultView.Columns[4].Width = (float)(fpSpread1.Width * 0.21);
                DataColumn ColumnLZ = new DataColumn();
                ColumnLZ.ColumnName = "拉断力值";
                resultDataSource.Columns.Add(ColumnLZ);

                DataColumn ColumnQD = new DataColumn();
                ColumnQD.ColumnName = "抗拉强度";
                resultDataSource.Columns.Add(ColumnQD);

                DataColumn ColumnQF = new DataColumn();
                ColumnQF.ColumnName = "屈服力值";
                resultDataSource.Columns.Add(ColumnQF);

                DataColumn ColumnQFQD = new DataColumn();
                ColumnQFQD.ColumnName = "屈服强度";
                resultDataSource.Columns.Add(ColumnQFQD);
            }
            DResultView.Rows.Count = 0;
        }

        /// <summary>
        /// 显示当前待做试验
        /// </summary>
        private void ShowStadiumInfo()
        {
            StaidumInfoForm stadiumFrom = new StaidumInfoForm(module, log);
            try
            {
                stadiumFrom.ShowDialog();
                InitTextInfoControl(false);
                _JZTestData = stadiumFrom.JZTestData;
                comboBox_Count.SelectedIndex = 0;
                textBox_CurrentNumber.Text = "1";
                lblTime.Text = "000.00";
                lblLZ.Text = "000.00";
                txtLZ.Text = "";
                LQ = stadiumFrom.LQ;
                if (_JZTestData.ModuleID != Guid.Empty)
                {
                    comboBox_Name.SelectedValue = _JZTestData.ModuleID;
                }
                if (_JZTestData.StadiumID != Guid.Empty)
                {
                    //设置曲线图基本信息
                    InitChart();
                    this.textBox_DelCode.Text = _JZTestData.WTBH.Trim();

                    textBox_DelCode.Enabled = false ;
                    ChangeTestBaseInfo();
                }
                if (stadiumFrom.SJCC != "")
                {
                    comboBox_SJCC.SelectedValue = stadiumFrom.SJCC;
                }
                if (stadiumFrom.QDDJ != "")
                {
                    for (int i = 0; i < comboBox_SJJB.Items.Count; i++)
                    {
                        if (comboBox_SJJB.Items[i].ToString() == stadiumFrom.QDDJ)
                        {
                            comboBox_SJJB.SelectedIndex = i;
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

       
        /// <summary>
        /// 开始试验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_IsUploadFinish)
                {
                    MessageBox.Show("数据正在处理中，请稍等");
                    return;
                }
                if (string.IsNullOrEmpty(this.textBox_DelCode.Text.Trim()))
                {
                    MessageBox.Show("请输入委托编号");
                    return;
                }
                if (this.comboBox_Name.SelectedItem == null)
                {
                    MessageBox.Show("请选择试验名称");
                    return;
                }
                if (module.SpecialSetting.MachineType == 1)
                {
                    if (this.comboBox_SJCC.SelectedItem == null)
                    {
                        MessageBox.Show("请输入正确的试件尺寸");
                        return;
                    }
                    if (this.comboBox_SJJB.SelectedItem == null)
                    {
                        MessageBox.Show("请输入正确的试件强度");
                        return;
                    }
                }
                if (module.SpecialSetting.MachineType == 2)
                {
                    if (string.IsNullOrEmpty(this.comboBox_SJCC.Text))
                    {
                        MessageBox.Show("请输入正确的试件直径");
                        return;
                    }
                    if (this.comboBox_SJJB.SelectedItem == null)
                    {
                        MessageBox.Show("请选择正确的试件级别代号");
                        return;
                    }
                    //计算屈服力值
                    CalculateQFLimite();
                    if (module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("标准屈服力值为：" + QFLimit, true, false);
                    }
                }
                if (textBox_DelCode.Enabled == true && !Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService && textBox_DelCode.Text.Trim().IndexOf("TEST")!=0)
                {
                    _JZTestData.DocumentID = CalHelper.GetDocumentIDByTestRoomModuleAndWTBH(new Guid(comboBox_Name.SelectedValue.ToString()),
                            module.SpecialSetting.TestRoomCode, textBox_DelCode.Text.Trim());
                    if (_JZTestData.DocumentID == Guid.Empty)
                    {
                        MessageBox.Show("委托编号和模版信息不存在管理系统中，请查证后重新开始实验");
                        return;
                    }
                }
                if (this.cb_jz.Checked)
                {
                    try
                    {
                        BioKeyInputForm BKIF = new BioKeyInputForm(Yqun.Common.ContextCache.ApplicationContext.Current.UserCode);
                        if (BKIF.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                    }
                    catch(Exception exBio)
                    {
                        MessageBox.Show("见证功能需加配指纹仪，如无配置请勿勾选");
                        log.WriteLog(exBio.Message + System.Environment.NewLine + exBio.StackTrace, false, true);
                        return;
                    }
                }
                GetItemArea();
                machine.IsFinished = false;
                InitChart();
                _JZTestData.ModuleID = new Guid(comboBox_Name.SelectedValue.ToString());
                _JZTestData.WTBH = textBox_DelCode.Text.Trim();
                configList = _CaijiCommHelper.GetConfigJson(_JZTestData.ModuleID.ToString());
                if (module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("模板配置信息为：" + Newtonsoft.Json.JsonConvert.SerializeObject(configList), true, false);
                }
                machine.CurrentNumber = int.Parse(textBox_CurrentNumber.Text.Trim());
                _TotalNumber = int.Parse(comboBox_Count.SelectedValue.ToString());

                _StartTime = DateTime.Now;
                txtLZ.Text = string.Empty;
                InitTextInfoControl(true);

                module.SpecialSetting.QFStartValue = module.SpecialSetting.QFStartValueMPA * Area / 1000;

                if (module.SpecialSetting.MachineCompany == "丰仪万能机")
                {
                    module.SpecialSetting.MinValidValue = _TempMinValidValue * Area / 1000;
                }

                if (module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("module.SpecialSetting.QFStartValue：" + module.SpecialSetting.QFStartValue.ToString(), true, false);
                    log.WriteLog("module.SpecialSetting.MinValidValue：" + module.SpecialSetting.MinValidValue.ToString(), true, false);
                    log.WriteLog("Module.SpecialSetting.MinFinishValue：" + module.SpecialSetting.MinFinishValue.ToString(), true, false); ;
                }
                OldMaxForce = 0.0;
                Thread UpData = new Thread(DoAction);
                UpData.IsBackground = true;
                UpData.Start();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                if (ex.Message.IndexOf("无法连接") > -1)
                {
                    Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService = true;
                }
            }
        }

        /// <summary>
        /// 结束实验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEndTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("您确定停止当前试验吗？", "停止采集", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    machine.IsFinished = true;
                    InitTextInfoControl(false);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        /// <summary>
        ///界面控件状态切换
        /// </summary>
        private void InitTextInfoControl(bool start)
        {
            if (_JZTestData != null && !start)
            {
                _JZTestData.DocumentID = Guid.Empty;
                _JZTestData.StadiumID = Guid.Empty;
            }
            try
            {
                    this.btnStartTest.Invoke(new InterfaceChangedHandle(this.IntiControlStatu), new object[] { start });
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        private void IntiControlStatu(bool start)
        {
            try
            {
                btnStartTest.Enabled = !start;
                btnEndTest.Enabled = start;
                comboBox_Count.Enabled = !start;
                comboBox_Name.Enabled = !start;
                comboBox_SJCC.Enabled = !start;
                comboBox_SJJB.Enabled = !start;
                textBox_DelCode.Enabled = !start;
                textBox_CurrentNumber.Enabled = !start;
                rb_sqf.Enabled = !start;
                rb_xqf.Enabled = !start;
                cb_jz.Enabled = !start;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        /// <summary>
        /// 开始触发采集
        /// </summary>
        private void DoAction()
        {
            try
            {
                if (module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("进入采集线程，并开启采集", true, false);
                }
                machine.StartAcquisition();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        /// <summary>
        /// 刷新控件状态
        /// </summary>
        /// <param name="tempJZRealTimeData"></param>
        void SetControlValue(Object GetobjectiveData, System.EventArgs e)
        {
            try
            {
                this.lblLZ.Text = Convert.ToDouble(GetobjectiveData).ToString("000.00");
                if (module.SpecialSetting.MachineCompany == "威海屏显")
                {
                    this.lblTime.Text = ((Convert.ToDouble(GetobjectiveData) - OldMaxForce) / 150 * 1000).ToString("00.000");
                }
                else
                {
                    this.lblTime.Text = (DateTime.Now - _StartTime).TotalSeconds.ToString("000.00");
                }
                if (txtLZ.Lines.Length > 100)
                {
                    txtLZ.Text = string.Empty;
                }
                this.txtLZ.AppendText(System.Environment.NewLine + "实时力值=" + Convert.ToDouble(GetobjectiveData).ToString("000.00"));
                OldMaxForce = Convert.ToDouble(GetobjectiveData);
                txtLZ.Focus();
                txtLZ.Select(txtLZ.TextLength, 0);
                txtLZ.ScrollToCaret();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message+System.Environment.NewLine+ex.StackTrace, false, false);
            }
        }

        /// <summary>
        /// 记录仪表单次返回力值并处理
        /// </summary>
        /// <param name="GetobjectiveData"></param>
        void DataReceive(object GetobjectiveData)
        {
            try
            {
                float lz = 0.0f;
                Boolean suc = float.TryParse(GetobjectiveData.ToString(), out lz);
                if (!suc)
                {
                    return;
                }
                MaxForce = MaxForce > lz ? MaxForce : lz;
                JZRealTimeData tempJZRealTimeData = new JZRealTimeData();
                tempJZRealTimeData.Time = DateTime.Now.AddMinutes(module.SpecialSetting.TimeSpanMinute);
                tempJZRealTimeData.Value = lz;
                _JZTestData.RealTimeData.Add(tempJZRealTimeData);
                log.InfoLog(tempJZRealTimeData.Time.ToString(), lz.ToString(), _JZTestData.WTBH, machine.CurrentNumber);
                double totalSeconds = Math.Round((DateTime.Now - _StartTime).TotalMilliseconds / 1000.0f, 2);
                if (_CrrTime == DateTime.MinValue)
                {
                    _CrrTime = DateTime.Now;
                }
                else if (( DateTime.Now-_CrrTime).TotalMilliseconds >= module.SpecialSetting.DrawChartInterval)
                {
                    _CrrTime = DateTime.Now;
                    if (this.ChartLineControl.InvokeRequired)
                    {
                        this.ChartLineControl.Invoke(new SetChartLine(this.ChartLine), new object[] { totalSeconds, lz });
                    }
                    else
                    {
                        ChartLine(totalSeconds, lz);
                    }
                }
                if (lblLZ.InvokeRequired)
                {
                    lblLZ.BeginInvoke(new System.EventHandler(SetControlValue), lz, null);
                }
                else
                {
                    SetControlValue(lz, null);
                }
                if (module.SpecialSetting.MachineType == 1)
                {
                    _UploadYLJInfo.F_YSKYLZ += lz.ToString() + ",";
                }
                else
                {
                    _UploadWNJInfo.F_YSKLLZ += lz.ToString() + ",";
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, false, false);
            }
        }

        /// <summary>
        /// 当前试验结束
        /// </summary>
        /// <param name="CurrentNumber"></param>
        void TestFinished(int CurrentNumber)
        {
            if (module.SpecialSetting.IsDebug)
            {
                log.WriteLog("当前试验完成，序号为：" + CurrentNumber, true, false);
            }
            UploadModule um = new UploadModule();
            try
            {
                um.CurrentNumber = CurrentNumber;
                um.List = new List<float>();
                um.RealTimeData = new List<JZRealTimeData>();
                um.MaxForce = MaxForce;
                um.TotalNumber = _TotalNumber;
                foreach (var item in _JZTestData.RealTimeData)
                {
                    um.RealTimeData.Add(item);
                    um.List.Add(item.Value);
                }
                if (module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("_JZTestData.RealTimeData.Count：" + _JZTestData.RealTimeData.Count.ToString(), true, false);
                }
                _JZTestData.RealTimeData.Clear();
                MaxForce = 0.0f;
                if (module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("Clear===_JZTestData.RealTimeData.Count：" + _JZTestData.RealTimeData.Count.ToString(), true, false);
                }
                if (module.SpecialSetting.ValidCount <= 0)
                {
                    module.SpecialSetting.ValidCount = 30;
                }
                if (module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("module.SpecialSetting.ValidCount：" + module.SpecialSetting.ValidCount.ToString(), true, false);
                }
                List<float> tempList = (from n in um.List
                                        where n >= module.SpecialSetting.MinValidValue
                                        select n).ToList();

                if (module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("tempList.Distinct().Count()：" + tempList.Distinct().Count().ToString(), true, false);
                }
                if (tempList.Distinct().Count() < module.SpecialSetting.ValidCount)
                {
                    if (module.SpecialSetting.IsDebug)
                    {

                        log.WriteLog("um.List:" + Newtonsoft.Json.JsonConvert.SerializeObject(um.List), true, false);
                        log.WriteLog("um.List.Distinct().Count：" + tempList.Distinct().Count().ToString(), true, false);
                        log.WriteLog("module.SpecialSetting.ValidCount：" + module.SpecialSetting.ValidCount.ToString(), true, false);
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n@@" + ex.StackTrace, true, true);
            }

            try
            {
                if (module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("定义处理线程，处理上传数据！", true, false);
                }
                Thread _ThreadUpload = new Thread(new ParameterizedThreadStart(ProcessDataAndUpload));
                _ThreadUpload.IsBackground = true;
                _ThreadUpload.Start(um);
                
                if (module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("定义处理线程、开始执行完成。", true, false);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        /// <summary>
        /// 处理数据并上传
        /// </summary>
        /// <param name="CurrentNumber"></param>
        private void ProcessDataAndUpload(Object obj)
        {
            int tempLineNum = 0;
            Guid _tempGuid = Guid.Empty;
            _IsUploadFinish = false;
            #region 上传线程方法
            UploadModule um = null;
            try
            {
                um = obj as UploadModule;
                if (module.SpecialSetting.IsDebug)
                {
                    if (um != null)
                    {
                        log.WriteLog("开启数据处理与上传线程，参数为：" + Newtonsoft.Json.JsonConvert.SerializeObject(um), true, false);
                    }
                    else
                    {
                        log.WriteLog("开启数据处理与上传线程，参数为NULL", true, false);
                    }
                }
            }
            catch (Exception exUm)
            {
                log.WriteLog(exUm.Message + "\r\n" + exUm.StackTrace, true, true);
            }
            List<JZTestCell> testData = new List<JZTestCell>();
            try
            {
                String moduleName = GetControlTextMoth(comboBox_Name, false);
                float qf = 0.0f;
                if (module.SpecialSetting.MachineType == 2 && (moduleName.Contains("钢筋试验报告") || moduleName.Contains("钢材物理性能")))
                {
                    qf = CalHelper.CalQF(module, um.List, QFLimit, um.MaxForce, log, machine);
                    if (this.InvokeRequired)
                    {

                        string a = GetControlTextMoth(comboBox_SJCC, true).ToString();
                        string b = um.CurrentNumber.ToString();
                        this.Invoke(new SetDHBJDel(SetDHBJ), new object[] { float.Parse(GetControlTextMoth(comboBox_SJCC, true)),um.CurrentNumber });
                    }
                    else
                    {
                        SetDHBJ(float.Parse(comboBox_SJCC.Text),um.CurrentNumber);
                    }
                    qf = CalHelper.CalQF(module, um.List, QFLimit, um.MaxForce, log, machine);
                }

                JZTestConfig currentConfig = GetCurrentConfig(um.CurrentNumber);
                testData = GetTestData(currentConfig, qf, um.MaxForce);
                tempLineNum = resultDataSource.Rows.Count;

                resultDataSource.Rows.Add(resultDataSource.NewRow());
                resultDataSource.Rows[resultDataSource.Rows.Count - 1][0] = um.CurrentNumber;
                if (module.SpecialSetting.MachineType == 1)
                {
                    resultDataSource.Rows[resultDataSource.Rows.Count - 1][1] = um.MaxForce.ToString("000.00");
                    resultDataSource.Rows[resultDataSource.Rows.Count - 1][2] = (um.MaxForce / Area * 1000).ToString("000.00");

                    if (module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("最大力值：" + um.MaxForce.ToString("000.00"), false, false);
                        log.WriteLog("面积：" + Area.ToString(), false, false);
                        log.WriteLog("破坏荷载：" + (um.MaxForce / Area * 1000).ToString("000.00"), false, false);
                    }
                }
                else
                {
                    resultDataSource.Rows[resultDataSource.Rows.Count - 1][1] = um.MaxForce.ToString("000.00");
                    resultDataSource.Rows[resultDataSource.Rows.Count - 1][2] = (um.MaxForce / Area * 1000).ToString("000.00");

                    if (module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("面积：" + Area.ToString(), false, false); 
                        log.WriteLog("抗拉强度：" + (um.MaxForce / Area * 1000).ToString("000.00"), false, false);
                    }
                    resultDataSource.Rows[resultDataSource.Rows.Count - 1][3] = qf.ToString("000.00");
                    resultDataSource.Rows[resultDataSource.Rows.Count - 1][4] = (qf / Area * 1000).ToString("000.00");


                    if (module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("面积：" +  Area .ToString(), false, false);
                        log.WriteLog("屈服强度：" + (qf / Area * 1000).ToString("000.00"), false, false);
                    }
                }
                if (this.InvokeRequired)
                {
                    this.Invoke(new SetGridViewValue(BindTestResult));
                }
                else
                {
                    BindTestResult();
                }

                if (module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("CurrentNumber:" + um.CurrentNumber.ToString() + ";_TotalNumber:" + _TotalNumber.ToString(), true, false);
                }
                if (um.CurrentNumber < um.TotalNumber)
                {
                    machine.CurrentNumber = machine.CurrentNumber + 1;
                    SetThreadControlText(textBox_CurrentNumber, machine.CurrentNumber.ToString());
                    if (module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("CurrentNumber:" + um.CurrentNumber.ToString() + ";_TotalNumber:" + _TotalNumber.ToString(), true, false);
                    }
                }
                else
                {
                    machine.IsFinished = true;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
            try
            {
                if (module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("开始设置铁道部数据", true, false);
                }

                //设置上传铁道部数据对象
                SetTDBUploadData(um.CurrentNumber);
                if (module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("设置铁道部数据完成，并开始上传", true, false);
                }
                if (_CaijiCommHelper.UploadTestData(_JZTestData, _TotalNumber, um.CurrentNumber, module.SpecialSetting.MachineCode, _UploadJson, _UploadCode.ToString(), testData, um.RealTimeData,out _tempGuid))
                {
                    SetLineColorMoth(1, tempLineNum);
                }
                else
                {
                    SetLineColorMoth(2, tempLineNum);
                }

                if (module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("数据上传完毕，铁道部数据为：" + _UploadJson, true, false);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }

            if (module.SpecialSetting.IsDebug)
            {
                if (um != null)
                {
                    log.WriteLog("数据处理与上传线程，参数为：" + Newtonsoft.Json.JsonConvert.SerializeObject(um)+System.Environment.NewLine+"上传完成-------------", true, false);
                }
                else
                {
                    log.WriteLog("开启数据处理与上传线程，参数为NULL" + System.Environment.NewLine + "上传完成-------------", true, false); 
                }
            }
            if (machine.IsFinished)
            {
                InitTextInfoControl(false);
            }
            #endregion
            _IsUploadFinish = true;
        }

        private List<JZTestCell> GetTestData(JZTestConfig config, float qf, float maxLZ)
        {
            List<JZTestCell> list = new List<JZTestCell>();
            if (config != null)
            {
                try
                {
                    for (int i = 0; i < config.Config.Count; i++)
                    {
                        JZTestCell cell = new JZTestCell();
                        cell.CellName = config.Config[i].CellName;
                        cell.Name = config.Config[i].Name;
                        cell.SheetID = config.Config[i].SheetID;
                        list.Add(cell);
                        switch (cell.Name)
                        {
                            case JZTestEnum.DHBJ:
                                {
                                    cell.Value = dhbj;
                                    break;
                                }
                            case JZTestEnum.LDZDL:
                                {
                                    cell.Value = maxLZ;
                                    _UploadWNJInfo.F_LZ = maxLZ.ToString();
                                    break;
                                }
                            case JZTestEnum.PHHZ:
                                {
                                    cell.Value = maxLZ;
                                    _UploadYLJInfo.F_KYLZ = maxLZ.ToString();
                                    break;
                                }
                            case JZTestEnum.QFL:
                                {
                                    cell.Value = qf;
                                    _UploadWNJInfo.F_QFLZ = qf.ToString();
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
            return list;
        }

        private JZTestConfig GetCurrentConfig(Int32 currentNumber)
        {
            JZTestConfig config = null;
            try
            {
                if (configList != null && configList.Count > 0)
                {
                    foreach (var item in configList)
                    {
                        if (item.SerialNumber == currentNumber)
                        {
                            config = item;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
            return config;
        }

        /// <summary>
        /// 获取上传铁道部数值
        /// </summary>
        private void SetTDBUploadData(Int32 currentNumber)
        {
            try
            {
                UploadOperation _UploadOperation = new UploadOperation();
                String sjjb = GetControlTextMoth(comboBox_SJJB, false);
                String sjcc = GetControlTextMoth(comboBox_SJCC, false);
                String machineCode = _CaijiCommHelper.GetECode(module.SpecialSetting.MachineCode);
                String moduleID = GetControlTextMoth(comboBox_Name, true);
                if (module.SpecialSetting.IsDebug)
                {
                    log.WriteLog("获取实验公共信息完成", true, false);
                }
                if (module.SpecialSetting.MachineType == 2)
                {
                    if (module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("设置万能机数据", true, false);
                    }
                    _UploadWNJInfo.F_AREA = Area.ToString();
                    _UploadWNJInfo.F_GCZJ = sjcc;
                    _UploadWNJInfo.F_GUID = Guid.NewGuid().ToString();
                    _UploadWNJInfo.F_OPERATOR = _JZTestData.UserName;
                    _UploadWNJInfo.F_PZCODE = (sjjb == "HRB400" || sjjb == "HRB400E") ? "HRB400/HRB400E" : sjjb;
                    _UploadWNJInfo.F_RTCODE = _CaijiCommHelper.GetInfoModelCode(moduleID);
                    _UploadWNJInfo.F_SBBH = machineCode;
                    _UploadWNJInfo.F_SCL = "";
                    _UploadWNJInfo.F_SJBH = currentNumber.ToString();
                    _UploadWNJInfo.F_SOFTCOM = "北京金舟";
                    _UploadWNJInfo.F_SYSJ = DateTime.Now.ToString();
                    _UploadWNJInfo.F_WTBH = _JZTestData.WTBH;
                    _UploadWNJInfo.F_WY = "0";
                    _UploadCode = _UploadOperation.EncodeDataModel(_UploadWNJInfo, out _UploadJson, module.SpecialSetting.MachineCode);
                }
                else
                {
                    if (module.SpecialSetting.IsDebug)
                    {
                        log.WriteLog("设置压力机数据", true, false);
                    }
                    _UploadYLJInfo.F_GUID = Guid.NewGuid().ToString();
                    _UploadYLJInfo.F_ISWJJ = "0";
                    _UploadYLJInfo.F_LQ = LQ;
                    _UploadYLJInfo.F_OPERATOR = _JZTestData.UserName;
                    _UploadYLJInfo.F_QDDJ = sjjb;
                    _UploadYLJInfo.F_RTCODE = _CaijiCommHelper.GetInfoModelCode(moduleID);
                    _UploadYLJInfo.F_SBBH = machineCode;
                    _UploadYLJInfo.F_SJBH = currentNumber.ToString();
                    _UploadYLJInfo.F_SJCC = GetSJCCMethod(sjcc);
                    _UploadYLJInfo.F_SOFTCOM = "北京金舟";
                    _UploadYLJInfo.F_SYSJ = DateTime.Now.ToString(); ;
                    _UploadYLJInfo.F_WTBH = _JZTestData.WTBH;
                    _UploadCode = _UploadOperation.EncodeDataModel(_UploadYLJInfo, out _UploadJson, module.SpecialSetting.MachineCode);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        /// <summary>
        /// 获取试件尺寸编码
        /// </summary>
        /// <returns></returns>
        private string GetSJCCMethod(String cc)
        {
            #region 试件尺寸编码
            string sjcc = string.Empty;
            switch (cc)
            {
                case "100×100×100":
                    sjcc = "2";
                    break;
                case "150×150×150":
                    sjcc = "1";
                    break;
                case "200×200×200":
                    sjcc = "3";
                    break;
                case "70.7×70.7×70.7":
                    sjcc = "4";
                    break;
            }
            return sjcc;
            #endregion
        }

        /// <summary>
        /// 输入断后标距
        /// </summary>
        private void SetDHBJ(float diameter, int crrentNum)
        {
            try
            {
                DHBJForm _DHBJForm = new DHBJForm(diameter,crrentNum);
                if (_DHBJForm.ShowDialog() == DialogResult.OK)
                {
                    dhbj = _DHBJForm.DHBJ;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        /// <summary>
        /// GridView中添加数据行
        /// </summary>
        private void BindTestResult()
        {
            try
            {
                DResultView.DataSource = resultDataSource;
                if (DResultView.Columns.Count == 3)
                {
                    DResultView.Columns[0].Width = 36;
                    DResultView.Columns[1].Width = 144;
                    DResultView.Columns[2].Width = 144;
                }
                else if (DResultView.Columns.Count == 5)
                {
                    DResultView.Columns[0].Width = 36;
                    DResultView.Columns[1].Width = 72;
                    DResultView.Columns[2].Width = 72;
                    DResultView.Columns[3].Width = 72;
                    DResultView.Columns[4].Width = 72;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        /// <summary>
        /// 画曲线图
        /// </summary>
        /// <param name="XValue"></param>
        /// <param name="YValue"></param>
        private void ChartLine(double XValue, double YValue)
        {
            try
            {
                if (XValue > this.ChartLineControl.ChartAreas[0].AxisX.Maximum)
                {
                    this.ChartLineControl.ChartAreas[0].AxisX.Maximum = XValue + 50;
                    this.ChartLineControl.ChartAreas[0].AxisX.Interval += 20;
                }
                if (YValue > this.ChartLineControl.ChartAreas[0].AxisY.Maximum)
                {
                    this.ChartLineControl.ChartAreas[0].AxisY.Maximum = YValue + 50;
                }
              this.ChartLineControl.Series[0].Points.AddXY(XValue, YValue);
                this.ChartLineControl.Invalidate();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, false, false);
            }
        }

        /// <summary>
        /// 计算标准屈服力值
        /// </summary>
        private void CalculateQFLimite()
        {
            try
            {
                Double jb = Convert.ToDouble(this.comboBox_SJJB.SelectedValue);//级别
                if (jb != 0)
                {
                    QFLimit = jb * ((Math.Pow(Convert.ToDouble(this.comboBox_SJCC.SelectedValue), 2.0)) / 4 * Math.PI) / 1000;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        /// <summary>
        /// 计算试件面积
        /// </summary>
        /// <returns></returns>
        private void GetItemArea()
        {
            #region 计算横截面积

            try
            {
                String sjccText = GetControlTextMoth(comboBox_SJCC, false);
                String sjccValue = GetControlTextMoth(comboBox_SJCC, true);
                if (module.SpecialSetting.MachineType == 1)
                {
                    switch (sjccText)
                    {
                        case "100×100×100":
                            Area = 100 * 100;
                            break;
                        case "150×150×150":
                            Area = 150 * 150;
                            break;
                        case "200×200×200":
                            Area = 200 * 200;
                            break;
                        case "70.7×70.7×70.7":
                            Area = 70.7 * 70.7;
                            break;
                        case "φ100×100":
                            Area = 3.14*50*50;
                            break;
                    }
                }
                if (module.SpecialSetting.MachineType == 2)
                {
                    Area = Math.PI * Math.Pow(Convert.ToDouble(sjccValue) / 2, 2.0);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
            #endregion
        }

        /// <summary>
        /// 获取控件的Text或者Value属性值
        /// </summary>
        /// <param name="_Control"></param>
        /// <param name="_Isvalue"></param>
        /// <returns></returns>
        private string GetControlTextMoth(Control _Control, bool _Isvalue)
        {
            try
            {
                if (_Control.InvokeRequired)
                {
                    return _Control.Invoke(new GetControlText(GetControlTextMoth), new object[] { _Control, _Isvalue }).ToString();
                }
                else
                {
                    return GetControlValueMoth(_Control, _Isvalue);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
                return string.Empty;
            }
        }

        private string GetControlValueMoth(Control _Control, bool _Isvalue)
        {
            if (_Isvalue)
            {
                ComboBox cb = _Control as ComboBox;
                if (cb != null)
                {
                    if (cb.SelectedValue != null)
                    {
                        return cb.SelectedValue.ToString().Trim();
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            else
            {
                if (_Control.Text != null)
                {
                    return _Control.Text.Trim();
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 设置控件text属相的值
        /// </summary>
        /// <param name="_Control"></param>
        /// <param name="text"></param>
        private void SetThreadControlText(Control _Control, string text)
        {
            try
            {
                if (_Control.InvokeRequired)
                {
                    _Control.Invoke(new SetControlText(SetThreadControlText), new object[] { _Control, text });
                }
                else
                {
                    SetThreadControlValueMoth(_Control, text);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        private void SetThreadControlValueMoth(Control _Control, string text)
        {
            _Control.Text = text;
        }

        #region 系统菜单事件

        /// <summary>
        /// 系统设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemSystemSet_Click(object sender, EventArgs e)
        {
            try
            {
                SystemSetupForm _SystemSetupForm = null;
                if (_SystemSetupForm == null)
                {
                    _SystemSetupForm = new SystemSetupForm(log,_CaijiCommHelper);
                    _SystemSetupForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        /// <summary>
        /// 龄期提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemStadium_Click(object sender, EventArgs e)
        {
            try
            {
                if (!btnStartTest.Enabled)
                {
                    MessageBox.Show("正在试验中，请结束实验后操作！", "提示", MessageBoxButtons.OK);
                    return;
                }
                if (!_IsUploadFinish)
                {
                    MessageBox.Show("数据正在上传中，请勿操作！", "提示", MessageBoxButtons.OK);
                    return;
                }
                ShowStadiumInfo();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        /// <summary>
        /// 关于选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            try
            {
                AboutBoxFrom _AboutBoxFrom = new AboutBoxFrom();
                _AboutBoxFrom.ShowDialog();
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        #endregion

        #region 触发事件

        /// <summary>
        /// 限制输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_CurrentNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
        }

        private void CaiJiForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("您确定退出采集系统吗？", "退出采集", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
            }
            else
            {
                if (!_IsUploadFinish)
                {
                    MessageBox.Show("数据正在处理中，请稍后退出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
                else
                {
                    System.Environment.Exit(System.Environment.ExitCode);
                }
            }
        }

        #endregion

        private void ChangeTestBaseInfo()
        {
            if (!isLoad)
            {
                return;
            }
            Guid moduleID = Guid.Empty;
            try
            {
                _TotalNumber = int.Parse(comboBox_Count.SelectedValue.ToString());
                indexList = new List<int>();
                for (int i = 1; i <= _TotalNumber; i++)
                {
                    indexList.Add(i);
                }
                GetItemArea();
                moduleID = new Guid(GetControlTextMoth(comboBox_Name, true));
                String wtbh = GetControlTextMoth(textBox_DelCode, false);
                if (wtbh.Trim() != "" && moduleID != Guid.Empty)
                {
                    _JZTestData.DocumentID = CalHelper.GetDocumentIDByTestRoomModuleAndWTBH(moduleID,
                        module.SpecialSetting.TestRoomCode, wtbh.Trim());
                    if (_JZTestData.DocumentID != Guid.Empty)
                    {
                        //DataTable _tempDt= _CaijiCommHelper.GetSingeStadiumByDataID(_JZTestData.DocumentID);
                        //if (_tempDt != null)
                        //{
                        //    _JZTestData.StadiumID = new Guid(_tempDt.Rows[0]["ID"].ToString());
                        //}
                        //else
                        //{
                            //_JZTestData.StadiumID = Guid.Empty;
                        //}
                        DataTable dt = CalHelper.GetTestDataByDataID(_JZTestData.DocumentID);
                        resultDataSource.Rows.Clear();
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Int32 index = 1;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                index = Int32.Parse(dt.Rows[i]["SerialNumber"].ToString());
                                indexList.Remove(index);
                                resultDataSource.Rows.Add(resultDataSource.NewRow());
                                resultDataSource.Rows[resultDataSource.Rows.Count - 1][0] = index;
                                List<JZTestCell> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZTestCell>>(dt.Rows[i]["TestData"].ToString());
                                if (module.SpecialSetting.MachineType == 1)
                                {
                                    Object phhz = GetCellValueByName(list, JZTestEnum.PHHZ);
                                    resultDataSource.Rows[resultDataSource.Rows.Count - 1][1] = phhz;
                                    if (phhz != null)
                                    {
                                        resultDataSource.Rows[resultDataSource.Rows.Count - 1][2] = (Convert.ToDouble(phhz) / Area  * 1000).ToString("000.00");
                                    }
                                }
                                else
                                {
                                    Object ldzdl = GetCellValueByName(list, JZTestEnum.LDZDL);
                                    Object qfl = GetCellValueByName(list, JZTestEnum.QFL);

                                    resultDataSource.Rows[resultDataSource.Rows.Count - 1][1] = ldzdl;
                                    if (ldzdl != null)
                                    {
                                        resultDataSource.Rows[resultDataSource.Rows.Count - 1][2] = (Convert.ToDouble(ldzdl) / Area * 1000).ToString("000.00");
                                    }
                                    resultDataSource.Rows[resultDataSource.Rows.Count - 1][3] = qfl;
                                    if (qfl != null)
                                    {
                                        resultDataSource.Rows[resultDataSource.Rows.Count - 1][4] = (Convert.ToDouble(qfl) / Area * 1000).ToString("000.00");
                                    }
                                }
                            }
                        }
                        BindTestResult();
                        LoadTestData(CalHelper.GetTestInfoByDataID(_JZTestData.DocumentID.ToString()));
                    }
                    else
                    {
                        MessageBox.Show("您所输入委托编号不存在管理系统中，请核实！");
                    }
                }
                if (indexList.Count > 0)
                {
                    textBox_CurrentNumber.Text = indexList[0].ToString();
                }
                else
                {
                    textBox_CurrentNumber.Text = "1";
                }
            }
            catch(Exception ex)
            {
                log.WriteLog(ex.Message + "\r\n" + ex.StackTrace, true, true);
            }
        }

        private Object GetCellValueByName(List<JZTestCell> list, JZTestEnum name)
        {
            Object obj = null;
            try
            {
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        if (item.Name == name)
                        {
                            obj = item.Value;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message + System.Environment.NewLine + ex.StackTrace, true, true);
            }
            return obj;
        }

        private void bt_check_Click(object sender, EventArgs e)
        {
            ChangeTestBaseInfo();
        }

        private void comboBox_Count_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeTestBaseInfo();
        }

        private void rb_sqf_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_sqf.Checked)
            {
                module.SpecialSetting.QuFuType = 1;
            }
            else if (rb_xqf.Checked)
            {
                module.SpecialSetting.QuFuType = 2;
            }
        }

        private void 查看曲线图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (DGVResult.SelectedRows.Count > 0)
            //{
            //    if (_JZTestData.DocumentID != Guid.Empty)
            //    {
            //        Int32 xh = Convert.ToInt32(DGVResult.SelectedRows[0].Cells[0].Value);

            //    }
            //    else
            //    {
            //        MessageBox.Show("请点击检测按钮，核实所输入委托编号");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请选择试件");
            //}
        }

        /// <summary>
        /// 加载历史数据，绑定相关实验信息
        /// </summary>
        private void LoadTestData(DataTable dt)
        {
            try
            {
                if (dt.Rows[0]["F_SJSize"].ToString() != "")
                {
                    comboBox_SJCC.SelectedValue = dt.Rows[0]["F_SJSize"].ToString();
                }
                if (dt.Rows[0]["F_Added"].ToString() != "")
                {
                    for (int i = 0; i < comboBox_SJJB.Items.Count; i++)
                    {
                        if (comboBox_SJJB.Items[i].ToString() == dt.Rows[0]["F_Added"].ToString())
                        {
                            comboBox_SJJB.SelectedIndex = i;
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

        /// <summary>
        /// 设置上传成功和失败后数据行颜色
        /// </summary>
        /// <param name="isSucceed"></param>
        /// <param name="LineNum"></param>
        private void SetLineColorMoth(int isSucceed,int LineNum)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetLineColor(SetLineColorMoth), new object[] { isSucceed,LineNum });
            }
            else
            {
                SetControlColorValueMoth(isSucceed, LineNum);
            }
        }

        private void SetControlColorValueMoth(int isSucceed, int LineNum)
        {
            switch (int.Parse(isSucceed.ToString()))
            {
                case 0:
                    DResultView.Rows[int.Parse(LineNum.ToString())].BackColor = Color.Red;
                    break;
                case 1:
                    DResultView.Rows[int.Parse(LineNum.ToString())].BackColor = Color.LawnGreen;
                    break;
                case 2:
                    DResultView.Rows[int.Parse(LineNum.ToString())].BackColor = Color.Yellow;
                    break;
            }
        }

        private void timer_UploadLocalData_Tick(object sender, EventArgs e)
        {
            if (_TimerIsFinish)
            {
                Thread _Thread = new Thread(TthreadUploadLocaldate);
                _Thread.IsBackground = true;
                _Thread.Start();
            }
        }

        /// <summary>
        ///  上传本地数据和同步本地时间
        /// </summary>
        private void TthreadUploadLocaldate()
        {
            _TimerIsFinish = false;
            if (!Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService)
            {
                if (this._IsUploadFinish)
                {
                    _CaijiCommHelper.UpdateLocalDateToServerThread();
                }
            }
            else
            {
                if (_CaijiCommHelper.TestServerConnection())
                {
                    Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService = false;
                }
                else
                {
                    Yqun.Common.ContextCache.ApplicationContext.Current.ISLocalService = true;
                }
            }

            _TimerIsFinish = true;
        }

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        private void GetServerDataTime()
        {
            module.SpecialSetting.TimeSpanMinute = 0;
            int tempNum = 0;
            while (tempNum < 5)
            {
                try
                {
                    _ServerDateTime = _CaijiCommHelper.GetServerTime();
                    if (_ServerDateTime != DateTime.Parse("0001-01-01 00:00:00"))
                    {
                        module.SpecialSetting.TimeSpanMinute = (_ServerDateTime - DateTime.Now).TotalMinutes;
                        log.WriteLog("本地时间和服务器相差：" + (DateTime.Now - _ServerDateTime).Days.ToString() + "天，" + (DateTime.Now - _ServerDateTime).Hours.ToString() + "小时；同步时间失败。", true, false);
                        break;
                    }
                }
                catch { }
                tempNum++;
            }
        }

        /// <summary>
        /// 绑定默认实验组数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Name.Text.IndexOf("钢筋试验报告") >= 0)
            {
                comboBox_Count.SelectedIndex = 0;
            }
            else
            {
                comboBox_Count.SelectedIndex = 1;
            }
        }
    }
}
