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
using System.Data;
using System.Data.SqlClient;
using Market.Entities;

namespace Market
{
    /// <summary>
    /// Interaction logic for DebtWindow.xaml
    /// </summary>
    public partial class DebtWindow : Window
    {
        public long selectedCustomerIDNumber { get; set; }
        public DebtWindow()
        {
            var context = new MarketDBContext();
            List<Customer> ls = context.Customers.ToList();
            
            InitializeComponent();
            CustomerList.ItemsSource = ls;
        }
        private void AraButtonClicked(object sender, RoutedEventArgs e)
        {
            long InputIDNumber = long.Parse(IDNumberText.Text);

            if (IDNumberText.Text != "")
            {
                var context = new MarketDBContext();
                
                var query = context.Customers.Where(s => s.IDNumber == InputIDNumber);
                if(query.Count() != 0)
                {
                    Customer cstmr = context.Customers.Find(InputIDNumber);

                    this.selectedCustomerIDNumber = cstmr.IDNumber;
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong ID!");
                }

            }
            else
            {
                MessageBox.Show("Sign customer's ID to textbox!");
            }
        }
        private void SecButtonClicked(object sender, RoutedEventArgs e)
        {
            var selection = CustomerList.SelectedItem;
            // Selection not being null means we can continue to the payment
            if (selection != null)
            {
                // Hand over the customerIDNumber info to the payment
                var customer = (Customer)selection;
                this.selectedCustomerIDNumber = customer.IDNumber;
                // Set dialogresult to true so we can move on to the payment
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Selection");
            }
        }
    }
}
