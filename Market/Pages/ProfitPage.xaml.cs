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
    /// Interaction logic for ProfitPage.xaml
    /// </summary>
    public partial class ProfitPage : Page
    {
        public ProfitPage()
        {
            InitializeComponent();

            var context = new MarketDBContext();

            var query = context.Sales;
            
            if (query.Any())
            {
                DatePicker.DisplayDateStart = query.First().Date;
                DateTime date = DateTime.Now;

                if (DatePicker.SelectedDate != null)
                {
                    date = (DateTime)DatePicker.SelectedDate;
                }

                var querySale = context.Sales;
                var queryPayment = context.Payments;

                if (date != null)
                {
                    querySale.Where(d => d.Date < date);
                    queryPayment.Where(d => d.Date < date);
                }

                double sumSale = 0;
                //Summary of all sales until selected date
                foreach (Sale sale in querySale.Where(s => s.CustomerIDNumber == null).ToList())
                {
                    var queryProdSale = context.ProductSales.Where(p => p.SaleID == sale.ID);

                    foreach (ProductSale ps in queryProdSale.ToList())
                    {
                        var prod = context.Products.Find(ps.ProductID);
                        sumSale += ps.Amount * prod.Price;
                    }
                }

                double sumCustPaym = 0;
                var queryCustPaym = context.Payments.Where(c => c.CustomerIDNumber != 0);

                foreach (Payment paymCust in queryCustPaym.ToList())
                {
                    sumCustPaym += paymCust.PaymentAmount;
                }

                double sumSupplPaym = 0;
                var querySupplPaym = context.Payments.Where(c => c.SupplierID != 0);
                foreach (Payment paymSuppl in querySupplPaym.ToList())
                {
                    sumSupplPaym += paymSuppl.PaymentAmount;
                }

                //Calculate profit
                double profit = sumSale + sumCustPaym - sumSupplPaym;

                ProfitLabel.Content = profit;
                
                Brush color;
                color = (profit > 0) ? Brushes.Green : Brushes.Red ;
                ProfitLabel.Foreground = color;
            }

        }
        //Go back to report main page
        private void GoBackButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateTo(new ReportMainPage());
        }
        private void HomeButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateToMain();
        }
    }
}
