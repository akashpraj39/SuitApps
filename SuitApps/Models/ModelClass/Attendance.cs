using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class Attendance
    {
        public string Id { get; set; }
        public string Userid { get; set; }
        public string Date { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string InPlace { get; set; }
        public string OutPlace { get; set; }
        public string InLatitude { get; set; }
        public string OutLatitude { get; set; }
        public string InLongitude { get; set; }
        public string OutLongitude { get; set; }
        public string CreatedBy { get; set; }
        public string Message { get; set; }
    }
}