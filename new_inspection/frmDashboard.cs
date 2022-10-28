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
    public partial class frmDashboard : Form
    {
        Main_control Insp_process = new Main_control();
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            Main_control.status_update += new Main_control.percent(percent_IU);
        }

        #region percent event
        private void percent_IU(object obj)
        {
            Type t = obj.GetType();
            schedule Schedule = new schedule();
            Console.WriteLine("OBJ : Type is {0}", obj.GetType());
            if (t.Equals(typeof(schedule))) //進度表
            {
                Schedule = (schedule)obj;
                this.Invoke(new MethodInvoker(delegate () { Progressbar_lp1_c(Schedule.percent); }));//百分比
            }

            if (t.Equals(typeof(job)))//工作
            {
                job now_job = (job)obj;
                this.Invoke(new MethodInvoker(delegate () { n_job_ui(now_job); }));
            }
            if (t.Equals(typeof(RFID_report)))
            {
                RFID_report data = (RFID_report)obj;
                if (data.port == Loadport.Loadport1)
                    this.Invoke(new MethodInvoker(delegate () { txt_L1_foupID.Text = data.ID; }));
                if (data.port == Loadport.Loadport2)
                    this.Invoke(new MethodInvoker(delegate () { txt_L2_foupID.Text = data.ID; }));
            }
            // 
        }
        public void Progressbar_lp1_c(float value)
        {
            if (value > 1.0f)
                value = 1.0f;
            else if (value < 0.0f)
                value = 0.0f;

            btn_Progressbar_lp1.Width = (int)(value * p_Progressbar_lp1.Width);
        }

        public void n_job_ui(job now_job)
        {
            //lbl_LP1_motion.Text = string.Format("{0}", now_job.unit);
        }

        #endregion

        #region pan controll
        private void btn_Home_Click_1(object sender, EventArgs e)
        {
            Insp_process.Insp_home();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_stop();
        }
        private void btn_pause_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_pause();
        }
        private void btn_continue_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_continue();
        }

        #endregion

        #region UI commend

        private void btn_lp1_start_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_start(Loadport.Loadport1, txt_L1_foupID.Text);
        }

        private void btn_lp2_start_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_start(Loadport.Loadport2, txt_L2_foupID.Text);
        }

        private void btn_lp1_load_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_Load(Loadport.Loadport1);
        }
        private void btn_lp2_load_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_Load(Loadport.Loadport2);
        }

        private void btn_Progressbar_lp1_Click(object sender, EventArgs e)
        {
            //Progressbar_lp1_c(0.5f);
        }
        #endregion


    }

}
