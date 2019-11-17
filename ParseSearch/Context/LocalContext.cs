using ParseSearch.Model;
using ParseSearch.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseSearch.Context
{
   public  class LocalContext
    {   
        public static LocalRepository  LocalRepository { get; set; }
     
        public static List<SearchResult> SearchResults { get { if (LocalRepository.SearchResults != null) return LocalRepository.SearchResults.Include("SearchElementResults").ToList(); return new List<SearchResult>(); } }

        public static void AddResult (string Request, List<SearchElementResult> searchResults, DateTime time, TypeOfSeacrhMachine typeOfSeacrhMachine)
        {
            LocalRepository.SearchResults.Add(new SearchResult() { Request = Request, SearchElementResults = searchResults, SearchTime = time, TypeOfSeacrhMachine = typeOfSeacrhMachine });
            LocalRepository.SaveChanges();
        }

    }
}
