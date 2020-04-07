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
            var dataStoreSupportsMigration = (IDataStoreSupportSchemaMigration)provider;
            var migrationScriptFormatter = (IUpdateSchemaSqlFormatter)provider;

            var dictionary = new ReflectionDictionary();
            DBTable[] targetSchema = dictionary.GetDataStoreSchema(typeof(Customer), typeof(Order), typeof(Product));

            var migrationOptions = new SchemaMigrationOptions();
            var updateSchemaStatements = dataStoreSupportsMigration.CompareSchema(targetSchema, migrationOptions);
            string sql = migrationScriptFormatter.FormatUpdateSchemaScript(updateSchemaStatements);

            Console.WriteLine("SQL Script to database schema migration:");
            Console.WriteLine("-----begin script------");
            Console.WriteLine(sql);
            Console.WriteLine("------end script-------");
        }
    }
}

namespace PersistentClasses { 

    class Customer : XPObject {
        public string CompanyName;     
       
        [Size(11)]
        public string Phone;

        [Association("customer-order")]
        public XPCollection<Order> Orders {
            get {
                return GetCollection<Order>();
            }
        }
    }

    class Order: XPLiteObject {
        [Key(true)]
        public int Id;
        
        [Association("customer-order")]
        public Customer Customer;

        [Association("product-order")]
        public XPCollection<Product> Products {
            get {
                return GetCollection<Product>();
            }
        }
    }
    
    class Product : XPLiteObject {
        [Key(false)]
        [Size(10)]
        public string ProductCode;

        public string ProductName;

        public decimal UnitPrice;

        [Association("product-order")]
        public XPCollection<Order> Orders {
            get {
                return GetCollection<Order>();
            }
        }

        public Product(Session session) : base(session) { }
    }
}
