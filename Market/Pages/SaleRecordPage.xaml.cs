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

            RefreshList("");
        }
        private void SilButtonClicked(object sender, RoutedEventArgs e)
        {
            var context = new MarketDBContext();

            var selection = SaleRecordList.SelectedItem;
            if(selection == null) { MessageBox.Show("Seçim yapılmadı"); return; }

            SaleItem saleItem = (SaleItem)selection;
            string message = $"Do you want to delete selected sale?";
            string caption = "Confirmation";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {
                //Ask for login
                var returnValue = false;

                LogInWindow Login = new LogInWindow();
                returnValue = (bool)Login.ShowDialog();

                if (returnValue == false) { return; }

                Sale sale = context.Sales.Find(saleItem.ID);
                context.Sales.Remove(sale);
                
                context.SaveChanges();
                SaleIDText.Text = String.Empty;
                RefreshList("");
            }
            else
            {
                return;
            }

        }

        private void RefreshList(string input)
        {
            var context = new MarketDBContext();

            var querySale = context.Sales;
            var queryCstmr = context.Customers;

            if (querySale.Any())
            {
                List<SaleItem> si = new List<SaleItem>();
                foreach (Sale sale in querySale.Where(c => c.ID.ToString().Contains(input)).ToList())
                {
                    var cstmr = queryCstmr.Find(sale.CustomerIDNumber);
                    string cFullName = cstmr == null ? "Peşin satış" : cstmr.Name + " " + cstmr.LastName;
                    si.Add(new SaleItem() { FullName = cFullName, ID = sale.ID, Date = sale.Date });
                }
                SaleRecordList.ItemsSource = si.OrderBy(i => i.Date);
            }
        }

        private void SaleIDText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SaleIDText != null)
            {
                RefreshList(SaleIDText.Text);
            }
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
        public class SaleItem
        {
            public string FullName { get; set; }
            public int ID { get; set; }
            public DateTime Date { get; set; }
        }
    }
}
