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
        public long IDNumber { get; set; }
        public double DebtAmount { get; set; }
        public CustomerDebt()
        {
        }
        public CustomerDebt(long IDNumber, double DebtAmount)
        {
            this.IDNumber = IDNumber;
            this.DebtAmount = DebtAmount;
        }
    }
}
