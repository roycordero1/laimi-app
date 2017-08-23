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

        // GET: Categories
        public ActionResult Index(int id)
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
        
    }
}