
namespace new_inspection
{
    partial class Log_Read
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.trvLog = new System.Windows.Forms.TreeView();
            this.dtpLogStart = new System.Windows.Forms.DateTimePicker();
            this.btnLogSearch = new System.Windows.Forms.Button();
            this.cboDirectory = new System.Windows.Forms.ComboBox();
            this.tbctLog = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // trvLog
            // 
            this.trvLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvLog.Location = new System.Drawing.Point(31, 121);
            this.trvLog.Name = "trvLog";
            this.trvLog.Size = new System.Drawing.Size(135, 108);
            this.trvLog.TabIndex = 22;
            this.trvLog.DoubleClick += new System.EventHandler(this.trvLog_DoubleClick);
            // 
            // dtpLogStart
            // 
            this.dtpLogStart.Location = new System.Drawing.Point(31, 33);
            this.dtpLogStart.MinDate = new System.DateTime(2019, 3, 1, 0, 0, 0, 0);
            this.dtpLogStart.Name = "dtpLogStart";
            this.dtpLogStart.Size = new System.Drawing.Size(135, 22);
            this.dtpLogStart.TabIndex = 1002;
            this.dtpLogStart.Value = new System.DateTime(2019, 3, 25, 16, 13, 9, 0);
            // 
            // btnLogSearch
            // 
            this.btnLogSearch.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLogSearch.Location = new System.Drawing.Point(31, 87);
            this.btnLogSearch.Name = "btnLogSearch";
            this.btnLogSearch.Size = new System.Drawing.Size(135, 28);
            this.btnLogSearch.TabIndex = 1004;
            this.btnLogSearch.Text = "Search";
            this.btnLogSearch.UseVisualStyleBackColor = true;
            this.btnLogSearch.Click += new System.EventHandler(this.btnLogSearch_Click);
            // 
            // cboDirectory
            // 
            this.cboDirectory.FormattingEnabled = true;
            this.cboDirectory.Location = new System.Drawing.Point(31, 61);
            this.cboDirectory.Name = "cboDirectory";
            this.cboDirectory.Size = new System.Drawing.Size(135, 20);
            this.cboDirectory.TabIndex = 1005;
            // 
            // tbctLog
            // 
            this.tbctLog.Location = new System.Drawing.Point(172, 33);
            this.tbctLog.Name = "tbctLog";
            this.tbctLog.SelectedIndex = 0;
            this.tbctLog.Size = new System.Drawing.Size(764, 540);
            this.tbctLog.TabIndex = 0;
            // 
            // Log_Read
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.cboDirectory);
            this.Controls.Add(this.btnLogSearch);
            this.Controls.Add(this.dtpLogStart);
            this.Controls.Add(this.trvLog);
            this.Controls.Add(this.tbctLog);
            this.Name = "Log_Read";
            this.Size = new System.Drawing.Size(982, 619);
            this.Load += new System.EventHandler(this.Log_Read_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TreeView trvLog;
        private System.Windows.Forms.DateTimePicker dtpLogStart;
        private System.Windows.Forms.Button btnLogSearch;
        private System.Windows.Forms.ComboBox cboDirectory;
        private System.Windows.Forms.TabControl tbctLog;
    }
}
