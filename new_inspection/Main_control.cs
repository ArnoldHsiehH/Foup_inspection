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

        misubushi_IO Misubushi_IO = new misubushi_IO();
        static logwriter01 logwriter = new logwriter01();
        Error err_write = new Error();
        INSP_recipe insp_Recipe = new INSP_recipe();
        PLC_motion PLC = new PLC_motion();

        private static Thread Main_thread;
        private static Thread Motion_thread;

        private delegate void inspEvent(object a);
        private static event inspEvent EventInsp;

        private delegate void simpleEvent(job Job);
        private static event simpleEvent send_simple;

        public delegate void percent(object obj);
        public static event percent status_update;

        static Queue<Interrupt_commend> Q_IC = new Queue<Interrupt_commend>();

        public static OMRON_RFID L1_RF = new OMRON_RFID();
        public static OMRON_RFID L2_RF = new OMRON_RFID();

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

        #region stratus

        public int readw_stratus(W_statusIO statusIO)
        {
            return PLC.w_read((int)statusIO); ;
        }
        #endregion

        #region commends

        public void Insp_initail()
        {
            EventInsp(new MC_initail() { PLC = true, RFID = true, adam = true, Insp = true });
        }

        public void Insp_Load(Loadport Lpunit)
        {
            EventInsp(new MC_load() { port = Lpunit });
        }
        public void Insp_start(Loadport Lpunit, string recipe, string foupID)
        {

            EventInsp(new MC_insp() { port = Lpunit, recipe = recipe, foupID = foupID });//ID: RFID
        }
        public void Insp_home()
        {
            EventInsp(new MC_commend_pack() { commend = MC_commend.home });
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
        public void cycle(bool Loadport1, bool Loadport2, bool RFID, bool Insp, int cycletimes)
        {
            EventInsp(new MC_cycle() { Loadport1 = Loadport1, Loadport2 = Loadport2, RFID = RFID, Insp = Insp, cycletimes = cycletimes });
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
            now_job.commend = new MP_RB_job() { Commend = jog_axis };
            send_simple(now_job);

        }
        //parameter
        public void RB_set_speed(string parameter)
        {
            job now_job = new job();
            now_job.commend = new MP_RB_job() { Commend = RB_jog_commend.set_speed, parameter = parameter };
            send_simple(now_job);
        }
        public void RB_set_jog_dis(string parameter)// 需要新增parameter
        {
            job now_job = new job();
            now_job.commend = new MP_RB_job() { Commend = RB_jog_commend.set_dis, parameter = parameter };

            send_simple(now_job);


        }
        public void RB_set_setpos()
        {
            job now_job = new job();
            now_job.commend = new MP_RB_job() { Commend = RB_jog_commend.setpos };

            send_simple(now_job);
        }

        public void LP_simple(Loadport loadport, LP_commend commend)
        {

            job now_job = new job();

            now_job.commend = new MP_Loadport() { port = loadport, Commend = commend };        // MC_unit.Loadport1;
            send_simple(now_job);
        }
        public void RF_check(Loadport port)
        {
            job now_job = new job();
            switch (port)
            {
                case Loadport.Loadport1:
                    now_job.commend = new MP_RFID { port = Loadport.Loadport1, Commend = RF_commend.L1_RFIDcheck };
                    break;

                case Loadport.Loadport2:
                    now_job.commend = new MP_RFID { port = Loadport.Loadport2, Commend = RF_commend.L2_RFIDcheck };
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
                    now_job.commend = new MP_RFID { port = Loadport.Loadport1, Commend = RF_commend.L1_RFID_read };
                    break;
                case Loadport.Loadport2:
                    now_job.commend = new MP_RFID { port = Loadport.Loadport2, Commend = RF_commend.L2_RFID_read };
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
            Motion_thread = new Thread(new ParameterizedThreadStart(Motion_process));
            Motion_thread.Start(new_job);
        }

        public void RB_simple(Loadport port, RB_commend commend)
        {
            EventInsp(new MC_commend_pack() { commend = MC_commend.RBsimple, RB_Commend = commend, LP_unit = port });
        }


        #endregion

        #region 流程控制

        private void Main_process(object Data)//觸發流程啟動
        {
            logwriter.write_local_log("Main process start");
            List<job> job_pack = new List<job>();

            #region 建立工作項目
            Type t = Data.GetType();
            string job_name = "";
            //建立工作項目
            if (t.Equals(typeof(MC_initail)))
            {
                job_name = "initail";
                MC_initail Setting;
                Setting = (MC_initail)Data;
                job_pack.Add(new job() { commend = Setting });

            }
            else if (t.Equals(typeof(MC_commend_pack)))
            {

                MC_commend_pack commend_pack;
                commend_pack = (MC_commend_pack)Data;
                logwriter.write_local_log(string.Format("Commend: {0}", commend_pack.commend));
                switch (commend_pack.commend)
                {
                    case MC_commend.home:   //建立工作項目: Home
                        job_name = "HOME";
                        job_pack.Add(new job() { commend = new MP_Robot() { Commend = RB_commend.home } });
                        job_pack.Add(new job() { commend = new MP_Robot() { Commend = RB_commend.home1 } });
                        job_pack.Add(new job() { commend = new MP_Loadport() { port = Loadport.Loadport1, Commend = LP_commend.ORGN } });
                        job_pack.Add(new job() { commend = new MP_Loadport() { port = Loadport.Loadport2, Commend = LP_commend.ORGN } });
                        break;
                    case MC_commend.RBsimple:
                        job_name = "RBSIMPLE";
                        job_pack.Add(new job() { commend = new MP_Robot() { Commend = commend_pack.RB_Commend } });
                        // job_pack.Add(new job() { unit = MC_unit.Robot, Robot_commend = commend_pack.RB_Commend });
                        break;
                }

            }

            //insp
            else if (t.Equals(typeof(MC_insp)))
            {
                job_name = "INSP";
                if (!Creat_insp_jobs((MC_insp)Data, ref job_pack))
                {
                    err_write.write_warnMessage(Error.error_unit.system, "recipe file error");
                    return;
                }
            }

            //Load
            else if (t.Equals(typeof(MC_load)))
            {
                job_name = "LOAD";
                MC_load setting = (MC_load)Data;

                if (setting.port == Loadport.Loadport1)
                {
                    job_pack.Add(new job() { commend = new MP_RFID() { port = setting.port, Commend = RF_commend.L1_RFID_read } });
                    job_pack.Add(new job() { commend = new MP_Loadport() { port = setting.port, Commend = LP_commend.Clamp } });
                }
                if (setting.port == Loadport.Loadport2)
                {
                    job_pack.Add(new job() { commend = new MP_RFID() { port = setting.port, Commend = RF_commend.L2_RFID_read } });
                    job_pack.Add(new job() { commend = new MP_Loadport() { port = setting.port, Commend = LP_commend.Clamp } });
                }
            }

            //cycle
            else if (t.Equals(typeof(MC_cycle)))
            {
                job_name = "CYCLE";
                MC_cycle setting = (MC_cycle)Data;
                string RF_read = "test";
                if (setting.RFID)
                    RF_read = "";
                for (int i = 0; i < setting.cycletimes; i++)
                {
                    if (setting.Loadport1)
                    {
                        MC_insp p1 = new MC_insp() { port = Loadport.Loadport1, foupID = RF_read };
                        if (!Creat_insp_jobs(p1, ref job_pack))
                        {
                            err_write.write_warnMessage(Error.error_unit.system, "recipe file error");
                            return;
                        }

                    }
                    if (setting.Loadport2)
                    {
                        MC_insp p2 = new MC_insp() { port = Loadport.Loadport2, foupID = RF_read };
                        if (!Creat_insp_jobs(p2, ref job_pack))
                        {
                            err_write.write_warnMessage(Error.error_unit.system, "recipe file error");
                            return;
                        }
                    }
                    job_pack.Add(new job() { commend = new MP_other { Commend = MP_report.conter_add } });
                }
            }
            else
            {
                job_name = "UNKNOW";
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

            status_update(string.Format("Process start,{0}", job_name));
            logwriter.write_local_log(string.Format("Process start,{0}", job_name));//工作結束
            #region 執行工作項目
            while (job_conter < job_pack.Count)//執行工作
            {
                #region 異常
                if (err_write.check_error)//異常產生
                {
                    err_write.write_warnMessage(Error.error_unit.system, " ");
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
                            status_update("stop");
                            logwriter.write_local_log("process stop");
                            break;
                        case Interrupt_commend.insp_pause:
                            status_update("pause");
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


            status_update(string.Format("Process End,{0}", job_name));
            logwriter.write_local_log(string.Format("Process End,{0}", job_name));//工作結束
            #endregion

        }
        #region 掃描動作建立
        private bool Creat_insp_jobs(MC_insp commend_pack, ref List<job> job_pack)
        {
            if (string.IsNullOrEmpty(commend_pack.recipe))
            {
                commend_pack.recipe = (commend_pack.port == Loadport.Loadport1) ? "A01L" : "A01R";
            }

            List<INSP_recipe.recipe> ins_list = insp_Recipe.get_insp_list(commend_pack.recipe);

            job_pack.Add(new job() { commend = new MP_Robot() { Commend = RB_commend.home1 } });
            job_pack.Add(new job() { commend = new MP_Robot() { Commend = RB_commend.home } });

            if (commend_pack.port == Loadport.Loadport1)
            {
                if (string.IsNullOrEmpty(commend_pack.foupID))
                    job_pack.Add(new job() { commend = new MP_RFID() { port = commend_pack.port, Commend = RF_commend.L1_RFID_read } });

                job_pack.Add(new job() { commend = new MP_Loadport() { port = Loadport.Loadport1, Commend = LP_commend.ORGN } });
                job_pack.Add(new job() { commend = new MP_Loadport() { port = Loadport.Loadport1, Commend = LP_commend.LOAD } });
                foreach (INSP_recipe.recipe target in ins_list)
                {
                    RB_commend RB_commendValue;
                    if (Enum.TryParse(target.RB_motion, out RB_commendValue))//格式是否又誤
                    {
                        if (target.do_undo != "O")//是否執行
                            continue;
                        job_pack.Add(new job() { commend = new MP_Robot() { Commend = RB_commendValue } });
                        job_pack.Add(new job() { commend = new MP_ins() { port = Loadport.Loadport1, ins = target.INSP_message } });
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            else if (commend_pack.port == Loadport.Loadport2)
            {
                if (string.IsNullOrEmpty(commend_pack.foupID))
                    job_pack.Add(new job() { commend = new MP_RFID() { port = commend_pack.port, Commend = RF_commend.L2_RFID_read } });

                job_pack.Add(new job() { commend = new MP_Loadport() { port = Loadport.Loadport2, Commend = LP_commend.ORGN } });
                job_pack.Add(new job() { commend = new MP_Loadport() { port = Loadport.Loadport2, Commend = LP_commend.LOAD } });

                foreach (INSP_recipe.recipe target in ins_list)
                {
                    RB_commend RB_commendValue;
                    if (Enum.TryParse(target.RB_motion, out RB_commendValue))
                    {
                        if (target.do_undo != "O")//是否執行
                            continue;
                        job_pack.Add(new job() { commend = new MP_Robot() { Commend = RB_commendValue } });
                        job_pack.Add(new job() { commend = new MP_ins() { port = Loadport.Loadport2, ins = target.INSP_message } });
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            job_pack.Add(new job() { commend = new MP_Robot() { Commend = RB_commend.home1 } });
            job_pack.Add(new job() { commend = new MP_Robot() { Commend = RB_commend.home } });
            job_pack.Add(new job() { commend = new MP_other() { Commend = MP_report.Insp_end } });
            return true;
        }
        #endregion

        #endregion

        #region 單動控制

        private void Motion_process(object Data)
        {
            string read_ID;
            string log = "unknow";
            job now_job = (job)Data;
            Type t = now_job.commend.GetType();
            if (t.Equals(typeof(MC_initail)))
            {
                log = "initail";
                MC_initail setting = (MC_initail)now_job.commend;
                Misubushi_IO.Initial();
                if (setting.RFID == true)
                {
                    L1_RF.Initial(22);
                }
            }

            #region Robot
            else if (t.Equals(typeof(MP_Robot)))
            {
                MP_Robot commend = (MP_Robot)now_job.commend;
                log = (string.Format("Robot: {0}", commend.Commend));
                PLC.robotSelect();
                PLC.robot_compoundmotion((int)commend.Commend);

            }
            else if (t.Equals(typeof(MP_RB_job)))
            {
                MP_RB_job commend = (MP_RB_job)now_job.commend;
                log = (string.Format("RB : {0}", commend.Commend));
            }

            #endregion

            #region Loadport
            else if (t.Equals(typeof(MP_Loadport)))
            {
                MP_Loadport commend = (MP_Loadport)now_job.commend;
                log = (string.Format("{0} : {1}", commend.port, commend.Commend));
                if ((int)commend.Commend < 6)
                {
                    if (commend.port == Loadport.Loadport1)
                    {

                        PLC.loadport1Select();
                        PLC.loadport1_compoundmotion((int)commend.Commend);

                    }
                    else if (commend.port == Loadport.Loadport2)
                    {
                        PLC.loadport2Select();
                        PLC.loadport2_compoundmotion((int)commend.Commend);

                    }

                }
                else if (0x10F >= (int)commend.Commend && (int)commend.Commend >= 0x100)//指令沒有分port
                {
                    if (commend.port == Loadport.Loadport1)
                    {

                        PLC.loadport1Select();
                        PLC.simplemotion((int)commend.Commend + 0x10, simplemotion_get.L1_occupied);
                       // PLC.loadport1_compoundmotion((int)commend.Commend + 0x10);

                    }
                    else if (commend.port == Loadport.Loadport2)
                    {
                        PLC.loadport2Select();
                        PLC.simplemotion((int)commend.Commend + 0x20, simplemotion_get.L2_occupied);
                        //  PLC.loadport2_compoundmotion((int)commend.Commend + 0x20);

                    }
                }
            }
            #endregion

            else if (t.Equals(typeof(MP_ins)))
            {
                MP_ins commend = (MP_ins)now_job.commend;
                log = (string.Format("Insp : {0}", commend.ins));
            }

            #region RFID
            else if (t.Equals(typeof(MP_RFID)))
            {

                read_ID = "";
                MP_RFID commend = (MP_RFID)now_job.commend;
                log = (string.Format("Insp {0}: {1}", commend.port, commend.Commend));
                switch (commend.Commend)
                {
                    case RF_commend.L1_RFIDcheck:
                        status_update(new RFID_report() { ID = "L1 check", port = Loadport.Loadport1 });
                        L1_RF.check_connection();
                        break;
                    case RF_commend.L2_RFIDcheck:
                        status_update(new RFID_report() { ID = "L2 check", port = Loadport.Loadport2 });
                        L2_RF.check_connection();
                        break;
                    case RF_commend.L1_RFID_read:
                        if (!L1_RF.READ_RFID("00000004", out read_ID))//
                        {
                        }
                        else
                        {
                            status_update(new RFID_report() { ID = read_ID, port = Loadport.Loadport1 });
                        }
                        break;
                    case RF_commend.L2_RFID_read:
                        if (!L1_RF.READ_RFID("00000004", out read_ID))//
                        {
                        }
                        else
                        {
                            status_update(new RFID_report() { ID = read_ID, port = Loadport.Loadport2 });
                        }
                        break;
                }
            }
            #endregion

            else if (t.Equals(typeof(MP_SCES)))
            {
                log = (string.Format("SCES"));
            }

            else if (t.Equals(typeof(MP_other)))
            {
                MP_other commend = (MP_other)now_job.commend;
                switch (commend.Commend)
                {
                    case MP_report.conter_add:
                        status_update(new conter() { });
                        log = "conter_add";
                        break;
                    case MP_report.Insp_end:
                        status_update(new RFID_report() { ID = "", port = Loadport.Loadport1 });
                        status_update(new RFID_report() { ID = "", port = Loadport.Loadport2 });
                        log = "Insp_end";
                        break;
                }
            }
            else
            {

            }

            logwriter.write_local_log(log);
            status_update(log);

            // logwriter.write_local_log(string.Format("{0},{1}", now_job.unit, now_job.Robot_commend));
            Thread.Sleep(1000);
        }
        #endregion

    }

    #region defind
    class MC_commend_pack// 大的指令
    {
        public MC_commend commend;
        public Loadport LP_unit;
        public RB_commend RB_Commend;
        public string ID;
    }
    class MC_initail
    {
        public bool PLC;
        public bool RFID;
        public bool Insp;
        public bool adam;

    }
    class MC_cycle
    {
        public bool Loadport1;
        public bool Loadport2;
        public bool RFID;
        public bool Insp;
        public int cycletimes;
    }
    class MC_insp
    {
        public string foupID;
        public string recipe;
        public Loadport port;
    }
    class MC_load
    {
        public string foupID;
        public Loadport port;
    }



    public class job//for job psck
    {
        public string process_name { get; set; }
        public object commend { get; set; }
        public string ID { get; set; }
        public string ins { get; set; }
        public int foupType { get; set; }

    }
    class MP_Robot
    {
        public RB_commend Commend { get; set; }
    }
    class MP_Loadport
    {
        public Loadport port { get; set; }
        public LP_commend Commend { get; set; }
    }
    class MP_RFID
    {
        public Loadport port { get; set; }
        public RF_commend Commend { get; set; }
    }
    class MP_RB_job//需要新增parameter
    {
        public string parameter;
        public RB_jog_commend Commend { get; set; }

    }
    class MP_ins
    {
        public Loadport port { get; set; }
        public string ins { get; set; }
    }
    class MP_SCES
    {
    }
    class MP_other
    {
        public MP_report Commend { get; set; }
    }

    public enum RF_commend
    {
        no_commend,
        L1_Initial,
        L2_Initial,
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
    #endregion

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

        RB_L1_Lach_L = 101,//"RB_R1_Lach_L"
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
        clamp,
        RBsimple
    }

    enum MP_report
    {
        conter_add,
        Insp_end
    }
    #endregion

    #region report
    public class schedule
    {
        public float percent { get; set; }
    }
    public class RFID_report
    {
        public Loadport port;
        public string ID;
    }
    public class conter
    {

    }
    #endregion

}
