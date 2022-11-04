using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace new_inspection
{
    internal class misubushi_IO
    {
        static logwriter01 logwriter = new logwriter01();

        public static int[] PLC_W_Value;
        public static int[] PLC_B_Value;
        public static bool connect;

        private Mitsubishi_PLC_Get_B PLC_B_Get;
        private Mitsubishi_PLC_Get_W PLC_W_Get_1;
        private Mitsubishi_PLC_Get_W PLC_W_Get_2;
        private Mitsubishi_PLC_Get_W PLC_W_Get_3;
        public static Mitsubishi_PLC_Send PLC_Send;

        string ref_PLCIP;
        int ref_Port;
        public void Initial()
        {
            logwriter.setLogType = logwriter01.LogDir.IO;
            logwriter.setDevice_Name = "PLC";
            //ref_PLCIP = "192.168.0.100";
            ref_PLCIP = "192.168.0.12";
            //ref_PLCIP = "192.168.0.99";
            ref_Port = 4000;

            PLC_Send = new Mitsubishi_PLC_Send();
            PLC_Send.Initial(ref_PLCIP, ref_Port);
            PLC_B_Value = new int[32768];
            PLC_W_Value = new int[32768];
            PLC_W_Get_1 = new Mitsubishi_PLC_Get_W();
            PLC_W_Get_1.Initial(ref_PLCIP, ref_Port + 1, "W*", 0, false);
            PLC_W_Get_2 = new Mitsubishi_PLC_Get_W();
            PLC_W_Get_2.Initial(ref_PLCIP, ref_Port + 2, "W*", 4800, false);
            PLC_W_Get_3 = new Mitsubishi_PLC_Get_W();
            PLC_W_Get_3.Initial(ref_PLCIP, ref_Port + 3, "W*", 9600, false);
            //PLC_W_Get_4 = new Mitsubishi_PLC_Get_W();
            //PLC_W_Get_4.Initial(ref_PLCIP, ref_Port + 4, "W*", 14400, false);
            //PLC_W_Get_5 = new Mitsubishi_PLC_Get_W();

            connect = new bool();

            PLC_B_Get = new Mitsubishi_PLC_Get_B();
            PLC_B_Get.Initial(ref_PLCIP, ref_Port + 4, "B*", 0);
            // PLC_B_Get.Initial(ref_PLCIP, ref_Port + 8, "B*", 0);
        }
        public static bool connected() 
        {

            return connect;
        }

        public static void getB(int address, out int value)
        {
            value = PLC_B_Value[address];
         //   logger.Debug("get B{0} as  {1}", address, value);
        }
        public static void getW(int address, out int value)
        {
            value = PLC_W_Value[address];
            //logger.Debug("get W{0} as  {1}", address, value);
        }
        public static void setB(int address, int value)
        {
            
            PLC_Send.SendToPLC("Interface", PLC_Device.B, address, value);//(string ref_Sendname, PLC_Device ref_DeviceName, int ref_Position, int value)
            logwriter.write_local_log(string.Format("SET B{0} as  {1}", address, value));
            
        }
        public static void setW(int address, int value)
        {
            PLC_Send.SendToPLC("Interface", PLC_Device.W, address, value);
            logwriter.write_local_log(string.Format("SET W{0} as  {1}", address, value));            
        }
    }
    public enum PLC_Device
    {
        B,
        W
    }
}
