using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Entities
{
    class ProductItem : Product
    {
        public double Amount { get; set; }

        public ProductItem() { }
        public ProductItem(Product p, double Amount)
        {
            this.Barcode = p.Barcode;
            this.ID = p.ID;
            this.Name = p.Name;
            this.Price = p.Price;
            this.Amount = Amount;
        }

    }
}
