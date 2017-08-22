using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAIMIStock.Models;
using LAIMIStock.ViewModels;

namespace LAIMIStock.Controllers
{
    public class CategoriesController : Controller
    {        

        // GET: Categories
        public ActionResult Index()
        {
            laimistockappEntities db = new laimistockappEntities();
            CategoriasSuministros categorydb = db.CategoriasSuministros.SingleOrDefault(x => x.idCategoriaSuministro == 1);

            
            
            Category category = new Category {categoryName = categorydb.nombre, categoryImage = categorydb.imagenURL};
            
            var categories2 = new List<Category>();
            categories2.Add(category);


            var categories = new List<Category>
            {
                new Category() { categoryName = "Papel", categoryImage = "https://image.freepik.com/iconos-gratis/papel-impreso_318-49898.jpg" },
                new Category() { categoryName = "Toner", categoryImage = "https://image.freepik.com/iconos-gratis/gota-de-tinta_318-53374.jpg" },
                new Category() { categoryName = "3D", categoryImage = "https://image.freepik.com/iconos-gratis/3d-impreso-hoja-de-papel-la-imagen-de-un-cubo-con_318-60115.jpg" }
            };

            var viewModel = new ListCategoriesViewModel
            {
                Categories = categories2
            };
            
            return View(viewModel);
        }

        public ActionResult Random()
        {
            return View();
        }
    }
}