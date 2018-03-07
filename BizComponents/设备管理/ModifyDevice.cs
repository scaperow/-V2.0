using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BizCommon;

namespace BizComponents
{
    public partial class ModifyDevice : Form
    {
        public bool IsEdit { private set; get; }
        public SheetDevice Source { private set; get; }
        public PrjFolder RoomSource { private set; get; }
        public Orginfo UnitSource { private set; get; }
        public Prjsct SectionSource { private set; get; }

        public ModifyDevice()
        {
            InitializeComponent();
            Section.DataSource = DepositoryPrjsctInfo.QueryPrjscts(Yqun.Common.ContextCache.ApplicationContext.Current.InProject.Code);
        }

        public ModifyDevice(Prjsct section, Orginfo unit, PrjFolder room)
        {
            InitializeComponent();

            SectionSource = section;
            UnitSource = unit;
            RoomSource = room;

            SetTreeValues(section, unit, room);
            GenerateMachineCode_LinkClicked(null, null);
        }

        public ModifyDevice(SheetDevice device)
        {
            InitializeComponent();
            IsEdit = true;
            Source = device;
            Text = "修改设备";
            ComboCompany.Text = Source.Device.DeviceCompany;
            Comment.Text = Source.Device.Comment;
            RemoteCode1.Text = Source.Device.RemoteCode1;
            RemoteCode2.Text = Source.Device.RemoteCode2;
            MachineCode.Text = Source.Device.MachineCode;
            Electro.Checked = Source.Device.IsDYSF;
            Universal.Checked = Source.Device.DeviceType == DeviceTypeEnum.Universal;
            Pressure.Checked = Source.Device.DeviceType == DeviceTypeEnum.Pressure;
            IsActive.Checked = Source.Device.IsActive;

            Quantum.Text = Source.Device.Quantum.ToString();

            SetTreeValues(new Prjsct()
            {
                PrjsctCode = device.SectionCode,
                PrjsctName = device.SectionName
            }, new Orginfo()
            {
                DepCode = device.UnitCode,
                DepName = device.UnitName
            }, new PrjFolder()
            {
                FolderName = device.TestRoomName,
                FolderCode = device.TestRoomCode
            });
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                SaveDevice();
            }
        }

