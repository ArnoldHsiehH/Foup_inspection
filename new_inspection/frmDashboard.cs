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

        private void button1_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_pause();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_continue();

        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_home();

        }

        private void btn_Load1_Click(object sender, EventArgs e)
        {
            Insp_process.Insp_Load();
        }
    }
}
