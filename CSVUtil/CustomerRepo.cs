using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CSVUtil
{
    class CustomerRepo
    {
        private String _connString;

        public CustomerRepo(String connString)
        {
            try
            {
                // Make sure the connection works
                using (var conn = new SqlConnection(connString))
                {
                    // Create Command
                    conn.Open();
                    var comm = conn.CreateCommand();
                    comm.CommandText = "SELECT [dbo].ufn_version()";
                    Object result = comm.ExecuteScalar();
                    result.ToString();
                }
                _connString = connString;
            }
            catch(SqlException ex)
            {
                throw new ArgumentException("connString failed to find ufn_version", ex);
            }
        }

        public void CreateCustomer(Model.Customer newCustomer)
        {
            using (var conn = new SqlConnection(_connString))
            {
                // Create Command
                conn.Open();
                var comm = conn.CreateCommand();
                comm.CommandText =
                    @"INSERT INTO Customers (TypeID, CustomerID, SiteID, Name, SiteName, Terms, CreditLimit) 
                      VALUES (@TypeID, @CustomerID, @SiteID, @Name, @SiteName, @Terms, @CreditLimit)";

                // Define params
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@TypeID", 0));
                parameters.Add(new SqlParameter("@CustomerID", newCustomer.CustomerID));
                parameters.Add(new SqlParameter("@SiteID", newCustomer.SiteID));
                parameters.Add(new SqlParameter("@Name", newCustomer.Name));
                parameters.Add(new SqlParameter("@SiteName", newCustomer.SiteName));
                parameters.Add(new SqlParameter("@Terms", newCustomer.Terms));
                parameters.Add(new SqlParameter("@CreditLimit", newCustomer.CreditLimit));

                comm.Parameters.AddRange(parameters.ToArray());

                // Execute
                comm.ExecuteNonQuery();
            }
        }

        public void CreateCustomer(IEnumerable<Model.Customer> newCustomers)
        {
            Model.Customer currCust;
            String valueString = String.Empty;
            System.Text.StringBuilder queryString = new StringBuilder();

            // Define fields to be inserted
            queryString.AppendLine("INSERT INTO Customers (TypeID, CustomerID, SiteID, Name, SiteName, Terms, CreditLimit) ");

            // Build up the string for each item
            for (int i = 0; i < newCustomers.Count(); ++i)
            {
                currCust = newCustomers.ElementAt(i);
                valueString = String.Format(
                    "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}",
                    0,
                    currCust.CustomerID,
                    currCust.SiteID,
                    currCust.Name,
                    currCust.SiteName,
                    currCust.Terms,
                    currCust.CreditLimit
                );

                // No comma on the last item
                queryString.Append(valueString);
                if (i < newCustomers.Count() - 1)
                    queryString.Append(",");
            }

            // Create the command
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                var comm = conn.CreateCommand();
                comm.CommandText = queryString.ToString();

                comm.ExecuteNonQuery();
            }
        }

        public Guid GetCustomerGuid(String customerID, String siteID)
        {
            // Return GUID from  
            using (var conn = new SqlConnection(_connString))
            {
                // Create Command
                conn.Open();
                var comm = conn.CreateCommand();
                comm.CommandText = "SELECT Guid FROM [dbo].Customers WHERE CustomerID = @CustomerID AND SiteID = @SiteID";

                // Define params
                SqlParameter spSiteID = new SqlParameter("@SiteID", siteID);
                SqlParameter spCustomerID = new SqlParameter("@CustomerID", customerID);

                comm.Parameters.Add(spSiteID);
                comm.Parameters.Add(spCustomerID);

                // Execute
                var reader = comm.ExecuteReader();
                return reader.GetGuid(0);
            }
        }
    }
}
