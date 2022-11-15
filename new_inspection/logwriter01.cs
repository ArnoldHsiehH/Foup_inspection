using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace new_inspection
{
    class logwriter01
    {
        #region Variable
        private static bool start_flag = false;     //首次使用
        public string LogPath = "Log";
        private string Device_Name = "unknown";
        private LogDir LogType = LogDir.Other;

        private int LogFolderLimit = 30;            //30_day
        private string LogFolder;

        private string Date = "";
        //private string Time = "";

        private string FileFormat = "yyyy_MM_dd";

        public delegate void LogEvent(LogDir _LogFile, string _LogMessage);
        public static event LogEvent EventLog;

        ParameterizedThreadStart pts;
        static Thread mThread;

        private static Queue<log_mail> Queue_message = new Queue<log_mail>();

        #endregion

        #region public

        public LogDir setLogType
        {
            set
            {
                LogType = value;
            }
        }
        public string setDevice_Name
        {
            set
            {
                Device_Name = value;
            }
        }
        public void write_local_log(string message)
        {

            log_mail log_data = new log_mail();


            if (string.IsNullOrEmpty(LogPath)) //已預設
            {
                return;
            }

            message = string.Format("{0,-20}    {1,-15}{2}",
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff"),
                                string.Format("[{0}]", Device_Name),
                                message);

            pts = new ParameterizedThreadStart(Threadwrit_Log);
            if (!start_flag)            //第一次使用宣告
            {
                mThread = new Thread(pts);
                mThread.IsBackground = true;
                start_flag = true;
            }

            CheckFolder(LogType);       //確認、新增資料夾
            log_data.message = message; //設定寫入資料
            log_data.type = LogType;
            log_data.LogFolder = LogFolder;
            log_data.Time = string.Format("{0:HH00}", DateTime.Now);

            if (EventLog != null)       //沒其他程式需要，就不觸發Event
                EventLog(log_data.type, message);


            if (mThread.IsAlive)        //執行續執行中，可以將資料傳出，一起執行
            {
                lock (balanceLock)
                    Queue_message.Enqueue(log_data);
                return;
            }


            mThread = new Thread(pts);
            mThread.Start(log_data);
            Console.WriteLine("send");
            //WriteLogToFile(LogType, message);
        }
        private static void Threadwrit_Log(object data)
        {
            log_mail log_data = new log_mail();
            log_data = (log_mail)data;
            WriteLogToFile(log_data);
            Console.WriteLine("WriteLog : " + log_data.message);
            while (Queue_message.Count > 0)//同時如果有其它LOG就會在這邊寫
            {
                log_data = Queue_message.Dequeue();
                WriteLogToFile(log_data);//寫入TXT
                Console.WriteLine("WriteLog : " + log_data.message);
            }
        }

        #endregion

        #region WriteFile

        private static readonly object balanceLock = new object();
        private static readonly object writeLock = new object();

        private static void WriteLogToFile(log_mail log_data)
        {
            LogDir _LogFile = log_data.type;
            string _LogMessage = log_data.message;
            string LogFolder = log_data.LogFolder;
            string Time = log_data.Time;
            lock (writeLock)
            {
                using (StreamWriter Stream = new StreamWriter(Path.Combine(LogFolder, string.Format("{0}_{1}.txt", _LogFile, Time)), true, Encoding.Default))
                {
                    Stream.WriteLine(_LogMessage);
                    Stream.Flush();
                    Stream.Dispose();
                    Stream.Close();
                }
            }
        }

        #endregion

        #region CheckFolder

        public void CheckFolder(LogDir Index)
        {
            DateTime dt = DateTime.Now;
            Date = dt.ToString(FileFormat);

            LogFolder = string.Format("{0}{1}{2}{3}{4}", LogPath, "\\", LogDir.System + (int)Index, "\\", Date); ;

            if (!Directory.Exists(LogFolder))
            {
                Directory.CreateDirectory(LogFolder);
            }
            //delete folder < 30
            string[] FolderList = Directory.GetDirectories(string.Format("{0}{1}{2}", LogPath, "\\", LogDir.System + (int)Index));

            if (FolderList.Length > LogFolderLimit)
            {
                for (int i = 0; i < FolderList.Length - LogFolderLimit; i++)
                {
                    Directory.Delete(FolderList[i], true);
                }
            }
        }

        #endregion

        #region log Table
        public enum LogDir
        {
            System = 0,
            Robot,
            LP,
            Aligner,
            E84,
            Stage,
            OCRReader,
            IO,
            Error,
            Alarm,
            Operation,
            SECS,
            Inspection,
            Other,
            MaxCnt,
        }

        private class log_mail
        {
            public string message;
            public LogDir type;
            public string LogFolder;
            public string Time;
        }
        #endregion

    }
}
