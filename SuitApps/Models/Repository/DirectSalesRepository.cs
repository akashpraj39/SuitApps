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
    public class DirectSalesRepository
    {
        #region DirectSaleIDAutogenerate
        public static DirectSales DirectSaleIDAutogenerate()
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            DirectSales directSales = new DirectSales();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("DirectSaleIDAutogenerate", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // con.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                           directSales.DSID = reader["DSID"].ToString();
                    }
                }
                reader.Close();

                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return directSales;
            }
            return directSales;
        }
        #endregion

        #region DirectSalesListing_ordered
        public static DirectSalesForCustomerList DirectSalesListing_ordered(EmployeeRoute emp)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            DirectSalesForCustomerList saleForCustomerList = new DirectSalesForCustomerList();
            List<DirectSalesForCustomer> sales = new List<DirectSalesForCustomer>();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("DirectSaleslisting", con);
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
                        DirectSalesForCustomer sale = new DirectSalesForCustomer();
                        //-----------SALEORDER-------------------------------
                        sale.DSID = reader["DSID"].ToString();
                        sale.OrderNo = reader["OrderNo"].ToString();
                        sale.OrderStatus = reader["OrderStatus"].ToString();
                        sale.UserID = reader["UserID"].ToString();
                        sale.TotAmo = reader["TotAmo"].ToString();
                        sale.CompanyID = reader["CompanyID"].ToString();
                        sale.CustomerID = reader["CustomerID"].ToString();
                        sale.Discount = reader["Discount"].ToString();
                        sale.Discount_rate = reader["Discount_rate"].ToString();
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
                saleForCustomerList.directSalesForCustomer = sales;
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

        #region InsertDirectSales_App
        public static DirectSales InsertDirectSales_App(DirectSales saleOrder)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse;
            DirectSales sale = new DirectSales();
            try
            {
                int saleorderid=Convert.ToInt32(saleOrder.DSID);
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("InsertDirectSales_App", con);

                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object
                cmd.Parameters.AddWithValue("@DSID", saleOrder.DSID);
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
                cmd.Parameters.AddWithValue("@Discount ", saleOrder.Discount);
                cmd.Parameters.AddWithValue("@Discount_rate ", saleOrder.Discount_rate);
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
                cmd.Parameters.AddWithValue("@Customer_SuitAppsId", saleOrder.Customer_SuitAppsId);
                cmd.Parameters.AddWithValue("@BillNo ", saleOrder.BillNo);
                cmd.Parameters.AddWithValue("@Bill_Series ", saleOrder.Bill_Series);
                cmd.Parameters.AddWithValue("@BillMode ", saleOrder.BillMode);
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
                        sale.DSID = saleOrder.DSID;
                        string Soid = sale.DSID;
                        //-----------delete item purchase before updation------------------
                        bool status=DeleteDirectSalesDetails_before_updation(Soid);
                    }
                    else
                    {
                        sale.Message = "Inserted";
                        sale.DSID = value.ToString();
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


        #region DirectSalesByOrderNo
        public static DirectSalesForCustomer DirectSalesByOrderNo(string OrderNo)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            DirectSalesForCustomer sale = new DirectSalesForCustomer();
                        
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("DirectSalesByOrderNo", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Orderno", OrderNo);
                // con.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        sale.DSID = reader["DSID"].ToString();
                        sale.OrderNo = reader["OrderNo"].ToString();
                        sale.OrderStatus = reader["OrderStatus"].ToString();
                        sale.UserID = reader["UserID"].ToString();
                        sale.TotAmo = reader["TotAmo"].ToString();
                        sale.CompanyID = reader["CompanyID"].ToString();
                        sale.CustomerID = reader["CustomerID"].ToString();
                        sale.Discount = reader["Discount"].ToString();
                        sale.Discount_rate = reader["Discount_rate"].ToString();
                        sale.SuitApps_id = reader["SuitApps_id"].ToString();
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

        #region DirectSalesOfcustomer_ordered
        public static DirectSalesForCustomerList DirectSalesOfcustomer_ordered(int dsid)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            DirectSalesForCustomerList saleForCustomerList = new DirectSalesForCustomerList();
            List<DirectSalesForCustomer> sales = new List<DirectSalesForCustomer>();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetDirectSaleDetailsByID", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Dsid", dsid);
                // con.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        DirectSalesForCustomer sale = new DirectSalesForCustomer();
                        //-----------DIRECT SALES-------------------------------
                        sale.DSID = reader["DSID"].ToString();
                        sale.OrderNo = reader["OrderNo"].ToString();
                        sale.OrderStatus = reader["OrderStatus"].ToString();
                        sale.UserID = reader["UserID"].ToString();
                        sale.TotAmo = reader["TotAmo"].ToString();
                        sale.CompanyID = reader["CompanyID"].ToString();
                        sale.CustomerID = reader["CustomerID"].ToString();
                        sale.Discount = reader["Discount"].ToString();
                        sale.Discount_rate = reader["Discount_rate"].ToString();
                        //-----------------CUSTOMER---------------------------
                        sale.AccountID = reader["AccountID"].ToString();
                        sale.AccountCode = reader["AccountCode"].ToString();
                        sale.AccountName = reader["AccountName"].ToString();
                        sale.AccountType = reader["AccountType"].ToString();
                        sale.Address = reader["Address"].ToString();
                        sale.Phone = reader["Phone"].ToString();
                        sale.TinNo = reader["TinNo"].ToString();
                        //------------------itemsale----------------------
                        sale.DSDID = reader["Dsdid"].ToString();
                        sale.ItemID = reader["ItemId"].ToString();
                        sale.ItemName = reader["ItemName"].ToString();
                        sale.Qty = reader["Qty"].ToString();
                        sale.NetAmount = reader["NetAmount"].ToString();
                        sale.UnitPrice = reader["wp"].ToString();
                        sale.SuitApps_id = reader["SuitApps_id"].ToString();
                        sale.Tax_Rate = reader["Tax_Rate"].ToString();
                        sale.Tax_Amt = reader["Tax_Amt"].ToString();
                        sale.Rate = reader["Rate"].ToString();
                        sale.GrossValue = reader["GrossValue"].ToString();
                        sale.CGST_Rate = reader["CGST_Rate"].ToString();
                        sale.CGST_Amt = reader["CGST_Amt"].ToString();
                        sale.SGST_Rate = reader["SGST_Rate"].ToString();
                        sale.SGST_Amt = reader["SGST_Amt"].ToString();
                        sales.Add(sale);
                    }


                }
                saleForCustomerList.directSalesForCustomer = sales;
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


        #region DeleteDirectSalesDetails_before_updation
        public static bool DeleteDirectSalesDetails_before_updation(string Dsid)
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
                SqlCommand cmd = new SqlCommand("DeleteDirectSaleDetails_before_updation", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DSID", Dsid);
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