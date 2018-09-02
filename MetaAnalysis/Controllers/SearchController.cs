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
            choices.ColumnChoices.Add("Variables controlled", "Variables Controlled");
            choices.ColumnChoices.Add("Study design", "Study Design");
            choices.ColumnChoices.Add("Adherence measure", "Adherence Measure");
            choices.ColumnChoices.Add("Conscientiousness measure", "Conscientiousness Measure");
            choices.ColumnChoices.Add("Mean age", "Mean Age");
            choices.ColumnChoices.Add("Methodological quality", "Methodological Quality");
            choices.ColumnChoices.Add("all", "All");


            return View(choices);
        }

        public IActionResult Values(string column, SearchViewModel Model)
        {
            if (column.Equals("all"))
            {
                List<Dictionary<string, string>> studies = StudyDataService.FindAll();
                ViewBag.title = "All Studies";
                ViewBag.studies = studies;
                return View("Studies");
            }
            else
            {
                List<string> items = StudyDataService.FindAll(column);
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
