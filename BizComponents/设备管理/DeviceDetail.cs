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
    public partial class DeviceDetail : Form
    {
        private SheetDevice Source;
        public DeviceDetail(SheetDevice device)
        {
            InitializeComponent();

            Source = device;
        }

        private void DeviceDetail_Load(object sender, EventArgs e)
        {
            if (Source == null)
            {
                return;
            }

            var builder = new StringBuilder();

            builder.AppendLine(string.Format("ID\t\t{0}", Source.Device.ID));
            builder.AppendLine(string.Format("设备类型\t\t{0}", Source.Device.DeviceType == DeviceTypeEnum.Universal ? "万能机" : "压力机"));
            builder.AppendLine(string.Format("实验室编码\t\t{0}", Source.Device.TestRoomCode));
            builder.AppendLine(string.Format("设备编码\t\t{0}", Source.Device.MachineCode));
            builder.AppendLine(string.Format("设备厂家\t\t{0}", Source.Device.DeviceCompany));
            builder.AppendLine(string.Format("标段名称\t\t{0}", Source.SectionName));
            builder.AppendLine(string.Format("单位名称\t\t{0}", Source.UnitName));
            builder.AppendLine(string.Format("试验室名称\t\t{0}", Source.TestRoomName));
            builder.AppendLine(string.Format("是否删除\t\t{0}", Source.Device.IsActive ? "是" : "否"));
            builder.AppendLine(string.Format("是否是电液伺服\t\t{0}", Source.Device.IsDYSF ? "是" : "否"));
            builder.AppendLine(string.Format("公管中心编码\t\t{0}", Source.Device.RemoteCode1));
            builder.AppendLine(string.Format("信息中心编码\t\t{0}", Source.Device.RemoteCode2));
            builder.AppendLine(string.Format("客户端配置\t\t{0}", Source.Device.ClientConfig));
            builder.AppendLine(string.Format("客户端配置状态\t\t{0}", Source.Device.ConfigStatus));
            builder.AppendLine(string.Format("客户端配置上传时间\t\t{0}", Source.Device.ConfigUpdateTime));
            builder.AppendLine(string.Format("创建人\t\t{0}", Source.Device.CreateBy));
            builder.AppendLine(string.Format("创建时间\t\t{0}", Source.Device.CreateTime));
            builder.AppendLine(string.Format("最后修改人\t\t{0}", Source.Device.LastEditBy));
            builder.AppendLine(string.Format("最后修改时间\t\t{0}", Source.Device.LastEditTime));
            builder.AppendLine(string.Format("备注\t\t{0}", Source.Device.Comment));

            Information.Text = builder.ToString();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            var form = new ModifyDevice(Source);
            form.Show();

            this.Close();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除吗?", "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                var ids = new List<string>();
                ids.Add(Source.Device.ID.ToString());

                var result = DeviceHelperClient.DeleteDevice(ids);
                if (result.HasValue && result.Value)
                {
                    MessageBox.Show("已删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("删除失败", "提示",  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
