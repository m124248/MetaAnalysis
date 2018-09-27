using Microsoft.AspNetCore.Mvc;
using MetaAnalysis.Models;
using MetaAnalysis.Data;
using MetaAnalysis.ViewModels;

namespace MetaAnalysis.Controllers
{
    public class SearchController : Controller
    {

        // Our reference to the data store
        private static StudyData studyData;

        static SearchController()
        {
            studyData = StudyData.GetInstance();
        }

        // Display the search form
        public IActionResult Index()
        {
            SearchStudiesViewModel studiesViewModel = new SearchStudiesViewModel();
            studiesViewModel.Title = "Search";
            return View(studiesViewModel);
        }

        // Process search submission and display search results
        public IActionResult Results(SearchStudiesViewModel studiesViewModel)
        {

            if (studiesViewModel.Column.Equals(StudyFieldType.All) || studiesViewModel.Value.Equals(""))
            {
                studiesViewModel.Studies = studyData.FindByValue(studiesViewModel.Value);
            }
            else
            {
                studiesViewModel.Studies = studyData.FindByColumnAndValue(studiesViewModel.Column, studiesViewModel.Value);
            }

            studiesViewModel.Title = "Search";

            return View("Index", studiesViewModel);
        }
    }
}
