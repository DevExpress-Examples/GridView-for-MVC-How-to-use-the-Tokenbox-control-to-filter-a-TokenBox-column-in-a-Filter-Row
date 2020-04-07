using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models {
    public class Customer {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public object Licenses { get; set; }
    }
    public class License {
        public int LicenseID { get; set; }
        public string LicenseName { get; set; }
    }
}