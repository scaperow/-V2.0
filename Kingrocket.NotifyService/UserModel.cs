using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingrocket.NotifyService
{
    public class OnlineUserInfo
    {
        #region Model
        private string _sessionid;
        private Guid _lineid;
        private string _testroomcode;
        private string _linename;
        private string _segmentname;
        private string _companyname;
        private string _testroomname;
        private byte[] _clientobj;
        private string _username;
        private DateTime _logintime = DateTime.Now;
        private DateTime _lastactivetime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public string SessionID
        {
            set { _sessionid = value; }
            get { return _sessionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid LineID
        {
            set { _lineid = value; }
            get { return _lineid; }
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
        public string LineName
        {
            set { _linename = value; }
            get { return _linename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SegmentName
        {
            set { _segmentname = value; }
            get { return _segmentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TestRoomName
        {
            set { _testroomname = value; }
            get { return _testroomname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] ClientObj
        {
            set { _clientobj = value; }
            get { return _clientobj; }
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
        public DateTime LoginTime
        {
            set { _logintime = value; }
            get { return _logintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastActiveTime
        {
            set { _lastactivetime = value; }
            get { return _lastactivetime; }
        }
        #endregion Model
    }
}
