using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ParseSearch.Interface
{
    public interface IAddSearchViewModel
    {

        bool UseYandexSearhOnly { get; set; }

        bool UseGoogleSearhOnly { get; set; }
        bool UseYahooSearhOnly { get; set; }

        bool UseAllSearch { get; set; }

        void Clear();
    }




}
