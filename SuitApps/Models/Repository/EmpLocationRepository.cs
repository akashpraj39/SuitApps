using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Web;
using System.Web;
using SuitApps.Models.ModelClass;
namespace SuitApps.Models.Repository
{
    public class EmpLocationRepository
    {
        public static EmpLocationList InsertEmpLocation(EmpLocationList emplocList)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse;
            EmpLocationList result = new EmpLocationList();
            List<EmpLocation> emplist = new List<EmpLocation>();
           // EmpLocation empl = new EmpLocation();
            try
            {

                con = DBconnect.getDataBaseConnection();
                foreach (EmpLocation empl in emplocList.emplocationList)
                {
                    int emplocid = Convert.ToInt32(empl.EmpLocID);


                    SqlCommand cmd = new SqlCommand("InsertEmpLocation", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpLocID", empl.EmpLocID);
                    cmd.Parameters.AddWithValue("@EmpID", empl.EmpID);
                    cmd.Parameters.AddWithValue("@Date",empl.Date);
                    cmd.Parameters.AddWithValue("@Place", empl.Place);
                    cmd.Parameters.AddWithValue("@Latitude", empl.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", empl.Longitude);
                    cmd.ExecuteNonQuery();
                    empl.Message = "Inserted";
                 // EmpLocation emplolist = new EmpLocation();
                   // emplolist.Message = "Inserted";
                  emplist.Add(empl);
                }
                result.emplocationList = emplist;

            }
            catch(Exception e)
            {
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return result;
            }
            con.Close();
          //  return empl;
            return result;
        }

    }
}