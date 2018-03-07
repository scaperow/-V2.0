using System;
using System.Collections.Generic;
using System.Text;

namespace BizFunctionInfos
{
    public abstract class NumericalAverge
    {
        IComparisonNumber _ComparisonNumber = new AvergeNumber();
        public IComparisonNumber ComparisonNumber
        {
            get
            {
                return _ComparisonNumber;
            }
            set
            {
                _ComparisonNumber = value;
            }
        }

        public virtual object getResult(double[] arguments, double percent)
        {
            double averge = ComparisonNumber.getNumber(arguments);

            double sum = 0;
            int validNum = 0;
            foreach(double d in arguments)
            {
                if (Math.Abs(d - averge) <= averge * percent)
                {
                    sum = sum + d;
                    validNum++;
                }
            }

            if (arguments.Length - validNum <= 1)
                return sum / validNum;

            throw new InvalidCastException();
        }

        public virtual object getResultGeneral(double[] arguments, double percent, int valideCount)
        {
            double averge = ComparisonNumber.getNumber(arguments);

            double sum = 0;
            int validNum = 0;
            foreach (double d in arguments)
            {
                if (Math.Abs(d - averge) <= averge * percent)
                {
                    sum = sum + d;
                    validNum++;
                }
            }

            if (validNum >= valideCount)
                return sum / validNum;

            throw new InvalidCastException();
        }
    }

    /// <summary>
    /// 获得可比较的数值
    /// </summary>
    public interface IComparisonNumber
    {
        double getNumber(double[] arguments);
    }

    /// <summary>
    /// 几个数的平均值
    /// </summary>
    public class AvergeNumber : IComparisonNumber
    {
        public double getNumber(double[] arguments)
        {
 	        double sum = 0;
            foreach (double d in arguments)
                sum = sum + d;

            return sum / arguments.Length;
        }
    }

    /// <summary>
    /// 几个数排序选择中间的值
    /// </summary>
    public class MedianNumber : IComparisonNumber
    {
        public double getNumber(double[] arguments)
        {
            Array.Sort<double>(arguments);
            if (arguments.Length > 0)
                return arguments[arguments.Length / 2];

            throw new InvalidCastException();
        }
    }

    /// <summary>
    /// 参照的数值选取几个数的平均值
    /// </summary>
    public class NumericalAvergeByAverge : NumericalAverge
    {
        public NumericalAvergeByAverge()
        {
            this.ComparisonNumber = new AvergeNumber();
        }
    }

    /// <summary>
    /// 参照的数值选取几个数的中位数,用于外加剂试验,砼试验
    /// </summary>
    public class NumericalAvergeByWJJTongMedian : NumericalAverge
    {
        public NumericalAvergeByWJJTongMedian()
        {
            this.ComparisonNumber = new MedianNumber();
        }

        public override object getResult(double[] arguments, double percent)
        {
            double averge = ComparisonNumber.getNumber(arguments);

            double max = MathEx.Max(arguments);
            double min = MathEx.Min(arguments);

            if (Math.Abs(max - averge) <= averge * percent && Math.Abs(min - averge) <= averge * percent)
                return (max + min + averge) / arguments.Length;
            else if (Math.Abs(max - averge) > averge * percent && Math.Abs(min - averge) > averge * percent)
                throw new InvalidCastException();
            else
                return averge;
        }
    }

    /// <summary>
    /// 参照的数值选取几个数的中位数,用于计算凝结时间
    /// </summary>
    public class NumericalAvergeByNJMedian : NumericalAverge
    {
        public NumericalAvergeByNJMedian()
        {
            this.ComparisonNumber = new MedianNumber();
        }

        public override object getResult(double[] arguments, double percent)
        {
            double averge = ComparisonNumber.getNumber(arguments);

            double max = MathEx.Max(arguments);
            double min = MathEx.Min(arguments);

            if (Math.Abs(max - averge) <= percent && Math.Abs(min - averge) <= percent)
                return (max + min + averge) / arguments.Length;
            else if (Math.Abs(max - averge) > percent && Math.Abs(min - averge) > percent)
                throw new InvalidCastException();
            else
                return averge;
        }
    }
}
