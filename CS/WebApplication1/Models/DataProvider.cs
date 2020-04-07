using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models {
    public static class CustomersDataProvider {
        public static List<Customer> Customers {
            get {
                if (HttpContext.Current.Session["_gridData"] == null)
                    HttpContext.Current.Session["_gridData"] = GetNewCustomersList();
                return (List<Customer>)HttpContext.Current.Session["_gridData"];
            }
            set {
                HttpContext.Current.Session["_gridData"] = value;
            }
        }

        private static List<Customer> GetNewCustomersList() {
            return Enumerable.Range(0, 50).Select(s => new Customer {
                CustomerID = s,
                CustomerName = String.Format("Customer_{0}", s),
                Licenses = GetLicensesForCustomer(s)
            }).ToList();
        }

        private static string GetLicensesForCustomer(int customerID) {
            var licenses = string.Join(";", LicensesDataProvider.Licenses.Take(customerID % 5).Select(s => s.LicenseID));
            return licenses;
        }
    }
    public static class LicensesDataProvider {
        public static List<License> Licenses {
            get {
                return Enumerable.Range(0, 5).Select(s => new License {
                    LicenseID = s,
                    LicenseName = String.Format("License_{0}", s)
                }).ToList();
            }
        }

    }
}