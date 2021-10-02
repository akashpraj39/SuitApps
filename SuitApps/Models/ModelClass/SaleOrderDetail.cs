using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class SaleOrderDetail
    {
        public string SODID { get; set; }
        public string SOID { get; set; }
        public string CustomerID { get; set; }
        public string ItemID { get; set; }
        public string Qty { get; set; }
        public string NetAmount { get; set; }
        public string SuitApps_id { get; set; }
        public string Message { get; set; }
        public string FreeQty { get; set; }
        public string Rate { get; set; }

    }
}