using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ShuXianCaiJiComponents
{
    /// <summary>
    /// 铁道数据上传加密处理
    /// </summary>
    public class UploadOperation
    {

        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="hashalgo"></param>
        /// <param name="signType"></param>
        /// <param name="clearText"></param>
        /// <param name="clearTextLength"></param>
        /// <param name="sign"></param>
        /// <param name="signLength"></param>
        /// <returns></returns>
        [DllImport("zhtClientApi.dll")]
        private extern static int Key_SignData(uint hashalgo, uint signType, string clearText, uint clearTextLength, ref byte sign, ref int signLength);

        /// <summary>
        /// 判断是否加入签名
        /// </summary>
        /// <returns></returns>
        [DllImport("SignRadom.dll")]
        private extern static int IsSign();

        /// <summary>
        /// 签名Json串
        /// </summary>
        /// <param name="_object">要加密数据对象</param>
        /// <param name="_Json">返回加密后的Json</param>
        /// <returns>
        /// -1:没有接入签名硬件
        /// 0：签名成功
        /// 256：签名硬件过忙
        /// -999:不需要签名返回明文
        /// -888:该条线路不需要加密
        /// </returns>
        public int EncodeDataModel(object _object, out string _Json, string _SignNumber)
        {
            int _ErrorCode = 0;
            int _temp = 0;
            _Json = string.Empty;
            try
            {
                _Json = ObjectToJson(_object);
                if (string.IsNullOrEmpty(_SignNumber) || _SignNumber=="")
                {
                    return -3;
                }
                else
                {
                    if (IsSign() == 0)
                    {
                        byte[] _TMPByte = new byte[173];
                        _ErrorCode = Key_SignData(32772, 2, _Json, (uint)_Json.Length, ref _TMPByte[0], ref _temp);
                        if (_ErrorCode == 0)
                        {
                            _Json = System.Text.Encoding.ASCII.GetString(_TMPByte);
                        }
                        return _ErrorCode;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
            catch
            {
                return -5;
            }
        }

        /// <summary>
        /// 实例对象转化Json串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string ObjectToJson(object obj)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}