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
    public partial class frmLog : Form
    {
        logwriter01 logwriter = new logwriter01();
        logwriter01 erroriter = new logwriter01();
        public frmLog()
        {
            InitializeComponent(); 
            logwriter.setLogType = logwriter01.LogDir.System;
            logwriter.setDevice_Name = "UC1";
            erroriter.setLogType = logwriter01.LogDir.Error;
            erroriter.setDevice_Name = "UC1";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            logwriter.write_local_log("message");

            StatusTable.now_page = StatusTable.page.Log;
        }
    }
}
