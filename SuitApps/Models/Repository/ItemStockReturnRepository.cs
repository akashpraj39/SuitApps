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
    public class ItemStockReturnRepository
    {
        #region itemstockreturn
        public static ItemStockReturnList Insert_ItemStockReturn(ItemStockReturnList itemstockreturn)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse;
            ItemStockReturnList result = new ItemStockReturnList();
             List<ItemStockReturn> itemstocklist1 = new List<ItemStockReturn>();

            try
            {
              
                con = DBconnect.getDataBaseConnection();
                foreach (ItemStockReturn itemlist in itemstockreturn.itemstockreturnlist)
                {
                    int prid = Convert.ToInt32(itemlist.PRID);
                    //Create the SqlCommand object
                    SqlCommand cmd = new SqlCommand("InsertItemStockReturn", con);

                    //Specify that the SqlCommand is a stored procedure
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Add the input parameters to the command object
                    cmd.Parameters.AddWithValue("@PRID", itemlist.PRID);
                    cmd.Parameters.AddWithValue("@ItemId", itemlist.ItemId);
                    cmd.Parameters.AddWithValue("@ItemName ", itemlist.ItemName);
                    cmd.Parameters.AddWithValue("@CustomerId", itemlist.CustomerId);
                    cmd.Parameters.AddWithValue("@CustomerName", itemlist.CustomerName);
                    cmd.Parameters.AddWithValue("@Customer_SuitAppsId", itemlist.Customer_SuitAppsId);
                    cmd.Parameters.AddWithValue("@Item_Stock", itemlist.Item_Stock);
                    cmd.Parameters.AddWithValue("@Item_Return", itemlist.Item_Return);
                    cmd.Parameters.AddWithValue("@CreatedBy", itemlist.CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("M/d/yyyy"));


                    if (prid > 0)
                    {
                        cmd.Parameters.AddWithValue("@ModifiedBy", itemlist.CreatedBy);
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
                    itemlist.Message = "Inserted";
                    itemstocklist1.Add(itemlist);
                   
                }

                result.itemstockreturnlist = itemstocklist1;
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