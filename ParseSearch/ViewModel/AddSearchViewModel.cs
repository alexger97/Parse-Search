using ParseSearch.Context;
using ParseSearch.Interface;
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
    class AddSearchViewModel : ViewModelBase, IAddSearchViewModel
    {
        public AddSearchViewModel(ISearchService searchService)
        {
            UseGoogleSearhOnly = true;
            SearchService = (SearchService)searchService;
        }
        public SearchService SearchService;

        #region typeSearch
        private bool useYandexSearhOnly;
        public bool UseYandexSearhOnly { get => useYandexSearhOnly; set { useYandexSearhOnly = value; if (value) SetChange(1); OnPropertyChanged("UseYandexSearhOnly"); } }

        private bool useGoogleSearhOnly;
        public bool UseGoogleSearhOnly { get => useGoogleSearhOnly; set { useGoogleSearhOnly = value; if (value) SetChange(2); OnPropertyChanged("UseGoogleSearhOnly"); } }

        private bool useYahooSearhOnly;
        public bool UseYahooSearhOnly { get => useYahooSearhOnly; set { useYahooSearhOnly = value; if (value) SetChange(3); OnPropertyChanged("UseYahooSearhOnly"); } }

        private bool useAllSearch;
        public bool UseAllSearch { get => useAllSearch; set { useAllSearch = value; if (value) SetChange(4); OnPropertyChanged("UseAllSearch"); } }

        private void SetChange(int i)
        {
            if (i == 1) { UseYahooSearhOnly = UseGoogleSearhOnly = UseAllSearch = false; }
            if (i == 2) { UseYandexSearhOnly = UseYahooSearhOnly = UseAllSearch = false; }
            if (i == 3) { UseYandexSearhOnly = UseGoogleSearhOnly = UseAllSearch = false; }
            if (i == 4) { UseYandexSearhOnly = UseGoogleSearhOnly = UseYahooSearhOnly = false; }

        }
        #endregion

        #region RequestData
        private string request;
        public string Request { get => request; set { request = value; OnPropertyChanged("Request"); } }


        private string lastrequest;
        private DateTime lastdateTimerequest;
        bool lastmultisearch;
        TypeOfSeacrhMachine typeOfSeacrhMachine;
        #endregion
        private List<SearchElementResult> searchResults;
        public List<SearchElementResult> SearchResults
        {
            get => searchResults;

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
            lastrequest = Request;
            lastdateTimerequest = DateTime.Now;
            List<SearchElementResult> rez = null;
            if (UseYandexSearhOnly) rez = SearchService.YaSearch(Request);
            if (UseGoogleSearhOnly) rez = SearchService.SearchWithGoogle(Request);
            if (UseYahooSearhOnly) rez = SearchService.YahooSearch(Request);
            if (UseAllSearch)
            {

                var rezult = SearchService.SearchwithAll(Request);
                rez = (List<SearchElementResult>)rezult[0];
                typeOfSeacrhMachine = (TypeOfSeacrhMachine)rezult[1];
                MessageBox.Show($"Самый быстрый ответ поступил от {typeOfSeacrhMachine}");
                lastmultisearch = true;

            }


            if (rez != null)
            {
                if (rez.Count > 0)
                {
                    SearchResults = rez;
                    MessageBox.Show($"Запрос успешно выполнен. Количество результатов: {rez.Count}");
                }
                else
                {
                    MessageBox.Show("Ошибка выполнения ");
                }
            }
            else { MessageBox.Show("Ошибка в получении данных. Возможно поисковик не нашел по вашему запросу данных"); }

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
                if (_clicSave == null)
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
                LocalContext.AddResult(request, SearchResults, lastdateTimerequest, TypeOfSeacrhMachine.Google);
                MessageBox.Show("Данные запроса добавлены в базу");
                Clear();
            }

        }
        public bool CanExecuteClicSave(object parameter)
        {
            if (SearchResults != null)
                if (SearchResults.Count > 0)
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
