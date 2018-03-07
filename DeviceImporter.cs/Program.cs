using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeviceImporter.cs
{
    public class Program
    {
        const string DeviceSummaryID = "BA23C25D-7C79-4CB3-A0DC-ACFA6C285295";
        const string DeviceRegistrationid = "A0C51954-302D-43C6-931E-0BAE2B8B10DB";
        const string Mapping = @"b,b5,
                                ,c,g5
                                ,d,k5
                                ,e,b6
                                ,f,g6
                                ,g,k6
                                ,h,b7
                                ,i,g7
                                ,j,k7
                                ,k,b9
                                ,l,g9
                                ,m,k9
                                ,n,b11
                                ,o,g11
                                ,p,k11";

        static void Main(string[] args)
        {
            var sql = "";

            sql = "SELECT ID, Data FROM sys_document WHERE ModuleID = '" + DeviceSummaryID + "' AND Status > 0";
            
        }
    }
}
