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
    /// Interaction logic for SupplierSelectionWindow.xaml
    /// </summary>
    public partial class SupplierSelectionWindow : Window
    {
        public int selectedSupplierID { get; set; }
        public SupplierSelectionWindow()
        {
            InitializeComponent();
            RefreshList("");
        }
        
        private void SecButtonClicked(object sender, RoutedEventArgs e)
        {
            var selection = SupplierList.SelectedItem;
            // Selection not being null means we can continue to the cart
            if (selection != null)
            {
                // Hand over the customerID info to the cart
                var supplier = (Supplier)selection;
                this.selectedSupplierID = supplier.ID;
                // Set dialogresult to true so we can move on to the cart
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Listeden tedarikçi seçiniz");
            }

        }

        private void RefreshList(string input)
        {
            // Refresh the list with customers with the given input text
            var context = new MarketDBContext();
            List<Supplier> ls = context.Suppliers.Where(s => s.Name.Contains(input)).ToList();
            SupplierList.ItemsSource = ls;
        }

        private void SupplierTextChanged(object sender, TextChangedEventArgs e)
        {
            if (SupplierNameText != null)
            {
                RefreshList(SupplierNameText.Text);
            }
        }
    }
}
