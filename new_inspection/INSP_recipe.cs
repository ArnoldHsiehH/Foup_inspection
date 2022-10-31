using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Microsoft.Office.Interop.Excel;

namespace new_inspection
{
    class INSP_recipe
    {
        // public static List<string> err_list = new List<string>();
        public static List<recipe> code_list = new List<recipe>();
        //protected
        public void Page_Load()//(object sender, EventArgs e)
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
            xlWorkBook = xlApp.Workbooks.Open(@"D:\new_ins\recipe.xlsx");
            //get the first sheet of the excel
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;
            // get the total row count
            rw = range.Rows.Count;
            //get the total column count
            cl = range.Columns.Count;

            List<recipe> myRows = new List<recipe>();
            // traverse all the row in the excel
            for (rCnt = 1; rCnt <= rw; rCnt++)
            {
                recipe myRow = new recipe();
                //myRow.Col1 = (string)(range.Cells[rCnt, 1] as Range).Value2.ToString();
                myRow.type = string.Format("{0}", range[rCnt, 1].Value2);
                myRow.num = string.Format("{0}", range[rCnt, 2].Value2);
                myRow.RB_motion = string.Format("{0}", range[rCnt, 3].Value2);
                myRow.INSP_message = string.Format("{0}", range[rCnt, 4].Value2);
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
        public List<recipe> get_insp_list(string recipe_name)
        {

            List<recipe> listFind = code_list.FindAll(x => x.type.Contains(recipe_name));

            return listFind;
        }
        public class recipe
        {
            public string type { get; set; }
            public string num { get; set; }
            public string RB_motion { get; set; }
            public string INSP_message { get; set; }

        }
    }
}
