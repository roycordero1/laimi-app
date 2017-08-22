using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAIMIStock.Models;
using LAIMIStock.ViewModels;

namespace LAIMIStock.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Usuarios userModel)
        {
            using (laimistockappEntities db = new laimistockappEntities())
            {
                var userDetails = db.Usuarios.Where(x => x.nombre == userModel.nombre && x.password == userModel.password).FirstOrDefault();

                if (userDetails == null)
                {
                    //userModel.LoginErrorMessage = "hola";

                    userModel.LoginErrorMessage = "Usuario o contraseña incorrectos";
                    //System.Diagnostics.Debug.WriteLine(userModel.LoginErrorMessage);
                    return View("Index", userModel);
                }
                else
                {
                    Session["usuarioID"] = userDetails.idUsuario;
                    return RedirectToAction("Index", "Home");
                }
            }

        }
    }
}