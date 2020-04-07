Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

Namespace WebApplication1.Models
	Public Class Customer
		Public Property CustomerID() As Integer
		Public Property CustomerName() As String
		Public Property Licenses() As Object
	End Class
	Public Class License
		Public Property LicenseID() As Integer
		Public Property LicenseName() As String
	End Class
End Namespace