using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Entities
{
    class ProductSale
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int SaleID { get; set; }
        public double Amount { get; set; }

        public ProductSale() { }
        public ProductSale(int ProductID, int SaleID, double Amount)
        {
            this.ProductID = ProductID;
            this.SaleID = SaleID;
            this.Amount = Amount;
        }
    }
}
