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
    /// Interaction logic for DebtWindow.xaml
    /// </summary>
    public partial class DebtWindow : Window
    {
        public int selectedCustomerID { get; set; }
        public DebtWindow()
        {
            var context = new MarketDBContext();
            List<CustomerDebt> ls = context.CustomerDebts.ToList();

            InitializeComponent();
            CustomerDebtList.ItemsSource = ls;
        }
        private void AraButtonClicked(object sender, RoutedEventArgs e)
        {

        }
        private void SecButtonClicked(object sender, RoutedEventArgs e)
        {
            var selection = CustomerDebtList.SelectedItem;
            // Selection not being null means we can continue to the payment
            if (selection != null)
            {
                // Hand over the customerID info to the payment
                var customer = (CustomerDebt)selection;
                this.selectedCustomerID = customer.ID;
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
