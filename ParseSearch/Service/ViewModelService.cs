using ParseSearch.Interface;
using ParseSearch.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseSearch.Service
{
    public class ViewModelService
    {
        public static IAddSearchViewModel AddSearchViewModel {get;set;}

        public static IHistorySearchViewModel HistorySearchViewModel { get; set; }


    }
}
