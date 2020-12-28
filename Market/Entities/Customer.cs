using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Entities
{
    class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Customer()
        {
        }
        public Customer(int ID, string Name, string LastName)
        {
            // Fix me - Implement auto incremented id
            this.ID = ID;
            this.Name = Name;
            this.LastName = LastName;
        }
    }
}
