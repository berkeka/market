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
using Market.Pages;

namespace Market
{
    /// <summary>
    /// Interaction logic for SaleCartWindow.xaml
    /// </summary>
    public partial class SaleCartPage : Page
    {
        public long SelectedCustomerIDNumber 
        { 
            get 
            { 
                return _SelectedCustomerIDNumber; 
            } 
            set 
            {
                _SelectedCustomerIDNumber = value;
                if(_SelectedCustomerIDNumber != 0)
                {
                    var context = new MarketDBContext();
                    Customer c = context.Customers.Find(SelectedCustomerIDNumber);
                    CustomerLabel.Content = "Seçilmiş Müşteri: " + c.Name + " " + c.LastName;
                }
            } 
        }
        private long _SelectedCustomerIDNumber;
        public SaleCartPage()
        {
            InitializeComponent();
        }

        private void EkleButtonClicked(object sender, RoutedEventArgs e)
        {
            var context = new MarketDBContext();
            var InputBarcode = BarcodeText.Text;
            int Amount;

            // If AmountText is empty
            if(AmountText.Text == "") { MessageBox.Show("Wrong Amount!"); return; }

            // Parse amount value
            Amount = int.Parse(AmountText.Text);

            var query = context.Products.Where(s => s.Barcode == InputBarcode);
            // If no product exists with the given barcode
            if (!query.Any()) { MessageBox.Show("Couldn't find Product"); return; }
            // If amount isn't possible
            if (Amount < 1) { MessageBox.Show("Wrong Amount!"); return; }

            // Create object for new item
            Product p = query.First();
            ProductItem pi = new ProductItem(p, Amount);

            // Check if the added item was already in the list
            bool productInList = false;

            for (int i = 0; i < ItemList.Items.Count; i++)
            {
                ProductItem piFromWindow = (ProductItem)ItemList.Items.GetItemAt(i);
                          
                // If product was already in the list
                if (piFromWindow.Barcode == pi.Barcode)
                {
                    productInList = true;
                    piFromWindow.Amount += pi.Amount;
                    ItemList.Items.Remove(piFromWindow);
                    ItemList.Items.Add(piFromWindow);
                }
            }
                    
            // If product wasn't already in the list 
            if(!productInList)
            {
                ItemList.Items.Add(pi);
            }

            // Calculate new sum
            RefreshSum();
        }

        private void CikarButtonClicked(object sender, RoutedEventArgs e)
        {
            // Get selected items
            var List = ItemList.SelectedItems;

            // Loop through items
            for (int i = 0; i < List.Count; i++)
            {
                // Remove each one of them
                ItemList.Items.Remove(List[i]);
                // Calculate sum after each removal
                RefreshSum();
            }

            // Remove the selected item from the list 
            // Calculate sum value and change the label

        }
        private void TamamlaButtonClicked(object sender, RoutedEventArgs e)
        {
            
            if (ItemList.Items.IsEmpty) { MessageBox.Show("List is Empty"); return; }

            var context = new MarketDBContext();

            long CustomerIDNumber = this.SelectedCustomerIDNumber;
            Sale sale = new Sale(DateTime.Now);
            // If there is a selected Customer (This means that we will continue with the "Cari" sale)
            // Else continue with the "Peşin" sale
            if (CustomerIDNumber != 0)
            {
                sale.CustomerIDNumber = CustomerIDNumber;
            }
            // Since ID is generated automatically we save sale to the database and then get its ID
            context.Sales.Add(sale);
            context.SaveChanges();

            // Use the newly created sale's id
            Sale s = (Sale)context.Sales.Find(sale.ID);

            // For each product in the listview create a Product-Sale duo and save them to the database
            for (int i = 0; i < ItemList.Items.Count; i++)
            {
                ProductItem pi = (ProductItem)ItemList.Items.GetItemAt(i);
                ProductSale ps = new ProductSale(pi.ID, s.ID, pi.Amount);

                context.ProductSales.Add(ps);
            }

            context.SaveChanges();
            // Clear the list after sale is complete
            ItemList.Items.Clear();
            RefreshSum();
            MessageBox.Show("Completed Sale");
        }

        private void RefreshSum()
        {
            double sum = 0.0;
            // Loop through each item in the list
            for (int i = 0; i < ItemList.Items.Count; i++)
            {
                ProductItem pi = (ProductItem)ItemList.Items.GetItemAt(i);

                sum += (pi.Amount * pi.Price);
            }
            // Set content of the label to sum of all values
            SumLabel.Content = sum.ToString();
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
