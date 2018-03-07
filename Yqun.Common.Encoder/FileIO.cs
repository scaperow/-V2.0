using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//
using System.Diagnostics;

namespace Yqun.Common.Encoder
{
    public class FileIO
    {
        public FileIO() 
        {
        
        }
                
        /// <summary>
        /// ������������дΪһ�������ļ�
        /// </summary>
        /// <param name="ReadBytes">����������</param>
        /// <param name="FileName">�ļ�����</param>
        /// <returns>���������ݵĳ���</returns>
        public static long WriteFile(byte[] ReadBytes, string FileName)
        {            
            try 
            {
                using (System.IO.FileStream s = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    try
                    {
                        s.SetLength(0);
                        s.Write(ReadBytes, 0, ReadBytes.Length);
                        s.Close();
                        return ReadBytes.Length;
                    }
                    catch 
                    {
                        s.Close();
                        return -1;
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// ���ַ��������ض����뼯����Ϊһ���������ļ�
        /// </summary>
        /// <param name="ReadString">�ַ���</param>
        /// <param name="CharSet">���뼯</param>
        /// <param name="FileName">�ļ���</param>
        /// <returns>�ļ�����</returns>
        public static long WriteFile(string ReadString, string CharSet, string FileName)
        {
            try
            {
                //����
                byte[] str = System.Text.Encoding.GetEncoding(CharSet).GetBytes(ReadString);
                return WriteFile(str, FileName);
            }
            catch (Exception)
            {
                return -1;
            }
        }
        
        /// <summary>
        /// �����������ݸ��ӵ��ļ�ĩβ
        /// </summary>
        /// <param name="ToAppendBytes">����������</param>
        /// <param name="FileName">�ļ���</param>
        /// <returns>�˴ζ��������ݳ���</returns>
        public static int AppendFile(byte[] ToAppendBytes, string FileName)
        {
            try
            {
                using (System.IO.FileStream s = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    //
                    try
                    {
                        s.Position = s.Length;
                        s.Write(ToAppendBytes, 0, ToAppendBytes.Length);
                        s.Close();
                        return ToAppendBytes.Length;
                    }
                    catch 
                    {
                        s.Close();
                        return -1;
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }            
        }

        /// <summary>
        /// ���ַ��������ض��ı��뼯���ӵ��ļ�
        /// </summary>
        /// <param name="ToAppendString">�ַ���</param>
        /// <param name="CharSet">���뼯</param>
        /// <param name="FileName">�ļ���</param>
        /// <returns>���ӵ��ֽ����鳤��</returns>
        public static long AppendFile(string ToAppendString, string CharSet, string FileName)
        {
            try
            {
                //����
                byte[] str = System.Text.Encoding.GetEncoding(CharSet).GetBytes(ToAppendString);
                return AppendFile(str, FileName);
            }
            catch (Exception)
            {
                return -1;
            }
        }
                
        /// <summary>
        /// ���ļ���ȡ������
        /// </summary>
        /// <param name="ReadFileName">�ļ���</param>
        /// <returns>����������</returns>
        public static byte[] ReadFileToBytes(string ReadFileName)
        {
            try
            {
                using (System.IO.FileStream s = new FileStream(ReadFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    try
                    {
                        byte[] data = new byte[s.Length];
                        s.Read(data, 0, (int)data.Length);
                        s.Close();

                        return data;
                    }
                    catch 
                    {
                        s.Close();
                        return new byte[0];
                    }
                }
                
            
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }

        /// <summary>
        /// ���ļ���ȡ���ַ���
        /// </summary>
        /// <param name="ReadFileName">�ļ���</param>
        /// <param name="CharSet">���뼯</param>
        /// <returns>�ַ���</returns>
        public static string ReadFileToString(string ReadFileName, string CharSet)
        {
            try
            {
                byte[] data = ReadFileToBytes(ReadFileName);
                string str = System.Text.Encoding.GetEncoding(CharSet).GetString(data);

                return str;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// ���ļ��ض�λ�ö�ȡ�ض����ȵ�����
        /// </summary>
        /// <param name="ReadFileName">�ļ���</param>
        /// <param name="StartPosition">��ȡ��ʼλ��</param>
        /// <param name="Lenth">��ȡ����</param>
        /// <returns>����������</returns>
        public static byte[] ReadFileToBytes(string ReadFileName, int StartPosition, int Lenth)
        {
            try
            {
                using (System.IO.FileStream s = new FileStream(ReadFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    try
                    {
                        byte[] data = new byte[Lenth];
                        s.Position = StartPosition;
                        int len = s.Read(data, 0, Lenth);
                        s.Close();

                        MemoryStream ms = new MemoryStream(data, 0, len);
                        byte[] b = ms.ToArray();



                        ms.Close();
                        return b;
                    }
                    catch 
                    {
                        s.Close();
                        return new byte[0]; 
                    }
                }
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }
        public static byte[] ReadFileToBytes(string ReadFileName, long StartPosition, int Lenth)
        {
            try
            {
                using (System.IO.FileStream s = new FileStream(ReadFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    try
                    {
                        byte[] data = new byte[Lenth];
                        s.Position = StartPosition;
                        int len = s.Read(data, 0, Lenth);
                        s.Close();

                        MemoryStream ms = new MemoryStream(data, 0, len);
                        byte[] b = ms.ToArray();


                        ms.Close();

                        return b;
                    }
                    catch 
                    {

                        s.Close();
                        return new byte[0];
                    }
                }
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }

        /// <summary>
        /// ���ļ��ض�λ�ö�ȡ�ض����ȵ����ݲ������ض��ı��뼯ת��Ϊ�ַ���
        /// </summary>
        /// <param name="ReadFileName">�ļ���</param>
        /// <param name="CharSet">���뼯</param>
        /// <param name="StartPosition">��ʼλ��</param>
        /// <param name="Lenth">��ȡ����</param>
        /// <returns>�ض����뼯���ַ���</returns>
        public static string ReadFileToString(string ReadFileName, string CharSet, int StartPosition, int Lenth)
        {
            try
            {
                byte[] data = ReadFileToBytes(ReadFileName, StartPosition,Lenth);
                string str = System.Text.Encoding.GetEncoding(CharSet).GetString(data);

                return str;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// ���ļ�����Base64�ַ���
        /// </summary>
        /// <param name="ReadFileName">�ļ�����</param>
        /// <returns>Base64�ַ���</returns>
        public static string FileToBase64(string ReadFileName) 
        {
            byte[] b = ReadFileToBytes(ReadFileName);
            string s = System.Convert.ToBase64String(b);
            return s;
        }


        /// <summary>
        /// ��Base64�����Ϊ�ļ�
        /// </summary>
        /// <param name="ToWriteFile">�ļ���</param>
        /// <param name="Base64String">Base64��</param>
        /// <returns>�ļ�����</returns>
        public static long WriteBase64ToFile(string ToWriteFile,string Base64String) 
        {
            byte[] b = System.Convert.FromBase64String(Base64String);
            long l = WriteFile(b, ToWriteFile);
            return l;
        }
    }
}
