using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;
using StorageHelper.Helpers;
using Microsoft.WindowsAzure.Storage.Table;
using StorageHelper.Entity;
using Microsoft.WindowsAzure.Storage.Queue;

namespace StorageHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            //TableHelper tableHelper = new TableHelper(System.Configuration.ConfigurationManager.AppSettings["StorageConnectionString"]);
            //var cloudTable = tableHelper.CreateTable();

            ////tableHelper.CreateEntity(new Entity.CustomerIndia("saharsh", "savan@local.com"));
            ////  tableHelper.RetrieveEntity("Pavan", "Pavan@local.com");

            //// CloudTable cloudTable = tableClient.GetTableReference("customers");
            //// cloudTable.CreateIfNotExists();
            //Console.WriteLine("Before:");
            //tableHelper.RetrieveAll();
            //var pavan = tableHelper.RetrieveEntity("INDIA", "Pavan@local.com");
            //pavan.Name = "HPK";
            //tableHelper.Update(pavan);
            //Console.WriteLine("After");
            //tableHelper.RetrieveAll();
            //pavan = tableHelper.RetrieveEntity("INDIA", "Pavan@local.com");
            //tableHelper.Delete(pavan);

            //tableHelper.PerformBatchInsert();
            //Console.WriteLine("After Batch Insert:");
            //tableHelper.RetrieveAll();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(System.Configuration.ConfigurationManager.AppSettings["StorageConnectionString"]);
            CloudQueueClient qClient = storageAccount.CreateCloudQueueClient();

            CloudQueue q = qClient.GetQueueReference("tasks");
            q.CreateIfNotExists();

            QueueHelper qHelper = new QueueHelper();
            //qHelper.InsertMessage(q, new CloudQueueMessage("Hello Pavan"));

            var msg = q.GetMessage();
            Console.WriteLine(msg.AsString);

            qHelper.DeleteMessage(q, msg);

            Console.ReadKey();
        }
    }
}
