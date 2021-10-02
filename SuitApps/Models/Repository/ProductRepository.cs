using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using SuitApps.Models.ModelClass;

namespace SuitApps.Models.Repository
{
    public class ProductRepository
    {
        #region GetAllProducts_App
        public static ProductList GetAllProducts_App()
        {
          SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            ProductList productList = new ProductList();
            List<Product> products = new List<Product>();

            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetAllProducts_App", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // con.Open();
/*
String sql = " select ProductID,ItemID,ItemName,MRP,WP,CategoryID,stock,ManufactureID FROM Product where isdeleted=0 Order by ProductID";
SqlCommand cmd = new SqlCommand(sql, con);
* */
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.ProductID = reader["ProductID"].ToString();
                        product.ItemID = reader["ItemID"].ToString();
                        product.ItemName = reader["ItemName"].ToString();
                        product.ManufactureID = reader["ManufactureID"].ToString();
                        product.MRP = reader["MRP"].ToString();
                        product.WP = reader["WP"].ToString();
                        product.CCP = reader["CCP"].ToString();
                        product.TotalStock = reader["stock"].ToString();
                        product.CategoryID = reader["CategoryID"].ToString();
                        product.HSNCode = reader["HSNCode"].ToString();
                        product.Tax = reader["Tax"].ToString();
                      //  product.AvailStock = GetStock(product.ItemID);
                        products.Add(product);
                    }
                }
                productList.productList = products;

                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return productList;
            }
            return productList;
        }
        #endregion

        #region GetVanItems
        public static ProductList GetVanItems(int VanID)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            ProductList productList = new ProductList();
            List<Product> products = new List<Product>();

            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetVanItems", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VanID", VanID);
                // con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.ProductID = reader["ProductID"].ToString();
                        product.ItemID = reader["ItemID"].ToString();
                        product.ItemName = reader["ItemName"].ToString();
                        product.ManufactureID = reader["ManufactureID"].ToString();
                        product.MRP = reader["MRP"].ToString();
                        product.WP = reader["WP"].ToString();
                        product.CCP = reader["CCP"].ToString();
                        product.TotalStock = reader["TotalStock"].ToString();
                        product.CategoryID = reader["CategoryID"].ToString();
                        product.HSNCode = reader["HSNCode"].ToString();
                        product.Tax = reader["Tax"].ToString();
                        product.FCessRate = reader["FCessRate"].ToString();
                        //  product.AvailStock = GetStock(product.ItemID);
                        products.Add(product);
                    }
                }
                productList.productList = products;

                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return productList;
            }
            return productList;
        }
        
        #endregion

        #region GetAllProducts_AppWithPagination
        public static ProductList GetAllProducts_AppWithPagination(Pagination page)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            ProductList productList = new ProductList();
            List<Product> products = new List<Product>();

            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetAllProducts_AppWithPagination", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pageno", page.pageNo);
                // con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.ProductID = reader["ProductID"].ToString();
                        product.ItemID = reader["ItemID"].ToString();
                        product.ItemName = reader["ItemName"].ToString();
                        product.ManufactureID = reader["ManufactureID"].ToString();
                        product.MRP = reader["MRP"].ToString();
                        product.WP = reader["WP"].ToString();
                        product.CCP = reader["CCP"].ToString();
                        product.TotalStock = reader["stock"].ToString();
                        product.CategoryID = reader["CategoryID"].ToString();
                        product.HSNCode = reader["HSNCode"].ToString();
                        product.Tax = reader["Tax"].ToString();

                        //product.AvailStock = GetStock(product.ItemID);
                        products.Add(product);
                    }
                }
                productList.productList = products;

                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return productList;
            }
            return productList;
        }
        #endregion

        #region GetProductDetailById
        public static Product GetProductDetailById(int itemId)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            ProductList productList = new ProductList();
            List<Product> products = new List<Product>();

            Product product = new Product();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetProductDetailsByID_App", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemID", itemId);
                // con.Open();
                reader = cmd.ExecuteReader();
                
                 if (reader.HasRows == true)
                {


                //  string availStock = GetStock(itemId.ToString());
                    while (reader.Read())
                    {
                        product.ProductID = reader["ProductID"].ToString();
                        product.ItemID = reader["ItemID"].ToString();
                        product.ItemName = reader["ItemName"].ToString();
                        product.ManufactureID = reader["ManufactureID"].ToString();
                        product.MRP = reader["MRP"].ToString();
                        product.WP = reader["WP"].ToString();
                        product.CCP = reader["CCP"].ToString();
                        product.TotalStock = reader["stock"].ToString(); // here stock is available stock
                    //    product.AvailStock = reader["orderstock"].ToString();
                        product.CategoryID = reader["CategoryID"].ToString();
                        product.HSNCode = reader["HSNCode"].ToString();
                        product.Tax = reader["Tax"].ToString();
                    //    product.AvailStock = availStock;
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
                return product;
            }
            return product;
        }
        #endregion

        #region GetStock
        public static Product GetStock(int itemId)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
          //  string availStock=null;
            Product product = new Product();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetStock", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemID", itemId);
                // con.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        product.ItemID = itemId.ToString();
                        product.AvailStock = reader["orderstock"].ToString();

                    }
                }
                reader.Close();

                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return product;
            }
            return product;
        }
        #endregion

        #region SearchProduct
        public static ProductList SearchProduct(string ItemName)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            ProductList productList = new ProductList();
            List<Product> products = new List<Product>();

            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("SearchProduct", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemName ", ItemName);
                // con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.ProductID = reader["ProductID"].ToString();
                        product.ItemID = reader["ItemID"].ToString();
                        product.ItemName = reader["ItemName"].ToString();
                        product.ManufactureID = reader["ManufactureID"].ToString();
                        product.MRP = reader["MRP"].ToString();
                        product.WP = reader["WP"].ToString();
                        product.CCP = reader["CCP"].ToString();
                        product.TotalStock = reader["stock"].ToString();
                        product.CategoryID = reader["CategoryID"].ToString();
                        product.HSNCode = reader["HSNCode"].ToString();
                        product.Tax = reader["Tax"].ToString();

                       // product.AvailStock = GetStock(product.ItemID);
                        products.Add(product);
                    }
                }
                productList.productList = products;

                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return productList;
            }
            return productList;
        }
        #endregion

     

    }
}