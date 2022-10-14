
namespace new_inspection
{
    partial class frmError
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
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_mute = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.LB_warn = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(27, 269);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(101, 45);
            this.btn_reset.TabIndex = 0;
            this.btn_reset.Text = "reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_mute
            // 
            this.btn_mute.Location = new System.Drawing.Point(134, 269);
            this.btn_mute.Name = "btn_mute";
            this.btn_mute.Size = new System.Drawing.Size(101, 45);
            this.btn_mute.TabIndex = 0;
            this.btn_mute.Text = "Mute";
            this.btn_mute.UseVisualStyleBackColor = true;
            this.btn_mute.Click += new System.EventHandler(this.btn_mute_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(241, 269);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(101, 45);
            this.btn_close.TabIndex = 1;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(27, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(315, 124);
            this.listBox1.TabIndex = 2;
            // 
            // LB_warn
            // 
            this.LB_warn.FormattingEnabled = true;
            this.LB_warn.ItemHeight = 12;
            this.LB_warn.Location = new System.Drawing.Point(27, 139);
            this.LB_warn.Name = "LB_warn";
            this.LB_warn.Size = new System.Drawing.Size(315, 124);
            this.LB_warn.TabIndex = 2;
            // 
            // frmError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 349);
            this.ControlBox = false;
            this.Controls.Add(this.LB_warn);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_mute);
            this.Controls.Add(this.btn_reset);
            this.Name = "frmError";
            this.Text = "Error";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmError_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_mute;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox LB_warn;
    }
}