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

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

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

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            var suppliesDb = db.Suministros.Where(x => x.idCategoria == id);
            var categoryDb = db.CategoriasSuministros.SingleOrDefault(x => x.idCategoriaSuministro == id);


            var suppliesList = new List<Suministros>();
            foreach (var supplyAux in suppliesDb)
            {
                suppliesList.Add(supplyAux);
            }

            var viewModel = new ListSuppliesViewModel()
            {
                Suministros = suppliesList,
                categoryId = id,
                categoryName = categoryDb.nombre
            };

            return View(viewModel);
        }

        public ActionResult ChooseAddType()
        {
            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            return View();
        }

        public ActionResult AddCategoryForm()
        {
            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            return View();
        }

        public ActionResult AddCategory(CategoriasSuministros model, HttpPostedFileBase file)
        {

            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

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

            /*
            * Se añade a bitácora
            */
            var bitacora = db.Set<Bitacora>();
            DateTime fechaHoy = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            bitacora.Add(new Bitacora { nombre = "Nueva categoría suminstro", descripcion = model.nombre, fecha = fechaHoy, idUsuario = 1, idTipoAccion = 3 });
            db.SaveChanges();

            TempData["msg"] = "<p class='status-message'>¡Categoría añadida!</p>";
            return RedirectToAction("AddCategoryForm");
        }

        public ActionResult AddSupplyForm()
        {
            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            List<CategoriasSuministros> list = db.CategoriasSuministros.ToList();
            ViewBag.CategoriesList = new SelectList(list, "IdCategoriaSuministro", "nombre");

            return View();
        }

        [HttpPost]
        public ActionResult AddSupply(Suministros model)
        {

            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

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
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();

            /*
            * Se añade a bitácora
            */
            var bitacora = db.Set<Bitacora>();
            DateTime fechaHoy = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            bitacora.Add(new Bitacora { nombre = "Nuevo suministro", descripcion = model.nombre, fecha = fechaHoy, idUsuario = 1, idTipoAccion = 1 });
            db.SaveChanges();

            TempData["msg"] = "<p class='status-message'>¡Suministro añadido!</p>";
            return RedirectToAction("Index");
        }

        public ActionResult EditSupplyForm(int id)
        {
            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            Suministros supply = db.Suministros.SingleOrDefault(x => x.idSuministro == id);

            List<CategoriasSuministros> list = db.CategoriasSuministros.ToList();
            ViewBag.CategoriesList = new SelectList(list, "IdCategoriaSuministro", "nombre");

            return View(supply);
        }

        [HttpPost]
        public ActionResult EditSupply(int idSuministro, Suministros model)
        {


            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            Suministros supplyDB = new Suministros();
            supplyDB = db.Suministros.SingleOrDefault(x => x.idSuministro == idSuministro);

            if (supplyDB.codigo != model.codigo |
                supplyDB.nombre != model.nombre |
                supplyDB.descripcion != model.descripcion |
                supplyDB.fechaCaducidad != model.fechaCaducidad |
                supplyDB.precio != model.precio |
                supplyDB.objetoGasto != model.objetoGasto |
                supplyDB.localizacion != model.localizacion |
                supplyDB.cantidad != model.cantidad |
                supplyDB.limiteSuministro != model.limiteSuministro |
                supplyDB.idCategoria != model.idCategoria)
            {
                supplyDB.codigo = model.codigo;
                supplyDB.nombre = model.nombre;
                supplyDB.descripcion = model.descripcion;
                supplyDB.fechaCaducidad = model.fechaCaducidad;
                supplyDB.precio = model.precio;
                supplyDB.objetoGasto = model.objetoGasto;
                supplyDB.localizacion = model.localizacion;
                supplyDB.cantidad = model.cantidad;
                supplyDB.limiteSuministro = model.limiteSuministro;
                supplyDB.idCategoria = model.idCategoria;
                db.SaveChanges();

                /*
                * Se añade a bitácora
                */
                var bitacora = db.Set<Bitacora>();
                DateTime fechaHoy = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bitacora.Add(new Bitacora { nombre = "Editar suministro", descripcion = model.nombre, fecha = fechaHoy, idUsuario = 1, idTipoAccion = 5 });
                db.SaveChanges();
            }

            TempData["msg"] = "<p class='status-message'>¡Suministro editado!</p>";
            return RedirectToAction("Index");
        }

        public ActionResult RechargeSupplyForm(Suministros supply)
        {
            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            supply.supplies = db.Suministros.Select(x => new SelectListItem
            {
                Value = x.idSuministro.ToString(),
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

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

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

                    /*
                    * Se añade a bitácora
                    */
                    var bitacora = db.Set<Bitacora>();
                    DateTime fechaHoy = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    bitacora.Add(new Bitacora { nombre = "Agregar suministro", descripcion = supply.nombre, fecha = fechaHoy, idUsuario = 1, idTipoAccion = 8 });
                    db.SaveChanges();
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

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            Suministros supply = db.Suministros.SingleOrDefault(x => x.idSuministro == id);

            return View(supply);
        }

        public ActionResult DeleteSupply(int id)
        {

            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            Suministros supply = db.Suministros.SingleOrDefault(x => x.idSuministro == id);

            db.Suministros.Remove(supply);
            db.SaveChanges();

            /*
            * Se añade a bitácora
            */
            var bitacora = db.Set<Bitacora>();
            DateTime fechaHoy = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            bitacora.Add(new Bitacora { nombre = "Eliminar suministro", descripcion = supply.nombre, fecha = fechaHoy, idUsuario = 1, idTipoAccion = 9 });
            db.SaveChanges();

            TempData["msg"] = "<p class='status-message'>¡Suministro eliminado!</p>";
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategoryForm(int id)
        {
            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            CategoriasSuministros category = db.CategoriasSuministros.SingleOrDefault(x => x.idCategoriaSuministro == id);

            return View(category);
        }

        public ActionResult DeleteCategory(int id)
        {

            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            CategoriasSuministros category = db.CategoriasSuministros.SingleOrDefault(x => x.idCategoriaSuministro == id);

            db.CategoriasSuministros.Remove(category);

            var suppliesDb = db.Suministros.Where(x => x.idCategoria == id);            
            foreach (var supplyAux in suppliesDb)
            {
                db.Suministros.Remove(supplyAux);
            }

            db.SaveChanges();

            /*
            * Se añade a bitácora
            */
            var bitacora = db.Set<Bitacora>();
            DateTime fechaHoy = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            bitacora.Add(new Bitacora { nombre = "Eliminar categoria suministro", descripcion = category.nombre, fecha = fechaHoy, idUsuario = 1, idTipoAccion = 11 });
            db.SaveChanges();

            TempData["msg"] = "<p class='status-message'>¡Categoría eliminada!</p>";
            return RedirectToAction("Index");
        }

        public ActionResult EditCategoryForm(int id)
        {
            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            CategoriasSuministros category = db.CategoriasSuministros.SingleOrDefault(x => x.idCategoriaSuministro == id);

            return View(category);
        }

        public ActionResult EditCategory(CategoriasSuministros model, HttpPostedFileBase file)
        {

            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            CategoriasSuministros categoryDB = new CategoriasSuministros();
            categoryDB = db.CategoriasSuministros.SingleOrDefault(x => x.idCategoriaSuministro == model.idCategoriaSuministro);
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

            if (file != null) {
                string path = HttpContext.Server.MapPath("~/Content/img/") + file.FileName;
                if (categoryDB.nombre != model.nombre |
                categoryDB.descripcion != model.descripcion |
                categoryDB.imagenURL != baseUrl + "Content/img/" + file.FileName)
                {

                    categoryDB.nombre = model.nombre;
                    categoryDB.descripcion = model.descripcion;
                    categoryDB.imagenURL = baseUrl + "Content/img/" + file.FileName;

                    db.SaveChanges();

                    if (categoryDB.imagenURL != baseUrl + "Content/img/" + file.FileName)
                    {
                        file.SaveAs(path);
                    }

                    /*
                    * Se añade a bitácora
                    */
                    var bitacora = db.Set<Bitacora>();
                    DateTime fechaHoy = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    bitacora.Add(new Bitacora { nombre = "Editar categoría suminisitro", descripcion = model.nombre, fecha = fechaHoy, idUsuario = 1, idTipoAccion = 13 });
                    db.SaveChanges();
                }
            }
            else {
                if (categoryDB.nombre != model.nombre |
                categoryDB.descripcion != model.descripcion)
                {

                    categoryDB.nombre = model.nombre;
                    categoryDB.descripcion = model.descripcion;

                    db.SaveChanges();                    

                    /*
                    * Se añade a bitácora
                    */
                    var bitacora = db.Set<Bitacora>();
                    DateTime fechaHoy = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    bitacora.Add(new Bitacora { nombre = "Editar categoría suministro", descripcion = model.nombre, fecha = fechaHoy, idUsuario = 1, idTipoAccion = 13 });
                    db.SaveChanges();
                }
            }

            TempData["msg"] = "<p class='status-message'>¡Categoría editada!</p>";
            return RedirectToAction("Index");
        }
    }
}