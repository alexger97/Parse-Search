using ParseSearch.Context;
using ParseSearch.Model;
using ParseSearch.Service;
using ParseSearch.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ParseSearch.ViewModel
{
    class AddSearchViewModel:ViewModelBase
    {
        public AddSearchViewModel(SearchService searchService) 
        {
            UseGoogleSearhOnly = true;
            SearchService = searchService; 
        }
        public SearchService SearchService;

        #region typeSearch
        private bool useYandexSearhOnly;
        public bool UseYandexSearhOnly { get => useYandexSearhOnly; set { useYandexSearhOnly = value; if (value) SetChange(1); OnPropertyChanged("UseYandexSearhOnly"); } }

        private bool useGoogleSearhOnly;
        public bool UseGoogleSearhOnly { get => useGoogleSearhOnly; set { useGoogleSearhOnly = value; if (value) SetChange(2); OnPropertyChanged("UseGoogleSearhOnly"); } }

        private bool useBingSearhOnly;
        public bool UseBingSearhOnly { get => useBingSearhOnly; set { useBingSearhOnly = value; if (value) SetChange(3); OnPropertyChanged("UseBingSearhOnly"); } }

        private bool useAllSearch;
        public bool UseAllSearch { get => useAllSearch; set { useAllSearch = value; if (value) SetChange(4); OnPropertyChanged("UseAllSearch"); } }

        private void SetChange(int i)
        {
            if (i == 1) { UseBingSearhOnly = UseGoogleSearhOnly = UseAllSearch = false; }
            if (i == 2) { UseYandexSearhOnly = UseBingSearhOnly = UseAllSearch = false; }
            if (i == 3) { UseYandexSearhOnly = UseGoogleSearhOnly = UseAllSearch = false; }
            if (i == 4) { UseYandexSearhOnly = UseGoogleSearhOnly = UseBingSearhOnly = false; }

        }
        #endregion

        #region RequestData
        private string request;
        public string Request { get => request; set { request = value; OnPropertyChanged("Request");  } }

       
        private string lastrequest;
        private DateTime lastdateTimerequest;
        #endregion
        private List<SearchElementResult> searchResults;
        public List<SearchElementResult> SearchResults
        {
            get =>  searchResults;

            set
            {
                searchResults = value; OnPropertyChanged("SearchResults");
            }

        }



        RelayCommand _clicSearch;
        public RelayCommand ClicSearch
        {
            get
            {
                if (_clicSearch == null)
                {
                    _clicSearch = new RelayCommand(ExecuteClicSearch, CanExecuteClicSearch);
                }
                return _clicSearch;
            }
        }

        public void ExecuteClicSearch(object parameter)
        {
            request = Request;
            lastdateTimerequest = DateTime.Now;
            var rez= SearchService.SearchGoogle(Request);
            if (rez != null)
                if (rez.Count > 0)
                {
                    SearchResults = rez;
                    MessageBox.Show($"Запрос успешно выполнен. Количество результатов: {rez.Count}");
                }
                else
                {
                    MessageBox.Show("Ошибка выполнения ");
                }
            

            OnPropertyChanged("SearchResults");
        }
        public bool CanExecuteClicSearch(object parameter)
        {
            if (!String.IsNullOrEmpty(Request)) return true;
            return false;
        }


        RelayCommand _clicSave;
        public RelayCommand ClicSave
        {
            get
            {
                if (_clicSave  == null)
                {
                    _clicSave = new RelayCommand(ExecuteClicSave, CanExecuteClicSave);
                }
                return _clicSave;
            }
        }

        public void ExecuteClicSave(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Сохранить данные запроса ?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                LocalContext.AddResult(request,SearchResults, lastdateTimerequest,TypeOfSeacrhMachine.Google);
                MessageBox.Show("Данные запроса добавлены в базу");
                Clear();
            }

        }
        public bool CanExecuteClicSave(object parameter)
        {
            if(SearchResults!=null)
                if (SearchResults.Count>0)
                return true;
            return false;
        }

        public void Clear()
        {
            SetChange(1);
            Request = String.Empty;
            request = string.Empty;
            SearchResults = null;

        }


    }
}
