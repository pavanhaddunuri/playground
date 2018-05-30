using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageHelper.Helpers
{
    class BlobHelper
    {
        public void BlobOperations()
        {
            var cloudStorage = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());

            var blobClient = cloudStorage.CreateCloudBlobClient();
            var containerRef = blobClient.GetContainerReference("testcontainer");

            containerRef.CreateIfNotExists();

            // Upload a file to the block blob
            using (var fs = System.IO.File.OpenRead(@"C:\Users\pkhaddun\Desktop\Sites_20180530011616.xml"))
            {
                var blobRef = containerRef.GetBlockBlobReference("Sites_20180530011616.xml");
                blobRef.UploadFromStream(fs);
            }

            // Download file from blob
            using (var fs = System.IO.File.OpenWrite(@"C:\Users\pkhaddun\Desktop\Sites.xml"))
            {
                var blockBlobRef = containerRef.GetBlockBlobReference("Sites_20180530011616.xml");
                blockBlobRef.DownloadToStream(fs);
            }

            //Enumerate the blobs
            var blobs = containerRef.ListBlobs();

            foreach (var blob in blobs)
            {
                Console.WriteLine("Blob Uri : " + blob.StorageUri.ToString());
            }

            //Copy blob from one container to another or with in the same container in Azure
            var blobOrigRef = containerRef.GetBlockBlobReference("Sites_20180530011616.xml");
            var blobCopyRef = containerRef.GetBlockBlobReference("Sitex_Copy.xml");

            var x = new AsyncCallback(xa => Console.WriteLine("Copy completed"));

            blobCopyRef.BeginStartCopy(blobOrigRef, x, null);

            //Create a block blob in the new container
            var newBlockBlob = containerRef.GetBlockBlobReference(@"testcontainernew/Sites_A.xml");
            using (var fs = System.IO.File.OpenRead(@"C:\Users\pkhaddun\Desktop\Sites_A.xml"))
            {
                newBlockBlob.UploadFromStream(fs);
            }

            //Add metadata 
            SetMetaData(containerRef);

            //Get Metadata
            FetchMetadata(containerRef);

            Console.ReadKey();
        }

        private static void FetchMetadata(CloudBlobContainer containerRef)
        {
            containerRef.FetchAttributes();

            foreach (var item in containerRef.Metadata)
            {
                Console.WriteLine(string.Format("Key: {0} , Value: {1}", item.Key, item.Value));
            }
        }

        private static void SetMetaData(CloudBlobContainer containerRef)
        {
            containerRef.Metadata.Clear();
            containerRef.Metadata.Add("Name", "Pavan");
            containerRef.Metadata["LastModified"] = DateTime.Now.ToString();

            containerRef.SetMetadata();
        }
    }
}
