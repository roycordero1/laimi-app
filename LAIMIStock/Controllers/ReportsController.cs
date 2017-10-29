using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAIMIStock.Models;
using System.Diagnostics;

namespace LAIMIStock.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult ReportTotalValue()
        {
            laimistockappEntities db = new laimistockappEntities();
            var reportDetails = db.reportTotalSupply().FirstOrDefault();
            //Debug.WriteLine("Holaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            Debug.WriteLine(reportDetails);
            ViewBag.TotalValue = reportDetails;
            return View();
        }

        public ActionResult ReportAmountInventory()
        {
            laimistockappEntities db = new laimistockappEntities();
            var reportDetails = db.reportTotalSupply().ToList();
            ViewBag.AmountInventory = reportDetails;
            return View();

        }

        public ActionResult ReportAssetsState()
        {
            return View();
        }

        public ActionResult ReportBlog()
        {
            return View();
        }

        public ActionResult ReportSuppliesPerLab()
        {
            return View();
        }

        public ActionResult ReportSupplyConsuption()
        {
            return View();
        }
    }
}