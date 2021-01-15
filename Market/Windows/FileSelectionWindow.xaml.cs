using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Market
{
    /// <summary>
    /// Interaction logic for FileSelectionWindow.xaml
    /// </summary>
    public partial class FileSelectionWindow : Window
    {
        public int selection { get; set; }
        public FileSelectionWindow()
        {
            selection = 0;
            InitializeComponent();

        }

        private void PdfClicked(object sender, RoutedEventArgs e)
        {
            selection = 1;
            this.Close();
        }

        private void ExcelClicked(object sender, RoutedEventArgs e)
        {
            selection = 2;
            this.Close();
        }
    }
}
