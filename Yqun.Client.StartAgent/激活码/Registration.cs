using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using Yqun.Common.Encoder;
using System.Management;
using System.Security.Cryptography;

namespace Yqun.MainUI
{
    public partial class Registration : Form
    {
        public Encryption.Hash hash = new Encryption.Hash(Encryption.Hash.Provider.SHA512);
        public Registration()
        {
            InitializeComponent();
        }

        public bool AuthSerial(string filename)
        {
            try
            {
                byte[] b = File.ReadAllBytes(filename);
                Hashtable ht = Serialize.DeSerializeFromBytes(Yqun.Common.Encoder.Encrypt.DecryptBytes(b,"www.kingrocket.com")) as Hashtable;
                if (ht != null && ht.Count == 3)
                {
                    string sss = ht["RegInfo"].ToString();
                    string ddd = ht["abc"].ToString();
                    return sss.Equals("PASS") && ddd == GetCPUSerialNumber();
                }
            }
            catch
            { 
            }

            return false;
        }

        public void AuthSerial() 
        {
            string MachineCode = TextBox1.Text.ToUpper();
            string RegCode = TextBox2.Text;
            string RegInfo = Encrypt(RegCode);
            if (RegInfo != "" && GetHashCode(MachineCode) == RegCode)
            {
                AuthorizeComputer();
            } 
            else 
            { 
                TextBox2.Text = ""; 
                Label5.Visible = true;

                DialogResult = DialogResult.None;
            } 
        }

        public string Encrypt(string text) 
        {
            Encryption.Data skey = new Encryption.Data("!41i0@7_EW>F");
            Encryption.Symmetric sym = new Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael); 
            Encryption.Data encryptedData = default(Encryption.Data); 
            encryptedData = sym.Encrypt(new Encryption.Data(text), skey); 
            return encryptedData.ToHex(); 
        }
 
        public string Decrypt(string hexstream) 
        {
            try
            {
                Encryption.Data skey = new Encryption.Data("!41i0@7_EW>F");
                Encryption.Symmetric sym = new Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael);
                Encryption.Data encryptedData = new Encryption.Data();
                encryptedData.Hex = hexstream;
                Encryption.Data decryptedData = default(Encryption.Data);
                decryptedData = sym.Decrypt(encryptedData, skey);
                return decryptedData.ToString();
            }
            catch
            { }

            return "";
        } 

        public string GetCPUSerialNumber()
        {
            try
            {
                ManagementClass ms = new ManagementClass();
                ms.Path = new ManagementPath("Win32_Processor");
                ManagementObjectCollection moc = ms.GetInstances();

                String strCpuID = null;
                foreach (ManagementObject mo in moc)
                {
                    strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }

                return strCpuID;
            }
            catch
            {
            }

            return Environment.UserDomainName;
        }

        public void AuthorizeComputer() 
        {
            Hashtable ht = new Hashtable();
            ht.Add("abc", GetCPUSerialNumber());
            ht.Add("RegInfo", "PASS");
            ht.Add("(0)", "NL:DFM:SD_");
            byte[] b = Yqun.Common.Encoder.Encrypt.EncryptBytes(Serialize.SerializeToBytes(ht),"www.kingrocket.com");
            string filename = Path.Combine(Application.StartupPath,"License.dat");

            if (File.Exists(filename))
                File.Delete(filename);

            File.WriteAllBytes(filename, b);
            MessageBox.Show("注册成功，你现在可以进入系统了。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            String SerialNumber = Guid.NewGuid().ToString().Replace("-", "").Trim().ToUpper();
            for (int i = 1; i < 4; i++)
            {
                SerialNumber = SerialNumber.Insert(i * 8 + i - 1, "-");
            }
            TextBox1.Text = SerialNumber;
        }

        private void exitbutton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void unlockbutton_Click(object sender, EventArgs e)
        {
            AuthSerial();
        }

        private string GetHashCode(string SerialNumber)
        {
            SerialNumber = Reverse(SerialNumber);

            Hasher hs = new Hasher();
            hs.HasherText = SerialNumber;
            return hs.MD5Hasher();
        }

        private string Reverse(string str)
        {
            char[] _chars = str.ToCharArray();
            Array.Reverse(_chars);
            string newstr = new string(_chars);
            return newstr;
        }

        private string GetSerialNumber()
        {
            StringBuilder Str = new StringBuilder();
            Str.Append("@");
            Str.Append(TextBox1.Text.ToUpper());

            return Str.ToString();
        }
    }

    public class Hasher
    {
        public string _HasherText;

        public string HasherText
        {
            set
            {
                _HasherText = value;
            }
            get
            {
                return _HasherText;
            }
        }


        public string MD5Hasher()
        {
            byte[] MD5Data = System.Text.Encoding.UTF8.GetBytes(HasherText);
            MD5 MD5 = new MD5CryptoServiceProvider();
            byte[] Result = MD5.ComputeHash(MD5Data);
            return System.Convert.ToBase64String(Result);
        }
    }
}
