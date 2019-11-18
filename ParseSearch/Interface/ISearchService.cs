using ParseSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseSearch.Interface
{
    public interface ISearchService
    {
        object[] SearchwithAll(string request);

        List<SearchElementResult> SearchWithGoogle(string request);
        List<SearchElementResult> YaSearch(string request);
        List<SearchElementResult> YahooSearch(string request);
        List<SearchElementResult> ProcessingGoogleRezult(string response);

        List<SearchElementResult> ProcessingYaRezult(string response);

        List<SearchElementResult> ProcessingYahooRezult(string response);

    }
}
