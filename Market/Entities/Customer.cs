using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Market.Entities
{
    class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IDNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }        
        public List<Sale> Sales { get; set; }
        public ICollection<CustomerDebt> Customers { get; set; }
        public Customer()
        {
        }
        public Customer(long IDNumber, string Name, string LastName)
        {
            this.IDNumber = IDNumber;
            this.Name = Name;
            this.LastName = LastName;
        }
    }
}
