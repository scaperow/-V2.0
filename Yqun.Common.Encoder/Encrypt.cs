using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;


namespace Yqun.Common.Encoder
{
    #region 加密类

    public class Encrypt
    {
        public Encrypt()
        {}

        /// <summary>
        /// 将二进制数组加密
        /// </summary>
        /// <param name="UnEncryptedBytes">未加密的二进制数组</param>
        /// <param name="Password">密码</param>
        /// <returns>加密后的二进制数组</returns>
        public static byte[] EncryptBytes(byte[] UnEncryptedBytes, string Password)
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
                string p = "`!@#$%^&*~!@#$%^&*()_+|~!@#$%^&*(";
                if (Password.Length < p.Length)
                {
                    Password = Password + p.Substring(Password.Length + 1);
                }
                string s = Password.Substring(0, 32);
                string i = Password.Substring(0, 16);

                RijndaelManaged myRijndael = new RijndaelManaged();
                byte[] key = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(s);
                byte[] IV = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(i);
                ICryptoTransform encryptor = myRijndael.CreateEncryptor(key, IV);
                MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                csEncrypt.Write(UnEncryptedBytes, 0, UnEncryptedBytes.Length);
                csEncrypt.FlushFinalBlock();

                byte[] encrypted = msEncrypt.ToArray();
                return encrypted;
            }
        }

        /// <summary>
        /// 将加密后的二进制数组解密
        /// </summary>
        /// <param name="EncryptedBytes">加密后的二进制数组</param>
        /// <param name="Password">密码</param>
        /// <returns>解密后的二进制数组</returns>
        public static byte[] DecryptBytes(byte[] EncryptedBytes, string Password)
        {
            if (EncryptedBytes == null)
            {
                return new byte[0];
            }

            if (EncryptedBytes.Length == 0)
            {
                return new byte[0];
            }

            using (MemoryStream fs = new MemoryStream())
            {
                string p = "`!@#$%^&*~!@#$%^&*()_+|~!@#$%^&*(";
                if (Password.Length < p.Length)
                {
                    Password = Password + p.Substring(Password.Length + 1);
                }
                string s = Password.Substring(0, 32);
                string i = Password.Substring(0, 16);

                RijndaelManaged myRijndael = new RijndaelManaged();
                byte[] key = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(s);
                byte[] IV = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(i);

                ICryptoTransform decryptor = myRijndael.CreateDecryptor(key, IV);
                MemoryStream msDecrypt = new MemoryStream(EncryptedBytes);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                byte[] fromEncrypt = new byte[EncryptedBytes.Length];
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

                return fromEncrypt;
            }
        }

        /// <summary>
        /// 加密文件
        /// </summary>
        /// <param name="FromFileName">未加密的文件</param>
        /// <param name="ToFileName">加密后保存的文件</param>
        /// <param name="Password">密码</param>
        /// <returns>成功为1，失败为-1</returns>
        public static int EncryptFile(string FromFileName, string ToFileName, string Password)
        {
            try
            {
                using (FileStream fs = new FileStream(FromFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    FileStream ts = new FileStream(ToFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

                    string str = "~.`!,@>/;':?|}#*$%*&^)(-=_+{l.,'";
                    str = Password + str;

                    byte[] key = System.Text.Encoding.ASCII.GetBytes(str.Substring(0, 32));
                    byte[] iv = System.Text.Encoding.ASCII.GetBytes(str.Substring(0, 16));


                    using (CryptoStream csEncrypt = new CryptoStream(ts, new RijndaelManaged().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                    {
                        byte[] b = new byte[1024];
                        int x = 0;
                        while ((x = fs.Read(b, 0, b.Length)) > 0)
                        {
                            csEncrypt.Write(b, 0, x);
                            csEncrypt.Flush();
                        }
                        csEncrypt.Close();
                        fs.Close();
                        ts.Close();

                    }


                }
                return 1;
            }
            catch
            {
                return -1;
            }

        }

        /// <summary>
        /// 解密文件
        /// </summary>
        /// <param name="FromFileName">加密后的文件</param>
        /// <param name="ToFileName">解密后保存的文件</param>
        /// <param name="Password">密码</param>
        /// <returns>成功为1，失败为-1</returns>
        public static int DecryptFile(string FromFileName, string ToFileName, string Password)
        {
            try
            {
                using (FileStream fs = new FileStream(FromFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    FileStream ts = new FileStream(ToFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

                    string str = "~.`!,@>/;':?|}#*$%*&^)(-=_+{l.,'";
                    str = Password + str;

                    byte[] key = System.Text.Encoding.ASCII.GetBytes(str.Substring(0, 32));
                    byte[] iv = System.Text.Encoding.ASCII.GetBytes(str.Substring(0, 16));


                    using (CryptoStream csEncrypt = new CryptoStream(fs, new RijndaelManaged().CreateDecryptor(key, iv), CryptoStreamMode.Read))
                    {
                        byte[] b = new byte[1024];
                        int x = 0;
                        while ((x = csEncrypt.Read(b, 0, b.Length)) > 0)
                        {
                            ts.Write(b, 0, x);
                            ts.Flush();
                        }
                        csEncrypt.Close();
                        fs.Close();
                        ts.Close();

                    }


                }
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 计算二进制数组的哈希
        /// </summary>
        /// <param name="Bytes">二进制数组</param>
        /// <returns>哈希</returns>
        public static byte[] ComputeHash(byte[] Bytes)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] result = sha.ComputeHash(Bytes);
            return result;
        }

        //计算哈西并转化成Base64
        public static string ComputeHashToBase64(byte[] UnComputeBytes)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] result = sha.ComputeHash(UnComputeBytes);
            string rest = System.Convert.ToBase64String(result);
            return rest;
        }

        //计算哈西并转化成Base64
        public static string ComputeHashToBase64(string UnComputeString)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] unbytes = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(UnComputeString);
            byte[] result = sha.ComputeHash(unbytes);
            string rest = System.Convert.ToBase64String(result);
            return rest;
        }
    }

    #endregion

    public class EncryptSerivce
    {
        static SymmCrypto objCrypto = new SymmCrypto(SymmCrypto.SymmProvEnum.Rijndael);
        public static string Encrypt(string Data)
        {
            return objCrypto.Encrypting(Data, "LXyqsoft");
        }

        public static string Dencrypt(string Data)
        {
            return objCrypto.Decrypting(Data, "LXyqsoft");
        }
    }

    /// <summary>
    /// 加密类
    /// </summary>
    public class SymmCrypto
    {
        /// <remarks>
        /// .Net支持的所有加密类型
        /// </remarks>
        public enum SymmProvEnum : int
        {
            DES, RC2, Rijndael
        }

        private SymmetricAlgorithm mobjCryptoService;

        /// <remarks>
        /// 构造一个.Net支持的加密类
        /// </remarks>
        public SymmCrypto(SymmProvEnum NetSelected)
        {
            switch (NetSelected)
            {
                case SymmProvEnum.DES:
                    mobjCryptoService = new DESCryptoServiceProvider();
                    break;
                case SymmProvEnum.RC2:
                    mobjCryptoService = new RC2CryptoServiceProvider();
                    break;
                case SymmProvEnum.Rijndael:
                    mobjCryptoService = new RijndaelManaged();
                    break;
            }
        }

        /// <remarks>
        /// 构造一个.Net支持的加密类
        /// </remarks>
        public SymmCrypto(SymmetricAlgorithm ServiceProvider)
        {
            mobjCryptoService = ServiceProvider;
        }

        /// <remarks>
        /// 获得一个合法的关键字
        /// </remarks>
        private byte[] GetLegalKey(string Key)
        {
            string sTemp;
            if (mobjCryptoService.LegalKeySizes.Length > 0)
            {
                int lessSize = 0, moreSize = mobjCryptoService.LegalKeySizes[0].MinSize;
                while (Key.Length * 8 > moreSize)
                {
                    lessSize = moreSize;
                    moreSize += mobjCryptoService.LegalKeySizes[0].SkipSize;
                }
                sTemp = Key.PadRight(moreSize / 8, ' ');
            }
            else
                sTemp = Key;

            return Encoding.UTF8.GetBytes(sTemp);
        }

        public string Encrypting(string Source, string Key)
        {
            byte[] bytIn = Encoding.UTF8.GetBytes(Source);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            byte[] bytKey = GetLegalKey(Key);

            mobjCryptoService.Key = bytKey;
            mobjCryptoService.IV = bytKey;

            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();

            byte[] bytOut = ms.GetBuffer();
            int i = 0;
            for (i = bytOut.Length - 1; i > 0; i--)
                if (bytOut[i] != 0)
                    break;
            i++;

            return System.Convert.ToBase64String(bytOut, 0, i);
        }

        public string Decrypting(string Source, string Key)
        {
            byte[] bytIn = System.Convert.FromBase64String(Source);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);

            byte[] bytKey = GetLegalKey(Key);

            mobjCryptoService.Key = bytKey;
            mobjCryptoService.IV = bytKey;

            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();

            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);

            System.IO.StreamReader sr = new System.IO.StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
    
}

