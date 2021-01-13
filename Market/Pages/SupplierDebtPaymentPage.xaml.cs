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
                    RefreshList(PaymentList);
                    RefreshSum(SumLabel);
                }
            }
        }
        private int _SelectedSupplierID;
        private void OdeButtonClicked(object sender, EventArgs e)
        {
           

            if (PaymentAmountText.Text != "")
            {
                var context = new MarketDBContext();

                //                                                                                  This operation removes the currency part of the string
                if (double.Parse(PaymentAmountText.Text) <= double.Parse(SumLabel.Content.ToString().Remove(SumLabel.Content.ToString().Length -1, 1)))
                {
                    double InputPaymentAmount = double.Parse(PaymentAmountText.Text);

                    Payment p = new Payment(0, SelectedSupplierID, InputPaymentAmount, DateTime.Now);
                    context.Payments.Add(p);

                    context.SaveChanges();

                    RefreshList(PaymentList);
                    RefreshSum(SumLabel);

                    PaymentAmountText.Text = "";
                }
                else
                {
                    MessageBox.Show("Wrong amount!");
                }
            }
        }
        public void RefreshList(ListView List)
        {
            var context = new MarketDBContext();

            List.ItemsSource = context.Payments.Where(p => p.SupplierID == SelectedSupplierID).ToList<Payment>();
        }
        public void RefreshSum(Label label)
        {
            var context = new MarketDBContext();
            Supplier sup = context.Suppliers.Find(SelectedSupplierID);

            var storages = context.Storages.Where(s => s.SupplierID == SelectedSupplierID).ToList();
            var payments = context.Payments.Where(p => p.SupplierID == SelectedSupplierID).ToList();
            double sum = 0.0;
            foreach(Storage storage in storages)
            {
                sum += (storage.Amount * storage.PriceForUnit);
            }
            foreach(Payment payment in payments)
            {
                sum -= payment.PaymentAmount;
            }

            // Set content of the label to sum
            SumLabel.Content = sum.ToString() + " ₺";
            SupplierLabel.Content = sup.Name;
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
