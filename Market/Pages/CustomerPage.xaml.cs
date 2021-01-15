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
                    Customer cst = new Customer(InputIDNumber, InputName, InputLastName);

                    context.Customers.Add(cst);
                    context.SaveChanges();
                    RefreshList(CustomerList);
                    NameText.Text = String.Empty;
                    LastNameText.Text = String.Empty;
                    IDNumberText.Text = String.Empty;
                }
                else
                {
                    MessageBox.Show("Girilen kimlik numarasına sahip kayıtlı müşteri var!");
                }
            }
            else 
            {
                MessageBox.Show("Lütfen isim, soyisim ve kimlik numarası giriniz!");
            }
        }

        public void RefreshList(ListView List)
        {
            var context = new MarketDBContext();

            List.ItemsSource = context.Customers.ToList<Customer>();
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
