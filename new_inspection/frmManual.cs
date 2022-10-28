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
    public partial class frmManual : Form
    {
        Main_control Insp_process = new Main_control();
        Button[] btn_Lp1;
        Button[] btn_Lp2;
        Button[] btn_robot_jog;

        public frmManual()
        {
            InitializeComponent();

            btn_Lp1 = new Button[] {
                btn_Lp1_clamp,      btn_Lp1_unclamp,
                btn_Lp1_A300Up,     btn_Lp1_A300Dn,
                btn_Lp1_purge,      btn_Lp1_unpurge,
                btn_Lp1_duck,       btn_Lp1_unduck,
                btn_Lp1_vacOn,      btn_Lp1_vacOff,
                btn_Lp1_Latch,      btn_Lp1_unLatch,
                btn_Lp1_coverOn,    btn_Lp1_coverOff,
                btn_Lp1_Dn,         btn_Lp1_Up
            };
            foreach (Button button in btn_Lp1)
                button.Click += new System.EventHandler(this.btn_Lp1_Click);

            btn_Lp2 = new Button[] {
                btn_Lp2_clamp,      btn_Lp2_unclamp,
                btn_Lp2_A300Up,     btn_Lp2_A300Dn,
                btn_Lp2_purge,      btn_Lp2_unpurge,
                btn_Lp2_duck,       btn_Lp2_unduck,
                btn_Lp2_vacOn,      btn_Lp2_vacOff,
                btn_Lp2_Latch,      btn_Lp2_unLatch,
                btn_Lp2_coverOn,    btn_Lp2_coverOff,
                btn_Lp2_Dn,         btn_Lp2_Up
            };
            foreach (Button button in btn_Lp2)
                button.Click += new System.EventHandler(this.btn_Lp2_Click);

            btn_robot_jog = new Button[]
            {
                btn_Xpositive,  btn_Xnegative,
                btn_Ypositive,  btn_Ynegative,
                btn_Zpositive,  btn_Znegative,
                btn_Wpositive,  btn_Wnegative,
                btn_Rpositive,  btn_Rnegative,
                btn_Cpositive,  btn_Cnegative,
                btn_Spositive,  btn_Snegative
            };
            foreach (Button button in btn_robot_jog)
                button.Click += new System.EventHandler(this.btn_robot_jog_Click);
        }

        private void frmManual_Load(object sender, EventArgs e)
        {
            Main_control.status_update += new Main_control.percent(percent_IU);
        }
        private void percent_IU(object obj)
        {
            Type t = obj.GetType();
            schedule Schedule = new schedule();
            Console.WriteLine("OBJ : Type is {0}", obj.GetType());
     

            if (t.Equals(typeof(RFID_report)))
            {
                RFID_report data = (RFID_report)obj;
                if (data.port == Loadport.Loadport1)
                    this.Invoke(new MethodInvoker(delegate () { txt_L1_RF_Console.Text = data.ID; }));
                if (data.port == Loadport.Loadport2)
                    this.Invoke(new MethodInvoker(delegate () { txt_L2_RF_Console.Text = data.ID; }));
            }
            // 
        }
        private void btn_Home_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_home();
        }

        private void btn_cycleStart_Click(object sender, EventArgs e)
        {

            Insp_process.cycle(ckb_LP1.Checked, ckb_LP2.Checked, ckb_rfid.Checked, ckb_Camera.Checked,10);
        }

        #region Loadport

        private void btn_Lp1_Click(object sender, EventArgs e)
        {
            Button btn_click = (Button)sender;
            // Console.WriteLine("{0}", btn_click.Name);
            LP_simple(Loadport.Loadport1, btn_click.Text);
        }

        private void btn_Lp2_Click(object sender, EventArgs e)
        {
            Button btn_click = (Button)sender;
            // Console.WriteLine("{0}", btn_click.Name);
            LP_simple(Loadport.Loadport2, btn_click.Text);
        }

        private void btn_port1start_Click_1(object sender, EventArgs e)
        {
            if (lsb_portmotion.SelectedItems.Count == 1)
                LP_simple(Loadport.Loadport1, lsb_portmotion.SelectedItem.ToString());
        }

        private void btn_port2start_Click(object sender, EventArgs e)
        {
            if (lsb_portmotion.SelectedItems.Count == 1)
                LP_simple(Loadport.Loadport2, lsb_portmotion.SelectedItem.ToString());
        }

        void LP_simple(Loadport unit, string txt)
        {
            switch (txt)
            {
                case "Clamp":
                    Insp_process.LP_simple(unit, LP_commend.Clamp);
                    break;
                case "unClamp":
                    Insp_process.LP_simple(unit, LP_commend.unClamp);
                    break;
                case "A300Up":
                    Insp_process.LP_simple(unit, LP_commend.A300Up);
                    break;
                case "A300Dn":
                    Insp_process.LP_simple(unit, LP_commend.A300Dn);
                    break;
                case "PurgeOn":
                    Insp_process.LP_simple(unit, LP_commend.PurgeOn);
                    break;
                case "PurgeOff":
                    Insp_process.LP_simple(unit, LP_commend.PurgeOff);
                    break;
                case "Duck":
                    Insp_process.LP_simple(unit, LP_commend.Duck);
                    break;
                case "unDuck":
                    Insp_process.LP_simple(unit, LP_commend.unDuck);
                    break;
                case "vacOn":
                    Insp_process.LP_simple(unit, LP_commend.VacOn);
                    break;
                case "vacOff":
                    Insp_process.LP_simple(unit, LP_commend.VacOff);
                    break;
                case "Latch":
                    Insp_process.LP_simple(unit, LP_commend.Latch);
                    break;
                case "unLatch":
                    Insp_process.LP_simple(unit, LP_commend.unLatch);
                    break;
                case "door open":
                    Insp_process.LP_simple(unit, LP_commend.cover_open);
                    break;
                case "door close":
                    Insp_process.LP_simple(unit, LP_commend.cover_close);
                    break;
                case "doorDn":
                    Insp_process.LP_simple(unit, LP_commend.DoorDn);
                    break;
                case "doorUp":
                    Insp_process.LP_simple(unit, LP_commend.DoorUp);
                    break;
                //
                case "ORGN":
                    Insp_process.LP_simple(unit, LP_commend.ORGN);
                    break;
                case "LOAD":
                    Insp_process.LP_simple(unit, LP_commend.LOAD);
                    break;
                case "Gasket":
                    Insp_process.LP_simple(unit, LP_commend.INSP1);
                    break;
                case "INSP2":
                    Insp_process.LP_simple(unit, LP_commend.INSP2);
                    break;
                case "INSP3":
                    Insp_process.LP_simple(unit, LP_commend.INSP3);
                    break;
            }
        }

        #endregion

        #region Robot

        private void btn_robotStart_Click_1(object sender, EventArgs e)
        {

            if (lsb_rbmotion.SelectedItems.Count != 1 || lsb_port.SelectedItems.Count != 1)
                return;
            int shift = 0;
            Loadport port = Loadport.unknow;
            RB_commend commend = RB_commend.none;
            if (lsb_port.SelectedItem.ToString() == "port1")
                port = Loadport.Loadport1;
            else if (lsb_port.SelectedItem.ToString() == "port2")
            {
                port = Loadport.Loadport2;
                shift = 900;
            }
            else
                return;
            switch (lsb_rbmotion.SelectedItem.ToString())
            {
                case "Select":
                    commend = (RB_commend)RB_commend.select;
                    break;
                case "Home":
                    commend = RB_commend.home;
                    break;
                case "Home1":
                    commend = RB_commend.home1;
                    break;
                case "Lach_L":
                    commend = RB_commend.RB_L1_Lach_L + shift;
                    break;
                case "Lach_R":
                    commend = RB_commend.RB_L1_Lach_R + shift;
                    break;
                case "code":
                    commend = RB_commend.RB_L1_code + shift;
                    break;
                case "TE_Out_L":
                    commend = RB_commend.RB_L1_TE_Out_L + shift;
                    break;
                case "TE_Out_R":
                    commend = RB_commend.RB_L1_TE_Out_R + shift;
                    break;
                case "BT_Out_L":
                    commend = RB_commend.RB_L1_BT_Out_L + shift;
                    break;
                case "BT_Out_R":
                    commend = RB_commend.RB_L1_BT_Out_R + shift;
                    break;
                case "FT_Out_L":
                    commend = RB_commend.RB_L1_FT_Out_L + shift;
                    break;
                case "FT_Out_R":
                    commend = RB_commend.RB_L1_FT_Out_R + shift;
                    break;
                case "Snorkel_L":
                    commend = RB_commend.RB_L1_Snorkel_L + shift;
                    break;
                case "Snorkel_R":
                    commend = RB_commend.RB_L1_Snorkel_R + shift;
                    break;
                case "CL_Out_L_1":
                    commend = RB_commend.RB_L1_CL_Out_L_1 + shift;
                    break;
                case "CL_Out_L_2":
                    commend = RB_commend.RB_L1_CL_Out_L_2 + shift;
                    break;
                case "CL_Out_L_3":
                    commend = RB_commend.RB_L1_CL_Out_L_3 + shift;
                    break;
                case "CL_Out_L_4":
                    commend = RB_commend.RB_L1_CL_Out_L_4 + shift;
                    break;
                case "CL_Out_L_5":
                    commend = RB_commend.RB_L1_CL_Out_L_5 + shift;
                    break;
                case "CL_Out_L_6":
                    commend = RB_commend.RB_L1_CL_Out_L_6 + shift;
                    break;
                case "CL_Out_L_7":
                    commend = RB_commend.RB_L1_CL_Out_L_7 + shift;
                    break;
                case "CL_Out_R_1":
                    commend = RB_commend.RB_L1_CL_Out_L_1 + shift;
                    break;
                case "CL_Out_R_2":
                    commend = RB_commend.RB_L1_CL_Out_L_2 + shift;
                    break;
                case "CL_Out_R_3":
                    commend = RB_commend.RB_L1_CL_Out_L_3 + shift;
                    break;
                case "CL_Out_R_4":
                    commend = RB_commend.RB_L1_CL_Out_L_4 + shift;
                    break;
                case "CL_Out_R_5":
                    commend = RB_commend.RB_L1_CL_Out_L_5 + shift;
                    break;
                case "CL_Out_R_6":
                    commend = RB_commend.RB_L1_CL_Out_L_6 + shift;
                    break;
                case "CL_Out_R_7":
                    commend = RB_commend.RB_L1_CL_Out_L_7 + shift;
                    break;
                case "IC_L_1":
                    commend = RB_commend.RB_L1_IC_L_1 + shift;
                    break;
                case "IC_L_2":
                    commend = RB_commend.RB_L1_IC_L_2 + shift;
                    break;
                case "IC_L_3":
                    commend = RB_commend.RB_L1_IC_L_3 + shift;
                    break;
                case "IC_L_4":
                    commend = RB_commend.RB_L1_IC_L_4 + shift;
                    break;
                case "IC_L_5":
                    commend = RB_commend.RB_L1_IC_L_5 + shift;
                    break;
                case "IC_L_6":
                    commend = RB_commend.RB_L1_IC_L_6 + shift;
                    break;
                case "IC_L_7":
                    commend = RB_commend.RB_L1_IC_L_7 + shift;
                    break;
                case "IC_R_1":
                    commend = RB_commend.RB_L1_IC_R_1 + shift;
                    break;
                case "IC_R_2":
                    commend = RB_commend.RB_L1_IC_R_2 + shift;
                    break;
                case "IC_R_3":
                    commend = RB_commend.RB_L1_IC_R_3 + shift;
                    break;
                case "IC_R_4":
                    commend = RB_commend.RB_L1_IC_R_4 + shift;
                    break;
                case "IC_R_5":
                    commend = RB_commend.RB_L1_IC_R_5 + shift;
                    break;
                case "IC_R_6":
                    commend = RB_commend.RB_L1_IC_R_6 + shift;
                    break;
                case "IC_R_7":
                    commend = RB_commend.RB_L1_IC_R_7 + shift;
                    break;
                case "Bottom_1":
                    commend = RB_commend.RB_L1_Bottom_1 + shift;
                    break;
                case "Bottom_2":
                    commend = RB_commend.RB_L1_Bottom_2 + shift;
                    break;
                case "Bottom_3":
                    commend = RB_commend.RB_L1_Bottom_3 + shift;
                    break;
                case "Bottom_4":
                    commend = RB_commend.RB_L1_Bottom_4 + shift;
                    break;
                case "Bottom_5":
                    commend = RB_commend.RB_L1_Bottom_5 + shift;
                    break;
                case "Bottom_6":
                    commend = RB_commend.RB_L1_Bottom_6 + shift;
                    break;
                case "Bottom_7":
                    commend = RB_commend.RB_L1_Bottom_7 + shift;
                    break;
                case "Bottom_8":
                    commend = RB_commend.RB_L1_Bottom_8 + shift;
                    break;
                case "Bottom_9":
                    commend = RB_commend.RB_L1_Bottom_9 + shift;
                    break;
                case "Bottom_10":
                    commend = RB_commend.RB_L1_Bottom_10 + shift;
                    break;
                case "Bottom_11":
                    commend = RB_commend.RB_L1_Bottom_11 + shift;
                    break;
            }
            Insp_process.RB_simple(port, commend);
        }

        private void btn_robot_jog_Click(object sender, EventArgs e)
        {
            Button btn_click = (Button)sender;
            switch (btn_click.Name)
            {
                case "btn_Xpositive":
                    Insp_process.RB_jog(RB_jog_commend.Rb_Xp);
                    break;

                case "btn_Ypositive":
                    Insp_process.RB_jog(RB_jog_commend.Rb_Yp);
                    break;

                case "btn_Zpositive":
                    Insp_process.RB_jog(RB_jog_commend.Rb_Zp);
                    break;

                case "btn_Wpositive":
                    Insp_process.RB_jog(RB_jog_commend.Rb_Wp);
                    break;

                case "btn_Rpositive":
                    Insp_process.RB_jog(RB_jog_commend.Rb_Rp);
                    break;

                case "btn_Cpositive":
                    Insp_process.RB_jog(RB_jog_commend.Rb_Cp);
                    break;

                case "btn_Spositive":
                    Insp_process.RB_jog(RB_jog_commend.Rb_Sp);
                    break;

                case "btn_Xnegative":
                    Insp_process.RB_jog(RB_jog_commend.Rb_Xn);
                    break;
                case "btn_Ynegative":
                    Insp_process.RB_jog(RB_jog_commend.Rb_Yn);
                    break;
                case "btn_Znegative":
                    Insp_process.RB_jog(RB_jog_commend.Rb_Zn);
                    break;
                case "btn_Wnegative":
                    Insp_process.RB_jog(RB_jog_commend.Rb_Wn);
                    break;
                case "btn_Rnegative":
                    Insp_process.RB_jog(RB_jog_commend.Rb_Rn);
                    break;
                case "btn_Cnegative":
                    Insp_process.RB_jog(RB_jog_commend.Rb_Cn);
                    break;
                case "btn_Snegative":
                    Insp_process.RB_jog(RB_jog_commend.Rb_Sn);
                    break;
                case "btn_rb_setpos":
                    break;

            }
        }

        private void btn_rb_setpos_Click(object sender, EventArgs e)
        {
            Insp_process.RB_set_setpos();
        }

        private void btn_set_speed_Click(object sender, EventArgs e)
        {
            string data = "";
            Insp_process.RB_set_speed(data);
        }

        private void btn_disp_Click(object sender, EventArgs e)
        {
            string data = "";
            Insp_process.RB_set_jog_dis(data);
        }

        private void btn_disn_Click(object sender, EventArgs e)
        {
            string data = "";
            Insp_process.RB_set_jog_dis(data);
        }
        #endregion

        #region RFID

        private void btn_p1_RF_r_Click(object sender, EventArgs e)
        {
            Insp_process.RF_read(Loadport.Loadport1);
        }

        private void btn_p1_RF_c_Click(object sender, EventArgs e)
        {
            Insp_process.RF_check(Loadport.Loadport1);
        }

        private void btn_p2_RF_r_Click(object sender, EventArgs e)
        {
            Insp_process.RF_read(Loadport.Loadport2);
        }

        private void btn_p2_RF_c_Click(object sender, EventArgs e)
        {
            Insp_process.RF_check(Loadport.Loadport2);
        }

        #endregion

        private void btn_stop_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_stop();
        }


    }
}
