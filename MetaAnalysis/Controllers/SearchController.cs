using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MetaAnalysis.ViewModels;
using MetaAnalysis.Services;
using System.Linq;

namespace MetaAnalysis.Controllers
{
    public class SearchController : Controller
    {
        private void LoadChoices(SearchViewModel choices)
        {
            choices.ColumnChoices.Add("id", "ID");
            choices.ColumnChoices.Add("PublicationYear", "Publication Year");
            choices.ColumnChoices.Add("n", "Correlation Coefficient");
            choices.ColumnChoices.Add("r", "Sample Size");
            choices.ColumnChoices.Add("VariablesControlled", "Variables Controlled");
            choices.ColumnChoices.Add("StudyDesign", "Study Design");
            choices.ColumnChoices.Add("AdherenceMeasure", "Adherence Measure");
            choices.ColumnChoices.Add("ConscientiousnessMeasure", "Conscientiousness Measure");
            choices.ColumnChoices.Add("MeanAge", "Mean Age");
            choices.ColumnChoices.Add("MethodologicalQuality", "Methodological Quality");
            choices.ColumnChoices.Add("all", "All");
        }

        public IActionResult Index()
        {
            SearchViewModel choices = new SearchViewModel();

            LoadChoices(choices);


            return View(choices);
        }

        public IActionResult Results (string column, string value)
        {
            var Model = new SearchViewModel();
            LoadChoices(Model);
            if (column == "all" && string.IsNullOrWhiteSpace(value))
            {
                List<Dictionary<string, string>> studies = StudyDataService.FindAll();
                ViewBag.title = "All Studies";
                Model.Studies = studies;
            }
            else if(column != "all" && string.IsNullOrWhiteSpace(value))
            {
                List<string> items = StudyDataService.FindAll(column);
                ViewBag.title = "All " + Model.ColumnChoices[column] + " Values";
                ViewBag.column = column;
                Model.ColumnValues = items;
            }
            //else
            //{
            //    List<string> items = StudyDataService.FindByColumnAndValue(column, value);
            //    ViewBag.title = "Studies with " + Model.ColumnChoices[column] + ": " + value;
            //    ViewBag.studies = studies;
            //}
            return View("Index", Model);
        }
    }
}
