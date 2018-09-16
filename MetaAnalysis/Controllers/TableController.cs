using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MetaAnalysis.Models;


namespace MetaAnalysis.Controllers
{
    public class TableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
     
    }
}

/*
    public IActionResult Index()
    {
        var allCategories = context.Categories.ToList();
        return View(allCategories);
    }*/
