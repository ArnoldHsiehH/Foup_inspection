using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace new_inspection
{

    public partial class Form1 : Form
    {
        frmError errfrm = new frmError();
        Error err_write = new Error();

        logwriter01 logwriter = new logwriter01();
        Main_control Insp_process = new Main_control();

        frmDashboard frmDashboard_vrb = new frmDashboard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        frmManual Manual_vrb = new frmManual() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true }; //Manual_vrb
        frmLog frmLog_vrb = new frmLog() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public Form1()
        {
            InitializeComponent();
            Error.AlarmTriger += new Error.Alarmflag(errbtn_color);
            logwriter.setLogType = logwriter01.LogDir.System;
            logwriter.setDevice_Name = "Form1";

            pnlNav.Height = btn_Dashbord.Height;
            pnlNav.Top = btn_Dashbord.Top;
            pnlNav.Left = btn_Dashbord.Left;
            btn_Dashbord.BackColor = System.Drawing.SystemColors.Window;

            pnlfromcontrol(frmLog_vrb);

        }
        private void errbtn_color(bool value)
        {
            this.Invoke(new MethodInvoker(delegate () { write_errbtn(value); }));
        }
        private void write_errbtn(bool value)
        {
            btn_error.BackColor = value ? Color.Red : System.Drawing.SystemColors.MenuBar;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pnlfromcontrol(frmDashboard_vrb);
            Insp_process.initail();

        }
        private void setlistbtn()
        {
            foreach (var item in panel1.Controls)
            {
                if (item is Button)
                {
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
            //for (int i = 0; i < 0x800; i++)
            //{
            //    misubushi_IO.setB(i, 0);
            //}

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
            setlisttxt("Result", btn_history);
            //pnlfromcontrol(frmResult_vrb);
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
    }
}
