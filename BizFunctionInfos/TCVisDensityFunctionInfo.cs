using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.CalcEngine;

namespace BizFunctionInfos
{
    [Serializable]
    public class TCVisDensityFunctionInfo : FunctionInfo
    {
        static Dictionary<double, double> List = new Dictionary<double, double>();

        static TCVisDensityFunctionInfo()
        {
            //温度 水的密度
            List.Add(5.0, 0.999992);
            List.Add(5.5, 0.999982);
            List.Add(6.0, 0.999968);
            List.Add(6.5, 0.999951);
            List.Add(7.0, 0.999930);
            List.Add(7.5, 0.999905);
            List.Add(8.0, 0.999876);
            List.Add(8.5, 0.999844);
            List.Add(9.0, 0.999809);
            List.Add(9.5, 0.999770);
            List.Add(10.0, 0.999728);
            List.Add(10.5, 0.999682);
            List.Add(11.0, 0.999633);
            List.Add(11.5, 0.999580);
            List.Add(12.0, 0.999525);
            List.Add(12.5, 0.999466);
            List.Add(13.0, 0.999404);
            List.Add(13.5, 0.999339);
            List.Add(14.0, 0.999271);
            List.Add(14.5, 0.999200);
            List.Add(15.0, 0.999126);
            List.Add(15.5, 0.999050);
            List.Add(16.0, 0.998970);
            List.Add(16.5, 0.998888);
            List.Add(17.0, 0.998802);
            List.Add(17.5, 0.998714);
            List.Add(18.0, 0.998623);
            List.Add(18.5, 0.998530);
            List.Add(19.0, 0.998433);
            List.Add(19.5, 0.998334);
            List.Add(20.0, 0.998232);
            List.Add(20.5, 0.998128);
            List.Add(21.0, 0.998021);
            List.Add(21.5, 0.997911);
            List.Add(22.0, 0.997799);
            List.Add(22.5, 0.997685);
            List.Add(23.0, 0.997567);
            List.Add(23.5, 0.997448);
            List.Add(24.0, 0.997327);
            List.Add(24.5, 0.997201);
            List.Add(25.0, 0.997074);
            List.Add(25.5, 0.996944);
            List.Add(26.0, 0.996813);
            List.Add(26.5, 0.996679);
            List.Add(27.0, 0.996542);
            List.Add(27.5, 0.996403);
            List.Add(28.0, 0.996262);
            List.Add(28.5, 0.996119);
            List.Add(29.0, 0.995974);
            List.Add(29.5, 0.995826);
            List.Add(30.0, 0.995676);
            List.Add(30.5, 0.995524);
            List.Add(31.0, 0.995369);
            List.Add(31.5, 0.995213);
            List.Add(32.0, 0.995054);
            List.Add(32.5, 0.994894);
            List.Add(33.0, 0.994731);
            List.Add(33.5, 0.994566);
            List.Add(34.0, 0.994399);
            List.Add(34.5, 0.994230);
        }

        public override object Evaluate(object[] args)
        {
            try
            {
                double value = ArgumentConvert.ToDouble(args[0]);
                if (List.ContainsKey(value))
                    return List[value];
                return CalcError.Value;
            }
            catch
            {
                return CalcError.Value;
            }
        }

        public override int MaxArgs
        {
            get
            {
                return 1;
            }
        }

        public override int MinArgs
        {
            get
            {
                return 1;
            }
        }

        public override string Name
        {
            get
            {
                return "TCVisDensity";
            }
        }
    }
}
