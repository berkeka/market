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
using Market.Utils;

namespace Market.Pages
{
    /// <summary>
    /// Interaction logic for ReportMainPage.xaml
    /// </summary>
    public partial class ReportMainPage : Page
    {

        public ReportMainPage()
        {
            InitializeComponent();
        }

        private void ProfitButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateTo(new ProfitPage());
        }

        private void ProductSaleButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateTo(new SaleCountReportPage());
        }
        private void TrendButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateTo(new TrendChartPage());
        }

        private void CustomerReportButtonClicked(object sender, RoutedEventArgs e)
        {
            var context = new MarketDBContext();
            // Show customer selection window
            CustomerSelectionWindow csw = new CustomerSelectionWindow();
            csw.ShowDialog();

            if (csw.selectedCustomerIDNumber == 0) { return; }
            FileSelectionWindow fsw = new FileSelectionWindow();
            fsw.ShowDialog();
            if (fsw.selection == 0) { return; }
            ReportFileGenerator fileGenerator = new ReportFileGenerator();
            fileGenerator.SingleCustomerReport(csw.selectedCustomerIDNumber, fsw.selection);
        }
        private void MultiCustomerReportButtonClicked(object sender, RoutedEventArgs e)
        {
            var context = new MarketDBContext();
            var query = context.CustomerDebts;

            if (!query.Any()) { MessageBox.Show("There is no customer debt!"); return; }
            FileSelectionWindow fsw = new FileSelectionWindow();
            fsw.ShowDialog();
            if (fsw.selection == 0) { return; }
            ReportFileGenerator fileGenerator = new ReportFileGenerator();
            fileGenerator.AllCustomerReport(fsw.selection);
        }
        private void HomeButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateToMain();
        }
    }
}
