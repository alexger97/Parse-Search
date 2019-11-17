using ParseSearch.Model;
using ParseSearch.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseSearch.ViewModel
{
    class HistorySearchViewModel:ViewModelBase
    {

        private string searchText;
        public string SearchText { get => searchText; set { searchText = value;OnPropertyChanged("SearchText"); } }

        private SearchResult currentSearchResult;
        public SearchResult CurrentSearchResult { get => currentSearchResult; set { currentSearchResult = value;OnPropertyChanged("CurrentSearchResult"); OnPropertyChanged("SearchElementResults"); } }

        public List<SearchResult> SearchResults;

        public List<SearchElementResult> SearchElementResults { get {
                if (CurrentSearchResult != null)
                    if (CurrentSearchResult.SearchElementResults != null)
                        return CurrentSearchResult.SearchElementResults;
                return new List<SearchElementResult>();
                    } }

    }
}
