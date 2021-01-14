using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Entities
{
    class Storage
    {
        public int ID { get; set; }
        public int DispatchNoteID { get; set; }
        public int SupplierID { get; set; }
        public string Barcode { get; set; }
        public double PriceForUnit { get; set; }
        public double Amount { get; set; }
        public DateTime IntakeDate { get; set; }
        public Storage()
        {
        }
        public Storage(int DispatchNoteID, int SupplierID, string Barcode, double PriceForUnit, double Amount, DateTime IntakeDate)
        {
            this.DispatchNoteID = DispatchNoteID;
            this.SupplierID = SupplierID;
            this.Barcode = Barcode;
            this.PriceForUnit = PriceForUnit;
            this.Amount = Amount;
            this.IntakeDate = IntakeDate;
        }
    }
}
