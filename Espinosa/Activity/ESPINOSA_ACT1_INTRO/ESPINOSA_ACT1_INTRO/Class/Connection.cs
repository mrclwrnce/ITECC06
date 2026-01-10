using System.Data.OleDb;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ESPINOSA_ACT1_INTRO.Class
{
    class Connection
    {
        static string dbPath = @"C:\LOCALDB\ACTIVITY1.mdb";
        public static String conString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";

        public static OleDbConnection mysqldb()
        {
            return new OleDbConnection(conString);

        }
        public static OleDbConnection con = mysqldb();
    }
}
