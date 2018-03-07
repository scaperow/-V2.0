using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShuXianCaiJiHlperClient
{
    public delegate void DataReceiveDelegate(object GetobjectiveData);
    public delegate void TestFinishedDelegate(Int32 CurrentNumber);
    public interface IMachines
    {
        event DataReceiveDelegate DataReceive;
        event TestFinishedDelegate TestFinished;
        String PortName
        { get; set; }
        Int32 PortBaud
        { get; set; }
        Int32 CurrentNumber
        { get; set; }
        Boolean IsContinue
        { get; set; }
        Boolean IsFinished
        { get; set; }
        Boolean IsRecordLog
        { get; set; }

        double MaxForce
        { get; set; }

        double HForce
        { get; set; }

        double LForce
        { get; set; }

        void StartAcquisition();
    }
}
