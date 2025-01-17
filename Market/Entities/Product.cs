﻿using System;
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
        public double Price { get; set; }
        public double WarningLimit { get; set; }
        public List<ProductSale> ProductSales { get; set; }

        public Product()
        {
        }
        public Product(string Barcode, string Name, double Price, double WarningLimit)
        {
            this.Barcode = Barcode;
            this.Name = Name;
            this.Price = Price;
            this.WarningLimit = WarningLimit;
        }
    }
}
