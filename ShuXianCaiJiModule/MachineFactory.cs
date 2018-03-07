using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShuXianCaiJiModule.parse;

namespace ShuXianCaiJiModule
{
    public static class MachineFactory
    {
        public static MachineBase GetMachine(String name, Int32 type, Boolean isTest)
        {
            MachineBase machine = null;
            switch (name)
            {
                case "丰仪":
                    if (type == 1)
                    {
                        machine = new FYPressure();
                    }
                    else if (type == 2)
                    {
                        machine = new FYUnivers();
                    }
                    if (isTest)
                    {
                        machine.parse = new FYParse();
                    }
                    break;
                case "丰仪液晶":
                    if (type == 1)
                    {
                        machine = new FYUnivers();
                    }
                    else if (type == 2)
                    {
                        machine = new FYUnivers();
                    }
                    break;
                case "建仪":
                    if (type == 1)
                    {
                        machine = new JYPressuer(isTest);
                    }
                    else if (type == 2)
                    {
                        machine = new JYUnivers(isTest);
                    }
                    break;
                case "肯特新液晶":
                    if (type == 1)
                    {
                        machine = new KentPressure(isTest);
                    }
                    else if (type == 2)
                    {
                        machine = new KentUnivers(isTest);
                    }
                    break;
                case "肯特液晶":
                    if (type == 1)
                    {
                        machine = new KentOldPressure(isTest);
                    }
                    else if (type == 2)
                    {
                        machine = new KentOldUnivers(isTest);
                    }
                    break;
                case "肯特数显":
                    if (type == 1)
                    {
                        machine = new KentSXPressure(isTest);
                    }
                    else if (type == 2)
                    {
                        machine = new KentSXUnivers(isTest);
                    }
                    break;
                case "肯特AD液晶":
                    if (type == 1)
                    {
                        machine = new KentADUnivers(isTest);
                    }
                    else if (type == 2)
                    {
                        machine = new KentADUnivers(isTest);
                    }
                    break;
                case "欧凯":
                    if (type == 1)
                    {
                        machine = new OKEPressure(isTest);
                    }
                    else if (type == 2)
                    {
                        machine = new OKEUnivers(isTest);
                    }
                    break;
                case "晨鑫":
                    if (type == 1)
                    {
                        machine = new OKEPressure(isTest);
                    }
                    else if (type == 2)
                    {
                        machine = new OKEUnivers(isTest);
                    }
                    break;
                case "威海":
                    if (type == 1)
                    {
                        machine = new WHPressure(isTest);
                    }
                    else if (type == 2)
                    {
                        machine = new WHUnivers(isTest);
                    }
                    break;
                case "威海屏显":
                    if (type == 1)
                    {
                        machine = new WHPingXian();
                    }
                    else if (type == 2)
                    {
                        machine = new WHPingXian();
                    }
                    break;
                case "丰仪万能机":
                    {
                        machine = new FYUniversQF();
                        if (isTest)
                        {
                            machine.parse = new FYParse();
                        }
                        break;
                    }
                case "丰仪万能机转压力机":
                    {
                        machine = new FYUniversToPressure();
                        if (isTest)
                        {
                            machine.parse = new FYParse();
                        }
                        break;
                    }
            }
            return machine;
        }
    }
}
