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

namespace Market
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Page
    {
        public CustomerWindow()
        {
            InitializeComponent();
        }

        private void EkleButtonClicked(object sender, RoutedEventArgs e)
        {
            //Add customer to the customers list if its already exists don't.
        }
    }
}
