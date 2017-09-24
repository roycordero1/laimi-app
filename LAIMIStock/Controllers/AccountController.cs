using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAIMIStock.Models;

namespace LAIMIStock.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult ChangePassword(Usuarios userModel)
        {
            using (laimistockappEntities db = new laimistockappEntities())
            {
                var userDetails = db.Usuarios.Where(x => x.nombre == userModel.nombre).FirstOrDefault();

                return View();
            }
        }
    }
}