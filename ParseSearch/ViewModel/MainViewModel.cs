using ParseSearch.Interface;
using ParseSearch.Model;
using ParseSearch.Service;
using ParseSearch.View;
using ParseSearch.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ParseSearch.ViewModel
{
    public class MainViewModel : ViewModelBase 
    {
        public MainViewModel(IView addSearchPage, IView historySearchPage)
        {
            AddSearchPage = addSearchPage;
            HistorySearchPage = historySearchPage;
            CurrentPage = AddSearchPage;
        }

        public IView AddSearchPage { get; set; }

        public IView HistorySearchPage { get; set; }

        private IView currentPage;
        public IView CurrentPage { get => currentPage; set { currentPage = value; OnPropertyChanged("CurrentPage"); } }

        RelayCommand _clicSelect;
        public RelayCommand ClicSelect
        {
            get
            {
                if (_clicSelect == null)
                {
                    _clicSelect = new RelayCommand(ExecuteClicSelect, (o) => true) ;
                }
                return _clicSelect;
            }
        }

        public void ExecuteClicSelect(object parameter)
        {
            int par = System.Convert.ToInt32(parameter);
            if(par==1)CurrentPage = AddSearchPage;
            if(par==2)CurrentPage = HistorySearchPage;


            //SearchService.YaSearch();
            //OnPropertyChanged("SearchResults");
        }
        

    }
}
