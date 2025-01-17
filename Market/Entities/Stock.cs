﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Market.Entities
{
    class Stock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Barcode { get; set; }
        public double Amount { get; set; }
        public Stock() { }
        public Stock(string Barcode, double Amount)
        {
            this.Barcode = Barcode;
            this.Amount = Amount;
        }
    }
}
