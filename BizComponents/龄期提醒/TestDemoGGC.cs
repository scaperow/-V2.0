using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BizComponents
{
    public partial class TestDemoGGC : Form
    {
        DataTable dt = null;
        Dictionary<String, Guid> moduleList = new Dictionary<string, Guid>();
        Dictionary<String, String> configList = new Dictionary<string, String>();

        public TestDemoGGC()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_sent_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean flag = true;
                Int32 currentNumber = int.Parse(cb_items.SelectedItem.ToString());

                Guid documentID = Guid.Empty;
                Guid stadiumID = Guid.Empty;
                Guid moduleID = Guid.Empty;
                String wtbh = tb_wtbh.Text;
                String testRoomCode = "0001000400010001";// Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code;
                #region
                if (rb_statum.Checked)
                {
                    if (list_Statum.SelectedItem != null)
                    {
                        wtbh = list_Statum.SelectedItem.ToString();
                    }
                    else
                    {
                        MessageBox.Show("未选择龄期");
                        return;
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow[] rows = dt.Select("委托编号='" + wtbh + "'");
                        if (rows.Length > 0)
                        {
                            documentID = new Guid(rows[0]["DataID"].ToString());
                            stadiumID = new Guid(rows[0]["ID"].ToString());
                            moduleID = new Guid(rows[0]["ModuleID"].ToString());
                        }
                        else
                        {
                            MessageBox.Show("未按委托编号找到龄期");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("没有龄期数据");
                        return;
                    }
                }
                else
                {
                    moduleID = moduleList[cb_module.SelectedItem.ToString()];
                }
                #endregion
                Int32 totalNumber = int.Parse(cb_total.SelectedItem.ToString());
                String machineCode = testRoomCode + "0001";
                String realTimeData = "";

                ThreadParameter p = new ThreadParameter();
                p.currentNumber = currentNumber;
                p.totalNumber = totalNumber;
                p.documentID = documentID;
                p.stadiumID = stadiumID;
                p.moduleID = moduleID;
                p.wtbh = wtbh;
                p.testRoomCode = testRoomCode;
                p.machineCode = machineCode;
                p.realTimeData = realTimeData;

                flag = UploadTestData(p.currentNumber, p.totalNumber, p.documentID, p.stadiumID, p.moduleID, p.wtbh, p.testRoomCode, p.machineCode, p.realTimeData);

                MessageBox.Show(flag.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool UploadTestData(int currentNumber, int totalNumber, Guid documentID, Guid stadiumID, Guid moduleID, string wtbh, string testRoomCode, string machineCode, string realTimeData)
        {

            Boolean flag = false;
            //上传更新包
            DataMoveHelperClient dmhc = new DataMoveHelperClient();

            String testDataJson = configList[moduleID.ToString().ToUpper() + "_" + currentNumber.ToString()];
            String ldzdl = tb_ldzdl.Text.Trim() == "" ? "null" : tb_ldzdl.Text.Trim();
            String phhz = tb_phhz.Text.Trim() == "" ? "null" : tb_phhz.Text.Trim();
            String dhbj = tb_dhbj.Text.Trim() == "" ? "null" : tb_dhbj.Text.Trim();
            String qfl = tb_qfl.Text.Trim() == "" ? "null" : tb_qfl.Text.Trim();
            String testData = "";
            flag = true;
            for (int i = 0; i < 1; i++)
            {
                if (moduleID == new Guid("68F05EBC-5D34-49C5-9B57-49B688DF24F7"))
                {//钢筋试验报告
                    #region realTimeData
                    realTimeData = "[{\"Time\":\"2014-07-04T11:29:37.8100324+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-04T11:29:37.821033+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-04T11:29:37.9290392+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-04T11:29:37.9340395+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-04T11:29:38.0550464+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-04T11:29:38.0630468+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-04T11:29:38.3750647+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-04T11:29:38.3830652+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-04T11:29:38.5010719+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-04T11:29:38.5060722+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-04T11:29:38.8160899+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-04T11:29:38.8230903+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-04T11:29:38.9420971+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-04T11:29:38.9520977+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-04T11:29:39.0681043+08:00\",\"Value\":0.0}]";
                    #endregion
                    testData = testDataJson.Replace("{ldzdl}", (float.Parse(ldzdl) + i + currentNumber).ToString()).Replace("{qfl}", (float.Parse(qfl) + i + currentNumber).ToString()).Replace("{dhbj}", (float.Parse(dhbj) + i + currentNumber).ToString());
                }
                else if (moduleID == new Guid("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B"))
                {
                    //混凝土检查试件抗压强度试验报告
                    #region realTimeData
                    realTimeData = "[{\"Time\":\"2014-07-11T15:55:16.2764039+08:00\",\"Value\":5.39},{\"Time\":\"2014-07-11T15:55:16.2764039+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-11T15:55:16.4792043+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-11T15:55:16.4792043+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-11T15:55:16.6820046+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-11T15:55:16.6820046+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-11T15:55:16.884805+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-11T15:55:16.900405+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-11T15:55:17.0876053+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-11T15:55:17.0876053+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-11T15:55:17.2904057+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-11T15:55:17.2904057+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-11T15:55:17.493206+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-11T15:55:17.493206+08:00\",\"Value\":0.0},{\"Time\":\"2014-07-11T15:55:17.6960064+08:00\",\"Value\":0.0}]";
                    #endregion
                    testData = testDataJson.Replace("{phhz}", (float.Parse(phhz) + i + currentNumber).ToString());
                }

                flag = flag & dmhc.UploadTestData(documentID, moduleID, stadiumID, wtbh, testRoomCode,
                currentNumber, "彭震宇",
                testData, BizCommon.JZCommonHelper.GZipCompressString(realTimeData), totalNumber, machineCode);// Yqun.Common.ContextCache.ApplicationContext.Current.UserName
            }
            return flag;
        }
        private void TestDemo_Load(object sender, EventArgs e)
        {
            //
            moduleList.Add("混凝土检查试件抗压强度试验报告", new Guid("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B"));
            moduleList.Add("钢筋试验报告", new Guid("68F05EBC-5D34-49C5-9B57-49B688DF24F7"));

            configList.Add("68F05EBC-5D34-49C5-9B57-49B688DF24F7_1", "[{\"Name\":1,\"SheetID\":\"270a1da6-2045-405a-ae77-18c0c98c1edd\",\"CellName\":\"D27\",\"Value\":{qfl}},{\"Name\":2,\"SheetID\":\"270a1da6-2045-405a-ae77-18c0c98c1edd\",\"CellName\":\"D29\",\"Value\":{ldzdl}},{\"Name\":0,\"SheetID\":\"270a1da6-2045-405a-ae77-18c0c98c1edd\",\"CellName\":\"D26\",\"Value\":{dhbj}}]");
            configList.Add("68F05EBC-5D34-49C5-9B57-49B688DF24F7_2", "[{\"Name\":1,\"SheetID\":\"270a1da6-2045-405a-ae77-18c0c98c1edd\",\"CellName\":\"G27\",\"Value\":{qfl}},{\"Name\":2,\"SheetID\":\"270a1da6-2045-405a-ae77-18c0c98c1edd\",\"CellName\":\"G29\",\"Value\":{ldzdl}},{\"Name\":0,\"SheetID\":\"270a1da6-2045-405a-ae77-18c0c98c1edd\",\"CellName\":\"G26\",\"Value\":{dhbj}}]");

            configList.Add("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B_1", "[{\"Name\":3,\"SheetID\":\"303c05a6-4ff4-4f15-8118-69968988ee3b\",\"CellName\":\"H6\",\"Value\":{phhz}}]");
            configList.Add("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B_2", "[{\"Name\":3,\"SheetID\":\"303c05a6-4ff4-4f15-8118-69968988ee3b\",\"CellName\":\"H7\",\"Value\":{phhz}}]");
            configList.Add("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B_3", "[{\"Name\":3,\"SheetID\":\"303c05a6-4ff4-4f15-8118-69968988ee3b\",\"CellName\":\"H8\",\"Value\":{phhz}}]");
            configList.Add("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B_4", "[{\"Name\":3,\"SheetID\":\"303c05a6-4ff4-4f15-8118-69968988ee3b\",\"CellName\":\"H9\",\"Value\":{phhz}}]");
            configList.Add("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B_5", "[{\"Name\":3,\"SheetID\":\"303c05a6-4ff4-4f15-8118-69968988ee3b\",\"CellName\":\"H10\",\"Value\":{phhz}}]");
            configList.Add("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B_6", "[{\"Name\":3,\"SheetID\":\"303c05a6-4ff4-4f15-8118-69968988ee3b\",\"CellName\":\"H11\",\"Value\":{phhz}}]");
            configList.Add("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B_7", "[{\"Name\":3,\"SheetID\":\"303c05a6-4ff4-4f15-8118-69968988ee3b\",\"CellName\":\"H12\",\"Value\":{phhz}}]");
            configList.Add("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B_8", "[{\"Name\":3,\"SheetID\":\"303c05a6-4ff4-4f15-8118-69968988ee3b\",\"CellName\":\"H13\",\"Value\":{phhz}}]");
            configList.Add("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B_9", "[{\"Name\":3,\"SheetID\":\"303c05a6-4ff4-4f15-8118-69968988ee3b\",\"CellName\":\"H14\",\"Value\":{phhz}}]");
            configList.Add("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B_10", "[{\"Name\":3,\"SheetID\":\"303c05a6-4ff4-4f15-8118-69968988ee3b\",\"CellName\":\"H15\",\"Value\":{phhz}}]");
            configList.Add("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B_11", "[{\"Name\":3,\"SheetID\":\"303c05a6-4ff4-4f15-8118-69968988ee3b\",\"CellName\":\"H16\",\"Value\":{phhz}}]");
            configList.Add("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B_12", "[{\"Name\":3,\"SheetID\":\"303c05a6-4ff4-4f15-8118-69968988ee3b\",\"CellName\":\"H17\",\"Value\":{phhz}}]");
            configList.Add("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B_13", "[{\"Name\":3,\"SheetID\":\"303c05a6-4ff4-4f15-8118-69968988ee3b\",\"CellName\":\"H18\",\"Value\":{phhz}}]");
            configList.Add("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B_14", "[{\"Name\":3,\"SheetID\":\"303c05a6-4ff4-4f15-8118-69968988ee3b\",\"CellName\":\"H19\",\"Value\":{phhz}}]");
            configList.Add("05D0D71B-DEF3-42EE-A16A-79B34DE97E9B_15", "[{\"Name\":3,\"SheetID\":\"303c05a6-4ff4-4f15-8118-69968988ee3b\",\"CellName\":\"H20\",\"Value\":{phhz}}]");

        }

        private void rb_yl_CheckedChanged(object sender, EventArgs e)
        {
            String testRoomCode = Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code;
            if (Yqun.Common.ContextCache.ApplicationContext.Current.IsAdministrator)
            {
                testRoomCode = "0001000100010001";
            }
            Int32 deviceType = 1;
            if (rb_yl.Checked)
            {
                deviceType = 1;
            }
            else if (rb_wn.Checked)
            {
                deviceType = 2;
            }
            list_Statum.Items.Clear();
            dt = ModuleHelperClient.GetStadiumList("", "", testRoomCode, deviceType);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string ModuleID = row["ModuleID"].ToString();
                    if (ModuleID.ToUpper() == "05D0D71B-DEF3-42EE-A16A-79B34DE97E9B" || ModuleID.ToUpper() == "68F05EBC-5D34-49C5-9B57-49B688DF24F7")
                    {
                        list_Statum.Items.Add(row["委托编号"].ToString());
                    }
                }
            }
        }

        private void rb_offLine_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnUploadAll_Click(object sender, EventArgs e)
        {
            Boolean flag = true;
            for (int j = 0; j < list_Statum.Items.Count; j++)
            {
                Int32 totalNumber = int.Parse(cb_total.SelectedItem.ToString());

                Guid documentID = Guid.Empty;
                Guid stadiumID = Guid.Empty;
                Guid moduleID = Guid.Empty;
                String wtbh = tb_wtbh.Text;
                String testRoomCode = "0001000100010001";// Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code;
                #region
                if (rb_statum.Checked)
                {
                    if (list_Statum.Items[j] != null)
                    {
                        wtbh = list_Statum.Items[j].ToString();
                    }
                    else
                    {
                        MessageBox.Show("未选择龄期");
                        return;
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow[] rows = dt.Select("委托编号='" + wtbh + "'");
                        if (rows.Length > 0)
                        {
                            documentID = new Guid(rows[0]["DataID"].ToString());
                            stadiumID = new Guid(rows[0]["ID"].ToString());
                            moduleID = new Guid(rows[0]["ModuleID"].ToString());
                        }
                        else
                        {
                            MessageBox.Show("未按委托编号找到龄期");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("没有龄期数据");
                        return;
                    }
                }
                else
                {
                    moduleID = moduleList[cb_module.SelectedItem.ToString()];
                }
                #endregion
                String machineCode = testRoomCode + "0001";
                String realTimeData = "";

                for (int i = 0; i < totalNumber; i++)
                {
                    ThreadParameter p = new ThreadParameter();
                    p.currentNumber = i + 1;
                    p.totalNumber = totalNumber;
                    p.documentID = documentID;
                    p.stadiumID = stadiumID;
                    p.moduleID = moduleID;
                    p.wtbh = wtbh;
                    p.testRoomCode = testRoomCode;
                    p.machineCode = machineCode;
                    p.realTimeData = realTimeData;
                    ThreadPool.QueueUserWorkItem(new WaitCallback(StartUpload), p);
                    //flag = UploadTestData(i + 1);
                }

            }

            MessageBox.Show(flag.ToString());
        }
        private void StartUpload(object paremeter)
        {
            ThreadParameter p = paremeter as ThreadParameter;
            UploadTestData(p.currentNumber, p.totalNumber, p.documentID, p.stadiumID, p.moduleID, p.wtbh, p.testRoomCode, p.machineCode, p.realTimeData);
        }

        private class ThreadParameter
        {
            public int currentNumber;
            public int totalNumber;
            public Guid documentID;
            public Guid stadiumID;
            public Guid moduleID;
            public string wtbh;
            public string testRoomCode;
            public string machineCode;
            public string realTimeData;
        }

        private void btnUploadAllOne_Click(object sender, EventArgs e)
        {
            Boolean flag = true;
            Int32 totalNumber = int.Parse(cb_total.SelectedItem.ToString());

            Guid documentID = Guid.Empty;
            Guid stadiumID = Guid.Empty;
            Guid moduleID = Guid.Empty;
            String wtbh = tb_wtbh.Text;
            String testRoomCode = "0001000100010001";// Yqun.Common.ContextCache.ApplicationContext.Current.InTestRoom.Code;
            #region
            if (rb_statum.Checked)
            {
                if (list_Statum.SelectedItem != null)
                {
                    wtbh = list_Statum.SelectedItem.ToString();
                }
                else
                {
                    MessageBox.Show("未选择龄期");
                    return;
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow[] rows = dt.Select("委托编号='" + wtbh + "'");
                    if (rows.Length > 0)
                    {
                        documentID = new Guid(rows[0]["DataID"].ToString());
                        stadiumID = new Guid(rows[0]["ID"].ToString());
                        moduleID = new Guid(rows[0]["ModuleID"].ToString());
                    }
                    else
                    {
                        MessageBox.Show("未按委托编号找到龄期");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("没有龄期数据");
                    return;
                }
            }
            else
            {
                moduleID = moduleList[cb_module.SelectedItem.ToString()];
            }
            #endregion
            String machineCode = testRoomCode + "0001";
            String realTimeData = "";

            for (int i = 0; i < totalNumber; i++)
            {
                ThreadParameter p = new ThreadParameter();
                p.currentNumber = i + 1;
                p.totalNumber = totalNumber;
                p.documentID = documentID;
                p.stadiumID = stadiumID;
                p.moduleID = moduleID;
                p.wtbh = wtbh;
                p.testRoomCode = testRoomCode;
                p.machineCode = machineCode;
                p.realTimeData = realTimeData;
                ThreadPool.QueueUserWorkItem(new WaitCallback(StartUpload), p);
                //flag = UploadTestData(i + 1);
            }



            MessageBox.Show(flag.ToString());
        }
    }
}
