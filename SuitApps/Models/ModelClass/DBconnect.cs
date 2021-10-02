using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class DBconnect
    { 
        public static SqlConnection getDataBaseConnection()
        {
           
            string connectionString = "";
        
            SqlConnection con = new SqlConnection(connectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }
    }
}