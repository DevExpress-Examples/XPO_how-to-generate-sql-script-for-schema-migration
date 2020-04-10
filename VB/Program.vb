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
