using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAIMIStock.Models;

namespace LAIMIStock.Controllers
{
    public class AlertsController : Controller
    {
        // GET: Alerts
        public ActionResult AlertsView()
        {
            laimistockappEntities db = new laimistockappEntities();
            var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
            ViewBag.alertsDetails = alerts;
            return View();
            
        }
    }
}