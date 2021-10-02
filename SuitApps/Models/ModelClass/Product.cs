using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class Product
    {
     public string ProductID { get; set; }
     public string ItemID { get; set; }
     public string ItemName { get; set; }
     public string MRP { get; set; }
     public string WP { get; set; }
     public string CCP { get; set; }
     public string CategoryID { get; set; }
     public string TotalStock { get; set; }
     public string AvailStock { get; set; }
     public string ManufactureID { get; set; }
     public string Tax { get; set; }
     public string HSNCode { get; set; }
     public string FCessRate { get; set; }

    }
}