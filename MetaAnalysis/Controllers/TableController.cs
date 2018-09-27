using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MetaAnalysis.Models;
using MetaAnalysis.Data;
using MetaAnalysis.ViewModels;
using System.Linq;

namespace MetaAnalysis.Controllers
{
    public class TableController : Controller
    {
        // Our reference to the data store
        private static StudyData studyData;

        static TableController ()
        {
            studyData = StudyData.GetInstance();
        }

        // Lists options for browsing, by "column"
        public IActionResult Index()
        {
            StudyFieldsViewModel studyFieldsViewModel = new StudyFieldsViewModel();
            studyFieldsViewModel.Title = "View Study Fields";

            return View(studyFieldsViewModel);
        }

        // Lists the values of a given column, or all studiess if selected
        public IActionResult Values(StudyFieldType column)
        {
            if (column.Equals(StudyFieldType.All))
            {
                SearchStudiesViewModel studiesViewModel = new SearchStudiesViewModel();
                studiesViewModel.Studies = studyData.Studies;
                studiesViewModel.Title =  "All Studies";
                return View("Studies", studiesViewModel);
            }
            else
            {
                StudyFieldsViewModel studyFieldsViewModel = new StudyFieldsViewModel();

                IEnumerable<StudyField> fields;

                switch (column)
                {
                    case StudyFieldType.PublicationYear:
                        fields = studyData.PublicationYears.ToList().Cast<StudyField>();
                        break;
                    case StudyFieldType.CorrelationCoefficient:
                        fields = studyData.CorrelationCoefficients.ToList().Cast<StudyField>();
                        break;
                    case StudyFieldType.SampleSize:
                        fields = studyData.SampleSizes.ToList().Cast<StudyField>();
                        break;
                    case StudyFieldType.VariablesControlled:
                        fields = studyData.VariablesControlled.ToList().Cast<StudyField>();
                        break;
                    case StudyFieldType.StudyDesign:
                        fields = studyData.StudyDesigns.ToList().Cast<StudyField>();
                        break;
                    case StudyFieldType.AdherenceMeasure:
                        fields = studyData.AdherenceMeasures.ToList().Cast<StudyField>();
                        break;
                    case StudyFieldType.ConscientiousnessMeasure:
                        fields = studyData.ConscientiousnessMeasures.ToList().Cast<StudyField>();
                        break;
                    case StudyFieldType.MeanAge:
                        fields = studyData.MeanAges.ToList().Cast<StudyField>();
                        break;
                    case StudyFieldType.MethodologicalQuality:
                    default:
                        fields = studyData.MethodologicalQualities.ToList().Cast<StudyField>();
                        break;
                }

                studyFieldsViewModel.Fields = fields;
                studyFieldsViewModel.Title =  "All " + column + " Values";
                studyFieldsViewModel.Column = column;

                return View(studyFieldsViewModel);
            }
        }

        // Lists Studies with a given field matching a given value
        public IActionResult Studies(StudyFieldType column, string value)
        {
            SearchStudiesViewModel studiesViewModel = new SearchStudiesViewModel();
            studiesViewModel.Studies = studyData.FindByColumnAndValue(column, value);
            studiesViewModel.Title = "Studies with " + column + ": " + value;

            return View(studiesViewModel);
        }
    }
}