        private void Reset()
        {
            var room = TestRoom.SelectedItem as PrjFolder;

            if (room == null || string.IsNullOrEmpty(room.FolderCode))
            {
                return;
            }

            MachineCode.Text = DeviceHelperClient.GenerateMachineCode(room.FolderCode);
            RemoteCode1.Text = RemoteCode2.Text = "";
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ElectroCheck_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UniversalRadio_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void SetValues()
        {

        }

        private void SetTreeValues(Prjsct section, Orginfo unit, PrjFolder room)
        {
            Section.SelectedIndexChanged -= new EventHandler(Section_SelectedIndexChanged);
            Unit.SelectedIndexChanged -= new EventHandler(Unit_SelectedIndexChanged);
            TestRoom.SelectedIndexChanged -= new EventHandler(TestRoom_SelectedIndexChanged);

            Section.DataSource = DepositoryPrjsctInfo.QueryPrjscts(Yqun.Common.ContextCache.ApplicationContext.Current.InProject.Code);
            Section.Text = section.PrjsctName;

            Unit.DataSource = DepositoryOrganInfo.QueryOrgans(section.PrjsctCode, "");
            Unit.Text = unit.DepName;

            TestRoom.DataSource = DepositoryFolderInfo.QueryPrjFolders(unit.DepCode, "");
            TestRoom.SelectedItem = room;
            TestRoom.SelectedValue = room.FolderCode;
            TestRoom.SelectedText = room.FolderName;
            TestRoom.Text = room.FolderName;


            Section.SelectedIndexChanged += new EventHandler(Section_SelectedIndexChanged);
            Unit.SelectedIndexChanged += new EventHandler(Unit_SelectedIndexChanged);
            TestRoom.SelectedIndexChanged += new EventHandler(TestRoom_SelectedIndexChanged);

        }

        private void SaveDevice()
        {
            if (Source == null)
            {
                Add();
            }
            else
            {
                Modify();
            }
        }

        private void Add()
        {
            var device = GetDeviceFill(null);
            var result = DeviceHelperClient.AddDevice(device);

            if (result.HasValue && result.Value)
            {
                //MessageBox.Show("添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
                //Tip.Show("添加成功", this.MachineCode, 5 * 1000);
                MessageBox.Show("添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("抱歉,没有添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Modify()
        {
            var device = GetDeviceFill(Source.Device);
            var result = DeviceHelperClient.EditDevice(device);

            if (result.HasValue && result.Value)
            {
                MessageBox.Show("修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Tip.Show("修改成功", MachineCode);
            }
            else
            {
                MessageBox.Show("抱歉,没有修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private Sys_Device GetDeviceFill(Sys_Device device)
        {
            if (device == null)
            {
                device = new Sys_Device()
                {
                    ID = Guid.NewGuid(),
                    CreateBy = Yqun.Common.ContextCache.ApplicationContext.Current.UserName,
                    CreateTime = DateTime.Now
                };
            }

            device.Comment = Comment.Text;
            device.LastEditBy = Yqun.Common.ContextCache.ApplicationContext.Current.UserName;
            device.LastEditTime = DateTime.Now;
            device.TestRoomCode = TestRoom.SelectedValue.ToString();
            device.DeviceCompany = ComboCompany.Text;
            device.IsActive = IsActive.Checked;
            device.DeviceType = Universal.Checked ? DeviceTypeEnum.Universal : DeviceTypeEnum.Pressure;
            device.RemoteCode1 = RemoteCode1.Text;
            device.RemoteCode2 = RemoteCode2.Text;
            device.MachineCode = MachineCode.Text;
            device.IsDYSF = Electro.Checked;
            device.DeviceType = Universal.Checked ? DeviceTypeEnum.Universal : DeviceTypeEnum.Pressure;
            device.Quantum = string.IsNullOrEmpty(Quantum.Text) ? 0 : int.Parse(Quantum.Text);

            return device;
        }

        private void Section_SelectedIndexChanged(object sender, EventArgs e)
        {
            var section = Section.SelectedItem as Prjsct;
            var units = new List<Orginfo>();

            if (section != null && !string.IsNullOrEmpty(section.PrjsctCode))
            {
                units.AddRange(DepositoryOrganInfo.QueryOrgans(section.PrjsctCode, ""));
            }

            Unit.DataSource = units;
        }

        private void Unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            var org = Unit.SelectedItem as Orginfo;
            var roomes = new List<PrjFolder>();

            if (org != null && !string.IsNullOrEmpty(org.DepCode))
            {
                roomes.AddRange(DepositoryFolderInfo.QueryPrjFolders(org.DepCode, ""));
            }

            TestRoom.DataSource = roomes;

        }

        private void TestRoom_Validating(object sender, CancelEventArgs e)
        {

            if (TestRoom.SelectedText == null)
            {
                e.Cancel = true;
                Errors.SetError(TestRoom, "请选择试验室");
            }
            else
            {
                Errors.SetError(TestRoom, "");
                e.Cancel = false;
            }
        }

        private void MachineCode_Validating(object sender, CancelEventArgs e)
        {
            var code = MachineCode.Text;

            if (!string.IsNullOrEmpty(code))
            {
                if (code.Length == 20)
                {
                    if (code.Substring(0, 16) == TestRoom.SelectedValue.ToString())
                    {

                        var table = DeviceHelperClient.GetDevice(code);
                        if (table == null || table.Rows.Count == 0)
                        {
                            Errors.SetError(MachineCode, "");
                            return;
                        }
                        else
                        {
                            if (Source == null)
                            {
                                Errors.SetError(MachineCode, "设备编码不能重复");
                            }
                            else
                            {
                                if (table.Rows[0]["ID"].ToString() != Source.Device.ID.ToString())
                                {
                                    Errors.SetError(MachineCode, "设备编码与其他的设备重复,请设置新的编码");
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        Errors.SetError(MachineCode, "编码后4位无效");
                    }
                }
                else
                {
                    Errors.SetError(MachineCode, "设备编码必须为20位数字");
                }
            }
            else
            {
                Errors.SetError(MachineCode, "请输入试验室编码");
            }

            e.Cancel = true;
        }

        private void DeviceCompany_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(ComboCompany.Text))
            {
                e.Cancel = true;
                Errors.SetError(ComboCompany, "请选择或输入厂家名称");
            }
            else
            {
                e.Cancel = false;
                Errors.SetError(ComboCompany, "");
            }
        }

        private void TestRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            var room = TestRoom.SelectedItem as PrjFolder;

            if (room == null || string.IsNullOrEmpty(room.FolderCode))
            {
                return;
            }

            if (Source == null)
            {
                MachineCode.Text = DeviceHelperClient.GenerateMachineCode(room.FolderCode);
            }
            else
            {
                MachineCode.Text = Source.Device.MachineCode;
            }
        }

        private void ModifyDevice_Load(object sender, EventArgs e)
        {
            SetValues();
        }

        private void GenerateMachineCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //if (++GenerateCount >= 5)
            //{
            //    MachineCode.ReadOnly = false;
            //}

            var room = TestRoom.SelectedItem as PrjFolder;

            if (room == null || string.IsNullOrEmpty(room.FolderCode))
            {
                return;
            }

            MachineCode.Text = DeviceHelperClient.GenerateMachineCode(room.FolderCode);
        }

        private void Quantum_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(Quantum.Text))
            {
                var quantum = 0;
                if (!int.TryParse(Quantum.Text, out quantum))
                {
                    e.Cancel = true;
                    Errors.SetError(Quantum, "请输入正确的数字");

                    return;
                }
                else
                {
                    Errors.SetError(Quantum, "");
                }
            }

            e.Cancel = false;
        }

        private void MachineCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
