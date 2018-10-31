using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MetaAnalysis.Controllers
{
    public class RDashboardController : Controller
    {
        public IActionResult Index()
        {

            //string strCmdLine;
            //strCmdLine = "R CMD BATCH" + "C:\\Users\\cluni\\source\\repos\\MetaAnalysis\\MetaAnalysis\\RScript\\FunnelPlot.R";
            //Process.Start("CMD.exe", strCmdLine);
            //process1.Close();

            //StreamReader ROutput = new StreamReader("C:Users\\cluni\\source\\repos\\MetaAnalysis\\MetaAnalysis\\wwwroot\\html\\funnel.png");

            return View(); 
        }
    }
}
