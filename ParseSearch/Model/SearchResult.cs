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

        public string MainText { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public TypeOfSeacrhMachine TypeOfSeacrhMachine { get; set; }


    }
}
