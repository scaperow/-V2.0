using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShuXianCaiJiModule;

namespace ShuXianCaiJiComponents
{
    public interface QuFuBase
    {
        QFModule CalQuFu(SXCJModule module, List<float> list, Double qfLimit, float maxForce, MachineBase machine);
    }
}
