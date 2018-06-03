using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageHelper.Helpers
{
    class QueueHelper
    {
        public void InsertMessage(CloudQueue queue, CloudQueueMessage message)
        {
            queue.AddMessage(message);
        }

        public void DeleteMessage(CloudQueue queue, CloudQueueMessage message)
        {
            queue.DeleteMessage(message);
        }
    }
}
