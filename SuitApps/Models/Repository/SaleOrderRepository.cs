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
    public class SaleOrderRepository
    {
        #region SaleOrderAutogenerate
        public static SaleOrder SaleOrderAutogenerate()
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;            
            SaleOrder saleOrder = new SaleOrder();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("SaleOrderAutogenerate", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // con.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                           saleOrder.SOID = reader["SOID"].ToString();
                    }
                }
                reader.Close();

                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return saleOrder;
            }
            return saleOrder;
        }
        #endregion

        #region SaleOrderListing_ordered
        public static SaleForCustomerList SaleOrderListing_ordered(EmployeeRoute emp)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            SaleForCustomerList saleForCustomerList = new SaleForCustomerList();
            List<SaleForCustomer> sales = new List<SaleForCustomer>();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("Saleorderlisting", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyID", emp.CompanyId);
                cmd.Parameters.AddWithValue("@UserID", emp.EmpId);
                // con.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        SaleForCustomer sale = new SaleForCustomer();
                        //-----------SALEORDER-------------------------------
                        sale.SOID = reader["SOID"].ToString();
                        sale.OrderNo = reader["OrderNo"].ToString();
                        sale.OrderStatus = reader["OrderStatus"].ToString();
                        sale.UserID = reader["UserID"].ToString();
                        sale.TotAmo = reader["TotAmo"].ToString();
                        sale.CompanyID = reader["CompanyID"].ToString();
                        sale.CustomerID = reader["CustomerID"].ToString();
                        sale.SuitApps_id = reader["SuitApps_id"].ToString();
                        //-----------------CUSTOMER---------------------------
                        sale.AccountID = reader["AccountID"].ToString();
                        sale.AccountCode = reader["AccountCode"].ToString();
                        sale.AccountName = reader["AccountName"].ToString();
                        sale.AccountType = reader["AccountType"].ToString();
                        sale.Address = reader["Address"].ToString();
                        sale.Phone = reader["Phone"].ToString();
                        sale.TinNo = reader["TinNo"].ToString();
                        sales.Add(sale);
                    }


                }
        /*        else
                {
                    saleForCustomerList.message = "No orders";
                }
         * */
                saleForCustomerList.saleForCustomerList = sales;
                reader.Close();

                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return saleForCustomerList;
            }
            return saleForCustomerList;
        }
        #endregion

        #region InsertSaleOrder_App
        public static SaleOrder InsertSaleOrder_App(SaleOrder saleOrder)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse;
            SaleOrder sale = new SaleOrder();
            try
            {
                int saleorderid=Convert.ToInt32(saleOrder.SOID);
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("InsertSaleOrder_App", con);

                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object
                cmd.Parameters.AddWithValue("@SOID", saleOrder.SOID);
                cmd.Parameters.AddWithValue("@Date",DateTime.Now.ToString("M/d/yyyy"));
                cmd.Parameters.AddWithValue("@OrderNo ",saleOrder.OrderNo);
                cmd.Parameters.AddWithValue("@CustomerID", saleOrder.CustomerID);
                cmd.Parameters.AddWithValue("@UserID", saleOrder.UserID);
                cmd.Parameters.AddWithValue("@Amount", saleOrder.Amount);
                cmd.Parameters.AddWithValue("@CompanyID", saleOrder.CompanyID);
                cmd.Parameters.AddWithValue("@AdvanceAmo",0);
                cmd.Parameters.AddWithValue("@TotAmo", saleOrder.Amount);
                cmd.Parameters.AddWithValue("@OrderStatus",saleOrder.OrderStatus );
                cmd.Parameters.AddWithValue("@CreatedBy",saleOrder.UserID );
                cmd.Parameters.AddWithValue("@CreatedDate",DateTime.Now.ToString("M/d/yyyy")) ;
                cmd.Parameters.AddWithValue("@SuitApps_id", saleOrder.SuitApps_id);

                if (saleorderid > 0)
                {
                    cmd.Parameters.AddWithValue("@ModifiedBy", saleOrder.UserID);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ModifiedBy",0);
                }
                cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now.ToString("M/d/yyyy"));
                cmd.Parameters.AddWithValue("@DeletedBy",0 );
                cmd.Parameters.AddWithValue("@DeletedDate",0 );
                cmd.Parameters.AddWithValue("@IsDeleted", 0);
