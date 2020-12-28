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
        public List<Sale> Sales { get; set; }
        public Customer()
        {
        }
        public Customer(string Name, string LastName)
        {
            this.Name = Name;
            this.LastName = LastName;
        }
    }
}
