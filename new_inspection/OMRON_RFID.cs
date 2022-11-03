using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace new_inspection
{
    class OMRON_RFID
    {
        logwriter01 logwriter = new logwriter01();
        Error err_write = new Error();
        RF_Serial_driver RFID = new RF_Serial_driver("RFID");
        RF_subscriber user_txb = new RF_subscriber("txt");//新增訂閱用戶
        RF_subscriber reply_F100 = new RF_subscriber("reply_F100");
        RF_subscriber read_RFID = new RF_subscriber("read_RFID");
        int portname = new int();

        public void Initial(int number)
        {
            logwriter.setLogType = logwriter01.LogDir.LP;
            logwriter.setDevice_Name = "RFID";
            portname = number;
            RFID.Initial(number);
            RFID.create_subscriber(user_txb, "txt");
            RFID.add_subscribe(user_txb);
        }
        public void read_log(out string str)
        {
            str = "";
            RFID.read(user_txb, out str);
        }
        public void send(string str)
        {
            RFID.send(str);
        }

        public bool check_connection()//check_connection 1012345678
        {
            logwriter.write_local_log("check_connection");
            string anser = "";
            bool success = false;
            RF_subscriber Subscriber = reply_F100;

            int times = 0;
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 100; // 觸發時間
            timer.AutoReset = true; // 重複觸發
            timer.Elapsed += (s, e) => // 觸發時執行的事件
            {
                times++;
            };
            timer.Start(); // 啟動定時器
            RFID.add_subscribe(Subscriber);//順序需要測試
            RFID.send("1012345678");
            while (true)
            {
                RFID.read(Subscriber, out anser);
                //anser = anser.Substring(2, anser.Length - 2);

                if (anser != "")
                {
                    break;
                }
                if (times > 20)
                    break;
            }
            timer.Stop();
            if (anser == "0012345678")
            {
                success = true;
                Console.WriteLine("complete");
            }
            else if (times > 20)//timeout check
            {
                err_write.write_alarmMessage( Error.error_unit.RFID,string.Format("RFID timeout: {0}", portname));
                //Console.WriteLine("timeout");
            }
            else
            {
                anser = "";
                Console.WriteLine("wrong ans");
            }
            RFID.unsubscribe(Subscriber);
            timer.Stop();
            timer.Close();

            return success;

        }

        public bool READ_RFID(string layer, out string receive)//check_connection 1012345678
        {
            logwriter.write_local_log("READ_RFID");
            string anser = "";
            bool success = false;
            if (layer.Length != 8)
            {
                receive = "Format_fail";
                return success;
            }


            RF_subscriber Subscriber = read_RFID;

            int times = 0;
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 100; // 觸發時間
            timer.AutoReset = true; // 重複觸發
            timer.Elapsed += (s, e) => // 觸發時執行的事件
            {
                times++;
            };
            timer.Start(); // 啟動定時器
            RFID.add_subscribe(Subscriber);//順序需要測試
            RFID.send(string.Format("0100{0}", layer));
            while (true)
            {
                RFID.read(Subscriber, out anser);
                //anser = anser.Substring(2, anser.Length - 2);

                if (anser != "")
                {
                    break;
                }
                if (times > 20)
                    break;
            }
            timer.Stop();
            if (anser != "")
            {
                if (anser == "72")
                {
                    anser = "";
                   
                    err_write.write_alarmMessage(Error.error_unit.RFID, string.Format("RFID NOT FOUND"));
                }
                else//ascii 轉換成foup id
                {
                    string output = "";

                    for (int i = 0; i < 8; i++)///需要改回8
                    {
                        int buffer;
                        string b = string.Format("{0}{1}", anser[i * 2 + 2], anser[i * 2 + 3]);
                        if (b != "00")
                        {
                            buffer = Convert.ToInt32(b, 16);
                            char ascii_txt = (char)buffer;
                            output = output + ascii_txt;
                        }
                    }
                    anser = output.Trim();

                }
                success = true;
                Console.WriteLine("complete");
            }
            else if (times > 20)//timeout check
            {
                err_write.write_alarmMessage( Error.error_unit.RFID,string.Format("RFID timeout: COM{0}", portname));
                Console.WriteLine("timeout");
            }
            else
            {
                anser = "";
                Console.WriteLine("wrong ans");
            }
            RFID.unsubscribe(Subscriber);
            timer.Stop();
            timer.Close();
            receive = anser;
            return success;

        }
    }
    class RF_Serial_driver
    {
        RF_newspaper_office Newsboy = new RF_newspaper_office("ITRI");  // 發送者   (將接收的資料發送給訂閱者 )
        Error err_write = new Error();
        public string DeviceName;

        //RS-232 
        private SerialPort RFID_COM;
        private List<byte> ReceiveTemp = new List<byte>();
        private BlockQueue<string> ReceiverQueue = new BlockQueue<string>();

        Thread t_rec;

        public RF_Serial_driver(string name)
        {
            Newsboy = new RF_newspaper_office(name);
        }

        public void Initial(int number)
        {
            DeviceName = string.Format("{0}{1}", "RFID", number);

            #region  RS232 Setting


            RFID_COM = new SerialPort(string.Format("COM{0}", number));
            // RFID_COM.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.Received);
            RFID_COM.BaudRate = 9600;
            RFID_COM.DataBits = 8;
            RFID_COM.Parity = Parity.Even;
            RFID_COM.StopBits = StopBits.One;

            #endregion

            RFID_COM.Close();
            try
            {
                RFID_COM.Open();
            }
            catch
            {
                err_write.write_alarmMessage(Error.error_unit.RFID, string.Format("RFID connect fail: {0}", RFID_COM.PortName));
                Console.WriteLine("RFID connect fail");
                return;
            }
            // t_rec=

            t_rec = new Thread(Received);
            t_rec.Start();
        }
        public void COM_Disconnect()
        {
            RFID_COM.Close();
        }
        public void create_subscriber(RF_subscriber A, string name)
        {
            A = new RF_subscriber(name);
        }
        public void add_subscribe(RF_subscriber A)
        {
            Newsboy.Newsboy_Event += new RF_newspaper_office.Newsboy_Handler(A.get_paper);
            Console.WriteLine("add_subscribe: {0}", A.name);
        }
        public void unsubscribe(RF_subscriber A)
        {
            Newsboy.Newsboy_Event -= new RF_newspaper_office.Newsboy_Handler(A.get_paper);
            Console.WriteLine("unsubscribe: {0}", A.name);
        }
        public void read(RF_subscriber A, out string message)
        {
            string str = "";
            A.read_news(out str);
            message = str;
        }
        private object send_lock = new object();//卡
        public void send(string Command_String)
        {
            lock (send_lock)
            {
                if (RFID_COM.IsOpen)
                {
                    string Send_String = Command_String;
                    List<byte> Command_Byte = new List<byte>(Encoding.ASCII.GetBytes(Send_String));
                    Command_Byte.Add(0x0D);//CR
                    RFID_COM.Write(Command_Byte.ToArray(), 0, Command_Byte.Count);
                    //UI.Log(NormalStatic.RFID, DeviceName, SystemList.DeviceSend, Command_String);

                }
                else
                {
                    err_write.write_alarmMessage(Error.error_unit.RFID, string.Format("RFID connect fail: {0}", RFID_COM.PortName));                    
                    Console.WriteLine("RFID connect fail");
                }
            }
        }
        private void Received()
        {
            byte[] raw_byte = new byte[100];
            List<Byte> tempList = new List<Byte>();

            while (true)
            {
                if (!RFID_COM.IsOpen)
                    break;
                if (RFID_COM.BytesToRead > 0)
                {
                    raw_byte = new byte[100];
                    tempList.Clear();

                    int times = 0;
                    int i = 0;
                    System.Timers.Timer timer = new System.Timers.Timer();
                    timer.Interval = 100; // 觸發時間
                    timer.AutoReset = true; // 重複觸發
                    timer.Elapsed += (s, e) => // 觸發時執行的事件
                    {
                        times++;
                    };
                    timer.Start(); // 啟動定時器
                    while (times < 10)
                    {

                        int rec_buffer = RFID_COM.ReadByte();
                        if (rec_buffer == 0x0D)//CR
                        {
                            string str = "";
                            ASCIIEncoding encoding = new ASCIIEncoding();
                            str = encoding.GetString(tempList.ToArray());

                            Newsboy.send_paper(str);
                            tempList.Clear();
                            break;
                        }
                        else
                        {
                            tempList.Add((Byte)rec_buffer);

                            i++;
                            times = 0;
                        }

                    }
                    timer.Stop();
                    timer.Close();

                }
            }


            SerialPort sp = RFID_COM;
            //Thread.Sleep(100);
            while (sp.BytesToRead > 0)
            {
                sp.Read(raw_byte, 0, 1);
            }
            Newsboy.send_paper(raw_byte.ToString());
        }
    }
    class RF_newspaper_office
    {
        private string name;
        public RF_newspaper_office(string name)
        {
            this.name = name;
        }
        // 委派(方法類別)
        public delegate void Newsboy_Handler(string a);

        // 事件(方法變數)
        public event Newsboy_Handler Newsboy_Event;

        public void send_paper(string str)
        {
            Console.WriteLine("Hi i am {0}", name);
            if (Newsboy_Event != null)
            {
                Newsboy_Event(str);
            }
        }

    }
    class RF_subscriber
    {
        Queue<string> news = new Queue<string>();
        public string name;
        public RF_subscriber(string name)
        {
            this.name = name;
        }
        public void get_paper(string a)//(string newspaper)
        {
            // string newspaper = "aaa";
            news.Enqueue(a);
            Console.WriteLine("send {1} to {0}", name, a);
        }
        //public void get_paper(string a, Queue<string> otherQ)//(string newspaper)
        //{
        //    // string newspaper = "aaa";
        //    otherQ.Enqueue(a);
        //    Console.WriteLine("send {1} to {0}", name, a);
        //}

        public void read_news(out string message)
        {
            if (news.Count > 0)
                message = news.Dequeue();
            else
                message = "";
        }
    }
}
