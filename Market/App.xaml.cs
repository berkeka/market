using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
    }
}
