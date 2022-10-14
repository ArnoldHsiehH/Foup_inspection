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

namespace new_inspection
{
    public partial class Log_Read : UserControl
    {
        public Log_Read()
        {
            InitializeComponent();
        }
        public string LogPath = "";

        private RichTextBox[] LogPage;
        private TabPage[] Tabs;

        private int[] LogCount;

        logwriter01 logwriter = new logwriter01();
        private void Log_Read_Load(object sender, EventArgs e)
        {
            UI_Ini();
            //  dtpLogEnd.Value = DateTime.Now;
            dtpLogStart.Value = DateTime.Now;//.AddDays(-7);

            for (int i = 0; i < (int)logwriter01.LogDir.MaxCnt; i++)
            {
                cboDirectory.Items.Add(logwriter01.LogDir.System + i);
            }
            cboDirectory.SelectedIndex = 0;
            logwriter01.EventLog += new logwriter01.LogEvent(UI_Log_update);
        }
        private void UI_Ini()
        {

            // Page = _Page;
            Tabs = new TabPage[(int)logwriter01.LogDir.MaxCnt];
            LogPage = new RichTextBox[(int)logwriter01.LogDir.MaxCnt];
            LogCount = new int[(int)logwriter01.LogDir.MaxCnt];
            LogPath = logwriter.LogPath;
            //LogPath = string.Format("{0}{1}{2}{3}", AppSetting.LoadSetting("LogPath", "D:\\HirataMain_Log\\"), NormalStatic.DeviceType, NormalStatic.UnderLine, NormalStatic.Log); // 20201211 Walson
            for (int i = 0; i < (int)logwriter01.LogDir.MaxCnt; i++)
            {
                LogCount[i] = 0;
                Tabs[i] = new TabPage();
                LogPage[i] = new RichTextBox();

                Tabs[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
                Tabs[i].Dock = System.Windows.Forms.DockStyle.Fill;
                Tabs[i].Location = new System.Drawing.Point(4, 29);
                Tabs[i].Padding = new System.Windows.Forms.Padding(3);
                Tabs[i].Text = (logwriter01.LogDir.System + i).ToString();
                //Tabs[i].Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                tbctLog.Controls.Add(Tabs[i]);

                LogPage[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
                LogPage[i].Dock = System.Windows.Forms.DockStyle.Fill;
                LogPage[i].Location = new System.Drawing.Point(3, 3);
                LogPage[i].ReadOnly = true;
                LogPage[i].Text = "";
                Tabs[i].Controls.Add(LogPage[i]);
                //   logwriter.CheckFolder((logwriter01.LogDir) i);
            }
        }
        private void UI_Log_update(logwriter01.LogDir _LogFile, string _LogMessage)
        {
            this.Invoke(
                new MethodInvoker
                (  
                    delegate (){write_UI(_LogFile, _LogMessage);}
                )
            );
        }
        private void write_UI(logwriter01.LogDir _LogFile, string _LogMessage) 
        {
            LogPage[(int)_LogFile].Text += "\r\n";
            LogPage[(int)_LogFile].Text += _LogMessage;
        }

        private void btnLogSearch_Click(object sender, EventArgs e)
        {
            logwriter.CheckFolder((logwriter01.LogDir)Enum.Parse(typeof(logwriter01.LogDir), cboDirectory.Text));
            trvLog.Nodes.Clear();

            LogPath = logwriter.LogPath;

            List<string> dirs = new List<string>
            (
                Directory.EnumerateDirectories
                (
                    string.Format("{0}\\{1}\\", LogPath, cboDirectory.Text),
                    dtpLogStart.Value.Date.AddDays(0).ToString("yyyy_MM_dd")  //
                )
            );

            if (dirs.Count == 0)
                return;
            DirectoryInfo rootDir = new DirectoryInfo(dirs[0]);
            foreach (var file in rootDir.GetFiles())    //取得檔案名放入表單
            {
                TreeNode n = new TreeNode(file.Name, 13, 13);
                trvLog.Nodes.Add(n);
            }

        }

        private void trvLog_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Process.Start(string.Format("{0}\\{1}\\{2}\\{3}", LogPath, cboDirectory.Text, dtpLogStart.Value.Date.AddDays(0).ToString("yyyy_MM_dd"), trvLog.SelectedNode.Text));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
