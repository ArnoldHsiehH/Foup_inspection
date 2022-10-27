using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace new_inspection
{
    class Main_control
    {
        static logwriter01 logwriter = new logwriter01();
        Error err_write = new Error();

        private static Thread Main_thread;
        private static Thread Motion_thread;

        private delegate void inspEvent(object a);
        private static event inspEvent EventInsp;

        private delegate void simpleEvent(job Job);
        private static event simpleEvent send_simple;

        public delegate void percent(object obj);
        public static event percent status_update;

        static Queue<Interrupt_commend> Q_IC = new Queue<Interrupt_commend>();
        public void initail()
        {
            logwriter.setLogType = logwriter01.LogDir.System;
            logwriter.setDevice_Name = "process";
            logwriter.write_local_log("initail process");

            Main_thread = new Thread(new ParameterizedThreadStart(Main_process));
            Motion_thread = new Thread(new ParameterizedThreadStart(Motion_process));
            Main_thread.IsBackground = true;
            Motion_thread.IsBackground = true;

            EventInsp += new inspEvent(get_commend);
            send_simple += new simpleEvent(get_sinle_commend);
        }

        #region commends
        public void Insp_Load(Loadport Lpunit)
        {
            EventInsp(new MC_commend_pack() { commend = MC_commend.load, LP_unit = Lpunit });
        }
        public void Insp_start(Loadport Lpunit)
        {
            EventInsp(new MC_commend_pack() { commend = MC_commend.insp_start, LP_unit = Lpunit, ID = "123456" });//ID: RFID
        }
        public void Insp_home()
        {
            EventInsp(new MC_commend_pack() { commend = MC_commend.home });
        }
        public void Insp_cycle()
        {
            EventInsp(new MC_commend_pack() { commend = MC_commend.cycle });
        }

        public void Insp_stop()
        {
            if (Main_thread.IsAlive)//如果流程正在進行
                Q_IC.Enqueue(Interrupt_commend.insp_stop);
        }
        public void Insp_pause()
        {
            if (Main_thread.IsAlive)//如果流程正在進行
                Q_IC.Enqueue(Interrupt_commend.insp_pause);
        }
        public void Insp_continue()
        {
            if (Main_thread.IsAlive)//如果流程正在進行
                Q_IC.Enqueue(Interrupt_commend.insp_continue);
        }

        private void get_commend(object commend)//指令抵達
        {

            if (!Main_thread.IsAlive)
            {
                Main_thread = new Thread(new ParameterizedThreadStart(Main_process)); //執行續處理
                Main_thread.IsBackground = true;
                Main_thread.Start(commend);
            }
            else
            {
                //Console.WriteLine("Main_thread busy");
                err_write.write_warnMessage(Error.error_unit.system, "Main_thread busy");
            }



        }
        #endregion

        #region 單動 commends

        public void RB_jog(RB_jog_commend jog_axis)
        {
            job now_job = new job();
            now_job.unit = MC_unit.RB_jog;
            now_job.RB_jog = jog_axis;
            send_simple(now_job);

        }
        //parameter
        public void RB_set_speed(string parameter)
        {
            job now_job = new job();
            now_job.unit = MC_unit.RB_jog;
            now_job.RB_jog = RB_jog_commend.set_speed;
            now_job.parameter = parameter;
            send_simple(now_job);
        }
        public void RB_set_jog_dis(string parameter)
        {
            job now_job = new job();
            now_job.unit = MC_unit.RB_jog;
            now_job.RB_jog = RB_jog_commend.set_dis;
            now_job.parameter = parameter;
            send_simple(now_job);
        }
        public void RB_set_setpos()
        {
            job now_job = new job();
            now_job.unit = MC_unit.RB_jog;
            now_job.RB_jog = RB_jog_commend.setpos;

            send_simple(now_job);
        }

        public void LP_simple(MC_unit loadport, LP_commend commend)
        {

            job now_job = new job();
            now_job.unit = loadport;        // MC_unit.Loadport1;
            now_job.Loadport_commend = commend;
            send_simple(now_job);
        }
        public void RF_check(Loadport port)
        {
            job now_job = new job();
            switch (port)
            {
                case Loadport.Loadport1:
                    now_job.unit = MC_unit.RFID1;
                    now_job.RFID_commend = RF_commend.L1_RFIDcheck;
                    break;

                case Loadport.Loadport2:
                    now_job.unit = MC_unit.RFID2;
                    now_job.RFID_commend = RF_commend.L2_RFIDcheck;
                    break;
            }
            send_simple(now_job);

        }
        public void RF_read(Loadport port)
        {
            job now_job = new job();
            switch (port)
            {
                case Loadport.Loadport1:
                    now_job.unit = MC_unit.RFID1;
                    now_job.RFID_commend = RF_commend.L1_RFID_read;
                    break;

                case Loadport.Loadport2:
                    now_job.unit = MC_unit.RFID2;
                    now_job.RFID_commend = RF_commend.L2_RFID_read;
                    break;
            }
            send_simple(now_job);

        }

        private void get_sinle_commend(job Job)
        {
            job new_job = new job();
            new_job = Job;
            if (Main_thread.IsAlive)
            {
                err_write.write_warnMessage(Error.error_unit.system, "Main_thread is running");
                return;
            }
            switch (new_job.unit)
            {
                case MC_unit.Robot:
                    break;
                case MC_unit.RB_jog:
                    Motion_thread = new Thread(new ParameterizedThreadStart(Motion_process));
                    Motion_thread.Start(new_job);
                    break;
                case MC_unit.Loadport1:
                    if ((int)new_job.Loadport_commend > 5)
                        new_job.Loadport_commend = new_job.Loadport_commend + 0x10;
                    Motion_thread = new Thread(new ParameterizedThreadStart(Motion_process));
                    Motion_thread.Start(new_job);
                    break;
                case MC_unit.Loadport2:
                    if ((int)new_job.Loadport_commend > 5)
                        new_job.Loadport_commend = (LP_commend)(new_job.Loadport_commend + 0x20);
                    Motion_thread = new Thread(new ParameterizedThreadStart(Motion_process));
                    Motion_thread.Start(new_job);
                    break;
                case MC_unit.RFID1:
                case MC_unit.RFID2:
                    Motion_thread = new Thread(new ParameterizedThreadStart(Motion_process));
                    Motion_thread.Start(new_job);
                    break;
                case MC_unit.ITRI:
                    break;
                case MC_unit.others:
                    break;
                case MC_unit.SCES:
                    break;
            }
        }

        public void RB_simple(Loadport port, RB_commend commend)
        {
            EventInsp(new MC_commend_pack() { commend = MC_commend.RBsimple, RB_Commend = commend, LP_unit = port });
        }
        public void cycle(bool Loadport1,bool Loadport2,bool RFID,bool Insp)
        {
            EventInsp(new MC_cycle() { Loadport1 = Loadport1, Loadport2 = Loadport2, RFID = RFID, Insp = Insp });
        }
        #endregion

        #region 流程控制

        private void Main_process(object Data)//觸發流程啟動
        {
            logwriter.write_local_log("Main process start");
            MC_unit LPunit = MC_unit.unknow;
            List<job> job_pack = new List<job>();

            #region 建立工作項目
            Type t = Data.GetType();

            if (t.Equals(typeof(MC_commend_pack)))
            {

                MC_commend_pack commend_pack;
                commend_pack = (MC_commend_pack)Data;
                logwriter.write_local_log(string.Format("Commend: {0}", commend_pack.commend));
                switch (commend_pack.commend)
                {
                    case MC_commend.home:   //建立工作項目: Home
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.home1 });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.home });
                        job_pack.Add(new job() { unit = MC_unit.Loadport1, Loadport_commend = LP_commend.ORGN });
                        job_pack.Add(new job() { unit = MC_unit.Loadport2, Loadport_commend = LP_commend.ORGN });
                        break;
                    case MC_commend.load:     //建立工作項目: Load
                        LPunit = (commend_pack.LP_unit == Loadport.Loadport1) ? MC_unit.Loadport1 : MC_unit.unknow;
                        LPunit = (commend_pack.LP_unit == Loadport.Loadport2) ? MC_unit.Loadport2 : LPunit;
                        if (LPunit == MC_unit.unknow)
                            return;
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.home1 });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.home });
                        job_pack.Add(new job() { unit = MC_unit.RFID1, RFID_commend = RF_commend.L1_RFID_read });
                        job_pack.Add(new job() { unit = LPunit, Loadport_commend = LP_commend.ORGN });
                        job_pack.Add(new job() { unit = LPunit, Loadport_commend = LP_commend.LOAD });
                        break;
                    case MC_commend.insp_start://建立工作項目: 掃描
                        Creat_ins_jobs(commend_pack, ref job_pack);
                        break;
                    case MC_commend.clamp:
                        LPunit = (commend_pack.LP_unit == Loadport.Loadport1) ? MC_unit.Loadport1 : LPunit;
                        LPunit = (commend_pack.LP_unit == Loadport.Loadport2) ? MC_unit.Loadport2 : LPunit;
                        if (LPunit == MC_unit.unknow)
                            return;
                        job_pack.Add(new job() { unit = LPunit, Loadport_commend = LP_commend.ORGN });
                        job_pack.Add(new job() { unit = LPunit, Loadport_commend = LP_commend.L1_Clamp });
                        break;
                    case MC_commend.RBsimple:
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = commend_pack.RB_Commend });
                        break;
                    case MC_commend.cycle://cycle
                        break;
                }//建立工作項目

            }


            else if (t.Equals(typeof(MC_cycle)))
            {
                MC_cycle setting = (MC_cycle)Data;
                for (int i = 0; i < 1000; i++)
                {
                    if (setting.Loadport1)
                    {
                        if (setting.RFID)
                            job_pack.Add(new job() { unit = MC_unit.RFID1, RFID_commend = RF_commend.L1_RFID_read });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.home1 });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.home });
                        job_pack.Add(new job() { unit = LPunit, Loadport_commend = LP_commend.LOAD });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_Snorkel_L });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_Snorkel_R });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_Lach_L });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_Lach_R });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_CL_Out_L_1 });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_CL_Out_L_2 });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_CL_Out_L_3 });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_CL_Out_L_4 });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_CL_Out_L_5 });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_CL_Out_L_6 });
                    }
                    if (setting.Loadport2)
                    {
                        if (setting.RFID)
                            job_pack.Add(new job() { unit = MC_unit.RFID2, RFID_commend = RF_commend.L2_RFID_read });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.home1 });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.home });
                        job_pack.Add(new job() { unit = LPunit, Loadport_commend = LP_commend.LOAD });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L2_Snorkel_L });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L2_Snorkel_R });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L2_Lach_L });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L2_Lach_R });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L2_CL_Out_L_1 });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L2_CL_Out_L_2 });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L2_CL_Out_L_3 });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L2_CL_Out_L_4 });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L2_CL_Out_L_5 });
                        job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L2_CL_Out_L_6 });
                    }
                }

                job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.home1 });
                job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.home });
                job_pack.Add(new job() { unit = MC_unit.Loadport1, Loadport_commend = LP_commend.ORGN });
                job_pack.Add(new job() { unit = MC_unit.Loadport2, Loadport_commend = LP_commend.ORGN });

            }


            #endregion

            process_status status = process_status.run;
            job_status job_Status = job_status.free;
            int job_conter = 0;
            status_update(new schedule() { percent = 0 });

            if (Motion_thread.IsAlive)
            {
                //異常:還有動作執行中(單動)
                err_write.write_warnMessage(Error.error_unit.system, "Motion busy");
                return;
            }

            #region 執行工作項目
            while (job_conter < job_pack.Count)//執行工作
            {
                #region 異常
                if (err_write.check_error)//異常產生
                {
                    err_write.write_warnMessage(Error.error_unit.system, "please reast error");
                    break;
                }
                #endregion

                #region 外部控制
                if (Q_IC.Count > 0)
                {
                    switch (Q_IC.Dequeue())
                    {
                        case Interrupt_commend.insp_stop:
                            status = process_status.stop;
                            logwriter.write_local_log("process stop");
                            break;
                        case Interrupt_commend.insp_pause:
                            logwriter.write_local_log("process pause");
                            status = process_status.pause;
                            break;
                        case Interrupt_commend.insp_continue:
                            logwriter.write_local_log("process continue");
                            status = process_status.run;//如果原始為wait(?)                           
                            break;
                    }
                    Q_IC.Clear();
                }

                if (status == process_status.stop)
                    break;
                if (status == process_status.pause)
                    continue;

                #endregion

                #region 執行
                job now_job = job_pack[job_conter];
                switch (job_Status)
                {
                    case job_status.free://下達命令
                        if (Motion_thread.IsAlive)
                        {
                            //還有動作執行中
                            // break;
                        }


                        job_Status = job_status.running;
                        Motion_thread = new Thread(new ParameterizedThreadStart(Motion_process));
                        Motion_thread.Start(now_job);
                        break;

                    case job_status.running://執行中
                        if (!Motion_thread.IsAlive)//確認是否完成
                        {
                            job_Status = job_status.end;
                        }
                        break;

                    case job_status.end://指令完成
                        float F_value = ((float)job_conter + 1) / (float)job_pack.Count;
                        //percent_update(F_value);
                        status_update(new schedule() { percent = F_value });
                        job_conter++;
                        job_Status = job_status.free;
                        //選擇下一則指令
                        break;
                }
                #endregion

                Thread.Sleep(1);
            }
            #endregion

            #region 工作結束
            logwriter.write_local_log("Main process End");//工作結束
            #endregion

        }

        private void Creat_ins_jobs(MC_commend_pack commend_pack, ref List<job> job_pack)
        {
            MC_unit LPunit;
            LPunit = (commend_pack.LP_unit == Loadport.Loadport1) ? MC_unit.Loadport1 : MC_unit.unknow;
            LPunit = (commend_pack.LP_unit == Loadport.Loadport2) ? MC_unit.Loadport2 : LPunit;
            if (LPunit == MC_unit.unknow)
                return;
            job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.home1 });
            job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.home });
            job_pack.Add(new job() { unit = LPunit, Loadport_commend = LP_commend.LOAD });
            job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_Snorkel_L });
            job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_Snorkel_R });
            job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_Lach_L });
            job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_Lach_R });
            job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_CL_Out_L_1 });
            job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_CL_Out_L_2 });
            job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_CL_Out_L_3 });
            job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_CL_Out_L_4 });
            job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_CL_Out_L_5 });
            job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = RB_commend.RB_L1_CL_Out_L_6 });
        }


        #endregion

        #region 單動控制

        private void Motion_process(object Data)
        {
            job now_job = (job)Data;
            status_update(now_job);
            switch (now_job.unit)
            {
                case MC_unit.Robot:
                    logwriter.write_local_log(string.Format("{0},{1}", now_job.unit, now_job.Robot_commend));
                    break;
                case MC_unit.RB_jog:
                    logwriter.write_local_log(string.Format("{0},{1}", now_job.unit, now_job.RB_jog));
                    break;
                case MC_unit.Loadport1:
                    logwriter.write_local_log(string.Format("{0},{1}", now_job.unit, now_job.Loadport_commend));
                    break;
                case MC_unit.Loadport2:
                    logwriter.write_local_log(string.Format("{0},{1}", now_job.unit, now_job.Loadport_commend));
                    break;
                case MC_unit.RFID1://RFID_commend
                    logwriter.write_local_log(string.Format("{0},{1}", now_job.unit, now_job.RFID_commend));
                    break;
                case MC_unit.RFID2:
                    logwriter.write_local_log(string.Format("{0},{1}", now_job.unit, now_job.RFID_commend));
                    break;
                case MC_unit.ITRI:
                    logwriter.write_local_log(string.Format("{0},{1}", now_job.unit, now_job.ins));
                    break;
                case MC_unit.SCES:
                    logwriter.write_local_log(string.Format("{0},{1}", now_job.unit, now_job.SCES_commend));
                    break;
            }

            Thread.Sleep(1000);
        }
        #endregion


    }
    class MC_commend_pack// 大的指令
    {
        public MC_commend commend;
        public Loadport LP_unit;
        public RB_commend RB_Commend;
        public string ID;
    }
    class MC_cycle
    {
        public bool Loadport1;
        public bool Loadport2;
        public bool RFID;
        public bool Insp;

    }
    public class job//for job psck
    {
        public MC_unit unit { get; set; }
        public LP_commend Loadport_commend { get; set; }//motion
        public RB_commend Robot_commend { get; set; }
        public RF_commend RFID_commend { get; set; }
        public RB_jog_commend RB_jog { get; set; }
        public string parameter { get; set; }
        public string SCES_commend { get; set; }
        public int port_number
        {
            get
            {
                if (unit == MC_unit.Loadport1)
                    return 1;
                if (unit == MC_unit.Loadport2)
                    return 2;
                return 0;
            }
        }
        public string ID { get; set; }
        public string ins { get; set; }
        public int foupType { get; set; }

    }
    public class schedule
    {
        public float percent { get; set; }
    }
    public enum RF_commend
    {
        no_commend,

        L1_RFIDcheck,
        L2_RFIDcheck,
        L1_RFID_read,
        L2_RFID_read
    }
    enum e_Inspection
    {
        no_commend,

        Select,//PLC reset 
        Home,
        //動作
        loadport1_motion,
        loadport2_motion,
        robot_motion,

        //單動
        loadport1_simple,
        loadport2_simple,
        robot_simple,
        //
        ITRI_insp,
        //SECS
        End_procss,
        WaitHostCmd,
        WaitHostJob,
        update_L1_RFID,
        update_L2_RFID
    }

    #region Loadport_commend
    public enum Loadport
    {
        unknow,
        Loadport1,
        Loadport2
    }
    public enum LP_commend
    {
        none = 0x00,
        ORGN = 0x01,
        LOAD,
        INSP1,
        INSP2,
        INSP3,

        Clamp = 0x100,
        unClamp,
        Duck,
        unDuck,
        PurgeOn,
        PurgeOff,
        VacOn,
        VacOff,
        Latch,
        unLatch,
        cover_open,//jog
        cover_close,//jog
        DoorUp,
        DoorDn,
        A300Up,
        A300Dn,

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
        L1_A300Up,
        L1_A300Dn,

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
        L2_A300Up,
        L2_A300Dn
    }

    #endregion

    #region RB_commend
    public enum RB_commend
    {
        none = 0x00,
        /// <summary>
        /// Robot
        /// </summary>
        home = 0x001,
        home1 = 2,
        select,

        RB_L1_Lach_L = 101,
        RB_L1_Lach_R,
        RB_L1_TE_Out_L,
        RB_L1_TE_Out_R,
        RB_L1_code,
        RB_L1_BT_Out_L,
        RB_L1_BT_Out_R,
        RB_L1_FT_Out_L,
        RB_L1_FT_Out_R,
        RB_L1_Snorkel_L,
        RB_L1_Snorkel_R,
        RB_L1_CL_Out_L_1,
        RB_L1_CL_Out_L_2,
        RB_L1_CL_Out_L_3,
        RB_L1_CL_Out_L_4,
        RB_L1_CL_Out_L_5,
        RB_L1_CL_Out_L_6,
        RB_L1_CL_Out_L_7,
        RB_L1_CL_Out_R_1,
        RB_L1_CL_Out_R_2,
        RB_L1_CL_Out_R_3,
        RB_L1_CL_Out_R_4,
        RB_L1_CL_Out_R_5,
        RB_L1_CL_Out_R_6,
        RB_L1_CL_Out_R_7,
        RB_L1_IC_L_1,
        RB_L1_IC_L_2,
        RB_L1_IC_L_3,
        RB_L1_IC_L_4,
        RB_L1_IC_L_5,
        RB_L1_IC_L_6,
        RB_L1_IC_L_7,
        RB_L1_IC_R_1,
        RB_L1_IC_R_2,
        RB_L1_IC_R_3,
        RB_L1_IC_R_4,
        RB_L1_IC_R_5,
        RB_L1_IC_R_6,
        RB_L1_IC_R_7,
        RB_L1_Bottom_1,
        RB_L1_Bottom_2,
        RB_L1_Bottom_3,
        RB_L1_Bottom_4,
        RB_L1_Bottom_5,
        RB_L1_Bottom_6,
        RB_L1_Bottom_7,
        RB_L1_Bottom_8,
        RB_L1_Bottom_9,
        RB_L1_Bottom_10,
        RB_L1_Bottom_11,

        RB_L2_Lach_L = 1001,
        RB_L2_Lach_R,
        RB_L2_TE_Out_L,
        RB_L2_TE_Out_R,
        RB_L2_code,
        RB_L2_BT_Out_L,
        RB_L2_BT_Out_R,
        RB_L2_FT_Out_L,
        RB_L2_FT_Out_R,
        RB_L2_Snorkel_L,
        RB_L2_Snorkel_R,
        RB_L2_CL_Out_L_1,
        RB_L2_CL_Out_L_2,
        RB_L2_CL_Out_L_3,
        RB_L2_CL_Out_L_4,
        RB_L2_CL_Out_L_5,
        RB_L2_CL_Out_L_6,
        RB_L2_CL_Out_L_7,
        RB_L2_CL_Out_R_1,
        RB_L2_CL_Out_R_2,
        RB_L2_CL_Out_R_3,
        RB_L2_CL_Out_R_4,
        RB_L2_CL_Out_R_5,
        RB_L2_CL_Out_R_6,
        RB_L2_CL_Out_R_7,
        RB_L2_IC_L_1,
        RB_L2_IC_L_2,
        RB_L2_IC_L_3,
        RB_L2_IC_L_4,
        RB_L2_IC_L_5,
        RB_L2_IC_L_6,
        RB_L2_IC_L_7,
        RB_L2_IC_R_1,
        RB_L2_IC_R_2,
        RB_L2_IC_R_3,
        RB_L2_IC_R_4,
        RB_L2_IC_R_5,
        RB_L2_IC_R_6,
        RB_L2_IC_R_7,
        RB_L2_Bottom_1,
        RB_L2_Bottom_2,
        RB_L2_Bottom_3,
        RB_L2_Bottom_4,
        RB_L2_Bottom_5,
        RB_L2_Bottom_6,
        RB_L2_Bottom_7,
        RB_L2_Bottom_8,
        RB_L2_Bottom_9,
        RB_L2_Bottom_10,
        RB_L2_Bottom_11
    }

    public enum RB_jog_commend
    {
        Rb_Xp = 0x80,//jog
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
        Rb_Sp,
        Rb_Sn,
        setpos,
        set_speed,
        set_dis
    }
    #endregion

    #region control
    public enum MC_unit
    {
        unknow,
        Robot,
        RB_jog,
        Loadport1,
        Loadport2,
        RFID1,
        RFID2,
        ITRI,
        others,
        SCES
    }
    enum job_status
    {
        free,
        running,
        end
    }
    enum process_status
    {
        run,
        pause,
        stop
    }
    enum Interrupt_commend
    {
        insp_stop,
        insp_pause,
        insp_continue
    }
    enum MC_commend
    {
        home,
        load,
        clamp,
        insp_start,
        insp_stop,
        cycle,
        RBsimple
    }
    #endregion

}
