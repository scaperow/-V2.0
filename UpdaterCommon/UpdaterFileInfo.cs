using System;
using System.Collections.Generic;
using System.Text;

namespace UpdaterCommon
{
    /// <summary>
    /// 数据库表中的提醒消息信息
    /// </summary>
    [Serializable]
    public class UpdaterFileInfo : IComparable
    { 
        /// <summary>
        /// 更新文件的名字
        /// </summary>
        string m_FileName;
        public string FileName
        {
            get
            {
                return m_FileName;
            }
            set
            {
                m_FileName = value;
            }
        }

        /// <summary>
        /// 更新文件的时间
        /// </summary>
        string m_FileDate;
        public string FileDate
        {
            get
            {
                return m_FileDate;
            }
            set
            {
                m_FileDate = value;
            }
        }

        /// <summary>
        /// 更新文件的数据
        /// </summary>
        byte[] m_FileData = new byte[0];
        public byte[] FileData
        {
            get
            {
                return m_FileData;
            }
            set
            {
                m_FileData = value;
            }
        }

        /// <summary>
        /// 更新文件的版本
        /// </summary>
        string m_FileVersion;
        public string FileVersion
        {
            get
            {
                return m_FileVersion;
            }
            set
            {
                m_FileVersion = value;
            }
        }

        public override bool Equals(object obj)
        {
            UpdaterFileInfo Info = obj as UpdaterFileInfo;
            if (Info != null)
                return string.Compare(this.FileName, Info.FileName) == 0;
            return false;
        }

        public override string ToString()
        {
            return this.FileName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IComparable 成员

        public int CompareTo(object obj)
        {
            UpdaterFileInfo Info = obj as UpdaterFileInfo;
            if (Info != null)
                return string.Compare(this.FileVersion, Info.FileVersion);
            return -1;
        }

        #endregion
    }
}
