using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class DirectSales
    {
       public string DSID { get; set; }
       public string BillDate { get; set; }
       public string BillNo { get; set; }
       public string Bill_Series { get; set; }
       public string BillMode { get; set; }
       public string OrderNo { get; set; }
       public string CustomerID { get; set; }
       public string UserID { get; set; }
       public string CompanyID { get; set; }
       public string Amount { get; set; }
       public string AdvanceAmo { get; set; }
       public string TotAmo { get; set; }
       public string OrderStatus { get; set; }
       public string SuitApps_id { get; set; }
       public string Discount { get; set; }
       public string Discount_rate { get; set; }
       public string Message { get; set; }
       public string Customer_SuitAppsId { get; set; }
       public string InvoiceType { get; set; }
    }
}