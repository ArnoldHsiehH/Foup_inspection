using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Data.SQLite;
using Dapper;

namespace new_inspection
{

    public partial class Form1 : Form
    {
        AES AEScheck = new AES();
        INSP_recipe insp_Recipe = new INSP_recipe();
        Error err_write = new Error();

        logwriter01 logwriter = new logwriter01();
        Main_control Insp_process = new Main_control();

        frmError errfrm = new frmError();
        frmDashboard frmDashboard_vrb = new frmDashboard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        frmManual Manual_vrb = new frmManual() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true }; //Manual_vrb
        frmLog frmLog_vrb = new frmLog() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        frmResult frmResult_vrb = new frmResult() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        frmSetting frmSetting_vrb = new frmSetting() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        DateTime time_start = DateTime.Now;
        public Form1()
        {

            InitializeComponent();
            user_login();
            //errfrm.Show();
            Error.AlarmTriger += new Error.Alarmflag(errbtn_color);
            logwriter.setLogType = logwriter01.LogDir.System;
            logwriter.setDevice_Name = "Form1";

            pnlNav.Height = btn_Dashbord.Height;
            pnlNav.Top = btn_Dashbord.Top;
            pnlNav.Left = btn_Dashbord.Left;
            btn_Dashbord.BackColor = System.Drawing.SystemColors.Window;

            insp_Recipe.Page_Load();
            pnlfromcontrol(frmLog_vrb);
            Insp_process.initail();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pnlfromcontrol(frmDashboard_vrb);

            Main_control.status_update += new Main_control.percent(percent_IU);
            Insp_process.Insp_initail();

            timer1.Interval = 100;
            timer1.Enabled = true;
            timer1.Start();

        }

        private void errbtn_color(bool value)
        {
            this.Invoke(new MethodInvoker(delegate () { write_errbtn(value); }));
        }

        private void write_errbtn(bool value)
        {
            btn_error.BackColor = value ? Color.Red : System.Drawing.SystemColors.MenuBar;

        }

        private void percent_IU(object obj)
        {
            Type t = obj.GetType();
            if (t.Equals(typeof(string)))//工作
            {
                ///job now_job = (job)obj;
                this.Invoke(new MethodInvoker(delegate () { n_job_ui((string)obj); }));
            }
            // 
        }
        public void n_job_ui(string now_job)
        {
            if (txt_Console.Lines.Length > 5000)
                txt_Console.Text = "";
            txt_Console.Text += now_job + "\r\n";
            txt_Console.SelectionStart = txt_Console.Text.Length;
            txt_Console.ScrollToCaret();

        }
        private void setlistbtn()
        {
            foreach (var item in panel1.Controls)
            {
                if (item is Button)
                {
                    Button a = (Button)item;
                    if (a.Text == "Error")//跳過error設定
                        continue;
                    ((Button)item).BackColor = System.Drawing.SystemColors.MenuBar;
                }
            }

        }
        private void setlisttxt(string txt, Button bn)
        {
            logwriter.write_local_log(string.Format("{0},click", txt));
            pnlNav.Height = bn.Height;
            pnlNav.Top = bn.Top;
            pnlNav.Left = bn.Left;
            bn.BackColor = System.Drawing.SystemColors.Window;

            lbl_Title.Text = txt;
        }
        private void pnlfromcontrol(Form loadform)
        {
            this.pnlFormLoader.Controls.Clear();
            loadform.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(loadform);
            loadform.Show();
        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            logwriter.write_local_log(string.Format("Exit,click"));

            System.Environment.Exit(0);
        }

        private void btn_Dashbord_Click(object sender, EventArgs e)
        {
            // do_Inspection.BG_process_start();
            setlistbtn();
            setlisttxt("Dashboard", btn_Dashbord);
            StatusTable.now_page = StatusTable.page.Dashboard;
            pnlfromcontrol(frmDashboard_vrb);

        }

        private void btn_Manual_Click(object sender, EventArgs e)
        {
            // do_Inspection.BG_process_start();
            setlistbtn();
            setlisttxt("Manual", btn_Manual);
            pnlfromcontrol(Manual_vrb);
        }

        private void btn_Log_Click(object sender, EventArgs e)
        {
            setlistbtn();
            setlisttxt("Log", btn_Log);
            pnlfromcontrol(frmLog_vrb);
        }

        private void btn_About_Click(object sender, EventArgs e)
        {
            setlistbtn();
            setlisttxt("About", btn_About);

            //    this.pnlFormLoader.Controls.Clear();
            // pnlfromcontrol(About_vrb);
        }

        private void btn_history_Click(object sender, EventArgs e)
        {
            setlistbtn();
            setlisttxt("Result", btn_Result);
            pnlfromcontrol(frmResult_vrb);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //errfrm.Show();
            err_write.write_alarmMessage(Error.error_unit.system, "GG");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            err_write.write_alarmMessage(Error.error_unit.system, 10);
        }

        private void btn_error_Click(object sender, EventArgs e)
        {
            logwriter.write_local_log(string.Format("{0},click", "btn_error"));
            errfrm.Show();
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            setlistbtn();
            setlisttxt("Setting", btn_setting);
            pnlfromcontrol(frmSetting_vrb);

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            user_login();
            //InitSQLiteDb();
            //TestInsert();
            // TestSelect();

        }
        static string dbPath = @".\Test.sqlite";
        static string cnStr = "data source=" + dbPath;


