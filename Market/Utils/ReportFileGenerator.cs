using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Market.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Market.Utils
{
    class ReportFileGenerator
    {
        public void SingleCustomerReport(long CustomerID)
        {

            var context = new MarketDBContext();

            Customer customer = context.Customers.Find(CustomerID);

            var result = context.Database.SqlQuery<ProductItem>($@"select 1 as ID, 'abc' as Barcode, Products.Price as Price, Products.Name, a.Amount
                                                                from (select ProductID, SUM(Amount) Amount
	                                                                from Sales
	                                                                join ProductSales
	                                                                on Sales.ID = ProductSales.SaleID
	                                                                where Sales.CustomerID = {customer.IDNumber}
	                                                                group by ProductID) a
                                                                join Products
                                                                on Products.ID = a.ProductID;");

            List<ProductItem> piList = result.ToList();

            if(piList.Count == 0) {Console.WriteLine("Selected customer bougth no items"); return; }

            // Create a A4 sized document
            Document document = new Document(PageSize.A4, 30, 30, 30, 30);

            // Set file path to desktop and create filename using customer name and lastname
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = "/Rapor_" + customer.Name + "_" + customer.LastName + ".pdf";

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path + fileName, FileMode.Create));

            if (document.IsOpen() == false)
            {

                document.Open();
                // Fill in customer info
                string customerInfoText = $"Müsteri Adi: {customer.Name}\nMüsteri Soyadi: {customer.LastName}\n";

                Paragraph customerPara = new Paragraph(customerInfoText);
                customerPara.Alignment = Element.ALIGN_LEFT;
                customerPara.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12f, BaseColor.GREEN);
                customerPara.SpacingAfter = 30;
                document.Add(customerPara);
                
                // Create a table for sold items ( 4 columns )
                PdfPTable table = new PdfPTable(4);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                List<PdfPCell> cellList = new List<PdfPCell>();

                PdfPCell c0 = new PdfPCell(new Phrase("Satis Bilgisi"));
                c0.Colspan = 4;
                c0.HorizontalAlignment = 1;

                cellList.Add(c0);
                cellList.Add(new PdfPCell(new Phrase("Urun Ismi")));
                cellList.Add(new PdfPCell(new Phrase("Urun Fiyati")));
                cellList.Add(new PdfPCell(new Phrase("Urun Miktari")));
                cellList.Add(new PdfPCell(new Phrase("Odenen Fiyat")));

                // Add all products to the table and calculate sum of spent money
                double sum = 0;
                foreach (ProductItem pi in piList)
                {
                    cellList.Add(new PdfPCell(new Phrase(pi.Name)));
                    cellList.Add(new PdfPCell(new Phrase(pi.Price.ToString())));
                    cellList.Add(new PdfPCell(new Phrase(pi.Amount.ToString())));

                    double paidAmount = (pi.Amount * pi.Price);
                    sum += paidAmount;
                    cellList.Add(new PdfPCell(new Phrase(paidAmount.ToString())));
                }

                foreach (PdfPCell cell in cellList)
                {
                    cell.Border = 0;
                    cell.FixedHeight = 25f;
                    cell.HorizontalAlignment = 1;
                    table.AddCell(cell);
                }

                table.AddCell(""); table.AddCell(""); table.AddCell("");
                PdfPCell sumCell = new PdfPCell(new Phrase(sum.ToString() + "₺"));
                sumCell.Border = 1; sumCell.HorizontalAlignment = 1;
                table.AddCell(sumCell);

                document.Add(table);
                document.Close();
            }
        }

        public void AllCustomerReport()
        {

        }
    }
}
