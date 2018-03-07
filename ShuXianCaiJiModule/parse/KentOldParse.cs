using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ShuXianCaiJiModule.parse
{
    public class KentOldParse : JZParseBase
    {

        #region JZParseBase 成员

        StringBuilder _StringBuilder = new StringBuilder();
        float Force = 0.0f; 
        Logger log = new Logger(Path.Combine(Directory.GetCurrentDirectory(), "Log"));

        SXCJModule _SXCJModule = null;
        byte[] Abytes;

        int num = 1;
        List<byte> _List = new List<byte>();
        Boolean KentEnd;
        Boolean KentBegin;

        public float Parse(object obj)
        {
            byte[] bytes = (byte[])obj;
            
            log.WriteLog(DataEncoder.byteToHexStr(bytes),true,false);
            KentEnd = false;
            if (KentBegin)
            {
                for (int lengh = 0; lengh < bytes.Length; lengh++)
                {
                    if (bytes[lengh] == 15)
                    {
                        KentBegin = false;
                        KentEnd = true;
                        break;
                    }
                }
                byte[] newbyte = new byte[Abytes.Length + bytes.Length];
                Abytes.CopyTo(newbyte, 0);
                bytes.CopyTo(newbyte, Abytes.Length);
                Abytes = newbyte;
            }
            if (!KentBegin && !KentEnd)
            {
                Boolean IsAdd = false;

                for (int lengh = 0; lengh < bytes.Length; lengh++)
                {
                    if (bytes[lengh] == 240)
                    {
                        IsAdd = true;
                        break;
                    }
                }
                if (IsAdd)
                {
                    Abytes = bytes;
                    KentBegin = true;
                }
            }
            if (KentEnd)
            {
                int BeginByte = 0;
                for (int lengh = 0; lengh < Abytes.Length; lengh++)
                {
                    if (Abytes[lengh] == 240)
                    {
                        BeginByte = lengh;
                        break;
                    }
                }
                int low3 = Abytes[BeginByte + 6] & 0xf;
                int high3 = (Abytes[BeginByte + 6] >> 4) * 10;

                int low2 = Abytes[BeginByte + 5] & 0xf;
                int high2 = (Abytes[BeginByte + 5] >> 4) * 10;

                int low1 = Abytes[BeginByte + 4] & 0xf;
                int high1 = (Abytes[BeginByte + 4] >> 4) * 10;

                Force = (float)((((Convert.ToDouble(low1 + high1) * 10000 + Convert.ToDouble(low2 + high2) * 100 + Convert.ToDouble(low3 + high3)) / num)) - _SXCJModule.SpecialSetting.ZeroParameters);
            }

            return Force;
            //byte[] bytes = (byte[])obj;
            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    _StringBuilder.Append(bytes[i].ToString("X2"));
            //}
            //int n = _StringBuilder.ToString().IndexOf("F0");
            //if (_StringBuilder.ToString().IndexOf("F0") > 0)
            //{
            //    _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("F0"));
            //}
            //while (_StringBuilder.Length >= 22)
            //{
            //    Force = float.Parse(_StringBuilder.ToString().Substring(8, 6)) / num;
            //    Force = Force - _SXCJModule.SpecialSetting.ZeroParameters;
            //    _StringBuilder.Remove(0, _StringBuilder.ToString().IndexOf("0F") + 2);
            //}
            //return Force;
        }

        public void SetModel(SXCJModule sxckModel)
        {
            _SXCJModule = sxckModel;
            switch (_SXCJModule.SpecialSetting.PointNum)
            {
                case 1:
                    {
                        num = 10;
                        break;
                    }
                case 2:
                    {
                        num = 100;
                        break;
                    }
                case 3:
                    {
                        num = 1000;
                        break;
                    }
            }
        }

        #endregion
    }
}
