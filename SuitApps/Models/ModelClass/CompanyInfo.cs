using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class CompanyInfo
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string TelephoneNo { get; set; }
        public string MobileNo { get; set; }
        public string GSTinNo { get; set; }
    }
}