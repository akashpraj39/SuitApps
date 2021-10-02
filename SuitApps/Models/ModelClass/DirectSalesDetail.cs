using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class DirectSalesDetail
    {
        public string DSDID { get; set; }
        public string DSID { get; set; }
        public string CustomerID { get; set; }
        public string ItemID { get; set; }
        public string Qty { get; set; }
        public string NetAmount { get; set; }
        public string SuitApps_id { get; set; }        
		public string Tax_Rate { get; set; }
		public string Tax_Amt { get; set; }
        public string Rate { get; set; }
        public string MRP { get; set; }
		public string GrossValue { get; set; }
		public string CGST_Rate { get; set; }
		public string CGST_Amt { get; set; }
		public string SGST_Rate { get; set; }
        public string SGST_Amt { get; set; }
        public string FreeQuantity { get; set; }
        public string Message { get; set; }
        public string disptg { get; set; }
        public string disamt { get; set; }
        public string FCessRate { get; set; }
        public string FCessAmt { get; set; }
    }
}