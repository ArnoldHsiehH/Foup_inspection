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
        private int oneSec { get; } = 1000;
        public int robot_motionTime { get; } = 50;//1000s
        public int robot_handshakeTime { get; } = 10;// 1/1000s
        public int loadport_motionTime { get; } = 50;//1000s
        public int loadport_handshakeTime { get; } = 10;//1/1000s

        static logwriter01 logwriter = new logwriter01();
        Error err_write = new Error();

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

        #region 複雜動作
        public motion_status robotSelect()
        {
            ioSet(0, 0);
            ioSet(1, 0);
            ioSet(2, 0);
            ioSet(3, 0);
            ioSet(4, 0);
            ioSet(5, 0);
            ioSet(6, 0);
            ioSet(7, 0);
            ioSet(8, 0);
            ioSet(9, 0);
            ioSet(10, 0);
            ioSet(11, 0);
            ioSet(12, 0);
            ioSet(13, 0);
            ioSet(14, 0);
            ioSet(15, 0);
            Thread.Sleep(1);
            int motion_complete = 1;
            while (true)
            {

                if (!ioRead_On((int)compoundmotionIO.robot_AUTO_get))
                {
                    logwriter.write_local_log("robot status not Auto");

                    err_write.write_alarmMessage(Error.error_unit.PLC, "robot status not Auto");
                    Console.WriteLine("not AUTO");
                    break;
                }//鑰匙 AUTO 模式

                if (!ioRead_On((int)compoundmotionIO.robot_ready_get))//off 做slect
                {
                    if (!ioSet((int)compoundmotionIO.robot_slect_set, 1))
                    {
                        //  error_code.enQ("not AUTO");
                        break;
                    }
                }
                if (!ioWait_bit((int)compoundmotionIO.robot_ready_get, true, loadport_handshakeTime))
                {
                    logwriter.write_local_log("PLC | RB: not ready");

                    err_write.write_alarmMessage(Error.error_unit.PLC, "PLC | RB: not ready");
                    break;
                }//ready
                motion_complete = 0;
                break;
            }
            if (motion_complete != 0)
            {
                Console.WriteLine("robot slect fill in {0} ", motion_complete);
                return motion_status.fail;
            }
            return motion_status.success;
        }
        //loadport ini 需要檢查時序
        public void loadport1Select()
        {
            ioSet(0x10, 0);
            ioSet(0x11, 0);
            ioSet(0x12, 0);
            ioSet(0x13, 0);
            ioSet(0x14, 0);
            ioSet(0x15, 0);
            ioSet(0x16, 0);
            ioSet(0x17, 0);
            ioSet(0x18, 0);
            ioSet(0x19, 0);
            ioSet(0x1A, 0);
            ioSet(0x1B, 0);
            ioSet(0x1C, 0);
            ioSet(0x1D, 0);
            ioSet(0x1E, 0);
            ioSet(0x1F, 0);
            int motion_complete = 0;
            while (true)
            {
                motion_complete++;
                if (!ioRead_On((int)compoundmotionIO.loadport1_AUTO_get)) { break; }//鑰匙 AUTO 模式
                motion_complete++;
                if (!ioRead_On((int)compoundmotionIO.loadport1_ready_get))//off 做
                {
                    motion_complete++;
                    if (!ioSet((int)compoundmotionIO.loadport1_slect_set, 1)) { break; }
                }
                motion_complete++;
                if (!ioWait_bit((int)compoundmotionIO.loadport1_ready_get, true, loadport_handshakeTime))
                {
                    logwriter.write_local_log("PLC | L1 : not ready");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "PLC | L1: not ready");
                    break;
                }//ready
                motion_complete = 0;
                break;
            }
            if (motion_complete != 0)
            {
                Console.WriteLine("robot slect fill in {0} ", motion_complete);
            }
        }
        public void loadport2Select()
        {
            ioSet(0x20, 0);
            ioSet(0x21, 0);
            ioSet(0x22, 0);
            ioSet(0x23, 0);
            ioSet(0x24, 0);
            ioSet(0x25, 0);
            ioSet(0x26, 0);
            ioSet(0x27, 0);
            ioSet(0x28, 0);
            ioSet(0x29, 0);
            ioSet(0x2A, 0);
            ioSet(0x2B, 0);
            ioSet(0x2C, 0);
            ioSet(0x2D, 0);
            ioSet(0x2E, 0);
            ioSet(0x2F, 0);
            int motion_complete = 0;
            while (true)
            {
                motion_complete++;
                if (!ioRead_On((int)compoundmotionIO.loadport2_AUTO_get)) { break; }//鑰匙 AUTO 模式
                motion_complete++;
                if (ioRead_Off((int)compoundmotionIO.loadport2_ready_get))//off 做
                {
                    motion_complete++;
                    if (!ioSet((int)compoundmotionIO.loadport2_slect_set, 1)) { break; }
                }
                motion_complete++;
                if (!ioWait_bit((int)compoundmotionIO.loadport2_ready_get, true, loadport_handshakeTime))
                {
                    logwriter.write_local_log("PLC | L2 : not ready");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "PLC | L2 : not ready");
                    break;
                }//ready
                motion_complete = 0;
                break;
            }
            if (motion_complete != 0)
            {
                Console.WriteLine("loadport1 slect fill in {0} ", motion_complete);
            }
            Console.WriteLine("loadport1Select! ");
        }
        public motion_status Home()
        {
            if (motion_status.success != robot_compoundmotion((int)RB_commend.home))
            {
                return motion_status.fail;
            }
            if (motion_status.success != loadport1_compoundmotion((int)LP_commend.ORGN))
            {
                return motion_status.fail;
            }

            /*
            if (motion_status.success != loadport2_compoundmotion((int)LP_commend.ORGN))
            {
                return motion_status.fail;
            }
            */
            return motion_status.success;
        }
        public motion_status robot_setPos()
        {
            motion_status reult = motion_status.fail;

            while (true)
            {
                if (!ioRead_On(0x850))
                {
                    Console.WriteLine("PLC | RB: cant not Teaching ");
                    logwriter.write_local_log("PLC | RB:  cant not Teaching.The 0x850 not on");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "PLC | RB:  cant not Teaching.The 0x850 not on");
                    break;
                }

                if (!ioSet(0x31, 1))
                {
                    logwriter.write_local_log("PLC | set 0x31 fail");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "PLC | set 0x31 fail");

                    break;
                }
                if (!ioWait_bit((int)compoundmotionIO.robot_set_pos, true, robot_motionTime, (int)compoundmotionIO.robot_set_pos))
                {
                    logwriter.write_local_log("PLC | RB: 0x851 not on");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "PLC | RB: 0x851 not on");
                    break;
                }
                if (!ioSet(0x31, 0))
                {
                    logwriter.write_local_log("PLC | set 0x31 fail");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "PLC | set 0x31 fail");
                    break;
                }

                if (!ioWait_bit((int)compoundmotionIO.robot_set_pos, false, robot_motionTime, (int)compoundmotionIO.robot_set_pos))
                {
                    logwriter.write_local_log("PLC | RB: 0x851 not off");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "PLC | RB: 0x851 not off");
                    break;
                }


                reult = motion_status.success;
                break;
            }
            ioSet(0x31, 0);
            return reult;
        }

        #region motion
        public motion_status robot_compoundmotion(int motion)
        {
            motion_status reult = motion_status.fail;
            int motion_complete = 0;
            compoundmotionIO[] addresss = new compoundmotionIO[]
                        {
                          compoundmotionIO.robot_add1,compoundmotionIO.robot_add2,compoundmotionIO.robot_add3,compoundmotionIO.robot_add4,
                          compoundmotionIO.robot_add5,compoundmotionIO.robot_add6,compoundmotionIO.robot_add7,compoundmotionIO.robot_add8,
                          compoundmotionIO.robot_add9,compoundmotionIO.robot_addA,compoundmotionIO.robot_addB,compoundmotionIO.robot_addC
                        };

            while (true)
            {

                //read AUTO on
                motion_complete++;
                if (!ioRead_On((int)compoundmotionIO.robot_AUTO_get))
                {
                    Console.WriteLine("PLC | RB: ROBOT not Auto");
                    logwriter.write_local_log("PLC | RB: ROBOT not Auto");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "PLC | RB: ROBOT not Auto");

                    break;
                }

                //read ready on
                motion_complete++;
                Console.WriteLine("ioWait_bit address {0} on ", compoundmotionIO.robot_ready_get);
                if (!ioRead_On((int)compoundmotionIO.robot_ready_get))
                {
                    Console.WriteLine("PLC | RB: ROBOT not ready");
                    if (ioWait_bit((int)compoundmotionIO.robot_ready_get, true, 5, (int)compoundmotionIO.robot_error_get))
                    {

                    }
                    else
                    {
                        logwriter.write_local_log("PLC | RB: ROBOT not ready");
                        err_write.write_alarmMessage(Error.error_unit.PLC, "PLC | RB: ROBOT not ready");
           
                        break;
                    }

                }//ready

                //read PCA on
                motion_complete++;
                if (!ioRead_On((int)compoundmotionIO.robot_PCA_get))
                {
                   // Console.WriteLine("PLC | RB: PCA error");
                    logwriter.write_local_log("PLC | RB: PCA not on");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "PLC | RB: PCA not on");
                   
                    break;
                }

                //set motion
                motion_complete++;
                foreach (int addbit in addresss)
                {
                    //if (!IoSet(addbit, motion & 0x0001))
                    //{
                    //    Console.WriteLine("set error");
                    //}
                    IoSet_fast(addbit, motion & 0x0001);
                    motion = motion >> 1;
                }
                ///ioSet_fast
                //set start on
                motion_complete++;
                if (!ioSet(1, 1)) { break; }

                //wait PCA off
                motion_complete++;
                if (false == ioWait_bit((int)compoundmotionIO.robot_PCA_get, false, 5, (int)compoundmotionIO.robot_error_get))
                {
                    //error_code.enQ("PLC | RB: PCA not off");
                    logwriter.write_local_log("PLC | RB: PCA not off");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "PLC | RB: PCA not off");
                    break;
                }

                //set start off

                motion_complete++;


                if (!ioSet((int)compoundmotionIO.robot_start_set, 0))
                {
                    //error_code.enQ("PLC | RB: robot_start_set 0 fail");
                    logwriter.write_local_log("PLC | RB: robot_start_set 0 fail");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "PLC | RB: robot_start_set 0 fail");
                    break;
                }
                ioSet(4, 0);
                ioSet(5, 0);
                ioSet(6, 0);
                ioSet(7, 0);
                ioSet(8, 0);
                ioSet(9, 0);
                ioSet(10, 0);
                ioSet(11, 0);
                ioSet(12, 0);
                ioSet(13, 0);
                ioSet(14, 0);
                ioSet(15, 0);
                //wait PCA on
                motion_complete++;
                // robot_motionTime, (int)compoundmotionIO.robot_error_get)
                Console.WriteLine("等待動作完成");
                Console.WriteLine("ioWait_bit address {0} on ", compoundmotionIO.robot_PCA_get);

                if (!ioWait_bit((int)compoundmotionIO.robot_PCA_get, true, robot_motionTime, (int)compoundmotionIO.robot_error_get))
                {
                    //error_code.enQ("PLC | RB: PCA not on");
                    logwriter.write_local_log("PLC | RB: PCA not on");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "PLC | RB: PCA not on");

                    break;
                }
                // ioWait_bit((int)compoundmotionIO.robot_PCA_get, true, 5, (int)compoundmotionIO.robot_error_get);
                Thread.Sleep(10);
                motion_complete = 0;
                reult = motion_status.success;
                break;
            }
            ioSet(1, 0);
            ioSet(4, 0);
            ioSet(5, 0);
            ioSet(6, 0);
            ioSet(7, 0);
            ioSet(8, 0);
            ioSet(9, 0);
            ioSet(10, 0);
            ioSet(11, 0);
            ioSet(12, 0);
            ioSet(13, 0);
            ioSet(14, 0);
            ioSet(15, 0);
            if (reult != motion_status.success)
            {
                Console.WriteLine("compound motion stop in {0} times ", motion_complete);
                //error_code.enQ("motion faild");
            }
            Console.WriteLine("compound motion_complete", motion_complete);
            return reult;
        }
        public motion_status loadport1_compoundmotion(int motion)
        {
            motion_status reult = motion_status.fail;
            int motion_complete = 0;
            compoundmotionIO[] addresss = new compoundmotionIO[]
                        { compoundmotionIO.loadport1_add1_set,compoundmotionIO.loadport1_add2_set,compoundmotionIO.loadport1_add3_set,compoundmotionIO.loadport1_add4_set,
                          compoundmotionIO.loadport1_add5_set,compoundmotionIO.loadport1_add6_set,compoundmotionIO.loadport1_add7_set,compoundmotionIO.loadport1_add8_set,
                          compoundmotionIO.loadport1_add9_set,compoundmotionIO.loadport1_addA_set,compoundmotionIO.loadport1_addB_set,compoundmotionIO.loadport1_addC_set
                        };
            ioSet(20, 0);
            ioSet(21, 0);
            ioSet(22, 0);
            ioSet(23, 0);
            ioSet(24, 0);
            ioSet(25, 0);
            ioSet(26, 0);
            ioSet(27, 0);
            ioSet(28, 0);
            ioSet(29, 0);
            ioSet(30, 0);
            ioSet(31, 0);

            while (true)
            {
                //read AUTO on
                motion_complete++;
                if (!ioRead_On((int)compoundmotionIO.loadport1_AUTO_get))
                {
                  //  error_code.enQ("L1 Auto off");


                    logwriter.write_local_log("L1 Auto off");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "L1 Auto off");
                    break;
                }
                if (!ioRead_On((int)compoundmotionIO.loadport1_ready_get))
                {
                    logwriter.write_local_log("L1 ready off");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "L1 ready off");
                   /// error_code.enQ("L1 ready off");
                    break;
                }

                //read PCA on
                motion_complete++;
                if (!ioRead_On((int)compoundmotionIO.loadport1_PCA_get))
                {
                    logwriter.write_local_log("L1 PCA off");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "L1 PCA off");
                    //error_code.enQ("L1 PCA off");
                    break;
                }

                //set motion
                motion_complete++;
                foreach (int addbit in addresss)//寫入地址
                {
                    if (!ioSet(addbit, motion & 0x0001)) { }
                    motion = motion >> 1;
                }

                //set start on
                motion_complete++;
                if (!ioSet((int)compoundmotionIO.loadport1_start_set, 1))
                {
                    logwriter.write_local_log("L1 ioSet fild");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "L1 ioSet fild");
                    //error_code.enQ("L1 ioSet fild"); break;
                }

                //wait PCA off
                motion_complete++;
                if (!ioWait_bit((int)compoundmotionIO.loadport1_PCA_get, false, loadport_handshakeTime))
                {
                    logwriter.write_local_log("time out! PCA not off");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "time out! PCA not off");
                   // error_code.enQ("time out! PCA not off");
                    break;
                }
                //set start off
                motion_complete++;
                if (!ioSet((int)compoundmotionIO.loadport1_start_set, 0))
                {
                    logwriter.write_local_log("L1 ioSet fild");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "L1 ioSet fild");
                    //error_code.enQ("L1 ioSet fild");
                    break;
                }
                ioSet(20, 0);
                ioSet(21, 0);
                ioSet(22, 0);
                ioSet(23, 0);
                ioSet(24, 0);
                ioSet(25, 0);
                ioSet(26, 0);
                ioSet(27, 0);
                ioSet(28, 0);
                ioSet(29, 0);
                ioSet(30, 0);
                ioSet(31, 0);

                //wait PCA on
                motion_complete++;
                if (!ioWait_bit((int)compoundmotionIO.loadport1_PCA_get, true, loadport_motionTime))
                {
                    logwriter.write_local_log("time out! PCA not on");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "time out! PCA not on");
                    //error_code.enQ("time out! PCA not on");
                    break;
                }

                motion_complete = 0;
                reult = motion_status.success;

                break;
            }
            ioSet(20, 0);
            ioSet(21, 0);
            ioSet(22, 0);
            ioSet(23, 0);
            ioSet(24, 0);
            ioSet(25, 0);
            ioSet(26, 0);
            ioSet(27, 0);
            ioSet(28, 0);
            ioSet(29, 0);
            ioSet(30, 0);
            ioSet(31, 0);

            if (motion_complete != 0)
            {
                Console.WriteLine("compound motion stop in {0} times ", motion_complete);
            }
            return reult;
        }
        public motion_status loadport2_compoundmotion(int motion)
        {
            motion_status reult = motion_status.fail;
            int motion_complete = 0;
            compoundmotionIO[] addresss = new compoundmotionIO[]
                        {
                          compoundmotionIO.loadport2_add1_set,compoundmotionIO.loadport2_add2_set,compoundmotionIO.loadport2_add3_set,compoundmotionIO.loadport2_add4_set,
                          compoundmotionIO.loadport2_add5_set,compoundmotionIO.loadport2_add6_set,compoundmotionIO.loadport2_add7_set,compoundmotionIO.loadport2_add8_set,
                          compoundmotionIO.loadport2_add9_set,compoundmotionIO.loadport2_addA_set,compoundmotionIO.loadport2_addB_set,compoundmotionIO.loadport2_addC_set
                        };
            ioSet(36, 0);
            ioSet(37, 0);
            ioSet(38, 0);
            ioSet(39, 0);
            ioSet(40, 0);
            ioSet(41, 0);
            ioSet(42, 0);
            ioSet(43, 0);
            ioSet(44, 0);
            ioSet(45, 0);
            ioSet(46, 0);
            ioSet(47, 0);


            while (true)
            {
                //read AUTO on
                motion_complete++;
                if (!ioRead_On((int)compoundmotionIO.loadport2_AUTO_get))
                {
                    //error_code.enQ("L2 Auto off");

                    logwriter.write_local_log("L2 Auto off");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "L2 Auto off");
                    break;
                }
                //read ready on
                motion_complete++;
                if (!ioRead_On((int)compoundmotionIO.loadport2_ready_get))
                {
                   // error_code.enQ("L2 ready off");
                    logwriter.write_local_log("L2 ready off");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "L2 ready off");
                    break;
                }

                //read PCA on
                motion_complete++;
                if (!ioRead_On((int)compoundmotionIO.loadport2_PCA_get))
                {
                   
                    logwriter.write_local_log("PCA off");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "PCA off");

                    break;
                }

                //set motion
                motion_complete++;
                foreach (int addbit in addresss)
                {
                    if (!ioSet(addbit, motion & 0x0001)) { }
                    motion = motion >> 1;
                }

                //set start on
                motion_complete++;
                if (!ioSet((int)compoundmotionIO.loadport2_start_set, 1))
                {
                    logwriter.write_local_log("L2 loadport2_start_set 1 fild");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "L2 loadport2_start_set 1 fild");
                    break;
                }

                //wait PCA off
                motion_complete++;
                if (!ioWait_bit((int)compoundmotionIO.loadport2_PCA_get, false, loadport_handshakeTime))
                {
                    
                    logwriter.write_local_log("L2  PCA off timeout");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "L2  PCA off timeout");
                    break;
                }

                //set start off
                motion_complete++;
                if (!ioSet((int)compoundmotionIO.loadport2_start_set, 0))
                {
                    logwriter.write_local_log("L2  loadport2_start_set 0 fild");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "L2  loadport2_start_set 0 fild");
                    break;
                }
                ioSet(36, 0);
                ioSet(37, 0);
                ioSet(38, 0);
                ioSet(39, 0);
                ioSet(40, 0);
                ioSet(41, 0);
                ioSet(42, 0);
                ioSet(43, 0);
                ioSet(44, 0);
                ioSet(45, 0);
                ioSet(46, 0);
                ioSet(47, 0);

                //wait PCA on
                motion_complete++;
                if (!ioWait_bit((int)compoundmotionIO.loadport2_PCA_get, true, loadport_motionTime))
                {
                    //error_code.enQ("L2  loadport2_PCA_get 1 timeout");
                    logwriter.write_local_log("L2  loadport2_PCA_get 1 timeout");
                    err_write.write_alarmMessage(Error.error_unit.PLC, "L2  loadport2_PCA_get 1 timeout");

                    break;
                }

                motion_complete = 0;
                reult = motion_status.success;

                break;
            }
            ioSet(36, 0);
            ioSet(37, 0);
            ioSet(38, 0);
            ioSet(39, 0);
            ioSet(40, 0);
            ioSet(41, 0);
            ioSet(42, 0);
            ioSet(43, 0);
            ioSet(44, 0);
            ioSet(45, 0);
            ioSet(46, 0);
            ioSet(47, 0);
            if (motion_complete != 0)
            {
                Console.WriteLine("compound motion stop in {0} times ", motion_complete);
            }
            return reult;
        }


        #endregion


        #endregion

        #region 簡單動作

        #region Robot
        public void robot_simplemotion(int motion)
        {
            ioSet(0x08F, 1);
            //         simplemotion((int)motion, simplemotion_get.Rb_occupied);
            while (true)
            {
                if (!ioWait_bit(0x88F, true, loadport_handshakeTime))
                {
                    Console.WriteLine("simplemotion fild");
                    break;
                }
                if (!ioRead_Off((int)simplemotion_get.Rb_occupied))
                {
                    Console.WriteLine("simplemotion is occupied");
                    break;
                }
                //PC trigger
                ioSet((int)motion, 1);
                Thread.Sleep(3);
                //if (!ioWait_bit((int)motion + 0x800, true, loadport_handshakeTime)) { }
                ioSet((int)motion, 0);
                if (!ioWait_bit((int)simplemotion_get.Rb_occupied, false, loadport_handshakeTime)) { }
                //if (!ioWait_bit((int)motion + 0x800, false, loadport_handshakeTime)) { }
                break;
            }
            ioSet(0x08F, 0);
        }



        public void Lp1_doorUpStart()
        {
            jogOn(simplemotion_get.L1_DoorUp, simplemotion_get.L1_occupied);
        }
        public void Lp2_doorUpStart()
        {
            jogOn(simplemotion_get.L2_DoorUp, simplemotion_get.L2_occupied);
        }
        public void Lp1_doorUpStop()
        {
            jogOff(simplemotion_get.L1_DoorUp, simplemotion_get.L1_occupied);
        }
        public void Lp2_doorUpStop()
        {
            jogOff(simplemotion_get.L2_DoorUp, simplemotion_get.L2_occupied);
        }
        public void Lp1_doorDnStart()
        {
            jogOn(simplemotion_get.L1_DoorDn, simplemotion_get.L1_occupied);
        }
        public void Lp2_doorDnStart()
        {
            jogOn(simplemotion_get.L2_DoorDn, simplemotion_get.L2_occupied);
        }
        public void Lp1_doorDnStop()
        {
            jogOff(simplemotion_get.L1_DoorDn, simplemotion_get.L1_occupied);
        }
        public void Lp2_doorDnStop()
        {
            jogOff(simplemotion_get.L2_DoorDn, simplemotion_get.L2_occupied);
        }
        private void jogOn(simplemotion_get motion, simplemotion_get loadportNum)
        {
            while (true)
            {
                if (!ioRead_Off((int)loadportNum))
                {
                    Console.WriteLine("loadport1_handshakeTime is occupied");
                    break;
                }
                //PC trigger
                ioSet((int)motion, 1);
                if (!ioWait_bit((int)motion + 0x800, true, loadport_handshakeTime)) { }
                break;
            }
        }

        private void jogOff(simplemotion_get motion, simplemotion_get loadportNum)
        {
            while (true)
            {
                //PC trigger
                ioSet((int)motion, 0);
                if (!ioWait_bit((int)motion + 0x800, false, loadport_handshakeTime)) { }
                break;
            }
        }

        #endregion

        #region Loadport

        public void simplemotion(int motion, simplemotion_get occupied)
        {
            while (true)
            {
                if (!ioRead_Off((int)occupied))
                {
                    Console.WriteLine("simplemotion is occupied");
                    break;
                }
                //PC trigger
                ioSet(motion, 1);
                Thread.Sleep(100);
                // if (!ioWait_bit(motion + 0x800, true, loadport_handshakeTime)) { }
                ioSet(motion, 0);
                if (!ioWait_bit((int)occupied, false, loadport_handshakeTime)) { }
                break;
            }
        }

        #endregion


        #endregion

        #region IO 讀寫

        public int readw_stratus(W_statusIO IO)
        {
            return w_read((int)IO);
        }
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
            logwriter.setLogType = logwriter01.LogDir.IO;
            logwriter.setDevice_Name = "PLC";
            logwriter.write_local_log(string.Format("ioWait_bit address {0}  ", address));

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
                err_write.write_alarmMessage(Error.error_unit.PLC, string.Format("ioWait_bit address 0x{0} timeout!", Convert.ToString(address, 16)));
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
    public enum compoundmotionIO : int
    {
        robot_Startbit_set = 0,
        robot_slect_set = robot_Startbit_set,
        robot_start_set,
        robot_TBD_set,
        robot_stop_set,
        robot_add1,
        robot_add2,
        robot_add3,
        robot_add4,
        robot_add5,
        robot_add6,
        robot_add7,
        robot_add8,
        robot_add9,
        robot_addA,
        robot_addB,
        robot_addC,

        loadport1_Startbit_set = 0x10,
        loadport1_slect_set = loadport1_Startbit_set,
        loadport1_start_set,
        loadport1_TBD_set,
        loadport1_stop_set,
        loadport1_add1_set,
        loadport1_add2_set,
        loadport1_add3_set,
        loadport1_add4_set,
        loadport1_add5_set,
        loadport1_add6_set,
        loadport1_add7_set,
        loadport1_add8_set,
        loadport1_add9_set,
        loadport1_addA_set,
        loadport1_addB_set,
        loadport1_addC_set,

        loadport2_Startbit_set = 0x20,
        loadport2_slect_set = loadport2_Startbit_set,
        loadport2_start_set,
        loadport2_stop_set,
        loadport2_TBD_set,
        loadport2_add1_set,
        loadport2_add2_set,
        loadport2_add3_set,
        loadport2_add4_set,
        loadport2_add5_set,
        loadport2_add6_set,
        loadport2_add7_set,
        loadport2_add8_set,
        loadport2_add9_set,
        loadport2_addA_set,
        loadport2_addB_set,
        loadport2_addC_set,

        robot_Startbit_get = 0x800,
        robot_ready_get = 0x800,
        robot_error_get = 0x801,
        robot_PCA_get = 0x802,
        robot_AUTO_get = 0x803,
        robot_set_pos = 0x851,

        loadport1_Startbit_get = 0x810,
        loadport1_ready_get = loadport1_Startbit_get,
        loadport1_error_get,
        loadport1_PCA_get,
        loadport1_AUTO_get,

        loadport2_Startbit_get = 0x820,
        loadport2_ready_get = loadport2_Startbit_get,
        loadport2_error_get,
        loadport2_PCA_get,
        loadport2_AUTO_get,
    }

    public enum simplemotion_set : int
    {
        L1_Clamp = 0x110,
        L1_unClamp,
        L1_Duck,
        L1_unDuck,
        L1_PurgeOn,
        L1_PurgeOff,
        L1_VacOn,
        L1_VacOff,
        L1_Latch,
        L1_unLatch,
        L1_cover_open,//jog
        L1_cover_close,//jog
        L1_DoorUp,
        L1_DoorDn,

        L2_Clamp = 0x120,
        L2_unClamp,
        L2_Duck,
        L2_unDuck,
        L2_PurgeOn,
        L2_PurgeOff,
        L2_VacOn,
        L2_VacOff,
        L2_Latch,
        L2_unLatch,
        L2_cover_open,//jog
        L2_cover_close,//jog
        L2_DoorUp,
        L2_DoorDn,

        Rb_Xp = 0x010,//jog
        Rb_Xn,//jog
        Rb_Yp,//jog
        Rb_Yn,//jog
        Rb_Zp,//jog
        Rb_Zn,//jog
        Rb_Wp,//jog
        Rb_Wn,//jog
        Rb_Rp,//jog
        Rb_Rn,//jog
        Rb_Cp,//jog
        Rb_Cn,//jog
    }

    public enum simplemotion_get : int
    {
        L1_occupied = 0x90E,
        L2_occupied = 0x90F,
        L1_Clamp = 0x910,
        L1_unClamp,
        L1_Duck,
        L1_unDuck,
        L1_PurgeOn,
        L1_PurgeOff,
        L1_VacOn,
        L1_VacOff,
        L1_Latch,
        L1_unLatch,
        L1_cover_open,//jog
        L1_cover_close,//jog
        L1_DoorUp,
        L1_DoorDn,
        L1_Warning,
        L1_Alarm,
        L2_Clamp = 0x920,
        L2_unClamp,
        L2_Duck,
        L2_unDuck,
        L2_PurgeOn,
        L2_PurgeOff,
        L2_VacOn,
        L2_VacOff,
        L2_Latch,
        L2_unLatch,
        L2_cover_open,//jog
        L2_cover_close,//jog
        L2_DoorUp,
        L2_DoorDn,
        L2_Warning,
        L2_Alarm,

        Rb_occupied = 0x80F,
        Rb_Xp = 0x810,//jog
        Rb_Xn,//jog
        Rb_Yp,//jog
        Rb_Yn,//jog
        Rb_Zp,//jog
        Rb_Zn,//jog
        Rb_Wp,//jog
        Rb_Wn,//jog
        Rb_Rp,//jog
        Rb_Rn,//jog
        Rb_Cp,//jog
        Rb_Cn,//jog

    }

    public enum W_statusIO : int  //W_status
    {
        status_EQ = 0x800,
        status_L1 = 0x801,
        status_L2 = 0x802,
        status_RB = 0x803,
        position_L1 = 0x804,
        position_L2 = 0x805,
        position_RB = 0x806,

        Robot_motion = 0x901,
        X_Lcoordinate = 0x902,
        X_Hcoordinate = 0x903,
        Y_Lcoordinate = 0x904,
        Y_Hcoordinate = 0x905,
        Z_Lcoordinate = 0x906,
        Z_Hcoordinate = 0x907,
        W_Lcoordinate = 0x908,
        W_Hcoordinate = 0x909,
        R_Lcoordinate = 0x90A,
        R_Hcoordinate = 0x90B,
        C_Lcoordinate = 0x90C,
        C_Hcoordinate = 0x90D,
        S_Lcoordinate,
        S_Hcoordinate,



        /// <loadport1>
        /// loadport1
        /// </loadport1>
        L1_Present = 0x910,
        L1_Clamp,
        L1_dock,
        L1_PurgeOn,
        L1_VacOn,
        L1_Latch,
        L1_A300_Cylinder_N2,
        L1_Door_open,
        L1_Bolt_detection_sensor,
        L1_Info_sensor,
        L1_Door_up,
        L1_A300,
        L1_E84_status = 0x91B,

        /// <loadport2>
        /// loadport2
        /// </loadport2>
        L2_Present = 0x920,
        L2_Clamp,
        L2_dock,
        L2_PurgeOn,
        L2_VacOn,
        L2_Latch,
        L2_A300_Cylinder_N2,
        L2_Door_open,
        L2_Bolt_detection_sensor,
        L2_Info_sensor,
        L2_Door_up,
        L2_A300,
        L2_E84_status = 0x92B,

        //RB setting
        X_Lsetcoordinate = 0x930,
        X_Hsetcoordinate = 0x931,
        Y_Lsetcoordinate = 0x932,
        Y_Hsetcoordinate = 0x933,
        Z_Lsetcoordinate = 0x934,
        Z_Hsetcoordinate = 0x935,
        W_Lsetcoordinate = 0x936,
        W_Hsetcoordinate = 0x937,
        R_Lsetcoordinate = 0x938,
        R_Hsetcoordinate = 0x939,
        C_Lsetcoordinate = 0x93A,
        C_Hsetcoordinate = 0x93B,
        S_Lsetcoordinate = 0x93C,
        S_Hsetcoordinate = 0x93D,

    }
}
