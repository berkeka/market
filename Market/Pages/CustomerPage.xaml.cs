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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        public CustomerPage()
        {
            InitializeComponent();
            RefreshList(CustomerList);
        }
        
        private void EkleButtonClicked(object sender, RoutedEventArgs e)
        {            
            var context = new MarketDBContext();            

            if (NameText.Text != "" && LastNameText.Text != "" && IDNumberText.Text != "")
            {
                string InputName = NameText.Text;
                string InputLastName = LastNameText.Text;
                long InputIDNumber = long.Parse(IDNumberText.Text);

                var query = context.Customers.Where(s => s.IDNumber == InputIDNumber);
                if (query.Count() == 0) 
                {
                    //if customer was not registered.
                    Customer cst = new Customer(InputName, InputLastName, InputIDNumber);

                    context.Customers.Add(cst);
                    context.SaveChanges();
                    RefreshList(CustomerList);
                    NameText.Text = String.Empty;
                    LastNameText.Text = String.Empty;
                    IDNumberText.Text = String.Empty;
                }
                else
                {
                    MessageBox.Show("Customer was registered before.");
                }
            }
            else 
            {
                MessageBox.Show("Please fill name, lastname and id!");
            }
        }

        public void RefreshList(ListView List)
        {
            var context = new MarketDBContext();

            List.ItemsSource = context.Customers.ToList<Customer>();
        }

        private void HomeButtonClicked(object sender, RoutedEventArgs e)
        {
            // Get the instance of the MainWindow
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            // Initialize new MainWindow
            MainWindow new_main = new MainWindow();

            // Rebuild mainWindow
            main.Title = new_main.Title;
            main.Content = new_main.Content;
            // Close the newly initialized window
            new_main.Close();
        }
    }
}
