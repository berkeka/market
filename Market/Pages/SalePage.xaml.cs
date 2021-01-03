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
using Market.Pages;

namespace Market
{
    /// <summary>
    /// Interaction logic for SaleWindow.xaml
    /// </summary>
    public partial class SalePage : Page
    {
        public SalePage()
        {
            InitializeComponent();
        }
        private void PesinButtonClicked(object sender, RoutedEventArgs e)
        {
            // Gets the instance of MainWindow
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            SaleCartPage NewWindow = new SaleCartPage();

            main.Title = NewWindow.Title;
            main.Content = NewWindow;

        }
        private void CariButtonClicked(object sender, RoutedEventArgs e)
        {
            CustomerSelectionWindow csw = new CustomerSelectionWindow();

            bool returnValue = (bool)csw.ShowDialog();

            if(returnValue == true)
            {
                long sc = csw.selectedCustomerIDNumber;

                MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

                SaleCartPage NewWindow = new SaleCartPage();
                NewWindow.SelectedCustomerIDNumber = sc;

                main.Title = NewWindow.Title;
                main.Content = NewWindow;
            }
        }

        private void MusteriButtonClicked(object sender, RoutedEventArgs e)
        {
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            CustomerPage NewWindow = new CustomerPage();

            main.Title = NewWindow.Title;
            main.Content = NewWindow;
        }

        private void UrunButtonClicked(object sender, RoutedEventArgs e)
        {
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            ProductPage NewPage = new ProductPage();

            main.Title = NewPage.Title;
            main.Content = NewPage;
        }

        private void UrunGirisButtonClicked(object sender, RoutedEventArgs e)
        {
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            ProductIntakePage NewPage = new ProductIntakePage();

            main.Title = NewPage.Title;
            main.Content = NewPage;
        }
        private void DebtButtonClicked(object sender, RoutedEventArgs e)
        {
            DebtWindow dw = new DebtWindow();

            bool returnValue = (bool)dw.ShowDialog();

            if (returnValue == true)
            {
                long sc = dw.selectedCustomerIDNumber;

                MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

                CustomerDebtPaymentPage NewWindow = new CustomerDebtPaymentPage();
                NewWindow.SelectedCustomerID = sc;

                main.Title = NewWindow.Title;
                main.Content = NewWindow;
            }
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
