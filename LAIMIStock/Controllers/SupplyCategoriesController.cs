using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAIMIStock.Models;
using LAIMIStock.ViewModels;

namespace LAIMIStock.Controllers
{
    public class SupplyCategoriesController : Controller
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

        // GET: Categories
        public ActionResult ListSupplies(int id)
        {
            laimistockappEntities db = new laimistockappEntities();
            var suppliesDb = db.Suministros.Where(x => x.idCategoria == id);


            var suppliesList = new List<Suministros>();
            foreach (var supplyAux in suppliesDb)
            {
                suppliesList.Add(supplyAux);
            }

            var viewModel = new ListSuppliesViewModel()
            {
                Suministros = suppliesList
            };

            return View(viewModel);
        }

        public ActionResult UseSupply()
        {
            laimistockappEntities db = new laimistockappEntities();
            return View();
        }
        
    }
}