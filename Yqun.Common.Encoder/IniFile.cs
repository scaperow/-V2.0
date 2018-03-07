using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Yqun.Common.Encoder
{
    public class IniFile
    {	
        public string path;

		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section,string key,string val,string filePath);
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section,string key,string def,StringBuilder retVal,int size,string filePath);

		/// <summary>
        /// IniFile Constructor.
		/// </summary>
		/// <param name="INIPath"></param>
		public IniFile(string IniPath)
		{
			path = IniPath;
		}
		/// <summary>
		/// 写数据到指定.ini文件
		/// </summary>
		/// <param name="Section"></param>
		/// Section 的名称
		/// <param name="Key"></param>
		/// Key 的名称
		/// <param name="Value"></param>
		/// Value 的内容
		public void IniWriteValue(string Section,string Key,string Value)
		{
			WritePrivateProfileString(Section,Key,Value,this.path);
		}
		
		/// <summary>
		/// Read Data Value From the Ini File
		/// </summary>
		/// <param name="Section"></param>
		/// <param name="Key"></param>
		/// <param name="Path"></param>
		/// <returns></returns>
		public string IniReadValue(string Section,string Key)
		{
			StringBuilder temp = new StringBuilder(1024);
			int i = GetPrivateProfileString(Section,Key,"",temp,1024,this.path);
			return temp.ToString();
		}
    }
}
