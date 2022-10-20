using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace new_inspection
{
    public partial class frmResult : Form
    {
        public frmResult()
        {
            InitializeComponent();
        }

        private void frmResult_Load(object sender, EventArgs e)
        {

        }

        private void btn_start_lp1_Click(object sender, EventArgs e)
        {
            string sPath = @"D:\inspectionBackup";//檔案位置///D:\inspectionBackup
            Process.Start(sPath);
        }

        private void btn_log_open_Click(object sender, EventArgs e)
        {
            DateTime date1 = dateTimePicker1.Value;
            string filename = string.Format(@"{0}.csv", date1.ToString("yyyyMMdd"));
            string sPath = System.IO.Path.Combine(@"C:\temp\Ins_result\", filename);//檔案位置

            if (!System.IO.File.Exists(sPath))
            {
                MessageBox.Show("file not Exists");
                return;
            }

            var readConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };

            using (var reader = new StreamReader(sPath))
            using (var csv = new CsvReader(reader, readConfiguration))
            {
                var records = csv.GetRecords<Ins_event>();
                dataGridView1.DataSource = records.ToList();
                foreach (var result in records)
                {
                    Console.WriteLine(result.Ins_item + "," + result.Ins_time);
                }
            }
        }

        private void btn_open_picture_Click(object sender, EventArgs e)
        {
            //檢測點_序號_時戳_結果 
            if (dataGridView1.ColumnCount == 0)
            {
                MessageBox.Show("please select picture");
                return;
            }

            string filename = string.Format(
                @"{0}_{1}_{2}_{3}",
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value,
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value,
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString().Trim(),
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value
                );
            filename = filename.Trim();
            filename = filename + ".png";
            Console.WriteLine(filename);//CL_In_R_2_123456789_199950608_1
                                        //   filename = @"BottomP1_1_WF602199_06222022120650_0.png";

            DateTime date1 = dateTimePicker1.Value;

            string date_time = date1.ToString("yyyyMMdd");
            string sPath = System.IO.Path.Combine(@"D:\inspectionBackup", date_time, "results", filename);//檔案位置///D:\inspectionBackup

            if (!System.IO.File.Exists(sPath))
            {
                MessageBox.Show("file not Exists");
                return;
            }

            Console.WriteLine(sPath);
            pictureBox1.ImageLocation = sPath;
            //  Process.Start(sPath);
        }
    }
    public class Ins_event
    {
        public string Ins_time { get; set; }
        public string Ins_Id { get; set; }
        public string Ins_item { get; set; }
        public string Ins_result { get; set; }

    }
}
