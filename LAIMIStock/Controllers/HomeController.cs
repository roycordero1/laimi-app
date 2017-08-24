using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAIMIStock.Models;
using LAIMIStock.ViewModels;

namespace LAIMIStock.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            laimistockappEntities db = new laimistockappEntities();
            var categoriesDb = db.CategoriasSuministros.DefaultIfEmpty();

            var categoriesList = new List<CategoriasSuministros>();
            foreach (var categoryAux in categoriesDb)
            {
                categoriesList.Add(categoryAux);
            }

            var viewModel = new ListCategoriesSViewModel
            {
                Categories = categoriesList
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}