using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_inspection
{
    public static class StatusTable
    {
        #region system
        public static page now_page = new page();
      
        public enum page 
        {
            unknow,
            Dashboard,
            Manual,
            Log
        }
        #endregion

        #region control
        public static string main_thread_status = "";
        #endregion
    }
}
