Option Infer On

Imports System
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo.Metadata
Imports XpoSchemaMigration.PersistentClasses

Namespace XpoSchemaMigration

    Friend Class Program

        Public Const ConnectionString As String = "Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;MultipleActiveResultSets=true;initial catalog=schemamigration"

        Shared Sub Main(ByVal args() As String)

            Dim provider As IDataStore = XpoDefault.GetConnectionProvider(ConnectionString, AutoCreateOption.DatabaseAndSchema)
            Dim dataStoreSupportsMigration = DirectCast(provider, IDataStoreSupportSchemaMigration)
            Dim migrationScriptFormatter = DirectCast(provider, IUpdateSchemaSqlFormatter)

            Dim dictionary = New ReflectionDictionary()
            Dim targetSchema() As DBTable = dictionary.GetDataStoreSchema(GetType(Customer), GetType(Order), GetType(Product))

            Dim migrationOptions = New SchemaMigrationOptions()
            Dim updateSchemaStatements = dataStoreSupportsMigration.CompareSchema(targetSchema, migrationOptions)
            Dim sql As String = migrationScriptFormatter.FormatUpdateSchemaScript(updateSchemaStatements)

            Console.WriteLine("SQL Script to database schema migration:")
            Console.WriteLine("-----begin script------")
            Console.WriteLine(sql)
            Console.WriteLine("------end script-------")
        End Sub
    End Class
End Namespace

Namespace PersistentClasses

    Friend Class Customer
        Inherits XPObject

        Public CompanyName As String

        <Size(11)>
        Public Phone As String

        <Association("customer-order")>
        Public ReadOnly Property Orders() As XPCollection(Of Order)
            Get
                Return GetCollection(Of Order)()
            End Get
        End Property
    End Class

    Friend Class Order
        Inherits XPLiteObject

        <Key(True)>
        Public Id As Integer

        <Association("customer-order")>
        Public Customer As Customer

        <Association("product-order")>
        Public ReadOnly Property Products() As XPCollection(Of Product)
            Get
                Return GetCollection(Of Product)()
            End Get
        End Property
    End Class

    Friend Class Product
        Inherits XPLiteObject

        <Key(False), Size(10)>
        Public ProductCode As String

        Public ProductName As String

        Public UnitPrice As Decimal

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
