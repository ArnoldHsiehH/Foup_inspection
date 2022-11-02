
namespace new_inspection
{
    partial class frmAccount
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
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_Adduser = new System.Windows.Forms.Button();
            this.btn_set_pw = new System.Windows.Forms.Button();
            this.btn_set = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(266, 388);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(246, 22);
            this.textBox3.TabIndex = 23;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(14, 93);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(246, 22);
            this.textBox2.TabIndex = 24;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(246, 22);
            this.textBox1.TabIndex = 25;
            // 
            // btn_Adduser
            // 
            this.btn_Adduser.Location = new System.Drawing.Point(14, 121);
            this.btn_Adduser.Name = "btn_Adduser";
            this.btn_Adduser.Size = new System.Drawing.Size(145, 23);
            this.btn_Adduser.TabIndex = 22;
            this.btn_Adduser.Text = "Add user";
            this.btn_Adduser.UseVisualStyleBackColor = true;
            this.btn_Adduser.Click += new System.EventHandler(this.btn_Adduser_Click);
            // 
            // btn_set_pw
            // 
            this.btn_set_pw.Location = new System.Drawing.Point(518, 388);
            this.btn_set_pw.Name = "btn_set_pw";
            this.btn_set_pw.Size = new System.Drawing.Size(128, 64);
            this.btn_set_pw.TabIndex = 20;
            this.btn_set_pw.Text = "set pw";
            this.btn_set_pw.UseVisualStyleBackColor = true;
            this.btn_set_pw.Click += new System.EventHandler(this.btn_set_pw_Click);
            // 
            // btn_set
            // 
            this.btn_set.Location = new System.Drawing.Point(652, 388);
            this.btn_set.Name = "btn_set";
            this.btn_set.Size = new System.Drawing.Size(128, 64);
            this.btn_set.TabIndex = 21;
            this.btn_set.Text = "set";
            this.btn_set.UseVisualStyleBackColor = true;
            this.btn_set.Click += new System.EventHandler(this.btn_set_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(266, 39);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(867, 343);
            this.dataGridView1.TabIndex = 19;
            // 
            // frmAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 535);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_Adduser);
            this.Controls.Add(this.btn_set_pw);
            this.Controls.Add(this.btn_set);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmAccount";
            this.Text = "frmAccount";
            this.Load += new System.EventHandler(this.frmAccount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_Adduser;
        private System.Windows.Forms.Button btn_set_pw;
        private System.Windows.Forms.Button btn_set;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}