using MetaAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaAnalysis.ViewModels
{
    public class BaseViewModel
    {
        public string Title { get; set; } = "";

        public List<StudyFieldType> Columns { get; set; }

        public BaseViewModel()
        {
            //Populate the list of all columns

            Columns = new List<StudyFieldType>();

            foreach (StudyFieldType enumVal in Enum.GetValues(typeof(StudyFieldType)))
            {
                Columns.Add(enumVal);
            }
        }
    }
}