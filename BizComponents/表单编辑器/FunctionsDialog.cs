using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FarPoint.CalcEngine;
using FarPoint.Win.Spread;
using Yqun.Common.Encoder;

namespace BizComponents
{
    public partial class FunctionsDialog : Form
    {
        internal class FunctionCompare : IComparer<FunctionInfo>
        {
            #region IComparer<FunctionInfo> 成员

            public int Compare(FunctionInfo x, FunctionInfo y)
            {
                return string.Compare(x.Name, y.Name);
            }

            #endregion
        }

        //函数类别和函数信息
        private Dictionary<String, List<FunctionInfo>> FunctionInfos = new Dictionary<string, List<FunctionInfo>>();
        //函数描述信息
        private Dictionary<String, String> FunctionDescriptions = new Dictionary<string, string>();

        FpSpread _fpSpread;

        FunctionCompare functionCompare = new FunctionCompare();

        public FunctionsDialog(FpSpread fpSpread)
        {
            InitializeComponent();

            _fpSpread = fpSpread;

            InitFunctionInfo();
        }

        /// <summary>
        /// 初始化内置函数
        /// </summary>
        private void InitFunctionInfo()
        {
            FunctionInfos.Clear();
            FunctionDescriptions.Clear();

            FunctionInfos.Add("常用函数", new List<FunctionInfo>());
            FunctionInfos.Add("全部", new List<FunctionInfo>());
            FunctionInfos.Add("数据库", new List<FunctionInfo>());
            FunctionInfos.Add("日期和时间", new List<FunctionInfo>());
            FunctionInfos.Add("工程", new List<FunctionInfo>());
            FunctionInfos.Add("财务", new List<FunctionInfo>());
            FunctionInfos.Add("查找", new List<FunctionInfo>());
            FunctionInfos.Add("逻辑", new List<FunctionInfo>());
            FunctionInfos.Add("数学与三角函数", new List<FunctionInfo>());
            FunctionInfos.Add("统计", new List<FunctionInfo>());
            FunctionInfos.Add("信息", new List<FunctionInfo>());
            FunctionInfos.Add("文本", new List<FunctionInfo>());

            #region Farpoint 内置函数

            //数据库类
            DAverageFunctionInfo daverage = new DAverageFunctionInfo();
            FunctionInfos["数据库"].Add(daverage);
            FunctionInfos["全部"].Add(daverage);
            FunctionDescriptions.Add(daverage.Name, "DAVERAGE(database, field, criteria) \r\n 计算满足给定条件的列表或数据库的列中数值的平均值。");

            DCountFunctionInfo dcount = new DCountFunctionInfo();
            FunctionInfos["数据库"].Add(dcount);
            FunctionInfos["全部"].Add(dcount);
            FunctionDescriptions.Add(dcount.Name, "DCOUNT(database, field, criteria) \r\n 从满足给定条件的数据库记录的字段(列)中，计算数值单元格数目。");

            DCountAFunctionInfo dcounta = new DCountAFunctionInfo();
            FunctionInfos["数据库"].Add(dcounta);
            FunctionInfos["全部"].Add(dcounta);
            FunctionDescriptions.Add(dcounta.Name, "DCOUNTA(database, field, criteria) \r\n 对满足指定条件的数据库记录字段(列)的非空单元格进行计数。");

            DGetFunctionInfo dget = new DGetFunctionInfo();
            FunctionInfos["数据库"].Add(dget);
            FunctionInfos["全部"].Add(dget);
            FunctionDescriptions.Add(dget.Name, "DGET(database, field, criteria) \r\n 从数据库中提取符合指定条件且唯一存在的记录。");

            DMaxFunctionInfo dmax = new DMaxFunctionInfo();
            FunctionInfos["数据库"].Add(dmax);
            FunctionInfos["全部"].Add(dmax);
            FunctionDescriptions.Add(dmax.Name, "DMAX(database, field, criteria) \r\n 返回满足给定条件的数据库中记录的字段(列)中数据的最大值。");

            DMinFunctionInfo dmin = new DMinFunctionInfo();
            FunctionInfos["数据库"].Add(dmin);
            FunctionInfos["全部"].Add(dmin);
            FunctionDescriptions.Add(dmin.Name, "DMIN(database, field, criteria) \r\n 返回满足给定条件的数据库中记录的字段(列)中数据的最小值。");

            DProductFunctionInfo dproduct = new DProductFunctionInfo();
            FunctionInfos["数据库"].Add(dproduct);
            FunctionInfos["全部"].Add(dproduct);
            FunctionDescriptions.Add(dproduct.Name, "DPRODUCT(database, field, criteria) \r\n 返回满足指定条件的数据库中记录字段(列)的值相乘。");

            DStDevFunctionInfo dstdev = new DStDevFunctionInfo();
            FunctionInfos["数据库"].Add(dstdev);
            FunctionInfos["全部"].Add(dstdev);
            FunctionDescriptions.Add(dstdev.Name, "DSTDEV(database, field, criteria) \r\n 根据所选数据库条目中的样本估算数据的标准偏差。");

            DStDevPFunctionInfo dstdevp = new DStDevPFunctionInfo();
            FunctionInfos["数据库"].Add(dstdevp);
            FunctionInfos["全部"].Add(dstdevp);
            FunctionDescriptions.Add(dstdevp.Name, "DSTDEVP(database, field, criteria) \r\n 以数据库选定项作为样本总体，计算数据的标准偏差。");

            DSumFunctionInfo dsum = new DSumFunctionInfo();
            FunctionInfos["数据库"].Add(dsum);
            FunctionInfos["全部"].Add(dsum);
            FunctionDescriptions.Add(dsum.Name, "DSUM(database, field, criteria) \r\n 求满足给定条件的数据库中记录的字段(列)数据的和。");

            DVarFunctionInfo dvar = new DVarFunctionInfo();
            FunctionInfos["数据库"].Add(dvar);
            FunctionInfos["全部"].Add(dvar);
            FunctionDescriptions.Add(dvar.Name, "DVAR(database, field, criteria) \r\n 根据所有数据库条目中的样本估算数据的偏差。");

            DVarPFunctionInfo dvarp = new DVarPFunctionInfo();
            FunctionInfos["数据库"].Add(dvarp);
            FunctionInfos["全部"].Add(dvarp);
            FunctionDescriptions.Add(dvarp.Name, "DVARP(database, field, criteria) \r\n 以数据库选定项作为样本总体，计算数据的总体方差。");

            //日期和时间类
            DateFunctionInfo date = new DateFunctionInfo();
            FunctionInfos["日期和时间"].Add(date);
            FunctionInfos["全部"].Add(date);
            FunctionDescriptions.Add(date.Name, "DATE(year,month,day) \r\n 返回日期时间代码中代表日期的数字。");

            DateDifFunctionInfo datedif = new DateDifFunctionInfo();
            FunctionInfos["日期和时间"].Add(datedif);
            FunctionInfos["全部"].Add(datedif);
            FunctionDescriptions.Add(datedif.Name, "DATEDIF(date1,date2,outputcode) \r\n 返回两个日期时间的差值。");

            DateValueFunctionInfo datevalue = new DateValueFunctionInfo();
            FunctionInfos["日期和时间"].Add(datevalue);
            FunctionInfos["全部"].Add(datevalue);
            FunctionDescriptions.Add(datevalue.Name, "DATEVALUE(date_string) \r\n 将日期值从字符串转化为序列数。");

            DayFunctionInfo day = new DayFunctionInfo();
            FunctionInfos["日期和时间"].Add(day);
            FunctionInfos["全部"].Add(day);
            FunctionDescriptions.Add(day.Name, "DAY(date) \r\n 返回一个月中的第几天的数值，介于 1 到 31 之间。");

            Days360FunctionInfo days360 = new Days360FunctionInfo();
            FunctionInfos["日期和时间"].Add(days360);
            FunctionInfos["全部"].Add(days360);
            FunctionDescriptions.Add(days360.Name, "DAYS360(startdate,enddate,method) \r\n 按每年 360 天返回两个日期间相差的天数(每月 30 天)。");

            EDateFunctionInfo edate = new EDateFunctionInfo();
            FunctionInfos["日期和时间"].Add(edate);
            FunctionInfos["全部"].Add(edate);
            FunctionDescriptions.Add(edate.Name, "EDATE(startdate,months) \r\n 返回一串日期，指示起始日期之前/之后的月数。");

            EoMonthFunctionInfo eomonth = new EoMonthFunctionInfo();
            FunctionInfos["日期和时间"].Add(eomonth);
            FunctionInfos["全部"].Add(eomonth);
            FunctionDescriptions.Add(eomonth.Name, "EOMONTH(startdate,months) \r\n 返回一串日期，表示指定月数之前/之后的月份的最后一天。");

            HourFunctionInfo hour = new HourFunctionInfo();
            FunctionInfos["日期和时间"].Add(hour);
            FunctionInfos["全部"].Add(hour);
            FunctionDescriptions.Add(hour.Name, "HOUR(time) \r\n 返回小时数值，是一个 0 (12:00 A.M.) 到 23 (11:00 P.M.) 之间的整数。");

            MinuteFunctionInfo minute = new MinuteFunctionInfo();
            FunctionInfos["日期和时间"].Add(minute);
            FunctionInfos["全部"].Add(minute);
            FunctionDescriptions.Add(minute.Name, "MINUTE(time) \r\n 返回分钟数值，是一个 0 到 59 之间的整数。");

            MonthFunctionInfo month = new MonthFunctionInfo();
            FunctionInfos["日期和时间"].Add(month);
            FunctionInfos["全部"].Add(month);
            FunctionDescriptions.Add(month.Name, "MONTH(date) \r\n 返回月份值，是一个 1(一月) 到 12(十二月) 之间的数字。");

            NetWorkdaysFunctionInfo networkdays = new NetWorkdaysFunctionInfo();
            FunctionInfos["日期和时间"].Add(networkdays);
            FunctionInfos["全部"].Add(networkdays);
            FunctionDescriptions.Add(networkdays.Name, "NETWORKDAYS(startdate,enddate,holidays) \r\n 返回两个日期之间完整的工作日数。");

            NowFunctionInfo now = new NowFunctionInfo();
            FunctionInfos["日期和时间"].Add(now);
            FunctionInfos["全部"].Add(now);
            FunctionDescriptions.Add(now.Name, "NOW() \r\n 返回日期时间格式的当前日期和时间。");

            SecondFunctionInfo second = new SecondFunctionInfo();
            FunctionInfos["日期和时间"].Add(second);
            FunctionInfos["全部"].Add(second);
            FunctionDescriptions.Add(second.Name, "SECOND(time) \r\n 返回秒数值，是一个 0 到 59 之间的整数。");

            TimeFunctionInfo time = new TimeFunctionInfo();
            FunctionInfos["日期和时间"].Add(time);
            FunctionInfos["全部"].Add(time);
            FunctionDescriptions.Add(time.Name, "TIME(hour,minutes,seconds) \r\n 返回特定时间的序列数。");

            TimeValueFunctionInfo timevalue = new TimeValueFunctionInfo();
            FunctionInfos["日期和时间"].Add(timevalue);
            FunctionInfos["全部"].Add(timevalue);
            FunctionDescriptions.Add(timevalue.Name, "TIMEVALUE(time_string) \r\n 将文本形式的时间转化为标准格式的时间。");

            TodayFunctionInfo today = new TodayFunctionInfo();
            FunctionInfos["日期和时间"].Add(today);
            FunctionInfos["全部"].Add(today);
            FunctionDescriptions.Add(today.Name, "TODAY() \r\n 返回日期格式的当前日期。");

            WeekdayFunctionInfo weekday = new WeekdayFunctionInfo();
            FunctionInfos["日期和时间"].Add(weekday);
            FunctionInfos["全部"].Add(weekday);
            FunctionDescriptions.Add(weekday.Name, "WEEKDAY(date,type) \r\n 返回代表一周中的第几天的数值，是一个 1 到 7 之间的整数。");

            WeekNumFunctionInfo weeknum = new WeekNumFunctionInfo();
            FunctionInfos["日期和时间"].Add(weeknum);
            FunctionInfos["全部"].Add(weeknum);
            FunctionDescriptions.Add(weeknum.Name, "WEEKNUM(date,weektype) \r\n 返回一年中的周数。");

            WorkdayFunctionInfo workday = new WorkdayFunctionInfo();
            FunctionInfos["日期和时间"].Add(workday);
            FunctionInfos["全部"].Add(workday);
            FunctionDescriptions.Add(workday.Name, "WORKDAY(startdate,numdays,holidays) \r\n 返回在指定的若干个工作日之前/之后的日期(一串数字)。");

            YearFunctionInfo year = new YearFunctionInfo();
            FunctionInfos["日期和时间"].Add(year);
            FunctionInfos["全部"].Add(year);
            FunctionDescriptions.Add(year.Name, "YEAR(date) \r\n 返回日期的年份值，一个 1900-9999 之间的数字。");

            YearFracFunctionInfo yearfrac = new YearFracFunctionInfo();
            FunctionInfos["日期和时间"].Add(yearfrac);
            FunctionInfos["全部"].Add(yearfrac);
            FunctionDescriptions.Add(yearfrac.Name, "YEARFRAC(startdate,enddate,basis) \r\n 返回一个年分数，表示 startdate 与 enddate 之间的整天天数。");

            //工程类
            BesselIFunctionInfo besseli = new BesselIFunctionInfo();
            FunctionInfos["工程"].Add(besseli);
            FunctionInfos["全部"].Add(besseli);
            FunctionDescriptions.Add(besseli.Name, "BESSELI(value,order) \r\n 返回修正的贝塞尔函数 In(x)。");

            BesselJFunctionInfo besselj = new BesselJFunctionInfo();
            FunctionInfos["工程"].Add(besselj);
            FunctionInfos["全部"].Add(besselj);
            FunctionDescriptions.Add(besselj.Name, "BESSELJ(value,order) \r\n 返回贝塞尔函数 Jn(x)。");

            BesselKFunctionInfo besselk = new BesselKFunctionInfo();
            FunctionInfos["工程"].Add(besselk);
            FunctionInfos["全部"].Add(besselk);
            FunctionDescriptions.Add(besselk.Name, "BESSELK(value,order) \r\n 返回修正的贝塞尔函数 Kn(x)。");

            BesselYFunctionInfo bessely = new BesselYFunctionInfo();
            FunctionInfos["工程"].Add(bessely);
            FunctionInfos["全部"].Add(bessely);
            FunctionDescriptions.Add(bessely.Name, "BESSELY(value,order) \r\n 返回贝塞尔函数 Yn(x)。");

            Bin2DecFunctionInfo bin2dec = new Bin2DecFunctionInfo();
            FunctionInfos["工程"].Add(bin2dec);
            FunctionInfos["全部"].Add(bin2dec);
            FunctionDescriptions.Add(bin2dec.Name, "BIN2DEC(number) \r\n 将二进制数转换为十进制。");

            Bin2HexFunctionInfo bin2hex = new Bin2HexFunctionInfo();
            FunctionInfos["工程"].Add(bin2hex);
            FunctionInfos["全部"].Add(bin2hex);
            FunctionDescriptions.Add(bin2hex.Name, "BIN2HEX(number,places) \r\n 将二进制数转换为十六进制。");

            Bin2OctFunctionInfo bin2oct = new Bin2OctFunctionInfo();
            FunctionInfos["工程"].Add(bin2oct);
            FunctionInfos["全部"].Add(bin2oct);
            FunctionDescriptions.Add(bin2oct.Name, "BIN2OCT(number,places) \r\n 将二进制数转换为八进制。");

            ComplexFunctionInfo complex = new ComplexFunctionInfo();
            FunctionInfos["工程"].Add(complex);
            FunctionInfos["全部"].Add(complex);
            FunctionDescriptions.Add(complex.Name, "COMPLEX(realcoeff,imagcoeff,suffix) \r\n 将实部系数和虚部系数转换为复数。");

            ConvertFunctionInfo convert = new ConvertFunctionInfo();
            FunctionInfos["工程"].Add(convert);
            FunctionInfos["全部"].Add(convert);
            FunctionDescriptions.Add(convert.Name, "CONVERT(number,from-unit,to-unit) \r\n 将数字从一种度量体系转换为另一种度量体系。");

            Dec2BinFunctionInfo dec2bin = new Dec2BinFunctionInfo();
            FunctionInfos["工程"].Add(dec2bin);
            FunctionInfos["全部"].Add(dec2bin);
            FunctionDescriptions.Add(dec2bin.Name, "DEC2BIN(number,places) \r\n 将十进制数转化为二进制。");

            Dec2HexFunctionInfo dec2hex = new Dec2HexFunctionInfo();
            FunctionInfos["工程"].Add(dec2hex);
            FunctionInfos["全部"].Add(dec2hex);
            FunctionDescriptions.Add(dec2hex.Name, "DEC2HEX(number,places) \r\n 将十进制数转化为十六进制。");

            Dec2OctFunctionInfo dec2oct = new Dec2OctFunctionInfo();
            FunctionInfos["工程"].Add(dec2oct);
            FunctionInfos["全部"].Add(dec2oct);
            FunctionDescriptions.Add(dec2oct.Name, "DEC2OCT(number,places) \r\n 将十进制数转化为八进制。");

            DeltaFunctionInfo delta = new DeltaFunctionInfo();
            FunctionInfos["工程"].Add(delta);
            FunctionInfos["全部"].Add(delta);
            FunctionDescriptions.Add(delta.Name, "DELTA(value1,value2) \r\n 测试两个数字是否相等。");

            ErfFunctionInfo erf = new ErfFunctionInfo();
            FunctionInfos["工程"].Add(erf);
            FunctionInfos["全部"].Add(erf);
            FunctionDescriptions.Add(erf.Name, "ERF(limit,upperlimit) \r\n 返回误差函数。");

            ErfcFunctionInfo erfc = new ErfcFunctionInfo();
            FunctionInfos["工程"].Add(erfc);
            FunctionInfos["全部"].Add(erfc);
            FunctionDescriptions.Add(erfc.Name, "ERFC(lowerlimit) \r\n 返回补余误差函数。");

            GeStepFunctionInfo gestep = new GeStepFunctionInfo();
            FunctionInfos["工程"].Add(gestep);
            FunctionInfos["全部"].Add(gestep);
            FunctionDescriptions.Add(gestep.Name, "GESTEP(number,step) \r\n 测试某个数字是否大于阈值。");

            Hex2BinFunctionInfo hex2bin = new Hex2BinFunctionInfo();
            FunctionInfos["工程"].Add(hex2bin);
            FunctionInfos["全部"].Add(hex2bin);
            FunctionDescriptions.Add(hex2bin.Name, "HEX2BIN(number, places) \r\n 将十六进制数转化为二进制。");

            Hex2DecFunctionInfo hex2dec = new Hex2DecFunctionInfo();
            FunctionInfos["工程"].Add(hex2dec);
            FunctionInfos["全部"].Add(hex2dec);
            FunctionDescriptions.Add(hex2dec.Name, "HEX2DEC(number) \r\n 将十六进制数转化为十进制。");

            Hex2OctFunctionInfo hex2oct = new Hex2OctFunctionInfo();
            FunctionInfos["工程"].Add(hex2oct);
            FunctionInfos["全部"].Add(hex2oct);
            FunctionDescriptions.Add(hex2oct.Name, "HEX2OCT(number, places) \r\n 将十六进制数转化为八进制。");

            ImAbsFunctionInfo imabs = new ImAbsFunctionInfo();
            FunctionInfos["工程"].Add(imabs);
            FunctionInfos["全部"].Add(imabs);
            FunctionDescriptions.Add(imabs.Name, "IMABS(complexnum) \r\n 返回复数的绝对值(模数)。");

            ImaginaryFunctionInfo imaginary = new ImaginaryFunctionInfo();
            FunctionInfos["工程"].Add(imaginary);
            FunctionInfos["全部"].Add(imaginary);
            FunctionDescriptions.Add(imaginary.Name, "IMAGINARY(complexnum) \r\n 返回复数的虚部系数。");

            ImArgumentFunctionInfo imargument = new ImArgumentFunctionInfo();
            FunctionInfos["工程"].Add(imargument);
            FunctionInfos["全部"].Add(imargument);
            FunctionDescriptions.Add(imargument.Name, "IMARGUMENT(complexnum) \r\n 返回幅角 q (以弧度表示的角度)。");

            ImConjugateFunctionInfo imconjugate = new ImConjugateFunctionInfo();
            FunctionInfos["工程"].Add(imconjugate);
            FunctionInfos["全部"].Add(imconjugate);
            FunctionDescriptions.Add(imconjugate.Name, "IMCONJUGATE(complexnum) \r\n 返回复数的共轭复数。");

            ImCosFunctionInfo imcos = new ImCosFunctionInfo();
            FunctionInfos["工程"].Add(imcos);
            FunctionInfos["全部"].Add(imcos);
            FunctionDescriptions.Add(imcos.Name, "IMCOS(complexnum) \r\n 返回复数的余弦值。");

            ImDivFunctionInfo imdiv = new ImDivFunctionInfo();
            FunctionInfos["工程"].Add(imdiv);
            FunctionInfos["全部"].Add(imdiv);
            FunctionDescriptions.Add(imdiv.Name, "IMCOS(complexnum,complexdenom) \r\n 返回两个复数之商。");

            ImExpFunctionInfo imexp = new ImExpFunctionInfo();
            FunctionInfos["工程"].Add(imexp);
            FunctionInfos["全部"].Add(imexp);
            FunctionDescriptions.Add(imexp.Name, "IMEXP(complexnum) \r\n 返回复数的指数值。");

            ImLnFunctionInfo imln = new ImLnFunctionInfo();
            FunctionInfos["工程"].Add(imln);
            FunctionInfos["全部"].Add(imln);
            FunctionDescriptions.Add(imln.Name, "IMLN(complexnum) \r\n 返回复数的自然对数。");

            ImLog10FunctionInfo imlog10 = new ImLog10FunctionInfo();
            FunctionInfos["工程"].Add(imlog10);
            FunctionInfos["全部"].Add(imlog10);
            FunctionDescriptions.Add(imlog10.Name, "IMLOG10(complexnum) \r\n 返回以 10 为底的复数的对数。");

            ImLog2FunctionInfo imlog2 = new ImLog2FunctionInfo();
            FunctionInfos["工程"].Add(imlog2);
            FunctionInfos["全部"].Add(imlog2);
            FunctionDescriptions.Add(imlog2.Name, "IMLOG2(complexnum) \r\n 返回以 2 为底的复数的对数。");

            ImPowerFunctionInfo impower = new ImPowerFunctionInfo();
            FunctionInfos["工程"].Add(impower);
            FunctionInfos["全部"].Add(impower);
            FunctionDescriptions.Add(impower.Name, "IMPOWER(complexnum,powernum) \r\n 返回复数的整数幂。");

            ImProductFunctionInfo improduct = new ImProductFunctionInfo();
            FunctionInfos["工程"].Add(improduct);
            FunctionInfos["全部"].Add(improduct);
            FunctionDescriptions.Add(improduct.Name, "IMPRODUCT(complexnum1,complexnum2, ...) \r\n 返回 1 到 255 个复数的乘积。");

            ImRealFunctionInfo imreal = new ImRealFunctionInfo();
            FunctionInfos["工程"].Add(imreal);
            FunctionInfos["全部"].Add(imreal);
            FunctionDescriptions.Add(imreal.Name, "IMREAL(complexnum) \r\n 返回复数的实部系数。");

            ImSinFunctionInfo imsin = new ImSinFunctionInfo();
            FunctionInfos["工程"].Add(imsin);
            FunctionInfos["全部"].Add(imsin);
            FunctionDescriptions.Add(imsin.Name, "IMSIN(complexnum) \r\n 返回复数的正弦值。");

            ImSqrtFunctionInfo imsqrt = new ImSqrtFunctionInfo();
            FunctionInfos["工程"].Add(imsqrt);
            FunctionInfos["全部"].Add(imsqrt);
            FunctionDescriptions.Add(imsqrt.Name, "IMSQRT(complexnum) \r\n 返回复数的平方根。");

            ImSubFunctionInfo imsub = new ImSubFunctionInfo();
            FunctionInfos["工程"].Add(imsub);
            FunctionInfos["全部"].Add(imsub);
            FunctionDescriptions.Add(imsub.Name, "IMSUB(complexnum1,complexnum2) \r\n 返回两个复数的差值。");

            ImSumFunctionInfo imsum = new ImSumFunctionInfo();
            FunctionInfos["工程"].Add(imsum);
            FunctionInfos["全部"].Add(imsum);
            FunctionDescriptions.Add(imsum.Name, "IMSUM(complexnum1,complexnum2, ...) \r\n 返回复数的和。");

            Oct2BinFunctionInfo oct2bin = new Oct2BinFunctionInfo();
            FunctionInfos["工程"].Add(oct2bin);
            FunctionInfos["全部"].Add(oct2bin);
            FunctionDescriptions.Add(oct2bin.Name, "OCT2BIN(number,places) \r\n 将八进制数转换为二进制。");

            Oct2DecFunctionInfo oct2dec = new Oct2DecFunctionInfo();
            FunctionInfos["工程"].Add(oct2dec);
            FunctionInfos["全部"].Add(oct2dec);
            FunctionDescriptions.Add(oct2dec.Name, "OCT2DEC(number) \r\n 将八进制数转换为十进制。");

            Oct2HexFunctionInfo oct2hex = new Oct2HexFunctionInfo();
            FunctionInfos["工程"].Add(oct2hex);
            FunctionInfos["全部"].Add(oct2hex);
            FunctionDescriptions.Add(oct2hex.Name, "OCT2HEX(number,places) \r\n 将八进制数转换为十六进制。");

            //财务类
            AccrIntFunctionInfo accrint = new AccrIntFunctionInfo();
            FunctionInfos["财务"].Add(accrint);
            FunctionInfos["全部"].Add(accrint);
            FunctionDescriptions.Add(accrint.Name, "ACCRINT(issue,first,settle,rate,par,frequency,basis) \r\n 返回定期支付利息的债权的应计利息。");

            AccrIntMFunctionInfo accrintm = new AccrIntMFunctionInfo();
            FunctionInfos["财务"].Add(accrintm);
            FunctionInfos["全部"].Add(accrintm);
            FunctionDescriptions.Add(accrintm.Name, "ACCRINTM(issue,maturity,rate,par,basis) \r\n 返回在到期日支付利息的债权的应计利息。");

            AmordegrcFunctionInfo amordegrc = new AmordegrcFunctionInfo();
            FunctionInfos["财务"].Add(amordegrc);
            FunctionInfos["全部"].Add(amordegrc);
            FunctionDescriptions.Add(amordegrc.Name, "AMORDEGRC(cost,datepurchased,firstperiod,salvage,period,drate,basis) \r\n 返回每个记账期内资产分配的线性折旧。");

            AmorlincFunctionInfo amorlinc = new AmorlincFunctionInfo();
            FunctionInfos["财务"].Add(amorlinc);
            FunctionInfos["全部"].Add(amorlinc);
            FunctionDescriptions.Add(amorlinc.Name, "AMORLINC(cost,datepurchased,firstperiod,salvage,period,drate,basis) \r\n 返回每个记账期内资产分配的线性折旧。");

            CoupDaysFunctionInfo coupdays = new CoupDaysFunctionInfo();
            FunctionInfos["财务"].Add(coupdays);
            FunctionInfos["全部"].Add(coupdays);
            FunctionDescriptions.Add(coupdays.Name, "COUPDAYS(settlement,maturity,frequency,basis) \r\n 返回包含结算日的票息期的天数。");

            CoupDayBSFunctionInfo coupdaybs = new CoupDayBSFunctionInfo();
            FunctionInfos["财务"].Add(coupdaybs);
            FunctionInfos["全部"].Add(coupdaybs);
            FunctionDescriptions.Add(coupdaybs.Name, "COUPDAYBS(settlement,maturity,frequency,basis) \r\n 返回从票息期开始到结算日之间的天数。");

            CoupDaysNCFunctionInfo coupdaysnc = new CoupDaysNCFunctionInfo();
            FunctionInfos["财务"].Add(coupdaysnc);
            FunctionInfos["全部"].Add(coupdaysnc);
            FunctionDescriptions.Add(coupdaysnc.Name, "COUPDAYSNC(settlement,maturity,frequency,basis) \r\n 返回从结算日到下一票息支付日之间的天数。");

            CoupNCDFunctionInfo coupncd = new CoupNCDFunctionInfo();
            FunctionInfos["财务"].Add(coupncd);
            FunctionInfos["全部"].Add(coupncd);
            FunctionDescriptions.Add(coupncd.Name, "COUPNCD(settlement,maturity,frequency,basis) \r\n 返回结算日后的下一票息支付日。");

            CoupNumFunctionInfo coupnum = new CoupNumFunctionInfo();
            FunctionInfos["财务"].Add(coupnum);
            FunctionInfos["全部"].Add(coupnum);
            FunctionDescriptions.Add(coupnum.Name, "COUPNUM(settlement,maturity,frequency,basis) \r\n 返回结算日与到期日之间可支付的票息数。");

            CoupPcdFunctionInfo couppcd = new CoupPcdFunctionInfo();
            FunctionInfos["财务"].Add(couppcd);
            FunctionInfos["全部"].Add(couppcd);
            FunctionDescriptions.Add(couppcd.Name, "COUPPCD(settlement,maturity,frequency,basis) \r\n 返回结算日前的上一票息支付日。");

            CumIpmtFunctionInfo cumipmt = new CumIpmtFunctionInfo();
            FunctionInfos["财务"].Add(cumipmt);
            FunctionInfos["全部"].Add(cumipmt);
            FunctionDescriptions.Add(cumipmt.Name, "CUMIPMT(rate,nper,pval,startperiod,endperiod,paytype) \r\n 返回两个付款期之间为贷款累积支付的利息。");

            CumPrincFunctionInfo cumprinc = new CumPrincFunctionInfo();
            FunctionInfos["财务"].Add(cumprinc);
            FunctionInfos["全部"].Add(cumprinc);
            FunctionDescriptions.Add(cumprinc.Name, "CUMPRINC(rate,nper,pval,startperiod,endperiod,paytype) \r\n 返回两个付款期之间为贷款累积支付的本金。");

            DbFunctionInfo db = new DbFunctionInfo();
            FunctionInfos["财务"].Add(db);
            FunctionInfos["全部"].Add(db);
            FunctionDescriptions.Add(db.Name, "DB(cost,salvage,life,period,month) \r\n 用固定余额递减法，返回指定期间内某项固定资产的折旧值。");

            DdbFunctionInfo ddb = new DdbFunctionInfo();
            FunctionInfos["财务"].Add(ddb);
            FunctionInfos["全部"].Add(ddb);
            FunctionDescriptions.Add(ddb.Name, "DDB(cost,salvage,life,period,factor) \r\n 用双倍余额递减法或其他指定方法，返回指定期间内某项固定资产的折旧值。");

            DiscFunctionInfo disc = new DiscFunctionInfo();
            FunctionInfos["财务"].Add(disc);
            FunctionInfos["全部"].Add(disc);
            FunctionDescriptions.Add(disc.Name, "DISC(settle,mature,pricep,redeem,basis) \r\n 返回债权的贴现率。");

            DollarDeFunctionInfo dollarde = new DollarDeFunctionInfo();
            FunctionInfos["财务"].Add(dollarde);
            FunctionInfos["全部"].Add(dollarde);
            FunctionDescriptions.Add(dollarde.Name, "DOLLARDE(fractionaldollar,fraction) \r\n 将以分数表示的货币值转换为以小数表示的货币值。");

            DollarFrFunctionInfo dollarfr = new DollarFrFunctionInfo();
            FunctionInfos["财务"].Add(dollarfr);
            FunctionInfos["全部"].Add(dollarfr);
            FunctionDescriptions.Add(dollarfr.Name, "DOLLARFR(decimaldollar,fraction) \r\n 将以小数表示的货币值转换为以分数表示的货币值。");

            DurationFunctionInfo duration = new DurationFunctionInfo();
            FunctionInfos["财务"].Add(duration);
            FunctionInfos["全部"].Add(duration);
            FunctionDescriptions.Add(duration.Name, "DURATION(settlement,maturity,coupon,yield,frequency,basis) \r\n 返回定期支付利息的债权的年持续时间。");

            EffectFunctionInfo effect = new EffectFunctionInfo();
            FunctionInfos["财务"].Add(effect);
            FunctionInfos["全部"].Add(effect);
            FunctionDescriptions.Add(effect.Name, "EFFECT(nomrate,comper) \r\n 返回年有效利率。");

            EuroFunctionInfo euro = new EuroFunctionInfo();
            FunctionInfos["财务"].Add(euro);
            FunctionInfos["全部"].Add(euro);
            FunctionDescriptions.Add(euro.Name, "EURO(code) \r\n 返回的 1 欧元的等价欧元成员国货币。");

            EuroConvertFunctionInfo euroconvert = new EuroConvertFunctionInfo();
            FunctionInfos["财务"].Add(euroconvert);
            FunctionInfos["全部"].Add(euroconvert);
            FunctionDescriptions.Add(euroconvert.Name, "EUROCONVERT(currency,source,target,fullprecision,triangulation) \r\n 将欧元货币转换成欧元成员国的货币。");

            FvFunctionInfo fv = new FvFunctionInfo();
            FunctionInfos["财务"].Add(fv);
            FunctionInfos["全部"].Add(fv);
            FunctionDescriptions.Add(fv.Name, "FV(rate,numper,paymt,pval,type) \r\n 基于固定利率和等额分期付款方式，返回某项投资的未来值。");

            FvScheduleFunctionInfo fvschedule = new FvScheduleFunctionInfo();
            FunctionInfos["财务"].Add(fvschedule);
            FunctionInfos["全部"].Add(fvschedule);
            FunctionDescriptions.Add(fvschedule.Name, "FVSCHEDULE(principal,schedule) \r\n 返回在应用一系列复利后，初始本金的终值。");

            IntRateFunctionInfo intrate = new IntRateFunctionInfo();
            FunctionInfos["财务"].Add(intrate);
            FunctionInfos["全部"].Add(intrate);
            FunctionDescriptions.Add(intrate.Name, "INTRATE(settle,mature,invest,redeem,basis) \r\n 返回完全投资型债权的利率。");

            IpmtFunctionInfo ipmt = new IpmtFunctionInfo();
            FunctionInfos["财务"].Add(ipmt);
            FunctionInfos["全部"].Add(ipmt);
            FunctionDescriptions.Add(ipmt.Name, "IPMT(rate,per,nper,pval,fval,type) \r\n 返回在定期偿还、固定利率条件下给定期次内某项投资回报(或贷款偿还)的利率部分。");

            IrrFunctionInfo irr = new IrrFunctionInfo();
            FunctionInfos["财务"].Add(irr);
            FunctionInfos["全部"].Add(irr);
            FunctionDescriptions.Add(irr.Name, "IRR(arrayvals,estimate) \r\n 返回一系列现金流的内部报酬率。");

            IspmtFunctionInfo ispmt = new IspmtFunctionInfo();
            FunctionInfos["财务"].Add(ispmt);
            FunctionInfos["全部"].Add(ispmt);
            FunctionDescriptions.Add(ispmt.Name, "ISPMT(rate,per,nper,pv) \r\n 返回普通(无担保)贷款的利息偿还。");

            MDurationFunctionInfo mduration = new MDurationFunctionInfo();
            FunctionInfos["财务"].Add(mduration);
            FunctionInfos["全部"].Add(mduration);
            FunctionDescriptions.Add(mduration.Name, "MDURATION(settlement,maturity,coupon,yield,frequency,basis) \r\n 为假定票面值为 100 元的债权返回麦考利修正持续时间。");

            MIrrFunctionInfo mirr = new MIrrFunctionInfo();
            FunctionInfos["财务"].Add(mirr);
            FunctionInfos["全部"].Add(mirr);
            FunctionDescriptions.Add(mirr.Name, "MIRR(arrayvals,payment_int,income_int) \r\n 返回在考虑投资成本以及现金再投资利率下一系列分期现金流的内部报酬率。");

            NominalFunctionInfo nominal = new NominalFunctionInfo();
            FunctionInfos["财务"].Add(nominal);
            FunctionInfos["全部"].Add(nominal);
            FunctionDescriptions.Add(nominal.Name, "NOMINAL(effrate,comper) \r\n 返回年度的单利。");

            NPerFunctionInfo nper = new NPerFunctionInfo();
            FunctionInfos["财务"].Add(nper);
            FunctionInfos["全部"].Add(nper);
            FunctionDescriptions.Add(nper.Name, "NPER(rate,paymt,pval,fval,type) \r\n 基于固定利率和等额分期付款方式，返回某项投资或贷款的期数。");

            NpvFunctionInfo npv = new NpvFunctionInfo();
            FunctionInfos["财务"].Add(npv);
            FunctionInfos["全部"].Add(npv);
            FunctionDescriptions.Add(npv.Name, "NPV(discount,value1,value2,...) \r\n 基于一系列将来的收(正值)支(负值)现金流和一贴现率，返回一项投资的净现值。");

            OddFPriceFunctionInfo oddfprice = new OddFPriceFunctionInfo();
            FunctionInfos["财务"].Add(oddfprice);
            FunctionInfos["全部"].Add(oddfprice);
            FunctionDescriptions.Add(oddfprice.Name, "ODDFPRICE(settle,maturity,issue,first,rate,yield,redeem,freq,basis) \r\n 返回每张票面为 100 元且第一期为奇数的债券的现价。");

            OddFYieldFunctionInfo oddfyield = new OddFYieldFunctionInfo();
            FunctionInfos["财务"].Add(oddfyield);
            FunctionInfos["全部"].Add(oddfyield);
            FunctionDescriptions.Add(oddfyield.Name, "ODDFYIELD(settle,maturity,issue,first,rate,price,redeem,freq,basis) \r\n 返回第一期为奇数的债券的收益。");

            OddLPriceFunctionInfo oddlprice = new OddLPriceFunctionInfo();
            FunctionInfos["财务"].Add(oddlprice);
            FunctionInfos["全部"].Add(oddlprice);
            FunctionDescriptions.Add(oddlprice.Name, "ODDLPRICE(settle,maturity,last,rate,yield,redeem,freq,basis) \r\n 返回每张票面为 100 元且最后一期为奇数的债券的现价。");

            OddLYieldFunctionInfo oddlyield = new OddLYieldFunctionInfo();
            FunctionInfos["财务"].Add(oddlyield);
            FunctionInfos["全部"].Add(oddlyield);
            FunctionDescriptions.Add(oddlyield.Name, "ODDLYIELD(settle,maturity,last,rate,price,redeem,freq,basis) \r\n 返回最后一期为奇数的债券的收益。");

            PmtFunctionInfo pmt = new PmtFunctionInfo();
            FunctionInfos["财务"].Add(pmt);
            FunctionInfos["全部"].Add(pmt);
            FunctionInfos["常用函数"].Add(pmt);
            FunctionDescriptions.Add(pmt.Name, "PMT(rate,nper,pval,fval,type) \r\n 计算在固定利率下，贷款的等额分期偿还额。");

            PpmtFunctionInfo ppmt = new PpmtFunctionInfo();
            FunctionInfos["财务"].Add(ppmt);
            FunctionInfos["全部"].Add(ppmt);
            FunctionDescriptions.Add(ppmt.Name, "PPMT(rate,per,nper,pval,fval,type) \r\n 返回在定期偿还、固定利率条件下给定期次内某项投资回报(或贷款偿还)的本金部分。");

            PriceFunctionInfo price = new PriceFunctionInfo();
            FunctionInfos["财务"].Add(price);
            FunctionInfos["全部"].Add(price);
            FunctionDescriptions.Add(price.Name, "PRICE(settlement,maturity,rate,yield,redeem,frequency,basis) \r\n 返回每张票面为 100 元且定期支付利息的债券的现价。");

            PriceDiscFunctionInfo pricedisc = new PriceDiscFunctionInfo();
            FunctionInfos["财务"].Add(pricedisc);
            FunctionInfos["全部"].Add(pricedisc);
            FunctionDescriptions.Add(pricedisc.Name, "PRICEDISC(settle,mature,discount,redeem,basis) \r\n 返回每张票面为 100 元的已贴现债券的现价。");

            PriceMatFunctionInfo pricemat = new PriceMatFunctionInfo();
            FunctionInfos["财务"].Add(pricemat);
            FunctionInfos["全部"].Add(pricemat);
            FunctionDescriptions.Add(pricemat.Name, "PRICEMAT(settle,mature,issue,rate,yield,basis) \r\n 返回每张票面为 100 元且在到期日支付利息的债券的现价。");

            PvFunctionInfo pv = new PvFunctionInfo();
            FunctionInfos["财务"].Add(pv);
            FunctionInfos["全部"].Add(pv);
            FunctionDescriptions.Add(pv.Name, "PV(rate,numper,paymt,fval,type) \r\n 返回某项投资的一系列将来偿还额的当前总值(或一次偿还额的现值)。");

            RateFunctionInfo rate = new RateFunctionInfo();
            FunctionInfos["财务"].Add(rate);
            FunctionInfos["全部"].Add(rate);
            FunctionDescriptions.Add(rate.Name, "RATE(nper,pmt,pval,fval,type,guess) \r\n 返回投资或贷款的每期实际利率。例如，当利率为 6% 时，使用 6%/4计算一个季度的还款额");

            ReceivedFunctionInfo received = new ReceivedFunctionInfo();
            FunctionInfos["财务"].Add(received);
            FunctionInfos["全部"].Add(received);
            FunctionDescriptions.Add(received.Name, "RECEIVED(settle,mature,invest,discount,basis) \r\n 返回完全投资型债券在到期日收回的金额。");

            SlnFunctionInfo sln = new SlnFunctionInfo();
            FunctionInfos["财务"].Add(sln);
            FunctionInfos["全部"].Add(sln);
            FunctionDescriptions.Add(sln.Name, "SLN(cost,salvage,life) \r\n 返回固定资产的每期线性折旧费。");

            SydFunctionInfo syd = new SydFunctionInfo();
            FunctionInfos["财务"].Add(syd);
            FunctionInfos["全部"].Add(syd);
            FunctionDescriptions.Add(syd.Name, "SYD(cost,salvage,life,period) \r\n 返回某项固定资产按年限总和折旧法计算的每期折旧资金。");

            TBillEqFunctionInfo tbilleq = new TBillEqFunctionInfo();
            FunctionInfos["财务"].Add(tbilleq);
            FunctionInfos["全部"].Add(tbilleq);
            FunctionDescriptions.Add(tbilleq.Name, "TBILLEQ(settle,mature,discount) \r\n 返回短期国库券的等价债券收益。");

            TBillPriceFunctionInfo tbillprice = new TBillPriceFunctionInfo();
            FunctionInfos["财务"].Add(tbillprice);
            FunctionInfos["全部"].Add(tbillprice);
            FunctionDescriptions.Add(tbillprice.Name, "TBILLPRICE(settle,mature,discount) \r\n 返回每张票面为 100 元的短期国库券的现价。");

            TBillYieldFunctionInfo tbillyield = new TBillYieldFunctionInfo();
            FunctionInfos["财务"].Add(tbillyield);
            FunctionInfos["全部"].Add(tbillyield);
            FunctionDescriptions.Add(tbillyield.Name, "TBILLYIELD(settle,mature,priceper) \r\n 返回短期国库券的收益。");

            VdbFunctionInfo vdb = new VdbFunctionInfo();
            FunctionInfos["财务"].Add(vdb);
            FunctionInfos["全部"].Add(vdb);
            FunctionDescriptions.Add(vdb.Name, "VDB(cost,salvage,life,start,end,factor,switchnot) \r\n 返回某项固定资产用余额递减法或其他指定方法计算的特定或部分时期的折旧额。");

            XirrFunctionInfo xirr = new XirrFunctionInfo();
            FunctionInfos["财务"].Add(xirr);
            FunctionInfos["全部"].Add(xirr);
            FunctionDescriptions.Add(xirr.Name, "XIRR(values,dates,guess) \r\n 返回现金流计划的内部回报率。");

            XnpvFunctionInfo xnpv = new XnpvFunctionInfo();
            FunctionInfos["财务"].Add(xnpv);
            FunctionInfos["全部"].Add(xnpv);
            FunctionDescriptions.Add(xnpv.Name, "XNPV(rate,values,dates) \r\n 返回现金流计划的净现值。");

            YieldFunctionInfo yield = new YieldFunctionInfo();
            FunctionInfos["财务"].Add(yield);
            FunctionInfos["全部"].Add(yield);
            FunctionDescriptions.Add(yield.Name, "YIELD(settle,maturity,rate,price,redeem,frequency,basis) \r\n 返回定期支付利息的债券的收益。");

            YieldDiscFunctionInfo yielddisc = new YieldDiscFunctionInfo();
            FunctionInfos["财务"].Add(yielddisc);
            FunctionInfos["全部"].Add(yielddisc);
            FunctionDescriptions.Add(yielddisc.Name, "YIELDDISC(settle,maturity,price,redeem,basis) \r\n 返回已贴现债券的年收益，如短期国库券。");

            YieldMatFunctionInfo yieldmat = new YieldMatFunctionInfo();
            FunctionInfos["财务"].Add(yieldmat);
            FunctionInfos["全部"].Add(yieldmat);
            FunctionDescriptions.Add(yieldmat.Name, "YIELDMAT(settle,maturity,issue,issrate,price,basis) \r\n 返回在到期日支付利息的债券的年收益。");

            //信息类
            CountBlankFunctionInfo countblank = new CountBlankFunctionInfo();
            FunctionInfos["信息"].Add(countblank);
            FunctionInfos["全部"].Add(countblank);
            FunctionDescriptions.Add(countblank.Name, "COUNTBLANK(cellrange) \r\n 返回数值为 null 的单元格的个数。");

            ErrorTypeFunctionInfo errortype = new ErrorTypeFunctionInfo();
            FunctionInfos["信息"].Add(errortype);
            FunctionInfos["全部"].Add(errortype);
            FunctionDescriptions.Add(errortype.Name, "ERRORTYPE(errorvalue) \r\n 返回与错误值对应的数字。");

            IsBlankFunctionInfo isblank = new IsBlankFunctionInfo();
            FunctionInfos["信息"].Add(isblank);
            FunctionInfos["全部"].Add(isblank);
            FunctionDescriptions.Add(isblank.Name, "ISBLANK(value) \r\n 检查是否引用了空单元格，返回 TRUE 或 FALSE。");

            IsErrFunctionInfo iserr = new IsErrFunctionInfo();
            FunctionInfos["信息"].Add(iserr);
            FunctionInfos["全部"].Add(iserr);
            FunctionDescriptions.Add(iserr.Name, "ISERR(value) \r\n 检查一个值是否为 #N/A 以外的错误(#VALUE!、#REF!、#DIV/0!、#NUM!、#NAME? 或 #NULL!)，返回 TRUE 或 FALSE。");

            IsErrorFunctionInfo iserror = new IsErrorFunctionInfo();
            FunctionInfos["信息"].Add(iserror);
            FunctionInfos["全部"].Add(iserror);
            FunctionDescriptions.Add(iserror.Name, "ISERROR(value) \r\n 检查一个值是否为错误(#N/A、#VALUE!、#REF!、#DIV/0!、#NUM!、#NAME? 或 #NULL!)，返回 TRUE 或 FALSE。");

            IsEvenFunctionInfo iseven = new IsEvenFunctionInfo();
            FunctionInfos["信息"].Add(iseven);
            FunctionInfos["全部"].Add(iseven);
            FunctionDescriptions.Add(iseven.Name, "ISEVEN(value) \r\n 如果数字为偶数则返回 TRUE。");

            IsLogicalFunctionInfo islogical = new IsLogicalFunctionInfo();
            FunctionInfos["信息"].Add(islogical);
            FunctionInfos["全部"].Add(islogical);
            FunctionDescriptions.Add(islogical.Name, "ISLOGICAL(value) \r\n 检查一个值是否是逻辑值(TRUE 或 FALSE)，返回 TRUE 或 FALSE。");

            IsNAFunctionInfo isna = new IsNAFunctionInfo();
            FunctionInfos["信息"].Add(isna);
            FunctionInfos["全部"].Add(isna);
            FunctionDescriptions.Add(isna.Name, "ISNA(value) \r\n 检查一个值是否为 #N/A，返回 TRUE 或 FALSE。");

            IsNonTextFunctionInfo isnontext = new IsNonTextFunctionInfo();
            FunctionInfos["信息"].Add(isnontext);
            FunctionInfos["全部"].Add(isnontext);
            FunctionDescriptions.Add(isnontext.Name, "ISNONTEXT(value) \r\n 检查一个值是否不是文本(空单元格不是文本)，返回 TRUE 或 FALSE。");

            IsNumberFunctionInfo isnumber = new IsNumberFunctionInfo();
            FunctionInfos["信息"].Add(isnumber);
            FunctionInfos["全部"].Add(isnumber);
            FunctionDescriptions.Add(isnumber.Name, "ISNUMBER(value) \r\n 检查一个值是否是数值，返回 TRUE 或 FALSE。");

            IsOddFunctionInfo isodd = new IsOddFunctionInfo();
            FunctionInfos["信息"].Add(isodd);
            FunctionInfos["全部"].Add(isodd);
            FunctionDescriptions.Add(isodd.Name, "ISODD(value) \r\n 如果数字为奇数则返回 TRUE。");

            IsRefFunctionInfo isref = new IsRefFunctionInfo();
            FunctionInfos["信息"].Add(isref);
            FunctionInfos["全部"].Add(isref);
            FunctionDescriptions.Add(isref.Name, "ISREF(value) \r\n 如果一个值是否为引用，返回 TRUE 或 FALSE。");

            IsTextFunctionInfo istext = new IsTextFunctionInfo();
            FunctionInfos["信息"].Add(istext);
            FunctionInfos["全部"].Add(istext);
            FunctionDescriptions.Add(istext.Name, "ISTEXT(value) \r\n 检测一个值是否为文本，返回 TRUE 或 FALSE。");

            NFunctionInfo n = new NFunctionInfo();
            FunctionInfos["信息"].Add(n);
            FunctionInfos["全部"].Add(n);
            FunctionDescriptions.Add(n.Name, "N(value) \r\n 将不是数值形式的值转换为数值形式。日期转换成序列值，TRUE 转换为 1，其他值转换为 0");

            NAFunctionInfo na = new NAFunctionInfo();
            FunctionInfos["信息"].Add(na);
            FunctionInfos["全部"].Add(na);
            FunctionDescriptions.Add(na.Name, "NA() \r\n 返回错误值 #N/A (无法计算出数值)");

            TypeFunctionInfo type = new TypeFunctionInfo();
            FunctionInfos["信息"].Add(type);
            FunctionInfos["全部"].Add(type);
            FunctionDescriptions.Add(type.Name, "TYPE(value) \r\n 以整数形式返回参数的数据类型:数值=1；文字=2；逻辑值=4；错误值=16；数组=64");

            //查找类
            AddressFunctionInfo address = new AddressFunctionInfo();
            FunctionInfos["查找"].Add(address);
            FunctionInfos["全部"].Add(address);
            FunctionDescriptions.Add(address.Name, "ADDRESS(row,column,absnum,a1style,sheettext) \r\n 创建一个以文本方式对工作簿中某一单元格的引用。");

            ChooseFunctionInfo choose = new ChooseFunctionInfo();
            FunctionInfos["查找"].Add(choose);
            FunctionInfos["全部"].Add(choose);
            FunctionDescriptions.Add(choose.Name, "CHOOSE(index,value1,value2,...) \r\n 根据给定的索引值，从参数串中选出相应值或操作。");

            ColumnFunctionInfo column = new ColumnFunctionInfo();
            FunctionInfos["查找"].Add(column);
            FunctionInfos["全部"].Add(column);
            FunctionDescriptions.Add(column.Name, "COLUMN(reference) \r\n 返回一引用的列号。");

            ColumnsFunctionInfo columns = new ColumnsFunctionInfo();
            FunctionInfos["查找"].Add(columns);
            FunctionInfos["全部"].Add(columns);
            FunctionDescriptions.Add(columns.Name, "COLUMNS(array) \r\n 返回某一引用或数组的列数。");

            HLookupFunctionInfo hlookup = new HLookupFunctionInfo();
            FunctionInfos["查找"].Add(hlookup);
            FunctionInfos["全部"].Add(hlookup);
            FunctionDescriptions.Add(hlookup.Name, "HLOOKUP(value,array,row,approx) \r\n 搜素数组区域首行满足条件的元素，确定待检索单元格在区域中的列序号，再进一步返回选定单元格的值。");

            IndexFunctionInfo index = new IndexFunctionInfo();
            FunctionInfos["查找"].Add(index);
            FunctionInfos["全部"].Add(index);
            FunctionDescriptions.Add(index.Name, "INDEX(return,row,col,area) \r\n 在给定的单元格区域中，返回特定行列交叉处的单元格的值或引用。");

            LookupFunctionInfo lookup = new LookupFunctionInfo();
            FunctionInfos["查找"].Add(lookup);
            FunctionInfos["全部"].Add(lookup);
            FunctionDescriptions.Add(lookup.Name, "LOOKUP(lookupvalue,lookupvector,resultvector) \r\n 从单行或单列或从数组中查找一个值，条件是向后兼容性。");

            OffsetFunctionInfo offset = new OffsetFunctionInfo();
            FunctionInfos["查找"].Add(offset);
            FunctionInfos["全部"].Add(offset);
            FunctionDescriptions.Add(offset.Name, "OFFSET(reference,rows,cols,height,width) \r\n 以指定的引用为参照系，通过给定偏移量返回新的引用。");

            RowFunctionInfo row = new RowFunctionInfo();
            FunctionInfos["查找"].Add(row);
            FunctionInfos["全部"].Add(row);
            FunctionDescriptions.Add(row.Name, "ROW(reference) \r\n 返回一个引用的行号。");

            RowsFunctionInfo rows = new RowsFunctionInfo();
            FunctionInfos["查找"].Add(rows);
            FunctionInfos["全部"].Add(rows);
            FunctionDescriptions.Add(rows.Name, "ROWS(array) \r\n 返回某一引用或数组的行数。");

            TransposeFunctionInfo transpose = new TransposeFunctionInfo();
            FunctionInfos["查找"].Add(transpose);
            FunctionInfos["全部"].Add(transpose);
            FunctionDescriptions.Add(transpose.Name, "TRANSPOSE(array) \r\n 转置单元格区域。");

            VLookupFunctionInfo vloopup = new VLookupFunctionInfo();
            FunctionInfos["查找"].Add(vloopup);
            FunctionInfos["全部"].Add(vloopup);
            FunctionDescriptions.Add(vloopup.Name, "VLOOKUP(value,array,colindex,approx) \r\n 搜素数组区域首列满足条件的元素，确定待检索单元格在区域中的行序号，再进一步返回选定单元格的值。默认情况下，表是以升序排列的");

            //逻辑类
            AndFunctionInfo and = new AndFunctionInfo();
            FunctionInfos["逻辑"].Add(and);
            FunctionInfos["全部"].Add(and);
            FunctionDescriptions.Add(and.Name, "AND(expression1,expression2,...) \r\n 搜素数组区域首列满足条件的元素，确定待检索单元格在区域中的行序号，再进一步返回选定单元格的值。默认情况下，表是以升序排列的");

            FalseFunctionInfo False = new FalseFunctionInfo();
            FunctionInfos["逻辑"].Add(False);
            FunctionInfos["全部"].Add(False);
            FunctionDescriptions.Add(False.Name, "FALSE() \r\n 返回逻辑值 FALSE。");

            IfFunctionInfo If = new IfFunctionInfo();
            FunctionInfos["逻辑"].Add(If);
            FunctionInfos["全部"].Add(If);
            FunctionInfos["常用函数"].Add(If);
            FunctionDescriptions.Add(If.Name, "IF(valueTest,valueTrue,valueFalse) \r\n 判断是否满足某个条件，如果满足返回一个值，如果不满足则返回另一个值。");

            NotFunctionInfo Not = new NotFunctionInfo();
            FunctionInfos["逻辑"].Add(Not);
            FunctionInfos["全部"].Add(Not);
            FunctionDescriptions.Add(Not.Name, "NOT(value) \r\n 对参数的逻辑值求反:参数为 TRUE 则返回 FALSE；参数为 FALSE 则返回 TRUE。");

            OrFunctionInfo Or = new OrFunctionInfo();
            FunctionInfos["逻辑"].Add(Or);
            FunctionInfos["全部"].Add(Or);
            FunctionDescriptions.Add(Or.Name, "OR(expression1,expression2,...) \r\n 如果任一参数值为 TRUE，即返回 TRUE；只有当所有参数值均为 FALSE 时才返回 FALSE。");

            TrueFunctionInfo True = new TrueFunctionInfo();
            FunctionInfos["逻辑"].Add(True);
            FunctionInfos["全部"].Add(True);
            FunctionDescriptions.Add(True.Name, "TRUE() \r\n 返回逻辑值 TRUE。");

            //数学与三角类
            AbsFunctionInfo Abs = new AbsFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Abs);
            FunctionInfos["全部"].Add(Abs);
            FunctionDescriptions.Add(Abs.Name, "ABS(expression) \r\n 返回给定数值的绝对值，即不带符号的数值。");

