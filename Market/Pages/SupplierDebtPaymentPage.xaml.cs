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
    /// Interaction logic for SupplierDebtPaymentPage.xaml
    /// </summary>
    public partial class SupplierDebtPaymentPage : Page
    {
        public SupplierDebtPaymentPage()
        {
            InitializeComponent();
        }

        public int SelectedSupplierID
        {
            get
            {
                return _SelectedSupplierID;
            }
            set
            {
                _SelectedSupplierID = value;
                if (_SelectedSupplierID != 0)
                {

                    RefreshSum(SumLabel);
                }
            }
        }
        private int _SelectedSupplierID;
        private void OdeButtonClicked(object sender, EventArgs e)
        {

            RefreshList(PaymentList);
            RefreshSum(SumLabel);
        }
        public void RefreshList(ListView List)
        {
           
        }
        public void RefreshSum(Label label)
        {
            var context = new MarketDBContext();

            var storages = context.Storages.Where(s => s.SupplierID == SelectedSupplierID).ToList();
            double sum = 0.0;
            foreach(Storage storage in storages)
            {
                sum += (storage.Amount * storage.PriceForUnit);
            }
            // Set content of the label to sum
            SumLabel.Content = sum.ToString() + "₺";
        }

        private void GoBackButtonClicked(object sender, RoutedEventArgs e)
        {
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            SalePage newPage = new SalePage();

            main.Title = newPage.Title;
            main.Content = newPage.Content;
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
