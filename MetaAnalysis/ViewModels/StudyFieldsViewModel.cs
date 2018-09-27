using System.Collections.Generic;
using System.Data;
using MetaAnalysis.Models;

namespace MetaAnalysis.ViewModels
{
    public class StudyFieldsViewModel : BaseViewModel
    {
        public StudyFieldType Column { get; set; }

        public IEnumerable<StudyField> Fields { get; set; } 
    }
}