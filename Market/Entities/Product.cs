using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Entities
{
    class Product
    {
        public string Barcode { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public List<ProductSale> ProductSales { get; set; }

        public Product()
        {
        }
        public Product(string Barcode, string Name, float Price)
        {
            this.Barcode = Barcode;
            this.Name = Name;
            this.Price = Price;
        }
    }
}
