using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Entities
{
    class Sale
    {
        public int ID { get; set; }
        public int? CustomerID { get; set; }
        public DateTime Date { get; set; }
        public List<ProductSale> ProductSales { get; set; }

        public Sale() { }
        public Sale(DateTime Date)
        {
            this.Date = Date;
        }
    }
}
