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
    public class LoginRepository
    {

        #region GetAllCompanyNames
        public static CompanyInfoList GetAllCompanyNames()
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            CompanyInfoList companyNameList = new CompanyInfoList();
            List<CompanyInfo> companies = new List<CompanyInfo>();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetCompanyInfo", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        CompanyInfo companyInfo = new CompanyInfo();
                        companyInfo.CompanyId = reader["CompanyId"].ToString();
                        companyInfo.CompanyName = reader["CompanyName"].ToString();
                        companyInfo.CompanyCode = reader["CompanyCode"].ToString();
                        companyInfo.Address = Regex.Replace(reader["Address"].ToString(), @"\t|\n|\r", ""); ;
                        companyInfo.TelephoneNo = reader["TelephoneNo"].ToString();
                        companyInfo.MobileNo = reader["MobileNO"].ToString();
                        companyInfo.City = reader["Citty"].ToString();
                        companyInfo.State = reader["State"].ToString();
                        companyInfo.District = reader["District"].ToString();
                        companyInfo.GSTinNo = reader["GSTinNo"].ToString();
                        companies.Add(companyInfo);
                    }
                }

                companyNameList.companyLists = companies;
                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return companyNameList;
            }
            return companyNameList;
        }
        #endregion

        #region Login
        public static LoginResponse Login(LoginRequest loginRequest)
        {
            SqlConnection con;
            SqlDataReader reader;
            HttpResponseMessage httpresponse;
            LoginResponse loginResponse = new LoginResponse();

            try
            {
                con = DBconnect.getDataBaseConnection();
                if (loginRequest.CompanyId == null || loginRequest.UserName == null || loginRequest.Password == null)
                {
                    httpresponse =  new HttpResponseMessage(HttpStatusCode.BadRequest);
                    loginResponse.Message = "Invalid request..!";
                    return loginResponse;
                }

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("AppLogin", con);

                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object
                cmd.Parameters.AddWithValue("@CompanyId", loginRequest.CompanyId);
                cmd.Parameters.AddWithValue("@Username", loginRequest.UserName);
                cmd.Parameters.AddWithValue("@Password", loginRequest.Password);
                cmd.Parameters.AddWithValue("@MacID", loginRequest.MacId);
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        {
                            loginResponse.UserId = reader["UserId"].ToString();
                            loginResponse.Name = reader["Name"].ToString();
                            loginResponse.CompanyId = reader["CompanyID"].ToString();
                            loginResponse.UserRoleId = reader["UserRoleId"].ToString();
                            loginResponse.Username = reader["UserName"].ToString();
                            loginResponse.Password = reader["Password"].ToString();
                           // loginResponse.DS_BillNo = DirectSaleBillNo(loginRequest.CompanyId);
                            //loginResponse.DS_BillNoSeries = reader["BillSeries"].ToString();
                            //loginResponse.DS_BillNo = reader["LastBillNo"].ToString();
                            loginResponse.VAN = reader["VAN"].ToString();
                            loginResponse.MAC = reader["MAC"].ToString();
                            loginResponse.DS_B2CBillNoSeries = reader["B2CSeries"].ToString();
                            loginResponse.DS_B2BBillNoSeries = reader["B2BSeries"].ToString();
                            loginResponse.DS_B2CBillNo = reader["B2CLastBillNo"].ToString();
                            loginResponse.DS_B2BBillNo = reader["B2BLastBillNo"].ToString();
                        }
                        loginResponse.Message = "User login successfull";
                        reader.Close();
                    }

                    else
                    {
                        httpresponse = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        loginResponse = new LoginResponse();
                        loginResponse.Message = "Invalid user credentials..!";
                    }
            }

            catch (Exception e)
            {
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                loginResponse.Message = e.Message.ToString();
                return loginResponse;
            }
            con.Close();
            return loginResponse;
        } 
        #endregion

        #region GetEmployeeRoute_DayWise
        public static EmployeeRoute GetEmployeeRoute_DayWise(EmployeeRoute employeeRoute)
        {
            SqlConnection con;
            SqlDataReader reader;
            HttpResponseMessage httpresponse;
            EmployeeRoute empRoute = new EmployeeRoute();
            try
            {

                con = DBconnect.getDataBaseConnection();
                
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetEmployeeRoute_DayWise", con);

                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object
                cmd.Parameters.AddWithValue("@CompanyId", employeeRoute.CompanyId);
                cmd.Parameters.AddWithValue("@EmpId", employeeRoute.EmpId);
                cmd.Parameters.AddWithValue("@Day", employeeRoute.Day);
                //cmd.Parameters.AddWithValue("@Target", employeeRoute.Target);

                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        empRoute.EmpId = reader["EmpId"].ToString();
                        empRoute.CompanyId = reader["CompanyId"].ToString();
                        empRoute.RootId = reader["RootId"].ToString();
                        empRoute.Target = reader["Target"].ToString();
                        empRoute.EmpName = reader["Name"].ToString();
                        empRoute.Mobile = reader["Mobile"].ToString();
                    }
                    reader.Close();
                }

                else
                {
                    httpresponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                }
            }

            catch (Exception e)
            {
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return empRoute;
            }
            con.Close();
            return empRoute;
        }
        #endregion

        #region GetEmployeeRoute_DayWise
        public static RouteList GetEmployee_MultiRoute_DayWise(EmployeeRoute employeeRoute)
        {
            SqlConnection con;
            SqlDataReader reader;
            HttpResponseMessage httpresponse;
            List<EmployeeRoute> routelist = new List<EmployeeRoute>();
            RouteList routes = new RouteList();
            try
            {

                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetEmployeeRoute_DayWise", con);

                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

             //Add the input parameters to the command object
                cmd.Parameters.AddWithValue("@CompanyId", employeeRoute.CompanyId);
                cmd.Parameters.AddWithValue("@EmpId", employeeRoute.EmpId);
                cmd.Parameters.AddWithValue("@Day", employeeRoute.Day);
                //cmd.Parameters.AddWithValue("@Target", employeeRoute.Target);

                /*       //Add the output parameter to the command object
                    cmd.Parameters.Add("@BillNo", SqlDbType.Int, 20);
                    cmd.Parameters["@BillNo"].Direction = ParameterDirection.Output;
                 */ 
                reader = cmd.ExecuteReader();
             //   string value = cmd.Parameters["@BillNo"].Value.ToString();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {

                        EmployeeRoute empRoute = new EmployeeRoute(); 
                        empRoute.EmpId = reader["EmpId"].ToString();
                        empRoute.CompanyId = reader["CompanyId"].ToString();
                        empRoute.RootId = reader["RootId"].ToString();
                        empRoute.RootName = reader["RootName"].ToString();
                        empRoute.Target = reader["Target"].ToString();
                        empRoute.State = reader["StateName"].ToString();
                        empRoute.StateID = reader["StateID"].ToString();
                        empRoute.Day = employeeRoute.Day;
                        routelist.Add(empRoute);
                    }
                    routes.routeList = routelist;
                    
                    reader.Close();

                }

                else
                {
                    httpresponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                }
            }

            catch (Exception e)
            {
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return routes;
            }
            con.Close();
            return routes;
        }
        #endregion


        #region DirectSaleBillNo
        public static string DirectSaleBillNo(string CompanyId)
        {
            string value = "0";
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            //  DirectSales directSales = new DirectSales();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetBillNo_App", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyID", CompanyId);
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        value = reader["BillNo"].ToString(); 
                    }
                }
                //     directSales.BillNo = value;

                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return value;
            }
            return value;
        }
        #endregion

    }
}