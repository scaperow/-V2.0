using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShuXianCaiJiModule;

namespace ShuXianCaiJiComponents
{
    public class OKEQF : QuFuBase
    {

        #region QuFuBase 成员


        public QFModule CalQuFu(ShuXianCaiJiModule.SXCJModule module, List<float> list, double qfLimit, float maxForce, MachineBase machine)
        {
            QFModule _QFModule = new QFModule();
            OKEUnivers ou = machine as OKEUnivers;
            if (ou != null)
            {
                _QFModule.DownQF = ou.GetQuFuL();
                _QFModule.UpQF = ou.GetQuFuH();
            }

            return _QFModule;
        }

        #endregion
    }
}
