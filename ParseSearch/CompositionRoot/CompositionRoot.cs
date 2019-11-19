using ParseSearch.Context;
using ParseSearch.Interface;
using ParseSearch.Repository;
using ParseSearch.Service;
using ParseSearch.View;
using ParseSearch.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseSearch 
{
    public class CompositionRoot
    {
        public CompositionRoot ()
        {
            LocalContext.LocalRepository = this.LocalRepository;
        }
             

        private LocalRepository localRepository;
       public LocalRepository LocalRepository
        {
            get
            {
                if (localRepository != null) return localRepository;

                LocalRepository = new LocalRepository();
                return localRepository;
            }
            set { localRepository = value; }
        }

        private ISearchService searchService;

        public ISearchService SearchService
        {
            get
            {
                if (searchService == null)
                    searchService = new SearchService();
                return searchService;
            }
            set { searchService = value; }

        }


        private IAddSearchViewModel addSearchViewModel;

        public IAddSearchViewModel AddSearchViewModel
        {
            get
            {
                if (addSearchViewModel == null)
                    addSearchViewModel = new AddSearchViewModel(SearchService);
                return addSearchViewModel;
            }
            set { addSearchViewModel = value; }

        }


        private IHistorySearchViewModel historySearchViewModel;

        public IHistorySearchViewModel HistorySearchViewModel
        {
            get
            {
                if (historySearchViewModel == null)
                    historySearchViewModel = new HistorySearchViewModel();
                return historySearchViewModel;
            }
            set { historySearchViewModel = value; }

        }

        public void MakeViewModelService() { ViewModelService.AddSearchViewModel = AddSearchViewModel; ViewModelService.HistorySearchViewModel = HistorySearchViewModel; }
        public IView MakeAddSearchPage() => new AddSearchPage() { DataContext = AddSearchViewModel };

        public IView MakeHistorySearchPage() => new HistorySearchPage() { DataContext = HistorySearchViewModel };
    }
}
     

 