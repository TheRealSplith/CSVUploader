using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVUtil.Model
{
    class Customer
    {
        public String CustomerID { get; set; }
        public String SiteID { get; set; }
        public String Name { get; set; }
        public String SiteName { get; set; }
        public String Terms { get; set; }
        public String SalespersonCode { get; set; }
        public String TaxCertificate { get; set; }
        public Double CreditLimit { get; set; }
    }
}
