
namespace new_inspection
{
    partial class frmUser_account
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_login = new System.Windows.Forms.Button();
            this.txb_pw = new System.Windows.Forms.TextBox();
            this.txb_user = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 69);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 30);
            this.label1.TabIndex = 12;
            this.label1.Text = "Pasword :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(18, 30);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 30);
            this.label8.TabIndex = 13;
            this.label8.Text = "User :";
            // 
            // btn_login
            // 
            this.btn_login.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F);
            this.btn_login.Location = new System.Drawing.Point(190, 107);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(128, 46);
            this.btn_login.TabIndex = 11;
            this.btn_login.Text = "login";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // txb_pw
            // 
            this.txb_pw.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F);
            this.txb_pw.Location = new System.Drawing.Point(142, 66);
            this.txb_pw.Name = "txb_pw";
            this.txb_pw.PasswordChar = '●';
            this.txb_pw.Size = new System.Drawing.Size(176, 35);
            this.txb_pw.TabIndex = 10;
            // 
            // txb_user
            // 
            this.txb_user.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F);
            this.txb_user.Location = new System.Drawing.Point(143, 27);
            this.txb_user.Name = "txb_user";
            this.txb_user.Size = new System.Drawing.Size(176, 35);
            this.txb_user.TabIndex = 9;
            this.txb_user.Text = "Hirata";
            // 
            // frmUser_account
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 180);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.txb_pw);
            this.Controls.Add(this.txb_user);
            this.Name = "frmUser_account";
            this.Text = "frmUser_account";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_login;
        public System.Windows.Forms.TextBox txb_pw;
        public System.Windows.Forms.TextBox txb_user;
    }
}