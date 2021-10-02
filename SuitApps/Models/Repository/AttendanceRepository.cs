using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Web;
using System.Web;
using SuitApps.Models.ModelClass;

namespace SuitApps.Models.Repository
{
    public class AttendanceRepository
    {
      
        public static AttendanceList Sync_Attendance(AttendanceList attendancelist)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse;
            AttendanceList result = new AttendanceList();
            List<Attendance> attlist = new List<Attendance>();
            try
            {

                con = DBconnect.getDataBaseConnection();
                foreach (Attendance atten in attendancelist.attendanceList)
                {
                    int attid = Convert.ToInt32(atten.Id);


                    SqlCommand cmd = new SqlCommand("InsertUpdateAttendance", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", atten.Id);
                    cmd.Parameters.AddWithValue("@Userid", atten.Userid);
                    cmd.Parameters.AddWithValue("@Date", atten.Date);
                    cmd.Parameters.AddWithValue("@InPlace", atten.InPlace);
                    cmd.Parameters.AddWithValue("@OutPlace", atten.OutPlace);
                    cmd.Parameters.AddWithValue("@InTime", atten.InTime);
                    cmd.Parameters.AddWithValue("@OutTime", atten.OutTime);
                    cmd.Parameters.AddWithValue("@InLatitude", atten.InLatitude);
                    cmd.Parameters.AddWithValue("@OutLatitude", atten.OutLatitude);
                    cmd.Parameters.AddWithValue("@InLongitude", atten.InLongitude);
                    cmd.Parameters.AddWithValue("@OutLongitude", atten.OutLongitude);
                    cmd.Parameters.AddWithValue("@CreatedBy", atten.CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("M/d/yyyy"));


                    if (attid > 0)
                    {
                        cmd.Parameters.AddWithValue("@ModifiedBy", atten.CreatedBy);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ModifiedBy", 0);
                    }
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now.ToString("M/d/yyyy"));
                    cmd.Parameters.AddWithValue("@DeletedBy", 0);
                    cmd.Parameters.AddWithValue("@DeletedDate", 0);
                    cmd.Parameters.AddWithValue("@IsDeletedBy", 0);

                    cmd.ExecuteNonQuery();
                    atten.Message = "Inserted";
                    attlist.Add(atten);
                }
                result.attendanceList = attlist;

            }
            catch (Exception e)
            {
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return result;
            }
            con.Close();
           
            return result;
        }

    }
}



