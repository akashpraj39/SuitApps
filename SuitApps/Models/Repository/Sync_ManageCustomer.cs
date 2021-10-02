using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using SuitApps.Models.ModelClass;

namespace SuitApps.Models.Repository
{
    public class Sync_ManageCustomer
    {
        #region ManageCustomer
        public static AccountList Sync_NewCustomer(AccountList newCustomerList)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse;
            AcHead acHead = new AcHead();
            Account account = new Account();
            AcHeadList acHeadList = new AcHeadList();
            List<AcHead> acHeads = new List<AcHead>();
            AccountList accList = new AccountList();
            List<Account> accounts = new List<Account>();
            
            try
            {

                con = DBconnect.getDataBaseConnection();

                foreach (Account acc in newCustomerList.accountlList)
                {
                    //acHead.AccountCode = InsertUpdateAcHead(acc);
                    account = InsertUpdateCustomer(acc, "0");
                    accounts.Add(account);
                }
                accList.accountlList = accounts;
            }

            catch (Exception e)
            {
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return accList;
            }
            return accList;
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

                //Add the input parameters to the command object
            
               cmd.Parameters.AddWithValue("@AccountID", newAccount.AccountId);	
	            cmd.Parameters.AddWithValue("@AccountName", newAccount.AccountName)		;
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
                cmd.Parameters.AddWithValue("@DeletedBy", DateTime.Now.ToString("M/d/yyyy"));
	            cmd.Parameters.AddWithValue("@Place", newAccount.Place);
                cmd.Parameters.AddWithValue("@GSTinNo", newAccount.GSTinNo);
	            cmd.Parameters.AddWithValue("@AdharNo", newAccount.Adhar);
	            cmd.Parameters.AddWithValue("@Customer_SuitAppsId", newAccount.Customer_SuitAppsId);
                cmd.Parameters.AddWithValue("@RouteId", newAccount.RootId);
                cmd.Parameters.AddWithValue("@Taluk", newAccount.Taluk);
               cmd.Parameters.AddWithValue("@Distributor", newAccount.Distributor);
               cmd.Parameters.AddWithValue("@Latitude", newAccount.Latitude);
               cmd.Parameters.AddWithValue("@Longitude",newAccount.Longitude);
               cmd.Parameters.AddWithValue("@LocAddress", newAccount.LocAddress);
               //cmd.Parameters.AddWithValue("@DistributorID", newAccount.DistributorID);

              
                //Add the output parameter to the command object
                cmd.Parameters.Add("@outId", SqlDbType.Int, 20);
                cmd.Parameters["@outId"].Direction = ParameterDirection.Output;
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
                cmd.Parameters.AddWithValue("@AcHeadId", newAccount.AcHeadNo);
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