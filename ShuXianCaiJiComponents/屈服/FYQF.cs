using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShuXianCaiJiModule;

namespace ShuXianCaiJiComponents
{
    public class FYQF : QuFuBase
    {
        #region QuFuBase 成员

        public QFModule CalQuFu(SXCJModule module, List<float> list, double qfLimit, float maxForce, MachineBase machine)
        {
            QFModule _QFModule = new QFModule();
            FYUniversQF ou = machine as FYUniversQF;
            if (ou != null)
            {
                float downQF = 0.00f;
                float upQF = 0.00f;
                ou.GetQF(out downQF, out upQF);
                _QFModule.DownQF = downQF;
                _QFModule.UpQF = upQF;
            }

            return _QFModule;
        }

        #endregion
    }
}
