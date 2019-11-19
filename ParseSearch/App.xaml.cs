using ParseSearch.Context;
using ParseSearch.Repository;
using ParseSearch.Service;
using ParseSearch.View;
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

            CompositionRoot comroot = new CompositionRoot();
            comroot.MakeViewModelService();
            var MainWindow = new MainWindow() { DataContext = new MainViewModel(comroot.MakeAddSearchPage(), comroot.MakeHistorySearchPage())  };
            MainWindow.Show();


        }
    }
}
