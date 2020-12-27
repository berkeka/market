using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Entities
{
    class Storage
    {
        public int DispatchNoteID { get; set; }
        public string Barcode { get; set; }
        public float PriceForUnit { get; set; }
        public int Amount { get; set; }
        public List<Product> Products { get; set; }
        public Storage()
        {
        }
        public Storage(int DispatchNoteID, string Barcode, float PriceForUnit, int Amount)
        {
            // Fix me - Implement auto incremented id
            this.DispatchNoteID = DispatchNoteID;
            this.Barcode = Barcode;
            this.PriceForUnit = PriceForUnit;
            this.Amount = Amount;
        }
    }
}
