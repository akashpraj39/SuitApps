using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class Account
    {
        public string AccountId { get; set; }
		public string AccountParent { get; set; }
		public string AccountType { get; set; }
		public string AccountGroup { get; set; }
		public string AccountName { get; set; }
		public string Address { get; set; }
        public string Place { get; set; }
        public string District { get; set; }
	    public string State { get; set; }
        public string Pincode { get; set; }
	
		public string country { get; set; }
		public string Mob { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string TinNo { get; set; }
		public string CSTNo { get; set; }
		public string AcHeadNo { get; set; }
        public string UserId { get; set; }        
		public string Companyid { get; set; }
		public string RootId { get; set; }
		public string CreatedBy	 { get; set; }
		public string CreatedDateAndTime { get; set; }
		public string ModifiedBy { get; set; }
		public string MoifiedDateAndTime { get; set; }
		public string DeletedBy { get; set; }
		public string DeletedDateAndTime { get; set; }
		public string PinNo { get; set; }
        public string GSTinNo { get; set; }
        public string Customer_SuitAppsId { get; set; }
        public string Message { get; set; }
        public string Adhar { get; set; }
        //new
       // public string Statenew { get; set; }
       // public string Districtnew { get; set; }
        public string Taluk { get; set; }
        public string Distributor { get; set; }
        public string DistributorID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LocAddress { get; set; }

        /*
        public string AccountCode { get; set; }
        public string Amount { get; set; }
        public string ReflectsIn { get; set; }
        public string OrderInPreview { get; set; }
        public string RateType { get; set; }
        public string SaleBlock { get; set; }
        public string BankName { get; set; }
        public string CardType { get; set; }
        public string CardNo { get; set; }
        public string BillMode { get; set; }
        public string GradeID { get; set; }
        public string DLNO { get; set; }
        public string CreditDays { get; set; }
        public string DiscountPercentage { get; set; }
        public string Disc8HNT { get; set; }
        public string Disc8HT { get; set; }
         * */
    }
}