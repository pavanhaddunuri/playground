using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using StorageHelper.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageHelper.Helpers
{
    class TableHelper
    {
        private string _storageConnectionString = string.Empty;
        private CloudTableClient tableClient = null;
        public TableHelper(string connectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            tableClient = storageAccount.CreateCloudTableClient();
        }

        public CloudTable CreateTable()
        {
            CloudTable cloudTable = tableClient.GetTableReference("customers");
            cloudTable.CreateIfNotExists();

            return cloudTable;
        }

        public void CreateEntity(CustomerIndia customerIndia)
        {
            TableOperation operation = TableOperation.Insert(customerIndia);
            CloudTable cloudTable = tableClient.GetTableReference("customers");
            //cloudTable.CreateIfNotExists();
            cloudTable.Execute(operation);
        }

        public CustomerIndia RetrieveEntity(string partitionKey, string rowKey)
        {
            CloudTable cloudTable = CreateTable();
            TableOperation tableOperation = TableOperation.Retrieve<CustomerIndia>(partitionKey, rowKey);
            var result = cloudTable.Execute(tableOperation);

            var customerIndia = (CustomerIndia)result.Result;

            Console.WriteLine("Name: " + customerIndia.Name);
            return customerIndia;
        }

        public void RetrieveAll()
        {
            TableQuery<CustomerIndia> tableQuery = new TableQuery<CustomerIndia>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "INDIA"));

            var table = CreateTable();
            var result = table.ExecuteQuery(tableQuery);

            foreach (var item in result)
            {
                Console.WriteLine(item.Name);
            }
        }

        public void Update(CustomerIndia customer)
        {
            TableOperation ope = TableOperation.Replace(customer);
            CloudTable table = CreateTable();
            table.Execute(ope);
        }

        public void Delete(CustomerIndia customer)
        {
            TableOperation to = TableOperation.Delete(customer);
            var tab = CreateTable();
            tab.Execute(to);
        }

        public void PerformBatchInsert()
        {
            CustomerIndia c1 = new CustomerIndia("1", "1@1.com");
            CustomerIndia c2 = new CustomerIndia("2", "2@1.com");
            CustomerIndia c3 = new CustomerIndia("3", "3@1.com");

            var table = CreateTable();

            TableBatchOperation tbo = new TableBatchOperation();
            tbo.Insert(c1);
            tbo.Insert(c2);
            tbo.Insert(c3);

            table.ExecuteBatch(tbo);
        }
    }
}
