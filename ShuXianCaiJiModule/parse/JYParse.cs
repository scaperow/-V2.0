using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ShuXianCaiJiModule.parse
{
    /// <summary>
    /// 建仪仪表的协议返回值不设定长,所以只能加条件长度大于7,头数据位1,第6位为4.
    /// 注:此型号仪表用usb线接受数据符合格式的频率很慢,不能符合需求
    /// </summary>
    public class JYParse : JZParseBase
    {
        #region JZParseBase 成员
        StringBuilder _StringBuilder = new StringBuilder();
        float Force = 0.0f;

        Logger log = new Logger(Path.Combine(Directory.GetCurrentDirectory(), "Log"));
        public float Parse(object obj)
        {
            try
            {
                byte[] Abytes = (byte[])obj;
                if (Abytes.Length != 0)
                {
                    //建仪仪表的协议返回值不设定长,所以只能加条件长度大于7,头数据位1,第6位为4.注:此型号仪表用usb线接受数据符合格式的频率很慢,不能符合需求
                    if (Abytes.Length > 7 && Abytes[0].ToString() == "1" && Abytes[5].ToString() == "4")
                    {
                        Force = (float.Parse(Abytes[6].ToString()) * 256 + float.Parse(Abytes[7].ToString())) / 10.0f;
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.StackTrace, false, true);
            }
            return Force;
        }

        #endregion

        #region JZParseBase 成员


        public void SetModel(SXCJModule _SXCJModule)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region JZParseBase 成员

        void JZParseBase.SetModel(SXCJModule _SXCJModule)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
