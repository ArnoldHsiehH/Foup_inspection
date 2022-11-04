using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace new_inspection
{
    class PLC_motion
    {
        public enum motion_status
        {
            success,
            fail
        }
        public void AlarmReset()
        {
            misubushi_IO.setB(0x100, 1);
            Thread.Sleep(50);
            misubushi_IO.setB(0x100, 0);

        }
        public void Stop()
        {
            ioSet(0x3, 1);
            Thread.Sleep(50);
            ioSet(0x3, 0);

        }
        #region IO 讀寫
        public int w_set(int address, int data)
        {
            Console.WriteLine("wSet address {0} set {1}", address, data);
            misubushi_IO.setW(address, data);
            return 1;
        }
        public int w_read(int address)
        {
            // Console.WriteLine("w_read address {0} ", address);
            int value;
            misubushi_IO.getW(address, out value);
            return value;
        }

        public bool ioSet(int address, int data)
        {
            int n = 0;
            System.Timers.Timer timer = new System.Timers.Timer();

            int value = 99;

            // Console.WriteLine("ioSet address {0,10} >> {1}", address, data);
            misubushi_IO.setB(address, data);

            //Thread.Sleep(1);
            DateTime dateTime = DateTime.Now;
            while ((DateTime.Now - dateTime).TotalSeconds < 2)
            {
                misubushi_IO.getB(address, out value);
                if (value == data)
                    break;
                n++;
            }
            //Console.WriteLine("n={0}", n);
            misubushi_IO.getB(address, out value);
            if (value == data)
            {
                return true;
            }
            return false;
        }

        public void IoSet_fast(int address, int data)
        {
            misubushi_IO.setB(address, data);
            Console.WriteLine("ioSet address {0} >> {1}", address, data);
        }
        private bool ioWait_bit(int address, bool status, int waitingTime)
        {
            Console.WriteLine("ioWait_bit address {0}  ", address);

            int commend_complete = 1;
            int t = 0;
            bool value;
            DateTime dateTime = DateTime.Now;

            while ((DateTime.Now - dateTime).TotalSeconds < waitingTime)
            {
                value = ioRead_On(address);
                if (status == value) //commend_complete 動作完成
                {
                    commend_complete = 0;
                    break;
                }

                t++;
                commend_complete = 1;//應該=1 暫時pass調
                Thread.Sleep(1);
            }
            if (commend_complete != 0)
            { //timeout
               // error_code.enQ("ioWait_bit address 0x" + Convert.ToString(address, 16) + " timeout!");
                Console.WriteLine("ioWait_bit address " + Convert.ToString(address) + " timeout!");
              //  logger.Trace("Wait B{0} fail {1} ", address, status);
                return false;
            }
           // logger.Trace("Wait B{0} {1} ", address, status);
            return true;
        }
        private bool ioWait_bit(int address, bool status, int waitingTime, int error)
        {


            int commend_complete = 1;
            int t = 0;
            bool value;
            DateTime dateTime = DateTime.Now;

            while ((DateTime.Now - dateTime).TotalSeconds < waitingTime)
            {
                value = ioRead_On(address);
                if (status == value) //commend_complete 動作完成
                {
                    commend_complete = 0;
                    break;
                }
                //if (ioRead_On(error) == true)///異常腳位暫時無用
                //{
                //    error_code.enQ("ioWait_bit address 0x" + Convert.ToString(address, 16) + " motion error!");
                //    break;
                //}
                t++;
                commend_complete = 1;//應該=1 暫時pass調
                Thread.Sleep(1);
            }
            if (commend_complete != 0)
            { //timeout
             //   error_code.enQ("ioWait_bit address 0x" + Convert.ToString(address, 16) + " timeout!");
                Console.WriteLine("ioWait_bit address " + Convert.ToString(address) + " timeout!");
               // logger.Trace("Wait B{0} fail {1} ", address, status);
                return false;
            }
            //logger.Trace("Wait B{0} {1} ", address, status);
            return true;
        }
        public bool ioRead_On(int address)
        {
            int value;
            misubushi_IO.getB(address, out value);
            return ((value == 1) ? true : false);
        }
        private bool ioRead_Off(int address)
        {
            int value;
            misubushi_IO.getB(address, out value);
            Console.WriteLine("ioRead_Off read {0} address set ", address);
            return true;
        }
        #endregion
    }
}
