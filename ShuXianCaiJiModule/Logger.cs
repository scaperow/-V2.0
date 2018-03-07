using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using Yqun.Services;

namespace ShuXianCaiJiModule
{
    public class Logger
    {


        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private String logFolder;
        private String logName = "";
        private StreamWriter writer = null;
        private DateTime previousTime = DateTime.Now;
        private Queue<String> messageQueue = new Queue<string>();
        private Int32 queueBufferSize = 5;

        /// <summary>
        /// 记录实时力值文件名称
        /// </summary>
        private string _FileInfoName = "Log-CJ-INFOJSON-" + DateTime.Now.ToString("yyyy-MM-dd") + "-";
        private StreamWriter _WriteInfo = null;

        /// <summary>
        /// 当前试验委托编号
        /// </summary>
        private StringBuilder _WTBH =new StringBuilder();

        private int _CurrentNum = 0;

        public Logger()
        {

        }

        public Logger(String logFolder)
        {
            this.logFolder = logFolder;
            if (!File.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }
        }

        public Int32 QueueBufferSize
        {
            get
            {
                return queueBufferSize;
            }
            set
            {
                queueBufferSize = value;
            }
        }

        public String UserName { get; set; }
        public String TestRoom { get; set; }
        public String MachineCode { get; set; }

        public String Logfolder
        {
            get { return logFolder; }
            set
            {
                if (!Directory.Exists(value))
                {
                    Directory.CreateDirectory(value);
                }
                logFolder = value;
            }
        }

        private Boolean isUseLog = true;

        public Boolean IsUseLog
        {
            get
            {
                return IsUseLog;
            }
            set
            {
                isUseLog = value;
            }
        }

        public void WriteLog(String message, Boolean alwaysWrite, Boolean needSent)
        {
            if (isUseLog)
            {
                if ((DateTime.Now - previousTime).TotalSeconds < 60 && messageQueue.Contains(message) && !alwaysWrite)
                {
                    return;
                }

                try
                {
                    if ("Log-CJ-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log" != logName)
                    {
                        logName = "Log-CJ-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                        if (writer != null)
                        {
                            writer.Close();
                            writer = null;
                        }
                        writer = new StreamWriter(File.Open(Path.Combine(Logfolder, logName), FileMode.Append, FileAccess.Write, FileShare.Read));
                        writer.AutoFlush = true;
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.StackTrace);
                }
                try
                {
                    writer.WriteLine(DateTime.Now.ToString() + "[" + (TestRoom ?? "") + "] [" + (UserName ?? "") + "] [" + (MachineCode ?? "") + "] -- " + message);

                    if (messageQueue.Count >= queueBufferSize)
                    {
                        messageQueue.Dequeue();
                    }
                    messageQueue.Enqueue(message);
                    previousTime = DateTime.Now;
                    if (needSent)
                    {
                        Thread t = new Thread(new ParameterizedThreadStart(SentLog));
                        t.IsBackground = true;
                        t.Start(DateTime.Now.ToString() + "[" + (TestRoom ?? "") + "] [" + (UserName ?? "") + "] [" + (MachineCode ?? "") + "] -- " + message);
                    }
                }
                catch (Exception ea)
                {
                    logger.Error(ea.StackTrace);
                }
            }
        }

        private void SentLog(Object msg)
        {
            try
            {
                Agent.CallService("Yqun.BO.BusinessManager.dll", "SaveCaiJiLog", new object[] { (TestRoom ?? ""), (MachineCode ?? ""), (UserName ?? ""), msg.ToString() });
            }
            catch(Exception ex)
            {
                logger.Error(ex.StackTrace);
            }
        }

        public void Close()
        {
            if (isUseLog)
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        public void InfoLog(string time,string value,string wtbh,int num)
        {
            try
            {
                if (_WTBH.Length <= 0 && _CurrentNum == 0)
                {
                    _CurrentNum = num;
                    _WTBH.Append(wtbh);
                    _WriteInfo = new StreamWriter(File.Open(Path.Combine(Logfolder, _FileInfoName + _WTBH.ToString() + ".Log"), FileMode.Append, FileAccess.Write, FileShare.Read));
                    _WriteInfo.AutoFlush = true;
                    _WriteInfo.Write(_CurrentNum.ToString() + System.Environment.NewLine);
                    _WriteInfo.Write("[");
                    _WriteInfo.Write("{\"Time\":\"" + time + "\",\"Value\":\"" + value + "\"}");
                }
                else if (_WTBH.ToString().Trim()==wtbh && _CurrentNum!=num)
                {
                    _CurrentNum = num;
                    _WriteInfo.Write("]" + System.Environment.NewLine);
                    _WriteInfo.Write(_CurrentNum.ToString() + System.Environment.NewLine);
                    _WriteInfo.Write("[");
                    _WriteInfo.Write("{\"Time\":\"" + time + "\",\"Value\":\"" + value + "\"}");
                }
                else if (_WTBH.ToString().Trim() != wtbh)
                {
                    _CurrentNum = num;
                    _WriteInfo.Write("]");
                    _WriteInfo.Close();
                    if (_WTBH.Length > 0)
                    {
                        _WTBH.Remove(0, _WTBH.Length);
                    }
                    _WTBH.Append(wtbh);
                    _WriteInfo = new StreamWriter(File.Open(Path.Combine(Logfolder, _FileInfoName + _WTBH.ToString() + ".Log"), FileMode.Append, FileAccess.Write, FileShare.Read));
                    _WriteInfo.AutoFlush = true; writer.Write(_CurrentNum.ToString() + System.Environment.NewLine);
                    _WriteInfo.Write("[");
                    _WriteInfo.Write("{\"Time\":\"" + time + "\",\"Value\":\"" + value + "\"}");
                }
                else
                {
                    _WriteInfo.Write(",{\"Time\":\"" + time + "\",\"Value\":\"" + value + "\"}");
                }
            }
            catch(Exception ex) 
            {
                logger.Error(ex.StackTrace);
                if (_WriteInfo != null)
                {
                    _WriteInfo.Close();
                }
            }
        }

    }
}
