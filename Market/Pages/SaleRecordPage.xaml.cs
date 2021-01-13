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
    /// Interaction logic for SaleRecordPage.xaml
    /// </summary>
    public partial class SaleRecordPage : Page
    {
        public SaleRecordPage()
        {
            InitializeComponent();

            var context = new MarketDBContext();

            var querySale = context.Sales;
            var queryCstmr = context.Customers;

            if (querySale.Any())
            {
                List<SaleItem> si = new List<SaleItem>();
                foreach(Sale sale in querySale.ToList())
                {
                    var cstmr = queryCstmr.Find(sale.CustomerIDNumber);
                    string cFullName = cstmr.Name + " " + cstmr.LastName;
                    si.Add(new SaleItem() { FullName = cFullName, ID = sale.ID, Date = sale.Date });
                }
                SaleRecordList.ItemsSource = si.OrderBy(i => i.Date);
            }
        }
        private void SilButtonClicked(object sender, RoutedEventArgs e)
        {

        }
        public class SaleItem
        {
            public string FullName { get; set; }
            public int ID { get; set; }
            public DateTime Date { get; set; }
        }
        //Go back to sale page
        private void GoBackButtonClicked(object sender, RoutedEventArgs e)
        {
            MainWindow main = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            SalePage newPage = new SalePage();

            main.Title = newPage.Title;
            main.Content = newPage.Content;
        }
        private void HomeButtonClicked(object sender, RoutedEventArgs e)
        {
            MainWindow main = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            MainWindow new_main = new MainWindow();

            main.Title = new_main.Title;
            main.Content = new_main.Content;
            // Close the newly initialized window
            new_main.Close();
        }
    }
}
