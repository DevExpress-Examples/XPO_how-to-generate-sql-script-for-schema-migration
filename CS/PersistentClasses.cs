using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersistentClasses {
    class Customer : XPObject {
        string fCompanyName;        

        public string CompanyName {
            get {
                return fCompanyName;
            }
            set {
                SetPropertyValue(nameof(CompanyName), ref fCompanyName, value);
            }
        }

        string fPhone;

        [Size(11)]
        public string Phone {
            get {
                return fPhone;
            }
            set {
                SetPropertyValue(nameof(Phone), ref fPhone, value);
            }
        }

        [Association("customer-order")]
        public XPCollection<Order> Orders {
            get {
                return GetCollection<Order>();
            }
        }
    }

    class Order : XPLiteObject {

        [Key(true), Persistent("Id")]
        int fId;

        [PersistentAlias(nameof(fId))]
        public int Id {
            get {
                return (int)EvaluateAlias();
            }
        }

        Customer fCustomer;

        [Association("customer-order")]
        public Customer Customer {
            get {
                return fCustomer;
            }
            set {
                SetPropertyValue(nameof(Customer), ref fCustomer, value);
            }
        }

        [Association("product-order")]
        public XPCollection<Product> Products {
            get {
                return GetCollection<Product>();
            }
        }
    }

    class Product : XPLiteObject {
        string fProductCode;

        [Key(false)]
        [Size(10)]
        public string ProductCode {
            get {
                return fProductCode;
            }
            set {
                SetPropertyValue(nameof(ProductCode), ref fProductCode, value);
            }
        }

        string fProductName;

        public string ProductName {
            get {
                return fProductName;
            }
            set {
                SetPropertyValue(nameof(ProductName), ref fProductName, value);
            }
        }

        decimal fUnitPrice;

        public decimal UnitPrice {
            get {
                return fUnitPrice;
            }
            set {
                SetPropertyValue(nameof(UnitPrice), ref fUnitPrice, value);
            }
        }

        [Association("product-order")]
        public XPCollection<Order> Orders {
            get {
                return GetCollection<Order>();
            }
        }

        public Product(Session session) : base(session) { }
    }
}