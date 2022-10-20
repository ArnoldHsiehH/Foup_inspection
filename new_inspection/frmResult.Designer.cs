
namespace new_inspection
{
    partial class frmResult
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txt_test = new System.Windows.Forms.Label();
            this.btn_log_open = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btn_open_picture = new System.Windows.Forms.Button();
            this.btn_start_lp1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(585, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(979, 820);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // txt_test
            // 
            this.txt_test.AutoSize = true;
            this.txt_test.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_test.Location = new System.Drawing.Point(43, 50);
            this.txt_test.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_test.Name = "txt_test";
            this.txt_test.Size = new System.Drawing.Size(47, 21);
            this.txt_test.TabIndex = 29;
            this.txt_test.Text = "Time";
            // 
            // btn_log_open
            // 
            this.btn_log_open.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_log_open.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F);
            this.btn_log_open.Location = new System.Drawing.Point(47, 90);
            this.btn_log_open.Name = "btn_log_open";
            this.btn_log_open.Size = new System.Drawing.Size(157, 38);
            this.btn_log_open.TabIndex = 28;
            this.btn_log_open.Text = "open";
            this.btn_log_open.UseVisualStyleBackColor = true;
            this.btn_log_open.Click += new System.EventHandler(this.btn_log_open_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.dateTimePicker1.Location = new System.Drawing.Point(95, 47);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 32);
            this.dateTimePicker1.TabIndex = 27;
            // 
            // btn_open_picture
            // 
            this.btn_open_picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_open_picture.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_open_picture.Location = new System.Drawing.Point(210, 90);
            this.btn_open_picture.Name = "btn_open_picture";
            this.btn_open_picture.Size = new System.Drawing.Size(157, 39);
            this.btn_open_picture.TabIndex = 25;
            this.btn_open_picture.Text = "Picture";
            this.btn_open_picture.UseVisualStyleBackColor = true;
            this.btn_open_picture.Click += new System.EventHandler(this.btn_open_picture_Click);
            // 
            // btn_start_lp1
            // 
            this.btn_start_lp1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_start_lp1.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_start_lp1.Location = new System.Drawing.Point(373, 90);
            this.btn_start_lp1.Name = "btn_start_lp1";
            this.btn_start_lp1.Size = new System.Drawing.Size(157, 39);
            this.btn_start_lp1.TabIndex = 26;
            this.btn_start_lp1.Text = "Picture_file";
            this.btn_start_lp1.UseVisualStyleBackColor = true;
            this.btn_start_lp1.Click += new System.EventHandler(this.btn_start_lp1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(47, 134);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(532, 737);
            this.dataGridView1.TabIndex = 24;
            // 
            // frmResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1720, 920);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txt_test);
            this.Controls.Add(this.btn_log_open);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btn_open_picture);
            this.Controls.Add(this.btn_start_lp1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmResult";
            this.Text = "frmResult";
            this.Load += new System.EventHandler(this.frmResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label txt_test;
        private System.Windows.Forms.Button btn_log_open;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btn_open_picture;
        private System.Windows.Forms.Button btn_start_lp1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}