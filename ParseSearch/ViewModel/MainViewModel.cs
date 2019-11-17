using ParseSearch.Model;
using ParseSearch.Service;
using ParseSearch.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseSearch.ViewModel
{
    public class MainViewModel : ViewModelBase 
    {
        public MainViewModel(SearchService searchService) => SearchService = searchService;
        public SearchService SearchService;

        #region typeSearch
        private bool useYandexSearhOnly;
        public  bool UseYandexSearhOnly { get => useYandexSearhOnly; set { useYandexSearhOnly = value;if (value) SetChange(1); OnPropertyChanged("UseYandexSearhOnly"); } }
      
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


        public List<SearchResult> SearchResults 
        {
            get
            {
              return   SearchService.SearchResults;
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

            SearchService.SearchGoogle();
            OnPropertyChanged("SearchResults");
        }
        public bool CanExecuteClicSearch(object parameter)
        {
            return true;
        }


    }
}
