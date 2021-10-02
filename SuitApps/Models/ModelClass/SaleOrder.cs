using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class SaleOrder
    {
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
       public string SuitApps_id { get; set; }
       public string Message { get; set; }
       public string Customer_SuitAppId { get; set; }
    }
}