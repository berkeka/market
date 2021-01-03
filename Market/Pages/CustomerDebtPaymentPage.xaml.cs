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

namespace Market.Pages
{
    /// <summary>
    /// Interaction logic for CustomerDebtPaymentPage.xaml
    /// </summary>
    public partial class CustomerDebtPaymentPage : Page
    {
        public long SelectedCustomerID
        {
            get
            {
                return _SelectedCustomerID;
            }
            set
            {
                _SelectedCustomerID = value;
                if (_SelectedCustomerID != 0)
                {
                    var context = new MarketDBContext();
                    Customer c = context.Customers.Find(SelectedCustomerID);
                    CustomerLabel.Content = "Seçilmiş Müşteri: " + c.Name + " " + c.LastName;
                }
            }
        }
        private long _SelectedCustomerID;
        public CustomerDebtPaymentPage()
        {
            InitializeComponent();
        }
        private void OdeButtonClicked(object sender, EventArgs e)
        {

        }
    }
}
