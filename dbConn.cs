using System;
using System.Collections.Generic;
using System.Text;

namespace Anchors
{
    class dbConn
    {
        private static string sDBConn;

        public static String DBConn
        {
            get { return sDBConn; }
            set { sDBConn = value; }
        }
    }
}
