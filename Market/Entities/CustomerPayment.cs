using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Entities
{
    class CustomerPayment
    {
        public int ID { get; set; }
        public long CustomerIDNumber { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime Date { get; set; }

        public CustomerPayment()
        {
        }
        public CustomerPayment(long CustomerIDNumber, double PaymentAmount, DateTime Date)
        {
            this.CustomerIDNumber = CustomerIDNumber;
            this.PaymentAmount = PaymentAmount;
            this.Date = Date;
        }
    }
}
