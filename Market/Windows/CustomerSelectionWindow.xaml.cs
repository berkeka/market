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
using System.Windows.Shapes;
using Market.Entities;

namespace Market
{
    /// <summary>
    /// Interaction logic for CustomerSelectionWindow.xaml
    /// </summary>
    public partial class CustomerSelectionWindow : Window
    {
        public long selectedCustomerIDNumber { get; set; }
        public CustomerSelectionWindow()
        {
            InitializeComponent();
            RefreshList("");
        }
        private void SecButtonClicked(object sender, RoutedEventArgs e)
        {
            var selection = CustomerList.SelectedItem;
            // Selection not being null means we can continue to the cart
            if(selection != null)
            {
                // Hand over the customerID info to the cart
                var customer = (Customer)selection;
                this.selectedCustomerIDNumber = customer.IDNumber;
                // Set dialogresult to true so we can move on to the cart
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Listeden müşteri seçin");
            }
            
        }

        private void RefreshList(string input)
        {
            // Refresh the list with customers with the given input text
            var context = new MarketDBContext();
            List<Customer> ls = context.Customers.Where(c => c.IDNumber.ToString().Contains(input)).ToList();
            CustomerList.ItemsSource = ls;
        }

        private void IDNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            if(IDNumberText != null)
            {
                RefreshList(IDNumberText.Text);
            }
        }
    }
}
