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
    public class Sync_DirectSalesReprository
    {
        #region Sync_DirectSales
        public static DirectSalesList Sync_DirectSalesData(DirectSalesList list)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse;
            DirectSalesList result = new DirectSalesList();
            List<DirectSales> sales = new List<DirectSales>();
            try
            {

                con = DBconnect.getDataBaseConnection();

                foreach (DirectSales saleOrder in list.directSalesList)
                {
                    int saleorderid = Convert.ToInt32(saleOrder.DSID);
                  DateTime BillDate = Convert.ToDateTime(saleOrder.BillDate);
                //  DateTime BillDate = DateTime.Parse(saleOrder.BillDate).ToString("yyyy/mm/dd", CultureInfo.InvariantCulture);
                    con = DBconnect.getDataBaseConnection();

                    //Create the SqlCommand object
                    SqlCommand cmd = new SqlCommand("Sync_DirectSales", con);

                    //Specify that the SqlCommand is a stored procedure
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Add the input parameters to the command object
                    cmd.Parameters.AddWithValue("@DSID", saleOrder.DSID);
                    cmd.Parameters.AddWithValue("@Date", BillDate.ToString("yyyy/MM/dd"));
                    cmd.Parameters.AddWithValue("@OrderNo ", saleOrder.OrderNo);
                    cmd.Parameters.AddWithValue("@CustomerID", saleOrder.CustomerID);
                    cmd.Parameters.AddWithValue("@UserID", saleOrder.UserID);
                    cmd.Parameters.AddWithValue("@Amount", saleOrder.Amount);
                    cmd.Parameters.AddWithValue("@CompanyID", saleOrder.CompanyID);
                    cmd.Parameters.AddWithValue("@AdvanceAmo", 0);
                    cmd.Parameters.AddWithValue("@TotAmo", saleOrder.Amount);
                    cmd.Parameters.AddWithValue("@OrderStatus", saleOrder.OrderStatus);
                    cmd.Parameters.AddWithValue("@CreatedBy", saleOrder.UserID);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("M/d/yyyy"));
                    cmd.Parameters.AddWithValue("@SuitApps_id", saleOrder.SuitApps_id);
                    cmd.Parameters.AddWithValue("@Discount", saleOrder.Discount);
                    cmd.Parameters.AddWithValue("@Discount_rate", saleOrder.Discount_rate);

                    if (saleorderid > 0)
                    {
                        cmd.Parameters.AddWithValue("@ModifiedBy", saleOrder.UserID);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ModifiedBy", 0);
                    }
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now.ToString("M/d/yyyy"));
                    cmd.Parameters.AddWithValue("@DeletedBy", 0);
                    cmd.Parameters.AddWithValue("@DeletedDate", 0);
                    cmd.Parameters.AddWithValue("@IsDeleted", 0);
                    cmd.Parameters.AddWithValue("@Customer_SuitAppsId", saleOrder.Customer_SuitAppsId);
                    cmd.Parameters.AddWithValue("@BillNo", saleOrder.BillNo);
                    cmd.Parameters.AddWithValue("@Bill_Series ", saleOrder.Bill_Series);
                    cmd.Parameters.AddWithValue("@BillMode", saleOrder.BillMode);
                    //Add the output parameter to the command object
                    cmd.Parameters.Add("@OutId", SqlDbType.Int, 20);
                    cmd.Parameters["@OutId"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@OutDSID", SqlDbType.Int, 20);
                    cmd.Parameters["@OutDSID"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@OutSuitApps_id", SqlDbType.NVarChar, 50);
                    cmd.Parameters["@OutSuitApps_id"].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@InvoiceType", saleOrder.InvoiceType);
                    cmd.ExecuteNonQuery();
                    int value = (int)cmd.Parameters["@OutId"].Value;
                    int OutDSID = (int)cmd.Parameters["@OutDSID"].Value;
                    string OutSuitApps_id = cmd.Parameters["@OutSuitApps_id"].Value.ToString();

                    DirectSales sale = new DirectSales(); 
                        if (value != 0)
                        {
                            if (value == -1)
                            {
                                sale.Message = "Updated";
                                sale.DSID = OutDSID.ToString();
                                sale.SuitApps_id = OutSuitApps_id;

                                bool status = Sync_DeleteDirectSalesPurchase_b4_updation(OutSuitApps_id);
                            }
                            else
                            {
                                sale.Message = "Inserted";
                                sale.DSID = OutDSID.ToString();
                                sale.SuitApps_id = OutSuitApps_id;
                            }
                        }
                        else
                        {
                            httpresponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                            sale.Message = "Failed";
                        }
                            //-------UPDATE-------------- 
                        sales.Add(sale);
                }
                result.directSalesList = sales;
            }

            catch (Exception e)
            {
                result.Message="EXCEPTION = "+e;
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return result;
            }
            con.Close();
            return result;
        }
        #endregion

        #region Sync_DeleteDirectSalesPurchase_b4_updation
        public static bool Sync_DeleteDirectSalesPurchase_b4_updation(string SuitApps_id)
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
                SqlCommand cmd = new SqlCommand("Sync_Delete_DirectSaledetails", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SuitApps_id", SuitApps_id);
                // con.Open();
                reader = cmd.ExecuteReader();
                status = true;
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

        #region Sync_DirectSalesDetails
        public static DirectSalesDetailList Sync_DirectSalesDetails(DirectSalesDetailList list)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse;
            DirectSalesDetailList result = new DirectSalesDetailList();
            List<DirectSalesDetail> sales = new List<DirectSalesDetail>();
            try
            {

                con = DBconnect.getDataBaseConnection();

                foreach (DirectSalesDetail saleOrderDetail in list.directSalesDetailList)
                {
                    SqlCommand cmd = new SqlCommand("Sync_DirectSaleDetails", con);
                    //Specify that the SqlCommand is a stored procedure
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //Add the input parameters to the command object
                    cmd.Parameters.AddWithValue("@DSID", saleOrderDetail.DSID);
                    cmd.Parameters.AddWithValue("@ItemID", saleOrderDetail.ItemID);
                    cmd.Parameters.AddWithValue("@Qty", saleOrderDetail.Qty);
                    cmd.Parameters.AddWithValue("@NetAmount", saleOrderDetail.NetAmount);
                    cmd.Parameters.AddWithValue("@SuitApps_id", saleOrderDetail.SuitApps_id);
                    cmd.Parameters.AddWithValue("@Tax_Rate", saleOrderDetail.Tax_Rate);
                    cmd.Parameters.AddWithValue("@Tax_Amt", saleOrderDetail.Tax_Amt);
                    cmd.Parameters.AddWithValue("@Rate", saleOrderDetail.Rate);
                    cmd.Parameters.AddWithValue("@MRP", saleOrderDetail.MRP);
                    cmd.Parameters.AddWithValue("@GrossValue", saleOrderDetail.GrossValue);
                    cmd.Parameters.AddWithValue("@CGST_Rate", saleOrderDetail.CGST_Rate);
                    cmd.Parameters.AddWithValue("@CGST_Amt", saleOrderDetail.CGST_Amt);
                    cmd.Parameters.AddWithValue("@SGST_Rate", saleOrderDetail.SGST_Rate);
                    cmd.Parameters.AddWithValue("@SGST_Amt", saleOrderDetail.SGST_Amt);
                    cmd.Parameters.AddWithValue("@FreeQuantity", saleOrderDetail.FreeQuantity);
                    cmd.Parameters.AddWithValue("@disptg", saleOrderDetail.disptg);
                    cmd.Parameters.AddWithValue("@disamt", saleOrderDetail.disamt);
                    cmd.Parameters.AddWithValue("@FCessRate", saleOrderDetail.FCessRate);
                    cmd.Parameters.AddWithValue("@FCessAmt", saleOrderDetail.FCessAmt);

                    //Add the output parameter to the command object
                    cmd.Parameters.Add("@outId", SqlDbType.Int, 20);
                    cmd.Parameters["@outId"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@OutSuitApps_id", SqlDbType.NVarChar, 50);
                    cmd.Parameters["@OutSuitApps_id"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    DirectSalesDetail sale = new DirectSalesDetail();

                    int value = (int)cmd.Parameters["@outId"].Value;
                    string OutSuitApps_id = cmd.Parameters["@OutSuitApps_id"].Value.ToString();
                    if (value != 0)
                    {
                        sale.Message = "Success";
                        sale.DSDID = value.ToString();
                        sale.SuitApps_id = OutSuitApps_id;
                    }
                    else
                    {
                        httpresponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                        sale.Message = "Failed";
                        sale.SuitApps_id = OutSuitApps_id;
                    }
                    sales.Add(sale);
                }
                result.directSalesDetailList = sales;
            }

            catch (Exception e)
            {
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return result;
            }
            con.Close();
            return result;
        }
        #endregion
    }
}