using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAIMIStock.Models;
using LAIMIStock.ViewModels;

namespace LAIMIStock.Controllers
{
    public class AssetCategoriesController : Controller
    {
        // GET: AssetCategories
        public ActionResult Index()
        {
            laimistockappEntities db = new laimistockappEntities();
            var categoriesDb = db.CategoriasActivos.DefaultIfEmpty();

            var categoriesList = new List<CategoriasActivos>();
            foreach (var categoryAux in categoriesDb)
            {
                categoriesList.Add(categoryAux);
            }

            var viewModel = new ListCategoriesAViewModel()
            {
                Categories = categoriesList
            };

            return View(viewModel);
        }

        public ActionResult ListAssets(int id)
        {
            laimistockappEntities db = new laimistockappEntities();
            var assetsDb = db.Activos.Where(x => x.idCategoria == id);


            var assetsList = new List<Activos>();
            foreach (var assetAux in assetsDb)
            {
                assetsList.Add(assetAux);
            }

            var viewModel = new ListAssetsViewModel()
            {
                Activos = assetsList
            };

            return View(viewModel);
        }
    }
}