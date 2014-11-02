using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LINQtoCSV;
using System.Globalization;

namespace CSVUtil.Contract
{
    class CustomerContract
    {
        #region "CSV Properties"
        [CsvColumn(CanBeNull = false, FieldIndex = 1, Name = "Customer ID (alphanumeric)")]
        public String CustomerID { get; set; }

        [CsvColumn(CanBeNull = false, FieldIndex = 2, Name = "Customer Company Name")]
        public String CustomerName { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 3, Name = "Customer First Address Line")]
        public String CustomerFirstAddress { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 4, Name = "Customer Second Address Line")]
        public String CustomerSecondAddress { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 5, Name = "Customer City")]
        public String CustomerCity { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 6, Name = "Customer State")]
        public String CustomerState { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 7, Name = "Customer Zip")]
        public String CustomerZip { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 8, Name = "Customer Contact Name")]
        public String CustomerContactName { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 9, Name = "Customer Telephone Number")]
        public String CustomerTelephoneNumber { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 10, Name = "Customer fax number")]
        public String CustomerFaxNumber { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 11, Name = "Customer Email Address")]
        public String CustomerEmailAddress { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 12, Name = "Billing First Address Line")]
        public String BillingFirstAddress { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 13, Name = "Billing Second Address Line")]
        public String BillingSecondAddress { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 14, Name = "Billing City")]
        public String BillingCity { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 15, Name = "Billing State")]
        public String BillingState { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 16, Name = "Billing Zip")]
        public String BillingZip { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 17, Name = "Billing Contact Name")]
        public String BillingContactName { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 18, Name = "Billing Telephone Number")]
        public String BillingTelephoneNumber { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 19, Name = "Billing Fax Number")]
        public String BillingFaxNumber { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 20, Name = "Billing Email Address")]
        public String BillingEmailAddress { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 21, Name = "Shipping First Address Line")]
        public String ShippingFirstAddress { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 22, Name = "Shipping Second Address Line")]
        public String ShippingSecondAddress { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 23, Name = "Shipping City")]
        public String ShippingCity { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 24, Name = "Shipping State")]
        public String ShippingState { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 25, Name = "Shipping Zip")]
        public String ShippingZip { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 26, Name = "Shipping Contact Name")]
        public String ShippingContactName { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 27, Name = "Shipping Telephone Number")]
        public String ShippingTelephoneNumber { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 28, Name = "Shipping fax number")]
        public String ShippingFaxNumber { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 29, Name = "Shipping Email Address")]
        public String ShippingEmailAddress { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 30, Name = "Customer Account Terms")]
        public String CustomerAccountTerms { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 31, Name = "Salesperson Code")]
        public String SalespersonCode { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 32, Name = "Tax Table")]
        public String TaxTable { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 33, Name = "Sales Tax Certificate")]
        public String SalesTaxCertificate { get; set; }

        [CsvColumn(CanBeNull = true, FieldIndex = 34, Name = "Credit Limit")]
        public String CreditLimit { get; set; }
        #endregion

        #region "Parsed Values"
        private static Object culture = CultureInfo.CreateSpecificCulture("en-US");
        public Model.Customer GetCustomer
        {
            get
            {
                // Copy tax values
                Model.Customer cust = new Model.Customer
                {
                    CustomerID = this.CustomerID,
                    SiteID = this.CustomerID,
                    Name = this.CustomerName,
                    SiteName = this.CustomerName,
                    Terms = this.CustomerAccountTerms,
                    SalespersonCode = this.SalespersonCode,
                    TaxCertificate = this.SalesTaxCertificate
                };

                try
                {
                    Double creditLimit =
                        Double.Parse(
                        this.CreditLimit,
                        new CultureInfo("en-US"));

                    cust.CreditLimit = creditLimit;
                    return cust;
                }
                catch (Exception ex)
                {
                    String custInfo = String.Format("Cust:{0}", this.CustomerID);
                    throw new Exception("Customer Construction Failed for " + custInfo, ex);
                }
            }
            private set { throw new NotImplementedException(); }
        }
        #endregion
    }
}
