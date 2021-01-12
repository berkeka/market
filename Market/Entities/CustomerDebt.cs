using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Market.Entities
{
    class CustomerDebt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public long CustomerIDNumber { get; set; }
        public double DebtAmount { get; set; }
        public CustomerDebt()
        {
        }
        public CustomerDebt(long CustomerIDNumber, double DebtAmount)
        {
            this.CustomerIDNumber = CustomerIDNumber;
            this.DebtAmount = DebtAmount;
        }
    }
}
