
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace IpUtils
{
	public class IpDetail
	{
		public String Ret { get; set; }

		public String Start { get; set; }

		public String End { get; set; }

		public String Country { get; set; }

		public String Province { get; set; }

		public String City { get; set; }

		public String District { get; set; }

		public String Isp { get; set; }

		public String Type { get; set; }

		public String Desc { get; set; }
	}

	public class IpHelper
	{
		/// <summary>
		/// 获取IP地址的详细信息，调用的借口为
		/// http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=json&ip={ip}
		/// </summary>
		/// <param name="ipAddress">请求分析得IP地址</param>
		/// <param name="sourceEncoding">服务器返回的编码类型</param>
		/// <returns>IpUtils.IpDetail</returns>
		public static IpDetail Get(String ipAddress,Encoding sourceEncoding)
		{
			String ip = string.Empty;
			if(sourceEncoding==null)
				sourceEncoding = Encoding.UTF8;
			using (var receiveStream = WebRequest.Create("http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=json&ip="+ipAddress).GetResponse().GetResponseStream())
			{
				using (var sr = new StreamReader(receiveStream, sourceEncoding))
				{
					var readbuffer = new char[256];
					int n = sr.Read(readbuffer, 0, readbuffer.Length);
					int realLen = 0;
					while (n > 0)
					{
						realLen = n;
						n = sr.Read(readbuffer, 0, readbuffer.Length);
					}
					ip = sourceEncoding.GetString(sourceEncoding.GetBytes(readbuffer, 0, realLen));
				}
			}
			return  !string.IsNullOrEmpty(ip)?new JavaScriptSerializer().Deserialize<IpDetail>(ip):null;
		}
	}

	public class EncodingHelper
	{
		public static String GetString(Encoding source, Encoding dest, String soureStr)
		{
			return dest.GetString(Encoding.Convert(source, dest, source.GetBytes(soureStr)));
		}

		public static String GetString(Encoding source, Encoding dest, Char[] soureCharArr, int offset, int len)
		{
			return dest.GetString(Encoding.Convert(source, dest, source.GetBytes(soureCharArr, offset, len)));
		}
	}
}
