using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MetaAnalysis.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MetaAnalysis.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            return View();
        }

        // TODO #1 - Create a Results action method to process 
        // search request and display results
        public IActionResult Results(string searchType, string searchTerm)
        {
            ViewBag.columns = ListController.columnChoices;

            List<Dictionary<string, string>> studies = new List<Dictionary<string, string>>();

            if (string.IsNullOrEmpty(searchTerm))
            {
                return View("Index");
            }
            if (searchType.Equals("all"))
            {
                studies = StudyData.FindByValue(searchTerm);
                ViewBag.studies = studies;
                return View("Index");
            }
            else
            {
                studies = StudyData.FindByColumnAndValue(searchType, searchTerm);
                ViewBag.studies = studies;
                return View("Index");
            }
        }
    }
}

