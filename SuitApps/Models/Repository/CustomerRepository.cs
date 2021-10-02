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
    public class CustomerRepository
    {
        #region GetAllCustomers_Root_Day_Wise
        /// <summary>
        /// Get all customers in a single route
        /// </summary>
        /// <param name="empRoute"></param>
        /// <returns></returns>
        public static CustomerList GetAllCustomers_Root_Day_Wise(EmployeeRoute empRoute)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            CustomerList customerList = new CustomerList();
            List<Customer> customers = new List<Customer>();

            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetAllCustomers_Root_Day_Wise", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object
                cmd.Parameters.AddWithValue("@EmpID", empRoute.EmpId);
                cmd.Parameters.AddWithValue("@CompanyID", empRoute.CompanyId);
                cmd.Parameters.AddWithValue("@RootID", empRoute.RootId);
                cmd.Parameters.AddWithValue("@Day", empRoute.Day);
                // con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.AccountID = reader["AccountID"].ToString();
                        customer.AccountCode = reader["AccountCode"].ToString();
                        customer.AccountGroup = reader["AccountGroup"].ToString();
                        customer.AccountParent = reader["AccountParent"].ToString();
                        customer.AccountName = reader["AccountName"].ToString();
                        customer.AccountType = reader["AccountType"].ToString();
                        customer.Address = reader["Address"].ToString();
                        customer.city = reader["city"].ToString();
                        customer.Email = reader["Email"].ToString();
                        customer.Mob = reader["Mobile"].ToString();
                        customer.Companyid = reader["Companyid"].ToString();
                        customer.RootId = reader["RootId"].ToString();
                        customer.Statenew = reader["State"].ToString();
                        customer.Districtnew = reader["District"].ToString();
                        customer.Taluk = reader["Taluk"].ToString();
                        customer.Distributor = reader["DistributorID"].ToString();
                        customers.Add(customer);
                    }
                }
                customerList.customerList = customers;

                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return customerList;
            }
            return customerList;
        }
        #endregion

        #region GetAllCustomers_MultiRoot_Day_Wise
        /// <summary>
        /// get all customers in multiple route
        /// </summary>
        /// <param name="empRouteList"></param>
        /// <returns></returns>
        public static CustomerList GetAllCustomers_MultiRoot_Day_Wise(RouteList empRouteList)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            CustomerList customerList = new CustomerList();
            List<Customer> customers = new List<Customer>();

            try
            {
                con = DBconnect.getDataBaseConnection();
                foreach (EmployeeRoute route in empRouteList.routeList)
                {

                    //Create the SqlCommand object
                    SqlCommand cmd = new SqlCommand("GetAllCustomers_Root_Day_Wise", con);
                    //Specify that the SqlCommand is a stored procedure
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Add the input parameters to the command object
                    cmd.Parameters.AddWithValue("@EmpID", route.EmpId);
                    cmd.Parameters.AddWithValue("@CompanyID", route.CompanyId);
                    cmd.Parameters.AddWithValue("@RootID", route.RootId);
                    cmd.Parameters.AddWithValue("@Day", route.Day);
                    // con.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer();
                            customer.AccountID = reader["AccountID"].ToString();
                            customer.AccountCode = reader["AccountCode"].ToString();
                            customer.AccountGroup = reader["AccountGroup"].ToString();
                            customer.AccountParent = reader["AccountParent"].ToString();
                            customer.AccountName = reader["AccountName"].ToString();
                            customer.AccountType = reader["AccountType"].ToString();
                            customer.Address = reader["Address"].ToString();
                            customer.city = reader["city"].ToString();
                            customer.Email = reader["Email"].ToString();
                            customer.Mob = reader["Mobile"].ToString();
                            customer.Phone = reader["Phone"].ToString();
                            customer.Companyid = reader["Companyid"].ToString();
                            customer.RootId = reader["RootId"].ToString();
                            customer.GSTinNo = reader["GSTinNo"].ToString();
                            customer.Distributor = reader["DistributorID"].ToString();
                            customer.Statenew = reader["State"].ToString();
                            customer.Districtnew = reader["District"].ToString();
                            customer.Taluk = reader["Taluk"].ToString();
                            customer.Latitude = reader["Latitude"].ToString();
                            customer.Longitude = reader["Longitude"].ToString();
                            customer.LocAddress = reader["LocAddress"].ToString();
                            customers.Add(customer);
                        }
                    }
                    customerList.customerList = customers;
                    reader.Close();
                }

                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return customerList;
            }
            return customerList;
        }
        #endregion

        #region SearchCustomer
        /// <summary>
        /// search from hosted DB
        /// </summary>
        /// <param name="empRoute"></param>
        /// <returns></returns>
        public static CustomerList SearchCustomer(EmployeeRoute empRoute)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            CustomerList customerList = new CustomerList();
            List<Customer> customers = new List<Customer>();

            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("SearchCustomer", con); 
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object
                cmd.Parameters.AddWithValue("@EmpID", empRoute.EmpId);
                cmd.Parameters.AddWithValue("@CompanyID", empRoute.CompanyId);
                cmd.Parameters.AddWithValue("@RootID", empRoute.RootId);
                cmd.Parameters.AddWithValue("@Day", empRoute.Day);
                cmd.Parameters.AddWithValue("@Name", empRoute.CustomerName);
                // con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.AccountID = reader["AccountID"].ToString();
                        customer.AccountCode = reader["AccountCode"].ToString();
                        customer.AccountGroup = reader["AccountGroup"].ToString();
                        customer.AccountParent = reader["AccountParent"].ToString();
                        customer.AccountName = reader["AccountName"].ToString();
                        customer.AccountType = reader["AccountType"].ToString();
                        customer.Address = reader["Address"].ToString();
                        customer.city = reader["city"].ToString();
                        customer.Email = reader["Email"].ToString();
                        customer.Mob = reader["Mobile"].ToString();
                        customer.Companyid = reader["Companyid"].ToString();
                        customer.RootId = reader["RootId"].ToString();
                      //  customer.Distributor = reader["Distributor"].ToString();
                        customers.Add(customer);
                    }
                }
                customerList.customerList = customers;

                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return customerList;
            }
            return customerList;
        }
        #endregion

        #region PaginationForCustomer
        public static CustomerList PaginationForCustomer(EmployeeRoute empRoute)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            CustomerList customerList = new CustomerList();
            List<Customer> customers = new List<Customer>();

            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("PaginationForCustomer", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object
                cmd.Parameters.AddWithValue("@EmpID", empRoute.EmpId);
                cmd.Parameters.AddWithValue("@CompanyID", empRoute.CompanyId);
                cmd.Parameters.AddWithValue("@RootID", empRoute.RootId);
                cmd.Parameters.AddWithValue("@Day", empRoute.Day);
                cmd.Parameters.AddWithValue("@pageno", empRoute.PageNo);
                // con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.AccountID = reader["AccountID"].ToString();
                        customer.AccountCode = reader["AccountCode"].ToString();
                        customer.AccountGroup = reader["AccountGroup"].ToString();
                        customer.AccountParent = reader["AccountParent"].ToString();
                        customer.AccountName = reader["AccountName"].ToString();
                        customer.AccountType = reader["AccountType"].ToString();
                        customer.Address = reader["Address"].ToString();
                        customer.city = reader["city"].ToString();
                        customer.Email = reader["Email"].ToString();
                        customer.Mob = reader["Mobile"].ToString();
                        customer.Companyid = reader["Companyid"].ToString();
                        customer.RootId = reader["RootId"].ToString();
                        customers.Add(customer);
                    }
                }
                customerList.customerList = customers;

                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return customerList;
            }
            return customerList;
        }
        #endregion

        #region GetCustomerDetailById
        public static Customer GetCustomerDetailById(Customer customerDetail)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            Customer customer = new Customer();

            Product product = new Product();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetCustomerByID", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@companyID", customerDetail.Companyid);
                cmd.Parameters.AddWithValue("@AccountID", customerDetail.AccountID);
                // con.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        customer.AccountID = reader["AccountID"].ToString();
                        customer.AccountCode = reader["AccountCode"].ToString();
                        customer.AccountGroup = reader["AccountGroup"].ToString();
                        customer.AccountParent = reader["AccountParent"].ToString();
                        customer.AccountName = reader["AccountName"].ToString();
                        customer.AccountType = reader["AccountType"].ToString();
                        customer.Address = reader["Address"].ToString();
                        customer.city = reader["city"].ToString();
                        customer.Email = reader["Email"].ToString();
                        customer.Mob = reader["Mobile"].ToString();
                        customer.Companyid = reader["Companyid"].ToString();
                        customer.RootId = reader["RootId"].ToString();
                        // products.Add(product);
                    }


                }
                //productList.productList = products;
                reader.Close();

                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return customer;
            }
            return customer;
        }
        #endregion
        
        #region ManageCustomer
        public static Account ManageCustomer(Account newAccount)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse;
            AcHead acHead = new AcHead();
            Account account = new Account();
            try
            {
                //acHead.AccountCode = InsertUpdateAcHead(newAccount);
                account = InsertUpdateCustomer(newAccount, "0");
            }

            catch (Exception e)
            {
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return account;
            }
            return account;
        }
        
        #endregion
        
        #region InsertUpdateCustomer
        public static Account InsertUpdateCustomer(Account newAccount, string AcHeadNo)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse;
            Account account = new Account();
            try
            {
                int accountId = Convert.ToInt32(newAccount.AccountId);
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("InsertUpdateAccout_App", con);

                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccountID", newAccount.AccountId);
                cmd.Parameters.AddWithValue("@AccountName", newAccount.AccountName);
                cmd.Parameters.AddWithValue("@Address", newAccount.Address);
                cmd.Parameters.AddWithValue("@country", newAccount.country);
                cmd.Parameters.AddWithValue("@mob", newAccount.Mob);
                cmd.Parameters.AddWithValue("@phone", newAccount.Phone);
                cmd.Parameters.AddWithValue("@District", newAccount.District);
                cmd.Parameters.AddWithValue("@State", newAccount.State);
                cmd.Parameters.AddWithValue("@Pincode", newAccount.Pincode);
                cmd.Parameters.AddWithValue("@Companyid", newAccount.Companyid);
                cmd.Parameters.AddWithValue("@isdeleted", 0);
                cmd.Parameters.AddWithValue("@CreatedBy", newAccount.UserId);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("M/d/yyyy"));
                if (accountId > 0)
                {
                    cmd.Parameters.AddWithValue("@ModifiedBy", newAccount.UserId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ModifiedBy", 0);
                }

                cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now.ToString("M/d/yyyy"));
                cmd.Parameters.AddWithValue("@DeletedBy", 0);
                cmd.Parameters.AddWithValue("@DeletedDate", DateTime.Now.ToString("M/d/yyyy"));
                cmd.Parameters.AddWithValue("@Place", newAccount.Place);
                cmd.Parameters.AddWithValue("@GSTinNo", newAccount.GSTinNo);
                cmd.Parameters.AddWithValue("@AdharNo", newAccount.Adhar);
                cmd.Parameters.AddWithValue("@Customer_SuitAppsId", newAccount.Customer_SuitAppsId);
                //cmd.Parameters.AddWithValue("@State", newAccount.Statenew);
               // cmd.Parameters.AddWithValue("@District", newAccount.Districtnew);
                cmd.Parameters.AddWithValue("@Taluk", newAccount.Taluk);
                cmd.Parameters.AddWithValue("@Distrubutor", newAccount.Distributor);
                cmd.Parameters.AddWithValue("@DistrubutorID", newAccount.Distributor);
              
                
                //Add the output parameter to the command object
                cmd.Parameters.Add("@OutCustomer_SuitAppsId", SqlDbType.NVarChar, 50);
                cmd.Parameters["@OutCustomer_SuitAppsId"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int value = (int)cmd.Parameters["@outId"].Value;
                string OutCustomer_SuitAppsId = cmd.Parameters["@OutCustomer_SuitAppsId"].Value.ToString();
                if (value != 0)
                {
                    if (value == -1)
                    {
                        account.Message = "Updated";
                        account.AccountId = newAccount.AccountId;
                        account.Customer_SuitAppsId = OutCustomer_SuitAppsId;
                    }
                    else
                    {
                        account.Message = "Inserted";
                        account.AccountId = value.ToString();
                        account.Customer_SuitAppsId = OutCustomer_SuitAppsId;
                    }
                }
                else
                {
                    httpresponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                    account.Message = "Failed";
                }
                //-------UPDATE-------------- 

            }

            catch (Exception e)
            {
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return account;
            }
            con.Close();
            return account;
        }
        #endregion


        #region InsertUpdateAcHead
        public static String InsertUpdateAcHead(Account newAccount)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse;
            AcHead acHead = new AcHead();
            try
            {
                int AcHeadNo = Convert.ToInt32(newAccount.AcHeadNo);
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("InsertUpdateBankAcHead_App", con);

                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object
                cmd.Parameters.AddWithValue("@AcHeadId", AcHeadNo);
                cmd.Parameters.AddWithValue("@AcHeadName", newAccount.AccountName);
                cmd.Parameters.AddWithValue("@GroupId", 14);
                cmd.Parameters.AddWithValue("@UserId", newAccount.UserId);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("M/d/yyyy"));
                cmd.Parameters.AddWithValue("@Type", "P");
                //Add the output parameter to the command object
                cmd.Parameters.Add("@OutId", SqlDbType.Int, 20);
                cmd.Parameters["@OutId"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int value = (int)cmd.Parameters["@OutId"].Value;
                if (value != 0)
                {
                    if (value == -1)
                    {
                        acHead.Message = "Updated";
                        acHead.AccountCode = newAccount.AcHeadNo;
                    }
                    else
                    {
                        acHead.Message = "Inserted";
                        acHead.AccountCode = value.ToString();
                    }
                }
                else
                {
                    httpresponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                    acHead.Message = "Failed";
                }
                //-------UPDATE-------------- 

            }

            catch (Exception e)
            {
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return acHead.AccountCode;
            }
            con.Close();
            return acHead.AccountCode;
        }
        #endregion
      



    }
}