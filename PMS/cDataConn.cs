using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PMS
{
    class cDataConn
    {
        public static string myConnection = "Server=localhost;Database=PMS;Integrated Security=SSPI";
        public static SqlConnection conn = new SqlConnection(myConnection);
    }
}
