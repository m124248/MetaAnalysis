using MetaAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaAnalysis.ViewModels
{
    public class SearchViewModel
    {
        public Dictionary<string, string> ColumnChoices = new Dictionary<string, string>();
        
        //TODO: Put Studies Object HERE
        public IEnumerable<Study> Studies { get; set; }

    }
}
