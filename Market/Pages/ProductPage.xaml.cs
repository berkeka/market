using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Market.Entities;

namespace Market.Pages
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public bool inUpdateState = false;
        public ProductPage()
        {
            
            InitializeComponent();
            RefreshList(ProductList);
            DegistirButton.IsEnabled = false;
            
        }

        private void EkleButtonClicked(object sender, RoutedEventArgs e)
        {
            var context = new MarketDBContext();

            // Check inputs for exceptions
            if (BarcodeText.Text == null || NameText.Text == null || PriceText.Text == null) { MessageBox.Show("Empty field!"); return; }
            string InputBarcode = BarcodeText.Text;
            string InputName = NameText.Text;
            int InputPrice = int.Parse(PriceText.Text);
            double InputWarningPrice = 0.0;
            if (WarningLimitText.Text != null)
            {
                InputWarningPrice = double.Parse(WarningLimitText.Text);
            }
            
            // If we are in the update state
            // Get selected product from database context
            // Update its values and save changes down below
            if (inUpdateState)
            {
                Product product = (Product)ProductList.SelectedItem;

                var productWithGivenBarcode = context.Products.Where(prd => prd.Barcode == InputBarcode);
                if (productWithGivenBarcode.Any() && productWithGivenBarcode.First().Barcode != product.Barcode) {MessageBox.Show("Product with the given Barcode exists"); return;}

                var dbProduct = context.Products.Find(product.ID);
                dbProduct.Name = InputName;
                dbProduct.Barcode = InputBarcode;
                dbProduct.Price = InputPrice;
                dbProduct.WarningLimit = InputWarningPrice;
                
                ClearFields();
            }
            else
            {
                // This part of the code handles adding products

                if (context.Products.Where(prd => prd.Barcode == InputBarcode).Any()) { MessageBox.Show("Product with the given Barcode exists"); return; }

                Product p = new Product(InputBarcode, InputName, InputPrice, InputWarningPrice);
                p.WarningLimit = InputWarningPrice;

                context.Products.Add(p);
            }
            context.SaveChanges();
            RefreshList(ProductList);

            NameText.Text = String.Empty;
            BarcodeText.Text = String.Empty;
            PriceText.Text = String.Empty;
            WarningLimitText.Text = String.Empty;
        }

        public void RefreshList(ListView List)
        {
            var context = new MarketDBContext();

            List.ItemsSource = context.Products.ToList<Product>();
        }
        private void DegistirButtonClicked(object sender, RoutedEventArgs e)
        {
            // Fill the corresponding files with the product info
            Product product = (Product)ProductList.SelectedItem;
            NameText.Text = product.Name;
            BarcodeText.Text = product.Barcode;
            PriceText.Text = product.Price.ToString();
            WarningLimitText.Text = product.WarningLimit.ToString();

            // Disable degistir button and set the stare to update
            DegistirButton.IsEnabled = false;
            inUpdateState = true;

        }

        private void TemizleButtonClicked(object sender, RoutedEventArgs e)
        {
            ProductList.UnselectAll();
            ClearFields();
            DegistirButton.IsEnabled = false;
            inUpdateState = false;
        }

        private void ClearFields()
        {
            NameText.Text = "";
            BarcodeText.Text = "";
            PriceText.Text = "";
            WarningLimitText.Text = "";
            inUpdateState = false;
        }
        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If we are in update state with an item already chosen
            // We cant change the selected item until we clear our selection
            List<Product> pl = ProductList.Items.Cast<Product>().ToList();
            if (inUpdateState && ProductList.SelectedIndex != -1)
            {
                foreach (Product p in e.RemovedItems)
                {
                    int index = pl.IndexOf(p);
                    ProductList.SelectedItem = null;
                    ProductList.SelectedItem = p;
                }
            }
            else
            {
                // After selection change event occurred 
                // Check we actually selected an item and did not deselect 
                // If so change button is disabled
                if (e.AddedItems.Count > 0) { DegistirButton.IsEnabled = true; }
            }
            
        }
        //Go back to sale page
        private void GoBackButtonClicked(object sender, RoutedEventArgs e)
        {
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            SalePage newPage = new SalePage();

            main.Title = newPage.Title;
            main.Content = newPage.Content;
        }
        private void HomeButtonClicked(object sender, RoutedEventArgs e)
        {
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            MainWindow new_main = new MainWindow();

            main.Title = new_main.Title;
            main.Content = new_main.Content;
            // Close the newly initialized window
            new_main.Close();
        }
    }
}
