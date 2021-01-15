using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Market.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using System.Windows;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Market.Utils
{
    class ReportFileGenerator
    {
        public void SingleCustomerReport(long CustomerID, int choice)
        {

            var context = new MarketDBContext();

            Customer customer = context.Customers.Find(CustomerID);

            var result = context.Database.SqlQuery<ReportItem>($@"EXEC CustomerSales @InputID = {CustomerID};");

            List<ReportItem> riList = result.ToList();

            if (riList.Count == 0) { Console.WriteLine("Seçilen müşteri satın alım yapmamış"); return; }

            if (choice == 1) { SinglePdf(customer, riList); }
            else { SingleExcel(customer, riList); }
        }

        public void SingleExcel(Customer customer, List<ReportItem> riList)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            String date = DateTime.Now.ToString("HHmmss");
            string fileName = "/Rapor_" + customer.Name + "_" + customer.LastName + "_" + date + ".xlsx";

            //string fullfilename = path + fileName;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excel = new ExcelPackage(new FileInfo(path + fileName));
            ExcelWorksheet sheetcreate = excel.Workbook.Worksheets.Add("Rapor");

            sheetcreate.Cells.LoadFromCollection(riList);
            excel.Save();
        }

        public void SinglePdf(Customer customer, List<ReportItem> riList)
        {
            Document document = new Document(PageSize.A4, 30, 30, 30, 30);

            String date = DateTime.Now.ToString("HHmmss");

            // Set file path to desktop and create filename using customer name and lastname
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = "/Rapor_" + customer.Name + "_" + customer.LastName + date + ".pdf";

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
                foreach (ReportItem ri in riList)
                {
                    cellList.Add(new PdfPCell(new Phrase(ri.Name)));
                    cellList.Add(new PdfPCell(new Phrase(ri.Price.ToString())));
                    cellList.Add(new PdfPCell(new Phrase(ri.Amount.ToString())));

                    double paidAmount = (ri.Amount * ri.Price);
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

        public void AllCustomerReport(int choice)
        {
            var context = new MarketDBContext();

            var resultCustomer = context.Database.SqlQuery<Customer>($@"select IDNumber, Name, Lastname from Customers");

            List<Customer> cList = resultCustomer.ToList();

            //Check if there is any customer
            if (cList.Count() == 0) { Console.WriteLine("Veri tabanında kayıtlı müşteri yok"); return; }

            if (choice == 1) { AllPdf(cList); }
            else { AllExcel(cList); }
        }
        public void AllExcel(List<Customer> cList)
        {
            String date = DateTime.Now.ToString("HHmmss");
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = "/Toplu_musteri_raporu" + date + ".xls";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excel = new ExcelPackage(new FileInfo(path + fileName));
            ExcelWorksheet sheetcreate = excel.Workbook.Worksheets.Add("Rapor");

            var context = new MarketDBContext();

            var rl = new List<ReportList>();

            foreach (Customer c in cList)
            {

                Customer cstmr = context.Customers.Find(c.IDNumber);

                var resultSale = context.Database.SqlQuery<ReportItem>($@"EXEC CustomerSales @InputID = {cstmr.IDNumber};");

                List<ReportItem> riList = resultSale.ToList();

                double purchase = 0;
                if (riList.Count > 0)
                {
                    double sumPaid = 0;
                    foreach (ReportItem ri in riList)
                    {
                        sumPaid += (ri.Amount * ri.Price);
                    }
                    purchase = sumPaid;
                }
                

                var resultPayment = context.Database.SqlQuery<Payment>($@"select ID, CustomerIDNumber, PaymentAmount, SupplierID, Date from Payments where Payments.CustomerIDNumber = {cstmr.IDNumber}");

                List<Payment> cpList = resultPayment.ToList();

                double payment = 0;
                if (cpList.Count() > 0)
                {
                    double sumPayment = 0;
                    foreach (Payment cp in cpList)
                    {
                        sumPayment += cp.PaymentAmount;
                    }
                    payment = sumPayment;
                }
                

                var queryDebt = context.CustomerDebts.Find(cstmr.IDNumber);

                double debt = 0;
                //Check if there is any debt
                if (queryDebt != null)
                {
                    debt = queryDebt.DebtAmount;
                }

                rl.Add(new ReportList { Name = c.Name, LastName = c.LastName, Purchase = purchase, PaymentAmount = payment, Debt = debt });
            }

            sheetcreate.Cells.LoadFromCollection(rl);
            excel.Save();
        }

        public void AllPdf(List<Customer> cList)
        {
            var context = new MarketDBContext();

            // Create a A4 sized document
            Document document = new Document(PageSize.A4, 30, 30, 30, 30);
            String date = DateTime.Now.ToString("HHmmss");

            // Set file path to desktop and create filename
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = "/Toplu_musteri_raporu"+ date + ".pdf";

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path + fileName, FileMode.Create));

            if (document.IsOpen() == false)
            {
                document.Open();
                //Report title
                string infoText = $"Toplu musteri raporu\n";

                Paragraph paragr = new Paragraph(infoText);
                paragr.Alignment = Element.ALIGN_CENTER;
                paragr.Font = FontFactory.GetFont(FontFactory.HELVETICA, 12f, BaseColor.GREEN);
                paragr.SpacingAfter = 30;
                document.Add(paragr);

                // Create a table( 5 columns )
                PdfPTable table = new PdfPTable(5);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                List<PdfPCell> cellList = new List<PdfPCell>();


                cellList.Add(new PdfPCell(new Phrase("Musteri adi")));
                cellList.Add(new PdfPCell(new Phrase("Musteri soyadi")));
                cellList.Add(new PdfPCell(new Phrase("Toplam satis")));
                cellList.Add(new PdfPCell(new Phrase("Toplam odeme")));
                cellList.Add(new PdfPCell(new Phrase("Toplam Kalan")));

                // Add all customers to the table and calculate

                foreach (Customer c in cList)
                {
                    cellList.Add(new PdfPCell(new Phrase(c.Name)));
                    cellList.Add(new PdfPCell(new Phrase(c.LastName)));

                    Customer cstmr = context.Customers.Find(c.IDNumber);

                    var resultSale = context.Database.SqlQuery<ReportItem>($@"EXEC CustomerSales @InputID = {cstmr.IDNumber};");

                    List<ReportItem> riList = resultSale.ToList();

                    if (riList.Count == 0)
                    {
                        cellList.Add(new PdfPCell(new Phrase("No purchase")));
                    }
                    else
                    {
                        double sumPaid = 0;
                        foreach (ReportItem ri in riList)
                        {
                            sumPaid += (ri.Amount * ri.Price);
                        }
                        cellList.Add(new PdfPCell(new Phrase(sumPaid.ToString())));
                    }

                    var resultPayment = context.Database.SqlQuery<Payment>($@"select ID, CustomerIDNumber, PaymentAmount, SupplierID, Date from Payments where Payments.CustomerIDNumber = {cstmr.IDNumber}");

                    List<Payment> cpList = resultPayment.ToList();

                    if (cpList.Count() == 0)
                    {
                        cellList.Add(new PdfPCell(new Phrase("No payment")));
                    }
                    else
                    {
                        double sumPayment = 0;
                        foreach (Payment cp in cpList)
                        {
                            sumPayment += cp.PaymentAmount;
                        }
                        cellList.Add(new PdfPCell(new Phrase(sumPayment.ToString())));
                    }

                    var queryDebt = context.CustomerDebts.Find(cstmr.IDNumber);

                    //Check if there is any debt
                    if (queryDebt != null)
                    {
                        cellList.Add(new PdfPCell(new Phrase(queryDebt.DebtAmount.ToString())));
                    }
                    else
                    {
                        cellList.Add(new PdfPCell(new Phrase("No Debt")));
                    }
                }

                foreach (PdfPCell cell in cellList)
                {
                    cell.Border = 0;
                    cell.FixedHeight = 25f;
                    cell.HorizontalAlignment = 1;
                    table.AddCell(cell);
                }

                document.Add(table);
                document.Close();
            }
        }
    }
}
public class ReportItem
{

    public string Name { get; set; }
    public double Price { get; set; }
    public double Amount { get; set; }
    ReportItem() { }

}

public class ReportList
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public double Purchase { get; set; }
    public double PaymentAmount { get; set; }
    public double Debt { get; set; }
    public ReportList()
    {

    }
    public ReportList(string Name, string LastName, double Purchase, double PaymentAmount, double Debt) { }
}