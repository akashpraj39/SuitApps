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
    public class SaleOrderDetailRepository
    {
        #region InsertSalesOrderDetails
        public static SaleOrderDetailList InsertSalesOrderDetails(SaleOrderDetailList list)
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
                        SqlCommand cmd = new SqlCommand("InsertSalesOrderDetails", con);
                        //Specify that the SqlCommand is a stored procedure
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        //Add the input parameters to the command object
                        cmd.Parameters.AddWithValue("@SOID", saleOrderDetail.SOID);
                        cmd.Parameters.AddWithValue("@ItemID", saleOrderDetail.ItemID);
                        cmd.Parameters.AddWithValue("@Qty", saleOrderDetail.Qty);
                        cmd.Parameters.AddWithValue("@NetAmount", saleOrderDetail.NetAmount);
                        cmd.Parameters.AddWithValue("@SuitApps_id", saleOrderDetail.SuitApps_id);

                        //Add the output parameter to the command object
                        cmd.Parameters.Add("@outId", SqlDbType.Int, 20);
                        cmd.Parameters["@outId"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        SaleOrderDetail sale = new SaleOrderDetail();
                        int value = (int)cmd.Parameters["@outId"].Value;
                        if (value != 0)
                        {
                            sale.Message = "Success";
                            sale.SODID = value.ToString();
                        }
                        else
                        {
                            httpresponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                            sale.Message = "Failed";
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

        #region GetAllFrom_SalesOrderDetails
        public static SaleOrderDetailList GetAllFrom_SalesOrderDetails(int Sodid)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            SaleOrderDetailList saleOrderList = new SaleOrderDetailList();
            List<SaleOrderDetail> saleOrderDetails = new List<SaleOrderDetail>();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetAllFrom_SalesOrderDetails", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Sodid", Sodid);
                // con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        SaleOrderDetail saleOrdes = new SaleOrderDetail();
                        saleOrdes.SODID = reader["Sodid"].ToString();
                        saleOrdes.SOID = reader["Soid"].ToString();
                        saleOrdes.ItemID = reader["ItemId"].ToString();
                        saleOrdes.Qty = reader["Qty"].ToString();
                        saleOrdes.NetAmount = reader["NetAmount"].ToString();
                        saleOrderDetails.Add(saleOrdes);
                    }
                }

                saleOrderList.saleOrderDetailList = saleOrderDetails;
                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return saleOrderList;
            }
            return saleOrderList;
        }
        #endregion

      

    }
}