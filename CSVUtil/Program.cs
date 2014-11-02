using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LINQtoCSV;

namespace CSVUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerRepo cr = new CustomerRepo("SERVER=SPLITHPC;DATABASE=FVMaster;TRUSTED_CONNECTION=TRUE;");
            String userFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            CsvFileDescription inpurFile = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true
            };

            CsvContext cc = new CsvContext();
            IEnumerable<Contract.CustomerContract> customers = 
                cc.Read<Contract.CustomerContract>(userFolder + @"\customers.csv", inpurFile);
            Model.Customer customer = null;

            cr.CreateCustomer(customers.Select(c => c.GetCustomer));
        }
    }
}
