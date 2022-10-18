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

        public frmManual()
        {
            InitializeComponent();

            btn_Lp1 = new Button[] {
                btn_Lp1_clamp,btn_Lp1_unclamp,
                btn_Lp1_A300Up,btn_Lp1_A300Dn,
                btn_Lp1_purge,btn_Lp1_unpurge,
                btn_Lp1_duck,btn_Lp1_unduck,
                btn_Lp1_vacOn,btn_Lp1_vacOff,
                btn_Lp1_Latch,btn_Lp1_unLatch,
                btn_Lp1_coverOn,btn_Lp2_coverOff,
                btn_Lp1_Dn,btn_Lp1_Up
            };
            foreach (Button button in btn_Lp1)
            {
                button.Click += new System.EventHandler(this.btn_Lp1_Click);
            }
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
            {
                button.Click += new System.EventHandler(this.btn_Lp2_Click);
            }
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

        void LP_simple(Main_control.MC_unit unit, string txt)
        {
            switch (txt)
            {
                case "Clamp":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.L1_Clamp);
                    break;
                case "unClamp":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.L1_unClamp);
                    break;
                case "A300Up":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.L1_A300Up);
                    break;
                case "A300Dn":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.L1_A300Dn);
                    break;
                case "PurgeOn":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.L1_PurgeOn);
                    break;
                case "PurgeOff":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.L1_PurgeOff);
                    break;
                case "Duck":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.L1_Duck);
                    break;
                case "unDuck":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.L1_unDuck);
                    break;
                case "vacOn":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.L1_VacOn);
                    break;
                case "vacOff":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.L1_VacOff);
                    break;
                case "Latch":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.L1_Latch);
                    break;
                case "unLatch":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.L1_unLatch);
                    break;
                case "door open":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.L1_cover_open);
                    break;
                case "door close":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.L1_cover_close);
                    break;
                case "doorDn":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.L1_DoorDn);
                    break;
                case "doorUp":
                    Insp_process.LP_simple(unit, Main_control.LP_commend.L1_DoorUp);
                    break;
            }
        }

        private void btn_port1start_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_port2start_Click(object sender, EventArgs e)
        {

        }

        #endregion


    }
}
