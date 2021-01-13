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
    /// Interaction logic for SupplierPage.xaml
    /// </summary>
    public partial class SupplierPage : Page
    {
        public SupplierPage()
        {
            InitializeComponent();
            RefreshList(SupplierList);
            SilButton.IsEnabled = false;
        }
        private void EkleButtonClicked(object sender, RoutedEventArgs e)
        {
            var context = new MarketDBContext();

            if (NameText.Text != "" && PhoneNumberText.Text != "")
            {
                string InputName = NameText.Text;
                string InputPhoneNumber = PhoneNumberText.Text;
                var query = context.Suppliers.Where(s => s.Name == InputName);
                if (query.Count() == 0)
                {
                    //if customer was not registered.
                    Supplier sup = new Supplier(InputName, InputPhoneNumber);

                    context.Suppliers.Add(sup);
                    context.SaveChanges();
                    RefreshList(SupplierList);
                    NameText.Text = String.Empty;
                    PhoneNumberText.Text = String.Empty;
                }
                else
                {
                    MessageBox.Show("Supplier with the given name exists.");
                }
            }
            else
            {
                MessageBox.Show("Please fill name and phone number fields.");
            }
                
        }
        private void SilButtonClicked(object sender, RoutedEventArgs e)
        {
            var context = new MarketDBContext();

            var selectedItems = SupplierList.SelectedItems;
            if(selectedItems.Count > 0)
            {
                string message = $"Do you want to delete selected {selectedItems.Count} suppliers?";
                string caption = "Confirmation";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
                {
                    foreach (Supplier supplier in selectedItems)
                    {
                        Supplier sup = context.Suppliers.Find(supplier.ID);
                        context.Suppliers.Remove(sup);
                    }
                    context.SaveChanges();
                    RefreshList(SupplierList);
                }
                else
                {
                    return; 
                }
            }
        }

        public void RefreshList(ListView List)
        {
            var context = new MarketDBContext();

            List.ItemsSource = context.Suppliers.ToList<Supplier>();
        }

        private void GoBackButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateTo(new SalePage());
        }

        private void HomeButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateToMain();
        }

        private void SupplierList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count - e.RemovedItems.Count > 0)
            {
                SilButton.IsEnabled = true;
            }
            else
            {
                SilButton.IsEnabled = false;
            }
        }
    }
}
