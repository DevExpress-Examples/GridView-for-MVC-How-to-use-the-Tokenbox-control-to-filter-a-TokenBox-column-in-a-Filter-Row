Imports DevExpress.Data.Filtering
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

Namespace WebApplication1.Code
	Public NotInheritable Class CriteriaHelper

		Private Sub New()
		End Sub

        Public Shared Function ExtractDisplayText(ByVal criteria As CriteriaOperator) As String
            Dim visitor = New CustomVisitor()
            visitor.AcceptOperator(criteria)
            Return String.Join(";", visitor.Values)
        End Function
        Public Shared Function GetCriteriaByText(ByVal text As String, ByVal columnName As String, ByVal groupOperatorType As GroupOperatorType) As CriteriaOperator
            Dim ids = text.Split(";"c)
            Dim operandProp = New OperandProperty(columnName)
            Dim criteriaList = Enumerable.Range(0, ids.Length).[Select](Function(s) New FunctionOperator(FunctionOperatorType.Contains, operandProp, ids(s)))
            Dim criteria = New GroupOperator(groupOperatorType, criteriaList)
            Return criteria
        End Function
    End Class

	Public Class CustomVisitor
		Implements IClientCriteriaVisitor(Of CriteriaOperator)

		Public Sub New()
			Values = New List(Of String)()
		End Sub
		Private privateValues As List(Of String)
		Public Property Values() As List(Of String)
			Get
				Return privateValues
			End Get
			Private Set(ByVal value As List(Of String))
				privateValues = value
			End Set
		End Property
		Public Function AcceptOperator(ByVal theOperator As CriteriaOperator) As CriteriaOperator
			If Object.ReferenceEquals(theOperator, Nothing) Then
				Return Nothing
			End If
			Return theOperator.Accept(Of CriteriaOperator)(Me)
		End Function

		Protected Function VisitOperands(ByVal operands As CriteriaOperatorCollection) As CriteriaOperatorCollection
			For Each operand As CriteriaOperator In operands
				AcceptOperator(operand)
			Next operand
			Return operands
		End Function
		Protected Overridable Function VisitGroup(ByVal theOperator As GroupOperator) As CriteriaOperator
			VisitOperands(theOperator.Operands)
			Return theOperator
		End Function
		Protected Overridable Function VisitFunction(ByVal theOperator As FunctionOperator) As CriteriaOperator
			VisitOperands(theOperator.Operands)
			Return theOperator
		End Function
		Protected Overridable Function VisitValue(ByVal theOperand As OperandValue) As CriteriaOperator
			Dim constantValue = TryCast(theOperand, ConstantValue)
			If Not ReferenceEquals(constantValue, Nothing) Then
				Values.Add(constantValue.Value.ToString())
			End If
			Return theOperand
		End Function
		#Region "IClientCriteriaVisitor"
		Private Function IClientCriteriaVisitorGeneric_Visit(ByVal theOperand As JoinOperand) As CriteriaOperator Implements IClientCriteriaVisitor(Of CriteriaOperator).Visit
			Throw New NotImplementedException()
		End Function
		Private Function IClientCriteriaVisitorGeneric_Visit(ByVal theOperand As OperandProperty) As CriteriaOperator Implements IClientCriteriaVisitor(Of CriteriaOperator).Visit
			Return theOperand
		End Function
		Private Function IClientCriteriaVisitorGeneric_Visit(ByVal theOperand As AggregateOperand) As CriteriaOperator Implements IClientCriteriaVisitor(Of CriteriaOperator).Visit
			Throw New NotImplementedException()
		End Function
		#End Region
		#Region "ICriteriaVisitor"
		Private Function ICriteriaVisitorGeneric_Visit(ByVal theOperator As FunctionOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
			Return VisitFunction(theOperator)
		End Function
		Private Function ICriteriaVisitorGeneric_Visit(ByVal theOperand As OperandValue) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
			Return VisitValue(theOperand)
		End Function
		Private Function ICriteriaVisitorGeneric_Visit(ByVal theOperator As GroupOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
			Return VisitGroup(theOperator)
		End Function
		Private Function ICriteriaVisitorGeneric_Visit(ByVal theOperator As InOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
			Throw New NotImplementedException()
		End Function
		Private Function ICriteriaVisitorGeneric_Visit(ByVal theOperator As UnaryOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
			Throw New NotImplementedException()
		End Function
		Private Function ICriteriaVisitorGeneric_Visit(ByVal theOperator As BinaryOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
			Throw New NotImplementedException()
		End Function
		Private Function ICriteriaVisitorGeneric_Visit(ByVal theOperator As BetweenOperator) As CriteriaOperator Implements ICriteriaVisitor(Of CriteriaOperator).Visit
			Throw New NotImplementedException()
		End Function
		#End Region
	End Class
End Namespace
