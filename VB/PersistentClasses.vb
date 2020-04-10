Option Infer On

Imports DevExpress.Xpo
Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace PersistentClasses
	Friend Class Customer
		Inherits XPObject

		Private fCompanyName As String

		Public Property CompanyName() As String
			Get
				Return fCompanyName
			End Get
			Set(ByVal value As String)
				SetPropertyValue(NameOf(CompanyName), fCompanyName, value)
			End Set
		End Property

		Private fPhone As String

		<Size(11)>
		Public Property Phone() As String
			Get
				Return fPhone
			End Get
			Set(ByVal value As String)
				SetPropertyValue(NameOf(Phone), fPhone, value)
			End Set
		End Property

		<Association("customer-order")>
		Public ReadOnly Property Orders() As XPCollection(Of Order)
			Get
				Return GetCollection(Of Order)()
			End Get
		End Property
	End Class

	Friend Class Order
		Inherits XPLiteObject

		<Key(True), Persistent("Id")>
		Private fId As Integer

		<PersistentAlias(NameOf(fId))>
		Public ReadOnly Property Id() As Integer
			Get
				Return CInt(Math.Truncate(EvaluateAlias()))
			End Get
		End Property

		Private fCustomer As Customer

		<Association("customer-order")>
		Public Property Customer() As Customer
			Get
				Return fCustomer
			End Get
			Set(ByVal value As Customer)
				SetPropertyValue(NameOf(Customer), fCustomer, value)
			End Set
		End Property

		<Association("product-order")>
		Public ReadOnly Property Products() As XPCollection(Of Product)
			Get
				Return GetCollection(Of Product)()
			End Get
		End Property
	End Class

	Friend Class Product
		Inherits XPLiteObject

		Private fProductCode As String

		<Key(False), Size(10)>
		Public Property ProductCode() As String
			Get
				Return fProductCode
			End Get
			Set(ByVal value As String)
				SetPropertyValue(NameOf(ProductCode), fProductCode, value)
			End Set
		End Property

		Private fProductName As String

		Public Property ProductName() As String
			Get
				Return fProductName
			End Get
			Set(ByVal value As String)
				SetPropertyValue(NameOf(ProductName), fProductName, value)
			End Set
		End Property

		Private fUnitPrice As Decimal

		Public Property UnitPrice() As Decimal
			Get
				Return fUnitPrice
			End Get
			Set(ByVal value As Decimal)
				SetPropertyValue(NameOf(UnitPrice), fUnitPrice, value)
			End Set
		End Property

		<Association("product-order")>
		Public ReadOnly Property Orders() As XPCollection(Of Order)
			Get
				Return GetCollection(Of Order)()
			End Get
		End Property

		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
	End Class
End Namespace