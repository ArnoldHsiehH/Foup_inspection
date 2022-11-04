using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace new_inspection
{
    public class Mitsubishi_PLC_Send
    {
        Error err_write = new Error();

        private BlockQueue<string> BK_RTSend;
        private Thread TD_RTSend;
        private Thread TD_Server;

        //Mc protocol 連線
        private TcpClient tcpclt;
        private string ip;
        private int port = -1;
        private bool ConnectionStatus = false;

        //MC Procotol Send 參數
        private object lk_Send = new object();
        private bool Send_Busy = true;
        private DateTime Send_Time;

        Stopwatch Stopwatch = new Stopwatch();

        public void Initial(string ref_ip, int ref_port)
        {
            ip = ref_ip;
            port = ref_port;
            tcpclt = new TcpClient();

            BK_RTSend = new BlockQueue<string>();
            TD_Server = new Thread(DoworkServer);
            TD_Server.IsBackground = true;
            TD_Server.Start();
            //TD_RTSend.Start();
        }


        //Sockek Connection check
        private void DoworkServer()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(1);

                    if (tcpclt == null || !ConnectionStatus)
                    {
                        CheckConn();
                    }

                    if (tcpclt.Client == null) continue;

                    string[] temp_send = BK_RTSend.DeQueue(-1).Split(',');
                    ShowLog = false;

                    if (temp_send.Length > 1 && temp_send[1] == "T")
                    {
                        ShowLog = true;
                        //  UI.Log(LogDir.PLC, "SEND", string.Format("Send:{0}", temp_send));
                    }

                    if (tcpclt.Connected)
                    {
                        byte[] testbyte = new byte[0];
                        tcpclt.Client.Send(testbyte, 0, 0);
                    }
                    else
                    {
                        CheckConn();
                        continue;
                    }

                    if (tcpclt.Connected)
                    {
                        byte[] tmpByte = System.Text.Encoding.ASCII.GetBytes(temp_send[0]);
                        tcpclt.Client.Send(tmpByte);
                    }

                    DateTime cmd_Time = DateTime.Now;

                    while (true)
                    {

                        Thread.Sleep(1);
                        if ((DateTime.Now - cmd_Time).TotalSeconds > 60)
                        {
                            //     UI.Log(LogDir.PLC, "Receive", string.Format("Receive TimeOut"));
                            break;
                        }

                        if (tcpclt.Connected && tcpclt.Available > 0)//ns.CanRead && ns.DataAvailable
                        {
                            byte[] buff = new byte[tcpclt.ReceiveBufferSize];
                            tcpclt.Client.Receive(buff);
                            string temp = Encoding.ASCII.GetString(buff, 0, buff.Length).Trim((char)0);

                            if (string.IsNullOrEmpty(temp)) continue;

                            string Receive = "";
                            int Receive_Count = 0;

                            string ReceiveCheck = temp.Length > 21 ? temp.Substring(18, 4) : "9999";
                            Receive_Count = Convert.ToInt32(temp.Substring(14, 4), 16) - 4;

                            if (ReceiveCheck != "0000")
                            {
                                //          UI.Log(LogDir.PLC, "Receive", string.Format("Receive Error , Error Code : {0} ,Prt : {1}", ReceiveCheck, port));
                                break;
                            }
                            else
                            {
                                //if (ShowLog)
                                //                UI.Log(LogDir.PLC, "Receive", string.Format("Receive OK"));
                                break;
                            }
                        }
                    }
                }
                catch
                {
                    CheckConn();
                }
            }
        }
        static bool connect_flag = true;
        private void CheckConn()
        {
            tcpclt = null;
            tcpclt = new TcpClient();
            try
            {
                SpinWait.SpinUntil(() => false, 2000);
                var result = tcpclt.BeginConnect(ip, port, null, null);
                var success = result.AsyncWaitHandle.WaitOne(100);

                if (success)
                {
                    connect_flag = true;
                    ConnectionStatus = true;
                }
                else
                {
                    if (connect_flag)
                    {
                        connect_flag= false;               
                        err_write.write_alarmMessage( Error.error_unit.PLC, "PLC_connect_error");
                    }
                    tcpclt.Close();
                    ConnectionStatus = false;
                }
            }
            catch
            {
                tcpclt.Close();
                ConnectionStatus = false;
            }
        }

        bool ShowLog = true;
        //Procee Send

        public void SendToPLC(string ref_Sendname, PLC_Device ref_DeviceName, int ref_Position, int value)
        {
            lock (lk_Send)
            {
                string DeviceName = "";
                string temp_Value = "";
                string SubCommand = "";
                switch (ref_DeviceName)
                {
                    case PLC_Device.B:
                        DeviceName = "B*";
                        temp_Value = value.ToString();
                        SubCommand = "0001";
                        break;
                    case PLC_Device.W:
                        DeviceName = "W*";
                        temp_Value = Convert.ToString(value, 16).PadLeft(4, '0');
                        SubCommand = "0000";
                        break;
                }

                string temp_DevicePosition = Convert.ToString(ref_Position, 16).PadLeft(6, '0');
                string Log = "F";
                if (ref_Sendname != "Interface")
                {
                    Log = "T";
                    //  UI.Log(LogDir.PLC, "SEND", string.Format("Device:{0}{1},Value:{2}", DeviceName, temp_DevicePosition, Convert.ToInt32(temp_Value, 16)));
                }

                string Send = string.Format("00101401{3}{0}{1}0001{2}", DeviceName, temp_DevicePosition, temp_Value, SubCommand);
                string temp = string.Format("500000FF03FF00{0}{1},{2}", Convert.ToString(Send.Length, 16).PadLeft(4, '0'), Send, Log);

                BK_RTSend.EnQueue(temp);
            }
        }

        public void SendToPLC_Multi(string ref_Sendname, PLC_Device ref_DeviceName, int ref_Position, int ref_Count, int value)
        {

            string DeviceName = "";
            switch (ref_DeviceName)
            {
                case PLC_Device.B:
                    DeviceName = "B*";
                    break;
                case PLC_Device.W:
                    DeviceName = "W*";
                    break;
            }

            string temp_DevicePosition = Convert.ToString(ref_Position, 16).PadLeft(6, '0');
            string temp_Count = Convert.ToString(ref_Count, 16).PadLeft(4, '0');
            string temp_Value = Convert.ToString(value, 16).PadLeft(4, '0');

            string temp_Log = "F";
            if (ref_Sendname != "Interface")
            {
                // UI.Log(LogDir.PLC, "SEND", string.Format("Device:{0}{1} Count:{3},Value:{2}", DeviceName, temp_DevicePosition, temp_Value, temp_Count));
            }

            string Send = string.Format("001014010000{0}{1}{2}", DeviceName, temp_DevicePosition, temp_Count);
            for (int i = 0; i < ref_Count; i++)
            {
                Send += temp_Value;
            }

            string temp = string.Format("500000FF03FF00{0}{1}", Convert.ToString(Send.Length, 16).PadLeft(4, '0'), Send);
            lock (lk_Send)
            {
                BK_RTSend.EnQueue(temp);
            }
        }

        public void SendToPLC_Random(SendPLCRandom[] tempSendPLC)
        {
            string Send = string.Format("001014020000{0}00", Convert.ToString(tempSendPLC.Length, 16).PadLeft(2, '0'));

            for (int i = 0; i < tempSendPLC.Length; i++)
            {
                string DeviceName = "";
                if (tempSendPLC[i].DeviceType == null)
                {
                    continue;
                }
                switch (tempSendPLC[i].DeviceType)
                {
                    case PLC_Device.B:
                        DeviceName = "B*";
                        break;
                    case PLC_Device.W:
                        DeviceName = "W*";
                        break;
                }

                string temp_DevicePosition = Convert.ToString(tempSendPLC[i].DevicePosition, 16).PadLeft(6, '0');
                string temp_Value = Convert.ToString(tempSendPLC[i].DeviceValue, 16).PadLeft(4, '0');
                Send += string.Format("{0}{1}{2}", DeviceName, temp_DevicePosition, temp_Value);
            }

            string temp = string.Format("500000FF03FF00{0}{1},{2}", Convert.ToString(Send.Length, 16).PadLeft(4, '0'), Send, "T").ToUpper(); ;
            lock (lk_Send)
            {
                BK_RTSend.EnQueue(temp);
            }
        }
    }

    //Random Struct
    public class SendPLCRandom
    {
        public PLC_Device DeviceType { get; set; }
        public int DevicePosition { get; set; }
        public int DeviceValue { get; set; }

    }
}
