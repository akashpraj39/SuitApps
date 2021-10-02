using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuitApps.Models.ModelClass;
using System.Data.SqlClient;
using System.ServiceModel.Web;
using SuitApps.Models.ModelClass;
using System.Data;
using System.Net.Http;
using System.Net;
using System.Text.RegularExpressions;

namespace SuitApps.Models.Repository
{
    public class EmpDistributorRepository
    {
        #region GetDistributor
        public static EmpDisList GetDistributor_RootWise(EmpDistributor empdis)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            //CustomerList customerList = new CustomerList();
            List<EmpDistributor> empdislist = new List<EmpDistributor>();
            EmpDisList empDis = new EmpDisList();

            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetDistributor_RootWise ", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object
                cmd.Parameters.AddWithValue("@EmpID", empdis.EmpID);
                cmd.Parameters.AddWithValue("@CompanyID", empdis.CompanyID);
                //cmd.Parameters.AddWithValue("@RootID", empRoute.RootId);
                //cmd.Parameters.AddWithValue("@Day", empRoute.Day);
                // con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        EmpDistributor empdist = new EmpDistributor();
                        empdist.DistID = reader["DistID"].ToString();
                        empdist.EmpID = reader["EmpID"].ToString();
                        empdist.Distributor = reader["Distributor"].ToString();
                        empdist.DistributorID = reader["DistributorID"].ToString();
                        empdist.CompanyID = reader["CompanyID"].ToString();
                        empdist.Status = reader["Status"].ToString();
                        empdislist.Add(empdist);

                    }
                }
                empDis.empdislist = empdislist;


                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return empDis;
            }
            return empDis;
        }
        #endregion
    }
}