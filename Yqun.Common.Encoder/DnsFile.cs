using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

namespace Yqun.Common.Encoder
{
    public class EncoderProgress : EventArgs 
    {
        public EncoderProgress() 
        {
        }
        #region 属性
        int _MainProgress = 0;
        public int MainProgress 
        {
            get 
            {
                return _MainProgress;
            }
            set 
            {
                _MainProgress = value;
            }
        }

        int _SubProgress = 0;
        public int SubProgress
        {
            get
            {
                return _SubProgress;
            }
            set
            {
                _SubProgress = value;
            }
        }

        #endregion
    }

    public delegate void EncoderHandler(EncoderProgress e);

    public class DnsFile
    {
        public DnsFile() 
        {        
        }
        object EncorderingObj = new object();
        public event EncoderHandler Encordering;
        void OnEncordering(EncoderProgress e) 
        {
            if (Encordering != null)
            {
                Encordering(e);
            }
        }

        public DnsFile(FileInfo[] FileInfos, SystemInfo PackageSysInfo)
        {
            _FileInfos = FileInfos;
            _PackageSysInfo = PackageSysInfo;
        }

        #region 属性
        private int _BufferLenth = 524288;
        public int BufferLenth 
        {
            get 
            {
                return _BufferLenth;
            }
            set 
            {
                _BufferLenth = value;
            }
        }

        private FileInfo[] _FileInfos;
        public FileInfo[] FileInfos 
        {
            get 
            {
                return _FileInfos;
            }
            set 
            {
                _FileInfos = value;
            }
        }

        private SystemInfo _PackageSysInfo;
        public SystemInfo PackageSysInfo
        {
            get
            {
                return _PackageSysInfo;
            }
            set
            {
                _PackageSysInfo = value;
            }
        }
        #endregion

        static DataSet GenerateInfoSet() 
        {
            DataSet d = new DataSet("fileinfoset");
            //mail table
            DataTable main = new DataTable("main");
            main.Columns.Add(new DataColumn("id", typeof(string)));
            main.Columns.Add(new DataColumn("property_name", typeof(string)));
            main.Columns.Add(new DataColumn("property_value", typeof(string)));
            d.Tables.Add(main);

            //file table
            DataTable file = new DataTable("file");
            file.Columns.Add(new DataColumn("id", typeof(string)));
            file.Columns.Add(new DataColumn("file_name", typeof(string)));
            file.Columns.Add(new DataColumn("file_start", typeof(long)));
            file.Columns.Add(new DataColumn("file_type", typeof(string)));
            file.Columns.Add(new DataColumn("file_lasttime", typeof(string)));
            file.Columns.Add(new DataColumn("action_type", typeof(string)));
            file.Columns.Add(new DataColumn("action_desc", typeof(string)));
            file.Columns.Add(new DataColumn("before_action_event", typeof(string)));
            file.Columns.Add(new DataColumn("after_action_event", typeof(string)));
            file.Columns.Add(new DataColumn("file_order", typeof(int)));
            
            d.Tables.Add(file);

            //segment table
            DataTable segment = new DataTable("segment");
            segment.Columns.Add(new DataColumn("id", typeof(string)));
            segment.Columns.Add(new DataColumn("file_id", typeof(string)));
            segment.Columns.Add(new DataColumn("segment_length", typeof(int)));
            segment.Columns.Add(new DataColumn("segment_fingermark", typeof(byte[])));
            segment.Columns.Add(new DataColumn("segment_order", typeof(int)));
            d.Tables.Add(segment);

            //certificate
            DataTable certificate = new DataTable("certificate");
            certificate.Columns.Add(new DataColumn("id", typeof(string)));
            certificate.Columns.Add(new DataColumn("private_key", typeof(string)));
            certificate.Columns.Add(new DataColumn("public_key", typeof(string)));
            d.Tables.Add(certificate);

            return d;          

        }

