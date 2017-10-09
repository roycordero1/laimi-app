using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAIMIStock.Models;
using LAIMIStock.ViewModels;

namespace LAIMIStock.Controllers
{
    public class OperatorController : Controller
    {
        // GET: Operator
        public ActionResult OperadorView()
        {
            laimistockappEntities db = new laimistockappEntities();
            IEnumerable<SelectListItem> supplies = db.Suministros.Select(x => new SelectListItem
            {
                Value = x.codigo,
                Text = x.nombre
            });
            ViewBag.Supply = supplies;

            return View();
        }

        public ActionResult loadAvailableAmount (Suministros supply)
        {
            laimistockappEntities db = new laimistockappEntities();
            //var cantidad = db.Suministros.Select();
            return View("OperadorView", supply);
        }

        public ActionResult UseSupply(Suministros supplyModel)
        {
            laimistockappEntities db = new laimistockappEntities();
            return View();
        }
    }

}