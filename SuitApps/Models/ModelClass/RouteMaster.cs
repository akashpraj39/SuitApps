using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class RouteMaster
    {
        public string RootId { get; set; }
        public string RootName { get; set; }
        public string PlaceName { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
    }
}