//Add the output parameter to the command object
                cmd.Parameters.Add("@OutId", SqlDbType.Int, 20);
                cmd.Parameters["@OutId"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int value = (int)cmd.Parameters["@OutId"].Value;
                if (value != 0)
                {
                    if (value == -1)
                    {
                        sale.Message = "Updated";
                        sale.SOID = saleOrder.SOID;
                        string Soid = sale.SOID;
                        bool status=DeleteSaleDetails_before_updation(Soid);
                    }
                    else
                    {
                        sale.Message = "Inserted";
                        sale.SOID = value.ToString();
                    }
                }
                else
                {
                    httpresponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                    sale.Message = "Failed";
                }
                //-------UPDATE-------------- 
               
            }

            catch (Exception e)
            {
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return sale;
            }
            con.Close();
            return sale;
        }
        #endregion
        
        #region SaleOrderByOrderNo
        public static SaleForCustomer SaleOrderByOrderNo(string OrderNo)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current; 
            SaleForCustomer sale = new SaleForCustomer();
                        
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("SaleOrderByOrderNo", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Orderno", OrderNo);
                // con.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        sale.SOID = reader["SOID"].ToString();
                        sale.OrderNo = reader["OrderNo"].ToString();
                        sale.OrderStatus = reader["OrderStatus"].ToString();
                        sale.UserID = reader["UserID"].ToString();
                        sale.TotAmo = reader["TotAmo"].ToString();
                        sale.CompanyID = reader["CompanyID"].ToString();
                        sale.CustomerID = reader["CustomerID"].ToString();
                        sale.AccountID = reader["AccountID"].ToString();
                        sale.AccountCode = reader["AccountCode"].ToString();
                        sale.AccountName = reader["AccountName"].ToString();
                        sale.AccountType = reader["AccountType"].ToString();
                        sale.Address = reader["Address"].ToString();
                        sale.Phone = reader["Phone"].ToString();
                        sale.TinNo = reader["TinNo"].ToString();
                    }
                }
                reader.Close();

                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return sale;
            }
            return sale;
        }
        #endregion

        #region SaleOrderOfcustomer_ordered
        public static SaleForCustomerList SaleOrderOfcustomer_ordered(int soid)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            SaleForCustomerList saleForCustomerList = new SaleForCustomerList();
            List<SaleForCustomer> sales = new List<SaleForCustomer>();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetSAleDetailsByID", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Soid", soid);
                // con.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        SaleForCustomer sale = new SaleForCustomer();
                        //-----------SALEORDER-------------------------------
                        sale.SOID = reader["SOID"].ToString();
                        sale.OrderNo = reader["OrderNo"].ToString();
                        sale.OrderStatus = reader["OrderStatus"].ToString();
                        sale.UserID = reader["UserID"].ToString();
                        sale.TotAmo = reader["TotAmo"].ToString();
                        sale.CompanyID = reader["CompanyID"].ToString();
                        sale.CustomerID = reader["CustomerID"].ToString();
                        //-----------------CUSTOMER---------------------------
                        sale.AccountID = reader["AccountID"].ToString();
                        sale.AccountCode = reader["AccountCode"].ToString();
                        sale.AccountName = reader["AccountName"].ToString();
                        sale.AccountType = reader["AccountType"].ToString();
                        sale.Address = reader["Address"].ToString();
                        sale.Phone = reader["Phone"].ToString();
                        sale.TinNo = reader["TinNo"].ToString();
                        //------------------itemsale----------------------
                        sale.SODID = reader["Sodid"].ToString();
                        sale.ItemID = reader["ItemId"].ToString();
                        sale.ItemName = reader["ItemName"].ToString();
                        sale.Qty = reader["Qty"].ToString();
                        sale.NetAmount = reader["NetAmount"].ToString();
                        sale.UnitPrice = reader["wp"].ToString();
                        sale.SuitApps_id = reader["SuitApps_id"].ToString();
                        sales.Add(sale);
                    }


                }
                saleForCustomerList.saleForCustomerList = sales;
                reader.Close();

                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return saleForCustomerList;
            }
            return saleForCustomerList;
        }
        #endregion

        #region DeleteSaleDetails_before_updation
        public static bool DeleteSaleDetails_before_updation(string soid)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            SaleForCustomer sale = new SaleForCustomer();
            bool status = false;

            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("DeleteSaleDetails_before_updation", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SOID", soid);
                // con.Open();
                reader = cmd.ExecuteReader();

                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return status;
            }
            return status;
        }
        #endregion

    }
}