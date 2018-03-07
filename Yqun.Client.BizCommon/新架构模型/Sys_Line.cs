using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon
{
    [Serializable]
    public class Sys_Line
    {
        public Guid ID { get; set; }
        public String LineName { get; set; }
        public String Description { get; set; }
        public String LineIP { get; set; }
        public String LinePort { get; set; }
        public String UserName { get; set; }
        public String PassWord  { get; set; }
        public String DataBaseName { get; set; }
        public int StartUpload { get; set; }
        public String UploadAddress { get; set; }
        public String TestRoomCodeMap { get; set; }
        public String ModuleCodeMap { get; set; }
        public String JSDWCode { get; set; }
    }
}
