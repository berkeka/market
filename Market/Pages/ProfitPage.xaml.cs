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
                
                // If selected date isn't possible
                if (query.First().Date >= DatePicker.SelectedDate) { MessageBox.Show("Selected date should be after first sale's date.");}
                else
                {
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
                    foreach(Sale sale in querySale.ToList())
                    {
                        var queryProdSale = context.ProductSales.Where(p => p.SaleID == sale.ID);
                        
                        foreach(ProductSale ps in queryProdSale.ToList())
                        {
                            var prod = context.Products.Find(ps.ProductID);
                            sumSale += ps.Amount * prod.Price;
                        }
                    }

                    double sumCustPaym = 0;
                    double sumSupplPaym = 0;
                    foreach(Payment payment in queryPayment.ToList())
                    {
                        var queryCustPaym = context.Payments.Where(c => c.CustomerIDNumber != 0);

                        foreach(Payment paymCust in queryCustPaym.ToList())
                        {
                            sumCustPaym += paymCust.PaymentAmount;
                        }

                        var querySupplPaym = context.Payments.Where(c => c.SupplierID != 0);

                        foreach(Payment paymSuppl in querySupplPaym.ToList())
                        {
                            sumSupplPaym += paymSuppl.PaymentAmount;
                        }
                    }

                    //Calculate profit
                    double profit = sumSale + sumCustPaym - sumSupplPaym;

                    ProfitLabel.Content = profit;
                }                
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
