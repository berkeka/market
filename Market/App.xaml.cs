using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Market.Pages;

namespace Market
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Some Application level variable to keep session data
        public static DateTime LastLogin { get; set; }
        public static int LoggedUser { get; set; }

        public static void CreateSession()
        {
            App.LastLogin = DateTime.Now;
        }

        public static void DestroySession()
        {
            App.LastLogin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            App.LoggedUser = 0;
        }

        public static void NavigateTo(Page page)
        {
            // Get the instance of the main window
            MainWindow main = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            // Change content of the main window 
            main.Title = page.Title;
            main.Content = page.Content;
        }

        public static void NavigateToMain()
        {
            // Get the instance of the main window
            MainWindow main = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            MainWindow new_main = new MainWindow();

            main.Title = new_main.Title;
            main.Content = new_main.Content;

            // Close the newly initialized window
            new_main.Close();
        }
    }
}
