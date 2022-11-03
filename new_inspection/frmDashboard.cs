﻿using System;
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
        Error err_write = new Error();
        INSP_recipe insp_Recipe = new INSP_recipe();
        Main_control Insp_process = new Main_control();
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            Main_control.status_update += new Main_control.percent(percent_IU);
            string ins_recipe_name = "";
            foreach (INSP_recipe.recipe ins_recipe in INSP_recipe.code_list)
            {
                if (ins_recipe_name == ins_recipe.type)
                    continue;
                ins_recipe_name = ins_recipe.type;
                listBox_recipe.Items.Add(ins_recipe_name);
            }
            //listBox_recipe
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
                this.Invoke(new MethodInvoker(delegate () { lbl_LP1_motion.Text = "End"; }));
            }

            if (t.Equals(typeof(string)))//工作
            {
                string now_job = (string)obj;
                if (now_job == "stop")
                    this.Invoke(new MethodInvoker(delegate () { Progressbar_lp1_c(0); }));//百分比
                this.Invoke(new MethodInvoker(delegate () { lbl_LP1_motion.Text = now_job; }));
            }
            if (t.Equals(typeof(RFID_report)))
            {
                RFID_report data = (RFID_report)obj;
                if (data.port == Loadport.Loadport1)
                    this.Invoke(new MethodInvoker(delegate () { txt_L1_foupID.Text = data.ID; }));
                if (data.port == Loadport.Loadport2)
                    this.Invoke(new MethodInvoker(delegate () { txt_L2_foupID.Text = data.ID; }));
            }
        }
        public void Progressbar_lp1_c(float value)
        {
            if (value > 1.0f)
                value = 1.0f;
            else if (value < 0.0f)
                value = 0.0f;
            btn_Progressbar_lp1.Width = (int)(value * p_Progressbar_lp1.Width);
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
            //this.Invoke(new MethodInvoker(delegate () { Progressbar_lp1_c(0); }));//百分比
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
            if (listBox_recipe.SelectedIndex == -1)
            {
                err_write.write_warnMessage(Error.error_unit.system, "please slect resipe");
            }
            else
                Insp_process.Insp_start(Loadport.Loadport1, listBox_recipe.SelectedItem.ToString(), txt_L1_foupID.Text);
        }

        private void btn_lp2_start_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_start(Loadport.Loadport2, "A01R", txt_L2_foupID.Text);
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
