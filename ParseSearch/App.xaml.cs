using ParseSearch.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ParseSearch
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
           
            var MainWindow = new MainWindow() { DataContext = new MainViewModel(new Service.SearchService()) };
            MainWindow.Show();


        }
    }
}
