using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class State
    {
        public string StateID { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public string StateType { get; set; }
        public string StateParent { get; set; }
        public string TaxID { get; set; }
        public string Taxname { get; set; }
        public string level { get; set; }
        public string createdBy { get; set; }
        public string CreatedDateAndTime { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDateAndTime { get; set; }
        public string DeletedBy { get; set; }
        public string IsDeleted { get; set; }
        public string DeletedDateAndTime { get; set; }
        public string Scategory { get; set; }



    }
}