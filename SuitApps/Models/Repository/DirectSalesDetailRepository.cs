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
    public class DirectSaleDetailRepository
    {
        #region InsertDirectSalesDetails
        public static DirectSalesDetailList InsertDirectSalesDetails(DirectSalesDetailList list)
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
                        SqlCommand cmd = new SqlCommand("InsertDirectSalesDetails", con);
                        //Specify that the SqlCommand is a stored procedure
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        //Add the input parameters to the command object
                        cmd.Parameters.AddWithValue("@DSID", saleOrderDetail.DSID);
                        cmd.Parameters.AddWithValue("@ItemID", saleOrderDetail.ItemID);
                        cmd.Parameters.AddWithValue("@Qty", saleOrderDetail.Qty);
                        cmd.Parameters.AddWithValue("@NetAmount", saleOrderDetail.NetAmount);
                        cmd.Parameters.AddWithValue("@SuitApps_id", saleOrderDetail.SuitApps_id);
                        cmd.Parameters.AddWithValue("@Tax_Rate",saleOrderDetail.Tax_Rate);
                        cmd.Parameters.AddWithValue("@Tax_Amt",saleOrderDetail.Tax_Amt);
                        cmd.Parameters.AddWithValue("@Rate",saleOrderDetail.Rate);
                        cmd.Parameters.AddWithValue("@MRP", saleOrderDetail.MRP);
                        cmd.Parameters.AddWithValue("@GrossValue",saleOrderDetail.GrossValue);
                        cmd.Parameters.AddWithValue("@CGST_Rate",saleOrderDetail.CGST_Rate);
                        cmd.Parameters.AddWithValue("@CGST_Amt",saleOrderDetail.CGST_Amt);
                        cmd.Parameters.AddWithValue("@SGST_Rate",saleOrderDetail.SGST_Rate);
                        cmd.Parameters.AddWithValue("@SGST_Amt",saleOrderDetail.SGST_Amt);
                        cmd.Parameters.AddWithValue("@FreeQuantity", saleOrderDetail.FreeQuantity);

                        //Add the output parameter to the command object
                        cmd.Parameters.Add("@outId", SqlDbType.Int, 20);
                        cmd.Parameters["@outId"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        DirectSalesDetail sale = new DirectSalesDetail();
                        int value = (int)cmd.Parameters["@outId"].Value;
                        if (value != 0)
                        {
                            sale.Message = "Success";
                            sale.DSDID = value.ToString();
                        }
                        else
                        {
                            httpresponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                            sale.Message = "Failed";
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

        #region GetAllFrom_DirectSalesDetails
        public static DirectSalesDetailList GetAllFrom_DirectSalesDetails(int Dsdid)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            DirectSalesDetailList saleOrderList = new DirectSalesDetailList();
            List<DirectSalesDetail> saleOrderDetails = new List<DirectSalesDetail>();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetAllFrom_DirectSalesDetails", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Dsdid", Dsdid);
                // con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        DirectSalesDetail saleOrdes = new DirectSalesDetail();
                        saleOrdes.DSDID = reader["Dsdid"].ToString();
                        saleOrdes.DSID = reader["Dsid"].ToString();
                        saleOrdes.ItemID = reader["ItemId"].ToString();
                        saleOrdes.Qty = reader["Qty"].ToString();
                        saleOrdes.NetAmount = reader["NetAmount"].ToString();
                        saleOrdes.Tax_Rate = reader["Tax_Rate"].ToString();
                        saleOrdes.Tax_Amt = reader["Tax_Amt"].ToString();
                        saleOrdes.Rate = reader["Rate"].ToString();
                        saleOrdes.MRP = reader["MRP"].ToString();
                        saleOrdes.GrossValue = reader["GrossValue"].ToString();
                        saleOrdes.CGST_Rate = reader["CGST_Rate"].ToString();
                        saleOrdes.CGST_Amt = reader["CGST_Amt"].ToString();
                        saleOrdes.SGST_Rate = reader["SGST_Rate"].ToString();
                        saleOrdes.SGST_Amt = reader["SGST_Amt"].ToString();
                        saleOrdes.FreeQuantity = reader["FreeQuantity"].ToString();
                        saleOrdes.SuitApps_id = reader["SuitApps_id"].ToString();
                        saleOrderDetails.Add(saleOrdes);
                    }
                }

                saleOrderList.directSalesDetailList = saleOrderDetails;
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