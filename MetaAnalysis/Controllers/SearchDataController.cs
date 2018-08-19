using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MetaAnalysis.Models;

namespace MetaAnalysis.Controllers
{
    public class SerachDataController : Controller
    {
        internal static Dictionary<string, string> columnChoices = new Dictionary<string, string>();

        static SerachDataController()
        {
            columnChoices.Add("id", "ID");
            columnChoices.Add("PublicationYear", "Publication Year");
            columnChoices.Add("n", "Correlation Coefficien");
            columnChoices.Add("r", "Sample Size");
            columnChoices.Add("Variables controlled", "Variables Controlled");
            columnChoices.Add("Study design", "Study Design");
            columnChoices.Add("Adherence measure", "Adherence Measure");
            columnChoices.Add("Conscientiousness measure", "Conscientiousness Measure");
            columnChoices.Add("Mean age", "Mean Age");
            columnChoices.Add("Methodological quality", "Methodological Quality");
            columnChoices.Add("all", "All");
        }

        public IActionResult Index()
        {
            ViewBag.columns = columnChoices;
            return View();
        }

        public IActionResult Values(string column)
        {
            if (column.Equals("all"))
            {
                List<Dictionary<string, string>> studies = StudyData.FindAll();
                ViewBag.title = "All Studies";
                ViewBag.studies = studies;
                return View("Studies");
            }
            else
            {
                List<string> items = StudyData.FindAll(column);
                ViewBag.title = "All " + columnChoices[column] + " Values";
                ViewBag.column = column;
                ViewBag.items = items;
                return View();
            }
        }

        public IActionResult Studies(string column, string value)
        {
            List<Dictionary<string, string>> studies = StudyData.FindByColumnAndValue(column, value);
            ViewBag.title = "Studies with " + columnChoices[column] + ": " + value;
            ViewBag.studies = studies;

            return View();
        }
    }
}
