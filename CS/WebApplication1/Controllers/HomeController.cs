using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace WebApplication1.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }
        public ActionResult GridViewPartial(bool? andOrValue) {
            ViewBag.Licenses = LicensesDataProvider.Licenses;
            ViewBag.andOrValue = andOrValue;
            return PartialView("_GridViewPartial", CustomersDataProvider.Customers);
        }
    }
}