        static object GetProperty(DataSet InfoSet,
            int TableIndex,
            string IDColumnName,
            string IDValue,
            string AimCoumnName)
        {
            try
            {
                string sele = IDColumnName + " = " + "'" + IDValue.ToLower() + "'";
                DataRow[] rs = InfoSet.Tables[TableIndex].Select(sele);
                if (rs.Length > 0)
                {
                    object o = rs[0][AimCoumnName];
                    return o;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        static int SetProperty(ref DataSet InfoSet,
            int TableIndex,
            string IDColumnName,
            string IDValue,
            string AimCoumnName,
            object AimValue
            )
        {
            try
            {
                string sele = IDColumnName + " = " + "'" + IDValue.ToLower() + "'";
                DataRow[] rs = InfoSet.Tables[TableIndex].Select(sele);
                if (rs.Length > 0)
                {
                    rs[0][AimCoumnName] = AimValue;
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return -1;
            }
        }

        DataSet SetPackageInfo() 
        {
            string id =Guid.NewGuid().ToString();
            DataSet PropertySet = GenerateInfoSet();
            DataRow r = PropertySet.Tables["main"].NewRow();
            r[0] = id;
            r[1] = "guid";
            r[2] = PackageSysInfo.Guid;
            PropertySet.Tables["main"].Rows.Add(r);

            r = PropertySet.Tables["main"].NewRow();
            r[0] = Guid.NewGuid();
            r[1] = "update_guid";
            r[2] = PackageSysInfo.UpdateGuid;
            PropertySet.Tables["main"].Rows.Add(r);

            r = PropertySet.Tables["main"].NewRow();
            r[0] = Guid.NewGuid();
            r[1] = "vision";
            r[2] = PackageSysInfo.Vision;
            PropertySet.Tables["main"].Rows.Add(r);

            r = PropertySet.Tables["main"].NewRow();
            r[0] = Guid.NewGuid();
            r[1] = "username";
            r[2] = PackageSysInfo.UserName;
            PropertySet.Tables["main"].Rows.Add(r);

            r = PropertySet.Tables["main"].NewRow();
            r[0] = Guid.NewGuid();
            r[1] = "password";
            r[2] = PackageSysInfo.Password;
            PropertySet.Tables["main"].Rows.Add(r);

            r = PropertySet.Tables["main"].NewRow();
            r[0] = Guid.NewGuid();
            r[1] = "datetime";
            r[2] = PackageSysInfo.DateTime;
            PropertySet.Tables["main"].Rows.Add(r);

            r = PropertySet.Tables["main"].NewRow();
            r[0] = Guid.NewGuid();
            r[1] = "invalid_datetime";
            r[2] = PackageSysInfo.InvalidDateTime;
            PropertySet.Tables["main"].Rows.Add(r);

            r = PropertySet.Tables["main"].NewRow();
            r[0] = Guid.NewGuid();
            r[1] = "iscompressed";
            r[2] = PackageSysInfo.IsCompressed;
            PropertySet.Tables["main"].Rows.Add(r);

            r = PropertySet.Tables["main"].NewRow();
            r[0] = Guid.NewGuid();
            r[1] = "isencried";
            r[2] = PackageSysInfo.IsEncried;
            PropertySet.Tables["main"].Rows.Add(r);

            r = PropertySet.Tables["main"].NewRow();
            r[0] = Guid.NewGuid();
            r[1] = "isreadonly";
            r[2] = PackageSysInfo.IsReadOnly;
            PropertySet.Tables["main"].Rows.Add(r);

            r = PropertySet.Tables["main"].NewRow();
            r[0] = Guid.NewGuid();
            r[1] = "issigned";
            r[2] = PackageSysInfo.IsSigned;
            PropertySet.Tables["main"].Rows.Add(r);

            
            r = PropertySet.Tables["certificate"].NewRow();
            r[0] = id;
            r[1] = PackageSysInfo.PrivateKey;
            r[2] = PackageSysInfo.PublicKey;
            PropertySet.Tables["certificate"].Rows.Add(r);


            return PropertySet;
        }

        string AddFileInfo(ref DataSet PropertySet, FileInfo HereFileInfo,long FileStart,int FileOrder) 
        {
            string id = FileOrder.ToString();
            DataRow r = PropertySet.Tables["file"].NewRow();
            r["id"] = id;

            string fileName = HereFileInfo.FileName;
            int x = fileName.LastIndexOf("\\");            
            if(x>0)
            {
                fileName = fileName.Substring(x+1,fileName.Length-x-1);
            }

            r["file_name"] = fileName;
            r["file_start"] = FileStart;
            r["file_type"] = HereFileInfo.FileType;
            r["action_type"] = HereFileInfo.ActionType;
            r["action_desc"] = HereFileInfo.ActionDesc;
            r["before_action_event"] = HereFileInfo.BeforeActionEvent;
            r["after_action_event"] = HereFileInfo.AfterActionEvent;
            r["file_order"] = FileOrder;
            PropertySet.Tables["file"].Rows.Add(r);
            return id;
        }

        string AddFileInfo(ref DataSet PropertySet, FileInfo HereFileInfo, long FileStart, int FileOrder,string TopPath)
        {
            string id = FileOrder.ToString();
            DataRow r = PropertySet.Tables["file"].NewRow();
            r["id"] = id;

            string fileName = HereFileInfo.FileName;
            //int x = fileName.LastIndexOf("\\");
            //if (x > 0)
            //{
            //    fileName = fileName.Substring(x + 1, fileName.Length - x - 1);
            //}

            string tempStr = fileName.ToUpper();
            TopPath = TopPath.ToUpper();
            int x = tempStr.IndexOf(TopPath);
            if(x>=0)
            {
                fileName = fileName.Substring(x + TopPath.Length, fileName.Length - x - TopPath.Length);
            }

            r["file_name"] = fileName;
            r["file_start"] = FileStart;
            r["file_type"] = HereFileInfo.FileType;
            r["action_type"] = HereFileInfo.ActionType;
            r["action_desc"] = HereFileInfo.ActionDesc;
            r["before_action_event"] = HereFileInfo.BeforeActionEvent;
            r["after_action_event"] = HereFileInfo.AfterActionEvent;
            r["file_order"] = FileOrder;
            PropertySet.Tables["file"].Rows.Add(r);
            return id;
        }

        void AddSegmentInfo(ref DataSet PropertySet,string FileID,byte[] WritedBytes,int SegmentOrder)
        {
            DataRow r = PropertySet.Tables["segment"].NewRow();
            r["id"] = FileID + "_" + SegmentOrder;
            r["file_id"] = FileID;
            r["segment_length"] = WritedBytes.Length;
            if(this.PackageSysInfo.IsSigned==true)
            {
                //添加数字签名代码
            }
            r["segment_order"] = SegmentOrder;
            PropertySet.Tables["segment"].Rows.Add(r);

        }

        public long Package(string ToFileName) 
        {
            try
            {
                using (FileStream ts = new FileStream(ToFileName,
                    FileMode.OpenOrCreate,
                    FileAccess.ReadWrite,FileShare.ReadWrite))
                {
                    ts.SetLength(0);
                    long fileLength = 0;
                    DataSet PropertySet = SetPackageInfo();



                    for(int i=0;i<FileInfos.Length;i++)
                    {

                        EncoderProgress ee = new EncoderProgress();
                        ee.MainProgress = ((i + 1) / FileInfos.Length) * 100;
                        if (ee.MainProgress > 100) 
                        {
                            ee.MainProgress = 100;
                        }
                        ee.SubProgress = 0;
                        OnEncordering(ee);

                        string id = AddFileInfo(ref PropertySet, FileInfos[i],fileLength, i);
                        string FromFileName = FileInfos[i].FileName;                        
                        FileStream fs = new FileStream(FromFileName,
                            FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        int x = 0;
                        byte[] rb = new byte[BufferLenth];
                        int sOrder=0;
                        
                        int hereTotalLen=0;
                        while((x=fs.Read(rb,0,rb.Length))>0)
                        {
                            hereTotalLen +=x;
                            ee.SubProgress = (int)(hereTotalLen / fs.Length) * 100;
                            if (ee.SubProgress > 100) 
                            {
                                ee.SubProgress = 100;
                            }
                            OnEncordering(ee);

                            byte[] wb = null;
                            byte[] tempRb = null;
                            if (x == rb.Length)
                            {
                                tempRb = rb;
                            }
                            else
                            {
                                MemoryStream ms = new MemoryStream(rb, 0, x);
                                tempRb = ms.ToArray();
                            }

                            if (this.PackageSysInfo.IsCompressed == true && this.PackageSysInfo.IsEncried == true)
                            {
                                wb = CompressAndEncrptBytes(tempRb, this.PackageSysInfo.Password);
                            }
                            else if (this.PackageSysInfo.IsCompressed == true && this.PackageSysInfo.IsEncried == false)
                            {
                                wb = CompressBytes(tempRb);
                            }
                            else if (this.PackageSysInfo.IsCompressed == false && this.PackageSysInfo.IsEncried == true)
                            {
                                wb = EncryptBytes(tempRb, this.PackageSysInfo.Password);
                            }
                            else
                            {
                                wb = tempRb;
                            }

                            ts.Write(wb, 0, wb.Length);
                            AddSegmentInfo(ref PropertySet, id, wb, sOrder);
                            sOrder++;
                            fileLength += wb.Length;
                        }

                        fs.Close();
                    }

                    //处理标记文档
                    PropertySet.AcceptChanges();
                    byte[] propertyBytes = SerializeDataSet(PropertySet);
                    long PropertyStart = fileLength;
                    //Convert c = new Convert();
                    string Start= LongToAnyRadix(PropertyStart, RadixType.Ra32);
                    while (Start.Length < 8) 
                    {
                        Start = "0" + Start;
                    }
                    propertyBytes = CompressAndEncrptBytes(propertyBytes, "zhoucunjie");
                    ts.Write(propertyBytes, 0, propertyBytes.Length);
                    fileLength += propertyBytes.Length;
                    byte[] endBytes= System.Text.UTF8Encoding.UTF8.GetBytes(Start);                    
                    ts.Write(endBytes,0,endBytes.Length);
                    ts.Close();
                    return fileLength;
                }
            }
            catch 
            {
                return -1;
            }
        }

        public long Package(string ToFileName,string TopPath)
        {
            try
            {
                using (FileStream ts = new FileStream(ToFileName,
                    FileMode.OpenOrCreate,
                    FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    ts.SetLength(0);
                    long fileLength = 0;
                    DataSet PropertySet = SetPackageInfo();



                    for (int i = 0; i < FileInfos.Length; i++)
                    {

                        EncoderProgress ee = new EncoderProgress();
                        ee.MainProgress = ((i + 1) / FileInfos.Length) * 100;
                        if (ee.MainProgress > 100)
                        {
                            ee.MainProgress = 100;
                        }
                        ee.SubProgress = 0;
                        OnEncordering(ee);

                        string id = AddFileInfo(ref PropertySet, FileInfos[i], fileLength, i,TopPath);
                        string FromFileName = FileInfos[i].FileName;
                        FileStream fs = new FileStream(FromFileName,
                            FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        int x = 0;
                        byte[] rb = new byte[BufferLenth];
                        int sOrder = 0;

                        int hereTotalLen = 0;
                        while ((x = fs.Read(rb, 0, rb.Length)) > 0)
                        {
                            hereTotalLen += x;
                            ee.SubProgress = (int)(hereTotalLen / fs.Length) * 100;
                            if (ee.SubProgress > 100)
                            {
                                ee.SubProgress = 100;
                            }
                            OnEncordering(ee);

                            byte[] wb = null;
                            byte[] tempRb = null;
                            if (x == rb.Length)
                            {
                                tempRb = rb;
                            }
                            else
                            {
                                MemoryStream ms = new MemoryStream(rb, 0, x);
                                tempRb = ms.ToArray();
                            }

                            if (this.PackageSysInfo.IsCompressed == true && this.PackageSysInfo.IsEncried == true)
                            {
                                wb = CompressAndEncrptBytes(tempRb, this.PackageSysInfo.Password);
                            }
                            else if (this.PackageSysInfo.IsCompressed == true && this.PackageSysInfo.IsEncried == false)
                            {
                                wb = CompressBytes(tempRb);
                            }
                            else if (this.PackageSysInfo.IsCompressed == false && this.PackageSysInfo.IsEncried == true)
                            {
                                wb = EncryptBytes(tempRb, this.PackageSysInfo.Password);
                            }
                            else
                            {
                                wb = tempRb;
                            }

                            ts.Write(wb, 0, wb.Length);
                            AddSegmentInfo(ref PropertySet, id, wb, sOrder);
                            sOrder++;
                            fileLength += wb.Length;
                        }

                        fs.Close();
                    }

                    //处理标记文档
                    PropertySet.AcceptChanges();
                    byte[] propertyBytes = SerializeDataSet(PropertySet);
                    long PropertyStart = fileLength;
                    //Convert c = new Convert();
                    string Start = LongToAnyRadix(PropertyStart, RadixType.Ra32);
                    while (Start.Length < 8)
                    {
                        Start = "0" + Start;
                    }
                    propertyBytes = CompressAndEncrptBytes(propertyBytes, "zhoucunjie");
                    ts.Write(propertyBytes, 0, propertyBytes.Length);
                    fileLength += propertyBytes.Length;
                    byte[] endBytes = System.Text.UTF8Encoding.UTF8.GetBytes(Start);
                    ts.Write(endBytes, 0, endBytes.Length);
                    ts.Close();
                    return fileLength;
                }
            }
            catch
            {
                return -1;
            }
        }

        #region 整合
        //--
        public  byte[] CompressAndEncrptBytes(byte[] HereBytes, string Password)
        {
            if (HereBytes == null)
            {
                return new byte[0];
            }
            if (HereBytes.Length == 0)
            {
                return new byte[0];
            }

            byte[] compressData = CompressBytes(HereBytes);
            byte[] encryData = EncryptBytes(compressData, Password);
            return encryData;
        }
        //-----
        public string LongToAnyRadix(long LongValue, RadixType HereType)
        {
            string formatStr = HereType.ToString();
            AnyRadix provider = new AnyRadix();
            string messageStr = String.Format("{{0:{0}}}", formatStr);
            string str = String.Format(provider, messageStr, LongValue);
            return str;
        }
        //---
        public byte[] CompressBytes(byte[] UnCompressBytes)
        {
            if (UnCompressBytes == null)
            {
                return new byte[0];
            }

            if (UnCompressBytes.Length == 0)
            {
                return new byte[0];
            }

            using (MemoryStream fs = new MemoryStream())
            {
                Hashtable p = new Hashtable();
                p.Add("value", UnCompressBytes);
                using (GZipStream compressStream = new GZipStream(fs, CompressionMode.Compress))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(compressStream, p);
                }
                return fs.ToArray();
            }
        }
        //---
        public  byte[] EncryptBytes(byte[] UnEncryptedBytes, string Password)
        {
            if (UnEncryptedBytes == null)
            {
                return new byte[0];
            }

            if (UnEncryptedBytes.Length == 0)
            {
                return new byte[0];
            }

            using (MemoryStream fs = new MemoryStream())
            {
                Hashtable p = new Hashtable();
                p.Add("value", UnEncryptedBytes);
                string str = "~.`!,@>/;':?|}#*$%*&^)(-=_+{l.,'";
                str = Password + str;

                byte[] key = System.Text.Encoding.ASCII.GetBytes(str.Substring(0, 32));
                byte[] iv = System.Text.Encoding.ASCII.GetBytes(str.Substring(0, 16));


                using (CryptoStream csEncrypt = new CryptoStream(fs, new RijndaelManaged().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(csEncrypt, p);
                    csEncrypt.Flush();
                }
                return fs.ToArray();
            }
        }
        //---
        public  byte[] SerializeDataSet(DataSet HereDataSet)
        {
            if (HereDataSet == null)
            {
                return new byte[0];
            }
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, HereDataSet);

                return ms.ToArray();
            }
        }
        //---
        #region  任意进制辅助类

        public enum RadixType : int
        {
            Ra2 = 2,
            Ra3 = 3,
            Ra4 = 4,
            Ra5 = 5,
            Ra6 = 6,
            Ra7 = 7,
            Ra8 = 8,
            Ra9 = 9,
            Ra10 = 10,
            Ra11 = 11,
            Ra12 = 12,
            Ra13 = 13,
            Ra14 = 14,
            Ra15 = 15,
            Ra16 = 16,
            Ra17 = 17,
            Ra18 = 18,
            Ra19 = 19,
            Ra20 = 20,
            Ra21 = 21,
            Ra22 = 22,
            Ra23 = 23,
            Ra24 = 24,
            Ra25 = 25,
            Ra26 = 26,
            Ra27 = 27,
            Ra28 = 28,
            Ra29 = 29,
            Ra30 = 30,
            Ra31 = 31,
            Ra32 = 32,
            Ra33 = 33,
            Ra34 = 34,
            Ra35 = 35,
            Ra36 = 36,
        }

        public class AnyRadix : ICustomFormatter, IFormatProvider
        {
            // The value to be formatted is returned as a signed string 
            // of digits from the rDigits array. 
            const string radixCode = "Ra";
            private static char[] rDigits = {
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 
        'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 
        'U', 'V', 'W', 'X', 'Y', 'Z' };

            // This method returns an object that implements ICustomFormatter 
            // to do the formatting. 
            public object GetFormat(Type argType)
            {
                // Here, the same object (this) is returned, but it would 
                // be possible to return an object of a different type.
                if (argType == typeof(ICustomFormatter))
                    return this;
                else
                    return null;
            }

            // This method does the formatting only if it recognizes the 
            // format codes. 
            public string Format(string formatString,
                object argToBeFormatted, IFormatProvider provider)
            {
                // If no format string is provided or the format string cannot 
                // be handled, use IFormattable or standard string processing.
                if (formatString == null ||
                    !formatString.Trim().StartsWith(radixCode))
                {
                    if (argToBeFormatted is IFormattable)
                        return ((IFormattable)argToBeFormatted).
                            ToString(formatString, provider);
                    else
                        return argToBeFormatted.ToString();
                }

                // The formatting is handled here.
                int digitIndex = 0;
                long radix;
                long longToBeFormatted;
                long longPositive;
                char[] outDigits = new char[63];

                // Extract the radix from the format string.
                formatString = formatString.Replace(radixCode, "");
                try
                {
                    radix = System.Convert.ToInt64(formatString);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(String.Format(
                        "The radix \"{0}\" is invalid.",
                        formatString), ex);
                }

                // Verify that the radix is in the proper range.
                if (radix < 2 || radix > 36)
                    throw new ArgumentException(String.Format(
                        "The radix \"{0}\" is not in the range 2..36.",
                        formatString));

                // Verify that the argument can be converted to a long integer.
                try
                {
                    longToBeFormatted = long.Parse(argToBeFormatted.ToString());
                }
                catch
                {
                    return "ERROR";
                }

                // Extract the magnitude for conversion.
                longPositive = Math.Abs(longToBeFormatted);

                // Convert the magnitude to a digit string.
                for (digitIndex = 0; digitIndex <= 64; digitIndex++)
                {
                    if (longPositive == 0) break;

                    outDigits[outDigits.Length - digitIndex - 1] =
                        rDigits[longPositive % radix];
                    longPositive /= radix;
                }

                // Add a minus sign if the argument is negative.
                if (longToBeFormatted < 0)
                    outDigits[outDigits.Length - digitIndex++ - 1] =
                        '-';

                return new string(outDigits,
                    outDigits.Length - digitIndex, digitIndex);
            }
        }

        #endregion  任意进制辅助类

        //--
        public int UnPackage(string FromFileName,string ToDirectory) 
        {
            try
            {
                using (FileStream fs = new FileStream(FromFileName,
                            FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    if(!Directory.Exists(ToDirectory))
                    {
                        Directory.CreateDirectory(ToDirectory);
                    }

                    int FileCounts = 0;

                    SystemInfo packageInfo = null;
                    FileInfo[] fileInfos = null;
                                        
                    DataSet packageSet = GetInfoData(FromFileName, out packageInfo, out fileInfos);
                    this.PackageSysInfo = packageInfo;
                    this.FileInfos = fileInfos;

                    for (int i = 0; i < fileInfos.Length; i++)
                    {
                        //System.Windows.Forms.MessageBox.Show(fileInfos.Length.ToString()+"\r\n"+
                        //    i.ToString()
                        //    );

                        try
                        {


                            EncoderProgress ee = new EncoderProgress();
                            ee.MainProgress = ((i + 1) / FileInfos.Length) * 100;
                            if (ee.MainProgress > 100)
                            {
                                ee.MainProgress = 100;
                            }
                            ee.SubProgress = 0;
                            OnEncordering(ee);

                            string fileId = fileInfos[i].FileID;
                            string fileName = fileInfos[i].FileName;
                            long start = fileInfos[i].FileStart;
                            fs.Position = start;

                            string path = ToDirectory.TrimEnd(new char[] { '\\' }) + "\\" + fileName;

                            int kkk = path.LastIndexOf("\\");
                            if(kkk>=0)
                            {
                                string dir = path.Substring(0, kkk);
                                bool exist = Directory.Exists(dir);
                                if(!exist)
                                {
                                    Directory.CreateDirectory(dir);
                                }
                            }

                            FileStream ts = new FileStream(path,
                                FileMode.OpenOrCreate,
                                FileAccess.ReadWrite, FileShare.ReadWrite);
                            ts.SetLength(0);

                            DataRow[] rs = packageSet.Tables[2].Select("file_id = " + "'" + fileId + "'", "segment_order");

                            int hereTotalLen = 0;
                            int TotalLen = 0;
                            for (int j = 0; j < rs.Length; j++)
                            {
                                int length = int.Parse(rs[j]["segment_length"].ToString());
                                TotalLen += length;
                            }

                            for (int j = 0; j < rs.Length; j++)
                            {
                                int length = int.Parse(rs[j]["segment_length"].ToString());
                                byte[] rb = new byte[length];
                                int x = fs.Read(rb, 0, rb.Length);

                                hereTotalLen += x;
                                ee.SubProgress = (int)(hereTotalLen / TotalLen) * 100;
                                if (ee.SubProgress > 100)
                                {
                                    ee.SubProgress = 100;
                                }
                                OnEncordering(ee);

                                byte[] wb = null;
                                byte[] tempRb = null;
                                if (x == rb.Length)
                                {
                                    tempRb = rb;
                                }
                                else
                                {
                                    MemoryStream ms = new MemoryStream(rb, 0, x);
                                    tempRb = ms.ToArray();
                                }

                                if (this.PackageSysInfo.IsSigned)
                                {
                                    bool trueSigned = false;
                                    //添加验证签名的代码

                                    if (!trueSigned)
                                    {
                                        return -2;
                                    }
                                }

                                if (this.PackageSysInfo.IsCompressed == true && this.PackageSysInfo.IsEncried == true)
                                {
                                    wb = DecrptAndDeCompressBytes(tempRb, this.PackageSysInfo.Password);
                                }
                                else if (this.PackageSysInfo.IsCompressed == true && this.PackageSysInfo.IsEncried == false)
                                {
                                    wb = DeCompressBytes(tempRb);
                                }
                                else if (this.PackageSysInfo.IsCompressed == false && this.PackageSysInfo.IsEncried == true)
                                {
                                    wb = DecryptBytes(tempRb, this.PackageSysInfo.Password);
                                }
                                else
                                {
                                    wb = tempRb;
                                }

                                ts.Write(wb, 0, wb.Length);
                            }

                            ts.Close();

                            try
                            {
                                System.IO.File.SetLastWriteTime(path, DateTime.Parse(fileInfos[i].FileLastTime));
                            }
                            catch { }

                            FileCounts++;
                        }
                        catch 
                        {
                            continue;
                        }

                        }//for
                        fs.Close();
                        return FileCounts;
                    
                }                 
            }
            catch(Exception ee)
            {
                System.Windows.Forms.MessageBox.Show(ee.ToString()
                          );
                return -1;
            }
        }
        //
        public  byte[] DecrptAndDeCompressBytes(byte[] HereBytes, string Password)
        {
            if (HereBytes == null)
            {
                return new byte[0];
            }
            if (HereBytes.Length == 0)
            {
                return new byte[0];
            }
            byte[] comBytes = DecryptBytes(HereBytes, Password);
            byte[] returnBytes = DeCompressBytes(comBytes);
            return returnBytes;
        }
        //
        public  byte[] DeCompressBytes(byte[] CompressBytes)
        {
            if (CompressBytes == null)
            {
                return new byte[0];
            }

            if (CompressBytes.Length == 0)
            {
                return new byte[0];
            }

            using (MemoryStream fs = new MemoryStream(CompressBytes))
            {
                using (GZipStream decompressStream = new GZipStream(fs, CompressionMode.Decompress))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    Hashtable tm = (bf.Deserialize(decompressStream) as Hashtable);
                    byte[] Value = tm["value"] as byte[];
                    return Value;
                }
            }
        }
        //
        public  byte[] DecryptBytes(byte[] EncryptedBytes, string Password)
        {
            if (EncryptedBytes == null)
            {
                return new byte[0];
            }

            if (EncryptedBytes.Length == 0)
            {
                return new byte[0];
            }

            using (MemoryStream fs = new MemoryStream(EncryptedBytes))
            {
                string str = "~.`!,@>/;':?|}#*$%*&^)(-=_+{l.,'";
                str = Password + str;
                fs.Position = 0;
                byte[] key = System.Text.Encoding.ASCII.GetBytes(str.Substring(0, 32));
                byte[] iv = System.Text.Encoding.ASCII.GetBytes(str.Substring(0, 16));

                using (CryptoStream csDncrypt = new CryptoStream(fs, new RijndaelManaged().CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    Hashtable p = bf.Deserialize(csDncrypt) as Hashtable;
                    byte[] v = p["value"] as byte[];
                    return v;
                }
            }
        }

        public DataSet GetInfoData(string FromFileName,out SystemInfo PackageInfo,out FileInfo[] FileInfos) 
        {
            try
            {
                using (FileStream fs = new FileStream(FromFileName,
            FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    fs.Position = fs.Length - 8;
                    byte[] b = new byte[8];
                    fs.Read(b, 0, 8);

                    string PropertyStart = System.Text.UTF8Encoding.UTF8.GetString(b);
                    //Convert c = new Convert();
                    long Start = AnyRadixToLong(PropertyStart, RadixType.Ra32);
                    fs.Position = Start;
                    long length = fs.Length - Start - 8;
                    byte[] encriedBytes = new byte[length];
                    fs.Read(encriedBytes, 0, encriedBytes.Length);
                    byte[] propertyBytes = DecrptAndDeCompressBytes(encriedBytes, "zhoucunjie");
                    DataSet d = DeSerializeDataSet(propertyBytes);
                    PackageInfo = GetackageInfo(d) ;
                    FileInfos = GetFileInfos(d);

                    return d;
                }   
            }
            catch 
            {
                PackageInfo =null;
                FileInfos = null;
                return null;
            }
        
        }
        //
        public long AnyRadixToLong(string AnyRadix, RadixType HereType)
        {
            long x = (long)HereType;
            string str = AnyRadix.ToUpper();
            long l = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                long w = 0;
                string s = str.Substring(i, 1);
                byte b = System.Text.Encoding.ASCII.GetBytes(s)[0];
                long t = long.Parse(b.ToString());

                if (t >= 65 && t <= 90)
                {
                    w = t - 65 + 10;
                }
                else
                {
                    w = long.Parse(s);
                }


                if (w >= x)
                {
                    l = -1;
                    break;
                }
                else
                {
                    l += w * (long)Math.Pow((double)x, (double)(str.Length - 1 - i));
                }


            }

            return l;
        }
        //
        public  DataSet DeSerializeDataSet(byte[] HereBytes)
        {
            if (HereBytes == null)
            {
                return null;
            }
            if (HereBytes.Length == 0)
            {
                return null;
            }
            using (MemoryStream ms = new MemoryStream(HereBytes))
            {
                BinaryFormatter bf = new BinaryFormatter();
                DataSet d = bf.Deserialize(ms) as DataSet;
                return d;
            }
        }
        //
        #endregion

        SystemInfo GetackageInfo(DataSet InfoDataSet) 
        {
            SystemInfo PackageInfo = new SystemInfo();
            PackageInfo.Guid = GetProperty(InfoDataSet,
                0, "property_name", "guid", "property_value").ToString();

            PackageInfo.UpdateGuid = GetProperty(InfoDataSet,
                0, "property_name", "update_guid", "property_value").ToString();

            PackageInfo.Vision = GetProperty(InfoDataSet,
                0, "property_name", "vision", "property_value").ToString();

            PackageInfo.UserName = GetProperty(InfoDataSet,
                0, "property_name", "username", "property_value").ToString();

            PackageInfo.Password = GetProperty(InfoDataSet,
                0, "property_name", "password", "property_value").ToString();

            PackageInfo.DateTime = GetProperty(InfoDataSet,
                0, "property_name", "datetime", "property_value").ToString();

            PackageInfo.InvalidDateTime = GetProperty(InfoDataSet,
                0, "property_name", "invalid_datetime", "property_value").ToString();

            PackageInfo.IsCompressed = bool.Parse(GetProperty(InfoDataSet,
                0, "property_name", "iscompressed", "property_value").ToString());

            PackageInfo.IsEncried = bool.Parse(GetProperty(InfoDataSet,
                0, "property_name", "isencried", "property_value").ToString());

            PackageInfo.IsReadOnly = bool.Parse(GetProperty(InfoDataSet,
                0, "property_name", "isreadonly", "property_value").ToString());

            PackageInfo.IsSigned = bool.Parse(GetProperty(InfoDataSet,
                0, "property_name", "issigned", "property_value").ToString());

            PackageInfo.PrivateKey = InfoDataSet.Tables[3].Rows[0][0].ToString();
            PackageInfo.PublicKey = InfoDataSet.Tables[3].Rows[0][1].ToString();

            return PackageInfo;
        }

        FileInfo[] GetFileInfos(DataSet InfoDataSet) 
        {
            FileInfo[] infos = new FileInfo[InfoDataSet.Tables[1].Rows.Count];
            for (int i = 0; i < InfoDataSet.Tables[1].Rows.Count;i++ )
            {
                infos[i] = new FileInfo();
                infos[i].FileID = InfoDataSet.Tables[1].Rows[i]["id"].ToString();
                infos[i].FileName = InfoDataSet.Tables[1].Rows[i]["file_name"].ToString();
                infos[i].FileStart = long.Parse(InfoDataSet.Tables[1].Rows[i]["file_start"].ToString());
                infos[i].FileType = InfoDataSet.Tables[1].Rows[i]["file_type"].ToString();
                infos[i].FileLastTime = InfoDataSet.Tables[1].Rows[i]["file_lasttime"].ToString();
                infos[i].ActionType = InfoDataSet.Tables[1].Rows[i]["action_type"].ToString();
                infos[i].ActionDesc = InfoDataSet.Tables[1].Rows[i]["action_desc"].ToString();
                infos[i].BeforeActionEvent = InfoDataSet.Tables[1].Rows[i]["before_action_event"].ToString();
                infos[i].AfterActionEvent = InfoDataSet.Tables[1].Rows[i]["after_action_event"].ToString();
                infos[i].FileOrder = int.Parse(InfoDataSet.Tables[1].Rows[i]["file_order"].ToString());
            }

            return infos;
        }
    }


    public class SystemInfo 
    {
        public SystemInfo() 
        {

        }

        #region 属性
        string _PrivateKey = "";
        public string PrivateKey 
        {
            get 
            {
                return _PrivateKey;
            }
            set 
            {
                _PrivateKey = value;
            }
        }

        string _PublicKey = "";
        public string PublicKey 
        {
            get 
            {
                return _PublicKey;
            }
            set 
            {
                _PublicKey = value;
            }
        }

        bool _IsReadOnly = false;
        public bool IsReadOnly 
        {
            get
            {
                return _IsReadOnly;
            }
            set 
            {
                _IsReadOnly = value;
            }
        }

        bool _IsSigned = false;
        public bool IsSigned 
        {
            get 
            {
                return _IsSigned;
            }
            set 
            {
                _IsSigned = value;
            }
        }

        bool _IsCompressed = true;
        public bool IsCompressed 
        {
            get 
            {
                return _IsCompressed;
            }
            set 
            {
                _IsCompressed = value;
            }
        }

        bool _IsEncried = false;
        public bool IsEncried 
        {
            get 
            {
                return _IsEncried;
            }
            set 
            {
                _IsEncried = value;
            }
        }

        string _UserName = "";
        public string UserName
        {
            get 
            {
                return _UserName;
            }
            set 
            {
                _UserName = value;
            }
        }

        string _Password = "";
        public string Password 
        {
            get { return _Password; }
            set { _Password = value; }
        }

        string _Vision = "";
        public string Vision 
        {
            get { return _Vision; }
            set { _Vision = value; }
        }

        string _DateTime = "";
        public string DateTime 
        {
            get { return _DateTime; }
            set { _DateTime = value; }
        }

        string _InvalidDateTime = "";
        public string InvalidDateTime 
        {
            get 
            {
                return _InvalidDateTime;
            }
            set 
            {
                _InvalidDateTime = value;
            }
        }

        string _Guid = "";
        public string Guid 
        {
            get { return _Guid; }
            set { _Guid = value; }
        }

        string _UpdateGuid = "";
        public string UpdateGuid 
        {
            get { return _UpdateGuid; }
            set { _UpdateGuid = value; }
        }
        #endregion
    }

    public class FileInfo
    {
        public FileInfo()
        {

        }
        #region 属性
        string _FileID = "";
        public string FileID 
        {
            get 
            {
                return _FileID;
            }
            set 
            {
                _FileID = value;
            }
        }

        string _FileName = "";
        public string FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                _FileName = value;
            }
        }

        long _FileStart = 0;
        public long FileStart
        {
            get
            {
                return _FileStart;
            }
            set
            {
                _FileStart = value;
            }
        }

        string _FileType = "";
        public string FileType
        {
            get
            {
                return _FileType;
            }
            set
            {
                _FileType = value;
            }
        }

        string _FileLastTime = DateTime.Now.ToString();
        public string FileLastTime
        {
            get
            {
                return _FileLastTime;
            }
            set
            {
                _FileLastTime = value;
            }
        }

        string _ActionType = "";
        public string ActionType
        {
            get
            {
                return _ActionType;
            }
            set
            {
                _ActionType = value;
            }
        }

        string _ActionDesc = "";
        public string ActionDesc
        {
            get
            {
                return _ActionDesc;
            }
            set
            {
                _ActionDesc = value;
            }
        }

        string _BeforeActionEvent = "";
        public string BeforeActionEvent
        {
            get
            {
                return _BeforeActionEvent;
            }
            set
            {
                _BeforeActionEvent = value;
            }
        }

        string _AfterActionEvent = "";
        public string AfterActionEvent
        {
            get
            {
                return _AfterActionEvent;
            }
            set
            {
                _AfterActionEvent = value;
            }
        }

        int _FileOrder = 0;
        public int FileOrder 
        {
            get 
            {
                return _FileOrder;
            }
            set 
            {
                _FileOrder = value;
            }
        }
        #endregion
    }
}
