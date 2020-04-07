Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

Namespace WebApplication1.Models
	Public NotInheritable Class CustomersDataProvider

		Private Sub New()
		End Sub

		Public Shared Property Customers() As List(Of Customer)
			Get
				If HttpContext.Current.Session("_gridData") Is Nothing Then
					HttpContext.Current.Session("_gridData") = GetNewCustomersList()
				End If
				Return CType(HttpContext.Current.Session("_gridData"), List(Of Customer))
			End Get
			Set(ByVal value As List(Of Customer))
				HttpContext.Current.Session("_gridData") = value
			End Set
		End Property

		Private Shared Function GetNewCustomersList() As List(Of Customer)
			Return Enumerable.Range(0, 50).Select(Function(s) New Customer With {.CustomerID = s, .CustomerName = String.Format("Customer_{0}", s), .Licenses = GetLicensesForCustomer(s)}).ToList()
		End Function

		Private Shared Function GetLicensesForCustomer(ByVal customerID As Integer) As String
			Dim licenses = String.Join(";", LicensesDataProvider.Licenses.Take(customerID Mod 5).Select(Function(s) s.LicenseID))
			Return licenses
		End Function
	End Class
	Public NotInheritable Class LicensesDataProvider

		Private Sub New()
		End Sub

		Public Shared ReadOnly Property Licenses() As List(Of License)
			Get
				Return Enumerable.Range(0, 5).Select(Function(s) New License With {.LicenseID = s, .LicenseName = String.Format("License_{0}", s)}).ToList()
			End Get
		End Property

	End Class
End Namespace