        private void user_login()
        {
            frmUser_account userlogin = new frmUser_account();
            DialogResult loginResult = userlogin.ShowDialog();
            //回傳正常]
            if (loginResult != DialogResult.OK)
                return;
            string user = userlogin.txb_user.Text;
            string pasword = userlogin.txb_pw.Text;

            user user_account = new user();
            // Console.WriteLine(userlogin.txb_user.Text);
            // Console.WriteLine(userlogin.txb_pw.Text);

            if (!System.IO.File.Exists(@"D:\new_ins\userDB.db"))
                return;

            using (IDbConnection db = new SQLiteConnection(@"Data source= D:\new_ins\userDB.db;Version=3;New=true;"))
            {
                //搜尋資料
                //var list = db.Query<user>("select * from users where account_number = 'Hirata'");
                user_account = db.QuerySingleOrDefault<user>("select * from users where account_number = '" + user + "'");
                if (user_account == null)
                {
                    login_fild();
                    err_write.write_warnMessage(Error.error_unit.system, "account error");
                    //login_fild();
                    return;
                }
                //

                Console.WriteLine("account_number:{0} password:{1}", user_account.account_number, AEScheck.aesDecryptBase64(user_account.password));

                string pw = user_account.password;
                //確認密碼
                if (pasword != AEScheck.aesDecryptBase64(pw))
                {
                    login_fild();
                    err_write.write_warnMessage(Error.error_unit.system, "password error");
                    //login_fild();
                    return;
                }
                Console.WriteLine("Pass");
                txt_userprint.Text = user_account.account_number;
                btn_Manual.Enabled = (user_account.manul == 0) ? false : true;
                //btn_Opration.Enabled = (user_account.opration == 0) ? false : true;
                btn_Log.Enabled = (user_account.log == 0) ? false : true;
                btn_Result.Enabled = (user_account.result == 0) ? false : true;
                btn_setting.Enabled = (user_account.setting == 0) ? false : true;
                frmSetting_vrb.btn_account.Enabled = (user_account.setuser == 0) ? false : true;

                setlistbtn();
                setlisttxt("Dashboard", btn_Dashbord);
                pnlfromcontrol(frmDashboard_vrb);

            }
        }

        private void login_fild()
        {
            txt_userprint.Text = "";
            btn_Manual.Enabled = false;
            //btn_Opration.Enabled = (user_account.opration == 0) ? false : true;
            btn_Log.Enabled = false;
            btn_Result.Enabled = false;
            btn_setting.Enabled = false;
            frmSetting_vrb.btn_account.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sys_time();
            equipment_status();
        }
        private void sys_time()
        {
            DateTime date1 = DateTime.Now;
            string fullPath = string.Format("Time: {0}", date1.ToString("yyyy-MM-dd HH:mm:ss"));
            txt_nowtime.Text = fullPath;
            DateTime time_end = DateTime.Now;
            TimeSpan run_time = time_end - time_start;
            string result2 = ((TimeSpan)(time_end - time_start)).ToString();
            txt_uptime.Text = string.Format("{0}{1}", "run time: ", run_time.ToString(@"dd\.hh\:mm\:ss"));
        }
        private void equipment_status()
        {
            int EQvalue_800 = Insp_process.readw_stratus(W_statusIO.status_EQ);
            int L1value_801 = Insp_process.readw_stratus(W_statusIO.status_L1);
            int L2value_802 = Insp_process.readw_stratus(W_statusIO.status_L2);
            int RBvalue_803 = Insp_process.readw_stratus(W_statusIO.status_RB);

            lbl_PLC_status.Text = string.Format("EQ_status: {0}", status_(EQvalue_800, 0));
            lbl_PLC_status.BackColor = status_color(EQvalue_800);

            lbl_RB.Text = string.Format("RB_status: {0}", status_(RBvalue_803, (int)compoundmotionIO.robot_ready_get));
            lbl_RB.BackColor = status_color(RBvalue_803);
            lbl_L1.Text = string.Format("LP1_status: {0}", status_(L1value_801, (int)compoundmotionIO.loadport1_ready_get));
            lbl_L1.BackColor = status_color(L1value_801);
            lbl_L2.Text = string.Format("LP2_status: {0}", status_(L2value_802, (int)compoundmotionIO.loadport2_ready_get));
            lbl_L2.BackColor = status_color(L2value_802);

        }
        private string status_(int status_v, int select)
        {
            string status;
            switch (status_v)
            {
                case 0:
                    status = "error";
                    break;
                case 1:
                    {
                        status = "stable";
                        //if (select == 0)
                        //{
                        //    status = "stable";
                        //    break;
                        //}
                        //int value;
                        //misubushi_IO.getB(select + 3, out value);
                        //if (value != 1)
                        //{
                        //    status = "not auto";
                        //    break;
                        //}
                        //misubushi_IO.getB(select, out value);
                        //if (value == 1)
                        //    status = "stable";
                        //else
                        //    status = "unselect";
                        break;
                    }
                case 2:
                    status = "working";
                    break;
                case 3:
                    status = "warning";
                    break;
                case 5:
                    status = "unselect";
                    break;
                default:
                    status = Convert.ToString(status_v);
                    break;

            }
            return status;
        }
        private Color status_color(int v)
        {
            Color out_color = Color.Black;
            switch (v)
            {
                case 0:
                    out_color = Color.Red;
                    break;
                case 1:
                    out_color = Color.Green;
                    break;

                case 2:
                    out_color = Color.Yellow;
                    break;

                case 3:
                    out_color = Color.Yellow;
                    break;
                case 5:
                    out_color = Color.Yellow;
                    break;
                default:
                    out_color = Color.White;
                    break;

            }
            return out_color;

        }

    }
}
