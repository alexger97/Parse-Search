using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseSearch.Model
{
  public class SearchResult
    {
        public int Id { get; set; }

        public string Request { get; set; }

        public List<SearchElementResult> SearchElementResults { get; set; }

        public DateTime SearchTime { get; set; }
        public TypeOfSeacrhMachine TypeOfSeacrhMachine { get; set; }
    }
}
