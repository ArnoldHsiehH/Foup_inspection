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

        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_home();
        }

        private void btn_cycleStart_Click(object sender, EventArgs e)
        {

        }

        #region Loadport

        private void btn_Lp1_Click(object sender, EventArgs e)
        {
            Button btn_click = (Button)sender;
            // Console.WriteLine("{0}", btn_click.Name);
            LP_simple(Main_control.MC_unit.Loadport1, btn_click.Text);
        }

        private void btn_Lp2_Click(object sender, EventArgs e)
        {
            Button btn_click = (Button)sender;
            // Console.WriteLine("{0}", btn_click.Name);
            LP_simple(Main_control.MC_unit.Loadport2, btn_click.Text);
        }

        private void btn_port1start_Click_1(object sender, EventArgs e)
        {
            LP_simple(Main_control.MC_unit.Loadport1, lsb_portmotion.SelectedItem.ToString());

        }

        private void btn_port2start_Click(object sender, EventArgs e)
        {
            LP_simple(Main_control.MC_unit.Loadport2, lsb_portmotion.SelectedItem.ToString());
        }

        void LP_simple(Main_control.MC_unit unit, string txt)
        {
            switch (txt)
            {
                case "Clamp":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.Clamp);
                    break;
                case "unClamp":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.unClamp);
                    break;
                case "A300Up":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.A300Up);
                    break;
                case "A300Dn":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.A300Dn);
                    break;
                case "PurgeOn":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.PurgeOn);
                    break;
                case "PurgeOff":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.PurgeOff);
                    break;
                case "Duck":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.Duck);
                    break;
                case "unDuck":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.unDuck);
                    break;
                case "vacOn":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.VacOn);
                    break;
                case "vacOff":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.VacOff);
                    break;
                case "Latch":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.Latch);
                    break;
                case "unLatch":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.unLatch);
                    break;
                case "door open":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.cover_open);
                    break;
                case "door close":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.cover_close);
                    break;
                case "doorDn":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.DoorDn);
                    break;
                case "doorUp":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.DoorUp);
                    break;
                //
                case "ORGN":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.ORGN);
                    break;
                case "LOAD":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.LOAD);
                    break;
                case "Gasket":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.INSP1);
                    break;
                case "INSP2":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.INSP2);
                    break;
                case "INSP3":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.INSP3);
                    break;
            }
        }

        #endregion

        #region Robot
        private void btn_robotStart_Click(object sender, EventArgs e)
        {
            Main_control.MC_unit unit = Main_control.MC_unit.Robot;
            Main_control.Loadport port = Main_control.Loadport.unknow;
            Main_control.RB_commend commend = Main_control.RB_commend.none;
            if (lsb_port.SelectedItem.ToString() == "port1")
                port = Main_control.Loadport.Loadport1;
            if (lsb_port.SelectedItem.ToString() == "port2")
                port = Main_control.Loadport.Loadport2;
            else
                return;
            switch (lsb_rbmotion.SelectedItem.ToString())
            {
                case "Select":
                    commend = Main_control.RB_commend.select;
                    break;
                case "Home":
                    commend = Main_control.RB_commend.home;
                    break;
                case "Home1":
                    commend = Main_control.RB_commend.home1;
                    break;
                case "Lach_L":
                    commend = Main_control.RB_commend.RB_L1_Lach_L;
                    break;
                case "Lach_R":
                    commend = Main_control.RB_commend.RB_L1_Lach_R;
                    break;
                case "code":
                    commend = Main_control.RB_commend.RB_L1_code;
                    break;
                case "TE_Out_L":
                    commend = Main_control.RB_commend.RB_L1_TE_Out_L;
                    break;
                case "TE_Out_R":
                    commend = Main_control.RB_commend.RB_L1_TE_Out_R;
                    break;
                case "BT_Out_L":
                    commend = Main_control.RB_commend.RB_L1_BT_Out_L;
                    break;
                case "BT_Out_R":
                    commend = Main_control.RB_commend.RB_L1_BT_Out_R;
                    break;
                case "FT_Out_L":
                    commend = Main_control.RB_commend.RB_L1_FT_Out_L;
                    break;
                case "FT_Out_R":
                    commend = Main_control.RB_commend.RB_L1_FT_Out_R;
                    break;
                case "Snorkel_L":
                    commend = Main_control.RB_commend.RB_L1_Snorkel_L;
                    break;
                case "Snorkel_R":
                    commend = Main_control.RB_commend.RB_L1_Snorkel_R;
                    break;
                case "CL_Out_L_1":
                    commend = Main_control.RB_commend.RB_L1_CL_Out_L_1;
                    break;
                case "CL_Out_L_2":
                    commend = Main_control.RB_commend.RB_L1_CL_Out_L_2;
                    break;
                case "CL_Out_L_3":
                    commend = Main_control.RB_commend.RB_L1_CL_Out_L_3;
                    break;
                case "CL_Out_L_4":
                    commend = Main_control.RB_commend.RB_L1_CL_Out_L_4;
                    break;
                case "CL_Out_L_5":
                    commend = Main_control.RB_commend.RB_L1_CL_Out_L_5;
                    break;
                case "CL_Out_L_6":
                    commend = Main_control.RB_commend.RB_L1_CL_Out_L_6;
                    break;
                case "CL_Out_L_7":
                    commend = Main_control.RB_commend.RB_L1_CL_Out_L_7;
                    break;
                case "CL_Out_R_1":
                    commend = Main_control.RB_commend.RB_L1_CL_Out_L_1;
                    break;
                case "CL_Out_R_2":
                    commend = Main_control.RB_commend.RB_L1_CL_Out_L_2;
                    break;
                case "CL_Out_R_3":
                    commend = Main_control.RB_commend.RB_L1_CL_Out_L_3;
                    break;
                case "CL_Out_R_4":
                    commend = Main_control.RB_commend.RB_L1_CL_Out_L_4;
                    break;
                case "CL_Out_R_5":
                    commend = Main_control.RB_commend.RB_L1_CL_Out_L_5;
                    break;
                case "CL_Out_R_6":
                    commend = Main_control.RB_commend.RB_L1_CL_Out_L_6;
                    break;
                case "CL_Out_R_7":
                    commend = Main_control.RB_commend.RB_L1_CL_Out_L_7;
                    break;
                case "IC_L_1":
                    commend = Main_control.RB_commend.RB_L1_IC_L_1;
                    break;
                case "IC_L_2":
                    commend = Main_control.RB_commend.RB_L1_IC_L_2;
                    break;
                case "IC_L_3":
                    commend = Main_control.RB_commend.RB_L1_IC_L_3;
                    break;
                case "IC_L_4":
                    commend = Main_control.RB_commend.RB_L1_IC_L_4;
                    break;
                case "IC_L_5":
                    commend = Main_control.RB_commend.RB_L1_IC_L_5;
                    break;
                case "IC_L_6":
                    commend = Main_control.RB_commend.RB_L1_IC_L_6;
                    break;
                case "IC_L_7":
                    commend = Main_control.RB_commend.RB_L1_IC_L_7;
                    break;
                case "IC_R_1":
                    commend = Main_control.RB_commend.RB_L1_IC_R_1;
                    break;
                case "IC_R_2":
                    commend = Main_control.RB_commend.RB_L1_IC_R_2;
                    break;                    
                case "IC_R_3":
                    commend = Main_control.RB_commend.RB_L1_IC_R_3;
                    break;
                case "IC_R_4":
                    commend = Main_control.RB_commend.RB_L1_IC_R_4;
                    break;
                case "IC_R_5":
                    commend = Main_control.RB_commend.RB_L1_IC_R_5;
                    break;
                case "IC_R_6":
                    commend = Main_control.RB_commend.RB_L1_IC_R_6;
                    break;
                case "IC_R_7":
                    commend = Main_control.RB_commend.RB_L1_IC_R_7;
                    break;
                case "Bottom_1":
                    commend = Main_control.RB_commend.RB_L1_Bottom_1;
                    break;
                case "Bottom_2":
                    commend = Main_control.RB_commend.RB_L1_Bottom_2;
                    break;
                case "Bottom_3":
                    commend = Main_control.RB_commend.RB_L1_Bottom_3;
                    break;
                case "Bottom_4":
                    commend = Main_control.RB_commend.RB_L1_Bottom_4;
                    break;
                case "Bottom_5":
                    commend = Main_control.RB_commend.RB_L1_Bottom_5;
                    break;
                case "Bottom_6":
                    commend = Main_control.RB_commend.RB_L1_Bottom_6;
                    break;
                case "Bottom_7":
                    commend = Main_control.RB_commend.RB_L1_Bottom_7;
                    break;
                case "Bottom_8":
                    commend = Main_control.RB_commend.RB_L1_Bottom_8;
                    break;
                case "Bottom_9":
                    commend = Main_control.RB_commend.RB_L1_Bottom_9;
                    break;
                case "Bottom_10":
                    commend = Main_control.RB_commend.RB_L1_Bottom_10;
                    break;
                case "Bottom_11":
                    commend = Main_control.RB_commend.RB_L1_Bottom_11;
                    break;
            }



        }
        private void btn_robot_jog_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
