using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using PersistentClasses;

namespace XpoSchemaMigration {
    
    class Program {

        public const string ConnectionString = @"Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;MultipleActiveResultSets=true;initial catalog=schemamigration";

        static void Main(string[] args) {

            IDataStore provider = XpoDefault.GetConnectionProvider(ConnectionString, AutoCreateOption.DatabaseAndSchema);
            var migrationProvider = (IDataStoreSchemaMigrationProvider)provider;
            var migrationScriptFormatter = (IUpdateSchemaSqlFormatter)provider;

            var dictionary = new ReflectionDictionary();
            DBTable[] targetSchema = dictionary.GetDataStoreSchema(typeof(Customer), typeof(Order), typeof(Product));

            var migrationOptions = new SchemaMigrationOptions();
            var updateSchemaStatements = migrationProvider.CompareSchema(targetSchema, migrationOptions);
            string sql = migrationScriptFormatter.FormatUpdateSchemaScript(updateSchemaStatements);

            Console.WriteLine("SQL Script to database schema migration:");
            Console.WriteLine("-----begin script------");
            Console.WriteLine(sql);
            Console.WriteLine("------end script-------");
        }
    }
}
