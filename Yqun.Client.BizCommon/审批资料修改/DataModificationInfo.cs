using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class DataModificationInfo
    {
        String _Index;
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

        Guid _DataID;
        public Guid DataID
        {
            get
            {
                return _DataID;
            }
            set
            {
                _DataID = value;
            }
        }

        Guid _ModelIndex;
        public Guid ModuleID
        {
            get
            {
                return _ModelIndex;
            }
            set
            {
                _ModelIndex = value;
            }
        }

        String _TestRoomCode;
        public String TestRoomCode
        {
            get
            {
                return _TestRoomCode;
            }
            set
            {
                _TestRoomCode = value;
            }
        }

        String _CompanyName;
        public String CompanyName
        {
            get
            {
                return _CompanyName;
            }
            set
            {
                _CompanyName = value;
            }
        }

        String _TestRoomName;
        public String TestRoomName
        {
            get
            {
                return _TestRoomName;
            }
            set
            {
                _TestRoomName = value;
            }
        }

        String _ModelName;
        public String ModelName
        {
            get
            {
                return _ModelName;
            }
            set
            {
                _ModelName = value;
            }
        }

        String _SponsorPerson;
        public String SponsorPerson
        {
            get
            {
                return _SponsorPerson;
            }
            set
            {
                _SponsorPerson = value;
            }
        }

        String _SponsorDate;
        public String SponsorDate
        {
            get
            {
                return _SponsorDate;
            }
            set
            {
                _SponsorDate = value;
            }
        }

        String _Caption;
        public String Caption
        {
            get
            {
                return _Caption;
            }
            set
            {
                _Caption = value;
            }
        }

        String _Reason;
        public String Reason
        {
            get
            {
                return _Reason;
            }
            set
            {
                _Reason = value;
            }
        }

        String _ApprovePerson;
        public String ApprovePerson
        {
            get
            {
                return _ApprovePerson;
            }
            set
            {
                _ApprovePerson = value;
            }
        }

        String _ApproveDate;
        public String ApproveDate
        {
            get
            {
                return _ApproveDate;
            }
            set
            {
                _ApproveDate = value;
            }
        }

        String _State;
        public String State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;
            }
        }

        String _ModifyItem;
        public String ModifyItem
        {
            get
            {
                return _ModifyItem;
            }
            set
            {
                _ModifyItem = value;
            }
        }

        public String Segment { get; set; }

        public String ProcessReason { get; set; }
    }
}