            AcosFunctionInfo Acos = new AcosFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Acos);
            FunctionInfos["全部"].Add(Acos);
            FunctionDescriptions.Add(Acos.Name, "ACOS(value) \r\n 返回一个弧度的反余弦。弧度值在 0 到 Pi 之间。反余弦值是指余弦值为 value 的角度");

            AcoshFunctionInfo Acosh = new AcoshFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Acosh);
            FunctionInfos["全部"].Add(Acosh);
            FunctionDescriptions.Add(Acosh.Name, "ACOSH(value) \r\n 返回反双曲余弦值。");

            AsinFunctionInfo Asin = new AsinFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Asin);
            FunctionInfos["全部"].Add(Asin);
            FunctionDescriptions.Add(Asin.Name, "ASIN(value) \r\n 返回一个弧度的反正弦。弧度值在 -Pi/2 到 Pi/2 之间");

            AsinhFunctionInfo Asinh = new AsinhFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Asinh);
            FunctionInfos["全部"].Add(Asinh);
            FunctionDescriptions.Add(Asinh.Name, "ASINH(value) \r\n 返回反双曲正弦值");

            AtanFunctionInfo Atan = new AtanFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Atan);
            FunctionInfos["全部"].Add(Atan);
            FunctionDescriptions.Add(Atan.Name, "ATAN(value) \r\n 返回反正切值。以弧度表示，大小在 -Pi/2 到 Pi/2 之间");

            Atan2FunctionInfo Atan2 = new Atan2FunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Atan2);
            FunctionInfos["全部"].Add(Atan);
            FunctionDescriptions.Add(Atan2.Name, "ATAN2(x,y) \r\n 根据给定的 x 轴及 y 轴的坐标值，返回反正切值。返回值在 -Pi 到 Pi 之间(不包括 -Pi)");

            AtanhFunctionInfo Atanh = new AtanhFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Atanh);
            FunctionInfos["全部"].Add(Atanh);
            FunctionDescriptions.Add(Atanh.Name, "ATANH(value) \r\n 返回反双曲正切值。");

            CeilingFunctionInfo Ceiling = new CeilingFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Ceiling);
            FunctionInfos["全部"].Add(Ceiling);
            FunctionDescriptions.Add(Ceiling.Name, "CEILING(value,signif) \r\n 将参数向上舍入为最接近的整数，或最接近的指定基数的倍数。");

            CombinFunctionInfo Combin = new CombinFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Combin);
            FunctionInfos["全部"].Add(Combin);
            FunctionDescriptions.Add(Combin.Name, "COMBIN(k,n) \r\n 返回从给定元素数目的集合中提取若干元素的组合数。");

            CosFunctionInfo Cos = new CosFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Cos);
            FunctionInfos["全部"].Add(Cos);
            FunctionDescriptions.Add(Cos.Name, "COS(angle) \r\n 返回给定角度的余弦值。");

            CoshFunctionInfo Cosh = new CoshFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Cosh);
            FunctionInfos["全部"].Add(Cosh);
            FunctionDescriptions.Add(Cosh.Name, "COSH(value) \r\n 返回双曲余弦值。");

            CountIfFunctionInfo CountIf = new CountIfFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(CountIf);
            FunctionInfos["全部"].Add(CountIf);
            FunctionDescriptions.Add(CountIf.Name, "COUNTIF(cellrange,condition) \r\n 返回满足指定条件的单元格的个数。");

            DegreesFunctionInfo Degrees = new DegreesFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Degrees);
            FunctionInfos["全部"].Add(Degrees);
            FunctionDescriptions.Add(Degrees.Name, "DEGREES(angle) \r\n 将弧度转换为角度。");

            EvenFunctionInfo Even = new EvenFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Even);
            FunctionInfos["全部"].Add(Even);
            FunctionDescriptions.Add(Even.Name, "EVEN(value) \r\n 将正数向上舍入到最近的偶数，负数向下舍入到最近的偶数。");

            ExpFunctionInfo Exp = new ExpFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Exp);
            FunctionInfos["全部"].Add(Exp);
            FunctionDescriptions.Add(Exp.Name, "EXP(value) \r\n 返回 e 的 n 次方。");

            FactFunctionInfo Fact = new FactFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Fact);
            FunctionInfos["全部"].Add(Fact);
            FunctionDescriptions.Add(Fact.Name, "FACT(number) \r\n 返回某数的阶乘，等于1*2*...*number。");

            FactDoubleFunctionInfo FactDouble = new FactDoubleFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(FactDouble);
            FunctionInfos["全部"].Add(FactDouble);
            FunctionDescriptions.Add(FactDouble.Name, "FACTDOUBLE(number) \r\n 返回数字的双阶乘。");

            FloorFunctionInfo Floor = new FloorFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Floor);
            FunctionInfos["全部"].Add(Floor);
            FunctionDescriptions.Add(Floor.Name, "FLOOR(value,signif) \r\n 按指定基数进行向下舍入计算。");

            GcdFunctionInfo Gcd = new GcdFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Gcd);
            FunctionInfos["全部"].Add(Gcd);
            FunctionDescriptions.Add(Gcd.Name, "GCD(number1,number2) \r\n 返回最大公约数。");

            IntFunctionInfo Int = new IntFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Int);
            FunctionInfos["全部"].Add(Int);
            FunctionDescriptions.Add(Int.Name, "INT(value) \r\n 将数值向下取整为最接近的整数。");

            LcmFunctionInfo Lcm = new LcmFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Lcm);
            FunctionInfos["全部"].Add(Lcm);
            FunctionDescriptions.Add(Lcm.Name, "LCM(number1,number2) \r\n 返回最小公倍数。");

            LnFunctionInfo Ln = new LnFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Ln);
            FunctionInfos["全部"].Add(Ln);
            FunctionDescriptions.Add(Ln.Name, "LN(value) \r\n 返回给定数值的自然对数。");

            LogFunctionInfo Log = new LogFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Log);
            FunctionInfos["全部"].Add(Log);
            FunctionDescriptions.Add(Log.Name, "LOG(number,base) \r\n 根据给定底数返回数字的对数。");

            Log10FunctionInfo Log10 = new Log10FunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Log10);
            FunctionInfos["全部"].Add(Log10);
            FunctionDescriptions.Add(Log10.Name, "LOG10(value) \r\n 返回给定数值以 10 为底的对数。");

            MDetermFunctionInfo MDeterm = new MDetermFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(MDeterm);
            FunctionInfos["全部"].Add(MDeterm);
            FunctionDescriptions.Add(MDeterm.Name, "MDETERM(array) \r\n 返回一数组所代表的矩阵行列式的值。");

            MInverseFunctionInfo MInverse = new MInverseFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(MInverse);
            FunctionInfos["全部"].Add(MInverse);
            FunctionDescriptions.Add(MInverse.Name, "MINVERSE(array) \r\n 返回一数组所代表的矩阵的逆矩阵。");

            MMultFunctionInfo MMult = new MMultFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(MMult);
            FunctionInfos["全部"].Add(MMult);
            FunctionDescriptions.Add(MMult.Name, "MMULT(array1,array2) \r\n 返回两数组的矩阵积，结果矩阵的行数与 array1 相等，列数与 array2 相等。");

            ModFunctionInfo Mod = new ModFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Mod);
            FunctionInfos["全部"].Add(Mod);
            FunctionDescriptions.Add(Mod.Name, "MOD(dividend,divisor) \r\n 返回两数相除的余数。");

            MRoundFunctionInfo MRound = new MRoundFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(MRound);
            FunctionInfos["全部"].Add(MRound);
            FunctionDescriptions.Add(MRound.Name, "MROUND(number,multiple) \r\n 返回一个舍入到所需倍数的数字。");

            MultinomialFunctionInfo Multinomial = new MultinomialFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Multinomial);
            FunctionInfos["全部"].Add(Multinomial);
            FunctionDescriptions.Add(Multinomial.Name, "MULTINOMIAL(value1,value2,...) \r\n 返回一组数字的多项式。");

            OddFunctionInfo Odd = new OddFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Odd);
            FunctionInfos["全部"].Add(Odd);
            FunctionDescriptions.Add(Odd.Name, "ODD(value) \r\n 将正(负)数向上(下)舍入到最接近的奇数。");

            PiFunctionInfo Pi = new PiFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Pi);
            FunctionInfos["全部"].Add(Pi);
            FunctionDescriptions.Add(Pi.Name, "PI() \r\n 返回圆周率 Pi 的值，3.1415926536，精确到第11位。");

            PowerFunctionInfo Power = new PowerFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Power);
            FunctionInfos["全部"].Add(Power);
            FunctionDescriptions.Add(Power.Name, "POWER(number,power) \r\n 返回某数的乘幂。");

            ProductFunctionInfo Product = new ProductFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Product);
            FunctionInfos["全部"].Add(Product);
            FunctionDescriptions.Add(Product.Name, "PRODUCT(value1,value2,...) \r\n 计算所有参数的乘积。");

            QuotientFunctionInfo Quotient = new QuotientFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Quotient);
            FunctionInfos["全部"].Add(Quotient);
            FunctionDescriptions.Add(Quotient.Name, "QUOTIENT(numerator,denominator) \r\n 返回除法的整数部分。");

            RadiansFunctionInfo Radians = new RadiansFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Radians);
            FunctionInfos["全部"].Add(Radians);
            FunctionDescriptions.Add(Radians.Name, "RADIANS(value) \r\n 将角度转为弧度。");

            RandFunctionInfo Rand = new RandFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Rand);
            FunctionInfos["全部"].Add(Rand);
            FunctionDescriptions.Add(Rand.Name, "RAND() \r\n 返回大于或等于 0 且小于 1 的平均分布随机数(依重新计算而变)。");

            RandBetweenFunctionInfo RandBetween = new RandBetweenFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(RandBetween);
            FunctionInfos["全部"].Add(RandBetween);
            FunctionDescriptions.Add(RandBetween.Name, "RANDBETWEEN(lower,upper) \r\n 返回一个介于指定的数字之间的随机数。");

            RomanFunctionInfo Roman = new RomanFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Roman);
            FunctionInfos["全部"].Add(Roman);
            FunctionDescriptions.Add(Roman.Name, "ROMAN(number,style) \r\n 将阿拉伯数字转换成文本式罗马数字。");

            RoundFunctionInfo Round = new RoundFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Round);
            FunctionInfos["全部"].Add(Round);
            FunctionDescriptions.Add(Round.Name, "ROUND(value,places) \r\n 按指定的位数对数值进行四舍五入。");

            RoundDownFunctionInfo RoundDown = new RoundDownFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(RoundDown);
            FunctionInfos["全部"].Add(RoundDown);
            FunctionDescriptions.Add(RoundDown.Name, "ROUNDDOWN(value,places) \r\n 向下舍入数字。");

            RoundUpFunctionInfo RoundUp = new RoundUpFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(RoundUp);
            FunctionInfos["全部"].Add(RoundUp);
            FunctionDescriptions.Add(RoundUp.Name, "ROUNDUP(value,places) \r\n 向上舍入数字。");

            SeriesSumFunctionInfo SeriesSum = new SeriesSumFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(SeriesSum);
            FunctionInfos["全部"].Add(SeriesSum);
            FunctionDescriptions.Add(SeriesSum.Name, "SERIESSUM(x,n,m,coeff) \r\n 返回基于公式的幂级数的和。");

            SignFunctionInfo Sign = new SignFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Sign);
            FunctionInfos["全部"].Add(Sign);
            FunctionDescriptions.Add(Sign.Name, "SIGN(value) \r\n 返回数字的正负号:为正时，返回 1；为零时，返回 0；为负时，返回 -1。");

            SinFunctionInfo Sin = new SinFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Sin);
            FunctionInfos["全部"].Add(Sin);
            FunctionInfos["常用函数"].Add(Sin);
            FunctionDescriptions.Add(Sin.Name, "SIN(angle) \r\n 返回给定角度的正弦值。");

            SinhFunctionInfo Sinh = new SinhFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Sinh);
            FunctionInfos["全部"].Add(Sinh);
            FunctionDescriptions.Add(Sinh.Name, "SINH(value) \r\n 返回双曲正弦值。");

            SqrtFunctionInfo Sqrt = new SqrtFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Sqrt);
            FunctionInfos["全部"].Add(Sqrt);
            FunctionDescriptions.Add(Sqrt.Name, "SQRT(value) \r\n 返回数值的平方根。");

            SqrtPiFunctionInfo SqrtPi = new SqrtPiFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(SqrtPi);
            FunctionInfos["全部"].Add(SqrtPi);
            FunctionDescriptions.Add(SqrtPi.Name, "SQRTPI(multiple) \r\n 返回(数字 * Pi)的平方根。");

            SubtotalFunctionInfo Subtotal = new SubtotalFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Subtotal);
            FunctionInfos["全部"].Add(Subtotal);
            FunctionDescriptions.Add(Subtotal.Name, "SUBTOTAL(functioncode,value1,value2,...) \r\n 返回一个数据列表或数据库的分类汇总。");

            SumFunctionInfo Sum = new SumFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Sum);
            FunctionInfos["全部"].Add(Sum);
            FunctionInfos["常用函数"].Add(Sum);
            FunctionDescriptions.Add(Sum.Name, "SUM(value1,value2,...) \r\n 返回单元格区域中所有数值的和。");

            SumIfFunctionInfo SumIf = new SumIfFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(SumIf);
            FunctionInfos["全部"].Add(SumIf);
            FunctionInfos["常用函数"].Add(SumIf);
            FunctionDescriptions.Add(SumIf.Name, "SUMIF(array,condition,sumrange) \r\n 对满足条件的单元格求和。");

            SumProductFunctionInfo SumProduct = new SumProductFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(SumProduct);
            FunctionInfos["全部"].Add(SumProduct);
            FunctionDescriptions.Add(SumProduct.Name, "SUMPRODUCT(array1,array2,...) \r\n 返回相应的数组区域乘积的和。");

            SumSqFunctionInfo SumSq = new SumSqFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(SumSq);
            FunctionInfos["全部"].Add(SumSq);
            FunctionDescriptions.Add(SumSq.Name, "SUMSQ(value1,value2,...) \r\n 返回所有参数的平方和。参数可以是数值、数组、名称，或者是对数值单元格的引用");

            SumX2MY2FunctionInfo SumX2MY2 = new SumX2MY2FunctionInfo();
            FunctionInfos["数学与三角函数"].Add(SumX2MY2);
            FunctionInfos["全部"].Add(SumX2MY2);
            FunctionDescriptions.Add(SumX2MY2.Name, "SUMX2MY2(array_x,array_y) \r\n 计算两数组中对应数值平方差的和。");

            SumX2PY2FunctionInfo SumX2PY2 = new SumX2PY2FunctionInfo();
            FunctionInfos["数学与三角函数"].Add(SumX2PY2);
            FunctionInfos["全部"].Add(SumX2PY2);
            FunctionDescriptions.Add(SumX2PY2.Name, "SUMX2PY2(array_x,array_y) \r\n 计算两数组中对应数值平方和的和。");

            SumXMY2FunctionInfo SumXMY2 = new SumXMY2FunctionInfo();
            FunctionInfos["数学与三角函数"].Add(SumXMY2);
            FunctionInfos["全部"].Add(SumXMY2);
            FunctionDescriptions.Add(SumXMY2.Name, "SUMXMY2(array_x,array_y) \r\n 求两数组中对应数值差的平方和。");

            TanFunctionInfo Tan = new TanFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Tan);
            FunctionInfos["全部"].Add(Tan);
            FunctionDescriptions.Add(Tan.Name, "TAN(angle) \r\n 返回给定角度的正切值。");

            TanhFunctionInfo Tanh = new TanhFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Tanh);
            FunctionInfos["全部"].Add(Tanh);
            FunctionDescriptions.Add(Tanh.Name, "TANH(value) \r\n 返回双曲正切值。");

            TruncFunctionInfo Trunc = new TruncFunctionInfo();
            FunctionInfos["数学与三角函数"].Add(Trunc);
            FunctionInfos["全部"].Add(Trunc);
            FunctionDescriptions.Add(Trunc.Name, "TRUNC(value,precision) \r\n 将数字截为整数或保留指定位数的小数。");

            //统计类
            AveDevFunctionInfo AveDev = new AveDevFunctionInfo();
            FunctionInfos["统计"].Add(AveDev);
            FunctionInfos["全部"].Add(AveDev);
            FunctionDescriptions.Add(AveDev.Name, "AVEDEV(value1,value2,...) \r\n 返回一组数据点到其算术平均值的绝对偏差的平均值。参数可以是数字、名称、数组或包含数字的引用");

            AverageFunctionInfo Average = new AverageFunctionInfo();
            FunctionInfos["统计"].Add(Average);
            FunctionInfos["全部"].Add(Average);
            FunctionInfos["常用函数"].Add(Average);
            FunctionDescriptions.Add(Average.Name, "AVERAGE(value1,value2,...) \r\n 返回其参数的算术平均值；参数可以是数值或包含数值的名称、数组或引用");

            AverageAFunctionInfo AverageA = new AverageAFunctionInfo();
            FunctionInfos["统计"].Add(AverageA);
            FunctionInfos["全部"].Add(AverageA);
            FunctionDescriptions.Add(AverageA.Name, "AVERAGEA(value1,value2,...) \r\n 返回所有参数的算术平均值；字符串和 FALSE 相当于 0；TRUE 相当于 1。参数可以是数值、名称、数组或引用");

            BetaDistFunctionInfo BetaDist = new BetaDistFunctionInfo();
            FunctionInfos["统计"].Add(BetaDist);
            FunctionInfos["全部"].Add(BetaDist);
            FunctionDescriptions.Add(BetaDist.Name, "BETADIST(x,alpha,beta,lower,upper) \r\n 返回累积 beta 分布的概率密度。");

            BetaInvFunctionInfo BetaInv = new BetaInvFunctionInfo();
            FunctionInfos["统计"].Add(BetaInv);
            FunctionInfos["全部"].Add(BetaInv);
            FunctionDescriptions.Add(BetaInv.Name, "BETAINV(prob,alpha,beta,lower,upper) \r\n 返回具有给定概率的累积 beta 分布的区间点。");

            BinomDistFunctionInfo BinomDist = new BinomDistFunctionInfo();
            FunctionInfos["统计"].Add(BinomDist);
            FunctionInfos["全部"].Add(BinomDist);
            FunctionDescriptions.Add(BinomDist.Name, "BINOMDIST(x,n,p,cumulative) \r\n 返回一元二项式分布的概率。");

            ChiDistFunctionInfo ChiDist = new ChiDistFunctionInfo();
            FunctionInfos["统计"].Add(ChiDist);
            FunctionInfos["全部"].Add(ChiDist);
            FunctionDescriptions.Add(ChiDist.Name, "CHIDIST(value,deg) \r\n 返回X二次方分布的收尾概率。");

            ChiInvFunctionInfo ChiInv = new ChiInvFunctionInfo();
            FunctionInfos["统计"].Add(ChiInv);
            FunctionInfos["全部"].Add(ChiInv);
            FunctionDescriptions.Add(ChiInv.Name, "CHIINV(prob,deg) \r\n 返回具有给定概率的收尾 X二次方 分布的区间点。");

            ChiTestFunctionInfo ChiTest = new ChiTestFunctionInfo();
            FunctionInfos["统计"].Add(ChiTest);
            FunctionInfos["全部"].Add(ChiTest);
            FunctionDescriptions.Add(ChiTest.Name, "CHITEST(obs_array,exp_array) \r\n 返回检验相关性。");

            ConfidenceFunctionInfo Confidence = new ConfidenceFunctionInfo();
            FunctionInfos["统计"].Add(Confidence);
            FunctionInfos["全部"].Add(Confidence);
            FunctionDescriptions.Add(Confidence.Name, "CONFIDENCE(alpha,stdev,size) \r\n 返回总体平均值的置信区间。");

            CorrelFunctionInfo Correl = new CorrelFunctionInfo();
            FunctionInfos["统计"].Add(Correl);
            FunctionInfos["全部"].Add(Correl);
            FunctionDescriptions.Add(Correl.Name, "CONFIDENCE(alpha,stdev,size) \r\n 返回两组数值的相关系数。");

            CountFunctionInfo Count = new CountFunctionInfo();
            FunctionInfos["统计"].Add(Count);
            FunctionInfos["全部"].Add(Count);
            FunctionInfos["常用函数"].Add(Count);
            FunctionDescriptions.Add(Count.Name, "COUNT(value1,value2,...) \r\n 计算区域中包含数字的单元格的个数。");

            CountAFunctionInfo CountA = new CountAFunctionInfo();
            FunctionInfos["统计"].Add(CountA);
            FunctionInfos["全部"].Add(CountA);
            FunctionDescriptions.Add(CountA.Name, "COUNTA(value1,value2,...) \r\n 计算区域中非空单元格的个数。");

            CovarFunctionInfo Covar = new CovarFunctionInfo();
            FunctionInfos["统计"].Add(Covar);
            FunctionInfos["全部"].Add(Covar);
            FunctionDescriptions.Add(Covar.Name, "COVAR(array1,array2) \r\n 返回协方差，即每对变量的偏差乘积的均值。");

            CritBinomFunctionInfo CritBinom = new CritBinomFunctionInfo();
            FunctionInfos["统计"].Add(CritBinom);
            FunctionInfos["全部"].Add(CritBinom);
            FunctionDescriptions.Add(CritBinom.Name, "CRITBINOM(n,p,alpha) \r\n 返回一个数值，它是使得累积二项式分布的函数值大于等于临界值 alpha 的最小整数。");

            DevSqFunctionInfo DevSq = new DevSqFunctionInfo();
            FunctionInfos["统计"].Add(DevSq);
            FunctionInfos["全部"].Add(DevSq);
            FunctionDescriptions.Add(DevSq.Name, "DEVSQ(value1,value2, ...) \r\n 返回各数据点与数据均值点之差(数据偏差)的平方和。");

            ExponDistFunctionInfo ExponDist = new ExponDistFunctionInfo();
            FunctionInfos["统计"].Add(ExponDist);
            FunctionInfos["全部"].Add(ExponDist);
            FunctionDescriptions.Add(ExponDist.Name, "EXPONDIST(value,lambda,cumulative) \r\n 返回指数分布。");

            FDistFunctionInfo FDist = new FDistFunctionInfo();
            FunctionInfos["统计"].Add(FDist);
            FunctionInfos["全部"].Add(FDist);
            FunctionDescriptions.Add(FDist.Name, "FDIST(value,degnum,degden) \r\n 返回两组数据的 F 概率分布。");

            FInvFunctionInfo FInv = new FInvFunctionInfo();
            FunctionInfos["统计"].Add(FInv);
            FunctionInfos["全部"].Add(FInv);
            FunctionDescriptions.Add(FInv.Name, "FINV(p,degnum,degden) \r\n 返回 F 概率分布的逆函数值，如果 p = FDIST(x,...)，那么 FINV(p,...) = x。");

            FisherFunctionInfo Fisher = new FisherFunctionInfo();
            FunctionInfos["统计"].Add(Fisher);
            FunctionInfos["全部"].Add(Fisher);
            FunctionDescriptions.Add(Fisher.Name, "FISHER(value) \r\n 返回 Fisher 变换值。");

            FisherInvFunctionInfo FisherInv = new FisherInvFunctionInfo();
            FunctionInfos["统计"].Add(FisherInv);
            FunctionInfos["全部"].Add(FisherInv);
            FunctionDescriptions.Add(FisherInv.Name, "FISHERINV(value) \r\n 返回 Fisher 逆变换值，如果 y = FISHER(x)，那么 FISHERINV(y) = x");

            ForecastFunctionInfo Forecast = new ForecastFunctionInfo();
            FunctionInfos["统计"].Add(Forecast);
            FunctionInfos["全部"].Add(Forecast);
            FunctionDescriptions.Add(Forecast.Name, "FORECAST(value,Yarray,Xarray) \r\n 通过一条线性回归拟合线返回一个预测值。");

            FrequencyFunctionInfo Frequency = new FrequencyFunctionInfo();
            FunctionInfos["统计"].Add(Frequency);
            FunctionInfos["全部"].Add(Frequency);
            FunctionDescriptions.Add(Frequency.Name, "FREQUENCY(dataarray,binarray) \r\n 以一列垂直数组返回一组数据的频率分布。");

            FTestFunctionInfo FTest = new FTestFunctionInfo();
            FunctionInfos["统计"].Add(FTest);
            FunctionInfos["全部"].Add(FTest);
            FunctionDescriptions.Add(FTest.Name, "FTEST(array1,array2) \r\n 返回 F 检验的结果，F 检验返回的是当 array1 和 array2 的方差无明显差异时的双尾概率。");

            GammaDistFunctionInfo GammaDist = new GammaDistFunctionInfo();
            FunctionInfos["统计"].Add(GammaDist);
            FunctionInfos["全部"].Add(GammaDist);
            FunctionDescriptions.Add(GammaDist.Name, "GAMMADIST(x,alpha,beta,cumulative) \r\n 返回 Y 分布函数。");

            GammaInvFunctionInfo GammaInv = new GammaInvFunctionInfo();
            FunctionInfos["统计"].Add(GammaInv);
            FunctionInfos["全部"].Add(GammaInv);
            FunctionDescriptions.Add(GammaInv.Name, "GAMMAINV(p,alpha,beta) \r\n 返回具有给定概率的 Y 累积分布的区间点。");

            GammaLnFunctionInfo GammaLn = new GammaLnFunctionInfo();
            FunctionInfos["统计"].Add(GammaLn);
            FunctionInfos["全部"].Add(GammaLn);
            FunctionDescriptions.Add(GammaLn.Name, "GAMMALN(value) \r\n 返回 Y 函数的自然对数。");

            GeoMeanFunctionInfo GeoMean = new GeoMeanFunctionInfo();
            FunctionInfos["统计"].Add(GeoMean);
            FunctionInfos["全部"].Add(GeoMean);
            FunctionDescriptions.Add(GeoMean.Name, "GEOMEAN(value1,value2,...) \r\n 返回一正数数组或数值区域的几何平均数。");

            GrowthFunctionInfo Growth = new GrowthFunctionInfo();
            FunctionInfos["统计"].Add(Growth);
            FunctionInfos["全部"].Add(Growth);
            FunctionDescriptions.Add(Growth.Name, "GROWTH(y,x,newx,constant) \r\n 返回指数回归拟合曲线的一组纵坐标值(y 值)。");

            HarMeanFunctionInfo HarMean = new HarMeanFunctionInfo();
            FunctionInfos["统计"].Add(HarMean);
            FunctionInfos["全部"].Add(HarMean);
            FunctionDescriptions.Add(HarMean.Name, "HARMEAN(value1,value2,...) \r\n 返回一组正数的调和平均数:所有参数倒数平均值的倒数。");

            HypGeomDistFunctionInfo HypGeomDist = new HypGeomDistFunctionInfo();
            FunctionInfos["统计"].Add(HypGeomDist);
            FunctionInfos["全部"].Add(HypGeomDist);
            FunctionDescriptions.Add(HypGeomDist.Name, "HYPGEOMDIST(x,n,M,N) \r\n 返回超几何分布。");

            InterceptFunctionInfo Intercept = new InterceptFunctionInfo();
            FunctionInfos["统计"].Add(Intercept);
            FunctionInfos["全部"].Add(Intercept);
            FunctionDescriptions.Add(Intercept.Name, "INTERCEPT(dependent,independent) \r\n 求线性回归拟合线方程的截距。");

            KurtFunctionInfo Kurt = new KurtFunctionInfo();
            FunctionInfos["统计"].Add(Kurt);
            FunctionInfos["全部"].Add(Kurt);
            FunctionDescriptions.Add(Kurt.Name, "KURT(value1,value2,value3,value4,...) \r\n 返回一组数据的峰值。");

            LargeFunctionInfo Large = new LargeFunctionInfo();
            FunctionInfos["统计"].Add(Large);
            FunctionInfos["全部"].Add(Large);
            FunctionDescriptions.Add(Large.Name, "LARGE(array,n) \r\n 返回数据组中第 k 个最大值。例如第五个最大者");

            LinEstFunctionInfo LinEst = new LinEstFunctionInfo();
            FunctionInfos["统计"].Add(LinEst);
            FunctionInfos["全部"].Add(LinEst);
            FunctionDescriptions.Add(LinEst.Name, "LINEST(y,x,constant,stats) \r\n 返回线性回归方程的参数。");

            LogEstFunctionInfo LogEst = new LogEstFunctionInfo();
            FunctionInfos["统计"].Add(LogEst);
            FunctionInfos["全部"].Add(LogEst);
            FunctionDescriptions.Add(LogEst.Name, "LOGEST(y,x,constant,stats) \r\n 返回指数回归拟合曲线方程的参数。");

            LogInvFunctionInfo LogInv = new LogInvFunctionInfo();
            FunctionInfos["统计"].Add(LogInv);
            FunctionInfos["全部"].Add(LogInv);
            FunctionDescriptions.Add(LogInv.Name, "LOGINV(prob,mean,stdev) \r\n 返回具有给定概率的对数正态分布函数的区间点。");

            LogNormDistFunctionInfo LogNormDist = new LogNormDistFunctionInfo();
            FunctionInfos["统计"].Add(LogNormDist);
            FunctionInfos["全部"].Add(LogNormDist);
            FunctionDescriptions.Add(LogNormDist.Name, "LOGNORMDIST(x,mean,stdev) \r\n 返回对数正态分布。");

            MaxFunctionInfo Max = new MaxFunctionInfo();
            FunctionInfos["统计"].Add(Max);
            FunctionInfos["全部"].Add(Max);
            FunctionInfos["常用函数"].Add(Max);
            FunctionDescriptions.Add(Max.Name, "MAX(value1,value2,...) \r\n 返回一组数值中的最大者，忽略逻辑值及文本。");

            MaxAFunctionInfo MaxA = new MaxAFunctionInfo();
            FunctionInfos["统计"].Add(MaxA);
            FunctionInfos["全部"].Add(MaxA);
            FunctionDescriptions.Add(MaxA.Name, "MAXA(value1,value2,...) \r\n 返回一组参数中的最大值(不忽略逻辑值和字符串)。");

            MedianFunctionInfo Median = new MedianFunctionInfo();
            FunctionInfos["统计"].Add(Median);
            FunctionInfos["全部"].Add(Median);
            FunctionDescriptions.Add(Median.Name, "MEDIAN(value1,value2,...) \r\n 返回一组数的中值。");

            MinFunctionInfo Min = new MinFunctionInfo();
            FunctionInfos["统计"].Add(Min);
            FunctionInfos["全部"].Add(Min);
            FunctionDescriptions.Add(Min.Name, "MIN(value1,value2,...) \r\n 返回一组数值中的最小值，忽略逻辑值及文本。");

            MinAFunctionInfo MinA = new MinAFunctionInfo();
            FunctionInfos["统计"].Add(MinA);
            FunctionInfos["全部"].Add(MinA);
            FunctionDescriptions.Add(MinA.Name, "MINA(value1,value2,...) \r\n 返回一组参数中的最小值(不忽略逻辑值和字符串)。");

            ModeFunctionInfo Mode = new ModeFunctionInfo();
            FunctionInfos["统计"].Add(Mode);
            FunctionInfos["全部"].Add(Mode);
            FunctionDescriptions.Add(Mode.Name, "MODE(value1,value2,...) \r\n 返回一组数据或数据区域中的众数(出现频率最高的数)。");

            NegBinomDistFunctionInfo NegBinomDist = new NegBinomDistFunctionInfo();
            FunctionInfos["统计"].Add(NegBinomDist);
            FunctionInfos["全部"].Add(NegBinomDist);
            FunctionDescriptions.Add(NegBinomDist.Name, "NEGBINOMDIST(x,r,p) \r\n 返回负二项式分布函数值。");

            NormDistFunctionInfo NormDist = new NormDistFunctionInfo();
            FunctionInfos["统计"].Add(NormDist);
            FunctionInfos["全部"].Add(NormDist);
            FunctionDescriptions.Add(NormDist.Name, "NORMDIST(x,mean,stdev,cumulative) \r\n 返回正态分布函数值。");

            NormInvFunctionInfo NormInv = new NormInvFunctionInfo();
            FunctionInfos["统计"].Add(NormInv);
            FunctionInfos["全部"].Add(NormInv);
            FunctionDescriptions.Add(NormInv.Name, "NORMINV(prob,mean,stdev) \r\n 返回具有给定概率正态分布的区间点。");

            NormSDistFunctionInfo NormSDist = new NormSDistFunctionInfo();
            FunctionInfos["统计"].Add(NormSDist);
            FunctionInfos["全部"].Add(NormSDist);
            FunctionDescriptions.Add(NormSDist.Name, "NORMSDIST(value) \r\n 返回标准正态分布函数值。");

            NormSInvFunctionInfo NormSInv = new NormSInvFunctionInfo();
            FunctionInfos["统计"].Add(NormSInv);
            FunctionInfos["全部"].Add(NormSInv);
            FunctionDescriptions.Add(NormSInv.Name, "NORMSINV(prob) \r\n 返回标准正态分布的区间点。");

            PearsonFunctionInfo Pearson = new PearsonFunctionInfo();
            FunctionInfos["统计"].Add(Pearson);
            FunctionInfos["全部"].Add(Pearson);
            FunctionDescriptions.Add(Pearson.Name, "PEARSON(array_ind,array_dep) \r\n 求皮尔生(Pearson)积矩法的相关系数 r。");

            PercentileFunctionInfo Percentile = new PercentileFunctionInfo();
            FunctionInfos["统计"].Add(Percentile);
            FunctionInfos["全部"].Add(Percentile);
            FunctionDescriptions.Add(Percentile.Name, "PERCENTILE(array,n) \r\n 返回数组的 K 百分点值。");

            PercentRankFunctionInfo PercentRank = new PercentRankFunctionInfo();
            FunctionInfos["统计"].Add(PercentRank);
            FunctionInfos["全部"].Add(PercentRank);
            FunctionDescriptions.Add(PercentRank.Name, "PERCENTRANK(array,n,sigdig) \r\n 返回特定数组在一组数中的百分比排名。");

            PermutFunctionInfo Permut = new PermutFunctionInfo();
            FunctionInfos["统计"].Add(Permut);
            FunctionInfos["全部"].Add(Permut);
            FunctionDescriptions.Add(Permut.Name, "PERMUT(k,n) \r\n 返回从给定元素数目的集合中选取若干元素的排列数。");

            PoissonFunctionInfo Poisson = new PoissonFunctionInfo();
            FunctionInfos["统计"].Add(Poisson);
            FunctionInfos["全部"].Add(Poisson);
            FunctionDescriptions.Add(Poisson.Name, "POISSON(nevents,mean,cumulative) \r\n 返回泊松(Poisson)分布。");

            ProbFunctionInfo Prob = new ProbFunctionInfo();
            FunctionInfos["统计"].Add(Prob);
            FunctionInfos["全部"].Add(Prob);
            FunctionDescriptions.Add(Prob.Name, "PROB(array,probs,lower,upper) \r\n 返回一概率事件组中符合指定条件的事件集所对应的概率之和。");

            QuartileFunctionInfo Quartile = new QuartileFunctionInfo();
            FunctionInfos["统计"].Add(Quartile);
            FunctionInfos["全部"].Add(Quartile);
            FunctionDescriptions.Add(Quartile.Name, "QUARTILE(array,quart) \r\n 返回一组数据的四分位点。");

            RankFunctionInfo Rank = new RankFunctionInfo();
            FunctionInfos["统计"].Add(Rank);
            FunctionInfos["全部"].Add(Rank);
            FunctionDescriptions.Add(Rank.Name, "RANK(number,array,order) \r\n 返回某数字在一列数字中相对于其他数值的大小排名。");

            RSqFunctionInfo RSq = new RSqFunctionInfo();
            FunctionInfos["统计"].Add(RSq);
            FunctionInfos["全部"].Add(RSq);
            FunctionDescriptions.Add(RSq.Name, "RSQ(array_dep,array_ind) \r\n 返回给定数据点的 Pearson 积矩法相关系数的平方。");

            SkewFunctionInfo Skew = new SkewFunctionInfo();
            FunctionInfos["统计"].Add(Skew);
            FunctionInfos["全部"].Add(Skew);
            FunctionDescriptions.Add(Skew.Name, "SKEW(number1,number2,...) \r\n 返回一个分布的不对称度:用来体现某一分布相对其平均值的不对称程度。");

            SlopeFunctionInfo Slope = new SlopeFunctionInfo();
            FunctionInfos["统计"].Add(Slope);
            FunctionInfos["全部"].Add(Slope);
            FunctionDescriptions.Add(Slope.Name, "SLOPE(array_dep,array_ind) \r\n 返回经过给定数据点的线性回归拟合线方程的斜率。");

            SmallFunctionInfo Small = new SmallFunctionInfo();
            FunctionInfos["统计"].Add(Small);
            FunctionInfos["全部"].Add(Small);
            FunctionDescriptions.Add(Small.Name, "SMALL(array,n) \r\n 返回数组中第 k 个最小值。");

            StandardizeFunctionInfo Standardize = new StandardizeFunctionInfo();
            FunctionInfos["统计"].Add(Standardize);
            FunctionInfos["全部"].Add(Standardize);
            FunctionDescriptions.Add(Standardize.Name, "STANDARDIZE(x,mean,stdev) \r\n 通过平均值和标准方差返回正态分布概率值。");

            StDevFunctionInfo StDev = new StDevFunctionInfo();
            FunctionInfos["统计"].Add(StDev);
            FunctionInfos["全部"].Add(StDev);
            FunctionDescriptions.Add(StDev.Name, "STDEV(value1,value2,...) \r\n 估算给定样本的标准偏差(忽略样本中的逻辑值及文本)。");

            StDevAFunctionInfo StDevA = new StDevAFunctionInfo();
            FunctionInfos["统计"].Add(StDevA);
            FunctionInfos["全部"].Add(StDevA);
            FunctionDescriptions.Add(StDevA.Name, "STDEVA(value1,value2,...) \r\n 估算基于给定样本(包括逻辑值和字符串)的标准偏差。字符串和逻辑值 FALSE 数值为 0；逻辑值 TRUE 为 1");

            StDevPFunctionInfo StDevP = new StDevPFunctionInfo();
            FunctionInfos["统计"].Add(StDevP);
            FunctionInfos["全部"].Add(StDevP);
            FunctionDescriptions.Add(StDevP.Name, "STDEVP(value1,value2,...) \r\n 计算基于给定的样本总体的标准偏差(忽略逻辑值及字符串)。");

            StDevPAFunctionInfo StDevPA = new StDevPAFunctionInfo();
            FunctionInfos["统计"].Add(StDevPA);
            FunctionInfos["全部"].Add(StDevPA);
            FunctionDescriptions.Add(StDevPA.Name, "STDEVPA(value1,value2,...) \r\n 计算样本(包括逻辑值和字符串)总体的标准偏差。字符串和逻辑值 FALSE 数值为 0；逻辑值 TRUE 为 1");

            StEyxFunctionInfo StEyx = new StEyxFunctionInfo();
            FunctionInfos["统计"].Add(StEyx);
            FunctionInfos["全部"].Add(StEyx);
            FunctionDescriptions.Add(StEyx.Name, "STEYX(array_dep,array_ind) \r\n 返回通过线性回归法计算纵坐标预测值所产生的标准误差。");

            TDistFunctionInfo TDist = new TDistFunctionInfo();
            FunctionInfos["统计"].Add(TDist);
            FunctionInfos["全部"].Add(TDist);
            FunctionDescriptions.Add(TDist.Name, "TDIST(x,deg,tails) \r\n 返回学生 t-分布。");

            TInvFunctionInfo TInv = new TInvFunctionInfo();
            FunctionInfos["统计"].Add(TInv);
            FunctionInfos["全部"].Add(TInv);
            FunctionDescriptions.Add(TInv.Name, "TINV(prog,deg) \r\n 返回给定自由度和双尾概率的学生 t-分布的区间点。");

            TrendFunctionInfo Trend = new TrendFunctionInfo();
            FunctionInfos["统计"].Add(Trend);
            FunctionInfos["全部"].Add(Trend);
            FunctionDescriptions.Add(Trend.Name, "TREND(y,x,newx,constant) \r\n 返回线性回归拟合线的一组纵坐标值(y 值)。");

            TrimMeanFunctionInfo TrimMean = new TrimMeanFunctionInfo();
            FunctionInfos["统计"].Add(TrimMean);
            FunctionInfos["全部"].Add(TrimMean);
            FunctionDescriptions.Add(TrimMean.Name, "TRIMMEAN(array,percent) \r\n 返回一组数据的修剪平均值。");

            TTestFunctionInfo TTest = new TTestFunctionInfo();
            FunctionInfos["统计"].Add(TTest);
            FunctionInfos["全部"].Add(TTest);
            FunctionDescriptions.Add(TTest.Name, "TTEST(array1,array2,tails,type) \r\n 返回学生 t-检验的概率值。");

            VarFunctionInfo Var = new VarFunctionInfo();
            FunctionInfos["统计"].Add(Var);
            FunctionInfos["全部"].Add(Var);
            FunctionDescriptions.Add(Var.Name, "VAR(value1,value2,...) \r\n 估算基于给定样本的方差(忽略样本中的逻辑值及文本)。");

            VarAFunctionInfo VarA = new VarAFunctionInfo();
            FunctionInfos["统计"].Add(VarA);
            FunctionInfos["全部"].Add(VarA);
            FunctionDescriptions.Add(VarA.Name, "VARA(value1,value2,...) \r\n 估算基于给定样本(包括逻辑值和字符串)的方差。字符串和逻辑值 FALSE 数值为 0；逻辑值 TRUE 为 1");

            VarPFunctionInfo VarP = new VarPFunctionInfo();
            FunctionInfos["统计"].Add(VarP);
            FunctionInfos["全部"].Add(VarP);
            FunctionDescriptions.Add(VarP.Name, "VARP(value1,value2,...) \r\n 计算基于给定的样本总体的方差(忽略样本中的逻辑值及文本)。");

            VarPAFunctionInfo VarPA = new VarPAFunctionInfo();
            FunctionInfos["统计"].Add(VarPA);
            FunctionInfos["全部"].Add(VarPA);
            FunctionDescriptions.Add(VarPA.Name, "VARPA(value1,value2,...) \r\n 计算给定样本(包括逻辑值和字符串)总体的方差。字符串和逻辑值 FALSE 数值为 0；逻辑值 TRUE 为 1");

            WeibullFunctionInfo Weibull = new WeibullFunctionInfo();
            FunctionInfos["统计"].Add(Weibull);
            FunctionInfos["全部"].Add(Weibull);
            FunctionDescriptions.Add(Weibull.Name, "WEIBULL(x,alpha,beta,cumulative) \r\n 返回 Weibull 分布(概率密度)。");

            ZTestFunctionInfo ZTest = new ZTestFunctionInfo();
            FunctionInfos["统计"].Add(ZTest);
            FunctionInfos["全部"].Add(ZTest);
            FunctionDescriptions.Add(ZTest.Name, "ZTEST(array,x,sigma) \r\n 返回 z 测试的单尾 P 值。");

            //文本类
            CharFunctionInfo Char = new CharFunctionInfo();
            FunctionInfos["文本"].Add(Char);
            FunctionInfos["全部"].Add(Char);
            FunctionDescriptions.Add(Char.Name, "CHAR(value) \r\n 根据本机中的字符集，返回由代码数字指定的字符。");

            CleanFunctionInfo Clean = new CleanFunctionInfo();
            FunctionInfos["文本"].Add(Clean);
            FunctionInfos["全部"].Add(Clean);
            FunctionDescriptions.Add(Clean.Name, "CLEAN(text) \r\n 删除文本中的所有非打印字符。");

            CodeFunctionInfo Code = new CodeFunctionInfo();
            FunctionInfos["文本"].Add(Code);
            FunctionInfos["全部"].Add(Code);
            FunctionDescriptions.Add(Code.Name, "CODE(text) \r\n 返回文本字符串第一个字符在本机所有字符集中的数字代码。");

            ConcatenateFunctionInfo Concatenate = new ConcatenateFunctionInfo();
            FunctionInfos["文本"].Add(Concatenate);
            FunctionInfos["全部"].Add(Concatenate);
            FunctionDescriptions.Add(Concatenate.Name, "CONCATENATE(text1,text2,...) \r\n 将多个文本字符串合并成一个。");

            DollarFunctionInfo dollar = new DollarFunctionInfo();
            FunctionInfos["文本"].Add(dollar);
            FunctionInfos["全部"].Add(dollar);
            FunctionDescriptions.Add(dollar.Name, "DOLLAR(value,digits) \r\n 按照货币格式及给定的小数位数，将数字转换成文本。");

            ExactFunctionInfo Exact = new ExactFunctionInfo();
            FunctionInfos["文本"].Add(Exact);
            FunctionInfos["全部"].Add(Exact);
            FunctionDescriptions.Add(Exact.Name, "EXACT(text1,text2) \r\n 比较两个字符串是否完全相同(区分大小写)。返回 TRUE 或 FALSE");

            FindFunctionInfo Find = new FindFunctionInfo();
            FunctionInfos["文本"].Add(Find);
            FunctionInfos["全部"].Add(Find);
            FunctionDescriptions.Add(Find.Name, "FIND(findtext,intext,start) \r\n 返回一个字符串在另一个字符串中出现的起始位置(区分大小写)。");

            FixedFunctionInfo Fixed = new FixedFunctionInfo();
            FunctionInfos["文本"].Add(Fixed);
            FunctionInfos["全部"].Add(Fixed);
            FunctionDescriptions.Add(Fixed.Name, "FIXED(num,digits,notcomma) \r\n 用定点小数格式将数值舍入成特定位数并返回带或不带逗号的文本。");

            LeftFunctionInfo Left = new LeftFunctionInfo();
            FunctionInfos["文本"].Add(Left);
            FunctionInfos["全部"].Add(Left);
            FunctionDescriptions.Add(Left.Name, "LEFT(mytext,num_chars) \r\n 从一个文本字符串的第一个字符开始返回指定个数的字符。");

            LenFunctionInfo Len = new LenFunctionInfo();
            FunctionInfos["文本"].Add(Len);
            FunctionInfos["全部"].Add(Len);
            FunctionDescriptions.Add(Len.Name, "LEN(value) \r\n 返回文本字符串中字符个数。");

            LowerFunctionInfo Lower = new LowerFunctionInfo();
            FunctionInfos["文本"].Add(Lower);
            FunctionInfos["全部"].Add(Lower);
            FunctionDescriptions.Add(Lower.Name, "LOWER(string) \r\n 将一个文本字符串的所有字母转换为小写形式。");

            MidFunctionInfo Mid = new MidFunctionInfo();
            FunctionInfos["文本"].Add(Mid);
            FunctionInfos["全部"].Add(Mid);
            FunctionInfos["常用函数"].Add(Mid);
            FunctionDescriptions.Add(Mid.Name, "MID(text,start_num,num_chars) \r\n 从文本字符串中指定的起始位置起返回指定长度的字符。");

            ProperFunctionInfo Proper = new ProperFunctionInfo();
            FunctionInfos["文本"].Add(Proper);
            FunctionInfos["全部"].Add(Proper);
            FunctionDescriptions.Add(Proper.Name, "PROPER(text) \r\n 将一个文本字符串中各英文单词的第一个字母转换成大写，将其他字符转换成小写。");

            ReplaceFunctionInfo Replace = new ReplaceFunctionInfo();
            FunctionInfos["文本"].Add(Replace);
            FunctionInfos["全部"].Add(Replace);
            FunctionDescriptions.Add(Replace.Name, "REPLACE(old_text,start_char,num_chars,new_text) \r\n 将一个字符串中的部分字符用另一个字符串替换。");

            ReptFunctionInfo Rept = new ReptFunctionInfo();
            FunctionInfos["文本"].Add(Rept);
            FunctionInfos["全部"].Add(Rept);
            FunctionDescriptions.Add(Rept.Name, "REPT(text,number) \r\n 根据指定次数重复文本。可用 REPT 在一个单元格中重复填写一个文本字符串");

            RightFunctionInfo Right = new RightFunctionInfo();
            FunctionInfos["文本"].Add(Right);
            FunctionInfos["全部"].Add(Right);
            FunctionDescriptions.Add(Right.Name, "RIGHT(text,num_chars) \r\n 从一个文本字符串的最后一个字符开始返回指定个数的字符。");

            SubstituteFunctionInfo Substitute = new SubstituteFunctionInfo();
            FunctionInfos["文本"].Add(Substitute);
            FunctionInfos["全部"].Add(Substitute);
            FunctionDescriptions.Add(Substitute.Name, "SUBSTITUTE(text,old_piece,new_piece,instance) \r\n 将字符串中部分字符串以新字符串替换。");

            TFunctionInfo T = new TFunctionInfo();
            FunctionInfos["文本"].Add(T);
            FunctionInfos["全部"].Add(T);
            FunctionDescriptions.Add(T.Name, "T(value) \r\n 检测给定值是否为文本，如果是文本按原样返回，如果不是文本则返回双引号(空文本)。");

            TrimFunctionInfo Trim = new TrimFunctionInfo();
            FunctionInfos["文本"].Add(Trim);
            FunctionInfos["全部"].Add(Trim);
            FunctionDescriptions.Add(Trim.Name, "TRIM(text) \r\n 删除字符串中多余的空格，但会在英文字符串中保留一个作为词与词之间分隔的空格。");

            UpperFunctionInfo Upper = new UpperFunctionInfo();
            FunctionInfos["文本"].Add(Upper);
            FunctionInfos["全部"].Add(Upper);
            FunctionDescriptions.Add(Upper.Name, "UPPER(string) \r\n 将文本字符串转换成字母全部大写形式。");

            ValueFunctionInfo Value = new ValueFunctionInfo();
            FunctionInfos["文本"].Add(Value);
            FunctionInfos["全部"].Add(Value);
            FunctionDescriptions.Add(Value.Name, "VALUE(text) \r\n 将代表数值的文本字符串转换成数值。");

            #endregion Farpoint 内置函数

            //按字母对函数进行排序
            FunctionInfos["常用函数"].Sort(functionCompare);
            FunctionInfos["全部"].Sort(functionCompare);
            FunctionInfos["数据库"].Sort(functionCompare);
            FunctionInfos["日期和时间"].Sort(functionCompare);
            FunctionInfos["工程"].Sort(functionCompare);
            FunctionInfos["财务"].Sort(functionCompare);
            FunctionInfos["查找"].Sort(functionCompare);
            FunctionInfos["逻辑"].Sort(functionCompare);
            FunctionInfos["数学与三角函数"].Sort(functionCompare);
            FunctionInfos["统计"].Sort(functionCompare);
            FunctionInfos["信息"].Sort(functionCompare);
            FunctionInfos["文本"].Sort(functionCompare);
        }

        /// <summary>
        /// 初始化界面内容
        /// </summary>
        private void InitFunctionDialogUI()
        {
            lBox_FunctionType.Items.Clear();
            List<String> FunctionTypes = new List<string>(FunctionInfos.Keys);
            lBox_FunctionType.Items.AddRange(FunctionTypes.ToArray());
            lBox_FunctionType.SelectedIndex = 1;

            lBox_SheetName.Items.Clear();
            foreach (SheetView Sheet in _fpSpread.Sheets)
            {
                lBox_SheetName.Items.Add(Sheet.SheetName);
            }
            lBox_SheetName.SelectedIndex = 0;
        }

        private void FunctionsDialog_Load(object sender, EventArgs e)
        {
            InitFunctionDialogUI();
        }

        private void lBox_FunctionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lBox_FunctionName.Items.Clear();
            if (lBox_FunctionType.SelectedItem is String)
            {
                String key = lBox_FunctionType.SelectedItem.ToString();
                lBox_FunctionName.Items.AddRange(FunctionInfos[key].ToArray());
                lBox_FunctionName.SelectedIndex = 0;
            }
        }

        private void lBox_FunctionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lBox_FunctionName.SelectedItem is FunctionInfo)
            {
                String key = lBox_FunctionName.SelectedItem.ToString();
                label_FunctionInfo.Text = FunctionDescriptions[key];
            }
        }

        private void lBox_FunctionName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lBox_FunctionName.SelectedItem is FunctionInfo)
            {
                String name = lBox_FunctionName.SelectedItem.ToString();
                String formatname = string.Format("{0}()", name);
                int prevIndex = tBox_Expression.SelectionStart;
                tBox_Expression.SelectedText = tBox_Expression.SelectedText + formatname;
                tBox_Expression.SelectionStart = prevIndex + formatname.Length - 1;
                tBox_Expression.Focus();
            }
        }

        private void lBox_SheetName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lBox_SheetName.SelectedItem is String)
            {
                String sheetname = lBox_SheetName.SelectedItem.ToString();
                tBox_Expression.SelectedText = tBox_Expression.SelectedText + sheetname + "!";
                tBox_Expression.Focus();
            }
        }

        public void SetFormula(string formula)
        {
            this.tBox_Expression.Text = formula;
        }

        public String GetFormula()
        {
            return this.tBox_Expression.Text;
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
