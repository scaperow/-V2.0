using System;
namespace BizCommon
{
    /// <summary>
    /// Sys_TestData:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Sys_TestData
    {
        public Sys_TestData()
        { }
        #region Model
        private Guid _id;
        private Guid _dataid;
        private Guid _stadiumid;
        private Guid _moduleid;
        private string _wtbh;
        private string _testroomcode;
        private int _serialnumber;
        private string _username;
        private DateTime _createdtime = DateTime.Now;
        private string _testdata;
        private string _realtimedata;
        private string _machinecode = "";
        private int _status = 0;
        private string _uploadinfo = "";
        private string _uploadcode = "";
        private int _uploadtdb = 0;
        private int _uploademc = 0;
        private int _totallnumber = 0;
        /// <summary>
        /// 
        /// </summary>
        public Guid ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid DataID
        {
            set { _dataid = value; }
            get { return _dataid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid StadiumID
        {
            set { _stadiumid = value; }
            get { return _stadiumid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid ModuleID
        {
            set { _moduleid = value; }
            get { return _moduleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WTBH
        {
            set { _wtbh = value; }
            get { return _wtbh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TestRoomCode
        {
            set { _testroomcode = value; }
            get { return _testroomcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SerialNumber
        {
            set { _serialnumber = value; }
            get { return _serialnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedTime
        {
            set { _createdtime = value; }
            get { return _createdtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TestData
        {
            set { _testdata = value; }
            get { return _testdata; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RealTimeData
        {
            set { _realtimedata = value; }
            get { return _realtimedata; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MachineCode
        {
            set { _machinecode = value; }
            get { return _machinecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UploadInfo
        {
            set { _uploadinfo = value; }
            get { return _uploadinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UploadCode
        {
            set { _uploadcode = value; }
            get { return _uploadcode; }
        }
        /// <summary>
        /// 上传铁道部，0未上传；1上传成功
        /// </summary>
        public int UploadTDB
        {
            set { _uploadtdb = value; }
            get { return _uploadtdb; }
        }
        /// <summary>
        /// 上传工管中心，1上传工管中心成功；0未上传工管中心
        /// </summary>
        public int UploadEMC
        {
            set { _uploademc = value; }
            get { return _uploademc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TotallNumber
        {
            set { _totallnumber = value; }
            get { return _totallnumber; }
        }
        #endregion Model

    }
}

