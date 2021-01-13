using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Entities
{
    class Payment
    {
        public int ID { get; set; }
        public long CustomerIDNumber { get; set; }
        public int SupplierID { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime Date { get; set; }

        public Payment()
        {
        }
        public Payment(long CustomerIDNumber, int SupplierID, double PaymentAmount, DateTime Date)
        {
            this.CustomerIDNumber = CustomerIDNumber;
            this.SupplierID = SupplierID;
            this.PaymentAmount = PaymentAmount;
            this.Date = Date;
        }
    }
}
