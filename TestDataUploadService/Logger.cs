using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestDataUploadService
{
    public class Logger
    {
        private String logFolder;
        private String logName = "";
        private StreamWriter writer = null;
        private DateTime previousTime = DateTime.Now;
        private Queue<String> messageQueue = new Queue<string>();
        private Int32 queueBufferSize = 3;

        public Logger()
        {

        }

        public Logger(String logFolder)
        {
            this.logFolder = logFolder;
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

        public String WriteLog(String message, Boolean alwaysWrite)
        {
            String result = "";
            if (isUseLog)
            {
                DateTime now = DateTime.Now;
                TimeSpan timeSpan = now - previousTime;
                if (timeSpan.TotalSeconds < 60 && messageQueue.Contains(message) && !alwaysWrite)
                {
                    return "";
                }

                String tempLogName = "Log3-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";

                try
                {
                    if (tempLogName != logName)
                    {
                        logName = tempLogName;
                        if (writer != null)
                        {
                            writer.Close();
                        }
                        writer = new StreamWriter(File.Open(Path.Combine(Logfolder, logName), FileMode.Append, FileAccess.Write, FileShare.Read));
                        writer.AutoFlush = true;
                    }
                }
                catch (Exception e)
                {

                }
                writer.WriteLine(DateTime.Now.ToString() + " -- " + message);

                if (messageQueue.Count >= queueBufferSize)
                {
                    messageQueue.Dequeue();
                }
                messageQueue.Enqueue(message);
                previousTime = now;
                result = DateTime.Now.ToString() + " -- " + message;


            }
            return result;
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

    }
}
