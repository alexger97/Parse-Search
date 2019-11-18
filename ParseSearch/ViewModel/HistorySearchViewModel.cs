using ParseSearch.Context;
using ParseSearch.Interface;
using ParseSearch.Model;
using ParseSearch.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseSearch.ViewModel
{
    class HistorySearchViewModel : ViewModelBase, IHistorySearchViewModel
    {

        private string searchText;
        public string SearchText { get => searchText; set { searchText = value; OnPropertyChanged("SearchText"); OnPropertyChanged("SearchResults"); } }

        private SearchResult currentSearchResult;
        public SearchResult CurrentSearchResult { get => currentSearchResult; set { currentSearchResult = value; OnPropertyChanged("CurrentSearchResult"); OnPropertyChanged("SearchElementResults"); } }

        public List<SearchResult> SearchResults
        {
            get
            {
                if (!String.IsNullOrEmpty(SearchText))
                {
                    if (LocalContext.SearchResults.Exists(x => x.Request.Contains(SearchText)))
                        return LocalContext.SearchResults.Where(x => x.Request.Contains(SearchText)).ToList();
                    else return new List<SearchResult>();
                }
                return LocalContext.SearchResults;

            }
        }
                
                
              

        public List<SearchElementResult> SearchElementResults
        {
            get
            {
                if (CurrentSearchResult != null)
                    if (CurrentSearchResult.SearchElementResults != null)
                        return CurrentSearchResult.SearchElementResults;
                return new List<SearchElementResult>();
            }
        }

    }
}
