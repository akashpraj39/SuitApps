using SuitApps.Models.ModelClass;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;

namespace SuitApps.Models.Repository
{
    public class CountRepository
    {
        #region GetProductCount
        public static Count GetProductCount()
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;

            Count count = new Count();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetProductRowCount", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // con.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                     while (reader.Read())
                    {
                        count.count = reader["product_count"].ToString();
                    }


                }
                //productList.productList = products;
                reader.Close();

                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return count;
            }
            return count;
        }
        #endregion


    }
}