using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageHelper.Entity
{
    class CustomerIndia : TableEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public CustomerIndia()
        {

        }
        public CustomerIndia(string name, string email)
        {
            this.Name = name;
            this.Email = email;
            this.PartitionKey = "INDIA";
            this.RowKey = email;
        }
        
    }
}
