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
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                file.SaveAs(path);

                CategoriasSuministros category = new CategoriasSuministros
                {
                    nombre = model.nombre,
                    descripcion = model.descripcion,
                    imagenURL = baseUrl + "Content/img/" + file.FileName
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
                    fechaCaducidad = model.fechaCaducidad,
                    precio = model.precio,
                    objetoGasto = model.objetoGasto,
                    localizacion = model.localizacion,
                    cantidad = model.cantidad,
                    limiteSuministro = model.limiteSuministro,
                    idCategoria = model.idCategoria,
                    fechaIngreso = System.DateTime.Now
                };

                db.Suministros.Add(supply);
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

        public ActionResult RechargeSupplyForm(Suministros supply)
        {
            laimistockappEntities db = new laimistockappEntities();
            supply.supplies = db.Suministros.Select(x => new SelectListItem
            {
                Value = x.codigo,
                Text = x.nombre
            });

            int selectedValue = supply.selectedSupply;
            if (selectedValue != 0)
            {
                var valoresSuministro = db.Suministros.Where(x => x.idSuministro == selectedValue).FirstOrDefault();
                if (valoresSuministro != null)
                {
                    supply.cantidad = valoresSuministro.cantidad;
                }
                else
                {
                    //Response.Write("<script>alert(" + SelectedValue + ")</script>");
                }

            }

            return View(supply);
        }

        [HttpPost]
        public ActionResult RechargeSupply(Suministros supply)
        {
            laimistockappEntities db = new laimistockappEntities();
            int selectedValueId = supply.selectedSupply;

            if (selectedValueId != 0)
            {
                var supplyDB = db.Suministros.Where(x => x.idSuministro == selectedValueId).FirstOrDefault();
                if (supplyDB != null)
                {
                    supplyDB.cantidad = supplyDB.cantidad + supply.cantidad;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    TempData["msg"] = "<script>alert('¡Recarga exitosa!');</script>";
                }
                else
                {

                    Response.Write("<script>alert(No hay suficiente suministros para consumir)</script>");
                }

                return RedirectToAction("RechargeSupplyForm", "SupplyCategories");

            }
            else
            {
                return RedirectToAction("RechargeSupplyForm", "SupplyCategories");
            }

        }

        public ActionResult DeleteSupplyForm(int id)
        {
            laimistockappEntities db = new laimistockappEntities();
            Suministros supply = db.Suministros.SingleOrDefault(x => x.idSuministro == id);

            return View(supply);
        }

        public ActionResult DeleteSupply(int id)
        {

            laimistockappEntities db = new laimistockappEntities();
            Suministros supply = db.Suministros.SingleOrDefault(x => x.idSuministro == id);

            db.Suministros.Remove(supply);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}