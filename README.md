<!-- default file list -->
*Files to look at*:
* [Program.cs](./CS/Program.cs) (VB: [Program.vb](./VB/Program.vb))
<!-- default file list end -->

# XPO - How to Use the Database Schema Migrations API

> ### Note
> The Database Schema Migrations API is available as CTP. Refer to the [Database Schema Migrations (CTP)](https://supportcenter.devexpress.com/ticket/details/t879111) KB article for additional information.

To support Database Schema Migrations, [XPO Data Store Providers](https://docs.devexpress.com/XPO/2114/fundamentals/database-systems-supported-by-xpo) implement the following interfaces:
- **IDataStoreSupportSchemaMigration** - exposes methods used to compare the Data Model with the database schema and to apply differences to the database schema.
- **IUpdateSchemaSqlFormatter** - exposes methods used to generate SQL statements for a manual schema update.

To retrieve differences and generate the schema migration script programmatically, cast a data store provider object to these interfaces and call the `IDataStoreSupportSchemaMigration.CompareSchema` and `IUpdateSchemaSqlFormatter.FormatUpdateSchemaScript` methods subsequently. 

The `FormatUpdateSchemaScript` method returns SQL statements as a plain text. If you want to get SQL statements as an array, use the `IUpdateSchemaSqlFormatter.FormatUpdateSchemaStatements` method.

### Code examples:

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
