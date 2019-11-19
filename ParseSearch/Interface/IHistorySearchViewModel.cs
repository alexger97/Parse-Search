using ParseSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseSearch.Interface
{
    public interface IHistorySearchViewModel
    {
        List<SearchResult> SearchResults { get; }
        List<SearchElementResult> SearchElementResults { get; }

        void Update();
    }
}
