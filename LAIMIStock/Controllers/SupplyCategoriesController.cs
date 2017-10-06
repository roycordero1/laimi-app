using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

        public ActionResult ChooseAddType()
        {
            return View();
        }

        public ActionResult AddCategoryForm()
        {
            return View();
        }

        public ActionResult AddCategory(CategoriasSuministros model, HttpPostedFileBase file)
        {

            try
            {
                laimistockappEntities db = new laimistockappEntities();

                string path = HttpContext.Server.MapPath("~/Content/img/") + file.FileName;
                file.SaveAs(path);

                CategoriasSuministros category = new CategoriasSuministros
                {
                    nombre = model.nombre,
                    descripcion = model.descripcion,
                    imagenURL = "/Content/img/" + file.FileName
                };

                db.CategoriasSuministros.Add(category);
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");
        }

        public ActionResult AddSupplyForm()
        {
            laimistockappEntities db = new laimistockappEntities();
            List<CategoriasSuministros> list = db.CategoriasSuministros.ToList();
            ViewBag.CategoriesList = new SelectList(list, "IdCategoriaSuministro", "nombre");

            return View();
        }

        public ActionResult AddSupply(Suministros model)
        {

            try
            {
                laimistockappEntities db = new laimistockappEntities();                

                Suministros supply = new Suministros
                {
                    codigo = model.codigo,
                    nombre = model.nombre,
                    descripcion = model.descripcion,
                    precio = model.precio,
                    idCategoria = model.idCategoria
                };

                db.Suministros.Add(supply);
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditSupplyForm(int id)
        {
            laimistockappEntities db = new laimistockappEntities();
            Suministros supply = db.Suministros.SingleOrDefault(x => x.idSuministro == id);

            List<CategoriasSuministros> list = db.CategoriasSuministros.ToList();
            ViewBag.CategoriesList = new SelectList(list, "IdCategoriaSuministro", "nombre");

            return View(supply);
        }

        [HttpPost]
        public ActionResult EditSupply(int idSuministro, Suministros model)
        {

            try
            {
                laimistockappEntities db = new laimistockappEntities();
                Suministros supplyDB = new Suministros();
                supplyDB = db.Suministros.SingleOrDefault(x => x.idSuministro == idSuministro);

                if (supplyDB.codigo != model.codigo |
                    supplyDB.nombre != model.nombre |
                    supplyDB.descripcion != model.descripcion |
                    supplyDB.precio != model.precio |
                    supplyDB.idCategoria != model.idCategoria)
                {
                    supplyDB.codigo = model.codigo;
                    supplyDB.nombre = model.nombre;
                    supplyDB.descripcion = model.descripcion;
                    supplyDB.precio = model.precio;
                    supplyDB.idCategoria = model.idCategoria;
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");
        }

        public ActionResult UseSupply()
        {
            laimistockappEntities db = new laimistockappEntities();
            return View();
        }
    
    }
}