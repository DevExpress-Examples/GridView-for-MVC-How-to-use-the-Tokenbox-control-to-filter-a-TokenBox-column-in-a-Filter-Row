
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports DevExpress.Web.Mvc
Imports WebApplication1.WebApplication1.Models

Namespace WebApplication1.Controllers
    Public Class HomeController
        Inherits Controller

        Public Function Index() As ActionResult
            Return View()
        End Function
        Public Function GridViewPartial(ByVal andOrValue? As Boolean) As ActionResult
            ViewBag.Licenses = LicensesDataProvider.Licenses
            ViewBag.andOrValue = andOrValue
            Return PartialView("_GridViewPartial", CustomersDataProvider.Customers)
        End Function
    End Class
End Namespace