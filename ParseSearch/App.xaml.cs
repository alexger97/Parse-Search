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

            SearchService searchService = new SearchService();
            AddSearchViewModel addSearchViewModel = new AddSearchViewModel(searchService);
            HistorySearchViewModel historySearchViewModel = new HistorySearchViewModel();

            AddSearchPage addSearchPage = new AddSearchPage() { DataContext = addSearchViewModel };
            HistorySearchPage historySearchPage = new HistorySearchPage() { DataContext = historySearchViewModel };
            var MainWindow = new MainWindow() { DataContext = new MainViewModel(addSearchPage, historySearchPage)  };
            MainWindow.Show();


        }
    }
}
