using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class SaleForCustomer
    {
//----------------Sale order---------------
        public string SOID { get; set; }
        public string Date { get; set; }
        public string OrderNo { get; set; }
        public string CustomerID { get; set; }
        public string UserID { get; set; }
        public string CompanyID { get; set; }
        public string Amount { get; set; }
        public string AdvanceAmo { get; set; }
        public string TotAmo { get; set; }
        public string OrderStatus { get; set; }
//-----------------Customer-------------------
        public string AccountID { get; set; }
        public string AccountCode { get; set; }
        public string AccountParent { get; set; }
        public string AccountType { get; set; }
        public string AccountGroup { get; set; }
        public string AccountName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mob { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string Phone { get; set; }
        public string TinNo { get; set; }
        public string Companyid { get; set; }
        public string RootId { get; set; }
//---------------itempurchase------------------
        public string SODID { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string Qty { get; set; }
        public string NetAmount { get; set; }
        public string UnitPrice { get; set; }
        public string SuitApps_id { get; set; }
        public string Message { get; set; }
    }
}