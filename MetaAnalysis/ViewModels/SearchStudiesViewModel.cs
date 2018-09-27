using MetaAnalysis.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MetaAnalysis.ViewModels
{
    public class SearchStudiesViewModel : BaseViewModel
    {
        //Extract members common to StudyFieldsViewModel to BaseVM

        //The search results
        public List<Study> Studies { get; set; }

        //The column to search, defaults to all
        public StudyFieldType Column { get; set; } = StudyFieldType.All;

        //The search value
        [Display(Name = "Keyword:")]
        public string Value { get; set; } = "";

    }
}