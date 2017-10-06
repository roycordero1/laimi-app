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

        public ActionResult ChooseAddType()
        {
            return View();
        }

        public ActionResult AddCategoryForm()
        {
            return View();
        }

        public ActionResult AddCategory(CategoriasActivos model, HttpPostedFileBase file)
        {

            try
            {
                laimistockappEntities db = new laimistockappEntities();

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
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");
        }

        public ActionResult AddAssetForm()
        {
            laimistockappEntities db = new laimistockappEntities();
            List<CategoriasActivos> list = db.CategoriasActivos.ToList();
            ViewBag.CategoriesList = new SelectList(list, "IdCategoriaActivo", "nombre");

            return View();
        }

        public ActionResult AddAsset(Activos model)
        {

            try
            {
                laimistockappEntities db = new laimistockappEntities();

                Activos asset = new Activos
                {
                    codigo = model.codigo,
                    nombre = model.nombre,
                    descripcion = model.descripcion,
                    precio = model.precio,
                    fechaIngreso = DateTime.Now,
                    fechaExpiracion = model.fechaExpiracion,
                    localizacion = model.localizacion,
                    estado = 1,
                    idCategoria = model.idCategoria
                };

                db.Activos.Add(asset);
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditAssetForm(int id)
        {
            laimistockappEntities db = new laimistockappEntities();
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
                Activos assetDB = new Activos();
                assetDB = db.Activos.SingleOrDefault(x => x.idActivo == idActivo);

                if (assetDB.codigo != model.codigo |
                    assetDB.nombre != model.nombre |
                    assetDB.descripcion != model.descripcion |
                    assetDB.precio != model.precio |
                    assetDB.fechaExpiracion != model.fechaExpiracion |
                    assetDB.localizacion != model.localizacion |
                    assetDB.idCategoria != model.idCategoria)
                {
                    assetDB.codigo = model.codigo;
                    assetDB.nombre = model.nombre;
                    assetDB.descripcion = model.descripcion;
                    assetDB.precio = model.precio;
                    assetDB.fechaExpiracion = model.fechaExpiracion;
                    assetDB.localizacion = model.localizacion;
                    assetDB.idCategoria = model.idCategoria;
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");
        }
    }
}