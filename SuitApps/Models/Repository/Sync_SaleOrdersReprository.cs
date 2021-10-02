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
    public class Sync_SaleOrdersReprository
    {
        #region Sync_SaleOrder
        public static SaleOrderList Sync_SaleOrder(SaleOrderList list)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse; 
            SaleOrderList result = new SaleOrderList();
            List<SaleOrder> sales = new List<SaleOrder>();
            try
            {

                con = DBconnect.getDataBaseConnection();

                foreach (SaleOrder saleOrder in list.saleOrderList)
                {
                    int saleorderid = Convert.ToInt32(saleOrder.SOID);
                    con = DBconnect.getDataBaseConnection();

                    //Create the SqlCommand object
                    SqlCommand cmd = new SqlCommand("Sync_SaleOrders", con);

                    //Specify that the SqlCommand is a stored procedure
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Add the input parameters to the command object
                    cmd.Parameters.AddWithValue("@SOID", saleOrder.SOID);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("M/d/yyyy"));
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
                    cmd.Parameters.AddWithValue("@Customer_SuitAppId", saleOrder.Customer_SuitAppId);



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
                    //Add the output parameter to the command object
                    cmd.Parameters.Add("@OutId", SqlDbType.Int, 20);
                    cmd.Parameters["@OutId"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@OutSOID", SqlDbType.Int, 20);
                    cmd.Parameters["@OutSOID"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@OutSuitApps_id", SqlDbType.NVarChar, 50);
                    cmd.Parameters["@OutSuitApps_id"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    int value = (int)cmd.Parameters["@OutId"].Value;
                    int OutSOID = (int)cmd.Parameters["@OutSOID"].Value;
                    string OutSuitApps_id = cmd.Parameters["@OutSuitApps_id"].Value.ToString();

                    SaleOrder sale = new SaleOrder(); 
                        if (value != 0)
                        {
                            if (value == -1)
                            {
                                sale.Message = "Updated";
                                sale.SOID = OutSOID.ToString();
                                sale.SuitApps_id = OutSuitApps_id;

                                bool status = Sync_DeleteItemPurchase_b4_updation(OutSuitApps_id);
                            }
                            else
                            {
                                sale.Message = "Inserted";
                                sale.SOID = OutSOID.ToString();
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
                result.saleOrderList = sales;
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

        #region Sync_delete_beforeupdation
        public static bool Sync_DeleteItemPurchase_b4_updation(string SuitApps_id)
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
                SqlCommand cmd = new SqlCommand("Sync_Delete_Saleorderdetails", con);
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

        #region Sync_SaleOrderDetails
        public static SaleOrderDetailList Sync_SaleOrderDetails(SaleOrderDetailList list)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse;
            SaleOrderDetailList result = new SaleOrderDetailList();
            List<SaleOrderDetail> sales = new List<SaleOrderDetail>();
            try
            {

                con = DBconnect.getDataBaseConnection();

                foreach (SaleOrderDetail saleOrderDetail in list.saleOrderDetailList)
                {
                    SqlCommand cmd = new SqlCommand("Sync_SaleOrderDetails", con);
                    //Specify that the SqlCommand is a stored procedure
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //Add the input parameters to the command object
                    cmd.Parameters.AddWithValue("@SOID", saleOrderDetail.SOID);
                    cmd.Parameters.AddWithValue("@ItemID", saleOrderDetail.ItemID);
                    cmd.Parameters.AddWithValue("@Qty", saleOrderDetail.Qty);
                    cmd.Parameters.AddWithValue("@NetAmount", saleOrderDetail.NetAmount);
                    cmd.Parameters.AddWithValue("@SuitApps_id", saleOrderDetail.SuitApps_id);
                    cmd.Parameters.AddWithValue("@FreeQty", saleOrderDetail.FreeQty);
                    cmd.Parameters.AddWithValue("@Rate", saleOrderDetail.Rate);


                    //Add the output parameter to the command object
                    cmd.Parameters.Add("@outId", SqlDbType.Int, 20);
                    cmd.Parameters["@outId"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@OutSuitApps_id", SqlDbType.NVarChar, 50);
                    cmd.Parameters["@OutSuitApps_id"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    SaleOrderDetail sale = new SaleOrderDetail();

                    int value = (int)cmd.Parameters["@outId"].Value;
                    string OutSuitApps_id = cmd.Parameters["@OutSuitApps_id"].Value.ToString();
                    if (value != 0)
                    {
                        sale.Message = "Success";
                        sale.SODID = value.ToString();
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
                result.saleOrderDetailList = sales;
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