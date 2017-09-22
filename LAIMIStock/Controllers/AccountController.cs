using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LAIMIStock.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}