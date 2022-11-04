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
    class Mitsubishi_PLC_Get_B
    {
        private BlockQueue<string> BK_RTRecv;
        private BlockQueue<Send_Str> BK_RTSend;
        private Thread TD_RTRecv;
        private Thread TD_RTSend;
        private Thread TD_Server;

        //Mc protocol 連線
        private TcpClient tcpclt;
        private string ip;
        private int port = -1;
        private bool ConnectionStatus = false;

        //MC Procotol Send 參數
        private string DeviceType= "04010000"; //Read
        private string DeviceName = ""; //PLC_Device
        private int Start_Position = 0;//PLC_Device Start
        private int Current_Psitin = 0;

        private int SendStep = 0;//For Get 所有Bit值，Bit 需要丟3次 0~7FFF

        public void Initial(string ref_ip, int ref_port,string ref_Device,int ref_Start) 
        {
            ip = ref_ip;
            port = ref_port;
            DeviceName = ref_Device;
            Start_Position = ref_Start;
            tcpclt = new TcpClient();

            BK_RTSend = new BlockQueue<Send_Str>();
            BK_RTRecv = new BlockQueue<string>();
            TD_Server = new Thread(DoworkServer);
            TD_RTRecv = new Thread(Dowrok_RTRecv);
            TD_RTSend = new Thread(Dowork_RTSend);
            TD_Server.IsBackground = true;
            TD_RTRecv.IsBackground = true;
            TD_RTSend.IsBackground = true;
            TD_Server.Start();
            TD_RTRecv.Start();
            TD_RTSend.Start();
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

                    if (tcpclt.Connected)
                    {
                        //ns = tcpclt.GetStream();
                        byte[] testbyte = new byte[0];
                        tcpclt.Client.Send(testbyte, 0, 0);
                        string Receive = "";
                        bool Receive_Header = true;
                        int Receive_Count = 0;


                        if (tcpclt.Connected && tcpclt.Available > 0)//ns.CanRead && ns.DataAvailable
                        {
                            byte[] buff = new byte[tcpclt.ReceiveBufferSize];
                            tcpclt.Client.Receive(buff);
                            string temp = Encoding.ASCII.GetString(buff, 0, buff.Length).Trim((char)0);
                            
                            if (string.IsNullOrEmpty(temp)) continue;
                            while (true)
                            {
                                if (Receive_Header)
                                {
                                    string ReceiveCheck = temp.Length > 21 ? temp.Substring(18, 4) : "9999";
                                    Receive_Count = Convert.ToInt32(temp.Substring(14, 4), 16) -4;

                                    if (ReceiveCheck != "0000")
                                    {
                                 //       UI.Log(LogDir.PLC, "Receive", string.Format("Receive Error , Error Code : {0}", ReceiveCheck));
                                        SendStep = 0;
                                        Send_PLC_Structure(0, 511);
                                        break;
                                    }
                                    else
                                    {
                                        string ReceiveString = temp.Substring(22, temp.Length - 22);
                                        Receive += ReceiveString;
                                        Receive_Header = false;
                                        if (Receive.Length == Receive_Count)
                                        {
                                            BK_RTRecv.EnQueue(Receive);
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (tcpclt.Connected && tcpclt.Available > 0)//ns.CanRead && ns.DataAvailable
                                    {
                                        buff = new byte[tcpclt.ReceiveBufferSize];
                                        tcpclt.Client.Receive(buff);
                                        temp = Encoding.ASCII.GetString(buff, 0, buff.Length).Trim((char)0);
                                        Receive += temp;

                                        if (Receive.Length  == Receive_Count)
                                        {
                                            BK_RTRecv.EnQueue(Receive);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        CheckConn();
                    }
                }
                catch
                {
                    CheckConn();
                }
            }
        }
        private void CheckConn()
        {
            tcpclt = new TcpClient();
            try
            {
                SpinWait.SpinUntil(() => false, 2000);
                var result = tcpclt.BeginConnect(ip, port, null, null);
                var success = result.AsyncWaitHandle.WaitOne(100);

                if (success) 
                {
                    ConnectionStatus = true;
                    misubushi_IO.connect = true;
                    SendStep = 0;
                    Send_PLC_Structure(0, 511);
                    
                }
                else 
                {
                    tcpclt.Close();
                    ConnectionStatus = false;
                    misubushi_IO.connect = false;
                }
            }
            catch
            {
                tcpclt.Close();
                ConnectionStatus = false;
                misubushi_IO.connect = false;
            }
        }

        Stopwatch watch = new Stopwatch();
        //Process Recrive
        private void Dowrok_RTRecv()
        {
            while (true)
            {
                string recv_str = BK_RTRecv.DeQueue(System.Threading.Timeout.Infinite);
                try
                {
                    string ReceiveString = recv_str;
                    for (int i = 0; i < ReceiveString.Length / 4; i++)
                    {
                        string temp_Byte_Value = Convert.ToString(Convert.ToInt32(ReceiveString.Substring(i * 4, 4), 16), 2).PadLeft(16, '0');
                        int idx = 15;
                        for (int j = 0; j < 16; j++)
                        {
                            misubushi_IO.PLC_B_Value[i * 16 + j + Current_Psitin] = temp_Byte_Value[idx] == '1' ? 1 : 0;
                            idx--;
                        }
                    }
                    Send_PLC_Structure(0, 511);

                    //switch (SendStep)
                    //{
                    //    case 0:
                    //        SendStep++;
                    //        Current_Psitin = (SendStep * 15360);
                    //        Send_PLC_Structure(15360, 960);

                    //        break;
                    //    case 1:
                    //        SendStep++;
                    //        Current_Psitin = (SendStep * 15360);
                    //        Send_PLC_Structure(30720, 128);
                    //        break;
                    //    case 2:
                    //        SendStep = 0;
                    //        Current_Psitin = (SendStep * 15360);
                    //        break;
                    //}
                }
                catch(Exception ex) 
                {
                 //   UI.Log(LogDir.Error, "PLC_B_Receive", string.Format("Message : {0}\r\nStackTrace : {1} \r\nInnerException : {2}", 
                                                                        //ex.Message,ex.StackTrace,ex.InnerException));
                    SendStep = 0;
                    Current_Psitin = (SendStep * 15360);
                    Send_PLC_Structure(0, 511);
                }

            }
        }
        //Procee Send
        private void Dowork_RTSend()
        {
            while (true)
            {
                Send_Str temp_send = BK_RTSend.DeQueue(System.Threading.Timeout.Infinite);
                Thread.Sleep(1);

                if (tcpclt.Connected)
                {
                    string Send = string.Format("0010{0}{1}{2}{3}", temp_send.DeviceType, temp_send.DeviceName, temp_send.StartPosition, temp_send.PositionCount);
                    string temp = string.Format("500000FF03FF00{0}{1}", Convert.ToString(Send.Length,16).PadLeft(4,'0'), Send).ToUpper();
                    byte[] tmpByte = System.Text.Encoding.ASCII.GetBytes(temp);

                    tcpclt.Client.Send(tmpByte);
                }
                else
                {
                    //MessageBox.Show("1111");
                    //UI.Alarm(Sk_Device.PLC_RT.ToString(),ErrorList.ConnectionError, "PLC DisConnection");
                }
            }
        }

        private void Send_PLC_Structure(int ref_StartPosition,int ref_PLC_Count) 
        {
            Send_Str temp_str = new Send_Str();
            temp_str.DeviceType = "04010000";
            temp_str.DeviceName = "B*";
            temp_str.StartPosition = Convert.ToString(ref_StartPosition, 16).PadLeft(6, '0');
            temp_str.PositionCount = Convert.ToString(ref_PLC_Count, 16).PadLeft(4, '0');
            Enqueue_Send(temp_str);
        }
        private void Enqueue_Send(Send_Str ref_Send) 
        {
            BK_RTSend.EnQueue(ref_Send);
        }

        private Stopwatch sw =new Stopwatch();

        //GetSet
        public bool GetSet_Connection 
        {
            set 
            {
                if (ConnectionStatus == value) return;
                ConnectionStatus = value;
            }
            get 
            {
                return ConnectionStatus;
            }
        }


        private class Send_Str
        {
            public string DeviceType { get; set; } //批次讀取 or 批次寫入 04010000   14010000
            public string DeviceName { get; set; } //PLC元件
            public string StartPosition { get; set; } //Devie 開始讀取點位
            public string PositionCount { get; set; }//讀取數量
        }
    }
}
