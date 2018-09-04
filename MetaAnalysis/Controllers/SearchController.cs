using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MetaAnalysis.ViewModels;
using MetaAnalysis.Services;
using System.Linq;

namespace MetaAnalysis.Controllers
{
    public class SearchController : Controller
    {
    

        public IActionResult Index()
        {
            SearchViewModel choices = new SearchViewModel();

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


            return View(choices);
        }

        public IActionResult Values(string column, SearchViewModel Model)
        {
            if (column.Equals("all"))
            {
                Model.Studies = StudyDataService.FindAll();
                ViewBag.title = "All Studies";

                return View("Studies");
            }
            else
            {
                IEnumerable<string> items = StudyDataService.FindAll(column);
                ViewBag.title = "All " + Model.ColumnChoices[column] + " Values";
                ViewBag.column = column;
                ViewBag.items = items;
                return View();
            }
        }

        public IActionResult Studies(string column, string value, SearchViewModel Model)
        {
            List<Dictionary<string, string>> studies = StudyDataService.FindByColumnAndValue(column, value);
            ViewBag.title = "Studies with " + Model.ColumnChoices[column] + ": " + value;
            ViewBag.studies = studies;

            return View();
        }
    }
}
