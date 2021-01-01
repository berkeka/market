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
        public long IDNumber { get; set; }
        public DateTime DebtDate { get; set; }
        public int TotalDebt { get; set; }
        public CustomerDebt()
        {
        }
        public CustomerDebt(long IDNumber, DateTime DebtDate, int TotalDebt)
        {
            this.IDNumber = IDNumber;
            this.DebtDate = DebtDate;
            this.TotalDebt = TotalDebt;
        }
    }
}
