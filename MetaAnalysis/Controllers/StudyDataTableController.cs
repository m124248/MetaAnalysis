using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MetaAnalysis.Models;


namespace MetaAnalysis.Controllers
{
    public class StudyDataTableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
