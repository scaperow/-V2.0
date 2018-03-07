using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using FarPoint.Win.Spread;
using FarPoint.CalcEngine;
using Yqun.Services;
using System.IO;
using System.Reflection;
using Yqun.Common.ContextCache;

namespace BizComponents
{
    /// <summary>
    /// 自定义公式
    /// </summary>
    public class FunctionItemInfoUtil
    {
        public static List<FunctionInfo> getFunctionItemInfos()
        {
            List<FunctionInfo> FunctionItems = new List<FunctionInfo>();

            List<FunctionItemInfo> FunctionItemInfos = Agent.CallService("Yqun.BO.BusinessManager.dll", "InitFunctionItemInfos", new object[] { }) as List<FunctionItemInfo>;
            foreach (FunctionItemInfo ItemInfo in FunctionItemInfos)
            {
                try
                {
                    String PathName = Path.Combine(ApplicationContext.Current.LocalStartPath, ItemInfo.AssemblyName);
                    FunctionInfo Info = getInstance(PathName, ItemInfo.FullClassName) as FunctionInfo;
                    if (Info != null)
                        FunctionItems.Add(Info);
                }
                catch
                {
                }
            }

            return FunctionItems;
        }

        static object getInstance(string AssemblyName, string TypeName)
        {
            try
            {
                object ins = null;
                if (File.Exists(AssemblyName))
                {
                    Assembly a = Assembly.LoadFrom(AssemblyName);
                    ins = a.CreateInstance(TypeName);
                }

                return ins;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }
                else
                {
                    throw ex;
                }
            }
        }
    }
}
