using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Entities
{
    class CustomerDebt
    {
        public int ID { get; set; }
        public DateTime DebtDate { get; set; }
        public int DebtAmount { get; set; }
        //foreign key for Customer
        public long IDNumber { get; set; }
        public Customer Customer { get; set; }
        public CustomerDebt()
        {
        }
        public CustomerDebt(long IDNumber, DateTime DebtDate, int DebtAmount)
        {
            this.IDNumber = IDNumber;
            this.DebtDate = DebtDate;
            this.DebtAmount = DebtAmount;
        }
    }
}
