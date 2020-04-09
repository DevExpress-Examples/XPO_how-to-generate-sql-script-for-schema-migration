<!-- default file list -->
*Files to look at*:
* [Program.cs](./CS/Program.cs) (VB: [Program.vb](./VB/Program.vb))
<!-- default file list end -->

# XPO - How to use the Database Schema Migrations API

> ### Note
> The Database Schema Migrations API is available as CTP. We can change the API in the final version. Use it for evaluation and testing purposes only. Refer to the [Database Schema Migrations]() blog post for additional information.

To support Database Schema Migrations, [XPO Data Store Adapters](https://docs.devexpress.com/XPO/2114/fundamentals/database-systems-supported-by-xpo) implement the following interfaces:
- **IDataStoreSupportSchemaMigration** - exposes methods to compare the Data Model with the database schema and apply differences to the database schema.
- **IUpdateSchemaSqlFormatter** - exposes methods to generate SQL statements for manual schema update.

To compare the database schema and generate the schema migration script programmatically, cast a Data Store Adapter object to these interfaces and call the `IDataStoreSupportSchemaMigration.GetDataStoreSchema` and `IUpdateSchemaSqlFormatter.FormatUpdateSchemaScript` methods subsequently.

### A code example

<details>
    <summary>C#</summary>

```cs
IDataStore provider = XpoDefault.GetConnectionProvider(ConnectionString, AutoCreateOption.DatabaseAndSchema);
var dataStoreSupportsMigration = (IDataStoreSupportSchemaMigration)provider;
var migrationScriptFormatter = (IUpdateSchemaSqlFormatter)provider;

var dictionary = new ReflectionDictionary();
DBTable[] targetSchema = dictionary.GetDataStoreSchema(typeof(Customer), typeof(Order), typeof(Product));

var migrationOptions = new SchemaMigrationOptions();
var updateSchemaStatements = dataStoreSupportsMigration.CompareSchema(targetSchema, migrationOptions);
string sql = migrationScriptFormatter.FormatUpdateSchemaScript(updateSchemaStatements);
```
</details>
<details>
    <summary>VB.NET</summary>

```vb
Dim provider As IDataStore = XpoDefault.GetConnectionProvider(ConnectionString, AutoCreateOption.DatabaseAndSchema)
Dim dataStoreSupportsMigration = DirectCast(provider, IDataStoreSupportSchemaMigration)
Dim migrationScriptFormatter = DirectCast(provider, IUpdateSchemaSqlFormatter)

Dim dictionary = New ReflectionDictionary()
Dim targetSchema() As DBTable = dictionary.GetDataStoreSchema(GetType(Customer), GetType(Order), GetType(Product))

Dim migrationOptions = New SchemaMigrationOptions()
Dim updateSchemaStatements = dataStoreSupportsMigration.CompareSchema(targetSchema, migrationOptions)
Dim sql As String = migrationScriptFormatter.FormatUpdateSchemaScript(updateSchemaStatements)
```
</details>

> ### Note
> The following Data Store Adapters support Database Schema Migrations in the current version: [MSSqlConnectionProvider](https://docs.devexpress.com/XPO/DevExpress.Xpo.DB.MSSqlConnectionProvider), **MySqlConnectionProvider**, **OracleConnectionProvider**, **ODPConnectionProvider**, **ODPManagedConnectionProvider**, and **PostgreSqlConnectionProvider**.