using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace new_inspection
{
    public partial class frmError : Form
    {
        Error err_code = new Error();
        public frmError()
        {
            InitializeComponent();
            Error.EventErr += new Error.ErrEvent(UI_error_update);

        }

        
        private void frmError_Load(object sender, EventArgs e)
        {

        }
        private void btn_mute_Click(object sender, EventArgs e)
        {

        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            //需新增 reset 動作
            err_code.err_clear();
            listBox1.Items.Clear();
        }

        private void UI_error_update(bool need_update, List<string> Message)
        {
            this.Show();
            this.Invoke(new MethodInvoker(delegate () { write_UI(need_update, Message); }));
        }
        public static List<string> err_list = new List<string>();
        private void write_UI(bool need_update, List<string> Message)
        {

            err_list = new List<string>();

            err_list = Message;
            //listBox1.DataSource = err_list;
            if (need_update == true)
            {
                listBox1.Items.Clear();
                listBox1.Items.AddRange(err_list.ToArray());
            }


        }

        private void btn_close_Click(object sender, EventArgs e)
        {
         
            this.Hide();
        }
    }
    public class Error
    {
        public delegate void ErrEvent(bool need_update, List<string> Message);
        public static event ErrEvent EventErr;


        logwriter01 logwriter = new logwriter01();

        public bool check_error
        {
            get 
            {
                return err_flag;
            }
        }
        public static List<string> err_list = new List<string>();
        public static List<MyRow> code_list = new List<MyRow>();

        static bool err_flag=false;
        bool first_run = true;

        public void err_clear()
        {
            Error.err_list.Clear();//異常清除
         
            err_flag = false;
        }
        public void write_alarmMessage(error_unit errlist, string errMessage)
        {
            err_flag = true;
            if (first_run)
            {
                Page_Load();
                first_run = false;
            }

            logwriter.setLogType = logwriter01.LogDir.Error;
            logwriter.setDevice_Name = "Error";
            if (!err_list.Exists(x => x.ToString() == errMessage))
            {
                logwriter.write_local_log(errMessage);
                err_list.Add(errMessage);
                EventErr(true, err_list);
            }
            else
                EventErr(false, err_list);
        }

        public void write_warnMessage(error_unit errlist, string errMessage)
        {
            if (first_run)
            {
                Page_Load();
                first_run = false;
            }

            logwriter.setLogType = logwriter01.LogDir.Error;
            logwriter.setDevice_Name = "Warning";
            if (!err_list.Exists(x => x.ToString() == errMessage))
            {
                logwriter.write_local_log(errMessage);
                err_list.Add(errMessage);
                EventErr(true, err_list);
            }
            else
                EventErr(false, err_list);
        }
        public void error_code(string err_code)
        {

        }
        protected void Page_Load()//(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            Range range;

            int rCnt;
            int rw = 0;
            int cl = 0;

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            //open the excel
            xlWorkBook = xlApp.Workbooks.Open(@"D:\Desktop\work\ni\new_inspection\new_inspection\bin\Debug\Alarm_codes.xlsx");
            //get the first sheet of the excel
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;
            // get the total row count
            rw = range.Rows.Count;
            //get the total column count
            cl = range.Columns.Count;

            List<MyRow> myRows = new List<MyRow>();
            // traverse all the row in the excel
            for (rCnt = 1; rCnt <= rw; rCnt++)
            {
                MyRow myRow = new MyRow();
                //myRow.Col1 = (string)(range.Cells[rCnt, 1] as Range).Value2.ToString();
                myRow.code = string.Format("{0}", range[rCnt, 1].Value2);
                myRow.message = string.Format("{0}", range[rCnt, 2].Value2);
                myRows.Add(myRow);
            }
            code_list.Clear();
            code_list = myRows;

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

        }
        public class MyRow
        {
            public string code { get; set; }
            public string message { get; set; }

        }
        public enum error_unit
        {
            system
            #region PLC
            #endregion
            #region Robot
            #endregion
            #region LP
            #endregion
            #region ITRI
            #endregion
            #region other
            #endregion
        }


    }
}
