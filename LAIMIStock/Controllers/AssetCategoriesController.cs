using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAIMIStock.Models;
using LAIMIStock.ViewModels;
using System.Data.Entity.Validation;

namespace LAIMIStock.Controllers
{
    public class AssetCategoriesController : Controller
    {        
        public ActionResult Index()
        {
            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

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

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            var assetsDb = db.Activos.Where(x => x.idCategoria == id);
            var categoryDb = db.CategoriasActivos.SingleOrDefault(x => x.idCategoriaActivo == id);


            var assetsList = new List<Activos>();
            foreach (var assetAux in assetsDb)
            {
                assetsList.Add(assetAux);
            }

            var viewModel = new ListAssetsViewModel()
            {
                Activos = assetsList,
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

        public ActionResult AddCategory(CategoriasActivos model, HttpPostedFileBase file)
        {

            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;      

            string path = HttpContext.Server.MapPath("~/Content/img/") + file.FileName;
            file.SaveAs(path);

            CategoriasActivos category = new CategoriasActivos
            {
                nombre = model.nombre,
                descripcion = model.descripcion,
                imagenURL = "/Content/img/" + file.FileName
            };

            db.CategoriasActivos.Add(category);
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();

            /*
            * Se añade a bitácora
            */               
            var bitacora = db.Set<Bitacora>();
            DateTime fechaHoy = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            bitacora.Add(new Bitacora { nombre = "Nueva categoría activo", descripcion = model.nombre, fecha = fechaHoy, idUsuario = 1, idTipoAccion = 4 });
            db.SaveChanges();
            

            TempData["msg"] = "<p class='status-message'>¡Categoría añadida!</p>";
            return RedirectToAction("AddCategoryForm");
        }

        public ActionResult AddAssetForm()
        {
            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            List<CategoriasActivos> list = db.CategoriasActivos.ToList();
            ViewBag.CategoriesList = new SelectList(list, "IdCategoriaActivo", "nombre");

            return View();
        }

        public ActionResult AddAsset(Activos model)
        {
            
            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            Activos asset = new Activos
            {
                codigo = model.codigo,
                nombre = model.nombre,
                descripcion = model.descripcion,
                precio = model.precio,
                fechaIngreso = System.DateTime.Now,
                fechaExpiracion = model.fechaExpiracion,
                localizacion = model.localizacion,
                estado = 1,
                idCategoria = model.idCategoria
            };

            db.Activos.Add(asset);
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();

            /*
            * Se añade a bitácora
            */
            var bitacora = db.Set<Bitacora>();
            DateTime fechaHoy = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            bitacora.Add(new Bitacora { nombre = "Nuevo activo", descripcion = model.nombre, fecha = fechaHoy, idUsuario = 1, idTipoAccion = 2 });
            db.SaveChanges();

            TempData["msg"] = "<p class='status-message'>¡Activo añadido!</p>";
            return RedirectToAction("Index");
        }

        public ActionResult EditAssetForm(int id)
        {
            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            Activos asset = db.Activos.SingleOrDefault(x => x.idActivo == id);

            List<CategoriasActivos> list = db.CategoriasActivos.ToList();
            ViewBag.CategoriesList = new SelectList(list, "IdCategoriaActivo", "nombre");

            return View(asset);
        }

        [HttpPost]
        public ActionResult EditAsset(int idActivo, Activos model)
        {

            try
            {
                laimistockappEntities db = new laimistockappEntities();

                /*
                * Revisar cantidad de alertas
                */
                var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
                Session["Alerts"] = alerts.Count;

                Activos assetDB = new Activos();
                assetDB = db.Activos.SingleOrDefault(x => x.idActivo == idActivo);

                if (assetDB.codigo != model.codigo |
                    assetDB.nombre != model.nombre |
                    assetDB.descripcion != model.descripcion |
                    assetDB.precio != model.precio |
                    assetDB.fechaExpiracion != model.fechaExpiracion |
                    assetDB.localizacion != model.localizacion |
                    assetDB.estado != model.estado |
                    assetDB.idCategoria != model.idCategoria)
                {
                    assetDB.codigo = model.codigo;
                    assetDB.nombre = model.nombre;
                    assetDB.descripcion = model.descripcion;
                    assetDB.precio = model.precio;
                    assetDB.fechaExpiracion = model.fechaExpiracion;
                    assetDB.localizacion = model.localizacion;
                    assetDB.estado = model.estado;
                    assetDB.idCategoria = model.idCategoria;
                    db.SaveChanges();
                    /*
                    * Se añade a bitácora
                    */

                    var bitacora = db.Set<Bitacora>();
                    DateTime fechaHoy = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    bitacora.Add(new Bitacora { nombre = "Editar activo", descripcion = model.nombre, fecha = fechaHoy, idUsuario = 1, idTipoAccion = 6 });
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            TempData["msg"] = "<p class='status-message'>¡Activo editado!</p>";
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAssetForm(int id)
        {
            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            Activos asset = db.Activos.SingleOrDefault(x => x.idActivo == id);

            return View(asset);
        }

        public ActionResult DeleteAsset(int id)
        {

            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            Activos asset = db.Activos.SingleOrDefault(x => x.idActivo == id);

            db.Activos.Remove(asset);
            db.SaveChanges();
            
            /*
            * Se añade a bitácora
            */
            var bitacora = db.Set<Bitacora>();
            DateTime fechaHoy = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            bitacora.Add(new Bitacora { nombre = "Eliminar activo", descripcion = asset.nombre, fecha = fechaHoy, idUsuario = 1, idTipoAccion = 10 });
            db.SaveChanges();

            TempData["msg"] = "<p class='status-message'>¡Activo eliminado!</p>";
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

            CategoriasActivos category = db.CategoriasActivos.SingleOrDefault(x => x.idCategoriaActivo == id);

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

            CategoriasActivos category = db.CategoriasActivos.SingleOrDefault(x => x.idCategoriaActivo == id);

            db.CategoriasActivos.Remove(category);

            var assetsDb = db.Activos.Where(x => x.idCategoria == id);
            foreach (var assetAux in assetsDb)
            {
                db.Activos.Remove(assetAux);
            }

            db.SaveChanges();

            /*
            * Se añade a bitácora
            */
            var bitacora = db.Set<Bitacora>();
            DateTime fechaHoy = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            bitacora.Add(new Bitacora { nombre = "Eliminar categoria activo", descripcion = category.nombre, fecha = fechaHoy, idUsuario = 1, idTipoAccion = 12 });
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

            CategoriasActivos category = db.CategoriasActivos.SingleOrDefault(x => x.idCategoriaActivo == id);

            return View(category);
        }

        public ActionResult EditCategory(CategoriasActivos model, HttpPostedFileBase file)
        {

            laimistockappEntities db = new laimistockappEntities();

            /*
            * Revisar cantidad de alertas
            */
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            Session["Alerts"] = alerts.Count;

            CategoriasActivos categoryDB = new CategoriasActivos();
            categoryDB = db.CategoriasActivos.SingleOrDefault(x => x.idCategoriaActivo == model.idCategoriaActivo);
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

            if (file != null)
            {
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
                    bitacora.Add(new Bitacora { nombre = "Editar categoría activo", descripcion = model.nombre, fecha = fechaHoy, idUsuario = 1, idTipoAccion = 14 });
                    db.SaveChanges();
                }
            }
            else
            {
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
                    bitacora.Add(new Bitacora { nombre = "Editar categoría activo", descripcion = model.nombre, fecha = fechaHoy, idUsuario = 1, idTipoAccion = 14 });
                    db.SaveChanges();
                }
            }

            TempData["msg"] = "<p class='status-message'>¡Categoría editada!</p>";
            return RedirectToAction("Index");
        }
    }
}