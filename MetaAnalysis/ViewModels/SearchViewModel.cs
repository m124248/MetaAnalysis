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
        //returns radio button

        public List<Dictionary<string, string>> Studies = new List<Dictionary<string, string>>();
        //put entire row into study, then display that on page

        public List<string> ColumnValues = new List<string>(); 
        //returns all values of columns specified as a search term OR 
        //all values of a column(radio button) if nothing typed
    }
}
