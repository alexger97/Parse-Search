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

           
            LocalRepository repositoryContext = new LocalRepository();
           /* repositoryContext.SearchResults.Add(new Model.SearchResult() { Request = "арбуз", SearchTime = DateTime.Now, TypeOfSeacrhMachine = Model.TypeOfSeacrhMachine.Google, SearchElementResults=new List<Model.SearchElementResult>() 
            { new Model.SearchElementResult(){ Description="Первое описание", Link="hh.hh", MainText="Купить арбуз не дорого" },
            new Model.SearchElementResult(){ Description="Второе описание", Link="uy.tu", MainText=" арбуз не дорого" },
            new Model.SearchElementResult(){ Description="Третье описание", Link="zd.rt", MainText="Купить   не дорого" },
            new Model.SearchElementResult(){ Description="Четвертое описание", Link="yyt.com", MainText="  арбуз арбузарбуз арбуз" },
            } });
            repositoryContext.SearchResults.Add(new Model.SearchResult()
            {
                Request = "баян",
                SearchTime = DateTime.Now,
                TypeOfSeacrhMachine = Model.TypeOfSeacrhMachine.Google,
                SearchElementResults = new List<Model.SearchElementResult>()
            { new Model.SearchElementResult(){ Description="Первое описание", Link="hh.hh", MainText="Купить баян не дорого" },
            new Model.SearchElementResult(){ Description="Второе описание", Link="uy.tu", MainText=" баян не дорого" },
            new Model.SearchElementResult(){ Description="Третье описание", Link="zd.rt", MainText="Купить   не дорого баян" },
            new Model.SearchElementResult(){ Description="Четвертое описание", Link="yyt.com", MainText="  баян баянбаян баян" },
            }
            });
            repositoryContext.SaveChanges();*/


            LocalContext.LocalRepository = repositoryContext;


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
