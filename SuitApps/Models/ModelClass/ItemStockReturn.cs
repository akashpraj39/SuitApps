using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class ItemStockReturn
    {
        public string PRID { get; set; }
        public string ItemName { get; set; }
        public string ItemId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Customer_SuitAppsId { get; set; }
        public string Item_Stock { get; set; }
        public string Item_Return { get; set; }
        public string CreatedBy { get; set; }
        public string Message { get; set; }
       

    }
}