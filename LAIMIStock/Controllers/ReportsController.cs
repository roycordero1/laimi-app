﻿using System;
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
            ViewBag.TotalValue = reportDetails;
            return View();
        }

        public ActionResult ReportAmountInventory()
        {
            laimistockappEntities db = new laimistockappEntities();
            var reportDetails = db.Suministros.ToList();
            ViewBag.AmountInventory = reportDetails;
            return View();

        }

        public ActionResult ReportAssetsState()
        {
            laimistockappEntities db = new laimistockappEntities();
            var reportDetails = db.Activos.ToList();
            ViewBag.AssetsState = reportDetails;
            return View();
        }

        public ActionResult ReportBlog()
        {
            laimistockappEntities db = new laimistockappEntities();
            var reportDetails = db.Bitacora.ToList();
            ViewBag.Log = reportDetails;
            return View();
        }

        public ActionResult ReportSuppliesPerLab()
        {
            laimistockappEntities db = new laimistockappEntities();
            var reportDetails = db.Suministros.ToList();
            ViewBag.SupplyPerLab = reportDetails;
            return View();
        }

        public ActionResult ReportSupplyConsuption()
        {
            return View();
        }

        public ActionResult GenerateReport()
        {
            laimistockappEntities db = new laimistockappEntities();
            var reportDetails = db.Bitacora.Where(x => x.fecha >= x.fechaInicio && x.fecha <= x.fechaFin);
            Debug.WriteLine(reportDetails);
            ViewBag.ConsumoSuministro = reportDetails;
            return View("ReportSupplyConsuption");
        }
    }
}