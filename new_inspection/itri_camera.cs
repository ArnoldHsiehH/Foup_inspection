using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace new_inspection
{
    internal class ITRI_protocol
    {
        static logwriter01 logwriter = new logwriter01();
        static Error err_write = new Error();

        static TCP_client client = new TCP_client();
        static subscriber user_txb = new subscriber("txt");//新增訂閱用戶
        static subscriber reply_F100 = new subscriber("reply_F100");
        static subscriber reply_F101 = new subscriber("reply_F101");
        static subscriber reply_F103 = new subscriber("reply_F103");
        static subscriber reply_F200 = new subscriber("reply_F200");
        public static bool connection_fild = new bool();


        public void Initial()
        {
            logwriter.setLogType = logwriter01.LogDir.Inspection;
            logwriter.setDevice_Name = "INSP";
            client.ini();
            client.create_subscriber(user_txb, "txt");
            client.add_subscribe(user_txb);
        }
        public static void read_log(out string str)
        {
            str = "";
            client.read(user_txb, out str);
        }
        public static void send(string str)
        {
            client.send(str);
        }
        public static bool check_connection()//F100
        {

            string anser = "";
            string[] anser_Split;
            int times = 0;
            bool success = false;
            subscriber Subscriber = reply_F100;

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 100; // 觸發時間
            timer.AutoReset = true; // 重複觸發
            timer.Elapsed += (s, e) => // 觸發時執行的事件
            {
                times++;
            };
            timer.Start(); // 啟動定時器
            client.add_subscribe(Subscriber);//順序需要測試
            client.send("F100");
            while (true)
            {
                client.read(Subscriber, out anser);
                anser_Split = anser.Split(',');
                if (anser_Split[0] == "F100" && anser_Split.Length == 2)
                {
                    break;
                }
                if (times > 10)
                    break;
                if (connection_fild)
                    break;
            }
            if (times > 10)//timeout check
                Console.WriteLine("timeout");
            else if (anser_Split[0] == "F100" && anser_Split.Length == 2)
            {
                success = true;
                if (anser_Split[1] == "1")
                {

                    Console.WriteLine("complete");
                }
                else
                {
                    success = false;
                    Console.WriteLine("wrong ans");
                }
            }
            else//理論上不會發生
            {
                Console.WriteLine("wrong ans");
            }
            client.unsubscribe(Subscriber);
            timer.Stop();
            timer.Close();
            return success;

        }
        public static bool check_cameras(out string result)//F101
        {
            string anser = "";
            string[] anser_Split;
            int times = 0;
            bool success = false;
            result = "fail";
            subscriber Subscriber = reply_F101;

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 100; // 觸發時間
            timer.AutoReset = true; // 重複觸發
            timer.Elapsed += (s, e) => // 觸發時執行的事件
            {
                times++;
            };
            timer.Start(); // 啟動定時器

            client.add_subscribe(Subscriber);//順序需要測試
            client.send("F101");// "F101";
            while (true)
            {
                client.read(Subscriber, out anser);
                anser_Split = anser.Split(',');
                if (anser_Split[0] == "F101" && anser_Split.Length == 6)
                    break;
                if (times > 10)
                    break;
                if (connection_fild)
                    break;
            }
            timer.Stop();
            if (times > 10)//timeout check
                Console.WriteLine("timeout");
            else if (anser_Split[0] == "F101" && anser_Split.Length == 6)
            {
                success = true; //回報成功
                result = "";
                Console.WriteLine("complete");
                for (int i = 1; i < 6; i++)
                {
                    result = result + anser_Split[i] + ",";//回報結果
                }
            }
            else//理論上不會發生
            {
                Console.WriteLine("wrong ans");
            }
            client.unsubscribe(Subscriber);

            timer.Close();
            return success;

        }
        public static bool check_version(out string result)//F103
        {


            string anser = "";
            string[] anser_Split;
            int times = 0;
            bool success = false;
            result = "fail";
            subscriber Subscriber = reply_F103;

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 100; // 觸發時間
            timer.AutoReset = true; // 重複觸發
            timer.Elapsed += (s, e) => // 觸發時執行的事件
            {
                times++;
            };
            timer.Start(); // 啟動定時器

            client.add_subscribe(Subscriber);//順序需要測試
            client.send("F103");// "F103";
            while (true)
            {
                client.read(Subscriber, out anser);
                anser_Split = anser.Split(',');
                if (anser_Split[0] == "F103")
                    break;
                if (times > 10)
                    break;
                if (connection_fild)
                    break;
            }
            timer.Stop();
            if (times > 10)//timeout check
                Console.WriteLine("timeout");
            else if (anser_Split[0] == "F103" && anser_Split.Length == 3)
            {
                success = true; //回報成功
                result = anser;
                Console.WriteLine("complete");

            }
            else//
            {
                Console.WriteLine("connection fild");
            }
            client.unsubscribe(Subscriber);

            timer.Close();
            return success;

        }
        public static bool AI_inspection(string content, string foupID, out string result)//F200
        {

            string anser = "";
            string[] anser_Split;
            int times = 0;
            int timeout = 5000;
            bool success = false;
            result = "";
            subscriber Subscriber = reply_F200;

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 100; // 觸發時間
            timer.AutoReset = true; // 重複觸發
            timer.Elapsed += (s, e) => // 觸發時執行的事件
            {
                times++;
            };
            timer.Start(); // 啟動定時器

            client.add_subscribe(Subscriber);//順序需要測試
            client.send(string.Format("F200,{0},{1}", foupID, content));//("F200," + Convert.ToString(content));// "F200,{0}", content;
            while (true)
            {
                client.read(Subscriber, out anser);
                anser_Split = anser.Split(',');
                //  if (anser_Split[0] == "F200" && anser_Split.Length == 5)
                if (anser_Split[0] == "F200")
                    break;
                if (times > timeout)
                    break;

                if (connection_fild)
                    break;
            }
            timer.Stop();
            if (times > timeout)//timeout check
            {
                Console.WriteLine("ins timeout");

                logwriter.write_local_log("inspection timeout");
              
                //error_code.enQ_txtConsole("ITRI timeout");
                //error_send.

            }
            else if (anser_Split[0] == "F200")
            {
                success = true; //回報成功
                if (anser_Split[2] == Convert.ToString(content) && anser_Split.Length == 5)//回報點位相同
                {
                    Console.WriteLine("complete");
                    result = anser;//回報結果
                }
                else
                {
                    Console.WriteLine("wrong ans");
                }
            }
            else//理論上不會發生
            {
                Console.WriteLine("connection fild");
            }
            client.unsubscribe(Subscriber);

            timer.Close();
            return success;

        }

    }
    class itri_define
    {

    }

    class TCP_client
    {
        static logwriter01 logwriter = new logwriter01();
        static Error err_write = new Error();

        newspaper_office Newsboy = new newspaper_office("ITRI");  // 發送者   (將接收的資料發送給訂閱者 ) 
        // subscriber subscriber_1 = new subscriber("log");                 // 訂閱者1 未使用
        // subscriber subscriber_2 = new subscriber("Ben");                 // 訂閱者2 未使用

        //Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket clientSocket;
        private Thread TD_Server;

        public void ini()
        {
            logwriter.setLogType = logwriter01.LogDir.Inspection;
            logwriter.setDevice_Name = "INSP";
            //clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //clientSocket.Connect(new IPEndPoint(IPAddress.Parse("192.168.1.23"), 502));
            Console.WriteLine("ITRI connected");
            try
            {
                clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 48879));//3919
                //clientSocket.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.99"), 4004));
            }
            catch (ArgumentNullException e)
            {

                string errorcode = string.Format("Camera ArgumentNullException: {0}", e);
                //Console.WriteLine("ArgumentNullException: {0}", e);
                Console.WriteLine(errorcode);
                err_write.write_alarmMessage( Error.error_unit.INSP, errorcode);
               
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                string errorcode = string.Format("Camera SocketException: {0}", e);
               
                err_write.write_alarmMessage(Error.error_unit.INSP, errorcode);
            }

            // Newsboy.Newsboy_Event += new newspaper_office.Newsboy_Handler(subscriber_1.get_paper);

            TD_Server = new Thread(Receive);
            TD_Server.IsBackground = true;
            TD_Server.Start();
        }
        public void create_subscriber(subscriber A, string name)
        {
            A = new subscriber(name);
        }
        public void add_subscribe(subscriber A)
        {
            Newsboy.Newsboy_Event += new newspaper_office.Newsboy_Handler(A.get_paper);
            Console.WriteLine("add_subscribe: {0}", A.name);
        }

        public void unsubscribe(subscriber A)
        {
            Newsboy.Newsboy_Event -= new newspaper_office.Newsboy_Handler(A.get_paper);
            Console.WriteLine("unsubscribe: {0}", A.name);
        }
        public void read(subscriber A, out string message)
        {
            string str = "";
            A.read_news(out str);
            message = str;
        }
        public void send(string str)
        {
            logwriter.write_local_log(str);
            if (clientSocket.Connected)
            {
                str = string.Format("{0}\r\n", str);
                byte[] date = new byte[1024];
                date = Encoding.UTF8.GetBytes(str);
                clientSocket.Send(date);//\r\n
            }
            else
            {
                logwriter.write_local_log("Ins Connected fail");
                err_write.write_alarmMessage(Error.error_unit.INSP, "Ins Connected fail");
                ITRI_protocol.connection_fild = true;
            }

        }

        private void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[500];
                    int r = clientSocket.Receive(buffer);
                    if (r == 0)
                    {
                        break;
                    }
                    string str = Encoding.UTF8.GetString(buffer, 0, r);
                    str = str.Trim();
                    //Console.WriteLine(str);
                    Newsboy.send_paper(str);
                    Thread.Sleep(1);
                }
            }
            catch
            {
                clientSocket.Close();
            }
        }

    }
    class newspaper_office
    {
        private string name;
        public newspaper_office(string name)
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
    class subscriber
    {
        Queue<string> news = new Queue<string>();
        public string name;
        public subscriber(string name)
        {
            this.name = name;
        }
        public void get_paper(string a)//(string newspaper)
        {
            // string newspaper = "aaa";
            news.Enqueue(a);
            Console.WriteLine("send {1} to {0}", name, a);
        }
        public void get_paper(string a, Queue<string> otherQ)//(string newspaper)
        {
            // string newspaper = "aaa";
            otherQ.Enqueue(a);
            Console.WriteLine("send {1} to {0}", name, a);
        }

        public void read_news(out string message)
        {
            if (news.Count > 0)
                message = news.Dequeue();
            else
                message = "";
        }
    }
}
