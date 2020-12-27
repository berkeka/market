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
    /// Interaction logic for SaleCartWindow.xaml
    /// </summary>
    public partial class SaleCartWindow : Page
    {
        public SaleCartWindow()
        {
            InitializeComponent();
        }

        private void EkleButtonClicked(object sender, RoutedEventArgs e)
        {
            // Add item to list 
            // Calculate sum value and change the label

        }

        private void CikarButtonClicked(object sender, RoutedEventArgs e)
        {
            // Remove the selected item from the list 
            // Calculate sum value and change the label

        }
        private void TamamlaButtonClicked(object sender, RoutedEventArgs e)
        {
            // Check if no problems exists
        }


    }
}
