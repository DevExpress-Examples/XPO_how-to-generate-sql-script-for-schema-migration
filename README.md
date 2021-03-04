<!-- default file list -->
*Files to look at*:
* [Program.cs](./CS/Program.cs) (VB: [Program.vb](./VB/Program.vb))
<!-- default file list end -->

# XPO - How to Use the Database Schema Migrations API

## How It Works
To support [Database Schema Migrations](https://supportcenter.devexpress.com/ticket/details/t879111), XPO data store [providers](https://docs.devexpress.com/XPO/2114/fundamentals/database-systems-supported-by-xpo) implement the following interfaces:
- **IDataStoreSchemaMigrationProvider** - exposes methods used to compare the Data Model with the database schema and to apply differences to the database schema.
- **IUpdateSchemaSqlFormatter** - exposes methods used to generate SQL statements for a manual schema update.

To retrieve differences and generate the schema migration script programmatically, cast a data store provider object to these interfaces and call the `IDataStoreSchemaMigrationProvider.CompareSchema` and `IUpdateSchemaSqlFormatter.FormatUpdateSchemaScript` methods subsequently. 

The `FormatUpdateSchemaScript` method returns SQL statements as a plain text. If you want to get SQL statements as an array, use the `IUpdateSchemaSqlFormatter.FormatUpdateSchemaStatements` method.

## Code Examples:

<details>
    <summary>C#</summary>

```cs
IDataStore provider = XpoDefault.GetConnectionProvider(ConnectionString, AutoCreateOption.DatabaseAndSchema);
var migrationProvider = (IDataStoreSchemaMigrationProvider)provider;
var migrationScriptFormatter = (IUpdateSchemaSqlFormatter)provider;

var dictionary = new ReflectionDictionary();
DBTable[] targetSchema = dictionary.GetDataStoreSchema(typeof(Customer), typeof(Order), typeof(Product));

var migrationOptions = new SchemaMigrationOptions();
var updateSchemaStatements = migrationProvider.CompareSchema(targetSchema, migrationOptions);
string sql = migrationScriptFormatter.FormatUpdateSchemaScript(updateSchemaStatements);
```
</details>
<details>
    <summary>VB.NET</summary>

```vb
Dim provider As IDataStore = XpoDefault.GetConnectionProvider(ConnectionString, AutoCreateOption.DatabaseAndSchema)
Dim migrationProvider = DirectCast(provider, IDataStoreSchemaMigrationProvider)
Dim migrationScriptFormatter = DirectCast(provider, IUpdateSchemaSqlFormatter)

Dim dictionary = New ReflectionDictionary()
Dim targetSchema() As DBTable = dictionary.GetDataStoreSchema(GetType(Customer), GetType(Order), GetType(Product))

Dim migrationOptions = New SchemaMigrationOptions()
Dim updateSchemaStatements = migrationProvider.CompareSchema(targetSchema, migrationOptions)
Dim sql As String = migrationScriptFormatter.FormatUpdateSchemaScript(updateSchemaStatements)
```
</details>
