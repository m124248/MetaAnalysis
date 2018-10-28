using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaAnalysis.Controllers
{
    public class RDashboardController : Controller
    {
        public IActionResult Index()
        {
            //something to run RScript
            return View(); 
        }
    }
}
