﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Market.Entities;

namespace Market.Pages
{
    /// <summary>
    /// Interaction logic for ProductIntakePage.xaml
    /// </summary>
    public partial class ProductIntakePage : Page
    {
        public ProductIntakePage()
        {
            InitializeComponent();
        }

        private void DosyaButtonClicked(object sender, RoutedEventArgs e)
        {

            // Input Format
            // 1- DispatchID
            // 2- SupplierID
            // 3- Barcode, Price, Amount
            // 4- Barcode, Price, Amount

            OpenFileDialog fd = new OpenFileDialog();

            fd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            //fd.InitialDirectory = "";

            if (fd.ShowDialog() == DialogResult.OK) 
            {
                var context = new MarketDBContext();
                // Get text from selected File
                var InputText = File.ReadAllText(fd.FileName);
                // Split the text into rows
                var Rows = InputText.Split('\n');
                // If there are more than one row 
                if(Rows.Length > 1)
                {
                    DispatchNoLabel.Content = "";
                    SupplierNameLabel.Content = "";
                    ProductList.Items.Clear();
                    try
                    {
                        // Get the Dispatch id from the file
                        int InputDispatchID = int.Parse(Rows[0]);
                        int SupplierID = int.Parse(Rows[1]);
                        var supplier = context.Suppliers.Find(SupplierID);
                        if(supplier == null) { System.Windows.MessageBox.Show("Verilen ID'ye sahip kayıtlı tedarikçi yok."); return; }

                        // Set the dispatch id and supplier name values to the corresponding labels
                        DispatchNoLabel.Content = Rows[0];
                        SupplierNameLabel.Content = supplier.Name;

                        // Loop through rows
                        for (int i = 2; i < Rows.Length; i++)
                        {
                            // Split the row into values
                            var Values = Rows.ElementAt(i).Split(',');
                            // Parse values
                            string Barcode = Values.ElementAt(0);
                            double Price = double.Parse(Values.ElementAt(1));
                            double Amount = double.Parse(Values.ElementAt(2));
                            // Find name of the product using given barcode
                            string Name = context.Products.Where(s => s.Barcode == Barcode).First().Name;

                            ProductItem pi = new ProductItem();

                            pi.Name = Name;
                            pi.Barcode = Barcode;
                            pi.Price = Price;
                            pi.Amount = (int)Amount;

                            ProductList.Items.Add(pi);
                        }
                    }
                    catch (Exception ex)
                    {
                        // If a problem occurs while parsing the file give a warning
                        // and clear the list
                        Console.WriteLine(ex.Message);
                        ProductList.Items.Clear();
                        DispatchNoLabel.Content = "";
                        System.Windows.MessageBox.Show("Hatalı girdi dosyası");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Hatalı girdi dosyası");
                }
            }
                
        }

        private void OnaylaButtonClicked(object sender, RoutedEventArgs e)
        {
            var context = new MarketDBContext();
            int DispatchID = int.Parse(DispatchNoLabel.Content.ToString());
            string SupplierName = (string)SupplierNameLabel.Content;

            Supplier supplier = context.Suppliers.Where(s => s.Name == SupplierName).First();

            // If there are products on the list and dispatchID is new
            if(context.Storages.Where(x => x.DispatchNoteID == DispatchID).Any()) { System.Windows.MessageBox.Show("Verilen ID'ye sahip sistemde kayıtlı irsaliye var."); return; }
            if(ProductList.Items.Count > 0 )
            {
                for(int i = 0; i < ProductList.Items.Count; i++)
                {
                    ProductItem item = (ProductItem)ProductList.Items.GetItemAt(i);
                    Storage s = new Storage(DispatchID, supplier.ID, item.Barcode, item.Price, item.Amount, DateTime.Now);
                    context.Storages.Add(s);

                    var query = context.Stocks.Where(t => t.Barcode == item.Barcode);
                    //Product already exists in stock
                    if(query.Count() != 0)
                    {
                        //Add how many came in to stock
                        Stock stck = context.Stocks.Find(item.Barcode);
                        stck.Amount += item.Amount;
                    }
                    //Product doesn't exist in stock
                    else
                    {
                        Stock stck = new Stock(item.Barcode, item.Amount);
                        context.Stocks.Add(stck);
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Dosya seçilmedi");
            }
            ProductList.Items.Clear();
            DispatchNoLabel.Content = "";
            context.SaveChanges();
        }
        //Go back to sale page
        private void GoBackButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateTo(new SalePage());
        }
        private void HomeButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateToMain();
        }
    }
}
