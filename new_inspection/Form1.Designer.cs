
namespace new_inspection
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.btn_About = new System.Windows.Forms.Button();
            this.btn_setting = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_error = new System.Windows.Forms.Button();
            this.btn_Result = new System.Windows.Forms.Button();
            this.btn_Log = new System.Windows.Forms.Button();
            this.btn_Manual = new System.Windows.Forms.Button();
            this.btn_Dashbord = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_login = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txt_userprint = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_RB = new System.Windows.Forms.Label();
            this.txt_L2m = new System.Windows.Forms.Label();
            this.lbl_L2 = new System.Windows.Forms.Label();
            this.txt_L1m = new System.Windows.Forms.Label();
            this.lbl_L1 = new System.Windows.Forms.Label();
            this.txt_Console = new System.Windows.Forms.TextBox();
            this.txt_nowtime = new System.Windows.Forms.Label();
            this.txt_RBm = new System.Windows.Forms.Label();
            this.lbl_E84_P2_status = new System.Windows.Forms.Label();
            this.lbl_E84_P1_status = new System.Windows.Forms.Label();
            this.lbl_PLC_status = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.txt_uptime = new System.Windows.Forms.Label();
            this.pnlFormLoader = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel1.Controls.Add(this.pnlNav);
            this.panel1.Controls.Add(this.btn_About);
            this.panel1.Controls.Add(this.btn_setting);
            this.panel1.Controls.Add(this.btn_exit);
            this.panel1.Controls.Add(this.btn_error);
            this.panel1.Controls.Add(this.btn_Result);
            this.panel1.Controls.Add(this.btn_Log);
            this.panel1.Controls.Add(this.btn_Manual);
            this.panel1.Controls.Add(this.btn_Dashbord);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 1021);
            this.panel1.TabIndex = 3;
            // 
            // pnlNav
            // 
            this.pnlNav.BackColor = System.Drawing.Color.Black;
            this.pnlNav.Location = new System.Drawing.Point(11, 199);
            this.pnlNav.Margin = new System.Windows.Forms.Padding(2);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(10, 55);
            this.pnlNav.TabIndex = 5;
            // 
            // btn_About
            // 
            this.btn_About.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_About.FlatAppearance.BorderSize = 0;
            this.btn_About.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_About.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.btn_About.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_About.Location = new System.Drawing.Point(0, 473);
            this.btn_About.Name = "btn_About";
            this.btn_About.Size = new System.Drawing.Size(200, 55);
            this.btn_About.TabIndex = 7;
            this.btn_About.Text = "About";
            this.btn_About.UseVisualStyleBackColor = true;
            this.btn_About.Click += new System.EventHandler(this.btn_About_Click);
            // 
            // btn_setting
            // 
            this.btn_setting.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_setting.FlatAppearance.BorderSize = 0;
            this.btn_setting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_setting.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.btn_setting.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_setting.Location = new System.Drawing.Point(0, 911);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Size = new System.Drawing.Size(200, 55);
            this.btn_setting.TabIndex = 6;
            this.btn_setting.Text = "Setting";
            this.btn_setting.UseVisualStyleBackColor = true;
            this.btn_setting.Click += new System.EventHandler(this.btn_setting_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_exit.FlatAppearance.BorderSize = 0;
            this.btn_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_exit.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.btn_exit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_exit.Location = new System.Drawing.Point(0, 966);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(200, 55);
            this.btn_exit.TabIndex = 10;
            this.btn_exit.Text = "Exit";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_error
            // 
            this.btn_error.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_error.FlatAppearance.BorderSize = 0;
            this.btn_error.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_error.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.btn_error.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_error.Location = new System.Drawing.Point(0, 418);
            this.btn_error.Name = "btn_error";
            this.btn_error.Size = new System.Drawing.Size(200, 55);
            this.btn_error.TabIndex = 4;
            this.btn_error.Text = "Error";
            this.btn_error.UseVisualStyleBackColor = true;
            this.btn_error.Click += new System.EventHandler(this.btn_error_Click);
            // 
            // btn_Result
            // 
            this.btn_Result.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btn_Result.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Result.FlatAppearance.BorderSize = 0;
            this.btn_Result.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Result.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.btn_Result.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_Result.Location = new System.Drawing.Point(0, 363);
            this.btn_Result.Name = "btn_Result";
            this.btn_Result.Size = new System.Drawing.Size(200, 55);
            this.btn_Result.TabIndex = 8;
            this.btn_Result.Text = "Result";
            this.btn_Result.UseVisualStyleBackColor = false;
            this.btn_Result.Click += new System.EventHandler(this.btn_history_Click);
            // 
            // btn_Log
            // 
            this.btn_Log.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btn_Log.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Log.FlatAppearance.BorderSize = 0;
            this.btn_Log.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Log.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.btn_Log.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_Log.Location = new System.Drawing.Point(0, 308);
            this.btn_Log.Name = "btn_Log";
            this.btn_Log.Size = new System.Drawing.Size(200, 55);
            this.btn_Log.TabIndex = 5;
            this.btn_Log.Text = "Log";
            this.btn_Log.UseVisualStyleBackColor = false;
            this.btn_Log.Click += new System.EventHandler(this.btn_Log_Click);
            // 
            // btn_Manual
            // 
            this.btn_Manual.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Manual.FlatAppearance.BorderSize = 0;
            this.btn_Manual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Manual.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.btn_Manual.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_Manual.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_Manual.Location = new System.Drawing.Point(0, 253);
            this.btn_Manual.Name = "btn_Manual";
            this.btn_Manual.Size = new System.Drawing.Size(200, 55);
            this.btn_Manual.TabIndex = 9;
            this.btn_Manual.Text = "Manual";
            this.btn_Manual.UseVisualStyleBackColor = true;
            this.btn_Manual.Click += new System.EventHandler(this.btn_Manual_Click);
            // 
            // btn_Dashbord
            // 
            this.btn_Dashbord.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Dashbord.FlatAppearance.BorderSize = 0;
            this.btn_Dashbord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Dashbord.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.btn_Dashbord.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_Dashbord.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_Dashbord.Location = new System.Drawing.Point(0, 198);
            this.btn_Dashbord.Name = "btn_Dashbord";
            this.btn_Dashbord.Size = new System.Drawing.Size(200, 55);
            this.btn_Dashbord.TabIndex = 3;
            this.btn_Dashbord.Text = "Dashboard";
            this.btn_Dashbord.UseVisualStyleBackColor = true;
            this.btn_Dashbord.Click += new System.EventHandler(this.btn_Dashbord_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_login);
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Controls.Add(this.txt_userprint);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 98);
            this.panel3.TabIndex = 3;
            // 
            // btn_login
            // 
            this.btn_login.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.btn_login.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_login.Location = new System.Drawing.Point(69, 54);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(131, 44);
            this.btn_login.TabIndex = 0;
            this.btn_login.Text = "change";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::new_inspection.Properties.Resources.profile;
            this.pictureBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(57, 51);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // txt_userprint
            // 
            this.txt_userprint.AutoSize = true;
            this.txt_userprint.Font = new System.Drawing.Font("Microsoft YaHei UI", 20.25F);
            this.txt_userprint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt_userprint.Location = new System.Drawing.Point(63, 3);
            this.txt_userprint.Name = "txt_userprint";
            this.txt_userprint.Size = new System.Drawing.Size(80, 35);
            this.txt_userprint.TabIndex = 3;
            this.txt_userprint.Text = "User:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::new_inspection.Properties.Resources._2560px_Hirata_Logo_svg;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Silver;
            this.panel4.Controls.Add(this.button6);
            this.panel4.Controls.Add(this.button5);
            this.panel4.Controls.Add(this.button4);
            this.panel4.Controls.Add(this.button3);
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.lbl_RB);
            this.panel4.Controls.Add(this.txt_L2m);
            this.panel4.Controls.Add(this.lbl_L2);
            this.panel4.Controls.Add(this.txt_L1m);
            this.panel4.Controls.Add(this.lbl_L1);
            this.panel4.Controls.Add(this.txt_Console);
            this.panel4.Controls.Add(this.txt_nowtime);
            this.panel4.Controls.Add(this.txt_RBm);
            this.panel4.Controls.Add(this.lbl_E84_P2_status);
            this.panel4.Controls.Add(this.lbl_E84_P1_status);
            this.panel4.Controls.Add(this.lbl_PLC_status);
            this.panel4.Controls.Add(this.lbl_Title);
            this.panel4.Controls.Add(this.txt_uptime);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(200, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1704, 151);
            this.panel4.TabIndex = 5;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(28, 106);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(113, 29);
            this.button6.TabIndex = 19;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(28, 72);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(113, 31);
            this.button5.TabIndex = 18;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button4.Location = new System.Drawing.Point(171, 112);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(92, 23);
            this.button4.TabIndex = 17;
            this.button4.Text = "ready to unload2";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            // 
            // button3
            // 
            this.button3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button3.Location = new System.Drawing.Point(171, 87);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(92, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "ready to unload1";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            // 
            // button2
            // 
            this.button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button2.Location = new System.Drawing.Point(171, 61);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "ready to load 2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // button1
            // 
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(171, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "ready to load 1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // lbl_RB
            // 
            this.lbl_RB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_RB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_RB.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.lbl_RB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_RB.Location = new System.Drawing.Point(488, 23);
            this.lbl_RB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_RB.Name = "lbl_RB";
            this.lbl_RB.Size = new System.Drawing.Size(182, 21);
            this.lbl_RB.TabIndex = 12;
            this.lbl_RB.Text = "lbl_RB";
            // 
            // txt_L2m
            // 
            this.txt_L2m.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_L2m.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txt_L2m.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.txt_L2m.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt_L2m.Location = new System.Drawing.Point(860, 47);
            this.txt_L2m.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_L2m.Name = "txt_L2m";
            this.txt_L2m.Size = new System.Drawing.Size(182, 21);
            this.txt_L2m.TabIndex = 11;
            this.txt_L2m.Text = "lbl_L2m";
            // 
            // lbl_L2
            // 
            this.lbl_L2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_L2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_L2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.lbl_L2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_L2.Location = new System.Drawing.Point(860, 23);
            this.lbl_L2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_L2.Name = "lbl_L2";
            this.lbl_L2.Size = new System.Drawing.Size(182, 21);
            this.lbl_L2.TabIndex = 11;
            this.lbl_L2.Text = "lbl_L2";
            // 
            // txt_L1m
            // 
            this.txt_L1m.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_L1m.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txt_L1m.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.txt_L1m.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt_L1m.Location = new System.Drawing.Point(674, 47);
            this.txt_L1m.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_L1m.Name = "txt_L1m";
            this.txt_L1m.Size = new System.Drawing.Size(182, 21);
            this.txt_L1m.TabIndex = 10;
            this.txt_L1m.Text = "lbl_L1m";
            // 
            // lbl_L1
            // 
            this.lbl_L1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_L1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_L1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.lbl_L1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_L1.Location = new System.Drawing.Point(674, 23);
            this.lbl_L1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_L1.Name = "lbl_L1";
            this.lbl_L1.Size = new System.Drawing.Size(182, 21);
            this.lbl_L1.TabIndex = 10;
            this.lbl_L1.Text = "lbl_L1";
            // 
            // txt_Console
            // 
            this.txt_Console.BackColor = System.Drawing.Color.DarkGray;
            this.txt_Console.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.txt_Console.Location = new System.Drawing.Point(1304, 33);
            this.txt_Console.Multiline = true;
            this.txt_Console.Name = "txt_Console";
            this.txt_Console.ReadOnly = true;
            this.txt_Console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Console.Size = new System.Drawing.Size(395, 113);
            this.txt_Console.TabIndex = 9;
            // 
            // txt_nowtime
            // 
            this.txt_nowtime.AutoSize = true;
            this.txt_nowtime.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.txt_nowtime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt_nowtime.Location = new System.Drawing.Point(1236, 9);
            this.txt_nowtime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_nowtime.Name = "txt_nowtime";
            this.txt_nowtime.Size = new System.Drawing.Size(53, 21);
            this.txt_nowtime.TabIndex = 8;
            this.txt_nowtime.Text = "TIME:";
            // 
            // txt_RBm
            // 
            this.txt_RBm.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_RBm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txt_RBm.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.txt_RBm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt_RBm.Location = new System.Drawing.Point(488, 47);
            this.txt_RBm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_RBm.Name = "txt_RBm";
            this.txt_RBm.Size = new System.Drawing.Size(182, 21);
            this.txt_RBm.TabIndex = 4;
            this.txt_RBm.Text = "lbl_txt_RBm";
            // 
            // lbl_E84_P2_status
            // 
            this.lbl_E84_P2_status.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_E84_P2_status.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_E84_P2_status.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.lbl_E84_P2_status.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_E84_P2_status.Location = new System.Drawing.Point(860, 72);
            this.lbl_E84_P2_status.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_E84_P2_status.Name = "lbl_E84_P2_status";
            this.lbl_E84_P2_status.Size = new System.Drawing.Size(182, 21);
            this.lbl_E84_P2_status.TabIndex = 4;
            this.lbl_E84_P2_status.Text = "lbl_E84_P2_status";
            // 
            // lbl_E84_P1_status
            // 
            this.lbl_E84_P1_status.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_E84_P1_status.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_E84_P1_status.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.lbl_E84_P1_status.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_E84_P1_status.Location = new System.Drawing.Point(674, 72);
            this.lbl_E84_P1_status.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_E84_P1_status.Name = "lbl_E84_P1_status";
            this.lbl_E84_P1_status.Size = new System.Drawing.Size(182, 21);
            this.lbl_E84_P1_status.TabIndex = 4;
            this.lbl_E84_P1_status.Text = "lbl_E84_P1_status";
            // 
            // lbl_PLC_status
            // 
            this.lbl_PLC_status.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_PLC_status.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_PLC_status.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.lbl_PLC_status.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_PLC_status.Location = new System.Drawing.Point(302, 23);
            this.lbl_PLC_status.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_PLC_status.Name = "lbl_PLC_status";
            this.lbl_PLC_status.Size = new System.Drawing.Size(182, 21);
            this.lbl_PLC_status.TabIndex = 4;
            this.lbl_PLC_status.Text = "lbl_Title";
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 28.2F);
            this.lbl_Title.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_Title.Location = new System.Drawing.Point(5, 9);
            this.lbl_Title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(161, 50);
            this.lbl_Title.TabIndex = 4;
            this.lbl_Title.Text = "lbl_Title";
            // 
            // txt_uptime
            // 
            this.txt_uptime.AutoSize = true;
            this.txt_uptime.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.txt_uptime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt_uptime.Location = new System.Drawing.Point(1487, 9);
            this.txt_uptime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_uptime.Name = "txt_uptime";
            this.txt_uptime.Size = new System.Drawing.Size(142, 21);
            this.txt_uptime.TabIndex = 8;
            this.txt_uptime.Text = "SYSTEM UPTIME:";
            // 
            // pnlFormLoader
            // 
            this.pnlFormLoader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFormLoader.Location = new System.Drawing.Point(200, 151);
            this.pnlFormLoader.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFormLoader.Name = "pnlFormLoader";
            this.pnlFormLoader.Size = new System.Drawing.Size(1704, 980);
            this.pnlFormLoader.TabIndex = 6;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1021);
            this.Controls.Add(this.pnlFormLoader);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_setting;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Panel pnlNav;
        private System.Windows.Forms.Button btn_error;
        private System.Windows.Forms.Button btn_About;
        private System.Windows.Forms.Button btn_Result;
        private System.Windows.Forms.Button btn_Log;
        private System.Windows.Forms.Button btn_Manual;
        private System.Windows.Forms.Button btn_Dashbord;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label txt_userprint;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_RB;
        private System.Windows.Forms.Label txt_L2m;
        private System.Windows.Forms.Label lbl_L2;
        private System.Windows.Forms.Label txt_L1m;
        private System.Windows.Forms.Label lbl_L1;
        private System.Windows.Forms.TextBox txt_Console;
        private System.Windows.Forms.Label txt_nowtime;
        private System.Windows.Forms.Label txt_RBm;
        private System.Windows.Forms.Label lbl_E84_P2_status;
        private System.Windows.Forms.Label lbl_E84_P1_status;
        private System.Windows.Forms.Label lbl_PLC_status;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Label txt_uptime;
        private System.Windows.Forms.Panel pnlFormLoader;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Timer timer1;
    }
}

